
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class PartySearch
    {
        public void GetData(protoPartySearch aprotoPartySearch)
        {
            ProtoDataConverter.GetData(aprotoPartySearch, this);
        }
        public protoPartySearch GetProto()
        {
            protoPartySearch lprotoPartySearch = new protoPartySearch();
            ProtoDataConverter.GetProto(this, lprotoPartySearch);
            return lprotoPartySearch;
        }
    }
    public partial class PartySearchResultset
    {
        public void GetBus(protoPartySearchResult aprotoPartySearchResult)
        {
            ProtoDataConverter.GetData(aprotoPartySearchResult, this);
        }
        public protoPartySearchResultSet GetProto()
        {
            protoPartySearchResultSet lprotoPartySearchResultSet = new protoPartySearchResultSet();
            ProtoDataConverter.GetProto(this, lprotoPartySearchResultSet);
            return lprotoPartySearchResultSet;
        }
    }
    public partial class PartySearchResult
    {
        public protoPartySearchResult GetProto (SearchResultBase<PartySearchResultset> result)
        {
            protoPartySearchResult lprotoPartySearchResult = new protoPartySearchResult();
            ProtoDataConverter.GetProto(result, lprotoPartySearchResult);
           // if (result != null && result. > 0)
            {
                foreach (PartySearchResultset obj in result.SearchResultSet)
                {
                    lprotoPartySearchResult.PlstprotoPartySearchResultSet.Add(obj.GetProto());
                }
            }
            return lprotoPartySearchResult;
        }
    }
}

