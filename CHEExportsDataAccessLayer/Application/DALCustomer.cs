

using CHEExportsDataObjects;
using CHEExportsProto;
using System;
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
    public class DALCustomer : DALBase
    {
        [DataMember]
        public Customer iCustomer { get; private set; }

        public DALCustomer(Customer aCustomer) : base(aCustomer)
        {
            iCustomer = aCustomer;
        }

        public void CreateNewCustomer()
        {
            try
            {
                iCustomer.status_value = Constants.Application.Active;
                iCustomer.changed_date = DateTime.Now;
                iCustomer.entered_date = DateTime.Now;
                iCustomer.entered_by = iCustomer.iLoggedInUserDetails.user_login_id;
                iCustomer.changed_by = iCustomer.iLoggedInUserDetails.user_login_id;
                string config_ids = Constants.Application.Active_Iactive_Status_id + "";
                List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
                iCustomer.status_description = lstSubConfig.Where(x => x.s_config_value == iCustomer.status_value).Select(x => x.s_config_description).FirstOrDefault();
                GenerateRefNo();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void SaveCustomer(string token)
        {
            try
            {
                //ValidateCustomerSave();
                if (iCustomer != null && (iCustomer.errorMsg_lsit == null || iCustomer.errorMsg_lsit.Count == 0))
                {
                    if (iCustomer.customer_id == 0)
                    {
                        GenerateRefNo();
                        Save(token);
                    }
                    else
                    {
                        iCustomer.changed_date = DateTime.Now;
                        iCustomer.changed_by = iCustomer.iLoggedInUserDetails.user_login_id;
                        Update(token);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        public void GenerateRefNo()
        {
            if (string.IsNullOrEmpty(iCustomer.customer_ref_no))
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_SP_GenerateReferenceNumber", new string[] { "@Config_const" }, new object[] { "CUST" });

                if (lDataSet != null && lDataSet.Tables.Count > 0 && lDataSet.Tables[0] != null && lDataSet.Tables[0].Rows.Count > 0)
                {
                    iCustomer.customer_ref_no = lDataSet.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        private void ValidateCustomerSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateCustomer(string token)
        {
            try
            {
                //ValidateCustomerSave();
                if (iCustomer != null && (iCustomer.errorMsg_lsit == null || iCustomer.errorMsg_lsit.Count == 0))
                {
                    Update(token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void DeleteCustomer(string token)
        {
            try
            {
                //ValidateCustomerDelete();
                if (iCustomer != null && (iCustomer.errorMsg_lsit == null || iCustomer.errorMsg_lsit.Count == 0) && iCustomer.customer_id > 0)
                {
                    Delete(token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void OpenCustomer(string token)
        {
            try
            {
                if (iCustomer != null && (iCustomer.errorMsg_lsit == null || iCustomer.errorMsg_lsit.Count == 0) && iCustomer.customer_id > 0)
                {
                    Open(token);
                    string config_ids = Constants.Application.Active_Iactive_Status_id + "";
                    List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
                    iCustomer.status_description = lstSubConfig.Where(x => x.s_config_value == iCustomer.status_value).Select(x => x.s_config_description).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public SearchResultBase<CustomerSearchResultset> SearchCustomer(protoSearchParams aprotoSearchParams)
        {
            SearchResultBase<CustomerSearchResultset> searchResult = new SearchResultBase<CustomerSearchResultset>();
            try
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_SP_SearchCustomer", new string[]
                 { "@SearchParam","@PageNumber","@RowsPerPage"}, new object[]
                { aprotoSearchParams.Keyword,aprotoSearchParams.PageNumber, aprotoSearchParams.RowPerPage});
                if (lDataSet != null)
                {
                    searchResult.SearchResultSet = CommonDAL.SetListFromDataTable<CustomerSearchResultset>(lDataSet.Tables[0]).OrderByDescending(x => x.customer_id).ToList(); ;
                    searchResult.total_count = Convert.ToInt32(lDataSet.Tables[1].Rows[0][0]);
                    searchResult.page_number = Convert.ToInt32(lDataSet.Tables[1].Rows[0][1]);
                    searchResult.page_size = Convert.ToInt32(lDataSet.Tables[1].Rows[0][2]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            return searchResult;
        }
    }
}
