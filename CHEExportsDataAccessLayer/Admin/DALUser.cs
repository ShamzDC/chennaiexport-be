using CHEExportsDataObjects;
using CHEExportsProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CHEExportsDataAccessLayer
{
    [Serializable]
    [DataContract]
    public class DALUser : DALBase
    {
        [DataMember]
        public User iUser { get; private set; }

        public DALUser(User aUser) : base (aUser)
        {
            iUser = aUser;
        }

        public void CreateNewUser()
        {
            try
            {
                iUser.branch_id = 20;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void SaveUser(string token)
        {
            try
            {
                //ValidateUserSave();
                if(iUser != null && (iUser.errorMsg_lsit == null || iUser.errorMsg_lsit.Count == 0))
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

        private void ValidateUserSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(string token)
        {
            try
            {
                //ValidateUserSave();
                if (iUser != null && (iUser.errorMsg_lsit == null || iUser.errorMsg_lsit.Count == 0))
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

        public void DeleteUser(string token)
        {
            try
            {
                //ValidateUserDelete();
                if (iUser != null && (iUser.errorMsg_lsit == null || iUser.errorMsg_lsit.Count == 0) && iUser.user_id > 0)
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

        public void OpenUser(string token)
        {
            try
            {
                if (iUser != null && (iUser.errorMsg_lsit == null || iUser.errorMsg_lsit.Count == 0) && iUser.user_id > 0)
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

        public protoLoggedInDetails AuthenticateUser()
        {
            protoLoggedInDetails lprotoLoggedInDetails = new protoLoggedInDetails();
            try
            {
                User lUser = CommonDAL.SelectDataFromDataBase<User>(new string[] { "USER_LOGIN_ID", "KEYWORD" }, new string[] { "=", "=" }, new object[] { iUser.user_login_id, iUser.keyword }).ToList().FirstOrDefault();
                if(lUser != null)
                {
                    lprotoLoggedInDetails.Msg = new protoMessage();
                    lprotoLoggedInDetails.Msg.InfoMessage = new protoMsgDetail() { MsgDescription = "Logged-in Successfully."};
                    LoggedInUserDetails loggedInUserDetails = new LoggedInUserDetails()
                    {
                        user_login_id = lUser.user_login_id,
                        first_name = lUser.first_name,
                        middle_name = lUser.middle_name,
                        last_name = lUser.last_name,
                        email_id = lUser.email_id
                    };
                    string json = JsonSerializer.Serialize(loggedInUserDetails);
                    string encrypted_json = CommonDAL.Encryption.Encrypt(json);
                    lUser.key_token = encrypted_json;
                    DALUser ldALUser = new DALUser(lUser);
                    ldALUser.Update("", true);
                    lprotoLoggedInDetails.PprotoUser = lUser.GetProto();
                }
                else
                {
                    lprotoLoggedInDetails.Msg = new protoMessage();
                    lprotoLoggedInDetails.Msg.ErrorMessage.Add(new protoMsgDetail() { MsgDescription = "User not found." });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            return lprotoLoggedInDetails;
        }
    }
}
