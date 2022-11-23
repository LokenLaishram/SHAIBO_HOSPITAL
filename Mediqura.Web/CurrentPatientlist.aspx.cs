using Mediqura.BOL.AdmissionBO;
using Mediqura.BOL.CommonBO;
using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.Common;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedPhr
{
    public partial class CurrentPatientlist : BasePage
    {

        static int slno;
        static int activePatient;
        static int passivePatient;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                bindgrid();
                hdnroldeID.Value = LogData.RoleID.ToString();
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetLookupsList(LookupName.WardType));
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void bindgrid()
        {
            slno = 0;
            activePatient = 0;
            passivePatient = 0;

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


                List<CurrentAdmissionListData> objdeposit = GetPatientList(0);
                if (objdeposit.Count > 0)
                {

                    gvadmissionlist.DataSource = objdeposit;
                    gvadmissionlist.DataBind();
                    gvadmissionlist.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;

                }
                else
                {
                    gvadmissionlist.DataSource = null;
                    gvadmissionlist.DataBind();
                    gvadmissionlist.Visible = true;
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<CurrentAdmissionListData> GetPatientList(int curIndex)
        {
            CurrentAdmissionListData objData = new CurrentAdmissionListData();
            CurrentAdmissionListBO objBO = new CurrentAdmissionListBO();
            objData.wardId = Convert.ToInt32(ddl_ward.SelectedValue == "" ? "0" : ddl_ward.SelectedValue);
            return objBO.GetPHRAdmissionDetailList(objData);
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            gvadmissionlist.DataSource = null;
            gvadmissionlist.DataBind();
            gvadmissionlist.Visible = true;
            lblmessage.Visible = false;
            divmsg1.Visible = false;
            ddl_ward.SelectedIndex = 0;
        }
        protected void gvadmissionlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSlNo = (Label)e.Row.FindControl("lblSlNo");
                Label lbluhid = (Label)e.Row.FindControl("lbluhid");
                Label lblSubHeading = (Label)e.Row.FindControl("lblSubHeading");
                Label lblIsRelease = (Label)e.Row.FindControl("lblIsRelease");
                Label lblPatientActive = (Label)e.Row.FindControl("lblPatientActive");
                Label lblDisChargeReady = (Label)e.Row.FindControl("lblDisChargeReady");
                Label name = (Label)e.Row.FindControl("lblname");
                Label bill = (Label)e.Row.FindControl("lbl_totalBill");
                Label deposit = (Label)e.Row.FindControl("lbl_deposit");
                Label payable = (Label)e.Row.FindControl("lbl_payable");
                Label print = (Label)e.Row.FindControl("lblprint");
                Label refundable = (Label)e.Row.FindControl("lbl_refundable");
                LinkButton sales = (LinkButton)e.Row.FindControl("lbl_sales");
                LinkButton final = (LinkButton)e.Row.FindControl("lbl_paybil");
                LinkButton advance = (LinkButton)e.Row.FindControl("lbl_advancepay");
                LinkButton refund = (LinkButton)e.Row.FindControl("lblreund");
                Label PbhbillClear = (Label)e.Row.FindControl("lbl_phbillclear");
                LinkButton Salreturn = (LinkButton)e.Row.FindControl("lbl_salesReturn");
                Label upperlimit = (Label)e.Row.FindControl("lbl_upperlimit");
                Label lowerlimit = (Label)e.Row.FindControl("lbl_lowerlimit");
                CheckBox ChkCredit = (CheckBox)e.Row.FindControl("ChkCredit");
                Label chkstatus = (Label)e.Row.FindControl("chkcreditstaus");



                if (LogData.RoleID == 1 || LogData.RoleID == 40)
                {
                    ChkCredit.Enabled = true;
                }
                else
                {
                    ChkCredit.Enabled = false;
                }
                if (chkstatus.Text == "1")
                {
                    ChkCredit.Checked = true;
                }
                else
                {
                    ChkCredit.Checked = false;
                }
                if (lblDisChargeReady.Text == "1")
                {
                    e.Row.BackColor = Color.FromName("#dcf852");
                }
                passivePatient = passivePatient + 1;
                slno = slno + 1;
                lblSlNo.Text = slno.ToString();

                if (lblSubHeading.Text == "1")
                {
                    e.Row.BackColor = Color.FromName("#33aa99");
                    lbluhid.Visible = false;
                    name.ForeColor = Color.FromName("#ffffff");
                    bill.Visible = false;
                    deposit.Visible = false;
                    payable.Visible = false;
                    print.Visible = false;
                    sales.Visible = false;
                    final.Visible = false;
                    advance.Visible = false;
                    refund.Visible = false;
                    Salreturn.Visible = false;
                    refundable.Visible = false;
                    slno = slno - 1;
                    passivePatient = passivePatient - 1;
                    lblSlNo.Text = "";
                }
                else
                {
                    bill.Visible = true;
                    deposit.Visible = true;
                    payable.Visible = true;
                    print.Visible = true;
                    sales.Visible = true;
                    final.Visible = true;
                    advance.Visible = true;
                    refund.Visible = true;
                    Salreturn.Visible = true;
                    refundable.Visible = true;
                }

                if (PbhbillClear.Text == "1")
                {
                    sales.Enabled = false;
                    final.Enabled = false;
                    advance.Enabled = false;
                    refund.Enabled = false;
                    Salreturn.Enabled = false;
                    final.Text = "Paid";
                    e.Row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    final.ForeColor = System.Drawing.Color.White;
                }
                else
                {
                    sales.Enabled = true;
                    final.Enabled = true;
                    advance.Enabled = true;
                    refund.Enabled = true;
                    Salreturn.Enabled = true;
                    final.Text = "Pay";
                }
                if (Convert.ToDecimal(bill.Text == "" ? "0" : bill.Text) > 0)
                {
                    if (Convert.ToDecimal(payable.Text == "" ? "0" : payable.Text) >= Convert.ToDecimal(lowerlimit.Text == "" ? "0" : lowerlimit.Text) && Convert.ToDecimal(payable.Text == "" ? "0" : payable.Text) < Convert.ToDecimal(upperlimit.Text == "" ? "0" : upperlimit.Text) && PbhbillClear.Text == "0" && lblSubHeading.Text == "0")
                    {
                        e.Row.Cells[2].BackColor = System.Drawing.Color.Yellow;
                        sales.Enabled = true;
                    }
                    if (Convert.ToDecimal(payable.Text == "" ? "0" : payable.Text) >= Convert.ToDecimal(upperlimit.Text == "" ? "0" : upperlimit.Text) && PbhbillClear.Text == "0" && lblSubHeading.Text == "0" && chkstatus.Text == "0")
                    {
                        e.Row.Cells[2].BackColor = System.Drawing.Color.IndianRed;
                        name.ForeColor = System.Drawing.Color.White;
                        sales.Enabled = false;
                    }
                    if (Convert.ToDecimal(payable.Text == "" ? "0" : payable.Text) >= Convert.ToDecimal(upperlimit.Text == "" ? "0" : upperlimit.Text) && PbhbillClear.Text == "0" && lblSubHeading.Text == "0" && chkstatus.Text == "1")
                    {
                        e.Row.Cells[2].BackColor = System.Drawing.Color.IndianRed;
                        name.ForeColor = System.Drawing.Color.White;
                        sales.Enabled = true;
                    }
                }
            }
            Messagealert_.ShowMessage(lblresult, " Total: " + (activePatient + passivePatient), 1);
        }
        protected void btn_print_Click(object sender, EventArgs e)
        {
            CurrentAdmissionListData objData = new CurrentAdmissionListData();
            CurrentAdmissionListBO objBO = new CurrentAdmissionListBO();
            Int32 WardID = Convert.ToInt32(ddl_ward.SelectedValue == "" ? "0" : ddl_ward.SelectedValue);
            string url = "../MedIPD/Reports/ReportViewer.aspx?option=CurrentPatientList&ward=" + WardID.ToString();
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }

        protected void chkCredit_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            Label PatientNo = (Label)gvadmissionlist.Rows[row.RowIndex].Cells[0].FindControl("lblRegId");
            Label uhid = (Label)gvadmissionlist.Rows[row.RowIndex].Cells[0].FindControl("lbluhid");
            CheckBox chkverify = (CheckBox)gvadmissionlist.Rows[row.RowIndex].Cells[0].FindControl("chkCredit");
            CurrentAdmissionListData objData = new CurrentAdmissionListData();
            CurrentAdmissionListBO objBO = new CurrentAdmissionListBO();
            objData.RegNo = PatientNo.Text.Trim();
            objData.UHID = Convert.ToInt64(uhid.Text == "" ? "0" : uhid.Text);
            objData.ChkCredit = chkverify.Checked ? 1 : 0;
            int result = objBO.UpdateCreditAlowed(objData);
            if (result == 1)
            {
                bindgrid();
            }
        }
        protected void gvadmissionlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "AdvancePay")
                {
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvadmissionlist.Rows[i];
                    Label EmrgNo = (Label)gr.Cells[0].FindControl("lblRegId");
                    Label UHID = (Label)gr.Cells[0].FindControl("lbluhid");
                    Label PatName = (Label)gr.Cells[0].FindControl("lbl_patient");
                    Label patctg = (Label)gr.Cells[0].FindControl("lblpatcatg");
                    if (patctg.Text == "1")
                    {
                        Session["AdvEmrgNo"] = PatName.Text + " | Emrg - " + EmrgNo.Text + ":" + UHID.Text;
                        Response.Redirect("/MedPhr/PhrAdvanceDeposit.aspx", false);
                    }
                    if (patctg.Text == "2")
                    {
                        Session["AdvEmrgNo"] = PatName.Text + " | IP - " + EmrgNo.Text + ":" + UHID.Text;
                        Response.Redirect("/MedPhr/PhrAdvanceDeposit.aspx", false);
                    }
                }
                if (e.CommandName == "Sale")
                {
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvadmissionlist.Rows[i];
                    Label PatNo = (Label)gr.Cells[0].FindControl("lblRegId");
                    Label UHID = (Label)gr.Cells[0].FindControl("lbluhid");
                    Label PatName = (Label)gr.Cells[0].FindControl("lbl_patient");
                    Label patctg = (Label)gr.Cells[0].FindControl("lblpatcatg");
                    if (patctg.Text == "1")
                    {
                        Session["EmgSaleDeatail"] = PatName.Text + " | UHID - " + UHID.Text + " | Emrg :" + PatNo.Text;
                        Response.Redirect("/MedPhr/Emg_DrugIssueRecord.aspx", false);
                    }
                    if (patctg.Text == "2")
                    {
                        Session["IPSaleDeatail"] = PatName.Text + " | UHID - " + UHID.Text + " | IP :" + PatNo.Text;
                        Response.Redirect("/MedPhr/PhrIPDrugIssue.aspx", false);
                    }
                }
                if (e.CommandName == "SaleReturn")
                {
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvadmissionlist.Rows[i];
                    Label PatNo = (Label)gr.Cells[0].FindControl("lblRegId");
                    Label UHID = (Label)gr.Cells[0].FindControl("lbluhid");
                    Label PatName = (Label)gr.Cells[0].FindControl("lbl_patient");
                    Label patctg = (Label)gr.Cells[0].FindControl("lblpatcatg");
                    if (patctg.Text == "1")
                    {
                        Session["SaleReturns"] = PatName.Text + " | UHID - " + UHID.Text + " | Emrg :" + PatNo.Text;
                        Response.Redirect("/MedStore/EmgIssueReturn.aspx", false);
                    }
                    if (patctg.Text == "2")
                    {
                        Session["SaleReturns"] = PatName.Text + " | UHID - " + UHID.Text + " | IP :" + PatNo.Text;
                        Response.Redirect("/MedStore/IPIssueReturn.aspx", false);
                    }
                }
                if (e.CommandName == "Refund")
                {
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvadmissionlist.Rows[i];
                    Label PatNo = (Label)gr.Cells[0].FindControl("lblRegId");
                    Label UHID = (Label)gr.Cells[0].FindControl("lbluhid");
                    Label PatName = (Label)gr.Cells[0].FindControl("lbl_patient");
                    Label patctg = (Label)gr.Cells[0].FindControl("lblpatcatg");
                    if (patctg.Text == "1")
                    {
                        Session["EmgRefund"] = PatName.Text + " | Emrg - " + PatNo.Text + " UHID:" + UHID.Text;
                        Response.Redirect("/MedPhr/PhrDepositRefund.aspx", false);
                    }
                    if (patctg.Text == "2")
                    {
                        Session["EmgRefund"] = PatName.Text + " | IP - " + PatNo.Text + "UHID:" + UHID.Text;
                        Response.Redirect("/MedPhr/PhrDepositRefund.aspx", false);
                    }
                }
                if (e.CommandName == "BillPay")
                {
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvadmissionlist.Rows[i];
                    Label PatNo = (Label)gr.Cells[0].FindControl("lblRegId");
                    Label UHID = (Label)gr.Cells[0].FindControl("lbluhid");
                    Label PatName = (Label)gr.Cells[0].FindControl("lbl_patient");
                    Label patctg = (Label)gr.Cells[0].FindControl("lblpatcatg");
                    if (patctg.Text == "1")
                    {
                        Session["BillPay"] = PatNo.Text;
                        Response.Redirect("/MedPhr/EmrgFinalBill.aspx", false);
                    }
                    if (patctg.Text == "2")
                    {
                        Session["BillPay"] = PatNo.Text;
                        Response.Redirect("/MedPhr/PhrIPfinalbill.aspx", false);
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
        protected void btn_return_Click(object sender, EventArgs e)
        {
            Response.Redirect("/MedStore/OPIssueReturn.aspx", false);
        }
        protected void btn_returnemg_Click(object sender, EventArgs e)
        {
            Response.Redirect("/MedStore/EmgIssueReturn.aspx", false);
        }
        protected void btn_pay_Click(object sender, EventArgs e)
        {
            Response.Redirect("/MedPhr/OPSales.aspx", false);
        }
        protected void btn_returnIP_Click(object sender, EventArgs e)
        {
            Response.Redirect("/MedStore/IPIssueReturn.aspx", false);
        }
        protected void ddl_ward_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindgrid();
        }
    }
}