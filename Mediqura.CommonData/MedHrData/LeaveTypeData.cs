 using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedHrData
{
   public class LeaveTypeData : BaseData
    {
        [DataMember]
        public Int64 LeaveID { get; set; }
        [DataMember]
        public string LeaveCode { get; set; }
        [DataMember]
        public string Leavedescp { get; set; }
        [DataMember]
        public Int32 MaxLeaveMonth { get; set; }
        [DataMember]
        public Int32 MaxLeaveYear { get; set; }
        [DataMember]
        public String LeaveEligible { get; set; }
        [DataMember]
        public Int32 Leavecarriedforward { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public Int32 LeaveAvailinAdvance { get; set; }
        [DataMember]
        public Int32 leaveextension { get; set; }
        [DataMember]
        public Int32 leaveHalfday { get; set; }
        [DataMember]
        public Int32 PaidLeave { get; set; }
        [DataMember]
        public Int32 LeaveDocument { get; set; }
		[DataMember]
		public string leavecombined { get; set; }
        [DataMember]
        public Int32 leavebonus { get; set; }
        [DataMember]
        public Int32 LeaveCountID { get; set; }
        [DataMember]
        public string XMLLeavecarriedforward { get; set; }
	    
	}
}
