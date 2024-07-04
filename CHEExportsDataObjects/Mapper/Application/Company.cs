
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    public partial class Company
    {
        public void GetData(protoCompany aprotoCompany)
        {
            ProtoDataConverter.GetData(aprotoCompany, this);
            if (aprotoCompany.LstprotoCompanyBankDetail != null && aprotoCompany.LstprotoCompanyBankDetail.Count > 0)
            {
                if (this.lstCompanyBankDetail != null)
                {
                    foreach (protoCompanyBankDetail obj in aprotoCompany.LstprotoCompanyBankDetail)
                    {
                        CompanyBankDetail lCompanyBankDetail = new CompanyBankDetail();
                        lCompanyBankDetail.GetData(obj);
                        this.lstCompanyBankDetail.Add(lCompanyBankDetail);
                    }
                }
            }
        }

        public protoCompany GetProto()
        {
            protoCompany lprotoCompany = new protoCompany();
            ProtoDataConverter.GetProto(this, lprotoCompany);
            if (this.lstCompanyBankDetail != null)
            {
                foreach (CompanyBankDetail obj in this.lstCompanyBankDetail)
                {
                    lprotoCompany.LstprotoCompanyBankDetail.Add(obj.GetProto());
                }
            }
            ProtoDataConverter.SetMessages(this, lprotoCompany);
            return lprotoCompany;
        }
    }
}

