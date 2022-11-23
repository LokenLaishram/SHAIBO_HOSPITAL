using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace Mediqura.CommonData.MedBankData
{
    public class BankTypeData:BaseData
    {
      

            [DataMember]
            public Int64 BankTypeID { get; set; }
            [DataMember]
            public string BankTypeCode { get; set; }
            [DataMember]
            public string BankTypedescp { get; set; }
            [DataMember]
            public string Remarks { get; set; }
            [DataMember]
            public string IPNo { get; set; }
    }
    public class BankTypeDatatoExcel
        {
            [DataMember]
            public Int64 BankTypeID { get; set; }
            [DataMember]
            public string BankTypeCode { get; set; }
            [DataMember]
            public string BankTypedescp { get; set; }
            [DataMember]
            public string AddedBy { get; set; }

        }
    
}
