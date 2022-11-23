using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class PaymentTypeMasterData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int BankID { get; set; }
        [DataMember]
        public string PaymentTypeCode { get; set; }
        [DataMember]
        public string PaymentType { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpName { get; set; }
         [DataMember]
        public string LedgerName { get; set; }
    }
    public class PaymentTypeDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string PaymentTypeCode { get; set; }
        [DataMember]
        public string PaymentType { get; set; }
        [DataMember]
        public string LedgerName { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
}
