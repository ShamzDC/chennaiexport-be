﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    [Serializable]
    [DataContract]
    public partial class FinalPakingDetailsSearch : SearchBase
    {
        [DataMember]
        public long repacking_detail_id { get; set; }

        [DataMember]
        public long order_id { get; set; }

        [DataMember]
        public int status_id { get; set; }
        [DataMember]
        public string status_value { get; set; }

        [DataMember]
        public decimal total_quantity { get; set; }

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
        public string status_description { get; set; }

    }

    [Serializable]
    [DataContract]
    public partial class FinalPackingDetailsSearchResultset : SearchResultSet
    {
        [DataMember] public long final_packing_detail_id { get; set; }
        [DataMember] public long repacking_detail_id { get; set; }
        [DataMember] public long order_id { get; set; }
        [DataMember] public string package_no { get; set; }
        [DataMember] public long product_id { get; set; }
        [DataMember] public int package_type_id { get; set; }
        [DataMember] public string package_type_value { get; set; }
        [DataMember] public decimal quantity { get; set; }
        [DataMember] public int unit_type_id { get; set; }
        [DataMember] public string unit_type_value { get; set; }
        [DataMember] public decimal net_weight { get; set; }
        [DataMember] public decimal gross_weight { get; set; }
        [DataMember] public int status_id { get; set; }
        [DataMember] public string status_value { get; set; }
        [DataMember] public string entered_by { get; set; }
        [DataMember] public DateTime? entered_date { get; set; }
        [DataMember] public string changed_by { get; set; }
        [DataMember] public DateTime? changed_date { get; set; }
        [DataMember] public string changed_by_full_name { get; set; }
        [DataMember] public string entered_by_full_name { get; set; }
        [DataMember]
        public string package_type_description { get; set; }
        [DataMember]
        public string unit_type_description { get; set; }
        [DataMember]
        public string status_description { get; set; }
        public int region_id { get; set; }
        [DataMember]
        public string region_value { get; set; }
        [DataMember]
        public string region_description { get; set; }
        [DataMember]
        public long customer_id { get; set; }

        [DataMember]
        public string order_ref_no { get; set; }
        [DataMember]
        public DateTime? order_date { get; set; }
        [DataMember]
        public DateTime? repacked_date { get; set; }
        [DataMember]
        public string customer_name { get; set; }
         [DataMember]
        public string product_name { get; set; }
        [DataMember]
        public string customer_details { get; set; }
        [DataMember]
        public string vendor_details { get; set; }



        public string final_packing_detail_id_column_name_is_primary = "FINAL_PACKING_DETAIL_ID";
        public string repacking_detail_id_column_name = "REPACKING_DETAIL_ID";
        public string order_id_column_name = "ORDER_ID";
        public string package_no_column_name = "PACKAGE_NO";
        public string product_id_column_name = "PRODUCT_ID";
        public string package_type_id_column_name = "PACKAGE_TYPE_ID";
        public string package_type_value_column_name = "PACKAGE_TYPE_VALUE";
        public string quantity_column_name = "QUANTITY";
        public string unit_type_id_column_name = "UNIT_TYPE_ID";
        public string unit_type_value_column_name = "UNIT_TYPE_VALUE";
        public string net_weight_column_name = "NET_WEIGHT";
        public string gross_weight_column_name = "GROSS_WEIGHT";
        public string status_id_column_name = "STATUS_ID";
        public string status_value_column_name = "STATUS_VALUE";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        public string changed_date_column_name = "CHANGED_DATE";
        public string customer_id_column_name = "CUSTOMER_ID";
        public string region_id_column_name = "REGION_ID";
        public string region_value_column_name = "REGION_VALUE";
        public string region_description_column_name = "REGION_DESCRIPTION";
        public string package_type_description_column_name = "PACKAGE_TYPE_DESCRIPTION";
        public string unit_type_description_column_name = "UNIT_TYPE_DESCRIPTION";
        public string status_description_column_name = "STATUS_DESCRIPTION";

            public string entered_by_full_name_column_name = "ENTERED_BY_FULL_NAME";
        public string changed_by_full_name_column_name = "CHANGED_BY_FULL_NAME";   
        public string order_ref_no_column_name = "ORDER_REF_NO";
        public string order_date_column_name = "ORDER_DATE"; 
        public string repacked_date_column_name = "REPACKED_DATE";
        public string customer_name_column_name = "CUSTOMER_NAME";
        public string product_name_column_name = "PRODUCT_NAME";
        public string customer_details_column_name = "CUSTOMER_DETAILS";
        public string vendor_details_column_name = "VENDOR_DETAILS";
    }

}
