using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.Common;

namespace Mediqura.CommonData.MedStore
{
    public class StoreProfitStatusData : BaseData
    {
        [DataMember]
        public string ItemName { get; set; }       
        [DataMember]
        public int StockID { get; set; }
        [DataMember]
        public string StockNo { get; set; }
        [DataMember]
        public Decimal TotalCP { get; set; }
        [DataMember]
        public Decimal TotalDiscount { get; set; }
        [DataMember]
        public Decimal FeeItemAmount { get; set; }
        [DataMember]
        public Decimal TotalMRP { get; set; }
        [DataMember]
        public Decimal Tax { get; set; }
        [DataMember]
        public Decimal Discount { get; set; }
        [DataMember]
        public Decimal Profit { get; set; }

        //TOTAL
        public Decimal NetTotalCP { get; set; }
        [DataMember]
        public Decimal NetTotalDiscount { get; set; }
        [DataMember]
        public Decimal NetFeeItemAmount { get; set; }
        [DataMember]
        public Decimal NetTotalMRP { get; set; }
        [DataMember]
        public Decimal NetTotalTax { get; set; }
      
        [DataMember]
        public Decimal NetTotalProfit { get; set; }
        [DataMember] 
        public int CustomerTypeID { get; set; }

        [DataMember]
        public int StockStatusID { get; set; }
        [DataMember]
        public string CustomerTypeName { get; set; }
        [DataMember]
        public int Month { get; set; }
        [DataMember]
        public int Year { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string Remarks { get; set; }           
    }
}
