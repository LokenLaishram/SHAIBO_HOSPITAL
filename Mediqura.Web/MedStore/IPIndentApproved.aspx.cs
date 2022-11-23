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

namespace Mediqura.Web.MedStore
{
    public partial class IPIndentApproved : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                btnsave.Attributes["disabled"] = "disabled";
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            // dropdown for tab 1
            Commonfunction.PopulateDdl(ddl_requestType, mstlookup.GetLookupsList(LookupName.requestType));
            Commonfunction.PopulateDdl(ddl_userAppBy, mstlookup.GetLookupsList(LookupName.DiscountBy));
            Commonfunction.PopulateDdl(ddl_userHndovTo, mstlookup.GetLookupsList(LookupName.DiscountBy));
         
            // dropdown for tab 2
            //Commonfunction.PopulateDdl(ddl_substockHandOv, mstlookup.GetLookupsList(LookupName.SubStockType));
            //Commonfunction.PopulateDdl(ddl_RequestHandOv, mstlookup.GetLookupsList(LookupName.requestType));
            //ddl_substockHandOv.SelectedIndex = 1;

            // dropdown for tab 4 Handover List 
         
            Commonfunction.PopulateDdl(ddl_HandOver, mstlookup.GetLookupsList(LookupName.DiscountBy));
            Commonfunction.PopulateDdl(ddl_approvedBy, mstlookup.GetLookupsList(LookupName.DiscountBy));
        
            btnsave.Attributes["disabled"] = "disabled";
            btnprint.Attributes["disabled"] = "disabled";


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
                bindIndentList();
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
                    lblmessage2.Visible = false;
                }
                if (txt_from.Text == "")
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
                    lblmessage2.Visible = false;
                }
                if (txt_To.Text == "")
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
                    lblmessage2.Visible = false;
                }
                //foreach (GridViewRow row in gvIndentlist.Rows)
                //{

                //    DropDownList Status = (DropDownList)gvIndentlist.Rows[row.RowIndex].Cells[0].FindControl("ddlIndentstatus");
                //    Status.Enabled = false;
                //}
                List<IPDrugIndentData> objdeposit = GetIndentList(0);
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
        public List<IPDrugIndentData> GetIndentList(int curIndex)
        {
            IPDrugIndentData objstock = new IPDrugIndentData();
            IPDrugIndentBO objBO = new IPDrugIndentBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objstock.IndentRequestID = Convert.ToInt32(ddl_requestType.SelectedValue == "" ? "0" : ddl_requestType.SelectedValue);
            DateTime from = txt_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_To.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_To.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            objstock.IPNo = txt_ipno.Text == "" ? "0" : txt_ipno.Text;
            return objBO.GetIndentList(objstock);
        }
       
        protected void gvIndentRequest_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    IPDrugIndentData objbill = new IPDrugIndentData();
                    IPDrugIndentBO objstdBO = new IPDrugIndentBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvIndentRequest.Rows[i];
                    Label Indno = (Label)gr.Cells[0].FindControl("lbl_Indentno");
                    objbill.IndentNo = Indno.Text;
                    //hdnIndNo.Value = Indno.Text;
                    List<IPDrugIndentData> List = new List<IPDrugIndentData>();
                    List = objstdBO.GetIndentList1(objbill);
                    if (List.Count > 0)
                    {
                        txt_TotApprv.Text = Commonfunction.Getrounding(List[0].TotalRqty.ToString());
                        gvIndentDetail.DataSource = List;
                        gvIndentDetail.DataBind();
                        gvIndentDetail.Visible = true;
                        btnsave.Attributes.Remove("disabled");
                        foreach (GridViewRow row1 in gvIndentDetail.Rows)
                        {
                            //Label CP = (Label)gvHandoverlist.Rows[row1.RowIndex].Cells[0].FindControl("lbl_cp");

                            TextBox totqty = (TextBox)gvIndentDetail.Rows[row1.RowIndex].Cells[0].FindControl("txt_approvedqty");
                            totqty.Text = "0";
                            //txt_totcp.Text = (Convert.ToDecimal(txt_totcp.Text) + Convert.ToDecimal(CP.Text)).ToString();
                            //txt_totappreqd.Text = (Convert.ToInt32(txt_totappreqd.Text) + Convert.ToInt32(totqty.Text)).ToString();
                        }
                    }
                    else
                    {
                        gvIndentDetail.DataSource = null;
                        gvIndentDetail.DataBind();
                        gvIndentDetail.Visible = true;
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

        protected void gvIndentRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label IndentID = (Label)e.Row.FindControl("lbl_Indentno");
                Label status = (Label)e.Row.FindControl("lblReqTypestatus");
                if (status.Text.Contains("Urgency"))
                {
                    e.Row.Cells[7].BackColor = System.Drawing.Color.YellowGreen;
                }
            }
        }

        protected void gvIndentDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvIndentDetail_RowDataBound(object sender, GridViewRowEventArgs e)
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
          
            List<IPDrugIndentData> List = new List<IPDrugIndentData>();
            IPDrugIndentBO objBO = new IPDrugIndentBO();
            IPDrugIndentData objrec = new IPDrugIndentData();
            //int countcond = 0;
            try
            {
                foreach (GridViewRow row in gvIndentDetail.Rows)
                {
                    //CheckBox cb1 = (CheckBox)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                    //if (cb1.Checked)
                    //{
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label IndentNo = (Label)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_Indentno");
                    Label ItemID = (Label)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label IndentID = (Label)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label StkID = (Label)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_stockID");
                    Label availQty = (Label)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_avail");
                    Label reqQty = (Label)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_ReqQty");
                    Label ID = (Label)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label Ipno = (Label)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_IPno");
                    TextBox apprvQty = (TextBox)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("txt_approvedqty");
                    if (Convert.ToInt32(apprvQty.Text == "" ? "0" : apprvQty.Text) == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ApprovedQty", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        apprvQty.Focus();
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
                    obj.ID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    obj.IPNo = Ipno.Text;
                    //txt_TotApprv.Text = (Convert.ToInt32(txt_TotApprv.Text == "" ? "0" : txt_TotApprv.Text) + obj.apprvQty).ToString();
                    //txt_TotApprv.Text = (txt_TotApprv.Text == "" ? "0" : txt_TotApprv.Text);
                    List.Add(obj);

                    //}
                }
                if (List.Count == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Checked", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                if (ddl_userAppBy.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "ApprvBy", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;

                }
                if (ddl_userHndovTo.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "HandOverBy", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;

                }

                objrec.XMLData = XmlConvertor.PHR_IndentRecordDatatoXML(List).ToString();
                objrec.TotApproved = Convert.ToInt32(txt_TotApprv.Text == "" ? "0" : txt_TotApprv.Text);
                objrec.ApprvBy = Convert.ToInt64(ddl_userAppBy.SelectedValue == "" ? "0" : ddl_userAppBy.SelectedValue);
                objrec.HandOverTo = Convert.ToInt64(ddl_userHndovTo.SelectedValue == "" ? "0" : ddl_userHndovTo.SelectedValue);
                objrec.FinancialYearID = LogData.FinancialYearID;
                objrec.EmployeeID = LogData.EmployeeID;
                objrec.HospitalID = LogData.HospitalID;
                objrec.IPaddress = LogData.IPaddress;
                objrec.ActionType = Enumaction.Insert;

                int result = objBO.UpdateIndentDetailPHR(objrec);
                if (result > 0)
                {
                    bindIndentList();
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    btnsave.Attributes["disabled"] = "disabled";
                    btnprint.Attributes.Remove("disabled");
                    if (LogData.PrintEnable == 0)
                    {
                        btnprint.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btnprint.Attributes.Remove("disabled");
                    }
                    gvIndentDetail.DataSource = null;
                    gvIndentDetail.DataBind();
                    gvIndentDetail.Visible = false;
                   
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

        protected void btnreset_Click(object sender, EventArgs e)
        {

            ddl_userHndovTo.SelectedIndex = 0;
            ddl_userAppBy.SelectedIndex = 0;
            ddl_requestType.SelectedIndex = 0;
            txt_TotApprv.Text ="0";
            txt_ipno.Text = "";
            txt_from.Text = "";
            txt_To.Text = "";
            gvIndentDetail.DataSource = null;
            gvIndentDetail.DataBind();
            gvIndentDetail.Visible = false;
            lblmessage.Visible = false;
            divmsg1.Visible = false;
            //txt_TotApprv.Text = "";
            btnsave.Attributes["disabled"] = "disabled";
            //bindIndentList();
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
                    gvHandoverlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    //gvIndentlist.Columns[4].Visible = false;
                    //gvIndentlist.Columns[5].Visible = false;
                    gvHandoverlist.Columns[6].Visible = false;
                    gvHandoverlist.Columns[7].Visible = false;

                    gvHandoverlist.RenderControl(hw);
                    gvHandoverlist.HeaderRow.Style.Add("width", "15%");
                    gvHandoverlist.HeaderRow.Style.Add("font-size", "10px");
                    gvHandoverlist.Style.Add("text-decoration", "none");
                    gvHandoverlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvHandoverlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=IPIndentApprovedList.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=IPIndentApprovedList.xlsx");
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
            List<IPDrugIndentData> DepositDetails = GetHandOverList(0);
            List<IPDrugIndentDataToExcel> ListexcelData = new List<IPDrugIndentDataToExcel>();
            int i = 0;
            foreach (IPDrugIndentData row in DepositDetails)
            {
                IPDrugIndentDataToExcel Ecxeclpat = new IPDrugIndentDataToExcel();
                Ecxeclpat.IndentNo = DepositDetails[i].IndentNo;
                Ecxeclpat.TotHandOver = DepositDetails[i].TotHandOver;
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

        protected void btnresets_Click(object sender, EventArgs e)
        {
            ddl_approvedBy.SelectedIndex = 0;
            ddl_HandOver.SelectedIndex = 0;
            txt_ToHand.Text = "";
            txt_fromHand.Text = "";
            gvHandoverlist.DataSource = null;
            gvHandoverlist.DataBind();
            gvHandoverlist.Visible = false;
            lblmessage2.Visible = false;
            divmsg2.Visible = false;
            btnexport.Visible = false;
            ddlexport.Visible = false;
            div3.Visible = false;
            lblresult1.Visible = false;
            txt_InHandover.Text = "0";
        }

        protected void gvHandoverlist_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    GridViewRow gr = gvHandoverlist.Rows[i];
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
                    IPDrugIndentBO objIndentStatusBO1 = new IPDrugIndentBO();
                    int Result = objIndentStatusBO1.DeleteIndentReqByID(objIndentStatusData);
                    if (Result == 1)
                    {
                        lblmessage2.Visible = true;
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "SucessAlert";
                        bindHandOverList();

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
        protected void txt_approvedqty_TextChanged(object sender, EventArgs e)
        {
            txt_TotApprv.Text = "0";
            GridViewRow currentrow = (GridViewRow)((TextBox)sender).Parent.Parent;
            foreach (GridViewRow row in gvIndentDetail.Rows)
            {
                Label Reqdqty = (Label)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_ReqQty");
                TextBox Apprvqty = (TextBox)gvIndentDetail.Rows[row.RowIndex].Cells[0].FindControl("txt_approvedqty");
                if (Convert.ToInt32(Apprvqty.Text == "" ? "0" : Apprvqty.Text) > Convert.ToInt32(Reqdqty.Text == "" ? "0" : Reqdqty.Text))
                {
                    Messagealert_.ShowMessage(lblmessage, "ApproveQty", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    Apprvqty.Focus();
                    return;
                }
                else
                {
                    txt_TotApprv.Text = (Convert.ToInt32(txt_TotApprv.Text == "" ? "0" : txt_TotApprv.Text) + Convert.ToInt32(Apprvqty.Text == "" ? "0" : Apprvqty.Text)).ToString();
                    divmsg1.Visible = false;
                }

            }
        }

        protected void btnsearchList_Click(object sender, EventArgs e)
        {
            bindHandOverList();

        }
        protected void bindHandOverList()
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
                    if (Commonfunction.isValidDate(txt_fromHand.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_fromHand.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (txt_To.Text == "")
                {
                    if (Commonfunction.isValidDate(txt_ToHand.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_ToHand.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                List<IPDrugIndentData> objdeposit = GetHandOverList(0);
                if (objdeposit.Count > 0)
                {
                    txt_InHandover.Text = Commonfunction.Getrounding(objdeposit[0].TotApproved.ToString());
                    gvHandoverlist.DataSource = objdeposit;
                    gvHandoverlist.DataBind();
                    gvHandoverlist.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    btnprint.Attributes.Remove("disabled"); 

                }
                else
                {
                    gvHandoverlist.DataSource = null;
                    gvHandoverlist.DataBind();
                    gvHandoverlist.Visible = true;

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
        public List<IPDrugIndentData> GetHandOverList(int curIndex)
        {
            IPDrugIndentData objstock = new IPDrugIndentData();
            IPDrugIndentBO objBO = new IPDrugIndentBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objstock.HandOverTo = Convert.ToInt32(ddl_HandOver.SelectedValue == "" ? "0" : ddl_HandOver.SelectedValue);
            objstock.ApprvBy = Convert.ToInt32(ddl_approvedBy.SelectedValue == "" ? "0" : ddl_approvedBy.SelectedValue);
            DateTime from = txt_fromHand.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_fromHand.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_ToHand.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_ToHand.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            return objBO.GetHandOverList(objstock);
        }
        protected void gvHandoverlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

    }
}