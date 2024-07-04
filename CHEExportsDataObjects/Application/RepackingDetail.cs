
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    [Serializable]
    [DataContract]
    public partial class RepackingDetail : DataObjectBase
    {
        public RepackingDetail()
        {
            TABLE_NAME = "APP_REPACKING_DETAIL";
            status_id = Constants.Application.repaking_status_id;
            lOrderDetails = new OrderDetails();
            lstRepackingListDetail = new List<RepackingListDetail>();
            lRepackingListDetail = new RepackingListDetail();
            lstOrderDetails=new List<OrderDetails>();
            iCustomer = new Customer();
            region_id = Constants.Application.Region_id;
        }
        

        public string TABLE_NAME { get; set; }

        [DataMember]
        public long repacking_detail_id { get; set; }

        [DataMember]
        public long order_id { get; set; }


        [DataMember]
        public bool test_flag { get; set; }

        [DataMember]
        public int status_id { get; set; }
        [DataMember]
        public string status_value { get; set; }

        [DataMember]
        public decimal total_quantity { get; set; }

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
        public OrderDetails lOrderDetails { get; set; }
        [DataMember]
        public List<OrderDetails> lstOrderDetails { get; set; }
        [DataMember]
        public List<RepackingListDetail> lstRepackingListDetail { get; set; }
        [DataMember]
        public RepackingListDetail lRepackingListDetail { get; set; }
        [DataMember]
        public int region_id { get; set; }
        [DataMember]
        public string region_value { get; set; }
        [DataMember]
        public string region_description { get; set; }
        [DataMember]
        public Customer iCustomer { get; set; }
        [DataMember]
        public long customer_id { get; set; }

        public string repacking_detail_id_column_name_is_primary = "REPACKING_DETAIL_ID";
        public string order_id_column_name = "ORDER_ID";
        public string status_id_column_name = "STATUS_ID";
        public string status_value_column_name = "STATUS_VALUE";
        public string total_quantity_column_name = "TOTAL_QUANTITY";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        public string changed_date_column_name = "CHANGED_DATE";
        public string customer_id_column_name = "CUSTOMER_ID";
        public string region_id_column_name = "REGION_ID";
        public string region_value_column_name = "REGION_VALUE";

    }
}

