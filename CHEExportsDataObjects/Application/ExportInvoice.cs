
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
    public partial class ExportInvoice : DataObjectBase
    {
        public ExportInvoice()
        {
            TABLE_NAME = "APP_EXPORT_INVOICE";
            lstExportInvoiceDetail=new List<ExportInvoiceDetail>();
            iParty = new Party();
            iExporter = new Exporter();
            iExportConsignee = new ExportConsignee();
            discharge_port_id = Constants.Application.discharge_port_id;
            loading_port_id=Constants.Application.loading_port_id;
            status_id = Constants.Application.repaking_status_id;
            region_id = Constants.Application.Region_id;
        }

        public string TABLE_NAME { get; set; }

        [DataMember] public long export_invoice_id { get; set; }
        [DataMember] public long order_id { get; set; }
        [DataMember] public string invoice_no { get; set; }
        [DataMember] public DateTime? invoice_date { get; set; }
        [DataMember] public string vessel_flight_no { get; set; }
        [DataMember] public string loading_port_value { get; set; }
        [DataMember] public string discharge_port_value { get; set; }
        [DataMember] public int loading_port_id { get; set; }
        [DataMember] public int discharge_port_id { get; set; }

        [DataMember] public string destination { get; set; }
        [DataMember] public string pre_carriage_by { get; set; }
        [DataMember] public string place_of_receipt_of_pre_carrier { get; set; }
        [DataMember] public string container_no { get; set; }
        [DataMember] public string country_of_origin_of_goods { get; set; }
        [DataMember] public string country_of_final_destination { get; set; }
        [DataMember] public string buyers_order_no_date { get; set; }
        [DataMember] public string terms_of_delivery_payment { get; set; }
        [DataMember] public long party_id { get; set; } 
        [DataMember] public long exporter_id { get; set; }
        [DataMember] public decimal net_weight { get; set; }
        [DataMember] public decimal gross_weight { get; set; }
        [DataMember] public decimal cgst_tax_percentage { get; set; }
        [DataMember] public decimal sgst_tax_percentage { get; set; }
        [DataMember] public decimal total_tax_amount { get; set; }
        [DataMember] public decimal round_off { get; set; }
        [DataMember] public decimal invoice_total { get; set; }
        [DataMember] public string signature_copy { get; set; }
        [DataMember] public long company_bank_id { get; set; }
        [DataMember] public string entered_by { get; set; }
        [DataMember]  public DateTime? entered_date { get; set; }
        [DataMember] public string changed_by { get; set; }
        [DataMember]public DateTime? changed_date { get; set; }
        [DataMember]public string changed_by_full_name { get; set; }
        [DataMember]public string entered_by_full_name { get; set; }
        [DataMember]public long export_consignee_id { get; set; }

        [DataMember] public long over_all_total { get; set; }
        [DataMember] public decimal taxable_value { get; set; }
        [DataMember] public long no_and_kind_of_pakage { get; set; }
        [DataMember] public string loading_port_description { get; set; }
        [DataMember] public string discharge_port_description { get; set; }

        [DataMember] public List<ExportInvoiceDetail> lstExportInvoiceDetail { get; set; }
        [DataMember] public Exporter iExporter { get; set; }
        [DataMember] public Party iParty { get; set; }
        [DataMember] public ExportConsignee iExportConsignee { get; set; }


        [DataMember] public int region_id { get; set; }
        [DataMember] public string region_value { get; set; }
        [DataMember] public int status_id { get; set; }
        [DataMember] public string status_value { get; set; }
        [DataMember] public string status_description { get; set; }
        [DataMember] public string region_description { get; set; }
        [DataMember] public decimal sgst_value { get; set; }
        [DataMember] public decimal cgst_value { get; set; }



        public string export_invoice_id_column_name_is_primary = "EXPORT_INVOICE_ID";
        public string order_id_column_name = "ORDER_ID";
        public string invoice_no_column_name = "INVOICE_NO";
        public string invoice_date_column_name = "INVOICE_DATE";
        public string vessel_flight_no_column_name = "VESSEL_FLIGHT_NO";
        public string loading_port_id_column_name = "LOADING_PORT_ID";
        public string loading_port_value_column_name = "LOADING_PORT_VALUE";
        public string discharge_port_id_column_name = "DISCHARGE_PORT_ID";
        public string discharge_port_value_column_name = "DISCHARGE_PORT_VALUE";
        public string destination_column_name = "DESTINATION";
        public string pre_carriage_by_column_name = "PRE_CARRIAGE_BY";
        public string place_of_receipt_of_pre_carrier_column_name = "PLACE_OF_RECEIPT_OF_PRE_CARRIER";
        public string container_no_column_name = "CONTAINER_NO";
        public string country_of_origin_of_goods_column_name = "COUNTRY_OF_ORIGIN_OF_GOODS";
        public string country_of_final_destination_column_name = "COUNTRY_OF_FINAL_DESTINATION";
        public string buyers_order_no_date_column_name = "BUYERS_ORDER_NO_DATE";
        public string terms_of_delivery_payment_column_name = "TERMS_OF_DELIVERY_PAYMENT";
        public string exporter_id_column_name = "EXPORTER_ID";
        public string part_id_column_name = "PARTY_ID";
        public string export_consignee_id_column_name = "EXPORT_CONSIGNEE_ID";
        public string net_weight_column_name = "NET_WEIGHT";
        public string gross_weight_column_name = "GROSS_WEIGHT";
        public string cgst_tax_percentage_column_name = "CGST_TAX_PERCENTAGE";
        public string sgst_tax_percentage_column_name = "SGST_TAX_PERCENTAGE";
        public string total_tax_amount_column_name = "TOTAL_TAX_AMOUNT";
        public string round_off_column_name = "ROUND_OFF";
        public string invoice_total_column_name = "INVOICE_TOTAL";
        public string signature_copy_column_name = "SIGNATURE_COPY";
        public string company_bank_id_column_name = "COMPANY_BANK_ID";
        public string entered_by_column_name = "ENTERED_BY";
        public string entered_date_column_name = "ENTERED_DATE";
        public string changed_by_column_name = "CHANGED_BY";
        public string changed_date_column_name = "CHANGED_DATE";
        public string over_all_total_column_name = "OVER_ALL_TOTAL";
        public string taxable_value_column_name = "TAXABLE_VALUE";
        public string no_and_kind_of_pakage_column_name = "NO_AND_KIND_OF_PAKAGE";
        public string region_id_column_name = "REGION_ID";
        public string region_value_column_name = "REGION_VALUE";
        public string status_id_column_name = "STATUS_ID";
        public string status_value_column_name = "STATUS_VALUE";
        public string cgst_value_column_name = "CGST_VALUE";
        public string sgst_value_column_name = "SGST_VALUE";

        


    }
}

