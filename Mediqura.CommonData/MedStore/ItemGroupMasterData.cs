﻿using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStoreData
{
    public class ItemGroupMasterData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string ItemType{ get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpName { get; set; }
    }
    public class ItemDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string ItemType { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
}
