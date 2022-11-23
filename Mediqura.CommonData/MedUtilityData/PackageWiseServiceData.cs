using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class PackageWiseServiceData:BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int CompanyID { get; set; }
        [DataMember]
        public int packageID { get; set; }
        [DataMember]
        public int ServiceID { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string CompanyName { get; set; }
        [DataMember]
        public string packageName { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public int servcID { get; set; }
        [DataMember]
        public decimal TestAmount { get; set; }
    }
    public class PackageWiseServiceDataToExcel 
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string CompanyName { get; set; }
        [DataMember]
        public string packageName { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public string EmpName { get; set; }
       }
}
