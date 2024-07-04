
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class FinalPackingDetail
    {
        public void GetData(protoFinalPackingDetail aprotoFinalPackingDetail)
        {
            ProtoDataConverter.GetData(aprotoFinalPackingDetail, this);
            if (aprotoFinalPackingDetail.IprotoCustomer != null)
            {
                this.iCustomer.GetData(aprotoFinalPackingDetail.IprotoCustomer);
            }
        }

        public protoFinalPackingDetail GetProto()
        {
            protoFinalPackingDetail lprotoFinalPackingDetail = new protoFinalPackingDetail();
            ProtoDataConverter.GetProto(this, lprotoFinalPackingDetail);
            if (this.iCustomer != null)
            {
                lprotoFinalPackingDetail.IprotoCustomer = this.iCustomer.GetProto();
            }
            ProtoDataConverter.SetMessages(this, lprotoFinalPackingDetail);
            return lprotoFinalPackingDetail;
        }
    }
}



