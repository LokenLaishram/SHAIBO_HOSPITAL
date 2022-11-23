using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedPharBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedBillData;
using Mediqura.CommonData.MedPharData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedPhr
{
	public partial class Vendor_Payment : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				MasterLookupBO mstlookup = new MasterLookupBO();
				Commonfunction.PopulateDdl(ddl_vendor, mstlookup.GetLookupsList(LookupName.Supplier));
				Commonfunction.PopulateDdl(ddl_tap3supplier, mstlookup.GetLookupsList(LookupName.Supplier));
				Commonfunction.PopulateDdl(ddl_purchasesupplier, mstlookup.GetLookupsList(LookupName.Supplier));
				Commonfunction.PopulateDdl(ddl_tap2paymentmode, mstlookup.GetLookupsList(LookupName.PaymentMode));
				Commonfunction.PopulateDdl(ddl_tap3paymentmode, mstlookup.GetLookupsList(LookupName.PaymentMode));
				txt_purchasedatefrom.Text = DateTime.Today.ToString("dd/MM/yyyy");
				txt_purchasedateto.Text = DateTime.Today.ToString("dd/MM/yyyy");
			



				btnpaid.Attributes["disabled"] = "disabled";
				btnpaidprint.Attributes["disabled"] = "disabled";
			}
		}
		private void bindpurchasegrid()
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
			IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
			VendorPaymentData objdata = new VendorPaymentData();
			VendorPaymentBO objbo = new VendorPaymentBO();
			DateTime From = DateTime.Parse(txt_purchasedatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			DateTime To = DateTime.Parse(txt_purchasedateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			objdata.DateFrom = From;
			objdata.DateTo = To;
			objdata.SupplierID = ddl_purchasesupplier.SelectedValue == "0" ? 0 : Convert.ToInt32(ddl_purchasesupplier.SelectedValue);
			;
			List<VendorPaymentData> lstdataresult = new List<VendorPaymentData>();
			lstdataresult = objbo.GetVendorPurchaseDetails(objdata);
			if (lstdataresult.Count > 0)
			{
				GVPurchaseList.DataSource = lstdataresult;
				GVPurchaseList.DataBind();
				GVPurchaseList.Visible = true;
				Messagealert_.ShowMessage(lblpurchasemsg, "Total:" + lstdataresult.Count + " Record(s) found.", 1);
				divpurchase.Attributes["class"] = "SucessAlert";
				txt_totalpurcaseamount.Text = Commonfunction.Getrounding(lstdataresult[0].GrandTotalAmount.ToString("N2"));
				txt_totalpurcasepaid.Text = Commonfunction.Getrounding(lstdataresult[0].GrandTotalPaidAmount.ToString("N2"));
				txt_purcasedue.Text = Commonfunction.Getrounding(lstdataresult[0].GrandTotalDueAmount.ToString("N2"));
			}
			else
			{
				GVPurchaseList.DataSource = null;
				GVPurchaseList.DataBind();
				GVPurchaseList.Visible = true;
				divpurchase.Visible = false;
				lblresult.Visible = false;
				lblpurchasemsg.Text = "";
				txt_totalpurcaseamount.Text = "0.0";
				txt_totalpurcasepaid.Text = "0.0";
				txt_purcasedue.Text = "0.0";
			}
		}
		protected void ddl_purchasesupplier_SelectedIndexChanged(object sender, EventArgs e)
		{
			bindpurchasegrid();
		}
		protected void btnpurchasesearch_Click(object sender, EventArgs e)
		{
			bindpurchasegrid();
		}

		protected void GVPurchaseList_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			try
			{
				if (e.Row.RowType == DataControlRowType.DataRow)
				{
					VendorPaymentData objdata = new VendorPaymentData();
					VendorPaymentBO objbo = new VendorPaymentBO();
					Label lblReceiptNo = (Label)e.Row.FindControl("lblpurcaseReceiptNo");
					Label lblpurcaseSupplierID = (Label)e.Row.FindControl("lblpurcaseSupplierID");

					objdata.ReceiptNo = lblReceiptNo.Text.Trim();
					objdata.SupplierID = Convert.ToInt32(lblpurcaseSupplierID.Text.Trim());

					List<VendorPaymentData> GetResult = objbo.SearchChildvendorpaymentDetails(objdata);
					if (GetResult.Count > 0)
					{
						GridView SC = (GridView)e.Row.FindControl("GridChildpurchase");
						SC.DataSource = GetResult;
						SC.DataBind();
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
		protected void ddl_vendor_SelectedIndexChanged(object sender, EventArgs e)
		{
			bindgrid();
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
		private void bindgrid()
		{
			IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
			VendorPaymentData objdata = new VendorPaymentData();
			VendorPaymentBO objbo = new VendorPaymentBO();
			objdata.SupplierID = Convert.ToInt32(ddl_vendor.SelectedValue==""? "0": ddl_vendor.SelectedValue);
			DateTime From = txt_datefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_datefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txt_dateto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_dateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			objdata.DateFrom = From;
			objdata.DateTo = To;
			objdata.PaymentStatus=Convert.ToInt32(ddlstatus.SelectedValue);
			List <VendorPaymentData> lstdataresult= new List <VendorPaymentData>();
			lstdataresult = objbo.GetVendorPaymentDetails(objdata);
			if (lstdataresult.Count > 0)
			{
				gvVendorpay.DataSource = lstdataresult;
				gvVendorpay.DataBind();
				gvVendorpay.Visible = true;
				Messagealert_.ShowMessage(lblresult, "Total:" + lstdataresult.Count + " Record(s) found.", 1);
				div1.Attributes["class"] = "SucessAlert";
				txt_TotalAmount.Text = Commonfunction.Getrounding(lstdataresult[0].GrandTotalAmount.ToString("N2"));
				txt_TotalPaid.Text = Commonfunction.Getrounding(lstdataresult[0].GrandTotalPaidAmount.ToString("N2"));
				txt_TotalDue.Text = Commonfunction.Getrounding(lstdataresult[0].GrandTotalDueAmount.ToString("N2"));
			}
			else
			{
				gvVendorpay.DataSource = null;
				gvVendorpay.DataBind();
				gvVendorpay.Visible = true;
				div1.Visible = false;
				lblresult.Visible = false;
				txt_TotalAmount.Text = "0.0";
				txt_TotalPaid.Text = "0.0";
				txt_TotalDue.Text = "0.0";
			}

		}

		protected void btnresets_Click(object sender, EventArgs e)

		{
			divmsg1.Visible = false;
			lblmessage.Text = "";
			ddl_vendor.SelectedIndex = 0;
			ddlstatus.SelectedIndex = 0;
			txt_datefrom.Text = "";
			txt_dateto.Text = "";
			gvVendorpay.DataSource = null;
			gvVendorpay.DataBind();
			gvVendorpay.Visible = true;
			div1.Visible = false;
			lblresult.Visible = false;
			txt_TotalAmount.Text = "";
			txt_TotalPaid.Text = "";
			txt_TotalDue.Text = "";
		}
		protected void gvVendorpay_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				if (e.CommandName == "Details")
				{
					IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
					VendorPaymentData objdata = new VendorPaymentData();
					VendorPaymentBO objbo = new VendorPaymentBO();
					int i = Convert.ToInt16(e.CommandArgument.ToString());
					GridViewRow gr = gvVendorpay.Rows[i];
					Label lblSupplierID = (Label)gr.Cells[0].FindControl("lblSupplierID");
					objdata.SupplierID = Convert.ToInt32(lblSupplierID.Text.Trim());
					DateTime From = txt_datefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_datefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
					DateTime To = txt_dateto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_dateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
					objdata.DateFrom = From;
					objdata.DateTo = To;
					objdata.PaymentStatus = Convert.ToInt32(ddlstatus.SelectedValue);
					List<VendorPaymentData> lstdataresult = new List<VendorPaymentData>();
					lstdataresult = objbo.GetVendorPaymentbySupplierID(objdata);
					if (lstdataresult.Count > 0)
					{
						divmsg2.Visible = false;
						lblmessage1.Text = "";
						txt_tap2supplier.Text = lstdataresult[0].SupplierName.ToString();
						txt_tap2supplierID.Text = lstdataresult[0].SupplierID.ToString();
						txt_tap2datefrom.Text = txt_datefrom.Text;
						txt_dateto.Text = txt_dateto.Text;
						txt_PaymentNo.Text = "";
						txt_PaymentNo.BorderColor = System.Drawing.Color.Transparent;
						txt_tap2totalamount.Text = lstdataresult[0].GrandTotalPayableAmount.ToString("N2");
						txt_tap2payableamount.Text = "";
						txt_tap2dueamount.Text = "";
						txt_tap2Paidamount.Text = "";
						txt_tap2remark.Text = "";
						txtbank.Text = "";
						txt_chequenumber.Text = "";
						txtinvoicenumber.Text = "";
						ddl_tap2paymentmode.SelectedIndex = 0;
					
						btnpaid.Attributes["disabled"] = "disabled";
						btnpaidprint.Attributes["disabled"] = "disabled";
						lblmessage1.Text = "";
						divmsg2.Visible = false;
						if (lstdataresult[0].GrandTotalPayableAmount == 0)
						{
							btnpaid.Attributes["disabled"] = "disabled";
							txt_tap2Paidamount.Attributes["disabled"] = "disabled";
							btnpaidprint.Attributes["disabled"] = "disabled";
						}
						else
						{
							btnpaid.Attributes.Remove("disabled");
							txt_tap2Paidamount.Attributes.Remove("disabled");
							btnpaidprint.Attributes.Remove("disabled");
						}
						gvvendorrecordlist.DataSource = lstdataresult;
						gvvendorrecordlist.DataBind();
						tabcontainervendorpay.ActiveTabIndex = 2;
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

		protected void gvvendorrecordlist_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			try
			{
				if (e.Row.RowType == DataControlRowType.DataRow)
				{
					VendorPaymentData objdata = new VendorPaymentData();
					VendorPaymentBO objbo = new VendorPaymentBO();
					Label lblReceiptNo = (Label)e.Row.FindControl("lblReceiptNo");
					Label lbl_SupplierID = (Label)e.Row.FindControl("lbl_SupplierID");
					objdata.ReceiptNo = lblReceiptNo.Text.Trim();
					objdata.SupplierID = Convert.ToInt32(lbl_SupplierID.Text.Trim());

					List<VendorPaymentData> GetResult = objbo.SearchChildvendorpaymentDetails(objdata);
					if (GetResult.Count > 0)
					{
						GridView SC = (GridView)e.Row.FindControl("GridChild1");
						SC.DataSource = GetResult;
						SC.DataBind();
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

		protected void paidall_CheckedChanged(object sender, EventArgs e)
		{
			divmsg2.Visible = false;
			lblmessage1.Text = "";
			Decimal totalPayableAmt = 0;
			CheckBox checkall = (CheckBox)sender;
			foreach (GridViewRow row in gvvendorrecordlist.Rows)
			{
				Label lbl_DueAmount = (Label)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_DueAmount");
				Label lbl_gvpaidamount = (Label)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_gvpaidamount");
				Label lbl_gvdueamount = (Label)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_gvdueamount");
				CheckBox chkpaid = (CheckBox)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("chkpaid");

				if (checkall.Checked)
				{
					chkpaid.Checked = true;
					totalPayableAmt = totalPayableAmt + Convert.ToDecimal(lbl_DueAmount.Text.Trim());
					lbl_gvpaidamount.Text = (Convert.ToDecimal(lbl_DueAmount.Text.Trim() == "" ? "0" : lbl_DueAmount.Text.Trim())).ToString("N2");
					lbl_gvdueamount.Text = "0";
				}
				else
				{
					chkpaid.Checked = false;
					totalPayableAmt = 0;
					lbl_gvpaidamount.Text = "";
					lbl_gvdueamount.Text = "";
				}
			}
			    txt_tap2payableamount.Text = totalPayableAmt.ToString("N2");
				txt_tap2Paidamount.Text = totalPayableAmt.ToString("N2");
				txt_tap2dueamount.Text = "0.0";
				if (checkall.Checked)
				{
					btnpaid.Attributes.Remove("disabled");
					txt_tap2remark.Focus();
				}
				else
				{
					btnpaid.Attributes["disabled"] = "disabled";
				}
			
		}
		protected void paid_CheckedChanged(object sender, EventArgs e)
		{
			divmsg2.Visible = false;
			lblmessage1.Text = "";
			Decimal totalPayableAmt = 0;
			CheckBox check= (CheckBox)sender;
			
			foreach (GridViewRow row in gvvendorrecordlist.Rows)
			{
				Label lbl_DueAmount = (Label)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_DueAmount");
				Label lbl_gvpaidamount = (Label)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_gvpaidamount");
				Label lbl_gvdueamount = (Label)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_gvdueamount");
				CheckBox chkpaid = (CheckBox)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("chkpaid");
				if (chkpaid.Checked)
				{
					lbl_gvpaidamount.Text = (Convert.ToDecimal(lbl_DueAmount.Text.Trim() == "" ? "0" : lbl_DueAmount.Text.Trim())).ToString("N2");
					lbl_gvdueamount.Text = "0";
					totalPayableAmt = totalPayableAmt + Convert.ToDecimal(lbl_DueAmount.Text.Trim());
				}
				else
				{
					lbl_gvpaidamount.Text = "";
					lbl_gvdueamount.Text = "";
				}
				
			}
			txt_tap2payableamount.Text = totalPayableAmt.ToString("N2");
			txt_tap2Paidamount.Text = totalPayableAmt.ToString("N2");
			txt_tap2dueamount.Text = "0.0";
			
			check.Focus();
		}

		protected void txt_tap2Paidamount_TextChanged(object sender, EventArgs e)
		{
			Decimal totaldueAmt = 0;
			if (Convert.ToDecimal(txt_tap2totalamount.Text.Trim() == "" ? "0" : txt_tap2totalamount.Text.Trim()) < Convert.ToDecimal(txt_tap2Paidamount.Text.Trim() == "" ? "0" : txt_tap2Paidamount.Text.Trim()))
			{
				Messagealert_.ShowMessage(lblmessage1, "Paid amount cannot be greater than payable amount.", 0);
				divmsg2.Visible = true;
				divmsg2.Attributes["class"] = "FailAlert";
				txt_tap2Paidamount.Focus();
				txt_tap2Paidamount.BorderColor = System.Drawing.Color.Red;
				
				btnpaid.Attributes["disabled"] = "disabled";
				return;
			}
			else
			{
				txt_tap2Paidamount.BorderColor = System.Drawing.Color.Transparent;
				totaldueAmt = Convert.ToDecimal(txt_tap2payableamount.Text.Trim() == "" ? "0" : txt_tap2payableamount.Text.Trim()) - Convert.ToDecimal(txt_tap2Paidamount.Text.Trim() == "" ? "0" : txt_tap2Paidamount.Text.Trim());
				txt_tap2dueamount.Text = totaldueAmt.ToString("N2");
				
				btnpaid.Attributes.Remove("disabled");				
				ddl_tap2paymentmode.Focus();
				lblmessage1.Text = "";
				divmsg2.Visible = false;
				Decimal dueamount = 0;
			    Decimal remainingamount = 0;
			    Decimal paidamount=0;
				Decimal gvdueamount = 0;

			    paidamount=Convert.ToDecimal(txt_tap2Paidamount.Text.Trim()=="" ? "0" :txt_tap2Paidamount.Text.Trim());
				foreach (GridViewRow row in gvvendorrecordlist.Rows)
				{
					
					Label lbl_DueAmount = (Label)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_DueAmount");
					Label lbl_gvpaidamount = (Label)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_gvpaidamount");
					Label lbl_gvdueamount = (Label)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_gvdueamount");
					CheckBox chkpaid = (CheckBox)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("chkpaid");
					remainingamount=paidamount - dueamount;
					dueamount =dueamount+ Convert.ToDecimal(lbl_DueAmount.Text == "" ? "0" : lbl_DueAmount.Text);
					gvdueamount = Convert.ToDecimal(lbl_DueAmount.Text == "" ? "0" : lbl_DueAmount.Text);
					if (remainingamount >= 0)
					{
					   txt_tap2payableamount.Text = dueamount.ToString("N2");
					   lbl_gvpaidamount.Text = remainingamount >= gvdueamount ? gvdueamount.ToString("N2") : remainingamount.ToString("N2");
					   lbl_gvdueamount.Text = remainingamount >= gvdueamount ? "0" : (gvdueamount - remainingamount).ToString("N2");
                       chkpaid.Checked=true;
					}
					else
					{
						lbl_gvpaidamount.Text = "";
						lbl_gvdueamount.Text = "";
					    chkpaid.Checked=false;
					}
				}
				txt_tap2dueamount.Text = (Convert.ToDecimal(txt_tap2payableamount.Text.Trim() == "" ? "0" : txt_tap2payableamount.Text.Trim()) - paidamount).ToString("N2");




			}
				
			
		}
		protected void ddlpaymentmode_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ddl_tap2paymentmode.SelectedIndex > 0)
			{
				if (ddl_tap2paymentmode.SelectedValue == "1")
				{
					txtbank.Text = "";
					txtbank.ReadOnly = true;
					txt_chequenumber.ReadOnly = true;
					txtinvoicenumber.ReadOnly = true;
				}
				else if (ddl_tap2paymentmode.SelectedValue == "2")
				{
					GetBankName(Convert.ToInt32(ddl_tap2paymentmode.SelectedValue == "" ? "0" : ddl_tap2paymentmode.SelectedValue));
					txtbank.ReadOnly = true;
					txt_chequenumber.ReadOnly = false;
					txtinvoicenumber.ReadOnly = false;
				}
				else if (ddl_tap2paymentmode.SelectedValue == "3")
				{
					GetBankName(Convert.ToInt32(ddl_tap2paymentmode.SelectedValue == "" ? "0" : ddl_tap2paymentmode.SelectedValue));
					txtbank.ReadOnly = true;
					txt_chequenumber.ReadOnly = false;
					txtinvoicenumber.ReadOnly = true;
				}
				else if (ddl_tap2paymentmode.SelectedValue == "4")
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
			VendorPaymentBO objbo = new VendorPaymentBO();
			BankDetail objbankdetail = new BankDetail();
			objbankdetail.PaymodeID = paymode;
			List<BankDetail> banklist = objbo.Getbanklist(objbankdetail);
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

		protected void btnpaid_Click(object sender, EventArgs e)
		{
			if (LogData.SaveEnable == 0)
			{
				Messagealert_.ShowMessage(lblmessage1, "SaveEnable", 0);
				divmsg2.Visible = true;
				divmsg2.Attributes["class"] = "FailAlert";
				return;
			}
			else
			{
				lblmessage1.Visible = false;
			}

			if (Convert.ToDecimal(txt_tap2Paidamount.Text == "" ? "0" : txt_tap2Paidamount.Text) == 0)
			{
				Messagealert_.ShowMessage(lblmessage1, "Paid Amount Cannot be 0", 0);
				divmsg2.Visible = true;
				divmsg2.Attributes["class"] = "FailAlert";
				txt_tap2Paidamount.Focus();
				txt_tap2Paidamount.Text = "0.0";
				txt_tap2Paidamount.BorderColor = System.Drawing.Color.Red;
				return;
			}
			else
			{
				txt_tap2Paidamount.BorderColor = System.Drawing.Color.Transparent;
				lblmessage1.Visible = false;
			}
			
			if (ddl_tap2paymentmode.SelectedIndex ==0)
			{
				Messagealert_.ShowMessage(lblmessage1, "Paymode", 0);
				divmsg2.Visible = true;
				divmsg2.Attributes["class"] = "FailAlert";
				ddl_tap2paymentmode.Focus();
				return;
			}
			else
			{
				lblmessage1.Visible = false;
			}
			if (ddl_tap2paymentmode.SelectedIndex > 1)
			{
				if (ddl_tap2paymentmode.SelectedValue == "2")
				{
					if (txtinvoicenumber.Text == "")
					{
						Messagealert_.ShowMessage(lblmessage1, "Invoicenumber", 0);
						divmsg2.Visible = true;
						divmsg2.Attributes["class"] = "FailAlert";
						txtinvoicenumber.Focus();
						return;
					}
					else
					{
						lblmessage1.Visible = false;
						divmsg2.Visible = false;
					}
				}
				if (ddl_tap2paymentmode.SelectedValue == "3")
				{
					if (txt_chequenumber.Text == "")
					{
						Messagealert_.ShowMessage(lblmessage1, "Chequenumber", 0);
						divmsg2.Visible = true;
						divmsg2.Attributes["class"] = "FailAlert";
						txt_chequenumber.Focus();
						return;
					}
					else
					{
						lblmessage.Visible = false;
						divmsg2.Visible = false;
					}
				}
				if (ddl_tap2paymentmode.SelectedValue == "4")
				{
					if (txtbank.Text == "")
					{
						Messagealert_.ShowMessage(lblmessage1, "BankName", 0);
						divmsg2.Visible = true;
						divmsg2.Attributes["class"] = "FailAlert";
						txtbank.Focus();
						return;
					}
					else
					{
						lblmessage1.Visible = false;
						divmsg2.Visible = false;
					}
					if (txt_chequenumber.Text == "")
					{
						Messagealert_.ShowMessage(lblmessage1, "Chequenumber", 0);
						divmsg2.Visible = true;
						divmsg2.Attributes["class"] = "FailAlert";
						txt_chequenumber.Focus();
						return;
					}
					else
					{
						lblmessage1.Visible = false;
						divmsg2.Visible = false;
					}
				}
			}
			else
			{
				lblmessage1.Visible = false;
				divmsg2.Visible = false;
			}
			
			

			VendorPaymentData objdata = new VendorPaymentData();
			VendorPaymentBO objbo = new VendorPaymentBO();
			List<VendorPaymentData> objlst = new List<VendorPaymentData>();
			IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
			try
			{
				int count = 0;
				// get all the record from the gridview
				foreach (GridViewRow row in gvvendorrecordlist.Rows)
				{
					
					Label lbl_InvoiceNo = (Label)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_InvoiceNo");
					Label lblVendorPayment_ID = (Label)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblVendorPayment_ID");
					Label lblReceiptNo = (Label)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblReceiptNo");
					Label lbl_SupplierID = (Label)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_SupplierID");
					Label lbl_ItemAddedOn = (Label)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemAddedOn");
					Label lbl_Amount = (Label)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_Amount");
					Label lbl_PaidAmount = (Label)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_PaidAmount");
					Label lbl_DueAmount = (Label)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_DueAmount");
					Label lbl_gvpaidamount = (Label)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_gvpaidamount");
					Label lbl_gvdueamount = (Label)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_gvdueamount");
					CheckBox chkpaid = (CheckBox)gvvendorrecordlist.Rows[row.RowIndex].Cells[0].FindControl("chkpaid");
					
					VendorPaymentData ObjDetails = new VendorPaymentData();

					if (chkpaid.Checked)
					{
						count++;
						ObjDetails.InvoiceNo = lbl_InvoiceNo.Text.Trim();
						ObjDetails.VendorPayment_ID = Convert.ToInt64(lblVendorPayment_ID.Text.Trim());
						ObjDetails.ReceiptNo = lblReceiptNo.Text.Trim();
						ObjDetails.SupplierID = Convert.ToInt32(lbl_SupplierID.Text == "" ? "0" : lbl_SupplierID.Text);
						DateTime Addeddate = DateTime.Parse(lbl_ItemAddedOn.Text.Trim(), provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
						ObjDetails.ItemAddedDate = Addeddate;
						ObjDetails.Amount = Convert.ToDecimal(lbl_DueAmount.Text == "" ? "0" : lbl_DueAmount.Text);
						ObjDetails.PaidAmount = Convert.ToDecimal(lbl_gvpaidamount.Text == "" ? "0" : lbl_gvpaidamount.Text);
						ObjDetails.DueAmount = Convert.ToDecimal(lbl_gvdueamount.Text == "" ? "0" : lbl_gvdueamount.Text);
						objlst.Add(ObjDetails);
				 }
						
				}
				if (count == 0)
				{
					lblmessage.Visible = true;
					Messagealert_.ShowMessage(lblmessage, "Please select atleast one Reciept No. for payment. ", 0);
					divmsg2.Visible = true;
					divmsg2.Attributes["class"] = "FailAlert";
					btnpaid.Attributes["disabled"] = "disabled";
					btnprints.Attributes["disabled"] = "disabled";
					return;
				}

				else
				{
					objdata.XMLData = XmlConvertor.VendorPaymentDatatoXML(objlst).ToString();
					objdata.SupplierID = Convert.ToInt32(txt_tap2supplierID.Text.Trim());
					objdata.PayableAmount = Convert.ToDecimal(txt_tap2payableamount.Text == "" ? "0" : txt_tap2payableamount.Text);
					objdata.TotalPaidAmount = Convert.ToDecimal(txt_tap2Paidamount.Text == "" ? "0" : txt_tap2Paidamount.Text);
					objdata.TotalDueAmount = Convert.ToDecimal(txt_tap2dueamount.Text == "" ? "0" : txt_tap2dueamount.Text);
					objdata.Paymode = Convert.ToInt32(ddl_tap2paymentmode.SelectedValue);
					objdata.BankName = txtbank.Text == "" ? null : txtbank.Text;
					objdata.ChequeUTRnumber = txt_chequenumber.Text == "" ? null : txt_chequenumber.Text;
					objdata.InvoiceNumber = txtinvoicenumber.Text == "" ? null : txtinvoicenumber.Text;
					objdata.Remark = txt_tap2remark.Text.Trim();
					objdata.EmployeeID = LogData.EmployeeID;
					objdata.HospitalID = LogData.HospitalID;
					objdata.FinancialYearID = LogData.FinancialYearID;
					objdata.ActionType = Enumaction.Insert;
					
					string result = objbo.UpdateVendorPaymentDetails(objdata);
					if (result!=null)
					{
						
						lblmessage1.Visible = true;
						Messagealert_.ShowMessage(lblmessage1, "save", 1);
						divmsg2.Visible = true;
						txt_PaymentNo.Text = result;
						txt_PaymentNo.BorderColor = System.Drawing.Color.Blue;
						divmsg2.Attributes["class"] = "SucessAlert";
						btnpaid.Attributes["disabled"] = "disabled";
						btnpaidprint.Attributes.Remove("disabled");
						
					}
					
				}
			}
			catch (Exception ex) //Exception in agent layer itself
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
				Messagealert_.ShowMessage(lblmessage, "system", 0);
				divmsg1.Attributes["class"] = "SuccessAlert";
				divmsg1.Visible = true;
			}
		}
		[System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
		public static List<string> GetVendorpaymentno(string prefixText, int count, string contextKey)
		{
			VendorPaymentData objdata = new VendorPaymentData();
			VendorPaymentBO objbo = new VendorPaymentBO();
			List<VendorPaymentData> getResult = new List<VendorPaymentData>();
			objdata.PaymentNo = prefixText.Trim();
			getResult = objbo.GetVendorpaymentno(objdata);
			List<String> list = new List<String>();

			for (int i = 0; i < getResult.Count; i++)
			{
				list.Add(getResult[i].PaymentNo.ToString());
			}
			return list;
		}
		protected void btntap3search_Click(object sender, EventArgs e)
		{
			if (LogData.SearchEnable == 0)
			{
				Messagealert_.ShowMessage(lblmessage3, "SearchEnable", 0);
				divmsg5.Visible = true;
				divmsg5.Attributes["class"] = "FailAlert";
				return;
			}
			else
			{
				lblmessage3.Visible = false;
			}
			tapbindgrid();
		}

		private void tapbindgrid()
		{
			IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
			VendorPaymentData objdata = new VendorPaymentData();
			VendorPaymentBO objbo = new VendorPaymentBO();
			DateTime From = txt_tap3datefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_tap3datefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			DateTime To = txt_tap3dateto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_tap3dateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			objdata.DateFrom = From;
			objdata.DateTo = To;
			objdata.PaymentNo=txt_tap3paymentno.Text.Trim()=="" ?"0":ddl_tap3paymentmode.Text.Trim();
			objdata.SupplierID = Convert.ToInt32(ddl_tap3supplier.Text.Trim() == "" ? "0" : ddl_tap3supplier.Text.Trim());
			objdata.Paymode = Convert.ToInt32(ddl_tap3paymentmode.SelectedValue == "" ? "0" : ddl_tap3paymentmode.SelectedValue);
			objdata.IsActive = ddl_tap3status.SelectedValue == "1" ? true : false;
			List<VendorPaymentData> lstdataresult = new List<VendorPaymentData>();
			lstdataresult = objbo.GetVendorPaymentList(objdata);
			if (lstdataresult.Count > 0)
			{
				gvpaymentlist.DataSource = lstdataresult;
				gvpaymentlist.DataBind();
				gvpaymentlist.Visible = true;
				Messagealert_.ShowMessage(lblresult3, "Total:" + lstdataresult.Count + " Record(s) found.", 1);
				divmsg3.Attributes["class"] = "SucessAlert";
				txt_tap3totalpaid.Text = Commonfunction.Getrounding(lstdataresult[0].TotalPaidAmount.ToString());
				txt_tap3totaldue.Text = Commonfunction.Getrounding(lstdataresult[0].TotalDueAmount.ToString());
			}
			else
			{
				gvpaymentlist.DataSource = null;
				gvpaymentlist.DataBind();
				gvpaymentlist.Visible = true;
				divmsg3.Visible = false;
				lblresult3.Visible = false;
				txt_tap3totalpaid.Text = "0.0";
				txt_tap3totaldue.Text = "0.0";
			}

		}


		protected void gvpaymentlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
					VendorPaymentData objdata = new VendorPaymentData();
					VendorPaymentBO objbo = new VendorPaymentBO();
					Label lbl_paymentNo = (Label)e.Row.FindControl("lbl_paymentNo");
					Label lbl_headerID = (Label)e.Row.FindControl("lbl_headerID");

					objdata.PaymentNo = lbl_paymentNo.Text.Trim();
					objdata.ID = Convert.ToInt64(lbl_headerID.Text.Trim());

					List<VendorPaymentData> GetResult = objbo.SearchChildvendorpaymentByPaymentNo(objdata);
                    if (GetResult.Count > 0)
                    {
                        GridView SC = (GridView)e.Row.FindControl("GridChild");
                        SC.DataSource = GetResult;
                        SC.DataBind();
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
		protected void gvpaymentlist_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				if (e.CommandName == "Deletes")
				{
                    if (LogData.RoleID == 1 || LogData.RoleID == 40)
                    {
                        if (LogData.DeleteEnable == 0)
                        {
                            Messagealert_.ShowMessage(lblmessage3, "DeleteEnable", 0);
                            divmsg5.Visible = true;
                            divmsg5.Attributes["class"] = "FailAlert";
                            return;
                        }
                        else
                        {
                            lblmessage3.Visible = false;
                        }
                        VendorPaymentData objdata = new VendorPaymentData();
                        VendorPaymentBO objstdBO = new VendorPaymentBO();
                        int i = Convert.ToInt16(e.CommandArgument.ToString());
                        GridViewRow gr = gvpaymentlist.Rows[i];
                        Label lbl_paymentNo = (Label)gr.Cells[0].FindControl("lbl_paymentNo");
                        Label lbl_tap3remark = (Label)gr.Cells[0].FindControl("lbl_tap3remark");
                        TextBox txt_tap3remarks = (TextBox)gr.Cells[0].FindControl("txt_tap3remarks");
                        lbl_tap3remark.Visible = false;
                        txt_tap3remarks.Visible = true;
                        if (txt_tap3remarks.Text == "")
                        {
                            Messagealert_.ShowMessage(lblmessage3, "Remarks", 0);
                            divmsg5.Attributes["class"] = "FailAlert";
                            txt_tap3remarks.Focus();
                            divmsg5.Visible = true;
                            return;
                        }
                        else
                        {
                            objdata.Remark = txt_tap3remarks.Text;
                            divmsg5.Visible = false;
                        }
                        objdata.PaymentNo = lbl_paymentNo.Text.Trim();
                        objdata.EmployeeID = LogData.EmployeeID;
                        int Result = objstdBO.DeleteVendorPaymentByPaymentNo(objdata);
                        if (Result == 1)
                        {
                            Messagealert_.ShowMessage(lblmessage3, "delete", 1);
                            divmsg5.Attributes["class"] = "SucessAlert";
                            divmsg5.Visible = true;
                            txt_tap3remarks.Visible = false;
                            lbl_tap3remark.Visible = true;
                            tapbindgrid();
                            return;
                        }
                        else
                        {
                            Messagealert_.ShowMessage(lblmessage3, "system", 0);
                            divmsg5.Attributes["class"] = "FailAlert";
                            divmsg5.Visible = true;
                            return;
                        }
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage3, "DeleteEnable", 0);
                        divmsg5.Attributes["class"] = "FailAlert";
                        divmsg5.Visible = true;
                        return;
                    }
				}
				if (e.CommandName == "DuplicateReciept")
				{
					if (LogData.PrintEnable == 0)
					{
						Messagealert_.ShowMessage(lblmessage3, "PrintEnable", 0);
						divmsg5.Visible = true;
						divmsg5.Attributes["class"] = "FailAlert";
						return;
					}
					else
					{
						lblmessage3.Visible = false;
					}
					int i = Convert.ToInt16(e.CommandArgument.ToString());
					GridViewRow gr = gvpaymentlist.Rows[i];
					Label lbl_paymentNo = (Label)gr.Cells[0].FindControl("lbl_paymentNo");
					string PaymentNo = lbl_paymentNo.Text.Trim();
					string url = "../MedPhr/Reports/ReportViewer.aspx?option=Vendorpayment&PaymentNo=" + PaymentNo;
					string fullURL = "window.open('" + url + "', '_blank');";
					ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
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
		protected void btntap3reset_Click(object sender, EventArgs e)
		{
			divmsg5.Visible = false;
			lblmessage3.Text = "";
			txt_tap3paymentno.Text = "";
			txt_tap3datefrom.Text = "";
			txt_tap3dateto.Text = "";
			ddl_tap3supplier.SelectedIndex = 0;
			ddl_tap3paymentmode.SelectedIndex = 0;
			ddl_tap3status.SelectedIndex = 0;
			divmsg3.Visible = false;
			lblresult3.Text = "";
			gvpaymentlist.DataSource = null;
			gvpaymentlist.DataBind();
			gvpaymentlist.Visible = true;
			txt_tap3totalpaid.Text = "";
			txt_tap3totaldue.Text = "";
		}

		protected void btnprints_Click(object sender, EventArgs e)
		{
			if (LogData.PrintEnable == 0)
			{
				Messagealert_.ShowMessage(lblmessage, "PrintEnable", 0);
				divmsg1.Visible = true;
				divmsg1.Attributes["class"] = "FailAlert";
				return;
			}
			else
			{
				lblmessage.Visible = false;
			}

			
			IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
			DateTime From = txt_datefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_datefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			DateTime To = txt_dateto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_dateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			String SupplierID = ddl_vendor.SelectedValue == "" ? "0" : ddl_vendor.SelectedValue;
			String Status = ddlstatus.SelectedValue;
			string url = "../MedPhr/Reports/ReportViewer.aspx?option=Phrvendordetails&SupplierID=" + SupplierID + "&From=" + From.ToString("yyyy-MM-dd") + "&To=" + To.ToString("yyyy-MM-dd") + "&Status=" + Status;
			string fullURL = "window.open('" + url + "', '_blank');";
			ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
		}

		protected void btnpaidprint_Click(object sender, EventArgs e)
		{
			if (LogData.PrintEnable == 0)
			{
				Messagealert_.ShowMessage(lblmessage1, "PrintEnable", 0);
				divmsg3.Visible = true;
				divmsg3.Attributes["class"] = "FailAlert";
				return;
			}
			else
			{
				lblmessage1.Visible = false;
			}
			string url = "../MedPhr/Reports/ReportViewer.aspx?option=Vendorpayment&PaymentNo=" + txt_PaymentNo.Text.Trim() ;
			string fullURL = "window.open('" + url + "', '_blank');";
			ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
		}

		protected void btntap3print_Click(object sender, EventArgs e)
		{
			if (LogData.PrintEnable == 0)
			{
				Messagealert_.ShowMessage(lblmessage3, "PrintEnable", 0);
				divmsg3.Visible = true;
				divmsg3.Attributes["class"] = "FailAlert";
				return;
			}
			else
			{
				lblmessage3.Visible = false;
			}
			IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
			DateTime From = txt_tap3datefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_tap3datefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			DateTime To = txt_tap3dateto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_tap3dateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
			String SupplierID = ddl_tap3supplier.SelectedValue == "" ? "0" : ddl_tap3supplier.SelectedValue;
			String PaymentMode = ddl_tap3paymentmode.SelectedValue == "" ? "0" : ddl_tap3paymentmode.SelectedValue;
			String PaymentNo=txt_tap3paymentno.Text.Trim()==""?"0":txt_tap3paymentno.Text.Trim();
			string url = "../MedPhr/Reports/ReportViewer.aspx?option=Vendorpaymentlist&PaymentNo=" + PaymentNo + "&From=" + From.ToString("yyyy-MM-dd") + "&To=" + To.ToString("yyyy-MM-dd") + "&PaymentMode=" + PaymentMode + "&SupplierID=" + SupplierID;
			
			string fullURL = "window.open('" + url + "', '_blank');";
			ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
		
		}

		

		

		
	}
}