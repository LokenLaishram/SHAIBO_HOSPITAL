using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedLab
{
    public class InvDashboardMasterData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 LabID { get; set; }
        [DataMember]
        public Int64 TestID { get; set; }
        [DataMember]
        public Int32 GroupID { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public int isVerified { get; set; }
        [DataMember]
        public int HeaderID { get; set; }
        [DataMember]
        public int isPrinted { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public string InvNo { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string ServNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string Verified { get; set; }
        [DataMember]
        public int IsReportGenerated { get; set; }
        [DataMember]
        public string ReportGenerated { get; set; }
        [DataMember]
        public int IsDeviceInitiated { get; set; }
        [DataMember]
        public string DeviceInitiated { get; set; }
        [DataMember]
        public DateTime InitiatedOn { get; set; }
        [DataMember]
        public int IsOutsourcedReportReceived { get; set; }
        [DataMember]
        public string OutsourcedReportReceived { get; set; }
        [DataMember]
        public int IsOutsourcedSamplesend { get; set; }
        [DataMember]
        public string OutsourcedSamplesend { get; set; }
        [DataMember]
        public int Issamplecollected { get; set; }
        [DataMember]
        public string samplecollected { get; set; }
        [DataMember]
        public int IsOutsourcedTest { get; set; }
        [DataMember]
        public string OutsourcedTest { get; set; }
        [DataMember]
        public int IsReportDelivered { get; set; }
        [DataMember]
        public string ReportDelivered { get; set; }
        [DataMember]
        public string Template { get; set; }
        [DataMember]
        public string ReportDate { get; set; }
        [DataMember]
        public Int64 billID { get; set; }
        [DataMember]
        public int TemplateID { get; set; }
        [DataMember]
        public int PatientType { get; set; }
        [DataMember]
        public Int32 UrgencyID { get; set; }
        [DataMember]
        public Int32 TestCenterID { get; set; }
        [DataMember]
        public string TestCenter { get; set; }
        [DataMember]
        public int ReportTypeID { get; set; }
   
  
   
    }
    public class InvTatData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 LabID { get; set; }
        [DataMember]
        public Int64 TestID { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public string InvNumber { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string TestRequisitionTime { get; set; }
        [DataMember]
        public string SampleCollectionTime { get; set; }
        [DataMember]
        public string SampleRecieveTime { get; set; }
        [DataMember]
        public string DeviceInitiationTime { get; set; }
        [DataMember]
        public string DeviceCompletionTime { get; set; }
        [DataMember]
        public string ReportRecieveTime { get; set; }
        [DataMember]
        public string ReportGenerationTime { get; set; }
        [DataMember]
        public string ReportVerificationTime { get; set; }
        [DataMember]
        public string ReportDeliveryTime { get; set; }
        [DataMember]
        public string TotalTime { get; set; }
        [DataMember]
        public string TotalDateTime { get; set; }
      
    }
}