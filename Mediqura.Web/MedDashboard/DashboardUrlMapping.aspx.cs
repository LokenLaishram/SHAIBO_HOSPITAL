using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedDashboardBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedDashboardData;
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


namespace Mediqura.Web.MedDashboard
{
    public partial class DashboardUrlMapping : BasePage
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
            }
        }
        private void bindgrid()
        {
            try
            {

                List<DashboardUrlMapData> lstemp = GetDashboardURL(0);
                if (lstemp.Count > 0)
                {
                    GvDashBoardMapping.DataSource = lstemp;
                    GvDashBoardMapping.DataBind();
                    GvDashBoardMapping.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvDashBoardMapping.DataSource = null;
                    GvDashBoardMapping.DataBind();
                    GvDashBoardMapping.Visible = true;
                    lblresult.Visible = false;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        private List<DashboardUrlMapData> GetDashboardURL(int p)
        {
            DashboardUrlMapData objData = new DashboardUrlMapData();
            DashboardUrlMapBO objBO = new DashboardUrlMapBO();
            objData.DashboardTittle= txt_DashTittle.Text == "" ? "" : txt_DashTittle.Text;
            objData.url = txt_url.Text == "" ? "" : txt_url.Text;
            objData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objBO.SearchDashboardURLDetails(objData);
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (LogData.SaveEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "SaveEnable", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_DashTittle.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_DashTittle.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_url.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Description", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_url.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                var url = txt_url.Text.ToString();
                if (url.Contains(".") == false)
                {
                    Messagealert_.ShowMessage(lblmessage, "aspx", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_url.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                DashboardUrlMapData objData = new DashboardUrlMapData();
                DashboardUrlMapBO objBO = new DashboardUrlMapBO();
                objData.DashboardTittle = txt_DashTittle.Text == "" ? null : txt_DashTittle.Text;
                objData.url = txt_url.Text == "" ? null : txt_url.Text;
                objData.EmployeeID = LogData.EmployeeID;
                objData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objData.HospitalID = LogData.HospitalID;
                objData.FinancialYearID = LogData.FinancialYearID;
                objData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_DashTittle.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    objData.ActionType = Enumaction.Update;
                    objData.MapID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objBO.UpdateDashboardURLDetails(objData);  // funtion at DAL
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    ViewState["ID"] = null;
                    bindgrid();
                }
                else if (result == 5)
                {
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                }
                else
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            bindgrid();
        }

        protected void btnresets_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            clearall();
            lblmessage.Visible = false;
            lblresult.Visible = false;
        }
        private void clearall()
        {
            txt_DashTittle.Text = "";
            txt_url.Text = "";
            GvDashBoardMapping.DataSource = null;
            GvDashBoardMapping.DataBind();
            GvDashBoardMapping.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
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
                Messagealert_.ShowMessage(lblresult, "ExportType", 0);
                divmsg3.Visible = true;
                divmsg3.Attributes["class"] = "FailAlert";
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
                    GvDashBoardMapping.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvDashBoardMapping.Columns[4].Visible = false;
                    GvDashBoardMapping.Columns[5].Visible = false;
                    GvDashBoardMapping.Columns[6].Visible = false;
                    GvDashBoardMapping.Columns[7].Visible = false;

                    GvDashBoardMapping.RenderControl(hw);
                    GvDashBoardMapping.HeaderRow.Style.Add("width", "15%");
                    GvDashBoardMapping.HeaderRow.Style.Add("font-size", "10px");
                    GvDashBoardMapping.Style.Add("text-decoration", "none");
                    GvDashBoardMapping.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvDashBoardMapping.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=DashboardUrlMappingDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=DashboardUrlMappingDetails.xlsx");
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
            List<DashboardUrlMapData> OTRoleDetails = GetDashboardURL(0);
            List<DashboardUrlMapDatatoExcel> ListexcelData = new List<DashboardUrlMapDatatoExcel>();
            int i = 0;
            foreach (DashboardUrlMapData row in OTRoleDetails)
            {
                DashboardUrlMapDatatoExcel ExcelSevice = new DashboardUrlMapDatatoExcel();
                ExcelSevice.MapID = OTRoleDetails[i].MapID;
                ExcelSevice.DashboardTittle = OTRoleDetails[i].DashboardTittle;
                ExcelSevice.url = OTRoleDetails[i].url;
                ExcelSevice.AddedBy = OTRoleDetails[i].EmpName;
                GvDashBoardMapping.Columns[4].Visible = false;
                GvDashBoardMapping.Columns[5].Visible = false;
                GvDashBoardMapping.Columns[6].Visible = false;
                GvDashBoardMapping.Columns[7].Visible = false;
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
        protected void GvDashBoardMapping_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvDashBoardMapping.PageIndex = e.NewPageIndex;
            bindgrid();
        }

        protected void GvDashBoardMapping_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edits")
                {
                    if (LogData.EditEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "EditEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    DashboardUrlMapData objData = new DashboardUrlMapData();
                    DashboardUrlMapBO objBO = new DashboardUrlMapBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvDashBoardMapping.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("code");
                    objData.MapID = Convert.ToInt32(ID.Text);
                    objData.ActionType = Enumaction.Select;

                    List<DashboardUrlMapData> GetResult = objBO.GetDashboardURLDetailsByID(objData);
                    if (GetResult.Count > 0)
                    {
                        txt_DashTittle.Text = GetResult[0].DashboardTittle;
                        txt_url.Text = GetResult[0].url;
                        ViewState["ID"] = GetResult[0].MapID;
                    }
                }
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
                    DashboardUrlMapData objData = new DashboardUrlMapData();
                    DashboardUrlMapBO objBO = new DashboardUrlMapBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvDashBoardMapping.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objData.MapID = Convert.ToInt32(ID.Text);
                    objData.EmployeeID = LogData.EmployeeID;
                    objData.ActionType = Enumaction.Delete;
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
                        objData.Remarks = txtremarks.Text;
                    }

                    DashboardUrlMapBO objBO1 = new DashboardUrlMapBO();
                    int Result = objBO1.DeleteDashboardURLDetailsByID(objData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        div1.Visible = true;
                        div1.Attributes["class"] = "SucessAlert";
                        bindgrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";

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
        }
    
}