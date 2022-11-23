using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedBillData
{
    public class DoctorWiseCollectionMasterData: BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public Int64 BillID { get; set; }
        [DataMember]
        public String BillNo { get; set; }
        [DataMember]
        public String PatientName { get; set; }
        [DataMember]
        public int DepartmentID { get; set; }
        [DataMember]
        public Int64 CommissionID { get; set; }
        [DataMember]
        public int Servicetype { get; set; }
        [DataMember]
        public String ServicetypeName { get; set; }
        [DataMember]
        public String ServiceName { get; set; }
        [DataMember]
        public int ServiceID { get; set; }
        [DataMember]
        public int Doctortype { get; set; }
        [DataMember]
        public String DoctortypeName { get; set; }
        [DataMember]
        public String DoctorName { get; set; }
        [DataMember]
        public String Remarks { get; set; }
        [DataMember]
        public String IsVerified { get; set; }
        [DataMember]
        public Int32 verify { get; set; }
        [DataMember]
        public String IsPaid { get; set; }
        [DataMember]
        public Int32 paid { get; set; }
        [DataMember]
        public Int64 verifyBy { get; set; }
        [DataMember]
        public int DoctorID { get; set; }
        [DataMember]
        public Decimal ServiceCharge { get; set; }
        [DataMember]
        public Decimal Commission { get; set; }
        [DataMember]
        public Decimal Tax { get; set; }
        [DataMember]
        public Decimal Hospitalcharge { get; set; }
        [DataMember]
        public Decimal DoctorPayable { get; set; }
        [DataMember]
        public Decimal TotalServiceCharge { get; set; }
        [DataMember]
        public Decimal TotalCommission { get; set; }
        [DataMember]
        public Decimal TotalTax { get; set; }
        [DataMember]
        public Decimal TotalHospitalCharge { get; set; }
        [DataMember]
        public Decimal TotalPayable { get; set; }
        [DataMember]
        public Decimal Payable { get; set; }
        [DataMember]
        public Decimal SubPayable { get; set; }
        [DataMember]
        public Decimal TotalAmount { get; set; }
        [DataMember]
        public Decimal Amount { get; set; }
        [DataMember]
        public Decimal TotalDue { get; set; }
        [DataMember]
        public DateTime LastVisitDate { get; set; }
        [DataMember]
        public Int64 AddedByID { get; set; }
        [DataMember]
        public Int32 Month { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public int HospitalID { get; set; }
        [DataMember]
        public int FinancialYearID { get; set; }
        [DataMember]
        public string XMLData { get; set; }
        [DataMember]
        public Int64 PayID { get; set; }
        [DataMember]
        public decimal LastDue { get; set; }
        [DataMember]
        public decimal Due { get; set; }
        [DataMember]
        public DateTime paydate { get; set; }
    }
    public class DoctorWiseCollectionMasterDataToExcel
    {
        public Int64 UHID { get; set; }
        [DataMember]
        public String BillNo { get; set; }
        [DataMember]
        public String PatientName { get; set; }
        [DataMember]
        public String ServicetypeName { get; set; }
        [DataMember]
        public String ServiceName { get; set; }
        [DataMember]
        public Decimal ServiceCharge { get; set; }
        [DataMember]
        public Decimal Commission { get; set; }
        [DataMember]
        public Decimal Tax { get; set; }
        [DataMember]
        public Decimal Hospitalcharge { get; set; }
        [DataMember]
        public Decimal DoctorPayable { get; set; }
        [DataMember]
        public DateTime LastVisitDate { get; set; }
      
      
    }
    public class DoctorWisePaymentMasterDataToExcel
    {
        [DataMember]
        public Int64 PayID { get; set; }
        [DataMember]
        public Decimal Payable { get; set; }
        [DataMember]
        public Decimal SubPayable { get; set; }
        [DataMember]
        public decimal LastDue { get; set; }
        [DataMember]
        public Decimal Amount { get; set; }
        [DataMember]
        public decimal Due { get; set; }
        [DataMember]
        public DateTime paydate { get; set; }
    }
}
