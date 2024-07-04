
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
        public class DALMessageConfig : DALBase
        {
            [DataMember]
            public MessageConfig iMessage { get; private set; }

            public DALMessageConfig(MessageConfig aMessage) : base(aMessage)
            {
                iMessage = aMessage;
            }

            public void CreateNewMessage()
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

            public void SaveMessage(string token)
            {
                try
                {
                    ValidateMessageSave();
                    if (iMessage != null && (iMessage.errorMsg_lsit == null || iMessage.errorMsg_lsit.Count == 0))
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

            private void ValidateMessageSave()
            {
                throw new NotImplementedException();
            }

            public void UpdateMessage(string token)
            {
                try
                {
                    //ValidateUserSave();
                    if (iMessage != null && (iMessage.errorMsg_lsit == null || iMessage.errorMsg_lsit.Count == 0))
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

            public void DeleteMessage(string token)
            {
                try
                {
                    // ValidateMessageDelete();
                    if (iMessage != null && (iMessage.errorMsg_lsit == null || iMessage.errorMsg_lsit.Count == 0) && iMessage.message_id > 0)
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

            public void OpenMessage(string token)
            {
                try
                {
                    if (iMessage != null && (iMessage.errorMsg_lsit == null || iMessage.errorMsg_lsit.Count == 0) && iMessage.message_id > 0)
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

