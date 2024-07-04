
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
    public class DALVendor : DALBase
    {
        [DataMember]
        public Vendor iVendor { get; private set; }

        public DALVendor(Vendor aVendor) : base(aVendor)
        {
            iVendor = aVendor;
        }
        public void Setdescription()
        {
            string config_ids = Constants.Application.Status_id + "," + Constants.Application.Active_Iactive_Status_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iVendor.status_description = lstSubConfig.Where(x => x.s_config_value == iVendor.status_value).Select(x => x.s_config_description).FirstOrDefault();

        }
        public void CreateNewVendor()
        {
            try
            {
                iVendor.status_value = Constants.Application.Active;
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
            if (string.IsNullOrEmpty(iVendor.vendor_ref_no))
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_SP_GenerateReferenceNumber", new string[] { "@Config_const" }, new object[] { "VEND" });

                if (lDataSet != null && lDataSet.Tables.Count > 0 && lDataSet.Tables[0] != null && lDataSet.Tables[0].Rows.Count > 0)
                {
                    iVendor.vendor_ref_no = lDataSet.Tables[0].Rows[0][0].ToString();
                }
            }
        }

        public void SaveVendor(string token)
        {
            try
            {
                //ValidateVendorSave();
                
                if (iVendor != null && (iVendor.errorMsg_lsit == null || iVendor.errorMsg_lsit.Count == 0))
                {
                    if (iVendor.vendor_id == 0)
                    {
                        GenerateRefNo();
                        Save(token);
                    }
                    else
                    {
                        iVendor.changed_date = DateTime.Now;
                        iVendor.changed_by = iVendor.iLoggedInUserDetails.user_login_id;
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

        private void ValidateVendorSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateVendor(string token)
        {
            try
            {
                //ValidateVendorSave();
                if (iVendor != null && (iVendor.errorMsg_lsit == null || iVendor.errorMsg_lsit.Count == 0))
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

        public void DeleteVendor(string token)
        {
            try
            {
                //ValidateVendorDelete();
                if (iVendor != null && (iVendor.errorMsg_lsit == null || iVendor.errorMsg_lsit.Count == 0) && iVendor.vendor_id > 0)
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

        public void OpenVendor(string token)
        {
            try
            {
                if (iVendor != null && (iVendor.errorMsg_lsit == null || iVendor.errorMsg_lsit.Count == 0) && iVendor.vendor_id > 0)
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

        public SearchResultBase<VendorSearchResultset> SearchVendor(protoSearchParams aprotoSearchParams)
        {
            SearchResultBase<VendorSearchResultset> searchResult = new SearchResultBase<VendorSearchResultset>();
            try
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_SP_SearchVendor", new string[]
                { "@SearchParam","@PageNumber","@RowsPerPage"}, new object[]
                { aprotoSearchParams.Keyword,aprotoSearchParams.PageNumber, aprotoSearchParams.RowPerPage});
                if (lDataSet != null)
                {
                    searchResult.SearchResultSet = CommonDAL.SetListFromDataTable<VendorSearchResultset>(lDataSet.Tables[0]).OrderByDescending(x => x.vendor_id).ToList(); ;
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




