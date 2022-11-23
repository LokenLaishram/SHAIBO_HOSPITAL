using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedMedication
{
    public class MedicationChartData : BaseData
    {

        [DataMember]
        public Int64 ID { get; set; }  
        [DataMember]
        public string MedCNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }         
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]        
        public string IPPatientName { get; set; }
        [DataMember]
        public string Address { get; set; }   
        [DataMember]
        public double Height { get; set; }
        [DataMember]
        public double Weight { get; set; }
        [DataMember]
        public string GenderName { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public DateTime DOA { get; set; }
        [DataMember]
        public string DOAdmission { get; set; }
        [DataMember]
        public string WardBedNo { get; set; }
        [DataMember]
        public int DrugID { get; set; }
        [DataMember]
        public string DrugName { get; set; }
        [DataMember]
        public string Frequency { get; set; }
        [DataMember]
        public int RouteID { get; set; }
        [DataMember]
        public string RouteName { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public string StartDate2 { get; set; }
        [DataMember]
        public Int64 DoctorID { get; set; }
        [DataMember]
        public string DoctorName { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public int Status { get; set; }
        //-----TAB2-------//
        [DataMember]
        public DateTime MedicationDate { get; set; }
        [DataMember]
        public string MedicationTime { get; set; }
        [DataMember]
        public string MedicationNo { get; set; }
        [DataMember]
        public int MonthID { get; set; }
      
       
    }
}
