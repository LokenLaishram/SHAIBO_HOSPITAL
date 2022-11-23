using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class ExpiryReportMasterData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public int PatientType { get; set; }
        [DataMember]
        public int MannerID { get; set; }
        [DataMember]
        public string EmergencyNo { get; set; }
        [DataMember]
        public string Template { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public int  AGE { get; set; }
        [DataMember]
        public string GenderName { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }
     

    }
    public class ExpiryDetailsDatatoExcel
    {
        [DataMember]
        public Int64 ID { get; set; }
     
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public DateTime ReportOn { get; set; }
        [DataMember]
        public string ReportBy { get; set; }
   


    }
}
