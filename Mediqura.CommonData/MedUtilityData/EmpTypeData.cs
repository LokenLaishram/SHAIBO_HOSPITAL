using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace Mediqura.CommonData.MedUtilityData
{
    public class EmpTypeData:BaseData
    {
        [DataMember]
        public int TypeID { get; set; }
        [DataMember]
        public string TypeCode { get; set; }
        [DataMember]
        public string Typedescp { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string IPNo { get; set; }
    }
    public class EmpTypetoExcel
    {
        [DataMember]
        public int TypeID { get; set; }
        [DataMember]
        public string TypeCode { get; set; }
        [DataMember]
        public string Typedescp { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
}
