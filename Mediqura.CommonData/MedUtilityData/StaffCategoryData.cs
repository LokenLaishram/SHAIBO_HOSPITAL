using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class StaffCategoryData:BaseData
    {
        [DataMember]
        public int CategoryID { get; set; }
        [DataMember]
        public string CategoryCode { get; set; }
        [DataMember]
        public string Categorydescp { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string IPNo { get; set; }
    }
    public class StaffCategorytoExcel
    {
        [DataMember]
        public int CategoryID { get; set; }
        [DataMember]
        public string CategoryCode { get; set; }
        [DataMember]
        public string Categorydescp { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }

}
