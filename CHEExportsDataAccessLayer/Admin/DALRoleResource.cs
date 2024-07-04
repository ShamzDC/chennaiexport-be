
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
    public class DALRoleResource : DALBase
    {
        [DataMember]
        public RoleResource iRoleResource { get; private set; }

        public DALRoleResource(RoleResource aRoleResource) : base(aRoleResource)
        {
            iRoleResource = aRoleResource;
        }
        public void Setdescription()
        {
            string config_ids = Constants.Application.Screen_id + "," + Constants.Application.Active_Iactive_Status_id+ "," + Constants.Application.resource_type_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            //iRoleResource.status_description = lstSubConfig.Where(x => x.s_config_value == iRoleResource.status_value).Select(x => x.s_config_description).FirstOrDefault();
            if (iRoleResource.role_id > 0)
            {
                Role lRole = CommonDAL.SelectDataFromDataBase<Role>(new string[] { "ROLE_ID" }, new string[] { "=" },
           new object[] { iRoleResource.role_id }).FirstOrDefault();
                if (lRole != null)
                {
                    iRoleResource.role_name = lRole.role_name;
                    iRoleResource.role_status = lstSubConfig.Where(x => x.s_config_value == lRole.status_value).Select(x => x.s_config_description).FirstOrDefault();
                }
            }
            if (iRoleResource.resource_id > 0)
            {
                Resource lResource = CommonDAL.SelectDataFromDataBase<Resource>(new string[] { "RESOURCE_ID" }, new string[] { "=" },
           new object[] { iRoleResource.resource_id }).FirstOrDefault();
                if (lResource != null)
                {
                    iRoleResource.resource_name = lResource.resource_name;
                    iRoleResource.resource_descrption = lResource.resource_description;
                    iRoleResource.screen_name = lstSubConfig.Where(x => x.s_config_value == lResource.screen_value).Select(x => x.s_config_description).FirstOrDefault();
                    iRoleResource.resource_type = lstSubConfig.Where(x => x.s_config_value == lResource.resource_type_value).Select(x => x.s_config_description).FirstOrDefault();
                }
            }
        }
        public void CreateNewRoleResource()
        {
            try
            {
             //  iRoleResource.status_value = Constants.Application.Active;
                Setdescription();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void SaveRoleResource(string token)
        {
            try
            {
                //ValidateRoleResourceSave();
                
                if (iRoleResource != null && (iRoleResource.errorMsg_lsit == null || iRoleResource.errorMsg_lsit.Count == 0))
                {
                    if (iRoleResource.role_resource_id == 0)
                    {
                        iRoleResource.changed_date = DateTime.Now;
                        iRoleResource.changed_by = iRoleResource.iLoggedInUserDetails.user_login_id;
                        iRoleResource.entered_date = DateTime.Now;
                        iRoleResource.entered_by = iRoleResource.iLoggedInUserDetails.user_login_id;
                        Save(token);
                    }
                    else
                    {
                        iRoleResource.changed_date = DateTime.Now;
                        iRoleResource.changed_by = iRoleResource.iLoggedInUserDetails.user_login_id;
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

        private void ValidateRoleResourceSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateRoleResource(string token)
        {
            try
            {
                //ValidateRoleResourceSave();
                if (iRoleResource != null && (iRoleResource.errorMsg_lsit == null || iRoleResource.errorMsg_lsit.Count == 0))
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

        public void DeleteRoleResource(string token)
        {
            try
            {
                //ValidateRoleResourceDelete();
                if (iRoleResource != null && (iRoleResource.errorMsg_lsit == null || iRoleResource.errorMsg_lsit.Count == 0) && iRoleResource.role_resource_id > 0)
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

        public void OpenRoleResource(string token)
        {
            try
            {
                if (iRoleResource != null && (iRoleResource.errorMsg_lsit == null || iRoleResource.errorMsg_lsit.Count == 0) && iRoleResource.role_resource_id > 0)
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

        //public SearchResultBase<RoleResourceSearchResultset> SearchRoleResource(protoSearchParams aprotoSearchParams)
        //{
        //    SearchResultBase<RoleResourceSearchResultset> searchResult = new SearchResultBase<RoleResourceSearchResultset>();
        //    try
        //    {
        //        DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("App_sp_SearchRoleResource", new string[]
        //         { "@SearchParam","@PageNumber","@RowsPerPage"}, new object[]
        //        { aprotoSearchParams.Keyword,aprotoSearchParams.PageNumber, aprotoSearchParams.RowPerPage});
        //        if (lDataSet != null)
        //        {
        //            searchResult.SearchResultSet = CommonDAL.SetListFromDataTable<RoleResourceSearchResultset>(lDataSet.Tables[0]);
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




