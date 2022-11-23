using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedHouseKeepingData
{
    public class BedStatusData : BaseData
    {
        [DataMember]
        public int BedID { get; set; }
        [DataMember]
        public int BlockID { get; set; }
        [DataMember]
        public int FloorID { get; set; }
        [DataMember]
        public int WardID { get; set; }
        [DataMember]
        public int bed_status { get; set; }
        [DataMember]
        public string bedstatus { get; set; }
        [DataMember]
        public string Block { get; set; }
        [DataMember]
        public string Floor1 { get; set; }
        [DataMember]
        public string Ward { get; set; }
        [DataMember]
        public string Room { get; set; }
        [DataMember]
        public string WorkingStatus { get; set; }
        [DataMember]
        public string BedNo { get; set; }
        [DataMember]
        public int WorkingStatusID { get; set; }
      
    }
    public class BedStatusDataToExcel
    {
        [DataMember]
        public string Block { get; set; }
        [DataMember]
        public string Floor1 { get; set; }
        [DataMember]
        public string Ward { get; set; }
        [DataMember]
        public string Room { get; set; }
        [DataMember]
        public string BedNo { get; set; }
        [DataMember]
        public string bedstatus { get; set; }
        [DataMember]
        public string WorkingStatus { get; set; }
     
    }
}
