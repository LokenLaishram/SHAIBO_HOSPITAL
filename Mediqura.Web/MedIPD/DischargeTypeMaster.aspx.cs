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

namespace Mediqura.Web.MedIPD
{
    public partial class DischargeTypeMaster :BasePage 
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

                List<DischargeData> lstemp = GetDischargeType(0);
                if (lstemp.Count > 0)
                {
                    GvDisTypeType.DataSource = lstemp;
                    GvDisTypeType.DataBind();
                    GvDisTypeType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvDisTypeType.DataSource = null;
                    GvDisTypeType.DataBind();
                    GvDisTypeType.Visible = true;
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
        private List<DischargeData> GetDischargeType(int p)
        {
            DischargeData DischargeTypeData = new DischargeData();
            DischargeBO DischargeTypeBO = new DischargeBO();
            DischargeTypeData.DischargeTypeCode= txt_DisTypecode.Text == "" ? "" : txt_DisTypecode.Text;
            DischargeTypeData.DischargeTypedescp = txt_DisTypeDescription.Text == "" ? "" : txt_DisTypeDescription.Text;
            DischargeTypeData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return DischargeTypeBO.SearchDischargeDetails(DischargeTypeData);
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
                if (txt_DisTypecode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_DisTypecode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_DisTypeDescription.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Description", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_DisTypeDescription.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                DischargeData DischargeTypeData = new DischargeData();
                DischargeBO DischargeTypeBO = new DischargeBO();
                DischargeTypeData.DischargeTypeCode = txt_DisTypecode.Text == "" ? null : txt_DisTypecode.Text;
                DischargeTypeData.DischargeTypedescp = txt_DisTypeDescription.Text == "" ? null : txt_DisTypeDescription.Text;
                DischargeTypeData.EmployeeID = LogData.EmployeeID;
                DischargeTypeData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                DischargeTypeData.HospitalID = LogData.HospitalID;
                DischargeTypeData.FinancialYearID = LogData.FinancialYearID;
                DischargeTypeData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_DisTypecode.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    DischargeTypeData.ActionType = Enumaction.Update;
                    DischargeTypeData.DischargeTypeID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = DischargeTypeBO.UpdateDischargeDetails(DischargeTypeData);  // funtion at DAL
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
            txt_DisTypecode.Text = "";
            txt_DisTypeDescription.Text = "";
            GvDisTypeType.DataSource = null;
            GvDisTypeType.DataBind();
            GvDisTypeType.Visible = false;
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
                    GvDisTypeType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvDisTypeType.Columns[4].Visible = false;
                    GvDisTypeType.Columns[5].Visible = false;
                    GvDisTypeType.Columns[6].Visible = false;
                    GvDisTypeType.Columns[7].Visible = false;

                    GvDisTypeType.RenderControl(hw);
                    GvDisTypeType.HeaderRow.Style.Add("width", "15%");
                    GvDisTypeType.HeaderRow.Style.Add("font-size", "10px");
                    GvDisTypeType.Style.Add("text-decoration", "none");
                    GvDisTypeType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvDisTypeType.Style.Add("font-size", "8px");
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
            List<DischargeData> OTRoleDetails = GetDischargeType(0);
            List<DischargeDatatoExcel> ListexcelData = new List<DischargeDatatoExcel>();
            int i = 0;
            foreach (DischargeData row in OTRoleDetails)
            {
                DischargeDatatoExcel ExcelSevice = new DischargeDatatoExcel();
                ExcelSevice.DischargeTypeID = OTRoleDetails[i].DischargeTypeID;
                ExcelSevice.DischargeTypeCode = OTRoleDetails[i].DischargeTypeCode;
                ExcelSevice.DischargeTypedescp = OTRoleDetails[i].DischargeTypedescp;
                ExcelSevice.AddedBy = OTRoleDetails[i].EmpName;
                GvDisTypeType.Columns[4].Visible = false;
                GvDisTypeType.Columns[5].Visible = false;
                GvDisTypeType.Columns[6].Visible = false;
                GvDisTypeType.Columns[7].Visible = false;
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
        protected void GvDisTypeType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvDisTypeType.PageIndex = e.NewPageIndex;
            bindgrid();
        }

        protected void GvDisTypeType_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    DischargeData DischargeTypeData = new DischargeData();
                    DischargeBO DischargeTypeBO = new DischargeBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvDisTypeType.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("code");
                    DischargeTypeData.DischargeTypeID = Convert.ToInt32(ID.Text);
                    DischargeTypeData.ActionType = Enumaction.Select;

                    List<DischargeData> GetResult = DischargeTypeBO.GetDischargeDetailsByID(DischargeTypeData);
                    if (GetResult.Count > 0)
                    {
                        txt_DisTypecode.Text = GetResult[0].DischargeTypeCode;
                        txt_DisTypeDescription.Text = GetResult[0].DischargeTypedescp;
                        ViewState["ID"] = GetResult[0].DischargeTypeID;
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
                    DischargeData DischargeTypeData = new DischargeData();
                    DischargeBO DischargeTypeBO = new DischargeBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvDisTypeType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    DischargeTypeData.DischargeTypeID = Convert.ToInt32(ID.Text);
                    DischargeTypeData.EmployeeID = LogData.EmployeeID;
                    DischargeTypeData.ActionType = Enumaction.Delete;
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
                        DischargeTypeData.Remarks = txtremarks.Text;
                    }

                    DischargeBO DischargeTypeBO1 = new DischargeBO();
                    int Result = DischargeTypeBO1.DeleteDischargeDetailsByID(DischargeTypeData);
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