using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedBillData
{
    public class LabDueCollectionData :BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string BillNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string CustomerAddress { get; set; }
        [DataMember]
        public int PatientTypeID { get; set; }
        [DataMember]
        public int PaymentStatusID { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string Remarks { get; set; }

        //-----TAB2-------//
        [DataMember]
        public Int64 DueReponsibleBy { get; set; }
        [DataMember]
        public string DueReponsibleName { get; set; }
        [DataMember]
        public decimal TotalBillAmount { get; set; }
        [DataMember]
        public decimal Discount { get; set; }
        [DataMember]
        public decimal PaidAmount { get; set; }
        [DataMember]
        public decimal DueAmount { get; set; }
        [DataMember]
        public decimal LastDuePaid { get; set; }
        [DataMember]
        public decimal DueBalance { get; set; }
        [DataMember]
        public decimal Paid { get; set; }
        [DataMember]
        public decimal Due { get; set; }
        [DataMember]
        public decimal DueDiscount { get; set; }
        [DataMember]
        public string DiscountRemark { get; set; }


        //------TOTAL SUM-----------//
        [DataMember]
        public decimal SumTotalBill { get; set; }
        [DataMember]
        public decimal SumDiscount { get; set; }
        [DataMember]
        public decimal SumPaidAmount { get; set; }
        [DataMember]
        public decimal SumDueAmount { get; set; }
        [DataMember]
        public decimal SumLastDuePaid { get; set; }
        [DataMember]
        public decimal SumDueBalance { get; set; }

        //---PAYMENT MODE DETAILS---//
        [DataMember]
        public int PaymentModeID { get; set; }
        [DataMember]
        public string PaymentMode { get; set; }
        [DataMember]
        public int BankID { get; set; }
        [DataMember]
        public string BankName { get; set; }
        [DataMember]
        public string CardNo_ChequeNo { get; set; }
        [DataMember]
        public string InvoiceNo { get; set; }
        [DataMember]
        public string AC { get; set; }
        [DataMember]
        public string Cheque { get; set; }

        //-----TAB3-------//
        [DataMember]
        public string DueCollNo { get; set; }
        [DataMember]
        public string RecieptNo { get; set; }
    }
}
