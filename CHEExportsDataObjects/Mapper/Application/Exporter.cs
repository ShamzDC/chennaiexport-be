
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class Exporter
    {
        public void GetData(protoExporter aprotoExporter)
        {
            ProtoDataConverter.GetData(aprotoExporter, this);
        }

        public protoExporter GetProto()
        {
            protoExporter lprotoExporter = new protoExporter();
            ProtoDataConverter.GetProto(this, lprotoExporter);
            ProtoDataConverter.SetMessages(this, lprotoExporter);
            return lprotoExporter;
        }
    }
}



