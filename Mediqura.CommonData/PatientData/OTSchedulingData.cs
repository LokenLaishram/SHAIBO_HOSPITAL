using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.PatientData
{
    public class OTSchedulingData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string OTNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string EmrgNo { get; set; }
        [DataMember]
        public Int32 DepartmentID { get; set; }
        [DataMember]
        public Int32 DoctorTypeID { get; set; }
        [DataMember]
        public Int64 DoctorID { get; set; }
        [DataMember]
        public string DoctorName { get; set; }
        [DataMember]
        public string Surgeon { get; set; }
        [DataMember]
        public Int32 PACID { get; set; } 
        [DataMember]
        public string PAC { get; set; } 
        [DataMember]
        public Int32 TheatreID { get; set; }    
        [DataMember]
        public Int32 CaseID { get; set; }
        [DataMember]
        public string OperationName { get; set; } 
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public Int32 RowNo { get; set; }       
        [DataMember]
        public string OpernDate { get; set; }
        [DataMember]
        public DateTime Date { get; set; }      
        [DataMember]
        public string OpernTime { get; set; }
        [DataMember]
        public string OTStartTime { get; set; }
        [DataMember]
        public int StartHour { get; set; }
        [DataMember]
        public int StartMinute { get; set; }
        [DataMember]
        public int StartMeridiem { get; set; }
        [DataMember]
        public string OTEndTime { get; set; }
        [DataMember]
        public int EndHour { get; set; }
        [DataMember]
        public int EndMinute { get; set; }
        [DataMember]
        public int EndMeridiem { get; set; }
        [DataMember]
        public Int32 OTStatusID { get; set; }
        [DataMember]
        public string OTStatus { get; set; }
        [DataMember]
        public string WardBedName { get; set; }
        [DataMember]
        public Int32 AnaesthetistID { get; set; }
        [DataMember]
        public Int32 ConsultantID { get; set; }
        [DataMember]
        public string AnaesthtistName { get; set; }
        [DataMember]
        public string Theatre { get; set; }
        [DataMember]
        public string Cases { get; set; }
        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public Int32 IsSubHeading { get; set; }
       
    }
    public class OTSchedulingDataTOeXCEL
    {
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string Surgeon { get; set; }
        [DataMember]
        public string OpernDate { get; set; }
        [DataMember]
        public string OpernTime { get; set; }
        [DataMember]
        public string Theatre { get; set; }
        [DataMember]
        public string Cases { get; set; }
    }
}
