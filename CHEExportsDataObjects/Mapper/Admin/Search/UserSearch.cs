
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class UserSearch
    {
        public void GetData(protoUserSearch aprotoUserSearch)
        {
            ProtoDataConverter.GetData(aprotoUserSearch, this);
        }
        public protoUserSearch GetProto()
        {
            protoUserSearch lprotoUserSearch = new protoUserSearch();
            ProtoDataConverter.GetProto(this, lprotoUserSearch);
            return lprotoUserSearch;
        }
    }
    public partial class UserSearchResultset
    {
        public void GetBus(protoUserSearchResult aprotoUserSearchResult)
        {
            ProtoDataConverter.GetData(aprotoUserSearchResult, this);
        }
        public protoUserSearchResultSet GetProto()
        {
            protoUserSearchResultSet lprotoUserSearchResultSet = new protoUserSearchResultSet();
            ProtoDataConverter.GetProto(this, lprotoUserSearchResultSet);
            return lprotoUserSearchResultSet;
        }
    }
    public partial class UserSearchResult
    {
        public protoUserSearchResult GetProto (SearchResultBase<UserSearchResultset> result)
        {
            protoUserSearchResult lprotoUserSearchResult = new protoUserSearchResult();
            ProtoDataConverter.GetProto(result, lprotoUserSearchResult);
           // if (result != null && result. > 0)
            {
                foreach (UserSearchResultset obj in result.SearchResultSet)
                {
                    lprotoUserSearchResult.PlstprotoUserSearchResultSet.Add(obj.GetProto());
                }
            }
            return lprotoUserSearchResult;
        }
    }
}

