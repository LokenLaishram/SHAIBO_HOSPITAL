using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace Mediqura.CommonData.MedLab
{
   public class LabOutsourceManagerData:BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 OrderID { get; set; }
        [DataMember]
        public int UnitID { get; set; }
        [DataMember]
        public Int32 PatientTypeID { get; set; }
        [DataMember]
        public Int32 TestID { get; set; }
        [DataMember]
        public Int64 SampleCollectedID { get; set; }
        [DataMember]
        public string Urgency { get; set; }
        [DataMember]
        public Double Result { get; set; }
        [DataMember]
        public Int64 DeviceInitiationID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string EmrgNo { get; set; }
        [DataMember]
        public string BillNo { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public Int64 BillID { get; set; }
        [DataMember]
        public Int64 LabID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public string AgeCount { get; set; }
        [DataMember]
        public string ReferalDoctor { get; set; }
        [DataMember]
        public Int64 TakenBy { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public string RegDNo { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public Int32 Samplecode { get; set; }
        [DataMember]
        public string descriptions { get; set; }
        [DataMember]
        public string Ranges { get; set; }
        [DataMember]
        public string Unit { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string TestCenter { get; set; }
        [DataMember]
        public Int32 GenderID { get; set; }
        [DataMember]
        public Int32 TestCenterID { get; set; }
        [DataMember]
        public Int32 LabServiceID { get; set; }
        [DataMember]
        public Int64 UrgencyID { get; set; }
        [DataMember]
        public Int32 SampleTypeID { get; set; }
        [DataMember]
        public string SampleType { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public int StatusID { get; set; }
        [DataMember]
        public string DevName { get; set; }
        [DataMember]
        public Int32 DeviceID { get; set; }
        [DataMember]
        public string DevStatus { get; set; }
        [DataMember]
        public int DevStatusID { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public DateTime InitTime { get; set; }
        [DataMember]
        public DateTime CompTime { get; set; }
        [DataMember]
        public Int32 IsOutsourcedTest { get; set; }
        [DataMember]
        public Int32 ISReportDelivered { get; set; }
        [DataMember]
        public Int32 IsOutsourcedSampleSend { get; set; }
        [DataMember]
        public Int32 IsSampleCollcteded { get; set; }
       [DataMember]
        public Int32 IsOutsourcedReportReceived { get; set; }
    }
}
