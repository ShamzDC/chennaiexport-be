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
    public partial class OrderSearch : SearchBase
    {
        [DataMember]
        public long order_detail_id { get; set; }

        [DataMember]
        public string order_ref_no { get; set; }

        [DataMember]
        public DateTime? order_date { get; set; }
        [DataMember]
        public long customer_id { get; set; }

        [DataMember]
        public long warehouse_id { get; set; }

        [DataMember]
        public string is_within_chennai { get; set; }
        [DataMember]
        public string is_invoice { get; set; }

        [DataMember]
        public int status_id { get; set; }

        [DataMember]
        public string status_value { get; set; }

        [DataMember]
        public string entered_by { get; set; }

        [DataMember]
        public DateTime? entered_date { get; set; }

        [DataMember]
        public string changed_by { get; set; }

        [DataMember]
        public DateTime? changed_date { get; set; }
        [DataMember]
        public decimal total_amount { get; set; }
        [DataMember]
        public string changed_by_full_name { get; set; }
        [DataMember]
        public string entered_by_full_name { get; set; }

        [DataMember]
        public string status_description { get; set; }
        [DataMember]
        public int region_id { get; set; }

        [DataMember]
        public string region_value { get; set; }

        [DataMember]
        public string region_description { get; set; }
        [DataMember]
        public DateTime? order_date_from { get; set; }
        [DataMember]
        public DateTime? order_date_to { get; set; }

    }

    [Serializable]
    [DataContract]
    public partial class OrderSearchResultset : SearchResultSet
    {

        [DataMember]
        public long order_detail_id { get; set; }

        [DataMember]
        public string order_ref_no { get; set; }

        [DataMember]
        public DateTime? order_date { get; set; }
        [DataMember]
        public long customer_id { get; set; }

        [DataMember]
        public long warehouse_id { get; set; }

        [DataMember]
        public string is_within_chennai { get; set; }
        [DataMember]
        public string is_invoice { get; set; }

        [DataMember]
        public int status_id { get; set; }

        [DataMember]
        public string status_value { get; set; }

        [DataMember]
        public string entered_by { get; set; }

        [DataMember]
        public DateTime? entered_date { get; set; }

        [DataMember]
        public string changed_by { get; set; }

        [DataMember]
        public DateTime? changed_date { get; set; }
        [DataMember]
        public decimal total_amount { get; set; }
        [DataMember]
        public string changed_by_full_name { get; set; }
        [DataMember]
        public string entered_by_full_name { get; set; }

        [DataMember]
        public string status_description { get; set; }
        [DataMember]
        public int region_id { get; set; }

        [DataMember]
        public string region_value { get; set; }

        [DataMember]
        public string region_description { get; set; }
        [DataMember]
        public string customer_Details { get; set; }
        [DataMember]
        public string WareHouse_details { get; set; }
        [DataMember]
        public int total_pakage { get; set; }
        [DataMember]
        public int pending_pakage { get; set; }
        [DataMember]
        public string customer_name { get; set; }
        [DataMember]
        public string warehouse_name { get; set; }
        [DataMember]
        public string customer_details { get; set; }
        [DataMember]
        public string warehouse_details { get; set; }  [DataMember]
        public string vendor_name { get; set; }


        public string order_detail_id_column_name_is_primary = "ORDER_DETAIL_ID";
        public string order_ref_no_column_name = "ORDER_REF_NO";
        public string order_date_column_name = "ORDER_DATE";
        public string customer_id_column_name = "CUSTOMER_ID";
        public string warehouse_id_column_name = "WAREHOUSE_ID";
        public string is_within_chennai_column_name = "IS_WITHIN_CHENNAI";
        public string is_invoice_column_name = "IS_INVOICE";
        public string total_amount_column_name = "TOTAL_AMOUNT";
        public string status_id_column_name = "STATUS_ID";
        public string status_value_column_name = "STATUS_VALUE";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        public string changed_date_column_name = "CHANGED_DATE";
        public string region_id_column_name = "REGION_ID";
        public string region_value_column_name = "REGION_VALUE";
        public string status_description_column_name = "STATUS_DESCRIPTION";
        public string region_description_column_name = "REGION_DESCRIPTION";  
        public string entered_by_full_name_column_name = "ENTERED_BY_FULL_NAME";
        public string changed_by_full_name_column_name = "CHANGED_BY_FULL_NAME";
        public string customer_Details_column_name = "CUSTOMER_DETAILS";
        public string WareHouse_details_column_name = "WAREHOUSE_DETAILS";
            public string total_pakage_column_name = "TOTAL_PAKAGE";
        public string pending_pakage_column_name = "PENDING_PAKAGE";
        public string customer_name_column_name = "CUSTOMER_NAME";
        public string warehouse_name_column_name = "WAREHOUSE_NAME";
        public string customer_details_column_name = "CUSTOMER_DETAILS";
        public string warehouse_details_column_name = "WAREHOUSE_DETAILS";
        public string vendor_name_column_name = "VENDOR_NAME";
    }

}
