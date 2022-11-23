using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedNurseData
{
    public class PatientSugarChartData:BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string Ipno { get; set; }
        [DataMember]
        public DateTime RecordDate { get; set; }
        [DataMember]
        public string RBSmgDl { get; set; }
        [DataMember]
        public string remarks { get; set; }
    }
    public class PatientSugarChartDatatoExcel 
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string Ipno { get; set; }
        [DataMember]
        public DateTime date { get; set; }
        [DataMember]
        public string RBSmgDl { get; set; }
        [DataMember]
        public string remarks { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
    }
}
