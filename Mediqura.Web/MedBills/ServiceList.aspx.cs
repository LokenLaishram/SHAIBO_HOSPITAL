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
using Mediqura.BOL.AdmissionBO;
using Mediqura.Web.MedCommon;
using Mediqura.CommonData.Common;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.BOL.CommonBO;
using Mediqura.CommonData.PatientData;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.MedBillData;
using Mediqura.Utility;
using System.Drawing;

namespace Mediqura.Web.MedBills
{
    public partial class ServiceList : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                txttotalquantity.Text = "0";
                if (Request["IP"] != null && Request["IP"] != "")
                {
                    txt_autoipno.Text = Request["IP"].ToString();
                    ipLoadData();
                }
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.OPDepartment));
            Commonfunction.Insertzeroitemindex(ddldoctor);
            AutoCompleteExtender2.ContextKey = "0";
            btnsave.Attributes["disabled"] = "disabled";
            Session["IPServiceList"] = null;
            txt_servicedate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txt_servicetimepicker.Text = System.DateTime.Now.ToString("hh:mm");
            hdngroupnumber.Value = null;
            btn_print.Attributes["disabled"] = "disabled";
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPNo(string prefixText, int count, string contextKey)
        {
            IPServiceRecordData Objpaic = new IPServiceRecordData();
            IPServiceRecordBO objInfoBO = new IPServiceRecordBO();
            List<IPServiceRecordData> getResult = new List<IPServiceRecordData>();
            Objpaic.IPNo = prefixText;
            getResult = objInfoBO.getIPNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPNo.ToString());
            }
            return list;
        }
        protected void txt_autoipno_TextChanged(object sender, EventArgs e)
        {
            ipLoadData();
        }
        private void ipLoadData()
        {

            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.IPNo = txt_autoipno.Text.Trim() == "" ? "" : txt_autoipno.Text.Trim();
            getResult = objInfoBO.GetPatientDetailsByIPNo(Objpaic);
            if (getResult.Count > 0)
            {
                if (getResult[0].ChkWardrecived == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "chkwardrecd", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    txtname.Text = getResult[0].PatientName.ToString() + " | Gender : " + getResult[0].Gender.ToString() + " | Age-" + getResult[0].Agecount.ToString();
                    txt_category.Text = getResult[0].PatientCategory.ToString();
                    hdnpatcategory.Value = getResult[0].PatientType.ToString();
                    hdMsbPc.Value = getResult[0].MSBpc.ToString();
                    hdnUHID.Value = getResult[0].UHID.ToString();
                    txt_address.Text = getResult[0].Address.ToString();
                }
            }
            else
            {
                txtname.Text = "";
                txt_autoipno.Text = "";
                txt_address.Text = "";
                hdnUHID.Value = null;
                hdnpatcategory.Value = null;
                txt_autoipno.Focus();
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetServices(string prefixText, int count, string contextKey)
        {
            LabServiceMasterData Objpaic = new LabServiceMasterData();
            LabServiceMasterBO objInfoBO = new LabServiceMasterBO();
            List<LabServiceMasterData> getResult = new List<LabServiceMasterData>();
            Objpaic.ServiceName = prefixText;
            Objpaic.ServicetypeID = Convert.ToInt32(contextKey);
            Objpaic.DoctorID = count;
            getResult = objInfoBO.GetIPServices(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ServiceName.ToString());
            }
            return list;
        }
        protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldepartment.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddldoctor, mstlookup.GetDepartmentDoctor(Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue)));
            }
        }
        protected void ddl_servicecategory_SelectedIndexChanged(object sender, EventArgs e)
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
        protected void ddldoctor_SelectedIndexChanged(object sender, EventArgs e)
        {

            AutoCompleteExtender2.CompletionSetCount = Convert.ToInt32(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
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
                OPDbillingBO ObjbillBO = new OPDbillingBO();
                LabBillingData ObjBillData = new LabBillingData();
                ObjBillData.ID = Convert.ToInt32(ID == "" ? "0" : ID);
                ObjBillData.ServiceCategoryID = Convert.ToInt32(ddl_servicecategory.SelectedValue == "" ? "0" : ddl_servicecategory.SelectedValue);
                ObjBillData.DocID = Convert.ToInt32(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
                ObjBillData.IPnumber = txt_autoipno.Text.Trim();
                List<LabBillingData> result = ObjbillBO.GetIPServiceChargeByID(ObjBillData);
                if (result.Count > 0)
                {
                    decimal discount;
                    discount = (result[0].ServiceCharge * Convert.ToDecimal(hdMsbPc.Value == "" ? "0" : hdMsbPc.Value)) / 100;
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
                }
            }
            else
            {
                txtservices.Text = "";
                txtservices.Focus();
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
                txtservices.Focus();
                txtservicecharge.Text = "";
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (txtservices.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Service", 0);
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
            if (Commonfunction.isValidDate(txt_servicedate.Text) == false || Commonfunction.CheckOverDate(txt_servicedate.Text) == true)
            {
                Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_servicedate.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            string ID;
            var source = txtservices.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                // Check Duplicate data 
                foreach (GridViewRow row in gvipservicerecordlist.Rows)
                {
                    Label ServiceID = (Label)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
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
            List<IPServiceRecordData> IPServiceList = Session["IPServiceList"] == null ? new List<IPServiceRecordData>() : (List<IPServiceRecordData>)Session["IPServiceList"];
            IPServiceRecordData ObjService = new IPServiceRecordData();

            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            ObjService.ServiceCategoryID = Convert.ToInt32(ddl_servicecategory.SelectedValue == "" ? "0" : ddl_servicecategory.SelectedValue);
            ObjService.ServiceTypeID = Convert.ToInt32(hdngroupID.Value == "" ? "0" : hdngroupID.Value);
            ObjService.SubGroupID = Convert.ToInt32(hdnsubgroupID.Value == "" ? "0" : hdnsubgroupID.Value);
            ObjService.ServiceCharge = Convert.ToDecimal(txtservicecharge.Text.ToString() == "" ? "0" : txtservicecharge.Text.ToString());
            ObjService.Quantity = Convert.ToInt32(txtquantity.Text == "" || txtquantity.Text == "0" ? "1" : txtquantity.Text);
            ObjService.DocID = Convert.ToInt32(ddl_servicecategory.SelectedValue == "2" ? "0" : ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
            ObjService.ServiceID = Convert.ToInt32(ID);
            ObjService.TestCenterID = Convert.ToInt32(hdntescenterID.Value == "" ? "0" : hdntescenterID.Value);
            ObjService.UrgencyID = 1;
            DateTime servdate = txt_servicedate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_servicedate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            string Date = servdate.ToString("yyyy-MM-dd");
            string picker = txt_servicetimepicker.Text.Trim();
            ObjService.ServiceDate = servdate;
            ObjService.NetServiceCharge = Convert.ToDecimal(txtservicecharge.Text.ToString() == "" ? "0" : txtservicecharge.Text.ToString()) * Convert.ToInt32(txtquantity.Text == "" || txtquantity.Text == "0" ? "1" : txtquantity.Text);
            ObjService.ServiceName = lblservicename.Text.Trim();
            txttotalquantity.Text = (Convert.ToInt32(txttotalquantity.Text == "" ? "0" : txttotalquantity.Text) + Convert.ToInt32(txtquantity.Text == "" || txtquantity.Text == "0" ? "1" : txtquantity.Text)).ToString();
            IPServiceList.Add(ObjService);
            if (IPServiceList.Count > 0)
            {
                gvipservicerecordlist.DataSource = IPServiceList;
                gvipservicerecordlist.DataBind();
                gvipservicerecordlist.Visible = true;
                Session["IPServiceList"] = IPServiceList;
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
                gvipservicerecordlist.DataSource = null;
                gvipservicerecordlist.DataBind();
                gvipservicerecordlist.Visible = true;
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
            if (txt_autoipno.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "IPNo", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_autoipno.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            List<IPServiceRecordData> Listser = new List<IPServiceRecordData>();
            IPServiceRecordBO objiprecBO = new IPServiceRecordBO();
            IPServiceRecordData objrec = new IPServiceRecordData();

            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in gvipservicerecordlist.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label Particulars = (Label)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblparticulars");
                    Label amount = (Label)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblamount");
                    Label qty = (Label)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblquantity");
                    Label NetCharge = (Label)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblnetcharges");
                    Label SerialID = (Label)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblserialID");
                    Label ID = (Label)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label DoctorID = (Label)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_docID");
                    Label ServiceCategory = (Label)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblservicecategory");
                    Label ServiceType = (Label)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblservicetype");
                    Label Subgroup = (Label)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblsubgroupID");
                    Label servicedate = (Label)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_serviceDate");
                    TextBox remark = (TextBox)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("txt_remarks");
                    DropDownList TestcenterID = (DropDownList)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("ddl_testcenter");
                    DropDownList Urgency = (DropDownList)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("ddl_urgency");

                    IPServiceRecordData ObjDetails = new IPServiceRecordData();

                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    ObjDetails.TestName = Particulars.Text == "" ? null : Particulars.Text;
                    ObjDetails.Remarks = remark.Text == "" ? "" : remark.Text;
                    ObjDetails.ServiceCharge = Convert.ToDecimal(amount.Text == "" ? "0" : amount.Text);
                    ObjDetails.Quantity = Convert.ToInt32(qty.Text == "" ? "0" : qty.Text);
                    ObjDetails.NetServiceCharge = Convert.ToDecimal(NetCharge.Text == "" ? "0" : NetCharge.Text);
                    ObjDetails.SerialID = Convert.ToInt32(SerialID.Text == "" ? "0" : SerialID.Text);
                    ObjDetails.ServiceID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    ObjDetails.DoctorID = Convert.ToInt64(DoctorID.Text == "" ? "0" : DoctorID.Text);
                    ObjDetails.TestCenterID = Convert.ToInt32(TestcenterID.Text == "" ? "0" : TestcenterID.Text);
                    ObjDetails.UrgencyID = Convert.ToInt32(Urgency.Text == "" ? "0" : Urgency.Text);
                    ObjDetails.ServiceCategoryID = Convert.ToInt32(ServiceCategory.Text == "" ? "0" : ServiceCategory.Text);
                    ObjDetails.ServiceTypeID = Convert.ToInt32(ServiceType.Text == "" ? "0" : ServiceType.Text);
                    ObjDetails.SubGroupID = Convert.ToInt32(Subgroup.Text == "" ? "0" : Subgroup.Text);
                    ObjDetails.isMsbDoctor = Convert.ToInt32(hdIsMsbDoctor.Value == "" ? "0" : hdIsMsbDoctor.Value);
                    ObjDetails.isMsbPatient = Convert.ToInt32(hdMsbPc.Value == "" ? "0" : hdMsbPc.Value) > 0 ? 1 : 0;
                    DateTime servdate = servicedate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(servicedate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                    string serviceTime = txt_servicetimepicker.Text.Trim();
                    ObjDetails.ServiceDate = Convert.ToDateTime(servdate.ToString("yyyy-MM-dd") + " " + serviceTime);
                    ObjDetails.MsbPc = Convert.ToInt32(hdIsMsbDoctor.Value == "" ? "0" : hdIsMsbDoctor.Value) == 1 ? Convert.ToInt32(hdMsbPc.Value == "" ? "0" : hdMsbPc.Value) : 0;
                    Listser.Add(ObjDetails);
                }
                objrec.XMLData = XmlConvertor.IPServiceRecordDatatoXML(Listser).ToString();
                objrec.IPNo = txt_autoipno.Text == "" ? "0" : txt_autoipno.Text;
                objrec.ServiceTypeID = 2;
                objrec.FinancialYearID = LogData.FinancialYearID;
                objrec.EmployeeID = LogData.EmployeeID;
                objrec.HospitalID = LogData.HospitalID;
                objrec.IPaddress = LogData.IPaddress;
                objrec.ActionType = Enumaction.Insert;
                objrec.UHID = Convert.ToInt64(hdnUHID.Value == "" ? "0" : hdnUHID.Value);
                objrec.ServiceCategoryID = Convert.ToInt32(ddl_servicecategory.SelectedValue == "" ? "0" : ddl_servicecategory.SelectedValue);
                Listser = objiprecBO.UpdateIPServiceRecord(objrec);
                if (Listser[0].ResultOutput.ToString() == "1")
                {
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    Session["IPServiceList"] = null;
                    txt_innumber.Text = Listser[0].InvNumber;
                    hdngroupnumber.Value = Listser[0].GroupNumber;
                    ddldepartment.SelectedIndex = 0;
                    gvipservicerecordlist.DataSource = null;
                    gvipservicerecordlist.Visible = false;
                    Commonfunction.Insertzeroitemindex(ddldoctor);
                    ddl_servicecategory.SelectedIndex = 0;
                    ddl_servicecategory.Attributes.Remove("disabled");
                    btnsave.Attributes["disabled"] = "disabled";
                    btn_print.Attributes.Remove("disabled");
                }
                else
                {
                    txt_innumber.Text = "";
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
        protected void gvipservicerecordlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvipservicerecordlist.Rows[i];
                    List<IPServiceRecordData> ItemList = Session["IPServiceList"] == null ? new List<IPServiceRecordData>() : (List<IPServiceRecordData>)Session["IPServiceList"];
                    if (ItemList.Count > 0)
                    {
                        int qty = ItemList[i].Quantity;
                        txttotalquantity.Text = (Convert.ToInt32(txttotalquantity.Text == "" ? "0" : txttotalquantity.Text) - qty).ToString();
                    }
                    ItemList.RemoveAt(i);
                    Session["IPServiceList"] = ItemList;
                    gvipservicerecordlist.DataSource = ItemList;
                    gvipservicerecordlist.DataBind();
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
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txt_autoipno.Text = "";
            txtname.Text = "";
            txt_address.Text = "";
            txtservicecharge.Text = "";
            txtservices.Text = "";
            txtquantity.Text = "";
            Session["IPServiceList"] = null;
            gvipservicerecordlist.DataSource = null;
            gvipservicerecordlist.DataBind();
            gvipservicerecordlist.Visible = false;
            lblmessage.Visible = false;
            div1.Visible = false;
            txtservices.Text = "";
            txtquantity.Text = "";
            txttotalquantity.Text = "";
            ddldoctor.SelectedIndex = 0;
            txtservices.ReadOnly = false;
            ddldoctor.SelectedIndex = 0;
            ddl_servicecategory.SelectedIndex = 0;
            ddl_servicecategory.Attributes.Remove("disabled");
            ddldepartment.SelectedIndex = 0;
            txt_innumber.Text = "";
            hdngroupID.Value = null;

        }
        protected void gvipservicerecordlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSerial = (Label)e.Row.FindControl("lblserialID");
                lblSerial.Text = ((gvipservicerecordlist.PageIndex * gvipservicerecordlist.PageSize) + e.Row.RowIndex + 1).ToString();
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
        protected void gvipservicerecord_RowDataBound(object sender, GridViewRowEventArgs e)
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
            txtautoIPNo.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            ddl_servicetypes.SelectedIndex = 0;
            ddlstatus.SelectedIndex = 0;
            gvipservicerecord.DataSource = null;
            gvipservicerecord.DataBind();
            gvipservicerecord.Visible = false;
            lblresult.Visible = false;
            txtpatientNames.Text = "";
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage2.Visible = false;
            lblresult.Visible = false;
            div1.Visible = false;
            div1.Visible = false;
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetServiceNumber(string prefixText, int count, string contextKey)
        {
            IPServiceRecordData Objpaic = new IPServiceRecordData();
            IPServiceRecordBO objBO = new IPServiceRecordBO();
            List<IPServiceRecordData> getResult = new List<IPServiceRecordData>();
            Objpaic.ServiceNumber = prefixText;
            Objpaic.IPNo = contextKey;
            getResult = objBO.Getipservicenumber(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ServiceNumber.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetInvNumber(string prefixText, int count, string contextKey)
        {

            IPServiceRecordData Objpaic = new IPServiceRecordData();
            IPServiceRecordBO objBO = new IPServiceRecordBO();
            List<IPServiceRecordData> getResult = new List<IPServiceRecordData>();
            Objpaic.InvNumber = prefixText;
            Objpaic.IPNo = contextKey;
            getResult = objBO.GetIpinvnumber(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].InvNumber.ToString());
            }
            return list;
        }
        protected void txtautoIPNo_TextChanged(object sender, EventArgs e)
        {
            if (txtautoIPNo.Text != "")
            {
                txtpatientNames.Text = "";
                bindgrid();
                AutoCompleteExtender4.ContextKey = txtautoIPNo.Text.ToString();
                AutoCompleteExtender5.ContextKey = txtautoIPNo.Text.ToString();
            }
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
            bindgrid();
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

                if (txtautoIPNo.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "IPNo", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    txtautoIPNo.Focus();
                    return;
                }
                else
                {
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
                List<IPServiceRecordData> objdeposit = GetIPDServiceList(0);
                if (objdeposit.Count > 0)
                {
                    gvipservicerecord.DataSource = objdeposit;
                    gvipservicerecord.DataBind();
                    gvipservicerecord.Visible = true;
                    txtpatientNames.Text = objdeposit[0].PatientName.ToString();
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    gvipservicerecord.DataSource = null;
                    gvipservicerecord.DataBind();
                    gvipservicerecord.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult.Visible = false;
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
        public List<IPServiceRecordData> GetIPDServiceList(int curIndex)
        {
            IPServiceRecordData objpat = new IPServiceRecordData();
            IPServiceRecordBO objBO = new IPServiceRecordBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.IPNo = txtautoIPNo.Text.Trim() == "" ? null : txtautoIPNo.Text.Trim();
            objpat.PatientName = txtpatientNames.Text == "" ? null : txtpatientNames.Text.Trim();
            objpat.ServiceNumber = txt_servicenumber.Text == "" ? "" : txt_servicenumber.Text.Trim();
            objpat.InvNumber = txt_searchinvnumber.Text == "" ? "" : txt_searchinvnumber.Text.Trim();
            objpat.ServiceCategoryID = Convert.ToInt32(ddl_servicetypes.SelectedValue == "" ? "0" : ddl_servicetypes.SelectedValue);
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objpat.DateFrom = from;
            objpat.DateTo = To;
            objpat.AmountEnable = LogData.AmountEnable;
            return objBO.GetIPDServiceList(objpat);
        }
        protected void gvipservicerecord_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    IPServiceRecordData objadmin = new IPServiceRecordData();
                    IPServiceRecordBO obadminBO = new IPServiceRecordBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvipservicerecord.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_recordID");
                    Label SerialID = (Label)gr.Cells[0].FindControl("lbl_serialID");
                    Label IPNo = (Label)gr.Cells[0].FindControl("lblIPNo");
                    Label name = (Label)gr.Cells[0].FindControl("lblname");
                    Label service = (Label)gr.Cells[0].FindControl("lblservices");
                    Label charge = (Label)gr.Cells[0].FindControl("lblcharges");
                    Label quantity = (Label)gr.Cells[0].FindControl("lblquantity");
                    Label netservicecharge = (Label)gr.Cells[0].FindControl("lblamount");
                    Label addedby = (Label)gr.Cells[0].FindControl("lbladdedby");
                    Label addeddate = (Label)gr.Cells[0].FindControl("lbladt");
                    Label servicenumber = (Label)gr.Cells[0].FindControl("lbl_servicenumber");
                    Label invnumber = (Label)gr.Cells[0].FindControl("lblinvnumber");
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

                    objadmin.IPNo = IPNo.Text == "" ? "" : IPNo.Text;
                    objadmin.InvNumber = invnumber.Text == "" ? "" : invnumber.Text;
                    objadmin.ServiceNumber = servicenumber.Text == "" ? "" : servicenumber.Text;
                    objadmin.SerialID = Convert.ToInt32(SerialID.Text == "" ? "0" : SerialID.Text);
                    objadmin.ID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    objadmin.EmployeeID = LogData.UserLoginId;
                    objadmin.EmployeeID = LogData.EmployeeID;
                    objadmin.HospitalID = LogData.HospitalID;
                    objadmin.IPaddress = LogData.IPaddress;

                    int Result = obadminBO.DeleteIPDServiceRecordByIPNo(objadmin);
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
                    GridViewRow gp = gvipservicerecord.Rows[j];
                    Label GroupNumber = (Label)gp.Cells[0].FindControl("lbl_groupnumber");

                    string url = "../MedIPD/Reports/ReportViewer.aspx?option=IPservicerecpt&Grpnumber=" + GroupNumber.Text + "&Actiontype=" + 2;
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
            List<IPServiceRecordData> ServiceDetails = GetIPDServiceList(0);
            List<IPDServiceListDataTOeXCEL> ListexcelData = new List<IPDServiceListDataTOeXCEL>();
            int i = 0;
            foreach (IPServiceRecordData row in ServiceDetails)
            {
                IPDServiceListDataTOeXCEL Ecxeclpat = new IPDServiceListDataTOeXCEL();
                Ecxeclpat.IPNo = ServiceDetails[i].IPNo;
                Ecxeclpat.UHID = ServiceDetails[i].UHID;
                Ecxeclpat.PatientName = ServiceDetails[i].PatientName;
                Ecxeclpat.ServiceName = ServiceDetails[i].ServiceName;
                Ecxeclpat.ServiceCharge = ServiceDetails[i].ServiceCharge;
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
                    gvipservicerecordlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvipservicerecordlist.Columns[9].Visible = false;
                    gvipservicerecordlist.Columns[10].Visible = false;
                    gvipservicerecordlist.RenderControl(hw);
                    gvipservicerecordlist.HeaderRow.Style.Add("width", "15%");
                    gvipservicerecordlist.HeaderRow.Style.Add("font-size", "10px");
                    gvipservicerecordlist.Style.Add("text-decoration", "none");
                    gvipservicerecordlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvipservicerecordlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=OPDBillDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=IPDServiceRecordDetails.xlsx");
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
        protected void gvipservicerecord_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvipservicerecord.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        protected void btnprint_Click(object sender, EventArgs e)
        {
            string url = "../MedIPD/Reports/ReportViewer.aspx?option=IPservicerecpt&Grpnumber=" + hdngroupnumber.Value + "&Actiontype=" + 2;
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }

    }

}
