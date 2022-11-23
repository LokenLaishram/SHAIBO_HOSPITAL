using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.LoginData
{
    [Serializable]
    public class AddUsersData : BaseData
    {
        [DataMember]
        public int LoginID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string UserPassword { get; set; }
        [DataMember]
        public string RoleName { get; set; }
        [DataMember]
        public int UserTypeID { get; set; }
       
        [DataMember]
        public string EmployeeName { get; set; }
        
        [DataMember]
        public int enableNotification { get; set; }
        [DataMember]
        public string isNotification { get; set; }
    }
}
