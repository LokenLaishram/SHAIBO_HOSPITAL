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
using Mediqura.CommonData.AdmissionData;
using Mediqura.BOL.AdmissionBO;

namespace Mediqura.Web.MedBills
{
    public partial class IPLabServiceRecord : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                // txttotalquantity.Text = "0";
                Session["LabServiceList"] = null;
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            //Commonfunction.PopulateDdl(ddl_servicetype, mstlookup.GetLookupsList(LookupName.LABServiceType));
            //ddl_servicetype.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddl_servicetypes, mstlookup.GetLookupsList(LookupName.LABServiceType));
            ddl_servicetypes.SelectedIndex = 1;
            Commonfunction.Insertzeroitemindex(ddldoctor);
            btnsave.Attributes["disabled"] = "disabled";
            // ddl_servicetype.Attributes["disabled"] = "disabled";
            ddl_servicetypes.Attributes["disabled"] = "disabled";
            Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.OPDepartment));
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
                list.Add(getResult[i].DetailIpnumber.ToString());
            }
            return list;
        }
        protected void txt_autoipno_TextChanged(object sender, EventArgs e)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.IPNo = txt_autoipno.Text.Substring(txt_autoipno.Text.LastIndexOf(':') + 1).Trim();
            getResult = objInfoBO.GetPatientDetailsByIPNo(Objpaic);
            if (getResult.Count > 0)
            {
                txt_age.Text = getResult[0].Agecount.ToString();
                txt_patcategory.Text = getResult[0].PatientCategory.ToString();
                txt_tpacompany.Text = getResult[0].TPAcompanyName.ToString();
                txt_beddetails.Text = getResult[0].BedDetail.ToString();
            }
            else
            {
                txt_age.Text = "";
                txt_patcategory.Text = "";
                txt_tpacompany.Text = "";
                txt_beddetails.Text = "";
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetLabServices(string prefixText, int count, string contextKey)
        {
            LabServiceMasterData Objpaic = new LabServiceMasterData();
            LabServiceMasterBO objInfoBO = new LabServiceMasterBO();
            List<LabServiceMasterData> getResult = new List<LabServiceMasterData>();
            Objpaic.TestName = prefixText;
            getResult = objInfoBO.GetLabServices(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].TestName.ToString());
            }
            return list;
        }
        protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldepartment.SelectedIndex > 0)
            {
                ddldoctor.Attributes.Remove("disabled");
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddldoctor, mstlookup.GetDepartmentDoctor(Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue)));
            }
            else
            {
                ddldoctor.Attributes["disabled"] = "disabled";

            }
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
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
            if (txt_labservices.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Testname", 0);
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
            if (txt_labservicecharge.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "", 0);
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

            string ID;
            var source = txt_labservices.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                // Check Duplicate data 
                foreach (GridViewRow row in gviplabservicelist.Rows)
                {
                    Label ServiceID = (Label)gviplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    if (Convert.ToInt32(ServiceID.Text) == Convert.ToInt32(ID))
                    {
                        Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_labservices.Text = "";
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
                txt_labservices.Text = "";
                return;
            }

            List<LabBillingData> LabServiceList = Session["LabServiceList"] == null ? new List<LabBillingData>() : (List<LabBillingData>)Session["LabServiceList"];
            LabBillingData ObjService = new LabBillingData();
            ObjService.LabServiceCharge = Convert.ToDecimal(txt_labservicecharge.Text.ToString() == "" ? "0" : txt_labservicecharge.Text.ToString());
            ObjService.DocID = Convert.ToInt64(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
            int qty = Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text) <= 0 ? 1 : Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text);
            ObjService.Quantity = qty;
            ObjService.ID = Convert.ToInt32(ID);
            ObjService.NetLabServiceCharge = Convert.ToDecimal(txt_labservicecharge.Text.ToString() == "" ? "0" : txt_labservicecharge.Text.ToString()) * qty;
            ObjService.TestName = lblservicename.Text.Trim();
            // txttotalquantity.Text = (Convert.ToInt32(txttotalquantity.Text == "" ? "0" : txttotalquantity.Text) + Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text)).ToString();
            LabServiceList.Add(ObjService);
            if (LabServiceList.Count > 0)
            {
                gviplabservicelist.DataSource = LabServiceList;
                gviplabservicelist.DataBind();
                gviplabservicelist.Visible = true;
                Session["LabServiceList"] = LabServiceList;
                txt_labservices.Text = "";
                txt_labservicecharge.Text = "";
                txtquantity.Text = "";
                txt_labservices.ReadOnly = false;
                txt_labservices.Focus();
                btnsave.Attributes.Remove("disabled");
            }
            else
            {
                gviplabservicelist.DataSource = null;
                gviplabservicelist.DataBind();
                gviplabservicelist.Visible = true;
                txt_labservices.ReadOnly = true;
                //ddl_servicetype.Attributes["disabled"] = "disabled";
            }
        }
        protected void txt_labservices_TextChanged(object sender, EventArgs e)
        {
            var source = txt_labservices.Text.ToString();
            if (source.Contains(":"))
            {
                string ID = source.Substring(source.LastIndexOf(':') + 1);

                OPDbillingBO ObjbillBO = new OPDbillingBO();
                LabBillingData ObjBillData = new LabBillingData();
                ObjBillData.ID = Convert.ToInt32(ID == "" ? "0" : ID);
                List<LabBillingData> result = ObjbillBO.GetLabServiceChargeByID(ObjBillData);
                if (result.Count > 0)
                {
                    txt_labservicecharge.Text = Commonfunction.Getrounding(result[0].LabServiceCharge.ToString());
                    txt_labservices.ReadOnly = true;
                    lblservicename.Text = result[0].TestName.ToString();
                    txtquantity.Text = "1";
                    txt_labservicecharge.Focus();
                    txt_labservices.ReadOnly = true;
                }
                else
                {
                    txt_labservicecharge.Text = "0.0";
                    txtquantity.Text = "0";
                    txt_labservices.Text = "";
                    txt_labservices.ReadOnly = true;
                }
            }
        }
        protected void gviplabservicelist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSerial = (Label)e.Row.FindControl("lblserialID");
                DropDownList ddltestcenter = (DropDownList)e.Row.FindControl("ddl_testcenter");
                DropDownList ddlurgencyState = (DropDownList)e.Row.FindControl("ddl_urgency");
                lblSerial.Text = ((gviplabservicelist.PageIndex * gviplabservicelist.PageSize) + e.Row.RowIndex + 1).ToString();
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddltestcenter, mstlookup.GetLookupsList(LookupName.TestCenter));
                ddltestcenter.SelectedIndex = 1;
                Commonfunction.PopulateDdl(ddlurgencyState, mstlookup.GetLookupsList(LookupName.Urgency));
                ddlurgencyState.SelectedIndex = 1;
            }
        }
        protected void gviplabservicelist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gviplabservicelist.Rows[i];
                    List<LabBillingData> ItemList = Session["LabServiceList"] == null ? new List<LabBillingData>() : (List<LabBillingData>)Session["LabServiceList"];
                    int Qty = ItemList[0].Quantity;
                    ItemList.RemoveAt(i);
                    if (ItemList.Count > 0)
                    {
                        Session["LabServiceList"] = ItemList;
                        gviplabservicelist.DataSource = ItemList;
                        gviplabservicelist.DataBind();
                    }
                    else
                    {
                        btnsave.Attributes["disabled"] = "disabled";
                        Session["LabServiceList"] = ItemList;
                        gviplabservicelist.DataSource = ItemList;
                        gviplabservicelist.DataBind();
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

            List<LabBillingData> Listbill = new List<LabBillingData>();

            List<IPServiceRecordData> Listser = new List<IPServiceRecordData>();
            IPServiceRecordBO objiprecBO = new IPServiceRecordBO();
            IPServiceRecordData objrec = new IPServiceRecordData();

            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in gviplabservicelist.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label Particulars = (Label)gviplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lbllabparticulars");
                    Label amount = (Label)gviplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lbl_labcharges");
                    Label DoctorID = (Label)gviplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lbl_doctorID");
                    Label qty = (Label)gviplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblquantity");
                    Label NetCharge = (Label)gviplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblnetcharges");
                    Label SerialID = (Label)gviplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblserialID");
                    Label ID = (Label)gviplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    DropDownList Testcenter = (DropDownList)gviplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("ddl_testcenter");
                    DropDownList urgency = (DropDownList)gviplabservicelist.Rows[row.RowIndex].Cells[0].FindControl("ddl_urgency");
                    LabBillingData ObjDetails = new LabBillingData();

                    ObjDetails.TestName = Particulars.Text == "" ? null : Particulars.Text;
                    ObjDetails.LabServiceCharge = Convert.ToDecimal(amount.Text == "" ? "0" : amount.Text);
                    ObjDetails.Quantity = Convert.ToInt32(qty.Text == "" ? "0" : qty.Text);
                    ObjDetails.DocID = Convert.ToInt32(DoctorID.Text == "" ? "0" : DoctorID.Text);
                    ObjDetails.NetLabServiceCharge = Convert.ToDecimal(NetCharge.Text == "" ? "0" : NetCharge.Text);
                    ObjDetails.SerialID = Convert.ToInt32(SerialID.Text == "" ? "0" : SerialID.Text);
                    ObjDetails.LabServiceID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    ObjDetails.TestCenterID = Convert.ToInt32(Testcenter.SelectedValue == "" ? "0" : Testcenter.SelectedValue);
                    ObjDetails.UrgencyID = Convert.ToInt32(urgency.SelectedValue == "" ? "0" : urgency.SelectedValue);
                    Listbill.Add(ObjDetails);
                }
                objrec.XMLData = XmlConvertor.IPLabServiceRecordDatatoXML(Listbill).ToString();
                objrec.IPNo = txt_autoipno.Text.Substring(txt_autoipno.Text.LastIndexOf(':') + 1).Trim();
                objrec.FinancialYearID = LogData.FinancialYearID;
                objrec.EmployeeID = LogData.EmployeeID;
                objrec.Remarks = txt_remarks.Text.Trim();
                //objrec.ServiceTypeID = Convert.ToInt32(ddl_servicetype.Text == "" ? "0" : ddl_servicetype.Text);
                objrec.HospitalID = LogData.HospitalID;
                objrec.IPaddress = LogData.IPaddress;
                objrec.ActionType = Enumaction.Insert;
                List<IPServiceRecordData> result = objiprecBO.UpdateIPLabServiceRecord(objrec);
                if (result.Count > 0)
                {
                    txt_investigationnumber.Text = result[0].InvNumber.ToString();
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    txt_autoipno.Text = "";
                    Session["LabServiceList"] = null;
                    btnsave.Attributes["disabled"] = "disabled";
                }
                else
                {
                    txt_investigationnumber.Text = "";
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
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txt_autoipno.Text = "";
            txt_labservicecharge.Text = "";
            txt_labservices.Text = "";
            txtquantity.Text = "";
            ddldoctor.SelectedIndex = 0;
            Session["LabServiceList"] = null;
            gviplabservicelist.DataSource = null;
            gviplabservicelist.DataBind();
            gviplabservicelist.Visible = false;
            lblmessage.Visible = false;
            div1.Visible = false;
            txt_labservices.Text = "";
            txtquantity.Text = "";
            // txttotalquantity.Text = "";
            ddldoctor.SelectedIndex = 0;
            div1.Visible = true;
            div1.Attributes["class"] = "Blank";
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
        protected void txtautoIPNo_TextChanged(object sender, EventArgs e)
        {
            if (txtautoIPNo.Text != "")
            {
                bindgrid();
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
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtautoIPNo.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            ddlstatus.SelectedIndex = 0;
            gviplabservicerecord.DataSource = null;
            gviplabservicerecord.DataBind();
            gviplabservicerecord.Visible = false;
            lblresult.Visible = false;
            txtpatientNames.Text = "";
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage2.Visible = false;
            lblresult.Visible = false;
            div1.Visible = false;
            div1.Visible = false;
            btnsave.Attributes["disabled"] = "disabled";
        }
        protected void gviplabservicerecord_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gviplabservicerecord.PageIndex = e.NewPageIndex;
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
                }

                if (txtdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDate", 0);
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
                        Messagealert_.ShowMessage(lblmessage2, "ValidDate", 0);
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
                List<IPServiceRecordData> objdeposit = GetIPDLabServiceList(0);
                if (objdeposit.Count > 0)
                {
                    gviplabservicerecord.DataSource = objdeposit;
                    gviplabservicerecord.DataBind();
                    gviplabservicerecord.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    gviplabservicerecord.DataSource = null;
                    gviplabservicerecord.DataBind();
                    gviplabservicerecord.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult.Visible = false;
                    div1.Visible = false;
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
        public List<IPServiceRecordData> GetIPDLabServiceList(int curIndex)
        {
            IPServiceRecordData objpat = new IPServiceRecordData();
            IPServiceRecordBO objBO = new IPServiceRecordBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.IPNo = txtautoIPNo.Text == "" ? null : txtautoIPNo.Text.Trim();
            objpat.PatientName = txtpatientNames.Text == "" ? null : txtpatientNames.Text.Trim();
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            //objpat.ServiceTypeID = Convert.ToInt32(ddl_servicetypes.SelectedValue == "" ? "0" : ddl_servicetypes.SelectedValue);
            objpat.ServiceTypeID = 3;
            objpat.DateFrom = from;
            objpat.DateTo = To;
            return objBO.GetIPDLabServiceList(objpat);
        }
        protected void gviplabservicerecord_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    GridViewRow gr = gviplabservicerecord.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    Label IPNo = (Label)gr.Cells[0].FindControl("lblIPNo");
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
                    objadmin.IPNo = IPNo.Text == "" ? "" : IPNo.Text.Trim();
                    objadmin.ID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                    objadmin.ServiceTypeID = Convert.ToInt32(ddl_servicetypes.SelectedValue == "" ? "0" : ddl_servicetypes.SelectedValue);
                    objadmin.EmployeeID = LogData.UserLoginId;
                    objadmin.EmployeeID = LogData.EmployeeID;
                    objadmin.HospitalID = LogData.HospitalID;
                    objadmin.IPaddress = LogData.IPaddress;

                    int Result = obadminBO.DeleteIPDLabServiceRecordByIPNo(objadmin);
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

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<IPServiceRecordData> ServiceDetails = GetIPDLabServiceList(0);
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
                divmsg3.Visible = true;
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
                    gviplabservicerecord.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gviplabservicerecord.Columns[10].Visible = false;
                    gviplabservicerecord.Columns[1].Visible = false;
                    gviplabservicerecord.RenderControl(hw);
                    gviplabservicerecord.HeaderRow.Style.Add("width", "15%");
                    gviplabservicerecord.HeaderRow.Style.Add("font-size", "10px");
                    gviplabservicerecord.Style.Add("text-decoration", "none");
                    gviplabservicerecord.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gviplabservicerecord.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=IPDLabServiceDetails.pdf");
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
                wb.Worksheets.Add(dt, "Deposit Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=IPDLabServiceRecordDetails.xlsx");
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
    }
}