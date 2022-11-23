using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.AdmissionData
{
    public class DueCollectionData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string IPnoEmrgNo { get; set; }
        [DataMember]
        public string BillNo { get; set; }
        [DataMember]
        public Decimal LastDuePaid { get; set; }
        [DataMember]
        public Decimal LastPaidDue { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string EMRGNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
       
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public Decimal DueAmount { get; set; }
        [DataMember]
        public Decimal DueBalance { get; set; }
        [DataMember]
        public Decimal DiscountAmount { get; set; }
        [DataMember]
        public Decimal Paidamnt { get; set; }
        [DataMember]
        public Decimal CurrentDue { get; set; }
        [DataMember]
        public Decimal Discount { get; set; }
        [DataMember]
        public Int32 PatientCategory { get; set; }
        [DataMember]
        public Decimal TotalDueAmount { get; set; }
        [DataMember]
        public Decimal TotalLastPaid { get; set; }
        [DataMember]
        public Decimal TotalDueBalance { get; set; }
        [DataMember]
        public Decimal TotalDiscountAmnt { get; set; }
        [DataMember]
        public Decimal DiscountAfterDue { get; set; }
        [DataMember]
        public string Agecount { get; set; }
        [DataMember]
        public string AdmissionDoctor { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public string PatRelatives { get; set; }
        [DataMember]
        public Int32 PaymentMode { get; set; }
        [DataMember]
        public Int32 SettlementModeID { get; set; }
        [DataMember]
        public string SettlementMode { get; set; }
        [DataMember]
        public string Chequenumber { get; set; }
        [DataMember]
        public DateTime AdmissionDate { get; set; }
        [DataMember]
        public Int32 PatientType { get; set; }
        [DataMember]
        public string DiscountRemarks { get; set; }

    }
    public class DueCollectionDataDataTOeXCEL
    {

        [DataMember]
        public string BillNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string IPnoEmrgNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public Decimal DueAmount { get; set; }
        [DataMember]
        public Decimal LastDuePaid { get; set; }
        [DataMember]
        public Decimal DueBalance { get; set; }
        //[DataMember]
        //public string EmpName { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }

    }
}
