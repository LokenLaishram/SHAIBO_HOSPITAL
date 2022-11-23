using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
    public class IndentRequestTypeData:BaseData
    {
        [DataMember]
        public Int32 RequestID { get; set; }
        [DataMember]
        public string RequestCode { get; set; }
        [DataMember]
        public string Requestdescp { get; set; }
        [DataMember]
        public string Remarks { get; set; }
       
    }
    public class IndentRequestTypeDatatoExcel
    {
        [DataMember]
        public Int32 RequestID { get; set; }
        [DataMember]
        public string RequestCode { get; set; }
        [DataMember]
        public string Requestdescp { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
}
