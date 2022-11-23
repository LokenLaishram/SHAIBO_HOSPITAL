using AjaxControlToolkit;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedAccount;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedAccount;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ClosedXML.Excel;
using System.Reflection;
using System.Text;
using System.Net;

namespace Mediqura.Web.MedAccount
{
    public partial class AccountTransaction : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                pnl_bank.Visible = false;
                btn_add_Debit.Visible = false;
                btn_add_credit.Visible = false;
                txt_voucher.ReadOnly = true;
                bindddl();
                btnprints.Attributes["disabled"] = "disabled";
                bindddlList();
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_payment_type, mstlookup.GetLookupsList(LookupName.PaymentType));
            Commonfunction.PopulateDdl(ddl_transaction_type, mstlookup.GetLookupsList(LookupName.TransactionType));

        }
        protected void ddl_payment_mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_payment_mode.SelectedIndex == 2)
            {
                pnl_bank.Visible = true;
            }
            else
            {
                pnl_bank.Visible = false;
            }
        }

        protected void ddl_payment_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_payment_type.SelectedIndex == 0)
            {
                GVCredit.DataSource = null;
                GVDebit.DataSource = null;
                GVCredit.DataBind();
                GVCredit.Visible = true;
                GVDebit.DataBind();
                GVDebit.Visible = true;
                ViewState["DebitTable"] = null;
                ViewState["CreditTable"] = null;
                btn_add_Debit.Visible = false;
                btn_add_credit.Visible = false;
            }
            else
            {
                if (ddl_payment_type.SelectedIndex == 1)
                {
                    btn_add_Debit.Visible = true;
                    btn_add_credit.Visible = false;



                }
                else
                {
                    btn_add_Debit.Visible = false;
                    btn_add_credit.Visible = true;
                }
                DebitFirstGridViewRow();
                CreditFirstGridViewRow();
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetAccountName(string prefixText, int count, string contextKey)
        {
            AcountLedgerData objData = new AcountLedgerData();
            AccountBO objBO = new AccountBO();
            List<AcountLedgerData> getResult = new List<AcountLedgerData>();
            objData.AccountName = prefixText;
            objData.Payment_type = Convert.ToInt32(contextKey == "" ? "0" : contextKey);
            getResult = objBO.SearchLedgerByName(objData);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].AccountName.ToString());
            }
            return list;
        }

        protected void GVCredit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox CreditAmount = (TextBox)e.Row.FindControl("txtCreditAmount");

                AutoCompleteExtender auto_credit_account = (AutoCompleteExtender)e.Row.FindControl("auto_credit_account");
                if (ddl_payment_type.SelectedIndex == 1)
                {
                    CreditAmount.ReadOnly = true;
                    auto_credit_account.ContextKey = "0";
                }
                else if (ddl_payment_type.SelectedIndex == 2)
                {
                    CreditAmount.ReadOnly = false;
                    auto_credit_account.ContextKey = "1";


                    int totalcredit = Convert.ToInt32(txt_total_credit_amount.Text == "" ? "0" : txt_total_credit_amount.Text);
                    int creditamount = Convert.ToInt32(CreditAmount.Text == "" ? "0" : CreditAmount.Text);
                    totalcredit = totalcredit + creditamount;
                    if (GVDebit.Rows.Count > 0)
                    {
                        TextBox txtdebitamount = (TextBox)GVDebit.Rows[0].Cells[0].FindControl("txtDebitAmount");
                        txtdebitamount.Text = "" + totalcredit;
                        txt_total_debit_amount.Text = txtdebitamount.Text;
                        txt_total_credit_amount.Text = "" + totalcredit;
                    }
                    else
                    {
                        txt_total_debit_amount.Text = "0";
                        txt_total_credit_amount.Text = "" + totalcredit;
                    }
                }
                else if (ddl_payment_type.SelectedIndex == 3)
                {
                    CreditAmount.ReadOnly = false;
                    auto_credit_account.ContextKey = "1";
                }
                LinkButton linkbtn = (LinkButton)e.Row.FindControl("lnkDeleteCredit");
                if (e.Row.RowIndex == 0)
                {
                    linkbtn.Visible = false;
                }


            }
        }

        protected void GVDebit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox debitAmount = (TextBox)e.Row.FindControl("txtDebitAmount");
                AutoCompleteExtender auto_debit_account = (AutoCompleteExtender)e.Row.FindControl("auto_debit_account");
                if (ddl_payment_type.SelectedIndex == 1)
                {
                    auto_debit_account.ContextKey = "1";
                    debitAmount.ReadOnly = false;
                    int totaldebit = Convert.ToInt32(txt_total_debit_amount.Text == "" ? "0" : txt_total_debit_amount.Text);
                    int debitamount = Convert.ToInt32(debitAmount.Text == "" ? "0" : debitAmount.Text);
                    totaldebit = totaldebit + debitamount;
                    if (GVCredit.Rows.Count > 0)
                    {
                        TextBox txtCreditAmount = (TextBox)GVCredit.Rows[0].Cells[0].FindControl("txtCreditAmount");
                        txtCreditAmount.Text = "" + totaldebit;
                        txt_total_credit_amount.Text = txtCreditAmount.Text;
                        txt_total_debit_amount.Text = "" + totaldebit;
                    }
                    else
                    {
                        txt_total_credit_amount.Text = "0";
                        txt_total_debit_amount.Text = "" + totaldebit;
                    }



                }
                else if (ddl_payment_type.SelectedIndex == 2)
                {
                    debitAmount.ReadOnly = true;
                    auto_debit_account.ContextKey = "0";

                }
                else
                    if (ddl_payment_type.SelectedIndex == 3)
                    {
                        debitAmount.ReadOnly = true;
                        auto_debit_account.ContextKey = "1";

                    }


                LinkButton linkbtn = (LinkButton)e.Row.FindControl("lnkDelete");
                if (e.Row.RowIndex == 0)
                {
                    linkbtn.Visible = false;
                }
            }
        }

        protected void btn_add_credit_Click(object sender, EventArgs e)
        {
            txt_total_credit_amount.Text = "0";
            CreditAddNewRow();
        }

        protected void btn_add_Debit_Click(object sender, EventArgs e)
        {
            txt_total_debit_amount.Text = "0";
            DebitAddNewRow();
        }

        private void DebitFirstGridViewRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("DebitAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("debitLedger", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["DebitAmount"] = "0";
            dr["debitLedger"] = "0";
            dt.Rows.Add(dr);

            ViewState["DebitTable"] = dt;
            GVDebit.DataSource = dt;
            GVDebit.DataBind();
            GVDebit.Visible = true;
        }
        private void DebitSetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["DebitTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["DebitTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txtamount = (TextBox)GVDebit.Rows[rowIndex].Cells[3].FindControl("txtDebitAmount");
                        TextBox txtDebitAccount = (TextBox)GVDebit.Rows[rowIndex].Cells[2].FindControl("txt_debit_account");

                        txtamount.Text = dt.Rows[i]["DebitAmount"].ToString();
                        txtDebitAccount.Text = dt.Rows[i]["debitLedger"].ToString();

                        rowIndex++;
                    }
                }
            }
        }
        private void DebitAddNewRow()
        {
            int rowIndex = 0;

            if (ViewState["DebitTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["DebitTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtamount = (TextBox)GVDebit.Rows[rowIndex].Cells[3].FindControl("txtDebitAmount");

                        TextBox txtDebitAccount = (TextBox)GVDebit.Rows[rowIndex].Cells[2].FindControl("txt_debit_account");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["DebitAmount"] = txtamount.Text;
                        dtCurrentTable.Rows[i - 1]["debitLedger"] = txtDebitAccount.Text;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["DebitTable"] = dtCurrentTable;

                    GVDebit.DataSource = dtCurrentTable;
                    GVDebit.DataBind();
                    GVDebit.Visible = true;
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            DebitSetPreviousData();
        }


        private void DebitSetRowData()
        {
            int rowIndex = 0;

            if (ViewState["DebitTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["DebitTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtamount = (TextBox)GVDebit.Rows[rowIndex].Cells[3].FindControl("txtDebitAmount");

                        TextBox txtDebitAccount = (TextBox)GVDebit.Rows[rowIndex].Cells[2].FindControl("txt_debit_account");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["DebitAmount"] = txtamount.Text;
                        dtCurrentTable.Rows[i - 1]["debitLedger"] = txtDebitAccount.Text;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["DebitTable"] = dtCurrentTable;

                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

        }

        protected void GVDebit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Deletes")
            {
                DebitSetRowData();
                if (ViewState["DebitTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["DebitTable"];

                    int rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                    if (dt.Rows.Count > 1)
                    {

                        dt.Rows.RemoveAt(rowIndex);
                        DataTable newDT = RemoveEmptyRowsFromDataTable(dt);

                        ViewState["DebitTable"] = newDT;
                        txt_total_debit_amount.Text = "0";

                        int countRw = newDT.Rows.Count;
                        GVDebit.DataSource = newDT;
                        GVDebit.DataBind();
                        GVDebit.Visible = true;
                        for (int i = 0; i < GVDebit.Rows.Count - 1; i++)
                        {
                            GVDebit.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                        }
                        DebitSetPreviousData();
                    }
                }
            }
        }
        DataTable RemoveEmptyRowsFromDataTable(DataTable dt)
        {
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                if (dt.Rows[i][1] == DBNull.Value)
                    dt.Rows[i].Delete();
            }
            dt.AcceptChanges();
            return dt;
        }

        private void CreditFirstGridViewRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("CreditAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("CreditLedger", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["CreditAmount"] = "0";
            dr["CreditLedger"] = "0";
            dt.Rows.Add(dr);

            ViewState["CreditTable"] = dt;
            GVCredit.DataSource = dt;
            GVCredit.DataBind();
            GVCredit.Visible = true;
        }
        private void CreditSetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CreditTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CreditTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txtamount = (TextBox)GVCredit.Rows[rowIndex].Cells[3].FindControl("txtCreditAmount");

                        TextBox txtCreditAcount = (TextBox)GVCredit.Rows[rowIndex].Cells[2].FindControl("txt_credit_accnt");

                        txtamount.Text = dt.Rows[i]["CreditAmount"].ToString();
                        txtCreditAcount.Text = dt.Rows[i]["CreditLedger"].ToString();

                        rowIndex++;
                    }
                }
            }
        }
        private void CreditAddNewRow()
        {
            int rowIndex = 0;

            if (ViewState["CreditTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CreditTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtamount = (TextBox)GVCredit.Rows[rowIndex].Cells[3].FindControl("txtCreditAmount");
                        TextBox txtCreditAcount = (TextBox)GVCredit.Rows[rowIndex].Cells[2].FindControl("txt_credit_accnt");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["CreditAmount"] = txtamount.Text;
                        dtCurrentTable.Rows[i - 1]["CreditLedger"] = txtCreditAcount.Text;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CreditTable"] = dtCurrentTable;

                    GVCredit.DataSource = dtCurrentTable;
                    GVCredit.DataBind();
                    GVCredit.Visible = true;
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            CreditSetPreviousData();
        }
        private void CreditSetRowData()
        {
            int rowIndex = 0;

            if (ViewState["CreditTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CreditTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtamount = (TextBox)GVCredit.Rows[rowIndex].Cells[3].FindControl("txtCreditAmount");
                        TextBox txtCreditAcount = (TextBox)GVCredit.Rows[rowIndex].Cells[2].FindControl("txt_credit_accnt");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["CreditAmount"] = txtamount.Text;
                        dtCurrentTable.Rows[i - 1]["CreditLedger"] = txtCreditAcount.Text;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CreditTable"] = dtCurrentTable;

                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

        }

        protected void GVCredit_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Deletes")
            {
                CreditSetRowData();
                if (ViewState["CreditTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CreditTable"];

                    int rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                    if (dt.Rows.Count > 1)
                    {

                        dt.Rows.RemoveAt(rowIndex);
                        DataTable newDT = RemoveEmptyRowsFromDataTable(dt);

                        ViewState["CreditTable"] = newDT;

                        txt_total_credit_amount.Text = "0";
                        int countRw = newDT.Rows.Count;
                        GVCredit.DataSource = newDT;
                        GVCredit.DataBind();
                        GVCredit.Visible = true;
                        for (int i = 0; i < GVCredit.Rows.Count - 1; i++)
                        {
                            GVCredit.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                        }
                        CreditSetPreviousData();
                    }
                }
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            List<AccountTransactionData> ListobjdataCredit = new List<AccountTransactionData>();
            List<AccountTransactionData> ListobjdataDebit = new List<AccountTransactionData>();
            AccountTransactionData objdata = new AccountTransactionData();
            AccountBO objstdBO = new AccountBO();

            if (ddl_payment_type.SelectedIndex == 0)
            {

                Messagealert_.ShowMessage(lblmessage, "PaymentType", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddl_payment_type.Focus();
                return;

            }
            if (ddl_payment_mode.SelectedIndex == 0)
            {

                Messagealert_.ShowMessage(lblmessage, "PaymentMode", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddl_payment_mode.Focus();
                return;

            }
            try
            {

                foreach (GridViewRow row in GVCredit.Rows)
                {

                    TextBox txt_credit_accnt = (TextBox)GVCredit.Rows[row.RowIndex].Cells[0].FindControl("txt_credit_accnt");
                    TextBox txtCreditAmount = (TextBox)GVCredit.Rows[row.RowIndex].Cells[0].FindControl("txtCreditAmount");

                    if (txt_credit_accnt.Text.Trim() == "")
                    {

                        Messagealert_.ShowMessage(lblmessage, "CreditLedger", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_credit_accnt.Focus();
                        return;

                    }
                    if (txtCreditAmount.Text.Trim() == "" || txtCreditAmount.Text.Trim() == "0")
                    {

                        Messagealert_.ShowMessage(lblmessage, "CreditAmount", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txtCreditAmount.Focus();
                        return;

                    }

                    AccountTransactionData objsubdata = new AccountTransactionData();

                    String CreditAcntID = "0";


                    String CreditText = txt_credit_accnt.Text == "" ? null : txt_credit_accnt.Text.ToString().Trim();
                    if (CreditText != null)
                    {
                        String[] credit = CreditText.Split(new[] { ":" }, StringSplitOptions.None);
                        CreditAcntID = credit[1];
                    }


                    objsubdata.CreditAmount = Convert.ToDecimal(txtCreditAmount.Text == "" ? "0" : txtCreditAmount.Text.ToString().Trim());
                    objsubdata.CreditID = Convert.ToInt32(CreditAcntID);
                    ListobjdataCredit.Add(objsubdata);

                }
                objdata.CreditXml = XmlConvertor.AccountTransactionCredittoXML(ListobjdataCredit).ToString();
                foreach (GridViewRow row in GVDebit.Rows)
                {

                    TextBox txt_debit_account = (TextBox)GVDebit.Rows[row.RowIndex].Cells[0].FindControl("txt_debit_account");
                    TextBox txtDebitAmount = (TextBox)GVDebit.Rows[row.RowIndex].Cells[0].FindControl("txtDebitAmount");


                    if (txt_debit_account.Text.Trim() == "")
                    {

                        Messagealert_.ShowMessage(lblmessage, "DebitLedger", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_debit_account.Focus();
                        return;

                    }
                    if (txtDebitAmount.Text.Trim() == "0" || txtDebitAmount.Text.Trim() == "")
                    {

                        Messagealert_.ShowMessage(lblmessage, "DebitAmount", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txtDebitAmount.Focus();
                        return;

                    }
                    AccountTransactionData objsubdata = new AccountTransactionData();

                    String DebitAcntID = "0";


                    String DebitText = txt_debit_account.Text == "" ? null : txt_debit_account.Text.ToString().Trim();
                    if (DebitText != null)
                    {
                        String[] debit = DebitText.Split(new[] { ":" }, StringSplitOptions.None);
                        DebitAcntID = debit[1];
                    }


                    objsubdata.DebitAmount = Convert.ToDecimal(txtDebitAmount.Text == "" ? "0" : txtDebitAmount.Text.ToString().Trim());
                    objsubdata.DebitID = Convert.ToInt32(DebitAcntID);
                    ListobjdataDebit.Add(objsubdata);

                }
                objdata.DebitXml = XmlConvertor.AccountTransactionDebittoXML(ListobjdataDebit).ToString();
                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                if (ddl_payment_mode.SelectedIndex == 2)
                {

                    if (ddl_transaction_type.SelectedIndex == 0)
                    {

                        Messagealert_.ShowMessage(lblmessage, "TransactionType", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        ddl_transaction_type.Focus();
                        return;

                    }
                    if (txt_instrument_name.Text.Trim() == "")
                    {

                        Messagealert_.ShowMessage(lblmessage, "InstrumentName", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_instrument_name.Focus();
                        return;

                    }
                    if (txt_intrumentDate.Text.Trim() == "")
                    {

                        Messagealert_.ShowMessage(lblmessage, "InstrumentDate", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_intrumentDate.Focus();
                        return;

                    }
                    if (txt_bank_payee_name.Text.Trim() == "")
                    {

                        Messagealert_.ShowMessage(lblmessage, "BankPayee", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_bank_payee_name.Focus();
                        return;

                    }
                    if (txt_bank_branch_name.Text.Trim() == "")
                    {

                        Messagealert_.ShowMessage(lblmessage, "BankBranch", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_bank_branch_name.Focus();
                        return;

                    }
                    if (txt_naration.Text.Trim() == "")
                    {

                        Messagealert_.ShowMessage(lblmessage, "AccountNarration", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_naration.Focus();
                        return;

                    }
                    objdata.TransactionType = Convert.ToInt32(ddl_transaction_type.SelectedValue);
                    objdata.InstrumentName = txt_instrument_name.Text == "" ? null : txt_instrument_name.Text.ToString().Trim();
                    objdata.InstrumentDate = Convert.ToDateTime(txt_intrumentDate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_intrumentDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault));
                    objdata.BankPayeeName = txt_bank_payee_name.Text == "" ? null : txt_bank_payee_name.Text.ToString().Trim();
                    objdata.BankBranchName = txt_bank_branch_name.Text == "" ? null : txt_bank_branch_name.Text.ToString().Trim();
                }
                else
                {

                    objdata.TransactionType = Convert.ToInt32(ddl_transaction_type.SelectedValue);
                    objdata.InstrumentName = txt_instrument_name.Text == "" ? null : txt_instrument_name.Text.ToString().Trim();
                    objdata.InstrumentDate = Convert.ToDateTime(txt_intrumentDate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_intrumentDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault));
                    objdata.BankPayeeName = txt_bank_payee_name.Text == "" ? null : txt_bank_payee_name.Text.ToString().Trim();
                    objdata.BankBranchName = txt_bank_branch_name.Text == "" ? null : txt_bank_branch_name.Text.ToString().Trim();
                }
                objdata.PaymentType = Convert.ToInt32(ddl_payment_type.SelectedValue);
                objdata.PaymentMode = Convert.ToInt32(ddl_payment_mode.SelectedValue);
                objdata.Date = System.DateTime.Now;
                objdata.TotalDebit = Convert.ToDecimal(txt_total_credit_amount.Text);
                objdata.TotalCredit = Convert.ToDecimal(txt_total_credit_amount.Text);
                if (ddl_payment_mode.SelectedIndex == 2)
                {
                    objdata.Naration = txt_naration.Text + " Transaction Type:" + ddl_transaction_type.SelectedItem + " Instrument Name:" + txt_instrument_name.Text + " Payee Name:" + txt_bank_payee_name.Text + " Branch Name:" + txt_bank_branch_name.Text;
                }
                else
                {
                    objdata.Naration = txt_naration.Text == "" ? null : txt_naration.Text.ToString().Trim();
                }
                objdata.EmployeeID = LogData.EmployeeID;
                objdata.HospitalID = LogData.HospitalID;
                objdata.FinancialYearID = LogData.FinancialYearID;


                AccountTransactionOutput outputdata = new AccountTransactionOutput();

                outputdata = objstdBO.UpdateAccountTransaction(objdata);

                if (outputdata.outputdata > 0)
                {
                    btnprints.Attributes.Remove("disabled");
                    txt_voucher.Text = outputdata.voucher;
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    btnsave.Attributes["disabled"] = "disabled";
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
            }
        }
        public void reset()
        {
            btnsave.Attributes.Remove("disabled");
            ddl_payment_type.SelectedIndex = 0;
            ddl_transaction_type.SelectedIndex = 0;
            ddl_payment_mode.SelectedIndex = 0;
            txt_voucher.Text = "";
            txt_naration.Text = "";
            txt_instrument_name.Text = "";
            txt_total_credit_amount.Text = "0";
            txt_total_debit_amount.Text = "0";
            txt_intrumentDate.Text = "";
            txt_difference_amt.Text = "0";
            txt_cross_inst.Text = "0";
            txt_bank_branch_name.Text = "";
            txt_bank_payee_name.Text = "";
            GVCredit.DataSource = null;
            GVDebit.DataSource = null;
            GVDebit.DataBind();
            GVCredit.DataBind();
            btnprints.Attributes["disabled"] = "disabled";

            if (ddl_payment_mode.SelectedIndex == 2)
            {
                pnl_bank.Visible = true;
            }
            else
            {
                pnl_bank.Visible = false;
            }

        }
        protected void txt_credit_accnt_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txt = (TextBox)currentRow.FindControl("txt_credit_accnt");
            TextBox txtamount = (TextBox)currentRow.FindControl("txtCreditAmount");
            if (!txt.Text.ToString().Contains("ID:"))
            {
                txt.Text = "";
            }
            else
            {
                if (ddl_payment_type.SelectedIndex == 2)
                {
                    txtamount.Text = "";
                    txtamount.Focus();
                }
            }
        }

        protected void txt_debit_account_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txt = (TextBox)currentRow.FindControl("txt_debit_account");
            TextBox txtamount = (TextBox)currentRow.FindControl("txtDebitAmount");
            if (!txt.Text.ToString().Contains("ID:"))
            {

                txt.Text = "";
            }
            else
            {
                if (ddl_payment_type.SelectedIndex == 1)
                {
                    txtamount.Text = "";
                    txtamount.Focus();
                }
            }

        }

        protected void txtDebitAmount_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txt = (TextBox)currentRow.FindControl("txtDebitAmount");
            int debitmount = Convert.ToInt32(txt.Text == "" ? "0" : txt.Text);
            TextBox txtCreditAmount = (TextBox)GVCredit.Rows[0].Cells[0].FindControl("txtCreditAmount");
            int creditAmount = Convert.ToInt32(txtCreditAmount.Text == "" ? "0" : txtCreditAmount.Text);
            creditAmount = creditAmount + debitmount;
            txtCreditAmount.Text = "" + creditAmount;
            int totaldebit = Convert.ToInt32(txt_total_debit_amount.Text == "" ? "0" : txt_total_debit_amount.Text);
            int totalcredit = Convert.ToInt32(txt_total_credit_amount.Text == "" ? "0" : txt_total_credit_amount.Text);
            int totaldiff = Convert.ToInt32(txt_difference_amt.Text == "" ? "0" : txt_difference_amt.Text);
            totaldebit = totaldebit + debitmount;
            totalcredit = creditAmount;
            totaldiff = totalcredit - totaldebit;
            txt_total_debit_amount.Text = "" + totaldebit;
            txt_total_credit_amount.Text = "" + totalcredit;
            txt_difference_amt.Text = "" + totaldiff;
            txt.Focus();
            return;
        }

        protected void txtCreditAmount_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txt = (TextBox)currentRow.FindControl("txtCreditAmount");
            int creditamount = Convert.ToInt32(txt.Text == "" ? "0" : txt.Text);
            TextBox txtDebitamount = (TextBox)GVDebit.Rows[0].Cells[0].FindControl("txtDebitAmount");
            int debitamount = Convert.ToInt32(txtDebitamount.Text == "" ? "0" : txtDebitamount.Text);
            debitamount = debitamount + creditamount;
            txtDebitamount.Text = "" + debitamount;
            int totaldebit = Convert.ToInt32(txt_total_debit_amount.Text == "" ? "0" : txt_total_debit_amount.Text);
            int totalcredit = Convert.ToInt32(txt_total_credit_amount.Text == "" ? "0" : txt_total_credit_amount.Text);
            int totaldiff = Convert.ToInt32(txt_difference_amt.Text == "" ? "0" : txt_difference_amt.Text);
            totaldebit = debitamount;
            totalcredit = totalcredit + creditamount;
            totaldiff = totalcredit - totaldebit;
            txt_total_debit_amount.Text = "" + totaldebit;
            txt_total_credit_amount.Text = "" + totalcredit;
            txt_difference_amt.Text = "" + totaldiff;
            txt.Focus();
            return;
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            reset();
        }

        protected void btnprints_Click(object sender, EventArgs e)
        {
            string url = "../MedAccount/Reports/ReportViewer.aspx?option=AccountTransaction&voucherNumber=" + txt_voucher.Text;
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }

        private void bindddlList()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_transaction, mstlookup.GetLookupsList(LookupName.PaymentType));
            Commonfunction.PopulateDdl(ddl_ledgers, mstlookup.GetLookupsList(LookupName.AccountLedger));
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }

            bindgridList();
        }
        protected void GVRecdTransaction_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                RecdTransactionData objfinalbill = new RecdTransactionData();
                RecdTransactionBO objstdBO = new RecdTransactionBO();
                GridView gv = (GridView)e.Row.FindControl("GvChild");
                LinkButton lbl_print = (LinkButton)e.Row.FindControl("lbl_print");

                string VoucherNo = GVRecdTransaction.DataKeys[e.Row.RowIndex].Value.ToString();
                objfinalbill.VoucherNo = VoucherNo;
                List<RecdTransactionData> Result = objstdBO.Get_TransactionDetailsByVoucherNo(objfinalbill);
                if (Result.Count > 0)
                {
                    gv.DataSource = Result;
                    gv.DataBind();
                    gv.Visible = true;
                }
                else
                {
                    gv.DataSource = null;
                    gv.DataBind();
                    gv.Visible = true;
                }
            }
        }
        protected void bindgridList()
        {
            try
            {

                List<RecdTransactionData> objdeposit = GetTransactionList(0);
                if (objdeposit.Count > 0)
                {
                    GVRecdTransaction.DataSource = objdeposit;
                    GVRecdTransaction.DataBind();
                    GVRecdTransaction.Visible = true;
                    Messagealert_.ShowMessage(lblresult2, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    txttotalamt.Text = Commonfunction.Getrounding(objdeposit[0].TotalAmount.ToString());
                    txttotalamtpaid.Text = Commonfunction.Getrounding(objdeposit[0].TotalAmountPaid.ToString());
                    txttotalamtcash.Text = Commonfunction.Getrounding(objdeposit[0].TotalCashPaid.ToString());
                    txttotalamtreceive.Text = Commonfunction.Getrounding(objdeposit[0].TotalCashRecieve.ToString());
                    txtContraRecieve.Text = Commonfunction.Getrounding(objdeposit[0].TotalCashContraRecieve.ToString());
                    txtContrapaid.Text = Commonfunction.Getrounding(objdeposit[0].TotalCashContraPaid.ToString());
                    txtTotalCashOutward.Text = Commonfunction.Getrounding((objdeposit[0].TotalCashContraPaid + objdeposit[0].TotalAmountPaid).ToString());
                    txtCashInHand.Text = Commonfunction.Getrounding(((objdeposit[0].TotalCashRecieve + objdeposit[0].TotalCashContraRecieve) - (objdeposit[0].TotalCashContraPaid + objdeposit[0].TotalAmountPaid)).ToString());
                    div4.Attributes["class"] = "SucessAlert";
                    div4.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;

                }
                else
                {
                    GVRecdTransaction.DataSource = null;
                    GVRecdTransaction.DataBind();
                    GVRecdTransaction.Visible = true;
                    lblresult.Visible = false;
                }
            }

            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<RecdTransactionData> GetTransactionList(int curIndex)
        {
            RecdTransactionData objpat = new RecdTransactionData();
            RecdTransactionBO objbillingBO = new RecdTransactionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.AccountID = Convert.ToInt32(ddl_ledgers.SelectedValue == "" ? "0" : ddl_ledgers.SelectedValue);
            objpat.TransactionTypeID = Convert.ToInt32(ddl_transaction.SelectedValue == "" ? "0" : ddl_transaction.SelectedValue);
            objpat.AccountState = Convert.ToInt32(ddl_account_close.SelectedValue == "" ? "0" : ddl_account_close.SelectedValue);

            objpat.DateFrom = Convert.ToDateTime(from.ToString("yyyy-MM-dd") + " " + "00:00:00");
            objpat.DateTo = Convert.ToDateTime(To.ToString("yyyy-MM-dd") + " " + "23:59:59");

            return objbillingBO.GetManualTransactionList(objpat);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            lblmessage2.Visible = false;
            lblresult2.Visible = false;
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            ddl_transaction.SelectedIndex = 0;
            ddl_ledgers.SelectedIndex = 0;
            GVRecdTransaction.DataSource = null;
            GVRecdTransaction.DataBind();
            GVRecdTransaction.Visible = true;

        }


        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ExportEnable", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddlexport.SelectedIndex == 1)
            {
                ExportoExcel();
            }
            else
            {
                Messagealert_.ShowMessage(lblmessage, "ExportType", 0);
                div1.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "All Transaction Details");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=AllTransactionDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<RecdTransactionData> EmployeeDetails = GetTransactionList(0);
            List<RecdTransactionDatatoExcel> ListexcelData = new List<RecdTransactionDatatoExcel>();
            int i = 0;
            foreach (RecdTransactionData row in EmployeeDetails)
            {
                RecdTransactionDatatoExcel Ecxeclemp = new RecdTransactionDatatoExcel();
                Ecxeclemp.TransactionType = EmployeeDetails[i].TransactionType;
                Ecxeclemp.VoucherNo = EmployeeDetails[i].VoucherNo;
                Ecxeclemp.Particulars = EmployeeDetails[i].Particulars;
                Ecxeclemp.Remarks = EmployeeDetails[i].Remarks;
                Ecxeclemp.Partlyledger = EmployeeDetails[i].Partlyledger;
                Ecxeclemp.AddedDate = EmployeeDetails[i].AddedDate;
                Ecxeclemp.Amount = EmployeeDetails[i].Amount;
                ListexcelData.Add(Ecxeclemp);
                i++;
            }
            RecdTransactionDatatoExcel footerdata;
            footerdata = new RecdTransactionDatatoExcel();
            ListexcelData.Add(footerdata);
            footerdata = new RecdTransactionDatatoExcel();
            footerdata.TransactionType = "Total Transaction";
            footerdata.VoucherNo = "Total Amount Paid";
            footerdata.Particulars = "Total Cash Recieve";
            footerdata.Remarks = "Total Cash Paid";
            ListexcelData.Add(footerdata);

            footerdata = new RecdTransactionDatatoExcel();
            footerdata.TransactionType = EmployeeDetails[0].TotalAmount.ToString();
            footerdata.VoucherNo = EmployeeDetails[0].TotalAmountPaid.ToString();
            footerdata.Particulars = EmployeeDetails[0].TotalCashRecieve.ToString();
            footerdata.Remarks = EmployeeDetails[0].TotalCashPaid.ToString();
            ListexcelData.Add(footerdata);
            footerdata = new RecdTransactionDatatoExcel();
            footerdata.TransactionType = "Total Contra Inward";
            footerdata.VoucherNo = "Total Contra Outward";
            footerdata.Particulars = "Total Cash Outward";
            footerdata.Remarks = "Cash In Hand";
            ListexcelData.Add(footerdata);

            footerdata = new RecdTransactionDatatoExcel();
            footerdata.TransactionType = Commonfunction.Getrounding(EmployeeDetails[0].TotalCashContraRecieve.ToString());
            footerdata.VoucherNo = Commonfunction.Getrounding(EmployeeDetails[0].TotalCashContraPaid.ToString());
            footerdata.Particulars = Commonfunction.Getrounding((EmployeeDetails[0].TotalCashContraPaid + EmployeeDetails[0].TotalAmountPaid).ToString());
            footerdata.Remarks = Commonfunction.Getrounding(((EmployeeDetails[0].TotalCashRecieve + EmployeeDetails[0].TotalCashContraRecieve) - (EmployeeDetails[0].TotalCashContraPaid + EmployeeDetails[0].TotalAmountPaid)).ToString());

            ListexcelData.Add(footerdata);
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
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

        protected void GVRecdTransaction_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
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
                        lblmessage.Visible = false;
                    }
                    RecdTransactionData objpat = new RecdTransactionData();
                    RecdTransactionBO objbillingBO = new RecdTransactionBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GVRecdTransaction.Rows[i];
                    Label VchNo = (Label)gr.Cells[0].FindControl("lblVoucherNo");
                    objpat.VoucherNo = VchNo.Text.Trim();
                    objpat.EmployeeID = LogData.EmployeeID;
                    objpat.ActionType = Enumaction.Delete;
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage2, "Remarks", 0);
                        div2.Visible = true;
                        div2.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objpat.Remarks = txtremarks.Text;
                    }

                    RecdTransactionBO objAccBO = new RecdTransactionBO();
                    int Result = objAccBO.DeleteAccountTransactionDetailsByID(objpat);
                    if (Result == 1 || Result == 2)
                    {
                        lblmessage2.Visible = true;
                        Messagealert_.ShowMessage(lblmessage2, Result == 1 ? "delete" : "AccountClosed", 1);
                        div2.Visible = true;
                        div2.Attributes["class"] = Result == 1 ? "SuccesAlert" : "FailAlert";
                        bindgridList();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage2, "system", 0);
                        div2.Visible = true;
                        div2.Attributes["class"] = "FailAlert";
                        return;
                    }

                }
                if (e.CommandName == "Print")
                {

                    int j = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gv = GVRecdTransaction.Rows[j];
                    Label voucher = (Label)gv.Cells[0].FindControl("lblVoucherNo");
                    string url = "../MedAccount/Reports/ReportViewer.aspx?option=AccountTransaction&voucherNumber=" + voucher.Text.Trim();
                    string fullURL = "window.open('" + url + "', '_blank');";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {

            }
        }



    }
}