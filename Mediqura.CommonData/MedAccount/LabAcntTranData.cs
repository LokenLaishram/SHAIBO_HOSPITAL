using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedAccount
{
    public class LabAcntTranData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string VoucherNo { get; set; }
        [DataMember]
        public int TransactionTypeID { get; set; }
        [DataMember]
        public string TransactionTypeName { get; set; }
        [DataMember]
        public Decimal TransactionAmount { get; set; }
        [DataMember]
        public string TransactionNaration { get; set; }
        [DataMember]
        public DateTime TransactionDate { get; set; }
        [DataMember]
        public DateTime FromDate { get; set; }
        [DataMember]
        public DateTime ToDate { get; set; }
        [DataMember]
        public int AccountStatusID { get; set; }
        [DataMember]
        public int NoOfIncomeTran { get; set; }
        [DataMember]
        public int NoOfExpTran { get; set; }
        [DataMember]
        public Decimal TotalIncome { get; set; }
        [DataMember]
        public Decimal TotalExpenditure { get; set; }
        [DataMember]
        public string Remarks { get; set; }
    }
}
