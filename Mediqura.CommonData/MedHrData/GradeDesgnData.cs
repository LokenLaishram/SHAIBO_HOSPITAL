using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedHrData
{
    public class GradeDesgnData:BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int32 GradeID { get; set; }
        [DataMember]
        public Int32 DesignID { get; set; }
        [DataMember]
        public string Designation { get; set; }
        [DataMember]
        public string Grade { get; set; }
    }
}
