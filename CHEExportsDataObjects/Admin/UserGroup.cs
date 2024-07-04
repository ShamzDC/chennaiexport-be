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
    public partial class UserGroup : DataObjectBase
    {
        public UserGroup()
        {
            TABLE_NAME = "ADM_USER_GROUP";
            status_id = Constants.Application.Active_Iactive_Status_id;
        }

        public string TABLE_NAME { get; set; }

        [DataMember]
        public long group_id { get; set; }
        [DataMember]
        public long user_id { get; set; }
        [DataMember]

        public long user_group_id { get; set; }

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
        [DataMember]
        public string user_name { get; set; }
        [DataMember]
        public string user_status { get; set; }
        [DataMember]
        public string group_name { get; set; }
        [DataMember]
        public string group_status { get; set; }



        public string user_group_id_column_name_is_primary = "USER_GROUP_ID";
        public string group_id_column_name = "GROUP_ID";
        public string user_id_column_name = "USER_ID";
        public string status_id_column_name = "STATUS_ID";
        public string status_value_column_name = "STATUS_VALUE";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        public string changed_date_column_name = "CHANGED_DATE";
    }
}
