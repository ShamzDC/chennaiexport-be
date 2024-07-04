using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
    {
        public partial class Customer
        {
            public void GetData(protoCustomer aprotoCustomer)
            {
                ProtoDataConverter.GetData(aprotoCustomer, this);
            }

            public protoCustomer GetProto()
            {
                protoCustomer lprotoCustomer = new protoCustomer();
                ProtoDataConverter.GetProto(this, lprotoCustomer);
                ProtoDataConverter.SetMessages(this, lprotoCustomer);
                return lprotoCustomer;
            }
        }
    }

