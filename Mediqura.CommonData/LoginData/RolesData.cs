using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.LoginData
{
    public class RolesData:BaseData
    {

        public string RoleName { get; set; }
        public string RoleCode { get; set; }
        public string UserDescription { get; set; }
        public bool DataAccessPermisn { get; set; }
        public string Permission { get; set; }
        public bool ViewToAll { get; set; }
        public string View { get; set; }
        protected LogData LogData
        {
            get;
            set;
        }
    }
}
