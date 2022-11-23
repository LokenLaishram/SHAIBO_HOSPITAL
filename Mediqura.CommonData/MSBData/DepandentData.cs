using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MSBData
{
    public class DepandentData : BaseData
    {
        [DataMember]
        public string MaritalStatus { get; set; }
        [DataMember]
        public Int32 MaritalStatusID { get; set; }
        [DataMember]
        public string Relation { get; set; }
        [DataMember]
        public Int32 Age { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public int ID { get; set; }
     
    }
    public class DepandentDataExcel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string MaritalStatus { get; set; }
        [DataMember]
        public string Relation { get; set; }
        [DataMember]
        public Int32 Age { get; set; }
       
    }

    public class MsbDepandentData :BaseData
    {
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string DependentName { get; set; }
        [DataMember]
        public string DependentAddress { get; set; }
        [DataMember]
        public string DependentAge{ get; set; }
        [DataMember]
        public string DependentDob { get; set; }
        [DataMember]
        public int DependentSex { get; set; }
        [DataMember]
        public int DependentState{ get; set; }
        [DataMember]
        public int DependentDistrict { get; set; }
        [DataMember]
        public int DependentPin { get; set; }
        [DataMember]
        public int DependentRelation { get; set; }
        [DataMember]
        public int DependentMarital { get; set; }
        [DataMember]
        public string DependentOccupation { get; set; }
        [DataMember]
        public string DependentContact { get; set; }
        [DataMember]
        public decimal DependentIncome { get; set; }
        [DataMember]
        public DateTime Applydate { get; set; }
        [DataMember]
        public DateTime IssueDate { get; set; }
        [DataMember]
        public DateTime ValidUpto { get; set; }
        [DataMember]
        public int Activation { get; set; }

        [DataMember]
        public string sApplydate { get; set; }
        [DataMember]
        public string sIssueDate { get; set; }
        [DataMember]
        public string sValidUpto { get; set; }

        [DataMember]
        public Int64 EmpID { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public String EmpAge { get; set; }
        [DataMember]
        public int EmpSex { get; set; }
        [DataMember]
        public string EmpAddress { get; set; }
        [DataMember]
        public int EmpMarital { get; set; }
        [DataMember]
        public int EmpDesignation { get; set; }
        [DataMember]
        public int EmpDepartment { get; set; }
        [DataMember]
        public int EmpGrade { get; set; }
        [DataMember]
        public String EmpContact { get; set; }
        [DataMember]
        public int DependentNo { get; set; }
        [DataMember]
        public int IsValid { get; set; }
        [DataMember]
        public int MsbType { get; set; }
        [DataMember]
        public int isSpecial { get; set; }
        [DataMember]
        public int EligibilityAge { get; set; }
        [DataMember]
        public String Remarks { get; set; }
        [DataMember]
        public int ID { get; set; }

    }

    public class MsbDepandentDataExcel
    {
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string DependentName { get; set; }
        [DataMember]
        public string DependentAddress { get; set; }
        [DataMember]
        public int DependentRelation { get; set; }
        [DataMember]
        public string sApplydate { get; set; }
        [DataMember]
        public string sIssueDate { get; set; }
        [DataMember]
        public string sValidUpto { get; set; }

        [DataMember]
        public string EmpName { get; set; }
       

    }
}
