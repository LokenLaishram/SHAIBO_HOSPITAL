using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Mediqura.CommonData.MedUtilityData
{
    public class LabServiceMasterData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 SeqNo { get; set; }
        [DataMember]
        public int TestCenterID { get; set; }
        [DataMember]
        public int LabservicetypeID { get; set; }
        [DataMember]
        public int ServicetypeID { get; set; }
        [DataMember]
        public int ReagentTypeID { get; set; }
        [DataMember]
        public string Reagent { get; set; }
        [DataMember]
        public string TestCenter { get; set; }
        [DataMember]
        public int defaultValue { get; set; }
        [DataMember]
        public int MethodID { get; set; }
        [DataMember]
        public string Method { get; set; }
        [DataMember]
        public int LabGroupID { get; set; }
        [DataMember]
        public string ServiceGroup { get; set; }
        [DataMember]
        public string LabGroup { get; set; }
        [DataMember]
        public string LabSubGroup { get; set; }
        [DataMember]
        public int TestID { get; set; }
        [DataMember]
        public int LabSubGroupID { get; set; }
        [DataMember]
        public string ServiceSubGroup { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public decimal TestAmount { get; set; }
        [DataMember]

        public string RangeWording { get; set; }
        [DataMember]

        public string Remarks { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public Int64 UnitID { get; set; }
        [DataMember]
        public string Descriptions { get; set; }
        [DataMember]
        public string Unit { get; set; }
        [DataMember]
        public Int64 SampleTypeID { get; set; }
        [DataMember]
        public string SampleType { get; set; }
        [DataMember]
        public string MachineName { get; set; }
        [DataMember]
        public Int64 MachineID { get; set; }
        [DataMember]
        public string NormalRangeMaleFrom { get; set; }
        [DataMember]
        public string NormalRangeMaleTo { get; set; }
        [DataMember]
        public string NormalRangeFeMaleFrom { get; set; }
        [DataMember]
        public string NormalRangeFeMaleTo { get; set; }
        [DataMember]
        public string NormalRangeChildren { get; set; }
        [DataMember]
        public string SubTestName { get; set; }
        [DataMember]
        public string RowType { get; set; }
        [DataMember]
        public string RowTypeName { get; set; }
        [DataMember]
        public int RowTypeID { get; set; }
        [DataMember]
        public string AgeRangeID { get; set; }
        [DataMember]
        public string AgeRangeFrom { get; set; }
        [DataMember]
        public string AgeRangeTo { get; set; }
        [DataMember]
        public string NormalRangeTransFeMaleFrom { get; set; }
        [DataMember]
        public string NormalRangeTransFeMaleTo { get; set; }
        [DataMember]
        public string NormalRangeTransMaleFrom { get; set; }
        [DataMember]
        public string NormalRangeTransMaleTo { get; set; }
        [DataMember]
        public int OrderNo { get; set; }
        [DataMember]
        public int ContainerID { get; set; }
        [DataMember]
        public int ReportTypeID { get; set; }
        [DataMember]
        public string ReportType { get; set; }
        [DataMember]
        public string Container { get; set; }
        [DataMember]
        public int TemplateType { get; set; }
        [DataMember]
        public Int64 DoctorID { get; set; }
        [DataMember]
        public int MaleID { get; set; }
        [DataMember]
        public int FemaleID { get; set; }
        [DataMember]
        public int BotheID { get; set; }
    }
    public class LabServicesDatatoExcel
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string ServiceGroup { get; set; }
        [DataMember]
        public string ServiceSubGroup { get; set; }
        [DataMember]
        public string ReportType { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public string TestAmount { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
    }
    public class LabSubTestDatatoExcel
    {
        //[DataMember]
        //public string SubTestCode { get; set;
        //
        [DataMember]
        public Int64 SeqNo { get; set; }
        [DataMember]
        public string SubTestName { get; set; }
        [DataMember]
        public string Unit { get; set; }
        [DataMember]
        public string SampleType { get; set; }
        [DataMember]
        public string Method { get; set; }
        [DataMember]
        public string Container { get; set; }

        [DataMember]
        public string Reagent { get; set; }

        [DataMember]
        public string AgeRangeFrom { get; set; }
        [DataMember]
        public string AgeRangeTo { get; set; }
        [DataMember]
        public string NormalRangeMaleFrom { get; set; }
        [DataMember]
        public string NormalRangeMaleTo { get; set; }
        [DataMember]
        public string NormalRangeFeMaleFrom { get; set; }
        [DataMember]
        public string NormalRangeFemaleTo { get; set; }
   
    }
}
