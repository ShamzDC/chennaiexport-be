
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class RepackingListDetail
    {
        public void GetData(protoRepackingListDetail aprotoRepackingListDetail)
        {
            ProtoDataConverter.GetData(aprotoRepackingListDetail, this);
            if (aprotoRepackingListDetail.IprotoProduct != null)
            {
                this.iProduct.GetData(aprotoRepackingListDetail.IprotoProduct);
            }
        }

        public protoRepackingListDetail GetProto()
        {
            protoRepackingListDetail lprotoRepackingListDetail = new protoRepackingListDetail();
            ProtoDataConverter.GetProto(this, lprotoRepackingListDetail);
            if (this.iProduct != null)
            {
                lprotoRepackingListDetail.IprotoProduct = this.iProduct.GetProto();
            }
            ProtoDataConverter.SetMessages(this, lprotoRepackingListDetail);
            return lprotoRepackingListDetail;
        }
    }
    public class RepackingListDetailList
    {
        public protoRepackingListDetailList GetProto(List<RepackingListDetail> ilstilstRepackingListDetail)
        {
            protoRepackingListDetailList lprotoRepackingListDetailList = new protoRepackingListDetailList();
            if (ilstilstRepackingListDetail != null && ilstilstRepackingListDetail.Count > 0)
            {
                foreach (RepackingListDetail obj in ilstilstRepackingListDetail)
                {
                    lprotoRepackingListDetailList.LstprotoRepackingListDetail.Add(obj.GetProto());
                }
            }
            return lprotoRepackingListDetailList;
        }
    }
}



