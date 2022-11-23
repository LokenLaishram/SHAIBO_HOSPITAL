using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedAccount
{
    public class InsuranceReceivableData : BaseData
    {
        [DataMember]
        public string BillNo { get; set; }
        [DataMember]
        public Int32 ServiceTypeID { get; set; }
        [DataMember]
        public string ServiceType { get; set; }
        [DataMember]
        public Int32 PatientCategoryID { get; set; }
        [DataMember]
        public string PatientCategory { get; set; }
        [DataMember]
        public Int32 SubCategoryID { get; set; }
        [DataMember]
        public string SubCategory { get; set; }
        [DataMember]
        public  Int64 UHID { get; set; }
        [DataMember]
        public decimal DiscountAmount { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public int IsReceived { get; set; }
        [DataMember]
        public int Receivable { get; set; }
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public int PaymentMode { get; set; }
        
    }
    public class InsuranceReceivableDataToExcel
    {
        [DataMember]
        public string ServiceType { get; set; }
        [DataMember]
        public string UHID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string BillNo { get; set; }
        [DataMember]
        public string PatientCategory { get; set; }
        [DataMember]
        public string SubCategory { get; set; }
        [DataMember]
        public string DiscountAmount { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
        [DataMember]
        public string Received { get; set; }
     }
}
