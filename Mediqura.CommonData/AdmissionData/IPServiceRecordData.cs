using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.AdmissionData
{
    public class IPServiceRecordData : BaseData
    {
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public Int64 IPServiceID { get; set; }
        [DataMember]
        public Int64 SubStockID { get; set; }
        [DataMember]
        public Int64 StockID { get; set; }
       
        [DataMember]
        public Int64 DoctorID { get; set; }
        [DataMember]
        public Int64 DocID { get; set; }
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public int LabServiceID { get; set; }
        [DataMember]
        public Decimal LabServiceCharge { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public Decimal NetServiceCharge { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public int SerialID { get; set; }
        [DataMember]
        public int ServiceID { get; set; }
        [DataMember]
        public int ItemID { get; set; }
        [DataMember]
        public int RefferaDocID { get; set; }
        [DataMember]
        public int TotalQuantity { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public int DischargeStatus { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
       
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public Decimal ServiceCharge { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public int ServiceTypeID { get; set; }
        [DataMember]
        public string DetailIpnumber { get; set; }
        [DataMember]
        public string InvNumber { get; set; }
        [DataMember]
        public int ServiceCategoryID { get; set; }
        [DataMember]
        public int SubGroupID { get; set; }
        [DataMember]
        public int TestCenterID { get; set; }
        [DataMember]
        public int UrgencyID { get; set; }
        [DataMember]
        public Int32 ResultOutput { get; set; }
        [DataMember]
        public string ServiceNumber { get; set; }
        [DataMember]
        public string Otpassnumber { get; set; }
        [DataMember]
        public int isMsbPatient { get; set; }
        [DataMember]
        public int isMsbDoctor { get; set; }
        [DataMember]
        public int MsbPc { get; set; }
        [DataMember]
        public DateTime ServiceDate { get; set; }
        [DataMember]
        public string ServiceDatetime { get; set; }
        [DataMember]
        public string Doctor { get; set; }
        [DataMember]
        public string GroupNumber { get; set; }
        [DataMember]
        public int IsDeviceInitiated { get; set; }
    }
    public class IPDServiceListDataTOeXCEL
    {
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string ServiceNumber { get; set; }
        [DataMember]
        public string InvNumber { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public Decimal ServiceCharge { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public Decimal NetServiceCharge { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
    }
    public class IPServiceListDataTOeXCEL
    {
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public Decimal ServiceCharge { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public Decimal NetServiceCharge { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }


    }

    public class IPDBillSettingData : BaseData
    {
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public Int64 IPServiceID { get; set; }
        
        [DataMember]
        public Int64 DoctorID { get; set; }
        [DataMember]
        public Int64 DocID { get; set; }
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public Decimal Rate { get; set; }
        [DataMember]
        public Decimal NetServiceCharge { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public int SerialID { get; set; }
        [DataMember]
        public int ServiceID { get; set; }
        [DataMember]
        public int ItemID { get; set; }
        [DataMember]
        public int RefferaDocID { get; set; }
        [DataMember]
        public int TotalQuantity { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public int DischargeStatus { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public Decimal ServiceCharge { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public int ServiceTypeID { get; set; }
        [DataMember]
        public string DetailIpnumber { get; set; }
        [DataMember]
        public string InvNumber { get; set; }
        [DataMember]
        public int ServiceCategoryID { get; set; }
        [DataMember]
        public int SubGroupID { get; set; }
        [DataMember]
        public int TestCenterID { get; set; }
        [DataMember]
        public int UrgencyID { get; set; }
        [DataMember]
        public Int32 ResultOutput { get; set; }
        [DataMember]
        public string ServiceNumber { get; set; }
        [DataMember]
        public string Otpassnumber { get; set; }
        [DataMember]
        public int isMsbPatient { get; set; }
        [DataMember]
        public int isMsbDoctor { get; set; }
        [DataMember]
        public int MsbPc { get; set; }
        [DataMember]
        public DateTime ServiceDate { get; set; }
        [DataMember]
        public string ServiceDatetime { get; set; }
        [DataMember]
        public Int32 PatientCategory { get; set; }
    }

}
