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
    public partial class CountryMST :BasePage
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
                List<CountryData> lstemp = GetCountry(0);
                if (lstemp.Count > 0)
                {
                    GvCountry.DataSource = lstemp;
                    GvCountry.DataBind();
                    GvCountry.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvCountry.DataSource = null;
                    GvCountry.DataBind();
                    GvCountry.Visible = true;
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
        private List<CountryData> GetCountry(int p)
        {
            CountryData objCountryMSTData = new CountryData();
            CountryBO objCountryMSTBO = new CountryBO();
            objCountryMSTData.CountryCode = txt_Countrycode.Text == "" ? "" : txt_Countrycode.Text;
            objCountryMSTData.Countrydescp = txt_CountryDescp.Text == "" ? "" : txt_CountryDescp.Text;
            objCountryMSTData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objCountryMSTBO.SearchCountryDetails(objCountryMSTData);
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
                if (txt_Countrycode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_Countrycode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_CountryDescp.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Description", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_CountryDescp.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                CountryData objCountryMSTData = new CountryData();
                CountryBO objCountryMSTBO = new CountryBO();
                objCountryMSTData.CountryCode = txt_Countrycode.Text == "" ? null : txt_Countrycode.Text;
                objCountryMSTData.Countrydescp = txt_CountryDescp.Text == "" ? null : txt_CountryDescp.Text;
                objCountryMSTData.EmployeeID = LogData.EmployeeID;
                objCountryMSTData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objCountryMSTData.HospitalID = LogData.HospitalID;
                objCountryMSTData.FinancialYearID = LogData.FinancialYearID;
                objCountryMSTData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_Countrycode.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    objCountryMSTData.ActionType = Enumaction.Update;
                    objCountryMSTData.CountryID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objCountryMSTBO.UpdateCountryDetails(objCountryMSTData);  // funtion at DAL
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

        protected void btnresets_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            clearall();
            lblmessage.Visible = false;
            lblresult.Visible = false;
        }
        private void clearall()
        {
            txt_Countrycode.Text = "";
            txt_CountryDescp.Text = "";
            GvCountry.DataSource = null;
            GvCountry.DataBind();
            GvCountry.Visible = false;
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
            bindgrid();
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
                    GvCountry.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvCountry.Columns[4].Visible = false;
                    GvCountry.Columns[5].Visible = false;
                    GvCountry.Columns[6].Visible = false;
                    GvCountry.Columns[7].Visible = false;

                    GvCountry.RenderControl(hw);
                    GvCountry.HeaderRow.Style.Add("width", "15%");
                    GvCountry.HeaderRow.Style.Add("font-size", "10px");
                    GvCountry.Style.Add("text-decoration", "none");
                    GvCountry.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvCountry.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=CountryDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=CountryDetails.xlsx");
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
            List<CountryData> labunitDetails = GetCountry(0);
            List<CountryDatatoEXCEL> ListexcelData = new List<CountryDatatoEXCEL>();
            int i = 0;
            foreach (CountryData row in labunitDetails)
            {
                CountryDatatoEXCEL ExcelSevice = new CountryDatatoEXCEL();
                ExcelSevice.CountryID = labunitDetails[i].CountryID;
                ExcelSevice.CountryCode = labunitDetails[i].CountryCode;
                ExcelSevice.Countrydescp = labunitDetails[i].Countrydescp;
                ExcelSevice.AddedBy = labunitDetails[i].EmpName;
                GvCountry.Columns[4].Visible = false;
                GvCountry.Columns[5].Visible = false;
                GvCountry.Columns[6].Visible = false;
                GvCountry.Columns[7].Visible = false;
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
        protected void GvCountry_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    CountryData objCountryMSTData = new CountryData();
                    CountryBO objCountryMSTBO = new CountryBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvCountry.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("code");
                    objCountryMSTData.CountryID = Convert.ToInt32(ID.Text);
                    objCountryMSTData.ActionType = Enumaction.Select;

                    List<CountryData> GetResult = objCountryMSTBO.GetCountryDetailsByID(objCountryMSTData);
                    if (GetResult.Count > 0)
                    {
                        txt_Countrycode.Text = GetResult[0].CountryCode;
                        txt_CountryDescp.Text = GetResult[0].Countrydescp;
                        ViewState["ID"] = GetResult[0].CountryID;
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
                    CountryData objCountryMSTData = new CountryData();
                    CountryBO objCountryMSTBO = new CountryBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvCountry.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objCountryMSTData.CountryID = Convert.ToInt32(ID.Text);
                    objCountryMSTData.EmployeeID = LogData.EmployeeID;
                    objCountryMSTData.ActionType = Enumaction.Delete;
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
                        objCountryMSTData.Remarks = txtremarks.Text;
                    }

                    CountryBO objCountryMSTBO1 = new CountryBO();
                    int Result = objCountryMSTBO1.DeleteCountryDetailsByID(objCountryMSTData);
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

        protected void GvCountry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvCountry.PageIndex = e.NewPageIndex;
            bindgrid();
        }
    }
}