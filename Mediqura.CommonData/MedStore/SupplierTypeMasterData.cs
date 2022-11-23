using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
    public class SupplierTypeMasterData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string SupplierTypeCode { get; set; }
        [DataMember]
        public string SupplierType { get; set; } // Subgroup ID
        [DataMember]
        public Int64 ContactNo { get; set; }
        [DataMember]
        public decimal SupplierPercent { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpName { get; set; }
    }
    public class SupplierDatatoExcel 
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string SupplierTypeCode { get; set; }
        [DataMember]
        public string SupplierType { get; set; } // Subgroup ID
        [DataMember]
        public Int64 ContactNo { get; set; }
        [DataMember]
        public string EmpName { get; set; }
    }

}
