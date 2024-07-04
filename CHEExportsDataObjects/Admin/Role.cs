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
    public partial class Role : DataObjectBase
    {
        public Role()
        {
            TABLE_NAME = "ADM_ROLE";
            status_id = Constants.Application.Active_Iactive_Status_id;
            screen_id = Constants.Application.Screen_id;
        }

        public string TABLE_NAME { get; set; }

        [DataMember]
        public long role_id { get; set; }

        [DataMember]
        public string role_name { get; set; }

        [DataMember]
        public int status_id { get; set; }

        [DataMember]
        public string status_value { get; set; } 
        [DataMember]
        public int screen_id { get; set; }

        [DataMember]
        public string screen_value { get; set; }

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
        [DataMember]
        public string screen_description { get; set; }


        public string role_id_column_name_is_primary = "ROLE_ID";
        public string role_name_column_name = "ROLE_NAME";
        public string screen_id_column_name = "SCREEN_ID";
        public string screen_value_column_name = "SCREEN_VALUE";
        public string status_id_column_name = "STATUS_ID";
        public string status_value_column_name = "STATUS_VALUE";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        public string changed_date_column_name = "CHANGED_DATE";
    }
}
