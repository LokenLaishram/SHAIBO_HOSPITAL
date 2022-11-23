using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.AdmissionData
{
    public class IPCreditMasterData: BaseData
    {          
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string PatientAddress { get; set; }
        [DataMember]
        public DateTime AdmissionDate { get; set; }
        [DataMember]
        public Decimal TotalOutstandingBill { get; set; }
        [DataMember]
        public Decimal DepositAmount { get; set; }
        [DataMember]
        public Decimal CreditLimit { get; set; }

    }
}
        