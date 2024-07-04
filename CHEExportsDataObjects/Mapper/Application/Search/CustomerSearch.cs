
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class CustomerSearch
    {
        public void GetData(protoCustomerSearch aprotoCustomerSearch)
        {
            ProtoDataConverter.GetData(aprotoCustomerSearch, this);
        }
        public protoCustomerSearch GetProto()
        {
            protoCustomerSearch lprotoCustomerSearch = new protoCustomerSearch();
            ProtoDataConverter.GetProto(this, lprotoCustomerSearch);
            return lprotoCustomerSearch;
        }
    }
    public partial class CustomerSearchResultset
    {
        public void GetBus(protoCustomerSearchResult aprotoCustomerSearchResult)
        {
            ProtoDataConverter.GetData(aprotoCustomerSearchResult, this);
        }
        public protoCustomerSearchResultSet GetProto()
        {
            protoCustomerSearchResultSet lprotoCustomerSearchResultSet = new protoCustomerSearchResultSet();
            ProtoDataConverter.GetProto(this, lprotoCustomerSearchResultSet);
            return lprotoCustomerSearchResultSet;
        }
    }
    public partial class CustomerSearchResult
    {
        public protoCustomerSearchResult GetProto (SearchResultBase<CustomerSearchResultset> result)
        {
            protoCustomerSearchResult lprotoCustomerSearchResult = new protoCustomerSearchResult();
            ProtoDataConverter.GetProto(result, lprotoCustomerSearchResult);
           // if (result != null && result. > 0)
            {
                foreach (CustomerSearchResultset obj in result.SearchResultSet)
                {
                    lprotoCustomerSearchResult.PlstprotoCustomerSearchResultSet.Add(obj.GetProto());
                }
            }
            return lprotoCustomerSearchResult;
        }
    }
}

