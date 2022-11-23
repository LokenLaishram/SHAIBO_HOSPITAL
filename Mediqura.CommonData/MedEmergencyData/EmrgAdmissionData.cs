using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedEmergencyData
{
    public class EmrgAdmissionData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string RegdNo { get; set; }
        [DataMember]
        public string EmrgNo { get; set; }
        [DataMember]
        public string EmergencyNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string PatientAddress { get; set; }
        [DataMember]
        public string GenderID { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public Int32 Agefrom { get; set; }
        [DataMember]
        public Int32 Ageto { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Room { get; set; }
        [DataMember]
        public Int64 DocID { get; set; }
        [DataMember]
        public int DeptID { get; set; }
        [DataMember]
        public string Cases { get; set; }
        [DataMember]
        public decimal AdmissionCharge { get; set; }
        [DataMember]
        public decimal Charges { get; set; }
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
        public int PaymentMode { get; set; }
        [DataMember]
        public decimal TotalAdmissionCharge { get; set; }
        [DataMember]
        public int DischargeStatus { get; set; }
        [DataMember]
        public string Discharge { get; set; }
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
        public int EmrgDoctor { get; set; }
        [DataMember]
        public string GenderName { get; set; }
        [DataMember]
        public Int32 GenID { get; set; }
        [DataMember]
        public string Agecount { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public Decimal ServiceCharge { get; set; }
        [DataMember]
        public Decimal NetServiceCharge { get; set; }
        [DataMember]
        public int ServiceID { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public int SerialID { get; set; }
        [DataMember]
        public int ServiceTypeID { get; set; }
        [DataMember]
        public decimal Tax { get; set; }
        [DataMember]
        public int ItemID { get; set; }
        [DataMember]
        public Int64 SubStockID { get; set; }
        [DataMember]
        public decimal TotalTax { get; set; }
        [DataMember]
        public Decimal NetLabServiceCharge { get; set; }
        [DataMember]
        public Decimal totLab { get; set; }
        [DataMember]
        public Decimal totEmrg { get; set; }
        [DataMember]
        public Int64 StockID { get; set; }
        [DataMember]
        public Decimal LabServiceCharge { get; set; }
        [DataMember]
        public Decimal NetPHRServiceCharge { get; set; }
        [DataMember]
        public Int32 SampleTypeID { get; set; }
        [DataMember]
        public string descriptions { get; set; }
        [DataMember]
        public Int32 Samplecode { get; set; }
        [DataMember]
        public Int64 EmrgID { get; set; }
        [DataMember]
        public Int32 sampleID { get; set; }
        [DataMember]
        public Int32 LabServiceID { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public string SampleType { get; set; }
        [DataMember]
        public Int32 TestCenterID { get; set; }
        [DataMember]
        public string TestCenter { get; set; }
        [DataMember]
        public Int32 UrgencyID { get; set; }
        [DataMember]
        public string Urgency { get; set; }
        [DataMember]
        public int StatusID { get; set; }
        [DataMember]
        public Int64 TakenBy { get; set; }
        [DataMember]
        public Decimal TotalBillAmount { get; set; }
        [DataMember]
        public Decimal BalanceAmount { get; set; }
        [DataMember]
        public Decimal TotalDiscount { get; set; }
        [DataMember]
        public Decimal TotalAdjustedAmount { get; set; }
        [DataMember]
        public string Chequenumber { get; set; }
        [DataMember]
        public string Invoicenumber { get; set; }
        [DataMember]
        public string BillNo { get; set; }
        [DataMember]
        public Decimal TotalBill { get; set; }
        [DataMember]
        public Decimal TotalPaidAmount { get; set; }
        [DataMember]
        public Decimal AdjustedAmount { get; set; }
        [DataMember]
        public Decimal DiscountedAmount { get; set; }
        [DataMember]
        public Decimal PaidAmount { get; set; }
        [DataMember]
        public Int16 Informehk { get; set; }
        [DataMember]
        public Int16 Informeward { get; set; }
        [DataMember]
        public Int32 SubGroupID { get; set; }
        [DataMember]
        public Int32 SourceID { get; set; }
        [DataMember]
        public Int64 ReferalID { get; set; }
        [DataMember]
        public Int32 IsExists { get; set; }
        [DataMember]
        public Int32 ServiceCategoryID { get; set; }
        [DataMember]
        public string InvNumber { get; set; }
        [DataMember]
        public string ServiceNumber { get; set; }
        [DataMember]
        public Int32 ResultOutput { get; set; }
        [DataMember]
        public string DoctorName { get; set; }
        [DataMember]
        public string PatRelatives { get; set; }
        [DataMember]
        public DateTime ServiceDate { get; set; }
        [DataMember]
        public string ServiceCategory { get; set; }
        [DataMember]
        public decimal TotalDuemanount { get; set; }
        [DataMember]
        public string OPnumber { get; set; }
        [DataMember]
        public int MsbPc { get; set; }
        [DataMember]
        public string patientCat { get; set; }
        [DataMember]
        public int ActualCategoryID { get; set; }
        [DataMember]
        public int isExclude { get; set; }
        [DataMember]
        public string extraDiscountXML { get; set; }
        [DataMember]
        public int isExtradiscount { get; set; }
        [DataMember]
        public Decimal TotalPayableAmount { get; set; }
        [DataMember]
        public byte[] QRImage { get; set; }
        [DataMember]
        public byte[] BarCodeImage { get; set; }
        [DataMember]
        public int isMsbPatient { get; set; }
        [DataMember]
        public int isMsbDoctor { get; set; }
        [DataMember]
        public Int32 userID { get; set; }
        [DataMember]
        public decimal DueAmount { get; set; }
        [DataMember]
        public string GroupNumber { get; set; }
        [DataMember]
        public int IsDeviceInitiated { get; set; }
        [DataMember]
        public int ChkPhrBillClear { get; set; }
    }
    public class IPfinalbillData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string GenderID { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Room { get; set; }
        [DataMember]
        public decimal Charges { get; set; }
        [DataMember]
        public string BedNo { get; set; }
        [DataMember]
        public int PaymentMode { get; set; }
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
        public int Doctor { get; set; }
        [DataMember]
        public string GenderName { get; set; }
        [DataMember]
        public string Agecount { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public Decimal ServiceCharge { get; set; }
        [DataMember]
        public Decimal NetServiceCharge { get; set; }
        [DataMember]
        public int ServiceID { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public int SerialID { get; set; }
        [DataMember]
        public int ServiceTypeID { get; set; }
        [DataMember]
        public decimal Tax { get; set; }
        [DataMember]
        public int StatusID { get; set; }
        [DataMember]
        public Int64 TakenBy { get; set; }
        [DataMember]
        public Decimal TotalBillAmount { get; set; }
        [DataMember]
        public Decimal BalanceAmount { get; set; }
        [DataMember]
        public Decimal TotalDiscount { get; set; }
        [DataMember]
        public Decimal TotalAdjustedAmount { get; set; }
        [DataMember]
        public string Chequenumber { get; set; }
        [DataMember]
        public string Invoicenumber { get; set; }
        [DataMember]
        public string BillNo { get; set; }
        [DataMember]
        public Decimal TotalBill { get; set; }
        [DataMember]
        public Decimal TotalPaidAmount { get; set; }
        [DataMember]
        public Decimal AdjustedAmount { get; set; }
        [DataMember]
        public Decimal DiscountedAmount { get; set; }
        [DataMember]
        public Decimal PaidAmount { get; set; }
        [DataMember]
        public Int32 ServiceCategoryID { get; set; }
        [DataMember]
        public string InvNumber { get; set; }
        [DataMember]
        public string ServiceNumber { get; set; }
        [DataMember]
        public Int32 ResultOutput { get; set; }
        [DataMember]
        public string DoctorName { get; set; }
        [DataMember]
        public string PatRelatives { get; set; }
        [DataMember]
        public DateTime ServiceDate { get; set; }
        [DataMember]
        public string ServiceCategory { get; set; }
        [DataMember]
        public decimal TotalDuemanount { get; set; }
        [DataMember]
        public int MsbPc { get; set; }
        [DataMember]
        public string patientCat { get; set; }
        [DataMember]
        public int ActualCategoryID { get; set; }
        [DataMember]
        public int isExclude { get; set; }
        [DataMember]
        public string extraDiscountXML { get; set; }
        [DataMember]
        public int isExtradiscount { get; set; }
        [DataMember]
        public Decimal TotalPayableAmount { get; set; }
        [DataMember]
        public byte[] QRImage { get; set; }
        [DataMember]
        public byte[] BarCodeImage { get; set; }
        [DataMember]
        public decimal DueAmount { get; set; }
        [DataMember]
        public int PatientCategory { get; set; }
        [DataMember]
        public int ChkPhrBillClear { get; set; }

    }
    public class EMRGServiceListDataTOeXCEL
    {
        [DataMember]
        public string EmrgNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public Decimal ServiceCharge { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public Decimal NetServiceCharge { get; set; }
        [DataMember]
        public Decimal TotalBillAmount { get; set; }
        [DataMember]
        public Decimal PaidAmount { get; set; }
    }
    public class IpbillDataToExcel
    {
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public Decimal ServiceCharge { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public Decimal NetServiceCharge { get; set; }
        [DataMember]
        public Decimal TotalBillAmount { get; set; }
        [DataMember]
        public Decimal PaidAmount { get; set; }
    }
    public class EMRGAdmissionListDataTOeXCEL
    {
        [DataMember]
        public string EmrgNo { get; set; }

        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string AdmissionDoctor { get; set; }
        //[DataMember]
        //public string Department { get; set; }
        //[DataMember]
        //public decimal AdmissionCharge { get; set; }
        [DataMember]
        public DateTime AdmissionDate { get; set; }
        //[DataMember]
        //public string Cases { get; set; }
    }
    public class EMRGAdmissionListReportDataTOeXCEL
    {
        [DataMember]
        public string EmrgNo { get; set; }

        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string GenderName { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public string AdmissionDoctor { get; set; }
        [DataMember]
        public string Discharge { get; set; }
        [DataMember]
        public DateTime AdmissionDate { get; set; }
        //[DataMember]
        //public string Cases { get; set; }
    }
    public class EMRDiscountListData
    {
        [DataMember]
        public Int64 ShareID { get; set; }
        [DataMember]
        public string DoctorName { get; set; }
        [DataMember]
        public decimal ShareAmount { get; set; }
        [DataMember]
        public decimal TotalBill { get; set; }
        [DataMember]
        public int disType { get; set; }
        [DataMember]
        public decimal disValue { get; set; }
        [DataMember]
        public decimal discountAmount { get; set; }
        [DataMember]
        public int DiscountStatus { get; set; }
    }
    public class IPDiscountListData
    {
        [DataMember]
        public Int64 ShareID { get; set; }
        [DataMember]
        public string DoctorName { get; set; }
        [DataMember]
        public decimal ShareAmount { get; set; }
        [DataMember]
        public decimal TotalBill { get; set; }
        [DataMember]
        public int disType { get; set; }
        [DataMember]
        public decimal disValue { get; set; }
        [DataMember]
        public decimal discountAmount { get; set; }
        [DataMember]
        public int DiscountStatus { get; set; }
    }
    public class BillAdjustmentData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 ServiceID { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public int BillCategory { get; set; }
        [DataMember]
        public string PatientDetail { get; set; }
        [DataMember]
        public string PatientNumber { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public string ServiceNumber { get; set; }
        [DataMember]
        public int PatientCategory { get; set; }
        [DataMember]
        public int ActualCategoryID { get; set; }
        [DataMember]
        public Decimal ServiceCharge { get; set; }
        [DataMember]
        public int PaymentMode { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public decimal NetServiceCharge { get; set; }
        [DataMember]
        public DateTime ServiceDate { get; set; }
        [DataMember]
        public DateTime BedLastDate { get; set; }
        [DataMember]
        public int ServiceCategoryID { get; set; }
        [DataMember]
        public string ServiceCategory { get; set; }
        [DataMember]
        public int IsSubheading { get; set; }
        [DataMember]
        public Int64 DoctorID { get; set; }
        [DataMember]
        public Decimal BalanceAmount { get; set; }
        [DataMember]
        public Decimal TotalAmount { get; set; }
        [DataMember]
        public Decimal TotalBalance { get; set; }
        [DataMember]
        public Decimal TotalPayable { get; set; }
        [DataMember]
        public DateTime AdmissionDate { get; set; }
        [DataMember]
        public string Doctor { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string ServiceStartDate { get; set; }
        [DataMember]
        public string ServiceEndDate { get; set; }
        [DataMember]
        public string InvNumber { get; set; }
        [DataMember]
        public string GroupNumber { get; set; }
    }
    public class PHREmrgFinalData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string EmrgNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string ReqNo { get; set; }
        [DataMember]
        public int SubmitType { get; set; }
        [DataMember]
        public string PatientAddress { get; set; }
        [DataMember]
        public string PatRelatives { get; set; }
        [DataMember]
        public string Agecount { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime AdmissionDate { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string AdmissionDoctor { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string BedDetails { get; set; }
        [DataMember]
        public Decimal NoUnit { get; set; }
        [DataMember]
        public Decimal Quantity { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public Decimal MRPperUnit { get; set; }
        [DataMember]
        public Decimal MRPperQty { get; set; }
        [DataMember]
        public Decimal NetAmount { get; set; }
        [DataMember]
        public int ItemID { get; set; }
        [DataMember]
        public Int64 SubStockID { get; set; }
        [DataMember]
        public Decimal TotalBillAmount { get; set; }
        [DataMember]
        public Decimal BalanceAmount { get; set; }
        [DataMember]
        public Decimal TotalDiscount { get; set; }
        [DataMember]
        public Decimal TotalAdjustedAmount { get; set; }
        [DataMember]
        public string Chequenumber { get; set; }
        [DataMember]
        public string Invoicenumber { get; set; }
        [DataMember]
        public string BillNo { get; set; }
        [DataMember]
        public Decimal TotalBill { get; set; }
        [DataMember]
        public Decimal TotalPaidAmount { get; set; }
        [DataMember]
        public Decimal AdjustedAmount { get; set; }
        [DataMember]
        public Decimal DiscountedAmount { get; set; }
        [DataMember]
        public Decimal PaidAmount { get; set; }
        [DataMember]
        public Decimal TotalPayableAmount { get; set; }
        [DataMember]
        public byte[] QRImage { get; set; }
        [DataMember]
        public byte[] BarCodeImage { get; set; }
        [DataMember]
        public string EmgDrgIssueNo { get; set; }
        [DataMember]
        public Decimal TotalDuemanount { get; set; }
        [DataMember]
        public Decimal DueAmount { get; set; }
        [DataMember]
        public Int64 ResponsiblePerson { get; set; }
        [DataMember]
        public Decimal TotalReturnAmnt { get; set; }

    }
    public class PHRIPFinalData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string ReqNo { get; set; }
        [DataMember]
        public int SubmitType { get; set; }
        [DataMember]
        public string PatientAddress { get; set; }
        [DataMember]
        public string PatRelatives { get; set; }
        [DataMember]
        public string Agecount { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime AdmissionDate { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string AdmissionDoctor { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string BedDetails { get; set; }
        [DataMember]
        public Decimal NoUnit { get; set; }
        [DataMember]
        public Decimal Quantity { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public Decimal MRPperUnit { get; set; }
        [DataMember]
        public Decimal MRPperQty { get; set; }
        [DataMember]
        public Decimal NetAmount { get; set; }
        [DataMember]
        public int ItemID { get; set; }
        [DataMember]
        public Int64 SubStockID { get; set; }
        [DataMember]
        public Decimal TotalBillAmount { get; set; }
        [DataMember]
        public Decimal BalanceAmount { get; set; }
        [DataMember]
        public Decimal TotalDiscount { get; set; }
        [DataMember]
        public Decimal TotalAdjustedAmount { get; set; }
        [DataMember]
        public string Chequenumber { get; set; }
        [DataMember]
        public string Invoicenumber { get; set; }
        [DataMember]
        public string BillNo { get; set; }
        [DataMember]
        public Decimal TotalBill { get; set; }
        [DataMember]
        public Decimal TotalPaidAmount { get; set; }
        [DataMember]
        public Decimal AdjustedAmount { get; set; }
        [DataMember]
        public Decimal DiscountedAmount { get; set; }
        [DataMember]
        public Decimal PaidAmount { get; set; }
        [DataMember]
        public Decimal TotalPayableAmount { get; set; }
        [DataMember]
        public byte[] QRImage { get; set; }
        [DataMember]
        public byte[] BarCodeImage { get; set; }
        [DataMember]
        public string IPDrgIssueNo { get; set; }
        [DataMember]
        public Decimal TotalDuemanount { get; set; }
        [DataMember]
        public Decimal DueAmount { get; set; }
        [DataMember]
        public Int64 ResponsiblePerson { get; set; }
        [DataMember]
        public Decimal TotalReturnAmnt { get; set; }


    }
}
