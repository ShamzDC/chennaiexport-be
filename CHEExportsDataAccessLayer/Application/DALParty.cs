
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
    public class DALParty : DALBase
    {
        [DataMember]
        public Party iParty { get; private set; }

        public DALParty(Party aParty) : base(aParty)
        {
            iParty = aParty;
        }
        public void Setdescription()
        {
            string config_ids = Constants.Application.Status_id + "," + Constants.Application.Active_Iactive_Status_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iParty.status_description = lstSubConfig.Where(x => x.s_config_value == iParty.status_value).Select(x => x.s_config_description).FirstOrDefault();

        }
        public void CreateNewParty()
        {
            try
            {
                iParty.status_value = Constants.Application.Active;
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
            if (string.IsNullOrEmpty(iParty.party_ref_no))
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_SP_GenerateReferenceNumber", new string[] { "@Config_const" }, new object[] { "PART" });

                if (lDataSet != null && lDataSet.Tables.Count > 0 && lDataSet.Tables[0] != null && lDataSet.Tables[0].Rows.Count > 0)
                {
                    iParty.party_ref_no = lDataSet.Tables[0].Rows[0][0].ToString();
                }
            }
        }

        public void SaveParty(string token)
        {
            try
            {
                //ValidatePartySave();
                
                if (iParty != null && (iParty.errorMsg_lsit == null || iParty.errorMsg_lsit.Count == 0))
                {
                    if (iParty.party_id == 0)
                    {
                        GenerateRefNo();
                        iParty.changed_date = DateTime.Now;
                        iParty.changed_by = iParty.iLoggedInUserDetails.user_login_id;
                        iParty.entered_date = DateTime.Now;
                        iParty.entered_by = iParty.iLoggedInUserDetails.user_login_id;
                        Save(token);
                    }
                    else
                    {
                        iParty.changed_date = DateTime.Now;
                        iParty.changed_by = iParty.iLoggedInUserDetails.user_login_id;
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

        private void ValidatePartySave()
        {
            throw new NotImplementedException();
        }

        public void UpdateParty(string token)
        {
            try
            {
                //ValidatePartySave();
                if (iParty != null && (iParty.errorMsg_lsit == null || iParty.errorMsg_lsit.Count == 0))
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

        public void DeleteParty(string token)
        {
            try
            {
                //ValidatePartyDelete();
                if (iParty != null && (iParty.errorMsg_lsit == null || iParty.errorMsg_lsit.Count == 0) && iParty.party_id > 0)
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

        public void OpenParty(string token)
        {
            try
            {
                if (iParty != null && (iParty.errorMsg_lsit == null || iParty.errorMsg_lsit.Count == 0) && iParty.party_id > 0)
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

        public SearchResultBase<PartySearchResultset> SearchParty(protoSearchParams aprotoSearchParams)
        {
            SearchResultBase<PartySearchResultset> searchResult = new SearchResultBase<PartySearchResultset>();
            try
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("App_sp_SearchParty", new string[]
                 { "@SearchParam","@PageNumber","@RowsPerPage"}, new object[]
                { aprotoSearchParams.Keyword,aprotoSearchParams.PageNumber, aprotoSearchParams.RowPerPage});
                if (lDataSet != null)
                {
                    searchResult.SearchResultSet = CommonDAL.SetListFromDataTable<PartySearchResultset>(lDataSet.Tables[0]).OrderByDescending(x => x.party_id).ToList(); ;
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




