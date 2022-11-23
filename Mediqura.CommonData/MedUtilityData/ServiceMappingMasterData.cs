using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class ServiceMappingMasterData :BaseData
    {
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public int ServicetypeID { get; set; }
        [DataMember]
        public int ServiceID { get; set; }
        [DataMember]
        public string Servicetype { get; set; }
        [DataMember]
        public int GroupType { get; set; }
        [DataMember]
        public int MappingType { get; set; }
        [DataMember]
        public int SubServicetype { get; set; }
        [DataMember]
        public Int64 ID { get; set; }
    
    }
}
