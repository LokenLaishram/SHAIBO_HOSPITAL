using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MIS
{
    public class MisReportData:BaseData
    {
        [DataMember]
        public int LedgerID { get; set; }
        [DataMember]
        public int AccountState { get; set; }
        [DataMember]
        public int IsSubHeading { get; set; }
        [DataMember]
        public DateTime AddedDTM { get; set; }
        [DataMember]
        public string Particulars { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public int AcountNature { get; set; }
        [DataMember]
        public string VoucherNo { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public Decimal amount { get; set; }
        [DataMember]
        public String PaymentMode { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string NatureName { get; set; }
        [DataMember]
        public string LedgerName { get; set; }
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
        public Decimal IncomeExpDiff { get; set; }
        [DataMember]
        public Decimal TotalRecievable { get; set; }
        [DataMember]
        public Decimal TotalDiposit { get; set; }
        [DataMember]
        public Decimal TotalWithdraw { get; set; }
        [DataMember]
        public Decimal CashIncome { get; set; }
        [DataMember]
        public Decimal BankIncome { get; set; }
        [DataMember]
        public Decimal BankExpense { get; set; }
        [DataMember]
        public Decimal cashExpense { get; set; }
        [DataMember]
        public Decimal CashInHand { get; set; }
        [DataMember]
        public Decimal TotalCashIncomeAndAdvance { get; set; }
        [DataMember]
        public Decimal TotalCashDiff { get; set; }
        [DataMember]
        public Decimal TotalCash { get; set; }
    }
    public class MisSummaryReport : BaseData {
        [DataMember]
        public int subHeading { get; set; }
        [DataMember]
        public DateTime SummaryDate { get; set; }
        [DataMember]
        public Decimal CashIncome { get; set; }
        [DataMember]
        public Decimal advanceIncome { get; set; }
        [DataMember]
        public Decimal IncomeAdvanceCash { get; set; }
        [DataMember]
        public Decimal CashExpense { get; set; }
        [DataMember]
        public Decimal BankIncome { get; set; }
        [DataMember]
        public Decimal AdvanceBank { get; set; }
        [DataMember]
        public Decimal IncomeAdvanceBank { get; set; }
        [DataMember]
        public Decimal BankExpense { get; set; }
        [DataMember]
        public Decimal BankWithdraw { get; set; }
        [DataMember]
        public Decimal BankDiposit { get; set; }
        [DataMember]
        public Decimal Recievable { get; set; }
        [DataMember]
        public Decimal AdvanceIncomeCashExpenseDiff { get; set; }
        [DataMember]
        public Decimal BankIncomeAdvanceExpenseDiff { get; set; }
        [DataMember]
        public Decimal CashInHand { get; set; }
        [DataMember]
        public Decimal CashInBank { get; set; }
        [DataMember]
        public Decimal NetIncomeAdvance { get; set; }
        [DataMember]
        public Decimal NetExpense { get; set; }
        [DataMember]
        public Decimal NetIncomeExpenseDiff { get; set; }
        [DataMember]
        public Decimal TotalExpense { get; set; }
        [DataMember]
        public Decimal TotalIncomeExpDiff { get; set; }
        [DataMember]
        public Decimal TotalRecievable { get; set; }
        [DataMember]
        public Decimal TotalDiposit { get; set; }
        [DataMember]
        public Decimal TotalWithdraw { get; set; }
        [DataMember]
        public Decimal TotalCashIncome { get; set; }
        [DataMember]
        public Decimal TotalBankIncome { get; set; }
        [DataMember]
        public Decimal TotalBankExpense { get; set; }
        [DataMember]
        public Decimal TotalcashExpense { get; set; }
        [DataMember]
        public Decimal TotalCashInHand { get; set; }
        [DataMember]
        public Decimal TotalCashIncomeAndAdvance { get; set; }
        [DataMember]
        public Decimal TotalCashDiff { get; set; }
        [DataMember]
        public Decimal TotalCash { get; set; }
        [DataMember]
        public Decimal TotalIncome { get; set; }
        
    }
}
