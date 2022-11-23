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
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.CommonData.AdmissionData;
using Mediqura.BOL.AdmissionBO;

namespace Mediqura.Web.MedIPD
{
    public partial class DueCollectionList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                btnprints.Attributes["disabled"] = "disabled";
                if (LogData.RoleID != 1)
                {
                    //ddl_settlementMode.Attributes["disabled"] = "disabled";
                }
                txtremarkdisc.ReadOnly = true;
            }
        }
        private void bindddl()
        {
            btnaddresponsibility.Visible = true;
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlpatienttype, mstlookup.GetLookupsList(LookupName.DuePatientType));
            Commonfunction.PopulateDdl(ddlPatientTypeList, mstlookup.GetLookupsList(LookupName.DuePatientType));
            Commonfunction.PopulateDdl(ddlpaymentmode, mstlookup.GetLookupsList(LookupName.PaymentMode));
            Commonfunction.PopulateDdl(ddlcollectedby, mstlookup.GetLookupsList(LookupName.CollectedBy));
            Commonfunction.PopulateDdl(ddl_responsiblestaff, mstlookup.GetLookupsList(LookupName.Employee));
            AutoCompleteExtender2.ContextKey = "0";
            ddlpaymentmode.SelectedIndex = 1;
            Session["Responsibilty"] = null;
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid(1);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIpnoEmrgNo(string prefixText, int count, string contextKey)
        {
            IPData Objpaic = new IPData();
            AdmissionBO objInfoBO = new AdmissionBO();
            List<IPData> getResult = new List<IPData>();
            Objpaic.IPNo = prefixText;
            Objpaic.PatientType = Convert.ToInt32(contextKey);
            getResult = objInfoBO.getIPNoNemrgNo(Objpaic);
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
            Objpaic.PatientType = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetDuePatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetDueBill(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.BillNo = prefixText;
            Objpaic.PatientType = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetDueBill(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].BillNo.ToString());
            }
            return list;
        }
        protected void bindgrid(int page)
        {
            try
            {
                if (LogData.SearchEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddlpatienttype.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "PatientType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddlpatienttype.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    divmsg1.Visible = false;
                }
                List<DueCollectionData> objdeposit = GetPatientDueCollectionList(page);
                if (objdeposit.Count > 0)
                {
                    gvduecollectionlist.VirtualItemCount = objdeposit[0].MaximumRows;//total item is required for custom paging
                    gvduecollectionlist.PageIndex = page - 1;

                    gvduecollectionlist.DataSource = objdeposit;
                    gvduecollectionlist.DataBind();
                    gvduecollectionlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    txt_totaldueamt.Text = Commonfunction.Getrounding(objdeposit[0].TotalDueAmount.ToString());
                    txt_totlastduepaid.Text = Commonfunction.Getrounding(objdeposit[0].TotalLastPaid.ToString());
                    txt_Totduebalance.Text = Commonfunction.Getrounding(objdeposit[0].TotalDueBalance.ToString());
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;

                }
                else
                {
                    gvduecollectionlist.DataSource = null;
                    gvduecollectionlist.DataBind();
                    gvduecollectionlist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Attributes["class"] = "SucessAlert";
                divmsg1.Visible = true;
            }
        }
        public List<DueCollectionData> GetPatientDueCollectionList(int curIndex)
        {
            DueCollectionData objpat = new DueCollectionData();
            DueCollectionBO objbillingBO = new DueCollectionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            Session["Responsibilty"] = null;
            objpat.PatientCategory = Convert.ToInt32(ddlpatienttype.SelectedValue == "" ? "0" : ddlpatienttype.SelectedValue);
            objpat.IPNo = "";
            objpat.UHID = Commonfunction.SemicolonSeparation_String_64(txtpatientNames.Text);
            objpat.BillNo = txt_billnos.Text.ToString() == "" ? "" : txt_billnos.Text.ToString();
            objpat.CurrentIndex = curIndex;
            return objbillingBO.GetPatientDueCollectionList(objpat);
        }

        public List<DueCollectionData> GetDueCollectionList(int curIndex)
        {
            DueCollectionData objpat = new DueCollectionData();
            DueCollectionBO objbillingBO = new DueCollectionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            objpat.PatientCategory = Convert.ToInt32(ddlpatienttype.SelectedValue == "" ? "0" : ddlpatienttype.SelectedValue);
            return objbillingBO.GetDueCollectionList(objpat);
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (LogData.SaveEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "SaveEnable", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                DueCollectionData objData = new DueCollectionData();
                DueCollectionBO objBO = new DueCollectionBO();
                if (ddl_settlementMode.SelectedIndex == 0)
                {
                    if (txtPaid.Text.Trim() == "" || txtPaid.Text.Trim() == "0")
                    {
                        Messagealert_.ShowMessage(lblmessage2, "Please enter amount.", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        txtPaid.Text = "";
                        txtPaid.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage2.Visible = false;
                    }
                    if (Convert.ToDecimal(txtPaid.Text == "" ? "0.0" : txtPaid.Text) > Convert.ToDecimal(txt_currentdue.Text == "" ? "0.0" : txt_currentdue.Text))
                    {
                        Messagealert_.ShowMessage(lblmessage2, "Paid amount could not be greater than due amount.", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        txtPaid.Text = "";
                        txtPaid.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage2.Visible = false;
                    }
                    objData.UHID = Convert.ToInt64(lbl_uhid.Text == "" ? "" : lbl_uhid.Text);
                    objData.IPNo = txt_ipnumber.Text == "" ? "" : txt_ipnumber.Text;
                    objData.BillNo = txt_billnumber.Text == "" ? "" : txt_billnumber.Text;
                    objData.TotalDueAmount = Convert.ToDecimal(txt_totaldueamt.Text == "" ? "0.0" : txt_totaldueamt.Text);
                    if (txt_duepaidlast.Text == "0.00")
                    {
                        objData.LastDuePaid = Convert.ToDecimal(txtPaid.Text == "" ? "0.0" : txtPaid.Text);
                    }
                    else
                    {
                        objData.LastDuePaid = Convert.ToDecimal(txtPaid.Text == "" ? "0.0" : txtPaid.Text);
                    }
                    objData.DueBalance = Convert.ToDecimal(txt_duelist.Text == "" ? "0.0" : txt_duelist.Text);
                    objData.Paidamnt = Convert.ToDecimal(txtPaid.Text == "" ? "0.0" : txtPaid.Text);
                    objData.PaymentMode = Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue);
                    objData.BankName = txtbank.Text == "" ? null : txtbank.Text;
                    objData.Chequenumber = txt_chequenumber.Text == "" ? "" : txt_chequenumber.Text;
                    objData.Invoicenumber = txtinvoicenumber.Text == "" ? "" : txtinvoicenumber.Text;
                    objData.BankID = Convert.ToInt32(hdnbankID.Value == "" || hdnbankID.Value == null ? "0" : hdnbankID.Value);
                    objData.PatientCategory = Convert.ToInt32(ddlpatienttype.SelectedValue == "" ? "0" : ddlpatienttype.SelectedValue);
                    objData.SettlementModeID = Convert.ToInt32(ddl_settlementMode.SelectedValue == "" ? "0" : ddl_settlementMode.SelectedValue);
                    objData.FinancialYearID = LogData.FinancialYearID;
                    objData.EmployeeID = LogData.EmployeeID;
                    objData.AddedBy = LogData.AddedBy;
                    objData.HospitalID = LogData.HospitalID;
                    objData.IsActive = LogData.IsActive;
                    objData.IPaddress = LogData.IPaddress;
                    objData.CurrentDue = Convert.ToDecimal(txt_currentdue.Text == "" ? "0.0" : txt_currentdue.Text);
                    List<DueCollectionData> result = objBO.UpdateDueDetail(objData);
                    if (result.Count > 0)
                    {
                        lbl_bill.InnerText = "Due Receipt Number";
                        txtDueBillNo.Text = result[0].BillNo.ToString();
                        hdnbillnumberDue.Value = result[0].BillNo.ToString();
                        bindgrid(1);
                        Messagealert_.ShowMessage(lblmessage2, "save", 1);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "SucessAlert";
                        btnsave.Attributes["disabled"] = "disabled";
                        Session["Responsibilty"] = null;

                    }
                }
                else
                {
                    if (txtPaid.Text.Trim() == "" || txtPaid.Text.Trim() == "0")
                    {
                        Messagealert_.ShowMessage(lblmessage2, "Please enter amount.", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        txtPaid.Text = "";
                        txtPaid.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage2.Visible = false;
                    }
                    if (Convert.ToDecimal(txtPaid.Text == "" ? "0.0" : txtPaid.Text) > Convert.ToDecimal(txt_currentdue.Text == "" ? "0.0" : txt_currentdue.Text))
                    {
                        Messagealert_.ShowMessage(lblmessage2, "Discount amount could not be greater than due amount.", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        txtPaid.Text = "";
                        txtPaid.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage2.Visible = false;
                    }
                    objData.UHID = Convert.ToInt64(lbl_uhid.Text == "" ? "" : lbl_uhid.Text);
                    objData.IPNo = txt_ipnumber.Text == "" ? "" : txt_ipnumber.Text;
                    objData.BillNo = txt_billnumber.Text == "" ? "" : txt_billnumber.Text;
                    objData.Discount = Convert.ToDecimal(txtPaid.Text == "" ? "0.0" : txtPaid.Text);
                    objData.PatientCategory = Convert.ToInt32(ddlpatienttype.SelectedValue == "" ? "0" : ddlpatienttype.SelectedValue);

                    objData.DiscountRemarks = txtremarkdisc.Text == "" ? "" : txtremarkdisc.Text;
                    objData.EmployeeID = LogData.EmployeeID;
                    int count = 0;
                    List<EmployeeData> emplist = new List<EmployeeData>();
                    foreach (GridViewRow row in gv_responsibledetais.Rows)
                    {
                        Label empID = (Label)gv_responsibledetais.Rows[row.RowIndex].Cells[0].FindControl("lbl_employeeID");
                        TextBox lblAmount = (TextBox)gv_responsibledetais.Rows[row.RowIndex].Cells[0].FindControl("lblAmount");
                        EmployeeData objempdata = new EmployeeData();
                        count = count + 1;
                        objempdata.EmployeeID = Convert.ToInt64(empID.Text == "" ? "0" : empID.Text);
                        objempdata.DiscountedAmount = Convert.ToDecimal(lblAmount.Text == "" ? "0" : lblAmount.Text);
                        emplist.Add(objempdata);
                    }
                    if (count == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "Please enter the resposnible employee.", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage2.Visible = false;
                    }
                    if (txtremarkdisc.Text.Trim() == "")
                    {
                        Messagealert_.ShowMessage(lblmessage2, "Please enter remark for discount.", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        txtremarkdisc.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage2.Visible = false;
                    }
                    objData.XMLData = XmlConvertor.Dueresponsiblemployee(emplist).ToString();
                    objData.EmployeeID = LogData.EmployeeID;
                    objData.HospitalID = LogData.HospitalID;
                    objData.FinancialYearID = LogData.FinancialYearID;
                    objData.SettlementModeID = Convert.ToInt32(ddl_settlementMode.SelectedValue == "" ? "0" : ddl_settlementMode.SelectedValue);
                    int result = objBO.UpdateDueDiscount(objData);
                    if (result == 1)
                    {
                        bindgrid(1);
                        Messagealert_.ShowMessage(lblmessage2, "save", 1);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "SucessAlert";
                        btnsave.Attributes["disabled"] = "disabled";
                    }
                    else if (result == 5)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "duplicate", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        return;

                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                return;
            }
        }
        protected void btnaddresponsibility_Click(object sender, EventArgs e)
        {
            txt_totaldueamount.Text = txtPaid.Text;
            this.mddueresponsible.Show();
        }
        protected void dddl_responsiblestaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gv_responsibledetais.Rows)
            {
                Label EmployeeID = (Label)gv_responsibledetais.Rows[row.RowIndex].Cells[0].FindControl("lbl_employeeID");
                if (Convert.ToInt32(EmployeeID.Text) == Convert.ToInt32(ddl_responsiblestaff.SelectedValue == "" ? "0" : ddl_responsiblestaff.SelectedValue))
                {
                    Messagealert_.ShowMessage(message, "duplicate", 0);
                    this.mddueresponsible.Show();
                    return;
                }
                else
                {
                    message.Visible = false;
                    this.mddueresponsible.Show();
                }
            }
            List<EmployeeData> Employeelist = Session["Responsibilty"] == null ? new List<EmployeeData>() : (List<EmployeeData>)Session["Responsibilty"];
            EmployeeData objemployee = new EmployeeData();
            objemployee.EmployeeID = Convert.ToInt64(ddl_responsiblestaff.SelectedValue == "" ? "0" : ddl_responsiblestaff.SelectedValue);
            objemployee.EmpName = ddl_responsiblestaff.SelectedItem.Text;
            objemployee.DiscountedAmount = Convert.ToDecimal(txt_totaldueamount.Text == "" ? "0" : txt_totaldueamount.Text);
            Employeelist.Add(objemployee);
            if (Employeelist.Count > 0)
            {
                gv_responsibledetais.DataSource = Employeelist;
                gv_responsibledetais.DataBind();
                gv_responsibledetais.Visible = true;
                ddl_responsiblestaff.SelectedIndex = 0;
                Session["Responsibilty"] = Employeelist;
                btnaddresponsibility.Visible = true;
            }
            else
            {
                gv_responsibledetais.DataSource = Employeelist;
                gv_responsibledetais.DataBind();
                gv_responsibledetais.Visible = true;
            }
            this.mddueresponsible.Show();
        }
        protected void gv_responsibledetais_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gv_responsibledetais.Rows[i];
                    List<EmployeeData> Employeelist = Session["Responsibilty"] == null ? new List<EmployeeData>() : (List<EmployeeData>)Session["Responsibilty"];
                    Employeelist.RemoveAt(i);
                    message.Visible = false;
                    Session["DiscountList"] = Employeelist;
                    gv_responsibledetais.DataSource = Employeelist;
                    gv_responsibledetais.DataBind();
                    this.mddueresponsible.Show();
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(message, "system", 0);
            }
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Decimal TotalDue = 0; int count = 0;
            foreach (GridViewRow row in gv_responsibledetais.Rows)
            {
                TextBox lbl_amnt = (TextBox)gv_responsibledetais.Rows[row.RowIndex].Cells[0].FindControl("lblAmount");
                if (Convert.ToDecimal(lbl_amnt.Text == "" ? "0" : lbl_amnt.Text) <= 0)
                {
                    lbl_amnt.Focus();
                    Messagealert_.ShowMessage(message, "Respamount", 0);
                    this.mddueresponsible.Show();
                    return;
                }
                else
                {
                    message.Visible = false;
                }
                count = count + 1;
                TotalDue = TotalDue + Convert.ToDecimal(lbl_amnt.Text == "" ? "0" : lbl_amnt.Text);
            }
            if (TotalDue != Convert.ToDecimal(txt_totaldueamount.Text == "" ? "0" : txt_totaldueamount.Text) && count > 0)
            {
                this.mddueresponsible.Show();
                Messagealert_.ShowMessage(message, "Dueamount", 0);
                return;
            }
            else
            {
                message.Visible = false;
            }
        }
        protected void btnprint_Click(object sender, EventArgs e)
        {
            string url = "../MedIPD/Reports/ReportViewer.aspx?option=DueReceipt&ReceiptNo=" + hdnbillnumberDue.Value.ToString();
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtdatefrom.Text = "";
            txtto.Text = "";
            gvduecollectionlist.DataSource = null;
            gvduecollectionlist.DataBind();
            gvduecollectionlist.Visible = false;
            lblresult.Visible = false;
            txt_totaldueamt.Text = "";
            txt_totlastduepaid.Text = "";
            txt_Totduebalance.Text = "";
            ddlpatienttype.SelectedIndex = 0;
            txtpatientNames.Text = "";
            txt_billnos.Text = "";
            ddlexport.Visible = false;
            btnexport.Visible = false;
            Session["Responsibilty"] = null;
            AutoCompleteExtender2.ContextKey = "0";
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            gvduelist.DataSource = null;
            gvduelist.DataBind();
            gvduelist.Visible = true;
            txt_billnumber.Text = "";
            txtname.Text = "";
            txt_ipnumber.Text = "";
            txt_address.Text = "";
            txt_Age.Text = "";
            txt_admissionDate.Text = "";
            txt_department.Text = "";
            txt_doctor.Text = "";
            txt_careof.Text = "";
            txt_duelist.Text = "";
            txt_duepaidlast.Text = "";
            txtPaid.Text = "";
            txtDueBillNo.Text = "";
            ddl_settlementMode.SelectedIndex = 0;
            TabPanel1.Visible = false;
            Session["Responsibilty"] = null;
            gv_responsibledetais.DataSource = null;
            gv_responsibledetais.DataBind();
            lblmessage2.Visible = false;
            txt_currentdue.Text = "";
            txtremarkdisc.Text = "";
            txt_totaldueamount.Text = "";
            btnaddresponsibility.Visible = false;
            tabcontainerservicemaster.ActiveTabIndex = 0;
        }
        protected void btnresetsList_Click(object sender, EventArgs e)
        {
            GvDuebillist.DataSource = null;
            GvDuebillist.DataBind();
            GvDuebillist.Visible = true;
            txtpatientNamesList.Text = "";
            txt_IpnoEmrgnoList.Text = "";
            txt_billnosList.Text = "";
            txtdatefromList.Text = "";
            txttoList.Text = "";
            txttotatDueamount.Text = "";
            txtTotaDuePaid.Text = "";
            txt_totaldiscountamnt.Text = "";
            ddlPatientTypeList.SelectedIndex = 0;
            ddlcollectedby.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblresult1.Visible = false;
            div5.Visible = false;
            divmsg4.Visible = false;
            Session["Responsibilty"] = null;
            txtremarkdisc.ReadOnly = true;

        }
        protected void txtPaid_TextChanged(object sender, EventArgs e)
        {
            if (txtPaid.Text.Trim() != "" && Convert.ToDecimal(txtPaid.Text.Trim() == "" ? "0" : txtPaid.Text.Trim()) > 0)
            {
                if (Convert.ToDecimal(txtPaid.Text == "" ? "0" : txtPaid.Text) > Convert.ToDecimal(txt_currentdue.Text == "" ? "0" : txt_currentdue.Text))
                {
                    txt_totaldueamount.Text = "";
                    txtPaid.Text = "";
                    Messagealert_.ShowMessage(lblmessage2, "Amount should not be greater than current due amount.", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    txtPaid.Focus();
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                    if (txt_duepaidlast.Text.Trim() == "0.00")
                    {
                        txt_duelist.Text = Commonfunction.Getrounding((Convert.ToDecimal(lblhdnTotalDue.Text == "" ? "0.0" : lblhdnTotalDue.Text) - Convert.ToDecimal(txtPaid.Text == "" ? "0.0" : txtPaid.Text)).ToString());
                        txtPaid.Focus();
                    }
                    else
                    {
                        txt_duelist.Text = Commonfunction.Getrounding((Convert.ToDecimal(lblhdnTotalDue.Text == "" ? "0.0" : lblhdnTotalDue.Text) - Convert.ToDecimal(txtPaid.Text == "" ? "0.0" : txtPaid.Text)).ToString());
                        txtPaid.Text = Convert.ToDecimal(txtPaid.Text == "" ? "0.0" : txtPaid.Text).ToString();
                        if (ddl_settlementMode.SelectedIndex == 1)
                        {
                            txt_totaldueamount.Text = txtPaid.Text;
                        }
                        else
                        {
                            txt_totaldueamount.Text = "";
                        }
                        txt_duepaidlast.Text = Convert.ToDecimal(lblHdnLastDuePaid.Text == "" ? "0.0" : lblHdnLastDuePaid.Text).ToString();
                        txtPaid.Focus();
                    }
                }
            }
            else
            {
                txt_totaldueamount.Text = "";
            }
        }

        protected void gvduecollectionlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }

        protected void ddlpatienttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  AutoCompleteExtender1.ContextKey = ddlpatienttype.SelectedValue == "" ? "0" : ddlpatienttype.SelectedValue;
            AutoCompleteExtender2.ContextKey = ddlpatienttype.SelectedValue == "" ? "0" : ddlpatienttype.SelectedValue;
            AutoCompleteExtender3.ContextKey = ddlpatienttype.SelectedValue == "" ? "0" : ddlpatienttype.SelectedValue;
        }
        protected void ddlPatientTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender4.ContextKey = ddlPatientTypeList.SelectedValue == "" ? "0" : ddlPatientTypeList.SelectedValue;
            AutoCompleteExtender5.ContextKey = ddlPatientTypeList.SelectedValue == "" ? "0" : ddlPatientTypeList.SelectedValue;
            AutoCompleteExtender6.ContextKey = ddlPatientTypeList.SelectedValue == "" ? "0" : ddlPatientTypeList.SelectedValue;
        }
        protected void gvduecollectionlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    DueCollectionData objpat = new DueCollectionData();
                    DueCollectionBO objbillingBO = new DueCollectionBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvduecollectionlist.Rows[i];
                    Label billNo = (Label)gr.Cells[0].FindControl("lblBillNo");
                    Label category = (Label)gr.Cells[0].FindControl("lblcategory");
                    objpat.BillNo = billNo.Text;
                    objpat.PatientCategory = Convert.ToInt32(category.Text);
                    List<DueCollectionData> List = new List<DueCollectionData>();
                    List = objbillingBO.DueDetailByBillNo(objpat);
                    if (List.Count > 0)
                    {
                        tabcontainerservicemaster.ActiveTabIndex = 1;
                        gvduelist.DataSource = List;
                        gvduelist.DataBind();
                        gvduelist.Visible = true;
                        txt_duelist.Text = Commonfunction.Getrounding(List[0].TotalDueAmount.ToString());
                        lblhdnTotalDue.Text = Commonfunction.Getrounding(List[0].TotalDueAmount.ToString());
                        txt_currentdue.Text = Commonfunction.Getrounding(List[0].TotalDueBalance.ToString());
                        txtPaid.Text = "";
                        lblHdnCurrentDue.Text = Commonfunction.Getrounding(List[0].TotalDueBalance.ToString());
                        txt_duepaidlast.Text = Commonfunction.Getrounding(List[0].TotalLastPaid.ToString());
                        lblHdnLastDuePaid.Text = Commonfunction.Getrounding(List[0].TotalLastPaid.ToString());
                        lbl_uhid.Text = List[0].UHID.ToString();
                        txt_billnumber.Text = List[0].BillNo.ToString();
                        txtname.Text = List[0].PatientName.ToString();
                        //txt_ipnumber.Text = List[0].IPNo.ToString();
                        txt_address.Text = List[0].Address.ToString();
                        txt_Age.Text = List[0].Agecount.ToString();
                        TabPanel1.Visible = true;
                        txtremarkdisc.Text = "";
                        ddl_settlementMode.SelectedIndex = 0;
                        btnsave.Attributes.Remove("disabled");
                        lblmessage2.Visible = false;
                    }
                    else
                    {
                        TabPanel1.Visible = false;
                        gvduelist.DataSource = null;
                        gvduelist.DataBind();
                        gvduelist.Visible = true;
                        txt_billnumber.Text = "";
                        txtname.Text = "";
                        txt_ipnumber.Text = "";
                        txt_address.Text = "";
                        txt_Age.Text = "";
                        txt_admissionDate.Text = "";
                        txt_department.Text = "";
                        txt_doctor.Text = "";
                        txt_careof.Text = "";
                        //lbl_UHIDTemp.Text = List[0].UHID.ToString();
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
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
        protected void ddl_settlementMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_settlementMode.SelectedIndex == 0)
            {
                ddlpaymentmode.Attributes.Remove("disabled");
                txtbank.Attributes.Remove("disabled");
                txt_chequenumber.Attributes.Remove("disabled");
                txtinvoicenumber.Attributes.Remove("disabled");
                txt_duelist.Attributes.Remove("disabled");
                txt_duepaidlast.Attributes.Remove("disabled");
                txtDueBillNo.Attributes.Remove("disabled");
                btnsave.Text = "Pay Due";
                lbl_pay_disc.Text = "Pay";
                btnaddresponsibility.Visible = false;
                txtremarkdisc.ReadOnly = true;
                btnprint.Visible = true;
            }
            else
            {
                btnaddresponsibility.Visible = true;
                lbl_pay_disc.Text = "Discount";
                ddlpaymentmode.Attributes["disabled"] = "disabled";
                txtbank.Attributes["disabled"] = "disabled";
                txt_chequenumber.Attributes["disabled"] = "disabled";
                txtinvoicenumber.Attributes["disabled"] = "";
                txt_duelist.Attributes["disabled"] = "disabled";
                txtDueBillNo.Attributes["disabled"] = "disabled";
                btnsave.Text = "Discount";
                txt_totaldueamount.Text = txtPaid.Text;
                txt_totaldueamount.Text = txt_duelist.Text;
                txt_totaldueamount.Text = txtPaid.Text;
                txtremarkdisc.ReadOnly = false;
                btnprint.Visible = false;
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

        protected void btnsearchList_Click(object sender, EventArgs e)
        {
            BindDuePaymentList(1);
        }
        protected void BindDuePaymentList(int page)
        {
            try
            {

                if (LogData.SearchEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage4, "SearchEnable", 0);
                    divmsg4.Visible = true;
                    divmsg4.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage4.Visible = false;
                }
                List<DueCollectionData> objdischarge = GetPatientCollectionList(page);
                if (objdischarge.Count > 0)
                {
                    GvDuebillist.DataSource = objdischarge;
                    GvDuebillist.DataBind();
                    GvDuebillist.Visible = true;
                    Messagealert_.ShowMessage(lblresult1, "Total: " + objdischarge[0].MaximumRows.ToString() + " Record found", 1);
                    txttotatDueamount.Text = Commonfunction.Getrounding(objdischarge[0].TotalDueAmount.ToString());
                    txtTotaDuePaid.Text = Commonfunction.Getrounding(objdischarge[0].TotalLastPaid.ToString());
                    txt_totaldiscountamnt.Text = Commonfunction.Getrounding(objdischarge[0].TotalDiscountAmnt.ToString());
                    div5.Attributes["class"] = "SucessAlert";
                    div5.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
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
                    GvDuebillist.DataSource = null;
                    GvDuebillist.DataBind();
                    GvDuebillist.Visible = true;
                    lblresult1.Visible = false;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;

                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                divmsg4.Attributes["class"] = "FailAlert";
                divmsg4.Visible = true;
            }
        }
        public List<DueCollectionData> GetPatientCollectionList(int curIndex)
        {
            DueCollectionData objpat = new DueCollectionData();
            DueCollectionBO objbillingBO = new DueCollectionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefromList.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefromList.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txttoList.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txttoList.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            objpat.PatientCategory = Convert.ToInt32(ddlPatientTypeList.SelectedValue == "" ? "0" : ddlPatientTypeList.SelectedValue);
            objpat.IPNo = txt_IpnoEmrgnoList.Text.ToString() == "" ? "" : txt_IpnoEmrgnoList.Text.ToString();
            objpat.PatientName = txtpatientNamesList.Text.ToString() == "" ? "" : txtpatientNamesList.Text.ToString();
            objpat.BillNo = txt_billnosList.Text.ToString() == "" ? "" : txt_billnosList.Text.ToString();
            objpat.CurrentIndex = curIndex;
            return objbillingBO.GetPatientCollectionList(objpat);
        }
        protected void GvDuebillist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage4, "DeleteEnable", 0);
                        divmsg4.Visible = true;
                        divmsg4.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage4.Visible = false;
                    }
                    DueCollectionData objadmin = new DueCollectionData();
                    DueCollectionBO obadminBO = new DueCollectionBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvDuebillist.Rows[i];
                    Label Bill = (Label)gr.Cells[0].FindControl("lbl_billno");
                    Label UHID = (Label)gr.Cells[0].FindControl("lbluhidBill");
                    objadmin.BillNo = Bill.Text.Trim();
                    objadmin.UHID = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                    objadmin.EmployeeID = LogData.EmployeeID;
                    objadmin.HospitalID = LogData.HospitalID;
                    objadmin.IPaddress = LogData.IPaddress;
                    int Result = obadminBO.DeleteDueCollectionDetail(objadmin);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage4, "delete", 1);
                        lblmessage4.Visible = false;
                        div5.Attributes["class"] = "SucessAlert";
                        div5.Visible = true;
                        BindDuePaymentList(1);
                    }


                }
                if (e.CommandName == "Print")
                {
                    if (LogData.PrintEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage4, "PrintEnable", 0);
                        divmsg4.Visible = true;
                        divmsg4.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage4.Visible = false;
                    }
                    int j = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gp = GvDuebillist.Rows[j];
                    Label billno = (Label)gp.Cells[0].FindControl("lbl_billno");
                    string url = "../MedIPD/Reports/ReportViewer.aspx?option=DueReceipt&ReceiptNo=" + billno.Text.ToString();
                    string fullURL = "window.open('" + url + "', '_blank');";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);

                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage4, "system", 0);
                divmsg4.Visible = true;
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIpnoEmrgNoList(string prefixText, int count, string contextKey)
        {
            DueCollectionData Objpaic = new DueCollectionData();
            DueCollectionBO objInfoBO = new DueCollectionBO();
            List<DueCollectionData> getResult = new List<DueCollectionData>();
            Objpaic.IPNo = prefixText;
            Objpaic.PatientType = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetIpnoEmrgNoList(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPatientNameList(string prefixText, int count, string contextKey)
        {
            DueCollectionData Objpaic = new DueCollectionData();
            DueCollectionBO objInfoBO = new DueCollectionBO();
            List<DueCollectionData> getResult = new List<DueCollectionData>();
            Objpaic.PatientName = prefixText;
            Objpaic.PatientType = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetPatientNameList(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetDueBillList(string prefixText, int count, string contextKey)
        {
            DueCollectionData Objpaic = new DueCollectionData();
            DueCollectionBO objInfoBO = new DueCollectionBO();
            List<DueCollectionData> getResult = new List<DueCollectionData>();
            Objpaic.BillNo = prefixText;
            Objpaic.PatientType = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetDueBillList(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].BillNo.ToString());
            }
            return list;
        }
        //protected void btnprints_Click(object sender, EventArgs e)
        //{
        //    string url = "../MedIPD/Reports/ReportViewer.aspx?option=DueReceiptList&PatientType=" + Convert.ToInt32(ddlPatientTypeList.SelectedValue) + "&DateFrom=" + txtdatefromList.Text + "&DateTo=" + txttoList.Text + "&DateTo=" + objdateto.value + "&DateTo=" + objdateto.value + "&DateTo=" + objdateto.value;
        //    string fullURL = "window.open('" + url + "', '_blank');";
        //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        //}
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
                divmsg3.Visible = true;
                divmsg3.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvDuebillist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    //gvSummaryList.Columns[4].Visible = false;
                    //gvSummaryList.Columns[5].Visible = false;
                    GvDuebillist.Columns[6].Visible = false;
                    GvDuebillist.Columns[7].Visible = false;

                    GvDuebillist.RenderControl(hw);
                    GvDuebillist.HeaderRow.Style.Add("width", "15%");
                    GvDuebillist.HeaderRow.Style.Add("font-size", "10px");
                    GvDuebillist.Style.Add("text-decoration", "none");
                    GvDuebillist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvDuebillist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=DischargeList.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Patient Type Detail List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=DischargeList.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        private DataTable GetDatafromDatabase()
        {
            List<DueCollectionData> DischargealistDetails = GetPatientCollectionList(0);
            List<DueCollectionDataDataTOeXCEL> ListexcelData = new List<DueCollectionDataDataTOeXCEL>();
            int i = 0;
            foreach (DueCollectionData row in DischargealistDetails)
            {
                DueCollectionDataDataTOeXCEL ExcelSevice = new DueCollectionDataDataTOeXCEL();
                ExcelSevice.BillNo = DischargealistDetails[i].BillNo;
                ExcelSevice.UHID = DischargealistDetails[i].UHID;
                ExcelSevice.IPnoEmrgNo = DischargealistDetails[i].IPnoEmrgNo;
                ExcelSevice.PatientName = DischargealistDetails[i].PatientName;
                ExcelSevice.Address = DischargealistDetails[i].Address;
                ExcelSevice.DueAmount = DischargealistDetails[i].DueAmount;
                ExcelSevice.LastDuePaid = DischargealistDetails[i].LastDuePaid;
                ExcelSevice.DueBalance = DischargealistDetails[i].DueBalance;
                ExcelSevice.AddedBy = DischargealistDetails[i].EmpName;
                ExcelSevice.AddedDate = DischargealistDetails[i].AddedDate;
               // ExcelSevice.DiscountRemarks = DischargealistDetails[i].DiscountRemarks;
                //gvDischargeList.Columns[4].Visible = false;
                //gvDischargeList.Columns[5].Visible = false;
                //gvDischargeList.Columns[6].Visible = false;
                //gvDischargeList.Columns[7].Visible = false;
                ListexcelData.Add(ExcelSevice);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        public class ListtoDataTableConverter
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];

                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable
                return dataTable;
            }
        }

        protected void GvDuebillist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label SettleMode = (Label)e.Row.FindControl("lblSettLeMode");
                Label SettleModeID = (Label)e.Row.FindControl("lblsettlementmode");
                if (SettleModeID.Text == "0")
                {
                    SettleMode.Text = "Due Payment";
                }
                else if (SettleModeID.Text == "2")
                {
                    SettleMode.Text = "Discount";
                }

            }
        }
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            if (Commonfunction.SemicolonSeparation_String_64(txtpatientNames.Text) == 0)
            {
                txtpatientNames.Text = "";
                txtpatientNames.Focus();
                return;
            }
            bindgrid(1);
        }
    }
}