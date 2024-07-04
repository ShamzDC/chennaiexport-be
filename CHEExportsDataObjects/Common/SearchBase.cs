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
    public abstract class SearchBase
    {
        [DataMember]
        public int total_count { get; set; }

        [DataMember]
        public int page_size { get; set; }

        [DataMember]
        public int page_number { get; set; }

        [DataMember]
        public string order_by_column_name { get; set; }

        [DataMember]
        public bool ascending { get; set; }
    }
}
