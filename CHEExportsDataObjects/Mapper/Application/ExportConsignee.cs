
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class ExportConsignee
    {
        public void GetData(protoExportConsignee aprotoExportConsignee)
        {
            ProtoDataConverter.GetData(aprotoExportConsignee, this);
        }

        public protoExportConsignee GetProto()
        {
            protoExportConsignee lprotoExportConsignee = new protoExportConsignee();
            ProtoDataConverter.GetProto(this, lprotoExportConsignee);
            ProtoDataConverter.SetMessages(this, lprotoExportConsignee);
            return lprotoExportConsignee;
        }
    }
}



