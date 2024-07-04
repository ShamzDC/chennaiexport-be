

using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class LRDetails
    {
        public void GetData(protoLRDetails aprotoLRDetails)
        {
            ProtoDataConverter.GetData(aprotoLRDetails, this);
        }

        public protoLRDetails GetProto()
        {
            protoLRDetails lprotoLRDetails = new protoLRDetails();
            ProtoDataConverter.GetProto(this, lprotoLRDetails);
            ProtoDataConverter.SetMessages(this, lprotoLRDetails);
            return lprotoLRDetails;
        }
    }
}


