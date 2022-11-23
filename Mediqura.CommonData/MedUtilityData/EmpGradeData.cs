using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class EmpGradeData:BaseData
    {
        [DataMember]
        public int GradeID { get; set; }
        [DataMember]
        public string GradeCode { get; set; }
        [DataMember]
        public string Gradedescp { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string IPNo { get; set; }
    }
    public class EmpGradeDatatoExcel
    {
        [DataMember]
        public int GradeID { get; set; }
        [DataMember]
        public string GradeCode { get; set; }
        [DataMember]
        public string Gradedescp { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
}
