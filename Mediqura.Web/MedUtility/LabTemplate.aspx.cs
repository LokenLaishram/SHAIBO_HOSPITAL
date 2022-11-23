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

namespace Mediqura.Web.MedUtility
{
    public partial class LabTemplate : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
            }
        }
        private void bindgrid(int page)
        {
            try
            {
                List<LabTemplateData> lstemp = GetLabTemplate(page);
                if (lstemp.Count > 0)
                {
                    GvLabTemplateType.VirtualItemCount = lstemp[0].MaximumRows;//total item is required for custom paging
                    GvLabTemplateType.PageIndex = page - 1;
                    GvLabTemplateType.DataSource = lstemp;
                    GvLabTemplateType.DataBind();
                    GvLabTemplateType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvLabTemplateType.DataSource = null;
                    GvLabTemplateType.DataBind();
                    GvLabTemplateType.Visible = true;
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
        private List<LabTemplateData> GetLabTemplate(int p)
        {
            LabTemplateData objLabUnitMasterData = new LabTemplateData();
            LabTemplateBO objLabUnitMasterBO = new LabTemplateBO();
            objLabUnitMasterData.Code = txt_labTemplatecode.Text == "" ? "" : txt_labTemplatecode.Text;
            objLabUnitMasterData.Descriptions = txt_TemplateDescription.Text == "" ? "" : txt_TemplateDescription.Text;
            objLabUnitMasterData.TemplateName = txt_templatename.Text == "" ? "" : txt_templatename.Text;
            objLabUnitMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objLabUnitMasterData.CurrentIndex = p;
            return objLabUnitMasterBO.SearchLabTemplateDetails(objLabUnitMasterData);
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
                if (txt_labTemplatecode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_labTemplatecode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_TemplateDescription.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Description", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_TemplateDescription.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                if (txt_templatename.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Template", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_templatename.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                LabTemplateData objMasterData = new LabTemplateData();
                LabTemplateBO objMasterBO = new LabTemplateBO();
                objMasterData.Code = txt_labTemplatecode.Text == "" ? null : txt_labTemplatecode.Text;
                objMasterData.Descriptions = txt_TemplateDescription.Text == "" ? null : txt_TemplateDescription.Text;
                objMasterData.TemplateName = txt_templatename.Text == "" ? null : txt_templatename.Text;
                objMasterData.EmployeeID = LogData.EmployeeID;
                objMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objMasterData.HospitalID = LogData.HospitalID;
                objMasterData.FinancialYearID = LogData.FinancialYearID;
                objMasterData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_labTemplatecode.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    objMasterData.ActionType = Enumaction.Update;
                    objMasterData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objMasterBO.UpdateLabTemplateDetails(objMasterData);  // funtion at DAL
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    ViewState["ID"] = null;
                    bindgrid(1);
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

        protected void btnresets_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            clearall();
            lblmessage.Visible = false;
            lblresult.Visible = false;
        }
        private void clearall()
        {
            txt_labTemplatecode.Text = "";
            txt_TemplateDescription.Text = "";
            txt_templatename.Text = "";
            GvLabTemplateType.DataSource = null;
            GvLabTemplateType.DataBind();
            GvLabTemplateType.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
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
            bindgrid(1);
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
                    GvLabTemplateType.AllowPaging = false;
                    GvLabTemplateType.DataSource = GetLabTemplateDetails(0);
                    GvLabTemplateType.DataBind();
      
                    GvLabTemplateType.BorderStyle = BorderStyle.None;
                    GvLabTemplateType.Columns[6].Visible = false;
                    GvLabTemplateType.Columns[7].Visible = false;
                    GvLabTemplateType.Columns[8].Visible = false;
           
                    GvLabTemplateType.RenderControl(hw);
                    GvLabTemplateType.HeaderRow.Style.Add("width", "15%");
                    GvLabTemplateType.HeaderRow.Style.Add("font-size", "10px");
                    GvLabTemplateType.Style.Add("text-decoration", "none");
                    GvLabTemplateType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvLabTemplateType.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=LabTemplateDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=LabTemplateDetails.xlsx");
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
            List<LabTemplateData> labunitDetails = GetLabTemplateDetails(0);
            List<LabTemplateDatatoExcel> ListexcelData = new List<LabTemplateDatatoExcel>();
            int i = 0;
            foreach (LabTemplateData row in labunitDetails)
            {
                LabTemplateDatatoExcel ExcelSevice = new LabTemplateDatatoExcel();
                ExcelSevice.ID = labunitDetails[i].ID;
                ExcelSevice.Code = labunitDetails[i].Code;
                ExcelSevice.Descriptions = labunitDetails[i].Descriptions;
                ExcelSevice.Descriptions = labunitDetails[i].Descriptions;
                ExcelSevice.TemplateName = labunitDetails[i].TemplateName;
                GvLabTemplateType.Columns[6].Visible = false;
                GvLabTemplateType.Columns[7].Visible = false;
                GvLabTemplateType.Columns[8].Visible = false;
                ListexcelData.Add(ExcelSevice);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }

        private List<LabTemplateData> GetLabTemplateDetails(int p)
        {
            LabTemplateData objLabUnitMasterData = new LabTemplateData();
            LabTemplateBO objLabUnitMasterBO = new LabTemplateBO();
            objLabUnitMasterData.Code = txt_labTemplatecode.Text == "" ? "" : txt_labTemplatecode.Text;
            objLabUnitMasterData.Descriptions = txt_TemplateDescription.Text == "" ? "" : txt_TemplateDescription.Text;
            objLabUnitMasterData.TemplateName = txt_templatename.Text == "" ? "" : txt_templatename.Text;
            objLabUnitMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objLabUnitMasterBO.SearchLabTemplate(objLabUnitMasterData);
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
        protected void GvLabTemplateType_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    LabTemplateData objLabUnitMasterData = new LabTemplateData();
                    LabTemplateBO objLabUnitMasterBO = new LabTemplateBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvLabTemplateType.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("code");
                    objLabUnitMasterData.ID = Convert.ToInt32(ID.Text);
                    objLabUnitMasterData.ActionType = Enumaction.Select;

                    List<LabTemplateData> GetResult = objLabUnitMasterBO.GetLabTemplateDetailsByID(objLabUnitMasterData);
                    if (GetResult.Count > 0)
                    {
                        txt_labTemplatecode.Text = GetResult[0].Code;
                        txt_TemplateDescription.Text = GetResult[0].Descriptions;
                        txt_templatename.Text = GetResult[0].TemplateName;
              
                        ViewState["ID"] = GetResult[0].ID;
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
                    LabTemplateData objLabUnitMasterData = new LabTemplateData();
                    LabTemplateBO objLabUnitMasterBO = new LabTemplateBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvLabTemplateType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objLabUnitMasterData.ID = Convert.ToInt32(ID.Text);
                    objLabUnitMasterData.EmployeeID = LogData.EmployeeID;
                    objLabUnitMasterData.ActionType = Enumaction.Delete;
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
                        objLabUnitMasterData.Remarks = txtremarks.Text;
                    }

                    LabTemplateBO objlabUnitMasterBO1 = new LabTemplateBO();
                    int Result = objlabUnitMasterBO1.DeleteLabTemplateDetailsByID(objLabUnitMasterData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        div1.Visible = true;
                        div1.Attributes["class"] = "SucessAlert";
                        bindgrid(1);
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

        protected void GvLabTemplateType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }
    }
}