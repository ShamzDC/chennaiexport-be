

using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class OrderDetails
    {
        public void GetData(protoOrderDetails aprotoOrderDetails)
        {
            ProtoDataConverter.GetData(aprotoOrderDetails, this);
            if (aprotoOrderDetails.IprotoLRDetails != null)
            {
                this.iLRDetails.GetData(aprotoOrderDetails.IprotoLRDetails);
            }
            if (aprotoOrderDetails.IprotoInvoiceDetails != null)
            {
                this.iInvoiceDetails.GetData(aprotoOrderDetails.IprotoInvoiceDetails);
            }if (aprotoOrderDetails.IprotoOrderDeliverySlipDetailWithoutInvoice != null)
            {
                this.iOrderDeliverySlipDetailWithoutInvoice.GetData(aprotoOrderDetails.IprotoOrderDeliverySlipDetailWithoutInvoice);
            }
            if (aprotoOrderDetails.LstprotoOrderDeliverySlipDetail != null && aprotoOrderDetails.LstprotoOrderDeliverySlipDetail.Count > 0)
            {
                if (this.lstOrderDeliverySlipDetails != null)
                {
                    foreach (protoOrderDeliverySlipDetail obj in aprotoOrderDetails.LstprotoOrderDeliverySlipDetail)
                    {
                        OrderDeliverySlipDetails lOrderDeliverySlipDetails = new OrderDeliverySlipDetails();

                        lOrderDeliverySlipDetails.GetData(obj);

                        this.lstOrderDeliverySlipDetails.Add(lOrderDeliverySlipDetails);
                    }
                }
            }
        }
        public protoOrderDetails GetProto()
        {
            protoOrderDetails lprotoOrderDetails = new protoOrderDetails();
            ProtoDataConverter.GetProto(this, lprotoOrderDetails);
            if (this.iLRDetails != null)
            {
                lprotoOrderDetails.IprotoLRDetails = this.iLRDetails.GetProto();
            }
            if (this.iOrderDeliverySlipDetailWithoutInvoice != null)
            {
                lprotoOrderDetails.IprotoOrderDeliverySlipDetailWithoutInvoice = this.iOrderDeliverySlipDetailWithoutInvoice.GetProto();
            }
            if (this.iInvoiceDetails != null)
            {
                lprotoOrderDetails.IprotoInvoiceDetails = this.iInvoiceDetails.GetProto();
            }
            if (this.lstOrderDeliverySlipDetails != null)
            {
                foreach (OrderDeliverySlipDetails obj in this.lstOrderDeliverySlipDetails)
                {
                    lprotoOrderDetails.LstprotoOrderDeliverySlipDetail.Add(obj.GetProto());
                }
            }
            ProtoDataConverter.SetMessages(this, lprotoOrderDetails);
            return lprotoOrderDetails;
        }
    }
}



