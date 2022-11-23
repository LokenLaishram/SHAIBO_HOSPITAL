using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.PatientData;
using Mediqura.CommonData.MedEmergencyData;
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
using Mediqura.BOL.MedBillBO;
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.AdmissionData;
using Mediqura.BOL.AdmissionBO;
using Mediqura.BOL.MedEmergencyBO;
using System.Drawing;
namespace Mediqura.Web.MedEmergency
{
    public partial class EmergencyServiceRecord : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                if (Request["EM"] != null && Request["EM"] != "")
                {
                    txt_emrgno.Text = Request["EM"].ToString();
                    EmLoadData();
                }
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.OPDepartment));
            Commonfunction.Insertzeroitemindex(ddldoctor);
            AutoCompleteExtender2.ContextKey = "0";
            Session["EMRGServiceList"] = null;
            hdngroupnumber.Value = null;
            btn_print.Attributes["disabled"] = "disabled";
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetEmrgNos(string prefixText, int count, string contextKey)
        {
            EmrgAdmissionData Objpaic = new EmrgAdmissionData();
            EmrgAdmissionBO objInfoBO = new EmrgAdmissionBO();
            List<EmrgAdmissionData> getResult = new List<EmrgAdmissionData>();
            Objpaic.EmrgNo = prefixText;

            getResult = objInfoBO.GetEmrgNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmrgNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetInvNumber(string prefixText, int count, string contextKey)
        {
            EmrgAdmissionData Objpaic = new EmrgAdmissionData();
            EmrgAdmissionBO objInfoBO = new EmrgAdmissionBO();
            List<EmrgAdmissionData> getResult = new List<EmrgAdmissionData>();
            Objpaic.InvNumber = prefixText;
            Objpaic.EmergencyNo = contextKey;
            getResult = objInfoBO.EmergencyInvnumber(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].InvNumber.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetServiceNumber(string prefixText, int count, string contextKey)
        {
            EmrgAdmissionData Objpaic = new EmrgAdmissionData();
            EmrgAdmissionBO objInfoBO = new EmrgAdmissionBO();
            List<EmrgAdmissionData> getResult = new List<EmrgAdmissionData>();
            Objpaic.ServiceNumber = prefixText;
            Objpaic.EmergencyNo = contextKey;
            getResult = objInfoBO.Getemergnecyservicenumber(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ServiceNumber.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetServices(string prefixText, int count, string contextKey)
        {
            EmrgAdmissionData Objpaic = new EmrgAdmissionData();
            EmrgAdmissionBO objInfoBO = new EmrgAdmissionBO();
            List<EmrgAdmissionData> getResult = new List<EmrgAdmissionData>();
            Objpaic.ServiceName = prefixText;
            Objpaic.ServiceTypeID = Convert.ToInt32(contextKey);
            Objpaic.DocID = count;
            getResult = objInfoBO.GetEmrgServices(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ServiceName.ToString());
            }
            return list;
        }
        //TAB 2 //
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetEmgPatientName(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetEmgPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmgPatientName.ToString());
            }
            return list;
        }
        protected void gvEmrgservicerecordlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvEmrgservicerecordlist.Rows[i];
                    List<EmrgAdmissionData> ItemList = Session["EMRGServiceList"] == null ? new List<EmrgAdmissionData>() : (List<EmrgAdmissionData>)Session["EMRGServiceList"];
                    if (ItemList.Count >= 0)
                    {
                        int qty = ItemList[i].Quantity;
                        txttotalquantity.Text = (Convert.ToInt32(txttotalquantity.Text == "" ? "0" : txttotalquantity.Text) - qty).ToString();
                    }
                    ItemList.RemoveAt(i);
                    Session["EmrgAdmissionData"] = ItemList;
                    gvEmrgservicerecordlist.DataSource = ItemList;
                    gvEmrgservicerecordlist.DataBind();
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
        protected void gvEmrgservicerecordlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSerial = (Label)e.Row.FindControl("lblserialID");
                lblSerial.Text = ((gvEmrgservicerecordlist.PageIndex * gvEmrgservicerecordlist.PageSize) + e.Row.RowIndex + 1).ToString();
                DropDownList ddltestcenter = (DropDownList)e.Row.FindControl("ddl_testcenter");
                Label lbltestcenterID = (Label)e.Row.FindControl("lbl_testcenterID");
                DropDownList ddlurgencyState = (DropDownList)e.Row.FindControl("ddl_urgency");
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddltestcenter, mstlookup.GetLookupsList(LookupName.TestCenter));
                ddltestcenter.SelectedValue = lbltestcenterID.Text == "" || lbltestcenterID.Text == null ? "0" : lbltestcenterID.Text;
                Commonfunction.PopulateDdl(ddlurgencyState, mstlookup.GetLookupsList(LookupName.Urgency));
                ddlurgencyState.SelectedIndex = 1;
                if (ddl_servicecategory.SelectedValue == "4")
                {
                    e.Row.Cells[5].Enabled = true;
                    e.Row.Cells[6].Enabled = true;
                }
                else
                {
                    e.Row.Cells[5].Enabled = false;
                    e.Row.Cells[6].Enabled = false;
                }
            }
        }
        protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldepartment.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddldoctor, mstlookup.GetDepartmentDoctor(Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue)));
            }
        }
        protected void ddldoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender2.CompletionSetCount = Convert.ToInt32(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
        }
        protected void txt_emrgno_TextChanged(object sender, EventArgs e)
        {
            EmLoadData();
        }
        private void EmLoadData()
        {
            EmrgAdmissionData Objpaic = new EmrgAdmissionData();
            EmrgAdmissionBO objInfoBO = new EmrgAdmissionBO();
            List<EmrgAdmissionData> getResult = new List<EmrgAdmissionData>();
            Objpaic.EmrgNo = txt_emrgno.Text.Trim() == "" ? "" : txt_emrgno.Text.Trim();
            getResult = objInfoBO.GetPatientsDetailsByEmrgNo(Objpaic);
            if (getResult.Count > 0)
            {
                txtname.Text = getResult[0].PatientName.ToString();
                txt_address.Text = getResult[0].Address.ToString();
                hdnUHID.Value = getResult[0].UHID.ToString();
                hdMsbPc.Value = getResult[0].MsbPc.ToString();
                txtPatientCat.Text = getResult[0].patientCat.ToString();
                AutoCompleteExtender2.CompletionSetCount = Convert.ToInt32(getResult[0].DocID.ToString());
            }
            else
            {
                txtname.Text = "";
                txt_emrgno.Text = "";
                hdnUHID.Value = "";
                ddldoctor.SelectedIndex = 0;
                AutoCompleteExtender2.CompletionSetCount = 0;
            }
        }
        protected void txtservices_TextChanged(object sender, EventArgs e)
        {
            if (ddl_servicecategory.SelectedValue != "2" && ddldoctor.SelectedIndex == 0)
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
                EmrgAdmissionBO ObjEmrgBO = new EmrgAdmissionBO();
                EmrgAdmissionData ObjEmrgData = new EmrgAdmissionData();
                ObjEmrgData.ID = Convert.ToInt32(ID == "" ? "0" : ID);
                ObjEmrgData.ServiceCategoryID = Convert.ToInt32(ddl_servicecategory.SelectedValue == "" ? "0" : ddl_servicecategory.SelectedValue);
                ObjEmrgData.DocID = Convert.ToInt32(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
                ObjEmrgData.EmergencyNo = txt_emrgno.Text.Trim();
                List<EmrgAdmissionData> result = ObjEmrgBO.GetServiceChargeByID(ObjEmrgData);
                if (result.Count > 0)
                {
                    decimal discount;
                    discount = (result[0].ServiceCharge * Convert.ToDecimal(hdMsbPc.Value)) / 100;
                    if (result[0].isExclude == 0)
                    {
                        hdIsMsbDoctor.Value = "1";
                        txtservicecharge.Text = Commonfunction.Getrounding((result[0].ServiceCharge - discount).ToString());
                    }
                    else
                    {
                        hdIsMsbDoctor.Value = "0";
                        txtservicecharge.Text = Commonfunction.Getrounding((result[0].ServiceCharge).ToString());
                    }

                    lblservicename.Text = result[0].ServiceName.ToString();
                    hdngroupID.Value = result[0].ServiceTypeID.ToString();
                    hdnsubgroupID.Value = result[0].SubGroupID.ToString();
                    hdntescenterID.Value = result[0].TestCenterID.ToString();
                    txtquantity.Text = "1";
                    txtquantity.Focus();
                    if (result[0].IsExists.ToString() == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Today this service is already added.Are you sure to add again?.');", true);
                    }
                }
                else
                {
                    txtservices.ReadOnly = false;
                    txtservicecharge.Text = "0.0";
                    txtquantity.Text = "0";
                    txtservices.Text = "";
                }
            }
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (ddl_servicecategory.SelectedValue != "2" && ddldoctor.SelectedIndex == 0)
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
            string ID;
            var source = txtservices.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                // Check Duplicate data 
                foreach (GridViewRow row in gvEmrgservicerecordlist.Rows)
                {
                    Label ServiceID = (Label)gvEmrgservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    if (Convert.ToInt32(ServiceID.Text) == Convert.ToInt32(ID))
                    {
                        Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txtservices.Text = "";
                        txtquantity.Text = "";
                        txtservicecharge.Text = "";
                        txtservices.ReadOnly = false;
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
            List<EmrgAdmissionData> EmrgServiceList = Session["EMRGServiceList"] == null ? new List<EmrgAdmissionData>() : (List<EmrgAdmissionData>)Session["EMRGServiceList"];
            EmrgAdmissionData ObjService = new EmrgAdmissionData();
            ObjService.ServiceCategoryID = Convert.ToInt32(ddl_servicecategory.SelectedValue == "" ? "0" : ddl_servicecategory.SelectedValue);
            ObjService.ServiceTypeID = Convert.ToInt32(hdngroupID.Value == "" ? "0" : hdngroupID.Value);
            ObjService.SubGroupID = Convert.ToInt32(hdnsubgroupID.Value == "" ? "0" : hdnsubgroupID.Value);
            ObjService.ServiceCharge = Convert.ToDecimal(txtservicecharge.Text.ToString() == "" ? "0" : txtservicecharge.Text.ToString());
            ObjService.Quantity = Convert.ToInt32(txtquantity.Text == "" || txtquantity.Text == "0" ? "1" : txtquantity.Text);
            ObjService.DocID = Convert.ToInt32(ddl_servicecategory.SelectedValue == "2" ? "0" : ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
            ObjService.ServiceID = Convert.ToInt32(ID);
            ObjService.TestCenterID = Convert.ToInt32(hdntescenterID.Value == "" ? "0" : hdntescenterID.Value);
            ObjService.UrgencyID = 1;
            ObjService.NetServiceCharge = Convert.ToDecimal(txtservicecharge.Text.ToString() == "" ? "0" : txtservicecharge.Text.ToString()) * Convert.ToInt32(txtquantity.Text == "" || txtquantity.Text == "0" ? "1" : txtquantity.Text);
            ObjService.ServiceName = lblservicename.Text.Trim();
            txttotalquantity.Text = (Convert.ToInt32(txttotalquantity.Text == "" ? "0" : txttotalquantity.Text) + Convert.ToInt32(txtquantity.Text == "" || txtquantity.Text == "0" ? "1" : txtquantity.Text)).ToString();
            EmrgServiceList.Add(ObjService);
            if (EmrgServiceList.Count > 0)
            {
                gvEmrgservicerecordlist.DataSource = EmrgServiceList;
                gvEmrgservicerecordlist.DataBind();
                gvEmrgservicerecordlist.Visible = true;
                Session["EMRGServiceList"] = EmrgServiceList;
                txtservices.Text = "";
                txtservicecharge.Text = "";
                txtquantity.Text = "";
                txtservices.Focus();
                txtservices.ReadOnly = false;
                btnsave.Attributes.Remove("disabled");
                ddl_servicecategory.Attributes["disabled"] = "disabled";
            }
            else
            {
                gvEmrgservicerecordlist.DataSource = null;
                gvEmrgservicerecordlist.DataBind();
                gvEmrgservicerecordlist.Visible = true;
                btnsave.Attributes["disabled"] = "disabled";
                ddl_servicecategory.Attributes.Remove("disabled");
            }
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
            if (txt_emrgno.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "EMRGNO", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_emrgno.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            List<EmrgAdmissionData> Listser = new List<EmrgAdmissionData>();
            EmrgAdmissionBO objiprecBO = new EmrgAdmissionBO();
            EmrgAdmissionData objrec = new EmrgAdmissionData();

            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in gvEmrgservicerecordlist.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label Particulars = (Label)gvEmrgservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblparticulars");
                    Label amount = (Label)gvEmrgservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblamount");
                    Label qty = (Label)gvEmrgservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblquantity");
                    Label NetCharge = (Label)gvEmrgservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblnetcharges");
                    Label SerialID = (Label)gvEmrgservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblserialID");
                    Label ID = (Label)gvEmrgservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label DoctorID = (Label)gvEmrgservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_docID");
                    Label ServiceCategory = (Label)gvEmrgservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblservicecategory");
                    Label ServiceType = (Label)gvEmrgservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblservicetype");
                    Label Subgroup = (Label)gvEmrgservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblsubgroupID");
                    DropDownList TestcenterID = (DropDownList)gvEmrgservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("ddl_testcenter");
                    DropDownList Urgency = (DropDownList)gvEmrgservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("ddl_urgency");
                    TextBox remark = (TextBox)gvEmrgservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("txt_remarks");

                    EmrgAdmissionData ObjDetails = new EmrgAdmissionData();

                    ObjDetails.ServiceName = Particulars.Text == "" ? null : Particulars.Text;
                    ObjDetails.Remarks = remark.Text == "" ? "" : remark.Text;
                    ObjDetails.ServiceCharge = Convert.ToDecimal(amount.Text == "" ? "0" : amount.Text);
                    ObjDetails.Quantity = Convert.ToInt32(qty.Text == "" ? "0" : qty.Text);
                    ObjDetails.NetServiceCharge = Convert.ToDecimal(NetCharge.Text == "" ? "0" : NetCharge.Text);
                    ObjDetails.SerialID = Convert.ToInt32(SerialID.Text == "" ? "0" : SerialID.Text);
                    ObjDetails.ServiceID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    ObjDetails.DocID = Convert.ToInt32(DoctorID.Text == "" ? "0" : DoctorID.Text);
                    ObjDetails.TestCenterID = Convert.ToInt32(TestcenterID.Text == "" ? "0" : TestcenterID.Text);
                    ObjDetails.UrgencyID = Convert.ToInt32(Urgency.SelectedValue == "" ? "0" : Urgency.SelectedValue);
                    ObjDetails.ServiceCategoryID = Convert.ToInt32(ServiceCategory.Text == "" ? "0" : ServiceCategory.Text);
                    ObjDetails.ServiceTypeID = Convert.ToInt32(ServiceType.Text == "" ? "0" : ServiceType.Text);
                    ObjDetails.SubGroupID = Convert.ToInt32(Subgroup.Text == "" ? "0" : Subgroup.Text);
                    ObjDetails.isMsbDoctor = Convert.ToInt32(hdIsMsbDoctor.Value == "" ? "0" : hdIsMsbDoctor.Value);
                    ObjDetails.isMsbPatient = Convert.ToInt32(hdMsbPc.Value == "" ? "0" : hdMsbPc.Value) > 0 ? 1 : 0;
                    ObjDetails.MsbPc = Convert.ToInt32(hdIsMsbDoctor.Value == "" ? "0" : hdIsMsbDoctor.Value) == 1 ? Convert.ToInt32(hdMsbPc.Value == "" ? "0" : hdMsbPc.Value) : 0;
                    Listser.Add(ObjDetails);
                }
                objrec.XMLData = XmlConvertor.EMRGServiceRecordDatatoXML(Listser).ToString();
                objrec.EmrgNo = txt_emrgno.Text == "" ? "0" : txt_emrgno.Text;
                objrec.FinancialYearID = LogData.FinancialYearID;
                objrec.EmployeeID = LogData.EmployeeID;
                objrec.HospitalID = LogData.HospitalID;
                objrec.IPaddress = LogData.IPaddress;
                objrec.UHID = Convert.ToInt64(hdnUHID.Value == "" ? "0" : hdnUHID.Value);
                objrec.ServiceCategoryID = Convert.ToInt32(ddl_servicecategory.SelectedValue == "" ? "0" : ddl_servicecategory.SelectedValue);
                objrec.ActionType = Enumaction.Insert;

                List<EmrgAdmissionData> result = objiprecBO.UpdateEMRGServiceRecord(objrec);
                if (result[0].ResultOutput.ToString() == "1")
                {
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    Session["EMRGServiceList"] = null;
                    txt_emrgno.Text = "";
                    txt_invnumber.Text = result[0].InvNumber;
                    hdngroupnumber.Value = result[0].GroupNumber;
                    btnsave.Attributes["disabled"] = "disabled";
                    btn_print.Attributes.Remove("disabled");
                }
                else
                {
                    txt_invnumber.Text = "";
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    btnsave.Attributes.Remove("disabled");
                    hdngroupnumber.Value = null;
                    btn_print.Attributes["disabled"] = "disabled";
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
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txt_emrgno.Text = "";
            txtname.Text = "";
            txtservicecharge.Text = "";
            txtservices.Text = "";
            txtquantity.Text = "";
            bindddl();
            txt_address.Text = "";
            Session["EMRGServiceList"] = null;
            gvEmrgservicerecordlist.DataSource = null;
            gvEmrgservicerecordlist.DataBind();
            gvEmrgservicerecordlist.Visible = false;
            lblmessage.Visible = false;
            div1.Visible = false;
            txtservices.Text = "";
            txtquantity.Text = "";
            txttotalquantity.Text = "";
            ddldoctor.SelectedIndex = 0;
            txtservices.ReadOnly = false;
            ddldoctor.SelectedIndex = 0;
            ddldoctor.Attributes.Remove("disabled");
            ddl_servicecategory.SelectedIndex = 0;
            hdnUHID.Value = null;
            ddl_servicecategory.Attributes.Remove("disabled");
            txt_invnumber.Text = "";
            ddldepartment.SelectedIndex = 0;
            Commonfunction.Insertzeroitemindex(ddldoctor);
            hdngroupnumber.Value = null;
            btn_print.Attributes["disabled"] = "disabled";
        }
        protected void txt_emrgNoList_TextChanged(object sender, EventArgs e)
        {
            if (txtpatientNames.Text != "")
            {
                string EmgNo;
                var source = txtpatientNames.Text.ToString();
                if (source.Contains(":"))
                {
                    EmgNo = source.Substring(source.LastIndexOf(':') + 1);
                    AutoCompleteExtender4.ContextKey = EmgNo.Trim().ToString();
                    AutoCompleteExtender5.ContextKey = EmgNo.Trim().ToString();
                }
                bindgrid();
            
            }
        }
        protected void bindgrid()
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

                if (txtpatientNames.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "EMRGNO", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    txtpatientNames.Focus();
                    return;
                }
                else
                {
                    var source1 = txtpatientNames.Text.ToString();
                    if (source1.Contains(":"))
                    {
                        lblmessage.Visible = false;
                    }
                    else
                    {
                        txtpatientNames.Text = "";
                        txtpatientNames.Focus();
                        return;
                    }

                    lblmessage2.Visible = false;
                    divmsg2.Visible = false;
                }

                if (txtdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDatefrom", 0);
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
                        Messagealert_.ShowMessage(lblmessage2, "ValidDateto", 0);
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
                List<EmrgAdmissionData> objdeposit = GetEMRGServiceList(0);
                if (objdeposit.Count > 0)
                {
                    gvEMRGservicerecord.DataSource = objdeposit;
                    gvEMRGservicerecord.DataBind();
                    gvEMRGservicerecord.Visible = true;
                    //txtpatientNames.Text = objdeposit[0].PatientName.ToString();
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    gvEMRGservicerecord.DataSource = null;
                    gvEMRGservicerecord.DataBind();
                    gvEMRGservicerecord.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult.Visible = false;
                    txtpatientNames.Text = "";
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        public List<EmrgAdmissionData> GetEMRGServiceList(int curIndex)
        {

            EmrgAdmissionData objpat = new EmrgAdmissionData();
            EmrgAdmissionBO objBO = new EmrgAdmissionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            //objpat.EmrgNo = txt_emrgNoList.Text.Trim() == "" ? "" : txt_emrgNoList.Text.Trim();
            objpat.PatientName = ""; // txtpatientNames.Text == "" ? "" : txtpatientNames.Text.Trim();
            string EmgNo;
            var source = txtpatientNames.Text.ToString();
            if (source.Contains(":"))
            {
                EmgNo = source.Substring(source.LastIndexOf(':') + 1);
                objpat.EmrgNo = EmgNo.Trim();
            }
            else
            {
                objpat.EmrgNo = "";
            }

            objpat.ServiceNumber = txt_servicenumber.Text == "" ? "" : txt_servicenumber.Text.Trim();
            objpat.InvNumber = txt_searchinvnumber.Text == "" ? "" : txt_searchinvnumber.Text.Trim();
            objpat.ServiceCategoryID = Convert.ToInt32(ddl_servicetypes.SelectedValue == "" ? "0" : ddl_servicetypes.SelectedValue);
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objpat.DateFrom = from;
            objpat.DateTo = To;
            objpat.AmountEnable = LogData.AmountEnable;
            return objBO.GetEMRGServiceList(objpat);
        }
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            if (txtpatientNames.Text != "")
            {
                bindgrid();
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            txtpatientNames.Text = "";
            bindgrid();
        }
        protected void gvEMRGservicerecord_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label devicestatus = (Label)e.Row.FindControl("lbl_isdeviceinitiated");
                Label service = (Label)e.Row.FindControl("lblservices");
                if (devicestatus.Text == "3")
                {
                    service.ForeColor = Color.FromName("#fd0808");
                }
            }
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txt_emrgNoList.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            ddlstatus.SelectedIndex = 0;
            ddl_servicetypes.SelectedIndex = 0;
            gvEMRGservicerecord.DataSource = null;
            gvEMRGservicerecord.DataBind();
            gvEMRGservicerecord.Visible = false;
            lblresult.Visible = false;
            txtpatientNames.Text = "";
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage2.Visible = false;
            lblresult.Visible = false;
            div1.Visible = false;
            div1.Visible = false;
            hdngroupID.Value = null;
            hdnsubgroupID.Value = null;
            hdntescenterID.Value = null;
            hdnUHID.Value = null;
            hdnurgencyID.Value = null;
        }
        protected void gvEMRGservicerecord_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    EmrgAdmissionData objadmin = new EmrgAdmissionData();
                    EmrgAdmissionBO obadminBO = new EmrgAdmissionBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvEMRGservicerecord.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_recordID");
                    Label SerialID = (Label)gr.Cells[0].FindControl("lbl_serialID");
                    Label IPNo = (Label)gr.Cells[0].FindControl("lblIPNo");
                    Label name = (Label)gr.Cells[0].FindControl("lblname");
                    Label service = (Label)gr.Cells[0].FindControl("lblservices");
                    Label charge = (Label)gr.Cells[0].FindControl("lblcharges");
                    Label quantity = (Label)gr.Cells[0].FindControl("lblquantity");
                    Label netservicecharge = (Label)gr.Cells[0].FindControl("lblamount");
                    Label addedby = (Label)gr.Cells[0].FindControl("lbladdedby");
                    Label iNVNUMBER = (Label)gr.Cells[0].FindControl("lblinvnumber");
                    Label servicenumber = (Label)gr.Cells[0].FindControl("lbl_servicenumber");
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
                        objadmin.Remarks = txtremarks.Text;
                    }

                    objadmin.EmrgNo = txt_emrgNoList.Text == "" ? "" : txt_emrgNoList.Text;
                    objadmin.InvNumber = iNVNUMBER.Text == "" ? "" : iNVNUMBER.Text;
                    objadmin.ServiceNumber = servicenumber.Text == "" ? "" : servicenumber.Text;
                    objadmin.SerialID = Convert.ToInt32(SerialID.Text == "" ? "0" : SerialID.Text);
                    objadmin.ID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    objadmin.EmployeeID = LogData.UserLoginId;
                    objadmin.EmployeeID = LogData.EmployeeID;
                    objadmin.HospitalID = LogData.HospitalID;
                    objadmin.IPaddress = LogData.IPaddress;

                    int Result = obadminBO.DeleteEMGServiceRecordByEMRGNo(objadmin);
                    if (Result == 1)
                    {
                        bindgrid();
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        divmsg2.Attributes["class"] = "SucessAlert";
                        divmsg2.Visible = true;

                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage2, "system", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                    }

                }
                if (e.CommandName == "Print")
                {
                    if (LogData.PrintEnable == 0)
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
                    int j = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gp = gvEMRGservicerecord.Rows[j];
                    Label GroupNumber = (Label)gp.Cells[0].FindControl("lbl_groupnumber");

                    string url = "../MedIPD/Reports/ReportViewer.aspx?option=IPservicerecpt&Grpnumber=" + GroupNumber.Text + "&Actiontype=" + 1;
                    string fullURL = "window.open('" + url + "', '_blank');";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);

                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<EmrgAdmissionData> ServiceDetails = GetEMRGServiceList(0);
            List<EMRGServiceListDataTOeXCEL> ListexcelData = new List<EMRGServiceListDataTOeXCEL>();
            int i = 0;
            foreach (EmrgAdmissionData row in ServiceDetails)
            {
                EMRGServiceListDataTOeXCEL Ecxeclpat = new EMRGServiceListDataTOeXCEL();
                Ecxeclpat.EmrgNo = ServiceDetails[i].EmrgNo;
                Ecxeclpat.UHID = ServiceDetails[i].UHID;
                Ecxeclpat.PatientName = ServiceDetails[i].PatientName;
                Ecxeclpat.ServiceName = ServiceDetails[i].ServiceName;
                Ecxeclpat.ServiceCharge = ServiceDetails[i].ServiceCharge;
                Ecxeclpat.Quantity = ServiceDetails[i].Quantity;
                Ecxeclpat.NetServiceCharge = ServiceDetails[i].NetServiceCharge;
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
                    gvEMRGservicerecord.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvEMRGservicerecord.Columns[10].Visible = false;
                    gvEMRGservicerecord.Columns[11].Visible = false;
                    gvEMRGservicerecord.RenderControl(hw);
                    gvEMRGservicerecord.HeaderRow.Style.Add("width", "15%");
                    gvEMRGservicerecord.HeaderRow.Style.Add("font-size", "10px");
                    gvEMRGservicerecord.Style.Add("text-decoration", "none");
                    gvEMRGservicerecord.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvEMRGservicerecord.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=EmergencyServiceDetails.pdf");
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
                wb.Worksheets.Add(dt, "IP service record");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=EmergencyServiceDetails.xlsx");
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
        protected void gvEMRGservicerecord_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEMRGservicerecord.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        protected void ddl_servicetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender2.ContextKey = ddl_servicecategory.SelectedValue;
            if (ddl_servicecategory.SelectedValue != "2" && ddldoctor.SelectedIndex == 0)
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
        protected void ddl_servicetypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_servicetypes.SelectedValue == "4")
            {
                txt_searchinvnumber.ReadOnly = false;
            }
            else
            {
                txt_searchinvnumber.ReadOnly = true;
            }
        }
        protected void btnprint_Click(object sender, EventArgs e)
        {
            string url = "../MedIPD/Reports/ReportViewer.aspx?option=IPservicerecpt&Grpnumber=" + hdngroupnumber.Value + "&Actiontype=" + 1;
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
    }
}