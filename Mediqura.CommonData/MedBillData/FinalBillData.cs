using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.AdmissionData
{
    public class FinalBillData : BaseData
    {
        [DataMember]
        public Int64 DischargeID { get; set; }
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string PatientAddress { get; set; }
        [DataMember]
        public string GenderID { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public string GenderName { get; set; }
        [DataMember]
        public string AdmissionNo { get; set; }
        [DataMember]
        public DateTime AdmissionDate { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string consultant { get; set; }
        [DataMember]
        public string DischargeDoc { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public DateTime IntimationDate { get; set; }
        [DataMember]
        public Int32 Quantity { get; set; }
        [DataMember]
        public Int32 ServiceTypeID { get; set; }
        [DataMember]
        public string ServiceType { get; set; }
        [DataMember]
        public Decimal TotalAmount { get; set; }
        [DataMember]
        public Decimal TotalBillAmount { get; set; }
        [DataMember]
        public Decimal BalanceAmount { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public Decimal ServiceCharge { get; set; }
        [DataMember]
        public Decimal NetServiceCharge { get; set; }
        [DataMember]
        public Decimal TotalAdjusted { get; set; }
        [DataMember]
        public Decimal DiscountedAmount { get; set; }
        [DataMember]
        public Decimal AdjustedAmount { get; set; }
        [DataMember]
        public Decimal TotalDiscount { get; set; }
        [DataMember]
        public Decimal PaidAmount { get; set; }
        [DataMember]
        public Decimal TotalDue { get; set; }
        [DataMember]
        public int PaymentMode { get; set; }
        [DataMember]
        public string AccountNo { get; set; }
        [DataMember]
        public string CardNo { get; set; }
        [DataMember]
        public string BillNo { get; set; }
        [DataMember]
        public Decimal TotalDiscountAmount { get; set; }
        [DataMember]
        public Decimal TotalAdjustedAmount { get; set; }
        [DataMember]
        public Decimal TotalPaidAmount { get; set; }
        [DataMember]
        public Decimal TotalBill { get; set; }

    }
    public class FinalBillDataTOeXCEL
    {
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public Decimal TotalBillAmount { get; set; }
        [DataMember]
        public Decimal TotalAdjusted { get; set; }
        [DataMember]
        public Decimal DiscountedAmount { get; set; }
        [DataMember]
        public Decimal AdjustedAmount { get; set; }
        [DataMember]
        public Decimal TotalDiscount { get; set; }
        [DataMember]
        public Decimal PaidAmount { get; set; }
        [DataMember]
        public Decimal TotalDue { get; set; }

    }
    public class DiscoutData
    {
        [DataMember]
        public Int64 DiscountByID { get; set; }
        [DataMember]
        public string DiscountBy { get; set; }
        [DataMember]
        public Decimal Discount { get; set; }
    }
    public class InterimBillData : BaseData
    {
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public Decimal TotalDeposited { get; set; }
        [DataMember]
        public Decimal TotalBill { get; set; }
        [DataMember]
        public Decimal OustandingBill { get; set; }
        [DataMember]
        public Decimal PC { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public Decimal TotalBillAmount { get; set; }
        [DataMember]
        public Decimal TotalDepositedAmount { get; set; }
        [DataMember]
        public Decimal TotalOutstandingBill { get; set; }
        [DataMember]
        public DateTime AdmissionDate { get; set; }
        [DataMember]
        public string BedDetail { get; set; }
        [DataMember]
        public string AdmissionDoctor { get; set; }
        [DataMember]
        public Decimal CreditLimit { get; set; }

    }
    public class InterimBillDataToExcel : BaseData
    {
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string BedDetail { get; set; }
        [DataMember]
        public Decimal TotalDeposited { get; set; }
        [DataMember]
        public Decimal TotalBill { get; set; }
        [DataMember]
        public Decimal OustandingBill { get; set; }
        [DataMember]
        public Decimal CreditLimit { get; set; }
        [DataMember]
        public string AdmissionDoctor { get; set; }
        [DataMember]
        public DateTime AdmissionDate { get; set; }

    }
}
