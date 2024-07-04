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
    public partial class User : DataObjectBase
    {
        public User()
        {
            TABLE_NAME = "ADM_USER";
        }
        public string TABLE_NAME { get; set; }

        [DataMember]
        public long user_id { get; set; }

        [DataMember]
        public string user_ref_no { get; set; }

        [DataMember]
        public string user_login_id { get; set; }

        [DataMember]
        public string keyword { get; set; }

        [DataMember]
        public string first_name { get; set; }

        [DataMember]
        public string middle_name { get; set; }

        [DataMember]
        public string last_name { get; set; }

        [DataMember]
        public string father_name { get; set; }

        [DataMember]
        public string mother_name { get; set; }

        [DataMember]
        public DateTime? dob { get; set; }

        [DataMember]
        public string email_id { get; set; }

        [DataMember]
        public string contact_number { get; set; }

        [DataMember]
        public string alternate_number { get; set; }

        [DataMember]
        public int gender_id { get; set; }

        [DataMember]
        public string gender_value { get; set; }

        [DataMember]
        public string key_token { get; set; }

        [DataMember]
        public DateTime? begin_date { get; set; }

        [DataMember]
        public DateTime? end_date { get; set; }

        [DataMember]
        public int status_id { get; set; }

        [DataMember]
        public string status_value { get; set; }

        [DataMember]
        public int designation_id { get; set; }

        [DataMember]
        public string designation_value { get; set; }

        [DataMember]
        public int branch_id { get; set; }

        [DataMember]
        public string branch_value { get; set; }

        [DataMember]
        public int department_id { get; set; }

        [DataMember]
        public string department_value { get; set; }

        [DataMember]
        public int team_id { get; set; }

        [DataMember]
        public string team_value { get; set; }

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
        public string team_description { get; set; }

        public string user_id_column_name_is_primary = "USER_ID";
        public string user_ref_no_column_name = "USER_REF_NO";
        public string user_login_id_column_name = "USER_LOGIN_ID";
        public string keyword_column_name = "KEYWORD";
        public string first_name_column_name = "FIRST_NAME";
        public string middle_name_column_name = "MIDDLE_NAME";
        public string last_name_column_name = "LAST_NAME";
        public string father_name_column_name = "FATHER_NAME";
        public string mother_name_column_name = "MOTHER_NAME";
        public string dob_column_name = "DOB";
        public string email_id_column_name = "EMAIL_ID";
        public string contact_number_column_name = "CONTACT_NUMBER";
        public string alternate_number_column_name = "ALTERNATE_NUMBER";
        public string gender_id_column_name = "GENDER_ID";
        public string gender_value_column_name = "GENDER_VALUE";
        public string key_token_column_name = "KEY_TOKEN";
        public string begin_date_column_name = "BEGIN_DATE";
        public string end_date_column_name = "END_DATE";
        public string status_id_column_name = "STATUS_ID";
        public string status_value_column_name = "STATUS_VALUE";
        public string designation_id_column_name = "DESIGNATION_ID";
        public string designation_value_column_name = "DESIGNATION_VALUE";
        public string branch_id_column_name = "BRANCH_ID";
        public string branch_value_column_name = "BRANCH_VALUE";
        public string department_id_column_name = "DEPARTMENT_ID";
        public string department_value_column_name = "DEPARTMENT_VALUE";
        public string team_id_column_name = "TEAM_ID";
        public string team_value_column_name = "TEAM_VALUE";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        public string changed_date_column_name = "CHANGED_DATE";
    }
}
