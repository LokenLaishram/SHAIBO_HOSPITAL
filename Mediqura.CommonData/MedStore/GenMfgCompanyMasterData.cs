using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
    public class GenMfgCompanyMasterData :BaseData
    {
        [DataMember]
        public Int64 StoreCompanyID { get; set; }
        [DataMember]
        public string StoreCompanyCode { get; set; }
        [DataMember]
        public string StoreCompanydescp { get; set; }
        [DataMember]
        public string Remarks { get; set; }
       }
    public class StrCompanyDatatoExcel
    {
        [DataMember]
        public Int64 StoreCompanyID { get; set; }
        [DataMember]
        public string StoreCompanyCode { get; set; }
        [DataMember]
        public string StoreCompanydescp { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
       

    }
}
