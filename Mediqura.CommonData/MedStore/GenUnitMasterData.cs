using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
    public class GenUnitMasterData : BaseData
    {
        [DataMember]
        public Int64 StoreUnitID { get; set; }
        [DataMember]
        public string StoreUnitCode { get; set; }
        [DataMember]
        public string StoreUnitdescp { get; set; }
        [DataMember]
        public string Remarks { get; set; }
     }
    public class StrUnitDatatoExcel
    {
        [DataMember]
        public Int64 StoreUnitID { get; set; }
        [DataMember]
        public string StoreUnitCode { get; set; }
        [DataMember]
        public string StoreUnitdescp { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
       

    }
}
