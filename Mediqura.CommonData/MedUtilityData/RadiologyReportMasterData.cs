using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
  public class RadiologyReportMasterData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int LabSubGroupId { get; set; }
        [DataMember]
        public int TestId { get; set; }
        [DataMember]
        public int Gender { get; set; }
        [DataMember]
        public string Template { get; set; }
     
    }
}
