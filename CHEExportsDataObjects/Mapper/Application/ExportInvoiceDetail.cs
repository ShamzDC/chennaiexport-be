
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class ExportInvoiceDetail
    {
        public void GetData(protoExportInvoiceDetail aprotoExportInvoiceDetail)
        {
            ProtoDataConverter.GetData(aprotoExportInvoiceDetail, this);
        }

        public protoExportInvoiceDetail GetProto()
        {
            protoExportInvoiceDetail lprotoExportInvoiceDetail = new protoExportInvoiceDetail();
            ProtoDataConverter.GetProto(this, lprotoExportInvoiceDetail);
            ProtoDataConverter.SetMessages(this, lprotoExportInvoiceDetail);
            return lprotoExportInvoiceDetail;
        }
    }
    public class ExportInvoiceDetailList
    {
        public protoExportInvoiceDetailList GetProto(List<ExportInvoiceDetail> ilstExportInvoiceDetail)
        {
            protoExportInvoiceDetailList lprotoExportInvoiceDetailList = new protoExportInvoiceDetailList();
            if (ilstExportInvoiceDetail != null && ilstExportInvoiceDetail.Count > 0)
            {
                foreach (ExportInvoiceDetail obj in ilstExportInvoiceDetail)
                {
                    lprotoExportInvoiceDetailList.LstprotoExportInvoiceDetail.Add(obj.GetProto());
                }
            }
            return lprotoExportInvoiceDetailList;
        }
    }
}



