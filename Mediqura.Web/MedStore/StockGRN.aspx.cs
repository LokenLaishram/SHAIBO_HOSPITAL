using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedUtilityData;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Drawing;
using Mediqura.CommonData.MedStore;
using Mediqura.BOL.MedStore;
using Mediqura.Utility;
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;

namespace Mediqura.Web.MedStore
{
    public partial class StockGRN : BasePage
    {
        int total, total1, total2, total9, total10, total11;
        decimal total3, total4, total5, total6, total7, total8, total12, total13;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
                txt_cp.Attributes.Add("readonly", "readonly");
                txt_totalcp.Attributes.Add("readonly", "readonly");
                txt_totalmrp.Attributes.Add("readonly", "readonly");
                txt_totalreceivedqty.Attributes.Add("readonly", "readonly");
                txt_CpberforeTax.Attributes.Add("readonly", "readonly");
                txt_RateafterTax.Attributes.Add("readonly", "readonly");
                txt_mrp.Attributes.Add("readonly", "readonly");
                txt_due.Attributes.Add("readonly", "readonly");
                txt_nettotalCp.Attributes.Add("readonly", "readonly");
				Session["StockreturnItemList"] = null;
				
                bindddl();
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_receivedby, mstlookup.GetLookupsList(LookupName.StockRecievedBy));
            Commonfunction.PopulateDdl(ddl_stockrecievedby, mstlookup.GetLookupsList(LookupName.StockRecievedBy));
            Commonfunction.PopulateDdl(ddlgroup, mstlookup.GetLookupsList(LookupName.Groups));
            Commonfunction.PopulateDdl(ddl_mfgcompnay, mstlookup.GetLookupsList(LookupName.Mfgcompany));
            Commonfunction.PopulateDdl(ddl_Supplier, mstlookup.GetLookupsList(LookupName.Supplier));
            ddl_receivedby.SelectedValue = LogData.EmployeeID.ToString();
            btnsave.Attributes["disabled"] = "disabled";
            // ddl_mfgcompnay.Attributes["disabled"] = "disabled";
            ddl_receivedby.Attributes["disabled"] = "disabled";
            hdn_total_free_item_Amount.Value = null;
            Session["StockItemList"] = null;
            CalendarExtender1.SelectedDate = DateTime.Now;
            ddl_purchagetype.Focus();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetCompanyName(string prefixText, int count, string contextKey)
        {
            StockGRNData Objpaic = new StockGRNData();
            StockGRNBO objInfoBO = new StockGRNBO();
            List<StockGRNData> getResult = new List<StockGRNData>();
            Objpaic.CompanyName = prefixText;
            getResult = objInfoBO.GetCompanyName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].CompanyName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetSupplierName(string prefixText, int count, string contextKey)
        {
            StockGRNData Objpaic = new StockGRNData();
            StockGRNBO objInfoBO = new StockGRNBO();
            List<StockGRNData> getResult = new List<StockGRNData>();
            Objpaic.SupplierName = prefixText;
            getResult = objInfoBO.GetSupplierName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].SupplierName.ToString());                 
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemName(string prefixText, int count, string contextKey)
        {
            StockGRNData Objpaic = new StockGRNData();
            StockGRNBO objInfoBO = new StockGRNBO();
            List<StockGRNData> getResult = new List<StockGRNData>();
            Objpaic.ItemName = prefixText;
            Objpaic.GroupID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetItemName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemNames(string prefixText, int count, string contextKey)
        {
            StockGRNData Objpaic = new StockGRNData();
            StockGRNBO objInfoBO = new StockGRNBO();
            List<StockGRNData> getResult = new List<StockGRNData>();
            Objpaic.ItemName = prefixText;
            getResult = objInfoBO.GetItemNames(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName.ToString());
            }
            return list;
        }
        //ITem Return ///
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetReturnItemName(string prefixText, int count, string contextKey)
        {
            StockGRNData ObjRItem = new StockGRNData();
            StockGRNBO objmedBO = new StockGRNBO();
            List<StockGRNData> getResult = new List<StockGRNData>();
            ObjRItem.SupplierID = Convert.ToInt32(contextKey);
            ObjRItem.ItemName = prefixText;
            getResult = objmedBO.GetReturnItemName(ObjRItem);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName.ToString());
            }
            return list;
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetEmployeeNames(string prefixText, int count, string contextKey)
        {
            EmployeeData Objpaic = new EmployeeData();
            EmployeeBO objInfoBO = new EmployeeBO();
            List<EmployeeData> getResult = new List<EmployeeData>();
            Objpaic.EmpName = prefixText;
            getResult = objInfoBO.GetEmployeeName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmpName.ToString());
            }
            return list;
        }
        protected void txt_itemname_TextChanged(object sender, EventArgs e)
        {

            txt_receiveddate.Text = "";             
            txt_expdate.Text = "";
            txt_qty.Text = "";
            txt_totalreceivedqty.Text = "";
            txt_cp.Text = "";
            txt_totalmrp.Text = "";
            txt_mrp.Text = "";
            txt_freeqty.Text = "";
            txt_totalcp.Text = "";
            txt_no0funit.Text = "";
           
            ItemMasterData objItemMasterData = new ItemMasterData();
            ItemMasterBO objItemMasterBO = new ItemMasterBO();
                        
            string ID;
            var source = txt_itemname.Text.ToString();
            bool isUHIDnumeric = source.Substring(source.LastIndexOf(':') + 1).All(char.IsDigit);
            if (source.Contains(":") && isUHIDnumeric == true)
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                objItemMasterData.ID = Convert.ToInt32(ID);
                objItemMasterData.ActionType = Enumaction.Select;
                List<ItemMasterData> GetResult = objItemMasterBO.GetItemMasterDetailsByID(objItemMasterData);
                if (GetResult.Count > 0)
                {
                    MasterLookupBO mstlookup = new MasterLookupBO();
                    Commonfunction.PopulateDdl(ddl_mfgcompnay, mstlookup.GetLookupsList(LookupName.Mfgcompany));
                    ddl_mfgcompnay.SelectedValue = GetResult[0].MfgCompanyID.ToString();
                }
				txtHSNCode.Focus();
            }
            else
            {
                txt_itemname.Text = "";
				txt_itemname.Focus();
            }
            
        }
        protected void ddl_Supplier_SelectedIndexChanged(object sender, EventArgs e)
        {
			if (Convert.ToInt32(ddl_Supplier.SelectedValue)>0)
			{
				AutoCompleteExtender1.ContextKey = ddl_Supplier.SelectedValue;
			}
			
			
        }
		protected void txtReturnItem_TextChanged(object sender, EventArgs e)
        {
			Int64 StockID = 0;
			var source = txtReturnItem.Text.ToString();
			if (source.Contains(":"))
			{
				StockID = Convert.ToInt64(source.Substring(source.LastIndexOf(':') + 1).Trim());

				
			}
			else
			{
				txtReturnItem.Text = "";
				return;
			}
			List<StockGRNData> ListStock = new List<StockGRNData>();
			StockGRNData objStock = new StockGRNData();
			StockGRNBO objBO = new StockGRNBO();
			objStock.ID = StockID;
			ListStock = objBO.GetStockItemDetailsByStockID(objStock);
			Rate.Text = ListStock[0].CPperunit.ToString();
			txt_returnsgst.Text = ListStock[0].SGST.ToString();
			txt_returncgst.Text = ListStock[0].CGST.ToString();
			txt_returnigst.Text = ListStock[0].IGST.ToString();
			TotalRecievedQty.Text = ListStock[0].TotalRecievedQty.ToString();
			ID.Text = ListStock[0].ID.ToString();
			BatchNo.Text = ListStock[0].BatchNo.ToString();
			ReceiptNo.Text = ListStock[0].ReceiptNo.ToString();
			ItemID.Text = ListStock[0].ItemID.ToString();
			CompanyID.Text = ListStock[0].CompanyID.ToString();
			SupplierID.Text = ListStock[0].SupplierID.ToString();
			ItemName.Text = ListStock[0].ItemName.ToString();
			txtreturnQty.Focus();
			ModalPopupExtender1.Show();
        }
		protected void btnretunadd_Click(object sender, EventArgs e)
		{
			
			IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
			List<StockGRNData> StockreturnItemList = Session["StockreturnItemList"] == null ? new List<StockGRNData>() : (List<StockGRNData>)Session["StockreturnItemList"];
			StockGRNData objStock = new StockGRNData();
			objStock.ReceiptNo = ReceiptNo.Text.Trim();
			objStock.BatchNo = BatchNo.Text.Trim();
			objStock.ItemName = ItemName.Text.Trim();
			objStock.StockID = Convert.ToInt64(ID.Text.Trim());
			objStock.ItemID = Convert.ToInt32(ItemID.Text.Trim());
			objStock.CompanyID = Convert.ToInt32(CompanyID.Text.Trim());
			objStock.TotalQuantity = Convert.ToInt32(txtreturnQty.Text == "" ? "0" : txtreturnQty.Text);
			objStock.TaxableAmount = Convert.ToDecimal(txt_returnamt.Text == "" ? "0" : txt_returnamt.Text);
			objStock.SGST = Convert.ToDouble(txt_returnsgst.Text == "" ? "0" : txt_returnsgst.Text);
			objStock.CGST = Convert.ToDouble(txt_returncgst.Text == "" ? "0" : txt_returncgst.Text);
			objStock.IGST = Convert.ToDouble(txt_returnigst.Text == "" ? "0" : txt_returnigst.Text);
			objStock.CPafterTax = Convert.ToDecimal(txt_returnNetamnt.Text == "" ? "0" : txt_returnNetamnt.Text);
			objStock.SupplierID = Convert.ToInt32(SupplierID.Text.Trim());
			objStock.ReceivedBy = LogData.EmployeeID;
			StockreturnItemList.Add(objStock);
			if (StockreturnItemList.Count > 0)
			{
				gvstockreturn.DataSource = StockreturnItemList;
				gvstockreturn.DataBind();
				gvstockreturn.Visible = true;
				Session["StockreturnItemList"] = StockreturnItemList;
				clearrtn();
				txtReturnItem.Focus();
				ModalPopupExtender1.Show();
			}
			else
			{
				gvstockreturn.DataSource = null;
				gvstockreturn.DataBind();
				gvstockreturn.Visible = true;
				ModalPopupExtender1.Show();
			}
		}
		protected void gvstockreturn_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			int totalrtnqty = 0;
			decimal returnamt=0;
			decimal cpaftertax = 0;
			decimal discount = 0;
			foreach (GridViewRow row in gvstockreturn.Rows)
			{
				Label lbl_rtncpAfterTax = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtncpAfterTax");
				Label lbl_rtntotalquantity = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtntotalquantity");

				returnamt = returnamt + Convert.ToDecimal(lbl_rtncpAfterTax.Text.Trim());
				totalrtnqty = totalrtnqty + Convert.ToInt32(lbl_rtntotalquantity.Text.Trim());
			}
			foreach (GridViewRow row in gvstocklist.Rows)
			{
				Label lbl_cpAfterTax = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_cpAfterTax");

				cpaftertax = cpaftertax + Convert.ToDecimal(lbl_cpAfterTax.Text.Trim());

			}
			txttotalreturnamt.Text=returnamt.ToString();
			txtgvreturnamt.Text = returnamt.ToString();
			txtgvreturnqty.Text = totalrtnqty.ToString();
			discount = Convert.ToDecimal(Commonfunction.Getrounding(txt_discount.Text == "" ? "0" : txt_discount.Text.Trim()));
			txt_nettotalCp.Text = Commonfunction.Getrounding((cpaftertax  - discount).ToString());
			txt_payableamt.Text = Commonfunction.Getrounding((cpaftertax - returnamt - discount).ToString());
		}
		protected void gvstockreturn_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			int totalrtnqty = 0;
			decimal returnamt = 0;
			decimal cpaftertax = 0;
			decimal discount = 0;
			try
			{
				if (e.CommandName == "Deletes")
				{
					int i = Convert.ToInt16(e.CommandArgument.ToString());
					GridViewRow gr = gvstockreturn.Rows[i];
					List<StockGRNData> StockreturnItemList = Session["StockreturnItemList"] == null ? new List<StockGRNData>() : (List<StockGRNData>)Session["StockreturnItemList"];
					StockreturnItemList.RemoveAt(i);
					txt_due.Text = "";
					txt_paidamount.Text = "";
					txt_discount.Text = "";
					Session["StockreturnItemList"] = StockreturnItemList;
					gvstockreturn.DataSource = StockreturnItemList;
					gvstockreturn.DataBind();

					foreach (GridViewRow row in gvstockreturn.Rows)
					{
						Label lbl_rtncpAfterTax = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtncpAfterTax");
						Label lbl_rtntotalquantity = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtntotalquantity");
						returnamt = returnamt + Convert.ToDecimal(lbl_rtncpAfterTax.Text.Trim());
						totalrtnqty = totalrtnqty + Convert.ToInt32(lbl_rtntotalquantity.Text.Trim());
					}
					foreach (GridViewRow row in gvstocklist.Rows)
					{
						Label lbl_cpAfterTax = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_cpAfterTax");

						cpaftertax = cpaftertax + Convert.ToDecimal(lbl_cpAfterTax.Text.Trim());

					}
					txttotalreturnamt.Text = returnamt.ToString();
					txtgvreturnamt.Text = returnamt.ToString();
					txtgvreturnqty.Text = totalrtnqty.ToString();
					discount = Convert.ToDecimal(Commonfunction.Getrounding(txt_discount.Text == "" ? "0" : txt_discount.Text.Trim()));
					txt_nettotalCp.Text = Commonfunction.Getrounding((cpaftertax  - discount).ToString());
					txt_payableamt.Text = Commonfunction.Getrounding((cpaftertax - returnamt - discount).ToString());
					ModalPopupExtender1.Show();
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

		private void clearrtn()
		{
			txtreturnQty.Text = "";
			txt_returnamt.Text = "";
			txt_returnNetamnt.Text = "";
			Rate.Text ="";
			txt_returnsgst.Text = "";
			txt_returncgst.Text = "";
			txt_returnigst.Text = "";
			TotalRecievedQty.Text = "";
			ID.Text = "";
			BatchNo.Text = "";
			ReceiptNo.Text = "";
			ItemID.Text = "";
			CompanyID.Text = "";
			SupplierID.Text = "";
			ItemName.Text = "";
			ModalPopupExtender1.Show();
		}
        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (ddl_purchagetype.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Purchagetype", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_purchagetype.Focus();
                return;
            }
            else
            {
                if (txt_receivedno.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "RCNO", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_receivedno.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_purchagetype.SelectedIndex == 1)
                {
                    if (txt_PONo.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "PO", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txt_PONo.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }
            }
            if (ddlgroup.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Group", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddlgroup.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (txt_itemname.Text == "" || !txt_itemname.Text.Contains(":"))
            {
                Messagealert_.ShowMessage(lblmessage, "ItemName", 0);
                txt_itemname.Text = "";
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_itemname.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (txtHSNCode.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "HSNCode", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txtHSNCode.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            if (txt_batchno.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "BatchNo", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_batchno.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            if (ddl_mfgcompnay.SelectedValue == "0")
            {
                Messagealert_.ShowMessage(lblmessage, "mfgcompany", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_mfgcompnay.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            //if (txt_supplier.Text == "")
            //{
            //    Messagealert_.ShowMessage(lblmessage, "supplier", 0);
            //    divmsg1.Visible = true;
            //    divmsg1.Attributes["class"] = "FailAlert";
            //    txt_supplier.Focus();
            //    return;
            //}
            //else
            //{
            //    lblmessage.Visible = false;
            //}
            if (ddl_Supplier.SelectedValue == "0")
            {
                Messagealert_.ShowMessage(lblmessage, "supplier", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_Supplier.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (txt_receiveddate.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "RecivedDate", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                txt_receiveddate.Focus();
                return;
            }
            else
            {
                if (Commonfunction.isValidDate(txt_receiveddate.Text) == false || Commonfunction.CheckOverDate(txt_receiveddate.Text) == true)
                {
                    Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txt_receiveddate.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
            }
            if (txt_expdate.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "ExpirDate", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                txt_expdate.Focus();
                return;
            }
            else
            {
                if (Commonfunction.isValidDate(txt_expdate.Text) == false || Commonfunction.ChecklowerDate(txt_expdate.Text) == true)
                {
                    Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txt_expdate.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
            }
           
            if (txt_no0funit.Text == "0" || txt_no0funit.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Nounit", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_no0funit.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            if (txt_qty.Text == "0" || txt_qty.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Unitperqty", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_qty.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            if (txt_totalreceivedqty.Text == "0" || txt_totalreceivedqty.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Quantity", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_totalreceivedqty.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            if (txt_totalcp.Text == "0.0" || txt_totalcp.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Costprice", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_totalcp.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }

            if (txt_rateperunit.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Please enter amount per unit.", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_rateperunit.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            if (txt_mrpperunit.Text == "0.0" || txt_mrpperunit.Text == "0" || txt_mrpperunit.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "MRP", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_mrpperunit.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            if (txt_totalmrp.Text == "0.0" || txt_totalmrp.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "MRP", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_totalmrp.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }

            IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
            List<StockGRNData> StockItemList = Session["StockItemList"] == null ? new List<StockGRNData>() : (List<StockGRNData>)Session["StockItemList"];
            StockGRNData objStock = new StockGRNData();
            objStock.ReceiptNo = txt_receivedno.Text.ToString() == "" ? "" : txt_receivedno.Text.ToString();
            objStock.HSNCode = txtHSNCode.Text.ToString() == "" ? "" : txtHSNCode.Text.ToString();
            objStock.BatchNo = txt_batchno.Text.ToString() == "" ? "" : txt_batchno.Text.ToString();
            objStock.PONo = txt_PONo.Text.ToString() == "" ? "" : txt_PONo.Text.ToString();
            objStock.ItemName = txt_itemname.Text.ToString() == "" ? "" : txt_itemname.Text.ToString() + " ( " + txt_no0funit.Text + " X " + txt_qty.Text + " )";
            string ID;
            var source = txt_itemname.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);

                //// Check Duplicate data 
                //foreach (GridViewRow row in gvstocklist.Rows)
                //{
                //    Label ItemID = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");

                //    if (Convert.ToInt32(ItemID.Text) == Convert.ToInt32(ID))
                //    {
                //        Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                //        divmsg1.Visible = true;
                //        divmsg1.Attributes["class"] = "FailAlert";
                //        txt_itemname.Focus();
                //        return;
                //    }
                //    else
                //    {
                //        lblmessage.Visible = false;
                //    }
                //}
            }
            else
            {
                txt_itemname.Text = "";
                return;
            }
            objStock.ItemID = Convert.ToInt32(ID);
            objStock.CompanyID = Convert.ToInt32(ddl_mfgcompnay.SelectedValue == "" ? "0" : ddl_mfgcompnay.SelectedValue);
            objStock.NoOfUnit = Convert.ToInt32(txt_no0funit.Text == "" ? "0" : txt_no0funit.Text);
            objStock.QtyperUnit = Convert.ToInt32(txt_qty.Text == "" ? "0" : txt_qty.Text);
            objStock.FreeQuantity = Convert.ToInt32(txt_freeqty.Text == "" ? "0" : txt_freeqty.Text) * Convert.ToInt32(txt_qty.Text == "" ? "0" : txt_qty.Text);
            objStock.TotalMRP = Convert.ToDecimal(txt_totalmrp.Text == "" ? "0" : txt_totalmrp.Text);
            objStock.MRPPerQty = Convert.ToDecimal(txt_mrp.Text == "" ? "0" : txt_mrp.Text);
            objStock.MRPperUnit = Convert.ToDecimal(txt_mrpperunit.Text == "" ? "0" : txt_mrpperunit.Text);
            objStock.DiscountType = Convert.ToInt32(ddl_discountype.SelectedValue == "" ? "0" : ddl_discountype.SelectedValue);
            objStock.Discount = ddl_discountype.SelectedValue == "1" ? Convert.ToDecimal(txt_discountperqty.Text == "" ? "0" : txt_discountperqty.Text) / 100 * Convert.ToDecimal(txt_totalcp.Text == "" ? "0" : txt_totalcp.Text) : Convert.ToDecimal(txt_discountperqty.Text == "" ? "0" : txt_discountperqty.Text);
            objStock.TotalRecdQty = Convert.ToInt32(txt_totalreceivedqty.Text == "" ? "0" : txt_totalreceivedqty.Text);
            objStock.TaxableAmount = Convert.ToDecimal(txt_CpberforeTax.Text == "" ? "0" : txt_CpberforeTax.Text);
            objStock.SGST = Convert.ToDouble(txt_sgst.Text == "" ? "0" : txt_sgst.Text);
            objStock.CGST = Convert.ToDouble(txt_cgst.Text == "" ? "0" : txt_cgst.Text);
            objStock.IGST = Convert.ToDouble(txt_igst.Text == "" ? "0" : txt_igst.Text);
            objStock.CP = Convert.ToDecimal(txt_totalcp.Text == "" ? "0" : txt_totalcp.Text);
            objStock.CPafterTax = Convert.ToDecimal(txt_RateafterTax.Text == "" ? "0" : txt_RateafterTax.Text);
            objStock.CPperunit = Convert.ToDecimal(txt_rateperunit.Text == "" ? "0" : txt_rateperunit.Text);
            objStock.Temperature = txt_temperature.Text.Trim();
            //var source2 = txt_supplier.Text.ToString();
            //if (source2.Contains(":"))
            //{
            //    string ID2 = source2.Substring(source2.LastIndexOf(':') + 1);
            //    objStock.SupplierID = Convert.ToInt32(ID2);
            //}
            objStock.SupplierID = Convert.ToUInt32(ddl_Supplier.SelectedValue);
            objStock.FreeItemAmount = Convert.ToInt32(txt_freeqty.Text == "" ? "0" : txt_freeqty.Text) * Convert.ToDecimal(txt_cp.Text == "" ? "0" : txt_cp.Text);
            objStock.TotalQuantity = Convert.ToInt32(txt_no0funit.Text == "" ? "0" : txt_no0funit.Text) * Convert.ToInt32(txt_qty.Text == "" ? "0" : txt_qty.Text);
            objStock.ReceivedBy = LogData.EmployeeID;
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime ReceivedDate = txt_receiveddate.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_receiveddate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime ExpDate = txt_expdate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_expdate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objStock.ReceivedDate = ReceivedDate;
            objStock.ExpDate = ExpDate;
            hdn_total_free_item_Amount.Value = "0";
            hdn_total_free_item_Amount.Value = Commonfunction.Getrounding((Convert.ToDecimal(hdn_total_free_item_Amount.Value == null ? "0.0" : hdn_total_free_item_Amount.Value) + Convert.ToInt32(txt_freeqty.Text == "" ? "0" : txt_freeqty.Text) * Convert.ToDecimal(txt_cp.Text == "" ? "0" : txt_cp.Text)).ToString());
            txt_recivedqty.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_recivedqty.Text == "" ? "0" : txt_recivedqty.Text) + Convert.ToInt32(txt_no0funit.Text == "" ? "0" : txt_no0funit.Text) * Convert.ToInt32(txt_qty.Text == "" ? "0" : txt_qty.Text)).ToString());
            txt_totalfreeqty.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalfreeqty.Text == "" ? "0" : txt_totalfreeqty.Text) + Convert.ToInt32(txt_freeqty.Text == "" ? "0" : txt_freeqty.Text) * Convert.ToInt32(txt_qty.Text == "" ? "0" : txt_qty.Text)).ToString());
            txt_total_recvqty.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_total_recvqty.Text == "" ? "0" : txt_total_recvqty.Text) + Convert.ToDecimal(txt_totalreceivedqty.Text == "" ? "0" : txt_totalreceivedqty.Text)).ToString());
            txt_totalamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalamount.Text == "" ? "0" : txt_totalamount.Text) + Convert.ToDecimal(txt_RateafterTax.Text == "" ? "0" : txt_RateafterTax.Text)).ToString());
			txt_payableamt.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_nettotalCp.Text == "" ? "0" : txt_nettotalCp.Text) + Convert.ToDecimal(txt_RateafterTax.Text == "" ? "0" : txt_RateafterTax.Text) - Convert.ToDecimal(txttotalreturnamt.Text == "" ? "0" : txttotalreturnamt.Text)).ToString());
			
			txt_nettotalCp.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_nettotalCp.Text == "" ? "0" : txt_nettotalCp.Text) + Convert.ToDecimal(txt_RateafterTax.Text == "" ? "0" : txt_RateafterTax.Text)).ToString());
            txt_totalMRPS.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalMRPS.Text == "" ? "0" : txt_totalMRPS.Text) + Convert.ToDecimal(txt_totalmrp.Text == "" ? "0" : txt_totalmrp.Text)).ToString());
			

            StockItemList.Add(objStock);
            if (StockItemList.Count > 0)
            {
                gvstocklist.DataSource = StockItemList;
                gvstocklist.DataBind();
                gvstocklist.Visible = true;
                Session["StockItemList"] = StockItemList;
                clearall();
                txt_itemname.Focus();
                btnsave.Attributes.Remove("disabled");
                //txt_supplier.Attributes["disabled"]="disabled";
                ddl_Supplier.Attributes["disabled"] = "disabled";
            }
            else
            {
                gvstocklist.DataSource = null;
                gvstocklist.DataBind();
                gvstocklist.Visible = true;
            }
        }
        protected void gvstocklist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSerial = (Label)e.Row.FindControl("lblserialID");
                lblSerial.Text = ((gvstocklist.PageIndex * gvstocklist.PageSize) + e.Row.RowIndex + 1).ToString();
            }
        }
        protected void gvstocklist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvstocklist.Rows[i];
                    List<StockGRNData> ItemList = Session["StockItemList"] == null ? new List<StockGRNData>() : (List<StockGRNData>)Session["StockItemList"];
                    Decimal totalmrp = ItemList[i].TotalMRP;
                    Decimal totalcp = ItemList[i].CPafterTax;
                    Decimal recvdqty = ItemList[i].TotalQuantity;
                    Decimal freeqty = ItemList[i].FreeQuantity;
                    Decimal totalrecvdqty = ItemList[i].TotalRecdQty;
                    Decimal FreeItemAmount = ItemList[i].FreeItemAmount;
                    hdn_total_free_item_Amount.Value = Commonfunction.Getrounding((Convert.ToDecimal(hdn_total_free_item_Amount.Value == null ? "0.0" : hdn_total_free_item_Amount.Value) - FreeItemAmount).ToString());
                    txt_recivedqty.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_recivedqty.Text == "" ? "0" : txt_recivedqty.Text) - recvdqty).ToString());
                    txt_total_recvqty.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_total_recvqty.Text == "" ? "0" : txt_total_recvqty.Text) - totalrecvdqty).ToString());
                    txt_totalfreeqty.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalfreeqty.Text == "" ? "0" : txt_totalfreeqty.Text) - freeqty).ToString());
                    txt_totalamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalamount.Text == "" ? "0" : txt_totalamount.Text) - totalcp).ToString());
					txt_payableamt.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_nettotalCp.Text == "" ? "0" : txt_nettotalCp.Text) - totalcp - Convert.ToDecimal(txttotalreturnamt.Text == "" ? "0" : txttotalreturnamt.Text)).ToString());
					
					txt_nettotalCp.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_nettotalCp.Text == "" ? "0" : txt_nettotalCp.Text) - totalcp).ToString());
                    txt_totalMRPS.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalMRPS.Text == "" ? "0" : txt_totalMRPS.Text) - totalmrp).ToString());
					
                    ItemList.RemoveAt(i);
                    txt_due.Text = "";
                    txt_paidamount.Text = "";
                    txt_discount.Text = "";
                    Session["StockItemList"] = ItemList;
                    gvstocklist.DataSource = ItemList;
                    gvstocklist.DataBind();

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
		protected void btnyes_Click(object sender, EventArgs e)
		{
			mpconfirmation.Hide();
			if (LogData.SaveEnable == 0)
			{
				Messagealert_.ShowMessage(lblmessage, "SaveEnable", 0);
				divmsg1.Visible = true;
				divmsg1.Attributes["class"] = "FailAlert";
				return;
			}
			else
			{
				lblmessage.Visible = false;
			}
			if (ddl_purchagetype.SelectedIndex == 0)
			{

				Messagealert_.ShowMessage(lblmessage, "Purchagetype", 0);
				divmsg1.Visible = true;
				divmsg1.Attributes["class"] = "FailAlert";
				ddl_purchagetype.Focus();
				return;
			}
			else
			{
				if (txt_receivedno.Text == "")
				{
					Messagealert_.ShowMessage(lblmessage, "RCNO", 0);
					divmsg1.Visible = true;
					divmsg1.Attributes["class"] = "FailAlert";
					txt_receivedno.Focus();
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}
				if (ddl_purchagetype.SelectedIndex == 1)
				{
					if (txt_PONo.Text == "")
					{
						Messagealert_.ShowMessage(lblmessage, "PO", 0);
						divmsg1.Visible = true;
						divmsg1.Attributes["class"] = "FailAlert";
						txt_PONo.Focus();
						return;
					}
					else
					{
						lblmessage.Visible = false;
					}
				}
				else
				{
					lblmessage.Visible = false;
				}
			}
			if (ddl_purchagetype.SelectedIndex == 0)
			{
				Messagealert_.ShowMessage(lblmessage, "Purchagetype", 0);
				divmsg1.Visible = true;
				divmsg1.Attributes["class"] = "FailAlert";
				return;
			}
			else
			{
				lblmessage.Visible = false;
			}
            //if (txt_supplier.Text == "")
            //{
            //    Messagealert_.ShowMessage(lblmessage, "supplier", 0);
            //    divmsg1.Visible = true;
            //    divmsg1.Attributes["class"] = "FailAlert";
            //    txt_supplier.Focus();
            //    return;
            //}
            //else
            //{
            //    lblmessage.Visible = false;
            //}
            if (ddl_Supplier.SelectedValue == "0")
            {
                Messagealert_.ShowMessage(lblmessage, "supplier", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_Supplier.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
			List<StockGRNData> ListStock = new List<StockGRNData>();
			List<StockGRNData> retuenStock = new List<StockGRNData>();
			StockGRNData objStock = new StockGRNData();
			StockGRNBO objBO = new StockGRNBO();

			try
			{
				// get all the record from the gridview
				int itemcount = 0;
				foreach (GridViewRow row in gvstocklist.Rows)
				{
					IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
					Label ReceiptNo = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_receiptNo");
                    Label HSNCode = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lblHSNCode");
					Label BatchNo = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_batchno");
					Label PONo = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_PONo");
					Label RecdDate = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_ReceivedDate");
					Label MfgDate = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_MfgDate");
					Label ExpDate = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_ExpDate");
					Label ItemID = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
					Label CompanyID = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_CompanyID");
					Label SupplierID = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_SupplierID");
					Label NoOfUnit = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_NoOfUnit");
					Label QtyperUnit = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_QtyperUnit");
					Label CPperunit = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_CPperunit");
					Label CP = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_cp");
					Label MRPperunit = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_MRPperunit");
					Label MRP = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_totalmrp");
					Label Tax = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_tax");
					Label TotalRecdQty = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_totalrecdquantity");
					Label FreeQuantity = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_freequantity");
					Label RecvQty = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_totalquantity");
					Label SerialID = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lblserialID");
					Label Particulars = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lblparticulars");
					Label ID = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
					Label Sgst = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_sgst");
					Label Cgst = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_cgst");
					Label Igst = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_igst");
					Label Taxable = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_taxable");
					Label CpAfterTax = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_cpAfterTax");
					Label Temp = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_temp");
					Label Disc = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_disc");

					StockGRNData ObjDetails = new StockGRNData();
					ObjDetails.ReceiptNo = ReceiptNo.Text == "" ? "0" : ReceiptNo.Text;
                    ObjDetails.HSNCode = HSNCode.Text == "" ? "0" : HSNCode.Text;
					ObjDetails.BatchNo = BatchNo.Text == "" ? "0" : BatchNo.Text;

					IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
					DateTime ReceivedDate1 = RecdDate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(RecdDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
					DateTime MfgDate1 = MfgDate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(MfgDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
					DateTime ExpDate1 = ExpDate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(ExpDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

					ObjDetails.PONo = PONo.Text == "" ? "0" : PONo.Text;
					//ObjDetails.MfgDate = MfgDate1;
					ObjDetails.ReceivedDate = ReceivedDate1;
					ObjDetails.ExpDate = ExpDate1;
					ObjDetails.PurchasetypeID = Convert.ToInt32(ddl_purchagetype.SelectedValue == "" ? "0" : ddl_purchagetype.SelectedValue);
					ObjDetails.ItemID = Convert.ToInt32(ItemID.Text == "" ? "" : ItemID.Text);
					ObjDetails.CompanyID = Convert.ToInt32(CompanyID.Text == "" ? "" : CompanyID.Text);
					ObjDetails.SupplierID = Convert.ToInt32(SupplierID.Text == "" ? "" : SupplierID.Text);
					ObjDetails.NoOfUnit = Convert.ToInt32(NoOfUnit.Text == "" ? "" : NoOfUnit.Text);
					ObjDetails.QtyperUnit = Convert.ToInt32(QtyperUnit.Text == "" ? "" : QtyperUnit.Text);
					ObjDetails.CPperunit = Convert.ToDecimal(CPperunit.Text == "" ? "" : CPperunit.Text);
					ObjDetails.CP = Convert.ToDecimal(CP.Text == "" ? "" : CP.Text);
					ObjDetails.MRPperunit = Convert.ToDecimal(MRPperunit.Text == "" ? "0" : MRPperunit.Text);
					ObjDetails.MRP = Convert.ToDecimal(MRP.Text == "" ? "" : MRP.Text);
					ObjDetails.FreeQuantity = Convert.ToInt32(FreeQuantity.Text == "" ? "0" : FreeQuantity.Text);
					ObjDetails.RecvQty = Convert.ToInt32(RecvQty.Text == "" ? "0" : RecvQty.Text);
					ObjDetails.TotalRecdQty = Convert.ToInt32(TotalRecdQty.Text == "" ? "0" : TotalRecdQty.Text);
					ObjDetails.SerialID = Convert.ToInt32(SerialID.Text == "" ? "0" : SerialID.Text);
					ObjDetails.ID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
					ObjDetails.TaxableAmount = Convert.ToDecimal(Taxable.Text == "" ? "0" : Taxable.Text);
					ObjDetails.SGST = Convert.ToDouble(Sgst.Text == "" ? "0" : Sgst.Text);
					ObjDetails.CGST = Convert.ToDouble(Cgst.Text == "" ? "0" : Cgst.Text);
					ObjDetails.IGST = Convert.ToDouble(Igst.Text == "" ? "0" : Igst.Text);
					ObjDetails.CPafterTax = Convert.ToDecimal(CpAfterTax.Text == "" ? "0" : CpAfterTax.Text);
					ObjDetails.Discount = Convert.ToDecimal(Disc.Text == "" ? "0" : Disc.Text);
					ObjDetails.Temperature = Temp.Text.Trim();
					itemcount = itemcount + 1;
					ListStock.Add(ObjDetails);
				}
				objStock.XMLData = XmlConvertor.StockDetailsDatatoXML(ListStock).ToString();
				foreach (GridViewRow row in gvstockreturn.Rows)
				{
					Label SerialID = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lblrtnserialID");
					Label lblrtnID = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lblrtnID");
					Label lbl_rtnbatchno = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtnbatchno");
					Label lbl_rtnreceiptNo = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtnreceiptNo");					
					Label lbl_rtnItemID = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtnItemID");
					Label lbl_rtnCompanyID = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtnCompanyID");
					Label lbl_rtnSupplierID = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtnSupplierID");
					Label lbl_rtntotalquantity = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtntotalquantity");
					Label lbl_rtntaxable = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtntaxable");
					Label lbl_rtnsgst = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtnsgst");
					Label lbl_rtncgst = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtncgst");
					Label lbl_rtnigst = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtnigst");
					Label lbl_rtncpAfterTax = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_rtncpAfterTax");
					StockGRNData Objstockreturn = new StockGRNData();
					Objstockreturn.StockID = Convert.ToInt64(lblrtnID.Text == "" ? "0" : lblrtnID.Text);
					Objstockreturn.SerialID = Convert.ToInt32(SerialID.Text == "" ? "0" : SerialID.Text);
					Objstockreturn.ReceiptNo = lbl_rtnreceiptNo.Text == "" ? "0" : lbl_rtnreceiptNo.Text;
					Objstockreturn.BatchNo = lbl_rtnbatchno.Text == "" ? "0" : lbl_rtnbatchno.Text;
					Objstockreturn.ItemID = Convert.ToInt32(lbl_rtnItemID.Text == "" ? "" : lbl_rtnItemID.Text);
					Objstockreturn.CompanyID = Convert.ToInt32(lbl_rtnCompanyID.Text == "" ? "" : lbl_rtnCompanyID.Text);
					Objstockreturn.SupplierID = Convert.ToInt32(lbl_rtnSupplierID.Text == "" ? "" : lbl_rtnSupplierID.Text);
					Objstockreturn.ReturnQty = Convert.ToInt32(lbl_rtntotalquantity.Text == "" ? "" : lbl_rtntotalquantity.Text);
					Objstockreturn.TaxableAmount = Convert.ToDecimal(lbl_rtntaxable.Text == "" ? "0" : lbl_rtntaxable.Text);
					Objstockreturn.SGST = Convert.ToDouble(lbl_rtnsgst.Text == "" ? "0" : lbl_rtnsgst.Text);
					Objstockreturn.CGST = Convert.ToDouble(lbl_rtncgst.Text == "" ? "0" : lbl_rtncgst.Text);
					Objstockreturn.IGST = Convert.ToDouble(lbl_rtnigst.Text == "" ? "0" : lbl_rtnigst.Text);
					Objstockreturn.CPafterTax = Convert.ToDecimal(lbl_rtncpAfterTax.Text == "" ? "0" : lbl_rtncpAfterTax.Text);
					retuenStock.Add(Objstockreturn);
				}
				objStock.returnXMLData = XmlConvertor.ReturnStockDetailsDatatoXML(retuenStock).ToString();
				objStock.TotalReturnQty = Convert.ToInt32(txtgvreturnqty.Text == "" ? "0" : txtgvreturnqty.Text);
				objStock.TrecievedQty = Convert.ToInt32(txt_recivedqty.Text == "" ? "0" : txt_recivedqty.Text);
				objStock.TotalFreeQty = Convert.ToInt32(txt_totalfreeqty.Text == "" ? "0" : txt_totalfreeqty.Text);
				objStock.TotalrecievedQty = Convert.ToInt32(txt_total_recvqty.Text == "" ? "0" : txt_total_recvqty.Text);
				objStock.TotalFreeItemAmount = Convert.ToDecimal(hdn_total_free_item_Amount.Value);
				objStock.ReceivedBy = Convert.ToInt64(ddl_receivedby.SelectedValue == "" ? "0" : ddl_receivedby.SelectedValue);
				objStock.PurchasetypeID = Convert.ToInt32(ddl_purchagetype.SelectedValue == "" ? "0" : ddl_purchagetype.SelectedValue);
				objStock.PaidAmount = Convert.ToDecimal(txt_paidamount.Text == "" ? "0.0" : txt_paidamount.Text);
				objStock.SubTotalMRP = Convert.ToDecimal(txt_totalMRPS.Text == "" ? "0.0" : txt_totalMRPS.Text);
				objStock.SubTotalCP = Convert.ToDecimal(txt_totalamount.Text == "" ? "0.0" : txt_totalamount.Text);
				objStock.Payableamt = Convert.ToDecimal(txt_payableamt.Text == "" ? "0.0" : txt_payableamt.Text);
				objStock.Returnamt = Convert.ToDecimal(txtgvreturnamt.Text == "" ? "0.0" : txtgvreturnamt.Text);
				objStock.NetTotalCP = Convert.ToDecimal(txt_nettotalCp.Text == "" ? "0.0" : txt_nettotalCp.Text);
				objStock.Discount = Convert.ToDecimal(txt_discount.Text == "" ? "0.0" : txt_discount.Text);
				objStock.DueAmt = Convert.ToDecimal(txt_due.Text == "" ? "0.0" : txt_due.Text);
				objStock.PONo = txt_PONo.Text.Trim();
				objStock.ReceiptNo = txt_receivedno.Text.Trim();
								
				
				if (itemcount == 0)
				{
					Messagealert_.ShowMessage(lblmessage, "ItemCount", 0);
					divmsg1.Visible = true;
					divmsg1.Attributes["class"] = "FailAlert";
					return;
				}
				else
				{
					lblmessage.Visible = false;
				}
				objStock.HospitalID = LogData.HospitalID;
				objStock.EmployeeID = LogData.EmployeeID;
                //var source2 = txt_supplier.Text.ToString();
                //if (source2.Contains(":"))
                //{
                //    string ID2 = source2.Substring(source2.LastIndexOf(':') + 1);
                //    objStock.SupplierID = Convert.ToInt32(ID2);
                //}
                objStock.SupplierID = Convert.ToUInt32(ddl_Supplier.SelectedValue);
				objStock.FinancialYearID = LogData.FinancialYearID;
				objStock.ActionType = Enumaction.Insert;

				int result = objBO.UpdateStockItemDetails(objStock);
				if (result == 1)
				{
					lblmessage.Visible = true;
					Messagealert_.ShowMessage(lblmessage, "save", 1);
					divmsg1.Visible = true;
					divmsg1.Attributes["class"] = "SucessAlert";
					btnsave.Attributes["disabled"] = "disabled";
					hdn_total_free_item_Amount.Value = null;
					//txt_supplier.Attributes.Remove("disabled");
                    ddl_Supplier.Attributes.Remove("disabled");
				}
				else
				{
					Messagealert_.ShowMessage(lblmessage, "system", 0);
					divmsg1.Visible = true;
					divmsg1.Attributes["class"] = "FailAlert";
				}

			}

			catch (Exception ex) //Exception in agent layer itself
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
				string msg = ex.ToString();
				Messagealert_.ShowMessage(lblmessage, msg, 0);
				divmsg1.Visible = true;
				divmsg1.Attributes["class"] = "FailAlert";
			}
		}
		protected void btnno_Click(object sender, EventArgs e)
		{
			mpconfirmation.Hide();
		}
        protected void btnsave_Click(object sender, EventArgs e)
        {
			lbl_totalrecievedqty.Text = txt_total_recvqty.Text.Trim() == "" ? "0" : txt_total_recvqty.Text.Trim();
			lbl_totpaidamount.Text = txt_paidamount.Text.Trim() == "" ? "0" : txt_paidamount.Text.Trim();
			lbl_totaldiscount.Text = txt_discount.Text.Trim() == "" ? "0" : txt_discount.Text.Trim();
			lbl_totdueamount.Text = txt_due.Text.Trim() == "" ? "0" : txt_due.Text.Trim();
			lbl_totalreturnamt.Text = txttotalreturnamt.Text.Trim() == "" ? "0" : txttotalreturnamt.Text.Trim();
			Refundableamt.Text = Convert.ToDecimal(txt_payableamt.Text.Trim() == "" ? "0" : txt_payableamt.Text.Trim()) > 0 ? "0" : (Convert.ToDecimal(txt_payableamt.Text.Trim())*-1).ToString();

			mpconfirmation.Show();
			btnyes.Focus();
			
        }
        protected void clearall()
        {
            txt_receiveddate.Text = "";
            txt_totalreceivedqty.Text = "";
            txt_qty.Text = "";
            txt_no0funit.Text = "";
            txt_cp.Text = "";
            txt_totalcp.Text = "";
            txt_mrp.Text = "";
            txt_totalmrp.Text = "";
            txt_freeqty.Text = "";
            txt_discount.Text = "";
            txt_itemname.Text = "";
            txtHSNCode.Text = "";
            txt_batchno.Text = "";
            txt_expdate.Text = "";
            txt_temperature.Text = "";
            txt_discountperqty.Text = "";
            txt_CpberforeTax.Text = "";
            txt_cgst.Text = "";
            txt_sgst.Text = "";
            txt_igst.Text = "";
            txt_RateafterTax.Text = "";
            txt_rateperunit.Text = "";
            txt_mrpperunit.Text = "";
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
			
            gvstocklist.DataSource = null;
            gvstocklist.DataBind();
            gvstocklist.Visible = false;
            ViewState["ID"] = null;
            lblmessage.Visible = false;
            txt_receivedno.Text = "";
            txt_receiveddate.Text = "";
            txt_PONo.Text = "";
            ddl_mfgcompnay.SelectedIndex = 0;
            //txt_supplier.Text = "";
            ddl_Supplier.SelectedValue = "0";
            txt_qty.Text = "";
            txt_no0funit.Text = "";
            txt_cp.Text = "";
            txt_totalcp.Text = "";
            txt_mrp.Text = "";
            txt_totalmrp.Text = "";
            txt_freeqty.Text = "";
            txt_discount.Text = "";
            ddl_purchagetype.SelectedIndex = 0;
            txt_itemname.Text = "";
            txt_batchno.Text = "";
            ddlgroup.SelectedIndex = 0;
            txt_expdate.Text = "";
            txt_totalreceivedqty.Text = "";
            txt_totalMRPS.Text = "";
            txt_totalamount.Text = "";
            //ddl_receivedby.SelectedIndex = 0;
            txt_recivedqty.Text = "";
            txt_totalfreeqty.Text = "";
            txt_total_recvqty.Text = "";
            Session["StockItemList"] = null;
            txt_temperature.Text = "";
            txt_discountperqty.Text = "";
            txt_CpberforeTax.Text = "";
            txt_cgst.Text = "";
            txt_sgst.Text = "";
            txt_igst.Text = "";
            txt_RateafterTax.Text = "";
            txt_nettotalCp.Text = "";
            txt_paidamount.Text = "";
            txt_due.Text = "";
			Session["StockreturnItemList"] = null;
			clearrtn();
			gvstockreturn.DataSource = null;
			gvstockreturn.DataBind();
            //txt_supplier.Attributes.Remove("disabled");
            ddl_Supplier.Attributes.Remove("disabled");
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetrecdNo(string prefixText, int count, string contextKey)
        {
            StockGRNData Objpaic = new StockGRNData();
            StockGRNBO objInfoBO = new StockGRNBO();
            List<StockGRNData> getResult = new List<StockGRNData>();
            Objpaic.ReceiptNo = prefixText;
            getResult = objInfoBO.GetrecdNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ReceiptNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPONo(string prefixText, int count, string contextKey)
        {
            StockGRNData Objpaic = new StockGRNData();
            StockGRNBO objInfoBO = new StockGRNBO();
            List<StockGRNData> getResult = new List<StockGRNData>();
            Objpaic.PONo = prefixText;
            getResult = objInfoBO.GetPONo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PONo.ToString());
            }
            return list;
        }
        protected void gvstocklist1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblqty = (Label)e.Row.FindControl("lbl_qty");
                int a_qty = Int32.Parse(lblqty.Text);
                total = total + a_qty;

                Label lblrecdqty = (Label)e.Row.FindControl("lbl_totalrecdquantity");
                int qty = Int32.Parse(lblrecdqty.Text);
                total1 = total1 + qty;

                Label lbl_totfreeqty = (Label)e.Row.FindControl("lbl_freequantity");
                int freeqty = Int32.Parse(lbl_totfreeqty.Text);
                total2 = total2 + freeqty;

                Label lbl_cp = (Label)e.Row.FindControl("lbl_cp");
                decimal cp = Decimal.Parse(lbl_cp.Text);
                total3 = total3 + cp;

                Label lbl_cpperqty = (Label)e.Row.FindControl("lbl_cpperqty");
                decimal cpperqty = Decimal.Parse(lbl_cpperqty.Text);
                total5 = total5 + cpperqty;

                Label lbl_totalmrp = (Label)e.Row.FindControl("lbl_totalmrp");
                decimal totalmrp = Decimal.Parse(lbl_totalmrp.Text);
                total6 = total6 + totalmrp;

                Label lbl_mrpperunit = (Label)e.Row.FindControl("lbl_mrpperunit");
                decimal mrpperunit = Decimal.Parse(lbl_mrpperunit.Text);
                total7 = total7 + mrpperunit;

                Label lbl_mrpperqty = (Label)e.Row.FindControl("lbl_mrpperqty");
                decimal mrpperqty = Decimal.Parse(lbl_mrpperqty.Text);
                total8 = total8 + mrpperqty;

                Label lbl_totalissued = (Label)e.Row.FindControl("lbl_totalissued");
                int totalissued = Int32.Parse(lbl_totalissued.Text);
                total9 = total9 + totalissued;

                //TextBox txt_totalcondemned = (TextBox)e.Row.FindControl("txt_totalcondemned");
                //int totalcondemned = Int32.Parse(txt_totalcondemned.Text);
                //total10 = total10 + totalcondemned;

                Label lbl_balstock = (Label)e.Row.FindControl("lbl_balstock");
                int totalbal = Int32.Parse(lbl_balstock.Text);
                total11 = total11 + totalbal;

                //Label lbl_cpremitem = (Label)e.Row.FindControl("lbl_cpremitem");
                //decimal totalcpremitem = Decimal.Parse(lbl_cpremitem.Text);
                //total12 = total12 + totalcpremitem;

                //Label lbl_mrpremitem = (Label)e.Row.FindControl("lbl_mrpremitem");
                //decimal totalmrpremitem = Decimal.Parse(lbl_mrpremitem.Text);
                //total13 = total13 + totalmrpremitem;

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label totalqty = (Label)e.Row.FindControl("lbl_totalqty");
                totalqty.Text = total.ToString();

                Label totalrecdqty = (Label)e.Row.FindControl("lbl_totrecdqty");
                totalrecdqty.Text = total1.ToString();

                Label lbl_totfreeqty = (Label)e.Row.FindControl("lbl_totfreeqty");
                lbl_totfreeqty.Text = total2.ToString();

                Label lbl_totcp = (Label)e.Row.FindControl("lbl_totcp");
                lbl_totcp.Text = total3.ToString();

                Label lbl_totcpperqty = (Label)e.Row.FindControl("lbl_totcpperqty");
                lbl_totcpperqty.Text = total5.ToString();

                Label lbl_totmrp = (Label)e.Row.FindControl("lbl_totmrp");
                lbl_totmrp.Text = total6.ToString();

                Label lbl_totmrpperunit = (Label)e.Row.FindControl("lbl_totmrpperunit");
                lbl_totmrpperunit.Text = total7.ToString();

                Label lbl_totmrpperqty = (Label)e.Row.FindControl("lbl_totmrpperqty");
                lbl_totmrpperqty.Text = total8.ToString();

                Label lbl_totissued = (Label)e.Row.FindControl("lbl_totissued");
                lbl_totissued.Text = total9.ToString();

                //TextBox txt_totcondemned = (TextBox)e.Row.FindControl("txt_totcondemned");
                //txt_totcondemned.Text = total10.ToString();

                Label lbl_totbalstock = (Label)e.Row.FindControl("lbl_totbalstock");
                lbl_totbalstock.Text = total11.ToString();

                Label lbl_totcpremitem = (Label)e.Row.FindControl("lbl_totcpremitem");
                lbl_totcpremitem.Text = total12.ToString();

                Label lbl_totmrpremitem = (Label)e.Row.FindControl("lbl_totmrpremitem");
                lbl_totmrpremitem.Text = total13.ToString();


            }

        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void chekboxselect_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvstocklist1.Rows)
            {
                CheckBox cb = (CheckBox)gvstocklist1.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                if (cb.Checked)
                {
                    TextBox txt_totalcondemned = (TextBox)gvstocklist1.Rows[row.RowIndex].Cells[0].FindControl("txt_totalcondemned"); //find the CheckBox
                    txt_totalcondemned.ReadOnly = false;
                    txt_totalcondemned.Focus();
                }
            }
        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            List<StockGRNData> ListStock = new List<StockGRNData>();
            StockGRNData objStock = new StockGRNData();
            StockGRNBO objBO = new StockGRNBO();
            try
            {

                // get all the record from the gridview
                foreach (GridViewRow row in gvstocklist1.Rows)
                {
                    CheckBox cb = (CheckBox)gvstocklist1.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                    if (cb.Checked)
                    {
                        Label ID = (Label)gvstocklist1.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                        TextBox TotalCondemned = (TextBox)gvstocklist1.Rows[row.RowIndex].Cells[0].FindControl("txt_totalcondemned");
                        StockGRNData ObjDetails = new StockGRNData();
                        ObjDetails.ID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                        ObjDetails.TotalCondemned = Convert.ToInt16(TotalCondemned.Text == "" ? "0" : TotalCondemned.Text);
                        ListStock.Add(ObjDetails);
                    }
                }
                objStock.XMLData = XmlConvertor.StockCondemnedDatatoXML(ListStock).ToString();
                objStock.EmployeeID = LogData.EmployeeID;

                if (ViewState["ID"] != null)
                {
                    objStock.ActionType = Enumaction.Update;
                    objStock.ID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objBO.UpdateStockCondemnedItemDetails(objStock);
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    ViewState["ID"] = null;
                    bindgrid();

                }
                else if (result == 5)
                {
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                }
            }

            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);

            }

        }
        protected void bindgrid()
        {
            try
            {
                if (LogData.SearchEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage1, "SearchEnable", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage1.Visible = false;
                }

                List<StockGRNData> objdeposit = GetStockItemList(0);
                if (objdeposit.Count > 0)
                {
                    gvstocklist1.DataSource = objdeposit;
                    gvstocklist1.DataBind();
                    gvstocklist1.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblmessage1.Visible = false;
                    lblmessage1.Visible = false;
                }
                else
                {
                    gvstocklist1.DataSource = null;
                    gvstocklist1.DataBind();
                    gvstocklist1.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult.Visible = false;
                    divmsg3.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage1, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        protected void ddlpurchagetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_receivedno.Text = "";
            txt_PONo.Text = "";
            if (ddl_purchagetype.SelectedIndex == 1)
            {
                txt_PONo.ReadOnly = false;
                idpo.Visible = true;
                txt_PONo.Focus();
            }
            else
            {
                txt_PONo.ReadOnly = true;
                idpo.Visible = false;
                txt_receivedno.Focus();
            }
        }
        protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender2.ContextKey = ddlgroup.SelectedValue;
            txt_itemname.Text = "";
            txt_batchno.Text = "";
            txt_receiveddate.Text = "";               
            txt_expdate.Text = "";
            txt_qty.Text = "";
            txt_totalreceivedqty.Text = "";
            txt_cp.Text = "";
            txt_totalmrp.Text = "";
            txt_mrp.Text = "";
            txt_freeqty.Text = "";
            txt_totalcp.Text = "";
            txt_no0funit.Text = "";
            ddlgroup.Focus();
            //txt_itemname.Focus();
            txt_rateperunit.Text = "";
            ddl_discountype.SelectedIndex = 0;
            txt_discountperqty.Text = "";
            txt_CpberforeTax.Text = "";
            txt_RateafterTax.Text = "";
            txt_mrpperunit.Text = "";
            txt_cgst.Text = "";
            txt_sgst.Text = "";
            txt_igst.Text = "";
            ddl_mfgcompnay.SelectedIndex = 0;
        }
        public List<StockGRNData> GetStockItemList(int curIndex)
        {
            StockGRNData objstock = new StockGRNData();
            StockGRNBO objBO = new StockGRNBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);

            var source2 = txtItemName.Text.ToString();
            if (source2.Contains(":"))
            {
                string ID2 = source2.Substring(source2.LastIndexOf(':') + 1);
                objstock.ItemID = Convert.ToInt32(ID2);
            }
            else
            {
                objstock.ItemID = 0;
                txtItemName.Text = "";
            }
            objstock.IsActive = ddl_Status.SelectedValue == "1" ? true : false;
            DateTime from = txt_recdfrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_recdfrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_recdTo.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_recdTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            objstock.ReceivedBy = Convert.ToInt64(ddl_stockrecievedby.SelectedValue == "" ? "0" : ddl_stockrecievedby.SelectedValue); ;
            return objBO.GetStockItemList(objstock);
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            gvstocklist1.DataSource = null;
            gvstocklist1.DataBind();
            gvstocklist1.Visible = false;
            lblresult.Visible = false;
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage1.Visible = false;
            lblresult.Visible = false;
            divmsg3.Visible = false;
            ddl_purchagetype.SelectedIndex = 0;
            txt_PONo.Text = "";
            txt_receivedno.Text = "";
            txt_recdfrom.Text = "";
            txt_recdTo.Text = "";
            ddl_Status.SelectedIndex = 0;
            //txt_supplier.Text = "";
            ddl_Supplier.SelectedValue = "0";
            txtItemName.Text = "";

        }
        protected void gvstocklist1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    StockGRNData objbill = new StockGRNData();
                    StockGRNBO objstdBO = new StockGRNBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvstocklist1.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    Label ReceiptNo = (Label)gr.Cells[0].FindControl("lbl_recdno");
                    Label PONo = (Label)gr.Cells[0].FindControl("lbl_PONo");
                    Label ItemName = (Label)gr.Cells[0].FindControl("lblitemname");

                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage1, "Remarks", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        divmsg2.Visible = false;
                        objbill.Remarks = txtremarks.Text;
                    }
                    objbill.ID = Convert.ToInt64(ID.Text);
                    objbill.ReceiptNo = ReceiptNo.Text.Trim();
                    objbill.PONo = PONo.Text.Trim();
                    objbill.ItemName = ItemName.Text.Trim();
                    objbill.EmployeeID = LogData.UserLoginId;

                    int Result = objstdBO.DeleteStockItemListByID(objbill);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "delete", 1);
                        lblmessage1.Attributes["class"] = "SucessAlert";
                        divmsg1.Visible = true;
                        bindgrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage1, "system", 0);
                        lblmessage1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
                lblmessage1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<StockGRNData> DepositDetails = GetStockItemList(0);
            List<StockItemDataTOeXCEL> ListexcelData = new List<StockItemDataTOeXCEL>();
            int i = 0;
            foreach (StockGRNData row in DepositDetails)
            {
                StockItemDataTOeXCEL Ecxeclpat = new StockItemDataTOeXCEL();
                Ecxeclpat.ReceiptNo = DepositDetails[i].ReceiptNo;
                Ecxeclpat.PONo = DepositDetails[i].PONo;
                Ecxeclpat.ItemName = DepositDetails[i].ItemName;
                Ecxeclpat.TotalRecdQty = DepositDetails[i].TotalRecdQty;
                Ecxeclpat.FreeQuantity = DepositDetails[i].FreeQuantity;
                Ecxeclpat.CP = Commonfunction.Getrounding(DepositDetails[i].CP.ToString());
                Ecxeclpat.CPperunit = Commonfunction.Getrounding(DepositDetails[i].CPperunit.ToString());
                Ecxeclpat.CPPerQty = Commonfunction.Getrounding(DepositDetails[i].CPPerQty.ToString());
                Ecxeclpat.TotalMRP = Commonfunction.Getrounding(DepositDetails[i].TotalMRP.ToString());
                Ecxeclpat.MRPperUnit = Commonfunction.Getrounding(DepositDetails[i].MRPperUnit.ToString());
                Ecxeclpat.MRPPerQty = Commonfunction.Getrounding(DepositDetails[i].MRPPerQty.ToString());
                Ecxeclpat.TotalIssued = DepositDetails[i].TotalIssued;
                Ecxeclpat.TotalCondemned = DepositDetails[i].TotalCondemned;
                Ecxeclpat.BalStock = DepositDetails[i].BalStock;
                Ecxeclpat.CPRemItem = Commonfunction.Getrounding(DepositDetails[i].CPRemItem.ToString());
                Ecxeclpat.MRPRemItem = Commonfunction.Getrounding(DepositDetails[i].MRPRemItem.ToString());
                Ecxeclpat.MfgDate = DepositDetails[i].MfgDate;
                Ecxeclpat.ExpDate = DepositDetails[i].ExpDate;
                Ecxeclpat.ReceivedDate = DepositDetails[i].ReceivedDate;
                Ecxeclpat.RecdBy = DepositDetails[i].RecdBy;
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
                Messagealert_.ShowMessage(lblmessage1, "ExportEnable", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage1.Visible = false;
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
                    gvstocklist1.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvstocklist1.Columns[21].Visible = false;
                    gvstocklist1.Columns[22].Visible = false;

                    gvstocklist1.RenderControl(hw);
                    gvstocklist1.HeaderRow.Style.Add("width", "15%");
                    gvstocklist1.HeaderRow.Style.Add("font-size", "10px");
                    gvstocklist1.Style.Add("text-decoration", "none");
                    gvstocklist1.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvstocklist1.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=StockDetails.pdf");
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
                wb.Worksheets.Add(dt, "Stock Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=StockDetails.xlsx");
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
        protected void gvstocklist1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvstocklist1.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        protected void ddl_discountype_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_discountperqty.Text = "0";
            txt_cgst.Text = "0";
            txt_sgst.Text = "0";
            txt_igst.Text = "0";
            txt_CpberforeTax.Text = txt_totalcp.Text;
            txt_RateafterTax.Text = txt_totalcp.Text;
            txt_cp.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalcp.Text == "" ? "0" : txt_totalcp.Text) / (Convert.ToDecimal(txt_qty.Text == "" ? "0" : txt_qty.Text) * Convert.ToDecimal(txt_no0funit.Text == "" ? "0" : txt_no0funit.Text))).ToString());
        }

        protected void txtItemName_TextChanged(object sender, EventArgs e)
        {
            bindgrid();
        }


    }
}