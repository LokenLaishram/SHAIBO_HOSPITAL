using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.Common
{
   public class PatientQRdata
    {
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string DOB { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public string IPNo { get; set; }
      

    }
}
