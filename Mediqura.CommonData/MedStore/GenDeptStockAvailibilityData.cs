using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace Mediqura.CommonData.MedStore
{
    public class GenDeptStockAvailibilityData:BaseData
    {
        [DataMember]
        public Int32 ID { get; set; }
        [DataMember]
        public Int32 deptID { get; set; }
        [DataMember]
        public string deptName { get; set; }
        [DataMember]
        public Int32 stockAvail { get; set; }

    }
}
