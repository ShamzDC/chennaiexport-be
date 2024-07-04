
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class ExportInvoiceSearch
    {
        public void GetData(protoExportInvoiceSearch aprotoExportInvoiceSearch)
        {
            ProtoDataConverter.GetData(aprotoExportInvoiceSearch, this);
        }
        public protoExportInvoiceSearch GetProto()
        {
            protoExportInvoiceSearch lprotoExportInvoiceSearch = new protoExportInvoiceSearch();
            ProtoDataConverter.GetProto(this, lprotoExportInvoiceSearch);
            return lprotoExportInvoiceSearch;
        }
    }
    public partial class ExportInvoiceSearchResultset
    {
        public void GetBus(protoExportInvoiceSearchResult aprotoExportInvoiceSearchResult)
        {
            ProtoDataConverter.GetData(aprotoExportInvoiceSearchResult, this);
        }
        public protoExportInvoiceSearchResultSet GetProto()
        {
            protoExportInvoiceSearchResultSet lprotoExportInvoiceSearchResultSet = new protoExportInvoiceSearchResultSet();
            ProtoDataConverter.GetProto(this, lprotoExportInvoiceSearchResultSet);
            return lprotoExportInvoiceSearchResultSet;
        }
    }
    public partial class ExportInvoiceSearchResult
    {
        public protoExportInvoiceSearchResult GetProto (SearchResultBase<ExportInvoiceSearchResultset> result)
        {
            protoExportInvoiceSearchResult lprotoExportInvoiceSearchResult = new protoExportInvoiceSearchResult();
            ProtoDataConverter.GetProto(result, lprotoExportInvoiceSearchResult);
           // if (result != null && result. > 0)
            {
                foreach (ExportInvoiceSearchResultset obj in result.SearchResultSet)
                {
                    lprotoExportInvoiceSearchResult.PlstprotoExportInvoiceSearchResultSet.Add(obj.GetProto());
                }
            }
            return lprotoExportInvoiceSearchResult;
        }
    }
}

