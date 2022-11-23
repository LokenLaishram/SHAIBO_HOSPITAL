using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
   public class DoctorMasterData : BaseData
    {
        [DataMember]
        public int DoctorID { get; set; }
        [DataMember]
        public int DoctorType { get; set; }
        [DataMember]
        public int DepartmentID { get; set; }
        [DataMember]
        public string DoctorName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public string Remarks { get; set; }
    }
}
