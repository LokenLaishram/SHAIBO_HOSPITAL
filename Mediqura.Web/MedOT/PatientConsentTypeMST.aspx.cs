using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.OTBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.OTData;
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
namespace Mediqura.Web.MedOT
{
    public partial class PatientConsentTypeMST : BasePage
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

                List<PatientConsentTypeData> lstemp = GetConsentType(0);
                if (lstemp.Count > 0)
                {
                    GvPatientConsentType.DataSource = lstemp;
                    GvPatientConsentType.DataBind();
                    GvPatientConsentType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvPatientConsentType.DataSource = null;
                    GvPatientConsentType.DataBind();
                    GvPatientConsentType.Visible = true;
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
        private List<PatientConsentTypeData> GetConsentType(int p)
        {
            PatientConsentTypeData objData = new PatientConsentTypeData();
            PatientConsentTypeBO objBO = new PatientConsentTypeBO();
            objData.TypeCode = txtPatientCnsTypecode.Text == "" ? "" : txtPatientCnsTypecode.Text;
            objData.Typedescp = txtPatientCnsTypeDesc.Text == "" ? "" : txtPatientCnsTypeDesc.Text;
            objData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objBO.SearchConsentType(objData);
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
                if (txtPatientCnsTypecode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtPatientCnsTypecode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtPatientCnsTypeDesc.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Description", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtPatientCnsTypeDesc.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                PatientConsentTypeData objData = new PatientConsentTypeData();
                PatientConsentTypeBO objBO = new PatientConsentTypeBO();
                objData.TypeCode = txtPatientCnsTypecode.Text == "" ? null : txtPatientCnsTypecode.Text;
                objData.Typedescp = txtPatientCnsTypeDesc.Text == "" ? null : txtPatientCnsTypeDesc.Text;
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
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txtPatientCnsTypecode.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    objData.ActionType = Enumaction.Update;
                    objData.TypeID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objBO.UpdateConsentTypeDetails(objData);  // funtion at DAL
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
            txtPatientCnsTypecode.Text = "";
            txtPatientCnsTypeDesc.Text = "";
            GvPatientConsentType.DataSource = null;
            GvPatientConsentType.DataBind();
            GvPatientConsentType.Visible = false;
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
                    GvPatientConsentType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvPatientConsentType.Columns[4].Visible = false;
                    GvPatientConsentType.Columns[5].Visible = false;
                    GvPatientConsentType.Columns[6].Visible = false;
                    GvPatientConsentType.Columns[7].Visible = false;

                    GvPatientConsentType.RenderControl(hw);
                    GvPatientConsentType.HeaderRow.Style.Add("width", "15%");
                    GvPatientConsentType.HeaderRow.Style.Add("font-size", "10px");
                    GvPatientConsentType.Style.Add("text-decoration", "none");
                    GvPatientConsentType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvPatientConsentType.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=PatientConsentTypeDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=PatientConsentTypeDetails.xlsx");
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
            List<PatientConsentTypeData> OTRoleDetails = GetConsentType(0);
            List<PatientConsentTypetoExcel> ListexcelData = new List<PatientConsentTypetoExcel>();
            int i = 0;
            foreach (PatientConsentTypeData row in OTRoleDetails)
            {
                PatientConsentTypetoExcel ExcelSevice = new PatientConsentTypetoExcel();
                ExcelSevice.TypeID = OTRoleDetails[i].TypeID;
                ExcelSevice.TypeCode = OTRoleDetails[i].TypeCode;
                ExcelSevice.Typedescp = OTRoleDetails[i].Typedescp;
                ExcelSevice.AddedBy = OTRoleDetails[i].EmpName;
                GvPatientConsentType.Columns[4].Visible = false;
                GvPatientConsentType.Columns[5].Visible = false;
                GvPatientConsentType.Columns[6].Visible = false;
                GvPatientConsentType.Columns[7].Visible = false;
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
        protected void GvPatientConsentType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvPatientConsentType.PageIndex = e.NewPageIndex;
            bindgrid();
        }

        protected void GvPatientConsentType_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    PatientConsentTypeData objData = new PatientConsentTypeData();
                    PatientConsentTypeBO objBO = new PatientConsentTypeBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvPatientConsentType.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("lblCategoryID");
                    objData.TypeID = Convert.ToInt32(ID.Text);
                    objData.ActionType = Enumaction.Select;

                    List<PatientConsentTypeData> GetResult = objBO.GetConsentTypeDetailsByID(objData);
                    if (GetResult.Count > 0)
                    {
                        txtPatientCnsTypecode.Text = GetResult[0].TypeCode;
                        txtPatientCnsTypeDesc.Text = GetResult[0].Typedescp;
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
                    PatientConsentTypeData objData = new PatientConsentTypeData();
                    PatientConsentTypeBO objBO = new PatientConsentTypeBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvPatientConsentType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblCategoryID");
                    objData.TypeID = Convert.ToInt32(ID.Text);
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

                    PatientConsentTypeBO objBO1 = new PatientConsentTypeBO();
                    int Result = objBO1.DeleteConsentTypeDetailsByID(objData);
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