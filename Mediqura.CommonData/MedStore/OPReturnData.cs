using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
    public class OPReturnData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string PatientAddress { get; set; }
        [DataMember]
        public Int64 BillNo { get; set; }          
        [DataMember]
        public String OPBillNo { get; set; }
        [DataMember]
        public Int64 ReturnID { get; set; }
        [DataMember]
        public int BankID { get; set; }
        [DataMember]
        public string BankName { get; set; }
        [DataMember]
        public string CardNo_ChequeNo { get; set; }
        [DataMember]
        public string InvoiceNo { get; set; }
        [DataMember]
        public decimal TotalBillAmount { get; set; }
        [DataMember]
        public decimal Discount { get; set; }
        [DataMember]
        public decimal PaidAmount { get; set; }
        [DataMember]
        public decimal DueAmount { get; set; } 
        [DataMember]
        public Int64 StockID { get; set; }
        [DataMember]
        public Int64 SubStockID { get; set; }
        [DataMember]
        public Int64 ItemID { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string BatchNo { get; set; }
        [DataMember]
        public DateTime ExpiryDate { get; set; }
        [DataMember]
        public int QuantityPerUnit { get; set; }
        [DataMember]
        public int UnitID { get; set; }
        [DataMember]
        public decimal MRPperQty { get; set; }
        [DataMember]
        public decimal MRPperUnit { get; set; }
        [DataMember]
        public decimal CPperQty { get; set; }
        [DataMember]
        public Int32 Quantity { get; set; }
        [DataMember]
        public Decimal NoUnit { get; set; }
        [DataMember]
        public Decimal ItemWiseDiscount { get; set; }
        [DataMember]
        public Decimal NetCharges { get; set; }

        [DataMember]
        public int SerialID { get; set; }
        [DataMember]
        public string ReturnNo { get; set; }
        [DataMember]
        public Int64 StockNo { get; set; }         
        [DataMember]
        public Decimal Charges { get; set; }         
        [DataMember]
        public Int32 Return { get; set; }
        [DataMember]
        public Decimal ReturnAmt { get; set; }    
        [DataMember]
        public Int32 NoReturn { get; set; }
        [DataMember]
        public Int64 HandOver { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string Remarks { get; set; }
       
        [DataMember]
        public int totalreturnQty { get; set; }
        [DataMember]
        public Decimal DeductedPC { get; set; }
        [DataMember]
        public Decimal TotalReturnAmount { get; set; }
        [DataMember]
        public Decimal TotalActualReturnAmount { get; set; }
        [DataMember]
        public int LreturnQty { get; set; }
        [DataMember]
        public int TotalReturnQty { get; set; }
        [DataMember]
        public Decimal TotalDeductedAmount { get; set; }
        [DataMember]
        public Decimal TotalReturnAmounts { get; set; }
        [DataMember]
        public Decimal DeductedAmount { get; set; }
        [DataMember]
        public string ReturnBy { get; set; }
        [DataMember]
        public DateTime ReturnDate { get; set; }
        [DataMember]
        public Decimal SumTotalReturn { get; set; }

    }
    public class OPReturnDatatoExcel : BaseData
    {
        [DataMember]
        public string ReturnNo { get; set; }
        [DataMember]
        public string ReturnBy { get; set; }
        [DataMember]
        public int TotalReturnQty { get; set; }
        [DataMember]
        public decimal TotalReturnAmount { get; set; }
        [DataMember]
        public decimal DeductedAmount { get; set; }
        [DataMember]
        public decimal TotalActualReturnAmount { get; set; }
        [DataMember]
        public DateTime ReturnDate { get; set; }
    }
}
