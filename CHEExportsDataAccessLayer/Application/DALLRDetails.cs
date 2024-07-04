


using CHEExportsDataObjects;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataAccessLayer
{
    [Serializable]
    [DataContract]
    public class DALLRDetails : DALBase
    {
        [DataMember]
        public LRDetails iLRDetails { get; private set; }

        public DALLRDetails(LRDetails aLRDetails) : base(aLRDetails)
        {
            iLRDetails = aLRDetails;
        }

        public void CreateNewLRDetails()
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

        public void SaveLRDetails(string token)
        {
            try
            {
             
                if (iLRDetails != null && (iLRDetails.errorMsg_lsit == null || iLRDetails.errorMsg_lsit.Count == 0))
                {
                    if (iLRDetails.lr_detail_id == 0)
                    {
                        Save(token);
                    }
                    else
                    {
                        iLRDetails.changed_date = DateTime.Now;
                        iLRDetails.changed_by = iLRDetails.iLoggedInUserDetails.user_login_id;
                        Update(token);
                    }
                    UpdateOrderStatus();
                    SetDescription();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        private void UpdateOrderStatus()
        {
            if((iLRDetails.consignee_id>0 || iLRDetails.vendor_id>0) && !string.IsNullOrEmpty(iLRDetails.consignee_name) && !string.IsNullOrEmpty(iLRDetails.package_type_value) &&
            !string.IsNullOrEmpty(iLRDetails.mode_of_packing_value) && iLRDetails.net_weight>0 && iLRDetails.gross_weight>0 && iLRDetails.value_of_goods_as_per_invoice>0
             && iLRDetails.to_pay_freight_charges>0 && !string.IsNullOrEmpty(iLRDetails.lr_photo_copy)&& !string.IsNullOrEmpty(iLRDetails.lr_no)
             && !string.IsNullOrEmpty(iLRDetails.transport_name_value) ) 
            {
                if (iLRDetails.order_detail_id > 0)
                {
                    OrderDetails lOrderDetails=new OrderDetails();
                    lOrderDetails.order_detail_id = iLRDetails.order_detail_id;
                    DALOrderDetails lDALOrderDetails=new DALOrderDetails(lOrderDetails);
                    lDALOrderDetails.Open("");
                    lOrderDetails.status_value = Constants.Application.complete;
                    lDALOrderDetails.Update("");
                }
            }
        }

        public void SetDescription()
        {
            string config_ids = Constants.Application.mode_of_packing_id + "," + Constants.Application.package_type_id + "," + Constants.Application.transport_name_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iLRDetails.mode_of_packing_description = lstSubConfig.Where(x => x.s_config_value == iLRDetails.mode_of_packing_value).Select(x => x.s_config_description).FirstOrDefault();
            iLRDetails.package_type_description = lstSubConfig.Where(x => x.s_config_value == iLRDetails.package_type_value).Select(x => x.s_config_description).FirstOrDefault();
            iLRDetails.transport_name_description = lstSubConfig.Where(x => x.s_config_value == iLRDetails.transport_name_value).Select(x => x.s_config_description).FirstOrDefault();
            if (iLRDetails.vendor_id > 0)
            {
                Vendor lVendor = CommonDAL.SelectDataFromDataBase<Vendor>(new string[] { "VENDOR_ID" }, new string[] { "=" },
           new object[] { iLRDetails.vendor_id }).FirstOrDefault();
                if (lVendor != null)
                {
                    iLRDetails.Vendor_details = string.Join("# ", lVendor.vendor_name, CommonDAL.ConcatAddressLine(lVendor.address_line_1, lVendor.address_line_2, lVendor.address_line_3));
                }
            }
            if (iLRDetails.consignee_id > 0)
            {
                Consignee lDALConsignee = CommonDAL.SelectDataFromDataBase<Consignee>(new string[] { "CONSIGNEE_ID" }, new string[] { "=" },
           new object[] { iLRDetails.consignee_id }).FirstOrDefault();
                if (lDALConsignee != null)
                {
                    iLRDetails.Consignee_details = string.Join("# ", lDALConsignee.consignee_ref_no, CommonDAL.ConcatAddressLine(lDALConsignee.address_line_1, lDALConsignee.address_line_2, lDALConsignee.address_line_3));
                    iLRDetails.consignee_name = lDALConsignee.consignee_name;
                }
            }
        }

        public void UpdateLRDetails(string token)
        {
            try
            {
                //ValidateLRDetailsSave();
                if (iLRDetails != null && (iLRDetails.errorMsg_lsit == null || iLRDetails.errorMsg_lsit.Count == 0))
                {
                    Update(token);
                    SetDescription();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void DeleteLRDetails(string token)
        {
            try
            {
                //ValidateLRDetailsDelete();
                if (iLRDetails != null && (iLRDetails.errorMsg_lsit == null || iLRDetails.errorMsg_lsit.Count == 0) && iLRDetails.lr_detail_id > 0)
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

        public void OpenLRDetails(string token)
        {
            try
            {
                if (iLRDetails != null && (iLRDetails.errorMsg_lsit == null || iLRDetails.errorMsg_lsit.Count == 0) && iLRDetails.lr_detail_id > 0)
                {
                    Open(token);
                    SetDescription();
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


