using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Mediqura.CommonData.MedStore
{
    public class GenIndentData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 RequestedBy { get; set; }
        [DataMember]
        public Int64 StockID { get; set; }
        [DataMember]
        public Int64 SubStockID { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public Int32 StockTypeID { get; set; }
        [DataMember]
        public DateTime IndentRaiseDate { get; set; }
        [DataMember]
        public Int32 IndentRequestID { get; set; }
        [DataMember]
        public string IndentRequestType { get; set; }
        [DataMember]
        public string IndentNo { get; set; }
        [DataMember]
        public Int64 DeptID { get; set; }
        [DataMember]
        public Int32 GenStockID { get; set; }
        [DataMember]
        public Int64 IndentID { get; set; }
        [DataMember]
        public string Stock_From { get; set; }
        [DataMember]
        public string ReturnNo { get; set; }
        [DataMember]
        public string ApprovalListNo { get; set; }
        [DataMember]
        public Int32 NoReturn { get; set; }
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
        //[DataMember]
        //public string RecdBy { get; set; }
        [DataMember]
        public string ApprovdBy { get; set; }
        [DataMember]
        public int SerialID { get; set; }
        [DataMember]
        public string Unitname { get; set; }
        [DataMember]
        public Decimal CP { get; set; }
        [DataMember]
        public Decimal MRP { get; set; }
        [DataMember]
        public Double Tax { get; set; }
        [DataMember]
        public int IssueNo { get; set; }
        [DataMember]
        public string ReceiptNo { get; set; }
        [DataMember]
        public string BatchNo { get; set; }
        [DataMember]
        public string StockTypeName { get; set; }
        [DataMember]
        public string StockNo { get; set; }
        [DataMember]
        public DateTime IssueDate { get; set; }
        [DataMember]
        public DateTime ApprovdDate { get; set; }
        [DataMember]
        public DateTime CollectedDate { get; set; }
        [DataMember]
        public DateTime VerifiedDate { get; set; }
        [DataMember]
        public DateTime ApprvdDate { get; set; }
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
        public Decimal MRPperQty { get; set; }
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
        public string Department { get; set; }
        [DataMember]
        public int ReqdQty { get; set; }
        [DataMember]
        public Int32 IndStatus { get; set; }
        [DataMember]
        public string RequestStat { get; set; }
        [DataMember]
        public int StatusID { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public int TotRequestQty { get; set; }
        [DataMember]
        public int TotApproved { get; set; }
        [DataMember]
        public int TotAccepted { get; set; }
        [DataMember]
        public Decimal ApprovedQty { get; set; }
        [DataMember]
        public int TotHandOver { get; set; }
        [DataMember]
        public int TotReceived { get; set; }
        [DataMember]
        public Decimal TotalCP { get; set; }
        [DataMember]
        public int apprvQty { get; set; }
        [DataMember]
        public Int64 ApprvBy { get; set; }
        [DataMember]
        public Int64 HandOverBy { get; set; }
        [DataMember]
        public Int64 HandOverTo { get; set; }
        [DataMember]
        public string IndentStatus { get; set; }
        [DataMember]
        public Int32 IndentStateID { get; set; }
        [DataMember]
        public Int32 VerifiedStatus { get; set; }
        [DataMember]
        public Int32 RequestQty { get; set; }
        [DataMember]
        public Int32 ReturnQty { get; set; }
        [DataMember]
        public Int64 ReturnBy { get; set; }
        [DataMember]
        public Int32 Totavailable { get; set; }
        [DataMember]
        public Int32 TotReturnQty { get; set; }
        [DataMember]
        public Int32 SumReturnQty { get; set; }
        [DataMember]
        public Int32 SumAvailQty { get; set; }
        [DataMember]
        public Int32 QtyApproved { get; set; }
        [DataMember]
        public Decimal QtyUsed { get; set; }
        [DataMember]
        public Decimal AvailableBalAfterUsed { get; set; }
        [DataMember]
        public Int32 CondemnQty { get; set; }
        [DataMember]
        public Int32 TotalApprovdQty { get; set; }
        [DataMember]
        public Int32 IsApproved { get; set; }
        [DataMember]
        public Int32 StockStatus { get; set; }
        [DataMember]
        public string VerificationStatus { get; set; }
        [DataMember]
        public Int64 VerifiedBy { get; set; }
        [DataMember]
        public Int64 TotalIndentQty { get; set; }
        [DataMember]
        public Double AvailablePC { get; set; }
    }
    public class GenINDHandOverDataToExcel
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 StockID { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public Int32 StockTypeID { get; set; }
        [DataMember]
        public DateTime IndentRaiseDate { get; set; }
        [DataMember]
        public DateTime ApprvdDate { get; set; }
        [DataMember]
        public DateTime HndOverDate { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }
        [DataMember]
        public Int32 IndentRequestID { get; set; }
        [DataMember]
        public string IndentRequestType { get; set; }
        [DataMember]
        public string IndentNo { get; set; }
        [DataMember]
        public Int64 SubStockID { get; set; }
        [DataMember]
        public Int64 ReturnID { get; set; }
        [DataMember]
        public int SerialID { get; set; }
        [DataMember]
        public Int64 IndentID { get; set; }
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
        public int TotRequested { get; set; }
        [DataMember]
        public int TotAccepted { get; set; }
        [DataMember]
        public int TotApprovedQty { get; set; }
        [DataMember]
        public Decimal ApprovedQty { get; set; }
        [DataMember]
        public int TotHandOver { get; set; }
        [DataMember]
        public int TotReceived { get; set; }
        [DataMember]
        public Decimal TotalCP { get; set; }
        [DataMember]
        public Decimal CP { get; set; }
        [DataMember]
        public Decimal MRP { get; set; }
        [DataMember]
        public Double Tax { get; set; }
        [DataMember]
        public int IssueNo { get; set; }
        [DataMember]
        public string ReceiptNo { get; set; }
        [DataMember]
        public string BatchNo { get; set; }
        [DataMember]
        public string StockTypeName { get; set; }
        [DataMember]
        public string StockNo { get; set; }
        [DataMember]
        public DateTime IssueDate { get; set; }
        [DataMember]
        public DateTime MfgDate { get; set; }
        [DataMember]
        public DateTime ExpDate { get; set; }
        [DataMember]
        public DateTime VerifiedDate { get; set; }
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
        public int BalStock { get; set; }
        [DataMember]
        public Decimal CPRemItem { get; set; }
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
        public int ReqdQty { get; set; }
        [DataMember]
        public string IndentStatus { get; set; }
        [DataMember]
        public int IndStatus { get; set; }
        [DataMember]
        public int StatusID { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public int ApprvQty { get; set; }
        [DataMember]
        public int apprvQty { get; set; }
        [DataMember]
        public int HndQty { get; set; }
        [DataMember]
        public Int32 ReturnQty { get; set; }
        [DataMember]
        public Int32 TotReturnQty { get; set; }
        [DataMember]
        public string HandOverTo { get; set; }
        [DataMember]
        public string ApprvBy { get; set; }
    }
    public class MedIndentData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 RequestedBy { get; set; }
        [DataMember]
        public Int64 StockID { get; set; }
        [DataMember]
        public Int64 SubStockID { get; set; }
        [DataMember]
        public Int64 MedSubStockID { get; set; }
        [DataMember]
        public string StockName { get; set; }
        [DataMember]
        public string HSNCode { get; set; }
        [DataMember]
        public string SGST { get; set; }
        [DataMember]
        public string CGST { get; set; }
        [DataMember]
        public string IGST { get; set; }
       
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public DateTime IndentRaiseDate { get; set; }
        [DataMember]
        public Int32 IndentRequestID { get; set; }
        [DataMember]
        public string IndentRequestType { get; set; }
        [DataMember]
        public string IndentNo { get; set; }
        [DataMember]
        public Decimal MRPperunit { get; set; }
        [DataMember]
        public Decimal MRPperQty { get; set; }
        [DataMember]
        public Int64 IndentID { get; set; }
        [DataMember]
        public string ReturnNo { get; set; }
        [DataMember]
        public Int64 ItemID { get; set; }
        [DataMember]
        public Int64 ReceivedBy { get; set; }
        [DataMember]
        public int SerialID { get; set; }
        [DataMember]
        public string Unitname { get; set; }
        [DataMember]
        public int IssueNo { get; set; }
        [DataMember]
        public string StockNo { get; set; }
        [DataMember]
        public DateTime IssueDate { get; set; }
        [DataMember]
        public DateTime ApprovdDate { get; set; }
        [DataMember]
        public DateTime VerifiedDate { get; set; }
        [DataMember]
        public DateTime ApprvdDate { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public decimal BalStock { get; set; }
        [DataMember]
        public int GroupID { get; set; }
        [DataMember]
        public int SubGroupID { get; set; }
        [DataMember]
        public int TotalRqty { get; set; }
        [DataMember]
        public int RecvQty { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public int ReqdQty { get; set; }
        [DataMember]
        public Int32 IndStatus { get; set; }
        [DataMember]
        public string RequestStat { get; set; }
        [DataMember]
        public int StatusID { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public int TotRequestQty { get; set; }
        [DataMember]
        public int TotApproved { get; set; }
        [DataMember]
        public Decimal ApprovedQty { get; set; }
        [DataMember]
        public int TotHandOver { get; set; }
        [DataMember]
        public int TotReceived { get; set; }
        [DataMember]
        public decimal apprvQty { get; set; }
        [DataMember]
        public Int64 ApprvBy { get; set; }
        [DataMember]
        public Int64 HandOverTo { get; set; }
        [DataMember]
        public string IndentStatus { get; set; }
        [DataMember]
        public Int32 IndentStateID { get; set; }
        [DataMember]
        public Int32 VerifiedStatus { get; set; }
        [DataMember]
        public Int32 RequestQty { get; set; }
        [DataMember]
        public Int32 ReturnQty { get; set; }
        [DataMember]
        public Int64 ReturnBy { get; set; }
        [DataMember]
        public Int32 Totavailable { get; set; }
        [DataMember]
        public Int32 TotReturnQty { get; set; }
        [DataMember]
        public Int32 SumReturnQty { get; set; }
        [DataMember]
        public Int32 SumAvailQty { get; set; }
        [DataMember]
        public Int32 QtyApproved { get; set; }
        [DataMember]
        public Decimal QtyUsed { get; set; }
        [DataMember]
        public Decimal AvailableBalAfterUsed { get; set; }
        [DataMember]
        public Int32 CondemnQty { get; set; }
        [DataMember]
        public Int32 TotalApprovdQty { get; set; }
        [DataMember]
        public Int32 IsApproved { get; set; }
        [DataMember]
        public Int32 StockStatus { get; set; }
        [DataMember]
        public string VerificationStatus { get; set; }
        [DataMember]
        public Int64 VerifiedBy { get; set; }
        [DataMember]
        public Int64 TotalIndentQty { get; set; }
        [DataMember]
        public Double AvailablePC { get; set; }
        [DataMember]
        public Int32 AvailableQty { get; set; }
        [DataMember]
        public string Unit { get; set; }
        [DataMember]
        public Int32 EquivalentQtyApproved { get; set; }
        [DataMember]
        public Decimal TotalEquivalentQtyBalance { get; set; }
        [DataMember]
        public DateTime ExpiryDate { get; set; }
        [DataMember]
        public Int32 SubHeading { get; set; }
        [DataMember]
        public Decimal TotalUnitBalance { get; set; }
        [DataMember]
        public Int32 IndentStatusID { get; set; }
        [DataMember]
        public Int32 EquQtyPerUnit { get; set; }
        [DataMember]
        public Decimal PC { get; set; }
        [DataMember]
        public Decimal TotalAvailUnit { get; set; }
        [DataMember]
        public Decimal MainStockAvail { get; set; }
        [DataMember]
        public DateTime LastPurchageDate { get; set; }
        [DataMember]
        public string ReqNo { get; set; }
        [DataMember]
        public string BatchNo { get; set; }
        [DataMember]
        public Int32 SupplierID { get; set; }
        [DataMember]
        public string SupplierName { get; set; }
		[DataMember]
		public string AlertTime { get; set; }
		[DataMember]
		public string ReqBy { get; set; }
		[DataMember]
		public string ApvBy { get; set; }
		[DataMember]
		public string phrreqtype { get; set; }
		[DataMember]
		public string phrbillno { get; set; }
    }
    public class MedIndentDatatoExcel
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string ReturnNo { get; set; }
        [DataMember]
        public Int64 ReturnQty { get; set; }
        [DataMember]
        public string ReturnBy { get; set; }
        [DataMember]
        public DateTime ReturnDate { get; set; }
    }
}
