using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Mediqura.CommonData.MedNurseData
{
    public class NurseStationData:BaseData
    {
        [DataMember]
        public Int64 StaionID { get; set; }
        [DataMember]
        public string StationCode { get; set; }
        [DataMember]
        public string Stationdescp { get; set; }
        [DataMember]
        public string Remarks { get; set; }
       
    }
    public class WardToStationData : BaseData
    {
        [DataMember]
        public Int32 ID { get; set; }
        [DataMember]
        public Int32 StationID { get; set; }
        [DataMember]
        public Int32 WardID { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string Station { get; set; }
        [DataMember]
        public string Ward { get; set; }

    }
    public class WardToStationDatatoExcel : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int32 StationID { get; set; }
        [DataMember]
        public Int32 WardID { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
        [DataMember]
        public string Station { get; set; }
        [DataMember]
        public string Ward { get; set; }

    }
    public class NurseStationDatatoExcel
    {
        [DataMember]
        public Int64 StaionID { get; set; }
        [DataMember]
        public string StationCode { get; set; }
        [DataMember]
        public string Stationdescp { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
}
