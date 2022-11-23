using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedStore
{
    public class GenSupplierTypeMasterData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string SupplierTypeCode { get; set; }
        [DataMember]
        public string SupplierType { get; set; }
        [DataMember]
        public string SupplierAddress { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpName { get; set; }
    }
    public class GenSupplierDatatoExcel 
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string SupplierTypeCode { get; set; }
        [DataMember]
        public string SupplierType { get; set; } // Subgroup ID
        [DataMember]
        public string SupplierAddress { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public string EmpName { get; set; }
    }

}
