using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
    public class CondemnReqApprovedData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 CondemnRegNo { get; set; }
        [DataMember]
        public string CondemnRequestNo { get; set; }
        [DataMember]
        public int ItemID { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public int SubStockID { get; set; }
        [DataMember]
        public int CondemnQty { get; set; }
        [DataMember]
        public string CondemnRemark { get; set; }
        [DataMember]
        public int TotalCondemnQty { get; set; }
        [DataMember]
        public int GrandTotalCondemnQty { get; set; }
        [DataMember]
        public int CondemnRequestBy { get; set; }
        [DataMember]
        public string CondemnRequestByName { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string CondemnOverallRemark { get; set; }
        [DataMember]
        public DateTime MfgDate { get; set; }
        [DataMember]
        public DateTime ExpiryDate { get; set; }
        [DataMember]
        public int ApprovedStatusID { get; set; }
        [DataMember]
        public string ApprovedStatus { get; set; }
        [DataMember]
        public string ApprovedBy { get; set; }
        [DataMember]
        public DateTime ApprovedDate { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string StockNo { get; set; }
        [DataMember]
        public int CondemnStatus { get; set; }
        [DataMember]
        public string CondemnRequestStatus { get; set; }
    }
}
