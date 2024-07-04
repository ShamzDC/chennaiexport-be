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
    public partial class InvoiceDetailsItems : DataObjectBase
    {
        public InvoiceDetailsItems()
        {
            TABLE_NAME = "APP_INVOICE_DETAILS_ITEMS";
        }

        public string TABLE_NAME { get; set; }
        [DataMember]
        public long invoice_detail_item_id {  get; set; }
        [DataMember]
        public long invoice_detail_id {  get; set; }
        [DataMember]
        public long product_id {  get; set; }
        [DataMember]
        public string hsn_or_sac_no {  get; set; }
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

        public string invoice_detail_item_id_column_name_is_primary = "INVOICE_DETAIL_ITEM_ID";
        public string invoice_detail_id_column_name = "INVOICE_DETAIL_ID";
        public string product_id_column_name = "PRODUCT_ID";
        public string hsn_or_sac_no_column_name = "HSN_OR_SAC_NO";
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
