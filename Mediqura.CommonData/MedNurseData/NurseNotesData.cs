using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedNurseData
{
    public class NurseNotesData : BaseData
    {
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int32 RowNo { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string Particular { get; set; }
        [DataMember]
        public DateTime NotedDate { get; set; }
        [DataMember]
        public DateTime NotedTime { get; set; }
        [DataMember]
        public String NotedTimes { get; set; }
        [DataMember]
        public string NurseName { get; set; }
        [DataMember]
        public Int64 NurseID { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public Int32 Age { get; set; }
        [DataMember]
        public string AgeCount { get; set; }
        [DataMember]
        public string Sex { get; set; }
        [DataMember]
        public DateTime AdmissionDate { get; set; }
        [DataMember]
        public string WardBedName { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string PatientDetails { get; set; }
    }
    public class NurseNotesDataTOeXCEL 
    {
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string Particular { get; set; }
        [DataMember]
        public string NotedDate { get; set; }
        [DataMember]
        public string NotedTime { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
     
    }
}
