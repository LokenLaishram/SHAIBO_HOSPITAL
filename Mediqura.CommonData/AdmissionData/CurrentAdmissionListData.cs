using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.AdmissionData
{
    public class CurrentAdmissionListData : BaseData
    {
        [DataMember]
        public Int32 wardId { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string wardName { get; set; }
        [DataMember]
        public DateTime AdmitedOn { get; set; }
        [DataMember]
        public string RegNo { get; set; }
        [DataMember]
        public string PatName { get; set; }
        [DataMember]
        public string patAddress { get; set; }
        [DataMember]
        public string gender { get; set; }
        [DataMember]
        public string Agecount { get; set; }
        [DataMember]
        public string BedNo { get; set; }
        [DataMember]
        public string Room { get; set; }
        [DataMember]
        public string Doctor { get; set; }
        [DataMember]
        public string Diagnosis { get; set; }
        [DataMember]
        public int isReleased { get; set; }
        [DataMember]
        public int PatientActive { get; set; }
        [DataMember]
        public int IsSubHeading { get; set; }
        [DataMember]
        public int IsDischargeReady { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public int PatCategory { get; set; }
        [DataMember]
        public decimal TotalBill { get; set; }
        [DataMember]
        public decimal TotalAdvance { get; set; }
        [DataMember]
        public decimal Payable { get; set; }
        [DataMember]
        public decimal Refundable { get; set; }
        [DataMember]
        public int IsPhrBillClear { get; set; }
        [DataMember]
        public Decimal PHRloweLimit { get; set; }
        [DataMember]
        public Decimal PHRupperLimit { get; set; }
        [DataMember]
        public int ChkCredit { get; set; }
    }
}
