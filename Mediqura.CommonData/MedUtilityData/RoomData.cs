using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Mediqura.CommonData.MedUtilityData
{
    public class RoomData:BaseData
    {

        [DataMember]
        public Int32 BlockID { get; set; }
        [DataMember]
        public Int32 FloorID { get; set; }
        [DataMember]
        public Int32 WardID { get; set; }
        [DataMember]
        public Int32 RoomID { get; set; }
        [DataMember]
        public string RoomCode { get; set; }
        [DataMember]
        public string RoomNo { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string Block { get; set; }
        [DataMember]
        public string Floor { get; set; }
        [DataMember]
        public string Ward { get; set; }
        [DataMember]
        public DateTime AddedOn { get; set; }
       
    }
    public class BedMstDatatoExcel
    {
        [DataMember]
        public string Block { get; set; }
        [DataMember]
        public string Floor { get; set; }
        [DataMember]
        public string Ward { get; set; }
        
        [DataMember]
        public string RoomNo { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
        [DataMember]
        public DateTime AddedOn { get; set; }

    }
}
