using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedStore;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.MedStore;
using Mediqura.Utility;
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
    public partial class StockReturn : BasePage
    {
        static int rowcount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDdl();
                btnSave.Attributes["disabled"] = "disabled";
            }
        }
        private void BindDdl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_Supplier, mstlookup.GetLookupsList(LookupName.Supplier));
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            BindGridTab1(1);
            BindTotalReturnQuantity();
        }
        protected void BindGridTab1(int page)
        {
            try
            {
                List<StockReturnData> ObjStockReturnList = GetStockReturnListItem(page);
                if (ObjStockReturnList.Count > 0)
                {
                    GvStockReturn.VirtualItemCount = ObjStockReturnList[0].MaximumRows;//total item is required for custom paging
                    GvStockReturn.PageIndex = page - 1;
                    GvStockReturn.DataSource = ObjStockReturnList;
                    GvStockReturn.DataBind();
                    GvStockReturn.Visible = true;
                    Messagealert_.ShowMessage(lblResult, "Total:" + ObjStockReturnList[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    btnSave.Attributes.Remove("disabled");
                }
                else
                {
                    GvStockReturn.DataSource = null;
                    GvStockReturn.DataBind();
                    GvStockReturn.Visible = true;
                    lblResult.Visible = false;
                    btnSave.Attributes["disabled"] = "disabled";
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
            }
        }
        public List<StockReturnData> GetStockReturnListItem(int curIndex)
        {
            StockReturnData ObjStockReturnData = new StockReturnData();
            StockReturnBO ObjStockReturnBO = new StockReturnBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            ObjStockReturnData.SupplierID = Convert.ToInt32(ddl_Supplier.SelectedValue == "" ? "0" : ddl_Supplier.SelectedValue);
            ObjStockReturnData.FilterTypeID = Convert.ToInt32(ddl_FilterType.SelectedValue == "" ? "0" : ddl_FilterType.SelectedValue);
            DateTime From = txt_ExpiredFrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_ExpiredFrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txt_ExpiredTo.Text.Trim() == "" ? GlobalConstant.MaxSQLDateTime : DateTime.Parse(txt_ExpiredTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            ObjStockReturnData.ExpiredFrom = From;
            ObjStockReturnData.ExpiredTo = To;

            return ObjStockReturnBO.GetStockReturnListItem(ObjStockReturnData);
        }
        protected void GvStockReturn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label CPPerUnit = (Label)e.Row.FindControl("lblCPPerUnit");
                TextBox ReturnPricePerItem = (TextBox)e.Row.FindControl("lblReturnPricePerItem");

                CPPerUnit.Text = Commonfunction.Getrounding(CPPerUnit.Text);
                ReturnPricePerItem.Text = Commonfunction.Getrounding(ReturnPricePerItem.Text);

            }
        }
        protected void txtReturnQuantity_TextChanged(object sender, EventArgs e)
        {
            TextBox ReturnQuantity = (TextBox)sender;
            GridViewRow GvRow = (GridViewRow)ReturnQuantity.NamingContainer;
            Label AvailableQuantity = (Label)GvRow.Cells[0].FindControl("lblAvailableQuantity");
            CheckBox ChkBoxIsReturn = (CheckBox)GvRow.Cells[0].FindControl("CheckBoxIsReturn");

            if (ReturnQuantity.Text == "0" || ReturnQuantity.Text == "")
            {
                ReturnQuantity.Text = "0";
                ChkBoxIsReturn.Checked = false;
            }
            if (Convert.ToInt32(AvailableQuantity.Text) >= Convert.ToInt32(ReturnQuantity.Text) && Convert.ToInt32(ReturnQuantity.Text) != 0 && ReturnQuantity.Text != "")
            {
                ChkBoxIsReturn.Checked = true;
                btnSave.Attributes.Remove("disabled");
            }
            if (Convert.ToInt32(AvailableQuantity.Text) < Convert.ToInt32(ReturnQuantity.Text))
            {
                ReturnQuantity.Focus();
                Messagealert_.ShowMessage(lblmessage, "Return Quantity shouldn't be greater than Available Quantity.", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                return;
            }
            else
            {
                lblmessage.Text = "";
                divmsg1.Visible = false;
                rowcount = GvStockReturn.Rows.Count;
                int index = GvRow.RowIndex;
                if (rowcount == index + 1)
                {
                    BindTotalReturnQuantity();
                    btnSave.Focus();
                }
                else
                {
                    GridViewRow NextRow = GvStockReturn.Rows[index + 1];
                    TextBox ReturnQnty = (TextBox)NextRow.Cells[0].FindControl("txtReturnQuantity");
                    ReturnQnty.Focus();
                    BindTotalReturnQuantity();
                }
            }
        }
		private void cpaftertax(decimal taxable,decimal sgst,decimal cgst,decimal igst, int index)
		{
			decimal cpaftertax = 0;
			GridViewRow GvRow = GvStockReturn.Rows[index];
			Label lbl_rtncpAfterTax = (Label)GvRow.Cells[0].FindControl("lbl_rtncpAfterTax");
			cpaftertax = taxable + (sgst/100)+ (cgst/100)+( igst/100);
			lbl_rtncpAfterTax.Text = cpaftertax.ToString();
			BindTotalReturnQuantity();
		}
		protected void Taxable_TextChanged(object sender, EventArgs e)
		{
			decimal taxable = 0, sgst = 0, cgst = 0, igst = 0;
			int index=0;
			TextBox container = (TextBox)sender;
			GridViewRow GvRow = (GridViewRow)container.NamingContainer;
			index=GvRow.RowIndex;
			TextBox lblReturnPricePerItem = (TextBox)GvRow.Cells[0].FindControl("lblReturnPricePerItem");
			TextBox lbl_rtnsgst = (TextBox)GvRow.Cells[0].FindControl("lbl_rtnsgst");
			TextBox lbl_rtncgst = (TextBox)GvRow.Cells[0].FindControl("lbl_rtncgst");
			TextBox lbl_rtnigst = (TextBox)GvRow.Cells[0].FindControl("lbl_rtnigst");
			taxable = Convert.ToDecimal(lblReturnPricePerItem.Text.Trim());
			sgst = Convert.ToDecimal(lbl_rtnsgst.Text.Trim());
			cgst = Convert.ToDecimal(lbl_rtncgst.Text.Trim());
			igst = Convert.ToDecimal(lbl_rtnigst.Text.Trim());
			cpaftertax(taxable, sgst, cgst, igst,index);
			
		}
	

        protected void CheckBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxAll = (CheckBox)sender;
            GridViewRow GvRow = (GridViewRow)ChkBoxAll.NamingContainer;
            Label AvailableQuantity = (Label)GvRow.Cells[0].FindControl("lblAvailableQuantity");
            TextBox ReturnQuantity = (TextBox)GvRow.Cells[0].FindControl("txtReturnQuantity");
			TextBox ReturnPricePerItem = (TextBox)GvRow.Cells[0].FindControl("lblReturnPricePerItem");
            if (ChkBoxAll.Checked)
            {
                btnSave.Attributes.Remove("disabled");
            }
            else
            {
                btnSave.Attributes["disabled"] = "disabled";
            }
        }
        protected void CheckBoxIsReturn_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxIsReturn = (CheckBox)sender;
            GridViewRow GvRow = (GridViewRow)ChkBoxIsReturn.NamingContainer;
            Label AvailableQuantity = (Label)GvRow.Cells[0].FindControl("lblAvailableQuantity");
            TextBox ReturnQuantity = (TextBox)GvRow.Cells[0].FindControl("txtReturnQuantity");
			TextBox ReturnPricePerItem = (TextBox)GvRow.Cells[0].FindControl("lblReturnPricePerItem");
            if (ChkBoxIsReturn.Checked)
            {
                if (ReturnQuantity.Text == "0" || ReturnQuantity.Text == "")
                {
                    ReturnQuantity.Focus();
                    Messagealert_.ShowMessage(lblmessage, "Please Enter Return Quantity.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    btnSave.Attributes["disabled"] = "disabled";
                    return;
                }
                else
                {
                    btnSave.Attributes.Remove("disabled");
                }
            }
            else
            {
                btnSave.Attributes["disabled"] = "disabled";
            }
            if (!ChkBoxIsReturn.Checked)
            {
                ReturnQuantity.Text = "0";
            }
            BindTotalReturnQuantity();
        }
        protected void BindTotalReturnQuantity()
        {
			decimal cpaftertax=0, sgst = 0, cgst = 0, igst = 0;
			int total = 0;
            decimal totalPrice = 0;
            foreach (GridViewRow row in GvStockReturn.Rows)
            {
                TextBox ReturnQuantity = (TextBox)GvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("txtReturnQuantity");
                Label CPPerQty = (Label)GvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lblCPPerUnit");
				TextBox ReturnPricePerItem = (TextBox)GvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lblReturnPricePerItem");
				TextBox lbl_rtnsgst = (TextBox)GvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtnsgst");
				TextBox lbl_rtncgst = (TextBox)GvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtncgst");
				TextBox lbl_rtnigst = (TextBox)GvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtnigst");
				Label lbl_rtncpAfterTax = (Label)GvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtncpAfterTax");
				Decimal ReturnPPI = Convert.ToInt32(ReturnQuantity.Text.Trim()) * Convert.ToDecimal(CPPerQty.Text.Trim());
				sgst = Convert.ToDecimal(lbl_rtnsgst.Text.Trim());
				cgst = Convert.ToDecimal(lbl_rtncgst.Text.Trim());
				igst = Convert.ToDecimal(lbl_rtnigst.Text.Trim());
				if (ReturnQuantity != null)
                {
                   
                    ReturnPricePerItem.Text = ReturnPPI.ToString();
					cpaftertax = ReturnPPI + (sgst / 100) + (cgst / 100) + (igst / 100);
					lbl_rtncpAfterTax.Text = cpaftertax.ToString();
                    totalPrice += ReturnPPI;
                    total += Convert.ToInt32(ReturnQuantity.Text);
                    GvStockReturn.FooterRow.Cells[6].Text = "Total: " + total.ToString();
                    lblTotalReturnQuantity.Text = total.ToString();
                    lblTotalReturnPrice.Text = totalPrice.ToString();
                    lblTotalReturnPrice.Text = Commonfunction.Getrounding(lblTotalReturnPrice.Text);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_Supplier.SelectedValue == "0")
                {
                    ddl_Supplier.Focus();
                    Messagealert_.ShowMessage(lblmessage, "Please Select Supplier Name.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    return;
                }
                if (ddl_FilterType.SelectedValue == "0")
                {
                    ddl_FilterType.Focus();
                    Messagealert_.ShowMessage(lblmessage, "Please Select Filter Type.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    return;
                }
                if (lblTotalReturnQuantity.Text == "0" || lblTotalReturnQuantity.Text == "")
                {                     
                    lblTotalReturnQuantity.BackColor=System.Drawing.Color.FromName("#FBE6DD");
                    lblTotalReturnQuantity.ForeColor = System.Drawing.Color.Red;
                    Messagealert_.ShowMessage(lblmessage, "Total Return Quantity sholudn't be blank or 0.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    return;
                }
                if (lblTotalReturnPrice.Text == "0" || lblTotalReturnPrice.Text == "")
                {
                    lblTotalReturnPrice.BackColor = System.Drawing.Color.FromName("#FBE6DD");
                    lblTotalReturnPrice.ForeColor = System.Drawing.Color.Red;
                    Messagealert_.ShowMessage(lblmessage, "Total Return Price sholudn't be blank or 0.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    return;
                }
                if (GvStockReturn.Rows.Count != 0)
                {
                    List<StockReturnData> StockReturnList = new List<StockReturnData>();
                    StockReturnData ObjStockReturnData = new StockReturnData();
                    StockReturnBO ObjStockReturnBO = new StockReturnBO();

                    ObjStockReturnData.SupplierID = Convert.ToInt32(ddl_Supplier.SelectedValue);
                    ObjStockReturnData.SupplierName = ddl_Supplier.SelectedItem.Text;
                    ObjStockReturnData.FilterTypeID = Convert.ToInt32(ddl_FilterType.SelectedValue);
                    ObjStockReturnData.FilterTypeName = ddl_FilterType.SelectedItem.Text;
                    foreach (GridViewRow row in GvStockReturn.Rows)
                    {
                        StockReturnData ObjStockReturnList = new StockReturnData();

						Label SerialID = (Label)GvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lblrtnserialID");
						Label lblstockid = (Label)GvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lblstockid");
						Label lbl_rtnbatchno = (Label)GvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtnbatchno");
						Label lbl_rtnreceiptNo = (Label)GvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtnreceiptNo");
						Label lblItemID = (Label)GvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lblItemID");
						Label lbl_rtnCompanyID = (Label)GvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtnCompanyID");
						Label lbl_rtnSupplierID = (Label)GvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtnSupplierID");
						TextBox txtReturnQuantity = (TextBox)GvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("txtReturnQuantity");
						TextBox lblReturnPricePerItem = (TextBox)GvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lblReturnPricePerItem");
						TextBox lbl_rtnsgst = (TextBox)GvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtnsgst");
						TextBox lbl_rtncgst = (TextBox)GvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtncgst");
						TextBox lbl_rtnigst = (TextBox)GvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtnigst");
						TextBox lbl_rtncpAfterTax = (TextBox)GvStockReturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtncpAfterTax");

						ObjStockReturnList.StockID = Convert.ToInt64(lblstockid.Text == "" ? "0" : lblstockid.Text);
						ObjStockReturnList.SerialID = Convert.ToInt32(SerialID.Text == "" ? "0" : SerialID.Text);
						ObjStockReturnList.ReceiptNo = lbl_rtnreceiptNo.Text == "" ? "0" : lbl_rtnreceiptNo.Text;
						ObjStockReturnList.BatchNo = lbl_rtnbatchno.Text == "" ? "0" : lbl_rtnbatchno.Text;
						ObjStockReturnList.ItemID = Convert.ToInt32(lblItemID.Text == "" ? "" : lblItemID.Text);
						ObjStockReturnList.CompanyID = Convert.ToInt32(lbl_rtnCompanyID.Text == "" ? "" : lbl_rtnCompanyID.Text);
						ObjStockReturnList.SupplierID = Convert.ToInt32(lbl_rtnSupplierID.Text == "" ? "" : lbl_rtnSupplierID.Text);
						ObjStockReturnList.ReturnQty = Convert.ToInt32(txtReturnQuantity.Text == "" ? "" : txtReturnQuantity.Text);
						ObjStockReturnList.TaxableAmount = Convert.ToDecimal(lblReturnPricePerItem.Text == "" ? "0" : lblReturnPricePerItem.Text);
						ObjStockReturnList.SGST = Convert.ToDouble(lbl_rtnsgst.Text == "" ? "0" : lbl_rtnsgst.Text);
						ObjStockReturnList.CGST = Convert.ToDouble(lbl_rtncgst.Text == "" ? "0" : lbl_rtncgst.Text);
						ObjStockReturnList.IGST = Convert.ToDouble(lbl_rtnigst.Text == "" ? "0" : lbl_rtnigst.Text);
						ObjStockReturnList.CPafterTax = Convert.ToDecimal(lbl_rtncpAfterTax.Text == "" ? "0" : lbl_rtncpAfterTax.Text);

                        StockReturnList.Add(ObjStockReturnList);
                    }
                    ObjStockReturnData.XMLData = XmlConvertor.StockReturnListToXML(StockReturnList).ToString();
                    ObjStockReturnData.TotalReturnQty = Convert.ToInt32(lblTotalReturnQuantity.Text.Trim());
                    ObjStockReturnData.TotalReturnPrice = Convert.ToDecimal(lblTotalReturnPrice.Text.Trim());
					ObjStockReturnData.Payableamt = 0;
                    ObjStockReturnData.EmployeeID = LogData.EmployeeID;
                    ObjStockReturnData.AddedBy = LogData.UserName;                       
                    ObjStockReturnData.FinancialYearID = LogData.FinancialYearID;
                    ObjStockReturnData.HospitalID = LogData.HospitalID;
                    ObjStockReturnData.IPaddress = LogData.IPaddress;
                    ObjStockReturnData.ActionType = Enumaction.Insert;

                    string Result = ObjStockReturnBO.SaveStockReturnList(ObjStockReturnData);
                    if (Result != "5")
                    {
                        lbl_ReturnNo.Text = Result;
                        btnSave.Attributes["disabled"]="disabled";
                        Messagealert_.ShowMessage(lblmessage, "Refund Successfully", 1);
                        divmsg1.Attributes["class"] = "SucessAlert";
                        divmsg1.Visible = true;
                    }
                    else
                    {
                        lbl_ReturnNo.Text = "";
                        Messagealert_.ShowMessage(lblmessage, "Something went wrong refund couldn't be process", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                return;
            }     
        }
       
        protected void btnPrint_Click(object sender, EventArgs e)
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

            String ReturnNo = lbl_ReturnNo.Text.Trim() == "" ? "0" : lbl_ReturnNo.Text.Trim();
            string url = "../MedPhr/Reports/ReportViewer.aspx?option=VendorItemReturnList&ReturnNo=" + ReturnNo;
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }

        //-----------------------------------------END TAB 1----------------------------------------

    }
}