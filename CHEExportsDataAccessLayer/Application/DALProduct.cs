
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
    public class DALProduct : DALBase
    {
        [DataMember]
        public Product iProduct { get; private set; }

        public DALProduct(Product aProduct) : base(aProduct)
        {
            iProduct = aProduct;
        }

        public void CreateNewProduct()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        public void GenerateRefNo()
        {
            if (string.IsNullOrEmpty(iProduct.product_ref_no))
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_SP_GenerateReferenceNumber", new string[] { "@Config_const" }, new object[] { "PROD" });

                if (lDataSet != null && lDataSet.Tables.Count > 0 && lDataSet.Tables[0] != null && lDataSet.Tables[0].Rows.Count > 0)
                {
                    iProduct.product_ref_no = lDataSet.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        public void Setdescription()
        {
            string config_ids = Constants.Application.Status_id + "," + Constants.Application.Active_Iactive_Status_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iProduct.status_description = lstSubConfig.Where(x => x.s_config_value == iProduct.status_value).Select(x => x.s_config_description).FirstOrDefault();

        }
        public void SaveProduct(string token)
        {
            try
            {
                //ValidateProductSave();
                if (iProduct != null && (iProduct.errorMsg_lsit == null || iProduct.errorMsg_lsit.Count == 0))
                {
                    if (iProduct.product_id == 0)
                    {
                        GenerateRefNo();
                        Save(token);
                    }
                    else
                    {
                        iProduct.changed_date = DateTime.Now;
                        iProduct.changed_by = iProduct.iLoggedInUserDetails.user_login_id;
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

        private void ValidateProductSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(string token)
        {
            try
            {
                //ValidateProductSave();
                if (iProduct != null && (iProduct.errorMsg_lsit == null || iProduct.errorMsg_lsit.Count == 0))
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

        public void DeleteProduct(string token)
        {
            try
            {
                //ValidateProductDelete();
                if (iProduct != null && (iProduct.errorMsg_lsit == null || iProduct.errorMsg_lsit.Count == 0) && iProduct.product_id > 0)
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

        public void OpenProduct(string token)
        {
            try
            {
                if (iProduct != null && (iProduct.errorMsg_lsit == null || iProduct.errorMsg_lsit.Count == 0) && iProduct.product_id > 0)
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
        public SearchResultBase<ProductSearchResultset> SearchProduct(protoSearchParams aprotoSearchParams)
        {
            SearchResultBase<ProductSearchResultset> searchResult = new SearchResultBase<ProductSearchResultset>();
            try
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("APP_SP_SearchProduct", new string[]
                 { "@SearchParam","@PageNumber","@RowsPerPage"}, new object[]
                { aprotoSearchParams.Keyword,aprotoSearchParams.PageNumber, aprotoSearchParams.RowPerPage});
                if (lDataSet != null)
                {
                    searchResult.SearchResultSet = CommonDAL.SetListFromDataTable<ProductSearchResultset>(lDataSet.Tables[0]).OrderByDescending(x => x.product_id).ToList(); ;
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



