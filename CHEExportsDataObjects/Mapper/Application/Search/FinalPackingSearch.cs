
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class FinalPackingDetailsSearch
    {
        public void GetData(protoFinalPackingDetailsSearch aprotoFinalPackingDetailsSearch)
        {
            ProtoDataConverter.GetData(aprotoFinalPackingDetailsSearch, this);
        }
        public protoFinalPackingDetailsSearch GetProto()
        {
            protoFinalPackingDetailsSearch lprotoFinalPackingDetailsSearch = new protoFinalPackingDetailsSearch();
            ProtoDataConverter.GetProto(this, lprotoFinalPackingDetailsSearch);
            return lprotoFinalPackingDetailsSearch;
        }
    }
    public partial class FinalPackingDetailsSearchResultset
    {
        public void GetBus(protoFinalPackingDetailsSearchResult aprotoFinalPackingDetailsSearchResult)
        {
            ProtoDataConverter.GetData(aprotoFinalPackingDetailsSearchResult, this);
        }
        public protoFinalPackingDetailsSearchResultSet GetProto()
        {
            protoFinalPackingDetailsSearchResultSet lprotoFinalPackingDetailsSearchResultSet = new protoFinalPackingDetailsSearchResultSet();
            ProtoDataConverter.GetProto(this, lprotoFinalPackingDetailsSearchResultSet);
            return lprotoFinalPackingDetailsSearchResultSet;
        }
    }
    public partial class FinalPackingDetailsSearchResult
    {
        public protoFinalPackingDetailsSearchResult GetProto(SearchResultBase<FinalPackingDetailsSearchResultset> result)
        {
            protoFinalPackingDetailsSearchResult lprotoFinalPackingDetailsSearchResult = new protoFinalPackingDetailsSearchResult();
            ProtoDataConverter.GetProto(result, lprotoFinalPackingDetailsSearchResult);
            // if (result != null && result. > 0)
            {
                foreach (FinalPackingDetailsSearchResultset obj in result.SearchResultSet)
                {
                    lprotoFinalPackingDetailsSearchResult.PlstprotoFinalPackingDetailSearchResultSet.Add(obj.GetProto());
                }
            }
            return lprotoFinalPackingDetailsSearchResult;
        }
    }
}

