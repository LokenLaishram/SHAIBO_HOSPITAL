using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class FloorMasterData:BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int BlockID { get; set; }
        [DataMember]
        public string FloorTypeCode { get; set; }
        [DataMember]
        public string FloorType { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public string Block { get; set; }
    }
    public class FloorDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int BlockID { get; set; }
        [DataMember]
        public string FloorTypeCode { get; set; }
        [DataMember]
        public string FloorType { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
    }
