
using CHEExportsDataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataAccessLayer
{
    [Serializable]
    [DataContract]
    public class DALCompanyBankDetail : DALBase
    {
        [DataMember]
        public CompanyBankDetail iCompanyBankDetail { get; private set; }

        public DALCompanyBankDetail(CompanyBankDetail aCompanyBankDetail) : base(aCompanyBankDetail)
        {
            iCompanyBankDetail = aCompanyBankDetail;
        }
        public void Setdescription()
        {
            string config_ids = Constants.Application.Status_id + "," + Constants.Application.Active_Iactive_Status_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iCompanyBankDetail.status_description = lstSubConfig.Where(x => x.s_config_value == iCompanyBankDetail.status_value).Select(x => x.s_config_description).FirstOrDefault();

        }
        public void CreateNewCompanyBankDetail()
        {
            try
            {
                iCompanyBankDetail.status_value = Constants.Application.Active;
                Setdescription();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        public void SaveCompanyBankDetail(string token)
        {
            try
            {
                //ValidateCompanyBankDetailSave();
                
                if (iCompanyBankDetail != null && (iCompanyBankDetail.errorMsg_lsit == null || iCompanyBankDetail.errorMsg_lsit.Count == 0))
                {
                    Save(token);
                    Setdescription();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        private void ValidateCompanyBankDetailSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateCompanyBankDetail(string token)
        {
            try
            {
                //ValidateCompanyBankDetailSave();
                if (iCompanyBankDetail != null && (iCompanyBankDetail.errorMsg_lsit == null || iCompanyBankDetail.errorMsg_lsit.Count == 0))
                {
                    Update(token);
                    Setdescription();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void DeleteCompanyBankDetail(string token)
        {
            try
            {
                //ValidateCompanyBankDetailDelete();
                if (iCompanyBankDetail != null && (iCompanyBankDetail.errorMsg_lsit == null || iCompanyBankDetail.errorMsg_lsit.Count == 0) && iCompanyBankDetail.company_bank_id > 0)
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

        public void OpenCompanyBankDetail(string token)
        {
            try
            {
                if (iCompanyBankDetail != null && (iCompanyBankDetail.errorMsg_lsit == null || iCompanyBankDetail.errorMsg_lsit.Count == 0) && iCompanyBankDetail.company_bank_id > 0)
                {
                    Open(token);
                    Setdescription();
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




