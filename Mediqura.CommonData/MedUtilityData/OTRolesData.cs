using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{

    public class OTRolesData:BaseData
    {
        [DataMember]
        public Int64 OTRoleID { get; set; }
        [DataMember]
        public string OTRoleCode { get; set; }
        [DataMember]
        public string OTRoledescp { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string IPNo { get; set; }
    }
    public class OTRoleDatatoExcel
    {
        [DataMember]
        public Int64 OTRoleID { get; set; }
        [DataMember]
        public string OTRoleCode { get; set; }
        [DataMember]
        public string OTRoledescp { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
}
