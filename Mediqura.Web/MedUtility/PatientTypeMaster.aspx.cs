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
    public partial class PatientTypeMaster : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
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
                if (txtpatienttypecode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

     
                    txtpatienttypecode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtpatienttype.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Patient", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

     
                    txtpatienttype.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                PatientTypeMasterData objPatientTypeMasterData = new PatientTypeMasterData();
                PatientTypeMasterBO objPatientTypeMasterBO = new PatientTypeMasterBO();
                objPatientTypeMasterData.PatientTypeCode = txtpatienttypecode.Text == "" ? null : txtpatienttypecode.Text;
                objPatientTypeMasterData.PatientType = txtpatienttype.Text == "" ? null : txtpatienttype.Text;
                objPatientTypeMasterData.EmployeeID = LogData.EmployeeID;
                objPatientTypeMasterData.receivable = Convert.ToInt32(ddlreceive.SelectedValue == "0" ? null : ddlreceive.SelectedValue);
                objPatientTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objPatientTypeMasterData.HospitalID = LogData.HospitalID;
                objPatientTypeMasterData.FinancialYearID = LogData.FinancialYearID;
                objPatientTypeMasterData.IPaddress = LogData.IPaddress;
           
                objPatientTypeMasterData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        objPatientTypeMasterData.ActionType = Enumaction.Update;
                        objPatientTypeMasterData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objPatientTypeMasterBO.UpdatePatientTypeDetails(objPatientTypeMasterData);
                if (result == 1 || result == 2)
                {
                 
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
     
                    ViewState["ID"] = null;
                    bindgrid();
                }
                else if (result == 5)
                {
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";


                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }

     
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void GvPatientType_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    PatientTypeMasterData objPatientTypeMasterData = new PatientTypeMasterData();
                    PatientTypeMasterBO objPatientTypeMasterBO = new PatientTypeMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvPatientType.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("lblpatienttypeID");
                    objPatientTypeMasterData.ID = Convert.ToInt32(ID.Text);
                    objPatientTypeMasterData.ActionType = Enumaction.Select;

                    List<PatientTypeMasterData> GetResult = objPatientTypeMasterBO.GetPatientTypeDetailsByID(objPatientTypeMasterData);
                    if (GetResult.Count > 0)
                    {
                        txtpatienttypecode.Text = GetResult[0].PatientTypeCode;
                        txtpatienttype.Text = GetResult[0].PatientType;
                        ddlreceive.SelectedValue = GetResult[0].receivable.ToString();
                        ViewState["ID"] = GetResult[0].ID;
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
                    PatientTypeMasterData objPatientTypeMasterData = new PatientTypeMasterData();
                    PatientTypeMasterBO objPatientTypeMasterBO = new PatientTypeMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvPatientType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblpatienttypeID");
                    objPatientTypeMasterData.ID = Convert.ToInt32(ID.Text);
                    objPatientTypeMasterData.EmployeeID = LogData.EmployeeID;
                    objPatientTypeMasterData.ActionType = Enumaction.Delete;
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
                        objPatientTypeMasterData.Remarks = txtremarks.Text;
                    }

                    PatientTypeMasterBO objPatientTypeMasterBO1 = new PatientTypeMasterBO();
                    int Result = objPatientTypeMasterBO1.DeletePatientTypeDetailsByID(objPatientTypeMasterData);
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
        private void bindgrid()
        {
            try
            {
               
                List<PatientTypeMasterData> lstemp = GetPatientType(0);
                if (lstemp.Count > 0)
                {
                    GvPatientType.DataSource = lstemp;
                    GvPatientType.DataBind();
                    GvPatientType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                 }
                else
                {
                    GvPatientType.DataSource = null;
                    GvPatientType.DataBind();
                    GvPatientType.Visible = true;
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        private List<PatientTypeMasterData> GetPatientType(int p)
        {
            PatientTypeMasterData objPatientTypeMasterData = new PatientTypeMasterData();
            PatientTypeMasterBO objPatientTypeMasterBO = new PatientTypeMasterBO();
            objPatientTypeMasterData.PatientTypeCode = txtpatienttypecode.Text == "" ? "" : txtpatienttypecode.Text;
            objPatientTypeMasterData.PatientType = txtpatienttype.Text == "" ? "" : txtpatienttype.Text;
            objPatientTypeMasterData.receivable = Convert.ToInt32(ddlreceive.SelectedValue == "0" ? null : ddlreceive.SelectedValue);
            objPatientTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objPatientTypeMasterBO.SearchPatientTypeDetails(objPatientTypeMasterData);
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
            txtpatienttypecode.Text = "";
            txtpatienttype.Text = "";
            ddlstatus.SelectedIndex = 0;
            ddlreceive.SelectedIndex = 0;
            GvPatientType.DataSource = null;
            GvPatientType.DataBind();
            GvPatientType.Visible = false;
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
                    GvPatientType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvPatientType.Columns[4].Visible = false;
                    GvPatientType.Columns[5].Visible = false;
                    GvPatientType.Columns[6].Visible = false;
                    GvPatientType.Columns[7].Visible = false;

                    GvPatientType.RenderControl(hw);
                    GvPatientType.HeaderRow.Style.Add("width", "15%");
                    GvPatientType.HeaderRow.Style.Add("font-size", "10px");
                    GvPatientType.Style.Add("text-decoration", "none");
                    GvPatientType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvPatientType.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=PatientTypeDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=PatientTypeDetails.xlsx");
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
            List<PatientTypeMasterData> PatientTypeDetails = GetPatientType(0);
            List<PatientTypeDatatoExcel> ListexcelData = new List<PatientTypeDatatoExcel>();
            int i = 0;
            foreach (PatientTypeMasterData row in PatientTypeDetails)
            {
                PatientTypeDatatoExcel ExcelSevice = new PatientTypeDatatoExcel();
                ExcelSevice.ID = PatientTypeDetails[i].ID;
                ExcelSevice.PatientTypeCode = PatientTypeDetails[i].PatientTypeCode;
                ExcelSevice.PatientType = PatientTypeDetails[i].PatientType;
                ExcelSevice.AddedBy = PatientTypeDetails[i].EmpName;
              
                GvPatientType.Columns[5].Visible = false;
                GvPatientType.Columns[6].Visible = false;
                GvPatientType.Columns[7].Visible = false;
                GvPatientType.Columns[8].Visible = false;
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
        protected void GvPatientType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvPatientType.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        protected void GvPatientType_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblrecv = (Label)e.Row.FindControl("lblrev");
                if (lblrecv.Text == "0")
                {
                    lblrecv.Text = "No";
                }
                else
                {
                    lblrecv.Text = "Yes";
                }
            }
        }

    }
}