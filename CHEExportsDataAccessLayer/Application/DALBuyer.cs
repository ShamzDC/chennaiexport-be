
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
    public class DALBuyer : DALBase
    {
        [DataMember]
        public Buyer iBuyer { get; private set; }

        public DALBuyer(Buyer aBuyer) : base(aBuyer)
        {
            iBuyer = aBuyer;
        }
        public void Setdescription()
        {
            string config_ids = Constants.Application.Status_id + "," + Constants.Application.Active_Iactive_Status_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iBuyer.status_description = lstSubConfig.Where(x => x.s_config_value == iBuyer.status_value).Select(x => x.s_config_description).FirstOrDefault();

        }
        public void CreateNewBuyer()
        {
            try
            {
                iBuyer.status_value = Constants.Application.Active;
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
            if (string.IsNullOrEmpty(iBuyer.buyer_ref_no))
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_SP_GenerateReferenceNumber", new string[] { "@Config_const" }, new object[] { "BUYR" });

                if (lDataSet != null && lDataSet.Tables.Count > 0 && lDataSet.Tables[0] != null && lDataSet.Tables[0].Rows.Count > 0)
                {
                    iBuyer.buyer_ref_no = lDataSet.Tables[0].Rows[0][0].ToString();
                }
            }
        }

        public void SaveBuyer(string token)
        {
            try
            {
                //ValidateBuyerSave();
                
                if (iBuyer != null && (iBuyer.errorMsg_lsit == null || iBuyer.errorMsg_lsit.Count == 0))
                {
                    if (iBuyer.buyer_id == 0)
                    {
                        GenerateRefNo();
                        Save(token);
                    }
                    else
                    {
                        iBuyer.changed_date = DateTime.Now;
                        iBuyer.changed_by = iBuyer.iLoggedInUserDetails.user_login_id;
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

        private void ValidateBuyerSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateBuyer(string token)
        {
            try
            {
                //ValidateBuyerSave();
                if (iBuyer != null && (iBuyer.errorMsg_lsit == null || iBuyer.errorMsg_lsit.Count == 0))
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

        public void DeleteBuyer(string token)
        {
            try
            {
                //ValidateBuyerDelete();
                if (iBuyer != null && (iBuyer.errorMsg_lsit == null || iBuyer.errorMsg_lsit.Count == 0) && iBuyer.buyer_id > 0)
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

        public void OpenBuyer(string token)
        {
            try
            {
                if (iBuyer != null && (iBuyer.errorMsg_lsit == null || iBuyer.errorMsg_lsit.Count == 0) && iBuyer.buyer_id > 0)
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

        public SearchResultBase<BuyerSearchResultset> SearchBuyer(protoSearchParams aprotoSearchParams)
        {
            SearchResultBase<BuyerSearchResultset> searchResult = new SearchResultBase<BuyerSearchResultset>();
            try
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("App_sp_SearchBuyer", new string[]
                 { "@SearchParam","@PageNumber","@RowsPerPage"}, new object[]
                { aprotoSearchParams.Keyword,aprotoSearchParams.PageNumber, aprotoSearchParams.RowPerPage});
                if (lDataSet != null)
                {
                    searchResult.SearchResultSet = CommonDAL.SetListFromDataTable<BuyerSearchResultset>(lDataSet.Tables[0]).OrderByDescending(x => x.buyer_id).ToList(); 
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




