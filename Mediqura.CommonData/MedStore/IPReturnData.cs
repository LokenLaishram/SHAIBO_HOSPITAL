using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
	public class IPReturnData : BaseData
	{
		[DataMember]
		public Int64 ID { get; set; }
		[DataMember]
		public Int64 UHID { get; set; }
		[DataMember]
		public string IPNo { get; set; }
		[DataMember]
		public Int64 ReturnID { get; set; }
		[DataMember]
		public Int64 ItemID { get; set; }
		[DataMember]
		public string ItemName { get; set; }
		[DataMember]
		public Int64 StockID { get; set; }
		[DataMember]
		public Int64 SubStockID { get; set; }
		[DataMember]
		public int SerialID { get; set; }
		[DataMember]
		public string ReturnNo { get; set; }
		[DataMember]
		public Int64 StockNo { get; set; }
		[DataMember]
		public decimal Quantity { get; set; }
		[DataMember]
		public decimal RtnQuantity { get; set; }
		[DataMember]
		public decimal Unit { get; set; }
		[DataMember]
		public decimal Return { get; set; }
		[DataMember]
		public Int32 NoReturn { get; set; }
		[DataMember]
		public Int64 HandOver { get; set; }
		[DataMember]
		public DateTime DateFrom { get; set; }
		[DataMember]
		public DateTime DateTo { get; set; }
		[DataMember]
		public string Remarks { get; set; }
		[DataMember]
		public string PatientDetails { get; set; }
		[DataMember]
		public string IPReturnItemDetails { get; set; }
		[DataMember]
		public string PatientDetailsList { get; set; }
		[DataMember]
		public string IPReturnItemDetailsList { get; set; }
		[DataMember]
		public decimal totalreturnQty { get; set; }
		[DataMember]
		public string LreturnQty { get; set; }
		[DataMember]
		public decimal TotalReturnQty { get; set; }
		[DataMember]
		public decimal GrandTotalReturnQty { get; set; }
		[DataMember]
		public string ReturnBy { get; set; }
		[DataMember]
		public DateTime ReturnDate { get; set; }
		[DataMember]
		public string IPDrgIssueNo { get; set; }
		[DataMember]
		public decimal MRPperQty { get; set; }
		[DataMember]
		public decimal ReturnAmt { get; set; }
		[DataMember]
		public decimal TotalReturnAmt { get; set; }
		[DataMember]
		public decimal GrandTotalReturnAmt { get; set; }
	}
}
