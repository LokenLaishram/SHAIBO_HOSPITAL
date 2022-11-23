using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.OTData
{
    public class OTRegnData : BaseData
    {
        [DataMember]
        public Int64 OTRegnID { get; set; }
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public int SerialID { get; set; }
        [DataMember]
        public string OTNo { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string OperationEndTime { get; set; }
        [DataMember]
        public string PatientAddress { get; set; }
        [DataMember]
        public string GenderID { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string GenderName { get; set; }
        [DataMember]
        public string AdmissionNo { get; set; }
        [DataMember]
        public DateTime OpernDate { get; set; }
        [DataMember]
        public string OpernTime { get; set; }
        [DataMember]
        public string CaseName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public Int32 CaseID { get; set; }
        [DataMember]
        public string OTemployee { get; set; }
        [DataMember]
        public Int64 OTemployeeID { get; set; }
        [DataMember]
        public Int32 OTemployeeTypeID { get; set; }
        [DataMember]
        public Int32 OTtype { get; set; }
        [DataMember]
        public Int32 OTroleID { get; set; }
        [DataMember]
        public string OTrole { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public DateTime OperationDate { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string OperationTime { get; set; }
        [DataMember]
        public int OTstatus { get; set; }
        [DataMember]
        public int AnaesthesiaType { get; set; }
        [DataMember]
        public string AnaesthesiaName { get; set; }
        [DataMember]
        public Decimal Amount { get; set; }
        [DataMember]
        public string OTpassnumber { get; set; }
        [DataMember]
        public DateTime AdmissionDate { get; set; }
        [DataMember]
        public Int32 OT_status { get; set; }
        [DataMember]
        public decimal TotalSurgeonAmount { get; set; }
        [DataMember]
        public decimal TotalAnaesthesiaAmount { get; set; }
        [DataMember]
        public decimal TotalAmount { get; set; }
        [DataMember]
        public int PayoutStatus { get; set; }
        [DataMember]
        public int MainDoctorID { get; set; }
        [DataMember]
        public int IsMainSurgeon { get; set; }
        [DataMember]
        public string OTTheatre { get; set; }
        [DataMember]
        public string OTDate { get; set; }
        [DataMember]
        public string OTDoc { get; set; }
        [DataMember]
        public decimal TotalCharges { get; set; }
    }
    public class OTRegnListDataTOeXCEL
    {
        [DataMember]
        public string OTpassnumber { get; set; }
        [DataMember]
        public string OTNo { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string OTDate { get; set; }
        [DataMember]
        public string OperationTime { get; set; }
        [DataMember]
        public string OperationEndTime { get; set; }
        [DataMember]
        public string OTStatus { get; set; }
     
    }
    public class OTRegnReportListDataTOeXCEL
    {
      
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public DateTime OperationDate { get; set; }
        [DataMember]
        public string CaseName { get; set; }
        [DataMember]
        public string OTDoc { get; set; }
        [DataMember]
        public Decimal Amount { get; set; }

    }
}
