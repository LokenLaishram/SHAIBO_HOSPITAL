using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedLabData
{
    public class OutsourceCommissionData:BaseData
    {
       [DataMember]
        public Int32 CommissionID { get; set; }
        [DataMember]
        public string TestCenterName { get; set; }
        [DataMember]
        public Int32 TestCenterID { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public Decimal HospCharge { get; set; }
        [DataMember]
        public Decimal TestCenterCharge { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
        [DataMember]
        public string Remarks { get; set; }
    }
    public class OutsourceCommissionDatatoExcel
    {
        [DataMember]
        public Int32 CommissionID { get; set; }
        [DataMember]
        public string TestCenterName { get; set; }
        [DataMember]
        public Int32 TestCenterID { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public Decimal HospCharge { get; set; }
        [DataMember]
        public Decimal TestCenterCharge { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
    }
}
