using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.Common;

namespace Mediqura.CommonData.MedUtilityData
{
    public class SampleCollectionData : BaseData
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
        public Int64 SampleCollectedBy { get; set; }
        [DataMember]
        public string Urgency { get; set; }
        [DataMember]
        public Double Result { get; set; }
        [DataMember]
        public Int64 IsDeviceInitiated { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
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
        public int StatusID { get; set; }
        [DataMember]
        public string Device { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public DateTime InitTime { get; set; }
        [DataMember]
        public DateTime CompTime { get; set; }
        [DataMember]
        public string Investigationumber { get; set; }
        [DataMember]
        public Int64 ConsultantID { get; set; }
        [DataMember]
        public int CollectionStatus { get; set; }
        [DataMember]
        public string Container { get; set; }
        [DataMember]
        public int IsOutsourcedTest { get; set; }
        [DataMember]
        public string Comment { get; set; }
        [DataMember]
        public string initiationtime { get; set; }
        [DataMember]
        public string recievedtime { get; set; }
        [DataMember]
        public string completiontime { get; set; }
        [DataMember]
        public int SubGroupID { get; set; }
        [DataMember]
        public string Subgroup { get; set; }
        [DataMember]
        public string RequestedTime { get; set; }
        [DataMember]
        public string Samplecollectiontime { get; set; }
        [DataMember]
        public DateTime MLTrecievingDatetime { get; set; }
        [DataMember]
        public string MLTrecievingtime { get; set; }
        [DataMember]
        public string Reportgeneratedtime { get; set; }
        [DataMember]
        public int IsMLTreceived { get; set; }
        [DataMember]
        public string Verifiedtime { get; set; }
        [DataMember]
        public Int32 ReportTypeID { get; set; }
        [DataMember]
        public Int32 IsSampleRecieve { get; set; }
        [DataMember]
        public DateTime InvSampleCollectedOn { get; set; }
        [DataMember]
        public string FromDetails { get; set; }
        [DataMember]
        public string ReportEntryStatus { get; set; }
        [DataMember]
        public Int32 LabSubGroupID { get; set; }
        [DataMember]
        public Int32 TemplateTypeID { get; set; }
        [DataMember]
        public Int32 RunnerID { get; set; }
      
       
    }
    public class LabResultData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public int UnitID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string PatientAddress { get; set; }
        [DataMember]
        public string GenderName { get; set; }
        [DataMember]
        public string PatContact { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string Investigationumber { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string PatientNumber { get; set; }
        [DataMember]
        public string AgeCount { get; set; }
        [DataMember]
        public string ReferalDoctor { get; set; }
        [DataMember]
        public Int64 VerifiedBy { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public string Ranges { get; set; }
        [DataMember]
        public string Unit { get; set; }
        [DataMember]
        public string ParmRemarks { get; set; }
        [DataMember]
        public Int32 LabServiceID { get; set; }
        [DataMember]
        public string RangeWording { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public int IsNormal { get; set; }
        [DataMember]
        public DateTime InvRequestedOn { get; set; }
        [DataMember]
        public string LabResultValue { get; set; }
        [DataMember]
        public Int64 ParameterID { get; set; }
        [DataMember]
        public string Parameter { get; set; }
        [DataMember]
        public int RowType { get; set; }
        [DataMember]
        public int MethodID { get; set; }
        [DataMember]
        public int MachineID { get; set; }
        [DataMember]
        public int ReagentID { get; set; }
        [DataMember]
        public int isVerified { get; set; }
        [DataMember]
        public string OPno { get; set; }
        [DataMember]
        public string IPno { get; set; }
        [DataMember]
        public string Emergno { get; set; }
        [DataMember]
        public Int32 PatientTypeID { get; set; }
        [DataMember]
        public Int32 UrgencyID { get; set; }
        [DataMember]
        public int ContainerID { get; set; }
        [DataMember]
        public int ReportType { get; set; }
        [DataMember]
        public string ResultTemplate { get; set; }
        [DataMember]
        public int TemplateType { get; set; }
        // Culture //
        [DataMember]
        public string Sample { get; set; }
        [DataMember]
        public string Colony { get; set; }
        [DataMember]
        public string OrganismYielded { get; set; }
        [DataMember]
        public Int32 GrowthType { get; set; }
        [DataMember]
        public Int32 AntibioticID { get; set; }
        [DataMember]
        public string AntibioticName { get; set; }
        [DataMember]
        public Int32 AntibioticSensitiveTypeID { get; set; }
        [DataMember]
        public byte[] EmpSignature { get; set; }
        [DataMember]
        public byte[] NurseSignature { get; set; }
        [DataMember]
        public string VisitType { get; set; }
        [DataMember]
        public string SexName { get; set; }
        [DataMember]
        public string RequestedOn { get; set; }
        [DataMember]
        public string VerifiedOn { get; set; }
       
    }
    public class SampleCollectionDatatoExcel
    {
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public string SampleType { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }


    }

}
