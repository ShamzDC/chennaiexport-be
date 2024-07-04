using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class RoleResource
    {
        public void GetData(protoRoleResource aprotoRoleResource)
        {
            ProtoDataConverter.GetData(aprotoRoleResource, this);
        }

        public protoRoleResource GetProto()
        {
            protoRoleResource lprotoRoleResource = new protoRoleResource();
            ProtoDataConverter.GetProto(this, lprotoRoleResource);
            ProtoDataConverter.SetMessages(this, lprotoRoleResource);
            return lprotoRoleResource;
        }
    }
}
