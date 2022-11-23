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
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.AdmissionData;

namespace Mediqura.Web.MedBills
{
    public partial class OpdBilling : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                txtdiscount.ReadOnly = true;
                if (Session["PAT_UHID"] != null)
                {
                    txtUHID.Text = Session["PAT_UHID"].ToString();
                    loadUHIDdata();
                    Session["PAT_UHID"] = null;
                }
                ViewState["BillID"] = null;
                if (Session["BILLID"] != null)
                {
                    Int64 ID = Convert.ToInt32(Session["BILLID"].ToString());
                    Session["BILLID"] = null;
                    ViewState["BillID"] = ID;
                    getServiceDetails(ID);
                }
                Session["DiscountList"] = null;
            }
        }
        private void getServiceDetails(Int64 ID)
        {
            OPDbillingBO objInfoBO = new OPDbillingBO();
            List<OPDbillingData> getResult = new List<OPDbillingData>();
            getResult = objInfoBO.GetDiscountOpServiceByID(ID);
            if (getResult.Count > 0)
            {
                PatientData Objpaic = new PatientData();
                RegistrationBO objBO = new RegistrationBO();
                List<PatientData> result = new List<PatientData>();
                Objpaic.UHID = getResult[0].UHID;
                result = objBO.GetPatientDetailsByUHID(Objpaic);
                if (result.Count > 0)
                {
                    txtUHID.Text = result[0].PatientDetailName.ToString();
                    hdMsbPc.Value = result[0].MSBpc.ToString();
                    txt_patientcategory.Text = result[0].PatientCategory.ToString();
                    txtUHID.ReadOnly = true;
                    ddldepartment.SelectedValue = getResult[0].DeptID.ToString();
                    hdnDepartmentID.Value = getResult[0].DeptID.ToString();
                    ddl_source.SelectedValue = getResult[0].SourceID.ToString();
                    txt_referby.Text = getResult[0].ReferalName.ToString();
                    MasterLookupBO mstlookup = new MasterLookupBO();
                    Commonfunction.PopulateDdl(ddldoctor, mstlookup.GetDepartmentDoctor(Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue)));
                    ddldoctor.SelectedValue = getResult[0].DocID.ToString();
                }
                Session["ServiceList"] = null;
                Session["DiscountList"] = null;
                ddlservicetype.Attributes["disabled"] = "disabled";
                ddldepartment.Attributes["disabled"] = "disabled";
                ddldoctor.Attributes["disabled"] = "disabled";
                txtquantity.ReadOnly = true;
                txtdiscount.ReadOnly = true;
                txtremarks.ReadOnly = true;
                btnsave.Visible = false;
                btnDisSave.Visible = true;
                btnlinkdiscount.Visible = false;
                txtdiscremoarks.ReadOnly = true;
                txtdiscremoarks.Text = getResult[0].Remarks.ToString();
                txttotalamount.Text = Commonfunction.Getrounding(getResult[0].TotalBill.ToString());
                txtdiscount.Text = Commonfunction.Getrounding(getResult[0].TotalDiscountedAmount.ToString());
                txtbalanceinac.Text = Commonfunction.Getrounding(getResult[0].BalanceAmount.ToString());
                txtadjustedamount.Text = Commonfunction.Getrounding(getResult[0].AdjustedAmount.ToString());
                txtpaidamount.Text = Commonfunction.Getrounding(getResult[0].TotalPaidAmount.ToString());
                GVDiscountApprovalList.DataSource = getResult;
                GVDiscountApprovalList.DataBind();
                GVDiscountApprovalList.Visible = true;
            }
            else
            {
                ddl_source.SelectedValue = "0";
                txt_referby.Text = "";
                Messagealert_.ShowMessage(lblmessage, "Bill already created!", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
        }
        private void bindddl()
        {
            hdisMsbDoctor.Value = "0";
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlpaymentmode, mstlookup.GetLookupsList(LookupName.PaymentMode));
            Commonfunction.PopulateDdl(ddlpaymentmodes, mstlookup.GetLookupsList(LookupName.PaymentMode));
            ddlpaymentmode.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.OPDepartment));
            Commonfunction.PopulateDdl(ddlcollectedby, mstlookup.GetLookupsList(LookupName.CollectedBy));
            Commonfunction.PopulateDdl(ddlservicetype, mstlookup.GetLookupsList(LookupName.OPServiceType));
            txtdatefrom.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            txttotalbill.Text = "0.00";
            txtajusted.Text = "0.00";
            txttotaldiscounted.Text = "0.00";
            txttotalpaid.Text = "0.00";
            Commonfunction.Insertzeroitemindex(ddldoctor);
            Session["ServiceList"] = null;
            Session["DiscountList"] = null;
            ddldepartment.Attributes["disabled"] = "disabled";
            ddldoctor.Attributes["disabled"] = "disabled";
            btnprints.Attributes["disabled"] = "disabled";
            btnprint.Attributes["disabled"] = "disabled";
            btn_cs.Attributes["disabled"] = "disabled";
            btn_redgcard.Attributes["disabled"] = "disabled";
            this.txtUHID.Focus();
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetServices(string prefixText, int count, string contextKey)
        {
            ServicesData Objpaic = new ServicesData();
            ServiceBO objInfoBO = new ServiceBO();
            List<ServicesData> getResult = new List<ServicesData>();
            Objpaic.ServiceName = prefixText;
            Objpaic.ServiceTypeID = Convert.ToInt32(contextKey);
            Objpaic.DoctorID = count;
            getResult = objInfoBO.Getopservices(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ServiceName.ToString());
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
        public void loadUHIDdata()
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.UHID = Convert.ToInt64(txtUHID.Text.Trim() == "" ? "0" : txtUHID.Text.Trim());
            getResult = objInfoBO.GetPatientDetailsByUHID(Objpaic);
            if (getResult.Count > 0)
            {
                txt_patientcategory.Text = getResult[0].PatientCategory.ToString();
                txtUHID.Text = getResult[0].PatientDetailName.ToString();
                txt_tpacompany.Text = getResult[0].TPAcompanyName.ToString();
                hdnprintID.Value = getResult[0].PrintregdCard.ToString();
                hdnlastvisitdoctorID.Value = getResult[0].DoctorID.ToString();
                hdnnumberdays.Value = getResult[0].NoDays.ToString();
                hdMsbPc.Value = getResult[0].MSBpc.ToString();
                hdPatCat.Value = getResult[0].PatientType.ToString();
                Session["ServiceList"] = null;
                Session["DiscountList"] = null;
                txtbalanceinac.Text = Commonfunction.Getrounding((Convert.ToDecimal(getResult[0].BalanceAmount.ToString())).ToString());
                if (Convert.ToInt32(getResult[0].NoDays) > 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Last visit detail : | Date :" + getResult[0].LastVisitDate.ToString("dd/MM/yyyy") + " | Department : " + getResult[0].DepartmentName + " | Doctor : " + getResult[0].DoctorName + ", " + getResult[0].NoDays + " day(s) ago. Please take service charge accordingly.", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    return;
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Today is the first visit. Please take service charge accordingly.", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    return;
                }
            }
            else
            {
                txt_patientcategory.Text = "";
                txt_tpacompany.Text = "";
                txtUHID.Text = "";
                hdnprintID.Value = null;
                hdnlastvisitdoctorID.Value = null;
                hdnnumberdays.Value = null;
                txtbalanceinac.Text = "";
                txtUHID.Focus();
            }
        }
        protected void txtUHID_TextChanged(object sender, EventArgs e)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
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
                    return;
                }
            }
            else
            {
                Objpaic.UHID = Convert.ToInt64(txtUHID.Text == "" ? "0" : txtUHID.Text);
            }
            getResult = objInfoBO.GetPatientDetailsByUHID(Objpaic);
            if (getResult.Count > 0)
            {
                txt_patientcategory.Text = getResult[0].PatientCategory.ToString();
                txtUHID.Text = getResult[0].PatientDetailName.ToString();
                txt_tpacompany.Text = getResult[0].TPAcompanyName.ToString();
                hdnprintID.Value = getResult[0].PrintregdCard.ToString();
                hdnlastvisitdoctorID.Value = getResult[0].DoctorID.ToString();
                hdnnumberdays.Value = getResult[0].NoDays.ToString();
                hdnUHID.Value = getResult[0].UHID.ToString();
                hdMsbPc.Value = getResult[0].MSBpc.ToString();
                hdPatCat.Value = getResult[0].PatientType.ToString();
                Session["ServiceList"] = null;
                Session["DiscountList"] = null;
                txtbalanceinac.Text = Commonfunction.Getrounding((Convert.ToDecimal(getResult[0].BalanceAmount.ToString())).ToString());
                if (Convert.ToInt32(getResult[0].NoDays) > 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Last visit detail : | Date :" + getResult[0].LastVisitDate.ToString("dd/MM/yyyy") + " | Department : " + getResult[0].DepartmentName + " | Doctor : " + getResult[0].DoctorName + ", " + getResult[0].NoDays + " day(s) ago. Please take service charge accordingly.", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    return;
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Today is the first visit. Please take service charge accordingly.", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    return;
                }
            }
            else
            {
                txt_patientcategory.Text = "";
                txt_tpacompany.Text = "";
                txtUHID.Text = "";
                hdnprintID.Value = null;
                hdnlastvisitdoctorID.Value = null;
                hdnnumberdays.Value = null;
                txtbalanceinac.Text = "";
                txtUHID.Focus();
            }
        }
        protected void txtservicecharge_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(hdncharges.Value) != Convert.ToDecimal(txtservicecharge.Text))
            {
                txtremarks.ReadOnly = false;
                txtremarks.Focus();
            }
            else
            {
                txtremarks.ReadOnly = true;
            }
        }
        protected void ddlservicetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddldepartment.SelectedIndex = 0;
            Commonfunction.Insertzeroitemindex(ddldoctor);
            if (ddlservicetype.SelectedIndex > 0)
            {

                txtservices.ReadOnly = false;
                AutoCompleteExtender2.ContextKey = ddlservicetype.SelectedValue == "" ? "0" : ddlservicetype.SelectedValue;
                ddldepartment.Attributes.Remove("disabled");
                ddldoctor.Attributes.Remove("disabled");
            }
            else
            {
                AutoCompleteExtender1.ContextKey = null;
                txtservices.ReadOnly = true;
            }
            if (ddl_source.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Source", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddl_source.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (txt_referby.Text.Trim() == "")
            {
                Messagealert_.ShowMessage(lblmessage, "PatReferBy", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_referby.Focus();
                return;
            }
            else
            {
                if (txt_referby.Text.Trim() != "" && Commonfunction.SemicolonSeparation_String_64(txt_referby.Text.ToString()) == 0)
                {
                    txt_referby.Text = "";
                    txt_referby.Focus();
                    Messagealert_.ShowMessage(lblmessage, "PatReferBy", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
            }
            if (ddlservicetype.SelectedValue == "2")
            {
                ddldepartment.SelectedIndex = 0;
                ddldoctor.SelectedIndex = 0;
                ddldepartment.Attributes["disabled"] = "disabled";
                ddldoctor.Attributes["disabled"] = "disabled";
            }
            else
            {
                ddldepartment.Attributes.Remove("disabled");
                ddldoctor.Attributes.Remove("disabled");
            }
            txtservicecharge.Text = "";
            txtservices.Text = "";
        }
        protected void ddlpaymentmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlpaymentmode.SelectedIndex > 0)
            {
                if (ddlpaymentmode.SelectedValue == "1")
                {
                    txtbank.Text = "";
                    txtbank.ReadOnly = true;
                    txt_chequenumber.ReadOnly = true;
                    txtinvoicenumber.ReadOnly = true;
                }
                else if (ddlpaymentmode.SelectedValue == "2")
                {
                    GetBankName(Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue));
                    txtbank.ReadOnly = true;
                    txt_chequenumber.ReadOnly = true;
                    txtinvoicenumber.ReadOnly = false;
                }
                else if (ddlpaymentmode.SelectedValue == "3")
                {
                    GetBankName(Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue));
                    txtbank.ReadOnly = true;
                    txt_chequenumber.ReadOnly = false;
                    txtinvoicenumber.ReadOnly = true;
                }
                else if (ddlpaymentmode.SelectedValue == "4")
                {
                    txtbank.Text = "";
                    txtbank.ReadOnly = false;
                    txt_chequenumber.ReadOnly = false;
                    txtinvoicenumber.ReadOnly = true;
                }
            }
            else
            {
                txtbank.Text = "";
                txtbank.ReadOnly = true;
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
                txtbank.Text = banklist[0].BankName.ToString();
                hdnbankID.Value = banklist[0].BankID.ToString();
            }
            else
            {
                txtbank.Text = "";
                hdnbankID.Value = null;
            }
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {

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
            if (ddl_source.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Source", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddl_source.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (txt_referby.Text.Trim() == "")
            {
                Messagealert_.ShowMessage(lblmessage, "PatReferBy", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_referby.Focus();
                return;
            }
            else
            {
                if (txt_referby.Text.Trim() != "" && Commonfunction.SemicolonSeparation_String_64(txt_referby.Text.ToString()) == 0)
                {
                    txt_referby.Text = "";
                    txt_referby.Focus();
                    Messagealert_.ShowMessage(lblmessage, "PatReferBy", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
            }
            if (ddlservicetype.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ServiceType", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddlservicetype.Focus();
                return;
            }
            else
            {
                if ((ddlservicetype.SelectedValue == "5" || ddlservicetype.SelectedValue == "1" || ddlservicetype.SelectedValue == "20" || ddlservicetype.SelectedValue == "21" || ddlservicetype.SelectedValue == "22") && ddldoctor.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Doctor", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txtservices.Text = "";
                    txtservicecharge.Text = "";
                    txtservices.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
            }
            if (txtservices.Text.Trim() == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Service", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtservices.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (txtservicecharge.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Charge", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtservicecharge.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (Convert.ToDecimal(hdncharges.Value) != Convert.ToDecimal(txtservicecharge.Text))
            {
                if (txtremarks.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Remarks", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txtremarks.Focus();
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
            string ID;
            var source = txtservices.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                // Check Duplicate data 
                foreach (GridViewRow row in gvopservicelist.Rows)
                {
                    Label ServiceID = (Label)gvopservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label servicetype = (Label)gvopservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblservicetypeID");
                    //if (servicetype.Text == "1" && (ID != "898" || ID != "897"))
                    //{
                    //    Messagealert_.ShowMessage(lblmessage, "Already added the consultant service", 0);
                    //    div1.Visible = true;
                    //    div1.Attributes["class"] = "FailAlert";
                    //    txtservices.ReadOnly = false;
                    //    txtservices.Text = "";
                    //    txtservices.Focus();
                    //    return;
                    //}
                    //else
                    //{
                    //    lblmessage.Visible = false;
                    //}
                    if (Convert.ToInt32(ServiceID.Text) == Convert.ToInt32(ID))
                    {
                        Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txtservices.ReadOnly = false;
                        txtservices.Text = "";
                        txtservices.Focus();
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
                txtservices.Text = "";
                return;
            }

            List<OPDbillingData> ServiceList = Session["ServiceList"] == null ? new List<OPDbillingData>() : (List<OPDbillingData>)Session["ServiceList"];
            OPDbillingData ObjService = new OPDbillingData();
            ObjService.ServiceCharge = Convert.ToDecimal(txtservicecharge.Text.ToString() == "" ? "0" : txtservicecharge.Text.ToString());
            ObjService.Quantity = Convert.ToInt32(txtquantity.Text == "" || txtquantity.Text == "0" ? "1" : txtquantity.Text);
            ObjService.ServiceTypeID = Convert.ToInt32(ddlservicetype.SelectedValue == "" ? "0" : ddlservicetype.SelectedValue);
            ObjService.DeptID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
            ObjService.DocID = Convert.ToInt32(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
            ObjService.SubGroupID = Convert.ToInt32(hdnsubgroupID.Value == null ? "0" : hdnsubgroupID.Value);
            ObjService.ServiceType = ddlservicetype.SelectedItem.Text;
            ObjService.Remarks = txtremarks.Text.Trim();
            ObjService.ID = Convert.ToInt32(ID);
            ObjService.IsDefaultService = 0;
            ObjService.NetServiceCharge = Convert.ToDecimal(txtservicecharge.Text.ToString() == "" ? "0" : txtservicecharge.Text.ToString()) * Convert.ToInt32(txtquantity.Text == "" || txtquantity.Text == "0" ? "1" : txtquantity.Text);
            ObjService.ServiceName = lblservicename.Text.Trim();
            ObjService.ServiceName = lblservicename.Text.Trim();
            txttotalamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txttotalamount.Text == "" ? "0" : txttotalamount.Text) + Convert.ToInt32(txtquantity.Text == "" || txtquantity.Text == "0" ? "1" : txtquantity.Text) * Convert.ToDecimal(txtservicecharge.Text.ToString() == "" ? "0" : txtservicecharge.Text.ToString())).ToString());
            if (Convert.ToDecimal(txtbalanceinac.Text) > 0)
            {
                if (Convert.ToDecimal(txtbalanceinac.Text) >= Convert.ToDecimal(txttotalamount.Text))
                {
                    txtadjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txttotalamount.Text == "" ? "0" : txttotalamount.Text)).ToString());
                    txtpaidamount.Text = "0.00";
                }
                else if (Convert.ToDecimal(txtbalanceinac.Text) < Convert.ToDecimal(txttotalamount.Text))
                {
                    txtadjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txtbalanceinac.Text == "" ? "0" : txtbalanceinac.Text)).ToString());
                    txtpaidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txttotalamount.Text == "" ? "0" : txttotalamount.Text) - Convert.ToDecimal(txtbalanceinac.Text.ToString() == "" ? "0" : txtbalanceinac.Text.ToString())).ToString());
                }
            }
            else
            {
                txtadjustedamount.Text = "0.00";
                txtpaidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txttotalamount.Text == "" ? "0" : txttotalamount.Text)).ToString());
            }
            ServiceList.Add(ObjService);
            if (ServiceList.Count > 0)
            {
                gvopservicelist.DataSource = ServiceList;
                gvopservicelist.DataBind();
                gvopservicelist.Visible = true;
                Session["ServiceList"] = ServiceList;
                txtservices.Text = "";
                txtservicecharge.Text = "";
                txtquantity.Text = "";
                txtservices.ReadOnly = false;
                hdnsubgroupID.Value = "0";
                //Commonfunction.Insertzeroitemindex(ddldoctor);
                //ddldepartment.SelectedIndex = 0;
                ddldepartment.Attributes["disabled"] = "disabled";
                ddldoctor.Attributes["disabled"] = "disabled";
                // ddl_referby.Attributes["disabled"] = "disabled";
                // ddl_source.Attributes["disabled"] = "disabled";
                btnsave.Attributes.Remove("disabled");
                txtservices.Focus();
            }
            else
            {
                btnsave.Attributes["disabled"] = "disabled";
                gvopservicelist.DataSource = null;
                gvopservicelist.DataBind();
                gvopservicelist.Visible = true;
                txtservices.ReadOnly = true;
            }
            totalCalculate();
        }
        protected void gvdepositlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }
        protected void gvopservicelist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSerial = (Label)e.Row.FindControl("lblserialID");
                lblSerial.Text = ((gvopservicelist.PageIndex * gvopservicelist.PageSize) + e.Row.RowIndex + 1).ToString();
                DropDownList ddl_discount_type = (DropDownList)e.Row.FindControl("ddl_discount_type");
                TextBox txt_dis_value = (TextBox)e.Row.FindControl("txt_dis_value");

                if (Convert.ToInt32(hdisMsbDoctor.Value == "" ? "0" : hdisMsbDoctor.Value) > 0)
                {
                    if (Convert.ToInt32(hdMsbPc.Value) > 0)
                    {
                        ddl_discount_type.Attributes["disabled"] = "disabled";
                        ddl_discount_type.SelectedIndex = 1;
                        txt_dis_value.Text = hdMsbPc.Value;
                        txt_dis_value.ReadOnly = true;

                        discountChange(sender, e.Row);
                    }
                }


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
                    gvopcollection.Columns[14].Visible = false;
                    gvopcollection.Columns[15].Visible = false;
                    gvopcollection.Columns[16].Visible = false;
                    gvopcollection.Columns[17].Visible = false;
                }
                else
                {
                    gvopcollection.Columns[14].Visible = true;
                    gvopcollection.Columns[15].Visible = true;
                    gvopcollection.Columns[16].Visible = true;
                    gvopcollection.Columns[17].Visible = true;
                }
                if (lblIsVerified.Text == "0")
                {
                    e.Row.Cells[14].Visible = false;
                    e.Row.Cells[15].Visible = false;
                    e.Row.Cells[16].Visible = false;
                    e.Row.Cells[17].ColumnSpan = 4;
                }
                else
                {
                    //e.Row.Cells[14].Controls.Clear();
                    //e.Row.Cells[15].Controls.Clear();
                    e.Row.Cells[16].ColumnSpan = 2;
                    e.Row.Cells[17].Visible = false;
                }
            }
        }
        protected void gvopservicelist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvopservicelist.Rows[i];

                    List<OPDbillingData> ItemList = Session["ServiceList"] == null ? new List<OPDbillingData>() : (List<OPDbillingData>)Session["ServiceList"];
                    if (ItemList.Count > 0)
                    {
                        Decimal totalamount = ItemList[i].ServiceCharge;
                        txttotalamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txttotalamount.Text == "" ? "0" : txttotalamount.Text) - totalamount).ToString());

                        if (Convert.ToDecimal(txtbalanceinac.Text) > 0)
                        {
                            if (Convert.ToDecimal(txtbalanceinac.Text) >= Convert.ToDecimal(txttotalamount.Text))
                            {
                                txtadjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txttotalamount.Text == "" ? "0" : txttotalamount.Text)).ToString());
                                txtpaidamount.Text = "0.00";
                            }
                            else if (Convert.ToDecimal(txtbalanceinac.Text) < Convert.ToDecimal(txttotalamount.Text))
                            {
                                txtadjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txtbalanceinac.Text == "" ? "0" : txtbalanceinac.Text)).ToString());
                                txtpaidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txttotalamount.Text == "" ? "0" : txttotalamount.Text) - Convert.ToDecimal(txtbalanceinac.Text.ToString() == "" ? "0" : txtbalanceinac.Text.ToString())).ToString());
                            }
                            else if (Convert.ToDecimal(txtbalanceinac.Text) == Convert.ToDecimal(txttotalamount.Text))
                            {
                                txtadjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txtbalanceinac.Text == "" ? "0" : txtbalanceinac.Text)).ToString());
                                txtpaidamount.Text = "0.00";
                            }
                            else if (Convert.ToDecimal(txttotalamount.Text) == 0)
                            {
                                txtpaidamount.Text = "0.00";
                            }
                        }
                        else
                        {
                            txtadjustedamount.Text = "0.00";
                            txtpaidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txttotalamount.Text == "" ? "0" : txttotalamount.Text)).ToString());
                        }
                    }
                    ItemList.RemoveAt(i);
                    Session["ServiceList"] = ItemList;
                    gvopservicelist.DataSource = ItemList;
                    gvopservicelist.DataBind();
                    txtdiscount.Text = "";
                    lblmessage.Visible = false;
                    if (ItemList.Count == 0)
                    {
                        txtservices.ReadOnly = false;
                        ddlservicetype.Attributes.Remove("disabled");
                        ddldepartment.Attributes.Remove("disabled");
                        ddldoctor.Attributes.Remove("disabled");
                        btnsave.Text = "Save";
                        btnsave.Attributes["disabled"] = "disabled";
                        txtservices.Focus();
                    }
                    else
                    {
                        btnsave.Attributes.Remove("disabled");
                    }

                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;
                return;
            }
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txtUHID.Text = "";
            hdncharges.Value = null;
            txtbillNo.Text = "";
            txtservicecharge.Text = "";
            txtdiscount.Text = "";
            ddlpaymentmode.SelectedIndex = 0;
            ddldepartment.SelectedIndex = 0;
            txtbank.Text = "";
            txttotalamount.Text = "";
            txtbank.ReadOnly = true;
            Session["ServiceList"] = null;
            Session["DiscountList"] = null;
            gvopservicelist.DataSource = null;
            gvopservicelist.DataBind();
            gvopservicelist.Visible = false;
            GVTPAList.DataSource = null;
            GVTPAList.DataBind();
            GVTPAList.Visible = true;
            lblmessage.Visible = false;
            txtservices.Text = "";
            txtquantity.Text = "";
            txtbalanceinac.Text = "";
            txtpaidamount.Text = "";
            txtpaidamount.Text = "";
            ddlpaymentmode.SelectedIndex = 1;
            div1.Visible = false;
            txtremarks.Text = "";
            ddlservicetype.SelectedIndex = 0;
            txt_chequenumber.Text = "";
            txtinvoicenumber.Text = "";
            txtbank.ReadOnly = true;
            txt_chequenumber.ReadOnly = true;
            txtinvoicenumber.ReadOnly = true;
            Commonfunction.Insertzeroitemindex(ddldoctor);
            ddldepartment.Attributes["disabled"] = "disabled";
            ddldoctor.Attributes["disabled"] = "disabled";
            btnprint.Attributes["disabled"] = "disabled";
            ddlservicetype.Attributes.Remove("disabled");
            btn_cs.Attributes["disabled"] = "disabled";
            btn_redgcard.Attributes["disabled"] = "disabled";
            txt_patientcategory.Text = "";
            txt_tpacompany.Text = "";
            txtUHID.Focus();
            hdnlastvisitdoctorID.Value = "0";
            hdnnumberdays.Value = "0";
            hdnprintID.Value = "0";
            hdnsubgroupID.Value = "0";
            hdnUHID.Value = null;
            btn_token.Visible = false;
            ddl_source.SelectedIndex = 0;
            txt_referby.Text = "";
            ddl_source.Attributes.Remove("disabled");
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
            if (ddl_source.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Source", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddl_source.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (txt_referby.Text.Trim() == "")
            {
                Messagealert_.ShowMessage(lblmessage, "PatReferBy", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_referby.Focus();
                return;
            }
            else
            {
                if (txt_referby.Text.Trim() != "" && Commonfunction.SemicolonSeparation_String_64(txt_referby.Text.ToString()) == 0)
                {
                    txt_referby.Text = "";
                    txt_referby.Focus();
                    Messagealert_.ShowMessage(lblmessage, "PatReferBy", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
            }
            if ((ddlservicetype.SelectedValue == "5" || ddlservicetype.SelectedValue == "1" || ddlservicetype.SelectedValue == "20" || ddlservicetype.SelectedValue == "21" || ddlservicetype.SelectedValue == "22") && ddldoctor.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Doctor", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtservices.Text = "";
                txtservicecharge.Text = "";
                txtservices.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (ddlpaymentmode.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Paymode", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddlpaymentmode.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (txttotalamount.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Totalamount", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddlpaymentmode.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (ddlpaymentmode.SelectedIndex > 1)
            {
                if (ddlpaymentmode.SelectedValue == "2")
                {
                    if (txtinvoicenumber.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "Invoicenumber", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txtinvoicenumber.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        div1.Visible = false;
                    }
                }
                if (ddlpaymentmode.SelectedValue == "3")
                {
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
                if (ddlpaymentmode.SelectedValue == "4")
                {
                    if (txtbank.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "BankName", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txtbank.Focus();
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
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (Convert.ToDecimal(txtdiscount.Text == "" ? "0" : txtdiscount.Text) > 0)
            {
                if (txtdiscremoarks.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Enter discount remarks.", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txtdiscremoarks.Focus();
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

            List<OPDbillingData> Listbill = new List<OPDbillingData>();
            List<Discount> ListDiscount = new List<Discount>();
            OPDbillingBO objbillingBO = new OPDbillingBO();
            OPDbillingData opdbill = new OPDbillingData();
            DepositBO objstdBO = new DepositBO();
            // int index = 0;
            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in gvopservicelist.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label Particulars = (Label)gvopservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblparticulars");
                    Label amount = (Label)gvopservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblamount");
                    Label qty = (Label)gvopservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblquantity");
                    Label NetCharge = (Label)gvopservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblnetcharges");
                    Label SerialID = (Label)gvopservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblserialID");
                    Label ID = (Label)gvopservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label ServicetypeID = (Label)gvopservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblservicetypeID");
                    Label SubgroupID = (Label)gvopservicelist.Rows[row.RowIndex].Cells[0].FindControl("lbl_subgroupID");
                    Label DoctorType = (Label)gvopservicelist.Rows[row.RowIndex].Cells[0].FindControl("lbldoctortype");
                    Label DepartmentType = (Label)gvopservicelist.Rows[row.RowIndex].Cells[0].FindControl("lbldeprtmentID");
                    Label Doctor = (Label)gvopservicelist.Rows[row.RowIndex].Cells[0].FindControl("lbldoctorID");
                    Label Remarks = (Label)gvopservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblremarks");

                    DropDownList ddl_discount_type = (DropDownList)gvopservicelist.Rows[row.RowIndex].Cells[0].FindControl("ddl_discount_type");
                    TextBox txt_dis_value = (TextBox)gvopservicelist.Rows[row.RowIndex].Cells[0].FindControl("txt_dis_value");
                    Label lbl_discount_amt = (Label)gvopservicelist.Rows[row.RowIndex].Cells[0].FindControl("lbl_discount_amt");

                    OPDbillingData ObjDetails = new OPDbillingData();

                    ObjDetails.ServiceName = Particulars.Text == "" ? null : Particulars.Text;
                    ObjDetails.Remarks = Remarks.Text == "" ? "null" : Remarks.Text;
                    ObjDetails.ServiceCharge = Convert.ToDecimal(amount.Text == "" ? "0" : amount.Text);
                    ObjDetails.Quantity = Convert.ToInt32(qty.Text == "" ? "0" : qty.Text);
                    ObjDetails.NetServiceCharge = Convert.ToDecimal(NetCharge.Text == "" ? "0" : NetCharge.Text);
                    ObjDetails.SerialID = Convert.ToInt32(SerialID.Text == "" ? "0" : SerialID.Text);
                    ObjDetails.ServiceTypeID = Convert.ToInt32(ServicetypeID.Text == "" ? "0" : ServicetypeID.Text);
                    ObjDetails.SubGroupID = Convert.ToInt32(SubgroupID.Text == "" ? "0" : SubgroupID.Text);
                    ObjDetails.DoctorTypeID = Convert.ToInt32(DoctorType.Text == "" ? "0" : DoctorType.Text);
                    ObjDetails.DeptID = Convert.ToInt32(DepartmentType.Text == "" ? "0" : DepartmentType.Text);
                    ObjDetails.DocID = Convert.ToInt32(Doctor.Text == "" ? "0" : Doctor.Text);
                    ObjDetails.ServiceID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    if (Convert.ToInt32(ServicetypeID.Text == "" ? "0" : ServicetypeID.Text) == 1)
                    {
                        hdnDepartmentID.Value = (DepartmentType.Text == "" ? "0" : DepartmentType.Text).ToString();
                    }
                    ObjDetails.DisType = Convert.ToInt32(ddl_discount_type.SelectedValue == "" ? "0" : ddl_discount_type.SelectedValue);
                    ObjDetails.isDis = Convert.ToInt32(Convert.ToDecimal(lbl_discount_amt.Text.Trim() == "" ? "0" : lbl_discount_amt.Text.Trim()) == 0 ? 0 : 1);

                    ObjDetails.DisValue = Convert.ToDecimal(txt_dis_value.Text == "" ? "0" : txt_dis_value.Text);
                    ObjDetails.DisAmount = Convert.ToDecimal(lbl_discount_amt.Text == "" ? "0" : lbl_discount_amt.Text);

                    ObjDetails.isMsbDoctor = Convert.ToInt32(hdisMsbDoctor.Value == "" ? "0" : hdisMsbDoctor.Value);
                    ObjDetails.isMsbPatient = Convert.ToInt32(hdMsbPc.Value == "" ? "0" : hdMsbPc.Value) > 0 ? 1 : 0;
                    ObjDetails.MsbPc = Convert.ToInt32(hdisMsbDoctor.Value == "" ? "0" : hdisMsbDoctor.Value) == 1 ? Convert.ToInt32(hdMsbPc.Value == "" ? "0" : hdMsbPc.Value) : 0;
                    Listbill.Add(ObjDetails);
                }
                decimal dis = Convert.ToDecimal(txtdiscount.Text.Trim() == "" ? "0" : txtdiscount.Text.Trim());
                opdbill.XMLData = XmlConvertor.OpdBillDatatoXML(Listbill).ToString();
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
                opdbill.extraDiscountXML = XmlConvertor.ExtraDiscountDatatoXML(ListDiscount).ToString();
                opdbill.TotalBillAmount = Convert.ToDecimal(txttotalamount.Text == "" ? "0" : txttotalamount.Text);
                opdbill.UHID = Convert.ToInt64(txtUHID.Text.Contains(":") ? txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1) : "0");
                hdnUHID.Value = txtUHID.Text.Contains(":") ? txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1) : "0";
                opdbill.AdjustedAmount = Convert.ToDecimal(txtadjustedamount.Text == "" ? "0" : txtadjustedamount.Text);
                opdbill.DiscountedAmount = Convert.ToDecimal(txtdiscount.Text == "" ? "0" : txtdiscount.Text);
                opdbill.isDis = dis > 0 ? 1 : 0;
                if (flag == 1)
                {
                    opdbill.isDis = 0;
                }
                opdbill.isExtraDiscount = flag;
                opdbill.patientType = Convert.ToInt32(hdisMsbDoctor.Value) == 0 ? 1 : Convert.ToInt32(hdPatCat.Value);
                opdbill.ID = 0;
                opdbill.Remarks = txtdiscremoarks.Text.Trim();
                opdbill.PaidAmount = Convert.ToDecimal(txtpaidamount.Text == "" ? "0" : txtpaidamount.Text);
                opdbill.PaymentMode = Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue);
                opdbill.SourceID = Convert.ToInt32(ddl_source.SelectedValue == "" ? "0" : ddl_source.SelectedValue);
                opdbill.ReferalID = Commonfunction.SemicolonSeparation_String_64(txt_referby.Text.Trim());
                opdbill.ReferalName = txt_referby.Text.Trim();
                opdbill.BankName = txtbank.Text == "" ? null : txtbank.Text;
                opdbill.ChequeUTRnumber = txt_chequenumber.Text == "" ? null : txt_chequenumber.Text;
                opdbill.InvoiceNumber = txtinvoicenumber.Text == "" ? null : txtinvoicenumber.Text;
                opdbill.FinancialYearID = LogData.FinancialYearID;
                opdbill.EmployeeID = LogData.EmployeeID;
                opdbill.AddedBy = LogData.AddedBy;
                opdbill.HospitalID = LogData.HospitalID;
                opdbill.IsActive = LogData.IsActive;
                opdbill.IPaddress = LogData.IPaddress;
                opdbill.ActionType = Enumaction.Insert;
                opdbill.IsVerified = LogData.BillSetting == 0 ? 1 : 0;
                opdbill.BarcodeImage = Commonfunction.getBarcodeImage(txtUHID.Text.Contains(":") ? txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1) : "0");
                //QR image generation//
                List<PatientQRdata> listQr = objbillingBO.GetPatientQRData(opdbill.UHID);
                PatientQRdata qrData = new PatientQRdata();
                qrData = listQr[0];
                string qrxml = XmlConvertor.PatientQRDataXML(qrData);
                opdbill.QRImage = Commonfunction.getQRImage(qrxml);

                List<OPDbillingData> result = objbillingBO.UpdateOPDBill(opdbill);
                if (result.Count > 0)
                {
                    if (result[0].isDis == 0)
                    {
                        Session["ServiceList"] = null;
                        Session["DiscountList"] = null;
                        GVTPAList.DataSource = null;
                        GVTPAList.DataBind();
                        GVTPAList.Visible = true;
                        txtbillNo.Text = result[0].BillNo.ToString();
                        txtUHID.Text = "";
                        btnsave.Attributes["disabled"] = "disabled";
                        ddldepartment.Attributes["disabled"] = "disabled";
                        ddldoctor.Attributes["disabled"] = "disabled";

                        if (LogData.PrintEnable == 0)
                        {
                            btnprint.Attributes["disabled"] = "disabled";
                            btn_redgcard.Attributes["disabled"] = "disabled";
                            btn_cs.Attributes["disabled"] = "disabled";
                        }
                        else
                        {
                            if (LogData.BillSetting == 0)
                            {
                                btnprint.Attributes.Remove("disabled");
                                if (result[0].ServiceTypeID == 1 || result[0].ServiceTypeID == 20)
                                {
                                    hdnDepartmentID.Value = result[0].DepartmentID.ToString();
                                    btn_cs.Attributes.Remove("disabled");
                                }
                                else
                                {
                                    btn_cs.Attributes["disabled"] = "disabled";
                                    hdnDepartmentID.Value = "0";
                                }
                                //if (result[0].CardID == 247)
                                //{
                                //    btn_redgcard.Attributes.Remove("disabled");
                                //}
                                //else
                                //{
                                //    btn_redgcard.Attributes["disabled"] = "disabled";
                                //}
                            }
                            else
                            {
                                btn_redgcard.Visible = false;
                                btn_token.Visible = true;
                                ddlpaymentmode.Attributes["disabled"] = "disabled";
                                txt_chequenumber.ReadOnly = true;
                                txtinvoicenumber.ReadOnly = true;

                            }
                        }

                        hdnlastvisitdoctorID.Value = "0";
                        hdnnumberdays.Value = "0";
                        hdnprintID.Value = "0";
                        hdnsubgroupID.Value = "0";

                        Messagealert_.ShowMessage(lblmessage, "save", 1);
                        div1.Visible = true;
                        div1.Attributes["class"] = "SucessAlert";
                        return;
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "Discount Request sent! " + "[Request No:" + result[0].ID + "]", 1);
                        Session["LabServiceList"] = null;
                        Session["DiscountList"] = null;
                        div1.Visible = true;
                        btnsave.Attributes["disabled"] = "disabled";
                        div1.Attributes["class"] = "SucessAlert";
                        btnprint.Attributes["disabled"] = "disabled";
                        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>pushMessage('" + result[0].Remarks + "','" + result[0].ID + "');</script>", false);
                    }
                }
                else
                {
                    btnsave.Attributes.Remove("disabled");
                    txtbillNo.Text = "";
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
        }
        private void GenerateBacode(string _data, string _filename)
        {

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetAutoUHID(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetDetailUHID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
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
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid(1);
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
                List<OPDbillingData> objdeposit = GetOPBillList(page);
                if (objdeposit.Count > 0)
                {
                    gvopcollection.VirtualItemCount = objdeposit[0].MaximumRows;//total item is required for custom paging
                    gvopcollection.PageIndex = page - 1;

                    gvopcollection.DataSource = objdeposit;
                    gvopcollection.DataBind();
                    gvopcollection.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    txttotalbill.Text = Commonfunction.Getrounding(objdeposit[0].TotalBill.ToString());
                    txtajusted.Text = Commonfunction.Getrounding(objdeposit[0].TotalAdjustedAmount.ToString());
                    txttotaldiscounted.Text = Commonfunction.Getrounding(objdeposit[0].TotalDiscountAmount.ToString());
                    txttotalpaid.Text = Commonfunction.Getrounding(objdeposit[0].TotalPaidAmount.ToString());
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    divmsg2.Visible = false;
                    if (LogData.PrintEnable == 0)
                    {
                        btnprints.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btnprints.Attributes.Remove("disabled");
                    }
                }
                else
                {
                    gvopcollection.DataSource = null;
                    gvopcollection.DataBind();
                    gvopcollection.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    txttotalbill.Text = "0.00";
                    txtajusted.Text = "0.00";
                    txttotaldiscounted.Text = "0.00";
                    txttotalpaid.Text = "0.00";
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
            }
        }
        public List<OPDbillingData> GetOPBillList(int curIndex)
        {
            OPDbillingData objpat = new OPDbillingData();
            OPDbillingBO objbillingBO = new OPDbillingBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            //objpat.UHID = Convert.ToInt64(txtautoUHID.Text == "" ? "0" : txtautoUHID.Text);
            //objpat.PatientName = txtpatientNames.Text == "" ? null : txtpatientNames.Text.Trim();

            string UHID;
            string PatName;
            var source = txtpatientNames.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                //int indexStop = source.LastIndexOf('/');
                //PatName = source.Substring(0, indexStop);
                objpat.UHID = Convert.ToInt64(ID);
                //objpat.PatientName = PatName;
            }
            else
            {
                objpat.PatientName = txtpatientNames.Text.Trim().ToString();
                objpat.UHID = 0;
            }

            objpat.Paymode = Convert.ToInt32(ddlpaymentmodes.SelectedValue == "" ? "0" : ddlpaymentmodes.SelectedValue);
            objpat.CollectedByID = Convert.ToInt64(ddlcollectedby.SelectedValue == "" ? "0" : ddlcollectedby.SelectedValue);
            string datefrom = from.ToString("yyyy-MM-dd");
            string timefrom = txttimepickerfrom.Text.Trim();
            from = Convert.ToDateTime(datefrom + " " + timefrom);
            objpat.DateFrom = from;
            string dateto = To.ToString("yyyy-MM-dd");
            string timeto = txttimepickerto.Text.Trim();
            To = Convert.ToDateTime(dateto + " " + timeto);
            objpat.DateTo = To;
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objpat.CurrentIndex = curIndex;
            objpat.AmountEnable = LogData.AmountEnable;
            return objbillingBO.GetOPBillList(objpat);
        }
        public List<OPDbillingData> GetOPPatientBillList(int curIndex)
        {
            OPDbillingData objpat = new OPDbillingData();
            OPDbillingBO objbillingBO = new OPDbillingBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.UHID = Convert.ToInt64(txtautoUHID.Text == "" ? "0" : txtautoUHID.Text);
            objpat.PatientName = txtpatientNames.Text == "" ? null : txtpatientNames.Text.Trim();
            objpat.Paymode = Convert.ToInt32(ddlpaymentmodes.SelectedValue == "" ? "0" : ddlpaymentmodes.SelectedValue);
            objpat.CollectedByID = Convert.ToInt64(ddlcollectedby.SelectedValue == "" ? "0" : ddlcollectedby.SelectedValue);
            string datefrom = from.ToString("yyyy-MM-dd");
            string timefrom = txttimepickerfrom.Text.Trim();
            from = Convert.ToDateTime(datefrom + " " + timefrom);
            objpat.DateFrom = from;
            string dateto = To.ToString("yyyy-MM-dd");
            string timeto = txttimepickerto.Text.Trim();
            To = Convert.ToDateTime(dateto + " " + timeto);
            objpat.DateTo = To;
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objpat.AmountEnable = LogData.AmountEnable;
            return objbillingBO.GetOPPatientBillList(objpat);
        }

        protected void btnresets_Click(object sender, EventArgs e)
        {

            ddlpaymentmodes.SelectedIndex = 0;
            txtautoUHID.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            ddlstatus.SelectedIndex = 0;
            gvopcollection.DataSource = null;
            gvopcollection.DataBind();
            gvopcollection.Visible = false;
            GVTPAList.DataSource = null;
            GVTPAList.DataBind();
            GVTPAList.Visible = true;
            lblresult.Visible = false;
            txtpatientNames.Text = "";
            ddlpaymentmode.SelectedIndex = 0;
            txtdatefrom.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage2.Visible = false;
            lblresult.Visible = false;
            divmsg2.Visible = false;
            divmsg3.Visible = false;
            txttotalbill.Text = "0.00";
            txtajusted.Text = "0.00";
            txttotaldiscounted.Text = "0.00";
            txttotalpaid.Text = "0.00";
            if (LogData.RoleID == 1)
            {
                ddlcollectedby.SelectedIndex = 0;
            }
            else
            {
                ddlcollectedby.SelectedIndex = 0;
            }
            btnprints.Attributes["disabled"] = "disabled";
            btn_token.Visible = false;
        }
        protected void gvdepositlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "DeleteEnable", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage2.Visible = false;
                    }
                    OPDbillingData objbill = new OPDbillingData();
                    OPDbillingBO objstdBO = new OPDbillingBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvopcollection.Rows[i];
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
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objbill.Remarks = txtremarks.Text;
                    }
                    objbill.BillNo = ID.Text.Trim();
                    objbill.UHID = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                    objbill.EmployeeID = LogData.UserLoginId;
                    objbill.FinancialYearID = LogData.FinancialYearID;
                    objbill.IPaddress = LogData.IPaddress;
                    objbill.HospitalID = LogData.HospitalID;
                    objbill.Amount = Convert.ToDecimal(amount.Text == "" ? "0" : amount.Text);
                    objbill.AdjustedAmount = Convert.ToDecimal(adjustedamount.Text == "" ? "0" : adjustedamount.Text);
                    int Result = objstdBO.DeleteOPDBillByID(objbill);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "SucessAlert";
                        bindgrid(1);
                    }
                    else
                    {
                        if (Result == 2)
                        {
                            Messagealert_.ShowMessage(lblmessage2, "AccountClosed", 0);
                            divmsg2.Visible = true;
                            divmsg2.Attributes["class"] = "FailAlert";
                            // bindgrid();
                        }
                        else
                        {
                            Messagealert_.ShowMessage(lblmessage2, "system", 0);
                            divmsg2.Attributes["class"] = "FailAlert";
                            divmsg2.Visible = true;
                        }
                    }

                }
                if (e.CommandName == "Print")
                {
                    //if (LogData.RoleID > 1)
                    //{
                    //    Messagealert_.ShowMessage(lblmessage2, "PrintEnable", 0);
                    //    divmsg2.Visible = true;
                    //    divmsg2.Attributes["class"] = "FailAlert";
                    //    return;
                    //}
                    //else
                    //{
                    //    lblmessage2.Visible = false;
                    //}
                    int j = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gv = gvopcollection.Rows[j];
                    Label regd = (Label)gv.Cells[0].FindControl("lbluhid");
                    string url = "../Reports/ReportViewer.aspx?option=RegdCard&UHID=" + regd.Text.Trim();
                    string fullURL = "window.open('" + url + "', '_blank');";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                }
                if (e.CommandName == "Token")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvopcollection.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    string code = Commonfunction.getBarcode(ID.Text.ToString());
                    String barcode = " <tr><td align=\"center\"><label style=\"font-size: 9px; text - align: left;\" >OPD Consultation</label><br><img style=\"width:60%\" src=\"" + code + "\"/> " +
                        "</td><td align=\"center\"><label style=\"font-size: 9px; text - align: left;\" >OPD Consultation</label><br><label style=\"font-size: 9px;\">" + ID.Text + "</label></td></tr>" +
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
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
                return;
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<OPDbillingData> DepositDetails = GetOPPatientBillList(0);
            List<OPDbillingDataTOeXCEL> ListexcelData = new List<OPDbillingDataTOeXCEL>();
            int i = 0;
            foreach (OPDbillingData row in DepositDetails)
            {
                OPDbillingDataTOeXCEL Ecxeclpat = new OPDbillingDataTOeXCEL();
                Ecxeclpat.UHID = DepositDetails[i].UHID;
                Ecxeclpat.PatientName = DepositDetails[i].PatientName;
                Ecxeclpat.Address = DepositDetails[i].Address;
                Ecxeclpat.DocName = DepositDetails[i].DocName;
                Ecxeclpat.BillAmount = DepositDetails[i].TotalBillAmount;
                Ecxeclpat.TotalAdjustedAmount = DepositDetails[i].TotalAdjustedAmount;
                Ecxeclpat.TotalDiscountedAmount = DepositDetails[i].TotalDiscountedAmount;
                Ecxeclpat.TotalPaidAmount = DepositDetails[i].TotalPaidAmount;
                Ecxeclpat.AddedBy = DepositDetails[i].EmpName;
                ListexcelData.Add(Ecxeclpat);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
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
                divmsg3.Attributes["class"] = "FailAlert";
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
                    gvopcollection.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvopcollection.Columns[11].Visible = false;
                    gvopcollection.Columns[12].Visible = false;
                    gvopcollection.Columns[13].Visible = false;
                    gvopcollection.Columns[14].Visible = false;
                    gvopcollection.RenderControl(hw);
                    gvopcollection.HeaderRow.Style.Add("width", "15%");
                    gvopcollection.HeaderRow.Style.Add("font-size", "10px");
                    gvopcollection.Style.Add("text-decoration", "none");
                    gvopcollection.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvopcollection.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=OPDCollectionDetails.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
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
                wb.Worksheets.Add(dt, "OPDCollectionDetails Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=OPDCollectionDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblresult, "Exported", 1);
                divmsg3.Attributes["class"] = "SucessAlert";
            }
        }
        //protected void txtautoUHID_TextChanged(object sender, EventArgs e)
        //{
        //    //PatientData Objpaic = new PatientData();
        //    //RegistrationBO objInfoBO = new RegistrationBO();
        //    //List<PatientData> getResult = new List<PatientData>();
        //    //bool isnumeric = txtUHID.Text.All(char.IsDigit);
        //    //if (isnumeric == false)
        //    //{
        //    //    if (txtUHID.Text.Contains(":"))
        //    //    {
        //    //        Objpaic.UHID = Convert.ToInt64(txtUHID.Text.Contains(":") ? txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1) : "0");
        //    //    }
        //    //    else
        //    //    {
        //    //        txtautoUHID.Text = "";
        //    //        txtautoUHID.Focus();
        //    //        return;
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    Objpaic.UHID = Convert.ToInt64(txtUHID.Text == "" ? "0" : txtUHID.Text);
        //    //}
        //    //getResult = objInfoBO.GetPatientDetailsByUHID(Objpaic);
        //    //if (getResult.Count > 0)
        //    //{

        //    //    Session["ServiceList"] = null;
        //    //    Session["DiscountList"] = null;
        //    //}
        //    //else
        //    //{
        //    //    txtpatientNames.Text = "";
        //    //    txtautoUHID.Text = "";
        //    //    txt_patientcategory.Text = "";
        //    //    txt_tpacompany.Text = "";
        //    //    txtUHID.Focus();
        //    //}
        //    if (txtautoUHID.Text != "")
        //    {
        //        bindgrid(1);
        //    }
        //}
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            if (txtpatientNames.Text != "")
            {
                bindgrid(1);
            }
        }
        protected void ddl_source_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender5.ContextKey = ddl_source.SelectedValue;

            if (ddl_source.SelectedIndex > 0)
            {
                if (ddl_source.SelectedIndex == 1)
                {
                    txt_referby.Text = "Self:1";
                    txt_referby.Enabled = false;
                }
                else
                {
                    txt_referby.Text = "";
                    txt_referby.Enabled = true;
                }
            }
            else
            {
                txt_referby.Text = "";
                txt_referby.Enabled = false;
            }
        }
        protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearOPvisit();
            if (ddldepartment.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddldoctor, mstlookup.GetDepartmentDoctor(Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue)));
            }
            else
            {
                if (ddlservicetype.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "ServiceType", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    div1.Visible = false;
                    lblmessage.Visible = false;
                }
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
                    txt_dis_value.Text = "";
                    lbl_discount_amt.Text = "";
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
            foreach (GridViewRow row in gvopservicelist.Rows)
            {
                Label lbl_discount_amt = (Label)gvopservicelist.Rows[row.RowIndex].Cells[0].FindControl("lbl_discount_amt");
                totalDiscount = totalDiscount + (Convert.ToDecimal(lbl_discount_amt.Text == "" ? "0" : lbl_discount_amt.Text));
            }
            decimal TotalAmount = Convert.ToDecimal(txttotalamount.Text == "" ? "0" : txttotalamount.Text.ToString());
            txtdiscount.Text = Commonfunction.Getrounding(totalDiscount.ToString());
            txtpaidamount.Text = Commonfunction.Getrounding((TotalAmount - Convert.ToDecimal(txtdiscount.Text == "" ? "0" : txtdiscount.Text)).ToString());
            if (totalDiscount > 0)
            {
                txtdiscount.ReadOnly = true;
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
        protected void ddldoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldoctor.SelectedIndex > 0)
            {
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
                if (ddl_source.SelectedIndex == 0)
                {
                    ddldoctor.SelectedIndex = 0;
                    Messagealert_.ShowMessage(lblmessage, "Source", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_source.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (txt_referby.Text.Trim() == "")
                {
                    ddldoctor.SelectedIndex = 0;
                    Messagealert_.ShowMessage(lblmessage, "PatReferBy", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_referby.Focus();
                    return;
                }
                else
                {
                    if (txt_referby.Text.Trim() != "" && Commonfunction.SemicolonSeparation_String_64(txt_referby.Text.ToString()) == 0)
                    {
                        ddldoctor.SelectedIndex = 0;
                        txt_referby.Text = "";
                        txt_referby.Focus();
                        Messagealert_.ShowMessage(lblmessage, "PatReferBy", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
                AutoCompleteExtender2.CompletionSetCount = Convert.ToInt32(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
                Int64 DoctorID = Convert.ToInt64(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
                int DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
                int Result = Commonfunction.CheckDoctorAvail(DepartmentID, DoctorID);
                int excludeMsb = Commonfunction.CheckMSBDoctor(DoctorID);
                hdisMsbDoctor.Value = excludeMsb == 1 ? "0" : "1";
                if (Result == 0)
                {
                    Session["ServiceList"] = null;
                    gvopservicelist.DataSource = null;
                    gvopservicelist.DataBind();

                    txtservices.ReadOnly = true;
                    btnadd.Attributes["disabled"] = "disabled";
                    Messagealert_.ShowMessage(lblmessage, "Dr. " + ddldoctor.SelectedItem.Text + " is not available now.", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    btnadd.Attributes.Remove("disabled");
                    if (txtUHID.Text != "" && (ddlservicetype.SelectedValue == "1" || ddlservicetype.SelectedValue == "20" || ddlservicetype.SelectedValue == "21" || ddlservicetype.SelectedValue == "22") && Session["EMG_UHID"] == null)
                    {

                        PatientData objpat = new PatientData();
                        RegistrationBO regdBO = new RegistrationBO();
                        objpat.UHID = Convert.ToInt64(txtUHID.Text.Contains(":") ? txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1) : "0");
                        objpat.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
                        objpat.DoctorID = Convert.ToInt64(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
                        List<PatientData> visits = regdBO.Getpatientlastvisitdetails(objpat);

                        if (visits.Count > 0 && visits[0].NoDays >= 0)
                        {
                            hdnnumberdays.Value = visits[0].NoDays.ToString();
                            hdnlastvisitdoctorID.Value = visits[0].LastVisitDoctorID.ToString();
                            OPservics(Convert.ToInt32(hdnnumberdays.Value == null ? "0" : hdnnumberdays.Value));
                            Messagealert_.ShowMessage(lblmessage, "Last first visit date :" + visits[0].LastVisitDate.ToString("dd/MM/yyyy") + " | Last Visit Department : " + visits[0].DepartmentName + " | Last Visit Doctor : " + visits[0].DoctorName + ", " + visits[0].NoDays + " day(s) ago. Please take service charges accordingly.", 1);
                            div1.Visible = true;
                            div1.Attributes["class"] = "SucessAlert";
                            OPservics(Convert.ToInt32(hdnnumberdays.Value == null ? "0" : hdnnumberdays.Value));
                            return;
                        }
                        else
                        {
                            hdnnumberdays.Value = "0";
                            hdnlastvisitdoctorID.Value = "0";
                            OPservics(Convert.ToInt32(hdnnumberdays.Value == null ? "0" : hdnnumberdays.Value));
                            Messagealert_.ShowMessage(lblmessage, "Today is the first visit for this doctor. Please take charge accordingly.", 1);
                            div1.Visible = true;
                            OPservics(Convert.ToInt32(hdnnumberdays.Value == null ? "0" : hdnnumberdays.Value));
                            div1.Attributes["class"] = "SucessAlert";
                            return;
                        }

                    }
                    else
                    {
                        Session["EMG_UHID"] = null;
                    }
                    txtservices.ReadOnly = false;
                    div1.Visible = false;
                    lblmessage.Visible = false;
                }
            }
            else
            {
                lblmessage.Visible = false;
                txtservices.Text = "";
                gvopservicelist.DataSource = null;
                gvopservicelist.DataBind();
            }

        }
        protected void OPservics(int Day)
        {
            if (ddlservicetype.SelectedValue == "1" || ddlservicetype.SelectedValue == "20" || ddlservicetype.SelectedValue == "21" || ddlservicetype.SelectedValue == "22")
            {
                Session["ServiceList"] = null;
                Session["DiscountList"] = null;
                txttotalamount.Text = "0.00";
                txtpaidamount.Text = "0.00";
                txtadjustedamount.Text = "0.00";
                List<OPDbillingData> ServiceList = Session["ServiceList"] == null ? new List<OPDbillingData>() : (List<OPDbillingData>)Session["ServiceList"];
                OPDbillingData ObjService = new OPDbillingData();
                OPDbillingBO objstdBO = new OPDbillingBO();
                ObjService.DocID = Convert.ToInt64(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
                ObjService.DeptID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
                ObjService.ServiceTypeID = Convert.ToInt32(ddlservicetype.SelectedValue == "" ? "0" : ddlservicetype.SelectedValue);
                ObjService.NoDays = Day;
                ObjService.UHID = Convert.ToInt64(hdnUHID.Value == "" ? "0" : hdnUHID.Value);
                ObjService.PrintregdCard = Convert.ToInt32(hdnprintID.Value == "" ? "0" : hdnprintID.Value);
                List<OPDbillingData> result = objstdBO.GetOpservicesbypatientvisitcount(ObjService);
                if (result.Count > 0)
                {
                    for (int i = 0; i <= result.Count - 1; i++)
                    {
                        OPDbillingData ObjServices = new OPDbillingData();
                        ObjServices.ServiceCharge = Convert.ToDecimal(result[i].ServiceCharge.ToString() == "" ? "0" : result[i].ServiceCharge.ToString());
                        ObjServices.Quantity = 1;
                        ObjServices.ServiceTypeID = Convert.ToInt32(result[i].ServiceTypeID.ToString() == "" ? "0" : result[i].ServiceTypeID.ToString());
                        ObjServices.DeptID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
                        ObjServices.DocID = Convert.ToInt32(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
                        ObjServices.ServiceType = result[i].ServiceType.ToString();
                        ObjServices.SubGroupID = Convert.ToInt32(result[i].SubGroupID.ToString() == "" ? "0" : result[i].SubGroupID.ToString());
                        ObjServices.Remarks = txtremarks.Text.Trim();
                        ObjServices.ID = Convert.ToInt32(result[i].ID);
                        ObjServices.IsDefaultService = 1;
                        ObjServices.NetServiceCharge = Convert.ToDecimal(result[i].ServiceCharge.ToString() == "" ? "0" : result[i].ServiceCharge.ToString()) * 1;
                        ObjServices.ServiceName = result[i].ServiceName;
                        txttotalamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txttotalamount.Text == "" ? "0" : txttotalamount.Text) + 1 * Convert.ToDecimal(result[i].ServiceCharge.ToString().ToString() == "" ? "0" : result[i].ServiceCharge.ToString().ToString())).ToString());
                        if (Convert.ToDecimal(txtbalanceinac.Text == "" ? "0" : txtbalanceinac.Text) > 0)
                        {
                            if (Convert.ToDecimal(txtbalanceinac.Text) >= Convert.ToDecimal(txttotalamount.Text))
                            {
                                txtadjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txttotalamount.Text == "" ? "0" : txttotalamount.Text)).ToString());
                                txtpaidamount.Text = "0.00";
                            }
                            else if (Convert.ToDecimal(txtbalanceinac.Text) < Convert.ToDecimal(txttotalamount.Text))
                            {
                                txtadjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txtbalanceinac.Text == "" ? "0" : txtbalanceinac.Text)).ToString());
                                txtpaidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txttotalamount.Text == "" ? "0" : txttotalamount.Text) - Convert.ToDecimal(txtbalanceinac.Text.ToString() == "" ? "0" : txtbalanceinac.Text.ToString())).ToString());
                            }
                        }
                        else
                        {
                            txtadjustedamount.Text = "0.00";
                            txtpaidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txttotalamount.Text == "" ? "0" : txttotalamount.Text)).ToString());
                        }
                        ServiceList.Add(ObjServices);
                    }
                }
                if (ServiceList.Count > 0)
                {
                    gvopservicelist.DataSource = ServiceList;
                    gvopservicelist.DataBind();
                    gvopservicelist.Visible = true;
                    // ddl_referby.Attributes["disabled"] = "disabled";
                    //ddl_source.Attributes["disabled"] = "disabled";
                    Session["ServiceList"] = ServiceList;
                    txtservices.Text = "";
                    txtservicecharge.Text = "";
                    txtquantity.Text = "";
                    btnsave.Attributes.Remove("disabled");
                    totalCalculate();
                }
                else
                {
                    btnsave.Attributes["disabled"] = "disabled";
                    gvopservicelist.DataSource = null;
                    gvopservicelist.DataBind();
                    gvopservicelist.Visible = true;
                    txtservices.ReadOnly = true;
                }
            }
        }
        private void clearOPvisit()
        {
            if (ddlservicetype.SelectedValue == "1")
            {
                Session["ServiceList"] = null;
                Session["DiscountList"] = null;
                txttotalamount.Text = "0.00";
                txtpaidamount.Text = "0.00";
                txtadjustedamount.Text = "0.00";
                gvopservicelist.DataSource = null;
                gvopservicelist.DataBind();
                gvopservicelist.Visible = true;
            }
        }
        protected void txtservices_TextChanged(object sender, EventArgs e)
        {
            if ((ddlservicetype.SelectedValue == "5" || ddlservicetype.SelectedValue == "1") && ddldoctor.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Doctor", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtservices.Text = "";
                txtservicecharge.Text = "";
                txtservices.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            var source = txtservices.Text.ToString();
            if (source.Contains(":"))
            {
                string ID = source.Substring(source.LastIndexOf(':') + 1);
                OPDbillingBO ObjbillBO = new OPDbillingBO();
                OPDbillingData ObjBillData = new OPDbillingData();
                ObjBillData.ID = Convert.ToInt32(ID == "" ? "0" : ID);
                ObjBillData.ServiceTypeID = Convert.ToInt32(ddlservicetype.SelectedValue == "" ? "0" : ddlservicetype.SelectedValue);
                ObjBillData.DocID = Convert.ToInt32(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
                if ((Convert.ToInt32(ID == "" ? "0" : ID) == 898 || Convert.ToInt32(ID == "" ? "0" : ID) == 897) && ddlservicetype.SelectedValue == "1")
                {
                    clearOPvisit();
                    txtservices.ReadOnly = true;
                }
                else
                {
                    txtservices.ReadOnly = true;
                }
                List<OPDbillingData> result = ObjbillBO.GetServiceChargeByID(ObjBillData);
                if (result.Count > 0)
                {
                    txtservicecharge.Text = Commonfunction.Getrounding(result[0].ServiceCharge.ToString());
                    lblservicename.Text = result[0].ServiceName.ToString();
                    txtquantity.Text = "1";
                    hdncharges.Value = Commonfunction.Getrounding(result[0].ServiceCharge.ToString());
                    hdnsubgroupID.Value = Commonfunction.Getrounding(result[0].SubGroupID.ToString());
                    btnadd.Focus();
                    txtservices.ReadOnly = true;
                }
                else
                {
                    txtservicecharge.Text = "0.0";
                    txtquantity.Text = "0";
                    txtservices.Text = "";
                    txtservices.ReadOnly = true;
                    hdncharges.Value = null;
                    hdnsubgroupID.Value = null;
                }
            }
            else
            {
                txtservicecharge.Text = "0.0";
                txtquantity.Text = "0";
                txtservices.Text = "";
                txtservices.Focus();
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
            if (txtUHID.Text == "")
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
            if (ddl_source.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Source", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddl_source.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (txt_referby.Text.Trim() == "")
            {
                Messagealert_.ShowMessage(lblmessage, "PatReferBy", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_referby.Focus();
                return;
            }
            else
            {
                if (txt_referby.Text.Trim() != "" && Commonfunction.SemicolonSeparation_String_64(txt_referby.Text.ToString()) == 0)
                {
                    txt_referby.Text = "";
                    txt_referby.Focus();
                    Messagealert_.ShowMessage(lblmessage, "PatReferBy", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
            }
            if (ddlpaymentmode.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Paymode", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddlpaymentmode.Focus();
                return;
            }
            if (txttotalamount.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Totalamount", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddlpaymentmode.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (ddlpaymentmode.SelectedIndex > 1)
            {
                if (ddlpaymentmode.SelectedValue == "2")
                {
                    if (txtinvoicenumber.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "Invoicenumber", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txtinvoicenumber.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        div1.Visible = false;
                    }
                }
                if (ddlpaymentmode.SelectedValue == "3")
                {
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
                if (ddlpaymentmode.SelectedValue == "4")
                {
                    if (txtbank.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "BankName", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txtbank.Focus();
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
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (Convert.ToDecimal(txtdiscount.Text == "" ? "0" : txtdiscount.Text) > 0)
            {
                if (txtdiscremoarks.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Enter discount remarks.", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txtdiscremoarks.Focus();
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

            List<OPDbillingData> Listbill = new List<OPDbillingData>();
            OPDbillingBO objbillingBO = new OPDbillingBO();
            OPDbillingData opdbill = new OPDbillingData();
            DepositBO objstdBO = new DepositBO();

            foreach (GridViewRow row in GVDiscountApprovalList.Rows)
            {
                IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                Label Particulars = (Label)GVDiscountApprovalList.Rows[row.RowIndex].Cells[0].FindControl("lblparticulars");
                Label amount = (Label)GVDiscountApprovalList.Rows[row.RowIndex].Cells[0].FindControl("lbl_charges");
                Label qty = (Label)GVDiscountApprovalList.Rows[row.RowIndex].Cells[0].FindControl("lblquantity");
                Label NetCharge = (Label)GVDiscountApprovalList.Rows[row.RowIndex].Cells[0].FindControl("lblnetcharges");
                Label ID = (Label)GVDiscountApprovalList.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                Label Doctor = (Label)GVDiscountApprovalList.Rows[row.RowIndex].Cells[0].FindControl("lbldoctorID");
                Label lblservicetype = (Label)GVDiscountApprovalList.Rows[row.RowIndex].Cells[0].FindControl("lblservicetype");
                Label lbl_subgroupID = (Label)GVDiscountApprovalList.Rows[row.RowIndex].Cells[0].FindControl("lbl_subgroupID");

                Label lbl_discount_amt = (Label)GVDiscountApprovalList.Rows[row.RowIndex].Cells[0].FindControl("lbl_discount_amt");
                OPDbillingData ObjDetails = new OPDbillingData();

                ObjDetails.ServiceName = Particulars.Text == "" ? null : Particulars.Text;
                ObjDetails.ServiceCharge = Convert.ToDecimal(amount.Text == "" ? "0" : amount.Text);
                ObjDetails.Quantity = Convert.ToInt32(qty.Text == "" ? "0" : qty.Text);
                ObjDetails.NetServiceCharge = Convert.ToDecimal(NetCharge.Text == "" ? "0" : NetCharge.Text);
                ObjDetails.ServiceID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                ObjDetails.DocID = Convert.ToInt32(Doctor.Text == "" ? "0" : Doctor.Text);
                ObjDetails.isDis = Convert.ToInt32(Convert.ToDecimal(lbl_discount_amt.Text.Trim() == "" ? "0" : lbl_discount_amt.Text.Trim()) == 0 ? 0 : 1);
                ObjDetails.DisAmount = Convert.ToDecimal(lbl_discount_amt.Text == "" ? "0" : lbl_discount_amt.Text);
                ObjDetails.ServiceTypeID = Convert.ToInt32(lblservicetype.Text == "" ? "0" : lblservicetype.Text);
                ObjDetails.SubGroupID = Convert.ToInt32(lbl_subgroupID.Text == "" ? "0" : lbl_subgroupID.Text);
                Listbill.Add(ObjDetails);
            }
            opdbill.XMLData = XmlConvertor.OpdDiscountBillDatatoXML(Listbill).ToString();
            opdbill.ID = Convert.ToInt64(ViewState["BillID"].ToString() == "" ? "0" : ViewState["BillID"].ToString());
            opdbill.TotalBillAmount = Convert.ToDecimal(txttotalamount.Text == "" ? "0" : txttotalamount.Text);
            opdbill.UHID = Convert.ToInt64(txtUHID.Text.Contains(":") ? txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1) : "0");
            opdbill.AdjustedAmount = Convert.ToDecimal(txtadjustedamount.Text == "" ? "0" : txtadjustedamount.Text);
            opdbill.DiscountedAmount = Convert.ToDecimal(txtdiscount.Text == "" ? "0" : txtdiscount.Text);
            opdbill.isExtraDiscount = 0;
            opdbill.SourceID = Convert.ToInt32(ddl_source.SelectedValue == "" ? "0" : ddl_source.SelectedValue);
            opdbill.ReferalID = Commonfunction.SemicolonSeparation_String_64(txt_referby.Text.Trim());
            opdbill.PaidAmount = Convert.ToDecimal(txtpaidamount.Text == "" ? "0" : txtpaidamount.Text);
            opdbill.PaymentMode = Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue);
            opdbill.BankName = txtbank.Text == "" ? null : txtbank.Text;
            opdbill.ChequeUTRnumber = txt_chequenumber.Text == "" ? null : txt_chequenumber.Text;
            opdbill.InvoiceNumber = txtinvoicenumber.Text == "" ? null : txtinvoicenumber.Text;
            opdbill.FinancialYearID = LogData.FinancialYearID;
            opdbill.EmployeeID = LogData.EmployeeID;
            opdbill.AddedBy = LogData.AddedBy;
            opdbill.HospitalID = LogData.HospitalID;
            opdbill.IsActive = LogData.IsActive;
            opdbill.IPaddress = LogData.IPaddress;
            opdbill.ActionType = Enumaction.Insert;
            opdbill.BarcodeImage = Commonfunction.getBarcodeImage(txtUHID.Text.Contains(":") ? txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1) : "0");
            //QR image generation//
            List<PatientQRdata> listQr = objbillingBO.GetPatientQRData(opdbill.UHID);
            PatientQRdata qrData = new PatientQRdata();
            qrData = listQr[0];
            string qrxml = XmlConvertor.PatientQRDataXML(qrData);
            opdbill.QRImage = Commonfunction.getQRImage(qrxml);
            opdbill.IsVerified = LogData.BillSetting == 0 ? 1 : 0;
            List<OPDbillingData> result = objbillingBO.UpdateOPDBill(opdbill);
            if (result.Count > 0)
            {
                if (result[0].isDis == 0)
                {
                    Session["ServiceList"] = null;
                    Session["DiscountList"] = null;
                    GVTPAList.DataSource = null;
                    GVTPAList.DataBind();
                    GVTPAList.Visible = true;
                    txtbillNo.Text = result[0].BillNo.ToString();
                    txtUHID.Text = "";
                    btnsave.Attributes["disabled"] = "disabled";
                    ddldepartment.Attributes["disabled"] = "disabled";
                    ddldoctor.Attributes["disabled"] = "disabled";

                    if (LogData.PrintEnable == 0)
                    {
                        btnprint.Attributes["disabled"] = "disabled";
                        btn_redgcard.Attributes["disabled"] = "disabled";
                        btn_cs.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        if (LogData.BillSetting == 0)
                        {
                            btn_cs.Attributes.Remove("disabled");
                            btn_redgcard.Attributes.Remove("disabled");
                            btnprint.Attributes.Remove("disabled");
                        }
                        else
                        {
                            btn_token.Visible = true;
                            ddlpaymentmode.Attributes["disabled"] = "disabled";
                            txt_chequenumber.ReadOnly = true;
                            txtinvoicenumber.ReadOnly = true;
                        }
                    }

                    hdnlastvisitdoctorID.Value = "0";
                    hdnnumberdays.Value = "0";
                    hdnprintID.Value = "0";
                    hdnsubgroupID.Value = "0";
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    return;
                }

            }
        }
        protected void txtdiscount_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txttotalamount.Text == "" ? "0.00" : txttotalamount.Text) >= Convert.ToDecimal(txtdiscount.Text == "" ? "0.00" : txtdiscount.Text))
            {
                if (Convert.ToDecimal(txtpaidamount.Text == "" ? "0.00" : txtpaidamount.Text) >= Convert.ToDecimal(txtdiscount.Text == "" ? "0.00" : txtdiscount.Text))
                {
                    txtpaidamount.Text = Commonfunction.Getrounding(((Convert.ToDecimal(txtpaidamount.Text == "" ? "0" : txtpaidamount.Text) - Convert.ToDecimal(txtdiscount.Text == "" ? "0" : txtdiscount.Text)).ToString()));
                }
                if (Convert.ToDecimal(txtpaidamount.Text == "" ? "0.00" : txtpaidamount.Text) < Convert.ToDecimal(txtdiscount.Text == "" ? "0.00" : txtdiscount.Text) && Convert.ToDecimal(txtadjustedamount.Text == "" ? "0.00" : txtadjustedamount.Text) > 0)
                {
                    txtpaidamount.Text = "0.00";
                    txtadjustedamount.Text = Commonfunction.Getrounding(((((Convert.ToDecimal(txtadjustedamount.Text == "" ? "0" : txtadjustedamount.Text) + ((Convert.ToDecimal(txttotalamount.Text == "" ? "0.00" : txttotalamount.Text) - Convert.ToDecimal(txtadjustedamount.Text == "" ? "0.00" : txtadjustedamount.Text))) - Convert.ToDecimal(txtdiscount.Text == "" ? "0" : txtdiscount.Text))))).ToString());
                }
            }
            else
            {
                Messagealert_.ShowMessage(lblmessage, "DiscountOver", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
            if (Convert.ToDecimal(txtdiscount.Text == "" ? "0.00" : txtdiscount.Text) == 0)
            {
                if (Convert.ToDecimal(txtbalanceinac.Text) > 0)
                {
                    if (Convert.ToDecimal(txtbalanceinac.Text) >= Convert.ToDecimal(txttotalamount.Text))
                    {
                        txtadjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txttotalamount.Text == "" ? "0" : txttotalamount.Text)).ToString());
                        txtpaidamount.Text = "0.00";
                    }
                    else if (Convert.ToDecimal(txtbalanceinac.Text) < Convert.ToDecimal(txttotalamount.Text))
                    {
                        txtadjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txtbalanceinac.Text == "" ? "0" : txtbalanceinac.Text)).ToString());
                        txtpaidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txttotalamount.Text == "" ? "0" : txttotalamount.Text) - Convert.ToDecimal(txtbalanceinac.Text.ToString() == "" ? "0" : txtbalanceinac.Text.ToString())).ToString());
                    }
                }
                else
                {
                    txtadjustedamount.Text = "0.00";
                    txtpaidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txttotalamount.Text == "" ? "0" : txttotalamount.Text)).ToString());
                }
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
            Decimal total_dis = Convert.ToDecimal(txtdiscount.Text == "" ? "0" : txtdiscount.Text) + Convert.ToDecimal(txt_tpa_amount.Text == "" ? "0" : txt_tpa_amount.Text);
            if (Convert.ToDecimal(txttotalamount.Text == "" ? "0.00" : txttotalamount.Text) < total_dis)
            {

                txtdiscount.Focus();
                ddl_tpa_patient_cat.SelectedIndex = 0;
                ddl_tpa_patient_sub_cat.SelectedIndex = 0;
                Messagealert_.ShowMessage(lblPopUpmsg, "DiscountOver", 0);
                divPopMsg.Visible = true;
                txtdiscount.Focus();
                this.MDTPA.Show();
                return;
            }
            else
            {
                txtdiscount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txtdiscount.Text == "" ? "0" : txtdiscount.Text) + Convert.ToDecimal(txt_tpa_amount.Text == "" ? "0.0" : txt_tpa_amount.Text)).ToString());
                txtpaidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txtpaidamount.Text == "" ? "0" : txtpaidamount.Text) - Convert.ToDecimal(txt_tpa_amount.Text == "" ? "0.0" : txt_tpa_amount.Text)).ToString());
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
                        txtdiscount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txtdiscount.Text == "" ? "0" : txtdiscount.Text) - Discount).ToString());
                        txtpaidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txtpaidamount.Text == "" ? "0" : txtpaidamount.Text) + Discount).ToString());
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
        protected void btn_token_Click(object sender, EventArgs e)
        {
            string code = Commonfunction.getBarcode(txtbillNo.Text.ToString());
            String barcode = " <tr><td align=\"center\"><label style=\"font-size: 9px; text - align: left;\" >OPD Consultation</label><br><img style=\"width:60%\" src=\"" + code + "\"/> " +
                "</td><td align=\"center\"><label style=\"font-size: 9px; text - align: left;\" >Investigation</label><br><label style=\"font-size: 9px;\">" + txtbillNo.Text + "</label><br><label style=\"font-size: 9px;\">" + ddlpaymentmode.SelectedItem + "</label>" +
                "<br><label style=\"font-size: 9px;\">" + txt_chequenumber.Text + "" + txtinvoicenumber.Text + "</label>" +
                "</td></tr>" +
                "<tr><td align=\"center\"><label style=\"font-size: 9px;\">" + txtbillNo.Text + "</label></td>" +
                     "<td align=\"right\"></td> </tr>";
            ltBarcode.Text = barcode;
            this.MDBarcode.Show();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

    }
}