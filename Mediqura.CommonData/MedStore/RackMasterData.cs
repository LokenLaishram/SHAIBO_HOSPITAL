using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
    public class RackMasterData : BaseData
    {
        [DataMember]
        public Int64 RackID { get; set; }
        [DataMember]
        public int StockTypeID { get; set; }
        [DataMember]
        public string RackNumber { get; set; }
        [DataMember]
        public Int64 StockID { get; set; }

        [DataMember]
        public string Stock { get; set; }
        [DataMember]
        public string SubRack { get; set; }
        [DataMember]
        public int SubRackID { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string BatchNo { get; set; }
        [DataMember]
        public string StockNo { get; set; }

        [DataMember]
        public Int64 ItemID { get; set; }
        [DataMember]
        public string ItemLocation { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }


    }
    public class RackTypeDatatoExcel
    {
        [DataMember]
        public Int64 RackID { get; set; }
        [DataMember]
        public string RackNumber { get; set; }
        [DataMember]
        public string Stock { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }


    }
    public class SubRackMasterDatatoExcel 
    {
        [DataMember]
        public Int64 SubRackID { get; set; }
        [DataMember]
        public string Stock { get; set; }
        [DataMember]
        public string RackNumber { get; set; }
        [DataMember]
        public string SubRack { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
    public class ItemLocationDatatoExcel
    {
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string BatchNo { get; set; }
        [DataMember]
        public string StockNo { get; set; }
        [DataMember]
        public string RackNumber { get; set; }
        [DataMember]
        public string SubRack { get; set; }
        [DataMember]
        public string ItemLocation { get; set; }
     }
}
