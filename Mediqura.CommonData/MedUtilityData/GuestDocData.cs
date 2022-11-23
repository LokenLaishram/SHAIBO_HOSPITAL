using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class GuestDocData:BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public int DeptID { get; set; }
        [DataMember]
        public int DepartmentID { get; set; }
        [DataMember]
        public string Deptname { get; set; }
        [DataMember]
        public string Specialization { get; set; }
       

    }
    public class GuestDocDatatoExcel
    {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public int DeptID { get; set; }
        [DataMember]
        public string Deptname { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
    }
}
