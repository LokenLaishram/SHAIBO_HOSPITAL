using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData
{
    public class RadiologyReportData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 billID { get; set; }
        [DataMember]
        public Int64 LabTestID { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public int TestID { get; set; }
        [DataMember]
        public int HeaderID { get; set; }
        [DataMember]
        public int Urgency { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public string InvNo { get; set; }
        [DataMember]
        public string IpNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public DateTime ToDate { get; set; }
        [DataMember]
        public DateTime FromDate { get; set; }
        [DataMember]
        public String TestDate { get; set; }
        [DataMember]
        public int LabSubGrpID { get; set; }
        [DataMember]
        public int LabGrpID { get; set; }
        [DataMember]
        public int PatientType { get; set; }
        [DataMember]
        public int ActionType { get; set; }
        [DataMember]
        public int TestStatus { get; set; }
        [DataMember]
        public string Template { get; set; }
        [DataMember]
        public string PatientAddress { get; set; }
        [DataMember]
        public string PatientAge { get; set; }
        [DataMember]
        public string ConsultingDcotor { get; set; }
        [DataMember]
        public DateTime ReportOn { get; set; }
        [DataMember]
        public string PatienSex { get; set; }
        [DataMember]
        public string VisitType { get; set; }
       
    }
}
