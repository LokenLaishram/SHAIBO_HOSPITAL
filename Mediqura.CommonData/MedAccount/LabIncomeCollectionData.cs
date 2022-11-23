using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedAccount
{
    public class LabIncomeCollectionData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public String RefNo { get; set; }
        [DataMember]
        public String BillNo { get; set; }
        [DataMember]
        public String VoucherNo { get; set; }
        [DataMember]
        public Int32 IspackageCompany { get; set; }
        [DataMember]
        public String PatientName { get; set; }
        [DataMember]
        public String PatientAddress { get; set; }
        [DataMember]
        public String TestNameList { get; set; }
        [DataMember]
        public Decimal TotalBillAmount { get; set; }
        [DataMember]
        public Decimal DiscountedAmount { get; set; }
        [DataMember]
        public Decimal PayableAmount { get; set; }
        [DataMember]
        public Decimal PaidAmount { get; set; }
        [DataMember]
        public Decimal DueAmount { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        //-------Expditure Data ----------------//
        [DataMember]
        public Int32 RecordComeFromID { get; set; }
        [DataMember]
        public Int32 TransactionTypeID { get; set; }
        [DataMember]
        public Decimal TransactionAmount { get; set; }
        [DataMember]
        public String TransactionNaration { get; set; }
        [DataMember]
        public DateTime TransactionDate { get; set; }
        //------Grand Total Amount--------------//
        [DataMember]
        public Decimal GTotalAmount { get; set; }
        [DataMember]
        public Decimal GTotalDiscountAmount { get; set; }
        [DataMember]
        public Decimal GTotalPayableAmount { get; set; }
        [DataMember]
        public Decimal GTotalPaidAmount { get; set; }
        [DataMember]
        public Decimal GTotalDueAmount { get; set; }
        // -----Total Expenditure-------------//
        [DataMember]
        public Decimal TotalIncome { get; set; }
        [DataMember]
        public Decimal TotalExpenditure { get; set; }
        [DataMember]
        public Decimal TotalIncomeBalance { get; set; }
        [DataMember]
        public String Remarks { get; set; }

    }
}
