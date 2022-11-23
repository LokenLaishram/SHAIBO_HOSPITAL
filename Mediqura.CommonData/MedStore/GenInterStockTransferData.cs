using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
    public class GenInterStockTransferData:BaseData
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
        public int GenStockID { get; set; }
        [DataMember]
        public int TransferFromGenStockID { get; set; }
        [DataMember]
        public int TransferToGenStockID { get; set; }
        [DataMember]
        public string TransferFromName { get; set; }
        [DataMember]
        public string TransferToName { get; set; }
        [DataMember]
        public string StockNo { get; set; }
        [DataMember]
        public Int32 Totavailable { get; set; }
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
         
    }
}
