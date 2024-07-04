
  using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class OrderDeliverySlipDetailsItems
    {
        public void GetData(protoOrderDeliverySlipDetailsItems aprotoOrderDeliverySlipDetailsItems)
        {
            ProtoDataConverter.GetData(aprotoOrderDeliverySlipDetailsItems, this);
        }

        public protoOrderDeliverySlipDetailsItems GetProto()
        {
            protoOrderDeliverySlipDetailsItems lprotoOrderDeliverySlipDetailsItems = new protoOrderDeliverySlipDetailsItems();
            ProtoDataConverter.GetProto(this, lprotoOrderDeliverySlipDetailsItems);
            ProtoDataConverter.SetMessages(this, lprotoOrderDeliverySlipDetailsItems);
            return lprotoOrderDeliverySlipDetailsItems;
        }
    }
}
