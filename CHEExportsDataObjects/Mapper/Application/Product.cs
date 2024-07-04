
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class Product
    {
        public void GetData(protoProduct aprotoProduct)
        {
            ProtoDataConverter.GetData(aprotoProduct, this);
        }

        public protoProduct GetProto()
        {
            protoProduct lprotoProduct = new protoProduct();
            ProtoDataConverter.GetProto(this, lprotoProduct);
            ProtoDataConverter.SetMessages(this, lprotoProduct);
            return lprotoProduct;
        }
    }
}



