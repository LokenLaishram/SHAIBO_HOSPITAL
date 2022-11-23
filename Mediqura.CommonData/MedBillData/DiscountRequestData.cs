using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedBillData
{
    public class DiscountRequestData : BaseData
    {
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public int serviceTypeID { get; set; }
        [DataMember]
        public int BillType { get; set; }
        [DataMember]
        public string BillNo { get; set; }
        [DataMember]
        public Int64 BillID { get; set; }
        [DataMember]
        public string PatName { get; set; }
        [DataMember]
        public string PatientAddress { get; set; }
        [DataMember]
        public int PatientAge { get; set; }
        [DataMember]
        public string Doctor { get; set; }
        [DataMember]
        public Int64 DoctorID { get; set; }
        [DataMember]
        public Int64 ServiceID { get; set; }
        [DataMember]
        public string ServiceCode { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public decimal amount { get; set; }
        [DataMember]
        public decimal NetAmount { get; set; }
        [DataMember]
        public int DisType { get; set; }
        [DataMember]
        public decimal DisValue { get; set; }
        [DataMember]
        public decimal DisAmount { get; set; }
        [DataMember]
        public string ReqDoctor { get; set; }
        [DataMember]
        public decimal TotalAmount { get; set; }
        [DataMember]
        public decimal TotalDiscount { get; set; }
        [DataMember]
        public decimal TotalNetAmount { get; set; }
        [DataMember]
        public String DisXmlData { get; set; }
        [DataMember]
        public String Remarks { get; set; }
        [DataMember]
        public int RequestNo { get; set; }

    }
    public class DiscountRequestServiceData : BaseData
    {
        [DataMember]
        public int RequestNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public int serviceTypeID { get; set; }
        [DataMember]
        public int BillType { get; set; }
        [DataMember]
        public string BillNo { get; set; }
        [DataMember]
        public Int64 BillID { get; set; }
        [DataMember]
        public string PatName { get; set; }
        [DataMember]
        public string PatientAddress { get; set; }
        [DataMember]
        public int PatientAge { get; set; }
        [DataMember]
        public string Doctor { get; set; }
        [DataMember]
        public Int64 DoctorID { get; set; }
        [DataMember]
        public int ServiceID { get; set; }
        [DataMember]
        public string ServiceCode { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public decimal amount { get; set; }
        [DataMember]
        public decimal NetAmount { get; set; }
        [DataMember]
        public int DisType { get; set; }
        [DataMember]
        public int isDis { get; set; }
        [DataMember]
        public decimal DisValue { get; set; }
        [DataMember]
        public decimal HosShare { get; set; }
        [DataMember]
        public decimal DocShare { get; set; }
        [DataMember]
        public decimal RefShare { get; set; }
        [DataMember]
        public decimal DisAmount { get; set; }
        [DataMember]
        public string ReqDoctor { get; set; }
        [DataMember]
        public decimal TotalAmount { get; set; }
        [DataMember]
        public decimal TotalDiscount { get; set; }
        [DataMember]
        public decimal TotalNetAmount { get; set; }
        [DataMember]
        public String DisXmlData { get; set; }
        [DataMember]
        public String Remarks { get; set; }
        [DataMember]
        public String AckRemarks { get; set; }
        [DataMember]
        public int DisStatus { get; set; }

    }
    public class DiscountCount : BaseData
    {

        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string AlertTime { get; set; }
        [DataMember]
        public string ReqBy { get; set; }
        [DataMember]
        public int BillType { get; set; }
        [DataMember]
        public int serviceType { get; set; }
    }
    public class DiscountOutput 
    {

        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int Resultoutput { get; set; }
  
    }
    public class DiscountRequestListData : BaseData
    {

        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public int serviceTypeID { get; set; }
        [DataMember]
        public int BillType { get; set; }
        [DataMember]
        public string BillNo { get; set; }
        [DataMember]
        public Int64 BillID { get; set; }
        [DataMember]
        public string PatName { get; set; }
        [DataMember]
        public string PatientAddress { get; set; }
        [DataMember]
        public int RequestNo { get; set; }
        [DataMember]
        public int DisStatus { get; set; }
        [DataMember]
        public int RequestedBy { get; set; }
        [DataMember]
        public DateTime RequestOn { get; set; }
        [DataMember]
        public DateTime Datefrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
        [DataMember]
        public decimal Discount { get; set; }
        [DataMember]
        public decimal Approve { get; set; }
        [DataMember]
        public decimal NetAmount { get; set; }
        [DataMember]
        public decimal TotalAmount { get; set; }
        [DataMember]
        public decimal TotalDiscount { get; set; }
        [DataMember]
        public decimal TotalApprove { get; set; }
        [DataMember]
        public decimal TotalNetAmount { get; set; }
        [DataMember]
        public String DiscountStatus { get; set; }
        [DataMember]
        public String Remarks { get; set; }
        [DataMember]
        public String ReqBy { get; set; }
        [DataMember]
        public int TotalonRequest { get; set; }
        [DataMember]
        public int TotalonApprove { get; set; }

    }
    public class AdmissionDiscountData : BaseData {
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public decimal TotalRequestAmount { get; set; }
        [DataMember]
        public decimal TotalAmount { get; set; }
        [DataMember]
        public int ServiceType { get; set; }
        [DataMember]
        public int BillingType { get; set; }
        [DataMember]
        public string BillNo { get; set; }
        [DataMember]
        public string Remarks { get; set; }
    }
}
