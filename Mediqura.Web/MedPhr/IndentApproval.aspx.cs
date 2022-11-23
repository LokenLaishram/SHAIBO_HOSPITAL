using Mediqura.BOL.CommonBO;
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

namespace Mediqura.Web.MedPhr
{
    public partial class IndentApproval : BasePage
    {
		public static string Indentno;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                Session["AvailList"] = null;
                txt_tolappqty.Text = "0";
				txt_from.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
				txt_To.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                bindIndentList();
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetAutoStockAvailability(string prefixText, int count, string contextKey)
        {
            MedIndentData Objpaic = new MedIndentData();
            MedStoreIndentBO objInfoBO = new MedStoreIndentBO();
            List<MedIndentData> getResult = new List<MedIndentData>();
            Objpaic.StockNo = prefixText;
            Objpaic.ItemID = Convert.ToInt64(contextKey);
            getResult = objInfoBO.GetAutoStockAvailability(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].StockNo.ToString());
            }
            return list;
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_stock, mstlookup.GetLookupsList(LookupName.SubStockType));
            Commonfunction.PopulateDdl(ddl_stockList, mstlookup.GetLookupsList(LookupName.SubStockType));
            Commonfunction.PopulateDdl(ddl_approvedBy, mstlookup.GetLookupsList(LookupName.Employee));
            ddl_approvedBy.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddl_requestType, mstlookup.GetLookupsList(LookupName.requestType));
            ddl_approvedBy.Attributes["disabled"] = "disabled";
            ddl_approvedBy.SelectedValue = LogData.EmployeeID.ToString();
            ddl_stockList.Attributes["disabled"] = "disabled";
            Commonfunction.PopulateDdl(ddl_handover, mstlookup.GetLookupsList(LookupName.MedHandOver));
        }
        protected void gvIndentRequest_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    hdnitemid.Value = "";
                    hdnIndNo.Value = "";
                    txt_tolappqty.Text = "0";
                    MedIndentData objbill = new MedIndentData();
                    MedStoreIndentBO objstdBO = new MedStoreIndentBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvIndentRequest.Rows[i];
                    Label Indno = (Label)gr.Cells[0].FindControl("lbl_Indentno");
                    Label status = (Label)gr.Cells[0].FindControl("lblStID");
                    Label SubStockID = (Label)gr.Cells[0].FindControl("lbl_SubstcokID");
                    txt_indentNumber.Text = Indno.Text;
                    bindindentdetails(Indno.Text);
                    if (status.Text == "3")
                    {
                        tabcontainerIndent.ActiveTabIndex = 0;
                        return;
                    }
                    if (status.Text == "2")
                    {
                        btnsave.Visible = false;
                        btnadd.Visible = false;
                    }
                    else
                    {
                        btnsave.Visible = true;
                        btnadd.Visible = true;
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void bindindentdetails(string indent)
        {
            MedIndentData objbill = new MedIndentData();
            MedStoreIndentBO objstdBO = new MedStoreIndentBO();
            objbill.IndentNo = indent;
            List<MedIndentData> List = new List<MedIndentData>();
            List = objstdBO.GetIndentDetailsByIndentNo(objbill);
            if (List.Count > 0)
            {
                tabcontainerIndent.ActiveTabIndex = 1;
                gvIndentDetail.DataSource = List;
                gvIndentDetail.DataBind();
                gvIndentDetail.Visible = true;
                txt_fromHand.Text = List[0].IndentRaiseDate.ToString();
                ddl_stockList.SelectedValue = List[0].MedSubStockID.ToString();
                txt_tolappqty.Text = List[0].TotApproved.ToString();
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_approvedBy, mstlookup.GetLookupsList(LookupName.Employee));
                ddl_approvedBy.SelectedIndex = 1;
                ddl_approvedBy.SelectedValue = LogData.EmployeeID.ToString();

            }
            else
            {
                tabcontainerIndent.ActiveTabIndex = 1;
                gvIndentDetail.DataSource = null;
                gvIndentDetail.DataBind();
                txt_tolappqty.Text = "";
                gvIndentDetail.Visible = true;
            }

        }
        protected void btnclose_Click(object sender, EventArgs e)
        {
            bindindentdetails(txt_indentNumber.Text);
            lblmsg1.Visible = false;
            div1.Visible = false;
            GVStockAvailable.DataSource = null;
            GVStockAvailable.DataBind();
            GVStockAvailable.Visible = true;
        }
        private void addqty()
        {
            txt_tolappqty.Text = "0";
            foreach (GridViewRow row in gvIndentDetail.Rows)
            {
                Label approd = (Label)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_approvedqty");
                txt_tolappqty.Text = (Convert.ToInt32(txt_tolappqty.Text.Trim() == "" ? "0" : txt_tolappqty.Text.Trim()) + Convert.ToInt32(approd.Text == "" ? "0" : approd.Text)).ToString();
            }
        }
        public List<MedIndentData> GetAvailableList(int curIndex)
        {
            MedIndentData objstock = new MedIndentData();
            MedStoreIndentBO objBO = new MedStoreIndentBO();
            objstock.ItemID = Convert.ToInt64(hdnitemid.Value);
            return objBO.GetPreapproveditemlistbyindentnumber(objstock);
        }
        protected void gvIndentRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label IndentID = (Label)e.Row.FindControl("lbl_Indentno");
                Label status = (Label)e.Row.FindControl("lbllReqTypeID");
                if (Convert.ToInt32(status.Text) == 3)
                {
                    e.Row.Cells[7].BackColor = System.Drawing.Color.YellowGreen;
                }
            }
        }
        protected void GVStockAvailable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label IsApproved = (Label)e.Row.FindControl("lblisapproved");
                LinkButton btndelete = (LinkButton)e.Row.FindControl("lnkDelete");
                if (IsApproved.Text == "1")
                {
                    btndelete.Visible = false;
                }
                else
                {
                    btndelete.Visible = true;
                }
            }
        }
        protected void gvIndentDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    txt_itemname.Text = "";
                    txt_reqdqty.Text = "";
                    txt_ItemIndentNos.Text = "";
                    txt_qty.Text = "";
                    txt_stockNO.Text = "";
                    GVStockAvailable.DataSource = null;
                    GVStockAvailable.DataBind();

                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvIndentDetail.Rows[i];
                    Label IndentNumber = (Label)gr.Cells[0].FindControl("lbl_Indentno");
                    Label itemID = (Label)gr.Cells[0].FindControl("lbl_ItemID");
                    Label ItemName = (Label)gr.Cells[0].FindControl("lblitemname");
                    Label RequestedQty = (Label)gr.Cells[0].FindControl("lbl_ReqQty");
                    //CHECK ITEM AVAIL//
                    MedIndentData objbill = new MedIndentData();
                    MedStoreIndentBO objstdBO = new MedStoreIndentBO();
                    List<MedIndentData> List = new List<MedIndentData>();
                    objbill.ItemID = Convert.ToInt32(itemID.Text == "" ? "0" : itemID.Text);
                    AutoCompleteExtender3.ContextKey = itemID.Text == "" ? "0" : itemID.Text;
                    List = objstdBO.CheckStockitemavailable(objbill);
                    if (List.Count > 0)
                    {
                        lblresult1.Visible = false;
                        div3.Visible = false;
                        txt_itemname.Text = ItemName.Text;
                        txt_reqdqty.Text = RequestedQty.Text;
                        string indentnumbers = IndentNumber.Text;
                        txt_indentNumber.Text = IndentNumber.Text;
                        txt_ItemIndentNos.Text = IndentNumber.Text;
                        int ItemID = Convert.ToInt32(itemID.Text == "" ? "0" : itemID.Text);
                        btnprint.Visible = false;
                        btnprint.Attributes["disabled"] = "disabled";
                        bindpreapprovessubtocks(indentnumbers, ItemID);
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblresult1, "Item not available in the Stock.", 0);
                        div3.Visible = true;
                        div3.Attributes["class"] = "FailAlert";
                        return;
                    }
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
        protected void bindpreapprovessubtocks(string indent, Int32 itemID)
        {
            this.MdItemAvailability.Show();
            MedIndentData objbill = new MedIndentData();
            MedStoreIndentBO objstdBO = new MedStoreIndentBO();
            List<MedIndentData> List = new List<MedIndentData>();
            objbill.IndentNo = indent;
            objbill.ItemID = itemID;
            List = objstdBO.GetPreapproveditemlistbyindentnumber(objbill);
            if (List.Count > 0)
            {
                GVStockAvailable.DataSource = List;
                GVStockAvailable.DataBind();
                GVStockAvailable.Visible = true;
                txt_totappd.Text = List[0].TotalApprovdQty.ToString();
            }
            else
            {
                txt_totappd.Text = "";
                GVStockAvailable.DataSource = null;
                GVStockAvailable.DataBind();
            }
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (txt_stockNO.Text.Trim() == "")
            {
                Messagealert_.ShowMessage(lblmsg1, "Please enter stock no.", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_stockNO.Focus();
                this.MdItemAvailability.Show();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
                this.MdItemAvailability.Show();
            }

            if (txt_qty.Text.Trim() == "")
            {
                Messagealert_.ShowMessage(lblmsg1, "Please enter quantity", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_qty.Focus();
                this.MdItemAvailability.Show();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
                this.MdItemAvailability.Show();
            }

            string StockNo;
            int totalapprovedqty = 0;
            var source = txt_stockNO.Text.ToString();
            if (source.Contains(":"))
            {
                StockNo = source.Substring(source.LastIndexOf(':') + 1);
                // Check Duplicate data 
                foreach (GridViewRow row in GVStockAvailable.Rows)
                {
                    Label apstockno = (Label)GVStockAvailable.Rows[row.RowIndex].Cells[0].FindControl("lblStockNo");
                    Label approveqty = (Label)GVStockAvailable.Rows[row.RowIndex].Cells[0].FindControl("lbl_approved");
                    totalapprovedqty = totalapprovedqty + Convert.ToInt32(approveqty.Text == "" ? "0" : approveqty.Text);
                    if (apstockno.Text == StockNo)
                    {
                        Messagealert_.ShowMessage(lblmsg1, "Listcheck", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_stockNO.ReadOnly = false;
                        txt_stockNO.Text = "";
                        txt_qty.Text = "";
                        txt_stockNO.Focus();
                        this.MdItemAvailability.Show();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        this.MdItemAvailability.Show();
                    }
                }
            }
            else
            {
                this.MdItemAvailability.Show();
                txt_stockNO.Text = "";
                return;
            }
            //add new item
            MedIndentData objstock = new MedIndentData();
            objstock.StockNo = StockNo;
            objstock.ItemName = txt_itemname.Text.Trim() == "" ? " " : txt_itemname.Text.Trim();
            string Qty;
            string ItemID;
            var source1 = txt_stockNO.Text.ToString();
            if (source.Contains(":"))
            {
                MedIndentData objbill = new MedIndentData();
                MedStoreIndentBO objstdBO = new MedStoreIndentBO();
                List<MedIndentData> List = new List<MedIndentData>();
                objbill.StockNo = StockNo;
                List = objstdBO.GetItemDetailsByStockNumbers(objbill);
                Qty = List[0].BalStock.ToString();
                ItemID = List[0].ItemID.ToString();
                if (Convert.ToInt32(txt_qty.Text == "" ? "0" : txt_qty.Text) > Convert.ToInt32(Qty == "" ? "0" : Qty))
                {
                    Messagealert_.ShowMessage(lblmsg1, "Approved Quantity cannot be greater than available balance.", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_stockNO.ReadOnly = false;
                    txt_stockNO.Text = "";
                    txt_qty.Text = "";
                    txt_stockNO.Focus();
                    this.MdItemAvailability.Show();
                    return;
                }
                else
                {
                    MedStoreIndentBO objBO = new MedStoreIndentBO();
                    MedIndentData objrec = new MedIndentData();
                    if (Convert.ToInt32(txt_qty.Text == "" ? "0" : txt_qty.Text) + totalapprovedqty > Convert.ToInt32(txt_reqdqty.Text == "" ? "0" : txt_reqdqty.Text))
                    {
                        Messagealert_.ShowMessage(lblmsg1, "approvedqty", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        this.MdItemAvailability.Show();
                        return;
                    }
                    objrec.StockNo = StockNo;
                    objrec.IndentNo = txt_indentNumber.Text.Trim();
                    objrec.ItemID = Convert.ToInt32(ItemID == "" ? "0" : ItemID);
                    objrec.ApprovedQty = Convert.ToInt32(txt_qty.Text == "" ? "0" : txt_qty.Text);
                    objrec.FinancialYearID = LogData.FinancialYearID;
                    objrec.EmployeeID = LogData.EmployeeID;
                    objrec.HospitalID = LogData.HospitalID;
                    objrec.IPaddress = LogData.IPaddress;
                    objrec.ActionType = Enumaction.Insert;
                    List<MedIndentData> result = objBO.UpdateMedIndentApprovedQtyDetail(objrec);
                    if (result.Count > 0)
                    {
                        GVStockAvailable.DataSource = result;
                        GVStockAvailable.DataBind();
                        GVStockAvailable.Visible = true;
                        txt_stockNO.Text = "";
                        txt_qty.Text = "";
                        txt_stockNO.Focus();
                        lblmessage.Visible = false;
                        txt_totappd.Text = result[0].TotalApprovdQty.ToString();
                        this.MdItemAvailability.Show();
                    }
                    else
                    {
                        GVStockAvailable.DataSource = null;
                        GVStockAvailable.DataBind();
                        txt_totappd.Text = "";
                        lblmessage.Visible = false;
                        this.MdItemAvailability.Show();
                    }
                }
            }
        }
        protected void gvIndentDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label apdqty1 = (Label)e.Row.FindControl("lbl_approvedqty");
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindIndentList();
        }
        protected void bindIndentList()
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
                    lblmessage.Visible = false;
                }
                if (txt_from.Text.Trim() != "")
                {
                    if (Commonfunction.isValidDate(txt_from.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txt_from.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_To.Text.Trim() != "")
                {
                    if (Commonfunction.isValidDate(txt_To.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txt_To.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }
                List<MedIndentData> objdeposit = GetIndentList(0);
                if (objdeposit.Count > 0)
                {
                    gvIndentRequest.DataSource = objdeposit;
                    gvIndentRequest.DataBind();
                    gvIndentRequest.Visible = true;
                }
                else
                {
                    gvIndentRequest.DataSource = null;
                    gvIndentRequest.DataBind();
                    gvIndentRequest.Visible = true;

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
        public List<MedIndentData> GetIndentList(int curIndex)
        {
            MedIndentData objstock = new MedIndentData();
            MedStoreIndentBO objBO = new MedStoreIndentBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objstock.MedSubStockID = Convert.ToInt32(ddl_stock.SelectedValue == "" ? "0" : ddl_stock.SelectedValue);
            objstock.IndentRequestID = Convert.ToInt32(ddl_requestType.SelectedValue == "" ? "0" : ddl_requestType.SelectedValue);
            DateTime from = txt_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_To.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_To.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            objstock.IndentStateID = Convert.ToInt32(ddlindentstatus.SelectedValue == "" ? "0" : ddlindentstatus.SelectedValue);
            return objBO.GetIndentList(objstock);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txt_fromHand.Text = "";
            ddl_stockList.SelectedIndex = 0;
            ddl_approvedBy.SelectedIndex = 0;
            gvIndentDetail.DataSource = null;
            gvIndentDetail.DataBind();
            gvIndentDetail.Visible = false;
            lblmessage2.Visible = false;
            divmsg2.Visible = false;
            div3.Visible = false;
            lblresult1.Visible = false;
            txt_totappd.Text = "";
            txt_tolappqty.Text = "";
            tabcontainerIndent.ActiveTabIndex = 0;
            btnprint.Attributes["disabled"] = "disabled";
            ddl_statuss.SelectedIndex = 0;
            ddl_handover.SelectedIndex = 0;
            lblmessage2.Visible = false;
            txt_indentNumber.Text = "";
            bindIndentList();
        }
        protected void btnprint_Click(object sender, EventArgs e)
        {

        }
        protected void gvHandoverlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void GVStockAvailable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    MedIndentData objstock = new MedIndentData();
                    MedStoreIndentBO objBO = new MedStoreIndentBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GVStockAvailable.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblsubtockID");
                    Label indentNumber = (Label)gr.Cells[0].FindControl("lblindentnumber");
                    Label itemID = (Label)gr.Cells[0].FindControl("lblitemID");
                    objstock.SubStockID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                    objstock.IndentNo = indentNumber.Text.Trim();
                    objstock.ItemID = Convert.ToInt64(itemID.Text == "" ? "0" : itemID.Text);
                    int result = objBO.DeletepreapprovedItem(objstock);
                    if (result == 1)
                    {
                        bindpreapprovessubtocks(indentNumber.Text, Convert.ToInt32(itemID.Text == "" ? "0" : itemID.Text));
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
        protected void gvHandoverlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void btn_save_Click(object sender, EventArgs e)
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
            List<MedIndentData> List = new List<MedIndentData>();
            MedStoreIndentBO objBO = new MedStoreIndentBO();
            MedIndentData objrec = new MedIndentData();
            int rejectcount = 0;

            try
            {
                foreach (GridViewRow row in gvIndentDetail.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label IndentNo = (Label)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_Indentno");
                    Label ItemID = (Label)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label IndentID = (Label)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label DeptId = (Label)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_dept");
                    Label batch = (Label)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_batch");
                    Label ID = (Label)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    TextBox totapprvQty = (TextBox)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_approvedqty");
                    Label totreqstdqty = (Label)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_ReqQty");
                    TextBox Remarks = (TextBox)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lblremarks");
                    Label MainStockID = (Label)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_mainstockID");
                    Label MainStockNo = (Label)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_stockNo");
					Label lbl_supplierID = (Label)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_supplierID");
                    MedIndentData obj = new MedIndentData();
                    obj.IndentNo = IndentNo.Text;
                    Session["indentnumber"] = IndentNo.Text;
                    obj.ItemID = Convert.ToInt64(ItemID.Text == "" ? "0" : ItemID.Text);
                    obj.IndentID = Convert.ToInt64(IndentID.Text == "" ? "0" : IndentID.Text);
                    obj.StockID = Convert.ToInt64(MainStockID.Text == "" ? "0" : MainStockID.Text);
                    obj.StockNo = MainStockNo.Text.Trim() == "" ? "0" : MainStockNo.Text.Trim();
                    obj.BatchNo = batch.Text.Trim() == "" ? "0" : batch.Text.Trim();
                    obj.apprvQty = Convert.ToDecimal(totapprvQty.Text == "" ? "0" : totapprvQty.Text);
					obj.SupplierID = Convert.ToInt32(lbl_supplierID.Text.Trim() == "" ? "0" : lbl_supplierID.Text.Trim());
                    if (Convert.ToInt32(totapprvQty.Text == "" ? "0" : totapprvQty.Text) == 0)
                    {
                        if (Remarks.Text == "")
                        {
                            rejectcount = rejectcount + 1;
                        }
                        Remarks.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
                    }
                    if (Convert.ToInt32(totapprvQty.Text == "" ? "0" : totapprvQty.Text) == Convert.ToInt32(totreqstdqty.Text == "" ? "0" : totreqstdqty.Text))
                    {

                        Remarks.BackColor = System.Drawing.ColorTranslator.FromHtml("#00FF00");
                    }
                    if (Convert.ToInt32(totapprvQty.Text == "" ? "0" : totapprvQty.Text) < Convert.ToInt32(totreqstdqty.Text == "" ? "0" : totreqstdqty.Text) && Convert.ToInt32(totapprvQty.Text == "" ? "0" : totapprvQty.Text) > 0)
                    {
                        Remarks.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFF00");
                    }
                    obj.Remarks = Remarks.Text;
                    obj.ID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    List.Add(obj);
                }
                if (ddl_approvedBy.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "ApprvBy", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_statuss.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "IndentStus", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_handover.SelectedIndex == 0 && ddl_statuss.SelectedValue == "2")
                {
                    Messagealert_.ShowMessage(lblmessage2, "HandOverBy", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (rejectcount > 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "Rejection", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                objrec.XMLData = XmlConvertor.Med_IndentRecordDatatoXML(List).ToString();
                objrec.TotApproved = Convert.ToInt32(txt_tolappqty.Text.Trim() == "" ? "0" : txt_tolappqty.Text.Trim());
                objrec.ApprvBy = LogData.EmployeeID;
                objrec.HandOverTo = Convert.ToInt64(ddl_handover.SelectedValue == "" ? "0" : ddl_handover.SelectedValue);
                objrec.FinancialYearID = LogData.FinancialYearID;
                objrec.IndentNo = Session["indentnumber"].ToString();
                objrec.EmployeeID = LogData.EmployeeID;
                objrec.HospitalID = LogData.HospitalID;
                objrec.IPaddress = LogData.IPaddress;
                objrec.MedSubStockID = LogData.MedSubStockID;
                objrec.ActionType = Enumaction.Insert;
                objrec.IndentStateID = Convert.ToInt32(ddl_statuss.SelectedValue == "" ? "0" : ddl_statuss.SelectedValue);
                int result = objBO.UpdateMedIndentDetail(objrec);
                if (result == 1)
				{
					
                    string IndentNo = Session["indentnumber"].ToString();
					ScriptManager.RegisterStartupScript(Page, GetType(), "Indent_confirm", "<script>pushMessageToPhar('Indent Approved ','PHRReq','1','"+IndentNo+"');</script>", false);
                    bindindentdetails(IndentNo);
                    Session["indentnumber"] = null;
                    Messagealert_.ShowMessage(lblmessage2, "save", 1);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "SucessAlert";
                    btnprint.Attributes.Remove("disabled");
                    btnsave.Attributes["disable"] = "disabled";
					
                }
                if (result == 2)
                {
                    Messagealert_.ShowMessage(lblmessage2, "Alreadyapprove", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
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
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txt_from.Text = "";
            txt_To.Text = "";
            ddl_stock.SelectedIndex = 0;
            ddl_requestType.SelectedIndex = 0;
            gvIndentRequest.DataSource = null;
            gvIndentRequest.DataBind();
            gvIndentRequest.Visible = false;
            lblmessage.Visible = false;
            divmsg1.Visible = false;
        }
        protected void btn_Reset_Click(object sender, EventArgs e)
        {
            txt_totappd.Text = "";
            txt_stockNO.Text = "";
            txt_qty.Text = "";
            GVStockAvailable.DataSource = null;
            GVStockAvailable.DataBind();
            GVStockAvailable.Visible = false;
            lblmsg1.Visible = false;
            div1.Visible = false;
            btnadd.Attributes.Remove("disabled");
            txt_stockNO.ReadOnly = false;
            txt_qty.ReadOnly = false;
            this.MdItemAvailability.Show();
            Session["AvailList"] = null;
            GVStockAvailable.DataSource = null;
            GVStockAvailable.DataBind();
            GVStockAvailable.Visible = true;
            Commonfunction.Insertzeroitemindex(ddl_handover);

        }
		protected void btnOnLoad_Click(object sender, EventArgs e)
		{
			if (Session["PHRIndentID"] != null)
			{
				string ID = Session["PHRIndentID"].ToString();
				Indentno = ID;
				Session["PHRIndentID"] = null;

				bindindentdetails(Indentno);
			}
		}

        protected void ddlindentstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindIndentList();
        }
    }
}
