using CHEExportsProto;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public class Common
    {
        [DataMember]
        public DateTime? string1 { get; set; }

        [DataMember]
        public DateTime? string2 { get; set; }

        [DataMember]
        public string string3 { get; set; }
        [DataMember]
        public long long1 { get; set; }

        [DataMember]
        public long long2 { get; set; }
        [DataMember]
        public int int1 { get; set; }
        [DataMember]
        public int int2 { get; set; }
    }

  

    public partial class CommonDDl
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string RefNo { get; set; }
        [DataMember]
        public long id { get; set; }

        [DataMember]
        public long id1 { get; set; }
        [DataMember]
        public string constant { get; set; }
        [DataMember]
        public string FullDetails { get; set; }  
        [DataMember]
        public string gst_no { get; set; }
        [DataMember]
        public string Hsn_no { get; set; }

        [DataMember]
        public DateTime? Date { get; set; }
        [DataMember]
        public string description { get; set; }

        public string Name_column_name = "NAME";
        public string Email_name_column_name = "EMAIL";
        public string RefNo_column_name = "REFNO";
        public string id_column_name = "ID";
        public string id1_column_name = "ID1";
        public string constant_column_name = "CONSTANT";
        public string FullDetails_column_name = "FULLDETAILS";
        public string gst_no_column_name = "GST_NO";
        public string Hsn_no_column_name = "HSN_NO";
        public string Date_number_column_name = "DATE";
        public string description_column_name = "DESCRIPTION";

    }
}
