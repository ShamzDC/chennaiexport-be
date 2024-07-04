
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class RepackingDetail
    {
        public void GetData(protoRepackingDetail aprotoRepackingDetail)
        {
            ProtoDataConverter.GetData(aprotoRepackingDetail, this);
            if (aprotoRepackingDetail.IprotoOrderDetails != null)
            {
                this.lOrderDetails.GetData(aprotoRepackingDetail.IprotoOrderDetails);
            }if (aprotoRepackingDetail.IprotoRepackingListDetail != null)
            {
                this.lRepackingListDetail.GetData(aprotoRepackingDetail.IprotoRepackingListDetail);
            }
            if (aprotoRepackingDetail.LstprotoRepackingListDetail != null && aprotoRepackingDetail.LstprotoRepackingListDetail.Count > 0)
            {
                if (this.lstRepackingListDetail != null)
                {
                    foreach (protoRepackingListDetail obj in aprotoRepackingDetail.LstprotoRepackingListDetail)
                    {
                        RepackingListDetail lRepackingListDetail = new RepackingListDetail();
                        lRepackingListDetail.GetData(obj);
                        this.lstRepackingListDetail.Add(lRepackingListDetail);
                    }
                }
            } if (aprotoRepackingDetail.LstprotoOrderDetails != null && aprotoRepackingDetail.LstprotoOrderDetails.Count > 0)
            {
                if (this.lstOrderDetails != null)
                {
                    foreach (protoOrderDetails obj in aprotoRepackingDetail.LstprotoOrderDetails)
                    {
                        OrderDetails lOrderDetails = new OrderDetails();
                        lOrderDetails.GetData(obj);
                        this.lstOrderDetails.Add(lOrderDetails);
                    }
                }
            }
            if (aprotoRepackingDetail.IprotoCustomer != null)
            {
                this.iCustomer.GetData(aprotoRepackingDetail.IprotoCustomer);
            }
        }
        public protoRepackingDetail GetProto()
        {
            protoRepackingDetail lprotoRepackingDetail = new protoRepackingDetail();
            ProtoDataConverter.GetProto(this, lprotoRepackingDetail);
            if (this.lOrderDetails != null)
            {
                lprotoRepackingDetail.IprotoOrderDetails = this.lOrderDetails.GetProto();
            }
            if (this.lRepackingListDetail != null)
            {
                lprotoRepackingDetail.IprotoRepackingListDetail = this.lRepackingListDetail.GetProto();
            }
            if (this.lstRepackingListDetail != null)
            {
                foreach (RepackingListDetail obj in this.lstRepackingListDetail)
                {
                    lprotoRepackingDetail.LstprotoRepackingListDetail.Add(obj.GetProto());
                }
            }  if (this.lstOrderDetails != null)
            {
                foreach (OrderDetails obj in this.lstOrderDetails)
                {
                    lprotoRepackingDetail.LstprotoOrderDetails.Add(obj.GetProto());
                }
            }
            if (this.iCustomer != null)
            {
                lprotoRepackingDetail.IprotoCustomer = this.iCustomer.GetProto();
            }
            ProtoDataConverter.SetMessages(this, lprotoRepackingDetail);
            return lprotoRepackingDetail;
        }
    }
}



