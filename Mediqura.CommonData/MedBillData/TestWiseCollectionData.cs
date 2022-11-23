using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Mediqura.CommonData.MedBillData
{
    public class TestWiseCollectionData : BaseData
    {
        [DataMember]
        public Int64 DischargeID { get; set; }
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int QTY { get; set; }
        [DataMember]
        public int lab_id { get; set; }
        [DataMember]
        public decimal TotalAmt { get; set; }
        [DataMember]
        public decimal Netamt { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public decimal charges { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public int ServiceID { get; set; }
        [DataMember]
        public int docID { get; set; }
        [DataMember]
        public string Services { get; set; }
        [DataMember]
        public string consultant { get; set; }
        [DataMember]
        public string DischargeDoc { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }
    }
    public class ServiceListDataTOeXCEL
    {
        [DataMember]
        public string Services { get; set; }
        [DataMember]
        public int QTY { get; set; }
        [DataMember]
        public decimal Netamt { get; set; }
        [DataMember]
        public decimal TotalAmt { get; set; }
      
    }
}
