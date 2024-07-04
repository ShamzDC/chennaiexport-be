using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
    {
        [Serializable]
        [DataContract]
        public partial class OrderDeliverySlipDetailWithoutInvoice : DataObjectBase
        {
            public OrderDeliverySlipDetailWithoutInvoice()
            {
                TABLE_NAME = "APP_ORDER_DELIVERY_SLIP_DETAIL_WITHOUT_INVOICE";
            }
   
        public string TABLE_NAME { get; set; }

        [DataMember]
        public long order_delivery_slip_without_id { get; set; }

        [DataMember]
        public long order_detail_id { get; set; }

        [DataMember]
        public string delivery_slip_number { get; set; }

        [DataMember]
        public DateTime? received_date { get; set; }

        [DataMember]
        public long vendor_id { get; set; }

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
        public string Vendor_details { get; set; }
        [DataMember]
        public string content { get; set; } 
        [DataMember]
        public decimal total_amount { get; set; }
        [DataMember]
        public long consignee_id { get; set; }
        [DataMember]
        public string consignee_details { get; set; }


        public string order_delivery_slip_without_id_column_name_is_primary = "ORDER_DELIVERY_SLIP_WITHOUT_ID";
        public string order_detail_id_column_name = "ORDER_DETAIL_ID";
        public string delivery_slip_number_column_name = "DELIVERY_SLIP_NUMBER";
        public string received_date_column_name = "RECEIVED_DATE";
        public string vendor_id_column_name = "VENDOR_ID";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        public string changed_date_column_name = "CHANGED_DATE";
        public string content_column_name = "CONTENT";
        public string total_amount_column_name = "TOTAL_AMOUNT";
        public string consignee_id_column_name = "CONSIGNEE_ID";
    }
}

