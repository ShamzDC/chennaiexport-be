using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataObjects
{
    [Serializable]
    [DataContract]
    public class LoggedInUserDetails
    {
        [DataMember]
        public string user_login_id { get; set; }

        [DataMember]
        public string first_name { get; set; }

        [DataMember]
        public string middle_name { get; set; }

        [DataMember]
        public string last_name { get; set; }

        [DataMember]
        public string email_id { get; set; }
    }
}
