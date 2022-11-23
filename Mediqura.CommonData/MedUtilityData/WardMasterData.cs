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
    public class WardMasterData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int BlockID { get; set; }
        [DataMember]
        public int FloorID { get; set; }
        [DataMember]
        public string Block { get; set; }
        [DataMember]
        public string Floor1 { get; set; }
        [DataMember]
        public string WardTypeCode { get; set; }
        [DataMember]
        public string WardType { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public int Admnchargeapplied { get; set; }
        [DataMember]
        public string admnchargestatus { get; set; }
        [DataMember]
        public Decimal PHRloweLimit { get; set; }
        [DataMember]
        public Decimal PHRupperLimit { get; set; }
    }
    public class WardDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Block { get; set; }
        [DataMember]
        public string Floor1 { get; set; }
        [DataMember]
        public string WardTypeCode { get; set; }
        [DataMember]
        public string WardType { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
    }

