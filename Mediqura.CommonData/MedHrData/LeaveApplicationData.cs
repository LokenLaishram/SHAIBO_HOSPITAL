using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedHrData
{
    public class LeaveApplicationData : BaseData
    {
		[DataMember]
		public Int64 LeaveApproverID { get; set; }
        [DataMember]
        public Int64 LeaveID { get; set; }
        [DataMember]
        public Int64 LeaveRecordID { get; set; }
		[DataMember]
		public Int32 MonthID { get; set; }
        [DataMember]
        public Int32 MaxLeaveMonth { get; set; }
        [DataMember]
        public Int32 MaxLeaveYear { get; set; }
        [DataMember]
        public Int32 Leavecarriedforward { get; set; }
        [DataMember]
        public Int32 LeaveEligible { get; set; }
        [DataMember]
        public double Leaveconsumed { get; set; }
        [DataMember]
        public double Leaveavailable { get; set; }
		[DataMember]
		public DateTime date { get; set; }
		[DataMember]
        public DateTime datefrom { get; set; }
        [DataMember]
        public DateTime dateto { get; set; }
        [DataMember]
        public Int32 LeaveCategoryID { get; set; }
		[DataMember]
		public string leavedate { get; set; }
		[DataMember]
		public string EmpName { get; set; }
		[DataMember]
        public string Leavereason { get; set; }
        [DataMember]
        public string LeaveStartDate { get; set; }
		[DataMember]
		public string LeaveEndDate { get; set; }
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
        [DataMember]
        public Int32 leaveaction { get; set; }
		[DataMember]
		public Int32 Status { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public int Outs { get; set; }
		[DataMember]
		public int IsAvailedAdvance { get; set; }
		[DataMember]
		public int AllowHalfDay { get; set; }
		[DataMember]
		public double Noofdays { get; set; }
		[DataMember]
		public Int64 LeaveEmployeeID { get; set; }
		[DataMember]
		public Int32 EmployeeTypeID { get; set; }
		[DataMember]
		public string XMLadjustApproveleave { get; set; }
		[DataMember]
		public string XMLadjustRejectleave { get; set; }
		[DataMember]
		public string LeaveAdjustcategory { get; set; }
		[DataMember]
		public string LeaveAdjustedtypes { get; set; }
		[DataMember]
		public string OutputMessage { get; set; }
		[DataMember]
		public Int32 messagetype { get; set; }
		[DataMember]
		public string CC_TO { get; set; }
		[DataMember]
		public string CC_IDs { get; set; }
		[DataMember]
		public string Department { get; set; }
		[DataMember]
		public string CC_Tooltips { get; set; }
		[DataMember]
		public string Reason_Tooltips { get; set; }
		[DataMember]
		public string ApprovedRemarks_Tooltips { get; set; }
		[DataMember]
		public string ApprovedDate { get; set; }
		[DataMember]
		public string AdjustedBy { get; set; }
		[DataMember]
		public string AdjustedDate { get; set; }
		[DataMember]
		public string AdjustedRemarks { get; set; }

	}
}
