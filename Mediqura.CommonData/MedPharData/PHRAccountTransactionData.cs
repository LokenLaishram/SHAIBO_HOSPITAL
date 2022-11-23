using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedPharData
{
	public class PHRAccountTransactionData : BaseData
	{
		[DataMember]
		public int ID { get; set; }
		[DataMember]
		public decimal DebitAmount { get; set; }
		[DataMember]
		public decimal CreditAmount { get; set; }
		[DataMember]
		public decimal TotalDebit { get; set; }
		[DataMember]
		public decimal TotalCredit { get; set; }
		[DataMember]
		public decimal TotalDiff { get; set; }
		[DataMember]
		public string VoucherNo { get; set; }
		[DataMember]
		public DateTime Date { get; set; }
		[DataMember]
		public string Naration { get; set; }
		[DataMember]
		public int PaymentType { get; set; }
		[DataMember]
		public int PaymentMode { get; set; }
		[DataMember]
		public int TransactionType { get; set; }
		[DataMember]
		public string InstrumentName { get; set; }
		[DataMember]
		public DateTime InstrumentDate { get; set; }
		[DataMember]
		public string BankPayeeName { get; set; }
		[DataMember]
		public string BankBranchName { get; set; }
		[DataMember]
		public string CrossInt { get; set; }
		[DataMember]
		public string CreditXml { get; set; }
		[DataMember]
		public string DebitXml { get; set; }
		[DataMember]
		public int CreditID { get; set; }
		[DataMember]
		public int DebitID { get; set; }
		[DataMember]
		public decimal Amount { get; set; }
		[DataMember]
		public int TransactionID { get; set; }
		[DataMember]
		public int AccountID { get; set; }
		[DataMember]
		public int AccountState { get; set; }
		[DataMember]
		public DateTime DateFrom { get; set; }
		[DataMember]
		public DateTime DateTo { get; set; }
		[DataMember]
		public string TransactionTypeName { get; set; }
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
		[DataMember]
		public string BillNo { get; set; }
		[DataMember]
		public string Particulars { get; set; }
		[DataMember]
		public string Payment { get; set; }
		[DataMember]
		public DateTime AddedDTM { get; set; }
	}
	public class PHRAccountTransactionOutput
	{
		[DataMember]
		public int outputdata { get; set; }
		[DataMember]
		public string voucher { get; set; }
	}

	public class PHRTransactionDetailData
	{
		[DataMember]
		public string LedgerType { get; set; }
		[DataMember]
		public string Ledger { get; set; }
		[DataMember]
		public decimal amount { get; set; }
	}

	public class PHRAccountVoucherData
	{
		[DataMember]
		public string voucher { get; set; }
		[DataMember]
		public string voucherType { get; set; }
		[DataMember]
		public int TransType { get; set; }
		[DataMember]
		public decimal amount { get; set; }
		[DataMember]
		public string Partlyledger { get; set; }
		[DataMember]
		public string TransactionDate { get; set; }
		[DataMember]
		public string Narration { get; set; }
		[DataMember]
		public List<PHRTransactionDetailData> TransactionDetails { get; set; }
	}
}
