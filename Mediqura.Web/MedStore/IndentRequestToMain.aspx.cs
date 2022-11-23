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
    public partial class IndentRequestToMain :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //bindddl();
            if (!IsPostBack)
            {
                bindddl();
                bindddlPostback();
                btnsave.Attributes["disabled"] = "disabled";
                btnprint.Attributes["disabled"] = "disabled";
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_substock, mstlookup.GetLookupsList(LookupName.SubStockType));
            ddl_substock.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddl_subStockList, mstlookup.GetLookupsList(LookupName.SubStockType));
            ddl_subStockList.SelectedIndex = 1;
          
          
         }
        private void bindddlPostback()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_requestType, mstlookup.GetLookupsList(LookupName.requestType));
            Commonfunction.PopulateDdl(ddl_requestTypeList, mstlookup.GetLookupsList(LookupName.requestType));
            Commonfunction.PopulateDdl(ddl_user, mstlookup.GetLookupsList(LookupName.StockRecievedBy));
            txt_IssuueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            ddl_substock.SelectedIndex = 1;
            ddl_subStockList.SelectedIndex = 1;
            btn_print.Attributes["disabled"] = "disabled";


        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemDetails(string prefixText, int count, string contextKey)
        {
            IndentToMainData Objpaic = new IndentToMainData();
            IndentToMainBO objInfoBO = new IndentToMainBO();
            List<IndentToMainData> getResult = new List<IndentToMainData>();
            Objpaic.ItemName = prefixText;
            getResult = objInfoBO.GetItemNameListInStock(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName);
            }
            return list;
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (txtItemName.Text == "" || !txtItemName.Text.Contains(":"))
            {
                Messagealert_.ShowMessage(lblmessage, "ItemName", 0);
                txtItemName.Text = "";
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txtItemName.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddl_substock.SelectedIndex == 0)
            {

                Messagealert_.ShowMessage(lblmessage, "Substock", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_substock.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddl_requestType.SelectedIndex == 0)
            {

                Messagealert_.ShowMessage(lblmessage, "RequestType", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_requestType.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (Commonfunction.isValidDate(txt_IssuueDate.Text) == false && Commonfunction.ChecklowerDate(txt_IssuueDate.Text) == true)
            {
                Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                txt_IssuueDate.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            List<IndentToMainData> IndentList = Session["IndentList"] == null ? new List<IndentToMainData>() : (List<IndentToMainData>)Session["IndentList"];
            IndentToMainData objStock = new IndentToMainData();
            objStock.StockTypeID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
            objStock.IndentRequestID = Convert.ToInt32(ddl_requestType.SelectedValue == "" ? "0" : ddl_requestType.SelectedValue);
            objStock.IndentNo = txt_IndentNo.Text.ToString() == "" ? "" : txt_IndentNo.Text.ToString();
            //objStock.ItemName = txtItemName.Text.ToString() == "" ? "" : txtItemName.Text.ToString();
            if (txt_IssuueDate.Text == "")
            {
                //txt_issueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                objStock.IndentRaiseDate = txt_IssuueDate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_IssuueDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            }
            else
            {

                DateTime issuedate = txt_IssuueDate.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_IssuueDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                objStock.IndentRaiseDate = issuedate;
            }
             string StkID, ItmID;
             string name; 
            
            string source = txtItemName.Text.ToString();
            
            if (source.Contains(":"))
            {
                ItmID = source.Substring(source.LastIndexOf(':') + 1);
                int indexStart = source.LastIndexOf('-');
                int indexStop = source.LastIndexOf(',');
                int count = indexStop - (indexStart+1);
                StkID = source.Substring(indexStart+1, count);
                // Check Duplicate data 
                foreach (GridViewRow row in gvIndentRequest.Rows)
                {
                    Label ItemID = (Label)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");

                    if (Convert.ToInt64(ItemID.Text) == Convert.ToInt64(ItmID))
                    {
                        Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txtItemName.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
            }
            else
            {
                txtItemName.Text = "";
                return;
            }

           
            objStock.StockID = Convert.ToInt64(StkID);
            objStock.ItemID = Convert.ToInt64(ItmID);

            IndentToMainData Objpaic = new IndentToMainData();
            IndentToMainBO objInfoBO = new IndentToMainBO();
            List<IndentToMainData> getResult = new List<IndentToMainData>();
            Objpaic.StockID = Convert.ToInt64(StkID);
            getResult = objInfoBO.GetItemDetailsByStockID(Objpaic);
            if (getResult.Count > 0)
            {
                objStock.BatchNo = getResult[0].BatchNo.ToString();
                objStock.BalStock = getResult[0].BalStock;
                objStock.NoOfUnit = getResult[0].NoOfUnit;
                objStock.ItemName= getResult[0].ItemName.ToString();
                objStock.ItemID = getResult[0].ItemID;
            }
            else
            {
                objStock.BatchNo = null;
                objStock.BalStock = 0;
                objStock.NoOfUnit = 0;
            }
            IndentList.Add(objStock);
            if (IndentList.Count > 0)
            {
                gvIndentRequest.DataSource = IndentList;
                gvIndentRequest.DataBind();
                gvIndentRequest.Visible = true;
                Session["IndentList"] = IndentList;
                clearall();
                txtItemName.Focus();
                btnsave.Attributes.Remove("disabled");
                ddl_substock.SelectedIndex = 1;
            }
            else
            {
                gvIndentRequest.DataSource = null;
                gvIndentRequest.DataBind();
                gvIndentRequest.Visible = true;
            }
            ddl_requestType.Attributes["disabled"] = "disabled";
        }
        protected void clearall()
        {
            ddl_substock.SelectedIndex = 1;
            //ddl_requestType.SelectedIndex = 0;
            txtItemName.Text = "";
           
        }
        protected void gvIndentRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label lblSerial = (Label)e.Row.FindControl("lblserialID");
                //lblSerial.Text = ((gvIndentRequest.PageIndex * gvIndentRequest.PageSize) + e.Row.RowIndex + 1).ToString();
            }
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
           
            if (txt_IssuueDate.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "IssueDate", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_IssuueDate.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
          
            List<IndentToMainData> ListStock = new List<IndentToMainData>();
            IndentToMainData objStock = new IndentToMainData();
            IndentToMainBO objBO = new IndentToMainBO();
            try
            {
                int itemcount = 0;
                foreach (GridViewRow row in gvIndentRequest.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label ItemID = (Label)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label StID = (Label)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lbl_stockID");
                    Label batchNo = (Label)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lbl_batchNo");
                    Label balStock = (Label)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lbl_avail");
                    TextBox reqdQty = (TextBox)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("txt_ReqQty");
                    Label Unit = (Label)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lbl_unit");
                    Label SerialID = (Label)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lblserialID");
                    Label ID = (Label)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label issuDt = (Label)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lbl_issueDate");
                    Label ReqType = (Label)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lbl_reqID");
                    Label SubStock = (Label)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lbl_subStock");

                    if (Convert.ToInt32(reqdQty.Text == "" ? " 0" : reqdQty.Text) == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ReqQty", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        reqdQty.Focus();
                        return;
                    }
                   
                    IndentToMainData ObjDetails = new IndentToMainData();
                    ObjDetails.SubStockID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
                    ObjDetails.IndentRequestID = Convert.ToInt32(ddl_requestType.SelectedValue == "" ? "0" : ddl_requestType.SelectedValue);
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    DateTime IssDate = issuDt.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(issuDt.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                    ObjDetails.IndentNo = txt_IndentNo.Text == "" ? "0" : txt_IndentNo.Text;

                    ObjDetails.SubStockID = Convert.ToInt32(SubStock.Text == "" ? "0" : SubStock.Text);
                    ObjDetails.IndentRequestID = Convert.ToInt32(ReqType.Text == "" ? "0" : ReqType.Text);
                    ObjDetails.IndentRaiseDate = IssDate;
                    ObjDetails.ItemID = Convert.ToInt32(ItemID.Text == "" ? "0" : ItemID.Text);
                    ObjDetails.StockID = Convert.ToInt32(StID.Text == "" ? "0" : StID.Text);
                    ObjDetails.BatchNo = batchNo.Text.ToString() == "" ? "0" : batchNo.Text.ToString();
                    ObjDetails.BalStock = Convert.ToInt32(balStock.Text == "" ? "0" : balStock.Text);
                    ObjDetails.ReqdQty = Convert.ToInt32(reqdQty.Text == "" ? "0" : reqdQty.Text);
                    ObjDetails.NoOfUnit = Convert.ToInt32(Unit.Text == "" ? "0" : Unit.Text);
                    ObjDetails.SerialID = Convert.ToInt32(SerialID.Text == "" ? "0" : SerialID.Text);
                    ObjDetails.ID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    itemcount = itemcount + 1;
                    ListStock.Add(ObjDetails);
                    
                }
                objStock.XMLData = XmlConvertor.IndentDetailsDatatoXML(ListStock).ToString();
                objStock.TotRequestQty = Convert.ToInt32(txt_totRequestQty.Text == "" ? "0" : txt_totRequestQty.Text);
                objStock.SubStockID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
                objStock.IndentRequestID = Convert.ToInt32(ddl_requestType.SelectedValue == "" ? "0" : ddl_requestType.SelectedValue);
                if (itemcount == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "ReqQty", 0);
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
                objStock.FinancialYearID = LogData.FinancialYearID;
                objStock.ActionType = Enumaction.Insert;

                int result = objBO.UpdateIndentItemDetails(objStock);
                if (result >= 0)
                {
                    txt_IndentNo.Text = result.ToString();
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    Session["IndentList"] = null;
                    btnsave.Attributes["disabled"] = "disabled";
                    btnprint.Attributes.Remove("disabled");
                    ddl_requestType.Attributes.Remove("disabled");
                    ddl_requestType.SelectedIndex = 0;
                   
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
        protected void txt_ReqQty_TextChanged(object sender, EventArgs e)
        {
            txt_totRequestQty.Text = "0";
            GridViewRow currentrow = (GridViewRow)((TextBox)sender).Parent.Parent;
            foreach (GridViewRow row in gvIndentRequest.Rows)
            {
                TextBox qty = (TextBox)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("txt_ReqQty");
                Label AvailQty = (Label)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lbl_avail");
                if (Convert.ToInt32(qty.Text == "" ? "0" : qty.Text) > Convert.ToInt32(AvailQty.Text == "" ? "0" : AvailQty.Text))
                {
                    Messagealert_.ShowMessage(lblmessage, "ReqQtyNo", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    qty.Focus();
                    return;
                }
                else
                {
                    txt_totRequestQty.Text = (Convert.ToInt32(txt_totRequestQty.Text == "" ? "0" : txt_totRequestQty.Text) + Convert.ToInt32(qty.Text == "" ? "0" : qty.Text)).ToString();
                    divmsg1.Visible = false;
                }
            }
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            gvIndentRequest.DataSource = null;
            gvIndentRequest.DataBind();
            gvIndentRequest.Visible = false;
            lblresult.Visible = false;
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            div1.Visible = false;
            divmsg1.Visible = false;
            ddl_substock.SelectedIndex = 1;
            ddl_requestType.SelectedIndex = 0;
            txtItemName.Text = "";
            btnsave.Attributes["disabled"] = "disabled";
            btnprint.Attributes["disabled"] = "disabled";
            ddl_requestType.Attributes.Remove("disabled");
            txt_IndentNo.Text = "";
            Session["IndentList"] = null;
            txt_totRequestQty.Text = "";

        }

        protected void btnresets_Click(object sender, EventArgs e)
        {
            gvIndentlist.DataSource = null;
            gvIndentlist.DataBind();
            gvIndentlist.Visible = false;
            //ViewState["ID"] = null;
            lblmessage2.Visible = false;
            lblresult1.Visible = false;
            ddl_subStockList.SelectedIndex = 1;
            ddl_requestTypeList.SelectedIndex = 0;
            ddl_user.SelectedIndex = 0;
            txt_indentList.Text = "";
            txt_from.Text = "";
            txt_To.Text = "";
            txt_IndentNo.Text = "";
            txt_totalReq.Text="";
            txt_totalInRecv.Text="";
            txt_TotAccepted.Text="";
            txt_Indentapprv.Text="";
            txt_InHandover.Text = "";
            txt_TotIndent.Text = "";
            ViewState["TotalReq"] = null;
            ddl_requestTypeList.Attributes.Remove("disabled");
            ddl_requestType.SelectedIndex = 0;

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
                    Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (txt_from.Text == "")
                {
                    if (Commonfunction.isValidDate(txt_from.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_from.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                if (txt_To.Text == "")
                {
                    if (Commonfunction.isValidDate(txt_To.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_To.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                List<IndentToMainData> objdeposit = GetIndentItemList(0);
                if (objdeposit.Count > 0)
                {
                    txt_totalReq.Text = Commonfunction.Getrounding(objdeposit[0].TotRequestQty.ToString());
                    txt_TotAccepted.Text = Commonfunction.Getrounding(objdeposit[0].TotAccepted.ToString());
                    txt_Indentapprv.Text = Commonfunction.Getrounding(objdeposit[0].TotApproved.ToString());
                    txt_InHandover.Text = Commonfunction.Getrounding(objdeposit[0].TotHandOver.ToString());
                    txt_totalInRecv.Text = Commonfunction.Getrounding(objdeposit[0].TotReceived.ToString());
                    gvIndentlist.DataSource = objdeposit;
                    gvIndentlist.DataBind();
                    gvIndentlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult1, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    div3.Attributes["class"] = "SucessAlert";
                    div3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblmessage2.Visible = false;
                    lblmessage.Visible = false;
                    //btn_update.Visible = true;
                    txt_TotIndent.Text = objdeposit[0].MaximumRows.ToString();
                    btn_print.Attributes.Remove("disabled"); 
                }
                else
                {
                    gvIndentlist.DataSource = null;
                    gvIndentlist.DataBind();
                    gvIndentlist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult.Visible = false;
                    div3.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        public List<IndentToMainData> GetIndentItemList(int curIndex)
        {
            IndentToMainData objstock = new IndentToMainData();
            IndentToMainBO objBO = new IndentToMainBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objstock.IndentNo = txt_indentList.Text.ToString() == "" ? "" : txt_indentList.Text.ToString();
            objstock.SubStockID = Convert.ToInt32(ddl_subStockList.SelectedValue == "" ? "0" : ddl_subStockList.SelectedValue);
            objstock.IndentRequestID = Convert.ToInt32(ddl_requestTypeList.SelectedValue == "" ? "0" : ddl_requestTypeList.SelectedValue);
            objstock.ReceivedBy = Convert.ToInt64(ddl_user.SelectedValue == "" ? "0" : ddl_user.SelectedValue); ;
            DateTime from = txt_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_To.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_To.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            return objBO.GetIndentItemList(objstock);
        }
        protected void gvIndentlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvIndentlist.PageIndex = e.NewPageIndex;
            bindgrid();
        }

        protected void gvIndentlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "DeleteEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    IndentToMainData objIndentStatusData = new IndentToMainData();
                    IndentToMainBO objIndentStatusBO = new IndentToMainBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvIndentlist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    Label indNo = (Label)gr.Cells[0].FindControl("lbl_Indentno");
                    Label qty = (Label)gr.Cells[0].FindControl("lbl_qty");
                    Label IndentState = (Label)gr.Cells[0].FindControl("lblstatus");
                    //if (IndentState.Text.Trim() == "Approved")
                    //{
                    //    Messagealert_.ShowMessage(lblresult1, "Approved", 0);
                    //    div3.Visible = true;
                    //    lblresult1.Visible = false;
                    //    div3.Attributes["class"] = "FailAlert";
                    //    return;
                    //}
                    //if (IndentState.Text.Trim() == "Handover")
                    //{
                    //    Messagealert_.ShowMessage(lblresult1, "HandOver", 0);
                    //    div3.Visible = true;
                    //    div3.Attributes["class"] = "FailAlert";
                    //    return;
                    //}
                    if (IndentState.Text.Trim() == "Received")
                    {
                        Messagealert_.ShowMessage(lblresult1, "Received", 0);
                        div3.Visible = true;
                        div3.Attributes["class"] = "FailAlert";
                        return;
                    }
                    objIndentStatusData.IndentID = Convert.ToInt64(ID.Text);
                    objIndentStatusData.IndentNo = indNo.Text;
                    
                    objIndentStatusData.EmployeeID = LogData.EmployeeID;
                    objIndentStatusData.ActionType = Enumaction.Delete;
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult1, "Remarks", 0);
                        div3.Visible = true;
                        div3.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objIndentStatusData.Remarks = txtremarks.Text;
                    }


                    //List<IndentToMainData> List = new List<IndentToMainData>();
                    //List = objIndentStatusBO.GetIndentList1(objIndentStatusData);
                    //if (List[0].ReqdQty > 0)
                    //{
                        
                    //    List<IndentToMainData> Listrqd = new List<IndentToMainData>();
                    //    IndentToMainBO objBO = new IndentToMainBO();
                    //    IndentToMainData objrec = new IndentToMainData();

                    //    for (int i = 0; i < List[0].ReqdQty; i++)
                    //    {
                    //        objIndentStatusData.ReqdQty = List[0].ReqdQty;
                    //        int Result = objIndentStatusBO.DeleteIndentReqByID(objIndentStatusData);
                    //        if (Result == 1)
                    //        {
                    //            Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                    //            divmsg2.Visible = true;
                    //            divmsg2.Attributes["class"] = "SucessAlert";
                    //            bindgrid();
                    //        }
                    //        else
                    //        {
                    //            Messagealert_.ShowMessage(lblmessage2, "system", 0);
                    //            divmsg2.Visible = true;
                    //            divmsg2.Attributes["class"] = "FailAlert";

                    //        }
                    //    }
                    //}
                    IndentToMainBO objIndentStatusBO1 = new IndentToMainBO();
                    int Result = objIndentStatusBO1.DeleteIndentReqByID(objIndentStatusData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "SucessAlert";
                        bindgrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage2, "system", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";

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

       protected void gvIndentRequest_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvIndentRequest.Rows[i];
                    List<IndentToMainData> ItemList = Session["IndentList"] == null ? new List<IndentToMainData>() : (List<IndentToMainData>)Session["IndentList"];
                   
                    ItemList.RemoveAt(i);
                    Session["IndentList"] = ItemList;
                    gvIndentRequest.DataSource = ItemList;
                    gvIndentRequest.DataBind();

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

        protected void gvIndentlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label IndentID = (Label)e.Row.FindControl("lbl_Indentno");
                Label status = (Label)e.Row.FindControl("lblReqTypestatus");
                if (status.Text.Contains("Urgency"))
                {
                    e.Row.Cells[5].BackColor = System.Drawing.Color.YellowGreen;
                }
            }
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ExportEnable", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
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
                div3.Visible = true;
                div3.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    gvIndentlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    //gvIndentlist.Columns[4].Visible = false;
                    //gvIndentlist.Columns[5].Visible = false;
                    gvIndentlist.Columns[7].Visible = false;
                    gvIndentlist.Columns[8].Visible = false;

                    gvIndentlist.RenderControl(hw);
                    gvIndentlist.HeaderRow.Style.Add("width", "15%");
                    gvIndentlist.HeaderRow.Style.Add("font-size", "10px");
                    gvIndentlist.Style.Add("text-decoration", "none");
                    gvIndentlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvIndentlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=IndentRequestList.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Patient Type Detail List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=IndentRequestList.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        private DataTable GetDatafromDatabase()
        {
            List<IndentToMainData> DepositDetails = GetIndentItemList(0);
            List<IndentDataToExcel> ListexcelData = new List<IndentDataToExcel>();
            int i = 0;
            foreach (IndentToMainData row in DepositDetails)
            {
                IndentDataToExcel Ecxeclpat = new IndentDataToExcel();
                Ecxeclpat.IndentNo = DepositDetails[i].IndentNo;
                Ecxeclpat.TotalRqty = DepositDetails[i].TotalRqty;
                Ecxeclpat.IndentRaiseDate = DepositDetails[i].IndentRaiseDate;
                Ecxeclpat.RecdBy = DepositDetails[i].RecdBy;
                Ecxeclpat.RequestStat = DepositDetails[i].RequestStat;
                Ecxeclpat.IndentStatus = DepositDetails[i].IndentStatus;


                ListexcelData.Add(Ecxeclpat);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        public class ListtoDataTableConverter
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];

                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable
                return dataTable;
            }
        }
       
    }
}