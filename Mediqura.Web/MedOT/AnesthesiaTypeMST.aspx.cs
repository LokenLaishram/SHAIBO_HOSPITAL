using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.PatientData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using OnBarcode.Barcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Reflection;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.OTData;
using Mediqura.BOL.AdmissionBO;
using Mediqura.BOL.OTBO;

namespace Mediqura.Web.MedOT
{
    public partial class AnesthesiaTypeMST : BasePage
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

                List<AnesthesiaData> lstemp = GetAnesThType(0);
                if (lstemp.Count > 0)
                {
                    GvAnesthesiaType.DataSource = lstemp;
                    GvAnesthesiaType.DataBind();
                    GvAnesthesiaType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvAnesthesiaType.DataSource = null;
                    GvAnesthesiaType.DataBind();
                    GvAnesthesiaType.Visible = true;
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
        private List<AnesthesiaData> GetAnesThType(int p)
        {
            AnesthesiaData objData = new AnesthesiaData();
            AnesthesiaBO objBO = new AnesthesiaBO();
            objData.AnesThCode = txt_Ansiacode.Text == "" ? "" : txt_Ansiacode.Text;
            objData.AnesThdescp = txt_AnsiaDescription.Text == "" ? "" : txt_AnsiaDescription.Text;
            objData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objBO.SearchAnesTHDetails(objData);
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
                if (txt_Ansiacode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_Ansiacode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_AnsiaDescription.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Description", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_AnsiaDescription.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                AnesthesiaData objData = new AnesthesiaData();
                AnesthesiaBO objBO = new AnesthesiaBO();
                objData.AnesThCode = txt_Ansiacode.Text == "" ? null : txt_Ansiacode.Text;
                objData.AnesThdescp = txt_AnsiaDescription.Text == "" ? null : txt_AnsiaDescription.Text;
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
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_Ansiacode.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    objData.ActionType = Enumaction.Update;
                    objData.AnesThID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objBO.UpdateAnesTHDetails(objData);  // funtion at DAL
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
            txt_Ansiacode.Text = "";
            txt_AnsiaDescription.Text = "";
            GvAnesthesiaType.DataSource = null;
            GvAnesthesiaType.DataBind();
            GvAnesthesiaType.Visible = false;
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
                    GvAnesthesiaType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvAnesthesiaType.Columns[4].Visible = false;
                    GvAnesthesiaType.Columns[5].Visible = false;
                    GvAnesthesiaType.Columns[6].Visible = false;
                    GvAnesthesiaType.Columns[7].Visible = false;

                    GvAnesthesiaType.RenderControl(hw);
                    GvAnesthesiaType.HeaderRow.Style.Add("width", "15%");
                    GvAnesthesiaType.HeaderRow.Style.Add("font-size", "10px");
                    GvAnesthesiaType.Style.Add("text-decoration", "none");
                    GvAnesthesiaType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvAnesthesiaType.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=AnesthesiaTypeDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=AnesthesiaTypeDetails.xlsx");
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
            List<AnesthesiaData> OTRoleDetails = GetAnesThType(0);
            List<AnesthesiaDatatoExcel> ListexcelData = new List<AnesthesiaDatatoExcel>();
            int i = 0;
            foreach (AnesthesiaData row in OTRoleDetails)
            {
                AnesthesiaDatatoExcel ExcelSevice = new AnesthesiaDatatoExcel();
                ExcelSevice.AnesThID = OTRoleDetails[i].AnesThID;
                ExcelSevice.AnesThCode = OTRoleDetails[i].AnesThCode;
                ExcelSevice.AnesThdescp = OTRoleDetails[i].AnesThdescp;
                ExcelSevice.AddedBy = OTRoleDetails[i].EmpName;
                GvAnesthesiaType.Columns[4].Visible = false;
                GvAnesthesiaType.Columns[5].Visible = false;
                GvAnesthesiaType.Columns[6].Visible = false;
                GvAnesthesiaType.Columns[7].Visible = false;
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
        protected void GvAnesthesiaType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvAnesthesiaType.PageIndex = e.NewPageIndex;
            bindgrid();
        }

        protected void GvAnesthesiaType_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    AnesthesiaData objData = new AnesthesiaData();
                    AnesthesiaBO objBO = new AnesthesiaBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvAnesthesiaType.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("code");
                    objData.AnesThID = Convert.ToInt32(ID.Text);
                    objData.ActionType = Enumaction.Select;

                    List<AnesthesiaData> GetResult = objBO.GetAnesTHDetailsByID(objData);
                    if (GetResult.Count > 0)
                    {
                        txt_Ansiacode.Text = GetResult[0].AnesThCode;
                        txt_AnsiaDescription.Text = GetResult[0].AnesThdescp;
                        ViewState["ID"] = GetResult[0].AnesThID;
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
                    AnesthesiaData objData = new AnesthesiaData();
                    AnesthesiaBO objBO = new AnesthesiaBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvAnesthesiaType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objData.AnesThID = Convert.ToInt32(ID.Text);
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

                    AnesthesiaBO objBO1 = new AnesthesiaBO();
                    int Result = objBO1.DeleteAnesTHDetailsByID(objData);
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