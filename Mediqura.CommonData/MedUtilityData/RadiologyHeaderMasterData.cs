using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class RadiologyHeaderMasterData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int GroupID { get; set; }
        [DataMember]
        public string HeaderCode { get; set; }
        [DataMember]
        public string HeaderName { get; set; }
        [DataMember]
        public string Template { get; set; }
        [DataMember]
        public string Remarks { get; set; }
    }
}
