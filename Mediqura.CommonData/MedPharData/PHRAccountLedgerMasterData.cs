using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedPharData
{
	public class PHRAccountLedgerMasterData :BaseData
	{
		[DataMember]
		public int LedgerID { get; set; }
		[DataMember]
		public string AccountName { get; set; }
		[DataMember]
		public int GroupUnderID { get; set; }
		[DataMember]
		public string GroupName { get; set; }
		[DataMember]
		public int NatureID { get; set; }
		[DataMember]
		public string NatureName { get; set; }
		[DataMember]
		public string Remarks { get; set; }
		[DataMember]
		public string Site { get; set; }
		[DataMember]
		public decimal Opnbal { get; set; }
		[DataMember]
		public DateTime OpnDate { get; set; }
	}
	public class PHRAccountLedgerMasterDataExcel
	{

		[DataMember]
		public int LedgerID { get; set; }
		[DataMember]
		public string AccountName { get; set; }
		[DataMember]
		public string GroupName { get; set; }
		[DataMember]
		public string NatureName { get; set; }
		[DataMember]
		public string Site { get; set; }
		[DataMember]
		public decimal Opnbal { get; set; }
		[DataMember]
		public DateTime OpnDate { get; set; }


	}
	public class PHRAcountLedgerData : BaseData
	{
		[DataMember]
		public int ID { get; set; }
		[DataMember]
		public int Payment_type { get; set; }
		[DataMember]
		public string AccountName { get; set; }
		[DataMember]
		public int GroupUnderID { get; set; }
		[DataMember]
		public string GroupName { get; set; }
		[DataMember]
		public int NatureID { get; set; }
		[DataMember]
		public string Remarks { get; set; }
		[DataMember]
		public string Site { get; set; }
		[DataMember]
		public decimal Opnbal { get; set; }
		[DataMember]
		public DateTime OpnDate { get; set; }

	}
}
