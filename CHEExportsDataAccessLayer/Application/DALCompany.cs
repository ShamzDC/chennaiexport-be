
using CHEExportsDataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataAccessLayer
{
    [Serializable]
    [DataContract]
    public class DALCompany : DALBase
    {
        [DataMember]
        public Company iCompany { get; private set; }

        public DALCompany(Company aCompany) : base(aCompany)
        {
            iCompany = aCompany;
        }

        public void CreateNewCompany()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void SaveCompany(string token)
        {
            try
            {
                //ValidateCompanySave();
                if (iCompany != null && (iCompany.errorMsg_lsit == null || iCompany.errorMsg_lsit.Count == 0))
                {
                    if (iCompany.company_id == 0)
                    {
                        Save(token);
                    }
                    else
                    {
                        iCompany.changed_date = DateTime.Now;
                        iCompany.changed_by = iCompany.iLoggedInUserDetails.user_login_id;
                        Update(token);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        private void ValidateCompanySave()
        {
            throw new NotImplementedException();
        }

        public void UpdateCompany(string token)
        {
            try
            {
                //ValidateCompanySave();
                if (iCompany != null && (iCompany.errorMsg_lsit == null || iCompany.errorMsg_lsit.Count == 0))
                {
                    Update(token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void DeleteCompany(string token)
        {
            try
            {
                //ValidateCompanyDelete();
                if (iCompany != null && (iCompany.errorMsg_lsit == null || iCompany.errorMsg_lsit.Count == 0) && iCompany.company_id > 0)
                {
                    Delete(token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void OpenCompany(string token)
        {
            try
            {
                if (iCompany != null && (iCompany.errorMsg_lsit == null || iCompany.errorMsg_lsit.Count == 0) && iCompany.company_id > 0)
                {
                    Open(token);
                    if (iCompany.company_id > 0)
                    {
                        iCompany.lstCompanyBankDetail = CommonDAL.SelectDataFromDataBase<CompanyBankDetail>(new string[] { "COMPANY_ID" }, new string[] { "=" },
                            new object[] { iCompany.company_id }).OrderByDescending(x => x.company_bank_id).ToList();
                        if (iCompany.lstCompanyBankDetail != null && iCompany.lstCompanyBankDetail.Count > 0)
                        {
                            foreach (CompanyBankDetail lCompanyBankDetail in iCompany.lstCompanyBankDetail)
                            {
                                DALCompanyBankDetail lDALCompanyBankDetail = new DALCompanyBankDetail(lCompanyBankDetail);
                                lDALCompanyBankDetail.Setdescription();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
    }
}
