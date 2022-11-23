using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
    public class StockGRNData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 SubStockID { get; set; }
        [DataMember]
        public Int32 MedSubStockTypeID { get; set; }
        [DataMember]
        public Int64 ReturnID { get; set; }
        [DataMember]
        public int SerialID { get; set; }
        [DataMember]
        public Int64 StockID { get; set; }
        [DataMember]
        public string IndentNo { get; set; }
        [DataMember]
        public string Stock_To { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string Stock_From { get; set; }
        [DataMember]
        public string ReturnNo { get; set; }
        [DataMember]
        public string ApprovalListNo { get; set; }
        [DataMember]
        public int NoReturn { get; set; }
        [DataMember]
        public Int64 ItemID { get; set; }
        [DataMember]
        public int StockType { get; set; }
        [DataMember]
        public int StockTo { get; set; }
        [DataMember]
        public int StockFrom { get; set; }
        [DataMember]
        public int NoOfQty { get; set; }
        [DataMember]
        public string PONo { get; set; }
        [DataMember]
        public int FreeQuantity { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string Supplier { get; set; }
        [DataMember]
        public string CompanyName { get; set; }
        [DataMember]
        public string SupplierName { get; set; }
        [DataMember]
        public int CompanyID { get; set; }
        [DataMember]
        public Int64 SupplierID { get; set; }
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
        public int OrderedQty { get; set; }
        [DataMember]
        public Decimal TotalCP { get; set; }
        [DataMember]
        public Decimal CP { get; set; }
        [DataMember]
        public Decimal MRP { get; set; }
        [DataMember]
        public int PaymentTypeID { get; set; }
        [DataMember]
        public Double Tax { get; set; }
        [DataMember]
        public Double SGST { get; set; }
        [DataMember]
        public Double CGST { get; set; }
        [DataMember]
        public Double IGST { get; set; }
        [DataMember]
        public int IssueNo { get; set; }
        [DataMember]
        public string ReceiptNo { get; set; }
        [DataMember]
        public string HSNCode { get; set; }    
        [DataMember]
        public string BatchNo { get; set; }
        [DataMember]
        public string StockTypeName { get; set; }
        [DataMember]
        public string StockNo { get; set; }
        [DataMember]
        public DateTime ReceivedDate { get; set; }
        [DataMember]
        public DateTime MfgDate { get; set; }
        [DataMember]
        public DateTime ExpDate { get; set; }
        [DataMember]
        public DateTime VerifiedDate { get; set; }
        [DataMember]
        public int IsCrossChecke { get; set; }
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
        public Decimal DueAmt { get; set; }
        [DataMember]
        public Decimal CPPerQty { get; set; }
        [DataMember]
        public Decimal MRPperUnit { get; set; }
        [DataMember]
        public Decimal MRPPerQty { get; set; }
        [DataMember]
        public int TotalIssued { get; set; }
        [DataMember]
        public int TotalCondemned { get; set; }
        [DataMember]
        public int ApprovedQty { get; set; }
        [DataMember]
        public int TotalCondemnedQty { get; set; }
        [DataMember]
        public int TotalExpiry { get; set; }
        [DataMember]
        public int TotalUsable { get; set; }
        [DataMember]
        public int BalStock { get; set; }
        [DataMember]
        public Decimal CPRemItem { get; set; }
        [DataMember]
        public int GroupID { get; set; }
        [DataMember]
        public int SubGroupID { get; set; }
        [DataMember]
        public int PurchasetypeID { get; set; }
        [DataMember]
        public Decimal MRPRemItem { get; set; }
        [DataMember]
        public int TotalFreeQty { get; set; }
        [DataMember]
        public int TrecievedQty { get; set; }
        [DataMember]
        public int TotalrecievedQty { get; set; }
        [DataMember]
        public int TotalRqty { get; set; }
        [DataMember]
        public int RecvQty { get; set; }
        [DataMember]
        public Decimal FreeItemAmount { get; set; }
        [DataMember]
        public Decimal TotalFreeItemAmount { get; set; }
        [DataMember]
        public int DiscountType { get; set; }
        [DataMember]
        public decimal TaxableAmount { get; set; }
        [DataMember]
        public decimal CPafterTax { get; set; }
        [DataMember]
        public string Temperature { get; set; }
        [DataMember]
        public Decimal PaidAmount { get; set; }
        [DataMember]
        public Decimal SubTotalMRP { get; set; }
        [DataMember]
        public Decimal SubTotalCP { get; set; }
        [DataMember]
        public Decimal BalanceAmount { get; set; }
        [DataMember]
        public Decimal NetTotalCP { get; set; }
        [DataMember]
        public int EquivalentQtyPerUnit { get; set; }
        [DataMember]
        public int EquivalentQtyBalance { get; set; }
        [DataMember]
        public int TotalEquivalentQtyBalance { get; set; }
        [DataMember]
        public Int64 TransactionID { get; set; }
        [DataMember]
        public Decimal NetCharge { get; set; }
        [DataMember]
        public Decimal NoUnit { get; set; }
        [DataMember]
        public int EquivalentQty { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public Decimal TotalBillAmount { get; set; }
        [DataMember]
        public int TotalItemCount { get; set; }
        [DataMember]
        public string ReqNo { get; set; }
        [DataMember]
        public string BillNo { get; set; }
        [DataMember]
        public Decimal ApprovedAmount { get; set; }
        [DataMember]
        public Decimal RequestedAmount { get; set; }
        [DataMember]
        public string RequestedBy { get; set; }
        [DataMember]
        public DateTime RequestedDate { get; set; }
        [DataMember]
        public string ApprovedBy { get; set; }
        [DataMember]
        public DateTime ApprovedDate { get; set; }
        [DataMember]
        public int StatusID { get; set; }
        [DataMember]
        public string StatusName { get; set; }
        [DataMember]
        public string UnitName { get; set; }
        [DataMember]
        public DateTime ExpiryDate { get; set; }
        [DataMember]
        public string RemarksCancel { get; set; }
        [DataMember]
        public string RemarksApproved { get; set; }
        [DataMember]
        public string RemarksRejected { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public DateTime PaidDate { get; set; }
        [DataMember]
        public int PatientTypeID { get; set; }
        [DataMember]
        public string EmrgNo { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string RefundNo { get; set; }
        [DataMember]
        public string RefundBy { get; set; }
        [DataMember]
        public DateTime RefundDate { get; set; }
        [DataMember]
        public int ExpireDays { get; set; }
        [DataMember]
        public int RequestTypeID { get; set; }
        [DataMember]
        public string RemarksRefund { get; set; }
        [DataMember]
        public Decimal RefundedAmount { get; set; }
        [DataMember]
        public int MedSubStockID { get; set; }
		[DataMember]
		public Decimal NoUnitBalance { get; set; }
		[DataMember]
		public int TotalRecievedQty { get; set; }
		[DataMember]
		public int ReturnQty { get; set; }
		[DataMember]
		public int TotalReturnQty { get; set; }
		[DataMember]
		public string returnXMLData { get; set; }
		[DataMember]
		public Decimal Payableamt { get; set; }
		[DataMember]
		public Decimal Returnamt { get; set; }
    }
    public class StockItemDataTOeXCEL
    {
        [DataMember]
        public string ReceiptNo { get; set; }
        [DataMember]
        public string PONo { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public int TotalRecdQty { get; set; }
        [DataMember]
        public int FreeQuantity { get; set; }
        [DataMember]
        public string CP { get; set; }
        [DataMember]
        public string CPperunit { get; set; }
        [DataMember]
        public string CPPerQty { get; set; }
        [DataMember]
        public string TotalMRP { get; set; }
        [DataMember]
        public string MRPperUnit { get; set; }
        [DataMember]
        public string MRPPerQty { get; set; }
        [DataMember]
        public int TotalIssued { get; set; }
        [DataMember]
        public int TotalCondemned { get; set; }
        [DataMember]
        public int BalStock { get; set; }
        [DataMember]
        public string CPRemItem { get; set; }
        [DataMember]
        public string MRPRemItem { get; set; }
        [DataMember]
        public DateTime MfgDate { get; set; }
        [DataMember]
        public DateTime ExpDate { get; set; }
        [DataMember]
        public DateTime ReceivedDate { get; set; }
        [DataMember]
        public string RecdBy { get; set; }
        [DataMember]
        public int IsCrossChecked { get; set; }
    }
    public class StockistItemDataTOeXCEL
    {
        [DataMember]
        public string ReturnNo { get; set; }
        [DataMember]
        public int NoReturn { get; set; }
        [DataMember]
        public Decimal CPPerQty { get; set; }
    }
    public class ItemListDataTOeXCEL
    {
        [DataMember]
        public string ApprovalListNo { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string Supplier { get; set; }
        [DataMember]
        public int BalStock { get; set; }
        [DataMember]
        public Decimal CPPerQty { get; set; }
        [DataMember]
        public Double Tax { get; set; }
        [DataMember]
        public Decimal CP { get; set; }
    }
    public class ItemPurchaseListDataTOeXCEL
    {
        [DataMember]
        public string PONo { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string Supplier { get; set; }
        [DataMember]
        public int ApprovedQty { get; set; }
        [DataMember]
        public Decimal CPPerQty { get; set; }
        [DataMember]
        public Decimal CP { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
    }
    public class PurchaseCheckedListDataTOeXCEL
    {
        [DataMember]
        public string PONo { get; set; }
        [DataMember]
        public int OrderedQty { get; set; }
        [DataMember]
        public Decimal RecdQty { get; set; }
        [DataMember]
        public Decimal CP { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
    public class ItemTransferListDataTOeXCEL
    {
        [DataMember]
        public int IssueNo { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string Stock_From { get; set; }
        [DataMember]
        public string Stock_To { get; set; }
        [DataMember]
        public int TotalQuantity { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }
    }
}
