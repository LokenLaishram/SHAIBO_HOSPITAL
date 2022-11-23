using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
	public class TransactionSummaryData : BaseData
	{
		[DataMember]
		public int TransactionID { get; set; }
		[DataMember]
		public int AccountID { get; set; }
		[DataMember]
		public int AccountState { get; set; }
		[DataMember]
		public DateTime AddedDTM { get; set; }
		[DataMember]
		public string Particulars { get; set; }
		[DataMember]
		public DateTime DateFrom { get; set; }
		[DataMember]
		public DateTime DateTo { get; set; }
		[DataMember]
		public string VoucherNo { get; set; }
		[DataMember]
		public string BillNo { get; set; }
		[DataMember]
		public string EmpName { get; set; }
		[DataMember]
		public Decimal amount { get; set; }
		[DataMember]
		public String PaymentMode { get; set; }
		[DataMember]
		public string Remarks { get; set; }
		[DataMember]
		public string TransactionTypeName { get; set; }
		[DataMember]
		public int TransactionType { get; set; }
		[DataMember]
		public string VoucherDate { get; set; }
		[DataMember]
		public Decimal TotalIncome { get; set; }
		[DataMember]
		public Decimal TotalExpense { get; set; }
		[DataMember]
		public Decimal TotalBalance { get; set; }
		[DataMember]
		public Decimal CashExpense { get; set; }
		[DataMember]
		public Decimal BankExpense { get; set; }
		[DataMember]
		public Decimal CashIncome { get; set; }
		[DataMember]
		public Decimal BankIncome { get; set; }
		[DataMember]
		public int noofIncometran { get; set; }
		[DataMember]
		public int noofincomecashtran { get; set; }
		[DataMember]
		public int noofincomebanktran { get; set; }
		[DataMember]
		public int noofexpensestran { get; set; }
		[DataMember]
		public int noofexpensescashtran { get; set; }
		[DataMember]
		public int noofexpensesbanktran { get; set; }
	}
}
