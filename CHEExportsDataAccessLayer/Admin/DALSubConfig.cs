
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
        public class DALSubConfig : DALBase
        {
            [DataMember]
            public SubConfig iSubConfig { get; private set; }

            public DALSubConfig(SubConfig aSubConfig) : base(aSubConfig)
            {
                iSubConfig = aSubConfig;
            }

            public void CreateNewSubConfig()
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

            public void SaveSubConfig(string token)
            {
                try
                {
                    ValidateSubConfigSave();
                    if (iSubConfig != null && (iSubConfig.errorMsg_lsit == null || iSubConfig.errorMsg_lsit.Count == 0))
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

            private void ValidateSubConfigSave()
            {
                throw new NotImplementedException();
            }

            public void UpdateSubConfig(string token)
            {
                try
                {
                    //ValidateUserSave();
                    if (iSubConfig != null && (iSubConfig.errorMsg_lsit == null || iSubConfig.errorMsg_lsit.Count == 0))
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

            public void DeleteSubConfig(string token)
            {
                try
                {
                    // ValidateSubConfigDelete();
                    if (iSubConfig != null && (iSubConfig.errorMsg_lsit == null || iSubConfig.errorMsg_lsit.Count == 0) && iSubConfig.sub_config_id > 0)
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

            public void OpenSubConfig(string token)
            {
                try
                {
                    if (iSubConfig != null && (iSubConfig.errorMsg_lsit == null || iSubConfig.errorMsg_lsit.Count == 0) && iSubConfig.sub_config_id > 0)
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


