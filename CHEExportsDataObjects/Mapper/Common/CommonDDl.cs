

using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class CommonDDl
    {
        public void GetData(protoCommonDDl aprotoCommonDDl)
        {
            ProtoDataConverter.GetData(aprotoCommonDDl, this);
        }

        public protoCommonDDl GetProto()
        {
            protoCommonDDl lprotoCommonDDl = new protoCommonDDl();
            ProtoDataConverter.GetProto(this, lprotoCommonDDl);
            return lprotoCommonDDl;
        }
    }
    public class CommonDDlList
    {
        public protoCommonDDlList GetProto(List<CommonDDl> lstCommonDDl)
        {
            protoCommonDDlList lprotoCommonDDlList = new protoCommonDDlList();
            if (lstCommonDDl != null && lstCommonDDl.Count > 0)
            {
                foreach (CommonDDl lCommonDDl in lstCommonDDl)
                {
                    lprotoCommonDDlList.LstprotoCommonDDl.Add(lCommonDDl.GetProto());
                }
            }
            return lprotoCommonDDlList;
        }
    }
}


