using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace Mediqura.CommonData.MedUtilityData
{
    public class DocTypeData:BaseData
    {
        [DataMember]
        public Int64 DocTypeID { get; set; }
        [DataMember]
        public string DocTypeCode { get; set; }
        [DataMember]
        public string DocTypedescp { get; set; }
        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public DateTime AddedDTM { get; set; }
        [DataMember]
        public string Specialization { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public Int32 ContactNo { get; set; }


    }
    public class DocTypeDatatoExcel
    {
        [DataMember]
        public Int64 DocTypeID { get; set; }
        [DataMember]
        public string DocTypeCode { get; set; }
        [DataMember]
        public string DocTypedescp { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
}
