using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedPharData
{
	public class VendorPaymentData :BaseData
	{
		[DataMember]
		public Int64 ID { get; set; }
		[DataMember]
		public Int64 VendorPayment_ID { get; set; }
		[DataMember]
		public DateTime ItemAddedDate { get; set; }
		[DataMember]
		public DateTime DateFrom { get; set; }
		[DataMember]
		public DateTime DateTo { get; set; }
		[DataMember]
		public int SupplierID { get; set; }
		[DataMember]
		public int PaymentStatus { get; set; }
		[DataMember]
		public string SupplierName { get; set; }
		[DataMember]
		public string ReceiptNo { get; set; }
		[DataMember]
		public string InvoiceNo { get; set; }
		[DataMember]
		public Int64 ItemID { get; set; }
		[DataMember]
		public string ItemName { get; set; }
		[DataMember]
		public string ItemAddedOn { get; set; }
		[DataMember]
		public Decimal Amount { get; set; }
		[DataMember]
		public Decimal PaidAmount { get; set; }
		[DataMember]
		public Decimal DueAmount { get; set; }
		[DataMember]
		public Decimal TotalAmount { get; set; }
		[DataMember]
		public Decimal TotalPaidAmount { get; set; }
		[DataMember]
		public Decimal TotalDueAmount { get; set; }
		[DataMember]
		public Decimal GrandTotalAmount { get; set; }
		[DataMember]
		public Decimal GrandTotalPaidAmount { get; set; }
		[DataMember]
		public Decimal GrandTotalDueAmount { get; set; }
		[DataMember]
		public Int64 PaidBy { get; set; }
		[DataMember]
		public Decimal PayableAmount { get; set; }
		[DataMember]
		public Decimal GrandTotalPayableAmount { get; set; }
		[DataMember]
		public string PaymentNo { get; set; }
		[DataMember]
		public string EmpName { get; set; }
		[DataMember]
		public string Remark { get; set; }
		[DataMember]
		public string PaymentRemark { get; set; }
		[DataMember]
		public string PaymentOn { get; set; }
		[DataMember]
		public string Paymenttype { get; set; }
		[DataMember]
		public string InvoiceNumber { get; set; }
		[DataMember]
		public string ChequeUTRnumber { get; set; }
		[DataMember]
		public string BatchNo { get; set;}
		[DataMember]
		public int TotalRecievedQty { get; set; }
		[DataMember]
		public int TotalFreeQty { get; set; }
		[DataMember]
		public string RecievedBy { get; set; }
		[DataMember]
		public string RecievedDate { get; set; }
		[DataMember]
		public Decimal TotalMRP { get; set; }
		[DataMember]
		public Decimal Discount { get; set; }
		[DataMember]
		public Decimal TotalCP { get; set; }
		[DataMember]
		public Decimal NoUnit { get; set; }
		[DataMember]
		public int QtyPerUnit { get; set; }
		[DataMember]
		public string ExpiryDate { get; set; }
		[DataMember]
		public Decimal MRP { get; set; }
		[DataMember]
		public Decimal Rate { get; set; }
		[DataMember]
		public Decimal Taxable { get; set; }
		[DataMember]
		public Decimal CGST { get; set; }
		[DataMember]
		public Decimal SGST { get; set; }
	}
}
