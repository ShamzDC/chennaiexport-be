
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class OrderDeliverySlipDetailWithoutInvoice
    {
        public void GetData(protoOrderDeliverySlipDetailWithoutInvoice aprotoOrderDeliverySlipDetailWithoutInvoice)
        {
            ProtoDataConverter.GetData(aprotoOrderDeliverySlipDetailWithoutInvoice, this);
        }

        public protoOrderDeliverySlipDetailWithoutInvoice GetProto()
        {
            protoOrderDeliverySlipDetailWithoutInvoice lprotoOrderDeliverySlipDetailWithoutInvoice = new protoOrderDeliverySlipDetailWithoutInvoice();
            ProtoDataConverter.GetProto(this, lprotoOrderDeliverySlipDetailWithoutInvoice);
            ProtoDataConverter.SetMessages(this, lprotoOrderDeliverySlipDetailWithoutInvoice);
            return lprotoOrderDeliverySlipDetailWithoutInvoice;
        }
    }
}

