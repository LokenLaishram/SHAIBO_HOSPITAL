using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
     [Serializable]
    public class VendorStockReturnData:BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string ReceiptNo { get; set; }
        [DataMember]
        public string VendorReturnNo { get; set; }
        [DataMember]
        public string StockNo { get; set; }
        [DataMember]
        public string BatchNo { get; set; }
        [DataMember]
        public Int64 ItemID { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public int RecievedQty { get; set; }
        [DataMember]
        public int AvialableQty { get; set; }   
        [DataMember]
        public int ReturnQty { get; set; }
        [DataMember]
        public decimal ReturnAmount { get; set; } 
        [DataMember]
        public decimal TotalReturnAmount { get; set; }  
        [DataMember]
        public int NoUnit { get; set; }
        [DataMember]
        public decimal QtyPerUnit { get; set; }
        [DataMember]
        public decimal RatePerQty { get; set; }
        [DataMember]
        public decimal CPPerQty { get; set; }
        [DataMember]
        public int TotalRecievedQty { get; set; }  
        [DataMember]
        public int TotalBalanceQty { get; set; }
        [DataMember]
        public int GrandTotalReturnQty { get; set; }
        [DataMember]
        public decimal GrandTotalReturnAmount { get; set; }
        [DataMember]
        public Int64 RecievedBy { get; set; }
        [DataMember]
        public int MfgCompanyID { get; set; }
        [DataMember]
        public string MfgCompanyName { get; set; }
        [DataMember]
        public int SupplierID { get; set; }
        [DataMember]
        public string SupplierName { get; set; }
        [DataMember]
        public DateTime RecievedDate { get; set; }
        [DataMember]
        public DateTime ExpiryDate { get; set; }  
        [DataMember]
        public string EmpName { get; set; } 
        [DataMember]
        public string ReturnRemark { get; set; } 
       
    }
}
