using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Mediqura.CommonData.MedUtilityData
{
    public class DistrictData:BaseData
    {
        [DataMember]
        public Int32 DistrictID { get; set; }
        [DataMember]
        public string DistrictCode { get; set; }
        [DataMember]
        public string DistrictDescp { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public Int32 CountryID { get; set; }
        [DataMember]
        public Int32 StateID { get; set; }
        [DataMember]
        public string Remarks { get; set; }
    }
    public class DistrictDatatoExcel
    {
        [DataMember]
        public Int32 DistrictID { get; set; }
        [DataMember]
        public string DistrictCode { get; set; }
        [DataMember]
        public string DistrictDescp { get; set; }
         [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
    }
}
