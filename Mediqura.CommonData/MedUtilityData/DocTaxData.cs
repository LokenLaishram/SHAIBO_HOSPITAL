using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class DocTaxData:BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int ServiceID { get; set; }
        [DataMember]
        public int ServiceSubGrpID { get; set; }
        [DataMember]
        public int DoctorID { get; set; }
        [DataMember]
        public Decimal Tax { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }
        [DataMember]
        public string ServiceType { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public string DocName { get; set; }


    
    }
    public class DocTaxDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
      
        [DataMember]
        public Decimal Tax { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }
        [DataMember]
        public string ServiceType { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public string DocName { get; set; }

    }
}
