
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
    public class DALFinalPackingDetail : DALBase
    {
        [DataMember]
        public FinalPackingDetail iFinalPackingDetail { get; private set; }

        public DALFinalPackingDetail(FinalPackingDetail aFinalPackingDetail) : base(aFinalPackingDetail)
        {
            iFinalPackingDetail = aFinalPackingDetail;
        }
        public void Setdescription()
        {
            string config_ids = Constants.Application.Status_id + "," + Constants.Application.Active_Iactive_Status_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iFinalPackingDetail.status_description = lstSubConfig.Where(x => x.s_config_value == iFinalPackingDetail.status_value).Select(x => x.s_config_description).FirstOrDefault();
            if (iFinalPackingDetail.customer_id > 0)
            {
                iFinalPackingDetail.iCustomer = CommonDAL.SelectDataFromDataBase<Customer>
                         (new string[] { "CUSTOMER_ID" }, new string[] { "=" }, new object[] { iFinalPackingDetail.customer_id }).FirstOrDefault();
            }
        }
        public void CreateNewFinalPackingDetail()
        {
            try
            {
                iFinalPackingDetail.status_value = Constants.Application.Active;
                Setdescription();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void SaveFinalPackingDetail(string token)
        {
            try
            {
                //ValidateFinalPackingDetailSave();
                
                if (iFinalPackingDetail != null && (iFinalPackingDetail.errorMsg_lsit == null || iFinalPackingDetail.errorMsg_lsit.Count == 0))
                {
                    iFinalPackingDetail.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                    iFinalPackingDetail.changed_date = DateTime.Now;
                    iFinalPackingDetail.entered_date = DateTime.Now;
                    iFinalPackingDetail.entered_by = iFinalPackingDetail.iLoggedInUserDetails.user_login_id;
                    iFinalPackingDetail.changed_by = iFinalPackingDetail.iLoggedInUserDetails.user_login_id;
                    Save(token);
                    Setdescription();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        private void ValidateFinalPackingDetailSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateFinalPackingDetail(string token)
        {
            try
            {
                //ValidateFinalPackingDetailSave();
                if (iFinalPackingDetail != null && (iFinalPackingDetail.errorMsg_lsit == null || iFinalPackingDetail.errorMsg_lsit.Count == 0))
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

        public void DeleteFinalPackingDetail(string token)
        {
            try
            {
                //ValidateFinalPackingDetailDelete();
                if (iFinalPackingDetail != null && (iFinalPackingDetail.errorMsg_lsit == null || iFinalPackingDetail.errorMsg_lsit.Count == 0) && iFinalPackingDetail.repacking_detail_id > 0)
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
        public  void SetRepakingDescription( )
        {
            string config_ids = Constants.Application.package_type_id + "," + Constants.Application.Unit_type_id + "," + Constants.Application.repaking_status_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iFinalPackingDetail.status_description = lstSubConfig.Where(x => x.s_config_value == iFinalPackingDetail.status_value).Select(x => x.s_config_description).FirstOrDefault();
            iFinalPackingDetail.package_type_description = lstSubConfig.Where(x => x.s_config_value == iFinalPackingDetail.package_type_value).Select(x => x.s_config_description).FirstOrDefault();
            iFinalPackingDetail.unit_type_description = lstSubConfig.Where(x => x.s_config_value == iFinalPackingDetail.unit_type_value).Select(x => x.s_config_description).FirstOrDefault();

        }
        public void OpenFinalPackingDetail(string token)
        {
            try
            {
                if (iFinalPackingDetail != null && (iFinalPackingDetail.errorMsg_lsit == null || iFinalPackingDetail.errorMsg_lsit.Count == 0) && iFinalPackingDetail.final_packing_detail_id > 0)
                {
                    Open(token);
                    Setdescription();
                    SetRepakingDescription();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        public SearchResultBase<FinalPackingDetailsSearchResultset> SearchFinalPackingDetails(protoSearchParams aprotoSearchParams)
        {
            SearchResultBase<FinalPackingDetailsSearchResultset> searchResult = new SearchResultBase<FinalPackingDetailsSearchResultset>();
            try
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_SP_SearchFinalPacking", new string[]
                { "@SearchParam","@PageNumber","@RowsPerPage","@RegionValue","@CustomerType"}, new object[]
                { aprotoSearchParams.Keyword,aprotoSearchParams.PageNumber,aprotoSearchParams.RowPerPage,aprotoSearchParams.RegionValue,aprotoSearchParams.CustomerType});
                if (lDataSet != null)
                {
                    searchResult.SearchResultSet = CommonDAL.SetListFromDataTable<FinalPackingDetailsSearchResultset>(lDataSet.Tables[0]).OrderByDescending(x => x.final_packing_detail_id).ToList(); ;
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




