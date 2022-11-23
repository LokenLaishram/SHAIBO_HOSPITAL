using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedAccount
{
    public class LedgerBookData : BaseData
    {
       
        [DataMember]
        public int LedgerID { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public string Ledgername { get; set; }
        [DataMember]
        public string GroupName { get; set; }
        
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
  
    }
    public class LedgerBookMasterDataExcel
    {
        [DataMember]
        public string Ledgername { get; set; }
        [DataMember]
        public string GroupName { get; set; }
        [DataMember]
        public Decimal Amount { get; set; }


    }
  
}
