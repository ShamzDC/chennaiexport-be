
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    [Serializable]
    [DataContract]
    public partial class Company : DataObjectBase
    {
        public Company()
        {
            TABLE_NAME = "APP_COMPANY";
        }

        public string TABLE_NAME { get; set; }

        [DataMember]
        public long company_id { get; set; }

        [DataMember]
        public string company_name { get; set; }

        [DataMember]
        public string company_logo { get; set; }
        [DataMember]
        public string website { get; set; }

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
        public List<CompanyBankDetail> lstCompanyBankDetail { get; set; }


        public string company_id_column_name_is_primary = "COMPANY_ID";
        public string company_name_column_name = "COMPANY_NAME";
        public string company_logo_column_name = "COMPANY_LOGO";
        public string website_column_name = "WEBSITE";
        public string email_id_column_name = "EMAIL_ID";
        public string contact_no_column_name = "CONTACT_NO";
        public string address_line_1_column_name = "ADDRESS_LINE_1";
        public string address_line_2_column_name = "ADDRESS_LINE_2";
        public string address_line_3_column_name = "ADDRESS_LINE_3";
        public string city_column_name = "CITY";
        public string state_column_name = "STATE";
        public string country_column_name = "COUNTRY";
        public string pincode_column_name = "PINCODE";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        public string changed_date_column_name = "CHANGED_DATE";
        public string gstn_uin_numbere_column_name = "GSTN_UIN_NUMBER";
    }
}

