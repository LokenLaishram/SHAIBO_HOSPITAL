using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedPharData
{
	public class PHRAccountGroupMasterData: BaseData
	{
		[DataMember]
		public int GroupID { get; set; }
		[DataMember]
		public string GroupName { get; set; }
		[DataMember]
		public string Under { get; set; }
		[DataMember]
		public int UnderID { get; set; }
		[DataMember]
		public string Nature { get; set; }
		[DataMember]
		public int NatureID { get; set; }
		[DataMember]
		public int GroupType { get; set; }
		[DataMember]
		public string Remarks { get; set; }
	}
	public class PHRAccountGroupMasterDataExcel
	{

		[DataMember]
		public int GroupID { get; set; }
		[DataMember]
		public string GroupName { get; set; }
		[DataMember]
		public string Under { get; set; }
		[DataMember]
		public string Nature { get; set; }


	}
	public class PHRAcountData : BaseData
	{
		[DataMember]
		public int ID { get; set; }
		[DataMember]
		public int GroupUnderID { get; set; }
		[DataMember]
		public string GroupName { get; set; }
		[DataMember]
		public int NatureID { get; set; }
		[DataMember]
		public string Remarks { get; set; }

	}
}
