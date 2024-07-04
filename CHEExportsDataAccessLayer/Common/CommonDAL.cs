using CHEExportsDataObjects;
using Google.Protobuf.WellKnownTypes;
using CHEExportsProto;
using Grpc.Core;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using System.Runtime.InteropServices;

namespace CHEExportsDataAccessLayer
{
    public static class CommonDAL
    {
       //public static readonly string DBConnection = "server=localhost;database=ENTITY;Integrated Security=true;TrustServerCertificate=true;";
         public static readonly string DBConnection = "Server=SG2NWPLS19SQL-v09.mssql.shr.prod.sin2.secureserver.net;Database=chennaiexports;uid=jjtraders;pwd=jChennaij19#;TrustServerCertificate=True;";

        public static List<SubConfig> GetAllSubConfigValueByConfigID(string config_ids)
        {
            List<SubConfig> lstSubConfig = new List<SubConfig>();
            if (!string.IsNullOrWhiteSpace(config_ids))
            {
                string[] configIdsArray = config_ids.Split(',');
                string configIdsJson = JsonSerializer.Serialize(configIdsArray);
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("ADM_SP_GetSubConfigValuesByConfigid", new string[] { "@M_CONFIG_IDs" }, new string[] { configIdsJson });

                if (lDataSet != null && lDataSet.Tables.Count > 0 && lDataSet.Tables[0] != null && lDataSet.Tables[0].Rows.Count > 0)
                {
                    lstSubConfig = SetListFromDataTable<SubConfig>(lDataSet.Tables[0]);
                }
            }
            return lstSubConfig;

        }

        public static List<T> SelectDataFromDataBase<T>(string[] columnnames, string[] operators, object[] values)
        {
            List<T> lst = new List<T>();
            try
            {
                T obj = Activator.CreateInstance<T>();
                if (obj != null && (columnnames.Length == operators.Length) && (operators.Length == values.Length))
                {
                    string TABLE_NAME = obj.GetType().GetProperty("TABLE_NAME").GetValue(obj).ToString();
                    if (!string.IsNullOrEmpty(TABLE_NAME))
                    {
                        string query = GenaerateSelectQuery(TABLE_NAME, columnnames, operators);
                        SqlConnection conn = new SqlConnection(DBConnection);
                        conn.Open();
                        SqlCommand command = new SqlCommand(query, conn);
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (values[i] != null)
                            {
                                command.Parameters.AddWithValue("@" + columnnames[i], values[i]);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@" + columnnames[i], DBNull.Value);
                            }
                        }
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj = Activator.CreateInstance<T>();
                                foreach (var var in obj.GetType().GetFields())
                                {
                                    if (var.Name.Contains("column_name"))
                                    {
                                        foreach (var var1 in obj.GetType().GetProperties())
                                        {
                                            if (var1.Name.ToLower() == var.GetValue(obj).ToString().ToLower())
                                            {
                                                if (reader[var.GetValue(obj).ToString()] != DBNull.Value)
                                                {
                                                    obj.GetType().GetProperty(var1.Name).SetValue(obj, reader[var.GetValue(obj).ToString()]);
                                                }
                                                else
                                                {
                                                    obj.GetType().GetProperty(var1.Name).SetValue(obj, null);
                                                }
                                            }
                                        }
                                    }
                                }
                                lst.Add(obj);
                            }
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            return lst;
        }

        private static string GenaerateSelectQuery(string TABLE_NAME, string[] columnnames, string[] operators)
        {
            string query = string.Empty;
            try
            {
                query = "select * from " + TABLE_NAME;
                if (columnnames.Length == operators.Length)
                {
                    if (operators.Length > 0)
                    {
                        query = query + " where ";
                        for (int i = 0; i < columnnames.Length; i++)
                        {
                            query += columnnames[i] + " " + operators[i] + " " + "@" + columnnames[i] + " and ";
                        }
                        query = query.Remove(query.Length - 4);
                    }
                    query = query + ";";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            return query;
        }

        public static class Encryption
        {
            public static string Encrypt(string clearText)
            {
                string EncryptionKey = "MAKV2SPBNI99212";
                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                    }
                }
                return clearText;
            }

            public static string Decrypt(string cipherText)
            {
                string EncryptionKey = "MAKV2SPBNI99212";
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                return cipherText;
            }
        }
        public static List<T> SetListFromDataTable<T>(DataTable dt)
        {
            List<T> list = new List<T>();
            try
            {
                T obj = Activator.CreateInstance<T>();
                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        obj = Activator.CreateInstance<T>();
                        foreach (var var in obj.GetType().GetFields())
                        {
                            if (var.Name.Contains("column_name"))
                            {
                                foreach (var var1 in obj.GetType().GetProperties())
                                {
                                    if (var1.Name.ToLower() == var.GetValue(obj).ToString().ToLower())
                                    {
                                        if (row[var.GetValue(obj).ToString()] != DBNull.Value)
                                        {
                                            obj.GetType().GetProperty(var1.Name).SetValue(obj, row[var.GetValue(obj).ToString()]);
                                        }
                                        else
                                        {
                                            obj.GetType().GetProperty(var1.Name).SetValue(obj, null);
                                        }
                                    }
                                }
                            }
                        }
                        list.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            return list;
        }

        public static DataSet GetDataSetbyExecuteSP(string SP_NAME, string[] parameters, object[] values)
        {
            DataSet ds = new DataSet();
            try
            {
                if (parameters.Length == values.Length)
                {
                    SqlConnection conn = new SqlConnection(DBConnection);
                    conn.Open();
                    SqlCommand command = new SqlCommand(SP_NAME, conn);
                    command.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (values[i] != null)
                        {
                            command.Parameters.AddWithValue(parameters[i], values[i]);
                        }
                        else
                        {
                            command.Parameters.AddWithValue(parameters[i], DBNull.Value);
                        }
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            return ds;
        }

        public static LoggedInUserDetails GetLoggedInDetailsFromToken(string token)

        {
            LoggedInUserDetails loggedInUserDetails = new LoggedInUserDetails();
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    string json = Encryption.Decrypt(token);

                    loggedInUserDetails = JsonSerializer.Deserialize<LoggedInUserDetails>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            return loggedInUserDetails;
        }
        public static entDDL GetSubConfigValues(this entDDL DDL, string Key, int ConfigId)
        {
            DDL.Key = Key;
            List<entDDLClass> ilstentDDLClass = GetSubConfigValues(ConfigId);
            foreach (entDDLClass lentDDLClass in ilstentDDLClass)
            {
                DDL.Value.Add(lentDDLClass);
            }
            return DDL;
        }
        public static List<entDDLClass> GetSubConfigValues(int ConfigId)
        {
            List<entDDLClass> ilst = new List<entDDLClass>();
            List<SubConfig> lSubConfig = new List<SubConfig>();
            lSubConfig = CommonDAL.SelectDataFromDataBase<SubConfig>(new string[] { "CUSTOMER_ID" }, new string[] { "=" },
                new object[] { ConfigId }).ToList();

            if (lSubConfig != null && lSubConfig.Count > 0)
            {
                foreach (SubConfig obj in lSubConfig)
                {
                    entDDLClass lDDLClass = new entDDLClass();
                    lDDLClass.Id = obj.m_config_id;
                    lDDLClass.Code = obj.s_config_value;
                    lDDLClass.Constant = obj.s_config_value;
                    lDDLClass.Description = obj.s_config_description;
                    ilst.Add(lDDLClass);
                }
            }
            return ilst;
        }
        public static entDDL GetAllCustomer(this entDDL DDL, string Key, int ConfigId)
        {
            DDL.Key = Key;
            List<entDDLClass> ilstentDDLClass = GetAllCustomer(ConfigId);
            foreach (entDDLClass lentDDLClass in ilstentDDLClass)
            {
                DDL.Value.Add(lentDDLClass);
            }
            return DDL;
        }
        public static List<entDDLClass> GetAllCustomer(int ConfigId)
        {
            List<entDDLClass> ilst = new List<entDDLClass>();
            List<Customer> lCustomer = new List<Customer>();
            lCustomer = CommonDAL.SelectDataFromDataBase<Customer>(new string[] { "CUSTOMER_ID" }, new string[] { ">" },
                new object[] { ConfigId }).OrderByDescending(x => x.customer_id).ToList();

            if (lCustomer != null && lCustomer.Count > 0)
            {
                foreach (Customer obj in lCustomer)
                {
                    entDDLClass lDDLClass = new entDDLClass();
                    lDDLClass.Id = obj.customer_id;
                    lDDLClass.Code = obj.email_id;
                    lDDLClass.Constant = obj.contact_no;
                    lDDLClass.Name = obj.customer_name;
                    lDDLClass.RefNo = obj.customer_ref_no;
                    lDDLClass.Description = string.Join(",", obj.customer_name, CommonDAL.ConcatAddressLine(obj.address_line_1, obj.address_line_2, obj.address_line_3));

                    ilst.Add(lDDLClass);
                }
            }
            return ilst;
        }

        public static entDDL GetAllWareHouse(this entDDL DDL, string Key, int ConfigId)
        {
            DDL.Key = Key;
            List<entDDLClass> ilstentDDLClass = GetAllWareHouse(ConfigId);
            foreach (entDDLClass lentDDLClass in ilstentDDLClass)
            {
                DDL.Value.Add(lentDDLClass);
            }
            return DDL;
        }
        public static List<entDDLClass> GetAllWareHouse(int ConfigId)
        {
            List<entDDLClass> ilst = new List<entDDLClass>();
            List<WareHouse> lWareHouse = new List<WareHouse>();
            lWareHouse = CommonDAL.SelectDataFromDataBase<WareHouse>(new string[] { "WAREHOUSE_ID" }, new string[] { ">" },
                new object[] { ConfigId }).OrderByDescending(x => x.warehouse_id).ToList();

            if (lWareHouse != null && lWareHouse.Count > 0)
            {
                foreach (WareHouse obj in lWareHouse)
                {
                    entDDLClass lDDLClass = new entDDLClass();
                    lDDLClass.Id = obj.warehouse_id;
                    lDDLClass.Code = obj.contact_person_no;
                    lDDLClass.RefNo = obj.warehouse_ref_no;
                    lDDLClass.Name = obj.warehouse_name;
                    lDDLClass.Description = string.Join(",", obj.warehouse_name, CommonDAL.ConcatAddressLine(obj.address_line_1, obj.address_line_2, obj.address_line_3));
                    ilst.Add(lDDLClass);
                }
            }
            return ilst;
        }

        public static entDDL GetSubConfigValuesfromlist(this entDDL lDDL, string Key, int sub_config_id, List<SubConfig> lstSubConfig)
        {
            lDDL.Key = Key;
            lDDL.Value.AddRange(GetSubConfigValuesfromlist(sub_config_id, lstSubConfig));
            return lDDL;
        }


        public static List<entDDLClass> GetSubConfigValuesfromlist(int sub_config_id, List<SubConfig> lstSubConfig)
        {
            List<SubConfig> lstSubConfig1 = new List<SubConfig>();
            lstSubConfig1.AddRange(lstSubConfig.Where(x => x.m_config_id == sub_config_id).ToList());
            try
            {
                if (lstSubConfig1 != null && lstSubConfig1.Count > 0)
                {
                    return ConvertentConfigValueToentDDLClass(lstSubConfig1);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return new List<entDDLClass>();
        }
        private static List<entDDLClass> ConvertentConfigValueToentDDLClass(List<SubConfig> lstSubConfig)
        {
            List<entDDLClass> llistentDDLClass = new List<entDDLClass>();
            try
            {
                if (lstSubConfig != null && lstSubConfig.Count > 0)
                {
                    foreach (SubConfig lSubConfig in lstSubConfig)
                    {
                        entDDLClass lentDDLClass = new entDDLClass();
                        lentDDLClass.Constant = lSubConfig.s_config_value;
                        lentDDLClass.Id = Convert.ToInt64(lSubConfig.m_config_id);
                        lentDDLClass.Description = lSubConfig.s_config_description;
                        llistentDDLClass.Add(lentDDLClass);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return llistentDDLClass;
        }
        public static List<CommonDDl> CommonAutoSearch(string data1, string data2,long data3)
        {
            List<CommonDDl> lstCommonDDl = new List<CommonDDl>();
            try
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_LoadAutoSearch", new string[] { "@screen_name", "@Search_value","@Config_id" },
                    new object[] { data1, data2, data3 });
                if (lDataSet != null)
                {
                    if (lDataSet.Tables.Count > 0 && lDataSet.Tables[0] != null && lDataSet.Tables[0].Rows.Count > 0)
                    {
                        lstCommonDDl = CommonDAL.SetListFromDataTable<CommonDDl>(lDataSet.Tables[0]).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return lstCommonDDl;
        }
        public static entDDL GetSubConfigValue(this entDDL DDL, string Key, int ConfigId)
        {
            DDL.Key = Key;
            List<entDDLClass> ilstentDDLClass = GetSubConfigValue(ConfigId);
            foreach (entDDLClass lentDDLClass in ilstentDDLClass)
            {
                DDL.Value.Add(lentDDLClass);
            }
            return DDL;
        }
        public static List<entDDLClass> GetSubConfigValue(int ConfigId)
        {
            List<entDDLClass> ilst = new List<entDDLClass>();
            List<SubConfig> lSubConfig = new List<SubConfig>();
            lSubConfig = CommonDAL.SelectDataFromDataBase<SubConfig>(new string[] { "M_CONFIG_ID" }, new string[] { "=" },
                new object[] { ConfigId }).ToList();

            if (lSubConfig != null && lSubConfig.Count > 0)
            {
                foreach (SubConfig obj in lSubConfig)
                {
                    entDDLClass lDDLClass = new entDDLClass();
                    lDDLClass.Id = obj.m_config_id;
                    lDDLClass.Code = obj.s_config_value;
                    lDDLClass.Description = obj.s_config_description;
                    ilst.Add(lDDLClass);
                }
            }
            return ilst;
        }

        public static entDDL GetAllProduct(this entDDL DDL, string Key, int ConfigId)
        {
            DDL.Key = Key;
            List<entDDLClass> ilstentDDLClass = GetAllProduct(ConfigId);
            foreach (entDDLClass lentDDLClass in ilstentDDLClass)
            {
                DDL.Value.Add(lentDDLClass);
            }
            return DDL;
        }
        public static List<entDDLClass> GetAllProduct(int ConfigId)
        {
            List<entDDLClass> ilst = new List<entDDLClass>();
            List<Product> lProduct = new List<Product>();
            lProduct = CommonDAL.SelectDataFromDataBase<Product>(new string[] { "PRODUCT_ID" }, new string[] { ">" },
                new object[] { ConfigId }).OrderByDescending(x => x.product_id).ToList();

            if (lProduct != null && lProduct.Count > 0)
            {
                foreach (Product obj in lProduct)
                {
                    entDDLClass lDDLClass = new entDDLClass();
                    lDDLClass.Id = obj.product_id;
                    lDDLClass.FullName = obj.product_name;
                    lDDLClass.RefNo = obj.product_ref_no;
                    lDDLClass.Description = obj.status_value;
                    lDDLClass.Constant = obj.hsn_sac_number;
                    ilst.Add(lDDLClass);
                }
            }
            return ilst;
        }

        public static entDDL GetAllVendor(this entDDL DDL, string Key, int ConfigId)
        {
            DDL.Key = Key;
            List<entDDLClass> ilstentDDLClass = GetAllVendor(ConfigId);
            foreach (entDDLClass lentDDLClass in ilstentDDLClass)
            {
                DDL.Value.Add(lentDDLClass);
            }
            return DDL;
        }
        public static List<entDDLClass> GetAllVendor(int ConfigId)
        {
            List<entDDLClass> ilst = new List<entDDLClass>();
            List<Vendor> lVendor = new List<Vendor>();
            lVendor = CommonDAL.SelectDataFromDataBase<Vendor>(new string[] { "VENDOR_ID" }, new string[] { ">" },
                new object[] { ConfigId }).OrderByDescending(x => x.vendor_id).ToList();

            if (lVendor != null && lVendor.Count > 0)
            {
                foreach (Vendor obj in lVendor)
                {
                    entDDLClass lDDLClass = new entDDLClass();
                    lDDLClass.Id = obj.vendor_id;
                    lDDLClass.Name = obj.vendor_name;
                    lDDLClass.RefNo = obj.vendor_ref_no;
                    lDDLClass.Description = string.Join(",", obj.vendor_name, CommonDAL.ConcatAddressLine(obj.address_line_1, obj.address_line_2, obj.address_line_3));
                    lDDLClass.FullAddress = string.Join(",", obj.vendor_name, CommonDAL.ConcatAddressLine(obj.address_line_1, obj.address_line_2, obj.address_line_3)
                        , obj.city, obj.state, obj.country, obj.pincode, obj.gstn_uin_number, obj.email_id);
                    ilst.Add(lDDLClass);
                }
            }
            return ilst;
        }
        public static string ConcatAddressLine(string line1, string line2, string line3)
        {
            if (string.IsNullOrEmpty(line2))
            {
                return Convert.ToString(line1) + "," + Convert.ToString(line3);
            }
            if (string.IsNullOrEmpty(line3))
            {
                return Convert.ToString(line1) + "," + Convert.ToString(line2);
            }
            if (string.IsNullOrEmpty(line3) && string.IsNullOrEmpty(line2))
            {
                return Convert.ToString(line1);
            }
            if (!string.IsNullOrEmpty(line3) && !string.IsNullOrEmpty(line2))
            {
                return Convert.ToString(line1) + "," + Convert.ToString(line2) + "," + Convert.ToString(line3);
            }
            return string.Empty;
        }
        public static entDDL GetAllCompany(this entDDL DDL, string Key, int ConfigId)
        {
            DDL.Key = Key;
            List<entDDLClass> ilstentDDLClass = GetAllCompany(ConfigId);
            foreach (entDDLClass lentDDLClass in ilstentDDLClass)
            {
                DDL.Value.Add(lentDDLClass);
            }
            return DDL;
        }
        public static List<entDDLClass> GetAllCompany(int ConfigId)
        {
            List<entDDLClass> ilst = new List<entDDLClass>();
            List<Company> lstCompany = new List<Company>();
            lstCompany = CommonDAL.SelectDataFromDataBase<Company>(new string[] { "COMPANY_ID" }, new string[] { ">" },
                new object[] { ConfigId }).OrderByDescending(x => x.company_id).ToList();

            if (lstCompany != null && lstCompany.Count > 0)
            {
                foreach (Company obj in lstCompany)
                {
                    entDDLClass lDDLClass = new entDDLClass();
                    lDDLClass.Id = obj.company_id;
                    lDDLClass.FullName = obj.company_name;
                    lDDLClass.Constant = obj.company_logo;
                    lDDLClass.RefNo = obj.contact_no;
                    lDDLClass.Name = obj.email_id;
                    lDDLClass.Description = string.Join(",", obj.company_name, CommonDAL.ConcatAddressLine(obj.address_line_1, obj.address_line_2, obj.address_line_3));
                    lDDLClass.FullAddress = string.Join(",", obj.company_name, CommonDAL.ConcatAddressLine(obj.address_line_1, obj.address_line_2, obj.address_line_3)
                        , obj.city, obj.state, obj.country, obj.pincode, obj.gstn_uin_number, obj.email_id);
                    ilst.Add(lDDLClass);
                }
            }
            return ilst;
        }
        public static entDDL GetOrderWithoutRepaking(this entDDL DDL, string Key, int ConfigId)
        {
            DDL.Key = Key;
            List<entDDLClass> ilstentDDLClass = GetOrderWithoutRepaking(ConfigId);
            foreach (entDDLClass lentDDLClass in ilstentDDLClass)
            {
                DDL.Value.Add(lentDDLClass);
            }
            return DDL;
        }
        public static List<entDDLClass> GetOrderWithoutRepaking(int ConfigId)
        {
            List<entDDLClass> ilst = new List<entDDLClass>();
            List<RepackingDetail> lstRepackingDetail = new List<RepackingDetail>();
            lstRepackingDetail = CommonDAL.SelectDataFromDataBase<RepackingDetail>(new string[] { "REPACKING_DETAIL_ID" }, new string[] { ">" },
                new object[] { ConfigId }).OrderByDescending(x => x.repacking_detail_id).ToList();
            List<OrderDetails> lstOrderDetails = new List<OrderDetails>();
            lstOrderDetails = CommonDAL.SelectDataFromDataBase<OrderDetails>(new string[] { "ORDER_DETAIL_ID" }, new string[] { ">" },
                new object[] { ConfigId }).OrderByDescending(x => x.order_detail_id).ToList();

            //lstOrderDetails = lstOrderDetails.Where(s => lstRepackingDetail.Any(l => (l.order_id == s.order_detail_id))).ToList();
            foreach (OrderDetails obj in lstOrderDetails)
            {
                RepackingDetail lRepackingDetail = lstRepackingDetail.Where(x => x.order_id == obj.order_detail_id).FirstOrDefault();
                if (lRepackingDetail == null || lRepackingDetail.repacking_detail_id == 0)
                {
                    entDDLClass lDDLClass = new entDDLClass();
                    lDDLClass.Id = obj.order_detail_id;
                    lDDLClass.RefNo = obj.order_ref_no;
                    lDDLClass.Description = obj.status_value;
                    lDDLClass.Constant = obj.is_invoice;
                    lDDLClass.Constant = Convert.ToString(obj.total_amount);
                    ilst.Add(lDDLClass);
                }
            }
            return ilst;
        }
        public static DateTime? SetFromDates(string astrFromDate)
        {
            DateTime? fDateTime = new DateTime();
            if (!string.IsNullOrEmpty(astrFromDate))
            {
                fDateTime = Convert.ToDateTime(astrFromDate);
                //fDateTime = fDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                return fDateTime;
            }

            return fDateTime;
        }

        public static DateTime? SetToDates(string astrToDate)
        {
            DateTime? tDateTime = new DateTime();
            if (!string.IsNullOrEmpty(astrToDate))
            {
                tDateTime = Convert.ToDateTime(astrToDate).AddDays(1).AddMilliseconds(-1);
                // date = tDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                return tDateTime;
            }

            return tDateTime;
        }
        public static string GetPlatformFilePath(string filePath)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return filePath.Replace('/', '\\');
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return filePath.Replace('\\', '/');
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return filePath.Replace('\\', '/');
            }

            return filePath;
        }
        public static List<string> GetMonths()
        {
            string[] monthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            List<string> lstMonths = new List<string>();
            for (int i = 0; i < monthNames.Length; i++)
            {
                if (lstMonths.Count == 12)
                    continue;
                string str_mon = monthNames[i].Substring(0, 3);
                lstMonths.Add(str_mon);
            }
            return lstMonths;
        }
    }
}
