using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class OPDEmergencyPayOutData : BaseData
    {
        [DataMember]
        public Int64 DoctorID { get; set; }
        [DataMember]
        public int DoctorTypeID { get; set; }
        [DataMember]
        public string DoctorName { get; set; }
        [DataMember]
        public string Remarks { get; set; }
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
        public int GroupID { get; set; }
        [DataMember]
        public int SubGroupID { get; set; }
        [DataMember]
        public int Validfrom { get; set; }
        [DataMember]
        public int Validto { get; set; }
        [DataMember]
        public int DepartmentID { get; set; }
        [DataMember]
        public int ValidDays { get; set; }

    }
    public class OPDEmergencyPayOutDatatoExcel
    {
        [DataMember]
        public string DoctorName { get; set; }
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




    }
}
