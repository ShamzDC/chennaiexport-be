


using CHEExportsDataObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataAccessLayer
{
    [Serializable]
    [DataContract]
    public class DALInvoiceDetails : DALBase
    {
        [DataMember]
        public InvoiceDetails iInvoiceDetails { get; private set; }

        public DALInvoiceDetails(InvoiceDetails aInvoiceDetails) : base(aInvoiceDetails)
        {
            iInvoiceDetails = aInvoiceDetails;
        }

        public void CreateNewInvoiceDetails()
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
        public void SetVendorAndDescriptionDetails()
        {
            if (iInvoiceDetails!=null && iInvoiceDetails.vendor_id > 0)
            {
                Vendor lVendor = CommonDAL.SelectDataFromDataBase<Vendor>(new string[] { "VENDOR_ID" }, new string[] { "=" },
                    new object[] { iInvoiceDetails.vendor_id }).FirstOrDefault();
                if (lVendor != null)
                {
                    iInvoiceDetails.Vendor_Address = string.Join("# ", lVendor.vendor_name, CommonDAL.ConcatAddressLine(lVendor.address_line_1, lVendor.address_line_2, lVendor.address_line_3)
                            , lVendor.city, lVendor.state, lVendor.country, lVendor.pincode, lVendor.gstn_uin_number, lVendor.email_id);
                    iInvoiceDetails.Vendor_Details = string.Join("# ", lVendor.vendor_name, CommonDAL.ConcatAddressLine(lVendor.address_line_1, lVendor.address_line_2, lVendor.address_line_3));
                }
            }
            if (iInvoiceDetails.Company_id > 0)
            {
                Company lCompany = CommonDAL.SelectDataFromDataBase<Company>(new string[] { "COMPANY_ID" }, new string[] { "=" },
                    new object[] { iInvoiceDetails.Company_id }).FirstOrDefault();
                if (lCompany != null)
                {
                    iInvoiceDetails.Company_Address = string.Join("# ", lCompany.company_name, CommonDAL.ConcatAddressLine(lCompany.address_line_1, lCompany.address_line_2, lCompany.address_line_3)
                            , lCompany.city, lCompany.state, lCompany.country, lCompany.pincode, lCompany.gstn_uin_number, lCompany.email_id);
                    iInvoiceDetails.Company_Details = string.Join("# ", lCompany.company_name, CommonDAL.ConcatAddressLine(lCompany.address_line_1, lCompany.address_line_2, lCompany.address_line_3));
                }
            }
            if (iInvoiceDetails != null && iInvoiceDetails.buyer_id > 0)
            {
                Buyer lBuyer = CommonDAL.SelectDataFromDataBase<Buyer>(new string[] { "BUYER_ID" }, new string[] { "=" },
                    new object[] { iInvoiceDetails.buyer_id }).FirstOrDefault();
                if (lBuyer != null)
                {
                    iInvoiceDetails.buyers_order_no = lBuyer.buyer_ref_no;
                    iInvoiceDetails.buyers_date = lBuyer.entered_date;
                    iInvoiceDetails.buyer_details = string.Join("# ", lBuyer.buyer_name, CommonDAL.ConcatAddressLine(lBuyer.address_line_1, lBuyer.address_line_2, lBuyer.address_line_3)
                            , lBuyer.city, lBuyer.state, lBuyer.country, lBuyer.pincode, lBuyer.gstn_uin_number, lBuyer.email_id);
                    //iInvoiceDetails.buyer_details = string.Join("# ", lBuyer.buyer_name, CommonDAL.ConcatAddressLine(lBuyer.address_line_1, lBuyer.address_line_2, lBuyer.address_line_3));
                }
            }
            if (iInvoiceDetails != null && iInvoiceDetails.consignee_id > 0)
            {
                Consignee lConsignee = CommonDAL.SelectDataFromDataBase<Consignee>(new string[] { "CONSIGNEE_ID" }, new string[] { "=" },
                    new object[] { iInvoiceDetails.consignee_id }).FirstOrDefault();
                if (lConsignee != null)
                {
                    iInvoiceDetails.consignee_name = lConsignee.consignee_name;
                    iInvoiceDetails.consignee_details = string.Join("# ",CommonDAL.ConcatAddressLine(lConsignee.address_line_1, lConsignee.address_line_2, lConsignee.address_line_3)
                            , lConsignee.city, lConsignee.state, lConsignee.country, lConsignee.pincode, lConsignee.gstn_uin_number, lConsignee.email_id);
                }
            }
            string config_ids = Constants.Application.Destination_id + "," + Constants.Application.Dispatched_name_id + "," + Constants.Application.Mode_Payment_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iInvoiceDetails.destination_decscription = lstSubConfig.Where(x => x.s_config_value == iInvoiceDetails.destination_value).Select(x => x.s_config_description).FirstOrDefault();
            iInvoiceDetails.dispatched_through_description = lstSubConfig.Where(x => x.s_config_value == iInvoiceDetails.dispatched_through_value).Select(x => x.s_config_description).FirstOrDefault();
            iInvoiceDetails.mode_of_payment_description = lstSubConfig.Where(x => x.s_config_value == iInvoiceDetails.mode_of_payment_value).Select(x => x.s_config_description).FirstOrDefault();
        }

        public void SaveInvoiceDetails(string token)
        {
            try
            {
                //ValidateInvoiceDetailsSave();
                if (iInvoiceDetails != null && (iInvoiceDetails.errorMsg_lsit == null || iInvoiceDetails.errorMsg_lsit.Count == 0))
                {
                    if (iInvoiceDetails.invoice_detail_id == 0)
                    {
                        Save(token);
                    }
                    else
                    {
                        iInvoiceDetails.changed_date = DateTime.Now;
                        iInvoiceDetails.changed_by = iInvoiceDetails.iLoggedInUserDetails.user_login_id;
                        Update(token);
                    }
                    SetVendorAndDescriptionDetails();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        private void ValidateInvoiceDetailsSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateInvoiceDetails(string token)
        {
            try
            {
                //ValidateInvoiceDetailsSave();
                if (iInvoiceDetails != null && (iInvoiceDetails.errorMsg_lsit == null || iInvoiceDetails.errorMsg_lsit.Count == 0))
                {
                    Update(token);
                    SetVendorAndDescriptionDetails();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void DeleteInvoiceDetails(string token)
        {
            try
            {
                //ValidateInvoiceDetailsDelete();
                if (iInvoiceDetails != null && (iInvoiceDetails.errorMsg_lsit == null || iInvoiceDetails.errorMsg_lsit.Count == 0) && iInvoiceDetails.invoice_detail_id > 0)
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

        public void OpenInvoiceDetails(string token)
        {
            try
            {
                if (iInvoiceDetails != null && (iInvoiceDetails.errorMsg_lsit == null || iInvoiceDetails.errorMsg_lsit.Count == 0) && iInvoiceDetails.invoice_detail_id > 0)
                {
                    Open(token);
                    SetVendorAndDescriptionDetails();
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

