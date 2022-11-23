using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedHrData
{
	public class DutyRosterData :BaseData
	{
		[DataMember]
		public int heading { get; set; }
        [DataMember]
		public int DepartmentID { get; set; }
        [DataMember]
		public string EmployeeNo { get; set; }
        [DataMember]
		public Int64 EmpID { get; set; }
        [DataMember]
		public string EmpName { get; set; }
        [DataMember]
		public int  Year { get; set; }
        [DataMember]
		public int Month { get; set; }
        [DataMember]
		public int SeasonID { get; set; }
	    [DataMember]
		public string Season { get; set; }
        [DataMember]
		public int No_Of_Working_Days { get; set; }
        [DataMember]
		public int No_Of_Days { get; set; }
        [DataMember]
		public string RosterDetails_Day1 { get; set; }
        [DataMember]
		public string RosterDetails_Day2 { get; set; }
	    [DataMember]
		public string RosterDetails_Day3 { get; set; }
        [DataMember]
		public string RosterDetails_Day4 { get; set; }
	    [DataMember]
		public string RosterDetails_Day5 { get; set; }
        [DataMember]
		public string RosterDetails_Day6 { get; set; }
	    [DataMember]
		public string RosterDetails_Day7 { get; set; }
        [DataMember]
		public string RosterDetails_Day8 { get; set; }
	    [DataMember]
		public string RosterDetails_Day9 { get; set; }
	    [DataMember]
		public string RosterDetails_Day10 { get; set; }
        [DataMember]
		public string RosterDetails_Day11 { get; set; }
        [DataMember]
		public string RosterDetails_Day12 { get; set; }
	    [DataMember]
		public string RosterDetails_Day13 { get; set; }
        [DataMember]
		public string RosterDetails_Day14 { get; set; }
	    [DataMember]
		public string RosterDetails_Day15 { get; set; }
        [DataMember]
		public string RosterDetails_Day16 { get; set; }
	    [DataMember]
		public string RosterDetails_Day17 { get; set; }
        [DataMember]
		public string RosterDetails_Day18 { get; set; }
	    [DataMember]
		public string RosterDetails_Day19 { get; set; }
        [DataMember]
		public string RosterDetails_Day20 { get; set; }
        [DataMember]
		public string RosterDetails_Day21 { get; set; }
        [DataMember]
		public string RosterDetails_Day22 { get; set; }
	    [DataMember]
		public string RosterDetails_Day23 { get; set; }
        [DataMember]
		public string RosterDetails_Day24 { get; set; }
	    [DataMember]
		public string RosterDetails_Day25 { get; set; }
        [DataMember]
		public string RosterDetails_Day26 { get; set; }
	    [DataMember]
		public string RosterDetails_Day27 { get; set; }
        [DataMember]
		public string RosterDetails_Day28 { get; set; }
	    [DataMember]
		public string RosterDetails_Day29 { get; set; }
        [DataMember]
		public string  RosterDetails_Day30 { get; set; }
        [DataMember]
		public string RosterDetails_Day31 { get; set; }
		[DataMember]
		public string OnLeave_Day1 { get; set; }
		[DataMember]
		public string OnLeave_Day2 { get; set; }
		[DataMember]
		public string OnLeave_Day3 { get; set; }
		[DataMember]
		public string OnLeave_Day4 { get; set; }
		[DataMember]
		public string OnLeave_Day5 { get; set; }
		[DataMember]
		public string OnLeave_Day6 { get; set; }
		[DataMember]
		public string OnLeave_Day7 { get; set; }
		[DataMember]
		public string OnLeave_Day8 { get; set; }
		[DataMember]
		public string OnLeave_Day9 { get; set; }
		[DataMember]
		public string OnLeave_Day10 { get; set; }
		[DataMember]
		public string OnLeave_Day11 { get; set; }
		[DataMember]
		public string OnLeave_Day12 { get; set; }
		[DataMember]
		public string OnLeave_Day13 { get; set; }
		[DataMember]
		public string OnLeave_Day14 { get; set; }
		[DataMember]
		public string OnLeave_Day15 { get; set; }
		[DataMember]
		public string OnLeave_Day16 { get; set; }
		[DataMember]
		public string OnLeave_Day17 { get; set; }
		[DataMember]
		public string OnLeave_Day18 { get; set; }
		[DataMember]
		public string OnLeave_Day19 { get; set; }
		[DataMember]
		public string OnLeave_Day20 { get; set; }
		[DataMember]
		public string OnLeave_Day21 { get; set; }
		[DataMember]
		public string OnLeave_Day22 { get; set; }
		[DataMember]
		public string OnLeave_Day23 { get; set; }
		[DataMember]
		public string OnLeave_Day24 { get; set; }
		[DataMember]
		public string OnLeave_Day25 { get; set; }
		[DataMember]
		public string OnLeave_Day26 { get; set; }
		[DataMember]
		public string OnLeave_Day27 { get; set; }
		[DataMember]
		public string OnLeave_Day28 { get; set; }
		[DataMember]
		public string OnLeave_Day29 { get; set; }
		[DataMember]
		public string OnLeave_Day30 { get; set; }
		[DataMember]
		public string OnLeave_Day31 { get; set; }
		[DataMember]
		public DateTime Date_Day1 { get; set; }
		[DataMember]
		public DateTime Date_Day2 { get; set; }
		[DataMember]
		public DateTime Date_Day3 { get; set; }
		[DataMember]
		public DateTime Date_Day4 { get; set; }
		[DataMember]
		public DateTime Date_Day5 { get; set; }
		[DataMember]
		public DateTime Date_Day6 { get; set; }
		[DataMember]
		public DateTime Date_Day7 { get; set; }
		[DataMember]
		public DateTime Date_Day8 { get; set; }
		[DataMember]
		public DateTime Date_Day9 { get; set; }
		[DataMember]
		public DateTime Date_Day10 { get; set; }
		[DataMember]
		public DateTime Date_Day11 { get; set; }
		[DataMember]
		public DateTime Date_Day12 { get; set; }
		[DataMember]
		public DateTime Date_Day13 { get; set; }
		[DataMember]
		public DateTime Date_Day14 { get; set; }
		[DataMember]
		public DateTime Date_Day15 { get; set; }
		[DataMember]
		public DateTime Date_Day16 { get; set; }
		[DataMember]
		public DateTime Date_Day17 { get; set; }
		[DataMember]
		public DateTime Date_Day18 { get; set; }
		[DataMember]
		public DateTime Date_Day19 { get; set; }
		[DataMember]
		public DateTime Date_Day20 { get; set; }
		[DataMember]
		public DateTime Date_Day21 { get; set; }
		[DataMember]
		public DateTime Date_Day22 { get; set; }
		[DataMember]
		public DateTime Date_Day23 { get; set; }
		[DataMember]
		public DateTime Date_Day24 { get; set; }
		[DataMember]
		public DateTime Date_Day25 { get; set; }
		[DataMember]
		public DateTime Date_Day26 { get; set; }
		[DataMember]
		public DateTime Date_Day27 { get; set; }
		[DataMember]
		public DateTime Date_Day28 { get; set; }
		[DataMember]
		public DateTime Date_Day29 { get; set; }
		[DataMember]
		public DateTime Date_Day30 { get; set; }
		[DataMember]
		public DateTime Date_Day31 { get; set; }
		[DataMember]
		public string XMLEmployeeDuty { get; set; }
	}
}
