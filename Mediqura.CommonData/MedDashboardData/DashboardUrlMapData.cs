using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedDashboardData
{
    public class DashboardUrlMapData:BaseData
    {
        [DataMember]
        public Int32 MapID { get; set; }
        [DataMember]
        public string DashboardTittle { get; set; }
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        
    }
    public class DashboardUrlMapDatatoExcel
    {
        [DataMember]
        public Int32 MapID { get; set; }
        [DataMember]
        public string DashboardTittle { get; set; }
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
    public class DashboardRoleWiseMapData : BaseData
    {
        [DataMember]
        public Int32 MapID { get; set; }
        [DataMember]
        public Int32 DesignationID { get; set; }
        [DataMember]
        public Int32 urlID { get; set; }
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string Designation { get; set; }
        [DataMember]
        public string Title { get; set; }

    }
    public class DashboardRoleWiseMapDatatoExcel
    {
        [DataMember]
        public Int32 MapID { get; set; }
        [DataMember]
        public string Designation { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
      


    }
}
