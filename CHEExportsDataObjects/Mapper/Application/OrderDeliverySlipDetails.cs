
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class OrderDeliverySlipDetails
    {
        public void GetData(protoOrderDeliverySlipDetail aprotoOrderDeliverySlipDetails)
        {
            ProtoDataConverter.GetData(aprotoOrderDeliverySlipDetails, this);
        }

        public protoOrderDeliverySlipDetail GetProto()
        {
            protoOrderDeliverySlipDetail lprotoOrderDeliverySlipDetails = new protoOrderDeliverySlipDetail();
            ProtoDataConverter.GetProto(this, lprotoOrderDeliverySlipDetails);
            ProtoDataConverter.SetMessages(this, lprotoOrderDeliverySlipDetails);
            return lprotoOrderDeliverySlipDetails;
        }
    }
    public class OrderDeliverySlipDetailsList
    {
        public protoOrderDeliverySlipDetailList GetProto(List<OrderDeliverySlipDetails> ilstOrderDeliverySlipDetails)
        {
            protoOrderDeliverySlipDetailList lprotoOrderDeliverySlipDetailList = new protoOrderDeliverySlipDetailList();
            if (ilstOrderDeliverySlipDetails != null && ilstOrderDeliverySlipDetails.Count > 0)
            {
                foreach (OrderDeliverySlipDetails obj in ilstOrderDeliverySlipDetails)
                {
                    lprotoOrderDeliverySlipDetailList.LstprotoOrderDeliverySlipDetail.Add(obj.GetProto());
                }
            }
            return lprotoOrderDeliverySlipDetailList;
        }
    }
}


