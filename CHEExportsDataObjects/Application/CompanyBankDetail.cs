
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
    public partial class CompanyBankDetail : DataObjectBase
    {
        public CompanyBankDetail()
        {
            TABLE_NAME = "APP_COMPANY_BANK_DETAIL";
            status_id = Constants.Application.Active_Iactive_Status_id;
        }

        public string TABLE_NAME { get; set; }

        [DataMember]
        public long company_bank_id { get; set; }

        [DataMember]
        public long company_id { get; set; }

        [DataMember]
        public string bank_name { get; set; }
        [DataMember]
        public string bank_acconut_number { get; set; }

        [DataMember]
        public string ifsc_code { get; set; }
        [DataMember]
        public string branch_code { get; set; }

        [DataMember]
        public string micr_code { get; set; }

        [DataMember]
        public string swift_code { get; set; }

        [DataMember]
        public string ad_code { get; set; }

        [DataMember]
        public string address_line_1 { get; set; }

        [DataMember]
        public string address_line_2 { get; set; }

        [DataMember]
        public string address_line_3 { get; set; }

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
        public string status_description { get; set; }


        public string company_bank_id_column_name_is_primary = "COMPANY_BANK_ID";
        public string company_id_column_name = "COMPANY_ID";
        public string bank_name_column_name = "BANK_NAME";
        public string bank_acconut_number_column_name = "BANK_ACCONUT_NUMBER";     
        public string ifsc_code_number_column_name = "IFSC_CODE";
        public string branch_code_column_name = "BRANCH_CODE";
        public string micr_code_column_name = "MICR_CODE";
        public string swift_code_column_name = "SWIFT_CODE";
        public string ad_code_column_name = "AD_CODE";
        public string address_line_1_column_name = "ADDRESS_LINE_1";
        public string address_line_2_column_name = "ADDRESS_LINE_2";
        public string address_line_3_column_name = "ADDRESS_LINE_3";
        public string status_id_column_name = "STATUS_ID";
        public string status_value_column_name = "STATUS_VALUE";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        public string changed_date_column_name = "CHANGED_DATE";

    }
}

