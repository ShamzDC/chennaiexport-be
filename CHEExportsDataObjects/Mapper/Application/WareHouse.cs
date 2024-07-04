

using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class WareHouse
    {
        public void GetData(protoWareHouse aprotoWareHouse)
        {
            ProtoDataConverter.GetData(aprotoWareHouse, this);
        }

        public protoWareHouse GetProto()
        {
            protoWareHouse lprotoWareHouse = new protoWareHouse();
            ProtoDataConverter.GetProto(this, lprotoWareHouse);
            ProtoDataConverter.SetMessages(this, lprotoWareHouse);
            return lprotoWareHouse;
        }
    }
}




