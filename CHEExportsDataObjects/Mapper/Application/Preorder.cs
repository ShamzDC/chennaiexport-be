
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class Preorder
    {
        public void GetData(protoPreorder aprotoPreorder)
        {
            ProtoDataConverter.GetData(aprotoPreorder, this);
            if (aprotoPreorder.IprotoCustomer != null)
            {
                this.iCustomer.GetData(aprotoPreorder.IprotoCustomer);
            }
        }

        public protoPreorder GetProto()
        {
            protoPreorder lprotoPreorder = new protoPreorder();
            ProtoDataConverter.GetProto(this, lprotoPreorder);
            if (this.iCustomer != null)
            {
                lprotoPreorder.IprotoCustomer = this.iCustomer.GetProto();
            }
            ProtoDataConverter.SetMessages(this, lprotoPreorder);
            return lprotoPreorder;
        }
    }
    public class PreorderList
    {
        public protoPreorderList GetProto(List<Preorder> aPreorderlist)
        {
            protoPreorderList lprotoPreorderList = new protoPreorderList();
            if (aPreorderlist != null && aPreorderlist.Count > 0)
            {
                foreach (Preorder obj in aPreorderlist)
                {
                    lprotoPreorderList.LatprotoPreorder.Add(obj.GetProto());
                }
            }
            return lprotoPreorderList;
        }
    }
}



