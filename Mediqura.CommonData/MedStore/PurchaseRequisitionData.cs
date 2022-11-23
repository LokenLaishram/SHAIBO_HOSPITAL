using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.Common;
using System.Runtime.Serialization;
namespace Mediqura.CommonData.MedStore
{
    public class PurchaseRequisitionData : BaseData
    {
        [DataMember]
        public int ItemID { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string ItemNameShort { get; set; }
        [DataMember]
        public string UnitDescription { get; set; }
        [DataMember]
        public string AttachmentFile { get; set; }
        [DataMember]
        public int PurchaseRequisitionTypeID { get; set; }
        [DataMember]
        public string PurchaseRequisitionTypeName { get; set; }
        [DataMember]
        public int PurchaseRequisitionQuantity { get; set; }
        [DataMember]
        public int TotalApprovedQuantity { get; set; }
        [DataMember]
        public int RecievedQuantity { get; set; }
        [DataMember]
        public int TotalAvailableQuantity { get; set; }
        [DataMember]
        public decimal ProbableRate { get; set; }
        [DataMember]
        public string RQNumber { get; set; }
        [DataMember]
        public int ItemStatusID { get; set; }
        [DataMember]
        public string ItemStatusName { get; set; }
        [DataMember]
        public int RQStatusID { get; set; }
        [DataMember]
        public string RQStatusName { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string RequestedBy { get; set; }
        [DataMember]
        public DateTime RequestedDate { get; set; }
        [DataMember]
        public string ApprovedBy { get; set; }
        [DataMember]
        public DateTime ApprovedDate { get; set; }
        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public string PONumber { get; set; }
        [DataMember]
        public int POStatusID { get; set; }
        [DataMember]
        public string POStatusName { get; set; }
        [DataMember]
        public string PurchaeOrderBy { get; set; }
        [DataMember]
        public DateTime PurchaseOrderDate { get; set; }
        [DataMember]
        public int SupplierID { get; set; }
        [DataMember]
        public string SupplierName { get; set; }
        [DataMember]
        public int RecievedStatus { get; set; }
    }
    public class PurchaseRequisitionDataToExcel
    {
        [DataMember]
        public string RQNumber { get; set; }
        [DataMember]
        public int ItemID { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string UnitDescription { get; set; }
        [DataMember]
        public int TotalAvailableQuantity { get; set; }
        [DataMember]
        public string PurchaseRequisitionTypeName { get; set; }
        [DataMember]
        public int PurchaseRequisitionQuantity { get; set; }
        [DataMember]
        public decimal ProbableRate { get; set; }
        [DataMember]
        public string RQStatusName { get; set; }
        [DataMember]
        public string RequestedBy { get; set; }
        [DataMember]
        public string ApprovedBy { get; set; }
    }
}
