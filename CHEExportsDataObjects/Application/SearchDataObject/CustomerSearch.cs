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
    public partial class CustomerSearch : SearchBase
    {

        [DataMember]
        public string customer_name { get; set; }

        [DataMember]
        public string customer_ref_no { get; set; }

        [DataMember]
        public string email_id { get; set; }
        [DataMember]
        public string contact_no { get; set; }

        [DataMember]
        public string city { get; set; }

        [DataMember]
        public string state { get; set; }

        [DataMember]
        public string country { get; set; }

        [DataMember]
        public string pincode { get; set; }

        [DataMember]
        public string status_value { get; set; }

        [DataMember]
        public string entered_by { get; set; }

        [DataMember]
        public string changed_by { get; set; } 
        [DataMember]
        public string customer_type { get; set; }
        [DataMember]
        public string address_line1 { get; set; }

    }

    [Serializable]
    [DataContract]
    public partial class CustomerSearchResultset : SearchResultSet
    {
        [DataMember]
        public long customer_id { get; set; }

        [DataMember]
        public string customer_name { get; set; }

        [DataMember]
        public string customer_ref_no { get; set; }

        [DataMember]
        public string email_id { get; set; }
        [DataMember]
        public string contact_no { get; set; }

        [DataMember]
        public string address_line_1 { get; set; }

        [DataMember]
        public string address_line_2 { get; set; }

        [DataMember]
        public string address_line_3 { get; set; }

        [DataMember]
        public string city { get; set; }

        [DataMember]
        public string state { get; set; }

        [DataMember]
        public string country { get; set; }

        [DataMember]
        public string pincode { get; set; }

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
        public string changed_by_full_name { get; set; }
        [DataMember]
        public string entered_by_full_name { get; set; }

        [DataMember]
        public string status_description { get; set; }

        [DataMember]
        public string last_order_ref_no { get; set; }

        [DataMember]
        public int order_count { get; set; }
        [DataMember]
        public string customer_type_description { get; set; }

        [DataMember]
        public DateTime? changed_date { get; set; }
        public string customer_id_column_name_is_primary = "CUSTOMER_ID";
        public string customer_name_column_name = "CUSTOMER_NAME";
        public string customer_ref_no_column_name = "CUSTOMER_REF_NO";
        public string email_id_column_name = "EMAIL_ID";
        public string contact_no_column_name = "CONTACT_NO";
        public string address_line_1_column_name = "ADDRESS_LINE_1";
        public string address_line_2_column_name = "ADDRESS_LINE_2";
        public string address_line_3_column_name = "ADDRESS_LINE_3";
        public string city_column_name = "CITY";
        public string state_column_name = "STATE";
        public string country_column_name = "COUNTRY";
        public string pincode_column_name = "PINCODE";
        public string status_id_column_name = "STATUS_ID";
        public string status_value_column_name = "STATUS_VALUE";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        //public string changed_date_column_name = "CHANGED_DATE";
        //public string changed_by_full_name_column_name = "CHANGED_BY_FULL_NAME";
        //public string entered_by_full_name_column_name = "ENTERED_BY_FULL_NAME";
        public string status_description_column_name = "STATUS_DESCRIPTION";
        //public string last_order_ref_no_column_name = "LAST_ORDER_REF_NO";
        //public string order_count_column_name = "ORDER_COUNT";
        public string customer_type_id_column_name = "CUSTOMER_TYPE_ID";
        public string customer_type_value_column_name = "CUSTOMER_TYPE_VALUE";
        public string customer_type_description_column_name = "CUSTOMER_TYPE_DESCRIPTION";
    }
}
