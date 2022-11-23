using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.AdmissionData
{
    public class AdmissionData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 AdmissionID { get; set; }
        [DataMember]
        public string RegdNo { get; set; }
        [DataMember]
        public string AdmissionNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string EmrgNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string PatientAddress { get; set; }
        [DataMember]
        public string GenderID { get; set; }
        [DataMember]
        public int Gender { get; set; }
        [DataMember]
        public int AgeFrom { get; set; }
        [DataMember]
        public int AgeTo { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Room { get; set; }
        [DataMember]
        public Int64 DocID { get; set; }
        [DataMember]
        public int DeptID { get; set; }
        [DataMember]
        public Int64 AdmissionDoc_II { get; set; }
        [DataMember]
        public string AdmissionDoc2nd { get; set; }
        [DataMember]
        public string AdmissionDoc3rd { get; set; }
        [DataMember]
        public Int64 AdmissionDoc_III { get; set; }
        [DataMember]
        public string Cases { get; set; }
        [DataMember]
        public Decimal AdmissionCharge { get; set; }
        [DataMember]
        public Decimal Charges { get; set; }
        [DataMember]
        public Decimal LastBedCharges { get; set; }
        [DataMember]
        public string BedNo { get; set; }
        [DataMember]
        public int BlockID { get; set; }
        [DataMember]
        public int BedID { get; set; }
        [DataMember]
        public int FloorID { get; set; }
        [DataMember]
        public int WardID { get; set; }
        [DataMember]
        public int LastBedId { get; set; }
        [DataMember]
        public int PaymentMode { get; set; }
        [DataMember]
        public decimal TotalAdmissionCharge { get; set; }
        [DataMember]
        public int DischargeStatus { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime AssignedDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        [DataMember]
        public DateTime AdmissionDate { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string AdmissionDoctor { get; set; }
        [DataMember]
        public string Block { get; set; }
        [DataMember]
        public string Floor1 { get; set; }
        [DataMember]
        public string Ward { get; set; }
        [DataMember]
        public int NoDays { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string BedTransferXML { get; set; }
        [DataMember]
        public string OccupiedBy { get; set; }
        [DataMember]
        public string BedDetails { get; set; }
        [DataMember]
        public int ReferalDoctor { get; set; }
        [DataMember]
        public String ReferalDoctorName { get; set; }
        [DataMember]
        public int BedStatus { get; set; }
        [DataMember]
        public String BedetailStatus { get; set; }
        [DataMember]
        public string Admdate { get; set; }
        [DataMember]
        public int Informeward { get; set; }
        [DataMember]
        public int Informehk { get; set; }
        [DataMember]
        public int Patient_Active { get; set; }
        [DataMember]
        public string OccupyBy { get; set; }
        [DataMember]
        public int IsReleased { get; set; }
        [DataMember]
        public int hrs { get; set; }
        [DataMember]
        public decimal BalanceAmount { get; set; }
        [DataMember]
        public decimal CreditLimit { get; set; }
        [DataMember]
        public int IsAdmittedToWard { get; set; }
        [DataMember]
        public string Agecount { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public Int32 PatientType { get; set; }
        [DataMember]
        public Int32 SourceID { get; set; }
        [DataMember]
        public Int64 ReferalID { get; set; }
    }
    public class IPData
    {
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string IPPatientName { get; set; }
        [DataMember]
        public Int32 PatientType { get; set; }

    }
    public class AdmissionListDataTOeXCEL
    {
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string AdmissionDoctor { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public Decimal AdmissionCharge { get; set; }
        [DataMember]
        public string Admdate { get; set; }
        [DataMember]
        public string Cases { get; set; }
    }
    public class AdmissionReportDataTOeXCEL
    {
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string UHID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Age { get; set; }
        [DataMember]
        public string GenderID { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public string AdmissionDoctor { get; set; }
        [DataMember]
        public string Admdate { get; set; }
     }
    public class BedTransferListDataTOeXCEL
    {
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string BedDetails { get; set; }
        [DataMember]
        public DateTime AssignedDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        [DataMember]
        public int NoDays { get; set; }
    }
    public class BedOccupancyListDataTOeXCEL
    {
        [DataMember]
        public string Block { get; set; }
        [DataMember]
        public string Floor1 { get; set; }
        [DataMember]
        public string Ward { get; set; }
        [DataMember]
        public string Room { get; set; }
        [DataMember]
        public string BedNo { get; set; }
        [DataMember]
        public decimal Charges { get; set; }
        [DataMember]
        public string OccupiedBy { get; set; }
    }
}
