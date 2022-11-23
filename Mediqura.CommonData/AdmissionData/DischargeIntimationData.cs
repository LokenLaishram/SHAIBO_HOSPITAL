using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Mediqura.CommonData.AdmissionData
{
    public class DischargeIntimationData : BaseData
    {
        [DataMember]
        public Int64 DischargeID { get; set; }
        [DataMember]
        public int ID { get; set; }
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
        public DateTime AdmissionDate { get; set; }
        [DataMember]
        public DateTime IntimationDate { get; set; }
        [DataMember]
        public DateTime IntimationTime { get; set; }
       
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string consultant { get; set; }
        [DataMember]
        public string DischargeDoc { get; set; }
        [DataMember]
        public Int64 DischargeintimatedBy { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
    }
    public class DischargeListDataTOeXCEL
    {
        [DataMember]
        public string IPNo { get; set; }
        
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public DateTime IntimationDate { get; set; }
        [DataMember]
        public string DischargeDoc { get; set; }
       
    }
}
