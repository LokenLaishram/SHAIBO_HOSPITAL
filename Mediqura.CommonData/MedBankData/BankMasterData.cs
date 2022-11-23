using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedBankData
{
    public class BankMasterData:BaseData
    {


        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int32 BankID { get; set; }
        [DataMember]
        public string BankName{ get; set; }
        [DataMember]
        public DateTime IssueDate { get; set; }
        [DataMember]
        public Int32 ChequeNoFrom { get; set; }
        [DataMember]
        public Int32 ChequeNoTo { get; set; }
        [DataMember]
        public Int32 ChequeNo { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
         [DataMember]
        public string Remarks { get; set; }
    }
    public class BankMasterDataExcel
    {
        public string BankName { get; set; }
        [DataMember]
        public DateTime IssueDate { get; set; }
        [DataMember]
        public Int32 ChequeNoFrom { get; set; }
        [DataMember]
        public Int32 ChequeNoTo { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
    
}
