using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedBillData;
using Mediqura.CommonData.PatientData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using OnBarcode.Barcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Reflection;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.BOL.MedBillBO;
using System.Text.RegularExpressions;
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;

namespace Mediqura.Web.MedBills
{
    public partial class OPLabBilling : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnprint.Attributes["disabled"] = "disabled";
                bindddl();
                if (Session["LAB_UHID"] != null)
                {
                    txtUHID.Text = Session["LAB_UHID"].ToString();
                    Int64 UHID = Convert.ToInt64(txtUHID.Text == "" ? "0" : txtUHID.Text);
                    if (Commonfunction.UpdatePatientAge("", UHID) == 1)
                        loadUHIDdata();
                    Session["LAB_UHID"] = null;
                }
                ViewState["BillID"] = null;
                if (Session["BILLID"] != null)
                {
                    Int64 ID = Convert.ToInt32(Session["BILLID"].ToString());
                    Session["BILLID"] = null;
                    getInvestigationDetails(ID);
                }
                Session["DiscountList"] = null;
            }
        }
        public void loadUHIDdata()
        {
            ddl_patienttype.SelectedIndex = 1;
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            if (ddl_patienttype.SelectedValue == "4")
            {
                bool isnumeric = txtUHID.Text.All(char.IsDigit);
                if (isnumeric == false)
                {
                    if (txtUHID.Text.Contains(":"))
                    {
                        bool isUHIDnumeric = txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                        Objpaic.UHID = isUHIDnumeric ? Convert.ToInt64(txtUHID.Text.Contains(":") ? txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1) : "0") : 0;
                    }
                    else
                    {
                        txtUHID.Text = "";
                        txtUHID.Focus();
                        return;
                    }
                }
                else
                {
                    Objpaic.UHID = Convert.ToInt64(txtUHID.Text == "" ? "0" : txtUHID.Text);
                }
            }
            if (ddl_patienttype.SelectedValue == "1")
            {

                if (txtUHID.Text.Contains(":"))
                {
                    Objpaic.OPnumber = txtUHID.Text.Contains(":") ? txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1) : "0";
                }
                else
                {
                    txtUHID.Text = "";
                    txtUHID.Focus();
                    return;
                }
            }
            Objpaic.PatientType = Convert.ToInt32(ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue);
            getResult = objInfoBO.GetPatientDetailsByUHIDandOpnumber(Objpaic);
            if (getResult.Count > 0)
            {
                txt_patcatgeory.Text = getResult[0].PatientCategory.ToString();
                txtUHID.Text = getResult[0].PatientDetailName.ToString();
                hdnuhid.Value = getResult[0].UHID.ToString();
                hdMsbPc.Value = getResult[0].MSBpc.ToString();
                hdPatCat.Value = getResult[0].PatientType.ToString();
                if (getResult[0].PatientType == 4)
                {
                    btnlinkdiscount.Visible = false;
                }
                txt_balanceinac.Text = Commonfunction.Getrounding((Convert.ToDecimal(getResult[0].BalanceAmount.ToString())).ToString());
                ddldepartment.Attributes.Remove("disabled");
                ddldoctor.Attributes.Remove("disabled");
                txt_labservices.ReadOnly = true;
                ddl_patienttype.Attributes["disabled"] = "disabled";
                ddl_servicecategory.Attributes.Remove("disabled");
                if (getResult[0].IspackageCompany == 1 && ddl_patienttype.SelectedValue == "1")
                {
                    ddl_servicecategory.SelectedValue = "2";
                    hdnIsPachageCompany.Value = getResult[0].IspackageCompany.ToString();
                    AutoCompleteExtender2.ContextKey = "2";
                    ddldepartment.SelectedValue = getResult[0].DepartmentID.ToString();
                    txt_tpacompany.Text = getResult[0].TPAcompanyName.ToString();
                    hdntpacompanyID.Value = getResult[0].TPAcompany.ToString();
                    MasterLookupBO mstlookup = new MasterLookupBO();
                    Commonfunction.PopulateDdl(ddldoctor, mstlookup.GetDepartmentDoctor(Convert.ToInt32(getResult[0].DepartmentID.ToString() == "" ? "0" : getResult[0].DepartmentID.ToString())));
                    ddldoctor.SelectedValue = getResult[0].DoctorID.ToString();
                    txt_opnumber.Text = txtUHID.Text.Contains(":") ? txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1) : "0";
                    ddldepartment.Attributes["disabled"] = "disabled";
                    ddldoctor.Attributes["disabled"] = "disabled";
                    txt_labservices.ReadOnly = false;
                }
                if ((getResult[0].IspackageCompany == 0 && ddl_patienttype.SelectedValue == "1"))
                {
                    ddl_servicecategory.SelectedValue = "1";
                    hdnIsPachageCompany.Value = "0";
                    AutoCompleteExtender2.ContextKey = "1";
                    ddldepartment.SelectedValue = getResult[0].DepartmentID.ToString();
                    txt_tpacompany.Text = "";
                    hdntpacompanyID.Value = null;
                    MasterLookupBO mstlookup = new MasterLookupBO();
                    Commonfunction.PopulateDdl(ddldoctor, mstlookup.GetDepartmentDoctor(Convert.ToInt32(getResult[0].DepartmentID.ToString() == "" ? "0" : getResult[0].DepartmentID.ToString())));
                    ddldoctor.SelectedValue = getResult[0].DoctorID.ToString();
                    txt_opnumber.Text = txtUHID.Text.Contains(":") ? txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1) : "0";
                    ddldepartment.Attributes["disabled"] = "disabled";
                    ddldoctor.Attributes["disabled"] = "disabled";
                    txt_labservices.ReadOnly = false;
                }
                if (ddl_patienttype.SelectedValue == "4")
                {
                    AutoCompleteExtender2.ContextKey = "1";
                    hdnIsPachageCompany.Value = "0";
                    ddldepartment.Attributes.Remove("disabled");
                    ddldoctor.Attributes.Remove("disabled");
                    ddl_source.Attributes.Remove("disabled");
                    txt_labservices.ReadOnly = false;
                }
                getpatientnumber();
            }
            else
            {
                txt_patcatgeory.Text = "";
                txt_tpacompany.Text = "";
                txtUHID.Text = "";
                txt_balanceinac.Text = "";
                txt_opnumber.Text = "";
                hdnuhid.Value = null;
                txtUHID.Focus();
            }
        }
        protected void btnprint_Click(object sender, EventArgs e)
        {
            string url = "../MedBills/Reports/ReportViewer.aspx?option=OPDLabBillReceipt&BillNo=" + txtbillNo.Text.ToString() + "&Ispacakge=" + hdnIsPachageCompany.Value;
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
        private void getInvestigationDetails(Int64 ID)
        {
            OPDbillingBO objInfoBO = new OPDbillingBO();
            List<LabBillingData> getResult = new List<LabBillingData>();
            getResult = objInfoBO.GetDiscountServiceByID(ID);
            if (getResult.Count > 0)
            {
                ViewState["BillID"] = getResult[0].BillID.ToString();
                PatientData Objpaic = new PatientData();
                RegistrationBO objBO = new RegistrationBO();
                List<PatientData> Result = new List<PatientData>();

                Objpaic.UHID = Convert.ToInt64(getResult[0].UHID.ToString());
                Result = objBO.GetPatientDetailsByUHID(Objpaic);
                if (Result.Count > 0)
                {
                    txtUHID.Text = Result[0].PatientDetailName.ToString();
                }
                ddl_patienttype.SelectedValue = getResult[0].PatientType.ToString();
                hdPatCat.Value = getResult[0].PatientType.ToString();
                txt_balanceinac.Text = Commonfunction.Getrounding((Convert.ToDecimal(getResult[0].BalanceAmount.ToString())).ToString());
                Session["ServiceList"] = null;
                Session["DiscountList"] = null;
                ddl_patienttype.Attributes["disabled"] = "disabled";
                ddl_servicecategory.Attributes.Remove("disabled");
                txtquantity.ReadOnly = true;
                btnsave.Visible = false;
                btnDisSave.Visible = true;
                txtremarkdisc.ReadOnly = true;
                txtremarkdisc.Text = getResult[0].Remarks.ToString() == null ? " " : getResult[0].Remarks.ToString();
                txt_totalbill.Text = Commonfunction.Getrounding(getResult[0].TotalBill.ToString());
                txt_discount.Text = Commonfunction.Getrounding(getResult[0].TotalDiscountedAmount.ToString());
                txt_balanceinac.Text = Commonfunction.Getrounding(getResult[0].BalanceAmount.ToString());
                txt_adjustedamount.Text = Commonfunction.Getrounding(getResult[0].AdjustedAmount.ToString());
                txt_paidamount.Text = Commonfunction.Getrounding(getResult[0].TotalPaidAmount.ToString());
                ddl_source.Attributes["disabled"] = "disabled";
                txt_referal.Attributes["disabled"] = "disabled";
                ddl_source.SelectedValue = getResult[0].SourceID.ToString();
                txt_referal.Text = getResult[0].ReferalName.ToString();
                GVDiscountApprovalList.DataSource = getResult;
                GVDiscountApprovalList.DataBind();
                GVDiscountApprovalList.Visible = true;
            }
            else
            {
                ddl_source.Attributes.Remove("disabled");
                txt_referal.Attributes.Remove("disabled");
                Messagealert_.ShowMessage(lblmessage, "Bill Already created!", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
        }
        private void bindddl()
        {
            hdisMsbDoctor.Value = "0";
            txtDiscount.Text = "0";
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_patienttype, mstlookup.GetLookupsList(LookupName.OPLabPatientcategory));
            Commonfunction.PopulateDdl(ddlpaymentmode, mstlookup.GetLookupsList(LookupName.PaymentMode));
            Commonfunction.PopulateDdl(ddlRunnerID, mstlookup.GetLookupsList(LookupName.RunnerList));
            Commonfunction.PopulateDdl(ddlRunnerBy, mstlookup.GetLookupsList(LookupName.RunnerList));
            Commonfunction.PopulateDdl(ddlpaymentmodes, mstlookup.GetLookupsList(LookupName.PaymentMode));
            ddlpaymentmode.SelectedIndex = 1;
            if (ddlRunnerID.DataSource != null)
            {
                ddlRunnerID.SelectedIndex = 1;
            }
           
            Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.OPDepartment));
            Commonfunction.PopulateDdl(ddlcollectedby, mstlookup.GetLookupsList(LookupName.CollectedBy));
            txtdatefrom.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            txttotalbillamount.Text = "0.00";
            txt_totaldue.Text = "0.00";
            txttotaldiscounted.Text = "0.00";
            Session["LabServiceList"] = null;
            Session["DiscountList"] = null;
            Commonfunction.Insertzeroitemindex(ddldoctor);
            ddl_source.Attributes["disabled"] = "disabled";
            ddldepartment.Attributes["disabled"] = "disabled";
            ddldoctor.Attributes["disabled"] = "disabled";
            txt_labservices.ReadOnly = true;
            txt_labservicecharge.ReadOnly = true;
            ddl_servicecategory.Attributes["disabled"] = "disabled";
            txt_due.Attributes["disabled"] = "disabled";
            hdnIsPachageCompany.Value = "0";
            //ddl_patienttype.SelectedValue = "4";
            btnsave.Attributes["disabled"] = "disabled";
            if (ddl_patienttype.SelectedValue == "4")
            {
                AutoCompleteExtender2.ContextKey = "1";
                ddl_source.Attributes.Remove("disabled");
            }
            AutoCompleteExtender3.ContextKey = ddl_patienttype.SelectedValue;
            txt_referal.Attributes["disabled"] = "disabled";
            Commonfunction.PopulateDdl(ddl_TestCenter, mstlookup.GetLookupsList(LookupName.TestCenter));

            ddl_TestCenter.SelectedIndex = 1;
        }
        protected void ddl_servicecategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender2.ContextKey = ddl_servicecategory.SelectedValue;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetLabServices(string prefixText, int count, string contextKey)
        {
            LabServiceMasterData Objpaic = new LabServiceMasterData();
            LabServiceMasterBO objInfoBO = new LabServiceMasterBO();
            List<LabServiceMasterData> getResult = new List<LabServiceMasterData>();
            Objpaic.TestName = prefixText;
            Objpaic.LabSubGroupID = Convert.ToInt32(contextKey == "" ? "0" : contextKey);
            getResult = objInfoBO.GetOPLabServices(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].TestName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetDiscountBy(string prefixText, int count, string contextKey)
        {
            EmployeeData Objpaic = new EmployeeData();
            EmployeeBO objInfoBO = new EmployeeBO();
            List<EmployeeData> getResult = new List<EmployeeData>();
            Objpaic.EmpName = prefixText;
            getResult = objInfoBO.GetdiscountBy(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmpName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetInv(string prefixText, int count, string contextKey)
        {
            SampleCollectionData Objpaic = new SampleCollectionData();
            LabSampleCollctionBO objInfoBO = new LabSampleCollctionBO();
            List<SampleCollectionData> getResult = new List<SampleCollectionData>();
            Objpaic.Investigationumber = prefixText;

            getResult = objInfoBO.GetOPInvnumber(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].Investigationumber.ToString());
            }
            return list;
        }
        protected void txtUHID_TextChanged(object sender, EventArgs e)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            if (ddl_patienttype.SelectedValue == "4")
            {
                bool isnumeric = txtUHID.Text.All(char.IsDigit);
                if (isnumeric == false)
                {
                    if (txtUHID.Text.Contains(":"))
                    {
                        bool isUHIDnumeric = txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                        Objpaic.UHID = isUHIDnumeric ? Convert.ToInt64(txtUHID.Text.Contains(":") ? txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1) : "0") : 0;
                    }
                    else
                    {
                        txtUHID.Text = "";
                        txtUHID.Focus();
                        return;
                    }
                }
                else
                {
                    Objpaic.UHID = Convert.ToInt64(txtUHID.Text == "" ? "0" : txtUHID.Text);
                }
            }
            if (ddl_patienttype.SelectedValue == "1")
            {

                if (txtUHID.Text.Contains(":"))
                {
                    Objpaic.OPnumber = txtUHID.Text.Contains(":") ? txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1) : "0";
                }
                else
                {
                    txtUHID.Text = "";
                    txtUHID.Focus();
                    return;
                }
            }
            Objpaic.PatientType = Convert.ToInt32(ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue);
            getResult = objInfoBO.GetPatientDetailsByUHIDandOpnumber(Objpaic);
            if (getResult.Count > 0)
            {
                txt_patcatgeory.Text = getResult[0].PatientCategory.ToString();
                txtUHID.Text = getResult[0].PatientDetailName.ToString();
                hdnuhid.Value = getResult[0].UHID.ToString();
                hdMsbPc.Value = getResult[0].MSBpc.ToString();
                hdPatCat.Value = getResult[0].PatientType.ToString();
                if (getResult[0].PatientType == 4)
                {
                    btnlinkdiscount.Visible = false;
                }
                txt_balanceinac.Text = Commonfunction.Getrounding((Convert.ToDecimal(getResult[0].BalanceAmount.ToString())).ToString());
                ddldepartment.Attributes.Remove("disabled");
                ddldoctor.Attributes.Remove("disabled");
                txt_labservices.ReadOnly = true;
                ddl_patienttype.Attributes["disabled"] = "disabled";
                if (getResult[0].IspackageCompany == 1 && ddl_patienttype.SelectedValue == "1")
                {
                    ddl_servicecategory.SelectedValue = "2";
                    hdnIsPachageCompany.Value = getResult[0].IspackageCompany.ToString();
                    AutoCompleteExtender2.ContextKey = "2";
                    ddldepartment.SelectedValue = getResult[0].DepartmentID.ToString();
                    txt_tpacompany.Text = getResult[0].TPAcompanyName.ToString();
                    hdntpacompanyID.Value = getResult[0].TPAcompany.ToString();
                    MasterLookupBO mstlookup = new MasterLookupBO();
                    Commonfunction.PopulateDdl(ddldoctor, mstlookup.GetDepartmentDoctor(Convert.ToInt32(getResult[0].DepartmentID.ToString() == "" ? "0" : getResult[0].DepartmentID.ToString())));
                    ddldoctor.SelectedValue = getResult[0].DoctorID.ToString();
                    txt_opnumber.Text = txtUHID.Text.Contains(":") ? txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1) : "0";
                    ddldepartment.Attributes["disabled"] = "disabled";
                    ddldoctor.Attributes["disabled"] = "disabled";
                    txt_labservices.ReadOnly = false;
                    OPDbillingBO objstdBO = new OPDbillingBO();
                    int excludeMsb = objstdBO.checkMsbDoctor(Convert.ToInt64(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue));
                    hdisMsbDoctor.Value = excludeMsb == 1 ? "0" : "1";
                }
                if ((getResult[0].IspackageCompany == 0 && ddl_patienttype.SelectedValue == "1"))
                {
                    ddl_servicecategory.SelectedValue = "1";
                    hdnIsPachageCompany.Value = "0";
                    AutoCompleteExtender2.ContextKey = "1";
                    ddldepartment.SelectedValue = getResult[0].DepartmentID.ToString();
                    txt_tpacompany.Text = "";
                    hdntpacompanyID.Value = null;
                    MasterLookupBO mstlookup = new MasterLookupBO();
                    Commonfunction.PopulateDdl(ddldoctor, mstlookup.GetDepartmentDoctor(Convert.ToInt32(getResult[0].DepartmentID.ToString() == "" ? "0" : getResult[0].DepartmentID.ToString())));
                    ddldoctor.SelectedValue = getResult[0].DoctorID.ToString() == "" || getResult[0].DoctorID.ToString() == null ? "0" : getResult[0].DoctorID.ToString();
                    txt_opnumber.Text = txtUHID.Text.Contains(":") ? txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1) : "0";
                    ddldepartment.Attributes["disabled"] = "disabled";
                    ddldoctor.Attributes["disabled"] = "disabled";
                    txt_labservices.ReadOnly = false;
                    OPDbillingBO objstdBO = new OPDbillingBO();
                    int excludeMsb = objstdBO.checkMsbDoctor(Convert.ToInt64(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue));
                    hdisMsbDoctor.Value = excludeMsb == 1 ? "0" : "1";
                }
                if (ddl_patienttype.SelectedValue == "4")
                {
                    AutoCompleteExtender2.ContextKey = "1";
                    hdnIsPachageCompany.Value = "0";
                    ddldepartment.Attributes.Remove("disabled");
                    ddldoctor.Attributes.Remove("disabled");
                    txt_labservices.ReadOnly = false;
                }
                getpatientnumber();
                ddl_servicecategory.Attributes.Remove("disabled");
                txt_labservices.Focus();
            }
            else
            {
                txt_patcatgeory.Text = "";
                txt_tpacompany.Text = "";
                txtUHID.Text = "";
                txt_balanceinac.Text = "";
                txt_opnumber.Text = "";
                hdnuhid.Value = null;
                txtUHID.Focus();
            }
        }
        protected void ddl_patienttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_labservices.ReadOnly = true;
            lblmessage.Visible = false;
            txt_labservicecharge.ReadOnly = true;
            if (ddl_patienttype.SelectedValue == "1")
            {
                AutoCompleteExtender3.ContextKey = ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue;
                ddl_source.SelectedIndex = 0;
                ddldepartment.SelectedIndex = 0;
                txt_referal.Text = "";
                Commonfunction.Insertzeroitemindex(ddldoctor);
                ddldepartment.Attributes.Remove("disabled");
                ddldoctor.Attributes.Remove("disabled");
                ddl_source.Attributes["disabled"] = "disabled";
                txt_referal.Attributes["disabled"] = "disabled";
                lbl_patnumber.Text = "OPNo.";

            }
            if (ddl_patienttype.SelectedValue == "4")
            {
                AutoCompleteExtender3.ContextKey = ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue;
                ddldepartment.SelectedIndex = 0;
                Commonfunction.Insertzeroitemindex(ddldoctor);
                ddldepartment.Attributes.Remove("disabled");
                ddldoctor.Attributes.Remove("disabled");
                ddl_source.Attributes.Remove("disabled");
                txt_referal.Attributes.Remove("disabled");
                lbl_patnumber.Text = "UHID";
            }
        }
        protected void ddl_referal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txt_referal.Text.Trim() == "" && ddl_patienttype.SelectedValue == "4")
            {
                lblmessage.Visible = false;
                txt_labservices.ReadOnly = false;
                txt_labservicecharge.ReadOnly = true;
                ddldepartment.SelectedIndex = 0;
                Commonfunction.Insertzeroitemindex(ddldoctor);
                btnadd.Attributes.Remove("disabled");
                ddldepartment.Attributes["disabled"] = "disabled";
                ddldoctor.Attributes["disabled"] = "disabled";
            }
            else
            {
                txt_labservices.ReadOnly = true;
                txt_labservicecharge.ReadOnly = true;
                ddldepartment.SelectedIndex = 0;
                btnadd.Attributes["disabled"] = "disabled";
                Commonfunction.Insertzeroitemindex(ddldoctor);
                ddldepartment.Attributes.Remove("disabled");
                ddldoctor.Attributes.Remove("disabled");
                txt_labservices.ReadOnly = true;
                txt_labservicecharge.ReadOnly = true;
            }
        }
        protected void ddlpaymentmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlpaymentmode.SelectedIndex > 0)
            {
                if (ddlpaymentmode.SelectedValue == "1")
                {
                    txt_bank.Text = "";
                    txt_bank.ReadOnly = true;
                    txt_chequenumber.ReadOnly = true;
                    txtinvoicenumber.ReadOnly = true;
                }
                else if (ddlpaymentmode.SelectedValue == "2")
                {
                    GetBankName(Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue));
                    txt_bank.ReadOnly = true;
                    txt_chequenumber.ReadOnly = false;
                    txtinvoicenumber.ReadOnly = false;
                }
                else if (ddlpaymentmode.SelectedValue == "3")
                {
                    GetBankName(Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue));
                    txt_bank.ReadOnly = true;
                    txtinvoicenumber.Text = "";
                    txt_chequenumber.ReadOnly = false;
                    txtinvoicenumber.ReadOnly = true;
                }
                else if (ddlpaymentmode.SelectedValue == "4")
                {
                    txt_bank.Text = "";
                    txt_bank.ReadOnly = false;
                    txt_chequenumber.ReadOnly = false;
                    txtinvoicenumber.ReadOnly = true;
                }
            }
            else
            {
                txt_bank.Text = "";
                txt_bank.ReadOnly = true;
                txt_chequenumber.ReadOnly = true;
                txtinvoicenumber.ReadOnly = true;
            }
        }
        protected void GetBankName(int paymode)
        {
            OPDbillingBO objbillingBO = new OPDbillingBO();
            BankDetail objbankdetail = new BankDetail();
            objbankdetail.PaymodeID = paymode;
            List<BankDetail> banklist = objbillingBO.Getbanklist(objbankdetail);
            if (banklist.Count > 0)
            {
                txt_bank.Text = banklist[0].BankName.ToString();
                hdnbankID.Value = banklist[0].BankID.ToString();
            }
            else
            {
                txt_bank.Text = "";
                hdnbankID.Value = null;
            }
        }
        protected void ddldoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldoctor.SelectedIndex > 0)
            {
                OPDbillingBO objstdBO = new OPDbillingBO();
                int excludeMsb = objstdBO.checkMsbDoctor(Convert.ToInt64(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue));
                hdisMsbDoctor.Value = excludeMsb == 1 ? "0" : "1";
                lblmessage.Visible = false;
                getpatientnumber();
                txt_labservices.Focus();
            }
            else
            {
                txt_labservices.ReadOnly = true;
                txt_labservicecharge.ReadOnly = true;
                txt_opnumber.Text = "";
            }
        }
        protected void getpatientnumber()
        {
            OPDdata objpat = new OPDdata();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<OPDdata> getResult = new List<OPDdata>();
            objpat.UHID = ddl_patienttype.SelectedValue == "4" ? Convert.ToInt64(txtUHID.Text.Contains(":") ? txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1) : "0") : 0;
            objpat.PatientTypeID = Convert.ToInt32(ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue);
            objpat.DoctorID = Convert.ToInt64(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
            objpat.FinancialYearID = LogData.FinancialYearID;
            getResult = objInfoBO.GetPatientNumber(objpat);
            if (getResult.Count > 0)
            {
                txt_labservices.ReadOnly = false;
                txt_labservicecharge.ReadOnly = true;
                btnadd.Attributes.Remove("disabled");
                if (ddl_patienttype.SelectedValue == "4")
                {
                    txt_opnumber.Text = getResult[0].PatientNumber.ToString();
                }
                else
                {
                    txt_opnumber.Text = txtUHID.Text.Contains(":") ? txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1) : "0";
                }
            }
        }
        protected void ddl_TestCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            OPDbillingBO ObjbillBO = new OPDbillingBO();
            LabBillingData ObjBillData = new LabBillingData();
            ObjBillData.ID = Commonfunction.SemicolonSeparation_String_32(txt_labservices.Text.Trim());
            ObjBillData.TestCenterID = Convert.ToInt32(ddl_TestCenter.SelectedValue == "" ? "0" : ddl_TestCenter.SelectedValue);
            List<LabBillingData> result = ObjbillBO.GetLabServiceAmountByIDTestCenterID(ObjBillData);
            if (result.Count > 0)
            {
                lblmessage.Visible = false;
                txt_labservicecharge.Text = Commonfunction.Getrounding(result[0].LabServiceCharge.ToString());
                hdnservicecharge.Value = Commonfunction.Getrounding(result[0].LabServiceCharge.ToString());
                hdntestcnetrID.Value = Commonfunction.Getrounding(result[0].TestCenterID.ToString());
                // hdnpackageID.Value = ddl_servicecategory.SelectedValue == "2" ? ID : "0";
                if (ddl_servicecategory.SelectedValue == "2" && txt_labservices.Text.Contains(":"))
                {
                    hdnpackageID.Value = txt_labservices.Text.ToString().Substring(txt_labservices.Text.ToString().LastIndexOf(':') + 1);
                }
                else
                {
                    hdnpackageID.Value = "0";
                }
                btnadd.Focus();
            }
            else
            {
                Messagealert_.ShowMessage(lblmessage, "No record found for your selected Test Center.", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;
                return;
            }
        }
        protected void btnreferal_Click(object sender, EventArgs e)
        {
            message.Text = "";
            message.CssClass = "popup-normal-msg-bg ";
            this.MdreferalDoctors.Show();
        }
        protected void btnclose_Click(object sender, EventArgs e)
        {
            message.Text = "";
            message.CssClass = "popup-normal-msg-bg ";
            //txtcode.Text = "";
            txtcontactno.Text = "";
            txtrefaddress.Text = "";
            txtdoctor.Text = "";
        }
        protected void btn_refsave_Onclick(object sender, EventArgs e)
        {
            try
            {
                MdreferalDoctors.Show();

                if (txtdoctor.Text == "")
                {
                    Messagealert_.ShowMessage(message, "Please enter Referal Doctor.", 0);
                    txtdoctor.Focus();
                    return;
                }
                {
                    message.Visible = false;
                }

                if (txtcontactno.Text != "")
                {
                    string pattern = null;
                    pattern = "^([7-9]{1})([0-9]{1})([0-9]{8})$";

                    if (!Regex.IsMatch(txtcontactno.Text.Trim(), pattern))
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please enter valid mobile no.", 0);
                        message.Attributes["class"] = "FailAlert";
                        message.Visible = true;
                        txtcontactno.Focus();
                        return;
                    }
                }
                ReferalData objReferalData = new ReferalData();
                ReferalBO objReferalBO = new ReferalBO();
                //objReferalData.Code = txtcode.Text == "" ? null : txtcode.Text;
                objReferalData.Name = txtdoctor.Text == "" ? null : txtdoctor.Text;
                objReferalData.ContactNo = txtcontactno.Text == "" ? null : txtcontactno.Text;
                objReferalData.Address = txtrefaddress.Text == "" ? null : txtrefaddress.Text;
                objReferalData.EmployeeID = LogData.EmployeeID;
                objReferalData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objReferalData.HospitalID = LogData.HospitalID;
                objReferalData.IPaddress = LogData.IPaddress;
                objReferalData.FinancialYearID = LogData.FinancialYearID;
                objReferalData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    objReferalData.ActionType = Enumaction.Update;
                    objReferalData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objReferalBO.UpdateReferalDetails(objReferalData);
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(message, result == 1 ? "save" : "update", 1);
                    ViewState["ID"] = null;
                    bindgrid(1);
                    MasterLookupBO mstlookup = new MasterLookupBO();
                }
                else if (result == 5)
                {
                    Messagealert_.ShowMessage(message, "duplicate", 0);
                }
                else
                {
                    Messagealert_.ShowMessage(message, "system", 0);
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (ddl_patienttype.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "PatientType", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddl_patienttype.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (txtUHID.Text.Trim() == "")
            {
                Messagealert_.ShowMessage(lblmessage, "UHID", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtUHID.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }

            if (txt_refno.Text.Trim() == "")
            {
                Messagealert_.ShowMessage(lblmessage, "RefNo", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_refno.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }

            if (ddl_patienttype.SelectedValue == "1")
            {
                if (ddldepartment.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Department", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddldepartment.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (ddldoctor.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Consultant", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddldoctor.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
            }
            else
            {
                if (ddldoctor.SelectedIndex == 0)
                {
                    if (txt_referal.Text.Trim() == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "Referal", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_referal.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        div1.Visible = false;
                    }
                }
            }

            if (txt_labservices.Text.Trim() == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Service", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_labservices.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (txt_labservicecharge.Text.Trim() == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Charge", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_labservicecharge.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
           

            if (ddl_TestCenter.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "TestCenter", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddl_TestCenter.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (ddl_servicecategory.SelectedValue == "1")
            {
                string ID;
                var source = txt_labservices.Text.ToString();
                if (source.Contains(":"))
                {
                    ID = source.Substring(source.LastIndexOf(':') + 1);
                    // Check Duplicate data 
                    foreach (GridViewRow row in gvoplabservicelist.Rows)
                    {
                        Label ServiceID = (Label)gvoplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                        if (Convert.ToInt32(ServiceID.Text == "" ? "0" : ServiceID.Text) == Convert.ToInt32(ID == "" || ID == null ? "0" : ID))
                        {
                            txt_labservices.Text = "";
                            txt_labservicecharge.Text = "";
                            txtquantity.Text = "";
                            Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                            div1.Visible = true;
                            div1.Attributes["class"] = "FailAlert";
                            txt_labservices.ReadOnly = false;
                            txt_labservices.Focus();
                            return;
                        }
                        else
                        {
                            lblmessage.Visible = false;
                        }
                    }
                }
                else
                {
                    txt_labservices.ReadOnly = false;
                    txt_labservices.Text = "";
                    txt_labservicecharge.Text = "";
                    txtquantity.Text = "";
                    return;
                }

                List<LabBillingData> LabServiceList = Session["LabServiceList"] == null ? new List<LabBillingData>() : (List<LabBillingData>)Session["LabServiceList"];
                LabBillingData ObjService = new LabBillingData();
                ObjService.LabServiceCharge = Convert.ToDecimal(txt_labservicecharge.Text.ToString() == "" ? "0" : txt_labservicecharge.Text.ToString());
                ObjService.Quantity = Convert.ToInt32(txtquantity.Text == "" || txtquantity.Text == "0" ? "1" : txtquantity.Text);
                ObjService.ID = Convert.ToInt32(ID == "" || ID == null ? "0" : ID);
                ObjService.DeptID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
                ObjService.DocID = Convert.ToInt32(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
                ObjService.TestCenterID = Convert.ToInt32(hdntestcnetrID.Value);
                ObjService.NetLabServiceCharge = Convert.ToDecimal(txt_labservicecharge.Text.ToString() == "" ? "0" : txt_labservicecharge.Text.ToString()) * Convert.ToInt32(txtquantity.Text == "" || txtquantity.Text == "0" ? "1" : txtquantity.Text);
                ObjService.TestName = lblservicename.Text.Trim();
                txt_totalbill.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text) + Convert.ToInt32(txtquantity.Text == "" || txtquantity.Text == "0" ? "1" : txtquantity.Text) * Convert.ToDecimal(txt_labservicecharge.Text.ToString() == "" ? "0" : txt_labservicecharge.Text.ToString())).ToString());
                if (ddl_patienttype.SelectedIndex == 0)
                {
                    if (Convert.ToDecimal(txt_balanceinac.Text == "" ? "0" : txt_balanceinac.Text) > 0)
                    {
                        if (Convert.ToDecimal(txt_balanceinac.Text == "" ? "0" : txt_balanceinac.Text) >= Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text))
                        {
                            txt_adjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text)).ToString());
                            txt_paidamount.Text = "0.00";
                        }
                        else if (Convert.ToDecimal(txt_balanceinac.Text == "" ? "0" : txt_balanceinac.Text) < Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text))
                        {
                            txt_adjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_balanceinac.Text == "" ? "0" : txt_balanceinac.Text)).ToString());
                            txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text) - Convert.ToDecimal(txt_balanceinac.Text.ToString() == "" ? "0" : txt_balanceinac.Text.ToString())).ToString());
                        }
                    }
                    else
                    {
                        txt_adjustedamount.Text = "0.00";
                        txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text)).ToString());
                    }
                }
                else
                {
                    txt_paidamount.Text = Commonfunction.Getrounding(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text);

                }
                LabServiceList.Add(ObjService);
                if (LabServiceList.Count > 0)
                {
                    gvoplabservicelist.DataSource = LabServiceList;
                    gvoplabservicelist.DataBind();
                    gvoplabservicelist.Visible = true;
                    Session["LabServiceList"] = LabServiceList;
                    txt_labservices.Text = "";
                    txt_labservicecharge.Text = "";
                    txtquantity.Text = "";
                    txt_labservices.Focus();
                    txt_labservices.ReadOnly = false;
                    ddl_patienttype.Attributes["disabled"] = "disabled";
                    ddldepartment.Attributes["disabled"] = "disabled";
                    ddldoctor.Attributes["disabled"] = "disabled";
                    //ddl_servicecategory.Attributes["disabled"] = "disabled";
                    ddl_source.Attributes["disabled"] = "disabled";
                    txt_paidamount.Attributes.Remove("disabled");
                    txtUHID.ReadOnly = true;
                    btnsave.Attributes.Remove("disabled");
                }
                else
                {
                    txt_paidamount.Attributes["disabled"] = "disabled";
                    gvoplabservicelist.DataSource = null;
                    gvoplabservicelist.DataBind();
                    gvoplabservicelist.Visible = true;
                    btnsave.Attributes["disabled"] = "disabled";
                }
                totalCalculate();
            }
            if (ddl_servicecategory.SelectedValue == "2")
            {
                GetPackageLabServices();
            }
        }
        protected void GetPackageLabServices()
        {
            Session["LabServiceList"] = null;
            Session["DiscountList"] = null;
            txt_totalbill.Text = "0.00";
            txt_paidamount.Text = "0.00";
            txt_totaldue.Text = "0.00";
            List<LabBillingData> LabServiceList = Session["LabServiceList"] == null ? new List<LabBillingData>() : (List<LabBillingData>)Session["LabServiceList"];
            LabBillingData ObjService = new LabBillingData();
            OPDbillingBO objstdBO = new OPDbillingBO();
            ObjService.ID = txt_labservices.Text.Contains(":") ? Convert.ToInt32(txt_labservices.Text.ToString().Substring(txt_labservices.Text.ToString().LastIndexOf(':') + 1)) : 0;
            List<LabBillingData> result = objstdBO.GetPackageservices(ObjService);
            if (result.Count > 0)
            {
                for (int i = 0; i <= result.Count - 1; i++)
                {
                    LabBillingData ObjServices = new LabBillingData();
                    ObjServices.Quantity = Convert.ToInt32(txtquantity.Text == "" || txtquantity.Text == "0" ? "1" : txtquantity.Text);
                    ObjServices.ID = Convert.ToInt32(result[i].ID);
                    ObjServices.PackageID = Convert.ToInt32(result[i].PackageID);
                    ObjServices.TestName = result[i].TestName.ToString();
                    ObjServices.TestCenterID = Convert.ToInt32(result[i].TestCenterID);
                    txt_totalbill.Text = Commonfunction.Getrounding((Convert.ToDecimal(result[0].TotalBill).ToString()));
                    if (ddl_patienttype.SelectedIndex == 0)
                    {
                        if (Convert.ToDecimal(txt_balanceinac.Text) > 0)
                        {
                            if (Convert.ToDecimal(txt_balanceinac.Text) >= Convert.ToDecimal(txt_totalbill.Text))
                            {
                                txt_adjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text)).ToString());
                                txt_paidamount.Text = "0.00";
                            }
                            else if (Convert.ToDecimal(txt_balanceinac.Text) < Convert.ToDecimal(txt_totalbill.Text))
                            {
                                txt_adjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_balanceinac.Text == "" ? "0" : txt_balanceinac.Text)).ToString());
                                txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text) - Convert.ToDecimal(txt_balanceinac.Text.ToString() == "" ? "0" : txt_balanceinac.Text.ToString())).ToString());
                            }
                        }
                        else
                        {
                            txt_adjustedamount.Text = "0.00";
                            txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text)).ToString());
                        }
                    }
                    else
                    {
                        txt_paidamount.Text = Commonfunction.Getrounding(txt_totalbill.Text);
                    }
                    LabServiceList.Add(ObjServices);
                    if (LabServiceList.Count > 0)
                    {
                        gvoplabservicelist.DataSource = LabServiceList;
                        gvoplabservicelist.DataBind();
                        gvoplabservicelist.Visible = true;
                        Session["LabServiceList"] = LabServiceList;
                        txt_labservices.Text = "";
                        txt_labservicecharge.Text = "";
                        txtquantity.Text = "";
                        txt_labservices.Focus();
                        txt_labservices.ReadOnly = false;
                        ddl_patienttype.Attributes["disabled"] = "disabled";
                        //txt_referal.Text = "";
                        ddldepartment.Attributes["disabled"] = "disabled";
                        ddldoctor.Attributes["disabled"] = "disabled";
                        //  ddl_servicecategory.Attributes["disabled"] = "disabled";
                        txtUHID.ReadOnly = true;
                        txt_labservices.ReadOnly = true;
                        lblpaidamount.Text = "Recievable Amount(₹)";
                        btnsave.Attributes.Remove("disabled");
                    }
                    else
                    {
                        gvoplabservicelist.DataSource = null;
                        gvoplabservicelist.DataBind();
                        gvoplabservicelist.Visible = true;
                        txt_labservices.ReadOnly = false;
                        lblpaidamount.Text = "Paid Amount(₹)";
                    }
                }
            }
        }
        protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldepartment.SelectedIndex > 0)
            {
                ddldoctor.Attributes.Remove("disabled");
                txt_referal.Text = "";
                txt_referal.Attributes["disabled"] = "disabled";
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddldoctor, mstlookup.GetDepartmentDoctor(Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue)));
                if (ddl_patienttype.SelectedValue == "4")
                {
                    ddl_source.SelectedIndex = 0;
                    ddl_source.Attributes["disabled"] = "disabled";
                    ddldepartment.Attributes.Remove("disabled");
                    ddldoctor.Attributes.Remove("disabled");
                }
                else
                {
                    ddldepartment.Attributes.Remove("disabled");
                    ddldoctor.Attributes.Remove("disabled");
                    ddl_source.Attributes["disabled"] = "disabled";
                }
            }
            else
            {
                if (ddl_patienttype.SelectedValue == "4")
                {
                    ddl_source.Attributes.Remove("disabled");
                }
                Commonfunction.Insertzeroitemindex(ddldoctor);
                ddldoctor.Attributes["disabled"] = "disabled";
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetAutoUHID(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            Objpaic.PatientType = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetDetailUHIDwithOpnumber(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        protected void txt_labservices_TextChanged(object sender, EventArgs e)
        {
            var source = txt_labservices.Text.ToString();
            if (source.Contains(":"))
            {
                OPDbillingBO ObjbillBO = new OPDbillingBO();
                LabBillingData ObjBillData = new LabBillingData();
                ObjBillData.ID = Commonfunction.SemicolonSeparation_String_32(txt_labservices.Text.ToString());
                ObjBillData.ServiceCategoryID = Convert.ToInt32(ddl_servicecategory.SelectedValue == "" ? "0" : ddl_servicecategory.SelectedValue);
                List<LabBillingData> result = ObjbillBO.GetLabServiceChargeByID(ObjBillData);
                if (result.Count > 0)
                {
                    txt_labservicecharge.Text = Commonfunction.Getrounding(result[0].LabServiceCharge.ToString());
                    hdnservicecharge.Value = Commonfunction.Getrounding(result[0].LabServiceCharge.ToString());
                    hdntestcnetrID.Value = Commonfunction.Getrounding(result[0].TestCenterID.ToString());
                    //hdnpackageID.Value = ddl_servicecategory.SelectedValue == "2" ? ID : "0";
                    if (ddl_servicecategory.SelectedValue == "2" && txt_labservices.Text.Contains(":"))
                    {
                        hdnpackageID.Value = txt_labservices.Text.ToString().Substring(txt_labservices.Text.ToString().LastIndexOf(':') + 1);
                    }
                    else
                    {
                        hdnpackageID.Value = "0";
                    }
                    txt_labservices.ReadOnly = true;
                    btnadd.Focus();
                    lblservicename.Text = result[0].TestName.ToString();
                    ddl_TestCenter.SelectedValue = result[0].TestCenterID.ToString();
                    ddl_TestCenter.Attributes.Remove("disabled");
                    txtquantity.Text = "1";
                    if (ddl_servicecategory.SelectedValue == "2")
                    {
                        txtquantity.ReadOnly = true;
                    }
                    else
                    {
                        txtquantity.ReadOnly = false;
                    }
                    lblmessage.Visible = false;
                }
            }
            else
            {
                txt_labservices.Text = "";
                hdnservicecharge.Value = null;
                Messagealert_.ShowMessage(lblmessage, "Test is not available.", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_labservices.ReadOnly = false;
                txt_labservices.Focus();
                return;
            }
        }

        protected void gvoplabservicelist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {

                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvoplabservicelist.Rows[i];
                    List<LabBillingData> ItemList = Session["LabServiceList"] == null ? new List<LabBillingData>() : (List<LabBillingData>)Session["LabServiceList"];
                    if (ItemList.Count > 0)
                    {
                        Decimal totalamount = ItemList[i].LabServiceCharge;
                        txt_totalbill.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text) - totalamount).ToString());
                        if (ddl_patienttype.SelectedIndex == 0)
                        {
                            if (Convert.ToDecimal(txt_balanceinac.Text) > 0)
                            {
                                if (Convert.ToDecimal(txt_balanceinac.Text) >= Convert.ToDecimal(txt_balanceinac.Text))
                                {
                                    txt_adjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text)).ToString());
                                    txt_paidamount.Text = "0.00";
                                }
                                else if (Convert.ToDecimal(txt_balanceinac.Text) < Convert.ToDecimal(txt_totalbill.Text))
                                {
                                    txt_adjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_balanceinac.Text == "" ? "0" : txt_balanceinac.Text)).ToString());
                                    txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text) - Convert.ToDecimal(txt_balanceinac.Text.ToString() == "" ? "0" : txt_balanceinac.Text.ToString())).ToString());
                                }
                                //else if (Convert.ToDecimal(txtbalanceinac.Text) > Convert.ToDecimal(txttotalamount.Text))
                                //{
                                //    txtadjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txttotalamount.Text == "" ? "0" : txttotalamount.Text)).ToString());
                                //    txtpaidamount.Text = "0.00";
                                //}
                                else if (Convert.ToDecimal(txt_balanceinac.Text) == Convert.ToDecimal(txt_totalbill.Text))
                                {
                                    txt_adjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_balanceinac.Text == "" ? "0" : txt_balanceinac.Text)).ToString());
                                    txt_paidamount.Text = "0.00";
                                }
                                else if (Convert.ToDecimal(txt_totalbill.Text) == 0)
                                {
                                    txt_paidamount.Text = "0.00";
                                }
                            }
                        }
                        else
                        {
                            txt_adjustedamount.Text = "0.00";
                            txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text)).ToString());
                        }
                    }
                    ItemList.RemoveAt(i);
                    Session["LabServiceList"] = ItemList;
                    gvoplabservicelist.DataSource = ItemList;
                    gvoplabservicelist.DataBind();
                    totalCalculate();
                    if (ItemList.Count > 0)
                    {
                        btnsave.Attributes.Remove("disabled");
                    }
                    else
                    {
                        btnsave.Attributes["disabled"] = "disabled";
                    }

                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                lblmessage.Visible = true;
                lblmessage.CssClass = "Message";
            }
        }
        protected void txt_discount_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txt_totalbill.Text == "" ? "0.00" : txt_totalbill.Text) >= Convert.ToDecimal(txt_discount.Text == "" ? "0.00" : txt_discount.Text))
            {
                if (Convert.ToDecimal(txt_paidamount.Text == "" ? "0.00" : txt_paidamount.Text) >= Convert.ToDecimal(txt_discount.Text == "" ? "0.00" : txt_discount.Text))
                {
                    txt_paidamount.Text = Commonfunction.Getrounding(((Convert.ToDecimal(txt_paidamount.Text == "" ? "0" : txt_paidamount.Text) - Convert.ToDecimal(txt_discount.Text == "" ? "0" : txt_discount.Text)).ToString()));
                }
                if (Convert.ToDecimal(txt_paidamount.Text == "" ? "0.00" : txt_paidamount.Text) < Convert.ToDecimal(txt_discount.Text == "" ? "0.00" : txt_discount.Text) && Convert.ToDecimal(txt_adjustedamount.Text == "" ? "0.00" : txt_adjustedamount.Text) > 0)
                {
                    txt_paidamount.Text = "0.00";
                    txt_adjustedamount.Text = Commonfunction.Getrounding(((((Convert.ToDecimal(txt_adjustedamount.Text == "" ? "0" : txt_adjustedamount.Text) + ((Convert.ToDecimal(txt_totalbill.Text == "" ? "0.00" : txt_totalbill.Text) - Convert.ToDecimal(txt_adjustedamount.Text == "" ? "0.00" : txt_adjustedamount.Text))) - Convert.ToDecimal(txt_discount.Text == "" ? "0" : txt_discount.Text))))).ToString());
                }
                div1.Visible = false;
            }
            else
            {
                Messagealert_.ShowMessage(lblmessage, "DiscountOver", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_discount.Text = "";
                return;
            }
            if (Convert.ToDecimal(txt_discount.Text == "" ? "0.00" : txt_discount.Text) == 0)
            {
                div1.Visible = false;
                if (Convert.ToDecimal(txt_balanceinac.Text) > 0)
                {
                    if (Convert.ToDecimal(txt_balanceinac.Text) >= Convert.ToDecimal(txt_totalbill.Text))
                    {
                        txt_adjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text)).ToString());
                        txt_paidamount.Text = "0.00";
                    }
                    else if (Convert.ToDecimal(txt_balanceinac.Text) < Convert.ToDecimal(txt_totalbill.Text))
                    {
                        txt_adjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_balanceinac.Text == "" ? "0" : txt_balanceinac.Text)).ToString());
                        txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text) - Convert.ToDecimal(txt_balanceinac.Text.ToString() == "" ? "0" : txt_balanceinac.Text.ToString())).ToString());
                    }
                }
                else
                {
                    txt_adjustedamount.Text = "0.00";
                    txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text)).ToString());
                }
            }
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            ddl_patienttype.SelectedIndex = 0;
            txt_referal.Text = "";
            txt_investigationno.Text = "";
            ddl_patienttype.Attributes["disabled"] = "disabled";
            txtdatefrom.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            txtUHID.ReadOnly = false;
            txtUHID.Text = "";
            txtbillNo.Text = "";
            txt_labservicecharge.Text = "";
            txt_discount.Text = "";
            ddlpaymentmode.SelectedIndex = 0;
            ddldepartment.SelectedIndex = 0;
            ddldoctor.Items.Clear();
            Commonfunction.Insertzeroitemindex(ddldoctor);
            txt_bank.Text = "";
            txt_totalbill.Text = "";
            txtDiscount.Text = "0";
            txt_payableamnt.Text = "";
            txt_due.Text = "";
            txt_chequenumber.Text = "";
            txt_bank.ReadOnly = true;
            txt_chequenumber.ReadOnly = true;
            Session["LabServiceList"] = null;
            Session["DiscountList"] = null;
            gvoplabservicelist.DataSource = null;
            gvoplabservicelist.DataBind();
            gvoplabservicelist.Visible = false;
            GVTPAList.DataSource = null;
            GVTPAList.DataBind();
            GVTPAList.Visible = true;
            lblmessage.Visible = false;
            div1.Visible = false;
            txt_patcatgeory.Text = "";
            txt_patcatgeory.Attributes["disabled"] = "disabled";
            txt_labservices.Text = "";
            txtquantity.Text = "";
            txt_balanceinac.Text = "";
            txt_paidamount.Text = "";
            ddlpaymentmode.SelectedIndex = 1;
            txt_labservices.ReadOnly = false;
            txt_adjustedamount.Text = "";
            hdnservicecharge.Value = null;
            txt_opnumber.ReadOnly = true;
            txtremarkdisc.Text = "";
            txt_labservicecharge.Text = "";
            txt_labservices.ReadOnly = true;
            txt_labservicecharge.ReadOnly = true;
            ddlcollectedby.SelectedIndex = 0;
            ddl_patienttype.SelectedIndex = 0;
            btnprint.Attributes["disabled"] = "disabled";
            ddldepartment.Attributes["disabled"] = "disabled";
            ddldoctor.Attributes["disabled"] = "disabled";
            btnprint.Attributes["disabled"] = "disabled";
            btnsave.Attributes["disabled"] = "disabled";
            ddl_patienttype.Attributes.Remove("disabled");
            txtUHID.ReadOnly = false;
            ViewState["BillID"] = null;
            GVDiscountApprovalList.Visible = false;
            btnsave.Visible = true;
            btnDisSave.Visible = false;
            ddl_servicecategory.SelectedIndex = 0;
            ddl_servicecategory.Attributes["disabled"] = "disabled";
            txt_opnumber.Text = "";
            hdntpacompanyID.Value = null;
            hdnpackageID = null;

            btn_token.Visible = false;
            ddl_source.Attributes["disabled"] = "disabled";
            ddl_source.SelectedIndex = 0;
            //ddl_patienttype.SelectedValue = "4";
            if (ddl_patienttype.SelectedValue == "4")
            {
                AutoCompleteExtender2.ContextKey = "1";
                ddl_source.Attributes.Remove("disabled");
            }
            AutoCompleteExtender3.ContextKey = ddl_patienttype.SelectedValue;
            txt_referal.Attributes["disabled"] = "disabled";
            ddlRunnerID.SelectedIndex = 1;
            txtDiscount.Text = "0";
            txt_refno.Text = "";
            ddl_TestCenter.SelectedIndex = 1;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (LogData.SaveEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SaveEnable", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddl_source.SelectedIndex == 0 && ddl_patienttype.SelectedValue == "4" && ddldoctor.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Source", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (txt_referal.Text.Trim() == "" && ddl_patienttype.SelectedValue == "4" && ddldoctor.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ReferBy", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (txtUHID.Text.Trim() == "" && ddl_patienttype.SelectedValue == "1")
            {
                Messagealert_.ShowMessage(lblmessage, "UHID", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtUHID.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (txt_totalbill.Text.Trim() == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Service", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_labservices.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (ddlRunnerID.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Collectioncentre", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_labservices.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }

            if (txtDiscount.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Enter discount amount.", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtDiscount.Focus();
                return;
            }
            else
            {
                if (Convert.ToDecimal(txtDiscount.Text.Trim() == "" ? "0" : txtDiscount.Text.Trim()) > 0)
                {
                    if (txtremarkdisc.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "Enter discount remarks.", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txtremarkdisc.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        div1.Visible = false;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
            }
            List<LabBillingData> Listbill = new List<LabBillingData>();
            List<Discount> ListDiscount = new List<Discount>();
            OPDbillingBO objbillingBO = new OPDbillingBO();
            LabBillingData objlabserv = new LabBillingData();

            try
            {
                // get all the record from the gridview

                Decimal TotalAmount = 0;
                foreach (GridViewRow row in gvoplabservicelist.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label Particulars = (Label)gvoplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lbllabparticulars");
                    Label amount = (Label)gvoplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lbl_labcharges");
                    Label qty = (Label)gvoplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblquantity");
                    Label NetCharge = (Label)gvoplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblnetcharges");
                    Label SerialID = (Label)gvoplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblserialID");
                    Label ID = (Label)gvoplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label Remarks = (Label)gvoplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblremarks");
                    Label DoctorType = (Label)gvoplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lbldoctortype");
                    Label DepartmentType = (Label)gvoplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lbldeprtmentID");
                    Label Doctor = (Label)gvoplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lbldoctorID");
                    DropDownList Testcenter = (DropDownList)gvoplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("ddl_testcenter");
                    DropDownList urgency = (DropDownList)gvoplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("ddl_urgency");
                    DropDownList ddl_discount_type = (DropDownList)gvoplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("ddl_discount_type");
                    TextBox txt_dis_value = (TextBox)gvoplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("txt_dis_value");
                    Label lbl_discount_amt = (Label)gvoplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lbl_discount_amt");
                    Label PackageID = (Label)gvoplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lbl_packageID");
                    LabBillingData ObjDetails = new LabBillingData();

                    ObjDetails.TestName = Particulars.Text == "" ? null : Particulars.Text;
                    ObjDetails.LabServiceCharge = Convert.ToDecimal(amount.Text == "" ? "0" : amount.Text);
                    ObjDetails.Quantity = Convert.ToInt32(txtquantity.Text == "" || txtquantity.Text == "0" ? "1" : txtquantity.Text);
                    ObjDetails.NetLabServiceCharge = Convert.ToDecimal(NetCharge.Text == "" ? "0" : NetCharge.Text);
                    ObjDetails.Remarks = Remarks.Text == "" ? "null" : Remarks.Text;
                    ObjDetails.SerialID = Convert.ToInt32(SerialID.Text == "" ? "0" : SerialID.Text);
                    ObjDetails.PackageID = Convert.ToInt32(PackageID.Text == "" ? "0" : PackageID.Text);
                    ObjDetails.LabServiceID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    ObjDetails.DoctorTypeID = Convert.ToInt32(ddl_patienttype.SelectedValue == "1" ? "1" : DoctorType.Text == "" ? "4" : DoctorType.Text);
                    ObjDetails.DeptID = Convert.ToInt32(DepartmentType.Text == "" ? "0" : DepartmentType.Text);
                    if (ddl_patienttype.SelectedValue == "1" && ddldoctor.SelectedIndex > 0)
                    {
                        ObjDetails.DocID = Convert.ToInt64(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
                    }
                    if (ddl_patienttype.SelectedValue == "4" && ddl_source.SelectedIndex > 0)
                    {
                        ObjDetails.DocID = Commonfunction.SemicolonSeparation_String_64(txt_referal.Text.ToString());
                    }
                    if (ddl_patienttype.SelectedValue == "4" && ddldoctor.SelectedIndex > 0)
                    {
                        ObjDetails.DocID = Convert.ToInt64(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
                    }
                    ObjDetails.TestCenterID = Convert.ToInt32(Testcenter.SelectedValue == "" ? "0" : Testcenter.SelectedValue);
                    ObjDetails.UrgencyID = Convert.ToInt32(urgency.SelectedValue == "" ? "0" : urgency.SelectedValue);
                    ObjDetails.DisType = Convert.ToInt32(ddl_discount_type.SelectedValue == "" ? "0" : ddl_discount_type.SelectedValue);
                    ObjDetails.isDis = Convert.ToInt32(Convert.ToDecimal(lbl_discount_amt.Text.Trim() == "" ? "0" : lbl_discount_amt.Text.Trim()) == 0 ? 0 : 1);
                    ObjDetails.DisValue = Convert.ToDecimal(txt_dis_value.Text == "" ? "0" : txt_dis_value.Text);
                    ObjDetails.DisAmount = Convert.ToDecimal(lbl_discount_amt.Text == "" ? "0" : lbl_discount_amt.Text);
                    ObjDetails.isMsbDoctor = Convert.ToInt32(hdisMsbDoctor.Value == "" ? "0" : hdisMsbDoctor.Value);
                    ObjDetails.isMsbPatient = Convert.ToInt32(hdMsbPc.Value == "" ? "0" : hdMsbPc.Value) > 0 ? 1 : 0;
                    ObjDetails.MsbPc = Convert.ToInt32(hdisMsbDoctor.Value == "" ? "0" : hdisMsbDoctor.Value) == 1 ? Convert.ToInt32(hdMsbPc.Value == "" ? "0" : hdMsbPc.Value) : 0;
                    TotalAmount = TotalAmount + Convert.ToDecimal(NetCharge.Text == "" ? "0" : NetCharge.Text);
                    Listbill.Add(ObjDetails);
                }
                objlabserv.XMLData = XmlConvertor.OpdLabBillDatatoXML(Listbill).ToString();
                decimal dis = Convert.ToDecimal(txtDiscount.Text.Trim() == "" ? "0" : txtDiscount.Text.Trim());
                int flag = 0;
                foreach (GridViewRow row in GVTPAList.Rows)
                {
                    Label lblCatID = (Label)GVTPAList.Rows[row.RowIndex].Cells[0].FindControl("lblCatID");
                    Label lblSubCatID = (Label)GVTPAList.Rows[row.RowIndex].Cells[0].FindControl("lblSubCatID");
                    Label lblDisAmount = (Label)GVTPAList.Rows[row.RowIndex].Cells[0].FindControl("lblDisAmount");
                    Discount ObjDetails = new Discount();

                    ObjDetails.PatientCatID = Convert.ToInt32(lblCatID.Text == "" ? "0" : lblCatID.Text);
                    ObjDetails.SubCatID = Convert.ToInt32(lblSubCatID.Text == "" ? "0" : lblSubCatID.Text);
                    ObjDetails.DiscountAmount = Convert.ToDecimal(lblDisAmount.Text == "" ? "0" : lblDisAmount.Text);
                    ListDiscount.Add(ObjDetails);
                    flag = 1;
                }
                objlabserv.extraDiscountXML = XmlConvertor.ExtraDiscountDatatoXML(ListDiscount).ToString();
                objlabserv.PatientCat = Convert.ToInt32(hdisMsbDoctor.Value) == 0 ? 1 : Convert.ToInt32(hdPatCat.Value);
                objlabserv.PatientType = Convert.ToInt32(ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue);
                objlabserv.isDis = dis > 0 ? 1 : 0;
                if (flag == 1)
                {
                    objlabserv.isDis = 0;
                }
                objlabserv.isExtraDiscount = 0;// flag;
                objlabserv.TotalBillAmount = Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text);
                objlabserv.UHID = Convert.ToInt64(hdnuhid.Value == null || hdnuhid.Value == "" ? "0" : hdnuhid.Value);
                objlabserv.CompanyID = Convert.ToInt32(hdntpacompanyID.Value == "" ? "0" : hdntpacompanyID.Value);
                objlabserv.PackageID = Convert.ToInt32(hdnpackageID.Value == "" ? "0" : hdnpackageID.Value);
                objlabserv.AdjustedAmount = Convert.ToDecimal(txt_adjustedamount.Text == "" ? "0" : txt_adjustedamount.Text);
                if (ddl_patienttype.SelectedValue == "1" && ddldoctor.SelectedIndex > 0)
                {
                    objlabserv.DocID = Convert.ToInt64(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
                }
                if (ddl_patienttype.SelectedValue == "4" && ddl_source.SelectedIndex > 0)
                {
                    objlabserv.DocID = Commonfunction.SemicolonSeparation_String_64(txt_referal.Text.ToString());
                }
                if (ddl_patienttype.SelectedValue == "4" && ddldoctor.SelectedIndex > 0)
                {
                    objlabserv.DocID = Convert.ToInt64(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
                }
                objlabserv.DiscountedAmount = Convert.ToDecimal(txtDiscount.Text == "" ? "0" : txtDiscount.Text);
                objlabserv.Remarks = txtremarkdisc.Text == "" ? null : txtremarkdisc.Text;
                objlabserv.ID = 0;
                objlabserv.PaidAmount = Convert.ToDecimal(txt_paidamount.Text == "" ? "0" : txt_paidamount.Text);
                objlabserv.DueAmount = Convert.ToDecimal(txt_due.Text == "" ? "0" : txt_due.Text);
                objlabserv.RunnerID = Convert.ToInt32(ddlRunnerID.SelectedValue == "" ? "0" : ddlRunnerID.SelectedValue);
                objlabserv.PaymentMode = Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue);
                objlabserv.BankName = txt_bank.Text == "" ? null : txt_bank.Text;
                objlabserv.CheaqueNo = txt_chequenumber.Text == "" ? null : txt_chequenumber.Text;
                objlabserv.INvoiceNo = txtinvoicenumber.Text == "" ? null : txtinvoicenumber.Text;
                objlabserv.OPnumber = txt_opnumber.Text.Trim();
                objlabserv.FinancialYearID = LogData.FinancialYearID;
                objlabserv.EmployeeID = LogData.EmployeeID;
                objlabserv.AddedBy = LogData.AddedBy;
                objlabserv.HospitalID = LogData.HospitalID;
                objlabserv.IsActive = LogData.IsActive;
                objlabserv.IPaddress = LogData.IPaddress;
                objlabserv.BarcodeImage = Commonfunction.getBarcodeImage(txtUHID.Text.Contains(":") ? txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1) : "0");
                objlabserv.ActionType = Enumaction.Insert;
                objlabserv.IsVerified = LogData.BillSetting == 0 ? 1 : 0;
                objlabserv.SourceID = Convert.ToInt32(ddl_source.SelectedValue == "" ? "0" : ddl_source.SelectedValue);
                objlabserv.ReferalID = Commonfunction.SemicolonSeparation_String_32(txt_referal.Text.ToString());
                objlabserv.ReferalName = txt_referal.Text.Trim() == "" ? null : txt_referal.Text.Trim();
                objlabserv.RefNo = txt_refno.Text.Trim() == "" ? null : txt_refno.Text.Trim();

                if (TotalAmount != Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text) && ddl_servicecategory.SelectedValue == "1")
                {
                    Messagealert_.ShowMessage(lblmessage, "Total bill amount is not eaqual to the sum of all the net test amount. Please reset  and try again.", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                List<LabBillingData> result = objbillingBO.UpdateOPDLabBill(objlabserv);
                if (result.Count > 0)
                {
                    //if (result[0].isDis == 0)
                    //{
                    if (LogData.PrintEnable == 0)
                    {
                        btnprint.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        if (LogData.BillSetting == 0)
                        {
                            btnprint.Attributes.Remove("disabled");

                        }
                        else
                        {
                            btnprint.Visible = false;
                            btn_token.Visible = true;
                            ddlpaymentmode.Attributes["disabled"] = "disabled";
                            txt_chequenumber.ReadOnly = true;
                            txtinvoicenumber.ReadOnly = true;
                        }
                    }
                    btnsave.Attributes["disabled"] = "disabled";
                    txtbillNo.Text = result[0].BillNo.ToString();
                    txt_investigationno.Text = result[0].InvestigationNo.ToString();
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    Session["LabServiceList"] = null;
                    Session["DiscountList"] = null;
                    GVTPAList.DataSource = null;
                    GVTPAList.DataBind();
                    GVTPAList.Visible = true;
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    txtUHID.Text = "";
                    hdntpacompanyID.Value = null;
                    hdnpackageID = null;
                    hdnuhid.Value = null;
                    //}
                    //else
                    //{
                    //    Messagealert_.ShowMessage(lblmessage, "Discount Request sent! " + "[Request No:" + result[0].ID + "]", 1);
                    //    Session["LabServiceList"] = null;
                    //    Session["DiscountList"] = null;
                    //    div1.Visible = true;
                    //    btnsave.Attributes["disabled"] = "disabled";
                    //    div1.Attributes["class"] = "SucessAlert";
                    //    btnprint.Attributes["disabled"] = "disabled";
                    //    ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>pushMessage('" + result[0].Remarks + "','" + result[0].ID + "');</script>", false);
                    //}
                }
                else
                {
                    txtbillNo.Text = "";
                    txt_investigationno.Text = "";
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
        }
        protected void ddl_source_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender5.ContextKey = ddl_source.SelectedValue;
            if (ddl_source.SelectedIndex > 0)
            {
                ddldepartment.SelectedIndex = 0;
                Commonfunction.Insertzeroitemindex(ddldoctor);
                if (ddl_source.SelectedIndex == 1)
                {
                    txt_referal.Text = "Self:1";
                    txt_referal.Attributes["disabled"] = "disabled";
                }
                else
                {
                    txt_referal.Text = "";
                    txt_referal.Attributes.Remove("disabled");
                    txt_referal.Focus();
                }
            }
            else
            {
                txt_referal.Text = "";
                txt_referal.Attributes["disabled"] = "disabled";
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> Getautoreferals(string prefixText, int count, string contextKey)
        {
            ReferalData objreferal = new ReferalData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<ReferalData> getResult = new List<ReferalData>();
            objreferal.Referal = prefixText;
            objreferal.ID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetReferalDetails(objreferal);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].Referal.ToString());
            }
            return list;
        }
        //protected void txtautoUHID_TextChanged(object sender, EventArgs e)
        //{
        //    if (txtautoUHID.Text != "")
        //    {
        //        bindgrid(1);
        //    }
        //    PatientData Objpaic = new PatientData();
        //    RegistrationBO objInfoBO = new RegistrationBO();
        //    List<PatientData> getResult = new List<PatientData>();
        //    Objpaic.UHID = Convert.ToInt64(txtautoUHID.Text.Trim() == "" ? "0" : txtautoUHID.Text.Trim());
        //    getResult = objInfoBO.GetPatientDetailsByUHID(Objpaic);
        //    if (getResult.Count > 0)
        //    {
        //        txtpatientNames.Text = getResult[0].PatientName.ToString();
        //        Session["ServiceList"] = null;
        //        Session["DiscountList"] = null;
        //    }
        //    else
        //    {
        //        txtpatientNames.Text = "";

        //        txtautoUHID.Text = "";
        //        txtautoUHID.Focus();
        //    }

        //}
        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            DateTime DateFrom = Convert.ToDateTime(txtdatefrom.Text);
            DateTime DateTo = Convert.ToDateTime(txtto.Text);
            TimeSpan objTimeSpan = DateTo - DateFrom;
            double Days = Convert.ToDouble(objTimeSpan.TotalDays);
            if (Days > 31)
            {
                if (LogData.RoleID == 1)
                {
                    lblmessage2.Text = "";
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage2, "Date range cannot be greater than 31 days. Please select date that equal or within 31 days.", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    txtdatefrom.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
                    txtto.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
                    return;
                }
            }
        }
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            if (txtpatientNames.Text != "")
            {
                hdnpatientName.Value = txtpatientNames.Text;
                bindgrid(1);
            }
            else
            {
                hdnpatientName.Value = "";
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid(1);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetUHID(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.UHID = Convert.ToInt64(prefixText);
            getResult = objInfoBO.GetUHID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].RegDNo.ToString());
            }
            return list;
        }
        protected void bindgrid(int page)
        {
            try
            {
                if (LogData.SearchEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (txtpatientNames.Text != "")
                {
                    var source = txtpatientNames.Text.ToString();
                    if (source.Contains(":"))
                    {
                    }
                    else
                    {
                        txtpatientNames.Text = "";
                        return;
                    }
                }
                if (txtdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "VaildDatefrom", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txtdatefrom.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                if (txtto.Text != "")
                {
                    if (Commonfunction.isValidDate(txtto.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "VaildDateto", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txtto.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }

                List<LabBillingData> objdeposit = GetLabBillList(page);
                if (objdeposit.Count > 0)
                {
                    if (LogData.PrintEnable == 0)
                    {
                        btnprints.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btnprints.Attributes.Remove("disabled");
                    }
                    gvdepositlist.VirtualItemCount = objdeposit[0].MaximumRows;//total item is required for custom paging
                    gvdepositlist.PageIndex = page - 1;

                    gvdepositlist.DataSource = objdeposit;
                    gvdepositlist.DataBind();
                    gvdepositlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    txttotalbillamount.Text = Commonfunction.Getrounding(objdeposit[0].TotalBill.ToString());
                    txt_totaldue.Text = Commonfunction.Getrounding(objdeposit[0].TotalDueamount.ToString());
                    txttotaldiscounted.Text = Commonfunction.Getrounding(objdeposit[0].TotalDiscountedAmount.ToString());
                    txttotalpaid.Text = Commonfunction.Getrounding(objdeposit[0].TotalPaidAmount.ToString());
                    txtTotalRunnerAmt.Text = Commonfunction.Getrounding(objdeposit[0].TotalRunnerAmt.ToString());
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    gvdepositlist.DataSource = null;
                    gvdepositlist.DataBind();
                    gvdepositlist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    txttotalbillamount.Text = "0.00";
                    txt_totaldue.Text = "0.00";
                    txttotaldiscounted.Text = "0.00";
                    txttotalpaid.Text = "0.00";
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        public List<LabBillingData> GetLabBillList(int curIndex)
        {
            LabBillingData objlabbill = new LabBillingData();
            OPDbillingBO objbillingBO = new OPDbillingBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);

            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

            //if (txtautoUHID.Text != "")
            //{
            //    objlabbill.UHID = Convert.ToInt64(txtautoUHID.Text == "" ? "0" : txtautoUHID.Text);
            //}
            //else
            //{
            bool isnumeric = txtpatientNames.Text.All(char.IsDigit);
            if (isnumeric == false)
            {
                if (txtpatientNames.Text.Contains(":"))
                {
                    bool isUHIDnumeric = txtpatientNames.Text.Substring(txtpatientNames.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                    objlabbill.UHID = isUHIDnumeric ? Convert.ToInt64(txtpatientNames.Text.Contains(":") ? txtpatientNames.Text.Substring(txtpatientNames.Text.LastIndexOf(':') + 1) : "0") : 0;

                }
                else
                {
                    objlabbill.UHID = 0;
                    hdnpatientName.Value = "";
                }
            }
            else
            {
                objlabbill.UHID = Convert.ToInt64(txtpatientNames.Text == "" ? "0" : txtpatientNames.Text);
                hdnpatientName.Value = "";
            }

            //}
            objlabbill.PatientName = txtpatientNames.Text = null;
            objlabbill.InvestigationNo = txt_invnumber.Text == "" ? "0" : txt_invnumber.Text.Trim();
            objlabbill.Paymode = Convert.ToInt32(ddlpaymentmodes.SelectedValue == "" ? "0" : ddlpaymentmodes.SelectedValue);
            objlabbill.CollectedByID = Convert.ToInt64(ddlcollectedby.SelectedValue == "" ? "0" : ddlcollectedby.SelectedValue);
            objlabbill.RunnerID = Convert.ToInt32(ddlRunnerBy.SelectedValue == "" ? "0" : ddlRunnerBy.SelectedValue);
            //string datefrom = from.ToString("yyyy-MM-dd");
            //string timefrom = txttimepickerfrom.Text.Trim();
            //from = Convert.ToDateTime(datefrom + " " + timefrom);
            //objlabbill.DateFrom = from;
            //string dateto = To.ToString("yyyy-MM-dd");
            //string timeto = txttimepickerto.Text.Trim();
            //To = Convert.ToDateTime(dateto + " " + timeto);
            //objlabbill.DateTo = To;
            objlabbill.DateFrom = from;
            objlabbill.DateTo = To;
            objlabbill.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objlabbill.CurrentIndex = curIndex;
            objlabbill.AmountEnable = LogData.AmountEnable;
            return objbillingBO.GetLabBillList(objlabbill);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txt_invnumber.Text = "";
            //txtautoUHID.Text = "";
            hdnpatientName.Value = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            ddlstatus.SelectedIndex = 0;
            gvdepositlist.DataSource = null;
            gvdepositlist.DataBind();
            gvdepositlist.Visible = false;
            lblresult.Visible = false;
            txtpatientNames.Text = "";
            ddlpaymentmode.SelectedIndex = 0;
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage2.Visible = false;
            lblresult.Visible = false;
            divmsg2.Visible = false;
            //divmsg3.Visible = false;
            txttotalbillamount.Text = "0.00";
            txt_totaldue.Text = "0.00";
            txttotaldiscounted.Text = "0.00";
            txttotalpaid.Text = "0.00";
            txttotalbillamount.Text = "0.00";
            txttotaldiscounted.Text = "0.00";
            txttotalpaid.Text = "0.00";
            txtTotalRunnerAmt.Text = "0.00";
            txt_refno.Text = "";
            btnprints.Attributes["disabled"] = "disabled";

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPatientName(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        protected void gvdepositlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {

                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblresult, "DeleteEnable", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                        return;
                    }
                    else
                    {
                        lblresult.Visible = false;
                    }
                    if (LogData.PrintEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblresult, "PrintEnable", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                        return;
                    }
                    else
                    {
                        lblresult.Visible = false;
                    }
                    LabBillingData objbill = new LabBillingData();
                    OPDbillingBO objstdBO = new OPDbillingBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvdepositlist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    Label UHID = (Label)gr.Cells[0].FindControl("lbluhid");
                    Label name = (Label)gr.Cells[0].FindControl("lblname");
                    Label address = (Label)gr.Cells[0].FindControl("lbladdress");
                    Label totalbillamount = (Label)gr.Cells[0].FindControl("lbltotalbillamount");
                    Label adjustedamount = (Label)gr.Cells[0].FindControl("lblaajustedamount");
                    Label discountedamount = (Label)gr.Cells[0].FindControl("lbldiscountedamount");
                    Label amount = (Label)gr.Cells[0].FindControl("lblamount");
                    Label addedby = (Label)gr.Cells[0].FindControl("lbladdedBy");
                    Label addeddate = (Label)gr.Cells[0].FindControl("lbladt");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objbill.Remarks = txtremarks.Text;
                    }
                    objbill.BillNo = ID.Text.Trim();
                    objbill.UHID = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                    objbill.EmployeeID = LogData.EmployeeID;
                    objbill.FinancialYearID = LogData.FinancialYearID;
                    objbill.IPaddress = LogData.IPaddress;
                    objbill.HospitalID = LogData.HospitalID;
                    objbill.Amount = Convert.ToDecimal(amount.Text == "" ? "0" : amount.Text);
                    objbill.AdjustedAmount = Convert.ToDecimal(adjustedamount.Text == "" ? "0" : adjustedamount.Text);

                    int Result = objstdBO.DeleteOPDLabBillByID(objbill);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        divmsg2.Attributes["class"] = "SucessAlert";
                        divmsg2.Visible = true;
                        bindgrid(1);
                    }
                    else
                    {
                        if (Result == 2)
                        {
                            Messagealert_.ShowMessage(lblmessage2, "AccountClosed", 0);
                            divmsg2.Visible = true;
                            divmsg2.Attributes["class"] = "FailAlert";
                            //bindgrid();
                        }
                        else
                        {
                            Messagealert_.ShowMessage(lblmessage2, "system", 0);
                            divmsg2.Attributes["class"] = "FailAlert";
                            divmsg2.Visible = true;
                        }
                    }

                }
                if (e.CommandName == "Token")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvdepositlist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    string code = Commonfunction.getBarcode(ID.Text.ToString());
                    String barcode = " <tr><td align=\"center\"><label style=\"font-size: 9px; text - align: left;\" >Investigation</label><br><img style=\"width:60%\" src=\"" + code + "\"/> " +
                        "</td><td align=\"center\"><label style=\"font-size: 9px; text - align: left;\" >Investigation</label><br><label style=\"font-size: 9px;\">" + ID.Text + "</label></td></tr>" +
                             "<tr><td align=\"center\"><label style=\"font-size: 9px;\">" + ID.Text + "</label></td>" +
                             "<td align=\"right\"></td> </tr>";
                    LitBarcodelist.Text = barcode;
                    this.ModelListBarcode.Show();
                }              
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
            }
        }
        protected void gvoplabservicelist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSerial = (Label)e.Row.FindControl("lblserialID");
                DropDownList ddltestcenter = (DropDownList)e.Row.FindControl("ddl_testcenter");
                Label lbltestcenterID = (Label)e.Row.FindControl("lbl_testcenterID");
                Label discountamount = (Label)e.Row.FindControl("lbl_discount_amt");
                LinkButton delete = (LinkButton)e.Row.FindControl("lnkDelete");
                TextBox value = (TextBox)e.Row.FindControl("txt_dis_value");
                DropDownList ddlurgencyState = (DropDownList)e.Row.FindControl("ddl_urgency");
                DropDownList discounttype = (DropDownList)e.Row.FindControl("ddl_discount_type");
                lblSerial.Text = ((gvdepositlist.PageIndex * gvdepositlist.PageSize) + e.Row.RowIndex + 1).ToString();
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddltestcenter, mstlookup.GetLookupsList(LookupName.TestCenter));
                ddltestcenter.SelectedValue = lbltestcenterID.Text == "" || lbltestcenterID.Text == null ? "0" : lbltestcenterID.Text;
                Commonfunction.PopulateDdl(ddlurgencyState, mstlookup.GetLookupsList(LookupName.Urgency));
                ddlurgencyState.SelectedIndex = 1;
                if (ddl_servicecategory.SelectedValue == "2")
                {
                    discountamount.Visible = false;
                    delete.Visible = false;
                    value.Visible = false;
                    discounttype.Visible = false;
                }
                else
                {
                    discountamount.Visible = true;
                    delete.Visible = true;
                    value.Visible = true;
                    discounttype.Visible = true;
                }
                if (Convert.ToInt32(hdisMsbDoctor.Value) > 0)
                {
                    if (Convert.ToInt32(hdMsbPc.Value) > 0)
                    {
                        discounttype.Attributes["disabled"] = "disabled";
                        discounttype.SelectedIndex = 1;
                        value.Text = hdMsbPc.Value;
                        value.ReadOnly = true;

                        discountChange(sender, e.Row);
                    }

                }
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<LabBillingData> DepositDetails = GetPatientLabBillList(0);
            List<OPDLabbillingDataTOeXCEL> ListexcelData = new List<OPDLabbillingDataTOeXCEL>();
            int i = 0;
            foreach (LabBillingData row in DepositDetails)
            {
                OPDLabbillingDataTOeXCEL Ecxeclpat = new OPDLabbillingDataTOeXCEL();
                Ecxeclpat.RefNo= DepositDetails[i].RefNo;
                Ecxeclpat.UHID = DepositDetails[i].UHID;
                Ecxeclpat.PatientName = DepositDetails[i].PatientName;
                Ecxeclpat.Address = DepositDetails[i].Address;
                Ecxeclpat.TestNameList = DepositDetails[i].TestNameList;
                Ecxeclpat.BillAmount = Convert.ToDecimal(DepositDetails[i].TotalBillAmount.ToString("0.00"));
                Ecxeclpat.Referal = DepositDetails[i].ReferalName;
                Ecxeclpat.TotalDiscountedAmount = Convert.ToDecimal(DepositDetails[i].DiscountedAmount.ToString("0.00"));
                Ecxeclpat.TotalAdjustedAmount = Convert.ToDecimal(DepositDetails[i].TotalAdjustedAmount.ToString("0.00"));
                Ecxeclpat.TotalPaidAmount = Convert.ToDecimal(DepositDetails[i].PaidAmount.ToString("0.00"));
                Ecxeclpat.DueBalance = Convert.ToDecimal(DepositDetails[i].DueAmount.ToString("0.00"));
                Ecxeclpat.AddedBy = DepositDetails[i].EmpName;
                ListexcelData.Add(Ecxeclpat);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        public List<LabBillingData> GetPatientLabBillList(int curIndex)
        {
            LabBillingData objlabbill = new LabBillingData();
            OPDbillingBO objbillingBO = new OPDbillingBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            //objlabbill.UHID = Convert.ToInt64(txtautoUHID.Text == "" ? "0" : txtautoUHID.Text);
            objlabbill.PatientName = txtpatientNames.Text == "" ? null : txtpatientNames.Text.Trim();
            objlabbill.InvestigationNo = txt_invnumber.Text == "" ? "0" : txt_invnumber.Text.Trim();
            objlabbill.Paymode = Convert.ToInt32(ddlpaymentmodes.SelectedValue == "" ? "0" : ddlpaymentmodes.SelectedValue);
            objlabbill.CollectedByID = Convert.ToInt64(ddlcollectedby.SelectedValue == "" ? "0" : ddlcollectedby.SelectedValue);
            objlabbill.RunnerID = Convert.ToInt32(ddlRunnerBy.SelectedValue == "" ? "0" : ddlRunnerBy.SelectedValue);
 
            string datefrom = from.ToString("yyyy-MM-dd");
            string timefrom = txttimepickerfrom.Text.Trim();
            from = Convert.ToDateTime(datefrom + " " + timefrom);
            objlabbill.DateFrom = from;
            string dateto = To.ToString("yyyy-MM-dd");
            string timeto = txttimepickerto.Text.Trim();
            To = Convert.ToDateTime(dateto + " " + timeto);
            objlabbill.DateTo = To;

            objlabbill.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objbillingBO.GetPatientLabBillList(objlabbill);
        }

        public class ListtoDataTableConverter
        {

            public DataTable ToDataTable<T>(List<T> items)
            {

                DataTable dataTable = new DataTable(typeof(T).Name);

                // Get all the properties

                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo prop in Props)
                {

                    //  Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);

                }

                foreach (T item in items)
                {

                    var values = new object[Props.Length];

                    for (int i = 0; i < Props.Length; i++)
                    {

                        //       inserting property values to datatable rows

                        values[i] = Props[i].GetValue(item, null);

                    }

                    dataTable.Rows.Add(values);

                }

                //     put a breakpoint here and check datatable

                return dataTable;

            }
        }
        protected void gvopcollection_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton linkdelete = (LinkButton)e.Row.FindControl("lnkDelete");
                Label lblIsVerified = (Label)e.Row.FindControl("lblIsVerified");
                if (LogData.DeleteEnable == 0)
                {
                    linkdelete.Attributes["disabled"] = "disabled";
                }
                else
                {
                    linkdelete.Attributes.Remove("disabled");
                }
                if (LogData.PrintEnable == 0)
                {
                    gvdepositlist.Columns[13].Visible = false;
                }
                else
                {
                    gvdepositlist.Columns[13].Visible = true;
                }
                if (lblIsVerified.Text == "0")
                {

                    e.Row.Cells[14].Visible = false;
                    e.Row.Cells[15].ColumnSpan = 4;
                }
                else
                {
                    //e.Row.Cells[14].Controls.Clear();
                    //e.Row.Cells[15].Controls.Clear();
                    e.Row.Cells[14].ColumnSpan = 2;
                    e.Row.Cells[15].Visible = false;
                }
            }
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "ExportEnable", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage2.Visible = false;
            }
            if (ddlexport.SelectedIndex == 1)
            {
                ExportoExcel();
            }
            else if (ddlexport.SelectedIndex == 2)
            {
                ExportToPdf();
            }
            else
            {
                Messagealert_.ShowMessage(lblresult, "ExportType", 0);
                // divmsg3.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        public void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    gvdepositlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvdepositlist.Columns[10].Visible = false;
                    gvdepositlist.Columns[11].Visible = false;
                    gvdepositlist.Columns[12].Visible = false;
                    gvdepositlist.Columns[13].Visible = false;

                    gvdepositlist.RenderControl(hw);
                    gvdepositlist.HeaderRow.Style.Add("width", "15%");
                    gvdepositlist.HeaderRow.Style.Add("font-size", "10px");
                    gvdepositlist.Style.Add("text-decoration", "none");
                    gvdepositlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvdepositlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=OPDLabBillDetails.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        protected void gvdepositlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Lab Billing Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=LabBillingDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblresult, "Exported", 1);
                // divmsg3.Attributes["class"] = "SucessAlert";
            }
        }

        protected void ddl_discount_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = ((GridViewRow)((DropDownList)sender).NamingContainer);
            discountChange(sender, currentRow);

        }
        public void discountChange(object sender, GridViewRow currentRow)
        {
            List<Discount> DiscountList = Session["DiscountList"] == null ? new List<Discount>() : (List<Discount>)Session["DiscountList"];


            DropDownList ddl_discount_type = (DropDownList)currentRow.FindControl("ddl_discount_type");
            TextBox txt_dis_value = (TextBox)currentRow.FindControl("txt_dis_value");
            Label lblNetAmount = (Label)currentRow.FindControl("lblnetcharges");
            Label lbl_discount_amt = (Label)currentRow.FindControl("lbl_discount_amt");
            if (DiscountList.Count > 0)
            {
                Messagealert_.ShowMessage(lblmessage, "CustomDiscount", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_dis_value.Text = "";
                lbl_discount_amt.Text = "";
                return;

            }
            decimal value = Convert.ToDecimal(txt_dis_value.Text == "" ? "0" : txt_dis_value.Text);
            decimal NetAmount = Convert.ToDecimal(lblNetAmount.Text == "" ? "0" : lblNetAmount.Text);

            if (ddl_discount_type.SelectedIndex == 0)
            {
                if (value > NetAmount)
                {
                    txt_dis_value.Text = "0";
                    lbl_discount_amt.Text = "0";
                    Messagealert_.ShowMessage(lblmessage, "DiscountAmount", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    totalCalculate();
                }
                else
                {
                    lbl_discount_amt.Text = Commonfunction.Getrounding(value.ToString());
                    totalCalculate();
                }
            }
            else
            {
                if (value > 100)
                {
                    txt_dis_value.Text = "";
                    lbl_discount_amt.Text = "";
                    Messagealert_.ShowMessage(lblmessage, "Percentage", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    totalCalculate();
                }
                else
                {

                    decimal pcValue = 0;

                    pcValue = ((value / 100) * NetAmount);
                    lbl_discount_amt.Text = Commonfunction.Getrounding(pcValue.ToString());
                    totalCalculate();
                }

            }
            if (hdPatCat.Value != "2")
            {
                decimal discountamount = Convert.ToDecimal(lbl_discount_amt.Text == "" ? "0" : lbl_discount_amt.Text);
                if (discountamount > 0)
                {
                    btnlinkdiscount.Visible = false;
                }
                else
                {
                    btnlinkdiscount.Visible = true;

                }
            }
            else
            {
                btnlinkdiscount.Visible = true;
            }
        }
        public void totalCalculate()
        {
            decimal totalDiscount = 0;
            foreach (GridViewRow row in gvoplabservicelist.Rows)
            {
                Label lbl_discount_amt = (Label)gvoplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lbl_discount_amt");
                totalDiscount = totalDiscount + (Convert.ToDecimal(lbl_discount_amt.Text == "" ? "0" : lbl_discount_amt.Text));
            }

            decimal TotalAmount = Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text.ToString());
            txt_discount.Text = Commonfunction.Getrounding(totalDiscount.ToString());
            txt_payableamnt.Text = Commonfunction.Getrounding((TotalAmount - Convert.ToDecimal(txt_discount.Text == "" ? "0" : txt_discount.Text)).ToString());
            txt_paidamount.Text = Commonfunction.Getrounding((TotalAmount - Convert.ToDecimal(txt_discount.Text == "" ? "0" : txt_discount.Text)).ToString());
            if (totalDiscount > 0)
            {
                txt_discount.ReadOnly = true;
                if (Convert.ToInt32(hdPatCat.Value) != 2)
                {
                    btnsave.Text = "Send Request";
                }

            }
            else
            {
                btnsave.Text = "Save";
            }

        }
        protected void txt_dis_value_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
            discountChange(sender, currentRow);
        }
        protected void txt_paidamount_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txt_payableamnt.Text == "" ? "0" : txt_payableamnt.Text) >= Convert.ToDecimal(txt_paidamount.Text == "" ? "0" : txt_paidamount.Text))
            {
                lblmessage.Visible = false;
                txt_due.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_payableamnt.Text == "" ? "0" : txt_payableamnt.Text) - Convert.ToDecimal(txt_paidamount.Text == "" ? "0" : txt_paidamount.Text)).ToString());
            }
            else
            {
                Messagealert_.ShowMessage(lblmessage, "ExceedAmount", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_due.Text = "";
                txt_paidamount.Text = "";
                this.txt_paidamount.Focus();
                return;
            }

        }
        protected void btnDisSave_Click(object sender, EventArgs e)
        {
            if (LogData.SaveEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SaveEnable", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }

            if (txtUHID.Text == "" && ddl_patienttype.SelectedValue == "1")
            {
                Messagealert_.ShowMessage(lblmessage, "UHID", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtUHID.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (txt_totalbill.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Service", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_labservices.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (ddlpaymentmode.SelectedIndex > 1)
            {
                if (txt_bank.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "BankName", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_bank.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (txt_chequenumber.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Chequenumber", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_chequenumber.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }

            List<LabBillingData> Listbill = new List<LabBillingData>();
            OPDbillingBO objbillingBO = new OPDbillingBO();
            LabBillingData objlabserv = new LabBillingData();

            foreach (GridViewRow row in GVDiscountApprovalList.Rows)
            {
                IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                Label Particulars = (Label)GVDiscountApprovalList.Rows[row.RowIndex].Cells[0].FindControl("lbllabparticulars");
                Label amount = (Label)GVDiscountApprovalList.Rows[row.RowIndex].Cells[0].FindControl("lbl_labcharges");
                Label qty = (Label)GVDiscountApprovalList.Rows[row.RowIndex].Cells[0].FindControl("lblquantity");
                Label NetCharge = (Label)GVDiscountApprovalList.Rows[row.RowIndex].Cells[0].FindControl("lblnetcharges");
                Label ID = (Label)GVDiscountApprovalList.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                Label DepartmentType = (Label)GVDiscountApprovalList.Rows[row.RowIndex].Cells[0].FindControl("lbldeprtmentID");
                Label Doctor = (Label)GVDiscountApprovalList.Rows[row.RowIndex].Cells[0].FindControl("lbldoctorID");

                DropDownList Testcenter = (DropDownList)GVDiscountApprovalList.Rows[row.RowIndex].Cells[0].FindControl("ddl_testcenter");
                DropDownList urgency = (DropDownList)GVDiscountApprovalList.Rows[row.RowIndex].Cells[0].FindControl("ddl_urgency");

                Label lbl_discount_amt = (Label)GVDiscountApprovalList.Rows[row.RowIndex].Cells[0].FindControl("lbl_discount_amt");
                LabBillingData ObjDetails = new LabBillingData();

                ObjDetails.TestName = Particulars.Text == "" ? null : Particulars.Text;
                ObjDetails.LabServiceCharge = Convert.ToDecimal(amount.Text == "" ? "0" : amount.Text);
                ObjDetails.Quantity = Convert.ToInt32(qty.Text == "" ? "0" : qty.Text);
                ObjDetails.NetLabServiceCharge = Convert.ToDecimal(NetCharge.Text == "" ? "0" : NetCharge.Text);
                ObjDetails.LabServiceID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                ObjDetails.DocID = Convert.ToInt32(Doctor.Text == "" ? "0" : Doctor.Text);
                ObjDetails.TestCenterID = Convert.ToInt32(Testcenter.SelectedValue == "" ? "0" : Testcenter.SelectedValue);
                ObjDetails.UrgencyID = Convert.ToInt32(urgency.SelectedValue == "" ? "0" : urgency.SelectedValue);
                ObjDetails.isDis = Convert.ToInt32(Convert.ToDecimal(lbl_discount_amt.Text.Trim() == "" ? "0" : lbl_discount_amt.Text.Trim()) == 0 ? 0 : 1);
                ObjDetails.DisAmount = Convert.ToDecimal(lbl_discount_amt.Text == "" ? "0" : lbl_discount_amt.Text);

                Listbill.Add(ObjDetails);
            }
            objlabserv.XMLData = XmlConvertor.OpdLabDiscountBillDatatoXML(Listbill).ToString();
            objlabserv.ID = Convert.ToInt64(ViewState["BillID"].ToString() == "" ? "0" : ViewState["BillID"].ToString());
            objlabserv.TotalBillAmount = Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text);
            objlabserv.UHID = Convert.ToInt64(txtUHID.Text.Contains(":") ? txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1) : "0");
            objlabserv.AdjustedAmount = Convert.ToDecimal(txt_adjustedamount.Text == "" ? "0" : txt_adjustedamount.Text);
            objlabserv.DiscountedAmount = Convert.ToDecimal(txt_discount.Text == "" ? "0" : txt_discount.Text);
            objlabserv.PatientType = Convert.ToInt32(ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue);

            objlabserv.PatientCat = ddl_patienttype.SelectedValue == "4" ? 1 : Convert.ToInt32(hdPatCat.Value);
            objlabserv.PaidAmount = Convert.ToDecimal(txt_paidamount.Text == "" ? "0" : txt_paidamount.Text);
            objlabserv.PaymentMode = Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue);
            objlabserv.BankName = txt_bank.Text == "" ? null : txt_bank.Text;
            objlabserv.CheaqueNo = txt_chequenumber.Text == "" ? null : txt_chequenumber.Text;
            objlabserv.INvoiceNo = txtinvoicenumber.Text == "" ? null : txtinvoicenumber.Text;
            objlabserv.FinancialYearID = LogData.FinancialYearID;
            objlabserv.isExtraDiscount = 0;
            objlabserv.EmployeeID = LogData.EmployeeID;
            objlabserv.AddedBy = LogData.AddedBy;
            objlabserv.HospitalID = LogData.HospitalID;
            objlabserv.IsActive = LogData.IsActive;
            objlabserv.IPaddress = LogData.IPaddress;
            objlabserv.ActionType = Enumaction.Insert;
            objlabserv.IsVerified = LogData.BillSetting == 0 ? 1 : 0;
            List<LabBillingData> result = objbillingBO.UpdateOPDLabBill(objlabserv);
            if (result.Count > 0)
            {
                if (result[0].isDis == 0)
                {
                    if (LogData.PrintEnable == 0)
                    {
                        btnprint.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        if (LogData.BillSetting == 0)
                        {
                            btnprint.Attributes.Remove("disabled");

                        }
                        else
                        {
                            btnprint.Visible = false;
                            btn_token.Visible = true;
                            ddlpaymentmode.Attributes["disabled"] = "disabled";
                            txt_chequenumber.ReadOnly = true;
                            txtinvoicenumber.ReadOnly = true;
                        }
                    }
                    btnsave.Attributes["disabled"] = "disabled";
                    txtbillNo.Text = result[0].BillNo.ToString();
                    txt_investigationno.Text = result[0].InvestigationNo.ToString();
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    Session["LabServiceList"] = null;
                    Session["DiscountList"] = null;
                    ViewState["BillID"] = null;
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    txtUHID.Text = "";
                }

            }
        }

        protected void GVDiscountApprovalList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTestCenter = (Label)e.Row.FindControl("lblTestCenter");
                Label lblUrgency = (Label)e.Row.FindControl("lblUrgency");
                DropDownList ddltestcenter = (DropDownList)e.Row.FindControl("ddl_testcenter");
                DropDownList ddlurgencyState = (DropDownList)e.Row.FindControl("ddl_urgency");

                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddltestcenter, mstlookup.GetLookupsList(LookupName.TestCenter));

                Commonfunction.PopulateDdl(ddlurgencyState, mstlookup.GetLookupsList(LookupName.Urgency));
                ddlurgencyState.SelectedValue = lblUrgency.Text;
                ddltestcenter.SelectedValue = lblTestCenter.Text;
                ddlurgencyState.Attributes["disabled"] = "disabled";
                ddltestcenter.Attributes["disabled"] = "disabled";
            }
        }

        protected void btnAddTpa_Click(object sender, EventArgs e)
        {
            Decimal TotalDiscount = 0;
            if (ddl_tpa_patient_cat.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblPopUpmsg, "Please Select Category!", 0);
                divPopMsg.Visible = true;
                ddl_tpa_patient_cat.Focus();
                this.MDTPA.Show();
                return;
            }
            else
            {
                lblPopUpmsg.Visible = false;
                divPopMsg.Visible = false;
                this.MDTPA.Show();
            }
            if (ddl_tpa_patient_cat.SelectedValue == "4")
            {
                if (ddl_tpa_patient_sub_cat.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblPopUpmsg, "Please Select sub Category!", 0);
                    divPopMsg.Visible = true;
                    ddl_tpa_patient_sub_cat.Focus();
                    this.MDTPA.Show();
                    return;
                }
                else
                {
                    lblPopUpmsg.Visible = false;
                    divPopMsg.Visible = false;
                    this.MDTPA.Show();
                }
            }
            else
            {
                lblPopUpmsg.Visible = false;
                divPopMsg.Visible = false;
                this.MDTPA.Show();
            }

            if (Convert.ToDecimal(txt_tpa_amount.Text == "" ? "0" : txt_tpa_amount.Text) <= 0)
            {
                Messagealert_.ShowMessage(lblPopUpmsg, "Discount", 0);
                divPopMsg.Visible = true;
                txt_tpa_amount.Focus();
                this.MDTPA.Show();
                return;
            }
            else
            {
                lblPopUpmsg.Visible = false;
                divPopMsg.Visible = false;
                this.MDTPA.Show();
            }
            Decimal total_dis = Convert.ToDecimal(txt_discount.Text == "" ? "0" : txt_discount.Text) + Convert.ToDecimal(txt_tpa_amount.Text == "" ? "0" : txt_tpa_amount.Text);

            if (Convert.ToDecimal(txt_totalbill.Text == "" ? "0.00" : txt_totalbill.Text) < total_dis)
            {

                txt_discount.Focus();
                ddl_tpa_patient_cat.SelectedIndex = 0;
                ddl_tpa_patient_sub_cat.SelectedIndex = 0;
                Messagealert_.ShowMessage(lblPopUpmsg, "DiscountOver", 0);
                divPopMsg.Visible = true;
                txt_discount.Focus();
                this.MDTPA.Show();
                return;
            }
            else
            {
                txt_discount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_discount.Text == "" ? "0" : txt_discount.Text) + Convert.ToDecimal(txt_tpa_amount.Text == "" ? "0.0" : txt_tpa_amount.Text)).ToString());
                txt_payableamnt.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_paidamount.Text == "" ? "0" : txt_paidamount.Text) - Convert.ToDecimal(txt_tpa_amount.Text == "" ? "0.0" : txt_tpa_amount.Text)).ToString());
                txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_paidamount.Text == "" ? "0" : txt_paidamount.Text) - Convert.ToDecimal(txt_tpa_amount.Text == "" ? "0.0" : txt_tpa_amount.Text)).ToString());
                divPopMsg.Visible = false;
                lblPopUpmsg.Visible = false;
                this.MDTPA.Show();
            }
            List<Discount> DiscountList = Session["DiscountList"] == null ? new List<Discount>() : (List<Discount>)Session["DiscountList"];
            Discount objdiscount = new Discount();
            objdiscount.PatientCatID = Convert.ToInt32(ddl_tpa_patient_cat.SelectedValue == "" ? "0" : ddl_tpa_patient_cat.SelectedValue);

            objdiscount.PatCat = ddl_tpa_patient_cat.SelectedItem.Text;


            if (ddl_tpa_patient_cat.SelectedValue == "4")
            {
                objdiscount.SubCatID = Convert.ToInt32(ddl_tpa_patient_sub_cat.SelectedValue == "" ? "0" : ddl_tpa_patient_sub_cat.SelectedValue);
                objdiscount.subCat = ddl_tpa_patient_sub_cat.SelectedItem.Text;
            }
            else
            {
                objdiscount.SubCatID = 0;
                objdiscount.subCat = "";
            }
            objdiscount.DiscountAmount = Convert.ToDecimal(txt_tpa_amount.Text == "" ? "0.0" : txt_tpa_amount.Text);

            TotalDiscount = TotalDiscount + Convert.ToDecimal(txt_tpa_amount.Text == "" ? "0.0" : txt_tpa_amount.Text);

            DiscountList.Add(objdiscount);
            if (DiscountList.Count > 0)
            {
                GVTPAList.DataSource = DiscountList;
                GVTPAList.DataBind();
                GVTPAList.Visible = true;
                txt_tpa_amount.Text = "";
                txt_tpa_amount.Focus();
                ddl_tpa_patient_cat.SelectedIndex = 0;
                ddl_tpa_patient_sub_cat.SelectedIndex = 0;
                Session["DiscountList"] = DiscountList;
            }
            else
            {
                GVTPAList.DataSource = null;
                GVTPAList.DataBind();
                GVTPAList.Visible = true;
            }
            this.MDTPA.Show();
        }
        protected void btnlinkdiscount_Click(object sender, EventArgs e)
        {
            MasterLookupBO mstlookup = new MasterLookupBO();

            if (hdPatCat.Value == "4")
            {
                Commonfunction.PopulateDdl(ddl_tpa_patient_cat, mstlookup.GetLookupsList(LookupName.TPAOnly));
            }
            else
            {
                Commonfunction.PopulateDdl(ddl_tpa_patient_cat, mstlookup.GetLookupsList(LookupName.WithoutTPA));
            }
            Commonfunction.PopulateDdl(ddl_tpa_patient_sub_cat, mstlookup.GetLookupsList(LookupName.TPAList));
            ddl_tpa_patient_sub_cat.Attributes["disabled"] = "disabled";
            this.MDTPA.Show();
        }

        protected void GVTPAList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GVTPAList.Rows[i];
                    List<Discount> DiscountList = Session["DiscountList"] == null ? new List<Discount>() : (List<Discount>)Session["DiscountList"];
                    if (DiscountList.Count > 0)
                    {
                        Decimal Discount = DiscountList[i].DiscountAmount;
                        txt_discount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_discount.Text == "" ? "0" : txt_discount.Text) - Discount).ToString());
                        txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_paidamount.Text == "" ? "0" : txt_paidamount.Text) + Discount).ToString());
                    }
                    DiscountList.RemoveAt(i);
                    Session["DiscountList"] = DiscountList;
                    GVTPAList.DataSource = DiscountList;
                    GVTPAList.DataBind();
                    this.MDTPA.Show();
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblPopUpmsg, "system", 0);
            }
        }

        protected void ddl_tpa_patient_cat_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            if (ddl_tpa_patient_cat.SelectedIndex == 0)
            {
                ddl_tpa_patient_sub_cat.SelectedIndex = 0;
                ddl_tpa_patient_sub_cat.Attributes["disabled"] = "disabled";
            }
            else
            {
                if (ddl_tpa_patient_cat.SelectedValue == "4")
                {
                    ddl_tpa_patient_sub_cat.Attributes.Remove("disabled");
                    Commonfunction.PopulateDdl(ddl_tpa_patient_sub_cat, mstlookup.GetLookupsList(LookupName.TPAList));
                }
                else
                {
                    ddl_tpa_patient_sub_cat.SelectedIndex = 0;
                    ddl_tpa_patient_sub_cat.Attributes["disabled"] = "disabled";
                }

            }
            this.MDTPA.Show();
        }

        protected void btnClose_Click1(object sender, EventArgs e)
        {

        }
        protected void txt_referal_TextChanged(object sender, EventArgs e)
        {
            txtUHID.Focus();
        }
        protected void btn_token_Click(object sender, EventArgs e)
        {
            string code = Commonfunction.getBarcode(txtbillNo.Text.ToString());
            String barcode = " <tr><td align=\"center\"><label style=\"font-size: 9px; text - align: left;\" >Investigation</label><br><img style=\"width:60%\" src=\"" + code + "\"/> " +
                "</td><td align=\"center\"><label style=\"font-size: 9px; text - align: left;\" >Investigation</label><br><label style=\"font-size: 9px;\">" + txtbillNo.Text + "</label><br><label style=\"font-size: 9px;\">" + ddlpaymentmode.SelectedItem + "</label>" +
                "<br><label style=\"font-size: 9px;\">" + txt_chequenumber.Text + "" + txtinvoicenumber.Text + "</label>" +
                "</td></tr>" +

                "<tr><td align=\"center\"><label style=\"font-size: 9px;\">" + txtbillNo.Text + "</label></td>" +
                     "<td align=\"right\"></td> </tr>";
            ltBarcode.Text = barcode;
            this.MDBarcode.Show();
        }

    }
}