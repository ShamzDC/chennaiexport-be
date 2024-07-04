
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class ExporterSearch
    {
        public void GetData(protoExporterSearch aprotoExporterSearch)
        {
            ProtoDataConverter.GetData(aprotoExporterSearch, this);
        }
        public protoExporterSearch GetProto()
        {
            protoExporterSearch lprotoExporterSearch = new protoExporterSearch();
            ProtoDataConverter.GetProto(this, lprotoExporterSearch);
            return lprotoExporterSearch;
        }
    }
    public partial class ExporterSearchResultset
    {
        public void GetBus(protoExporterSearchResult aprotoExporterSearchResult)
        {
            ProtoDataConverter.GetData(aprotoExporterSearchResult, this);
        }
        public protoExporterSearchResultSet GetProto()
        {
            protoExporterSearchResultSet lprotoExporterSearchResultSet = new protoExporterSearchResultSet();
            ProtoDataConverter.GetProto(this, lprotoExporterSearchResultSet);
            return lprotoExporterSearchResultSet;
        }
    }
    public partial class ExporterSearchResult
    {
        public protoExporterSearchResult GetProto (SearchResultBase<ExporterSearchResultset> result)
        {
            protoExporterSearchResult lprotoExporterSearchResult = new protoExporterSearchResult();
            ProtoDataConverter.GetProto(result, lprotoExporterSearchResult);
           // if (result != null && result. > 0)
            {
                foreach (ExporterSearchResultset obj in result.SearchResultSet)
                {
                    lprotoExporterSearchResult.PlstprotoExporterSearchResultSet.Add(obj.GetProto());
                }
            }
            return lprotoExporterSearchResult;
        }
    }
}

