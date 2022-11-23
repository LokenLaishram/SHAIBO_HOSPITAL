using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedLab
{
    public class LadReportCollectionData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 billID { get; set; }
        [DataMember]
        public Int64 LabTestID { get; set; }
        [DataMember]
        public Int64 LabID { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string InVnumber { get; set; }
        [DataMember]
        public string IpNo { get; set; }
        [DataMember]
        public int HeaderID { get; set; }
        [DataMember]
        public int TestID { get; set; }
        [DataMember]
        public int Urgency { get; set; }
        [DataMember]
        public int ReportTypeID { get; set; }
        [DataMember]
        public int CultueReportTypeID { get; set; }
        [DataMember]
        public int isVerified { get; set; }
        [DataMember]
        public int isPrinted { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public string EmailTestName { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public DateTime TestDate { get; set; }
        [DataMember]
        public int LabGrpID { get; set; }
        [DataMember]
        public int LabSubGrpID { get; set; }
        [DataMember]
        public int PatientType { get; set; }        
        [DataMember]
        public int TestStatus { get; set; }
        [DataMember]
        public int TemplateID { get; set; }
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
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string TemplateType { get; set; }
        [DataMember]
        public int DeliveryStatus { get; set; }
        [DataMember]
        public string ReportImage { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Testids { get; set; }
        [DataMember]
        public int RunnerID { get; set; }
    }
    public class LadReportCollectionDataTOeXCEL
    {
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string InVnumber { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public DateTime TestDate { get; set; }

    }
}

