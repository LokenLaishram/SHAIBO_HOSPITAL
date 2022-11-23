using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedBillData
{
    public class ActivityTrackerData:BaseData
    {            
            [DataMember]
            public int ID { get; set; }
            [DataMember]
            public Int64 UHID { get; set; }
            [DataMember]
            public string PatientName { get; set; }
            [DataMember]
            public string Address { get; set; }
            [DataMember]
            public string Number { get; set; }
            [DataMember]
            public decimal Deposit { get; set; }
            [DataMember]
            public decimal Adjusted { get; set; }
            [DataMember]
            public decimal Refund { get; set; }
            [DataMember]
            public decimal CancelAmt { get; set; }
            [DataMember]
            public string EmpName { get; set; }
            [DataMember]
            public DateTime Date { get; set; }
            [DataMember]
            public DateTime DateFrom { get; set; }
            [DataMember]
            public DateTime DateTo { get; set; }
            [DataMember]
            public decimal DepositAmount { get; set; }
            [DataMember]
            public decimal AdustedAmount { get; set; }
            [DataMember]
            public decimal BalanceAmount { get; set; }
            [DataMember]
            public decimal TotalRefund { get; set; }
            [DataMember]
            public decimal TdepositAmt { get; set; }
            [DataMember]
            public decimal TadjustedAmt { get; set; }
            [DataMember]
            public decimal TrefAmt { get; set; }
            [DataMember]
            public decimal TbalanceAmt { get; set; }

        }
        public class ActivityDataTOeXCEL
        {
          
            [DataMember]
            public Int64 UHID { get; set; }
            [DataMember]
            public string PatientName { get; set; }
            [DataMember]
            public decimal Deposit { get; set; }
            [DataMember]
            public decimal Adjusted { get; set; }
            [DataMember]
            public decimal Refund { get; set; }
            [DataMember]
            public decimal CancelAmt { get; set; }
            [DataMember]
            public decimal TbalanceAmt { get; set; }
            [DataMember]
            public decimal TrefAmt { get; set; }
            [DataMember]
            public decimal TadjustedAmt { get; set; }
            [DataMember]
            public decimal TdepositAmt { get; set; }
            [DataMember]
            public DateTime Date { get; set; }
        }
    
}
