
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class OrderSearch
    {
        public void GetData(protoOrderDetailsSearch aprotoOrderDetailsSearch)
        {
            ProtoDataConverter.GetData(aprotoOrderDetailsSearch, this);
        }
        public protoOrderDetailsSearch GetProto()
        {
            protoOrderDetailsSearch lprotoOrderDetailsSearch = new protoOrderDetailsSearch();
            ProtoDataConverter.GetProto(this, lprotoOrderDetailsSearch);
            return lprotoOrderDetailsSearch;
        }
    }
    public partial class OrderSearchResultset
    {
        public void GetBus(protoOrderDetailsResult aprotoCustomerSearchResult)
        {
            ProtoDataConverter.GetData(aprotoCustomerSearchResult, this);
        }
        public protoOrderDetailsResultSet GetProto()
        {
            protoOrderDetailsResultSet lprotoOrderDetailsResultSet = new protoOrderDetailsResultSet();
            ProtoDataConverter.GetProto(this, lprotoOrderDetailsResultSet);
            return lprotoOrderDetailsResultSet;
        }
    }
    public partial class OrderSearchResult
    {
        public protoOrderDetailsResult GetProto(SearchResultBase<OrderSearchResultset> result)
        {
            protoOrderDetailsResult lprotoOrderDetailsResult = new protoOrderDetailsResult();
            ProtoDataConverter.GetProto(result, lprotoOrderDetailsResult);
            // if (result != null && result. > 0)
            {
                foreach (OrderSearchResultset obj in result.SearchResultSet)
                {
                    lprotoOrderDetailsResult.PlstprotoOrderDetailsResultSet.Add(obj.GetProto());
                }
            }
            return lprotoOrderDetailsResult;
        }
    }
}

