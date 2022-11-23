using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedStore;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.CommonData.MedStore;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using Mediqura.Web.MedStore;
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

namespace Mediqura.Web.MedStore
{
    public partial class IndentStatusMaster : BasePage
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

                List<IndentStatusMasterData> lstemp = GetStoreUnits(0);
                if (lstemp.Count > 0)
                {
                    GvIndentStatus.DataSource = lstemp;
                    GvIndentStatus.DataBind();
                    GvIndentStatus.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvIndentStatus.DataSource = null;
                    GvIndentStatus.DataBind();
                    GvIndentStatus.Visible = true;
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
        private List<IndentStatusMasterData> GetStoreUnits(int p)
        {

            IndentStatusMasterData objIndentStatusMasterData = new IndentStatusMasterData();
            IndentStatusBO objIndentStatusBO = new IndentStatusBO();
            objIndentStatusMasterData.IndentCode = txt_Indentstatucode.Text == "" ? "" : txt_Indentstatucode.Text;
            objIndentStatusMasterData.Indentdescp = txt_IndentstatuDescription.Text == "" ? "" : txt_IndentstatuDescription.Text;
            objIndentStatusMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objIndentStatusBO.SearchIndentStatusDetails(objIndentStatusMasterData);
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
                if (txt_Indentstatucode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_Indentstatucode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_IndentstatuDescription.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Description", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_IndentstatuDescription.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                IndentStatusMasterData objIndentStatusMasterData = new IndentStatusMasterData();
                IndentStatusBO objIndentStatusBO = new IndentStatusBO();
                objIndentStatusMasterData.IndentCode = txt_Indentstatucode.Text == "" ? null : txt_Indentstatucode.Text;
                objIndentStatusMasterData.Indentdescp = txt_IndentstatuDescription.Text == "" ? null : txt_IndentstatuDescription.Text;
                objIndentStatusMasterData.EmployeeID = LogData.EmployeeID;
                objIndentStatusMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objIndentStatusMasterData.HospitalID = LogData.HospitalID;
                objIndentStatusMasterData.FinancialYearID = LogData.FinancialYearID;
                objIndentStatusMasterData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_Indentstatucode.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    objIndentStatusMasterData.ActionType = Enumaction.Update;
                    objIndentStatusMasterData.IndentID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objIndentStatusBO.UpdateIndentStatusDetails(objIndentStatusMasterData);  // funtion at DAL
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
            txt_Indentstatucode.Text = "";
            txt_IndentstatuDescription.Text = "";
            GvIndentStatus.DataSource = null;
            GvIndentStatus.DataBind();
            GvIndentStatus.Visible = false;
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
                    GvIndentStatus.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvIndentStatus.Columns[4].Visible = false;
                    GvIndentStatus.Columns[5].Visible = false;
                    GvIndentStatus.Columns[6].Visible = false;
                    GvIndentStatus.Columns[7].Visible = false;

                    GvIndentStatus.RenderControl(hw);
                    GvIndentStatus.HeaderRow.Style.Add("width", "15%");
                    GvIndentStatus.HeaderRow.Style.Add("font-size", "10px");
                    GvIndentStatus.Style.Add("text-decoration", "none");
                    GvIndentStatus.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvIndentStatus.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=IndentStatusDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=IndentStatusDetails.xlsx");
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
            List<IndentStatusMasterData> StoreUnitDetails = GetStoreUnits(0);
            List<IndentStatusDatatoExcel> ListexcelData = new List<IndentStatusDatatoExcel>();
            int i = 0;
            foreach (IndentStatusMasterData row in StoreUnitDetails)
            {
                IndentStatusDatatoExcel ExcelSevice = new IndentStatusDatatoExcel();
                ExcelSevice.IndentID = StoreUnitDetails[i].IndentID;
                ExcelSevice.IndentCode = StoreUnitDetails[i].IndentCode;
                ExcelSevice.Indentdescp = StoreUnitDetails[i].Indentdescp;
                ExcelSevice.AddedBy = StoreUnitDetails[i].EmpName;
                GvIndentStatus.Columns[4].Visible = false;
                GvIndentStatus.Columns[5].Visible = false;
                GvIndentStatus.Columns[6].Visible = false;
                GvIndentStatus.Columns[7].Visible = false;
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
        protected void GvIndentStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvIndentStatus.PageIndex = e.NewPageIndex;
            bindgrid();
        }

        protected void GvIndentStatus_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    IndentStatusMasterData objIndentStatusMasterData = new IndentStatusMasterData();
                    IndentStatusBO objIndentStatusBO = new IndentStatusBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvIndentStatus.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("code");
                    objIndentStatusMasterData.IndentID = Convert.ToInt32(ID.Text);
                    objIndentStatusMasterData.ActionType = Enumaction.Select;

                    List<IndentStatusMasterData> GetResult = objIndentStatusBO.GetIndentStatusDetailsByID(objIndentStatusMasterData);
                    if (GetResult.Count > 0)
                    {
                        txt_Indentstatucode.Text = GetResult[0].IndentCode;
                        txt_IndentstatuDescription.Text = GetResult[0].Indentdescp;
                        ViewState["ID"] = GetResult[0].IndentID;
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
                    IndentStatusMasterData objIndentStatusMasterData = new IndentStatusMasterData();
                    IndentStatusBO objIndentStatusBO = new IndentStatusBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvIndentStatus.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objIndentStatusMasterData.IndentID = Convert.ToInt32(ID.Text);
                    objIndentStatusMasterData.EmployeeID = LogData.EmployeeID;
                    objIndentStatusMasterData.ActionType = Enumaction.Delete;
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
                        objIndentStatusMasterData.Remarks = txtremarks.Text;
                    }

                    IndentStatusBO objIndentStatusBO1 = new IndentStatusBO();
                    int Result = objIndentStatusBO1.DeleteIndentStatusDetailsByID(objIndentStatusMasterData);
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