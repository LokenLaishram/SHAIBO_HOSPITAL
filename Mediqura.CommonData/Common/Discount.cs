using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.Common
{
   public class Discount: BaseData
    {
        [DataMember]
        public int PatientCatID { get; set; }
        [DataMember]
        public String PatCat { get; set; }
        [DataMember]
        public int SubCatID { get; set; }
        [DataMember]
        public String subCat { get; set; }
        [DataMember]
        public decimal DiscountAmount { get; set; }
    }
}
