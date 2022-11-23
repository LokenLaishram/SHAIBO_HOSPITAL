using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.PatientData
{
    [Serializable]
    public class PatientData : BaseData
    {
        [DataMember]
        public Int64 TotalSum { get; set; }
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string Descriptions { get; set; }
        [DataMember]
        public string OTpassnumber { get; set; }
        [DataMember]
        public DateTime LastVisitDate { get; set; }
        [DataMember]
        public double Height { get; set; }
        [DataMember]
        public double Weight { get; set; }
        [DataMember]
        public string BP { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string RegDNo { get; set; }
        [DataMember]
        public string ReferalDoctor { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public string BillNo { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public int DepositType { get; set; }
        [DataMember]
        public Int32 SalutationID { get; set; }
        [DataMember]
        public string DateofBirth { get; set; }
        [DataMember]
        public Int32 NoDays { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string EmgPatientName { get; set; }
        [DataMember]
        public string Patientshortdetail { get; set; }
        [DataMember]
        public string CaseName { get; set; }
        [DataMember]
        public string DoctorName { get; set; }
        [DataMember]
        public string PatRelatives { get; set; }
        [DataMember]
        public Int32 RelationshipID { get; set; }
        [DataMember]
        public Int32 LabServiceID { get; set; }
        [DataMember]
        public DateTime DOB { get; set; }
        [DataMember]
        public DateTime ConsultingDate { get; set; }
        [DataMember]
        public Int32 Age { get; set; }
        [DataMember]
        public string Agecount { get; set; }
        [DataMember]
        public Int32 Month { get; set; }
        [DataMember]
        public Int32 Days { get; set; }
        [DataMember]
        public Int32 GenderID { get; set; }
        [DataMember]
        public Int32 ReligionID { get; set; }
        [DataMember]
        public Int32 MaritalStatusID { get; set; }
        [DataMember]
        public Int32 BloodGrp { get; set; }
        [DataMember]
        public string IDmark { get; set; }
        [DataMember]
        public Int32 NationalityID { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public Int32 CountryID { get; set; }
        [DataMember]
        public Int32 StateID { get; set; }
        [DataMember]
        public Int32 DistrictID { get; set; }
        [DataMember]
        public Int32 Pin { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public string EmrgNo { get; set; }
        [DataMember]
        public string EmailID { get; set; }
        [DataMember]
        public Int32 Status { get; set; }
        [DataMember]
        public Int32 PatientType { get; set; }
        [DataMember]
        public Int32 ProfessionID { get; set; }
        [DataMember]
        public DateTime AssignedDate { get; set; }
        [DataMember]
        public string treatmentstatus { get; set; }
        [DataMember]
        public DateTime TodayDate { get; set; }
        [DataMember]
        public string BedDetails { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string GenderName { get; set; }
        [DataMember]
        public string Religion { get; set; }
        [DataMember]
        public string MaritalStatus { get; set; }
        [DataMember]
        public string Nationality { get; set; }
        [DataMember]
        public string Profession { get; set; }
        [DataMember]
        public String PatientTypeName { get; set; }
        [DataMember]
        public decimal DepositAmount { get; set; }
        [DataMember]
        public decimal AdustedAmount { get; set; }
        [DataMember]
        public decimal LastRefundedAmount { get; set; }
        [DataMember]
        public decimal BalanceAmount { get; set; }
        [DataMember]
        public Int64 DoctorID { get; set; }
        [DataMember]
        public Int32 PulseRate { get; set; }
        [DataMember]
        public Int32 DoctorTypeID { get; set; }
        [DataMember]
        public Int32 DepartmentID { get; set; }
        [DataMember]
        public Int32 Serial { get; set; }
        [DataMember]
        public DateTime AdmissionDate { get; set; }
        [DataMember]
        public decimal ServiceCharge { get; set; }
        [DataMember]
        public decimal TotalCharge { get; set; }
        [DataMember]
        public string OTNo { get; set; }
        [DataMember]
        public string OTRole { get; set; }
        [DataMember]
        public string Time { get; set; }
        [DataMember]
        public DateTime BookingDate { get; set; }
        [DataMember]
        public string dept { get; set; }
        [DataMember]
        public string FPtemplate { get; set; }
        [DataMember]
        public DateTime AddDate { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public string AadhaarNumber { get; set; }
        [DataMember]
        public Int32 TPAcompany { get; set; }
        [DataMember]
        public string ReferBy { get; set; }
        [DataMember]
        public string PatientCategory { get; set; }
        [DataMember]
        public string TPAcompanyName { get; set; }
        [DataMember]
        public int PrintregdCard { get; set; }
        [DataMember]
        public Int64 LastVisitDoctorID { get; set; }
        [DataMember]
        public string PatientDetailName { get; set; }
        [DataMember]
        public string BedDetail { get; set; }
        [DataMember]
        public string OPnumber { get; set; }
        [DataMember]
        public int MSBpc { get; set; }
        [DataMember]
        public Int32 IspackageCompany { get; set; }
        [DataMember]
        public Int32 userID { get; set; }
        [DataMember]
        public Int32 isExclude { get; set; }
        [DataMember]
        public Int32 ChkWardrecived { get; set; }
        [DataMember]
        public string PatientNumber { get; set; }
        [DataMember]
        public Int32 BillCategory { get; set; }
        
    }
    public class PatientAgeData : BaseData
    {
        [DataMember]
        public Int32 Age { get; set; }
        [DataMember]
        public string Agecount { get; set; }
        [DataMember]
        public Int32 Month { get; set; }
        [DataMember]
        public Int32 Days { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public DateTime DOB { get; set; }
        [DataMember]
        public Int32 AvailableIN { get; set; }

    }
    public class BookingData : BaseData
    {
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public Int32 Age { get; set; }
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string Time { get; set; }
        [DataMember]
        public Int32 RowNo { get; set; }
        [DataMember]
        public Int64 DoctorID { get; set; }
        [DataMember]
        public Int32 DoctorTypeID { get; set; }
        [DataMember]
        public Int32 DepartmentID { get; set; }
        [DataMember]
        public DateTime BookingDate { get; set; }


    }
    public class PatientDatatoExcel
    {
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public String PatientType { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string PatRelatives { get; set; }
        [DataMember]
        public string GenderName { get; set; }
        [DataMember]
        public string DOB { get; set; }
        [DataMember]
        public string Age { get; set; }
        [DataMember]
        public string Religion { get; set; }
        [DataMember]
        public string MaritalStatus { get; set; }
        [DataMember]
        public string IDmark { get; set; }
        [DataMember]
        public string Nationality { get; set; }
        [DataMember]
        public string Occupation { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public Int32 Pin { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public string EmailID { get; set; }
        [DataMember]
        public DateTime AddDate { get; set; }
        [DataMember]
        public string AddedBy { get; set; }


    }
    public class VisitHistorytoExcel
    {
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string DoctorName { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public DateTime AddDate { get; set; }
    }
    public class ConsultingshhettoExcel
    {

        [DataMember]
        public Int32 Serial { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public double Height { get; set; }
        [DataMember]
        public double Weight { get; set; }
        [DataMember]
        public string BP { get; set; }
        [DataMember]
        public Int32 PulseRate { get; set; }
    }
    public class IPDPatientEnqDatatoExcel
    {
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public DateTime AssignedDate { get; set; }
        [DataMember]
        public string BedDetails { get; set; }

    }
    public class IPDTreatmentStatusDatatoExcel
    {
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public double Height { get; set; }
        [DataMember]
        public double Weight { get; set; }
        [DataMember]
        public string BP { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public string treatmentstatus { get; set; }

    }
    public class OPDdata : BaseData
    {
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string PatientNumber { get; set; }
        [DataMember]
        public Int64 DoctorID { get; set; }
        [DataMember]
        public Int32 PatientTypeID { get; set; }
        [DataMember]
        public string OPnumber { get; set; }
    }
    public class PatientListDataTOeXCEL
    {
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public Int32 Age { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public DateTime BookingDate { get; set; }
        [DataMember]
        public string Time { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
    }
    public class PatientCaseHistoryData : BaseData
    {
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string CaseType { get; set; }
        [DataMember]
        public string CaseNo { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string Extension { get; set; }
        [DataMember]
        public string ContentType { get; set; }
        [DataMember]
        public byte[] FileData { get; set; }
        [DataMember]
        public Int64 FileSize { get; set; }
        [DataMember]
        public Int64 serviceID { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public Int64 FileAddedBy { get; set; }
        [DataMember]
        public DateTime FileAddedDate { get; set; }

    }
}