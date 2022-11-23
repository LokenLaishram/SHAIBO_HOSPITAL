using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.AdmissionBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.BOL.PatientBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedBillData;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.PatientData;
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
namespace Mediqura.Web.MedBills
{
    public partial class IPDrugRequest : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!IsPostBack)
            {
                bindddl();
                btnsave.Attributes["disabled"] = "disabled";
                btnprint.Attributes["disabled"] = "disabled";
                if (Request["IP"] != null && Request["IP"] != "") {
                    txt_ipno.Text = Request["IP"].ToString();
                    ipLoadData();
                }

            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_requestType, mstlookup.GetLookupsList(LookupName.requestType));
            btn_print.Attributes["disabled"] = "disabled";
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPNo(string prefixText, int count, string contextKey)
        {
            IPServiceRecordData Objpaic = new IPServiceRecordData();
            IPServiceRecordBO objInfoBO = new IPServiceRecordBO();
            List<IPServiceRecordData> getResult = new List<IPServiceRecordData>();
            Objpaic.IPNo = prefixText;
            getResult = objInfoBO.getIPNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPNo.ToString());
            }
            return list;
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
            if (txt_ipno.Text == "" || !txt_ipno.Text.Contains(":"))
            {
                Messagealert_.ShowMessage(lblmessage1, "ItemName", 0);
                txt_ipno.Text = "";
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_ipno.Focus();
                return;
            }
            else
            {
                lblmessage1.Visible = false;
            }
            if (txtItemName.Text == "" || !txtItemName.Text.Contains(":"))
            {
                Messagealert_.ShowMessage(lblmessage1, "ItemName", 0);
                txtItemName.Text = "";
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtItemName.Focus();
                return;
            }
            else
            {
                lblmessage1.Visible = false;
            }
            if (ddl_requestType.SelectedIndex == 0)
            {

                Messagealert_.ShowMessage(lblmessage1, "RequestType", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddl_requestType.Focus();
                return;
            }
            else
            {
                lblmessage1.Visible = false;
            }
            string StkID, ItmID;
            string name;

            string source = txtItemName.Text.ToString();

            if (source.Contains(":"))
            {
                ItmID = source.Substring(source.LastIndexOf(':') + 1);
                int indexStart = source.LastIndexOf('-');
                int indexStop = source.LastIndexOf(',');
                int count = indexStop - (indexStart + 1);
                StkID = source.Substring(indexStart + 1, count);
                // Check Duplicate data 
                foreach (GridViewRow row in gvReqList.Rows)
                {
                    Label ItemID = (Label)gvReqList.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");


                    if (Convert.ToInt64(ItemID.Text) == Convert.ToInt64(ItmID))
                    {
                        Messagealert_.ShowMessage(lblmessage1, "Listcheck", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txtItemName.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage1.Visible = false;
                    }
                }
            }
            else
            {
                txtItemName.Text = "";
                return;
            }
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            List<IPDrugIndentData> IndentList = Session["IndentList"] == null ? new List<IPDrugIndentData>() : (List<IPDrugIndentData>)Session["IndentList"];
            IPDrugIndentData objStock = new IPDrugIndentData();
            objStock.StockID = Convert.ToInt64(StkID);
            objStock.ItemID = Convert.ToInt64(ItmID);
            objStock.IndentRequestID = Convert.ToInt32(ddl_requestType.SelectedValue == "" ? "0" : ddl_requestType.SelectedValue);
            
            IPDrugIndentData Objpaic = new IPDrugIndentData();
            IPDrugIndentBO objInfoBO = new IPDrugIndentBO();
            List<IPDrugIndentData> getResult = new List<IPDrugIndentData>();
            Objpaic.StockID = Convert.ToInt64(StkID);
            //Objpaic.NoOfQty = Convert.ToInt32(txt_qty.Text);
            getResult = objInfoBO.GetItemDetailsByStockID(Objpaic);
            if (getResult.Count > 0)
            {
                txt_totRequest.Text = Commonfunction.Getrounding(getResult[0].BalStock.ToString());
                objStock.BatchNo = getResult[0].BatchNo.ToString();
                objStock.BalStock = getResult[0].BalStock;
                objStock.NoOfUnit = getResult[0].NoOfUnit;
                objStock.ItemName = getResult[0].ItemName.ToString();
                objStock.ItemID = getResult[0].ItemID;
                objStock.CPperunit = getResult[0].CPperunit;
                objStock.NetAmt = getResult[0].NetAmt;
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
                gvReqList.DataSource = IndentList;
                gvReqList.DataBind();
                gvReqList.Visible = true;
                Session["IndentList"] = IndentList;
                clearall();
                txtItemName.Focus();
                btnsave.Attributes.Remove("disabled");

              
            }
            else
            {
                gvReqList.DataSource = null;
                gvReqList.DataBind();
                gvReqList.Visible = true;
            }
            ddl_requestType.Attributes["disabled"] = "disabled";
        }
        protected void clearall()
        {
            //txt_qty.Text = "";
            txtItemName.Text = "";
            //txt_ipno.Text = "";

        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (LogData.SaveEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage1, "SaveEnable", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage1.Visible = false;
            }
            if (txt_ipno.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage1, "IssueDate", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_ipno.Focus();
                return;
            }
            else
            {
                lblmessage1.Visible = false;
            }
            List<IPDrugIndentData> ListStock = new List<IPDrugIndentData>();
            IPDrugIndentData objStock = new IPDrugIndentData();
            IPDrugIndentBO objBO = new IPDrugIndentBO();

            try
            {
                int itemcount = 0;
                foreach (GridViewRow row in gvReqList.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label ItemID = (Label)gvReqList.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label StID = (Label)gvReqList.Rows[row.RowIndex].Cells[0].FindControl("lbl_stockID");
                    Label batchNo = (Label)gvReqList.Rows[row.RowIndex].Cells[0].FindControl("lbl_batchNo");
                    Label balStock = (Label)gvReqList.Rows[row.RowIndex].Cells[0].FindControl("lbl_avail");
                    TextBox reqdQty = (TextBox)gvReqList.Rows[row.RowIndex].Cells[0].FindControl("txt_ReqQty");
                    Label SerialID = (Label)gvReqList.Rows[row.RowIndex].Cells[0].FindControl("lblserialID");
                    Label ID = (Label)gvReqList.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label SubStock = (Label)gvReqList.Rows[row.RowIndex].Cells[0].FindControl("lbl_subStock");
                    Label net = (Label)gvReqList.Rows[row.RowIndex].Cells[0].FindControl("lbl_netamt");
                    Label rate = (Label)gvReqList.Rows[row.RowIndex].Cells[0].FindControl("lbl_rate");
                    Label ReqType = (Label)gvReqList.Rows[row.RowIndex].Cells[0].FindControl("lbl_reqID");
                    if (Convert.ToInt32(reqdQty.Text == "" ?" 0" : reqdQty.Text)==0)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "ReqQty", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }

                    IPDrugIndentData ObjDetails = new IPDrugIndentData();
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    ObjDetails.IndentNo = txt_IndentNo.Text == "" ? "0" : txt_IndentNo.Text;
                    ObjDetails.IndentRequestID = Convert.ToInt32(ReqType.Text == "" ? "0" : ReqType.Text);
                    ObjDetails.ItemID = Convert.ToInt32(ItemID.Text == "" ? "0" : ItemID.Text);
                    ObjDetails.StockID = Convert.ToInt32(StID.Text == "" ? "0" : StID.Text);
                    ObjDetails.BatchNo = batchNo.Text.ToString() == "" ? "0" : batchNo.Text.ToString();
                    ObjDetails.BalStock = Convert.ToInt32(balStock.Text == "" ? "0" : balStock.Text);
                    ObjDetails.ReqdQty = Convert.ToInt32(reqdQty.Text == "" ? "0" : reqdQty.Text);
                    ObjDetails.SerialID = Convert.ToInt32(SerialID.Text == "" ? "0" : SerialID.Text);
                    ObjDetails.ID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    itemcount = itemcount + 1;
                    ListStock.Add(ObjDetails);
                }
                objStock.XMLData = XmlConvertor.IndentDataPhrDatatoXML(ListStock).ToString();
                objStock.TotRequestQty = Convert.ToInt32(txt_totRequest.Text == "" ? "0" : txt_totRequest.Text);
                objStock.IPNo =txt_ipno.Text == "" ? "0" : txt_ipno.Text;
                objStock.IndentRequestID = Convert.ToInt32(ddl_requestType.SelectedValue == "" ? "0" : ddl_requestType.SelectedValue);
                if (itemcount == 0)
                {
                    Messagealert_.ShowMessage(lblmessage1, "ReqQty", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage1.Visible = false;
                }
                objStock.HospitalID = LogData.HospitalID;
                objStock.EmployeeID = LogData.EmployeeID;
                objStock.FinancialYearID = LogData.FinancialYearID;
                objStock.ActionType = Enumaction.Insert;
                int result = objBO.UpdateIndentItemDetails(objStock);
                if (result >= 0)
                {
                    txt_IndentNo.Text = result.ToString();
                    lblmessage1.Visible = true;
                    Messagealert_.ShowMessage(lblmessage1, "save", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    Session["IndentList"] = null;
                    btnsave.Attributes["disabled"] = "disabled";
                    btnprint.Attributes.Remove("disabled");
                    ddl_requestType.Attributes.Remove("disabled");
                    ddl_requestType.SelectedIndex = 0;

                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage1, "system", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                string msg = ex.ToString();
                Messagealert_.ShowMessage(lblmessage1, msg, 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            gvReqList.DataSource = null;
            gvReqList.DataBind();
            gvReqList.Visible = false;
            lblmessage2.Visible = false;
            lblresult1.Visible = false;
            txt_ipno.Text = "";
            txt_ipnoList.Text = "";
            txt_fromReqList.Text = "";
            txt_ToReqList.Text = "";
            txt_IndentNo.Text = "";
            txt_totRequest.Text = "";
            txtItemName.Text = "";
            Session["IndentList"] = null;
            ViewState["TotalReq"] = null;
            div1.Visible = false;
            lblmessage1.Visible = false;
            ddl_requestType.Attributes.Remove("disabled");
            ddl_requestType.SelectedIndex = 0;
          
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            gvRequestlist.DataSource = null;
            gvRequestlist.DataBind();
            gvRequestlist.Visible = false;
            lblmessage2.Visible = false;
            lblresult1.Visible = false;
            txt_ipnoList.Text = "";
            Txt_NameLIst.Text = "";
            txt_fromReqList.Text = "";
            txt_ToReqList.Text = "";
            txt_IndentNo.Text = "";
            divmsg2.Visible = false;
            lblmessage2.Text = "";
            divresult1.Visible = false;
            lblresult1.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            ddlstatus.SelectedIndex = 0;
            btn_print.Attributes.Remove("disabled");
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
                if (txt_fromReqList.Text == "")
                {
                    if (Commonfunction.isValidDate(txt_fromReqList.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_fromReqList.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                if (txt_ToReqList.Text == "")
                {
                    if (Commonfunction.isValidDate(txt_ToReqList.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_ToReqList.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                 List<IPDrugIndentData> objdeposit = GetIndentItemList(0);
                if (objdeposit.Count > 0)
                {
                    if (txt_ipnoList.Text != "")
                    {
                        Txt_NameLIst.Text = objdeposit[0].PatientName.ToString();
                    }
                    gvRequestlist.DataSource = objdeposit;
                    gvRequestlist.DataBind();
                    gvRequestlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult1, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divresult1.Attributes["class"] = "SucessAlert";
                    divresult1.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblmessage2.Visible = false;
                    lblmessage1.Visible = false;
                    btn_print.Attributes.Remove("disabled"); 
                  
                }
                else
                {
                    gvRequestlist.DataSource = null;
                    gvRequestlist.DataBind();
                    gvRequestlist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult1.Visible = false;
                    divresult1.Visible = false;
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
        public List<IPDrugIndentData> GetIndentItemList(int curIndex)
        {
            IPDrugIndentData objstock = new IPDrugIndentData();
            IPDrugIndentBO objBO = new IPDrugIndentBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objstock.IPNo = txt_ipnoList.Text.ToString() == "" ? "" : txt_ipnoList.Text.ToString();
            //objstock.SubStockID = Convert.ToInt32(ddl_subStockList.SelectedValue == "" ? "0" : ddl_subStockList.SelectedValue);
            //objstock.IndentRequestID = Convert.ToInt32(ddl_requestTypeList.SelectedValue == "" ? "0" : ddl_requestTypeList.SelectedValue);
            objstock.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            DateTime from = txt_fromReqList.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_fromReqList.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_ToReqList.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_ToReqList.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            return objBO.GetIndentItemList(objstock);
        }
        protected void txt_ipno_TextChanged(object sender, EventArgs e)
        {
            ipLoadData();
        }
        private void ipLoadData() {

            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.IPNo = txt_ipno.Text.Trim() == "" ? "" : txt_ipno.Text.Trim();
            getResult = objInfoBO.GetPatientDetailsByIPNo(Objpaic);
            if (getResult.Count > 0)
            {
                txtname.Text = getResult[0].PatientName.ToString();
                //txt_gender.Text = getResult[0].GenderName.ToString();
                //txt_age.Text = getResult[0].Agecount.ToString();
                //txt_contactno.Text = getResult[0].ContactNo.ToString();

            }
            else
            {
                txtname.Text = "";
                txt_ipno.Text = "";
                txt_ipno.Focus();
                //txt_gender.Text = "";
                //txt_age.Text = "";
                //txt_contactno.Text = "";
                //txt_ipno.Focus();
            }
        }
        protected void txt_ipnoList_TextChanged(object sender, EventArgs e)
        {
            if (txt_ipnoList.Text != "")
            {
                bindgrid();
            }
        }
        protected void txt_ReqQty_TextChanged(object sender, EventArgs e)
        {
            txt_totRequest.Text = "0";
            GridViewRow currentrow = (GridViewRow)((TextBox)sender).Parent.Parent;
            foreach (GridViewRow row in gvReqList.Rows)
            {
                TextBox qty = (TextBox)gvReqList.Rows[row.RowIndex].Cells[0].FindControl("txt_ReqQty");
                Label AvailQty = (Label)gvReqList.Rows[row.RowIndex].Cells[0].FindControl("lbl_avail");
                Label rate = (Label)gvReqList.Rows[row.RowIndex].Cells[0].FindControl("lbl_rate");
                Label NetAmt = (Label)gvReqList.Rows[row.RowIndex].Cells[0].FindControl("lbl_netamt");
                if (Convert.ToInt32(qty.Text == "" ? "0" : qty.Text) > Convert.ToInt32(AvailQty.Text == "" ? "0" : AvailQty.Text))
                {
                    Messagealert_.ShowMessage(lblmessage1, "ReqQtyNo", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    qty.Focus();
                    return;
                }
                else
                {
                    txt_totRequest.Text = (Convert.ToInt32(txt_totRequest.Text == "" ? "0" : txt_totRequest.Text) + Convert.ToInt32(qty.Text == "" ? "0" : qty.Text)).ToString();
                    NetAmt.Text = Commonfunction.Getrounding((Convert.ToInt32(qty.Text == "" ? "0" : qty.Text) * Convert.ToDecimal(rate.Text == "" ? "0" : rate.Text)).ToString());
                    div1.Visible = false;
                }
            }
        }

        protected void gvReqList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvReqList.Rows[i];
                    List<IndentToMainData> ItemList = Session["IndentList"] == null ? new List<IndentToMainData>() : (List<IndentToMainData>)Session["IndentList"];

                    ItemList.RemoveAt(i);
                    Session["IndentList"] = ItemList;
                    gvReqList.DataSource = ItemList;
                    gvReqList.DataBind();

                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage1.Text = ExceptionMessage.GetMessage(ex);
                lblmessage1.Visible = true;
                lblmessage1.CssClass = "Message";
            }
        }

        protected void gvReqList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvRequestlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "DeleteEnable", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage2.Visible = false;
                    }
                    IPDrugIndentData objIndentStatusData = new IPDrugIndentData();
                    IPDrugIndentBO objIndentStatusBO = new IPDrugIndentBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvRequestlist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    Label indNo = (Label)gr.Cells[0].FindControl("lbl_Indentno");
                    Label IndentState = (Label)gr.Cells[0].FindControl("lblstatus");
                    //if (IndentState.Text.Trim() == "Approved")
                    //{
                    //    Messagealert_.ShowMessage(lblresult1, "Approved", 0);
                    //    divresult1.Visible = true;
                    //    divresult1.Attributes["class"] = "FailAlert";
                    //    return;
                    //}
                    //if (IndentState.Text.Trim() == "Handover")
                    //{
                    //    Messagealert_.ShowMessage(lblresult1, "HandOver", 0);
                    //    divresult1.Visible = true;
                    //    divresult1.Attributes["class"] = "FailAlert";
                    //    return;
                    //}
                    if (IndentState.Text.Trim() == "Received")
                    {
                        Messagealert_.ShowMessage(lblresult1, "Received", 0);
                        divresult1.Visible = true;
                        divresult1.Attributes["class"] = "FailAlert";
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
                        divresult1.Visible = true;
                        divresult1.Attributes["class"] = "FailAlert";
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
                    IPDrugIndentBO objIndentStatusBO1 = new IPDrugIndentBO();
                    int Result = objIndentStatusBO1.DeleteIndentReqByID(objIndentStatusData);
                    if (Result == 1)
                    {
                        lblmessage2.Visible = true;
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
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
            }
        }

        protected void gvRequestlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label IndentID = (Label)e.Row.FindControl("lbl_Indentno");
                Label status = (Label)e.Row.FindControl("lblReqTypestatus");
                if (status.Text.Contains("Urgency"))
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.YellowGreen;
                }
            }
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "ExportEnable", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage2.Visible = false;
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
                divresult1.Visible = true;
                divresult1.Attributes["class"] = "FailAlert";
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
                    gvRequestlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvRequestlist.Columns[7].Visible = false;
                    gvRequestlist.Columns[8].Visible = false;


                    gvRequestlist.RenderControl(hw);
                    gvRequestlist.HeaderRow.Style.Add("width", "15%");
                    gvRequestlist.HeaderRow.Style.Add("font-size", "10px");
                    gvRequestlist.Style.Add("text-decoration", "none");
                    gvRequestlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvRequestlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=DrugRequestDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=DrugRequestDetails.xlsx");
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
            List<IPDrugIndentData> DepositDetails = GetIndentItemList(0);
            List<IPDrugIndentDataToExcel> ListexcelData = new List<IPDrugIndentDataToExcel>();
            int i = 0;
            foreach (IPDrugIndentData row in DepositDetails)
            {
                IPDrugIndentDataToExcel Ecxeclpat = new IPDrugIndentDataToExcel();
                Ecxeclpat.IndentNo = DepositDetails[i].IndentNo;
                Ecxeclpat.IPNo = DepositDetails[i].IPNo;
                Ecxeclpat.ReqdQty = DepositDetails[i].ReqdQty;
                Ecxeclpat.IndentRaiseDate = DepositDetails[i].IndentRaiseDate;
                Ecxeclpat.RecdBy = DepositDetails[i].RecdBy;
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