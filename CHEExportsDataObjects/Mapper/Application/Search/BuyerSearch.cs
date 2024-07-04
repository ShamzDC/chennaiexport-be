
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class BuyerSearch
    {
        public void GetData(protoBuyerSearch aprotoBuyerSearch)
        {
            ProtoDataConverter.GetData(aprotoBuyerSearch, this);
        }
        public protoBuyerSearch GetProto()
        {
            protoBuyerSearch lprotoBuyerSearch = new protoBuyerSearch();
            ProtoDataConverter.GetProto(this, lprotoBuyerSearch);
            return lprotoBuyerSearch;
        }
    }
    public partial class BuyerSearchResultset
    {
        public void GetBus(protoBuyerSearchResult aprotoBuyerSearchResult)
        {
            ProtoDataConverter.GetData(aprotoBuyerSearchResult, this);
        }
        public protoBuyerSearchResultSet GetProto()
        {
            protoBuyerSearchResultSet lprotoBuyerSearchResultSet = new protoBuyerSearchResultSet();
            ProtoDataConverter.GetProto(this, lprotoBuyerSearchResultSet);
            return lprotoBuyerSearchResultSet;
        }
    }
    public partial class BuyerSearchResult
    {
        public protoBuyerSearchResult GetProto (SearchResultBase<BuyerSearchResultset> result)
        {
            protoBuyerSearchResult lprotoBuyerSearchResult = new protoBuyerSearchResult();
            ProtoDataConverter.GetProto(result, lprotoBuyerSearchResult);
           // if (result != null && result. > 0)
            {
                foreach (BuyerSearchResultset obj in result.SearchResultSet)
                {
                    lprotoBuyerSearchResult.PlstprotoBuyerSearchResultSet.Add(obj.GetProto());
                }
            }
            return lprotoBuyerSearchResult;
        }
    }
}

