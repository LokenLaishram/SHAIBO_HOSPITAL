using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
    public class IndentToMainData : BaseData
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
        public int ReqdQty { get; set; }
        [DataMember]
        public string IndentStatus { get; set; }
        [DataMember]
        public string RequestStat { get; set; }
        [DataMember]
        public int StatusID { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public int TotRequestQty { get; set; }
        [DataMember]
        public int TotAccepted { get; set; }
        [DataMember]
        public int TotApproved { get; set; }
        [DataMember]
        public int TotHandOver { get; set; }
        [DataMember]
        public int TotReceived { get; set; }
        [DataMember]
        public Decimal TotalCP { get; set; }
    }
    public class IndentDataToExcel
    {

        [DataMember]
        public string IndentNo { get; set; }
        [DataMember]
        public int TotalRqty { get; set; }
        [DataMember]
        public DateTime IndentRaiseDate { get; set; }
        [DataMember]
        public string RecdBy { get; set; }
        [DataMember]
        public string RequestStat { get; set; }
        [DataMember]
        public string IndentStatus { get; set; }
    }
}
