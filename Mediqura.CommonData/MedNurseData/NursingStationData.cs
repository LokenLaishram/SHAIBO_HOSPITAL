using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedNurseData
{
    public class NursingStationData : BaseData
    {
        [DataMember]
        public Int32 WardID { get; set; }
        [DataMember]
        public Int32 StationTypeID { get; set; }
        [DataMember]
        public Int64 NurseID { get; set; }
        [DataMember]
        public string NursingStation { get; set; }
        [DataMember]
        public Int32 GenStockID { get; set; }
        [DataMember]
        public string StockName { get; set; }
        [DataMember]
        public string Remarks { get; set; }
    }
}
