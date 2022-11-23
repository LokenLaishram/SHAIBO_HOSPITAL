using System;
using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class BedMasterData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int BlockID { get; set; }
        [DataMember]
        public int FloorID { get; set; }
        [DataMember]
        public int WardID { get; set; }
        [DataMember]
        public string Block { get; set; }
        [DataMember]
        public string Floor1 { get; set; }
        [DataMember]
        public string Ward { get; set; }
        [DataMember]
        public string Room { get; set; }
        [DataMember]
        public string BedNo { get; set; }
        [DataMember]
        public Decimal Charges { get; set; }
        [DataMember]
        public Decimal NuCharges { get; set; }
        [DataMember]
        public Decimal Consumable { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public int BedStatus { get; set; }
        
    }
    public class BedDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Block { get; set; }
        [DataMember]
        public string Floor1 { get; set; }
        [DataMember]
        public string Ward { get; set; }
        [DataMember]
        public string Room { get; set; }
        [DataMember]
        public string BedNo { get; set; }
        [DataMember]
        public Decimal Charges { get; set; }
        [DataMember]
        public Decimal NuCharges { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
    }
    public class BedDasboard
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]  
        public string Ward { get; set; }
        [DataMember]
        public string Room { get; set; }
        [DataMember]
        public string BedNo { get; set; }
        [DataMember]
        public int BedStatus { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public int IsOT { get; set; }
        [DataMember]
        public int IsActivePatient { get; set; }

    }
}


