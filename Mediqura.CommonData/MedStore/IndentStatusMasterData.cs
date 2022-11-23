using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
    public class IndentStatusMasterData:BaseData
    {
        [DataMember]
        public Int32 IndentID { get; set; }
        [DataMember]
        public string IndentCode { get; set; }
        [DataMember]
        public string Indentdescp { get; set; }
        [DataMember]
        public string Remarks { get; set; }
       
    }
    public class IndentStatusDatatoExcel
    {
        [DataMember]
        public Int32 IndentID { get; set; }
        [DataMember]
        public string IndentCode { get; set; }
        [DataMember]
        public string Indentdescp { get; set; }
        
        [DataMember]
        public string AddedBy { get; set; }

    }
}
