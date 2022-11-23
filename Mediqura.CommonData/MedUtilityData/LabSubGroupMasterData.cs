using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class LabSubGroupMasterData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int LabGroupID { get; set; }
        [DataMember]
        public string LabGroup { get; set; }
        [DataMember]
        public string SubGroupCode { get; set; }
        [DataMember]
        public string SubGroupType { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpName { get; set; }
    }
    public class LabSubGroupMasterDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string LabGroup { get; set; }
        [DataMember]
        public string SubGroupCode { get; set; }
        [DataMember]
        public string SubGroupType { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
}
