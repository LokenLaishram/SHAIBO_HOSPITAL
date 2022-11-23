using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
    public class ItemMasterData : BaseData
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
        public int PhrUnitID { get; set; } // Subgroup ID
        [DataMember]
        public string PhrUnit { get; set; } // Subgroup ID
        [DataMember]
        public Int32 PHRSoundsID { get; set; }
        [DataMember]
        public string PHRSounds { get; set; }
        [DataMember]
        public Int32 PHRLooksID { get; set; }
        [DataMember]
        public string PHRLooks { get; set; }
        [DataMember]
        public Int32 MfgCompanyID { get; set; }
        [DataMember]
        public string CompanyName { get; set; }
        [DataMember]
        public Int32 VEDcatgID { get; set; }
        [DataMember]
        public string VEDcatg { get; set; }
    }
    public class ItemMasterDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Groups { get; set; }
        [DataMember]
        public string SubGroup { get; set; }
        [DataMember]
        public string PHRSounds { get; set; }
        [DataMember]
        public string PHRLooks { get; set; }
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
        [DataMember]
        public DateTime AddedDate { get; set; }
    }

}
