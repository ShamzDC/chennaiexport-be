
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
    public class DALRole : DALBase
    {
        [DataMember]
        public Role iRole { get; private set; }

        public DALRole(Role aRole) : base(aRole)
        {
            iRole = aRole;
        }
        public void Setdescription()
        {
            string config_ids = Constants.Application.Screen_id + "," + Constants.Application.Active_Iactive_Status_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iRole.status_description = lstSubConfig.Where(x => x.s_config_value == iRole.status_value).Select(x => x.s_config_description).FirstOrDefault();
            iRole.screen_description = lstSubConfig.Where(x => x.s_config_value == iRole.screen_value).Select(x => x.s_config_description).FirstOrDefault();
        }
        public void CreateNewRole()
        {
            try
            {
                iRole.status_value = Constants.Application.Active;
                Setdescription();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void SaveRole(string token)
        {
            try
            {
                //ValidateRoleSave();
                
                if (iRole != null && (iRole.errorMsg_lsit == null || iRole.errorMsg_lsit.Count == 0))
                {
                    if (iRole.role_id == 0)
                    {
                        iRole.changed_date = DateTime.Now;
                        iRole.changed_by = iRole.iLoggedInUserDetails.user_login_id;
                        iRole.entered_date = DateTime.Now;
                        iRole.entered_by = iRole.iLoggedInUserDetails.user_login_id;
                        Save(token);
                    }
                    else
                    {
                        iRole.changed_date = DateTime.Now;
                        iRole.changed_by = iRole.iLoggedInUserDetails.user_login_id;
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

        private void ValidateRoleSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateRole(string token)
        {
            try
            {
                //ValidateRoleSave();
                if (iRole != null && (iRole.errorMsg_lsit == null || iRole.errorMsg_lsit.Count == 0))
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

        public void DeleteRole(string token)
        {
            try
            {
                //ValidateRoleDelete();
                if (iRole != null && (iRole.errorMsg_lsit == null || iRole.errorMsg_lsit.Count == 0) && iRole.role_id > 0)
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

        public void OpenRole(string token)
        {
            try
            {
                if (iRole != null && (iRole.errorMsg_lsit == null || iRole.errorMsg_lsit.Count == 0) && iRole.role_id > 0)
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

        //public SearchResultBase<RoleSearchResultset> SearchRole(protoSearchParams aprotoSearchParams)
        //{
        //    SearchResultBase<RoleSearchResultset> searchResult = new SearchResultBase<RoleSearchResultset>();
        //    try
        //    {
        //        DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("App_sp_SearchRole", new string[]
        //         { "@SearchParam","@PageNumber","@RowsPerPage"}, new object[]
        //        { aprotoSearchParams.Keyword,aprotoSearchParams.PageNumber, aprotoSearchParams.RowPerPage});
        //        if (lDataSet != null)
        //        {
        //            searchResult.SearchResultSet = CommonDAL.SetListFromDataTable<RoleSearchResultset>(lDataSet.Tables[0]);
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




