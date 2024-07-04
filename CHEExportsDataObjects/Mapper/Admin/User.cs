using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class User
    {
        public void GetData(protoUser aprotoUser)
        {
            ProtoDataConverter.GetData(aprotoUser, this);
        }

        public protoUser GetProto()
        {
            protoUser lprotoUser = new protoUser();
            ProtoDataConverter.GetProto(this, lprotoUser);
            ProtoDataConverter.SetMessages(this, lprotoUser);
            return lprotoUser;
        }
    }
}
