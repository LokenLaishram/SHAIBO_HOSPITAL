using AjaxControlToolkit;
using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedPharBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.MedPharData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedPhr
{
	public partial class PHRAccountTransaction : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

				pnl_bank.Visible = false;
				txt_voucher.ReadOnly = true;
				MasterLookupBO mstlookup = new MasterLookupBO();
				Commonfunction.PopulateDdl(ddl_payment_type, mstlookup.GetLookupsList(LookupName.PHRPaymentType));
				Commonfunction.PopulateDdl(ddl_ledger, mstlookup.GetLookupsList(LookupName.PHRAccountLedger));
				Commonfunction.PopulateDdl(ddl_transaction, mstlookup.GetLookupsList(LookupName.PHRPaymentType));
				Commonfunction.PopulateDdl(ddl_ledgers, mstlookup.GetLookupsList(LookupName.PHRAccountLedger));
				ddl_payment_mode.SelectedIndex = 1;
				btnprints.Attributes["disabled"] = "disabled";
				btnsave.Attributes["disabled"] = "diabled";
				txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
				txttimepickerfrom.Text = "12:00 AM";
				txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
				txttimepickerto.Text = "11:59 PM";
				lblgridexpenses.Visible = false;
				lblgridincome.Visible = false;
				cashincome.Visible = false;
				cashexpenses.Visible = false;
				bankincome.Visible = false;
				bankexpenses.Visible = false;
				totalincome.Visible = false;
				totalexpenses.Visible = false;
			}
		}

		protected void ddl_payment_SelectedIndexChanged(object sender, EventArgs e)
		{
			
			if (ddl_payment_type.SelectedIndex>0)
			{
				btnsave.Attributes.Remove("disabled");
			}
			else
			{
				btnsave.Attributes["disabled"]="diabled";
			}
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
		[System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
		public static List<string> GetAccountName(string prefixText, int count, string contextKey)
		{
			PHRAcountLedgerData objData = new PHRAcountLedgerData();
			PHRAccountBO objBO = new PHRAccountBO();
			List<PHRAcountLedgerData> getResult = new List<PHRAcountLedgerData>();
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
		
		protected void btnsave_Click(object sender, EventArgs e)
		{
			try
			{
				IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
				PHRAccountTransactionData objdata = new PHRAccountTransactionData();
			    PHRAccountBO objstdBO = new PHRAccountBO();
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
			if (ddl_ledger.SelectedIndex == 0)
			{

				Messagealert_.ShowMessage(lblmessage, "Please select account head!", 0);
				div1.Visible = true;
				div1.Attributes["class"] = "FailAlert";
				ddl_ledger.Focus();
				return;

			}
			if (Convert.ToDecimal(txt_amount.Text.Trim()=="" ? "0" :txt_amount.Text.Trim())<=0)
			{

				Messagealert_.ShowMessage(lblmessage, "validAmount", 0);
				div1.Visible = true;
				div1.Attributes["class"] = "FailAlert";
				txt_amount.Focus();
				return;

			}
				if (ddl_payment_mode.SelectedIndex == 2)
				{
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
				}
				if (txt_naration.Text.Trim() == "")
			{

				Messagealert_.ShowMessage(lblmessage, "AccountNarration", 0);
				div1.Visible = true;
				div1.Attributes["class"] = "FailAlert";
				txt_naration.Focus();
				return;

			}
					
					objdata.InstrumentName = txt_instrument_name.Text == "" ? null : txt_instrument_name.Text.ToString().Trim();
					objdata.InstrumentDate = Convert.ToDateTime(txt_intrumentDate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_intrumentDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault));
					objdata.AccountID = Convert.ToInt32(ddl_ledger.SelectedValue=="" ? "0" : ddl_ledger.SelectedValue);
					objdata.BankPayeeName = txt_bank_payee_name.Text == "" ? null : txt_bank_payee_name.Text.ToString().Trim();
					objdata.BankBranchName = txt_bank_branch_name.Text == "" ? null : txt_bank_branch_name.Text.ToString().Trim();
					objdata.PaymentType = Convert.ToInt32(ddl_payment_type.SelectedValue);
			     	objdata.PaymentMode = Convert.ToInt32(ddl_payment_mode.SelectedValue);
				    objdata.TransactionType = Convert.ToInt32(ddl_payment_type.SelectedValue);
					objdata.Amount = Convert.ToDecimal(txt_amount.Text.Trim() == "" ? "0" : txt_amount.Text.Trim());
				    objdata.Date = System.DateTime.Now;
				if (ddl_payment_mode.SelectedIndex == 2)
				{
					objdata.Naration = txt_naration.Text + " Transaction Type:"  + " Instrument Name:" + txt_instrument_name.Text + " Payee Name:" + txt_bank_payee_name.Text + " Branch Name:" + txt_bank_branch_name.Text;
				}
				else
				{
					objdata.Naration = txt_naration.Text == "" ? null : txt_naration.Text.ToString().Trim();
				}
				objdata.EmployeeID = LogData.EmployeeID;
				objdata.HospitalID = LogData.HospitalID;
				objdata.FinancialYearID = LogData.FinancialYearID;


				PHRAccountTransactionOutput outputdata = new PHRAccountTransactionOutput();

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
			ddl_payment_mode.SelectedIndex = 1;
			txt_amount.Text = "";
			ddl_ledger.SelectedIndex = 0;
			txt_voucher.Text = "";
			txt_naration.Text = "";
			txt_instrument_name.Text = "";
			txt_bank_payee_name.Text = "";
			txt_cross_inst.Text = "";
			txt_intrumentDate.Text = "";
			txt_bank_branch_name.Text = "";
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

	

		protected void btnreset_Click(object sender, EventArgs e)
		{
			reset();
		}

		protected void btnprints_Click(object sender, EventArgs e)
		{
			string url = "../MedPhr/Reports/ReportViewer.aspx?option=ManualAccountTransaction&voucherNumber=" + txt_voucher.Text;
			string fullURL = "window.open('" + url + "', '_blank');";
			ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
		}

		protected void btnsearch_Click(object sender, EventArgs e)
		{
			transactiontype();
		}

		private void IncomeBindGrid(int page)
		{
			try
			{

				List<PHRAccountTransactionData> objincome = GetIncomeTransactionList(page);
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
						lbldescription.Text = " No. Of Recieved Transaction : " + objincome[0].noofIncometran.ToString()
										   + " | No. Of Cash Recieved : " + objincome[0].noofincomecashtran.ToString()
										   + " | No. Of Bank Recieved : " + objincome[0].noofincomebanktran.ToString()
										   + " | No. Of Payment Transaction : " + objincome[0].noofexpensestran.ToString()
										   + " | No. Of Cash Payment : " + objincome[0].noofexpensescashtran.ToString()
										   + " | No. Of Bank Payment : " + objincome[0].noofexpensesbanktran.ToString();
					}
					else
					{
						lbldescription.Text = "No. Of Payment Transaction : " + objincome[0].noofexpensestran.ToString()
										   + " | No. Of Cash Payment : " + objincome[0].noofexpensescashtran.ToString()
										   + " | No. Of Bank Payment : " + objincome[0].noofexpensesbanktran.ToString();
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
					lbldescription.Text = "";
				}
			}

			catch (Exception ex)
			{
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
				Messagealert_.ShowMessage(lblmessage, "system", 0);
			}
		}
		public List<PHRAccountTransactionData> GetIncomeTransactionList(int curIndex)
		{
			PHRAccountTransactionData ObjData = new PHRAccountTransactionData();
			PHRAccountBO objgBO = new PHRAccountBO();
			IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
			DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			ObjData.TransactionID = Convert.ToInt32(ddl_transaction.SelectedValue == "" ? "0" : ddl_transaction.SelectedValue);
			ObjData.AccountID = Convert.ToInt32(ddl_ledgers.SelectedValue == "" ? "0" : ddl_ledgers.SelectedValue);
			ObjData.AccountState = Convert.ToInt32(ddl_account_close.SelectedValue == "" ? "0" : ddl_account_close.SelectedValue);
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

				List<PHRAccountTransactionData> objexpenses = GetExpensesTransactionList(page);
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
					
					if (ddl_transaction.SelectedIndex == 0)
					{
						lbldescription.Text = " No. Of Recieved Transaction : " + objexpenses[0].noofIncometran.ToString()
										   + " | No. Of Cash Recieved  : " + objexpenses[0].noofincomecashtran.ToString()
										   + " | No. Of Bank Recieved  : " + objexpenses[0].noofincomebanktran.ToString()
										   + " | No. Of Payment Transaction : " + objexpenses[0].noofexpensestran.ToString()
										   + " | No. Of Cash Payment : " + objexpenses[0].noofexpensescashtran.ToString()
										   + " | No. Of Bank Payment : " + objexpenses[0].noofexpensesbanktran.ToString();
					}
					else
					{
						lbldescription.Text = "No. Of Payment Transaction : " + objexpenses[0].noofexpensestran.ToString()
										   + " | No. Of Cash Payment : " + objexpenses[0].noofexpensescashtran.ToString()
										   + " | No. Of Bank Payment : " + objexpenses[0].noofexpensesbanktran.ToString();
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
					lbldescription.Text = "";
				}
			}


			catch (Exception ex)
			{
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
				Messagealert_.ShowMessage(lblmessage, "system", 0);
			}
		}
		public List<PHRAccountTransactionData> GetExpensesTransactionList(int curIndex)
		{
			PHRAccountTransactionData ObjData = new PHRAccountTransactionData();
			PHRAccountBO objgBO = new PHRAccountBO();
			IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
			DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			ObjData.TransactionID = Convert.ToInt32(ddl_transaction.SelectedValue == "" ? "0" : ddl_transaction.SelectedValue);
			ObjData.AccountID = Convert.ToInt32(ddl_ledgers.SelectedValue == "" ? "0" : ddl_ledgers.SelectedValue);
			ObjData.AccountState = Convert.ToInt32(ddl_account_close.SelectedValue == "" ? "0" : ddl_account_close.SelectedValue);
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
				Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
				div3.Visible = true;
				div3.Attributes["class"] = "FailAlert";
				return;
			}
			else
			{
				lblmessage2.Visible = false;
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
			}
		}

		protected void ddl_transaction_SelectedIndexChanged(object sender, EventArgs e)
		{
			transactiontype();
		}
		protected void ddl_ledgers_SelectedIndexChanged(object sender, EventArgs e)
		{
			transactiontype();
		}
		protected void ddl_account_close_SelectedIndexChanged(object sender, EventArgs e)
		{
			transactiontype();
		}
		protected void btnresets_Click(object sender, EventArgs e)
		{

			ddl_transaction.SelectedIndex = 0;
			ddl_account_close.SelectedIndex = 0;
			txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
			txttimepickerfrom.Text = "12:00 AM";
			txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
			txttimepickerto.Text = "11:59 PM";
			lblgridexpenses.Visible = false;
			lblgridincome.Visible = false;
			cashincome.Visible = false;
			cashexpenses.Visible = false;
			bankincome.Visible = false;
			bankexpenses.Visible = false;
			totalincome.Visible = false;
			totalexpenses.Visible = false;
		}


	

		
	}
}