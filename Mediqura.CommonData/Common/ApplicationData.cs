using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.Common
{
    public class ApplicationData:BaseData
    {
        [DataMember]
        public int AppID { get; set; }
        [DataMember]
        public string AppName { get; set; }
        [DataMember]
        public int Status { get; set; }
     
    }
}
