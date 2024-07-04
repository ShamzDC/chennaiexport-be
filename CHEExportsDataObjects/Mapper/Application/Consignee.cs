
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class Consignee
    {
        public void GetData(protoConsignee aprotoConsignee)
        {
            ProtoDataConverter.GetData(aprotoConsignee, this);
        }

        public protoConsignee GetProto()
        {
            protoConsignee lprotoConsignee = new protoConsignee();
            ProtoDataConverter.GetProto(this, lprotoConsignee);
            ProtoDataConverter.SetMessages(this, lprotoConsignee);
            return lprotoConsignee;
        }
    }
}



