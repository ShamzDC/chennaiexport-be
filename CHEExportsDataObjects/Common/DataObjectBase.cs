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
    public class DataObjectBase
    {
        [DataMember]
        public List<string> errorMsg_lsit {  get; set; }

        [DataMember]
        public string infoMsg {  get; set; }
        [DataMember]
        public List<string> infoMsgList {  get; set; }

        [DataMember]
        public LoggedInUserDetails iLoggedInUserDetails {  get; set; }
    }
}
