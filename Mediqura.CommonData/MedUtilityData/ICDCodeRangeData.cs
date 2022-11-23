using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks; 

namespace Mediqura.CommonData.MedUtilityData
{
    public class ICDCodeRangeData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int CodeCategoryID { get; set; }
        [DataMember]
        public string CodeCategoryRange { get; set; }
        [DataMember]
        public string CodeGroupRange { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpName { get; set; }
    }
    public class ICDCodeRangeDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string CodeCategoryRange { get; set; }
        [DataMember]
        public string CodeGroupRange { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
}