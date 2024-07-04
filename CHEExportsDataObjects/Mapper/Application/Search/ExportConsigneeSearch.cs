
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class ExportConsigneeSearch
    {
        public void GetData(protoExportConsigneeSearch aprotoExportConsigneeSearch)
        {
            ProtoDataConverter.GetData(aprotoExportConsigneeSearch, this);
        }
        public protoExportConsigneeSearch GetProto()
        {
            protoExportConsigneeSearch lprotoExportConsigneeSearch = new protoExportConsigneeSearch();
            ProtoDataConverter.GetProto(this, lprotoExportConsigneeSearch);
            return lprotoExportConsigneeSearch;
        }
    }
    public partial class ExportConsigneeSearchResultset
    {
        public void GetBus(protoExportConsigneeSearchResult aprotoExportConsigneeSearchResult)
        {
            ProtoDataConverter.GetData(aprotoExportConsigneeSearchResult, this);
        }
        public protoExportConsigneeSearchResultSet GetProto()
        {
            protoExportConsigneeSearchResultSet lprotoExportConsigneeSearchResultSet = new protoExportConsigneeSearchResultSet();
            ProtoDataConverter.GetProto(this, lprotoExportConsigneeSearchResultSet);
            return lprotoExportConsigneeSearchResultSet;
        }
    }
    public partial class ExportConsigneeSearchResult
    {
        public protoExportConsigneeSearchResult GetProto (SearchResultBase<ExportConsigneeSearchResultset> result)
        {
            protoExportConsigneeSearchResult lprotoExportConsigneeSearchResult = new protoExportConsigneeSearchResult();
            ProtoDataConverter.GetProto(result, lprotoExportConsigneeSearchResult);
           // if (result != null && result. > 0)
            {
                foreach (ExportConsigneeSearchResultset obj in result.SearchResultSet)
                {
                    lprotoExportConsigneeSearchResult.PlstprotoExportConsigneeSearchResultSet.Add(obj.GetProto());
                }
            }
            return lprotoExportConsigneeSearchResult;
        }
    }
}

