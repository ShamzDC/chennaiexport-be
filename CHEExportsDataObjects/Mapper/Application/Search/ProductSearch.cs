
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class ProductSearch
    {
        public void GetData(protoProductSearch aprotoProductSearch)
        {
            ProtoDataConverter.GetData(aprotoProductSearch, this);
        }
        public protoProductSearch GetProto()
        {
            protoProductSearch lprotoProductSearch = new protoProductSearch();
            ProtoDataConverter.GetProto(this, lprotoProductSearch);
            return lprotoProductSearch;
        }
    }
    public partial class ProductSearchResultset
    {
        public void GetBus(protoProductSearchResult aprotoCustomerSearchResult)
        {
            ProtoDataConverter.GetData(aprotoCustomerSearchResult, this);
        }
        public protoProductSearchResultSet GetProto()
        {
            protoProductSearchResultSet lprotoProductResultSet = new protoProductSearchResultSet();
            ProtoDataConverter.GetProto(this, lprotoProductResultSet);
            return lprotoProductResultSet;
        }
    }
    public partial class ProductSearchResult
    {
        public protoProductSearchResult GetProto(SearchResultBase<ProductSearchResultset> result)
        {
            protoProductSearchResult lprotoProductResult = new protoProductSearchResult();
            ProtoDataConverter.GetProto(result, lprotoProductResult);
            // if (result != null && result. > 0)
            {
                foreach (ProductSearchResultset obj in result.SearchResultSet)
                {
                    lprotoProductResult.PlstprotoProductSearchResultSet.Add(obj.GetProto());
                }
            }
            return lprotoProductResult;
        }
    }
}

