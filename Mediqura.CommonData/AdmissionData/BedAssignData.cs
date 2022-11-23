using System;
using Mediqura.CommonData.Common;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.AdmissionData
{
    public class BedAssignData:BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string IPNo { get; set; } 
        [DataMember]
        public int BlockID { get; set; }
        [DataMember]
        public Int64 DocID { get; set; }
        [DataMember]
        public int DeptID { get; set; }
        [DataMember]
        public int FloorID { get; set; }
        [DataMember]
        public decimal AdmissionCharge { get; set; }
        [DataMember]
        public decimal TotalAdmissionCharge { get; set; }
        [DataMember]
        public string Cases { get; set; }
        [DataMember]
        public int PaymentMode { get; set; }
        [DataMember]
        public int DischargeStatus { get; set; }
        [DataMember]
        public int BedID { get; set; }
        [DataMember]
        public int WardID { get; set; }
        [DataMember]
        public string Block { get; set; }
        [DataMember]
        public string Floor1 { get; set; }
        [DataMember]
        public string Ward { get; set; }
        [DataMember]
        public string Room { get; set; }
        [DataMember]
        public string BedNo { get; set; }
        [DataMember]
        public Decimal Charges { get; set; }
        [DataMember]
        public Decimal NuCharges { get; set; }
        [DataMember]
        public string Remarks { get; set; }
    }
    public class BedAssignDatatoExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Block { get; set; }
        [DataMember]
        public string Floor1 { get; set; }
        [DataMember]
        public string Ward { get; set; }
        [DataMember]
        public string Room { get; set; }
        [DataMember]
        public string BedNo { get; set; }
        [DataMember]
        public Decimal Charges { get; set; }
        [DataMember]
        public string AddedBy { get; set; }

    }
}
