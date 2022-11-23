using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedBillData
{
    public class DepositData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public int SerialID { get; set; }
        [DataMember]
        public Int64 DepositID { get; set; }
        [DataMember]
        public String DepositNo { get; set; }
        [DataMember]
        public String OTpassnumber { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public String IPNo { get; set; }
        [DataMember]
        public int Deposittype { get; set; }
        [DataMember]
        public String EmrgNo { get; set; }
        [DataMember]
        public String DepositParticulars { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
        [DataMember]
        public decimal TotalAmount { get; set; }
        [DataMember]
        public decimal AdustedAmount { get; set; }
        [DataMember]
        public decimal RefundAmount { get; set; }
        [DataMember]
        public decimal BalanceAmount { get; set; }
        [DataMember]
        public decimal DepositAmount { get; set; }
        [DataMember]
        public String XMLData { get; set; }
        [DataMember]
        public Byte[] UHIDtoBarcode { get; set; }
        [DataMember]
        public String PatientName { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public String Address { get; set; }
        [DataMember]
        public String Remarks { get; set; }
    }
    public class DepositDataTOeXCEL
    {
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public String PatientName { get; set; }
        [DataMember]
        public String Address { get; set; }
        [DataMember]
        public decimal DepositAmount { get; set; }
    }
    public class RefundData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public int SerialID { get; set; }
        [DataMember]
        public Int64 RefundID { get; set; }
        [DataMember]
        public String RefundNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public decimal TotalDepositedAmount { get; set; }
        [DataMember]
        public decimal RefundAmount { get; set; }
        [DataMember]
        public decimal LastRefundedAmount { get; set; }
        [DataMember]
        public decimal TotalBalanceAmount { get; set; }
        [DataMember]
        public decimal TotalRefundAmount { get; set; }
        [DataMember]
        public decimal DueAmount { get; set; }
        [DataMember]
        public String XMLData { get; set; }
        [DataMember]
        public Byte[] UHIDtoBarcode { get; set; }
        [DataMember]
        public String PatientName { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public String Address { get; set; }
        [DataMember]
        public String Remarks { get; set; }
        [DataMember]
        public String BillNo { get; set; }

    }
    public class RefundDatatoExcel
    {
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public String PatientName { get; set; }
        [DataMember]
        public String Address { get; set; }
        [DataMember]
        public decimal RefundAmount { get; set; }
    }
    public class BankDetail
    {
        [DataMember]
        public int BankID { get; set; }
        [DataMember]
        public int PaymodeID { get; set; }
        [DataMember]
        public String BankName { get; set; }
    }
    public class DoctorPayoutData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 BillID { get; set; }
        [DataMember]
        public String BillNo { get; set; }
        [DataMember]
        public String PaymentNumber { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }       
        [DataMember]
        public String PatientName { get; set; }       
        [DataMember]
        public String PatAddress { get; set; }
        
        [DataMember]
        public decimal TotalAmount { get; set; }        
        [DataMember]
        public decimal DiscountAmnt { get; set; }
        [DataMember]
        public decimal NetAmount { get; set; }
        [DataMember]
        public decimal PC { get; set; }
        [DataMember]
        public Int64 ReferralID { get; set; }
        [DataMember]
        public string ReferralName { get; set; }
        [DataMember]
        public decimal ReferralPC { get; set; } 
        [DataMember]
        public decimal RunnerPC { get; set; }
        [DataMember]
        public decimal PayableAmount { get; set; }
        [DataMember]
        public decimal ReferralPayable { get; set; }
        [DataMember]
        public decimal RunnerPayable { get; set; }
        [DataMember]
        public decimal PaidAmount { get; set; }
        [DataMember]
        public decimal DueAmount { get; set; }

        //=================================//
        [DataMember]
        public decimal GdTotalBillAmount { get; set; }
        [DataMember]
        public decimal GdTotalDiscount { get; set; }
        [DataMember]
        public decimal GdNetAmount { get; set; }
        [DataMember]
        public decimal GdReferralPayable { get; set; }
        [DataMember]
        public decimal GdRunnerPayable { get; set; }
        [DataMember]
        public decimal GdRefDiscount { get; set; }
        [DataMember]
        public decimal GdPaid { get; set; }

        //===============================//
        [DataMember]
        public decimal TotalPayable { get; set; }
        [DataMember]
        public decimal TotalPaidamount { get; set; }       
        [DataMember]
        public string DueDateFrom { get; set; }
        [DataMember]
        public string DueDateTo { get; set; }
       
        [DataMember]
        public string Doctor { get; set; }
        
        [DataMember]
        public String ServiceName { get; set; }
        [DataMember]
        public decimal ConsultantShare { get; set; }
        [DataMember]
        public decimal HospitalShare { get; set; }       
        [DataMember]
        public decimal TotalPredDiscount { get; set; }
        [DataMember]
        public decimal TotalPostDiscount { get; set; }
       
        [DataMember]
        public int PCType { get; set; }
        [DataMember]
        public int PServiceType { get; set; }
        [DataMember]
        public int ServiceID { get; set; }
       
        [DataMember]
        public int Subheading { get; set; }
        
        [DataMember]
        public Int64 DoctorID { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public int PaymentStatus { get; set; }
        [DataMember]
        public int ServiceCategory { get; set; }
        [DataMember]
        public int PayableCategory { get; set; }
        [DataMember]
        public string Remarks { get; set; }       
        [DataMember]
        public string PaymentCatgory { get; set; }
        [DataMember]
        public DateTime PaidOn { get; set; }
        [DataMember]
        public string Otnumber { get; set; }
        [DataMember]
        public int AdjustementType { get; set; }       
        [DataMember]
        public decimal ServiceCharge { get; set; }
    }
    public class DoctorPayoutDataToExcel 
    {
        [DataMember]
        public String BillNo { get; set; }       
        [DataMember]
        public String PatientName { get; set; }
        [DataMember]
        public String PatAddress { get; set; }
        [DataMember]
        public String TestName { get; set; }
        [DataMember]
        public String TotalAmount { get; set; }
        [DataMember]
        public String DiscountAmnt { get; set; }
        [DataMember]
        public String NetAmount { get; set; }       
        [DataMember]
        public String ReferralName { get; set; }
        [DataMember]
        public String ReferralPC { get; set; }       
        [DataMember]
        public String ReferralPayable { get; set; }
        [DataMember]
        public String RunnerPC { get; set; }
        [DataMember]
        public String RunnerPayable { get; set; }       
    }
    public class OPDbillingData : BaseData
    {
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public String PatientName { get; set; }
        [DataMember]
        public String Gender { get; set; }
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public int DoctorTypeID { get; set; }
        [DataMember]
        public int SerialID { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public Decimal ServiceCharge { get; set; }
        [DataMember]
        public Decimal NetServiceCharge { get; set; }
        [DataMember]
        public int ServiceID { get; set; }
        [DataMember]
        public String ServiceName { get; set; }
        [DataMember]
        public Decimal TotalBill { get; set; }
        [DataMember]
        public Decimal TotalBillAmount { get; set; }
        [DataMember]
        public Decimal AdjustedAmount { get; set; }
        [DataMember]
        public Decimal DiscountedAmount { get; set; }
        [DataMember]
        public Decimal PaidAmount { get; set; }
        [DataMember]
        public int PaymentMode { get; set; }
        [DataMember]
        public Int64 DiscountByID { get; set; }
        [DataMember]
        public String ChequeUTRnumber { get; set; }
        [DataMember]
        public String InvoiceNumber { get; set; }
        [DataMember]
        public String Remarks { get; set; }
        [DataMember]
        public Decimal BalanceAmount { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public decimal DepositAmount { get; set; }
        [DataMember]
        public Int64 BillID { get; set; }
        [DataMember]
        public String BillNo { get; set; }
        [DataMember]
        public String DepositNo { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
        [DataMember]
        public decimal TotalDiscountAmount { get; set; }
        [DataMember]
        public decimal TotalAdjustedAmount { get; set; }
        [DataMember]
        public decimal TotalPaidAmount { get; set; }
        [DataMember]
        public Decimal TotalDiscountedAmount { get; set; }
        [DataMember]
        public string DeptName { get; set; }
        [DataMember]
        public string DocName { get; set; }
        [DataMember]
        public int DeptID { get; set; }
        [DataMember]
        public Int64 DocID { get; set; }
        [DataMember]
        public int ServiceTypeID { get; set; }
        [DataMember]
        public string ServiceType { get; set; }
        [DataMember]
        public Int64 CollectedByID { get; set; }
        [DataMember]
        public string Ages { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public Int32 SubGroupID { get; set; }
        [DataMember]
        public Int32 NoDays { get; set; }
        [DataMember]
        public Int32 PrintregdCard { get; set; }
        [DataMember]
        public Int32 IsDefaultService { get; set; }
        public int isDis { get; set; }
        [DataMember]
        public int DisType { get; set; }
        [DataMember]
        public decimal DisValue { get; set; }
        [DataMember]
        public decimal DisAmount { get; set; }
        [DataMember]
        public string Barcode { get; set; }
        [DataMember]
        public int patientType { get; set; }
        [DataMember]
        public int isExtraDiscount { get; set; }
        [DataMember]
        public String extraDiscountXML { get; set; }
        [DataMember]
        public int PatientCat { get; set; }
        [DataMember]
        public int isMsbPatient { get; set; }
        [DataMember]
        public int isMsbDoctor { get; set; }
        [DataMember]
        public int MsbPc { get; set; }
        [DataMember]
        public int IsVerified { get; set; }
        [DataMember]
        public Int64 VerifyBy { get; set; }
        [DataMember]
        public Int32 VerifyID { get; set; }
        [DataMember]
        public string VerifyByName { get; set; }
        [DataMember]
        public Int32 SourceID { get; set; }
        [DataMember]
        public Int64 ReferalID { get; set; }
        [DataMember]
        public string ReferalName { get; set; }
        [DataMember]
        public decimal RunnerTax { get; set; }
        [DataMember]
        public decimal RunnerTaxAmount { get; set; }
        [DataMember]
        public decimal TotalRunnerAmt { get; set; }
    }
    public class LabBillingData : BaseData
    {
        [DataMember]
        public String RefNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public String PatientName { get; set; }
        [DataMember]
        public String AdjustmentNo { get; set; }
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 AdjustmentID { get; set; }
        [DataMember]
        public Int64 StockID { get; set; }
        [DataMember]
        public Int64 SubStockID { get; set; }
        [DataMember]
        public int PatientType { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public int GenderID { get; set; }
        [DataMember]
        public string AgeCount { get; set; }
        [DataMember]
        public int isDis { get; set; }
        [DataMember]
        public int DisType { get; set; }
        [DataMember]
        public decimal DisValue { get; set; }
        [DataMember]
        public decimal DisAmount { get; set; }
        [DataMember]
        public String PatientAddress { get; set; }
        [DataMember]
        public String ContactNo { get; set; }
        [DataMember]
        public String ReferredDoctor { get; set; }
        [DataMember]
        public int ReferredDoctorID { get; set; }
        [DataMember]
        public int SerialID { get; set; }
        [DataMember]
        public int Status { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public Decimal LabServiceCharge { get; set; }
        [DataMember]
        public Decimal TotalRefundable { get; set; }
        [DataMember]
        public Decimal Commission { get; set; }
        [DataMember]
        public int TestCenterID { get; set; }
        [DataMember]
        public string TestCenter { get; set; }
        [DataMember]
        public int UrgencyID { get; set; }
        [DataMember]
        public int SubGroupID { get; set; }
        [DataMember]
        public int ServiceTypeID { get; set; }
        [DataMember]
        public string Urgency { get; set; }
        [DataMember]
        public Decimal NetLabServiceCharge { get; set; }
        [DataMember]
        public int DoctorTypeID { get; set; }
        [DataMember]
        public int LabServiceID { get; set; }
        [DataMember]
        public String TestName { get; set; }
        [DataMember]
        public String TestNameList { get; set; }
        [DataMember]
        public Decimal TotalBillAmount { get; set; }
        [DataMember]
        public Decimal AdjustedAmount { get; set; }
        [DataMember]
        public Decimal DiscountedAmount { get; set; }
        [DataMember]
        public Decimal PaidAmount { get; set; }
        [DataMember]
        public Decimal DueAmount { get; set; }
        [DataMember]
        public int RunnerID { get; set; }
        [DataMember]
        public string RunnerName { get; set; }
        [DataMember]
        public int PaymentMode { get; set; }
        [DataMember]
        public String BankName { get; set; }
        [DataMember]
        public String RefundedBy { get; set; }
        [DataMember]
        public String Address { get; set; }
        [DataMember]
        public String CardNo { get; set; }
        [DataMember]
        public String INvoiceNo { get; set; }
        [DataMember]
        public String CheaqueNo { get; set; }
        [DataMember]
        public String AccountNo { get; set; }
        [DataMember]
        public String Remarks { get; set; }
        [DataMember]
        public Int64 DiscountByID { get; set; }
        [DataMember]
        public Decimal BalanceAmount { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public decimal DepositAmount { get; set; }
        [DataMember]
        public Int64 BillID { get; set; }
        [DataMember]
        public Int64 DeptID { get; set; }
        [DataMember]
        public Int64 DocID { get; set; }
        [DataMember]
        public String BillNo { get; set; }
        [DataMember]
        public Decimal TotalDiscountedAmount { get; set; }
        [DataMember]
        public Decimal TotalPaidAmount { get; set; }
        [DataMember]
        public Decimal TotalBill { get; set; }
        [DataMember]
        public Decimal TotalAdjustedAmount { get; set; }
        [DataMember]
        public Decimal TotalDueamount { get; set; }
        [DataMember]
        public Decimal Amount { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public Decimal ServiceCharge { get; set; }
        [DataMember]
        public int ItemID { get; set; }
        [DataMember]
        public Decimal Charge { get; set; }
        [DataMember]
        public Decimal NetCharges { get; set; }
        [DataMember]
        public string DocName { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public Decimal TotalMRP { get; set; }
        [DataMember]
        public Int64 CollectedByID { get; set; }
        [DataMember]
        public Decimal Tax { get; set; }
        [DataMember]
        public string InvestigationNo { get; set; }
        [DataMember]
        public string OPnumber { get; set; }
        [DataMember]
        public Int32 PackageID { get; set; }
        [DataMember]
        public Int32 ServiceCategoryID { get; set; }
        [DataMember]
        public Int32 IspackageCompany { get; set; }
        [DataMember]
        public int isExtraDiscount { get; set; }
        [DataMember]
        public String extraDiscountXML { get; set; }
        [DataMember]
        public int PatientCat { get; set; }
        [DataMember]
        public String IPnumber { get; set; }
        [DataMember]
        public int IsExists { get; set; }
        [DataMember]
        public int isExclude { get; set; }
        [DataMember]
        public int isMsbPatient { get; set; }
        [DataMember]
        public int isMsbDoctor { get; set; }
        [DataMember]
        public int MsbPc { get; set; }
        [DataMember]
        public int IsVerified { get; set; }
        [DataMember]
        public Int64 VerifyBy { get; set; }
        [DataMember]
        public Int64 VerifyID { get; set; }
        [DataMember]
        public string VerifyByName { get; set; }
        [DataMember]
        public Int32 SourceID { get; set; }
        [DataMember]
        public string SourceName { get; set; }
        [DataMember]
        public Int64 ReferalID { get; set; }
        [DataMember]
        public string ReferalName { get; set; }
        [DataMember]
        public decimal RunnerTax { get; set; }
        [DataMember]
        public decimal RunnerTaxAmount { get; set; }
        [DataMember]
        public decimal TotalRunnerAmt { get; set; }
    }
    public class PHRbillingData : BaseData
    {
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public String PatientName { get; set; }
        [DataMember]
        public String AdjustmentNo { get; set; }
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 AdjustmentID { get; set; }
        [DataMember]
        public Int64 StockID { get; set; }
        [DataMember]
        public Int64 SubStockID { get; set; }
        [DataMember]
        public int PatientType { get; set; }
        [DataMember]
        public String PatientAddress { get; set; }
        [DataMember]
        public String ContactNo { get; set; }
        [DataMember]
        public String ReferredDoctor { get; set; }
        [DataMember]
        public int ReferredDoctorID { get; set; }
        [DataMember]
        public int SerialID { get; set; }
        [DataMember]
        public int Status { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public Decimal LabServiceCharge { get; set; }
        [DataMember]
        public Decimal TotalRefundable { get; set; }
        [DataMember]
        public Decimal Commission { get; set; }
        [DataMember]
        public int TestCenterID { get; set; }
        [DataMember]
        public string TestCenter { get; set; }
        [DataMember]
        public int UrgencyID { get; set; }
        [DataMember]
        public string Urgency { get; set; }
        [DataMember]
        public Decimal NetLabServiceCharge { get; set; }
        [DataMember]
        public int DoctorTypeID { get; set; }
        [DataMember]
        public int LabServiceID { get; set; }
        [DataMember]
        public String TestName { get; set; }
        [DataMember]
        public Decimal TotalBillAmount { get; set; }
        [DataMember]
        public Decimal AdjustedAmount { get; set; }
        [DataMember]
        public Decimal DiscountedAmount { get; set; }
        [DataMember]
        public Decimal PaidAmount { get; set; }
        [DataMember]
        public int PaymentMode { get; set; }
        [DataMember]
        public String BankName { get; set; }
        [DataMember]
        public String RefundedBy { get; set; }
        [DataMember]
        public String Address { get; set; }
        [DataMember]
        public String CardNo { get; set; }
        [DataMember]
        public String AccountNo { get; set; }
        [DataMember]
        public String Remarks { get; set; }
        [DataMember]
        public Int64 DiscountByID { get; set; }
        [DataMember]
        public Decimal BalanceAmount { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public decimal DepositAmount { get; set; }
        [DataMember]
        public Int64 BillID { get; set; }
        [DataMember]
        public Int64 DeptID { get; set; }
        [DataMember]
        public Int64 DocID { get; set; }
        [DataMember]
        public String BillNo { get; set; }
        [DataMember]
        public Decimal TotalDiscountedAmount { get; set; }
        [DataMember]
        public Decimal TotalPaidAmount { get; set; }
        [DataMember]
        public Decimal TotalBill { get; set; }
        [DataMember]
        public Decimal TotalAdjustedAmount { get; set; }
        [DataMember]
        public Decimal Amount { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public Decimal ServiceCharge { get; set; }
        [DataMember]
        public int ItemID { get; set; }
        [DataMember]
        public Decimal Charge { get; set; }
        [DataMember]
        public Decimal NetCharges { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public Decimal TotalMRP { get; set; }
        [DataMember]
        public Int64 CollectedByID { get; set; }
        [DataMember]
        public decimal Tax { get; set; }
        [DataMember]
        public decimal TotalTax { get; set; }
        [DataMember]
        public decimal TotalBillTax { get; set; }
        [DataMember]
        public int custommertype { get; set; }

    }
    public class OPDbillingDataTOeXCEL
    {
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public String PatientName { get; set; }
        [DataMember]
        public String Address { get; set; }
        [DataMember]
        public string DocName { get; set; }
        [DataMember]
        public String BillNo { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public decimal BillAmount { get; set; }
        [DataMember]
        public Decimal Discount { get; set; }
        [DataMember]
        public Decimal PaidAmount { get; set; }
        [DataMember]
        public Decimal TotalAdjustedAmount { get; set; }
        [DataMember]
        public Decimal TotalDiscountedAmount { get; set; }
        [DataMember]
        public Decimal TotalPaidAmount { get; set; }
        [DataMember]
        public string Verify { get; set; }
        [DataMember]
        public string VerifyBy { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
    public class OPDLabbillingDataTOeXCEL
    {
        [DataMember]
        public String RefNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public String PatientName { get; set; }
        [DataMember]
        public String Address { get; set; }
        [DataMember]
        public String TestNameList { get; set; }
        [DataMember]
        public decimal BillAmount { get; set; }
        [DataMember]
        public String Referal { get; set; }
        [DataMember]
        public Decimal TotalDiscountedAmount { get; set; }
        [DataMember]
        public Decimal TotalPaidAmount { get; set; }
        [DataMember]
        public Decimal DueBalance { get; set; }
        [DataMember]
        public Decimal TotalAdjustedAmount { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
    }
    public class PHRbillingDataTOeXCEL
    {
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public String PatientName { get; set; }
        [DataMember]
        public String Address { get; set; }
        [DataMember]
        public decimal BillAmount { get; set; }
        [DataMember]
        public decimal TotalTax { get; set; }
        [DataMember]
        public Decimal TotalDiscountedAmount { get; set; }
        [DataMember]
        public Decimal TotalPaidAmount { get; set; }
        [DataMember]
        public Decimal TotalAdjustedAmount { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
    public class LabServiceAdjustmentDataTOeXCEL
    {
        [DataMember]
        public String AdjustmentNo { get; set; }
        [DataMember]
        public String BillNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public String PatientName { get; set; }
        [DataMember]
        public String TestName { get; set; }
        [DataMember]
        public decimal NetCharges { get; set; }
        [DataMember]
        public String RefundedBy { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }
    }
    public class DiscountRefundData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public String RefundNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public decimal RefundAmount { get; set; }
        [DataMember]
        public decimal TotalAmount { get; set; }
        [DataMember]
        public decimal TotalRefundAmount { get; set; }
        [DataMember]
        public String PatientName { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public String BillNo { get; set; }
        [DataMember]
        public int PatientType { get; set; }
    }
    public class OPdoctoravailable : BaseData
    {
        [DataMember]
        public Int64 DoctorID { get; set; }
        [DataMember]
        public Int32 DepartmentID { get; set; }
    }

}

