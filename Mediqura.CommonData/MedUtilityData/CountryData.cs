using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class CountryData:BaseData
    {
        [DataMember]
        public Int32 CountryID { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string Countrydescp { get; set; }
        [DataMember]
        public string Remarks { get; set; }
      
    }
    public class CountryDatatoEXCEL
    {
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string Countrydescp { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
    }
}
