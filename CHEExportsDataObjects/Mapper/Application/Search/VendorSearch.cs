
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class VendorSearch
    {
        public void GetData(protoVendorSearch aprotoVendorSearch)
        {
            ProtoDataConverter.GetData(aprotoVendorSearch, this);
        }
        public protoVendorSearch GetProto()
        {
            protoVendorSearch lprotoVendorSearch = new protoVendorSearch();
            ProtoDataConverter.GetProto(this, lprotoVendorSearch);
            return lprotoVendorSearch;
        }
    }
    public partial class VendorSearchResultset
    {
        public void GetBus(protoVendorSearchResult aprotoVendorSearchResult)
        {
            ProtoDataConverter.GetData(aprotoVendorSearchResult, this);
        }
        public protoVendorSearchResultSet GetProto()
        {
            protoVendorSearchResultSet lprotoVendorSearchResultSet = new protoVendorSearchResultSet();
            ProtoDataConverter.GetProto(this, lprotoVendorSearchResultSet);
            return lprotoVendorSearchResultSet;
        }
    }
    public partial class VendorSearchResult
    {
        public protoVendorSearchResult GetProto (SearchResultBase<VendorSearchResultset> result)
        {
            protoVendorSearchResult lprotoVendorSearchResult = new protoVendorSearchResult();
            ProtoDataConverter.GetProto(result, lprotoVendorSearchResult);
           // if (result != null && result. > 0)
            {
                foreach (VendorSearchResultset obj in result.SearchResultSet)
                {
                    lprotoVendorSearchResult.PstprotoVendorSearchResultSet.Add(obj.GetProto());
                }
            }
            return lprotoVendorSearchResult;
        }
    }
}

