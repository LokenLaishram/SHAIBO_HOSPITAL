using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
    public class StockStatusData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public int SerialID { get; set; }
        [DataMember]
        public int ItemID { get; set; }
        [DataMember]
        public Int64 StockID { get; set; }
        [DataMember]
        public Int64 SubStockID { get; set; }
        [DataMember]
        public string StockType { get; set; }
        [DataMember]
        public int StockTypeID { get; set; }
        [DataMember]
        public Decimal NoUnit { get; set; }
        [DataMember]
        public int NoOfQty { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string CompanyName { get; set; }
        [DataMember]
        public string SupplierName { get; set; }
        [DataMember]
        public int CompanyID { get; set; }
        [DataMember]
        public int SubstockReturnQty { get; set; }
        [DataMember]
        public int StockistReturnQty { get; set; }
        [DataMember]
        public int SupplierID { get; set; }
        [DataMember]
        public int FreeQty { get; set; }
        [DataMember]
        public Int64 ReceivedBy { get; set; }
        [DataMember]
        public string RecdBy { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public int TotalQuantity { get; set; }
        [DataMember]
        public int TotalRecdQty { get; set; }
        [DataMember]
        public Decimal CP { get; set; }
        [DataMember]
        public Decimal MRP { get; set; }
        [DataMember]
        public Decimal Tax { get; set; }
        [DataMember]
        public Decimal TaxableAmount { get; set; }
        [DataMember]
        public string ReceiptNo { get; set; }
        [DataMember]
        public string POno { get; set; }
        [DataMember]
        public DateTime ReceivedDate { get; set; }
        [DataMember]
        public DateTime SubReceivedDate { get; set; }
        [DataMember]
        public DateTime MfgDate { get; set; }
        [DataMember]
        public DateTime ExpDate { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public int NoOfUnit { get; set; }
        [DataMember]
        public int QtyperUnit { get; set; }
        [DataMember]
        public Decimal CPperunit { get; set; }
        [DataMember]
        public Decimal MRPperunit { get; set; }
        [DataMember]
        public Decimal TotalMRP { get; set; }
        [DataMember]
        public Decimal Discount { get; set; }
        [DataMember]
        public Decimal CPPerUnit { get; set; }
        [DataMember]
        public Decimal CPPerQty { get; set; }
        [DataMember]
        public Decimal MRPperUnit { get; set; }
        [DataMember]
        public Decimal MRPPerQty { get; set; }
        [DataMember]
        public Decimal CPafterTax { get; set; }
        [DataMember]
        public int TotalIssued { get; set; }
        [DataMember]
        public int TotalCondemnQty { get; set; }
        [DataMember]
        public int BalStock { get; set; }
        [DataMember]
        public Decimal CPRemItem { get; set; }
        [DataMember]
        public Decimal MRPRemItem { get; set; }
        [DataMember]
        public string BatchNo { get; set; }
        [DataMember]
        public string StockNo { get; set; }
        [DataMember]
        public int TotalSale { get; set; }
        [DataMember]
        public int Availbalancefrom { get; set; }
        [DataMember]
        public int Availbalanceto { get; set; }
        [DataMember]
        public int ExpiryDayfrom { get; set; }
        [DataMember]
        public int ExpiryDayto { get; set; }
        [DataMember]
        public int GroupID { get; set; }
        [DataMember]
        public int SubGroupID { get; set; }
        [DataMember]
        public int StockStatus { get; set; }
        [DataMember]
        public int Recievedyear { get; set; }
        [DataMember]
        public int Recievedmonth { get; set; }
        [DataMember]
        public int Nodaystoexpire { get; set; }
        [DataMember]
        public Int64 TotalRecievedQty { get; set; }
        [DataMember]
        public Int64 TotalSoldQty { get; set; }
        [DataMember]
        public Int64 TotalIsssuedQty { get; set; }
        [DataMember]
        public Int64 TotalCondmQty { get; set; }
        [DataMember]
        public decimal TotalRCP { get; set; }
        [DataMember]
        public decimal TotalRMRP { get; set; }
        [DataMember]
        public decimal TotalCMRP { get; set; }
        [DataMember]
        public Int64 TotalbalaQty { get; set; }
        [DataMember]
        public string RecivedDates { get; set; }
        [DataMember]
        public string ExpireDates { get; set; }
        [DataMember]
        public string Numpoint { get; set; }
        [DataMember]
        public string Numright { get; set; }
        [DataMember]
        public int SubHeading { get; set; }
        [DataMember]
        public Int64 TotalSubstockReturnQty { get; set; }
        [DataMember]
        public Int64 TotalVendorReturnQty { get; set; }
        [DataMember]
        public Int32 TotalUnitBalance { get; set; }
        [DataMember]
        public Double SGST { get; set; }
        [DataMember]
        public Double CGST { get; set; }
        [DataMember]
        public string RecievedDates { get; set; }
        [DataMember]
        public string ExpiryDates { get; set; }
		[DataMember]
		public decimal AvailableBalAfterUsed { get; set; }

	}
    public class MedOPDSalesData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 SubStockID { get; set; }
        [DataMember]
        public Int64 TransactionID { get; set; }
        [DataMember]
        public string BillNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public int PaymentMode { get; set; }
        [DataMember]
        public string BankName { get; set; }
        [DataMember]
        public int BankID { get; set; }
        [DataMember]
        public string CardNo_ChequeNo { get; set; }
        [DataMember]
        public string InvoiceNo { get; set; }
        [DataMember]
        public Decimal TotalBillAmount { get; set; }
        [DataMember]
        public Decimal Discount { get; set; }
        [DataMember]
        public Decimal PaidAmount { get; set; }
        [DataMember]
        public Decimal MRPperQty { get; set; }
        [DataMember]
        public Decimal MRPperUnit { get; set; }
        [DataMember]
        public Decimal CPperQty { get; set; }
        [DataMember]
        public Decimal NoUnit { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public Decimal NetCharge { get; set; }
        [DataMember]
        public int ItemID { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public int SerialID { get; set; }
        [DataMember]
        public string BatchNo { get; set; }
        [DataMember]
        public DateTime ExpiryDate { get; set; }
        [DataMember]
        public int SubmitType { get; set; }
        [DataMember]
        public Decimal DueAmount { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string ReqNo { get; set; }
        [DataMember]
        public Int64 DueReponsibleBy { get; set; }
        [DataMember]
        public DateTime Datefrom { get; set; }
        [DataMember]
        public DateTime Dateto { get; set; }
        [DataMember]
        public int RequestStatus { get; set; }
        [DataMember]
        public string RStatus { get; set; }
        [DataMember]
        public int PatientType { get; set; }
        [DataMember]
        public Decimal RequestedAmount { get; set; }
        [DataMember]
        public Decimal ApprovedAmount { get; set; }
        [DataMember]
        public string ReqstBy { get; set; }
        [DataMember]
        public string ApprovedBy { get; set; }
        [DataMember]
        public DateTime ReqDate { get; set; }
        [DataMember]
        public DateTime ApprovedDate { get; set; }
        [DataMember]
        public Decimal SumtotalBillAmount { get; set; }
        [DataMember]
        public Decimal SumTotalDiscount { get; set; }
        [DataMember]
        public Decimal sumTotalPaid { get; set; }
        [DataMember]
        public Decimal sumTotalDueAmnt { get; set; }
        [DataMember]
        public string CollectedBy { get; set; }
    }
    public class StockStatusListDataTOeXCEL
    {
        [DataMember]
        public string StockNo { get; set; }
        [DataMember]
        public string StockType { get; set; }
        [DataMember]
        public string BatchNo { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public Int64 SubStockID { get; set; }
        [DataMember]
        public Int32 TotalRecdQty { get; set; }
        [DataMember]
        public Int32 TotalIssued { get; set; }
        [DataMember]
        public Int32 TotalSale { get; set; }
        [DataMember]
        public Int32 TotalCondemnQty { get; set; }
        [DataMember]
        public Int32 BalStock { get; set; }
        [DataMember]
        public decimal CPPerQty { get; set; }
        [DataMember]
        public decimal CP { get; set; }
        [DataMember]
        public decimal MRPperqty { get; set; }
        [DataMember]
        public decimal TotalMRP { get; set; }
        [DataMember]
        public DateTime ExpDate { get; set; }
        [DataMember]
        public int Nodaystoexpire { get; set; }
        [DataMember]
        public DateTime ReceivedDate { get; set; }
        [DataMember]
        public DateTime SubReceivedDate { get; set; }
        [DataMember]
        public string SupplierName { get; set; }
        [DataMember]
        public string CompanyName { get; set; }

    }
}
