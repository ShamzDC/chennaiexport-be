
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
    public partial class LRDetails : DataObjectBase
    {
        public LRDetails()
        {
            TABLE_NAME = "APP_LR_DETAILS";
            transport_name_id = Constants.Application.transport_name_id;
            package_type_id = Constants.Application.package_type_id;
            mode_of_packing_id = Constants.Application.mode_of_packing_id;
        }

        public string TABLE_NAME { get; set; }

        [DataMember]
        public long lr_detail_id { get; set; }

        [DataMember]
        public long order_detail_id { get; set; }

        [DataMember]
        public DateTime? received_date { get; set; }
        [DataMember]
        public string lr_no { get; set; }

        [DataMember]
        public int transport_name_id { get; set; }

        [DataMember]
        public string transport_name_value { get; set; }
        [DataMember]
        public long vendor_id { get; set; }

        [DataMember]
        public string consignee_name { get; set; }

        [DataMember]
        public int package_type_id { get; set; }
        [DataMember]
        public string package_type_value { get; set; }

        [DataMember]
        public int mode_of_packing_id { get; set; }

        [DataMember]
        public string mode_of_packing_value { get; set; }
        [DataMember]
        public decimal net_weight { get; set; }

        [DataMember]
        public decimal gross_weight { get; set; }

        [DataMember]
        public decimal value_of_goods_as_per_invoice { get; set; }
        [DataMember]
        public decimal to_pay_freight_charges { get; set; }

        [DataMember]
        public string lr_photo_copy { get; set; }

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
        public string transport_name_description { get; set; }

        [DataMember]
        public string package_type_description { get; set; }
        [DataMember]
        public string mode_of_packing_description { get; set; }
        [DataMember]
        public long consignee_id { get; set; }
        [DataMember]
        public string Vendor_details { get; set; }
        [DataMember]
        public string Consignee_details { get; set; }

        public string lr_detail_id_column_name_is_primary = "LR_DETAIL_ID";
        public string order_detail_id_column_name = "ORDER_DETAIL_ID";
        public string lr_no_column_name = "LR_NO";
        public string received_date_column_name = "RECEIVED_DATE";
        public string transport_name_id_column_name = "TRANSPORT_NAME_ID";
        public string transport_name_value_column_name = "TRANSPORT_NAME_VALUE";
        public string vendor_id_column_name = "VENDOR_ID";
        public string consignee_name_column_name = "CONSIGNEE_NAME";
        public string package_type_id_column_name = "PACKAGE_TYPE_ID";
        public string package_type_value_column_name = "PACKAGE_TYPE_VALUE";
        public string mode_of_packing_id_column_name = "MODE_OF_PACKING_ID";
        public string mode_of_packing_value_column_name = "MODE_OF_PACKING_VALUE";
        public string net_weight_column_name = "NET_WEIGHT";
        public string gross_weight_column_name = "GROSS_WEIGHT";
        public string value_of_goods_as_per_invoice_column_name = "VALUE_OF_GOODS_AS_PER_INVOICE";
        public string to_pay_freight_charges_column_name = "TO_PAY_FREIGHT_CHARGES";
        public string lr_photo_copy_column_name = "LR_PHOTO_COPY";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        public string changed_date_column_name = "CHANGED_DATE";
        public string consignee_id_column_name = "CONSIGNEE_ID";
    }
}

