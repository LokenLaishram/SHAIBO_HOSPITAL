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
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;

namespace Mediqura.Web.MedStore
{
    public partial class StockReturn : BasePage
    {
        int total = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlbind();
                lblmessage.Visible = false;
            }
        }

        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_handover, mstlookup.GetLookupsList(LookupName.HandOver));
            Commonfunction.PopulateDdl(ddlhandedto, mstlookup.GetLookupsList(LookupName.HandOver));
            Commonfunction.PopulateDdl(ddlretrunby, mstlookup.GetLookupsList(LookupName.ReturnBy));
            btnsave.Attributes["disabled"] = "disabled";
            btnprints.Attributes["disabled"] = "disabled";
            btnprint.Attributes["disabled"] = "disabled";
            txt_totalreturnqty.Text = "0";
            txt_total_returnqty.Text = "0";
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemName(string prefixText, int count, string contextKey)
        {
            StockIssueData Objpaic = new StockIssueData();
            StockIssueBO objInfoBO = new StockIssueBO();
            List<StockIssueData> getResult = new List<StockIssueData>();
            Objpaic.ItemName = prefixText;
            getResult = objInfoBO.GetItemNameWithStockNO(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName);
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetReturnNo(string prefixText, int count, string contextKey)
        {
            StockIssueData Objpaic = new StockIssueData();
            StockIssueBO objInfoBO = new StockIssueBO();
            List<StockIssueData> getResult = new List<StockIssueData>();
            Objpaic.Return = prefixText;
            getResult = objInfoBO.GetReturnNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].Return);
            }
            return list;
        }
        protected void txt_itemnamechange(object sender, EventArgs e)
        {
            AddNewitem();
        }
        protected void Btn_add(object sender, EventArgs e)
        {
            AddNewitem();
        }
        protected void AddNewitem()
        {
            txt_returnNo.Text = "";
            if (txt_itemName.Text == "" || !txt_itemName.Text.Contains(":"))
            {
                Messagealert_.ShowMessage(lblmessage, "ItemName", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_itemName.Text = "";
                txt_itemName.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }

            IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
            List<StockReturnData> StockReturnList = Session["StockReturnList"] == null ? new List<StockReturnData>() : (List<StockReturnData>)Session["StockReturnList"];
            StockReturnData objStock = new StockReturnData();
            StockReturnBO objInfoBO = new StockReturnBO();
            objStock.ItemName = txt_itemName.Text.ToString() == "" ? "" : txt_itemName.Text.ToString();
            var source1 = txt_itemName.Text.ToString();

            if (source1.Contains(":"))
            {
                string ID1 = source1.Substring(source1.LastIndexOf(':') + 1);
                foreach (GridViewRow row in gvstockreturn.Rows)
                {
                    Label StockID = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_stockID");
                    if (Convert.ToInt32(StockID.Text) == Convert.ToInt32(ID1))
                    {
                        Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txt_itemName.Text = "";
                        txt_itemName.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
                List<StockReturnData> Result = new List<StockReturnData>();
                objStock.StockID = Convert.ToInt64(ID1);
                Result = objInfoBO.Getstock_ReturnDetails(objStock);
                if (Result.Count > 0)
                {
                    objStock.StockID = Result[0].StockID;
                    objStock.StockNo = Result[0].StockNo;
                    objStock.SubStockID = Result[0].SubStockID;
                    objStock.ItemID = Result[0].ItemID;
                    objStock.ItemName = Result[0].ItemName;
                    objStock.TotalReceivedQuantity = Result[0].TotalReceivedQuantity;
                    objStock.Available = Result[0].Available;
                    objStock.Return = Result[0].Return;
                }
                else
                {
                    txt_itemName.Text = "";
                    return;
                }
                StockReturnList.Add(objStock);
                if (StockReturnList.Count > 0)
                {
                    gvstockreturn.DataSource = StockReturnList;
                    gvstockreturn.DataBind();
                    gvstockreturn.Visible = true;
                    Session["StockReturnList"] = StockReturnList;
                    txt_itemName.Text = "";
                    txt_itemName.Focus();

                }
                else
                {
                    gvstockreturnlist.DataSource = null;
                    gvstockreturnlist.DataBind();
                    gvstockreturnlist.Visible = true;
                }
            }
        }
        protected void txt_return_TextChanged(object sender, EventArgs e)
        {
            txt_totalreturnqty.Text = "0";
            int Lastindex = gvstockreturn.Rows.Count - 1;
            TextBox txt = sender as TextBox;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            foreach (GridViewRow row in gvstockreturn.Rows)
            {
                TextBox Qty = (TextBox)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("txt_return");
                Label StockID = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_substockID");
                Label Available = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_avail");
                if (Convert.ToInt32(Qty.Text == "" ? "0" : Qty.Text) > Convert.ToInt32(Available.Text == "" ? "0" : Available.Text))
                {
                    Messagealert_.ShowMessage(lblmessage, "ReturnQty", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    Qty.Text = "";
                    Qty.Focus();
                }
                else
                {
                    lblmessage.Visible = false;
                    List<StockReturnData> ItemList = Session["StockReturnList"] == null ? new List<StockReturnData>() : (List<StockReturnData>)Session["StockReturnList"];
                    ItemList[index].Return = Convert.ToInt32(Qty.Text == "" ? "0" : Qty.Text);
                    txt_totalreturnqty.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalreturnqty.Text == "" ? "0" : txt_totalreturnqty.Text) + Convert.ToDecimal(Qty.Text.ToString() == "" ? "0" : Qty.Text.ToString())).ToString());
                    if (Lastindex > row.RowIndex)
                    {
                        TextBox Qty1 = (TextBox)gvstockreturn.Rows[row.RowIndex + 1].Cells[0].FindControl("txt_return");
                        Qty1.Focus();
                    }
                    else if (Lastindex == row.RowIndex)
                    {
                        TextBox Qty2 = (TextBox)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("txt_return");
                        Qty2.Focus();
                    }
                }
            }
            if (Convert.ToInt32(txt_totalreturnqty.Text == "" ? "0" : txt_totalreturnqty.Text) > 0)
            {
                btnsave.Attributes.Remove("disabled");
            }
            else
            {
                btnsave.Attributes["disabled"] = "disabled";
            }
        }
        protected void gvstockreturn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvstockreturn.Rows[i];
                    List<StockReturnData> ItemList = Session["StockReturnList"] == null ? new List<StockReturnData>() : (List<StockReturnData>)Session["StockReturnList"];
                    TextBox RQty = (TextBox)gr.FindControl("txt_return");
                    txt_totalreturnqty.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalreturnqty.Text == "" ? "0" : txt_totalreturnqty.Text) - Convert.ToDecimal(RQty.Text.ToString() == "" ? "0" : RQty.Text.ToString())).ToString());
                    ItemList[i].Return = 0;
                    ItemList.RemoveAt(i);
                    Session["StockReturnList"] = ItemList;
                    gvstockreturn.DataSource = ItemList;
                    gvstockreturn.DataBind();
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
            }
        }
        public List<StockIssueData> GetStockReturnList(int curIndex)
        {
            StockIssueData objstock = new StockIssueData();
            StockIssueBO objBO = new StockIssueBO();
            string ID;
            var source = txt_itemName.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                objstock.StockID = Convert.ToInt64(ID);
            }

            return objBO.GetStockReturnList(objstock);
        }
        protected void gvstockreturnlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton linkdelete = (LinkButton)e.Row.FindControl("lnkDelete");
                if (LogData.DeleteEnable == 0)
                {
                    linkdelete.Attributes["disabled"] = "disabled";
                }
                else
                {
                    linkdelete.Attributes.Remove("disabled");
                }
                if (LogData.PrintEnable == 0)
                {
                    gvstockreturnlist.Columns[7].Visible = false;
                }
                else
                {
                    gvstockreturnlist.Columns[7].Visible = true;
                }
            }

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetEmployeeName(string prefixText, int count, string contextKey)
        {
            EmployeeData Objpaic = new EmployeeData();
            EmployeeBO objInfoBO = new EmployeeBO();
            List<EmployeeData> getResult = new List<EmployeeData>();
            Objpaic.EmpName = prefixText;
            getResult = objInfoBO.GetEmployeeName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmpName);
            }
            return list;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (LogData.SaveEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SaveEnable", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_itemName.Focus();
                return;
            }
            else
            {
                divmsg1.Visible = false;
            }
            List<StockReturnData> ListStock = new List<StockReturnData>();
            StockReturnData objStock = new StockReturnData();
            StockReturnBO objBO = new StockReturnBO();
            try
            {
                // get all the record from the gridview
                // int Check_returnQty = 0;
                foreach (GridViewRow row in gvstockreturn.Rows)
                {
                    Label StockID = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_stockID");
                    Label SubStockID = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_substockID");
                    Label ItemID = (Label)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    TextBox txt_return = (TextBox)gvstockreturn.Rows[row.RowIndex].Cells[0].FindControl("txt_return");

                    StockReturnData ObjDetails = new StockReturnData();
                    ObjDetails.StockID = Convert.ToInt64(StockID.Text == "" ? "0" : StockID.Text);
                    ObjDetails.ItemID = Convert.ToInt64(ItemID.Text == "" ? "0" : ItemID.Text);
                    ObjDetails.SubStockID = Convert.ToInt64(SubStockID.Text == "" ? "0" : SubStockID.Text);
                    ObjDetails.Return = Convert.ToInt32(txt_return.Text == "" ? "0" : txt_return.Text);
                    if (Convert.ToInt32(txt_return.Text == "" ? "0" : txt_return.Text) <= 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ReturnQty", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txt_return.Focus();
                        return;
                    }
                    else
                    {
                        divmsg1.Visible = false;
                        lblmessage.Visible = false;

                    }
                    ListStock.Add(ObjDetails);
                }
                objStock.XMLData = XmlConvertor.StockReturnDetailsDatatoXML(ListStock).ToString();
                objStock.HandOver = Convert.ToInt32(ddl_handover.SelectedValue == "" ? "0" : ddl_handover.SelectedValue);
                objStock.EmployeeID = LogData.EmployeeID;
                objStock.HospitalID = LogData.HospitalID;
                objStock.TotalReturnQty = Convert.ToInt32(txt_totalreturnqty.Text == "" ? "0" : txt_totalreturnqty.Text);
                objStock.FinancialYearID = LogData.FinancialYearID;
                objStock.ActionType = Enumaction.Insert;
                if (ddl_handover.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Handedto", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_handover.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                }

                int result = objBO.UpdateStockReturnDetails(objStock);
                if (result > 0)
                {
                    txt_returnNo.Text = result.ToString();
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    txt_itemName.Text = "";
                    if (LogData.PrintEnable == 0)
                    {
                        btnprint.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btnprint.Attributes.Remove("disabled");
                    }
                    btnsave.Attributes["disabled"] = "disabled";
                    Session["StockReturnList"] = null;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;

            }
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            gvstockreturn.DataSource = null;
            gvstockreturn.DataBind();
            gvstockreturn.Visible = false;
            Session["StockReturnList"] = null;
            lblmessage.Visible = false;
            txt_itemName.Text = "";
            txt_returnNo.Text = "";
            ddl_handover.SelectedIndex = 0;
            divmsg1.Visible = false;
            divmsg3.Visible = false;
            btnsave.Attributes["disabled"] = "disabled";
            btnprint.Attributes["disabled"] = "disabled";
            txt_totalreturnqty.Text = "";

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
                    Messagealert_.ShowMessage(lblmessage1, "SearchEnable", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage1.Visible = false;
                }
                if (txt_retdatefrom.Text == "")
                {
                    if (Commonfunction.isValidDate(txt_retdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_retdatefrom.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                if (txt_returndateTo.Text == "")
                {
                    if (Commonfunction.isValidDate(txt_returndateTo.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_returndateTo.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                List<StockReturnData> objdeposit = GetStockReturnList1(0);
                if (objdeposit.Count > 0)
                {
                    gvstockreturnlist.DataSource = objdeposit;
                    gvstockreturnlist.DataBind();
                    gvstockreturnlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult1, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    div2.Attributes["class"] = "SucessAlert";
                    div2.Visible = true;
                    txt_total_returnqty.Text = Commonfunction.Getrounding(objdeposit[0].TotalReturn.ToString());
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblmessage1.Visible = false;
                    lblmessage1.Visible = false;
                    if (LogData.PrintEnable == 0)
                    {
                        btnprints.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btnprints.Attributes.Remove("disabled");
                    }
                }
                else
                {
                    gvstockreturnlist.DataSource = null;
                    gvstockreturnlist.DataBind();
                    gvstockreturnlist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult.Visible = false;
                    div2.Visible = false;
                    txt_total_returnqty.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div2.Attributes["class"] = "SucessAlert";
                div2.Visible = true;
            }
        }
        public List<StockReturnData> GetStockReturnList1(int curIndex)
        {
            StockReturnData objstock = new StockReturnData();
            StockReturnBO objBO = new StockReturnBO();
            objstock.ReturnNo = txt_returnNum.Text.ToString() == "" ? "" : txt_returnNum.Text.ToString();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_retdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_retdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_returndateTo.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_returndateTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.ReturnByID = Convert.ToInt64(ddlretrunby.SelectedValue == "" ? "0" : ddlretrunby.SelectedValue);
            objstock.HandOver = Convert.ToInt64(ddlhandedto.SelectedValue == "" ? "0" : ddlhandedto.SelectedValue);
            objstock.IsActive = ddlstatus.SelectedValue == "1" ? true : false;
            objstock.DateFrom = from;
            objstock.DateTo = to;
            return objBO.GetStockReturnList(objstock);
        }
        protected void gvstockreturnlist_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        lblmessage.Visible = false;
                    }
                    StockReturnData objbill = new StockReturnData();
                    StockReturnBO objstdBO = new StockReturnBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvstockreturnlist.Rows[i];
                    Label ReturnID = (Label)gr.Cells[0].FindControl("lbl_returnID");
                    Label ReturnNo = (Label)gr.Cells[0].FindControl("lbl_returnno");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage1, "Remarks", 0);
                        div2.Attributes["class"] = "FailAlert";
                        div2.Visible = true;
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objbill.Remarks = txtremarks.Text;
                    }
                    objbill.EmployeeID = LogData.UserLoginId;
                    objbill.ReturnID = Convert.ToInt64(ReturnID.Text == "" ? "0" : ReturnID.Text);
                    objbill.ReturnNo = ReturnNo.Text == "" ? null : ReturnNo.Text.Trim();
                    int Result = objstdBO.DeleteStockReturnItemListByID(objbill);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "delete", 1);
                        divmsg2.Attributes["class"] = "SucessAlert";
                        divmsg2.Visible = true;
                        bindgrid();
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
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<StockReturnData> DepositDetails = GetStockReturnList1(0);
            List<StockItemReturnDataTOeXCEL> ListexcelData = new List<StockItemReturnDataTOeXCEL>();
            int i = 0;
            foreach (StockReturnData row in DepositDetails)
            {
                StockItemReturnDataTOeXCEL Ecxeclpat = new StockItemReturnDataTOeXCEL();
                Ecxeclpat.ReturnNo = DepositDetails[i].ReturnNo;
                Ecxeclpat.TotalReturnQty = DepositDetails[i].TotalReturnQty;
                Ecxeclpat.ReturnDate = DepositDetails[i].ReturnDate;
                Ecxeclpat.ReturnBy = DepositDetails[i].ReturnBy;
                Ecxeclpat.HandedTo = DepositDetails[i].HandedTo;
                ListexcelData.Add(Ecxeclpat);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            gvstockreturnlist.DataSource = null;
            gvstockreturnlist.DataBind();
            gvstockreturnlist.Visible = false;
            lblmessage.Visible = false;
            txt_returnNum.Text = "";
            txt_retdatefrom.Text = "";
            txt_returndateTo.Text = "";
            div2.Visible = false;
            btnprints.Attributes["disabled"] = "disabled";
            ddlretrunby.SelectedIndex = 0;
            ddlhandedto.SelectedIndex = 0;
            ddlstatus.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            txt_total_returnqty.Text = "";
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
                    gvstockreturnlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvstockreturnlist.Columns[4].Visible = false;
                    gvstockreturnlist.Columns[5].Visible = false;

                    gvstockreturnlist.RenderControl(hw);
                    gvstockreturnlist.HeaderRow.Style.Add("width", "15%");
                    gvstockreturnlist.HeaderRow.Style.Add("font-size", "10px");
                    gvstockreturnlist.Style.Add("text-decoration", "none");
                    gvstockreturnlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvstockreturnlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=StockReturnDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=StockReturnDetails.xlsx");
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
        protected void gvstockreturnlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvstockreturnlist.PageIndex = e.NewPageIndex;
            bindgrid();
        }
    }
}