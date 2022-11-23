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
    public partial class GENIndentCollection : BasePage
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
            Commonfunction.PopulateDdl(ddl_dept, mstlookup.GetLookupsList(LookupName.Department));
            Commonfunction.PopulateDdl(ddl_RequestTypeRecv, mstlookup.GetLookupsList(LookupName.requestType));
            //ddl_dept.SelectedIndex = 1;
            // dropdown for tab 2
            Commonfunction.PopulateDdl(ddl_deptList, mstlookup.GetLookupsList(LookupName.Department));
            Commonfunction.PopulateDdl(ddl_rcvBy, mstlookup.GetLookupsList(LookupName.StoreEmp));
            Commonfunction.PopulateDdl(ddl_status, mstlookup.GetLookupsList(LookupName.IndentStatus));
            Commonfunction.Insertzeroitemindex(ddl_user);
            //Commonfunction.PopulateDdl(ddl_user, mstlookup.GetLookupsList(LookupName.StoreEmp));
            Commonfunction.PopulateDdl(ddl_HndovTo, mstlookup.GetLookupsList(LookupName.StoreEmp));
            Commonfunction.PopulateDdl(ddl_HndovToList, mstlookup.GetLookupsList(LookupName.StoreEmp));
            //ddl_deptList.SelectedIndex = 1;
            ddl_status.SelectedIndex = 4;
            btnSaveRecv.Attributes["disabled"] = "disabled";
            btnPrintRecv.Attributes["disabled"] = "disabled";


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
                    Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }


                if (txt_fromRecv.Text == "")
                {
                    if (Commonfunction.isValidDate(txt_fromRecv.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_fromRecv.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (txt_ToRecv.Text == "")
                {
                    if (Commonfunction.isValidDate(txt_ToRecv.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_ToRecv.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                List<GenIndentData> objdeposit = bindIndentRecvList(0);
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
        public List<GenIndentData> bindIndentRecvList(int curIndex)
        {
            GenIndentData objstock = new GenIndentData();
            GENindentCollectionBO objBO = new GENindentCollectionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objstock.DeptID = Convert.ToInt32(ddl_dept.SelectedValue == "" ? "0" : ddl_dept.SelectedValue);
            objstock.IndentRequestID = Convert.ToInt32(ddl_RequestTypeRecv.SelectedValue == "" ? "0" : ddl_RequestTypeRecv.SelectedValue);
            DateTime from = txt_fromRecv.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_fromRecv.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_ToRecv.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_ToRecv.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            return objBO.bindIndentRecvList(objstock);
        }
        protected void gvHndOvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                if (e.CommandName == "lnkSelectRecv")
                {
                    GenIndentData objbill = new GenIndentData();
                    GENindentCollectionBO objstdBO = new GENindentCollectionBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvHndOvList.Rows[i];
                    Label Indno = (Label)gr.Cells[0].FindControl("lbl_IndnoHndOv");
                    Label deptID = (Label)gr.Cells[0].FindControl("lbl_deptID");
                    objbill.IndentNo = Indno.Text;
                    hdnIndNo.Value = Indno.Text;
                    List<GenIndentData> List = new List<GenIndentData>();
                    List = objstdBO.GetHndoverDetail(objbill);
                    if (List.Count > 0)
                    {
                        MasterLookupBO mstlookup = new MasterLookupBO();
                        Commonfunction.PopulateDdl(ddl_user, mstlookup.GetUserByDeptID(Convert.ToInt32(deptID.Text)));
                        txt_totApprvRecv.Text = Commonfunction.Getrounding(List[0].TotApproved.ToString());
                        txt_totHandOvRecv.Text = Commonfunction.Getrounding(List[0].TotHandOver.ToString());
                        txt_totRecv.Text = Commonfunction.Getrounding(List[0].TotRequestQty.ToString());
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
                        Commonfunction.Insertzeroitemindex(ddl_user);
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

                Label IndentID = (Label)e.Row.FindControl("lbl_IndnoHndOv");
                Label status = (Label)e.Row.FindControl("lblReqTypestatus");
                if (status.Text.Contains("Urgency"))
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.YellowGreen;
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
            List<GenIndentData> List = new List<GenIndentData>();
            GENindentCollectionBO objBO = new GENindentCollectionBO();
            GenIndentData objrec = new GenIndentData();
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
                    if (Convert.ToInt32(Recv.Text) == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage6, "ReceivedQty", 0);
                        div6.Visible = true;
                        div6.Attributes["class"] = "FailAlert";
                        return;
                    }
                    GenIndentData obj = new GenIndentData();
                    obj.IndentNo = IndentNo.Text;
                    obj.ItemID = Convert.ToInt64(ItemID.Text);
                    obj.IndentID = Convert.ToInt64(IndentID.Text);
                    obj.StockID = Convert.ToInt64(StkID.Text);
                    obj.BalStock = Convert.ToInt32(availQty.Text);
                    obj.ReqdQty = Convert.ToInt32(reqQty.Text);
                    obj.apprvQty = Convert.ToInt32(apprvQty.Text);
                    //obj.HndQty = Convert.ToInt32(HndQty.Text);
                    obj.RecvQty = Convert.ToInt32(Recv.Text);
                    obj.ID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
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
                if (ddl_user.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage6, "ApprvBy", 0);
                    div6.Visible = true;
                    div6.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage6.Visible = false;

                }
                objrec.XMLData = XmlConvertor.GENindentCollectionRecordDatatoXML(List).ToString();
                objrec.TotReceived = Convert.ToInt32(txt_totRecv.Text == "" ? "0" : txt_totRecv.Text);
                objrec.ReceivedBy = Convert.ToInt64(ddl_user.SelectedValue == "" ? "0" : ddl_user.SelectedValue);
                objrec.HandOverBy = Convert.ToInt64(ddl_HndovTo.SelectedValue == "" ? "0" : ddl_HndovTo.SelectedValue);
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
            txt_fromRecv.Text = "";
            txt_ToRecv.Text = "";
            ddl_dept.SelectedIndex = 0;
            ddl_RequestTypeRecv.SelectedIndex = 0;
            ddl_user.SelectedIndex = 0;
            ddl_HndovTo.SelectedIndex = 0;
            gvHndetail.DataSource = null;
            gvHndetail.DataBind();
            gvHndetail.Visible = false;
            lblmessage6.Visible = false;
            div6.Visible = false;
            txt_totRecv.Text = "";
            txt_totApprvRecv.Text = "";
            txt_totHandOvRecv.Text = "";
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
        protected void btn_save_Click(object sender, EventArgs e)
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
                    Messagealert_.ShowMessage(lblmessage6, "SearchEnable", 0);
                    div6.Visible = true;
                    div6.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage6.Visible = false;
                }


                if (txt_fromRecvList.Text == "")
                {
                    if (Commonfunction.isValidDate(txt_fromRecvList.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage6, "ValidDate", 0);
                        div6.Attributes["class"] = "FailAlert";
                        div6.Visible = true;
                        txt_fromRecvList.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage6.Visible = false;
                }
                if (txt_ToRecvList.Text == "")
                {
                    if (Commonfunction.isValidDate(txt_ToRecvList.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage6, "ValidDate", 0);
                        div6.Attributes["class"] = "FailAlert";
                        div6.Visible = true;
                        txt_ToRecvList.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage6.Visible = false;
                }
                List<GenIndentData> objdeposit = GetRecvList(0);
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
                Messagealert_.ShowMessage(lblmessage6, "system", 0);
                div6.Attributes["class"] = "FailAlert";
                div6.Visible = true;
            }
        }
        public List<GenIndentData> GetRecvList(int curIndex)
        {
            GenIndentData objstock = new GenIndentData();
            GENindentCollectionBO objBO = new GENindentCollectionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objstock.DeptID = Convert.ToInt32(ddl_deptList.SelectedValue == "" ? "0" : ddl_deptList.SelectedValue);
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
            txt_fromRecvList.Text = "";
            txt_ToRecvList.Text = "";
            ddl_deptList.SelectedIndex = 1;
            ddl_rcvBy.SelectedIndex = 0;
            gvReceivedlist.DataSource = null;
            gvReceivedlist.DataBind();
            gvReceivedlist.Visible = false;
            lblmessage2.Visible = false;
            divmsg2.Visible = false;
            divresult1.Visible = false;
            lblresult1.Visible = false;
            txt_totRecvList.Text = "";
            btnSaveRecv.Attributes["disabled"] = "disabled";
            btnprint.Attributes["disabled"] = "disabled";
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        protected void gvReceivedlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {

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
            List<GenIndentData> ReceivedDetails = GetRecvList(0);
            List<IndentHandOverDataToExcel> ListexcelData = new List<IndentHandOverDataToExcel>();
            int i = 0;
            foreach (GenIndentData row in ReceivedDetails)
            {
                IndentHandOverDataToExcel ExcelSevice = new IndentHandOverDataToExcel();
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