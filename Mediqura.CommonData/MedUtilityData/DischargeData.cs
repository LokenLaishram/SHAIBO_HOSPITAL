using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class DischargeData:BaseData
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 DischargeTypeID { get; set; }
        [DataMember]
        public string DischargeTypeCode { get; set; }
        [DataMember]
        public string DischargeTypedescp { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string Template { get; set; }
        [DataMember]
        public Int64 TypeFeatureID { get; set; }
        [DataMember]
        public DateTime IssueDate { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string Agecount { get; set; }
        [DataMember]
        public string GenderName { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public string DoctorName { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public string PatientAddress { get; set; }
        [DataMember]
        public string District { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Pincode { get; set; }
        [DataMember]
        public string Contact { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public DateTime AdmissionDate { get; set; }
        [DataMember]
        public string DischargeDate { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }
        [DataMember]
        public int Action { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public Int64 DischargeDocID { get; set; }
        [DataMember]
        public Int32 WardID { get; set; }
        [DataMember]
        public string Ward { get; set; }
        [DataMember]
        public Int32 PatientCategory { get; set; }
    }
    public class DischargeDatatoExcel
    {
        [DataMember]
        public Int64 DischargeTypeID { get; set; }
        [DataMember]
        public string DischargeTypeCode { get; set; }
        [DataMember]
        public string DischargeTypedescp { get; set; }
        [DataMember]
        public string AddedBy { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }

    }
    public class DischargeListDatatoExcel
    {

        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string Ward { get; set; }
        [DataMember]
        public string DischargeTypedescp { get; set; }
        [DataMember]
        public string DoctorName { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }


    }
}
