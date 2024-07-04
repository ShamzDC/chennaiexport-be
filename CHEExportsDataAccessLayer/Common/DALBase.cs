using CHEExportsDataObjects;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataAccessLayer
{
    [Serializable]
    [DataContract]
    public class DALBase
    {
        public static readonly string DBConnection = "Server=SG2NWPLS19SQL-v09.mssql.shr.prod.sin2.secureserver.net;Database=chennaiexports;uid=jjtraders;pwd=jChennaij19#;TrustServerCertificate=True;";

      // public readonly string DBConnection = "server=localhost;database=ENTITY;Integrated Security=true;TrustServerCertificate=true;";
        
        private readonly string insert_into = "INSERT INTO ";
        private readonly string update_query = "UPDATE ";
        private readonly string open_query = "SELECT * FROM ";
        private readonly string delete_query = "DELETE FROM ";
        public DALBase(DataObjectBase DataObjectBase)
        {
            iDataObjectBase = DataObjectBase;
        }

        [DataMember]
        public DataObjectBase iDataObjectBase { get; private set; }

        public void Save(string token)
        {
            try
            {
                //CheckLoggedInUser(token);
                if (iDataObjectBase != null)
                {
                    string TABLE_NAME = iDataObjectBase.GetType().GetProperty("TABLE_NAME").GetValue(iDataObjectBase).ToString();
                    if (!string.IsNullOrEmpty(TABLE_NAME))
                    {
                        string query = GenerateInsertQuery(TABLE_NAME);
                        SqlConnection conn = new SqlConnection(DBConnection);
                        conn.Open();
                        SqlCommand command = new SqlCommand(query, conn);
                        foreach (var var in iDataObjectBase.GetType().GetFields())
                        {
                            if (var.Name.Contains("column_name"))
                            {
                                if (var.Name.Contains("is_primary"))
                                {
                                    continue;
                                }
                                if (!string.IsNullOrEmpty(var.GetValue(iDataObjectBase).ToString()))
                                {
                                    foreach (var var1 in iDataObjectBase.GetType().GetProperties())
                                    {
                                        if (var1.Name.ToLower() == var.GetValue(iDataObjectBase).ToString().ToLower())
                                        {
                                            if(var1.GetValue(iDataObjectBase)!= null)
                                            {
                                                command.Parameters.AddWithValue("@" + var.GetValue(iDataObjectBase).ToString(), var1.GetValue(iDataObjectBase));
                                            }
                                            else
                                            {
                                                command.Parameters.AddWithValue("@" + var.GetValue(iDataObjectBase).ToString(), DBNull.Value);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //int result = command.ExecuteNonQuery();
                        long insertedId = Convert.ToInt64(command.ExecuteScalar());
                        conn.Close();
                        iDataObjectBase.GetType().GetProperty(iDataObjectBase.GetType().GetFields().Where(x => x.Name.Contains("is_primary")).FirstOrDefault().GetValue(iDataObjectBase).ToString().ToLower()).SetValue(iDataObjectBase, insertedId);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        private string GenerateInsertQuery(string TABLE_NAME)
        {
            string query = string.Empty;
            try
            {
                query = insert_into + TABLE_NAME + "(";
                PropertyInfo PropertyInfo = iDataObjectBase.GetType().GetProperty("ColumnNames");

                foreach (var var in iDataObjectBase.GetType().GetFields())
                {
                    if (var.Name.Contains("column_name"))
                    {
                        if (var.Name.Contains("is_primary"))
                        {
                            continue;
                        }
                        query += var.GetValue(iDataObjectBase).ToString() + ",";
                    }
                }
                query = query.Remove(query.Length - 1);
                query = query + ") VALUES (";
                foreach (var var in iDataObjectBase.GetType().GetFields())
                {
                    if (var.Name.Contains("column_name"))
                    {
                        if (var.Name.Contains("is_primary"))
                        {
                            continue;
                        }
                        query += "@" + var.GetValue(iDataObjectBase).ToString() + ",";
                    }
                }
                query = query.Remove(query.Length - 1);
                query = query + ");";
                query = query + "SELECT SCOPE_IDENTITY();";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            return query;
        }

        private void CheckLoggedInUser(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Session has been expired. Please logout and login again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void Update(string token, bool istokenupdate = false)
        {
            try
            {
                if(!istokenupdate)
                {
                    //CheckLoggedInUser(token);
                }
                if (iDataObjectBase != null)
                {
                    string TABLE_NAME = iDataObjectBase.GetType().GetProperty("TABLE_NAME").GetValue(iDataObjectBase).ToString();
                    if (!string.IsNullOrEmpty(TABLE_NAME))
                    {
                        string query = GenerateUpdateQuery(TABLE_NAME);
                        SqlConnection conn = new SqlConnection(DBConnection);
                        conn.Open();
                        SqlCommand command = new SqlCommand(query, conn);
                        foreach (var var in iDataObjectBase.GetType().GetFields())
                        {
                            if (var.Name.Contains("column_name"))
                            {
                                if (var.Name.Contains("is_primary"))
                                {
                                    continue;
                                }
                                if (!string.IsNullOrEmpty(var.GetValue(iDataObjectBase).ToString()))
                                {
                                    foreach (var var1 in iDataObjectBase.GetType().GetProperties())
                                    {
                                        if (var1.Name.ToLower() == var.GetValue(iDataObjectBase).ToString().ToLower())
                                        {
                                            if (var1.GetValue(iDataObjectBase) != null)
                                            {
                                                command.Parameters.AddWithValue("@" + var.GetValue(iDataObjectBase).ToString(), var1.GetValue(iDataObjectBase));
                                            }
                                            else
                                            {
                                                command.Parameters.AddWithValue("@" + var.GetValue(iDataObjectBase).ToString(), DBNull.Value);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        foreach (var var in iDataObjectBase.GetType().GetFields())
                        {
                            if (var.Name.Contains("column_name"))
                            {
                                if (var.Name.Contains("is_primary"))
                                {
                                    if (!string.IsNullOrEmpty(var.GetValue(iDataObjectBase).ToString()))
                                    {
                                        foreach (var var1 in iDataObjectBase.GetType().GetProperties())
                                        {
                                            if (var1.Name.ToLower() == var.GetValue(iDataObjectBase).ToString().ToLower())
                                            {
                                                if (var1.GetValue(iDataObjectBase) != null)
                                                {
                                                    command.Parameters.AddWithValue("@" + var.GetValue(iDataObjectBase).ToString(), var1.GetValue(iDataObjectBase));
                                                }
                                                else
                                                {
                                                    command.Parameters.AddWithValue("@" + var.GetValue(iDataObjectBase).ToString(), DBNull.Value);
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        int result = command.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        private string GenerateUpdateQuery(string TABLE_NAME)
        {
            try
            {
                string query = string.Empty;
                try
                {
                    query = update_query + TABLE_NAME + " SET ";
                    PropertyInfo PropertyInfo = iDataObjectBase.GetType().GetProperty("ColumnNames");

                    foreach (var var in iDataObjectBase.GetType().GetFields())
                    {
                        if (var.Name.Contains("column_name"))
                        {
                            if (var.Name.Contains("is_primary"))
                            {
                                continue;
                            }
                            query += var.GetValue(iDataObjectBase).ToString() + " = " + "@" + var.GetValue(iDataObjectBase).ToString() + ",";
                        }
                    }
                    query = query.Remove(query.Length - 1);
                    query = query + " WHERE ";
                    foreach (var var in iDataObjectBase.GetType().GetFields())
                    {
                        if (var.Name.Contains("column_name"))
                        {
                            if (var.Name.Contains("is_primary"))
                            {
                                query += var.GetValue(iDataObjectBase).ToString() + " = " + "@" + var.GetValue(iDataObjectBase).ToString();
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    query = query + ";";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw ex;
                }
                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void Open(string token)
        {
            try
            {
                //CheckLoggedInUser(token);
                if (iDataObjectBase != null)
                {
                    string TABLE_NAME = iDataObjectBase.GetType().GetProperty("TABLE_NAME").GetValue(iDataObjectBase).ToString();
                    if (!string.IsNullOrEmpty(TABLE_NAME))
                    {
                        string query = GenerateOpenQuery(TABLE_NAME);
                        SqlConnection conn = new SqlConnection(DBConnection);
                        conn.Open();
                        SqlCommand command = new SqlCommand(query, conn);
                        foreach (var var in iDataObjectBase.GetType().GetFields())
                        {
                            if (var.Name.Contains("column_name"))
                            {
                                if (var.Name.Contains("is_primary"))
                                {
                                    if (!string.IsNullOrEmpty(var.GetValue(iDataObjectBase).ToString()))
                                    {
                                        foreach (var var1 in iDataObjectBase.GetType().GetProperties())
                                        {
                                            if (var1.Name.ToLower() == var.GetValue(iDataObjectBase).ToString().ToLower())
                                            {
                                                if (var1.GetValue(iDataObjectBase) != null)
                                                {
                                                    command.Parameters.AddWithValue("@" + var.GetValue(iDataObjectBase).ToString(), var1.GetValue(iDataObjectBase));
                                                }
                                                else
                                                {
                                                    command.Parameters.AddWithValue("@" + var.GetValue(iDataObjectBase).ToString(), DBNull.Value);
                                                }
                                            }
                                        }
                                    }
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                foreach (var var in iDataObjectBase.GetType().GetFields())
                                {
                                    if (var.Name.Contains("column_name"))
                                    {
                                        foreach (var var1 in iDataObjectBase.GetType().GetProperties())
                                        {
                                            if (var1.Name.ToLower() == var.GetValue(iDataObjectBase).ToString().ToLower())
                                            {
                                                if (reader[var.GetValue(iDataObjectBase).ToString()] != DBNull.Value)
                                                {
                                                    iDataObjectBase.GetType().GetProperty(var1.Name).SetValue(iDataObjectBase, reader[var.GetValue(iDataObjectBase).ToString()]);
                                                }
                                                else
                                                {
                                                    iDataObjectBase.GetType().GetProperty(var1.Name).SetValue(iDataObjectBase, null);
                                                }
                                            }
                                        }
                                    }
                                }
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
        }

        private string GenerateOpenQuery(string TABLE_NAME)
        {
            string query = string.Empty;
            try
            {
                query = open_query + TABLE_NAME + " ";
                foreach (var var in iDataObjectBase.GetType().GetFields())
                {
                    if (var.Name.Contains("column_name"))
                    {
                        if (var.Name.Contains("is_primary"))
                        {
                            query += " where "+var.GetValue(iDataObjectBase).ToString() + "= " + "@" + var.GetValue(iDataObjectBase).ToString();
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            return query;
        }

        public int Delete(string token)
        {
            int result = -1;
            try
            {
                //CheckLoggedInUser(token);
                if (iDataObjectBase != null)
                {
                    string TABLE_NAME = iDataObjectBase.GetType().GetProperty("TABLE_NAME").GetValue(iDataObjectBase).ToString();
                    if (!string.IsNullOrEmpty(TABLE_NAME))
                    {
                        string query = GenerateDeleteQuery(TABLE_NAME);
                        SqlConnection conn = new SqlConnection(DBConnection);
                        conn.Open();
                        SqlCommand command = new SqlCommand(query, conn);
                        foreach (var var in iDataObjectBase.GetType().GetFields())
                        {
                            if (var.Name.Contains("column_name"))
                            {
                                if (var.Name.Contains("is_primary"))
                                {
                                    if (!string.IsNullOrEmpty(var.GetValue(iDataObjectBase).ToString()))
                                    {
                                        foreach (var var1 in iDataObjectBase.GetType().GetProperties())
                                        {
                                            if (var1.Name.ToLower() == var.GetValue(iDataObjectBase).ToString().ToLower())
                                            {
                                                if (var1.GetValue(iDataObjectBase) != null)
                                                {
                                                    command.Parameters.AddWithValue("@" + var.GetValue(iDataObjectBase).ToString(), var1.GetValue(iDataObjectBase));
                                                }
                                                else
                                                {
                                                    command.Parameters.AddWithValue("@" + var.GetValue(iDataObjectBase).ToString(), DBNull.Value);
                                                }
                                            }
                                        }
                                    }
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        result = command.ExecuteNonQuery();
                        conn.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            return result;
        }

        private string GenerateDeleteQuery(string TABLE_NAME)
        {
            string query = string.Empty;
            try
            {
                query = delete_query + TABLE_NAME + " ";
                foreach (var var in iDataObjectBase.GetType().GetFields())
                {
                    if (var.Name.Contains("column_name"))
                    {
                        if (var.Name.Contains("is_primary"))
                        {
                            query += " where " + var.GetValue(iDataObjectBase).ToString() + " = " + "@" + var.GetValue(iDataObjectBase).ToString();
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            return query;
        }
    }
}
