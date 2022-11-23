using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.OTData
{
    public class PatientConsentTypeData:BaseData
    {
        [DataMember]
        public int TypeID { get; set; }
        [DataMember]
        public string TypeCode { get; set; }
        [DataMember]
        public string Typedescp { get; set; }
        [DataMember]
        public string Remarks { get; set; }
    }
    public class PatientConsentData : BaseData
    {
        [DataMember]
        public Int32 ID { get; set; }
        [DataMember]
        public Int32 TypeID { get; set; }
        [DataMember]
        public string Template { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string Agecount { get; set; }
        [DataMember]
        public string GenderName { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public string DoctorName { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public string PatientAddress { get; set; }
         [DataMember]
        public string ConsentType { get; set; }
         [DataMember]
         public DateTime AdmissionDate { get; set; }
         [DataMember]
         public string RelativeName { get; set; }
         [DataMember]
         public Int32 RelationID { get; set; }
         [DataMember]
         public string Relation { get; set; }
    }
    public class PatientConsentTypetoExcel
    {
        [DataMember]
        public int TypeID { get; set; }
        [DataMember]
        public string TypeCode { get; set; }
        [DataMember]
        public string Typedescp { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
}
