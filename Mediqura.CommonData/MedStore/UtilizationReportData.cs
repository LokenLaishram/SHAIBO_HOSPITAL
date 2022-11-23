using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
    public class UtilizationReportData :BaseData
    {
        [DataMember]
        public int FinancialYearID { get; set; }
        [DataMember]
        public int ItemID { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public int GenStockID { get; set; }
        [DataMember]
        public string StockNo { get; set; }
        [DataMember]
        public int MonthID { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public Int64 OpeningStock { get; set; }
        [DataMember]
        public Int64 TotalRecivedQty { get; set; }
        [DataMember]
        public Int64 TotalAvailable { get; set; }
        [DataMember]
        public Int64 TotalQuantityUsed { get; set; }
        [DataMember]
        public Int64 TotalCondemnQty { get; set; }
        [DataMember]
        public Int64 TotalReturnQty { get; set; }
        [DataMember]
        public Int64 TotalBalanceQty { get; set; }       
        [DataMember]
        public Int64 AddedBy { get; set; }       
        [DataMember]
        public string ModifiedBy { get; set; } 
        [DataMember]
        public DateTime ModifiedDate { get; set; }
       
    }
}
