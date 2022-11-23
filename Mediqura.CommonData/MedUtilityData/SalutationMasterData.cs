using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class SalutationMasterData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
       [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Salutation { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public int GenderID { get; set; }
        [DataMember]
        public string Remarks { get; set; }
  
    }
    public class SalutationDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Salutation { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public string EmpName { get; set; }
    }
}