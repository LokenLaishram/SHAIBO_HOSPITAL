using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
    public class MfgCompanyMasterData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string MfgCompanyTypeCode { get; set; }
        [DataMember]
        public string MfgCompanyType { get; set; } 
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }
    }
    public class MfgCompanyDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string MfgCompanyTypeCode { get; set; }
        [DataMember]
        public string MfgCompanyType { get; set; } 
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }
    }
}
