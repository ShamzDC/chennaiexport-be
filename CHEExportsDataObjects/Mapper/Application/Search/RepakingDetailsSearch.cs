
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class RepakingDetailsSearch
    {
        public void GetData(protoRepackingDetailSearch aprotoRepackingDetailSearch)
        {
            ProtoDataConverter.GetData(aprotoRepackingDetailSearch, this);
        }
        public protoRepackingDetailSearch GetProto()
        {
            protoRepackingDetailSearch lprotoRepackingDetailSearch = new protoRepackingDetailSearch();
            ProtoDataConverter.GetProto(this, lprotoRepackingDetailSearch);
            return lprotoRepackingDetailSearch;
        }
    }
    public partial class RepakingDetailsSearchResultset
    {
        public void GetBus(protoRepackingDetailResult aprotoRepackingDetailResult)
        {
            ProtoDataConverter.GetData(aprotoRepackingDetailResult, this);
        }
        public protoRepackingDetailResultSet GetProto()
        {
            protoRepackingDetailResultSet lprotoRepackingDetailResultSet = new protoRepackingDetailResultSet();
            ProtoDataConverter.GetProto(this, lprotoRepackingDetailResultSet);
            return lprotoRepackingDetailResultSet;
        }
    }
    public partial class RepackingDetailSearchResult
    {
        public protoRepackingDetailResult GetProto (SearchResultBase<RepakingDetailsSearchResultset> result)
        {
            protoRepackingDetailResult lprotoRepackingDetailResult = new protoRepackingDetailResult();
            ProtoDataConverter.GetProto(result, lprotoRepackingDetailResult);
           // if (result != null && result. > 0)
            {
                foreach (RepakingDetailsSearchResultset obj in result.SearchResultSet)
                {
                    lprotoRepackingDetailResult.PlstprotoRepackingDetailResultSet.Add(obj.GetProto());
                }
            }
            return lprotoRepackingDetailResult;
        }
    }
}

