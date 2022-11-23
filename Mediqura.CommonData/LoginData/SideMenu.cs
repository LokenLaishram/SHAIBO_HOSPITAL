using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.LoginData
{
   public class SideMenu
   {
       [DataMember]
       public int RoleID { get; set; }
       [DataMember]
       public int MenuitemID { get; set; }
       [DataMember]
       public string MenuItemName { get; set; }
    }
}
