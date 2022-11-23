using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedNurseData
{
    public class NurseRoasterData:BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string Day_I { get; set; }
        [DataMember]
        public string Day_II { get; set; }
        [DataMember]
        public string Day_III { get; set; }
        [DataMember]
        public string Day_IV { get; set; }
        [DataMember]
        public string Day_V { get; set; }
        [DataMember]
        public string Day_VI { get; set; }
        [DataMember]
        public string Day_VII { get; set; }
        [DataMember]
        public Int32 weekID { get; set; }
        [DataMember]
        public Int32 monthID { get; set; }
        [DataMember]
        public Int32 yearID { get; set; }
        [DataMember]
        public Int32 InsertType { get; set; }
        [DataMember]
        public Int32 designationID { get; set; }
        [DataMember]
        public Int32 deptID { get; set; }
        [DataMember]
        public Int32 nurseID { get; set; }
        [DataMember]
        public Int32 nurseType { get; set; }
        [DataMember]
        public string NurseName { get; set; }
        [DataMember]
        public string Nursedesgn { get; set; }
        [DataMember]
        public Int64 EmployeeID { get; set; }

    }
    public class NurRoasterDataTOeXCEL
    {



        [DataMember]
        public Int64 EmployeeID { get; set; }
        [DataMember]
        public string NurseName { get; set; }
        [DataMember]
        public string Day_I { get; set; }
        [DataMember]
        public string Day_II { get; set; }
        [DataMember]
        public string Day_III { get; set; }
        [DataMember]
        public string Day_IV { get; set; }
        [DataMember]
        public string Day_V { get; set; }
        [DataMember]
        public string Day_VI { get; set; }
        [DataMember]
        public string Day_VII { get; set; }
       
    }
}
