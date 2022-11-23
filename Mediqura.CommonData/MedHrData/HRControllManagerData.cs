using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
 
namespace Mediqura.CommonData.MedHrData
{
	public class HRControllManagerData :BaseData
	{
		[DataMember]
		public int DepartmentID { get; set; }
		[DataMember]
		public int DesignationID { get; set; }
		[DataMember]
		public int EmployeeTypeID { get; set; }
		[DataMember]
		public Int64 EmployeeID { get; set; }
		[DataMember]
		public string EmpName { get; set; }
		[DataMember]
		public string Department { get; set; }
		[DataMember]
		public string Designation { get; set; }
		[DataMember]
		public string EmployeeType { get; set; }
		[DataMember]
		public int LeaveRequestEnable { get; set; }
		[DataMember]
		public int LeaveApproveEnable { get; set; }
		[DataMember]
		public int RosterUpdateEnable { get; set; }
		[DataMember]
		public int RosterChangeRequestEnable { get; set; }
		[DataMember]
		public int RosterChangeApproveEnable { get; set; }
		[DataMember]
		public string ControllisttoXML { get; set; }
	
	}
}
