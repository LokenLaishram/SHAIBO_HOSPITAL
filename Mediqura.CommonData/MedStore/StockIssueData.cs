using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Mediqura.CommonData.MedStore
{
    public class StockIssueData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 ItemID { get; set; }
        [DataMember]
        public Int64 StockID { get; set; }
        [DataMember]
        public Int64 SubStockID { get; set; }
        [DataMember]
        public int SerialID { get; set; }
        [DataMember]
        public int IssueTo { get; set; }
        [DataMember]
        public int TotalQuantity { get; set; }
        [DataMember]
        public string IndentNo { get; set; }
        [DataMember]
        public string BatchNo { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public int QtyPerUnit { get; set; }
        [DataMember]
        public int IssueQuantity { get; set; }
        [DataMember]
        public int TotalBalanceQty { get; set; }
        [DataMember]
        public int TotalIssueQuantity { get; set; }
        [DataMember]
        public int Totalqtyissuedperitem { get; set; }
        [DataMember]
        public Decimal CPPerQty { get; set; }
        [DataMember]
        public Decimal MRPperqty { get; set; }
        [DataMember]
        public Decimal CP { get; set; }
        [DataMember]
        public Decimal MRP { get; set; }
        [DataMember]
        public Decimal TotalCP { get; set; }
        [DataMember]
        public Decimal TotalMRP { get; set; }
        [DataMember]
        public Int64 HandOver { get; set; }
        [DataMember]
        public int StockTypeID { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string StockNo { get; set; }
        [DataMember]
        public int TotalReceivedQuantity { get; set; }
        [DataMember]
        public string Return { get; set; }
        [DataMember]
        public Int32 TotalReturnQuantity { get; set; }
        [DataMember]
        public Int32 GroupID { get; set; }
        [DataMember]
        public Int32 SubGroupID { get; set; }
        [DataMember]
        public string GroupName { get; set; }
        [DataMember]
        public String SubGrpName { get; set; }
        [DataMember]
        public int Unit { get; set; }
        [DataMember]
        public Int64 IssuedByID { get; set; }
        [DataMember]
        public string IssueNo { get; set; }
        [DataMember]
        public Int64 TotalIssuedQty { get; set; }
        [DataMember]
        public Decimal TotalCostPrice { get; set; }
        [DataMember]
        public Decimal TotalMRPS { get; set; }
        [DataMember]
        public string IssuedBy { get; set; }
        [DataMember]
        public string SubStockName { get; set; }
        [DataMember]
        public int TotalQty { get; set; }
        [DataMember]
        public string HandedTo { get; set; }
        [DataMember]
        public DateTime IssueDate { get; set; }
        [DataMember]
        public Int64 IssueID { get; set; }
    }
    public class StockItemIssuedDataTOeXCEL
    {
        [DataMember]
        public string IssueNo { get; set; }
        [DataMember]
        public string IndentNo { get; set; }
        [DataMember]
        public string SubStockName { get; set; }
        [DataMember]
        public int TotalQty { get; set; }
        [DataMember]
        public decimal TotalCP { get; set; }
        [DataMember]
        public decimal TotalMRP { get; set; }
        [DataMember]
        public string HandedTo { get; set; }
        [DataMember]
        public string IssuedBy { get; set; }
        [DataMember]
        public DateTime IssueDate { get; set; }
    }
}
