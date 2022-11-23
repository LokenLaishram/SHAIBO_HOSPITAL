using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class AppoinmentScheduleData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public DateTime date { get; set; }
        [DataMember]
        public string Day { get; set; }
        [DataMember]
        public Int32 DoctorType { get; set; }
        [DataMember]
        public Int32 DepartmentID { get; set; }
        [DataMember]
        public Int64 DoctorID { get; set; }
        [DataMember]
        public Int32 MonthID { get; set; }
        [DataMember]
        public Int32 Year { get; set; }
        [DataMember]
        public Int32 YearID { get; set; }
        [DataMember]
        public string Morning { get; set; }
        [DataMember]
        public string Afternoon { get; set; }
        [DataMember]
        public string Evening { get; set; }
        [DataMember]
        public Int32 MorningSlots { get; set; }
        [DataMember]
        public Int32 AfternoonSlots { get; set; }
        [DataMember]
        public Int32 EveningSlots { get; set; }
        [DataMember]
        public Int32 Availibility { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public Int64 DateID { get; set; }

    }
    public class AppoinmentDataTOeXCEL
    {
        [DataMember]
        public string Day { get; set; }
        [DataMember]
        public string Morning { get; set; }
        [DataMember]
        public string MorningSlots { get; set; }
        [DataMember]
        public string Afternoon { get; set; }
        [DataMember]
        public string AfternoonSlots { get; set; }
        [DataMember]
        public string Evening { get; set; }
        [DataMember]
        public string EveningSlots { get; set; }
        [DataMember]
        public DateTime date { get; set; }
        [DataMember]
        public string Status { get; set; }
    }
}
