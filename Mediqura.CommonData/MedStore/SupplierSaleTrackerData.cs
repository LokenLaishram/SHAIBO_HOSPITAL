using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.Common;
using System.Runtime.Serialization;

namespace Mediqura.CommonData.MedStore
{
    public class SupplierSaleTrackerData :  BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 SupplierID { get; set; }
        [DataMember]
        public string SupplierName { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public Int64 ItemID { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string BatchNo { get; set; }
        [DataMember]
        public int FreeUnit { get; set; }
        [DataMember]
        public int TotalUnitRecieve { get; set; }
        [DataMember]
        public int TotalUnitAvailable { get; set; }
        [DataMember]
        public decimal TotalUnitSales { get; set; }
        [DataMember]
        public decimal RatePerUnit { get; set; }
        [DataMember]                            
        public decimal MRPperUnit { get; set; }
        [DataMember]
        public decimal TotalSalesRateAmnt { get; set; }
        [DataMember]
        public decimal TotalSalesMRPAmt { get; set; }

    }
}
