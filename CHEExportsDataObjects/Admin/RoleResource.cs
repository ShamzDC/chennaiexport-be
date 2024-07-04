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
    public partial class RoleResource : DataObjectBase
    {
        public RoleResource()
        {
            TABLE_NAME = "ADM_ROLE_ROSOURCE";
        }

        public string TABLE_NAME { get; set; }

        [DataMember]
        public long role_id { get; set; }
        [DataMember]
        public long role_resource_id { get; set; }

        [DataMember]
        public long resource_id { get; set; }

        [DataMember]
        public int permission_value { get; set; }

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
        public string role_name { get; set; }
        [DataMember]
        public string role_status { get; set; }
       [DataMember]
        public string resource_name { get; set; }
        [DataMember]
        public string screen_name { get; set; }
        [DataMember]
        public string resource_descrption { get; set; }
        [DataMember]
        public string resource_type { get; set; }




        public string role_resource_id_column_name_is_primary = "ROLE_RESOURCE_ID";
        public string role_id_column_name = "ROLE_ID";
        public string resource_id_column_name = "RESOURCE_ID";
        public string permission_value_column_name = "PERMISSION_VALUE";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        public string changed_date_column_name = "CHANGED_DATE";
    }
}
