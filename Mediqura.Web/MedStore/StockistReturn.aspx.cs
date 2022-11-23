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


namespace Mediqura.Web.MedStore
{
    public partial class StockistReturn : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txttotalquantity.Text = "0";
                txt_totalcp.Text = "0.00";
                Session["IndentList"] = null;
                btnprint.Attributes["disabled"] = "disabled";

            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetSupplierName(string prefixText, int count, string contextKey)
        {
            StockGRNData Objpaic = new StockGRNData();
            StockGRNBO objInfoBO = new StockGRNBO();
            List<StockGRNData> getResult = new List<StockGRNData>();
            Objpaic.SupplierName = prefixText;
            getResult = objInfoBO.GetSupplier(Objpaic);
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
            getResult = objInfoBO.Get_ItemName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName.ToString());
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
        protected void txt_itemname_TextChanged(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void txt_SupplierName_TextChanged(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void bindgrid()
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
                    lblmessage1.Visible = false;
                }

                if (txt_recdfrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_recdfrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please enter valid from date.", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txt_recdfrom.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                if (txt_recdTo.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_recdTo.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please enter valid to date.", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txt_recdTo.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                List<StockGRNData> objdeposit = GetStockistItemList(0);
                if (objdeposit.Count > 0)
                {
                    gvstockistreturn1.DataSource = objdeposit;
                    gvstockistreturn1.DataBind();
                    gvstockistreturn1.Visible = true;
                    btnadd.Visible = true;
                }
                else
                {
                    gvstockistreturn1.DataSource = null;
                    gvstockistreturn1.DataBind();
                    gvstockistreturn1.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }
        public List<StockGRNData> GetStockistItemList(int curIndex)
        {
            StockGRNData objstock = new StockGRNData();
            StockGRNBO objBO = new StockGRNBO();
            objstock.Supplier = txt_SupplierName.Text.ToString() == "" ? "" : txt_SupplierName.Text.ToString(); ;
            objstock.PONo = txtPONo.Text.ToString() == "" ? "" : txtPONo.Text.ToString();
            objstock.ItemName = txt_itemName.Text.ToString() == "" ? "" : txt_itemName.Text.ToString();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_recdfrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_recdfrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_recdTo.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_recdTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            objstock.ReceivedBy = LogData.EmployeeID;
            return objBO.GetStockistItemList(objstock);
        }
        protected void gvstockistreturn1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in gvstockistreturn1.Rows)
            {
                CheckBox cb = (CheckBox)gvstockistreturn1.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                cb.Checked = false;
            }

        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvstockistreturn1.Rows)
            {
                IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                List<StockGRNData> IndentList = Session["IndentList"] == null ? new List<StockGRNData>() : (List<StockGRNData>)Session["IndentList"];

                CheckBox cb = (CheckBox)gvstockistreturn1.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                if (cb.Checked)
                {
                    Label ID = (Label)gvstockistreturn1.Rows[row.RowIndex].Cells[0].FindControl("lbl_stockID");
                    Label ReceiptQty = (Label)gvstockistreturn1.Rows[row.RowIndex].Cells[0].FindControl("lbl_totalrecd");
                    Label CPperQty = (Label)gvstockistreturn1.Rows[row.RowIndex].Cells[0].FindControl("lbl_cpperqty");
                    Label ItemName = (Label)gvstockistreturn1.Rows[row.RowIndex].Cells[0].FindControl("lblitemname");
                    Label ItemID = (Label)gvstockistreturn1.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label Tax = (Label)gvstockistreturn1.Rows[row.RowIndex].Cells[0].FindControl("lbl_tax");
                    Label CP = (Label)gvstockistreturn1.Rows[row.RowIndex].Cells[0].FindControl("lbl_cp");
                    Label SupplierID = (Label)gvstockistreturn1.Rows[row.RowIndex].Cells[0].FindControl("lbl_supplier");
                    Label PO = (Label)gvstockistreturn1.Rows[row.RowIndex].Cells[0].FindControl("lblpo");
                    Label Avail = (Label)gvstockistreturn1.Rows[row.RowIndex].Cells[0].FindControl("lbl_avail");

                    StockGRNData obj = new StockGRNData();
                    obj.StockID = Convert.ToInt32(ID.Text);
                    obj.TotalRecdQty = Convert.ToInt32(ReceiptQty.Text);
                    obj.CPPerQty = Convert.ToDecimal(CPperQty.Text);
                    obj.ItemName = (ItemName.Text);
                    obj.ItemID = Convert.ToInt32(ItemID.Text);
                    obj.Tax = Convert.ToDouble(Tax.Text);
                    obj.CP = Convert.ToDecimal(CPperQty.Text) * Convert.ToInt32(Avail.Text);
                    obj.SupplierID = Convert.ToInt32(SupplierID.Text);
                    obj.PONo = PO.Text;
                    obj.BalStock = Convert.ToInt32(Avail.Text);
                    txttotalquantity.Text = (Convert.ToInt32(txttotalquantity.Text == "" ? "0" : txttotalquantity.Text) + Convert.ToInt32(obj.BalStock)).ToString();
                    txt_totalcp.Text = (Convert.ToDecimal(txt_totalcp.Text == "" ? "0" : txt_totalcp.Text) + Convert.ToDecimal(obj.CP)).ToString();

                    foreach (GridViewRow row1 in gvstockistreturn2.Rows)
                    {
                        Label Item_ID = (Label)gvstockistreturn2.Rows[row1.RowIndex].Cells[0].FindControl("lbl_ItemID");
                        if (Convert.ToInt32(Item_ID.Text) == Convert.ToInt32(obj.ItemID))
                        {
                            Messagealert_.ShowMessage(lblmessage, "Already added to the list", 0);
                            divmsg1.Visible = true;
                            return;
                        }
                        else
                        {
                            lblmessage.Visible = false;
                        }
                    }
                    IndentList.Add(obj);
                }
                if (IndentList.Count > 0)
                {
                    gvstockistreturn2.DataSource = IndentList;
                    gvstockistreturn2.DataBind();
                    gvstockistreturn2.Visible = true;
                    Session["IndentList"] = IndentList;
                    cb.Checked = false;

                }
                else
                {
                    gvstockistreturn2.DataSource = null;
                    gvstockistreturn2.DataBind();
                    gvstockistreturn2.Visible = true;

                }
            }

        }
        protected void gvstockistreturn2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvstockistreturn2.Rows[i];
                    List<StockGRNData> IndentList = Session["IndentList"] == null ? new List<StockGRNData>() : (List<StockGRNData>)Session["IndentList"];
                    Label ID = (Label)gvstockistreturn2.Rows[i].Cells[0].FindControl("lbl_stockID");
                    TextBox ReceiptQty = (TextBox)gvstockistreturn2.Rows[i].Cells[0].FindControl("txt_totalqty");
                    Label CPperQty = (Label)gvstockistreturn2.Rows[i].Cells[0].FindControl("lbl_cpperqty1");
                    Label ItemName = (Label)gvstockistreturn2.Rows[i].Cells[0].FindControl("lblitemname");
                    Label Tax = (Label)gvstockistreturn2.Rows[i].Cells[0].FindControl("lbl_tax1");
                    TextBox CP = (TextBox)gvstockistreturn2.Rows[i].Cells[0].FindControl("txt_totalprice");
                    Label Avail = (Label)gvstockistreturn2.Rows[i].Cells[0].FindControl("lbl_avail");

                    StockGRNData obj = new StockGRNData();
                    obj.StockID = Convert.ToInt32(ID.Text);
                    obj.BalStock = Convert.ToInt32(Avail.Text);
                    obj.CPPerQty = Convert.ToDecimal(CPperQty.Text);
                    obj.ItemName = (ItemName.Text);
                    obj.Tax = Convert.ToDouble(Tax.Text);
                    obj.CP = Convert.ToDecimal(CP.Text);

                    IndentList.RemoveAt(i);
                    txttotalquantity.Text = (Convert.ToInt32(txttotalquantity.Text == "" ? "0" : txttotalquantity.Text) - Convert.ToInt32(obj.BalStock)).ToString();
                    txt_totalcp.Text = (Convert.ToDecimal(txt_totalcp.Text == "" ? "0" : txt_totalcp.Text) - Convert.ToDecimal(obj.CP)).ToString();

                    Session["IndentList"] = IndentList;
                    gvstockistreturn2.DataSource = IndentList;
                    gvstockistreturn2.DataBind();
                    gvstockistreturn2.Visible = true;
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
        protected void txt_totalqty_TextChanged(object sender, EventArgs e)
        {
            txttotalquantity.Text = "0";
            txt_totalcp.Text = "0.00";
            foreach (GridViewRow row in gvstockistreturn2.Rows)
            {
                TextBox ReceiptQty = (TextBox)gvstockistreturn2.Rows[row.RowIndex].Cells[0].FindControl("txt_totalqty");
                Label CPperQty = (Label)gvstockistreturn2.Rows[row.RowIndex].Cells[0].FindControl("lbl_cpperqty1");
                TextBox CP = (TextBox)gvstockistreturn2.Rows[row.RowIndex].Cells[0].FindControl("txt_totalprice");
                Label Avail = (Label)gvstockistreturn2.Rows[row.RowIndex].Cells[0].FindControl("lbl_avail");
                if (Convert.ToInt32(Avail.Text) >= Convert.ToInt32(ReceiptQty.Text))
                {
                    CP.Text = Commonfunction.Getrounding((Convert.ToInt32(ReceiptQty.Text == "" ? "0" : ReceiptQty.Text) * Convert.ToDecimal(CPperQty.Text == "" ? "0" : CPperQty.Text)).ToString());
                    txttotalquantity.Text = (Convert.ToInt32(txttotalquantity.Text == "" ? "0" : txttotalquantity.Text) + Convert.ToInt32(ReceiptQty.Text)).ToString();
                    txt_totalcp.Text = (Convert.ToDecimal(txt_totalcp.Text == "" ? "0" : txt_totalcp.Text) + Convert.ToDecimal(CP.Text)).ToString();

                }
                else
                {
                    ReceiptQty.Text = (Convert.ToInt32(Avail.Text)).ToString();
                    CP.Text = Commonfunction.Getrounding((Convert.ToInt32(ReceiptQty.Text == "" ? "0" : ReceiptQty.Text) * Convert.ToDecimal(CPperQty.Text == "" ? "0" : CPperQty.Text)).ToString());
                    txttotalquantity.Text = (Convert.ToInt32(txttotalquantity.Text == "" ? "0" : txttotalquantity.Text) + Convert.ToInt32(ReceiptQty.Text)).ToString();
                    txt_totalcp.Text = (Convert.ToDecimal(txt_totalcp.Text == "" ? "0" : txt_totalcp.Text) + Convert.ToDecimal(CP.Text)).ToString();
                    Messagealert_.ShowMessage(lblmessage, "ReturnQty", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ReceiptQty.Focus();
                }


            }

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
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
            List<StockGRNData> List = new List<StockGRNData>();
            StockGRNBO objBO = new StockGRNBO();
            StockGRNData objrec = new StockGRNData();
            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in gvstockistreturn2.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label ID = (Label)gvstockistreturn2.Rows[row.RowIndex].Cells[0].FindControl("lbl_stockID");
                    TextBox ReceiptQty = (TextBox)gvstockistreturn2.Rows[row.RowIndex].Cells[0].FindControl("txt_totalqty");
                    Label CPperQty = (Label)gvstockistreturn2.Rows[row.RowIndex].Cells[0].FindControl("lbl_cpperqty1");
                    Label ItemID = (Label)gvstockistreturn2.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label Tax = (Label)gvstockistreturn2.Rows[row.RowIndex].Cells[0].FindControl("lbl_tax1");
                    TextBox CP = (TextBox)gvstockistreturn2.Rows[row.RowIndex].Cells[0].FindControl("txt_totalprice");
                    Label SupplierID = (Label)gvstockistreturn2.Rows[row.RowIndex].Cells[0].FindControl("lbl_supplier");
                    Label PO = (Label)gvstockistreturn2.Rows[row.RowIndex].Cells[0].FindControl("lbl_PO");
                    Label Avail = (Label)gvstockistreturn2.Rows[row.RowIndex].Cells[0].FindControl("lbl_avail");

                    if (Convert.ToInt32(Avail.Text) >= Convert.ToInt32(ReceiptQty.Text))
                    {
                        Messagealert_.ShowMessage(lblmessage, "ReturnQty", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        ReceiptQty.Focus();
                    }
                  
                    StockGRNData Obj = new StockGRNData();

                    StockGRNData obj = new StockGRNData();
                    obj.StockID = Convert.ToInt32(ID.Text);
                    obj.TotalRecdQty = Convert.ToInt32(ReceiptQty.Text);
                    obj.CPPerQty = Convert.ToDecimal(CPperQty.Text);
                    obj.ItemID = Convert.ToInt32(ItemID.Text);
                    obj.Tax = Convert.ToDouble(Tax.Text);
                    obj.CP = Convert.ToDecimal(CP.Text);
                    obj.SupplierID = Convert.ToInt32(SupplierID.Text);
                    obj.PONo = PO.Text;

                    List.Add(obj);
                }
                objrec.XMLData = XmlConvertor.StockistItemRecordDatatoXML(List).ToString();
                objrec.TotalQuantity = Convert.ToInt32(txttotalquantity.Text == "" ? "0" : txttotalquantity.Text);
                foreach (GridViewRow row in gvstockistreturn2.Rows)
                {
                    Label SupplierID = (Label)gvstockistreturn2.Rows[row.RowIndex].Cells[0].FindControl("lbl_supplier");
                    objrec.SupplierID = Convert.ToInt32(SupplierID.Text);
                }
                objrec.CP = Convert.ToDecimal(txt_totalcp.Text == "" ? "0.00" : txt_totalcp.Text);
                objrec.FinancialYearID = LogData.FinancialYearID;
                objrec.EmployeeID = LogData.EmployeeID;
                objrec.HospitalID = LogData.HospitalID;
                objrec.IPaddress = LogData.IPaddress;
                objrec.ActionType = Enumaction.Insert;

                int result = objBO.UpdateStockistReturn(objrec);
                if (result > 0)
                {
                    txt_returnNo.Text = result.ToString();
                    Messagealert_.ShowMessage(lblmessage, "return", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    gvstockistreturn2.DataSource = null;
                    gvstockistreturn2.DataBind();
                    gvstockistreturn2.Visible = false;
                    Session["IndentList"] = null;
                    btnprint.Attributes.Remove("disabled");

                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
            }
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txt_SupplierName.Text = "";
            txt_itemName.Text = "";
            txt_returnNo.Text = "";
            txtPONo.Text = "";
            txt_recdfrom.Text = "";
            txt_recdTo.Text = "";
            txttotalquantity.Text = "";
            txt_totalcp.Text = "";
            Session["IndentList"] = null;
            gvstockistreturn2.DataSource = null;
            gvstockistreturn2.DataBind();
            gvstockistreturn2.Visible = false;
            gvstockistreturn1.DataSource = null;
            gvstockistreturn1.DataBind();
            gvstockistreturn1.Visible = false;
            lblmessage.Visible = false;
            divmsg1.Visible = false;
            btnprint.Attributes["disabled"] = "disabled";
            btnadd.Visible = false;

        }
        protected void btnsearch1_Click(object sender, EventArgs e)
        {
            bindgrid1();
        }
        protected void txtitemName_TextChanged(object sender, EventArgs e)
        {
            bindgrid1();
        }
        protected void txtSupplierName_TextChanged(object sender, EventArgs e)
        {
            bindgrid1();
        }
        protected void bindgrid1()
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
                    divmsg2.Visible = false;
                }

                if (txt_retdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_retdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "ValidDatefrom", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_recdfrom.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                if (txt_recdTo.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_recdTo.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "ValidDateto", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_recdTo.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                List<StockGRNData> objdeposit = GetStockistItemReturnList(0);
                if (objdeposit.Count > 0)
                {
                    divmsg2.Visible = true;
                    gvstockistreturnlist.DataSource = objdeposit;
                    gvstockistreturnlist.DataBind();
                    gvstockistreturnlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult1, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found", 1);
                    div2.Attributes["class"] = "SucessAlert";
                    div2.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                  
                }
                else
                {
                    gvstockistreturnlist.DataSource = null;
                    gvstockistreturnlist.DataBind();
                    gvstockistreturnlist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<StockGRNData> GetStockistItemReturnList(int curIndex)
        {
            StockGRNData objstock = new StockGRNData();
            StockGRNBO objBO = new StockGRNBO();
            objstock.Supplier = txtSupplierName.Text.ToString() == "" ? "" : txtSupplierName.Text.ToString();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_retdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_retdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_retdateTo.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_retdateTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            return objBO.GetStockistItemReturnList(objstock);
        }
        protected void gvstockistreturnlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "DeleteEnable", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage1.Visible = false;
                    }
                    StockGRNData objbill = new StockGRNData();
                    StockGRNBO objstdBO = new StockGRNBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvstockistreturnlist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    Label ReturnNo = (Label)gr.Cells[0].FindControl("lbl_returnno");
                    Label StockID = (Label)gr.Cells[0].FindControl("lbl_StockID");
                    Label NoReturn = (Label)gr.Cells[0].FindControl("lbl_totalreturn");
                    Label CP = (Label)gr.Cells[0].FindControl("lbl_cp");


                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult1, "Remarks", 0);
                        div2.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objbill.Remarks = txtremarks.Text;
                    }
                    objbill.ID = Convert.ToInt64(ID.Text);
                    objbill.ReturnNo = ReturnNo.Text.Trim();
                    objbill.StockID = Convert.ToInt64(StockID.Text);
                    objbill.NoReturn = Convert.ToInt32(NoReturn.Text);
                    objbill.CP = Convert.ToDecimal(CP.Text);

                  
                    objbill.EmployeeID = LogData.UserLoginId;

                    int Result = objstdBO.DeleteStockistReturnItemListByID(objbill);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "delete", 1);
                        divmsg2.Attributes["class"] = "SucessAlert";
                        divmsg2.Visible = true;
                        bindgrid1();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage1, "system", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                    }

                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage1, "system", 0);
            }
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txtSupplierName.Text = "";
            txt_retdatefrom.Text = "";
            txt_retdateTo.Text = "";
            gvstockistreturnlist.DataSource = null;
            gvstockistreturnlist.DataBind();
            gvstockistreturnlist.Visible = false;
            lblmessage1.Visible = false;
            divmsg2.Visible = false;
            lblresult1.Visible = false;
            div2.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            btnadd.Visible = false;

        }
        protected DataTable GetDatafromDatabase()
        {
            List<StockGRNData> DepositDetails = GetStockistItemReturnList(0);
            List<StockistItemDataTOeXCEL> ListexcelData = new List<StockistItemDataTOeXCEL>();
            int i = 0;
            foreach (StockGRNData row in DepositDetails)
            {
                StockistItemDataTOeXCEL Ecxeclpat = new StockistItemDataTOeXCEL();
                Ecxeclpat.ReturnNo = DepositDetails[i].ReturnNo;
                Ecxeclpat.NoReturn = DepositDetails[i].NoReturn;
                Ecxeclpat.CPPerQty = DepositDetails[i].CPPerQty;
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
                Messagealert_.ShowMessage(lblresult1, "ExportType", 0);
                divmsg2.Attributes["class"] = "FailAlert";
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
                    gvstockistreturnlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvstockistreturnlist.Columns[6].Visible = false;
                    gvstockistreturnlist.Columns[7].Visible = false;

                    gvstockistreturnlist.RenderControl(hw);
                    gvstockistreturnlist.HeaderRow.Style.Add("width", "15%");
                    gvstockistreturnlist.HeaderRow.Style.Add("font-size", "10px");
                    gvstockistreturnlist.Style.Add("text-decoration", "none");
                    gvstockistreturnlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvstockistreturnlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=StockistReturn.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=StockistReturnDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblresult1, "Exported", 1);
                divmsg2.Attributes["class"] = "SucessAlert";
            }
        }
        protected void gvstockistreturnlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvstockistreturnlist.PageIndex = e.NewPageIndex;
            bindgrid1();
        }
    }

}





