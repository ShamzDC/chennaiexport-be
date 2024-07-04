
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
    public class DALUserGroup : DALBase
    {
        [DataMember]
        public UserGroup iUserGroup { get; private set; }

        public DALUserGroup(UserGroup aUserGroup) : base(aUserGroup)
        {
            iUserGroup = aUserGroup;
        }
        public void Setdescription()
        {
            string config_ids = Constants.Application.Status_id + "," + Constants.Application.Active_Iactive_Status_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iUserGroup.status_description = lstSubConfig.Where(x => x.s_config_value == iUserGroup.status_value).Select(x => x.s_config_description).FirstOrDefault();
            if (iUserGroup.user_id > 0)
            {
                User lUser = CommonDAL.SelectDataFromDataBase<User>(new string[] { "USER_ID" }, new string[] { "=" },
           new object[] { iUserGroup.user_id }).FirstOrDefault();
                if (lUser != null)
                    iUserGroup.user_name = string.Join(" ", lUser.first_name, lUser.middle_name, lUser.last_name);
                iUserGroup.user_status = lstSubConfig.Where(x => x.s_config_value == lUser.status_value).Select(x => x.s_config_description).FirstOrDefault();

            }
            if (iUserGroup.group_id > 0)
            {
                Group lGroup = CommonDAL.SelectDataFromDataBase<Group>(new string[] { "GROUP_ID" }, new string[] { "=" },
                    new object[] { iUserGroup.group_id }).FirstOrDefault();
                if (lGroup != null)
                    iUserGroup.group_name = lGroup.group_name;
                iUserGroup.group_status = lstSubConfig.Where(x => x.s_config_value == lGroup.status_value).Select(x => x.s_config_description).FirstOrDefault();
            }
        }
        public void CreateNewUserGroup()
        {
            try
            {
                iUserGroup.status_value = Constants.Application.Active;
                Setdescription();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void SaveUserGroup(string token)
        {
            try
            {
                //ValidateUserGroupSave();

                if (iUserGroup != null && (iUserGroup.errorMsg_lsit == null || iUserGroup.errorMsg_lsit.Count == 0))
                {
                    if (iUserGroup.user_group_id == 0)
                    {
                        iUserGroup.changed_date = DateTime.Now;
                        iUserGroup.changed_by = iUserGroup.iLoggedInUserDetails.user_login_id;
                        iUserGroup.entered_date = DateTime.Now;
                        iUserGroup.entered_by = iUserGroup.iLoggedInUserDetails.user_login_id;
                        Save(token);
                    }
                    else
                    {
                        iUserGroup.changed_date = DateTime.Now;
                        iUserGroup.changed_by = iUserGroup.iLoggedInUserDetails.user_login_id;
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

        private void ValidateUserGroupSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateUserGroup(string token)
        {
            try
            {
                //ValidateUserGroupSave();
                if (iUserGroup != null && (iUserGroup.errorMsg_lsit == null || iUserGroup.errorMsg_lsit.Count == 0))
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

        public void DeleteUserGroup(string token)
        {
            try
            {
                //ValidateUserGroupDelete();
                if (iUserGroup != null && (iUserGroup.errorMsg_lsit == null || iUserGroup.errorMsg_lsit.Count == 0) && iUserGroup.user_group_id > 0)
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

        public void OpenUserGroup(string token)
        {
            try
            {
                if (iUserGroup != null && (iUserGroup.errorMsg_lsit == null || iUserGroup.errorMsg_lsit.Count == 0) && iUserGroup.user_group_id > 0)
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

        //public SearchResultBase<UserGroupSearchResultset> SearchUserGroup(protoSearchParams aprotoSearchParams)
        //{
        //    SearchResultBase<UserGroupSearchResultset> searchResult = new SearchResultBase<UserGroupSearchResultset>();
        //    try
        //    {
        //        DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("App_sp_SearchUserGroup", new string[]
        //         { "@SearchParam","@PageNumber","@RowsPerPage"}, new object[]
        //        { aprotoSearchParams.Keyword,aprotoSearchParams.PageNumber, aprotoSearchParams.RowPerPage});
        //        if (lDataSet != null)
        //        {
        //            searchResult.SearchResultSet = CommonDAL.SetListFromDataTable<UserGroupSearchResultset>(lDataSet.Tables[0]);
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




