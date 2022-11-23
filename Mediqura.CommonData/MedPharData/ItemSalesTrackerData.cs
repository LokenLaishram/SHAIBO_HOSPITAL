using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedPharData
{
    public class ItemSalesTrackerData :BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string CustomerType { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string EmgNo { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string Address { get; set; }
           

        //---Item Details---//
        [DataMember]
        public string RecieptNo { get; set; }    
        [DataMember]
        public Int64 SupplierID  { get; set; }  
        [DataMember]
        public Int64 ItemID { get; set; }
        [DataMember]
        public string BatchNo { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string Composition { get; set; }
        [DataMember]
        public int ExpireDays { get; set; }
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
        public decimal SaleEqvQty { get; set; }          
        [DataMember]
        public decimal NetAmount { get; set; }
        [DataMember]
        public decimal NoUnitBalance { get; set; }
        [DataMember]
        public Int32 EquivalentQtyPerUnit { get; set; }         
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string Remarks { get; set; }
		[DataMember]
		public string Supplier { get; set; }
		[DataMember]
        public DateTime Itemaddedon { get; set; }
		[DataMember]
		public int PatientTypeID { get; set; }

        //---------------------------------- //
        [DataMember]
        public decimal ActualSalesAmnt { get; set; }
        [DataMember]
        public decimal ReturnQty { get; set; }
        [DataMember]
        public decimal SalesReturnAmnt { get; set; }

        //-------TOTAL-------------------- //
        [DataMember]
        public decimal TotalSalesQty { get; set; }
        [DataMember]
        public decimal TotalReturnQty { get; set; }
        [DataMember]
        public decimal TotalSalesAmnt { get; set; }
        [DataMember]
        public decimal TotalSalesReturnAmnt { get; set; }
        [DataMember]
        public decimal SUMMRPperQty { get; set; }
        [DataMember]
        public decimal SUMSaleEqvQty { get; set; }
        [DataMember]
        public decimal SUMReturnQty { get; set; }
        [DataMember]
        public decimal SUMNetAmount { get; set; }
        [DataMember]
        public decimal SUMSalesReturnAmnt { get; set; }
       

    }
}
