using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedAccount
{
    public class RecdTransactionData:BaseData
    {
        [DataMember]
        public int AccountID { get; set; }
       
        [DataMember]
        public string Particulars { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string Account { get; set; }
        [DataMember]
        public string VoucherNo { get; set; }
        [DataMember]
        public string PassNo { get; set; }
        [DataMember]
        public string Username { get; set; }
      
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string LedgerType { get; set; }
        [DataMember]
        public string LedgerName { get; set; }
        [DataMember]
        public string TransactionType { get; set; }
        [DataMember]
        public int TransactionTypeID { get; set; }
        [DataMember]
        public string VoucherDate { get; set; }
        [DataMember]
        public string Partlyledger { get; set; }
        [DataMember]
        public decimal CurBalance { get; set; }
        [DataMember]
        public Decimal TotalAmount { get; set; }
        [DataMember]
        public Decimal TotalAmountPaid { get; set; }
        [DataMember]
        public Decimal TotalCashRecieve { get; set; }
        [DataMember]
        public Decimal TotalCashPaid { get; set; }
        [DataMember]
        public Decimal TotalCashContraPaid { get; set; }
        [DataMember]
        public Decimal TotalCashContraRecieve { get; set; }
        [DataMember]
        public int IsManual { get; set; }
        [DataMember]
        public int AccountState { get; set; }

        


    }
    public class RecdTransactionDatatoExcel
    {
        [DataMember]
        public string TransactionType { get; set; }
        [DataMember]
        public string VoucherNo { get; set; }
        [DataMember]
        public string Particulars { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string Partlyledger { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }
        [DataMember]
        public Decimal Amount { get; set; }
    }
}
