using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace Mediqura.CommonData.MedStore
{
    public class GenItemMasterData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int GroupID { get; set; } //  Groups ID
        [DataMember]
        public string Groups { get; set; }
        [DataMember]
        public int SubGroupID { get; set; } // Subgroup ID
        [DataMember]
        public string SubGroup { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public int DaycountStart { get; set; }
        [DataMember]
        public int DaycountEnd { get; set; }
        [DataMember]
        public Int32 PhrUnitID { get; set; } // Subgroup ID
        [DataMember]
        public string PhrUnit { get; set; } // Subgroup ID
        [DataMember]
        public Int32 GenRackID { get; set; }
        [DataMember]
        public string GenRack { get; set; }
        [DataMember]
        public Int32 GenSubRackID { get; set; }
        [DataMember]
        public string GenSubRack { get; set; }
        [DataMember]
        public Int32 GenSoundsID { get; set; }
        [DataMember]
        public string GenSounds { get; set; }
        [DataMember]
        public Int32 GenLooksID { get; set; }
        [DataMember]
        public string GenLooks { get; set; }
        [DataMember]
        public Int32 ItemCategoryID { get; set; }
        [DataMember]
        public string ItemCategory { get; set; }

    }
    public class GENItemMasterDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Groups { get; set; }
        [DataMember]
        public string SubGroup { get; set; }
        [DataMember]
        public string GenSounds { get; set; }
        [DataMember]
        public string GenLooks { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string PhrUnit { get; set; } // Subgroup ID
        [DataMember]
        public int DaycountStart { get; set; }
        [DataMember]
        public int DaycountEnd { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
    }


}
