using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.OTData
{
    public class AnesthesiaData:BaseData 
    {
        [DataMember]
        public Int32 AnesThID { get; set; }
        [DataMember]
        public string AnesThCode { get; set; }
        [DataMember]
        public string AnesThdescp { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        
    }
    public class AnesthesiaDatatoExcel
    {
        [DataMember]
        public Int32 AnesThID { get; set; }
        [DataMember]
        public string AnesThCode { get; set; }
        [DataMember]
        public string AnesThdescp { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
}
