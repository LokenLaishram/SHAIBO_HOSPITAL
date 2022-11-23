using Mediqura.BOL.CommonBO;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.PatientData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;
using System.Reflection;
using System.IO;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Mediqura.CommonData.AdmissionData;
using Mediqura.BOL.MedBillBO;
using Mediqura.BOL.AdmissionBO;

namespace Mediqura.Web.MedIPD
{
    public partial class InterimBill : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txt_totaldeposited.Text = "0.0";
                txt_totalbill.Text = "0.0";
                txt_totaloutstanding.Text = "0.0";
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPNo(string prefixText, int count, string contextKey)
        {
            IPData Objpaic = new IPData();
            AdmissionBO objInfoBO = new AdmissionBO();
            List<IPData> getResult = new List<IPData>();
            Objpaic.IPNo = prefixText;
            getResult = objInfoBO.getIPNoWithNameAgeNAddress(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void bindgrid()
        {
            try
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
                if (txtdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "VaildDatefrom", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtdatefrom.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg1.Visible = false;
                }
                if (txtdateto.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdateto.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "VaildDateto", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtdateto.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }
                List<InterimBillData> objdeposit = GetIPDbillList(0);
                if (objdeposit.Count > 0)
                {
                    GvInterimBill.DataSource = objdeposit;
                    GvInterimBill.DataBind();
                    GvInterimBill.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    divmsg1.Visible = false;
                    if (LogData.PrintEnable == 0)
                    {
                        btn_print.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btn_print.Attributes.Remove("disabled");
                    }
                }
                else
                {
                    GvInterimBill.DataSource = null;
                    GvInterimBill.DataBind();
                    GvInterimBill.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    txt_totaldeposited.Text = "0.0";
                    txt_totalbill.Text = "0.0";
                    txt_totaloutstanding.Text = "0.0";
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetUHID(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.UHID = Convert.ToInt64(prefixText);
            getResult = objInfoBO.GetUHID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].RegDNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPatientName(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            GvInterimBill.DataSource = null;
            GvInterimBill.DataBind();
            GvInterimBill.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblresult.Visible = false;
            divmsg3.Visible = false;
            lblmessage.Visible = false;
            txt_IPNo.Text = "";
            txtdateto.Text = "";
            txtdatefrom.Text = "";
            txt_totaldeposited.Text = "0.0";
            txt_totalbill.Text = "0.0";
            txt_totaloutstanding.Text = "0.0";

        }
        public List<InterimBillData> GetIPDbillList(int curIndex)
        {
            InterimBillData objbill = new InterimBillData();
            FInalBillBO objbillingBO = new FInalBillBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);

            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtdateto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objbill.IPNo = txt_IPNo.Text == "" ? "" : txt_IPNo.Text.Substring(txt_IPNo.Text.LastIndexOf(':') + 1);
            objbill.DateFrom = from;
            objbill.DateTo = To;
            objbill.FinancialYearID = LogData.FinancialYearID;
            objbill.HospitalID = LogData.HospitalID;
            return objbillingBO.Get_IPD_Iterimbill_List(objbill);
        }
        public class ListtoDataTableConverter
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                // Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //  Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //       inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //     put a breakpoint here and check datatable
                return dataTable;
            }
        }

        Decimal TotalBill = 0, TotalDeposited = 0, TotalOutstanding = 0;
        protected void GvInterimBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label OsBill = (Label)e.Row.FindControl("lbl_outstandingbill");
                Label Crlimit = (Label)e.Row.FindControl("lbl_creditlimit");
                Label totalbill = (Label)e.Row.FindControl("lbl_totalbill");
                Label totaldeposited = (Label)e.Row.FindControl("lbl_totaldeposited");
                if (Convert.ToDecimal(OsBill.Text == "" ? "0" : OsBill.Text) > Convert.ToDecimal(Crlimit.Text == "" ? "0" : Crlimit.Text))
                {
                    e.Row.Cells[2].BackColor = System.Drawing.Color.Yellow;
                }
                TotalBill = TotalBill + Convert.ToDecimal(totalbill.Text == "" ? "0" : totalbill.Text);
                TotalDeposited = TotalDeposited + Convert.ToDecimal(totaldeposited.Text == "" ? "0" : totaldeposited.Text);
                TotalOutstanding = TotalOutstanding + Convert.ToDecimal(OsBill.Text == "" ? "0" : OsBill.Text);
                txt_totalbill.Text = Commonfunction.Getrounding(TotalBill.ToString());
                txt_totaldeposited.Text = Commonfunction.Getrounding(TotalDeposited.ToString());
                txt_totaloutstanding.Text = Commonfunction.Getrounding(TotalOutstanding.ToString());
            }
        }
        protected void GvInterimBill_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Print")
                {
                    if (LogData.PrintEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblresult, "PrintEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblresult.Visible = false;
                    }
                    int j = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gp = GvInterimBill.Rows[j];
                    Label billno = (Label)gp.Cells[0].FindControl("lbl_billno");
                    Label IPNo = (Label)gp.Cells[0].FindControl("lbl_IPno");
                    InterimBillData objbill = new InterimBillData();
                    FInalBillBO objbillingBO = new FInalBillBO();
                    objbill.IPNo = IPNo.Text.Trim();
                    objbill.FinancialYearID = LogData.FinancialYearID;
                    objbill.HospitalID = LogData.HospitalID;
                    int result = objbillingBO.Update_interimbilldetails(objbill);
                    if (result == 1)
                    {
                        string url = "../MedBills/Reports/ReportViewer.aspx?option=InterimBill&IPno=" + IPNo.Text.ToString();
                        string fullURL = "window.open('" + url + "', '_blank');";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }
        protected void GvInterimBill_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvInterimBill.PageIndex = e.NewPageIndex;
            bindgrid();
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
                divmsg3.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        public void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvInterimBill.BorderStyle = BorderStyle.None;

                    GvInterimBill.RenderControl(hw);
                    GvInterimBill.HeaderRow.Style.Add("width", "15%");
                    GvInterimBill.HeaderRow.Style.Add("font-size", "10px");
                    GvInterimBill.Style.Add("text-decoration", "none");
                    GvInterimBill.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvInterimBill.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=IntermBillList.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected DataTable GetDatafromDatabase()
        {
            List<InterimBillData> DepositDetails = GetIPDbillList(0); ;
            List<InterimBillDataToExcel> ListexcelData = new List<InterimBillDataToExcel>();
            int i = 0;
            foreach (InterimBillData row in DepositDetails)
            {
                InterimBillDataToExcel Ecxeclpat = new InterimBillDataToExcel();
                Ecxeclpat.UHID = DepositDetails[i].UHID;
                Ecxeclpat.IPNo = DepositDetails[i].IPNo;
                Ecxeclpat.PatientName = DepositDetails[i].PatientName;
                Ecxeclpat.TotalBill = DepositDetails[i].TotalBill;
                // Ecxeclpat.BedDetail = DepositDetails[i].BedDetail;
                Ecxeclpat.TotalDeposited = DepositDetails[i].TotalDeposited;
                Ecxeclpat.OustandingBill = DepositDetails[i].OustandingBill;
                // Ecxeclpat.CreditLimit = DepositDetails[i].CreditLimit;
                //  Ecxeclpat.AdmissionDoctor = DepositDetails[i].AdmissionDoctor;
                Ecxeclpat.AdmissionDate = DepositDetails[i].AdmissionDate;
                ListexcelData.Add(Ecxeclpat);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Interim Bill Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=InterimBill.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblresult, "Exported", 1);
                divmsg3.Attributes["class"] = "SucessAlert";
            }
        }
        protected void btn_print_Click(object sender, EventArgs e)
        {
            string IPNo = txt_IPNo.Text == "" ? " " : txt_IPNo.Text.Substring(txt_IPNo.Text.LastIndexOf(':') + 1);
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);

            string url = "../MedBills/Reports/ReportViewer.aspx?option=InterimBillList&IPno=" + IPNo + "&Datefrom=" + txtdatefrom.Text + "&Dateto=" + txtdateto.Text;
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
    }
}