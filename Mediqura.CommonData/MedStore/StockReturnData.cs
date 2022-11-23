using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
    public class StockReturnData : BaseData
    {

		[DataMember]
		public string ReceiptNo { get; set; }
		[DataMember]
		public Int32 CompanyID { get; set; }
		[DataMember]
		public string BatchNo { get; set; }
		[DataMember]
		public Double SGST { get; set; }
		[DataMember]
		public Double CGST { get; set; }
		[DataMember]
		public Double IGST { get; set; }
		[DataMember]
		public Decimal CPafterTax { get; set; }
		[DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        
		public Int64 ReturnID { get; set; }
        [DataMember]
        public Int64 ItemID { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public Int64 StockID { get; set; }
        [DataMember]
        public int SerialID { get; set; }
        [DataMember]
        public Int64 SubStockID { get; set; }
        [DataMember]
        public string ReturnNo { get; set; }
        [DataMember]
        public string StockNo { get; set; }
        [DataMember]
        public Int64 TotalReceivedQuantity { get; set; }
        [DataMember]
        public Int64 Available { get; set; }
        [DataMember]
        public Int64 Return { get; set; }
        [DataMember]
        public Int64 HandOver { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public Int32 NoReturn { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public Int32 TotalReturnQty { get; set; }
        [DataMember]
        public string HandedTo { get; set; }
        [DataMember]
        public Int64 ReturnByID { get; set; }
        [DataMember]
        public string ReturnBy { get; set; }
        [DataMember]
        public Int64 TotalReturn { get; set; }
        [DataMember]
        public DateTime ReturnDate { get; set; }
        [DataMember]
        public Int32 SupplierID { get; set; }
        [DataMember]
        public string SupplierName { get; set; }
        [DataMember]
        public Int32 FilterTypeID { get; set; }
        [DataMember]
        public string FilterTypeName { get; set; }
        [DataMember]
        public DateTime ExpiredFrom { get; set; }
        [DataMember]
        public DateTime ExpiredTo { get; set; }
        [DataMember]
        public Decimal CPPerQty { get; set; }
        [DataMember]
        public DateTime ExpiryDate { get; set; }
        [DataMember]
        public Int32 TotalBalanceQty { get; set; }
        [DataMember]
        public Int32 ReturnQty { get; set; }
        [DataMember]
        public Decimal ReturnPrice { get; set; }
        [DataMember]
        public Decimal TotalReturnPrice { get; set; }
		[DataMember]
		public decimal TaxableAmount { get; set; }
		[DataMember]
		public decimal Payableamt { get; set; }
	}
    public class StockItemReturnDataTOeXCEL
    {
        [DataMember]
        public string ReturnNo { get; set; }
        [DataMember]
        public Int32 TotalReturnQty { get; set; }
        [DataMember]
        public string ReturnBy { get; set; }
        [DataMember]
        public string HandedTo { get; set; }
        [DataMember]
        public DateTime ReturnDate { get; set; }


    }
}
