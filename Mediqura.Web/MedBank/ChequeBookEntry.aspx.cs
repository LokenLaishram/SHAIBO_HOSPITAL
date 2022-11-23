using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBankBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedBankData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Utility;
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
namespace Mediqura.Web.MedBank
{
    public partial class ChequeBookEntry : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();

            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_bankName, mstlookup.GetLookupsList(LookupName.bankName));
        }

        private void bindgrid()
        {
            try
            {

                List<BankMasterData> lstemp = GetBankMaster(0);
                if (lstemp.Count > 0)
                {
                    GvBankMaster.DataSource = lstemp;
                    GvBankMaster.DataBind();
                    GvBankMaster.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvBankMaster.DataSource = null;
                    GvBankMaster.DataBind();
                    GvBankMaster.Visible = true;
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
        private List<BankMasterData> GetBankMaster(int p)
        {
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            BankMasterData objBankMasterData = new BankMasterData();
            BankMasterBO objBankMasterBO = new BankMasterBO();
            objBankMasterData.BankID = Convert.ToInt32(ddl_bankName.SelectedValue == "" ? "0" : ddl_bankName.SelectedValue);
            objBankMasterData.ChequeNoFrom = Convert.ToInt32(txt_ChequeNofrom.Text == "" ? "0" : txt_ChequeNofrom.Text);
            objBankMasterData.ChequeNoTo = Convert.ToInt32(txt_ChequeNoTo.Text == "" ? "0" : txt_ChequeNoTo.Text);
            if (txt_issueDate.Text == "")
            {
                //txt_issueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                objBankMasterData.IssueDate = txt_issueDate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_issueDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            }
            else
            {
               
                DateTime issuedate = txt_issueDate.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_issueDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                objBankMasterData.IssueDate = issuedate;
            }
            objBankMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objBankMasterBO.SearchBankChkBookIssueDetails(objBankMasterData);
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
                if (ddl_bankName.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_bankName.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_issueDate.Text == "" || Commonfunction.isValidDate(txt_issueDate.Text) == false)
                {
                    Messagealert_.ShowMessage(lblmessage, "Operationdate", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_issueDate.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (txt_ChequeNofrom.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "from", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_ChequeNofrom.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_ChequeNoTo.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "to", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_ChequeNoTo.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                BankMasterData objBankMasterData = new BankMasterData();
                BankMasterBO objBankMasterBO = new BankMasterBO();
                objBankMasterData.BankID = Convert.ToInt32(ddl_bankName.SelectedValue == "0" ? null : ddl_bankName.SelectedValue);
                objBankMasterData.ChequeNoFrom = Convert.ToInt32(txt_ChequeNofrom.Text == "" ? "" : txt_ChequeNofrom.Text);
                objBankMasterData.ChequeNoTo = Convert.ToInt32(txt_ChequeNoTo.Text == "" ? "" : txt_ChequeNoTo.Text);
                objBankMasterData.EmployeeID = LogData.EmployeeID;
                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                DateTime issuedate = txt_issueDate.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_issueDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                objBankMasterData.IssueDate = issuedate;
                objBankMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objBankMasterData.HospitalID = LogData.HospitalID;
                objBankMasterData.FinancialYearID = LogData.FinancialYearID;
                objBankMasterData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        ddl_bankName.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    objBankMasterData.ActionType = Enumaction.Update;
                    objBankMasterData.ID = Convert.ToInt64(ViewState["ID"].ToString());
                }
                int result = objBankMasterBO.UpdateBankChkBookIssueDetails(objBankMasterData);  // funtion at DAL
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
            txt_ChequeNoTo.Text = "";
            txt_ChequeNofrom.Text = "";
            txt_issueDate.Text = "";
            ddl_bankName.SelectedIndex = 0;
            GvBankMaster.DataSource = null;
            GvBankMaster.DataBind();
            GvBankMaster.Visible = false;
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
                    GvBankMaster.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvBankMaster.Columns[4].Visible = false;
                    GvBankMaster.Columns[5].Visible = false;
                    GvBankMaster.Columns[6].Visible = false;
                    GvBankMaster.Columns[7].Visible = false;

                    GvBankMaster.RenderControl(hw);
                    GvBankMaster.HeaderRow.Style.Add("width", "15%");
                    GvBankMaster.HeaderRow.Style.Add("font-size", "10px");
                    GvBankMaster.Style.Add("text-decoration", "none");
                    GvBankMaster.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvBankMaster.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=BankMasterDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=BankMasterDetails.xlsx");
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
            List<BankMasterData> BankDetails = GetBankMaster(0);
            List<BankMasterDataExcel> ListexcelData = new List<BankMasterDataExcel>();
            int i = 0;
            foreach (BankMasterData row in BankDetails)
            {
                BankMasterDataExcel ExcelSevice = new BankMasterDataExcel();
                ExcelSevice.BankName = BankDetails[i].BankName;
                ExcelSevice.IssueDate = BankDetails[i].IssueDate;
                ExcelSevice.ChequeNoFrom = BankDetails[i].ChequeNoFrom;
                ExcelSevice.ChequeNoTo = BankDetails[i].ChequeNoTo;
                ExcelSevice.AddedBy = BankDetails[i].EmpName;
                GvBankMaster.Columns[4].Visible = false;
                GvBankMaster.Columns[5].Visible = false;
                GvBankMaster.Columns[6].Visible = false;
                GvBankMaster.Columns[7].Visible = false;
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
        protected void GvBankMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvBankMaster.PageIndex = e.NewPageIndex;
            bindgrid();
        }

        protected void GvBankMaster_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    BankMasterData objBankMasterData = new BankMasterData();
                    BankMasterBO objBankMasterBO = new BankMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvBankMaster.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("code");
                    objBankMasterData.ID = Convert.ToInt64(ID.Text);
                    objBankMasterData.ActionType = Enumaction.Select;

                    List<BankMasterData> GetResult = objBankMasterBO.GetBankChkBookIssueDetailsByID(objBankMasterData);
                    if (GetResult.Count > 0)
                    {
                        ddl_bankName.SelectedValue =GetResult[0].BankID.ToString();
                        txt_issueDate.Text = GetResult[0].IssueDate.ToString("dd/MM/yyyy");
                        txt_ChequeNofrom.Text = GetResult[0].ChequeNoFrom.ToString();
                        txt_ChequeNoTo.Text = GetResult[0].ChequeNoTo.ToString();
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
                    BankMasterData objBankMasterData = new BankMasterData();
                    BankMasterBO objBankMasterBO = new BankMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvBankMaster.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objBankMasterData.ID = Convert.ToInt64(ID.Text);
                    objBankMasterData.EmployeeID = LogData.EmployeeID;
                    objBankMasterData.ActionType = Enumaction.Delete;
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
                        objBankMasterData.Remarks = txtremarks.Text;
                    }

                    BankMasterBO objBankMasterBO1 = new BankMasterBO();
                    int Result = objBankMasterBO1.DeleteBankChkBookIssueDetailsByID(objBankMasterData);
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