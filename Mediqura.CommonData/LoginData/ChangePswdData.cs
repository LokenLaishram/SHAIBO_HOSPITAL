using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.LoginData
{
    public class ChangePswdData:BaseData
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string newPassword { get; set; }
        [DataMember]
        public string oldpassword { get; set; }
        [DataMember]
        public int ID { get; set; }
    }
}
