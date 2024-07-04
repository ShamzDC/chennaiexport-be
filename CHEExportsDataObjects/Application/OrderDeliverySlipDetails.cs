using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CHEExportsDataObjects
{
    [Serializable]
    [DataContract]
    public partial class OrderDeliverySlipDetails : DataObjectBase
    {
        public OrderDeliverySlipDetails()
        {
            TABLE_NAME = "APP_ORDER_DELIVERY_SLIP_DETAIL";
            unit_type_id = Constants.Application.Unit_type_id;
            lProduct = new Product();
        }

        public string TABLE_NAME { get; set; }


        [DataMember]
        public long order_delivery_slip_detail_id { get; set; }

        [DataMember]
        public long order_detail_id { get; set; }


        [DataMember]
        public string delivery_slip_photo_copy { get; set; }


        [DataMember]
        public string entered_by { get; set; }

        [DataMember]
        public DateTime? entered_date { get; set; }

        [DataMember]
        public string changed_by { get; set; }

        [DataMember]
        public DateTime? changed_date { get; set; }
        [DataMember]
        public string changed_by_full_name { get; set; }
        [DataMember]
        public string entered_by_full_name { get; set; }
        [DataMember]
        public decimal quantity { get; set; }
        [DataMember]
        public long product_id { get; set; }
        [DataMember]
        public decimal rate { get; set; }
        [DataMember]
        public decimal amount { get; set; }
        [DataMember]
        public int unit_type_id { get; set; }
        [DataMember]
        public string unit_type_value { get; set; }
        [DataMember]
        public string unit_Description { get; set; }
        [DataMember]
        public string ProductDetails { get; set; }
        [DataMember]
        public string HSN_NO { get; set; }
         [DataMember]
        public Product lProduct { get; set; }



        public string order_delivery_slip_detail_id_column_name_is_primary = "ORDER_DELIVERY_SLIP_DETAIL_ID";
        public string order_detail_id_column_name = "ORDER_DETAIL_ID";
        public string quantity_column_name = "QUANTITY";
        public string product_id_column_name = "PRODUCT_ID";
        public string rate_column_name = "RATE";
        public string amount_column_name = "AMOUNT";
        public string delivery_slip_photo_copy_column_name = "DELIVERY_SLIP_PHOTO_COPY";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        public string changed_date_column_name = "CHANGED_DATE";
        public string unit_type_id_column_name = "UNIT_TYPE_ID";
        public string unit_type_value_column_name = "UNIT_TYPE_VALUE";

  
    }
}

