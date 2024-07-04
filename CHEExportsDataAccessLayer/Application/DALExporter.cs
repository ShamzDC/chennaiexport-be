
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
    public class DALExporter : DALBase
    {
        [DataMember]
        public Exporter iExporter { get; private set; }

        public DALExporter(Exporter aExporter) : base(aExporter)
        {
            iExporter = aExporter;
        }
        public void Setdescription()
        {
            string config_ids = Constants.Application.Status_id + "," + Constants.Application.Active_Iactive_Status_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iExporter.status_description = lstSubConfig.Where(x => x.s_config_value == iExporter.status_value).Select(x => x.s_config_description).FirstOrDefault();

        }
        public void CreateNewExporter()
        {
            try
            {
                iExporter.status_value = Constants.Application.Active;
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
            if (string.IsNullOrEmpty(iExporter.exporter_ref_no))
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_SP_GenerateReferenceNumber", new string[] { "@Config_const" }, new object[] { "EXPO" });

                if (lDataSet != null && lDataSet.Tables.Count > 0 && lDataSet.Tables[0] != null && lDataSet.Tables[0].Rows.Count > 0)
                {
                    iExporter.exporter_ref_no = lDataSet.Tables[0].Rows[0][0].ToString();
                }
            }
        }

        public void SaveExporter(string token)
        {
            try
            {
                //ValidateExporterSave();
                
                if (iExporter != null && (iExporter.errorMsg_lsit == null || iExporter.errorMsg_lsit.Count == 0))
                {
                    if (iExporter.exporter_id == 0)
                    {
                        iExporter.changed_date = DateTime.Now;
                        iExporter.changed_by = iExporter.iLoggedInUserDetails.user_login_id;
                        iExporter.entered_date = DateTime.Now;
                        iExporter.entered_by = iExporter.iLoggedInUserDetails.user_login_id;
                        GenerateRefNo();
                        Save(token);
                    }
                    else
                    {
                        iExporter.changed_date = DateTime.Now;
                        iExporter.changed_by = iExporter.iLoggedInUserDetails.user_login_id;
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

        private void ValidateExporterSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateExporter(string token)
        {
            try
            {
                //ValidateExporterSave();
                if (iExporter != null && (iExporter.errorMsg_lsit == null || iExporter.errorMsg_lsit.Count == 0))
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

        public void DeleteExporter(string token)
        {
            try
            {
                //ValidateExporterDelete();
                if (iExporter != null && (iExporter.errorMsg_lsit == null || iExporter.errorMsg_lsit.Count == 0) && iExporter.exporter_id > 0)
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

        public void OpenExporter(string token)
        {
            try
            {
                if (iExporter != null && (iExporter.errorMsg_lsit == null || iExporter.errorMsg_lsit.Count == 0) && iExporter.exporter_id > 0)
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

        public SearchResultBase<ExporterSearchResultset> SearchExporter(protoSearchParams aprotoSearchParams)
        {
            SearchResultBase<ExporterSearchResultset> searchResult = new SearchResultBase<ExporterSearchResultset>();
            try
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("App_sp_SearchExporter", new string[]
                 { "@SearchParam","@PageNumber","@RowsPerPage"}, new object[]
                { aprotoSearchParams.Keyword,aprotoSearchParams.PageNumber, aprotoSearchParams.RowPerPage});
                if (lDataSet != null)
                {
                    searchResult.SearchResultSet = CommonDAL.SetListFromDataTable<ExporterSearchResultset>(lDataSet.Tables[0]).OrderByDescending(x => x.exporter_id).ToList(); ;
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




