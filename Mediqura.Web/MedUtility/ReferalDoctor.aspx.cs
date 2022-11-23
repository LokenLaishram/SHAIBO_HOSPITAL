using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Drawing;
using System.Data;
using ClosedXML.Excel;
using System.Reflection;

namespace Mediqura.Web.MedUtility
{
    public partial class ReferalDoctor : BasePage
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
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                if (txt_code.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_code.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_Referal.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "ReferalDoctor", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_Referal.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtPercentage.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter referal percentage", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtPercentage.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                ReferalData objReferalData = new ReferalData();
                ReferalBO objReferalBO = new ReferalBO();
                objReferalData.Code = txt_code.Text == "" ? null : txt_code.Text;
                objReferalData.Name = txt_Referal.Text == "" ? null : txt_Referal.Text;
                objReferalData.Percentage = Convert.ToDecimal(txtPercentage.Text == "" ? "0" : txtPercentage.Text);
                objReferalData.ContactNo = txtcontact.Text == "" ? null : txtcontact.Text;
                objReferalData.Address = txtadress.Text == "" ? null : txtadress.Text;
                objReferalData.EmployeeID = LogData.EmployeeID;
                objReferalData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objReferalData.HospitalID = LogData.HospitalID;
                objReferalData.IPaddress = LogData.IPaddress;
                objReferalData.FinancialYearID = LogData.FinancialYearID;
                objReferalData.ActionType = Enumaction.Insert;
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
                        objReferalData.ActionType = Enumaction.Update;
                        objReferalData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objReferalBO.UpdateReferalDetails(objReferalData);
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
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
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
        protected void GvReferal_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    ReferalData objReferalData = new ReferalData();
                    ReferalBO objReferalBO = new ReferalBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvReferal.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("lblID");
                    objReferalData.ID = Convert.ToInt32(ID.Text);
                    objReferalData.ActionType = Enumaction.Select;

                    List<ReferalData> GetResult = objReferalBO.GetReferalDetailsByID(objReferalData);
                    if (GetResult.Count > 0)
                    {
                        txt_code.Text = GetResult[0].Code;
                        txt_Referal.Text = GetResult[0].Name;
                        txtPercentage.Text = GetResult[0].Percentage.ToString("N");
                        txtadress.Text = GetResult[0].Address;
                        txtcontact.Text = GetResult[0].ContactNo;
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
                    ReferalData objReferalData = new ReferalData();
                    ReferalBO objReferalBO = new ReferalBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvReferal.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    objReferalData.ID = Convert.ToInt32(ID.Text);
                    objReferalData.EmployeeID = LogData.EmployeeID;
                    objReferalData.ActionType = Enumaction.Delete;
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
                        objReferalData.Remarks = txtremarks.Text;
                    }

                    ReferalBO objReferalBO1 = new ReferalBO();
                    int Result = objReferalBO1.DeleteReferalDetailsByID(objReferalData);
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
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";


            }
        }
        private void bindgrid()
        {
            try
            {

                List<ReferalData> lstemp = Getreferal(0);

                if (lstemp.Count > 0)
                {
                    GvReferal.DataSource = lstemp;
                    GvReferal.DataBind();
                    GvReferal.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";

                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvReferal.DataSource = null;
                    GvReferal.DataBind();
                    GvReferal.Visible = true;
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
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";


            }
        }
        private List<ReferalData> Getreferal(int p)
        {
            ReferalData objReferalData = new ReferalData();
            ReferalBO objReferalBO = new ReferalBO();
            objReferalData.Code = txt_code.Text == "" ? "" : txt_code.Text;
            objReferalData.Name = txt_Referal.Text == "" ? "" : txt_Referal.Text;
            objReferalData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objReferalBO.SearchReferalDetails(objReferalData);
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
            txt_code.Text = "";
            txt_Referal.Text = "";
            txtPercentage.Text = "";
            txtcontact.Text = "";
            txtadress.Text = "";
            GvReferal.DataSource = null;
            GvReferal.DataBind();
            GvReferal.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvReferal.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvReferal.Columns[4].Visible = false;
                    GvReferal.Columns[5].Visible = false;
                    GvReferal.Columns[6].Visible = false;
                    GvReferal.Columns[7].Visible = false;
                    GvReferal.RenderControl(hw);
                    GvReferal.HeaderRow.Style.Add("width", "15%");
                    GvReferal.HeaderRow.Style.Add("font-size", "10px");
                    GvReferal.Style.Add("text-decoration", "none");
                    GvReferal.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvReferal.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Referal.pdf");
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
                wb.Worksheets.Add(dt, "Referal Doctor List");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Referal.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private DataTable GetDatafromDatabase()
        {
            List<ReferalData> referal = Getreferal(0);
            List<ReferalDatatoExcel> ListexcelData = new List<ReferalDatatoExcel>();
            int i = 0;
            foreach (ReferalData row in referal)
            {
                ReferalDatatoExcel ExcelSevice = new ReferalDatatoExcel();

                ExcelSevice.Code = referal[i].Code;
                ExcelSevice.Name = referal[i].Name;
                ExcelSevice.ContactNo = referal[i].ContactNo;
                ExcelSevice.Address = referal[i].Address;
                GvReferal.Columns[4].Visible = false;
                GvReferal.Columns[5].Visible = false;
                GvReferal.Columns[6].Visible = false;
                GvReferal.Columns[7].Visible = false;
                ListexcelData.Add(ExcelSevice);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
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
        protected void GvReferal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvReferal.PageIndex = e.NewPageIndex;
            bindgrid();
        }


    }
}