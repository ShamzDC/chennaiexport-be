using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class Group
    {
        public void GetData(protoGroup aprotoGroup)
        {
            ProtoDataConverter.GetData(aprotoGroup, this);
        }

        public protoGroup GetProto()
        {
            protoGroup lprotoGroup = new protoGroup();
            ProtoDataConverter.GetProto(this, lprotoGroup);
            ProtoDataConverter.SetMessages(this, lprotoGroup);
            return lprotoGroup;
        }
    }
}
