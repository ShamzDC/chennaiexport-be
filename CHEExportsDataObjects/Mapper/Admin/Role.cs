using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class Role
    {
        public void GetData(protoRole aprotoRole)
        {
            ProtoDataConverter.GetData(aprotoRole, this);
        }

        public protoRole GetProto()
        {
            protoRole lprotoRole = new protoRole();
            ProtoDataConverter.GetProto(this, lprotoRole);
            ProtoDataConverter.SetMessages(this, lprotoRole);
            return lprotoRole;
        }
    }
}
