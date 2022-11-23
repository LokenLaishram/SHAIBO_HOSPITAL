using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedMedication
{
    public class NureseRecordSheetData:BaseData
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
        public DateTime EntryDate { get; set; }
        [DataMember]
        public string EntryTime { get; set; }
        [DataMember]
        public string Temperature { get; set; }
        [DataMember]
        public string Pulse { get; set; }
        [DataMember]
        public string BP { get; set; }
        [DataMember]
        public string RR { get; set; }
        [DataMember]
        public string SpO2 { get; set; }
        [DataMember]
        public int GCSEyeOpeningID { get; set; }
        [DataMember]
        public int GCSEyeOpeningValue { get; set; }
        [DataMember]
        public string GCSEyeOpening { get; set; }
        [DataMember]
        public int GCSEVerbalID { get; set; }
        [DataMember]
        public int GCSEVerbalValue { get; set; }
        [DataMember]
        public string GCSEVerbal { get; set; }
        [DataMember]
        public int GCSMotorResponseID { get; set; }
        [DataMember]
        public int GCSMotorResponseValue { get; set; }
        [DataMember]
        public string GCSMotorResponse { get; set; }
        [DataMember]
        public int RightPupilID { get; set; }
        [DataMember]
        public int RightPupilValue { get; set; }
        [DataMember]
        public string RightPupil { get; set; }
        [DataMember]
        public int LeftPupilID { get; set; }
        [DataMember]
        public int LeftPupilValue { get; set; }
        [DataMember]
        public string LeftPupil { get; set; }
        [DataMember]
        public string InvasiveLine { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string Remarks { get; set; }
    }
}
