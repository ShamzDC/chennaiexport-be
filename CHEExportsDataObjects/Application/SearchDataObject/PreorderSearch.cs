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
    public partial class PreorderSearch : SearchBase
    {

        [DataMember]
        public long preorder_id { get; set; }

        [DataMember]
        public long order_id { get; set; }

        [DataMember]
        public string preorder_ref_no { get; set; }

        [DataMember]
        public DateTime? order_expected_date { get; set; }
        [DataMember]
        public long customer_id { get; set; }

        [DataMember]
        public string vendor_cognisee_type { get; set; }

        [DataMember]
        public string notes { get; set; }

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
        public string status_description { get; set; }

        [DataMember]
        public DateTime? changed_date { get; set; }
        [DataMember]
        public int region_id { get; set; }
        [DataMember]
        public string region_value { get; set; }
        [DataMember]
        public string region_description { get; set; }

    }

    [Serializable]
    [DataContract]
    public partial class PreorderSearchResultset : SearchResultSet
    {
        [DataMember]
        public long preorder_id { get; set; }

        [DataMember]
        public long order_id { get; set; }

        [DataMember]
        public string preorder_ref_no { get; set; }

        [DataMember]
        public DateTime? order_expected_date { get; set; }
        [DataMember]
        public long customer_id { get; set; }

        [DataMember]
        public string vendor_cognisee_type { get; set; }

        [DataMember]
        public string notes { get; set; }

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
        public string status_description { get; set; }

        [DataMember]
        public DateTime? changed_date { get; set; }
        [DataMember]
        public int region_id { get; set; }
        [DataMember]
        public string region_value { get; set; }
        [DataMember]
        public string region_description { get; set; }
        [DataMember]
        public string changed_by_full_name { get; set; }
        [DataMember]
        public string entered_by_full_name { get; set; }
        [DataMember]
        public string order_ref_no { get; set; }
        [DataMember]
        public string customer_details { get; set; }
         [DataMember]
        public string customer_name { get; set; }




        public string preorder_id_column_name_is_primary = "PREORDER_ID";
        public string order_id_column_name = "ORDER_ID";
        public string preorder_ref_no_column_name = "PREORDER_REF_NO";
        public string order_expected_date_column_name = "ORDER_EXPECTED_DATE";
        public string customer_id_column_name = "CUSTOMER_ID";
        public string notes_column_name = "NOTES";
        public string status_id_column_name = "STATUS_ID";
        public string status_value_column_name = "STATUS_VALUE";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        public string changed_date_column_name = "CHANGED_DATE";
        public string vendor_cognisee_type_column_name = "VENDOR_COGNISEE_TYPE";
        public string region_id_column_name = "REGION_ID";
        public string region_value_column_name = "REGION_VALUE";
        public string status_description_column_name = "STATUS_DESCRIPTION";
        public string entered_by_full_name_column_name = "ENTERED_BY_FULL_NAME";
        public string changed_by_full_name_column_name = "CHANGED_BY_FULL_NAME";
        public string region_description_column_name = "REGION_DESCRIPTION";
        public string order_ref_no_column_name = "ORDER_REF_NO";
        public string customer_details_column_name = "CUSTOMER_DETAILS";
        public string customer_name_column_name = "CUSTOMER_NAME";
    }
}
