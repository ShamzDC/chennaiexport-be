
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class InvoiceDetails
    {
        public void GetData(protoInvoiceDetails aprotoInvoiceDetails)
        {
            ProtoDataConverter.GetData(aprotoInvoiceDetails, this);
        }

        public protoInvoiceDetails GetProto()
        {
            protoInvoiceDetails lprotoInvoiceDetails = new protoInvoiceDetails();
            ProtoDataConverter.GetProto(this, lprotoInvoiceDetails);
            ProtoDataConverter.SetMessages(this, lprotoInvoiceDetails);
            return lprotoInvoiceDetails;
        }
    }
}

