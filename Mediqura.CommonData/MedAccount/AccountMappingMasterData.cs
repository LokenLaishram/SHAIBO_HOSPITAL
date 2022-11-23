using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedAccount
{
   public class AccountMappingMasterData : BaseData
    {

        [DataMember]
        public int ServiceType { get; set; }
        [DataMember]
        public int GroupType { get; set; }
        [DataMember]
        public int SubServiceType { get; set; }
        [DataMember]
        public int MappingType { get; set; }
        [DataMember]
        public string SubServiceTypeName { get; set; }
        [DataMember]
        public string ServiceTypeName { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public int ServiceID { get; set; }
        [DataMember]
        public int DebitID { get; set; }
        [DataMember]
        public int CreditID { get; set; }
        [DataMember]
        public int DebitMappingStatus { get; set; }
        [DataMember]
        public int CreditMappingStatus { get; set; }
        [DataMember]
        public string DebitAccount { get; set; }
        [DataMember]
        public string CreditAccount { get; set; }
        [DataMember]
        public string DebitAccountdata { get; set; }
        [DataMember]
        public string CreditAccountdata { get; set; }
        

    }
   public class AccountMappingMasterExcelData
   {

       [DataMember]
       public string ServiceTypeName { get; set; }
       [DataMember]
       public string ServiceName { get; set; }
       [DataMember]
       public string DebitAccount { get; set; }
       [DataMember]
       public string CreditAccount { get; set; }

   }
}
