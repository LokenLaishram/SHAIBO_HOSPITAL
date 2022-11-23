using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.Common
{

    [Serializable]
    public class BaseData
    {
        [DataMember]
        public string AddedBy { get; set; }
        [DataMember]
        public int RoleID { get; set; }
        [DataMember]
        public int IsRefresh { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }
        [DataMember]
        public DateTime LastModifiedDate { get; set; }
        [DataMember]
        public Int64 LastModifiedBy { get; set; }
        [DataMember]
        public Enumaction ActionType;
        [DataMember]
        public int CompanyID { get; set; }
        [DataMember]
        public int FinancialYearID { get; set; }
        [DataMember]
        public string FinancialYear { get; set; }
        [DataMember]
        public int HospitalID { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public string IsActiveALL { get; set; }
        [DataMember]
        public int CurrentIndex { get; set; }
        [DataMember]
        public int PageSize { get; set; }
        [DataMember]
        public int MaximumRows { get; set; }
        [DataMember]
        public string XMLData { get; set; }
        [DataMember]
        public string XMLPaidData { get; set; }
        [DataMember]
        public string Proce_XMLData { get; set; }
        [DataMember]
        public string Anas_XMLData { get; set; }
        [DataMember]
        public Int64 EmployeeID { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
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
        public byte[] BarcodeImage { get; set; }
        [DataMember]
        public byte[] QRImage { get; set; }
        [DataMember]
        public string IPBarcode { get; set; }
        [DataMember]
        public Int64 ReceivedBy { get; set; }
        [DataMember]
        public int BankID { get; set; }
        [DataMember]
        public int AmountEnable { get; set; }
        [DataMember]
        public int DesignationID { get; set; }
        [DataMember]
        public int DepartmentID { get; set; }
        [DataMember]
        public Decimal Amount { get; set; }
        [DataMember]
        public int PageID { get; set; }
        [DataMember]
        public int MenuHeaderID { get; set; }
        [DataMember]
        public string PageName { get; set; }
        [DataMember]
        public int IsView { get; set; }
        [DataMember]
        public int IsMenuheader { get; set; }
        [DataMember]
        public int PageStatus { get; set; }
    }
}
