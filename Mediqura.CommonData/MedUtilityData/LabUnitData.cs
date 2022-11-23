using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class LabUnitData:BaseData
    {
        [DataMember]
        public Int32 UnitID { get; set; }
        [DataMember]
        public string LabUnitCode { get; set; }
        [DataMember]
        public string LabUnitdescp { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string IPNo { get; set; }
    }
    public class LabUniteDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string LabUnitCode { get; set; }
        [DataMember]
        public string LabUnitdescp { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
}
