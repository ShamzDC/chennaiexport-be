
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
    public class DALConsignee : DALBase
    {
        [DataMember]
        public Consignee iConsignee { get; private set; }

        public DALConsignee(Consignee aConsignee) : base(aConsignee)
        {
            iConsignee = aConsignee;
        }
        public void Setdescription()
        {
            string config_ids = Constants.Application.Status_id + "," + Constants.Application.Active_Iactive_Status_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iConsignee.status_description = lstSubConfig.Where(x => x.s_config_value == iConsignee.status_value).Select(x => x.s_config_description).FirstOrDefault();

        }
        public void CreateNewConsignee()
        {
            try
            {
                iConsignee.status_value = Constants.Application.Active;
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
            if (string.IsNullOrEmpty(iConsignee.consignee_ref_no))
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_SP_GenerateReferenceNumber", new string[] { "@Config_const" }, new object[] { "CONG" });

                if (lDataSet != null && lDataSet.Tables.Count > 0 && lDataSet.Tables[0] != null && lDataSet.Tables[0].Rows.Count > 0)
                {
                    iConsignee.consignee_ref_no = lDataSet.Tables[0].Rows[0][0].ToString();
                }
            }
        }

        public void SaveConsignee(string token)
        {
            try
            {
                //ValidateConsigneeSave();
                
                if (iConsignee != null && (iConsignee.errorMsg_lsit == null || iConsignee.errorMsg_lsit.Count == 0))
                {
                    if (iConsignee.consignee_id == 0)
                    {
                        GenerateRefNo();
                        Save(token);
                    }
                    else
                    {
                        iConsignee.changed_date = DateTime.Now;
                        iConsignee.changed_by = iConsignee.iLoggedInUserDetails.user_login_id;
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

        private void ValidateConsigneeSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateConsignee(string token)
        {
            try
            {
                //ValidateConsigneeSave();
                if (iConsignee != null && (iConsignee.errorMsg_lsit == null || iConsignee.errorMsg_lsit.Count == 0))
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

        public void DeleteConsignee(string token)
        {
            try
            {
                //ValidateConsigneeDelete();
                if (iConsignee != null && (iConsignee.errorMsg_lsit == null || iConsignee.errorMsg_lsit.Count == 0) && iConsignee.consignee_id > 0)
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

        public void OpenConsignee(string token)
        {
            try
            {
                if (iConsignee != null && (iConsignee.errorMsg_lsit == null || iConsignee.errorMsg_lsit.Count == 0) && iConsignee.consignee_id > 0)
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

        public SearchResultBase<ConsigneeSearchResultset> SearchConsignee(protoSearchParams aprotoSearchParams)
        {
            SearchResultBase<ConsigneeSearchResultset> searchResult = new SearchResultBase<ConsigneeSearchResultset>();
            try
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_SP_SearchConsignee", new string[]
                 { "@SearchParam","@PageNumber","@RowsPerPage"}, new object[]
                { aprotoSearchParams.Keyword,aprotoSearchParams.PageNumber, aprotoSearchParams.RowPerPage});
                if (lDataSet != null)
                {
                    searchResult.SearchResultSet = CommonDAL.SetListFromDataTable<ConsigneeSearchResultset>(lDataSet.Tables[0]).OrderByDescending(x => x.consignee_id).ToList(); ;
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




