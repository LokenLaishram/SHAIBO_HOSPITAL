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
    public partial class LabTestCenterMaster :BasePage
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
                if (txt_labCenterCode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_labCenterCode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_labCenterName.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Description", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_labCenterName.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                TestCenterMasterData objTestCenterMasterData = new TestCenterMasterData();
                TestCenterMasterBO objTestCenterMasterBO = new TestCenterMasterBO();
                objTestCenterMasterData.TestCenterCode = txt_labCenterCode.Text == "" ? null : txt_labCenterCode.Text;
                objTestCenterMasterData.TestCenterName = txt_labCenterName.Text == "" ? null : txt_labCenterName.Text;
                objTestCenterMasterData.EmployeeID = LogData.EmployeeID;
                objTestCenterMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objTestCenterMasterData.CenterTypeID = Convert.ToInt32(ddl_centertype.SelectedValue == "" ? "0" : ddl_centertype.SelectedValue);
                objTestCenterMasterData.HospitalID = LogData.HospitalID;
                objTestCenterMasterData.FinancialYearID = LogData.FinancialYearID;
                objTestCenterMasterData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_labCenterCode.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }

                    objTestCenterMasterData.ActionType = Enumaction.Update;
                    objTestCenterMasterData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objTestCenterMasterBO.UpdateTestCenterDetails(objTestCenterMasterData);

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
        protected void GvTestCenter_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    TestCenterMasterData objTestCenterMasterData = new TestCenterMasterData();
                    TestCenterMasterBO objTestCenterMasterBO = new TestCenterMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvTestCenter.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("lblID");
                    objTestCenterMasterData.ID = Convert.ToInt32(ID.Text);
                    objTestCenterMasterData.ActionType = Enumaction.Select;

                    List<TestCenterMasterData> GetResult = objTestCenterMasterBO.GetTestCenterDetailsByID(objTestCenterMasterData);
                    if (GetResult.Count > 0)
                    {
                        txt_labCenterCode.Text = GetResult[0].TestCenterCode;
                        txt_labCenterName.Text = GetResult[0].TestCenterName;
                        ddl_centertype.SelectedValue = GetResult[0].CenterTypeID.ToString();
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
                    TestCenterMasterData objTestCenterMasterData = new TestCenterMasterData();
                    TestCenterMasterBO objTestCenterMasterBO = new TestCenterMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvTestCenter.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    objTestCenterMasterData.ID = Convert.ToInt32(ID.Text);
                    objTestCenterMasterData.EmployeeID = LogData.EmployeeID;
                    objTestCenterMasterData.ActionType = Enumaction.Delete;
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {

                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objTestCenterMasterData.Remarks = txtremarks.Text;
                    }

                    TestCenterMasterBO objTestCenterMasterBO1 = new TestCenterMasterBO();
                    int Result = objTestCenterMasterBO1.DeleteTestCenterDetailsByID(objTestCenterMasterData);
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
        private void bindgrid()
        {
            try
            {
               
                List<TestCenterMasterData> lstemp = GetTestCenterName(0);
                if (lstemp.Count > 0)
                {
                    GvTestCenter.DataSource = lstemp;
                    GvTestCenter.DataBind();
                    GvTestCenter.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;

                }
                else
                {
                    GvTestCenter.DataSource = null;
                    GvTestCenter.DataBind();
                    GvTestCenter.Visible = true;
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
        private List<TestCenterMasterData> GetTestCenterName(int p)
        {
            TestCenterMasterData objTestCenterMasterData = new TestCenterMasterData();
            TestCenterMasterBO objTestCenterMasterBO = new TestCenterMasterBO();
            objTestCenterMasterData.TestCenterCode = txt_labCenterCode.Text == "" ? "" : txt_labCenterCode.Text;
            objTestCenterMasterData.TestCenterName = txt_labCenterName.Text == "" ? "" : txt_labCenterName.Text;
            objTestCenterMasterData.CenterTypeID = Convert.ToInt32(ddl_centertype.SelectedValue == "" ? "0" : ddl_centertype.SelectedValue);
            objTestCenterMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objTestCenterMasterBO.SearchTestCenterDetails(objTestCenterMasterData);
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
            txt_labCenterCode.Text = "";
            txt_labCenterName.Text = "";
            GvTestCenter.DataSource = null;
            GvTestCenter.DataBind();
            GvTestCenter.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            ddl_centertype.SelectedIndex = 0;
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
                    GvTestCenter.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvTestCenter.Columns[4].Visible = false;
                    GvTestCenter.Columns[5].Visible = false;
                    GvTestCenter.Columns[6].Visible = false;
                    GvTestCenter.Columns[7].Visible = false;

                    GvTestCenter.RenderControl(hw);
                    GvTestCenter.HeaderRow.Style.Add("width", "15%");
                    GvTestCenter.HeaderRow.Style.Add("font-size", "10px");
                    GvTestCenter.Style.Add("text-decoration", "none");
                    GvTestCenter.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvTestCenter.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=TestCenterDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=TestCenterDetails.xlsx");
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
            List<TestCenterMasterData> TestCenterNameDetails = GetTestCenterName(0);
            List<TestCenterDatatoExcel> ListexcelData = new List<TestCenterDatatoExcel>();
            int i = 0;
            foreach (TestCenterMasterData row in TestCenterNameDetails)
            {
                TestCenterDatatoExcel ExcelSevice = new TestCenterDatatoExcel();
                ExcelSevice.ID = TestCenterNameDetails[i].ID;
                ExcelSevice.TestCenterCode = TestCenterNameDetails[i].TestCenterCode;
                ExcelSevice.TestCenterName = TestCenterNameDetails[i].TestCenterName;
                ExcelSevice.AddedBy = TestCenterNameDetails[i].EmpName;
                ExcelSevice.AddedDate = TestCenterNameDetails[i].AddedDate;
                GvTestCenter.Columns[4].Visible = false;
                GvTestCenter.Columns[5].Visible = false;
                GvTestCenter.Columns[6].Visible = false;
                GvTestCenter.Columns[7].Visible = false;
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

        protected void GvTestCenter_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvTestCenter.PageIndex = e.NewPageIndex;
            bindgrid();
        }
    }
}