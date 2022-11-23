using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedAccount;
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
using Mediqura.CommonData.MedAccount;

namespace Mediqura.Web.MedAccount
{
    public partial class TransactionDefaultMST : BasePage
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
            Commonfunction.PopulateDdl(ddl_bank, mstlookup.GetLookupsList(LookupName.TransDeft));
        }
        private void bindgrid()
        {
            try
            {
                //foreach (GridViewRow row in GvTransaction.Rows)
                //{

                //    DropDownList BankName = (DropDownList)GvTransaction.Rows[row.RowIndex].Cells[0].FindControl("ddl_Bank");
                //    BankName.Enabled = false;
                //}
                List<TransactionDefaultData> lstemp = GetTransaction(0);
                if (lstemp.Count > 0)
                {
                    GvTransaction.DataSource = lstemp;
                    GvTransaction.DataBind();
                    GvTransaction.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvTransaction.DataSource = null;
                    GvTransaction.DataBind();
                    GvTransaction.Visible = true;
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
        private List<TransactionDefaultData> GetTransaction(int p)
        {
            TransactionDefaultData objCountryMSTData = new TransactionDefaultData();
            TransactionDefaultBO objCountryMSTBO = new TransactionDefaultBO();
            objCountryMSTData.PaymentMode = txt_Trans.Text == "" ? "" : txt_Trans.Text;
            objCountryMSTData.BankGroupID = Convert.ToInt32(ddl_bank.SelectedValue == "" ? null : ddl_bank.SelectedValue);
            objCountryMSTData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objCountryMSTBO.GetTransaction(objCountryMSTData);
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
                if (txt_Trans.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_Trans.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_bank.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "BankName", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_bank.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                TransactionDefaultData objCountryMSTData = new TransactionDefaultData();
                TransactionDefaultBO objCountryMSTBO = new TransactionDefaultBO();
                objCountryMSTData.PaymentMode = txt_Trans.Text == "" ? null : txt_Trans.Text;
                objCountryMSTData.BankGroupID = Convert.ToInt32(ddl_bank.SelectedValue == "0" ? null : ddl_bank.SelectedValue);
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
                        txt_Trans.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    objCountryMSTData.ActionType = Enumaction.Update;
                    objCountryMSTData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objCountryMSTBO.UpdateTransactionDetails(objCountryMSTData);  // funtion at DAL
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
            txt_Trans.Text = "";
            ddl_bank.SelectedIndex = 0;
            GvTransaction.DataSource = null;
            GvTransaction.DataBind();
            GvTransaction.Visible = false;
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
                    GvTransaction.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvTransaction.Columns[5].Visible = false;
                    GvTransaction.Columns[6].Visible = false;
                    GvTransaction.Columns[7].Visible = false;

                    GvTransaction.RenderControl(hw);
                    GvTransaction.HeaderRow.Style.Add("width", "15%");
                    GvTransaction.HeaderRow.Style.Add("font-size", "10px");
                    GvTransaction.Style.Add("text-decoration", "none");
                    GvTransaction.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvTransaction.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=DefaultTransactionDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=DefaultTransactionDetails.xlsx");
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
            List<TransactionDefaultData> labunitDetails = GetTransaction(0);
            List<TransactionDefaultDatatoExcel> ListexcelData = new List<TransactionDefaultDatatoExcel>();
            int i = 0;
            foreach (TransactionDefaultData row in labunitDetails)
            {
                TransactionDefaultDatatoExcel ExcelSevice = new TransactionDefaultDatatoExcel();
                ExcelSevice.ID = labunitDetails[i].ID;
                ExcelSevice.PaymentMode = labunitDetails[i].PaymentMode;
                ExcelSevice.LedgerName = labunitDetails[i].LedgerName;
                ExcelSevice.AddedBy = labunitDetails[i].EmpName;
                GvTransaction.Columns[5].Visible = false;
                GvTransaction.Columns[6].Visible = false;
                GvTransaction.Columns[7].Visible = false;
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
        protected void GvTransaction_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    TransactionDefaultData objCountryMSTData = new TransactionDefaultData();
                    TransactionDefaultBO objCountryMSTBO = new TransactionDefaultBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvTransaction.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("code");
                    objCountryMSTData.ID = Convert.ToInt32(ID.Text);
                    objCountryMSTData.ActionType = Enumaction.Select;

                    List<TransactionDefaultData> GetResult = objCountryMSTBO.GetTransactionDetailsByID(objCountryMSTData);
                    if (GetResult.Count > 0)
                    {
                        txt_Trans.Text = GetResult[0].PaymentMode;
                        ddl_bank.SelectedValue = GetResult[0].BankGroupID.ToString();
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
                    TransactionDefaultData objCountryMSTData = new TransactionDefaultData();
                    TransactionDefaultBO objCountryMSTBO = new TransactionDefaultBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvTransaction.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objCountryMSTData.ID = Convert.ToInt32(ID.Text);
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

                    TransactionDefaultBO objCountryMSTBO1 = new TransactionDefaultBO();
                    int Result = objCountryMSTBO1.DeleteTransactionDetailsByID(objCountryMSTData);
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

        protected void GvTransaction_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvTransaction.PageIndex = e.NewPageIndex;
            bindgrid();
        }

        protected void GvTransaction_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    MasterLookupBO mstlookup = new MasterLookupBO();
            //    Label Bank = (Label)e.Row.FindControl("lblbank");
            //    Label BankID = (Label)e.Row.FindControl("lblBankId");
            //    DropDownList ddlBank = (DropDownList)e.Row.FindControl("ddl_Bank");
            //    Commonfunction.PopulateDdl(ddl_bank, mstlookup.GetLookupsList(LookupName.TransDeft));
            //    if (Bank.Text == "")
            //    {
            //        ddl_bank.SelectedIndex = 0;
            //    }
            //    else
            //    {
            //        ddl_bank.SelectedItem.Text = Bank.Text;
            //        //ddl_bank.SelectedIndex = Convert.ToInt32(BankID.Text);
            //        ddl_bank.SelectedValue = BankID.Text;
            //    }
            //}
        }
    }
}
