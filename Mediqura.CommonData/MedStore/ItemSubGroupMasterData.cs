using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
    public class ItemSubGroupMasterData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int GroupID { get; set; }
        [DataMember]
        public string ItemGroupType { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string ItemSubGroupType { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpName { get; set; }
    }
    public class ItemSubGroupMasterDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string ItemGroupType { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string ItemSubGroupType { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
}
