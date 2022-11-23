using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Mediqura.CommonData.MedUtilityData
{
    public class LabCommentMakerData:BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int LabSubGroupId { get; set; }
        [DataMember]
        public int LabGroupID { get; set; }
        [DataMember]
        public string LabSubGroup { get; set; }
        [DataMember]
        public int TestId { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public int Gender { get; set; }
        [DataMember]
        public string Template { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }
        [DataMember]
        public string GenderName { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
        [DataMember]
        public string Remarks { get; set; }
       
    }
    public class LabCommentDatatoEXCEL
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string LabSubGroup { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }
        [DataMember]
        public string GenderName { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
        [DataMember]
        public string Remarks { get; set; }
    }
}
