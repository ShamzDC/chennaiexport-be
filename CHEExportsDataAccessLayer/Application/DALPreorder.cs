
using CHEExportsDataObjects;
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataAccessLayer
{
    [Serializable]
    [DataContract]
    public class DALPreorder : DALBase
    {
        [DataMember]
        public Preorder iPreorder { get; private set; }

        public DALPreorder(Preorder aPreorder) : base(aPreorder)
        {
            iPreorder = aPreorder;
        }
        public void Setdescription()
        {
            string config_ids = Constants.Application.PreOrder_Status_id + "," + Constants.Application.Region_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iPreorder.status_description = lstSubConfig.Where(x => x.s_config_value == iPreorder.status_value).Select(x => x.s_config_description).FirstOrDefault();
            if(!string.IsNullOrEmpty (iPreorder.region_value))
            iPreorder.status_description = lstSubConfig.Where(x => x.s_config_value == iPreorder.region_value).Select(x => x.s_config_description).FirstOrDefault();
            if(iPreorder.customer_id>0)
            {
                iPreorder.iCustomer  = CommonDAL.SelectDataFromDataBase<Customer>(new string[] { "CUSTOMER_ID" }, new string[] { "=" },
                    new object[] { iPreorder.customer_id }).FirstOrDefault();
            }
        }
        public void Setdescription(Preorder lPreorder)
        {
            string config_ids = Constants.Application.PreOrder_Status_id + "," + Constants.Application.Region_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            lPreorder.status_description = lstSubConfig.Where(x => x.s_config_value == lPreorder.status_value).Select(x => x.s_config_description).FirstOrDefault();
            if (!string.IsNullOrEmpty(lPreorder.region_value))
                lPreorder.region_description = lstSubConfig.Where(x => x.s_config_value == lPreorder.region_value).Select(x => x.s_config_description).FirstOrDefault();
            if (lPreorder.customer_id > 0)
            {
                lPreorder.iCustomer = CommonDAL.SelectDataFromDataBase<Customer>(new string[] { "CUSTOMER_ID" }, new string[] { "=" },
                    new object[] { lPreorder.customer_id }).FirstOrDefault();
            }
        }
        public void CreateNewPreorder()
        {
            try
            {
                iPreorder.status_value = Constants.Application.Created;
                GenerateRefNo();
                Setdescription();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        public void GenerateRefNo()
        {
            if (string.IsNullOrEmpty(iPreorder.preorder_ref_no))
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_SP_GenerateReferenceNumber", new string[] { "@Config_const" }, new object[] { "PROR" });

                if (lDataSet != null && lDataSet.Tables.Count > 0 && lDataSet.Tables[0] != null && lDataSet.Tables[0].Rows.Count > 0)
                {
                    iPreorder.preorder_ref_no = lDataSet.Tables[0].Rows[0][0].ToString();
                }
            }
        }

        public void SavePreorder(string token)
        {
            try
            {
                //ValidatePreorderSave();
                
                if (iPreorder != null && (iPreorder.errorMsg_lsit == null || iPreorder.errorMsg_lsit.Count == 0))
                {
                    if (iPreorder.preorder_id == 0)
                    {
                        iPreorder.entered_date = DateTime.Now;
                        iPreorder.entered_by = iPreorder.iLoggedInUserDetails.user_login_id;
                        iPreorder.changed_date = DateTime.Now;
                        iPreorder.changed_by = iPreorder.iLoggedInUserDetails.user_login_id;
                        // GenerateRefNo();
                        Save(token);
                    }
                    else
                    {
                        iPreorder.changed_date = DateTime.Now;
                        iPreorder.changed_by = iPreorder.iLoggedInUserDetails.user_login_id;
                        Update(token);
                    }
                    Setdescription();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void UpdatePreorder(string token)
        {
            try
            {
                //ValidatePreorderSave();
                if (iPreorder != null && (iPreorder.errorMsg_lsit == null || iPreorder.errorMsg_lsit.Count == 0))
                {
                    Update(token);
                    Setdescription();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void DeletePreorder(string token)
        {
            try
            {
                //ValidatePreorderDelete();
                if (iPreorder != null && (iPreorder.errorMsg_lsit == null || iPreorder.errorMsg_lsit.Count == 0) && iPreorder.preorder_id > 0)
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

        public void OpenPreorder(string token)
        {
            try
            {
                if (iPreorder != null && (iPreorder.errorMsg_lsit == null || iPreorder.errorMsg_lsit.Count == 0) && iPreorder.preorder_id > 0)
                {
                    Open(token);
                    Setdescription();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public List<Preorder> GetTodayPreorders()
        {
            List<Preorder> lstPreorder = CommonDAL.SelectDataFromDataBase<Preorder>(new string[] { "ORDER_EXPECTED_DATE", "STATUS_VALUE" }, new string[] { "=","=" },
                     new object[] { DateTime.Today,Constants.Application.Created }).OrderByDescending(x => x.preorder_id).ToList();
            if(lstPreorder!=null && lstPreorder.Count>0)
            {
                foreach(Preorder lpreorder in lstPreorder)
                {
                    Setdescription(lpreorder);
                }
            }
            return lstPreorder;
        }

        internal void SetAndCompletePreOrder()
        {
            Open("");
            if(iPreorder.status_value!=Constants.Application.Completed)
            {
                iPreorder.status_value= Constants.Application.Completed;
                Update("");
            }
        }

        public SearchResultBase<PreorderSearchResultset> SearchPreorder(protoSearchParams aprotoSearchParams)
        {
            SearchResultBase<PreorderSearchResultset> searchResult = new SearchResultBase<PreorderSearchResultset>();
            try
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_SP_SearchPreOrderDetail", new string[]
                 { "@SearchParam","@PageNumber","@RowsPerPage","@RegionValue","@ExpectingDate"}, new object[]
                { aprotoSearchParams.Keyword,aprotoSearchParams.PageNumber, aprotoSearchParams.RowPerPage,aprotoSearchParams.RegionValue,
                  aprotoSearchParams.Date});
                if (lDataSet != null)
                {
                    searchResult.SearchResultSet = CommonDAL.SetListFromDataTable<PreorderSearchResultset>(lDataSet.Tables[0]).OrderByDescending(x => x.preorder_id).ToList(); ;
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




