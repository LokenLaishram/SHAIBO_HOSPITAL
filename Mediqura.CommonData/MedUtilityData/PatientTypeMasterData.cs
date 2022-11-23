using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Mediqura.CommonData.MedUtilityData
{
    public class PatientTypeMasterData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public Int32 receivable { get; set; }
        [DataMember]
        public string PatientTypeCode { get; set; }
        [DataMember]
        public string PatientType { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public string PatientCategoryCode { get; set; }
        [DataMember]
        public string PatientCategory { get; set; }
    }
    public class PatientTypeDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string PatientTypeCode { get; set; }
        [DataMember]
        public string PatientType { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
        [DataMember]
        public string PatientCategoryCode { get; set; }
        [DataMember]
        public string PatientCategory { get; set; }

    }
}
