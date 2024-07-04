
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class Vendor
    {
        public void GetData(protoVendor aprotoVendor)
        {
            ProtoDataConverter.GetData(aprotoVendor, this);
        }

        public protoVendor GetProto()
        {
            protoVendor lprotoVendor = new protoVendor();
            ProtoDataConverter.GetProto(this, lprotoVendor);
            ProtoDataConverter.SetMessages(this, lprotoVendor);
            return lprotoVendor;
        }
    }
}



