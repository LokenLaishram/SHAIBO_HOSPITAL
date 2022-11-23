using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class LabTestPackageMasterData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int CompanyID { get; set; }
        [DataMember]
        public string Company { get; set; }
        [DataMember]
        public int PackageID { get; set; }
        [DataMember]
        public string Package { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public decimal TestAmount { get; set; }
        [DataMember]
        public string Remarks { get; set; }
     
    }
    public class LabTestPackageDataToExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Company { get; set; }
        [DataMember]
        public string Package { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public decimal TestAmount { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
     
    }
}
