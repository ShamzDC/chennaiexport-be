
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class CompanyBankDetail
    {
        public void GetData(protoCompanyBankDetail aprotoCompanyBankDetail)
        {
            ProtoDataConverter.GetData(aprotoCompanyBankDetail, this);
           
        }

        public protoCompanyBankDetail GetProto()
        {
            protoCompanyBankDetail lprotoCompanyBankDetail = new protoCompanyBankDetail();
            ProtoDataConverter.GetProto(this, lprotoCompanyBankDetail);
           
            ProtoDataConverter.SetMessages(this, lprotoCompanyBankDetail);
            return lprotoCompanyBankDetail;
        }
    }
}



