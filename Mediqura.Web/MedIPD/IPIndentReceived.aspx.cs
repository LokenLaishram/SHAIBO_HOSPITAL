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
namespace Mediqura.Web.MedIPD
{
    public partial class IPIndentReceived : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                btnprint.Attributes["disabled"] = "disabled";
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            // drop for tab 1 Indent received
       
            Commonfunction.PopulateDdl(ddl_RequestTypeRecv, mstlookup.GetLookupsList(LookupName.requestType));
       
            // dropdown for tab 2
        
            Commonfunction.PopulateDdl(ddl_rcvBy, mstlookup.GetLookupsList(LookupName.StockRecievedBy));
            Commonfunction.PopulateDdl(ddl_status, mstlookup.GetLookupsList(LookupName.IndentStatus));
            Commonfunction.PopulateDdl(ddl_user, mstlookup.GetLookupsList(LookupName.StockRecievedBy));
          
            ddl_status.SelectedIndex = 4;
            btnSaveRecv.Attributes["disabled"] = "disabled";
            btnPrintRecv.Attributes["disabled"] = "disabled";


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
        protected void txt_ipno_TextChanged(object sender, EventArgs e)
        {
            if (txt_ipno.Text != "")
            {
                bindIndentRecvList();
            }
        }
        protected void btnsearcgRecv_Click(object sender, EventArgs e)
        {
            bindIndentRecvList();
        }
        protected void bindIndentRecvList()
        {
            try
            {
                if (LogData.SearchEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage6, "SearchEnable", 0);
                    div6.Visible = true;
                    div6.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage6.Visible = false;
                }

                if (txt_fromRecv.Text == "")
                {
                    if (Commonfunction.isValidDate(txt_fromRecv.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage6, "ValidDate", 0);
                        div6.Attributes["class"] = "FailAlert";
                        div6.Visible = true;
                        txt_fromRecv.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage6.Visible = false;
                }
                if (txt_ToRecv.Text == "")
                {
                    if (Commonfunction.isValidDate(txt_ToRecv.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage6, "ValidDate", 0);
                        div6.Attributes["class"] = "FailAlert";
                        div6.Visible = true;
                        txt_ToRecv.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage6.Visible = false;
                }
                List<IPDrugIndentData> objdeposit = bindIndentRecvList(0);
                if (objdeposit.Count > 0)
                {
                    gvHndOvList.DataSource = objdeposit;
                    gvHndOvList.DataBind();
                    gvHndOvList.Visible = true;


                }
                else
                {
                    gvHndOvList.DataSource = null;
                    gvHndOvList.DataBind();
                    gvHndOvList.Visible = true;

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
        public List<IPDrugIndentData> bindIndentRecvList(int curIndex)
        {
            IPDrugIndentData objstock = new IPDrugIndentData();
            IPDrugIndentBO objBO = new IPDrugIndentBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objstock.IndentRequestID = Convert.ToInt32(ddl_RequestTypeRecv.SelectedValue == "" ? "0" : ddl_RequestTypeRecv.SelectedValue);
            DateTime from = txt_fromRecv.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_fromRecv.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_ToRecv.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_ToRecv.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            objstock.IPNo = txt_ipno.Text == "" ? "0" : txt_ipno.Text;
            return objBO.bindIndentRecvList(objstock);
        }
        protected void gvHndOvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                if (e.CommandName == "lnkSelectRecv")
                {
                    IPDrugIndentData objbill = new IPDrugIndentData();
                    IPDrugIndentBO objstdBO = new IPDrugIndentBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvHndOvList.Rows[i];
                    Label Indno = (Label)gr.Cells[0].FindControl("lbl_IndnoHndOv");
                    objbill.IndentNo = Indno.Text;
                    List<IPDrugIndentData> List = new List<IPDrugIndentData>();
                    List = objstdBO.GetHndoverDetail(objbill);
                    if (List.Count > 0)
                    {
                        txt_totApprvRecv.Text = Commonfunction.Getrounding(List[0].TotApproved.ToString());
                        txt_totHandOvRecv.Text = Commonfunction.Getrounding(List[0].TotHandOver.ToString());
                        txt_totRecv.Text = Commonfunction.Getrounding(List[0].TotRequested.ToString());
                        gvHndetail.DataSource = List;
                        gvHndetail.DataBind();
                        gvHndetail.Visible = true;
                        btnSaveRecv.Attributes.Remove("disabled");
                        //foreach (GridViewRow row1 in gvHandoverlist.Rows)
                        //{
                        //    Label CP = (Label)gvHandoverlist.Rows[row1.RowIndex].Cells[0].FindControl("lbl_cp");
                        //    TextBox totqty = (TextBox)gvHandoverlist.Rows[row1.RowIndex].Cells[0].FindControl("txt_approvedqty");
                        //    txt_totcp.Text = (Convert.ToDecimal(txt_totcp.Text) + Convert.ToDecimal(CP.Text)).ToString();
                        //    txt_totappreqd.Text = (Convert.ToInt32(txt_totappreqd.Text) + Convert.ToInt32(totqty.Text)).ToString();
                        //}
                    }
                    else
                    {
                        gvHndetail.DataSource = null;
                        gvHndetail.DataBind();
                        gvHndetail.Visible = true;
                    }
                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage6, "system", 0);
            }

        }

        protected void gvHndOvList_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvHndetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvHndetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void txt_RecvQTY_TextChanged(object sender, EventArgs e)
        {
            txt_totRecv.Text = "0";
            GridViewRow currentrow = (GridViewRow)((TextBox)sender).Parent.Parent;
            foreach (GridViewRow row in gvHndetail.Rows)
            {
                Label Hndqty = (Label)gvHndetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_HndQty");
                TextBox RecvQty = (TextBox)gvHndetail.Rows[row.RowIndex].Cells[0].FindControl("txt_Recvqty");
                if (Convert.ToInt32(RecvQty.Text) > Convert.ToInt32(Hndqty.Text))
                {
                    Messagealert_.ShowMessage(lblmessage6, "RecvQty", 0);
                    div6.Visible = true;
                    div6.Attributes["class"] = "FailAlert";
                    RecvQty.Focus();
                    return;
                }
                else
                {
                    txt_totRecv.Text = (Convert.ToInt32(txt_totRecv.Text) + Convert.ToInt32(RecvQty.Text)).ToString();
                }

            }
        }
        protected void btnSaveRecv_Click(object sender, EventArgs e)
        {
            if (LogData.SaveEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage6, "SaveEnable", 0);
                div6.Visible = true;
                div6.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage6.Visible = false;

            }
            List<IPDrugIndentData> List = new List<IPDrugIndentData>();
            IPDrugIndentBO objBO = new IPDrugIndentBO();
            IPDrugIndentData objrec = new IPDrugIndentData();
            try
            {
                foreach (GridViewRow row in gvHndetail.Rows)
                {
                    //CheckBox cb1 = (CheckBox)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                    //if (cb1.Checked)
                    //{code
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label IndentNo = (Label)gvHndetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_IndentnoRecv");

                    Label ItemID = (Label)gvHndetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label IndentID = (Label)gvHndetail.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    //Label IndentID = (Label)gvHndetail.Rows[row.RowIndex].Cells[0].FindControl("code");
                    Label StkID = (Label)gvHndetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_stockID");
                    Label availQty = (Label)gvHndetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_avail");
                    Label reqQty = (Label)gvHndetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_ReqQty");
                    Label apprvQty = (Label)gvHndetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_ApprvQty");
                    Label HndQty = (Label)gvHndetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_ApprvQty");
                    Label ID = (Label)gvHndetail.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label Recv = (Label)gvHndetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_RecvQty");
                    Label Ipno = (Label)gvHndetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_Ipno");
                    if (Convert.ToInt32(Recv.Text) == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage6, "ReceivedQty", 0);
                        div6.Visible = true;
                        div6.Attributes["class"] = "FailAlert";
                        return;
                    }
                    IPDrugIndentData obj = new IPDrugIndentData();
                    obj.IndentNo = IndentNo.Text;
                    obj.ItemID = Convert.ToInt64(ItemID.Text);
                    obj.IndentID = Convert.ToInt64(IndentID.Text);
                    obj.StockID = Convert.ToInt64(StkID.Text);
                    obj.BalStock = Convert.ToInt32(availQty.Text);
                    obj.ReqdQty = Convert.ToInt32(reqQty.Text);
                    obj.apprvQty = Convert.ToInt32(apprvQty.Text);
                    obj.HndQty = Convert.ToInt32(HndQty.Text);
                    obj.RecvQty = Convert.ToInt32(Recv.Text);
                    obj.ID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    obj.IPNo = Ipno.Text;
                    //txt_TotApprv.Text = (Convert.ToInt32(txt_TotApprv.Text == "" ? "0" : txt_TotApprv.Text) + obj.apprvQty).ToString();
                    //txt_TotApprv.Text = (txt_TotApprv.Text == "" ? "0" : txt_TotApprv.Text);
                    List.Add(obj);

                    //}
                }
                if (List.Count == 0)
                {
                    Messagealert_.ShowMessage(lblmessage6, "Checked", 0);
                    div6.Visible = true;
                    div6.Attributes["class"] = "FailAlert";
                    return;
                }
                objrec.XMLData = XmlConvertor.IPReceivedRecordDatatoXML(List).ToString();
                objrec.TotReceived = Convert.ToInt32(txt_totRecv.Text == "" ? "0" : txt_totRecv.Text);
                objrec.ReceivedBy = Convert.ToInt64(ddl_user.SelectedValue == "" ? "0" : ddl_user.SelectedValue);
                objrec.FinancialYearID = LogData.FinancialYearID;
                objrec.EmployeeID = LogData.EmployeeID;
                objrec.HospitalID = LogData.HospitalID;
                objrec.IPaddress = LogData.IPaddress;
                objrec.ActionType = Enumaction.Insert;

                int result = objBO.UpdateReceivedDetail(objrec);
                if (result > 0)
                {
                    bindIndentRecvList();
                    Messagealert_.ShowMessage(lblmessage6, "save", 1);
                    div6.Visible = true;
                    div6.Attributes["class"] = "SucessAlert";
                    btnSaveRecv.Attributes["disabled"] = "disabled";
                    btnPrintRecv.Attributes.Remove("disabled");
                    if (LogData.PrintEnable == 0)
                    {
                        btnPrintRecv.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btnPrintRecv.Attributes.Remove("disabled");
                    }
                    gvHndetail.DataSource = null;
                    gvHndetail.DataBind();
                    gvHndetail.Visible = false;
                   
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage6, "Error", 0);
                    div6.Visible = true;
                    div6.Attributes["class"] = "FailAlert";
                }

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage6.Text = ExceptionMessage.GetMessage(ex);
                div6.Visible = true;
                div6.Attributes["class"] = "FailAlert";
            }
        }
        protected void btnresetRecv_Click(object sender, EventArgs e)
        {
            ddl_user.SelectedIndex = 0;
            ddl_RequestTypeRecv.SelectedIndex = 0;
            txt_totHandOvRecv.Text = "";
            txt_totApprvRecv.Text = "";
            txt_totRecv.Text = "";
            txt_ipno.Text = "";
            txt_fromRecv.Text = "";
            txt_ToRecv.Text = "";
            gvHndetail.DataSource = null;
            gvHndetail.DataBind();
            gvHndetail.Visible = false;
            lblmessage6.Visible = false;
            div6.Visible = false;
            //txt_TotApprv.Text = "";
            btnSaveRecv.Attributes["disabled"] = "disabled";
        }

        protected void gvApprvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvApprvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvapprvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvapprvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnreset_Click(object sender, EventArgs e)
        {

        }

        protected void btnsearchList_Click(object sender, EventArgs e)
        {
            bindReceivedList();
        }
        protected void bindReceivedList()
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

                if (txt_fromRecvList.Text == "")
                {
                    if (Commonfunction.isValidDate(txt_fromRecvList.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_fromRecvList.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (txt_ToRecvList.Text == "")
                {
                    if (Commonfunction.isValidDate(txt_ToRecvList.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_ToRecvList.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                List<IPDrugIndentData> objdeposit = GetRecvList(0);
                if (objdeposit.Count > 0)
                {
                    txt_totRecvList.Text = Commonfunction.Getrounding(objdeposit[0].TotReceived.ToString());
                    gvReceivedlist.DataSource = objdeposit;
                    gvReceivedlist.DataBind();
                    gvReceivedlist.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    btnprint.Attributes.Remove("disabled");

                }
                else
                {
                    gvReceivedlist.DataSource = null;
                    gvReceivedlist.DataBind();
                    gvReceivedlist.Visible = true;

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
        public List<IPDrugIndentData> GetRecvList(int curIndex)
        {
            IPDrugIndentData objstock = new IPDrugIndentData();
            IPDrugIndentBO objBO = new IPDrugIndentBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
         
            objstock.ReceivedBy = Convert.ToInt32(ddl_rcvBy.SelectedValue == "" ? "0" : ddl_rcvBy.SelectedValue);
            objstock.IndStatus = Convert.ToInt32(ddl_status.SelectedValue == "" ? "0" : ddl_status.SelectedValue);
            DateTime from = txt_fromRecvList.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_fromRecvList.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_ToRecvList.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_ToRecvList.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            return objBO.GetRecvList(objstock);
        }

        protected void btnresets_Click(object sender, EventArgs e)
        {
            ddl_status.SelectedIndex = 0;
            ddl_rcvBy.SelectedIndex = 0;
            txt_ToRecvList.Text = "";
            txt_fromRecvList.Text = "";
            gvReceivedlist.DataSource = null;
            gvReceivedlist.DataBind();
            gvReceivedlist.Visible = false;
            lblmessage2.Visible = false;
            divmsg2.Visible = false;
            btnexport.Visible = false;
            ddlexport.Visible = false;
            divresult1.Visible = false;
            lblresult1.Visible = false;
            txt_totRecvList.Text = "0";
            btnprint.Attributes["disabled"] = "disabled";
        }

        protected void gvReceivedlist_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    GridViewRow gr = gvReceivedlist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    Label indNo = (Label)gr.Cells[0].FindControl("lbl_Indentno");
                    Label IndentState = (Label)gr.Cells[0].FindControl("lblstatus");
                    //if (IndentState.Text.Trim() == "Approved")
                    //{
                    //    Messagealert_.ShowMessage(lblresult1, "Approved", 0);
                    //    div3.Visible = true;
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
                        bindReceivedList();

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

        protected void gvHandoverlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void gvReceivedlist_PageIndexChanging(object sender, GridViewRowEventArgs e)
        {

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
                    gvReceivedlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvReceivedlist.Columns[4].Visible = false;
                    gvReceivedlist.Columns[5].Visible = false;
                  

                    gvReceivedlist.RenderControl(hw);
                    gvReceivedlist.HeaderRow.Style.Add("width", "15%");
                    gvReceivedlist.HeaderRow.Style.Add("font-size", "10px");
                    gvReceivedlist.Style.Add("text-decoration", "none");
                    gvReceivedlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvReceivedlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=OTRolesDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=OTRolesDetails.xlsx");
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
            List<IPDrugIndentData> ReceivedDetails = GetRecvList(0);
            List<IPDrugIndentDataToExcel> ListexcelData = new List<IPDrugIndentDataToExcel>();
            int i = 0;
            foreach (IPDrugIndentData row in ReceivedDetails)
            {
                IPDrugIndentDataToExcel ExcelSevice = new IPDrugIndentDataToExcel();
                ExcelSevice.IndentNo = ReceivedDetails[i].IndentNo;
                ExcelSevice.TotReceived = ReceivedDetails[i].TotReceived;
                ExcelSevice.IndentRaiseDate = ReceivedDetails[i].IndentRaiseDate;
                ExcelSevice.RecdBy = ReceivedDetails[i].EmpName;
                gvReceivedlist.Columns[5].Visible = false;
               
                ListexcelData.Add(ExcelSevice);
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