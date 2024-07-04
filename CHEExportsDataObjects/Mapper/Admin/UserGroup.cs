using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class UserGroup
    {
        public void GetData(protoUserGroup aprotoUserGroup)
        {
            ProtoDataConverter.GetData(aprotoUserGroup, this);
        }

        public protoUserGroup GetProto()
        {
            protoUserGroup lprotoUserGroup = new protoUserGroup();
            ProtoDataConverter.GetProto(this, lprotoUserGroup);
            ProtoDataConverter.SetMessages(this, lprotoUserGroup);
            return lprotoUserGroup;
        }
    }
}
