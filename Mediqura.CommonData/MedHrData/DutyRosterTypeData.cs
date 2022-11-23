using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedHrData
{
	public class DutyRosterTypeData :BaseData
	{
		[DataMember]
		public Int64 RosterID { get; set; }
		[DataMember]
		public string RosterCode { get; set; }
		[DataMember]
		public string RosterDescp { get; set; }
		[DataMember]
		public Int32 ShiftTypeID { get; set; }
		[DataMember]
		public DateTime Shift_I_SummerStartTime { get; set; }
		[DataMember]
		public DateTime Shift_II_SummerStartTime { get; set; }
		[DataMember]
		public DateTime Shift_I_SummerEndTime { get; set; }
		[DataMember]
		public DateTime Shift_II_SummerEndTime { get; set; }
		[DataMember]
		public DateTime Shift_I_SummerInrelaxation{ get; set; }
		[DataMember]
		public DateTime Shift_II_SummerInrelaxation { get; set; }
		[DataMember]
		public DateTime Shift_I_SummerOutrelaxation { get; set; }
		[DataMember]
		public DateTime Shift_II_SummerOutrelaxation{ get; set; }
		[DataMember]
		public int Shift_I_SummerNextDay { get; set; }
		[DataMember]
		public int Shift_II_SummerNextDay { get; set; }
		[DataMember]
		public DateTime Shift_I_WinterStartTime { get; set; }
		[DataMember]
		public DateTime Shift_II_WinterStartTime { get; set; }
		[DataMember]
		public DateTime Shift_I_WinterEndTime { get; set; }
		[DataMember]
		public DateTime Shift_II_WinterEndTime { get; set; }
		[DataMember]
		public DateTime Shift_I_WinterInrelaxation { get; set; }
		[DataMember]
		public DateTime Shift_II_WinterInrelaxation { get; set; }
		[DataMember]
		public DateTime Shift_I_WinterOutrelaxation { get; set; }
		[DataMember]
		public DateTime Shift_II_WinterOutrelaxation { get; set; }
		[DataMember]
		public int Shift_I_WinterNextDay { get; set; }
		[DataMember]
		public int Shift_II_WinterNextDay { get; set; }
		[DataMember]
		public string Remarks { get; set; }
		[DataMember]
		public string ShiftType { get; set; }
		[DataMember]
		public string Summer_Start { get; set; }
		[DataMember]
		public string Summer_End { get; set; }
		[DataMember]
		public string SummerNext { get; set; }
		[DataMember]
		public string Summer_Inrelaxation { get; set; }
		[DataMember]
		public string Summer_Outrelaxation { get; set; }
		[DataMember]
		public string Summer_Duration { get; set; }
		[DataMember]
		public string Winter_Start { get; set; }
		[DataMember]
		public string Winter_End { get; set; }
		[DataMember]
		public string WinterNext { get; set; }
		[DataMember]
		public string Winter_Inrelaxation { get; set; }
		[DataMember]
		public string Winter_Outrelaxation { get; set; }
		[DataMember]
		public string Winter_Duration { get; set; }
	}
}
