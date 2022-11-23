using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedAccount
{
   public class AccountTransactionData : BaseData
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
        

        


     
    }
   public class AccountTransactionOutput {
       [DataMember]
       public int outputdata { get; set; }
       [DataMember]
       public string voucher { get; set; }
   }

    public class TransactionDetailData
    {
        [DataMember]
        public string LedgerType { get; set; }
        [DataMember]
        public string Ledger { get; set; }
        [DataMember]
        public decimal amount { get; set; }
    }

    public class AccountVoucherData
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
        public List<TransactionDetailData> TransactionDetails { get; set; }
    }

}
