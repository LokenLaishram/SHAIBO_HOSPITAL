using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class CommonSubGroupMasterData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int GroupID { get; set; }
        [DataMember]
        public string Group1 { get; set; }
        [DataMember]
        public string SubGroupCode { get; set; }
        [DataMember]
        public string SubGroupType { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpName { get; set; }
    }
    public class CommonSubGroupMasterDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Group1 { get; set; }
        [DataMember]
        public string SubGroupCode { get; set; }
        [DataMember]
        public string SubGroupType { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
}
