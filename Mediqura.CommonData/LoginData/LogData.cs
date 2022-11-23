using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Mediqura.CommonData.Common;

namespace Mediqura.CommonData.LoginData
{
    [Serializable]
    public class LogData : BaseData
    {
        //User Login detail
        [DataMember]
        public Int64 UserLoginId { get; set; }
        [DataMember]
        public int UserTypeID { get; set; }
        [DataMember]
        public string UserTypeCode { get; set; }
        [DataMember]
        public string UserTypeName { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public Guid LoginUniqueId { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int Status { get; set; }
       
        [DataMember]
        public string FinancialYearCode { get; set; }
        [DataMember]
        public string FinacialYear { get; set; }       
        [DataMember]
        public string HospitalCode { get; set; }
        [DataMember]
        public string HospitalName { get; set; }
        [DataMember]
        public string HospitalAddress { get; set; }
        [DataMember]
        public string HospitalPhoneSTDCode { get; set; }
        [DataMember]
        public string HospitalPhoneNo { get; set; }
        [DataMember]
        public string HospitalPhoneAltNo { get; set; }
        [DataMember]
        public string HospitalMobileNo { get; set; }
        [DataMember]
        public string HospitalFaxNo { get; set; }
        [DataMember]
        public string HospitalEmailID { get; set; }
        [DataMember]
        public string HospitalWebsite { get; set; }
        //Role Detail
        [DataMember]
        public string[] RoleNames { get; set; }
        [DataMember]
        public int[] RoleIds { get; set; }
        
        //Navigation Data
        [DataMember]
        public string NavigationXmlData { get; set; }        
        [DataMember]
        public Int64 LoginID { get; set; }
        [DataMember]
        public string UserPassword { get; set; }
        [DataMember]
        public string RoleName { get; set; }
        [DataMember]
        public string LoginDate { get; set; }
        [DataMember]
        public string LogOutDate { get; set; }
        [DataMember]
        public string LoginTime { get; set; }
        [DataMember]
        public string Logintime { get; set; }
        [DataMember]
        public string LogOuttime { get; set; }
        [DataMember]
        public string LogOutTime { get; set; }
        [DataMember]
        public int scheduleID { get; set; }
        [DataMember]
        public int SiteMapID { get; set; }
        // Access Role for Buttons such as Delete, Add, Print etc.
        public Boolean IsAddOn { get; set; }
        [DataMember]
        public Boolean IsModify { get; set; }
        [DataMember]
        public string ServerDateTime { get; set; }
       
        [DataMember]
        public int SaveEnable { get; set; }
        [DataMember]
        public int UpdateEnable { get; set; }
        [DataMember]
        public int SearchEnable { get; set; }
        [DataMember]
        public int EditEnable { get; set; }
        [DataMember]
        public int DeleteEnable { get; set; }
        [DataMember]
        public int PrintEnable { get; set; }
        [DataMember]
        public int ExportEnable { get; set; }
        
        [DataMember]
        public int ActivateLogin { get; set; }
        [DataMember]
        public int isNotify { get; set; }
        [DataMember]
        public string DashBoardUrl { get; set; }
        [DataMember]
        public string PhrPatientlistdUrl { get; set; }
        [DataMember]
        public string FPData { get; set; }
        [DataMember]
        public int BillSetting { get; set; }
        [DataMember]
        public int GenSubStockID { get; set; }
        [DataMember]
        public int GenItemRequestEnable { get; set; }
        [DataMember]
        public int GenItemApproveEnable { get; set; }
        [DataMember]
        public int GenItemHandoverEnable { get; set; }
        [DataMember]
        public int GenItemVerifyEnable { get; set; }
        [DataMember]
        public int MedSubStockID { get; set; }
        [DataMember]
        public int MedItemRequestEnable { get; set; }
        [DataMember]
        public int MedItemApproveEnable { get; set; }
        [DataMember]
        public int MedItemHandoverEnable { get; set; }
        [DataMember]
        public int MedItemVerifyEnable { get; set; }
        [DataMember]
        public int IsActiveLogin { get; set; }
        [DataMember]
        public int EnableMultiLogin { get; set; }
       
        [DataMember]
        public string DepartmentName { get; set; }
        //HR&payroll
        [DataMember]
        public int LeaveRequestEnable { get; set; }
        [DataMember]
        public int LeaveApproveEnable { get; set; }
        [DataMember]
        public int RosterUpdateEnable { get; set; }
        [DataMember]
        public int RosterChangeRequestEnable { get; set; }
        [DataMember]
        public int RosterChangeApproveEnable { get; set; }

    }
}
