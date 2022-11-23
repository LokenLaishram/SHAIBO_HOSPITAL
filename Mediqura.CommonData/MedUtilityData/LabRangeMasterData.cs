using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.Common;

namespace Mediqura.CommonData.MedUtilityData
{
    public class LabRangeMasterData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public string Units { get; set; }
        [DataMember]
        public string Age { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public string NormalRange { get; set; }
        [DataMember]
        public string MachineName { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpName { get; set; }
    }
    public class LabRangeDatatoExcel
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public string Units { get; set; }
        [DataMember]
        public string Age { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public string NormalRange { get; set; }
        [DataMember]
        public string MachineName { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }

    }
}

