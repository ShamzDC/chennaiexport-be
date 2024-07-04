using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class RoleGroup
    {
        public void GetData(protoRoleGroup aprotoRoleGroup)
        {
            ProtoDataConverter.GetData(aprotoRoleGroup, this);
        }

        public protoRoleGroup GetProto()
        {
            protoRoleGroup lprotoRoleGroup = new protoRoleGroup();
            ProtoDataConverter.GetProto(this, lprotoRoleGroup);
            ProtoDataConverter.SetMessages(this, lprotoRoleGroup);
            return lprotoRoleGroup;
        }
    }
}
