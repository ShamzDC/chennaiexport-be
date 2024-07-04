
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
    public partial class ExportInvoiceDetail : DataObjectBase
    {
        public ExportInvoiceDetail()
        {
            TABLE_NAME = "APP_EXPORT_INVOICE_DETAIL";
            unit_type_id = Constants.Application.Unit_type_id;
        }

        public string TABLE_NAME { get; set; }

        [DataMember] public long export_invoice_detail_id { get; set; }
        [DataMember] public long export_invoice_id { get; set; }
        [DataMember] public long final_packing_detail_id { get; set; }
        [DataMember] public string package_no { get; set; }
        [DataMember] public decimal quantity { get; set; }
        [DataMember] public int unit_type_id { get; set; }
        [DataMember] public string unit_type_value { get; set; }
        [DataMember] public decimal rate { get; set; }
        [DataMember] public decimal invoice_rate { get; set; }
        [DataMember] public decimal amount { get; set; }

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
        public string unit_type_description { get; set; }
        [DataMember]
        public long product_id { get; set; }
        [DataMember]
        public decimal gross_weight { get; set; }
        [DataMember]
        public decimal net_weight { get; set; }



        public string export_invoice_detail_id_column_name_is_primary = "EXPORT_INVOICE_DETAIL_ID";
        public string export_invoice_id_column_name = "EXPORT_INVOICE_ID";
        public string final_packing_detail_id_column_name = "FINAL_PACKING_DETAIL_ID";
        public string package_no_column_name = "PACKAGE_NO";
        public string quantity_column_name = "QUANTITY";
        public string unit_type_id_column_name = "UNIT_TYPE_ID";
        public string unit_type_value_column_name = "UNIT_TYPE_VALUE";
        public string rate_column_name = "RATE";
        public string invoice_rate_column_name = "INVOICE_RATE";
        public string amount_column_name = "AMOUNT";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        public string changed_date_column_name = "CHANGED_DATE";
        //public string gstn_uin_numbere_column_name = "GSTN_UIN_NUMBER"; 
        public string product_id_column_name = "PRODUCT_ID";
        public string gross_weight_column_name = "GROSS_WEIGHT";
        public string net_weight_column_name = "NET_WEIGHT";
    }
}

