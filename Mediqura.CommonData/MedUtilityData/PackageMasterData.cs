using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class PackageMasterData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public int CompanyID { get; set; }
        [DataMember]
        public Decimal Charges { get; set; }
        [DataMember]
        public Decimal HospitalShare { get; set; }
        [DataMember]
        public Decimal ConsultantShare { get; set; }
        [DataMember]
        public Decimal ReportingShare { get; set; }
        [DataMember]
        public string Company { get; set; }
        [DataMember]
        public int ShareTypeID { get; set; }
        [DataMember]
        public string ShareType { get; set; }

    }

    public class PackageDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Company { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Charges { get; set; }
        [DataMember]
        public string ShareType { get; set; }
        [DataMember]
        public string HospitalShare { get; set; }
        [DataMember]
        public string ConsultantShare { get; set; }
        [DataMember]
        public string ReportingShare { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
}
