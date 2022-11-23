﻿using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedPharData
{
    public class PharIPIssueData : BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string IPDrgIssueNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string IPPatientName { get; set; }
        [DataMember]
        public double Height { get; set; }
        [DataMember]
        public double Weight { get; set; }
        [DataMember]
        public string GenderName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public DateTime DOA { get; set; }
        [DataMember]
        public string DOAdmission { get; set; }
        [DataMember]
        public string WardBedNo { get; set; }
        [DataMember]
        public decimal PHRLowerLimit { get; set; }
        [DataMember]
        public decimal PHRUpperLimit { get; set; }
        [DataMember]
        public int PHRcreditAlowed { get; set; }
        [DataMember]
        public decimal Payable { get; set; }
        [DataMember]
        public int MedSubStockID { get; set; }
        [DataMember]
        public int SubStockID { get; set; }
        [DataMember]
        public int DrugID { get; set; }
        [DataMember]
        public string DrugName { get; set; }
        [DataMember]
        public int ItemID { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string DrugComposition { get; set; }
        [DataMember]
        public Decimal NoUnit { get; set; }
        [DataMember]
        public Decimal EquivalentQty { get; set; }
        [DataMember]
        public Decimal Rate { get; set; }
        [DataMember]
        public Decimal CPperQty { get; set; }
        [DataMember]
        public Decimal MRPperQty { get; set; }
        [DataMember]
        public Decimal NetCharge { get; set; }
        [DataMember]
        public Int64 DoctorID { get; set; }
        [DataMember]
        public string DoctorName { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public bool Status { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public int Result { get; set; }

        //-----BANK & Bill DETAILS-------//
        public int Paymode { get; set; }
        [DataMember]
        public string BankName { get; set; }
        [DataMember]
        public string AC { get; set; }
        [DataMember]
        public string Invoicenumber { get; set; }
        [DataMember]
        public string Cheque { get; set; }
        [DataMember]
        public string IPaddress { get; set; }
        [DataMember]
        public Int64 CollectedByID { get; set; }
        [DataMember]
        public string CardNo_ChequeNo { get; set; }
        [DataMember]
        public string InvoiceNo { get; set; }
        [DataMember]
        public Decimal TotalBillAmount { get; set; }
        [DataMember]
        public Decimal Discount { get; set; }
        [DataMember]
        public Decimal PaidAmount { get; set; }
        [DataMember]
        public Decimal DueAmount { get; set; }
        [DataMember]
        public Int64 DueReponsibleBy { get; set; }
        //-----TOTAL-----//
        [DataMember]
        public Decimal TotalMRPperQty { get; set; }
        [DataMember]
        public Decimal TotalNoUnit { get; set; }
        [DataMember]
        public Decimal TotalEqvQty { get; set; }
        [DataMember]
        public Decimal TotalNetCharge { get; set; }
        [DataMember]
        public Decimal DepositAmount { get; set; }
        [DataMember]
        public string DepositNos { get; set; }  
    }
}
