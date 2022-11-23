using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.Common
{
   public class MedicineData
    {
       
        [DataMember]
        public String MedName { get; set; }
    }
}
