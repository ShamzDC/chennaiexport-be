
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
    public class DALOrderDeliverySlipDetailWithoutInvoice : DALBase
    {
        [DataMember]
        public OrderDeliverySlipDetailWithoutInvoice iOrderDeliverySlipDetailWithoutInvoice { get; private set; }

        public DALOrderDeliverySlipDetailWithoutInvoice(OrderDeliverySlipDetailWithoutInvoice aOrderDeliverySlipDetailWithoutInvoice) : base(aOrderDeliverySlipDetailWithoutInvoice)
        {
            iOrderDeliverySlipDetailWithoutInvoice = aOrderDeliverySlipDetailWithoutInvoice;
        }

        public void CreateNewOrderDeliverySlipDetailWithoutInvoice()
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

        public void SaveOrderDeliverySlipDetailWithoutInvoice(string token)
        {
            try
            {
                //ValidateOrderDeliverySlipDetailWithoutInvoiceSave();
                if (iOrderDeliverySlipDetailWithoutInvoice != null && (iOrderDeliverySlipDetailWithoutInvoice.errorMsg_lsit == null || iOrderDeliverySlipDetailWithoutInvoice.errorMsg_lsit.Count == 0))
                {
                    if (iOrderDeliverySlipDetailWithoutInvoice.order_delivery_slip_without_id == 0)
                    {
                        Save(token);
                    }
                    else
                    {
                        iOrderDeliverySlipDetailWithoutInvoice.changed_date = DateTime.Now;
                        iOrderDeliverySlipDetailWithoutInvoice.changed_by = iOrderDeliverySlipDetailWithoutInvoice.iLoggedInUserDetails.user_login_id;
                        Update(token);
                    }
                    SetDescription();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        private void ValidateOrderDeliverySlipDetailWithoutInvoiceSave()
        {
            throw new NotImplementedException();
        }
        public void SetDescription()
        {
            if (iOrderDeliverySlipDetailWithoutInvoice.vendor_id > 0)
            {
                Vendor lVendor = CommonDAL.SelectDataFromDataBase<Vendor>(new string[] { "VENDOR_ID" }, new string[] { "=" },
           new object[] { iOrderDeliverySlipDetailWithoutInvoice.vendor_id }).FirstOrDefault();
                if(lVendor != null)
                iOrderDeliverySlipDetailWithoutInvoice.Vendor_details = string.Join("# ", lVendor.vendor_name, CommonDAL.ConcatAddressLine(lVendor.address_line_1, lVendor.address_line_2, lVendor.address_line_3), lVendor.gstn_uin_number);
            }
            if (iOrderDeliverySlipDetailWithoutInvoice.consignee_id > 0)
            {
                Consignee lConsignee = CommonDAL.SelectDataFromDataBase<Consignee>(new string[] { "CONSIGNEE_ID" }, new string[] { "=" },
                    new object[] { iOrderDeliverySlipDetailWithoutInvoice.consignee_id }).FirstOrDefault();
                if (lConsignee != null)
                    iOrderDeliverySlipDetailWithoutInvoice.consignee_details = string.Join("# ", lConsignee.consignee_name, CommonDAL.ConcatAddressLine(lConsignee.address_line_1, lConsignee.address_line_2, lConsignee.address_line_3));
            }
        }

        public void UpdateOrderDeliverySlipDetailWithoutInvoice(string token)
        {
            try
            {
                //ValidateOrderDeliverySlipDetailWithoutInvoiceSave();
                if (iOrderDeliverySlipDetailWithoutInvoice != null && (iOrderDeliverySlipDetailWithoutInvoice.errorMsg_lsit == null || iOrderDeliverySlipDetailWithoutInvoice.errorMsg_lsit.Count == 0))
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

        public void DeleteOrderDeliverySlipDetailWithoutInvoice(string token)
        {
            try
            {
                //ValidateOrderDeliverySlipDetailWithoutInvoiceDelete();
                if (iOrderDeliverySlipDetailWithoutInvoice != null && (iOrderDeliverySlipDetailWithoutInvoice.errorMsg_lsit == null || iOrderDeliverySlipDetailWithoutInvoice.errorMsg_lsit.Count == 0)
                && iOrderDeliverySlipDetailWithoutInvoice.order_delivery_slip_without_id > 0)
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

        public void OpenOrderDeliverySlipDetailWithoutInvoice(string token)
        {
            try
            {
                if (iOrderDeliverySlipDetailWithoutInvoice != null && (iOrderDeliverySlipDetailWithoutInvoice.errorMsg_lsit == null || iOrderDeliverySlipDetailWithoutInvoice.errorMsg_lsit.Count == 0)
                && iOrderDeliverySlipDetailWithoutInvoice.order_delivery_slip_without_id > 0)
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




