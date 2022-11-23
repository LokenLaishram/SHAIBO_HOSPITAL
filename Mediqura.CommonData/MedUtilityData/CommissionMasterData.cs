using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class CommissionMasterData: BaseData
    {
        [DataMember]
        public Int64 CommissionID { get; set; }
        [DataMember]
        public int Servicetype { get; set; }
        [DataMember]
        public String ServicetypeName { get; set; }
        [DataMember]
        public String ServiceName { get; set; }
        [DataMember]
        public int ServiceID { get; set; }
        [DataMember]
        public int Doctortype { get; set; }
        [DataMember]
        public String DoctortypeName { get; set; }
        [DataMember]
        public String DoctorName { get; set; }
        [DataMember]
        public String Remarks { get; set; }
        [DataMember]
        public int DoctorID { get; set; }
        [DataMember]
        public Decimal ServiceCharge { get; set; }
        [DataMember]
        public Decimal Commission { get; set; }
        [DataMember]
        public Decimal Tax { get; set; }
        [DataMember]
        public Decimal Hospitalcharge { get; set; }
    }
    public class CommissionMasterDataToExcel
    {
        
        [DataMember]
        public String ServicetypeName { get; set; }
        [DataMember]
        public String ServiceName { get; set; }
        [DataMember]
        public String DoctorName { get; set; }
        [DataMember]
        public Decimal ServiceCharge { get; set; }
        [DataMember]
        public Decimal Commission { get; set; }
        [DataMember]
        public Decimal Tax { get; set; }
        [DataMember]
        public Decimal Hospitalcharge { get; set; }
    }
}
