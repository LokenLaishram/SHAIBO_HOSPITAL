using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class LabTemplateData : BaseData
    {
        [DataMember]
        public Int32 ID { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Descriptions { get; set; }
        [DataMember]
        public string TemplateName { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string IPNo { get; set; }
    }
    public class LabTemplateDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Descriptions { get; set; }
        [DataMember]
        public string TemplateName { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
}
