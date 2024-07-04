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
    public partial class ExportConsigneeSearch : SearchBase
    {

        [DataMember]
        public long Export_Export_consignee_id { get; set; }

        [DataMember]
        public string Export_consignee_name { get; set; }

        [DataMember]
        public string Export_consignee_ref_no { get; set; }

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
        public DateTime? changed_date { get; set; }

        [DataMember]
        public string changed_by_full_name { get; set; }
        [DataMember]
        public string entered_by_full_name { get; set; }

        [DataMember]
        public string gstn_uin_number { get; set; }


        [DataMember]
        public string status_description { get; set; }

    }

    [Serializable]
    [DataContract]
    public partial class ExportConsigneeSearchResultset : SearchResultSet
    {
        [DataMember]
        public long export_consignee_id { get; set; }

        [DataMember]
        public string export_consignee_name { get; set; }

        [DataMember]
        public string export_consignee_ref_no { get; set; }

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
        public DateTime? changed_date { get; set; }

        [DataMember]
        public string changed_by_full_name { get; set; }
        [DataMember]
        public string entered_by_full_name { get; set; }

        [DataMember]
        public string gstn_uin_number { get; set; }


        [DataMember]
        public string status_description { get; set; }

        public string export_consignee_id_column_name_is_primary = "EXPORT_CONSIGNEE_ID";
        public string export_consignee_name_column_name = "EXPORT_CONSIGNEE_NAME";
        public string export_consignee_ref_no_column_name = "EXPORT_CONSIGNEE_REF_NO";
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
        public string changed_date_column_name = "CHANGED_DATE";
        public string gstn_uin_number_column_name = "GSTN_UIN_NUMBER";
        public string status_description_column_name = "STATUS_DESCRIPTION";
    }
}
