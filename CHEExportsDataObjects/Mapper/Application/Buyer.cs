
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class Buyer
    {
        public void GetData(protoBuyer aprotoBuyer)
        {
            ProtoDataConverter.GetData(aprotoBuyer, this);
        }

        public protoBuyer GetProto()
        {
            protoBuyer lprotoBuyer = new protoBuyer();
            ProtoDataConverter.GetProto(this, lprotoBuyer);
            ProtoDataConverter.SetMessages(this, lprotoBuyer);
            return lprotoBuyer;
        }
    }
}



