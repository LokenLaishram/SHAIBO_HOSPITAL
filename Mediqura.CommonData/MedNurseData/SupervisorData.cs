using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedNurseData
{
    public class SupervisorData : BaseData
    {

        [DataMember]
        public string EmployeeName { get; set; }
        [DataMember]
        public Int32 WardID { get; set; }
        [DataMember]
        public string WardName { get; set; }
        [DataMember]
        public Int64 SupervisorID { get; set; }
        [DataMember]
        public string SupervisortName { get; set; }
        [DataMember]
        public Int64 AsstSuperitendantID { get; set; }
        [DataMember]
        public string AsstSuperitendant { get; set; }
    }
    public class SupervisorDatatoEcxel : BaseData
    {
        [DataMember]
        public string WardName { get; set; }
        [DataMember]
        public string SupervisortName { get; set; }
    }
    public class AsstNursingSPDatatoEcxel : BaseData
    {
        [DataMember]
        public string AsstSuperitendant { get; set; }
        [DataMember]
        public string SupervisorName { get; set; }
    }
}
