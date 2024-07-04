
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class Party
    {
        public void GetData(protoParty aprotoParty)
        {
            ProtoDataConverter.GetData(aprotoParty, this);
        }

        public protoParty GetProto()
        {
            protoParty lprotoParty = new protoParty();
            ProtoDataConverter.GetProto(this, lprotoParty);
            ProtoDataConverter.SetMessages(this, lprotoParty);
            return lprotoParty;
        }
    }
}



