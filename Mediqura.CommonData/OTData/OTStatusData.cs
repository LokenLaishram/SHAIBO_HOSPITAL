using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.OTData
{
    public class OTStatusData : BaseData
    {
        [DataMember]
        public Int64 OTRegnID { get; set; }
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string PatientAddress { get; set; }
        [DataMember]
        public string GenderID { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string GenderName { get; set; }
        [DataMember]
        public string AdmissionNo { get; set; }
        [DataMember]
        public DateTime OpernDate { get; set; }
        [DataMember]
        public DateTime OpernDateNext { get; set; }
        [DataMember]
        public DateTime FirstDayYear { get; set; }
        [DataMember]
        public DateTime IntimationDate { get; set; }
        [DataMember]
        public int Otstatus { get; set; }
        [DataMember]
        public string Surgeon { get; set; }
        [DataMember]
        public string AsstSurgeon { get; set; }
        [DataMember]
        public string Nurse { get; set; }
        [DataMember]
        public string consultant { get; set; }
        [DataMember]
        public string CaseName { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public int StatusID { get; set; }
    }
    public class OTStatusListDataTOeXCEL
    {
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string CaseName { get; set; }
        [DataMember]
        public DateTime OpernDate { get; set; }
        [DataMember]
        public string Status { get; set; }
    }
}
