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

namespace Mediqura.Web.MedStore
{
    public partial class StockIssue : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlbind();
                lblmessage.Visible = false;
                txt_total_qty.Text = "0";
                txt_totcp.Text = "0.00";
                txt_totmrp.Text = "0.00";
                Session["StockIssueList"] = null;
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_issueto, mstlookup.GetLookupsList(LookupName.Stocktoissue));
            Commonfunction.PopulateDdl(ddl_stocktype, mstlookup.GetLookupsList(LookupName.Stocktoissue));
            Commonfunction.PopulateDdl(ddl_group, mstlookup.GetLookupsList(LookupName.Groups));
            Commonfunction.PopulateDdl(ddl_handedto, mstlookup.GetLookupsList(LookupName.ItemHandedTo));
            Commonfunction.PopulateDdl(ddlhandedto, mstlookup.GetLookupsList(LookupName.ItemHandedTo));
            Commonfunction.PopulateDdl(ddlissuedby, mstlookup.GetLookupsList(LookupName.IssuedBy));
            Commonfunction.Insertzeroitemindex(ddl_subgroup);
            btnsave.Attributes["disabled"] = "disabled";
            txt_totaquatityissued.Text = "0";
            txt_totalcp_issued.Text = "0.0";
            txt_totalmrp_issued.Text = "0.0";
            btnprints.Attributes["disabled"] = "disabled";
            btnprint.Attributes["disabled"] = "disabled";
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemDetails(string prefixText, int count, string contextKey)
        {
            StockIssueData Objpaic = new StockIssueData();
            StockIssueBO objInfoBO = new StockIssueBO();
            List<StockIssueData> getResult = new List<StockIssueData>();
            Objpaic.ItemName = prefixText;
            Objpaic.GroupID = Convert.ToInt32(contextKey);
            Objpaic.SubGroupID = count;
            getResult = objInfoBO.GetItemtoissueList(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName);
            }
            return list;
        }
        protected void txtservices_TextChanged(object sender, EventArgs e)
        {
            AddNewitem();
        }
        protected void AddNewitem()
        {
            txtIssueNo.Text = "";
            if (ddl_issueto.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Issueto", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_issueto.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            if (txt_indentno.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "IndentNo", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_indentno.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            if (ddl_group.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Group", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_issueto.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            if (ddl_subgroup.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Subgroup", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_issueto.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            if (txtItemName.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "ItemName", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txtItemName.Text = "";
                txtItemName.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }


            IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
            List<StockIssueData> IndentList = Session["StockIssueList"] == null ? new List<StockIssueData>() : (List<StockIssueData>)Session["StockIssueList"];
            StockIssueData objStock = new StockIssueData();
            StockIssueBO objInfoBO = new StockIssueBO();
            objStock.IssueTo = Convert.ToInt16(ddl_issueto.SelectedValue == "" ? "0" : ddl_issueto.SelectedValue);
            objStock.IndentNo = txt_indentno.Text == "" ? "" : txt_indentno.Text;
            objStock.ItemName = txtItemName.Text.ToString() == "" ? "" : txtItemName.Text.ToString();

            var source1 = txtItemName.Text.ToString();
            if (source1.Contains(":"))
            {
                string ID1 = source1.Substring(source1.LastIndexOf(':') + 1);
                foreach (GridViewRow row in gvstockissuelist.Rows)
                {
                    Label StockID = (Label)gvstockissuelist.Rows[row.RowIndex].Cells[0].FindControl("lbl_stockID");
                    if (Convert.ToInt32(StockID.Text) == Convert.ToInt32(ID1))
                    {
                        Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txtItemName.Text = "";
                        txtItemName.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
                List<StockIssueData> Result = new List<StockIssueData>();
                objStock.StockID = Convert.ToInt64(ID1);
                Result = objInfoBO.GetStockPrices(objStock);
                if (Result.Count > 0)
                {
                    objStock.StockID = Result[0].StockID;
                    objStock.ItemID = Result[0].ItemID;
                    objStock.ItemName = Result[0].ItemName;
                    objStock.Unit = Result[0].Unit;
                    objStock.CPPerQty = Result[0].CPPerQty;
                    objStock.MRPperqty = Result[0].MRPperqty;
                    objStock.QtyPerUnit = Result[0].QtyPerUnit;
                    objStock.TotalBalanceQty = Result[0].TotalBalanceQty;
                }
                else
                {
                    txtItemName.Text = "";
                    return;
                }
                IndentList.Add(objStock);
                if (IndentList.Count > 0)
                {
                    gvstockissuelist.DataSource = IndentList;
                    gvstockissuelist.DataBind();
                    gvstockissuelist.Visible = true;
                    Session["StockIssueList"] = IndentList;
                    txtItemName.Text = "";
                    txtItemName.Focus();

                }
                else
                {
                    gvstockissuelist.DataSource = null;
                    gvstockissuelist.DataBind();
                    gvstockissuelist.Visible = true;
                }
            }
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            AddNewitem();
        }
        protected void gvstockissuelist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //int index = Convert.ToInt16(e.Row);
                Label lblSerial = (Label)e.Row.FindControl("lblserialID");
                TextBox Qty = (TextBox)e.Row.FindControl("txtqty");
                lblSerial.Text = ((gvstockissuelist.PageIndex * gvstockissuelist.PageSize) + e.Row.RowIndex + 1).ToString();
                List<StockIssueData> ItemList = Session["StockIssueList"] == null ? new List<StockIssueData>() : (List<StockIssueData>)Session["StockIssueList"];
                //Qty.Text = ItemList[index].IssueQuantity.ToString();
            }
        }


        protected void ddl_itemgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender2.ContextKey = ddl_group.SelectedValue;
            if (ddl_group.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_subgroup, mstlookup.GetItemSubGroupByItemGroupID(Convert.ToInt32(ddl_group.SelectedValue)));
            }
        }
        protected void gvstockissuelist1_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    gvstockissuelist1.Columns[10].Visible = false;
                }
                else
                {
                    gvstockissuelist1.Columns[10].Visible = true;
                }
            }

        }
        protected void txtissueqty_TextChanged(object sender, EventArgs e)
        {
            txt_total_qty.Text = "0";
            txt_totcp.Text = "0";
            txt_totmrp.Text = "0";
            int Lastindex = gvstockissuelist.Rows.Count - 1;
            TextBox txt = sender as TextBox;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            foreach (GridViewRow row in gvstockissuelist.Rows)
            {
                TextBox Qty = (TextBox)gvstockissuelist.Rows[row.RowIndex].Cells[0].FindControl("txtqty");
                // Label hdnqty = (Label)gvstockissuelist.Rows[row.RowIndex].Cells[0].FindControl("lbl_qty");
                Label CP = (Label)gvstockissuelist.Rows[row.RowIndex].Cells[0].FindControl("lblcpperqty");
                Label MRP = (Label)gvstockissuelist.Rows[row.RowIndex].Cells[0].FindControl("lbl_MRP_Qty");
                Label Available = (Label)gvstockissuelist.Rows[row.RowIndex].Cells[0].FindControl("lbl_totalbalqty");
                if (Convert.ToInt32(Qty.Text == "" ? "0" : Qty.Text) > Convert.ToInt32(Available.Text == "" ? "0" : Available.Text))
                {
                    Messagealert_.ShowMessage(lblmessage, "IssueQty", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    Qty.Text = "";
                    Qty.Focus();
                }
                else
                {
                    lblmessage.Visible = false;
                    //hdnqty.Text = Qty.Text; 

                    List<StockIssueData> ItemList = Session["StockIssueList"] == null ? new List<StockIssueData>() : (List<StockIssueData>)Session["StockIssueList"];
                    ItemList[index].IssueQuantity = Convert.ToInt32(Qty.Text == "" ? "0" : Qty.Text);

                    txt_total_qty.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_total_qty.Text == "" ? "0" : txt_total_qty.Text) + Convert.ToDecimal(Qty.Text.ToString() == "" ? "0" : Qty.Text.ToString())).ToString());
                    txt_totcp.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totcp.Text == "" ? "0" : txt_totcp.Text) + Convert.ToDecimal(CP.Text == "" ? "0" : CP.Text) * Convert.ToDecimal(Qty.Text.ToString() == "" ? "0" : Qty.Text.ToString())).ToString());
                    txt_totmrp.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totmrp.Text == "" ? "0" : txt_totmrp.Text) + Convert.ToDecimal(MRP.Text == "" ? "0" : MRP.Text) * Convert.ToDecimal(Qty.Text.ToString() == "" ? "0" : Qty.Text.ToString())).ToString());
                    if (Lastindex > row.RowIndex)
                    {
                        TextBox Qty1 = (TextBox)gvstockissuelist.Rows[row.RowIndex + 1].Cells[0].FindControl("txtqty");
                        Qty1.Focus();
                    }
                    else if (Lastindex == row.RowIndex)
                    {
                        TextBox Qty2 = (TextBox)gvstockissuelist.Rows[row.RowIndex].Cells[0].FindControl("txtqty");
                        Qty2.Focus();
                    }
                }
            }
            if (Convert.ToInt32(txt_total_qty.Text == "" ? "0" : txt_total_qty.Text) > 0)
            {
                btnsave.Attributes.Remove("disabled");
            }
            else
            {
                btnsave.Attributes["disabled"] = "disabled";
            }
        }
        protected void ddl_itemsubgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender2.CompletionSetCount = Convert.ToInt32(ddl_subgroup.SelectedValue == "" ? "0" : ddl_subgroup.SelectedValue);
        }
        protected void gvstockissuelist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {

                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvstockissuelist.Rows[i];
                    List<StockIssueData> ItemList = Session["StockIssueList"] == null ? new List<StockIssueData>() : (List<StockIssueData>)Session["StockIssueList"];

                    TextBox Qty = (TextBox)gr.FindControl("txtqty");
                    Qty.Text = ItemList[i].IssueQuantity.ToString();
                    Label CP = (Label)gr.FindControl("lblcpperqty");
                    Label MRP = (Label)gr.FindControl("lbl_MRP_Qty");

                    txt_total_qty.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_total_qty.Text == "" ? "0" : txt_total_qty.Text) - Convert.ToDecimal(Qty.Text.ToString() == "" ? "0" : Qty.Text.ToString())).ToString());
                    txt_totcp.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totcp.Text == "" ? "0" : txt_totcp.Text) - Convert.ToDecimal(CP.Text == "" ? "0" : CP.Text) * Convert.ToDecimal(Qty.Text.ToString() == "" ? "0" : Qty.Text.ToString())).ToString());
                    txt_totmrp.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totmrp.Text == "" ? "0" : txt_totmrp.Text) - Convert.ToDecimal(MRP.Text == "" ? "0" : MRP.Text) * Convert.ToDecimal(Qty.Text.ToString() == "" ? "0" : Qty.Text.ToString())).ToString());
                    ItemList[i].IssueQuantity = 0;
                    ItemList.RemoveAt(i);

                    Session["StockIssueList"] = ItemList;
                    gvstockissuelist.DataSource = ItemList;
                    gvstockissuelist.DataBind();

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
            if (ddl_issueto.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Issueto", 0);
                lblmessage.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_issueto.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            if (txt_indentno.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "IndentNo", 0);
                lblmessage.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_indentno.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }

            List<StockIssueData> ListStock = new List<StockIssueData>();
            StockIssueData objStock = new StockIssueData();
            StockIssueBO objBO = new StockIssueBO();
            try
            {
                int ItemCount = 0;
                // get all the record from the gridview
                foreach (GridViewRow row in gvstockissuelist.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label ItemID = (Label)gvstockissuelist.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label StockID = (Label)gvstockissuelist.Rows[row.RowIndex].Cells[0].FindControl("lbl_stockID");
                    TextBox IssueQuantity = (TextBox)gvstockissuelist.Rows[row.RowIndex].Cells[0].FindControl("txtqty");
                    StockIssueData ObjDetails = new StockIssueData();
                    ItemCount = ItemCount + 1;
                    ObjDetails.StockID = Convert.ToInt64(StockID.Text == "" ? "0" : StockID.Text);
                    ObjDetails.ItemID = Convert.ToInt64(ItemID.Text == "" ? "0" : ItemID.Text);
                    ObjDetails.IssueQuantity = Convert.ToInt32(IssueQuantity.Text == "" ? "" : IssueQuantity.Text);
                    if (Convert.ToInt32(IssueQuantity.Text == "" ? "" : IssueQuantity.Text) <= 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ZeroQty", 0);
                        lblmessage.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txt_indentno.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        divmsg1.Visible = false;
                    }
                    ListStock.Add(ObjDetails);
                }
                objStock.XMLData = XmlConvertor.StockIssueDetailsDatatoXML(ListStock).ToString();
                objStock.StockTypeID = Convert.ToInt32(ddl_issueto.SelectedValue == "" ? "0" : ddl_issueto.SelectedValue);
                objStock.IndentNo = txt_indentno.Text == "" ? "" : txt_indentno.Text;
                objStock.TotalIssueQuantity = Convert.ToInt32(txt_total_qty.Text == "" ? "0" : txt_total_qty.Text);
                objStock.TotalCP = Convert.ToDecimal(txt_totcp.Text == "" ? "0" : txt_totcp.Text);
                objStock.TotalMRP = Convert.ToDecimal(txt_totmrp.Text == "" ? "0" : txt_totmrp.Text);
                objStock.EmployeeID = LogData.EmployeeID;
                objStock.HospitalID = LogData.HospitalID;
                objStock.FinancialYearID = LogData.FinancialYearID;
                objStock.ActionType = Enumaction.Insert;
                if (ddl_handedto.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Handedto", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_issueto.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    divmsg1.Visible = false;
                    objStock.HandOver = Convert.ToInt64(ddl_handedto.SelectedValue == "" ? "0" : ddl_handedto.SelectedValue);
                }
                if (Convert.ToInt32(txt_total_qty.Text == "" ? "0" : txt_total_qty.Text) > 0)
                {
                    lblmessage.Visible = false;
                    divmsg1.Visible = false;
                    btnsave.Attributes.Remove("disabled");
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "ItemCount", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_issueto.Focus();
                    return;
                }
                int result = objBO.UpdateStockIssueDetails(objStock);
                if (result > 0)
                {
                    lblmessage.Visible = true;
                    txtIssueNo.Text = result.ToString();
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    Session["StockIssueList"] = null;
                    txt_total_qty.Text = "0";
                    txt_totcp.Text = "0.00";
                    txt_totmrp.Text = "0.00";
                    btnsave.Attributes["disabled"] = "disabled";
                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "SucessAlert";
            }
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            lblmessage.Visible = false;
            ddl_issueto.SelectedValue = null;
            txt_indentno.Text = "";
            txtItemName.Text = "";
            txt_total_qty.Text = "";
            txt_totcp.Text = "";
            txt_totmrp.Text = "";
            gvstockissuelist.DataSource = null;
            gvstockissuelist.DataBind();
            gvstockissuelist.Visible = false;
            Session["StockIssueList"] = null;
            btnsave.Attributes["disabled"] = "disabled";
            ddl_group.SelectedIndex = 0;
            ddl_subgroup.SelectedIndex = 0;
            txtIssueNo.Text = "";
            ddl_handedto.SelectedIndex = 0;
            btnprint.Attributes["disabled"] = "disabled";

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIssueNo(string prefixText, int count, string contextKey)
        {
            StockIssueData Objpaic = new StockIssueData();
            StockIssueBO objInfoBO = new StockIssueBO();
            List<StockIssueData> getResult = new List<StockIssueData>();
            Objpaic.IssueNo = prefixText;
            getResult = objInfoBO.GetIssueNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IssueNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIndentNos(string prefixText, int count, string contextKey)
        {
            StockIssueData Objpaic = new StockIssueData();
            StockIssueBO objInfoBO = new StockIssueBO();
            List<StockIssueData> getResult = new List<StockIssueData>();
            Objpaic.IssueNo = prefixText;
            getResult = objInfoBO.GetIndentNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IndentNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemName(string prefixText, int count, string contextKey)
        {
            StockIssueData Objpaic = new StockIssueData();
            StockIssueBO objInfoBO = new StockIssueBO();
            List<StockIssueData> getResult = new List<StockIssueData>();
            Objpaic.ItemName = prefixText;
            getResult = objInfoBO.GetItemName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName);
            }
            return list;
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
                if (txt_datefrom.Text == "")
                {
                    if (Commonfunction.isValidDate(txt_datefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_datefrom.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                if (txt_dateto.Text == "")
                {
                    if (Commonfunction.isValidDate(txt_dateto.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_dateto.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                if (ddl_stocktype.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage1, "StockType", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage1.Visible = false;
                }
                List<StockIssueData> objdeposit = GetStockIsuedList(0);
                if (objdeposit.Count > 0)
                {
                    gvstockissuelist1.DataSource = objdeposit;
                    gvstockissuelist1.DataBind();
                    gvstockissuelist1.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    txt_totaquatityissued.Text = Commonfunction.Getrounding(objdeposit[0].TotalIssuedQty.ToString());
                    txt_totalcp_issued.Text = Commonfunction.Getrounding(objdeposit[0].TotalCostPrice.ToString());
                    txt_totalmrp_issued.Text = Commonfunction.Getrounding(objdeposit[0].TotalMRPS.ToString());
                    divmsg3.Visible = true;
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
                    gvstockissuelist1.DataSource = null;
                    gvstockissuelist1.DataBind();
                    gvstockissuelist1.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult.Visible = false;
                    divmsg3.Visible = false;
                    txt_totaquatityissued.Text = "0";
                    txt_totalcp_issued.Text = "0.0";
                    txt_totalmrp_issued.Text = "0.0";

                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<StockIssueData> GetStockIsuedList(int curIndex)
        {
            StockIssueData objstock = new StockIssueData();
            StockIssueBO objBO = new StockIssueBO();
            objstock.IssueNo = txt_issueNo.Text.ToString() == "" ? "0" : txt_issueNo.Text.ToString();
            objstock.IndentNo = txt_indentnos.Text.ToString() == "" ? null : txt_indentnos.Text.ToString().Trim();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_datefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_datefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_dateto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_dateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            objstock.StockTypeID = Convert.ToInt32(ddl_stocktype.SelectedValue == "" ? "0" : ddl_stocktype.SelectedValue);
            objstock.IssuedByID = Convert.ToInt64(ddlissuedby.SelectedValue == "" ? "0" : ddlissuedby.SelectedValue);
            objstock.HandOver = Convert.ToInt64(ddlhandedto.SelectedValue == "" ? "0" : ddlhandedto.SelectedValue);
            objstock.IsActive = ddlstatus.SelectedValue == "1" ? true : false;
            return objBO.GetStockIssuedList(objstock);
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txt_issueNo.Text = null;
            txt_indentnos.Text = null;
            txtItemName.Text = null;
            txt_datefrom.Text = null;
            txt_dateto.Text = null;
            ddl_stocktype.SelectedIndex = 0;
            ddlexport.SelectedIndex = 0;
            gvstockissuelist1.DataSource = null;
            gvstockissuelist1.DataBind();
            gvstockissuelist1.Visible = false;
            lblresult.Visible = false;
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage1.Visible = false;
            lblresult.Visible = false;
            divmsg2.Visible = false;
            divmsg3.Visible = false;
            txtIssueNo.Text = "";
            ddlissuedby.SelectedIndex = 0;
            ddlhandedto.SelectedIndex = 0;
            ddlstatus.SelectedIndex = 0;
            txt_totaquatityissued.Text = "0";
            txt_totalcp_issued.Text = "0.0";
            txt_totalmrp_issued.Text = "0.0";
            btnprints.Attributes["disabled"] = "disabled";
        }
        protected void gvstockissuelist1_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    StockIssueData objbill = new StockIssueData();
                    StockIssueBO objstdBO = new StockIssueBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvstockissuelist1.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    Label IssueNo = (Label)gr.Cells[0].FindControl("lbl_issueno");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        divmsg3.Visible = true;
                        divmsg3.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objbill.Remarks = txtremarks.Text;
                    }
                    objbill.IssueNo = IssueNo.Text.Trim();
                    objbill.EmployeeID = LogData.UserLoginId;
                    objbill.ID = Convert.ToInt64(ID.Text);
                    int Result = objstdBO.DeleteStockIssuedItemListByID(objbill);
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
                Messagealert_.ShowMessage(lblresult, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<StockIssueData> DepositDetails = GetStockIsuedList(0);
            List<StockItemIssuedDataTOeXCEL> ListexcelData = new List<StockItemIssuedDataTOeXCEL>();
            int i = 0;
            foreach (StockIssueData row in DepositDetails)
            {
                StockItemIssuedDataTOeXCEL Ecxeclpat = new StockItemIssuedDataTOeXCEL();
                Ecxeclpat.IndentNo = DepositDetails[i].IndentNo;
                Ecxeclpat.IssueNo = DepositDetails[i].IssueNo;
                Ecxeclpat.SubStockName = DepositDetails[i].SubStockName;
                Ecxeclpat.TotalQty = DepositDetails[i].TotalQty;
                Ecxeclpat.TotalCP = DepositDetails[i].TotalCP;
                Ecxeclpat.TotalMRP = DepositDetails[i].TotalMRP;
                Ecxeclpat.IssuedBy = DepositDetails[i].IssuedBy;
                Ecxeclpat.HandedTo = DepositDetails[i].HandedTo;
                Ecxeclpat.IssueDate = DepositDetails[i].IssueDate;
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
                    gvstockissuelist1.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvstockissuelist1.Columns[7].Visible = false;
                    gvstockissuelist1.Columns[8].Visible = false;

                    gvstockissuelist1.RenderControl(hw);
                    gvstockissuelist1.HeaderRow.Style.Add("width", "15%");
                    gvstockissuelist1.HeaderRow.Style.Add("font-size", "10px");
                    gvstockissuelist1.Style.Add("text-decoration", "none");
                    gvstockissuelist1.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvstockissuelist1.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=StockIssuedDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=StockIssuedDetails.xlsx");
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
        protected void gvstockissuelist1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvstockissuelist1.PageIndex = e.NewPageIndex;
            bindgrid();
        }
    }
}