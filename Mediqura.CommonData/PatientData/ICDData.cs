using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.PatientData
{
    public class ICDData:BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public Int64 DiagnosisID { get; set; }
        [DataMember]
        public int ICDCodeID { get; set; }
        [DataMember]
        public string ICDCode { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }
        [DataMember]
        public string EmpName { get; set; }
      
    }
    public class ICDDataDatatoExcel
    {
        [DataMember]
        public Int64 DiagnosisID { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }

    } 
}
