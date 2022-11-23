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
    public partial class LabSampleTypeMST : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
            }
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
                if (txt_labSamplecode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_labSamplecode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_SampleDescription.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Description", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_SampleDescription.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                LabSampleData objPaymentTypeMasterData = new LabSampleData();
                LabSampleBO objPaymentTypeMasterBO = new LabSampleBO();
                objPaymentTypeMasterData.LabSampleTypeCode = txt_labSamplecode.Text == "" ? null : txt_labSamplecode.Text;
                objPaymentTypeMasterData.LabSampleType = txt_SampleDescription.Text == "" ? null : txt_SampleDescription.Text;
                objPaymentTypeMasterData.EmployeeID = LogData.EmployeeID;
                objPaymentTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objPaymentTypeMasterData.HospitalID = LogData.HospitalID;
                objPaymentTypeMasterData.FinancialYearID = LogData.FinancialYearID;
                objPaymentTypeMasterData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_labSamplecode.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    objPaymentTypeMasterData.ActionType = Enumaction.Update;
                    objPaymentTypeMasterData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objPaymentTypeMasterBO.UpdateSampleTypeDetails(objPaymentTypeMasterData);  // funtion at DAL
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
        protected void GvLabSampleType_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    LabSampleData objPaymentTypeMasterData = new LabSampleData();
                    LabSampleBO objPaymentTypeMasterBO = new LabSampleBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvLabSampleType.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("code");
                    objPaymentTypeMasterData.ID = Convert.ToInt32(ID.Text);
                    objPaymentTypeMasterData.ActionType = Enumaction.Select;

                    List<LabSampleData> GetResult = objPaymentTypeMasterBO.GetSampleTypeDetailsByID(objPaymentTypeMasterData);
                    if (GetResult.Count > 0)
                    {
                        txt_labSamplecode.Text = GetResult[0].LabSampleTypeCode;
                        txt_SampleDescription.Text = GetResult[0].LabSampleType;
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
                    LabSampleData objPaymentTypeMasterData = new LabSampleData();
                    LabSampleBO objPaymentTypeMasterBO = new LabSampleBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvLabSampleType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objPaymentTypeMasterData.ID = Convert.ToInt32(ID.Text);
                    objPaymentTypeMasterData.EmployeeID = LogData.EmployeeID;
                    objPaymentTypeMasterData.ActionType = Enumaction.Delete;
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
                        objPaymentTypeMasterData.Remarks = txtremarks.Text;
                    }

                    LabSampleBO objPaymentTypeMasterBO1 = new LabSampleBO();
                    int Result = objPaymentTypeMasterBO1.DeleteSampleTypeDetailsByID(objPaymentTypeMasterData);
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
        private void bindgrid(int page)
        {
            try
            {

                List<LabSampleData> lstemp = GetLabSampleType(page);
                if (lstemp.Count > 0)
                {
                    GvLabSampleType.VirtualItemCount = lstemp[0].MaximumRows;//total item is required for custom paging
                    GvLabSampleType.PageIndex = page - 1;
            
                    GvLabSampleType.DataSource = lstemp;
                    GvLabSampleType.DataBind();
                    GvLabSampleType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvLabSampleType.DataSource = null;
                    GvLabSampleType.DataBind();
                    GvLabSampleType.Visible = true;
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
        private List<LabSampleData> GetLabSampleType(int p)
        {
            LabSampleData objPaymentTypeMasterData = new LabSampleData();
            LabSampleBO objPaymentTypeMasterBO = new LabSampleBO();
            objPaymentTypeMasterData.LabSampleTypeCode = txt_labSamplecode.Text == "" ? "" : txt_labSamplecode.Text;
            objPaymentTypeMasterData.LabSampleType = txt_SampleDescription.Text == "" ? "" : txt_SampleDescription.Text;
            objPaymentTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objPaymentTypeMasterData.CurrentIndex = p;
            return objPaymentTypeMasterBO.SearchSampleTypeDetails(objPaymentTypeMasterData);
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
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            clearall();
            lblmessage.Visible = false;
            lblresult.Visible = false;
        }
        private void clearall()
        {
            txt_labSamplecode.Text = "";
            txt_SampleDescription.Text = "";
            GvLabSampleType.DataSource = null;
            GvLabSampleType.DataBind();
            GvLabSampleType.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            ddlstatus.SelectedIndex = 0;
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
                    GvLabSampleType.AllowPaging = false;
                    GvLabSampleType.DataSource = GetLabSampleDetails(0);
                    GvLabSampleType.DataBind();
             
                    GvLabSampleType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvLabSampleType.Columns[5].Visible = false;
                    GvLabSampleType.Columns[6].Visible = false;
                    GvLabSampleType.Columns[7].Visible = false;

                    GvLabSampleType.RenderControl(hw);
                    GvLabSampleType.HeaderRow.Style.Add("width", "15%");
                    GvLabSampleType.HeaderRow.Style.Add("font-size", "10px");
                    GvLabSampleType.Style.Add("text-decoration", "none");
                    GvLabSampleType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvLabSampleType.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=LabSampleTypeDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=LabSampleTypeDetails.xlsx");
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
            List<LabSampleData> PaymentTypeDetails = GetLabSampleDetails(0);
            List<LabSampleTypeDatatoExcel> ListexcelData = new List<LabSampleTypeDatatoExcel>();
            int i = 0;
            foreach (LabSampleData row in PaymentTypeDetails)
            {
                LabSampleTypeDatatoExcel ExcelSevice = new LabSampleTypeDatatoExcel();
                ExcelSevice.ID = PaymentTypeDetails[i].ID;
                ExcelSevice.LabSampleTypeCode = PaymentTypeDetails[i].LabSampleTypeCode;
                ExcelSevice.LabSampleType = PaymentTypeDetails[i].LabSampleType;
                ExcelSevice.AddedBy = PaymentTypeDetails[i].EmpName;
                ExcelSevice.AddedDate = PaymentTypeDetails[i].AddedDate;
                GvLabSampleType.Columns[4].Visible = false;
                GvLabSampleType.Columns[5].Visible = false;
                GvLabSampleType.Columns[6].Visible = false;
                GvLabSampleType.Columns[7].Visible = false;
                ListexcelData.Add(ExcelSevice);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }

        private List<LabSampleData> GetLabSampleDetails(int p)
        {
            LabSampleData objPaymentTypeMasterData = new LabSampleData();
            LabSampleBO objPaymentTypeMasterBO = new LabSampleBO();
            objPaymentTypeMasterData.LabSampleTypeCode = txt_labSamplecode.Text == "" ? "" : txt_labSamplecode.Text;
            objPaymentTypeMasterData.LabSampleType = txt_SampleDescription.Text == "" ? "" : txt_SampleDescription.Text;
            objPaymentTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objPaymentTypeMasterBO.SearchSampleDetails(objPaymentTypeMasterData);
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

        protected void GvLabSampleType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }
    }
}