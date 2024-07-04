
   using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
    {
        public partial class MessageConfig
        {
            public void GetData(protoMessageConfig aprotoMessage)
            {
                ProtoDataConverter.GetData(aprotoMessage, this);
            }

            public protoMessageConfig GetProto()
            {
               protoMessageConfig lprotoMessageConfig = new protoMessageConfig();
                ProtoDataConverter.GetProto(this, lprotoMessageConfig);
                ProtoDataConverter.SetMessages(this, lprotoMessageConfig);
                return lprotoMessageConfig;
            }
        }
    }

