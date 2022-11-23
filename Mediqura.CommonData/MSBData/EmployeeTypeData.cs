using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MSBData
{
    public class EmployeeTypeData : BaseData
    {
        [DataMember]
        public Int32 EmplyeetypeID { get; set; }
        [DataMember]
        public string EmployeeType { get; set; }
        [DataMember]
        public Int32 IsMSB { get; set; }
    }
    public class EmployeeDependentData : BaseData
    {
        [DataMember]
        public Int32 dependent { get; set; }
        [DataMember]
        public Int32 IsExclution { get; set; }
       
    }
    public class BenefitData : BaseData
    {
        [DataMember]
        public int BeneficiaryTypeID { get; set; }
        [DataMember]
        public string BeneficiaryType{ get; set; }
        [DataMember]
        public double opd { get; set; }
        [DataMember]
        public double investigation { get; set; }
        [DataMember]
        public double ipd { get; set; }

    }
}
