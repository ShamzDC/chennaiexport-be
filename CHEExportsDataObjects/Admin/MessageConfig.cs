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
    public partial class MessageConfig : DataObjectBase
    {
        public MessageConfig()
        {
            TABLE_NAME = "ADM_MESSAGE";
        }

        public string TABLE_NAME { get; set; }

        [DataMember]
        public long message_id { get; set; }

        [DataMember]
        public int message_no { get; set; }

        [DataMember]
        public string message { get; set; }

        [DataMember]
        public int message_type_id { get; set; }

        [DataMember]
        public string message_type_value { get; set; }

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
        public string message_type_description { get; set; }


        public string message_id_column_name_is_primary = "MESSAGE_ID";
        public string message_no_column_name = "MESSAGE_NO";
        public string message_column_name = "MESSAGE";
        public string message_type_id_column_name = "MESSAGE_TYPE_ID";
        public string message_type_value_column_name = "MESSAGE_TYPE_VALUE";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        public string changed_date_column_name = "CHANGED_DATE";
    }
}
