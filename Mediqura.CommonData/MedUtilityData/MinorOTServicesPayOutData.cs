using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class MinorOTServicesPayOutData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public int DoctorID { get; set; }
        [DataMember]
        public int PatientTypeID { get; set; }
        [DataMember]
        public string DoctorName { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string ServiceCode { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public int ServiceTypeID { get; set; }
        [DataMember]
        public int ServiceID { get; set; }
        [DataMember]
        public Decimal ServiceCharge { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public int ShareType { get; set; }
        [DataMember]
        public Decimal HospitalShare { get; set; }
        [DataMember]
        public Decimal ConsultantShare { get; set; }
        [DataMember]
        public int Activate { get; set; }
        [DataMember]
        public string ShareTypeName { get; set; }
        [DataMember]
        public int BedClassID { get; set; }
        [DataMember]
        public Decimal CorpusFund { get; set; }
        [DataMember]
        public Decimal StaffFund { get; set; }
        [DataMember]
        public Decimal AnaesthesiaShare { get; set; }
        [DataMember]
        public Decimal OTConsumableShare { get; set; }
        [DataMember]
        public Decimal InstrumentUsedShare { get; set; }
        [DataMember]
        public Decimal AssistantShare { get; set; }
        [DataMember]
        public Decimal Surgeon1 { get; set; }
        [DataMember]
        public Decimal Surgeon2 { get; set; }
        [DataMember]
        public Decimal Surgeon3 { get; set; }
        [DataMember]
        public int GroupID { get; set; }
        [DataMember]
        public int SubGroupID { get; set; }

    }
    public class MinorOTServicesPayOutDatatoExcel
    {
        [DataMember]
        public string ServiceCode { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public string ServiceCharge { get; set; }
        [DataMember]
        public string ShareTypeName { get; set; }
        [DataMember]
        public string HospitalShare { get; set; }
        [DataMember]
        public string ConsultantShare { get; set; }
        [DataMember]
        public string CorpusFund { get; set; }
        [DataMember]
        public string StaffFund { get; set; }
    }

    public class MajorOTServicesPayOutDatatoExcel
    {
        [DataMember]
        public string ServiceCode { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public string ServiceCharge { get; set; }
        [DataMember]
        public string ShareTypeName { get; set; }
        [DataMember]
        public string ConsultantShare { get; set; }
        [DataMember]
        public string CorpusFund { get; set; }
        [DataMember]
        public string StaffFund { get; set; }
        [DataMember]
        public string Surgeon1 { get; set; }
        [DataMember]
        public string Surgeon2 { get; set; }
        [DataMember]
        public string Surgeon3 { get; set; }
        [DataMember]
        public string AnaesthesiaShare { get; set; }
        [DataMember]
        public string OTConsumableShare { get; set; }
        [DataMember]
        public string InstrumentUsedShare { get; set; }
        [DataMember]
        public string AssistantShare { get; set; }

    }
}
