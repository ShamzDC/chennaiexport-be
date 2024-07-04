
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class ConsigneeSearch
    {
        public void GetData(protoConsigneeSearch aprotoConsigneeSearch)
        {
            ProtoDataConverter.GetData(aprotoConsigneeSearch, this);
        }
        public protoConsigneeSearch GetProto()
        {
            protoConsigneeSearch lprotoConsigneeSearch = new protoConsigneeSearch();
            ProtoDataConverter.GetProto(this, lprotoConsigneeSearch);
            return lprotoConsigneeSearch;
        }
    }
    public partial class ConsigneeSearchResultset
    {
        public void GetBus(protoConsigneeSearchResult aprotoConsigneeSearchResult)
        {
            ProtoDataConverter.GetData(aprotoConsigneeSearchResult, this);
        }
        public protoConsigneeSearchResultSet GetProto()
        {
            protoConsigneeSearchResultSet lprotoConsigneeSearchResultSet = new protoConsigneeSearchResultSet();
            ProtoDataConverter.GetProto(this, lprotoConsigneeSearchResultSet);
            return lprotoConsigneeSearchResultSet;
        }
    }
    public partial class ConsigneeSearchResult
    {
        public protoConsigneeSearchResult GetProto (SearchResultBase<ConsigneeSearchResultset> result)
        {
            protoConsigneeSearchResult lprotoConsigneeSearchResult = new protoConsigneeSearchResult();
            ProtoDataConverter.GetProto(result, lprotoConsigneeSearchResult);
           // if (result != null && result. > 0)
            {
                foreach (ConsigneeSearchResultset obj in result.SearchResultSet)
                {
                    lprotoConsigneeSearchResult.PlstprotoConsigneeSearchResultSet.Add(obj.GetProto());
                }
            }
            return lprotoConsigneeSearchResult;
        }
    }
}

