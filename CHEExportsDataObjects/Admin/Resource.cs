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
    public partial class Resource : DataObjectBase
    {
        public Resource()
        {
            TABLE_NAME = "ADM_RESOURCE";
            resource_type_id = Constants.Application.resource_type_id;
            screen_id = Constants.Application.Screen_id;
        }

        public string TABLE_NAME { get; set; }

        [DataMember]
        public long resource_id { get; set; }

        [DataMember]
        public int resource_type_id { get; set; }
        [DataMember]
        public string resource_type_value { get; set; }
        [DataMember]
        public string resource_name { get; set; }
        [DataMember]
        public string resource_description { get; set; }
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
        public string screen_description { get; set; }

        [DataMember]
        public string resource_type_description { get; set; }


        public string resource_id_column_name_is_primary = "RESOURCE_ID";
        public string resource_type_id_column_name = "RESOURCE_TYPE_ID";
        public string resource_type_value_column_name = "RESOURCE_TYPE_VALUE";
        public string resource_name_column_name = "RESOURCE_NAME";
        public string resource_description_column_name = "RESOURCE_DESCRIPTION";
        public string screen_id_column_name = "SCREEN_ID";
        public string screen_value_column_name = "SCREEN_VALUE";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        public string changed_date_column_name = "CHANGED_DATE";
    }
}
