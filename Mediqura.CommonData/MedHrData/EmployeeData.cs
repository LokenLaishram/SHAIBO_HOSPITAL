using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedHrData
{
    public class EmployeeData : BaseData
    {
        [DataMember]
        public Int64 EmployeeID { get; set; }
        [DataMember]
        public Int64 UserLoginId { get; set; }
        [DataMember]
        public Int64 DependentID { get; set; }
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public int EmployeeTypeID { get; set; }
        [DataMember]
        public int EmployeeCatgeroyID { get; set; }
        [DataMember]
        public int StaffTypeID { get; set; }
        [DataMember]
        public string EmployeeCode { get; set; }
        [DataMember]
        public string AliasName { get; set; }
        [DataMember]
        public string EmpName1 { get; set; }
        [DataMember]
        public string SpouseName { get; set; }
        [DataMember]
        public string DateofBirth { get; set; }
        [DataMember]
        public string GuardianName { get; set; }
        [DataMember]
        public int CasteID { get; set; }
        [DataMember]
        public int StaffCategoryID { get; set; }
        [DataMember]
        public string WorkExp { get; set; }
        [DataMember]
        public Byte[] ImageFile { get; set; }
        [DataMember]
        public byte[] SignatureFile { get; set; }
        [DataMember]
        public string DependentName { get; set; }
        [DataMember]
        public string EmployeeNo { get; set; }
        [DataMember]
        public Int32 Relation { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public int DesignationID { get; set; }
        [DataMember]
        public int CurrentCountryID { get; set; }
        [DataMember]
        public int CurrentStateID { get; set; }
        [DataMember]
        public int CurrentPIN { get; set; }
        [DataMember]
        public string CurrentLandMark { get; set; }
        [DataMember]
        public int CurrentDistrictID { get; set; }
        [DataMember]
        public int SexID { get; set; }
        [DataMember]
        public DateTime DOB { get; set; }
        [DataMember]
        public string MobileNo { get; set; }
        [DataMember]
        public string PhoneNo { get; set; }
        [DataMember]
        public string DigitalSignatureLocation { get; set; }
        [DataMember]
        public string EmployeePhotoLocation { get; set; }
        [DataMember]
        public string CurrentAddress { get; set; }
        [DataMember]
        public string PermAddress { get; set; }
        [DataMember]
        public int PermCountryID { get; set; }
        [DataMember]
        public int PermStateID { get; set; }
        [DataMember]
        public int PermDistrictID { get; set; }
        [DataMember]
        public int PermPIN { get; set; }
        [DataMember]
        public string PermLandMark { get; set; }
        [DataMember]
        public int CastID { get; set; }
        [DataMember]
        public int DepartmentID { get; set; }
        [DataMember]
        public int SalutationID { get; set; }
        [DataMember]
        public int ReligionID { get; set; }
        [DataMember]
        public string EmailID { get; set; }
        [DataMember]
        public int MaritalStatusID { get; set; }
        [DataMember]
        public string SexName { get; set; }
        [DataMember]
        public string CurrCountry { get; set; }
        [DataMember]
        public string PerCountry { get; set; }
        [DataMember]
        public string CurrState { get; set; }
        [DataMember]
        public string PerState { get; set; }
        [DataMember]
        public string CurrDistrict { get; set; }
        [DataMember]
        public string PerDistrict { get; set; }
        [DataMember]
        public string Religion { get; set; }
        [DataMember]
        public string CastName { get; set; }
        [DataMember]
        public string EmployeeType { get; set; }
        [DataMember]
        public string MaritalStatus { get; set; }
        [DataMember]
        public string Salutation { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public string Designation { get; set; }
        [DataMember]
        public string Qualification { get; set; }
        [DataMember]
        public string Experience { get; set; }
        [DataMember]
        public DateTime DOJ { get; set; }
        [DataMember]
        public string IDmarks { get; set; }
        [DataMember]
        public int BloodGroupID { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public int NationalityID { get; set; }
        [DataMember]
        public DateTime AppDt { get; set; }
        [DataMember]
        public DateTime IssueDt { get; set; }
        [DataMember]
        public DateTime ValidDt { get; set; }
        [DataMember]
        public DateTime SurDt { get; set; }
        [DataMember]
        public string StaffCategory { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public Double DiscountIPD { get; set; }
        [DataMember]
        public Double DiscountOPD { get; set; }
        [DataMember]
        public Double DiscountOPDLab { get; set; }
        [DataMember]
        public int EmpGradeID { get; set; }
        [DataMember]
        public string EmpGrade { get; set; }
        [DataMember]
        public string Nationality { get; set; }
        [DataMember]
        public int StatusID { get; set; }
        [DataMember]
        public Int32 IsPhotoUploaded { get; set; }
        [DataMember]
        public Int32 IsOutsourcedTest { get; set; }
        [DataMember]
        public string EmpPhotoPath { get; set; }
        [DataMember]
        public Int32 ExcludeMsb { get; set; }
        [DataMember]
        public Int32 IsDigiSignUploaded { get; set; }
        [DataMember]
        public string DateOfJoining { get; set; }
        [DataMember]
        public string AadhaarNo { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string FPData { get; set; }
        [DataMember]
        public Decimal DiscountedAmount { get; set; }
        [DataMember]
        public string IPemrgNo { get; set; }
        [DataMember]
        public string BillNo { get; set; }
    }
    public class GenStockEmployeeData : BaseData
    {
        [DataMember]
        public Int64 EmployeeID { get; set; }
        [DataMember]
        public int DepartmentID { get; set; }
        [DataMember]
        public int GenSubStockID { get; set; }
        [DataMember]
        public int GenItemRequestEnable { get; set; }
        [DataMember]
        public int GenItemApproveEnable { get; set; }
        [DataMember]
        public int GenItemHandoverEnable { get; set; }
        [DataMember]
        public int GenItemVerifyEnable { get; set; }
    }
    public class MedStockEmployeeData : BaseData
    {
        [DataMember]
        public Int64 EmployeeID { get; set; }
        [DataMember]
        public int DepartmentID { get; set; }
        [DataMember]
        public int MedSubStockID { get; set; }
        [DataMember]
        public int MedItemRequestEnable { get; set; }
        [DataMember]
        public int MedItemApproveEnable { get; set; }
        [DataMember]
        public int MedItemHandoverEnable { get; set; }
        [DataMember]
        public int MedItemVerifyEnable { get; set; }
    }
    public class EmployeeDatatoExcel
    {
        [DataMember]
        public string EmployeeNo { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public string AliasName { get; set; }
        [DataMember]
        public string AadhaarNo { get; set; }
        [DataMember]
        public string SpouseName { get; set; }
        [DataMember]
        public string GuardianName { get; set; }
        [DataMember]
        public string Qualification { get; set; }
        [DataMember]
        public string StaffCategory { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public string Designation { get; set; }
        [DataMember]
        public string WorkExp { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public string DateofBirth { get; set; }
        [DataMember]
        public string DateOfJoining { get; set; }
        [DataMember]
        public string CastName { get; set; }
        [DataMember]
        public string CurrentAddress { get; set; }
        [DataMember]
        public string CurrDistrict { get; set; }
        [DataMember]
        public string CurrState { get; set; }
        [DataMember]
        public string CurrCountry { get; set; }
        [DataMember]
        public int CurrentPIN { get; set; }
        [DataMember]
        public string PermAddress { get; set; }
        [DataMember]
        public string PerDistrict { get; set; }
        [DataMember]
        public string PerState { get; set; }
        [DataMember]
        public string PerCountry { get; set; }
        [DataMember]
        public int PermPIN { get; set; }
        [DataMember]
        public string EmailID { get; set; }
        [DataMember]
        public string EmployeeType { get; set; }
        [DataMember]
        public string MaritalStatus { get; set; }
        [DataMember]
        public string MobileNo { get; set; }
        [DataMember]
        public string PhoneNo { get; set; }
        [DataMember]
        public string EmpGrade { get; set; }
        [DataMember]
        public string Description { get; set; }

    }
}
