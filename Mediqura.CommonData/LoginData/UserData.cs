using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.LoginData
{
    [Serializable]
    public class UserData
    {
        public LogData objmeduser { get; set; }
        public List<RolesData> RoleList { get; set; }
        public List<SiteMapData> SiteMapList { get; set; }
        public DataTable SiteMapDT { get; set; }
        public string DashBoardUrl { get; set; }
        public string FPData { get; set; }
        public int BillSetting { get; set; }
    }
}
