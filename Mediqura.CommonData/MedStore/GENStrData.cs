using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
    public class GENStrData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 SubStockID { get; set; }
        [DataMember]
        public Int64 ReturnID { get; set; }
        [DataMember]
        public int PuchaseMethodID { get; set; }
        [DataMember]
        public int SerialID { get; set; }
        [DataMember]
        public Int64 StockID { get; set; }
        [DataMember]
        public string IndentNo { get; set; }
        [DataMember]
        public string Unit { get; set; }
        [DataMember]
        public string Stock_To { get; set; }
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
        public int RackGroup { get; set; }
        [DataMember]
        public int RackSubGroup { get; set; }
        [DataMember]
        public string RackDetail { get; set; }
        [DataMember]
        public Decimal TotalCPbeforeTax { get; set; }
        [DataMember]
        public Decimal RatePerQty { get; set; }
        [DataMember]
        public Decimal DiscountPerQty { get; set; }
        [DataMember]
        public int DiscountType { get; set; }
        [DataMember]
        public Decimal TotalCPBeforeDisc { get; set; }
        [DataMember]
        public string Temperature { get; set; }
    }
}
