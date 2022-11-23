using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.Common
{
    [Serializable]
    public class PageControlsData : BaseData
    {
        [DataMember]
        public int ID { get; set; }       
        [DataMember]
        public int SaveEnable { get; set; }
        [DataMember]
        public int UpdateEnable { get; set; }
        [DataMember]
        public int EditEnable { get; set; }
        [DataMember]
        public int DeleteEnable { get; set; }
        [DataMember]
        public int SearchEnable { get; set; }
        [DataMember]
        public int PrintEnable { get; set; }
        [DataMember]
        public int ExportEnable { get; set; }
        [DataMember]
        public int SitemapID { get; set; }
       
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public string Module { get; set; }
       
    }
}
