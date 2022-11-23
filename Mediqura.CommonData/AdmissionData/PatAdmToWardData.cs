using System;
using Mediqura.CommonData.Common;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.AdmissionData
{
    public class PatAdmToWardData:BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int BlockID { get; set; }
        [DataMember]
        public int FloorID { get; set; }
        [DataMember]
        public int WardID { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
 
        [DataMember]
        public int AdmToWardStatusID { get; set; }
        [DataMember]
        public string bedstatus { get; set; }
        [DataMember]
        public string Block { get; set; }
        [DataMember]
        public string Floor1 { get; set; }
        [DataMember]
        public string Ward { get; set; }
        [DataMember]
        public string Room { get; set; }
        [DataMember]
        public string AdmToWardStatus { get; set; }
        [DataMember]
        public string BedNo { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public DateTime AdmToWdDateTime { get; set; }

    }
    public class PatAdmToWardDataToExcel
    {
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string Block { get; set; }
        [DataMember]
        public string Floor1 { get; set; }
        [DataMember]
        public string Ward { get; set; }
        [DataMember]
        public string Room { get; set; }
        [DataMember]
        public string BedNo { get; set; }
        [DataMember]
        public string AdmToWardStatus { get; set; }
    }
}
