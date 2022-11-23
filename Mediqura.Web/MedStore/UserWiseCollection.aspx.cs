using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedStore;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedStore;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedStore
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
			Commonfunction.PopulateDdl(ddl_transaction, mstlookup.GetLookupsList(LookupName.PHRtransactionType));
			txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
			txttimepickerfrom.Text = "00:00:00";
			txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
			txttimepickerto.Text = "23:59:59";
			lblgridexpenses.Visible = false;
			lblgridincome.Visible = false;
			cashincome.Visible = false;
			cashexpenses.Visible = false;
			bankincome.Visible = false;
			bankexpenses.Visible = false;
			totalincome.Visible = false;
			totalexpenses.Visible = false;
			totalbalance.Visible = false;
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
			transactiontype();
		}

		protected void btn_print_Click(object sender, EventArgs e)
		{
			IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
			DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			string TransactionID = ddl_transaction.SelectedValue == "" ? "0" : ddl_transaction.SelectedValue;
			string CollectedByID = LogData.EmployeeID.ToString();
			string AccountState = ddl_account_close.SelectedValue == "" ? "0" : ddl_account_close.SelectedValue;
			string timefrom = txttimepickerfrom.Text.Trim();
			string timeto = txttimepickerto.Text.Trim();
			string url = "../MedPhr/Reports/ReportViewer.aspx?option=TransactionStatement&TransactionID=" + TransactionID.Trim() + "&CollectedByID=" + CollectedByID + "&AccountState=" + AccountState + "&AccountID=0" + "&from=" + (from.ToString("yyyy-MM-dd") + " " + timefrom).ToString() + "&To=" + (To.ToString("yyyy-MM-dd") + " " + timeto).ToString();
			string fullURL = "window.open('" + url + "', '_blank');";
			ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
		}

		protected void btnresets_Click(object sender, EventArgs e)
		{
			
			ddl_transaction.SelectedIndex = 0;
			ddl_account_close.SelectedIndex = 0;
			txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
			txttimepickerfrom.Text = "00:00:00";
			txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
			txttimepickerto.Text = "23:59:59";
			lblgridexpenses.Visible = false;
			lblgridincome.Visible = false;
			cashincome.Visible = false;
			cashexpenses.Visible = false;
			bankincome.Visible = false;
			bankexpenses.Visible = false;
			totalincome.Visible = false;
			totalexpenses.Visible = false;
			totalbalance.Visible = false;
		}

		


		private void IncomeBindGrid(int page)
		{
			try
			{

				List<TransactionSummaryData> objincome = GetIncomeTransactionList(page);
				if (objincome.Count > 0)
				{
					Gv_incomereport.DataSource = objincome;
					Gv_incomereport.DataBind();
					Gv_incomereport.VirtualItemCount = objincome.Count;
					Gv_incomereport.PageIndex = page - 1;
					Gv_incomereport.Visible = true;
					GV_expensesreport.Visible = false;
					lblgridincome.Visible = true;
					txt_BankIncome.Text = Commonfunction.Getrounding(objincome[0].BankIncome.ToString("N2"));
					txt_CashIncome.Text = Commonfunction.Getrounding(objincome[0].CashIncome.ToString("N2"));
					txt_TotalIncome.Text = Commonfunction.Getrounding(objincome[0].TotalIncome.ToString("N2"));
					if (ddl_transaction.SelectedIndex == 0)
					{
						lbldescription.Text = " No. Of Income Transaction : " + objincome[0].noofIncometran.ToString()
										   + " | No. Of Cash Income Transaction : " + objincome[0].noofincomecashtran.ToString()
										   + " | No. Of Bank Income Transaction : " + objincome[0].noofincomebanktran.ToString() 
										   + " No. Of Expenses Transaction : " + objincome[0].noofexpensestran.ToString()
										   + " | No. Of Cash Expenses Transaction : " + objincome[0].noofexpensescashtran.ToString()
										   + " | No. Of Bank Expenses Transaction : " + objincome[0].noofexpensesbanktran.ToString();
					}
					else
					{
						lbldescription.Text = "No. Of Expenses Transaction : " + objincome[0].noofexpensestran.ToString()
										   + " | No. Of Cash Expenses Transaction : " + objincome[0].noofexpensescashtran.ToString()
										   + " | No. Of Bank Expenses Transaction : " + objincome[0].noofexpensesbanktran.ToString();
					}
				}
				else
				{
					Gv_incomereport.DataSource = null;
					Gv_incomereport.DataBind();
					Gv_incomereport.Visible = false;
					GV_expensesreport.Visible = false;
					lblgridincome.Visible = false;
					lblresult.Visible = false;
					txt_BankIncome.Text = "";
					txt_CashIncome.Text = "";
					txt_TotalIncome.Text = "";
					txt_Balance.Text = "";
					lbldescription.Text = "";
				}
			}

			catch (Exception ex)
			{
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
				Messagealert_.ShowMessage(lblmessage, "system", 0);
			}
		}
		public List<TransactionSummaryData> GetIncomeTransactionList(int curIndex)
		{
			TransactionSummaryData ObjData = new TransactionSummaryData();
			TransactionSummaryBO objgBO = new TransactionSummaryBO();
			IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
			DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			ObjData.TransactionID = Convert.ToInt32(ddl_transaction.SelectedValue == "" ? "0" : ddl_transaction.SelectedValue);
			ObjData.CollectedByID = LogData.EmployeeID;
			ObjData.AccountState = Convert.ToInt32(ddl_account_close.SelectedValue == "" ? "0" : ddl_account_close.SelectedValue);
			ObjData.Paymode = Convert.ToInt32(ddl_paymentmode.SelectedValue == "" ? "0" : ddl_paymentmode.SelectedValue);
			ObjData.EmpName = LogData.EmpName;
			string timefrom = txttimepickerfrom.Text.Trim();
			string timeto = txttimepickerto.Text.Trim();
			ObjData.DateFrom = Convert.ToDateTime(from.ToString("yyyy-MM-dd") + " " + timefrom);
			ObjData.DateTo = Convert.ToDateTime(To.ToString("yyyy-MM-dd") + " " + timeto);			
			return objgBO.GetIncomeTransactionList(ObjData);

			
		}

		private void ExpensesBindGrid(int page)
		{
			try
			{

				List<TransactionSummaryData> objexpenses = GetExpensesTransactionList(page);
				if (objexpenses.Count > 0)
				{
					GV_expensesreport.DataSource = objexpenses;
					GV_expensesreport.DataBind();
					GV_expensesreport.VirtualItemCount = objexpenses.Count;
					GV_expensesreport.PageIndex = page - 1;
					Gv_incomereport.Visible = false;
					GV_expensesreport.Visible = true;
					lblgridexpenses.Visible = true;
					txt_BankExpenses.Text = Commonfunction.Getrounding(objexpenses[0].BankExpense.ToString("N2"));
					txt_CashExpenses.Text = Commonfunction.Getrounding(objexpenses[0].CashExpense.ToString("N2"));
					txt_TotalExpenses.Text = Commonfunction.Getrounding(objexpenses[0].TotalExpense.ToString("N2"));
					txt_Balance.Text = (Convert.ToDecimal(txt_TotalIncome.Text.Trim() == "" ? "0" : txt_TotalIncome.Text.Trim()) - Convert.ToDecimal(txt_TotalExpenses.Text.Trim() == "" ? "0" : txt_TotalExpenses.Text.Trim())).ToString("N2");
					if (ddl_transaction.SelectedIndex == 0)
					{
						lbldescription.Text = " No. Of Income Transaction : " + objexpenses[0].noofIncometran.ToString()
										   + " | No. Of Cash Income Transaction : " + objexpenses[0].noofincomecashtran.ToString()
										   + " | No. Of Bank Income Transaction : " + objexpenses[0].noofincomebanktran.ToString() 
										   + " No. Of Expenses Transaction : " + objexpenses[0].noofexpensestran.ToString()
										   + " | No. Of Cash Expenses Transaction : " + objexpenses[0].noofexpensescashtran.ToString()
										   + " | No. Of Bank Expenses Transaction : " + objexpenses[0].noofexpensesbanktran.ToString();
					}
					else
					{
						lbldescription.Text = "No. Of Expenses Transaction : " + objexpenses[0].noofexpensestran.ToString()
										   + " | No. Of Cash Expenses Transaction : " + objexpenses[0].noofexpensescashtran.ToString()
										   + " | No. Of Bank Expenses Transaction : " + objexpenses[0].noofexpensesbanktran.ToString();
					}

				}
				else
				{
					GV_expensesreport.DataSource = null;
					GV_expensesreport.DataBind();
					GV_expensesreport.Visible = false;
					Gv_incomereport.Visible = false;
					lblgridexpenses.Visible = false;
					lblresult.Visible = false;
					txt_BankExpenses.Text = "";
					txt_CashExpenses.Text = "";
					txt_TotalExpenses.Text = "";
					txt_Balance.Text = (Convert.ToDecimal(txt_TotalIncome.Text.Trim() == "" ? "0" : txt_TotalIncome.Text.Trim()) - Convert.ToDecimal(txt_TotalExpenses.Text.Trim() == "" ? "0" : txt_TotalExpenses.Text.Trim())).ToString("N2");
					
						lbldescription.Text = "";
					}
				}
			

			catch (Exception ex)
			{
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
				Messagealert_.ShowMessage(lblmessage, "system", 0);
			}
		}
		public List<TransactionSummaryData> GetExpensesTransactionList(int curIndex)
		{
			TransactionSummaryData ObjData = new TransactionSummaryData();
			TransactionSummaryBO objgBO = new TransactionSummaryBO();
			IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
			DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			ObjData.TransactionID = Convert.ToInt32(ddl_transaction.SelectedValue == "" ? "0" : ddl_transaction.SelectedValue);
			ObjData.CollectedByID = LogData.EmployeeID;
			ObjData.AccountState = Convert.ToInt32(ddl_account_close.SelectedValue == "" ? "0" : ddl_account_close.SelectedValue);
			ObjData.Paymode = Convert.ToInt32(ddl_paymentmode.SelectedValue == "" ? "0" : ddl_paymentmode.SelectedValue);
			ObjData.EmpName = LogData.EmpName;
			string timefrom = txttimepickerfrom.Text.Trim();
			string timeto = txttimepickerto.Text.Trim();
			ObjData.DateFrom = Convert.ToDateTime(from.ToString("yyyy-MM-dd") + " " + timefrom);
			ObjData.DateTo = Convert.ToDateTime(To.ToString("yyyy-MM-dd") + " " + timeto);
			
			return objgBO.GetExpensesTransactionList(ObjData);
		}
		

		private void transactiontype()
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

			if (ddl_transaction.SelectedValue == "1")
			{
				IncomeBindGrid(1);
				GV_expensesreport.Visible = false;
				Gv_incomereport.Visible = true;
				lblgridexpenses.Visible = false;
				lblgridincome.Visible = true;
				cashincome.Visible = true;
				cashexpenses.Visible = false;
				bankincome.Visible = true;
				bankexpenses.Visible = false;
				totalincome.Visible = true;
				totalexpenses.Visible = false;
				totalbalance.Visible = false;

			}
			else if (ddl_transaction.SelectedValue == "2")
			{
				ExpensesBindGrid(1);
				Gv_incomereport.Visible = false;
				GV_expensesreport.Visible = true;
				lblgridexpenses.Visible = true;
				lblgridincome.Visible = false;
				cashincome.Visible = false;
				cashexpenses.Visible = true;
				bankincome.Visible = false;
				bankexpenses.Visible = true;
				totalincome.Visible = false;
				totalexpenses.Visible = true;
				totalbalance.Visible = false;
			}
			else
			{
				IncomeBindGrid(1);
				ExpensesBindGrid(1);
				GV_expensesreport.Visible = true;
				Gv_incomereport.Visible = true;
				lblgridexpenses.Visible = true;
				lblgridincome.Visible = true;
				cashincome.Visible = true;
				cashexpenses.Visible = true;
				bankincome.Visible = true;
				bankexpenses.Visible = true;
				totalincome.Visible = true;
				totalexpenses.Visible = true;
				totalbalance.Visible = true;
			}
		}

		protected void ddl_transaction_SelectedIndexChanged(object sender, EventArgs e)
		{
			transactiontype();
		}
		protected void ddl_paymentmode_SelectedIndexChanged(object sender, EventArgs e)
		{
			transactiontype();
		}
		protected void ddl_account_close_SelectedIndexChanged(object sender, EventArgs e)
		{
			transactiontype();
		}
		
	}
}