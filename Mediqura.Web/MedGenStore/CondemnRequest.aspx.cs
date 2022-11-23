using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedGenStoreBO;
using Mediqura.BOL.MedStore;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedStore;
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

namespace Mediqura.Web.MedGenStore
{
    public partial class CondemnRequest : BasePage
    {
        int Condemntotal;
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
            Commonfunction.PopulateDdl(ddl_substock, mstlookup.GetLookupsList(LookupName.GenStockType));
           // ddl_substock.SelectedValue = LogData.GenSubStockID.ToString();
            Commonfunction.PopulateDdl(ddl_substocks, mstlookup.GetLookupsList(LookupName.GenStockType));
            //ddl_substocks.SelectedValue = LogData.GenSubStockID.ToString();
           // Commonfunction.PopulateDdl(ddl_condemnrequestby, mstlookup.GetGenitemRequestedEmployeeByID(Convert.ToInt32(LogData.GenSubStockID)));
            //if (LogData.RoleID == 1 || LogData.RoleID == 25)
            //{
            //    ddl_substock.Attributes.Remove("disabled");
            //    ddl_substocks.Attributes.Remove("disabled");
            //}
            //else
            //{
            //    ddl_substock.Attributes["disabled"] = "disabled";
            //    ddl_substocks.Attributes["disabled"] = "disabled";
            //}
        }
        protected void ddl_substock_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender2.ContextKey = ddl_substock.SelectedValue;
            txt_itemname.Text = "";
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemName(string prefixText, int count, string contextKey)
        {
            CondemnReqApprovedData Objcondemn = new CondemnReqApprovedData();
            CondemnReqApprovedBO objCondemnBO = new CondemnReqApprovedBO();
            List<CondemnReqApprovedData> getResult = new List<CondemnReqApprovedData>();
            Objcondemn.ItemName = prefixText;
            Objcondemn.SubStockID = Convert.ToInt32(contextKey);
            getResult = objCondemnBO.GetSubStockItemName(Objcondemn);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName.ToString());
            }
            return list;
        }
        protected void txt_itemname_TextChanged(object sender, EventArgs e)
        {
            if (txt_itemname.Text != "" || !txt_itemname.Text.Contains(":"))
            {
                txt_condemnqty.Focus();
                GenIndentData objbill = new GenIndentData();
                GenIndentApprovedBO objstdBO = new GenIndentApprovedBO();
                List<GenIndentData> List = new List<GenIndentData>();
                objbill.StockNo = txt_itemname.Text.ToString().Substring(txt_itemname.Text.ToString().LastIndexOf(':') + 1);
                objbill.SubStockID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
                List = objstdBO.GetCondemnItemDetailsByStockNumbers(objbill);
                txt_Avail.Text = List[0].Totavailable.ToString();
                txt_itemNametoAdd.Text = List[0].ItemName.ToString();
            }
            else
            {
                txt_itemname.Focus();
                txt_itemNametoAdd.Text = "";
            }
        }
        protected void txt_condemnqty_TextChanged(object sender, EventArgs e)
        {
            if (txt_condemnqty.Text != "")
            {
                txt_condemnremarks.Focus();
            }
            else
            {
                txt_condemnqty.Focus();
            }
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (ddl_substock.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "StockType", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_substock.Focus();
                return;
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
            if (txt_condemnqty.Text == "" || txt_condemnqty.Text == "0")
            {
                Messagealert_.ShowMessage(lblmessage, "StockQty", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_condemnqty.Focus();
                return;
            }
            if (txt_condemnremarks.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Remarks", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_condemnremarks.Focus();
                return;
            }
            if (Convert.ToInt32(txt_condemnqty.Text == "" ? "0" : txt_condemnqty.Text) > Convert.ToInt32(txt_Avail.Text == "" ? "0" : txt_Avail.Text))
            {
                Messagealert_.ShowMessage(lblmessage, "CondemnOver", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_condemnqty.Focus();
                return;
            }
            IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
            List<CondemnReqApprovedData> StockItemList = Session["CondemnItemList"] == null ? new List<CondemnReqApprovedData>() : (List<CondemnReqApprovedData>)Session["CondemnItemList"];
            CondemnReqApprovedData objCondemnSubStock = new CondemnReqApprovedData();
            objCondemnSubStock.SubStockID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
            objCondemnSubStock.CondemnQty = Convert.ToInt32(txt_condemnqty.Text.ToString() == "" ? "" : txt_condemnqty.Text.ToString());
            objCondemnSubStock.ItemName = txt_itemNametoAdd.Text.ToString() == "" ? "" : txt_itemNametoAdd.Text.ToString();
            objCondemnSubStock.CondemnRemark = txt_condemnremarks.Text.ToString() == "" ? "" : txt_condemnremarks.Text.ToString();
            string Stocknos, ItemID;
            var source = txt_itemname.Text.ToString();
            if (source.Contains(":"))
            {
                Stocknos = source.Substring(source.LastIndexOf(':') + 1);
                // Check Duplicate data 
                foreach (GridViewRow row in gvsubstocklist.Rows)
                {
                    Label StockNo = (Label)gvsubstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_stockNo");
                    if (Stocknos == StockNo.Text)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);    
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txt_itemname.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
                objCondemnSubStock.StockNo = Stocknos;
            }
            else
            {
                txt_itemname.Text = "";
                return;
            }
            ItemID = source.Split('#', '|')[1];
            objCondemnSubStock.ItemID = Convert.ToInt32(ItemID == "" ? "0" : ItemID);
            StockItemList.Add(objCondemnSubStock);

            if (StockItemList.Count > 0)
            {
                gvsubstocklist.DataSource = StockItemList;
                gvsubstocklist.DataBind();
                gvsubstocklist.Visible = true;
                Session["CondemnItemList"] = StockItemList;
                clearall();
                txt_itemname.Focus();
                btnsave.Attributes.Remove("disabled");
                TotalSum();
            }
            else
            {
                gvsubstocklist.DataSource = null;
                gvsubstocklist.DataBind();
                gvsubstocklist.Visible = true;
            }
        }
        //Sum of the gridview
        protected void TotalSum()
        {
            int qtytotal = 0;
            foreach (GridViewRow gvr in gvsubstocklist.Rows)
            {
                Label qty = (Label)gvr.Cells[0].FindControl("lbl_condemnqty");
                qtytotal = qtytotal + Convert.ToInt32(qty.Text.Trim());

            }
            txt_totalCondemnqty.Text = qtytotal.ToString();
        }
        protected void clearall()
        {

            txt_itemname.Text = "";
            txt_condemnqty.Text = "";
            txt_condemnremarks.Text = "";
            txt_Avail.Text = "";
            txt_itemNametoAdd.Text = "";
        }
        protected void gvsubstocklistt_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvsubstocklist.Rows[i];
                    List<CondemnReqApprovedData> ItemList = Session["CondemnItemList"] == null ? new List<CondemnReqApprovedData>() : (List<CondemnReqApprovedData>)Session["CondemnItemList"];
                    ItemList.RemoveAt(i);
                    Label qty = (Label)gr.Cells[0].FindControl("lbl_condemnqty");
                    txt_totalCondemnqty.Text = (Convert.ToInt32(txt_totalCondemnqty.Text == "" ? "0" : txt_totalCondemnqty.Text) - Convert.ToInt32(qty.Text == "" ? "0" : qty.Text)).ToString();
                    Session["StockItemList"] = ItemList;
                    gvsubstocklist.DataSource = ItemList;
                    gvsubstocklist.DataBind();
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
            List<CondemnReqApprovedData> CondemnListStock = new List<CondemnReqApprovedData>();
            CondemnReqApprovedData objStock = new CondemnReqApprovedData();
            CondemnReqApprovedBO objBO = new CondemnReqApprovedBO();

            try
            {
                // get all the record from the gridview
                int itemcount = 0;
                foreach (GridViewRow row in gvsubstocklist.Rows)
                {
                    Label CondemnItemID = (Label)gvsubstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label SubstockID = (Label)gvsubstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_SubstockID");
                    Label StockNumber = (Label)gvsubstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_stockNo");
                    Label CondemnQty = (Label)gvsubstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_condemnqty");
                    Label CondemnRemark = (Label)gvsubstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_condemnremark");
                    CondemnReqApprovedData ObjDetails = new CondemnReqApprovedData();
                    ObjDetails.ItemID = Convert.ToInt32(CondemnItemID.Text == "" ? "0" : CondemnItemID.Text);
                    ObjDetails.SubStockID = Convert.ToInt32(SubstockID.Text == "" ? "0" : SubstockID.Text);
                    ObjDetails.CondemnQty = Convert.ToInt32(CondemnQty.Text == "" ? "0" : CondemnQty.Text);
                    ObjDetails.StockNo = StockNumber.Text.Trim();
                    ObjDetails.CondemnRemark = CondemnRemark.Text;
                    itemcount = itemcount + 1;
                    CondemnListStock.Add(ObjDetails);
                }
                objStock.XMLData = XmlConvertor.CondemnStockDetailsDatatoXML(CondemnListStock).ToString();
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
                objStock.SubStockID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
                objStock.TotalCondemnQty = Convert.ToInt32(txt_totalCondemnqty.Text.Trim() == "" ? "0" : txt_totalCondemnqty.Text.Trim());
                objStock.HospitalID = LogData.HospitalID;
                objStock.EmployeeID = LogData.EmployeeID;
                objStock.FinancialYearID = LogData.FinancialYearID;
                objStock.ActionType = Enumaction.Insert;
                List<CondemnReqApprovedData> result = objBO.UpdateCondemnStockItemDetails(objStock);
                if (result.Count > 0)
                {
                    txt_CondemnRegNo.Text = result[0].CondemnRequestNo.ToString();
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    btnsave.Attributes["disabled"] = "disabled";
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
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txt_itemname.Text = "";
            txt_condemnqty.Text = "";
            gvsubstocklist.DataSource = null;
            gvsubstocklist.DataBind();
            gvsubstocklist.Visible = false;
            ViewState["ID"] = null;
            lblmessage.Visible = false;
            Session["CondemnItemList"] = null;
            txt_CondemnRegNo.Text = "";
            txt_totalCondemnqty.Text = "";
            txt_Avail.Text = "";
            txt_condemnremarks.Text = "";
            ddl_substock.SelectedIndex = 0;
        }
        //-------------- TAB 2 SEARCH -----------------

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemNames(string prefixText, int count, string contextKey)
        {
            CondemnReqApprovedData Objcondemn = new CondemnReqApprovedData();
            CondemnReqApprovedBO objCondemnBO = new CondemnReqApprovedBO();
            List<CondemnReqApprovedData> getResult = new List<CondemnReqApprovedData>();
            Objcondemn.ItemName = prefixText;
            Objcondemn.SubStockID = Convert.ToInt32(contextKey);
            getResult = objCondemnBO.GetSubStockItemName(Objcondemn);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName.ToString());
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
                if (ddl_substocks.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage1, "GenStock", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage1.Visible = false;
                }
                List<CondemnReqApprovedData> objcondemn = GetCondemnStockItemList(0);
                if (objcondemn.Count > 0)
                {
                    gvcondemnsubstocklist.DataSource = objcondemn;
                    gvcondemnsubstocklist.DataBind();
                    gvcondemnsubstocklist.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objcondemn[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblmessage1.Visible = false;
                    lblmessage1.Visible = false;
                }
                else
                {
                    gvcondemnsubstocklist.DataSource = null;
                    gvcondemnsubstocklist.DataBind();
                    gvcondemnsubstocklist.Visible = true;
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
        public List<CondemnReqApprovedData> GetCondemnStockItemList(int curIndex)
        {
            CondemnReqApprovedData objcondemnstock = new CondemnReqApprovedData();
            CondemnReqApprovedBO objBO = new CondemnReqApprovedBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objcondemnstock.CondemnRequestNo = txt_condemnrequestno.Text.Trim().ToString() == "" ? "" : txt_condemnrequestno.Text.Trim().ToString();
            objcondemnstock.SubStockID = Convert.ToInt32(ddl_substocks.SelectedValue == "" ? "0" : ddl_substocks.SelectedValue);
            DateTime from = txt_requestfrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_requestfrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_requestTo.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_requestTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objcondemnstock.DateFrom = from;
            objcondemnstock.DateTo = to;
            objcondemnstock.CondemnRequestBy = Convert.ToInt32(ddl_condemnrequestby.SelectedValue == "" ? "0" : ddl_condemnrequestby.SelectedValue);
            objcondemnstock.CondemnStatus = Convert.ToInt32(ddlcondemnStatus.SelectedValue == "" ? "0" : ddlcondemnStatus.SelectedValue);
            objcondemnstock.IsActive = ddl_Status.SelectedValue == "1" ? true : false;
            return objBO.GetCondemnStockItemList(objcondemnstock);
        }
        protected void gvcondemnsubstocklist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    CondemnReqApprovedData objcondemn = new CondemnReqApprovedData();
                    CondemnReqApprovedBO objstkcondemnBO = new CondemnReqApprovedBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvcondemnsubstocklist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    Label requestno = (Label)gr.Cells[0].FindControl("lbl_requestno");
                    TextBox Remark = (TextBox)gr.Cells[0].FindControl("lbl_condemremark");
                    if (Remark.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        divmsg3.Visible = true;
                        divmsg3.Attributes["class"] = "FailAlert";
                        Remark.Focus();
                        return;
                    }
                    else
                    {
                        objcondemn.Remarks = Remark.Text;
                    }
                    objcondemn.ID = Convert.ToInt64(ID.Text);
                    objcondemn.CondemnRequestNo = requestno.Text;
                    int Result = objstkcondemnBO.DeleteCondemnStockItemListByID(objcondemn);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "delete", 1);
                        lblmessage1.Attributes["class"] = "SucessAlert";
                        divmsg1.Visible = true;
                        bindgrid();
                    }
                    else if (Result == 7)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "This condemn request has been approved, it cannot be delete. ", 0);
                        lblmessage1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
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

        protected void btnreset_Click(object sender, EventArgs e)
        {
            gvcondemnsubstocklist.DataSource = null;
            gvcondemnsubstocklist.DataBind();
            gvcondemnsubstocklist.Visible = false;
            lblresult.Visible = false;
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage1.Visible = false;
            lblresult.Visible = false;
            divmsg3.Visible = false;
            ddl_substocks.SelectedIndex = 0;
            ddl_condemnrequestby.SelectedIndex = 0;
            txt_condemnrequestno.Text = "";
            txt_requestfrom.Text = "";
            txt_requestTo.Text = "";
        }
    }
}