using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class DesignationTypeMasterData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string DesignationTypeCode { get; set; }
        [DataMember]
        public string DesignationType { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpName { get; set; }
    }
    public class DesignationtDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string DesignationTypeCode { get; set; }
        [DataMember]
        public string DesignationType { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
    }
}
