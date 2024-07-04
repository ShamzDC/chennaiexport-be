
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class ExportInvoice
    {
        public void GetData(protoExportInvoice aprotoExportInvoice)
        {
            ProtoDataConverter.GetData(aprotoExportInvoice, this);
            if (aprotoExportInvoice.LstprotoExportInvoiceDetail != null && aprotoExportInvoice.LstprotoExportInvoiceDetail.Count > 0)
            {
                if (this.lstExportInvoiceDetail != null)
                {
                    foreach (protoExportInvoiceDetail obj in aprotoExportInvoice.LstprotoExportInvoiceDetail)
                    {
                        ExportInvoiceDetail lExportInvoiceDetail = new ExportInvoiceDetail();

                        lExportInvoiceDetail.GetData(obj);

                        this.lstExportInvoiceDetail.Add(lExportInvoiceDetail);
                    }
                }
            }
            if (aprotoExportInvoice.IprotoExporter != null)
            {
                this.iExporter.GetData(aprotoExportInvoice.IprotoExporter);
            }
            if (aprotoExportInvoice.IprotoParty != null)
            {
                this.iParty.GetData(aprotoExportInvoice.IprotoParty);
            }
            if (aprotoExportInvoice.IprotoExportConsignee != null)
            {
                this.iExportConsignee.GetData(aprotoExportInvoice.IprotoExportConsignee);
            }


        }

        public protoExportInvoice GetProto()
        {
            protoExportInvoice lprotoExportInvoice = new protoExportInvoice();
            ProtoDataConverter.GetProto(this, lprotoExportInvoice);
            if (this.lstExportInvoiceDetail != null)
            {
                foreach (ExportInvoiceDetail obj in this.lstExportInvoiceDetail)
                {
                    lprotoExportInvoice.LstprotoExportInvoiceDetail.Add(obj.GetProto());
                }
            }
            if (this.iExportConsignee != null)
            {
                lprotoExportInvoice.IprotoExportConsignee = this.iExportConsignee.GetProto();
            }
            if (this.iExporter != null)
            {
                lprotoExportInvoice.IprotoExporter = this.iExporter.GetProto();
            }
            if (this.iParty != null)
            {
                lprotoExportInvoice.IprotoParty = this.iParty.GetProto();
            }
            ProtoDataConverter.SetMessages(this, lprotoExportInvoice);
            return lprotoExportInvoice;
        }
    }
}



