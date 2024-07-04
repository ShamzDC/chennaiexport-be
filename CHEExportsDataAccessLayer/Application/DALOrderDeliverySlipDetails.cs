


using CHEExportsDataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataAccessLayer
{
    [Serializable]
    [DataContract]
    public class DALOrderDeliverySlipDetails : DALBase
    {
        [DataMember]
        public OrderDeliverySlipDetails iOrderDeliverySlipDetails { get; private set; }

        public DALOrderDeliverySlipDetails(OrderDeliverySlipDetails aOrderDeliverySlipDetails) : base(aOrderDeliverySlipDetails)
        {
            iOrderDeliverySlipDetails = aOrderDeliverySlipDetails;
        }

        public void CreateNewOrderDeliverySlipDetails()
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

        public void SaveOrderDeliverySlipDetails(string token)
        {
            try
            {
                //ValidateOrderDeliverySlipDetailsSave();
                if (iOrderDeliverySlipDetails != null && (iOrderDeliverySlipDetails.errorMsg_lsit == null || iOrderDeliverySlipDetails.errorMsg_lsit.Count == 0))
                {
                    iOrderDeliverySlipDetails.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                    iOrderDeliverySlipDetails.changed_date = DateTime.Now;
                    iOrderDeliverySlipDetails.entered_date = DateTime.Now;
                    iOrderDeliverySlipDetails.entered_by = iOrderDeliverySlipDetails.iLoggedInUserDetails.user_login_id;
                    iOrderDeliverySlipDetails.changed_by = iOrderDeliverySlipDetails.iLoggedInUserDetails.user_login_id;
                    Save(token);
                    SetDescriptionAndChildDetails();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void SetDescriptionAndChildDetails()
        {
            if (iOrderDeliverySlipDetails.product_id > 0)
            {
                Product lProduct = CommonDAL.SelectDataFromDataBase<Product>(new string[] { "PRODUCT_ID" }, new string[] { "=" },
                    new object[] { iOrderDeliverySlipDetails.product_id }).FirstOrDefault();
                if (lProduct != null)
                {
                    iOrderDeliverySlipDetails.ProductDetails = lProduct.product_name;
                    iOrderDeliverySlipDetails.HSN_NO = lProduct.hsn_sac_number;
                }
            }
            if (!string.IsNullOrEmpty(iOrderDeliverySlipDetails.unit_type_value))
            {
                iOrderDeliverySlipDetails.unit_type_id = Constants.Application.Unit_type_id;
                SubConfig lSubConfig = CommonDAL.SelectDataFromDataBase<SubConfig>(new string[] { "M_CONFIG_ID", "S_CONFIG_VALUE" }, new string[] { "=", "=" },
                        new object[] { iOrderDeliverySlipDetails.unit_type_id, iOrderDeliverySlipDetails.unit_type_value }).FirstOrDefault();
                if (lSubConfig != null)
                    iOrderDeliverySlipDetails.unit_Description = lSubConfig.s_config_description;
            }


        }
        public void SetDescriptionAndChildDetails(OrderDeliverySlipDetails aOrderDeliverySlipDetails)
        {
            if (aOrderDeliverySlipDetails.product_id > 0)
            {
                Product lProduct = CommonDAL.SelectDataFromDataBase<Product>(new string[] { "PRODUCT_ID" }, new string[] { "=" },
                    new object[] { aOrderDeliverySlipDetails.product_id }).FirstOrDefault();
                if (lProduct != null)
                {
                    aOrderDeliverySlipDetails.ProductDetails = lProduct.product_name;
                    aOrderDeliverySlipDetails.HSN_NO = lProduct.hsn_sac_number;
                }
            }
            if (!string.IsNullOrEmpty(aOrderDeliverySlipDetails.unit_type_value))
            {
                aOrderDeliverySlipDetails.unit_type_id = Constants.Application.Unit_type_id;
                SubConfig lSubConfig = CommonDAL.SelectDataFromDataBase<SubConfig>(new string[] { "M_CONFIG_ID", "S_CONFIG_VALUE" }, new string[] { "=", "=" },
                        new object[] { aOrderDeliverySlipDetails.unit_type_id, aOrderDeliverySlipDetails.unit_type_value }).FirstOrDefault();
                if (lSubConfig != null)
                    aOrderDeliverySlipDetails.unit_Description = lSubConfig.s_config_description;
            }


        }

        public void UpdateOrderDeliverySlipDetails(string token)
        {
            try
            {
                //ValidateOrderDeliverySlipDetailsSave();
                if (iOrderDeliverySlipDetails != null && (iOrderDeliverySlipDetails.errorMsg_lsit == null || iOrderDeliverySlipDetails.errorMsg_lsit.Count == 0))
                {
                    iOrderDeliverySlipDetails.iLoggedInUserDetails = CommonDAL.GetLoggedInDetailsFromToken(token);
                    iOrderDeliverySlipDetails.changed_date = DateTime.Now;
                    iOrderDeliverySlipDetails.entered_by = iOrderDeliverySlipDetails.iLoggedInUserDetails.user_login_id;
                    Update(token);
                    SetDescriptionAndChildDetails();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public List<OrderDeliverySlipDetails> DeleteOrderDeliverySlipDetails(string token)
        {
            List<OrderDeliverySlipDetails> lstOrderDeliverySlipDetails = new List<OrderDeliverySlipDetails>();
            try
            {
                //ValidateOrderDeliverySlipDetailsDelete();
                if (iOrderDeliverySlipDetails != null && (iOrderDeliverySlipDetails.errorMsg_lsit == null || iOrderDeliverySlipDetails.errorMsg_lsit.Count == 0) && iOrderDeliverySlipDetails.order_delivery_slip_detail_id > 0)
                {
                    Delete(token);
                }
                lstOrderDeliverySlipDetails = GetListofSlipDetailsList();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            return lstOrderDeliverySlipDetails;
        }
        private List<OrderDeliverySlipDetails> GetListofSlipDetailsList()
        {
            List<OrderDeliverySlipDetails> lstOrderDeliverySlipDetails = new List<OrderDeliverySlipDetails>();
            if (iOrderDeliverySlipDetails.order_detail_id > 0)
            {
                lstOrderDeliverySlipDetails = CommonDAL.SelectDataFromDataBase<OrderDeliverySlipDetails>(new string[] { "ORDER_DETAIL_ID" }, new string[] { "=" },
                    new object[] { iOrderDeliverySlipDetails.order_detail_id }).ToList();
                foreach (OrderDeliverySlipDetails lOrderDeliverySlipDetails in lstOrderDeliverySlipDetails)
                {
                    SetDescriptionAndChildDetails(lOrderDeliverySlipDetails);
                }
            }
            return lstOrderDeliverySlipDetails;
        }


        public void OpenOrderDeliverySlipDetails(string token)
        {
            try
            {
                if (iOrderDeliverySlipDetails != null && (iOrderDeliverySlipDetails.errorMsg_lsit == null || iOrderDeliverySlipDetails.errorMsg_lsit.Count == 0) && iOrderDeliverySlipDetails.order_delivery_slip_detail_id > 0)
                {
                    Open(token);
                    SetDescriptionAndChildDetails();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
    }
}



