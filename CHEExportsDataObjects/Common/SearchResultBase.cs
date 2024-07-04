using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public class SearchResultBase<T> where T : SearchResultSet
    {
        [DataMember]
        public long total_count { get; set; }

        [DataMember]
        public int page_size { get; set; }

        [DataMember]
        public int page_number { get; set; }

        [DataMember]
        public List<T> SearchResultSet { get; set; }

        public SearchResultBase()
        {
            SearchResultSet = new List<T>();
        }
    }
}
