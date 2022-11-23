using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
    public class GenDeptWiseUsedItemData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 ItemID { get; set; }
        [DataMember]
        public Int64 SubStockID { get; set; }
        [DataMember]
        public Int64 StockID { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public Int32 DeptID { get; set; }
        [DataMember]
        public Int32 UsedItem { get; set; }
        [DataMember]
        public Int32 BalStock { get; set; }
        [DataMember]
        public string IndentNo { get; set; }
        [DataMember]
        public Int32 QtyUsed { get; set; }
        [DataMember]
        public Int32 BalAfterUsed { get; set; }
        [DataMember]
        public string XMLData { get; set; }
        [DataMember]
        public Int32 TotalUsedQty { get; set; }
        [DataMember]
        public string RecordNo { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public Int32 ApprovedQty { get; set; }
        [DataMember]
        public Int64 GenStockID { get; set; }
        [DataMember]
        public string StockNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string IPno { get; set; }
    }
    public class GenDeptWiseUsedItemToExcel
    {
        [DataMember]
        public string RecordNo { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public Int32 TotalUsedQty { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
}
