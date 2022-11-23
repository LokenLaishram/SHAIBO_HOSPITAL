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
    public partial class LabNormalRangeMaster : BasePage
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
                if (txt_testName.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Testname", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_testName.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_units.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Unit", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_units.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_machineName.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Machine", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_machineName.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddlgender.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Gender", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddlgender.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_age.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Age", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_age.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                if (txt_Range.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Range", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_Range.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
               
                LabRangeMasterData objLabRangeMasterData = new LabRangeMasterData();
                LabRangeMasterBO objLabRangeMasterBO = new LabRangeMasterBO();
                objLabRangeMasterData.TestName = txt_testName.Text == "" ? null : txt_testName.Text;
                objLabRangeMasterData.Units = txt_units.Text == "" ? null : txt_units.Text;
                objLabRangeMasterData.Age = txt_age.Text == "" ? null : txt_age.Text;
                objLabRangeMasterData.NormalRange = txt_Range.Text == "" ? null : txt_Range.Text;
                objLabRangeMasterData.MachineName = txt_machineName.Text == "" ? null : txt_machineName.Text;
                objLabRangeMasterData.EmployeeID = LogData.EmployeeID;
                objLabRangeMasterData.Gender = ddlgender.SelectedValue;
                objLabRangeMasterData.HospitalID = LogData.HospitalID;
                objLabRangeMasterData.FinancialYearID = LogData.FinancialYearID;
                objLabRangeMasterData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_testName.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    objLabRangeMasterData.ActionType = Enumaction.Update;
                    objLabRangeMasterData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objLabRangeMasterBO.UpdateLabRangeDetails(objLabRangeMasterData);

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
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
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
        private void bindgrid()
        {
            try
            {
               

                List<LabRangeMasterData> lstemp = GetLabRangeName(0);
                if (lstemp.Count > 0)
                {
                    GvLabRange.DataSource = lstemp;
                    GvLabRange.DataBind();
                    GvLabRange.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvLabRange.DataSource = null;
                    GvLabRange.DataBind();
                    GvLabRange.Visible = true;
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
        private List<LabRangeMasterData> GetLabRangeName(int p)
        {
            LabRangeMasterData objLabRangeMasterData = new LabRangeMasterData();
            LabRangeMasterBO objLabRangeMasterBO = new LabRangeMasterBO();
            objLabRangeMasterData.TestName = txt_testName.Text == "" ? "" : txt_testName.Text;
            objLabRangeMasterData.MachineName = txt_machineName.Text == "" ? "" : txt_machineName.Text;
            return objLabRangeMasterBO.SearchLabRangeDetails(objLabRangeMasterData);
        }
        private void clearall()
        {
            txt_testName.Text = "";
            txt_units.Text = "";
            txt_machineName.Text = "";
            txt_Range.Text = "";
            txt_age.Text = "";
            ddlgender.SelectedIndex = 0;
            GvLabRange.DataSource = null;
            GvLabRange.DataBind();
            GvLabRange.Visible = false;
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
                    GvLabRange.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvLabRange.Columns[7].Visible = false;
                    GvLabRange.Columns[8].Visible = false;
                    GvLabRange.Columns[9].Visible = false;
                    GvLabRange.Columns[10].Visible = false;

                    GvLabRange.RenderControl(hw);
                    GvLabRange.HeaderRow.Style.Add("width", "15%");
                    GvLabRange.HeaderRow.Style.Add("font-size", "10px");
                    GvLabRange.Style.Add("text-decoration", "none");
                    GvLabRange.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvLabRange.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=LabNormalRangeMaster.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=LabNormalRangeMaster.xlsx");
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
            List<LabRangeMasterData> LabRangeNameDetails = GetLabRangeName(0);
            List<LabRangeDatatoExcel> ListexcelData = new List<LabRangeDatatoExcel>();
            int i = 0;
            foreach (LabRangeMasterData row in LabRangeNameDetails)
            {
                LabRangeDatatoExcel ExcelSevice = new LabRangeDatatoExcel();
                ExcelSevice.ID = LabRangeNameDetails[i].ID;
                ExcelSevice.TestName = LabRangeNameDetails[i].TestName;
                ExcelSevice.Units = LabRangeNameDetails[i].Units;
                ExcelSevice.Age = LabRangeNameDetails[i].Age;
                ExcelSevice.NormalRange = LabRangeNameDetails[i].NormalRange;
                ExcelSevice.MachineName = LabRangeNameDetails[i].MachineName;
                ExcelSevice.AddedBy = LabRangeNameDetails[i].EmpName;
                ExcelSevice.AddedDate = LabRangeNameDetails[i].AddedDate;
                GvLabRange.Columns[4].Visible = false;
                GvLabRange.Columns[5].Visible = false;
                GvLabRange.Columns[6].Visible = false;
                GvLabRange.Columns[7].Visible = false;
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
        protected void GvLabRange_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    LabRangeMasterData objLabRangeMasterData = new LabRangeMasterData();
                    LabRangeMasterBO objLabRangeMasterBO = new LabRangeMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvLabRange.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("lblID");
                    objLabRangeMasterData.ID = Convert.ToInt32(ID.Text);
                    objLabRangeMasterData.ActionType = Enumaction.Select;

                    List<LabRangeMasterData> GetResult = objLabRangeMasterBO.GetLabRangeDetailsByID(objLabRangeMasterData);
                    if (GetResult.Count > 0)
                    {
                        txt_testName.Text = GetResult[0].TestName;
                        txt_units.Text = GetResult[0].Units;
                        txt_machineName.Text = GetResult[0].MachineName;
                        txt_age.Text = GetResult[0].Age;
                        txt_Range.Text = GetResult[0].NormalRange;
                        ddlgender.SelectedValue = GetResult[0].Gender;
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
                    LabRangeMasterData objLabRangeMasterData = new LabRangeMasterData();
                    LabRangeMasterBO objLabRangeMasterBO = new LabRangeMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvLabRange.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    objLabRangeMasterData.ID = Convert.ToInt32(ID.Text);
                    objLabRangeMasterData.EmployeeID = LogData.EmployeeID;
                    objLabRangeMasterData.ActionType = Enumaction.Delete;
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
                        objLabRangeMasterData.Remarks = txtremarks.Text;
                    }

                    LabRangeMasterBO objLabRangeMasterBO1 = new LabRangeMasterBO();
                    int Result = objLabRangeMasterBO1.DeleteLabRangeDetailsByID(objLabRangeMasterData);
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

        protected void txt_testName_TextChanged(object sender, EventArgs e)
        {
            //LabRangeMasterData objLabRangeMasterData = new LabRangeMasterData();
            //LabRangeMasterBO objLabRangeMasterBO = new LabRangeMasterBO();
            //List<LabRangeMasterData> getResult = new List<LabRangeMasterData>();
            //objLabRangeMasterData.TestName = txt_testName.Text;
            //getResult = objLabRangeMasterBO.GetTestName(objLabRangeMasterData);
            //if (getResult.Count > 0)
            //{
            //    txtname.Text = getResult[0].PatientName.ToString();
            //    txtaddress.Text = getResult[0].Address.ToString();
            //    txtbalanceinac.Text = Commonfunction.Getrounding((Convert.ToDecimal(getResult[0].BalanceAmount.ToString())).ToString());
            //    Session["ServiceList"] = null;
            //}
            //else
            //{
            //    txtname.Text = "";
            //    txtaddress.Text = "";
            //    txt_testName.Text = "";
            //    txtbalanceinac.Text = "";
            //    txt_testName.Focus();
            //}
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetTestName(string prefixText, int count, string contextKey)
        {
            LabRangeMasterData ObjlabRange = new LabRangeMasterData();
            LabRangeMasterBO objInfoBO = new LabRangeMasterBO();
            List<LabRangeMasterData> getResult = new List<LabRangeMasterData>();
            ObjlabRange.TestName = prefixText;
            getResult = objInfoBO.GetTestName(ObjlabRange);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].TestName.ToString());
            }
            return list;
        }

        protected void GvLabRange_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvLabRange.PageIndex = e.NewPageIndex;
            bindgrid();
        }

    }
}