
  using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class MasterConfig
    {
        public void GetData(protoMasterConfig aprotoMasterConfig)
        {
            ProtoDataConverter.GetData(aprotoMasterConfig, this);
        }

        public protoMasterConfig GetProto()
        {
            protoMasterConfig lprotoMasterConfig = new protoMasterConfig();
            ProtoDataConverter.GetProto(this, lprotoMasterConfig);
            ProtoDataConverter.SetMessages(this, lprotoMasterConfig);
            return lprotoMasterConfig;
        }
    }
}
