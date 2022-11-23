using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MIS;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MIS;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MIS
{
    public partial class UserWiseCollection : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
            }
        }

        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_accounthead, mstlookup.GetLookupsList(LookupName.CollectionLedger));
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            if (LogData.PrintEnable == 0)
            {
                btn_print.Attributes["disabled"] = "disabled";
            }
            else
            {
                btn_print.Attributes.Remove("disabled");
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
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

            bindgrid();
        }

        protected void bindgrid()
        {
            try
            {

                List<MisReportData> objdeposit = GetTransactionList(0);
                if (objdeposit.Count > 0)
                {
                    Gv_collectionreport.DataSource = objdeposit;
                    Gv_collectionreport.DataBind();
                    Gv_collectionreport.Visible = true;
                    txtTotalIncome.Text = Commonfunction.Getrounding(objdeposit[0].TotalIncome.ToString());
                    txtTotalExpenses.Text = Commonfunction.Getrounding(objdeposit[0].TotalExpense.ToString());
                    txtcashIncome.Text = Commonfunction.Getrounding(objdeposit[0].CashIncome.ToString());
                    txtCashExpense.Text = Commonfunction.Getrounding(objdeposit[0].cashExpense.ToString());
                    txtIncomeExpDiff.Text = Commonfunction.Getrounding(objdeposit[0].IncomeExpDiff.ToString());
                    txtbankDiposit.Text = Commonfunction.Getrounding(objdeposit[0].TotalDiposit.ToString());
                    txtBankWithdraw.Text = Commonfunction.Getrounding((objdeposit[0].TotalWithdraw).ToString());
                    txtCashInHand.Text = Commonfunction.Getrounding((objdeposit[0].CashInHand).ToString());
                    txtTotalReceivable.Text = Commonfunction.Getrounding((objdeposit[0].TotalRecievable).ToString());
                    txtbankIncome.Text = Commonfunction.Getrounding((objdeposit[0].BankIncome).ToString());
                    txtBankExpense.Text = Commonfunction.Getrounding((objdeposit[0].BankExpense).ToString());
                    txtTotalCashOutward.Text = Commonfunction.Getrounding((objdeposit[0].cashExpense + objdeposit[0].TotalDiposit).ToString());
                    txtCashIncomeExpDiff.Text = Commonfunction.Getrounding((objdeposit[0].TotalCashDiff).ToString());
                    txtCashExpenseTot.Text = Commonfunction.Getrounding((objdeposit[0].cashExpense + objdeposit[0].cashExpense).ToString());
                    txtCashIncomeTotal.Text = Commonfunction.Getrounding((objdeposit[0].TotalCashIncomeAndAdvance).ToString());
                    txtCashOnlyCash.Text = Commonfunction.Getrounding((objdeposit[0].TotalCash).ToString());

                }
                else
                {
                    Gv_collectionreport.DataSource = null;
                    Gv_collectionreport.DataBind();
                    Gv_collectionreport.Visible = true;
                    lblresult.Visible = false;
                }
            }

            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<MisReportData> GetTransactionList(int curIndex)
        {
            MisReportData ObjData = new MisReportData();
            MisReportBO objgBO = new MisReportBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            ObjData.LedgerID = Convert.ToInt32(ddl_accounthead.SelectedValue == "" ? "0" : ddl_accounthead.SelectedValue);
            ObjData.TransactionType = 0;
            ObjData.CollectedByID = LogData.EmployeeID;
            ObjData.AccountState = Convert.ToInt32(ddl_account_close.SelectedValue == "" ? "0" : ddl_account_close.SelectedValue);
            ObjData.EmpName = LogData.EmpName;

            string timefrom = txttimepickerfrom.Text.Trim();
            string timeto = txttimepickerto.Text.Trim();
            ObjData.DateFrom = Convert.ToDateTime(from.ToString("yyyy-MM-dd") + " " + timefrom);
            ObjData.DateTo = Convert.ToDateTime(To.ToString("yyyy-MM-dd") + " " + timeto);

            return objgBO.GetTransactionList(ObjData);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            lblmessage.Visible = false;
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        
            ddl_accounthead.SelectedIndex = 0;
         
            Gv_collectionreport.DataSource = null;
            Gv_collectionreport.DataBind();
            Gv_collectionreport.Visible = true;
            txttimepickerfrom.Text = "";
            txttimepickerto.Text = "";
            txtTotalIncome.Text = "0.00";
            txtTotalExpenses.Text = "0.00";
            txtcashIncome.Text = "0.00";
            txtCashExpense.Text = "0.00";
            txtIncomeExpDiff.Text = "0.00";
            txtbankDiposit.Text = "0.00";
            txtBankWithdraw.Text = "0.00";
            txtCashInHand.Text = "0.00";
            txtTotalReceivable.Text = "0.00";
            txtbankIncome.Text = "0.00";
            txtBankExpense.Text = "0.00";
            txtTotalCashOutward.Text = "0.00";


        }


        protected void Gv_collectionreport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label isSubHeading = (Label)e.Row.FindControl("lblIsSubHeading");
                Label lblVoucher = (Label)e.Row.FindControl("lblVoucher");
                Label lblTransactionAmount = (Label)e.Row.FindControl("lblTransactionAmount");

                if (Convert.ToInt32(isSubHeading.Text) == 1)
                {
                    if (Convert.ToDecimal(lblTransactionAmount.Text) > 0)
                    {
                        e.Row.BackColor = Color.FromName("#cfede3");
                        lblVoucher.ForeColor = Color.FromName("#fd0808");
                        lblTransactionAmount.ForeColor = Color.FromName("#fd0808");
                        GridView editGrid = sender as GridView;

                        e.Row.Cells[0].Controls.Clear();

                        e.Row.Cells[1].ColumnSpan = 2;
                        e.Row.Cells[2].Visible = false;

                        e.Row.Cells[3].Controls.Clear();

                        e.Row.Cells[4].Controls.Clear();

                        e.Row.Cells[5].Controls.Clear();

                        e.Row.Cells[6].Controls.Clear();
                    }
                    else
                    {
                        e.Row.Visible = false;
                    }



                }
                else if (Convert.ToInt32(isSubHeading.Text) == 2)
                {
                    if (Convert.ToDecimal(lblTransactionAmount.Text) > 0)
                    {
                        e.Row.BackColor = Color.FromName("#33aa99");
                        lblVoucher.ForeColor = Color.FromName("#FFFFFF");
                        GridView editGrid = sender as GridView;

                        e.Row.Cells[0].Controls.Clear();

                        e.Row.Cells[1].ColumnSpan = 2;
                        e.Row.Cells[2].Visible = false;

                        e.Row.Cells[3].Controls.Clear();

                        e.Row.Cells[4].Controls.Clear();

                        e.Row.Cells[5].Controls.Clear();

                        e.Row.Cells[6].Controls.Clear();

                        e.Row.Cells[7].Controls.Clear();
                    }
                    else
                    {
                        e.Row.Visible = false;
                    }
                }


            }

        }
        protected void btn_print_Click(object sender, EventArgs e)
        {
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            int ledger = Convert.ToInt32(ddl_accounthead.SelectedValue == "" ? "0" : ddl_accounthead.SelectedValue);
            int tType = 0;
            Int64 collectedBy = LogData.EmployeeID;
            int AcntStat = Convert.ToInt32(ddl_account_close.SelectedValue == "" ? "0" : ddl_account_close.SelectedValue);
            string name = LogData.EmpName;

            string timefrom = txttimepickerfrom.Text.Trim();
            string timeto = txttimepickerto.Text.Trim();
            string url = "../MIS/Reports/ReportViewer.aspx?option=UserWise&ledger=" + ledger.ToString() + "&Ttype=" + tType.ToString() + "&CollectedBy=" + collectedBy.ToString() + "&status=" + AcntStat.ToString() + "&Datefrom=" + (from.ToString("yyyy-MM-dd") + " " + timefrom).ToString() + "&Dateto=" + (To.ToString("yyyy-MM-dd") + " " + timeto).ToString();
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
    }
}