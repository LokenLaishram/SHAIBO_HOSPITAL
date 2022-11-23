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
    public partial class EmployeeTypeMST : BasePage
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

                List<EmpTypeData> lstemp = GetEmployeeType(0);
                if (lstemp.Count > 0)
                {
                    GvEmployeeType.DataSource = lstemp;
                    GvEmployeeType.DataBind();
                    GvEmployeeType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvEmployeeType.DataSource = null;
                    GvEmployeeType.DataBind();
                    GvEmployeeType.Visible = true;
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
        private List<EmpTypeData> GetEmployeeType(int p)
        {
            EmpTypeData objOTRoleMasterData = new EmpTypeData();
            EmployeeTypeBO objOTRoleMasterBO = new EmployeeTypeBO();
            objOTRoleMasterData.TypeCode = txtEmployeeTypecode.Text == "" ? "" : txtEmployeeTypecode.Text;
            objOTRoleMasterData.Typedescp = txtEmployeeTypeDesc.Text == "" ? "" : txtEmployeeTypeDesc.Text;
            objOTRoleMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objOTRoleMasterBO.SearchEmployeeType(objOTRoleMasterData);
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
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
                if (txtEmployeeTypecode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtEmployeeTypecode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtEmployeeTypeDesc.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Description", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtEmployeeTypeDesc.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                EmpTypeData objOTRoleMasterData = new EmpTypeData();
                EmployeeTypeBO objOTRoleMasterBO = new EmployeeTypeBO();
                objOTRoleMasterData.TypeCode = txtEmployeeTypecode.Text == "" ? null : txtEmployeeTypecode.Text;
                objOTRoleMasterData.Typedescp = txtEmployeeTypeDesc.Text == "" ? null : txtEmployeeTypeDesc.Text;
                objOTRoleMasterData.EmployeeID = LogData.EmployeeID;
                objOTRoleMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objOTRoleMasterData.HospitalID = LogData.HospitalID;
                objOTRoleMasterData.FinancialYearID = LogData.FinancialYearID;
                objOTRoleMasterData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txtEmployeeTypecode.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    objOTRoleMasterData.ActionType = Enumaction.Update;
                    objOTRoleMasterData.TypeID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objOTRoleMasterBO.UpdateEmplyeeTypeDetails(objOTRoleMasterData);  // funtion at DAL
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    ViewState["ID"] = null;
                    bindgrid();
                }
                else if (result == 5)
                {
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
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
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
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
            txtEmployeeTypecode.Text = "";
            txtEmployeeTypeDesc.Text = "";
            GvEmployeeType.DataSource = null;
            GvEmployeeType.DataBind();
            GvEmployeeType.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ExportEnable", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
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
                    GvEmployeeType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvEmployeeType.Columns[4].Visible = false;
                    GvEmployeeType.Columns[5].Visible = false;
                    GvEmployeeType.Columns[6].Visible = false;
                    GvEmployeeType.Columns[7].Visible = false;

                    GvEmployeeType.RenderControl(hw);
                    GvEmployeeType.HeaderRow.Style.Add("width", "15%");
                    GvEmployeeType.HeaderRow.Style.Add("font-size", "10px");
                    GvEmployeeType.Style.Add("text-decoration", "none");
                    GvEmployeeType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvEmployeeType.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=EmployeeTypeDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=EmployeeTypeDetails.xlsx");
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
            List<EmpTypeData> OTRoleDetails = GetEmployeeType(0);
            List<EmpTypetoExcel> ListexcelData = new List<EmpTypetoExcel>();
            int i = 0;
            foreach (EmpTypeData row in OTRoleDetails)
            {
                EmpTypetoExcel ExcelSevice = new EmpTypetoExcel();
                ExcelSevice.TypeID = OTRoleDetails[i].TypeID;
                ExcelSevice.TypeCode = OTRoleDetails[i].TypeCode;
                ExcelSevice.Typedescp = OTRoleDetails[i].Typedescp;
                ExcelSevice.AddedBy = OTRoleDetails[i].EmpName;
                GvEmployeeType.Columns[4].Visible = false;
                GvEmployeeType.Columns[5].Visible = false;
                GvEmployeeType.Columns[6].Visible = false;
                GvEmployeeType.Columns[7].Visible = false;
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
        protected void GvEmployeeType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvEmployeeType.PageIndex = e.NewPageIndex;
            bindgrid();
        }

        protected void GvEmployeeType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edits")
                {
                    if (LogData.EditEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "EditEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    EmpTypeData objOTRoleMasterData = new EmpTypeData();
                    EmployeeTypeBO objOTRoleMasterBO = new EmployeeTypeBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvEmployeeType.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("lblCategoryID");
                    objOTRoleMasterData.TypeID = Convert.ToInt32(ID.Text);
                    objOTRoleMasterData.ActionType = Enumaction.Select;

                    List<EmpTypeData> GetResult = objOTRoleMasterBO.GetEmployeeTypeDetailsByID(objOTRoleMasterData);
                    if (GetResult.Count > 0)
                    {
                        txtEmployeeTypecode.Text = GetResult[0].TypeCode;
                        txtEmployeeTypeDesc.Text = GetResult[0].Typedescp;
                        ViewState["ID"] = GetResult[0].TypeID;
                    }
                }
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "DeleteEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    EmpTypeData objOTRoleMasterData = new EmpTypeData();
                    EmployeeTypeBO objOTRoleMasterBO = new EmployeeTypeBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvEmployeeType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblCategoryID");
                    objOTRoleMasterData.TypeID = Convert.ToInt32(ID.Text);
                    objOTRoleMasterData.EmployeeID = LogData.EmployeeID;
                    objOTRoleMasterData.ActionType = Enumaction.Delete;
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
                        objOTRoleMasterData.Remarks = txtremarks.Text;
                    }

                    EmployeeTypeBO objOTRoleMasterBO1 = new EmployeeTypeBO();
                    int Result = objOTRoleMasterBO1.DeleteEmplyeeTypeDetailsByID(objOTRoleMasterData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SucessAlert";
                        bindgrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";

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