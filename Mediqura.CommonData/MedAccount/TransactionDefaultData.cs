using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedAccount
{
    public class TransactionDefaultData:BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public String PaymentMode { get; set; }
        [DataMember]
        public int BankGroupID { get; set; }
        
        [DataMember]
        public String Remarks { get; set; }
        [DataMember]
        public String LedgerName { get; set; }
    }
    public class TransactionDefaultDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public String PaymentMode { get; set; }
        [DataMember]
        public String AddedBy { get; set; }
        [DataMember]
        public String LedgerName { get; set; }
    }
}


