using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.Common;

namespace Mediqura.CommonData.LoginData
{
    [Serializable]
    public class SiteMapData : BaseData 
    {
        [DataMember]
        public int SiteMapID
        { get; set; }
        [DataMember]
        public string Title
        { get; set; }
        [DataMember]
        public string Text
        { get; set; }
        [DataMember]
        public string Description
        { get; set; }
        [DataMember]
        public string Url
        { get; set; }
        [DataMember]
        public int ParentID
        { get; set; }

    }
}
