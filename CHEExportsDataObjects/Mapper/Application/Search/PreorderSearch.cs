
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class PreorderSearch
    {
        public void GetData(protoPreorderSearch aprotoPreorderSearch)
        {
            ProtoDataConverter.GetData(aprotoPreorderSearch, this);
        }
        public protoPreorderSearch GetProto()
        {
            protoPreorderSearch lprotoPreorderSearch = new protoPreorderSearch();
            ProtoDataConverter.GetProto(this, lprotoPreorderSearch);
            return lprotoPreorderSearch;
        }
    }
    public partial class PreorderSearchResultset
    {
        public void GetBus(protoPreorderSearchResult aprotoPreorderSearchResult)
        {
            ProtoDataConverter.GetData(aprotoPreorderSearchResult, this);
        }
        public protoPreorderSearchResultSet GetProto()
        {
            protoPreorderSearchResultSet lprotoPreorderSearchResultSet = new protoPreorderSearchResultSet();
            ProtoDataConverter.GetProto(this, lprotoPreorderSearchResultSet);
            return lprotoPreorderSearchResultSet;
        }
    }
    public partial class PreorderSearchResult
    {
        public protoPreorderSearchResult GetProto (SearchResultBase<PreorderSearchResultset> result)
        {
            protoPreorderSearchResult lprotoPreorderSearchResult = new protoPreorderSearchResult();
            ProtoDataConverter.GetProto(result, lprotoPreorderSearchResult);
           // if (result != null && result. > 0)
            {
                foreach (PreorderSearchResultset obj in result.SearchResultSet)
                {
                    lprotoPreorderSearchResult.PlstprotoPreorderSearchResultSet.Add(obj.GetProto());
                }
            }
            return lprotoPreorderSearchResult;
        }
    }
}

