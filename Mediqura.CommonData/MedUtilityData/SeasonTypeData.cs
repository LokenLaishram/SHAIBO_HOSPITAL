using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
	public class SeasonTypeData : BaseData
	{
		[DataMember]
		public Int32 SeasonID { get; set; }
		[DataMember]
		public string Season{ get; set; }
		[DataMember]
		public string Remarks { get; set; }
	    [DataMember]
		public Int32 January { get; set; }
		[DataMember]
		public Int32 February { get; set; }
		[DataMember]
		public Int32 March { get; set; }
		[DataMember]
		public Int32 April { get; set; }
		[DataMember]
		public Int32 May { get; set; }
		[DataMember]
		public Int32 June { get; set; }
		[DataMember]
		public Int32 July { get; set; }
		[DataMember]
		public Int32 August { get; set; }
		[DataMember]
		public Int32 September { get; set; }
		[DataMember]
		public Int32 October { get; set; }
		[DataMember]
		public Int32 November { get; set; }
		[DataMember]
		public Int32 December { get; set; }
	}
}
