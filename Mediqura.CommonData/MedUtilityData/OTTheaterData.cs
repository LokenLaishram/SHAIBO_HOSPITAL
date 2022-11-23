using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{

    public class OTTheaterData:BaseData
    {   [DataMember]
        public Int64 OTTheaterID { get; set; }
        [DataMember]
        public string OTTheaterCode { get; set; }
        [DataMember]
        public string OTTheaterdescp { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string IPNo { get; set; }
    }
    public class OTTheaterDatatoExcel
    {
        [DataMember]
        public Int64 OTTheaterID { get; set; }
        [DataMember]
        public string OTTheaterCode { get; set; }
        [DataMember]
        public string OTTheaterdescp { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
}
