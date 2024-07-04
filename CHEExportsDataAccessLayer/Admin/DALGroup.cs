
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
    public class DALGroup : DALBase
    {
        [DataMember]
        public Group iGroup { get; private set; }

        public DALGroup(Group aGroup) : base(aGroup)
        {
            iGroup = aGroup;
        }
        public void Setdescription()
        {
            string config_ids = Constants.Application.Status_id + "," + Constants.Application.Active_Iactive_Status_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iGroup.status_description = lstSubConfig.Where(x => x.s_config_value == iGroup.status_value).Select(x => x.s_config_description).FirstOrDefault();

        }
        public void CreateNewGroup()
        {
            try
            {
                iGroup.status_value = Constants.Application.Active;
                Setdescription();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void SaveGroup(string token)
        {
            try
            {
                //ValidateGroupSave();
                
                if (iGroup != null && (iGroup.errorMsg_lsit == null || iGroup.errorMsg_lsit.Count == 0))
                {
                    if (iGroup.group_id == 0)
                    {
                        iGroup.changed_date = DateTime.Now;
                        iGroup.entered_date = DateTime.Now;
                        iGroup.entered_by = iGroup.iLoggedInUserDetails.user_login_id;
                        iGroup.changed_by = iGroup.iLoggedInUserDetails.user_login_id;
                        Save(token);
                    }
                    else
                    {
                        iGroup.changed_date = DateTime.Now;
                        iGroup.changed_by = iGroup.iLoggedInUserDetails.user_login_id;
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

        private void ValidateGroupSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateGroup(string token)
        {
            try
            {
                //ValidateGroupSave();
                if (iGroup != null && (iGroup.errorMsg_lsit == null || iGroup.errorMsg_lsit.Count == 0))
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

        public void DeleteGroup(string token)
        {
            try
            {
                //ValidateGroupDelete();
                if (iGroup != null && (iGroup.errorMsg_lsit == null || iGroup.errorMsg_lsit.Count == 0) && iGroup.group_id > 0)
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

        public void OpenGroup(string token)
        {
            try
            {
                if (iGroup != null && (iGroup.errorMsg_lsit == null || iGroup.errorMsg_lsit.Count == 0) && iGroup.group_id > 0)
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

        //public SearchResultBase<GroupSearchResultset> SearchGroup(protoSearchParams aprotoSearchParams)
        //{
        //    SearchResultBase<GroupSearchResultset> searchResult = new SearchResultBase<GroupSearchResultset>();
        //    try
        //    {
        //        DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("App_sp_SearchGroup", new string[]
        //         { "@SearchParam","@PageNumber","@RowsPerPage"}, new object[]
        //        { aprotoSearchParams.Keyword,aprotoSearchParams.PageNumber, aprotoSearchParams.RowPerPage});
        //        if (lDataSet != null)
        //        {
        //            searchResult.SearchResultSet = CommonDAL.SetListFromDataTable<GroupSearchResultset>(lDataSet.Tables[0]);
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




