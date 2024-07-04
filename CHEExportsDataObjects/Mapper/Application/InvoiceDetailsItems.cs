
  using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class InvoiceDetailsItems
    {
        public void GetData(protoInvoiceDetailsItems aprotoInvoiceDetailsItems)
        {
            ProtoDataConverter.GetData(aprotoInvoiceDetailsItems, this);
        }

        public protoInvoiceDetailsItems GetProto()
        {
            protoInvoiceDetailsItems lprotoInvoiceDetailsItems = new protoInvoiceDetailsItems();
            ProtoDataConverter.GetProto(this, lprotoInvoiceDetailsItems);
            ProtoDataConverter.SetMessages(this, lprotoInvoiceDetailsItems);
            return lprotoInvoiceDetailsItems;
        }
    }
    public class InvoiceDetailsItemsList
    {
        public protoInvoiceDetailsItemsList GetProto(List<InvoiceDetailsItems> ilstInvoiceDetailsItems)
        {
            protoInvoiceDetailsItemsList lprotoInvoiceDetailsItemsList = new protoInvoiceDetailsItemsList();
            if (ilstInvoiceDetailsItems != null && ilstInvoiceDetailsItems.Count > 0)
            {
                foreach (InvoiceDetailsItems obj in ilstInvoiceDetailsItems)
                {
                    lprotoInvoiceDetailsItemsList.LstprotoInvoiceDetailsItems.Add(obj.GetProto());
                }
            }
            return lprotoInvoiceDetailsItemsList;
        }
    }
}
