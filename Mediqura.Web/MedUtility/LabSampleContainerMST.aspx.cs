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
    public partial class LabSampleContainerMST : BasePage
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

                List<SampleContainerData> lstemp = Getcontainers(0);
                if (lstemp.Count > 0)
                {
                    GvSampleContainer.DataSource = lstemp;
                    GvSampleContainer.DataBind();
                    GvSampleContainer.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvSampleContainer.DataSource = null;
                    GvSampleContainer.DataBind();
                    GvSampleContainer.Visible = true;
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
        private List<SampleContainerData> Getcontainers(int p)
        {
            SampleContainerData objcontainerMasterData = new SampleContainerData();
            SampleContainerBO objcontainerMasterBO = new SampleContainerBO();
            objcontainerMasterData.Code = txt_SampleContainercode.Text == "" ? "" : txt_SampleContainercode.Text;
            objcontainerMasterData.Descriptions = txt_SampleContainerDescription.Text == "" ? "" : txt_SampleContainerDescription.Text;
            objcontainerMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objcontainerMasterBO.SearchcontainerDetails(objcontainerMasterData);
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
                if (txt_SampleContainercode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_SampleContainercode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_SampleContainerDescription.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Description", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_SampleContainerDescription.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                SampleContainerData objcontainerMasterData = new SampleContainerData();
                SampleContainerBO objcontainerMasterBO = new SampleContainerBO();
                objcontainerMasterData.Code = txt_SampleContainercode.Text == "" ? null : txt_SampleContainercode.Text;
                objcontainerMasterData.Descriptions = txt_SampleContainerDescription.Text == "" ? null : txt_SampleContainerDescription.Text;
                objcontainerMasterData.EmployeeID = LogData.EmployeeID;
                objcontainerMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objcontainerMasterData.HospitalID = LogData.HospitalID;
                objcontainerMasterData.FinancialYearID = LogData.FinancialYearID;
                objcontainerMasterData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_SampleContainercode.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    objcontainerMasterData.ActionType = Enumaction.Update;
                    objcontainerMasterData.ContainerID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objcontainerMasterBO.UpdatecontainerDetails(objcontainerMasterData);  // funtion at DAL
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
                { 
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                }
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
            txt_SampleContainercode.Text = "";
            txt_SampleContainerDescription.Text = "";
            GvSampleContainer.DataSource = null;
            GvSampleContainer.DataBind();
            GvSampleContainer.Visible = false;
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
                    GvSampleContainer.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvSampleContainer.Columns[4].Visible = false;
                    GvSampleContainer.Columns[5].Visible = false;
                    GvSampleContainer.Columns[6].Visible = false;
                    GvSampleContainer.Columns[7].Visible = false;

                    GvSampleContainer.RenderControl(hw);
                    GvSampleContainer.HeaderRow.Style.Add("width", "15%");
                    GvSampleContainer.HeaderRow.Style.Add("font-size", "10px");
                    GvSampleContainer.Style.Add("text-decoration", "none");
                    GvSampleContainer.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvSampleContainer.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=SampleContainerDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=SampleContainerDetails.xlsx");
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
            List<SampleContainerData> containerDetails = Getcontainers(0);
            List<SampleContainerDatatoExcel> ListexcelData = new List<SampleContainerDatatoExcel>();
            int i = 0;
            foreach (SampleContainerData row in containerDetails)
            {
                SampleContainerDatatoExcel ExcelSevice = new SampleContainerDatatoExcel();
                ExcelSevice.ContainerID = containerDetails[i].ContainerID;
                ExcelSevice.Code = containerDetails[i].Code;
                ExcelSevice.Descriptions = containerDetails[i].Descriptions;
                ExcelSevice.AddedBy = containerDetails[i].EmpName;
                GvSampleContainer.Columns[4].Visible = false;
                GvSampleContainer.Columns[5].Visible = false;
                GvSampleContainer.Columns[6].Visible = false;
                GvSampleContainer.Columns[7].Visible = false;
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
        protected void GvSampleContainer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvSampleContainer.PageIndex = e.NewPageIndex;
            bindgrid();
        }

        protected void GvSampleContainer_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    SampleContainerData objcontainerMasterData = new SampleContainerData();
                    SampleContainerBO objcontainerMasterBO = new SampleContainerBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvSampleContainer.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("code");
                    objcontainerMasterData.ContainerID = Convert.ToInt32(ID.Text);
                    objcontainerMasterData.ActionType = Enumaction.Select;

                    List<SampleContainerData> GetResult = objcontainerMasterBO.GetcontainerDetailsByID(objcontainerMasterData);
                    if (GetResult.Count > 0)
                    {
                        txt_SampleContainercode.Text = GetResult[0].Code;
                        txt_SampleContainerDescription.Text = GetResult[0].Descriptions;
                        ViewState["ID"] = GetResult[0].ContainerID;
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
                    SampleContainerData objcontainerMasterData = new SampleContainerData();
                    SampleContainerBO objcontainerMasterBO = new SampleContainerBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvSampleContainer.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objcontainerMasterData.ContainerID = Convert.ToInt32(ID.Text);
                    objcontainerMasterData.EmployeeID = LogData.EmployeeID;
                    objcontainerMasterData.ActionType = Enumaction.Delete;
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
                        objcontainerMasterData.Remarks = txtremarks.Text;
                    }

                    SampleContainerBO objcontainerMasterBO1 = new SampleContainerBO();
                    int Result = objcontainerMasterBO1.DeletecontainerDetailsByID(objcontainerMasterData);
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