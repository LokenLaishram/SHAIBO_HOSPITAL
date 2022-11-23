using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class ServicesData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public int ServiceTypeID { get; set; }
        [DataMember]
        public int SubServiceTypeID { get; set; }
        [DataMember]
        public string SubServiceType { get; set; }
        [DataMember]
        public string ServiceType { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public decimal ServiceCharge { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public Int64 DoctorID { get; set; }
    }
    public class ServicesDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string ServiceType { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public string ServiceCharge { get; set; }
    }
}
