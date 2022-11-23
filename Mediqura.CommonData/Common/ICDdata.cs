using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.Common
{
   public class ICDdata
    {
        [DataMember]
        public String ICDCODE { get; set; }
        [DataMember]
        public String DeseaseName { get; set; }

    }

}
