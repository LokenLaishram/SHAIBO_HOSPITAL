using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.Common;

namespace Mediqura.CommonData.MedUtilityData
{
    public class TestCenterMasterData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string TestCenterCode { get; set; }
        [DataMember]
        public string TestCenterName { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpName { get; set; }

         [DataMember]
        public int CenterTypeID { get; set; }


    }
    public class TestCenterDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string TestCenterCode { get; set; }
        [DataMember]
        public string TestCenterName { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }
    }
}
