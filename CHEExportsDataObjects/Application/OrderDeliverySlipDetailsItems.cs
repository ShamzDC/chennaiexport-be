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
    public partial class OrderDeliverySlipDetailsItems : DataObjectBase
    {
        public OrderDeliverySlipDetailsItems()
        {
            TABLE_NAME = "APP_ORDER_DELIVERY_SLIP_DETAIL_ITEMS";
        }

        public string TABLE_NAME { get; set; }
        [DataMember]
        public long order_delivery_slip_detail_items_id {  get; set; }
        [DataMember]
        public long order_delivery_slip_detail_id {  get; set; }
        [DataMember]
        public long product_id {  get; set; }
        [DataMember]
        public decimal quantity {  get; set; }
        [DataMember]
        public int unit_type_id {  get; set; }
        [DataMember]
        public string unit_type_value {  get; set; }
        [DataMember]
        public decimal unit_rate {  get; set; }
        [DataMember]
        public decimal amount {  get; set; }
        [DataMember]
        public string entered_by {  get; set; }
        [DataMember]
        public DateTime? entered_date {  get; set; }
        [DataMember]
        public string changed_by {  get; set; }
        [DataMember]
        public DateTime? changed_date {  get; set; }

        public string order_delivery_slip_detail_items_id_column_name_is_primary = "ORDER_DELIVERY_SLIP_DETAIL_ITEMS_ID";
        public string order_delivery_slip_detail_id_column_name = "ORDER_DELIVERY_SLIP_DETAIL_ID";
        public string product_id_column_name = "PRODUCT_ID";
        public string quantity_column_name = "QUANTITY";
        public string unit_type_id_column_name = "UNIT_TYPE_ID";
        public string unit_type_value_column_name = "UNIT_TYPE_VALUE";
        public string unit_rate_column_name = "UNIT_RATE";
        public string amount_column_name = "AMOUNT";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        public string changed_date_column_name = "CHANGED_DATE";
    }
}
