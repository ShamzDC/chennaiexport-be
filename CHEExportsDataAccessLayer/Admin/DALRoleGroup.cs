
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
    public class DALRoleGroup : DALBase
    {
        [DataMember]
        public RoleGroup iRoleGroup { get; private set; }

        public DALRoleGroup(RoleGroup aRoleGroup) : base(aRoleGroup)
        {
            iRoleGroup = aRoleGroup;
        }
        public void Setdescription()
        {
            string config_ids = Constants.Application.Status_id + "," + Constants.Application.Active_Iactive_Status_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iRoleGroup.status_description = lstSubConfig.Where(x => x.s_config_value == iRoleGroup.status_value).Select(x => x.s_config_description).FirstOrDefault();
            if (iRoleGroup.role_id > 0)
            {
                Role lRole = CommonDAL.SelectDataFromDataBase<Role>(new string[] { "ROLE_ID" }, new string[] { "=" },
           new object[] { iRoleGroup.role_id }).FirstOrDefault();
                if (lRole != null)
                    iRoleGroup.role_name = lRole.role_name;
                iRoleGroup.role_status = lstSubConfig.Where(x => x.s_config_value == lRole.status_value).Select(x => x.s_config_description).FirstOrDefault();

            }
            if (iRoleGroup.group_id > 0)
            {
                Group lGroup = CommonDAL.SelectDataFromDataBase<Group>(new string[] { "GROUP_ID" }, new string[] { "=" },
                    new object[] { iRoleGroup.group_id }).FirstOrDefault();
                if (lGroup != null)
                    iRoleGroup.group_name = lGroup.group_name;
                  iRoleGroup.group_status = lstSubConfig.Where(x => x.s_config_value == lGroup.status_value).Select(x => x.s_config_description).FirstOrDefault();
            }
        }
        public void CreateNewRoleGroup()
        {
            try
            {
                iRoleGroup.status_value = Constants.Application.Active;
                Setdescription();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void SaveRoleGroup(string token)
        {
            try
            {
                //ValidateRoleGroupSave();
                
                if (iRoleGroup != null && (iRoleGroup.errorMsg_lsit == null || iRoleGroup.errorMsg_lsit.Count == 0))
                {
                    if (iRoleGroup.role_group_id == 0)
                    {
                        iRoleGroup.changed_date = DateTime.Now;
                        iRoleGroup.changed_by = iRoleGroup.iLoggedInUserDetails.user_login_id;
                        iRoleGroup.entered_date = DateTime.Now;
                        iRoleGroup.entered_by = iRoleGroup.iLoggedInUserDetails.user_login_id;
                        Save(token);
                    }
                    else
                    {
                        iRoleGroup.changed_date = DateTime.Now;
                        iRoleGroup.changed_by = iRoleGroup.iLoggedInUserDetails.user_login_id;
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

        private void ValidateRoleGroupSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateRoleGroup(string token)
        {
            try
            {
                //ValidateRoleGroupSave();
                if (iRoleGroup != null && (iRoleGroup.errorMsg_lsit == null || iRoleGroup.errorMsg_lsit.Count == 0))
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

        public void DeleteRoleGroup(string token)
        {
            try
            {
                //ValidateRoleGroupDelete();
                if (iRoleGroup != null && (iRoleGroup.errorMsg_lsit == null || iRoleGroup.errorMsg_lsit.Count == 0) && iRoleGroup.role_group_id > 0)
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

        public void OpenRoleGroup(string token)
        {
            try
            {
                if (iRoleGroup != null && (iRoleGroup.errorMsg_lsit == null || iRoleGroup.errorMsg_lsit.Count == 0) && iRoleGroup.role_group_id > 0)
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

        //public SearchResultBase<RoleGroupSearchResultset> SearchRoleGroup(protoSearchParams aprotoSearchParams)
        //{
        //    SearchResultBase<RoleGroupSearchResultset> searchResult = new SearchResultBase<RoleGroupSearchResultset>();
        //    try
        //    {
        //        DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("App_sp_SearchRoleGroup", new string[]
        //         { "@SearchParam","@PageNumber","@RowsPerPage"}, new object[]
        //        { aprotoSearchParams.Keyword,aprotoSearchParams.PageNumber, aprotoSearchParams.RowPerPage});
        //        if (lDataSet != null)
        //        {
        //            searchResult.SearchResultSet = CommonDAL.SetListFromDataTable<RoleGroupSearchResultset>(lDataSet.Tables[0]);
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




