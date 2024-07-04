
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
    public partial class InvoiceDetails : DataObjectBase
    {
        public InvoiceDetails()
        {
            TABLE_NAME = "APP_INVOICE_DETAILS";
            destination_id = Constants.Application.Destination_id;
            mode_of_payment_id = Constants.Application.Mode_Payment_id;
            dispatched_through_id = Constants.Application.Dispatched_name_id;
            mode_of_packing_id = Constants.Application.mode_of_packing_id;
        }

        public string TABLE_NAME { get; set; }

        [DataMember]
        public long invoice_detail_id { get; set; }

        [DataMember]
        public long order_detail_id { get; set; }

        [DataMember]
        public string invoice_no { get; set; }

        [DataMember]
        public DateTime? invoice_date { get; set; }

        [DataMember]
        public string delivery_notes { get; set; }
        [DataMember]
        public int mode_of_payment_id { get; set; }

        [DataMember]
        public string mode_of_payment_value { get; set; }

        [DataMember]
        public int dispatched_through_id { get; set; }
        [DataMember]
        public string dispatched_through_value { get; set; }

        [DataMember]
        public int mode_of_packing_id { get; set; }

        [DataMember]
        public string mode_of_packing_value { get; set; }
        [DataMember]
        public int destination_id { get; set; }

        [DataMember]
        public string destination_value { get; set; }

        [DataMember]
        public string reference_no { get; set; }
        [DataMember]
        public DateTime? reference_date { get; set; }

        [DataMember]
        public string other_references { get; set; }


        [DataMember]
        public string buyers_order_no { get; set; }

        [DataMember]
        public DateTime? buyers_date { get; set; }
        [DataMember]
        public string dispatch_documet_no { get; set; }

        [DataMember]
        public DateTime? delivery_note_date { get; set; }

        [DataMember]
        public string terms_of_delivery { get; set; }
        [DataMember]
        public long vendor_id { get; set; }

        [DataMember]
        public string consignee_name { get; set; }

        [DataMember]
        public string invoice_photo_copy { get; set; }

        [DataMember]
        public decimal total_quantity { get; set; }
        [DataMember]
        public decimal taxable_value { get; set; }

        [DataMember]
        public decimal cgs_tax_percentage { get; set; }

        [DataMember]
        public decimal sgs_tax_percentage { get; set; }

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
        public string mode_of_payment_description { get; set; }
        [DataMember]
        public string dispatched_through_description { get; set; }
        [DataMember]
        public string mode_of_packing_description { get; set; }
        [DataMember]
        public string destination_decscription { get; set; }


        [DataMember]
        public decimal total_tax_amount { get; set; }
        [DataMember]
        public decimal sgs_tax_value { get; set; }
        [DataMember]
        public decimal cgs_tax_value { get; set; } 
        [DataMember]
        public decimal invoice_total { get; set; }
        [DataMember]
        public string Vendor_Details { get; set; } 
        [DataMember]
        public string Company_Details { get; set; }
        [DataMember]
        public string Company_Address { get; set; }
        [DataMember]
        public string Vendor_Address { get; set; }
        [DataMember]
        public long Company_id { get; set; }
        [DataMember]
        public long buyer_id { get; set; }
        [DataMember]
        public long consignee_id { get; set; }
        [DataMember]
        public string buyer_details { get; set; }
        [DataMember]
        public string consignee_details { get; set; }

        [DataMember]
        public decimal round_off { get; set; }


        public string invoice_detail_id_column_name_is_primary = "INVOICE_DETAIL_ID";
        public string order_detail_id_column_name = "ORDER_DETAIL_ID";
        public string invoice_no_column_name = "INVOICE_NO";
        public string invoice_date_column_name = "INVOICE_DATE";
        public string delivery_notes_column_name = "DELIVERY_NOTES";
        public string mode_of_payment_id_column_name = "MODE_OF_PAYMENT_ID";
        public string mode_of_payment_value_column_name = "MODE_OF_PAYMENT_VALUE";
        public string dispatched_through_id_column_name = "DISPATCHED_THROUGH_ID";
        public string dispatched_through_value_column_name = "DISPATCHED_THROUGH_VALUE";
        public string mode_of_packing_id_column_name = "MODE_OF_PACKING_ID";
        public string mode_of_packing_value_column_name = "MODE_OF_PACKING_VALUE";
        public string destination_id_column_name = "DESTINATION_ID";
        public string destination_value_column_name = "DESTINATION_VALUE";
        public string reference_no_column_name = "REFERENCE_NO";
        public string reference_date_column_name = "REFERENCE_DATE";
        public string other_references_column_name = "OTHER_REFERENCES";
        public string buyers_order_no_column_name = "BUYERS_ORDER_NO";
        public string buyers_date_column_name = "BUYERS_DATE";
        public string dispatch_documet_no_column_name = "DISPATCH_DOCUMET_NO";
        public string delivery_note_date_column_name = "DELIVERY_NOTE_DATE";
        public string terms_of_delivery_column_name = "TERMS_OF_DELIVERY";
        public string vendor_id_column_name = "VENDOR_ID";
        public string consignee_name_column_name = "CONSIGNEE_NAME";
        public string invoice_photo_copy_column_name = "INVOICE_PHOTO_COPY";
        public string total_quantity_column_name = "TOTAL_QUANTITY";
        public string taxable_value_column_name = "TAXABLE_VALUE";
        public string cgs_tax_percentage_column_name = "CGS_TAX_PERCENTAGE";
        public string sgs_tax_percentage_column_name = "SGS_TAX_PERCENTAGE";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        public string changed_date_column_name = "CHANGED_DATE";
        public string total_tax_amount_column_name = "TOTAL_TAX_AMOUNT";
        public string sgs_tax_value_column_name = "SGS_TAX_VALUE";
        public string cgs_tax_value_column_name = "CGS_TAX_VALUE";
        public string invoice_total_column_name = "INVOICE_TOTAL";
        public string buyer_id_column_name = "BUYER_ID";
        public string consignee_id_column_name = "CONSIGNEE_ID";
        public string round_off_column_name = "ROUND_OFF";
    }
}

