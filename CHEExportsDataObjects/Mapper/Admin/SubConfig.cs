using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class SubConfig
    {
        public void GetData(protoSubConfig aprotoSubConfig)
        {
            ProtoDataConverter.GetData(aprotoSubConfig, this);
        }

        public protoSubConfig GetProto()
        {
            protoSubConfig lprotoSubConfig = new protoSubConfig();
            ProtoDataConverter.GetProto(this, lprotoSubConfig);
            ProtoDataConverter.SetMessages(this, lprotoSubConfig);
            return lprotoSubConfig;
        }
    }
    public class SubConfigList
    {
        public protoSubConfigList GetProto(List<SubConfig> ilstSubConfig)
        {
            protoSubConfigList lprotoSubConfigList = new protoSubConfigList();
            if (ilstSubConfig != null && ilstSubConfig.Count > 0)
            {
                foreach (SubConfig obj in ilstSubConfig)
                {
                    lprotoSubConfigList.IlstprotoSubConfig.Add(obj.GetProto());
                }
            }
            return lprotoSubConfigList;
        }
    }
   
}

