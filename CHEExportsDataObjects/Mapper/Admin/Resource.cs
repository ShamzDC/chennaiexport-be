using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class Resource
    {
        public void GetData(protoResource aprotoResource)
        {
            ProtoDataConverter.GetData(aprotoResource, this);
        }

        public protoResource GetProto()
        {
            protoResource lprotoResource = new protoResource();
            ProtoDataConverter.GetProto(this, lprotoResource);
            ProtoDataConverter.SetMessages(this, lprotoResource);
            return lprotoResource;
        }
    }
}
