using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedHrData
{
    public class RunnerDetailsData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string RunnerCode { get; set; }
        [DataMember]
        public string RunnerName { get; set; }
        [DataMember]
        public string RunnerAddress { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public Decimal RunnerTax { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmailID { get; set; }

    }
}
