
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
    public class DALExportConsignee : DALBase
    {
        [DataMember]
        public ExportConsignee iExportConsignee { get; private set; }

        public DALExportConsignee(ExportConsignee aExportConsignee) : base(aExportConsignee)
        {
            iExportConsignee = aExportConsignee;
        }
        public void Setdescription()
        {
            string config_ids = Constants.Application.Status_id + "," + Constants.Application.Active_Iactive_Status_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iExportConsignee.status_description = lstSubConfig.Where(x => x.s_config_value == iExportConsignee.status_value).Select(x => x.s_config_description).FirstOrDefault();

        }
        public void CreateNewExportConsignee()
        {
            try
            {
                iExportConsignee.status_value = Constants.Application.Active;
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
            if (string.IsNullOrEmpty(iExportConsignee.export_consignee_ref_no))
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_SP_GenerateReferenceNumber", new string[] { "@Config_const" }, new object[] { "EXCN" });

                if (lDataSet != null && lDataSet.Tables.Count > 0 && lDataSet.Tables[0] != null && lDataSet.Tables[0].Rows.Count > 0)
                {
                    iExportConsignee.export_consignee_ref_no = lDataSet.Tables[0].Rows[0][0].ToString();
                }
            }
        }

        public void SaveExportConsignee(string token)
        {
            try
            {
                //ValidateExportConsigneeSave();
                
                if (iExportConsignee != null && (iExportConsignee.errorMsg_lsit == null || iExportConsignee.errorMsg_lsit.Count == 0))
                {
                    if (iExportConsignee.export_consignee_id == 0)
                    {
                        GenerateRefNo();
                        Save(token);
                    }
                    else
                    {
                        iExportConsignee.changed_date = DateTime.Now;
                        iExportConsignee.changed_by = iExportConsignee.iLoggedInUserDetails.user_login_id;
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

        private void ValidateExportConsigneeSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateExportConsignee(string token)
        {
            try
            {
                //ValidateExportConsigneeSave();
                if (iExportConsignee != null && (iExportConsignee.errorMsg_lsit == null || iExportConsignee.errorMsg_lsit.Count == 0))
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

        public void DeleteExportConsignee(string token)
        {
            try
            {
                //ValidateExportConsigneeDelete();
                if (iExportConsignee != null && (iExportConsignee.errorMsg_lsit == null || iExportConsignee.errorMsg_lsit.Count == 0) && iExportConsignee.export_consignee_id > 0)
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

        public void OpenExportConsignee(string token)
        {
            try
            {
                if (iExportConsignee != null && (iExportConsignee.errorMsg_lsit == null || iExportConsignee.errorMsg_lsit.Count == 0) && iExportConsignee.export_consignee_id > 0)
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

        public SearchResultBase<ExportConsigneeSearchResultset> SearchExportConsignee(protoSearchParams aprotoSearchParams)
        {
            SearchResultBase<ExportConsigneeSearchResultset> searchResult = new SearchResultBase<ExportConsigneeSearchResultset>();
            try
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_SP_SearchExportConsignee", new string[]
                 { "@SearchParam","@PageNumber","@RowsPerPage"}, new object[]
                { aprotoSearchParams.Keyword,aprotoSearchParams.PageNumber, aprotoSearchParams.RowPerPage});
                if (lDataSet != null)
                {
                    searchResult.SearchResultSet = CommonDAL.SetListFromDataTable<ExportConsigneeSearchResultset>(lDataSet.Tables[0]).OrderByDescending(x => x.export_consignee_id).ToList(); ;
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




