using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MSBData
{
    public class MsbBedAllotData: BaseData
    {
        [DataMember]
        public int ID { get; set; }
      
        [DataMember]
        public int EmployeeGradeID { get; set; }
        [DataMember]
        public string EmployeeGrade { get; set; }
        [DataMember]
        public int BedAllotedID { get; set; }
        [DataMember]
        public string BedAlloted { get; set; }
        [DataMember]
        public int isSelf { get; set; }
        [DataMember]
        public int isDependent { get; set; }
    }
}
