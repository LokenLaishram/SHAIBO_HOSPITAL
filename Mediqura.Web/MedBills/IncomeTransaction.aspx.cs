using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedAccount;
using Mediqura.BOL.MedBillBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedAccount;
using Mediqura.CommonData.MedBillData;
using Mediqura.Utility;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedBills
{
    public partial class IncomeTransaction : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlpaymentmode, mstlookup.GetLookupsList(LookupName.PaymentMode));
            Commonfunction.PopulateDdl(ddl_income_head, mstlookup.GetLookupsList(LookupName.IncomeTransaction));
            btnPrint.Attributes["disabled"] = "disabled";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ddl_income_head.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Please select account head!", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddl_income_head.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
         
            if (txtParticular.Text.Trim() == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Please enter particular!", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtParticular.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (ddl_income_head.SelectedValue != "100000")
            {
                if (Convert.ToDecimal(txt_amount.Text.Trim() == "" ? "0" : txt_amount.Text.Trim()) <= 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "validAmount", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_amount.Focus();
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
            }
           
           
            AccountTransactionData objdata = new AccountTransactionData();
            AccountBO objstdBO = new AccountBO();
            objdata.DebitID= Convert.ToInt32(ddl_income_head.SelectedValue);
            objdata.PaymentMode = Convert.ToInt32(ddlpaymentmode.SelectedValue);
            objdata.TotalDebit = Convert.ToDecimal(txt_amount.Text==""?"0": txt_amount.Text);
            objdata.Naration = txtParticular.Text;
            objdata.BankName = txtbank.Text;
            objdata.Cheque = txt_chequenumber.Text == "" ? null : txt_chequenumber.Text;
            objdata.Invoicenumber = txtinvoicenumber.Text == "" ? null : txtinvoicenumber.Text;
            objdata.EmployeeID = LogData.EmployeeID;
            objdata.HospitalID = LogData.HospitalID;
            objdata.FinancialYearID = LogData.FinancialYearID;


            AccountTransactionOutput outputdata = new AccountTransactionOutput();

            outputdata = objstdBO.UpdateIncomeTransaction(objdata);

            if (outputdata.outputdata > 0)
            {
                btnPrint.Attributes.Remove("disabled");
                txt_voucherNo.Text = outputdata.voucher;
                Messagealert_.ShowMessage(lblmessage, "save", 1);
                div1.Visible = true;
                div1.Attributes["class"] = "SucessAlert";
                btnSave.Attributes["disabled"] = "disabled";
                btnPrint.Attributes.Remove("disabled");
                if (outputdata.outputdata == 3)
                {
                    isPass.Value = "1";
                }
                else {
                    isPass.Value = "0";
                }
            }
            else
            {

                Messagealert_.ShowMessage(lblmessage, "Error", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(isPass.Value == "" ? "0" : isPass.Value) == 0)
            {
                string url = "../MedAccount/Reports/ReportViewer.aspx?option=AccountTransaction&voucherNumber=" + txt_voucherNo.Text;
                string fullURL = "window.open('" + url + "', '_blank');";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
            }
            else {
                string url = "../MedAccount/Reports/ReportViewer.aspx?option=VehiclePass&PassNo=" + txt_voucherNo.Text;
                string fullURL = "window.open('" + url + "', '_blank');";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            btnSave.Visible = true;
            ddl_income_head.SelectedIndex = 0;
            ddlpaymentmode.SelectedIndex = 0;
            txt_amount.Text = "";
            txt_voucherNo.Text = "";
            txtParticular.Text = "";

        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void bindgrid()
        {
            try
            {
                List<RecdTransactionData> objTransaction = GetTranasctionList(0);
                if (objTransaction.Count > 0)
                {
                    GVTransactionList.DataSource = objTransaction;
                    GVTransactionList.DataBind();
                    GVTransactionList.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objTransaction[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;

                   
                    lblmessage2.Visible = false;
                    div2.Visible = false;
                }
                else
                {
                    GVTransactionList.DataSource = null;
                    GVTransactionList.DataBind();
                    GVTransactionList.Visible = true;
                    lblresult.Visible = false;
                    divmsg3.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<RecdTransactionData> GetTranasctionList(int curIndex)
        {
            RecdTransactionData ObjData = new RecdTransactionData();
            RecdTransactionBO objbillingBO = new RecdTransactionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            ObjData.DateFrom = from;
            ObjData.DateTo = To;
            ObjData.VoucherNo = txt_voucher.Text.Trim() == "" ? "0" : txt_voucher.Text;
            return objbillingBO.GetIncomeTransactionList(ObjData);
        }
        public List<RecdTransactionData> GetPassList(int curIndex)
        {
            RecdTransactionData ObjData = new RecdTransactionData();
            RecdTransactionBO objbillingBO = new RecdTransactionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtDateFromPass.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtDateFromPass.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtDateToPass.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtDateToPass.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            ObjData.DateFrom = from;
            ObjData.DateTo = To;
            ObjData.PassNo = txt_pass_no.Text.Trim() == "" ? "0" : txt_pass_no.Text;
            return objbillingBO.GetPassList(ObjData);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txt_voucher.Text = "";
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
                if (ddlpaymentmode.SelectedValue == "2")
                {
                    GetBankName(Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue));
                    txtbank.ReadOnly = true;
                    txt_chequenumber.ReadOnly = true;
                    txtinvoicenumber.ReadOnly = false;
                }
                if (ddlpaymentmode.SelectedValue == "3")
                {
                    GetBankName(Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue));
                    txtbank.ReadOnly = true;
                    txt_chequenumber.ReadOnly = false;
                    txtinvoicenumber.ReadOnly = true;
                }
                if (ddlpaymentmode.SelectedValue == "4")
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

        protected void GVTransactionList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Deletes")
            {
                if (LogData.DeleteEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "DeleteEnable", 0);
                    div2.Visible = true;
                    div2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                RecdTransactionData ObjData = new RecdTransactionData();
                RecdTransactionBO objbillingBO = new RecdTransactionBO();
                int i = Convert.ToInt16(e.CommandArgument.ToString());
                GridViewRow gr = GVTransactionList.Rows[i];
                Label ID = (Label)gr.Cells[0].FindControl("lblVoucher");
    
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
                    ObjData.Remarks = txtremarks.Text;
                }
                ObjData.VoucherNo = ID.Text.Trim();
                ObjData.EmployeeID = LogData.UserLoginId;
                ObjData.FinancialYearID = LogData.FinancialYearID;
                ObjData.HospitalID = LogData.HospitalID;
               
                int Result = objbillingBO.DeleteIncomeByID(ObjData);
                if (Result == 1)
                {
                    Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                    div2.Visible = true;
                    div2.Attributes["class"] = "SucessAlert";
                    bindgrid();
                }
                else
                {
                    if (Result == 2)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "AccountClosed", 0);
                        div2.Visible = true;
                        div2.Attributes["class"] = "FailAlert";
                        // bindgrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage2, "system", 0);
                        div2.Attributes["class"] = "FailAlert";
                        div2.Visible = true;
                    }
                }

            }
        }

        protected void ddl_income_head_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_income_head.SelectedValue == "100000")
            {
                ddlpaymentmode.Attributes["disabled"] = "disabled";
                txt_amount.ReadOnly = true;
            }
            else {
                txt_amount.ReadOnly = false;
                ddlpaymentmode.Attributes.Remove("disabled");
            }
        }

        protected void btn_pass_search_Click(object sender, EventArgs e)
        {
            bindgridPass();
        }
        protected void bindgridPass()
        {
            try
            {
                List<RecdTransactionData> objTransaction = GetPassList(0);
                if (objTransaction.Count > 0)
                {
                    GvPassList.DataSource = objTransaction;
                    GvPassList.DataBind();
                    GvPassList.Visible = true;
                    Messagealert_.ShowMessage(lblResult5, "Total:" + objTransaction[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    div5.Attributes["class"] = "SucessAlert";
                    div5.Visible = true;


                    lblmessage4.Visible = false;
                    div4.Visible = false;
                }
                else
                {
                    GvPassList.DataSource = null;
                    GvPassList.DataBind();
                    GvPassList.Visible = true;
                    lblResult5.Visible = false;
                    div5.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }

        protected void GvPassList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Deletes")
            {
                if (LogData.DeleteEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage4, "DeleteEnable", 0);
                    div4.Visible = true;
                    div4.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage4.Visible = false;
                }
                RecdTransactionData ObjData = new RecdTransactionData();
                RecdTransactionBO objbillingBO = new RecdTransactionBO();
                int i = Convert.ToInt16(e.CommandArgument.ToString());
                GridViewRow gr = GvPassList.Rows[i];
                Label ID = (Label)gr.Cells[0].FindControl("lblPassNo");

                TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                txtremarks.Enabled = true;
                if (txtremarks.Text == "")
                {
                    Messagealert_.ShowMessage(lblResult5, "Remarks", 0);
                    div5.Attributes["class"] = "FailAlert";
                    txtremarks.Focus();
                    return;
                }
                else
                {
                    ObjData.Remarks = txtremarks.Text;
                }
                ObjData.PassNo = ID.Text.Trim();
                ObjData.EmployeeID = LogData.UserLoginId;
                ObjData.FinancialYearID = LogData.FinancialYearID;
                ObjData.HospitalID = LogData.HospitalID;

                int Result = objbillingBO.DeleteVehiclePassByID(ObjData);
                if (Result == 1)
                {
                    Messagealert_.ShowMessage(lblmessage4, "delete", 1);
                    div4.Visible = true;
                    div4.Attributes["class"] = "SucessAlert";
                    bindgridPass();
                }
                else
                {
                    if (Result == 2)
                    {
                        Messagealert_.ShowMessage(lblmessage4, "AccountClosed", 0);
                        div4.Visible = true;
                        div4.Attributes["class"] = "FailAlert";
                        // bindgrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage4, "system", 0);
                        div4.Attributes["class"] = "FailAlert";
                        div4.Visible = true;
                    }
                }

            }
        }
    }
}