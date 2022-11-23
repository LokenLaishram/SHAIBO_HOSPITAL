using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedPharData
{
    public class Phr_InterStockTransferData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 ITNo { get; set; }
        [DataMember]
        public string InterTransferNo { get; set; }
        [DataMember]
        public Int64 ItemID { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string Composition { get; set; }
        [DataMember]
        public int MedSubStockID { get; set; }
        [DataMember]
        public Int64 SubStockID { get; set; }
        [DataMember]
        public string StockNo { get; set; }
        [DataMember]
        public decimal CPperQty { get; set; }  
        [DataMember]
        public decimal MRPperUnit { get; set; }
        [DataMember]
        public decimal MRPperQty { get; set; }
        [DataMember]
        public decimal NoUnitBalance { get; set; }
        [DataMember]
        public Int32 EquivalentQtyBalance { get; set; }
        [DataMember]
        public Int32 EquivalentQtyPerUnit { get; set; }
        [DataMember]
        public int ExpireDays { get; set; }

        //----TRANSFER DETAILS-----//
        [DataMember]
        public int TransferFromMedSubStockID { get; set; }
        [DataMember]
        public int TransferToMedSubStockID { get; set; }
        [DataMember]
        public string TransferFromName { get; set; }
        [DataMember]
        public string TransferToName { get; set; }
        [DataMember]
        public int TransferQty { get; set; }
        [DataMember]
        public int TotalTransferQty { get; set; }
        [DataMember]
        public string TransferRemarks { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public int TransferBy { get; set; }
        [DataMember]
        public string TransferByName { get; set; }
        [DataMember]
        public int GrandTotalTransferQty { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public object XMLData { get; set; }
    }
}
