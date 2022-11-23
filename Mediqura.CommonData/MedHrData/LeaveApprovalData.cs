using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedHrData
{
    public class LeaveApprovalData : BaseData
    {
        [DataMember]
        public Int64 LeaveRecordID { get; set; }
        [DataMember]
        public Int64 LeaveID { get; set; }
        [DataMember]
        public Int32 NoOfDays { get; set; }
        [DataMember]
        public DateTime datefrom { get; set; }
        [DataMember]
        public DateTime dateto { get; set; }
        [DataMember]
        public Int32 LeaveCategoryID { get; set; }
        [DataMember]
        public string LeaveDate { get; set; }
        [DataMember]
        public string Leavetype { get; set; }
        [DataMember]
        public string Leavecategory { get; set; }
        [DataMember]
        public string Leavestatus { get; set; }
        [DataMember]
        public string LeaveApproverName { get; set; }
        [DataMember]
        public string ApproverRemarks { get; set; }
        [DataMember]
        public Int32 SearchType { get; set; }
    }
}
