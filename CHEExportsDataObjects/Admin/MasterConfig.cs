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
    public partial class MasterConfig : DataObjectBase
    {
        public MasterConfig()
        {
            TABLE_NAME = "ADM_MASTER_CONFIG";
        }

        public string TABLE_NAME { get; set; }

        [DataMember]
        public long master_config_id { get; set; }

        [DataMember]
        public int m_config_id { get; set; }

        [DataMember]
        public string m_config_value { get; set; }

        [DataMember]
        public string m_config_description { get; set; }

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

      

        public string master_config_id_column_name_is_primary = "MASTER_CONFIG_ID";
        public string m_config_id_column_name = "M_CONFIG_ID";
        public string m_config_value_column_name = "M_CONFIG_VALUE";
        public string m_config_description_column_name = "M_CONFIG_DESCRIPTION";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        public string changed_date_column_name = "CHANGED_DATE";
    }
}
