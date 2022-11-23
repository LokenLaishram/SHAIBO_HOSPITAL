using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class LabSampleData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string LabSampleTypeCode { get; set; }
        [DataMember]
        public string LabSampleType { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string LabUnits { get; set; }

    }
    public class LabSampleTypeDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string LabSampleTypeCode { get; set; }
        [DataMember]
        public string LabSampleType { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }

    }
}
