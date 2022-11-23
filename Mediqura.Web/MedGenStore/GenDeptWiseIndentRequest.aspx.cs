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
using Mediqura.BOL.MedGenStoreBO;
namespace Mediqura.Web.MedGenStore
{
    public partial class GenDeptWiseIndentRequest : BasePage
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
                Session["IndentList"] = null;
            }
        }
        private void bindddl()
        {

            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_substock, mstlookup.GetGestockByDesignationforIndent(LogData.DesignationID, LogData.EmployeeID));
            ddl_substock.SelectedValue = LogData.GenSubStockID.ToString();
            Commonfunction.PopulateDdl(ddl_substocklist, mstlookup.GetGestockByDesignationforIndent(LogData.DesignationID, LogData.EmployeeID));
            ddl_substocklist.SelectedValue = LogData.GenSubStockID.ToString();
            Commonfunction.PopulateDdl(ddl_userByDepID, mstlookup.GetLookupsList(LookupName.Employee));
            ddl_userByDepID.SelectedValue = LogData.EmployeeID.ToString();
            Commonfunction.PopulateDdl(ddl_user, mstlookup.GetGenitemRequestedEmployeeByID(Convert.ToInt32(LogData.GenSubStockID.ToString())));
            Commonfunction.PopulateDdl(ddl_requestType, mstlookup.GetLookupsList(LookupName.requestType));
            Commonfunction.PopulateDdl(ddl_requestTypeList, mstlookup.GetLookupsList(LookupName.requestType));
            ddl_userByDepID.Attributes["disabled"] = "disabled";
            txtItemName.Focus();
            if (LogData.DesignationID == 93 || LogData.DesignationID == 20 || LogData.DesignationID == 122 || LogData.DesignationID == 25)
            {
                ddl_substock.Attributes.Remove("disabled");
                ddl_substocklist.Attributes.Remove("disabled");
            }
            else
            {
                ddl_substock.Attributes["disabled"] = "disabled";
                ddl_substocklist.Attributes["disabled"] = "disabled";
            }
            if (LogData.RoleID == 1 || LogData.RoleID == 25)
            {
                Commonfunction.PopulateDdl(ddl_substock, mstlookup.GetLookupsList(LookupName.GenStockType));
                ddl_substock.SelectedValue = LogData.GenSubStockID.ToString();
                Commonfunction.PopulateDdl(ddl_substocklist, mstlookup.GetLookupsList(LookupName.GenStockType));
                ddl_substocklist.SelectedValue = LogData.GenSubStockID.ToString();
                ddl_substocklist.Attributes.Remove("disabled");
                ddl_substock.Attributes.Remove("disabled");
            }
        }
        private void bindddlPostback()
        {
            txt_IssuueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            btn_print.Attributes["disabled"] = "disabled";
            ddl_requestType.SelectedIndex = 1;
        }
        protected void txtItemName_TextChanged(object sender, EventArgs e)
        {
            GenIndentData Objpaic = new GenIndentData();
            GenIndentBO objInfoBO = new GenIndentBO();
            List<GenIndentData> getResult = new List<GenIndentData>();
            string source = txtItemName.Text;
            bool isUHIDnumeric = source.Substring(source.LastIndexOf(':') + 1).All(char.IsDigit);
            if (source.Contains(":") && isUHIDnumeric == true)
            {
                Objpaic.ID = Convert.ToInt32(source.Substring(source.LastIndexOf(':') + 1));
                Objpaic.GenStockID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
                getResult = objInfoBO.GetItemAvailabilty(Objpaic);
                if (getResult.Count > 0)
                {
                    txt_Avail.Text = "Avail: " + Commonfunction.Getrounding(getResult[0].AvailablePC.ToString()) + " % | Qty : " + getResult[0].NoOfQty.ToString();
                    if (getResult[0].AvailablePC > 25)
                    {
                        btnadd.Attributes["disabled"] = "disabled";
                        txtquantity.ReadOnly = true;
                    }
                    else
                    {
                        txtquantity.Focus();
                        btnadd.Attributes.Remove("disabled");
                        txtquantity.ReadOnly = false;
                    }
                }
            }
            else
            {
                txtItemName.Text = "";
                txt_Avail.Text = "";
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemDetails(string prefixText, int count, string contextKey)
        {
            GenIndentData Objpaic = new GenIndentData();
            GenIndentBO objInfoBO = new GenIndentBO();
            List<GenIndentData> getResult = new List<GenIndentData>();
            Objpaic.ItemName = prefixText;
            getResult = objInfoBO.GetItemNameListInStore(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName);

            }
            return list;
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            additem();
        }
        private void additem()
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
                Messagealert_.ShowMessage(lblmessage, "GenSubStock", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
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
            if (txtquantity.Text == "" || txtquantity.Text == "0")
            {
                Messagealert_.ShowMessage(lblmessage, "StockQty", 0);
                txtquantity.Text = "";
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txtquantity.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            List<GenIndentData> IndentList = Session["IndentList"] == null ? new List<GenIndentData>() : (List<GenIndentData>)Session["IndentList"];
            GenIndentData objStock = new GenIndentData();
            string ItmID;
            string name;
            string source = txtItemName.Text.ToString();

            if (source.Contains(":"))
            {
                ItmID = source.Substring(source.LastIndexOf(':') + 1);
                int indexStart = source.LastIndexOf('>');
                int indexStop = source.LastIndexOf(',');
                int count = indexStop - (indexStart + 1);
                name = source.Substring(indexStart + 1, count);
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

            objStock.ItemID = Convert.ToInt64(ItmID);
            objStock.ReqdQty = Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text);

            objStock.ItemName = name;
            GenIndentData Objpaic = new GenIndentData();
            GenIndentBO objInfoBO = new GenIndentBO();
            List<GenIndentData> getResult = new List<GenIndentData>();
            Objpaic.ItemID = Convert.ToInt64(ItmID);
            getResult = objInfoBO.GetItemDetailsByItemID(Objpaic);
            if (getResult.Count > 0)
            {
                objStock.Unitname = getResult[0].Unitname;
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
                txt_totRequestQty.Text = (Convert.ToInt32(txt_totRequestQty.Text == "" ? "0" : txt_totRequestQty.Text) + Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text)).ToString();
                gvIndentRequest.DataSource = IndentList;
                gvIndentRequest.DataBind();
                gvIndentRequest.Visible = true;
                Session["IndentList"] = IndentList;
                clearall();
                txtItemName.Focus();
                btnsave.Attributes.Remove("disabled");
            }
            else
            {
                gvIndentRequest.DataSource = null;
                gvIndentRequest.DataBind();
                gvIndentRequest.Visible = true;
            }
        }
        protected void clearall()
        {
            txtItemName.Text = "";
            txtquantity.Text = "";

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
            if (LogData.SaveEnable == 0 || LogData.GenItemRequestEnable == 0)
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

            List<GenIndentData> ListStock = new List<GenIndentData>();
            GenIndentData objStock = new GenIndentData();
            GenIndentBO objBO = new GenIndentBO();
            try
            {
                int itemcount = 0;
                foreach (GridViewRow row in gvIndentRequest.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label ItemID = (Label)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label StID = (Label)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lbl_stockID");
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
                    GenIndentData ObjDetails = new GenIndentData();
                    ObjDetails.GenStockID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
                    ObjDetails.IndentRequestID = Convert.ToInt32(ddl_requestType.SelectedValue == "" ? "0" : ddl_requestType.SelectedValue);
                    ObjDetails.IndentNo = txt_IndentNo.Text == "" ? "0" : txt_IndentNo.Text;
                    ObjDetails.IndentRequestID = Convert.ToInt32(ReqType.Text == "" ? "0" : ReqType.Text);
                    ObjDetails.ItemID = Convert.ToInt32(ItemID.Text == "" ? "0" : ItemID.Text);
                    ObjDetails.StockID = Convert.ToInt32(StID.Text == "" ? "0" : StID.Text);
                    ObjDetails.ReqdQty = Convert.ToInt32(reqdQty.Text == "" ? "0" : reqdQty.Text);
                    ObjDetails.ID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    itemcount = itemcount + 1;
                    ListStock.Add(ObjDetails);
                }
                objStock.XMLData = XmlConvertor.GEN_IndentDetailsDatatoXML(ListStock).ToString();
                objStock.TotRequestQty = Convert.ToInt32(txt_totRequestQty.Text == "" ? "0" : txt_totRequestQty.Text);
                objStock.GenStockID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
                objStock.IndentRequestID = Convert.ToInt32(ddl_requestType.SelectedValue == "" ? "0" : ddl_requestType.SelectedValue);
                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                DateTime IssDate = txt_IssuueDate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_IssuueDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                objStock.IndentRaiseDate = IssDate;
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
                objStock.RequestedBy = Convert.ToInt64(ddl_userByDepID.SelectedValue == "" ? "0" : ddl_userByDepID.SelectedValue);
                objStock.EmployeeID = LogData.EmployeeID;
                objStock.FinancialYearID = LogData.FinancialYearID;
                objStock.ActionType = Enumaction.Insert;
                List<GenIndentData> result = new List<GenIndentData>();
                result = objBO.UpdateIndentItemDetails(objStock);
                if (result != null)
                {
                    btnsave.Attributes["disabled"] = "disabled";
                    txt_IndentNo.Text = result[0].IndentNo.ToString();
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    Session["IndentList"] = null;
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
            //ddl_substock.SelectedIndex = 0;
            ddl_requestType.SelectedIndex = 0;
            txtItemName.Text = "";
            btnsave.Attributes["disabled"] = "disabled";
            btnprint.Attributes["disabled"] = "disabled";
            ddl_requestType.Attributes.Remove("disabled");
            txt_IndentNo.Text = "";
            Session["IndentList"] = null;
            txt_totRequestQty.Text = "";
            ddl_requestType.SelectedIndex = 1;
            Session["IndentList"] = null;
            ddl_userByDepID.SelectedValue = LogData.EmployeeID.ToString();
            txtItemName.Focus();
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            gvIndentlist.DataSource = null;
            gvIndentlist.DataBind();
            gvIndentlist.Visible = false;
            //ViewState["ID"] = null;
            lblmessage2.Visible = false;
            lblresult1.Visible = false;
            ddl_substocklist.SelectedIndex = 0;
            ddl_requestTypeList.SelectedIndex = 0;
            ddl_user.SelectedIndex = 0;
            txt_indentList.Text = "";
            txt_from.Text = "";
            txt_To.Text = "";
            txt_IndentNo.Text = "";
            txt_totalReq.Text = "";
            txt_Indentapprv.Text = "";
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
                if (ddl_substocklist.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "GenStock", 0);
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
                List<GenIndentData> objdeposit = GetIndentItemList(0);
                if (objdeposit.Count > 0)
                {
                    txt_totalReq.Text = Commonfunction.Getrounding(objdeposit[0].TotRequestQty.ToString());
                    txt_Indentapprv.Text = Commonfunction.Getrounding(objdeposit[0].TotApproved.ToString());
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
        public List<GenIndentData> GetIndentItemList(int curIndex)
        {
            GenIndentData objstock = new GenIndentData();
            GenIndentBO objBO = new GenIndentBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objstock.IndentNo = txt_indentList.Text.ToString() == "" ? "" : txt_indentList.Text.ToString();
            objstock.GenStockID = Convert.ToInt32(ddl_substocklist.SelectedValue == "" ? "0" : ddl_substocklist.SelectedValue);
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
                    GenIndentData objIndentStatusData = new GenIndentData();
                    GenIndentBO objIndentStatusBO = new GenIndentBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvIndentlist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    Label indNo = (Label)gr.Cells[0].FindControl("lbl_Indentno");
                    Label qty = (Label)gr.Cells[0].FindControl("lbl_qty");
                    Label IndentState = (Label)gr.Cells[0].FindControl("lblstatus");
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
                    GenIndentBO objIndentStatusBO1 = new GenIndentBO();
                    int Result = objIndentStatusBO1.DeleteIndentReqByID(objIndentStatusData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "SucessAlert";
                        bindgrid();
                    }
                    if (Result == 2)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "DeleteIndent", 0);
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
                    List<GenIndentData> ItemList = Session["IndentList"] == null ? new List<GenIndentData>() : (List<GenIndentData>)Session["IndentList"];
                    ItemList.RemoveAt(i);
                    TextBox ReqQty = (TextBox)gvIndentRequest.Rows[i].Cells[2].FindControl("txt_ReqQty");
                    txt_totRequestQty.Text = (Convert.ToInt32(txt_totRequestQty.Text == "" ? "0" : txt_totRequestQty.Text) - Convert.ToInt32(ReqQty.Text == "" ? "0" : ReqQty.Text)).ToString();
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
                Label StockStaus = (Label)e.Row.FindControl("lblStID");
                Label Label1 = (Label)e.Row.FindControl("lblstatus");
                if (StockStaus.Text == "1")
                {
                    e.Row.Cells[7].BackColor = System.Drawing.Color.Gray;
                    Label1.ForeColor = System.Drawing.Color.Black;
                }
                if (StockStaus.Text == "2")
                {
                    e.Row.Cells[7].BackColor = System.Drawing.Color.Green;
                    Label1.ForeColor = System.Drawing.Color.White;
                }
                if (StockStaus.Text == "3")
                {
                    e.Row.Cells[7].BackColor = System.Drawing.Color.Red;
                    Label1.ForeColor = System.Drawing.Color.White;
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
                    Response.AddHeader("content-disposition", "attachment;filename=StoreIndentRequestList.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=StoreIndentRequestList.xlsx");
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
            List<GenIndentData> DepositDetails = GetIndentItemList(0);
            List<IndentDataToExcel> ListexcelData = new List<IndentDataToExcel>();
            int i = 0;
            foreach (GenIndentData row in DepositDetails)
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