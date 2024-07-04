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
    public class DALMasterConfig : DALBase
    {
        [DataMember]
        public MasterConfig iMasterConfig { get; private set; }

        public DALMasterConfig(MasterConfig aMasterConfig) : base(aMasterConfig)
        {
            iMasterConfig = aMasterConfig;
        }

        public void CreateNewMasterConfig()
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

        public void SaveMasterConfig(string token)
        {
            try
            {
                ValidateMasterConfigSave();
                if (iMasterConfig != null && (iMasterConfig.errorMsg_lsit == null || iMasterConfig.errorMsg_lsit.Count == 0))
                {

                    Save(token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        private void ValidateMasterConfigSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateMasterConfig(string token)
        {
            try
            {
                //ValidateUserSave();
                if (iMasterConfig != null && (iMasterConfig.errorMsg_lsit == null || iMasterConfig.errorMsg_lsit.Count == 0))
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

        public void DeleteMasterConfig(string token)
        {
            try
            {
                // ValidateMasterConfigDelete();
                if (iMasterConfig != null && (iMasterConfig.errorMsg_lsit == null || iMasterConfig.errorMsg_lsit.Count == 0) && iMasterConfig.master_config_id > 0)
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

        public void OpenMasterConfig(string token)
        {
            try
            {
                if (iMasterConfig != null && (iMasterConfig.errorMsg_lsit == null || iMasterConfig.errorMsg_lsit.Count == 0) && iMasterConfig.master_config_id > 0)
                {
                    Open(token);
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
