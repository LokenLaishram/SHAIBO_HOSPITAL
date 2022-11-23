using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.Common
{
    [Serializable]
    public class LookupItem : BaseData
    {
        public Int64 ItemId { get; set; }
        public string ItemName { get; set; }
    }
}
