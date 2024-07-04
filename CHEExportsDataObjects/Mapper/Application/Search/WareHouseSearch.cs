
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class WareHouseSearch
    {
        public void GetData(protoWareHouseSearch aprotoWareHouseSearch)
        {
            ProtoDataConverter.GetData(aprotoWareHouseSearch, this);
        }
        public protoWareHouseSearch GetProto()
        {
            protoWareHouseSearch lprotoWareHouseSearch = new protoWareHouseSearch();
            ProtoDataConverter.GetProto(this, lprotoWareHouseSearch);
            return lprotoWareHouseSearch;
        }
    }
    public partial class WareHouseSearchResultset
    {
        public void GetBus(protoWareHouseSearchResult aprotoWareHouseSearchResult)
        {
            ProtoDataConverter.GetData(aprotoWareHouseSearchResult, this);
        }
        public protoWareHouseSearchResultSet GetProto()
        {
            protoWareHouseSearchResultSet lprotoWareHouseSearchResultSet = new protoWareHouseSearchResultSet();
            ProtoDataConverter.GetProto(this, lprotoWareHouseSearchResultSet);
            return lprotoWareHouseSearchResultSet;
        }
    }
    public partial class WareHouseSearchResult
    {
        public protoWareHouseSearchResult GetProto (SearchResultBase<WareHouseSearchResultset> result)
        {
            protoWareHouseSearchResult lprotoWareHouseSearchResult = new protoWareHouseSearchResult();
            ProtoDataConverter.GetProto(result, lprotoWareHouseSearchResult);
           // if (result != null && result. > 0)
            {
                foreach (WareHouseSearchResultset obj in result.SearchResultSet)
                {
                    lprotoWareHouseSearchResult.PlstprotoWareHouseSearchResultSet.Add(obj.GetProto());
                }
            }
            return lprotoWareHouseSearchResult;
        }
    }
}

