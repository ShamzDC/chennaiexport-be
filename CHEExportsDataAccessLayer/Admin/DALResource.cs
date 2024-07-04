
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
    public class DALResource : DALBase
    {
        [DataMember]
        public Resource iResource { get; private set; }

        public DALResource(Resource aResource) : base(aResource)
        {
            iResource = aResource;
        }
        public void Setdescription()
        {
            string config_ids = Constants.Application.resource_type_id + "," + Constants.Application.Screen_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iResource.screen_description = lstSubConfig.Where(x => x.s_config_value == iResource.screen_value).Select(x => x.s_config_description).FirstOrDefault();
            iResource.resource_type_description = lstSubConfig.Where(x => x.s_config_value == iResource.resource_type_value).Select(x => x.s_config_description).FirstOrDefault();

        }
        public void CreateNewResource()
        {
            try
            {
             //   iResource.status_value = Constants.Application.Active;
                Setdescription();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void SaveResource(string token)
        {
            try
            {
                //ValidateResourceSave();
                
                if (iResource != null && (iResource.errorMsg_lsit == null || iResource.errorMsg_lsit.Count == 0))
                {
                    if (iResource.resource_id == 0)
                    {
                        iResource.changed_date = DateTime.Now;
                        iResource.changed_by = iResource.iLoggedInUserDetails.user_login_id;
                        iResource.entered_date = DateTime.Now;
                        iResource.entered_by = iResource.iLoggedInUserDetails.user_login_id;
                        Save(token);
                    }
                    else
                    {
                        iResource.changed_date = DateTime.Now;
                        iResource.changed_by = iResource.iLoggedInUserDetails.user_login_id;
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

        private void ValidateResourceSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateResource(string token)
        {
            try
            {
                //ValidateResourceSave();
                if (iResource != null && (iResource.errorMsg_lsit == null || iResource.errorMsg_lsit.Count == 0))
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

        public void DeleteResource(string token)
        {
            try
            {
                //ValidateResourceDelete();
                if (iResource != null && (iResource.errorMsg_lsit == null || iResource.errorMsg_lsit.Count == 0) && iResource.resource_id > 0)
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

        public void OpenResource(string token)
        {
            try
            {
                if (iResource != null && (iResource.errorMsg_lsit == null || iResource.errorMsg_lsit.Count == 0) && iResource.resource_id > 0)
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

        //public SearchResultBase<ResourceSearchResultset> SearchResource(protoSearchParams aprotoSearchParams)
        //{
        //    SearchResultBase<ResourceSearchResultset> searchResult = new SearchResultBase<ResourceSearchResultset>();
        //    try
        //    {
        //        DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("App_sp_SearchResource", new string[]
        //         { "@SearchParam","@PageNumber","@RowsPerPage"}, new object[]
        //        { aprotoSearchParams.Keyword,aprotoSearchParams.PageNumber, aprotoSearchParams.RowPerPage});
        //        if (lDataSet != null)
        //        {
        //            searchResult.SearchResultSet = CommonDAL.SetListFromDataTable<ResourceSearchResultset>(lDataSet.Tables[0]);
        //            searchResult.total_count = Convert.ToInt32(lDataSet.Tables[1].Rows[0][0]);
        //            searchResult.page_number = Convert.ToInt32(lDataSet.Tables[1].Rows[0][1]);
        //            searchResult.page_size = Convert.ToInt32(lDataSet.Tables[1].Rows[0][2]);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        throw ex;
        //    }
        //    return searchResult;
        //}
    }
}




