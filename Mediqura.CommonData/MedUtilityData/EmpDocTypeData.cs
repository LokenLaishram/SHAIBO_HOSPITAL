using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class EmpDocTypeData:BaseData
    {
        [DataMember]
        public int EmpDocID { get; set; }
        [DataMember]
        public string EmpDocCode { get; set; }
        [DataMember]
        public string EmpDocdescp { get; set; }
        [DataMember]
        public string Remarks { get; set; }
       
    }
    public class EmpDocTypeDatatoExcel
    {
        [DataMember]
        public int EmpDocID { get; set; }
        [DataMember]
        public string EmpDocCode { get; set; }
        [DataMember]
        public string EmpDocdescp { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
}
