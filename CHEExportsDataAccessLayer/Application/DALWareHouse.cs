

using CHEExportsDataObjects;
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CHEExportsDataAccessLayer
{
    [Serializable]
    [DataContract]
    public class DALWareHouse : DALBase
    {
        [DataMember]
        public WareHouse iWareHouse { get; private set; }

        public DALWareHouse(WareHouse aWareHouse) : base(aWareHouse)
        {
            iWareHouse = aWareHouse;
        }

        public void CreateNewWareHouse()
        {
            try
            {
                iWareHouse.status_value = Constants.Application.Active;
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
            if (string.IsNullOrEmpty(iWareHouse.warehouse_ref_no))
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_SP_GenerateReferenceNumber", new string[] { "@Config_const" }, new object[] { "WARH" });

                if (lDataSet != null && lDataSet.Tables.Count > 0 && lDataSet.Tables[0] != null && lDataSet.Tables[0].Rows.Count > 0)
                {
                    iWareHouse.warehouse_ref_no = lDataSet.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        public void Setdescription()
        {
            string config_ids = Constants.Application.Status_id + "," + Constants.Application.Active_Iactive_Status_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iWareHouse.status_description = lstSubConfig.Where(x => x.s_config_value == iWareHouse.status_value).Select(x => x.s_config_description).FirstOrDefault();

        }
        public void SaveWareHouse(string token)
        {
            try
            {
                //ValidateWareHouseSave();
                if (iWareHouse != null && (iWareHouse.errorMsg_lsit == null || iWareHouse.errorMsg_lsit.Count == 0))
                {
                    if (iWareHouse.warehouse_id == 0)
                    {
                        GenerateRefNo();
                        Save(token);
                    }
                    else
                    {
                        iWareHouse.changed_date = DateTime.Now;
                        iWareHouse.changed_by = iWareHouse.iLoggedInUserDetails.user_login_id;
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

        private void ValidateWareHouseSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateWareHouse(string token)
        {
            try
            {
                //ValidateWareHouseSave();
                if (iWareHouse != null && (iWareHouse.errorMsg_lsit == null || iWareHouse.errorMsg_lsit.Count == 0))
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

        public void DeleteWareHouse(string token)
        {
            try
            {
                //ValidateWareHouseDelete();
                if (iWareHouse != null && (iWareHouse.errorMsg_lsit == null || iWareHouse.errorMsg_lsit.Count == 0) && iWareHouse.warehouse_id > 0)
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

        public void OpenWareHouse(string token)
        {
            try
            {
                if (iWareHouse != null && (iWareHouse.errorMsg_lsit == null || iWareHouse.errorMsg_lsit.Count == 0) && iWareHouse.warehouse_id > 0)
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
        public SearchResultBase<WareHouseSearchResultset> SearchWareHouse(protoSearchParams aprotoSearchParams)
        {
            SearchResultBase<WareHouseSearchResultset> searchResult = new SearchResultBase<WareHouseSearchResultset>();
            try
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("App_sp_SearchWareHouse", new string[]
                 { "@SearchParam","@PageNumber","@RowsPerPage"}, new object[]
                { aprotoSearchParams.Keyword,aprotoSearchParams.PageNumber, aprotoSearchParams.RowPerPage});
                if (lDataSet != null)
                {
                    searchResult.SearchResultSet = CommonDAL.SetListFromDataTable<WareHouseSearchResultset>(lDataSet.Tables[0]).OrderByDescending(x => x.warehouse_id).ToList(); ;
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




