using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedNurseData
{
    public class DailyNursingAssessmentData : BaseData
    {
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int32 NurShiftID { get; set; }
        [DataMember]
        public Int32 RowNo { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public Int32 DischargeStatus { get; set; }
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
        public int NurseShift { get; set; }
        [DataMember]
        public int EmotionalState { get; set; }
        [DataMember]
        public int Consciousness { get; set; }
        [DataMember]
        public int Speech { get; set; }
        [DataMember]
        public int PhysicalType { get; set; }
        [DataMember]
        public int Oriented { get; set; }
        [DataMember]
        public int RbTime { get; set; }
        [DataMember]
        public int RbPlace { get; set; }
        [DataMember]
        public int RbPerson { get; set; }
        [DataMember]
        public int Respiratory { get; set; }
        [DataMember]
        public string Pulse { get; set; }
        [DataMember]
        public string Breath { get; set; }
        [DataMember]
        public string Cough { get; set; }
        [DataMember]
        public string Oxygen { get; set; }
        [DataMember]
        public int ChestDrain { get; set; }
        [DataMember]
        public int FluidFluctuating { get; set; }
        [DataMember]
        public int RbFluid { get; set; }
        [DataMember]
        public int RbR_Plueral { get; set; }
        [DataMember]
        public int RbL_Plueral { get; set; }
        [DataMember]
        public int Cardio { get; set; }
        [DataMember]
        public int Tension { get; set; }
        [DataMember]
        public int Peripheral { get; set; }
        [DataMember]
        public int RbDistension { get; set; }
        [DataMember]
        public int RbChestpain { get; set; }
        [DataMember]
        public int Gastrointestinalmouth { get; set; }
        [DataMember]
        public int Gastrointestinalteeth { get; set; }
        [DataMember]
        public int Gastrointestinaltongue { get; set; }
        [DataMember]
        public int RbOralulcers { get; set; }
        [DataMember]
        public int RbAbndistension { get; set; }
        [DataMember]
        public int RbNausea { get; set; }
        [DataMember]
        public int RbVomitting { get; set; }
        [DataMember]
        public int RbNpo { get; set; }
        [DataMember]
        public int Nutrition { get; set; }
        [DataMember]
        public int RbOral { get; set; }
        [DataMember]
        public int RbTubefeeding { get; set; }
        [DataMember]
        public int RbParenteral { get; set; }
        [DataMember]
        public int RbBowel { get; set; }
        [DataMember]
        public int RbConstipation { get; set; }
        [DataMember]
        public int RbDiarrhoea { get; set; }
        [DataMember]
        public int RbMalena { get; set; }
        [DataMember]
        public int Mouth { get; set; }
        [DataMember]
        public int RbUrine { get; set; }
        [DataMember]
        public int RbHematuria { get; set; }
        [DataMember]
        public int Skin { get; set; }
        [DataMember]
        public int Cyanosis { get; set; }
        [DataMember]
        public int Peripheries { get; set; }
        [DataMember]
        public string Oedemasite { get; set; }
        [DataMember]
        public int Temperature { get; set; }
        [DataMember]
        public int Scalp { get; set; }
        [DataMember]
        public int Eyes { get; set; }
        [DataMember]
        public int Nose { get; set; }
        [DataMember]
        public int Ear { get; set; }
        [DataMember]
        public int Sleep { get; set; }
        [DataMember]
        public int Joint { get; set; }
        [DataMember]
        public int Ambulate { get; set; }
        [DataMember]
        public string Sitecentralline { get; set; }
        [DataMember]
        public string Conditioncentralline { get; set; }
        [DataMember]
        public string Siteperipheralline { get; set; }
        [DataMember]
        public string Conditionperipheralline { get; set; }
        [DataMember]
        public string Sitearterialline { get; set; }
        [DataMember]
        public string Conditionarterialline { get; set; }
        [DataMember]
        public string Siteanyotherline { get; set; }
        [DataMember]
        public string Conditionanyotherline { get; set; }
        [DataMember]
        public int RbHealthy { get; set; }
        [DataMember]
        public int RbDressing { get; set; }
        [DataMember]
        public int PainScale { get; set; }
        [DataMember]
        public string Done { get; set; }
        [DataMember]
        public string Due { get; set; }
        [DataMember]
        public string Reports { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public int userID { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string Particular { get; set; }
        [DataMember]
        public string PatientDetails { get; set; }
        [DataMember]
        public string IPNos { get; set; }
        [DataMember]
        public long patID { get; set; }
    }

}
