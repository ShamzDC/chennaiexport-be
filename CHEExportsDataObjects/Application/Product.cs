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
        public partial class Product : DataObjectBase
        {
            public Product()
            {
                TABLE_NAME = "APP_PRODUCT";
               status_id = Constants.Application.Active_Iactive_Status_id;
            }
   
        public string TABLE_NAME { get; set; }

        [DataMember]
        public long product_id { get; set; }

        [DataMember]
        public string product_name { get; set; }

        [DataMember]
        public string product_ref_no { get; set; }

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
        public string hsn_sac_number { get; set; }

        public string product_id_column_name_is_primary = "PRODUCT_ID";
        public string product_name_column_name = "PRODUCT_NAME";
        public string product_ref_no_column_name = "PRODUCT_REF_NO";
        public string status_id_column_name = "STATUS_ID";
        public string status_value_column_name = "STATUS_VALUE";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        public string changed_date_column_name = "CHANGED_DATE";
        public string hsn_sac_number_column_name = "HSN_SAC_NUMBER";
    }
}

