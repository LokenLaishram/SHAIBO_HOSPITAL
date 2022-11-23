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

namespace Mediqura.Web.MedReport
{
    public partial class OTProcedureReport : BasePage
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
            Commonfunction.PopulateDdl(dd_ottheaters, mstlookup.GetLookupsList(LookupName.OTtheater));
            Commonfunction.PopulateDdl(ddl_doctor, mstlookup.GetLookupsList(LookupName.OTpayabledoctors));
            Commonfunction.PopulateDdl(ddl_cases, mstlookup.GetLookupsList(LookupName.OTcases));
           
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindOtpatientlist(0);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            lblmessage.Visible = false;
            lblresult2.Visible = false;
            div2.Visible = false;
            ddl_doctor.SelectedIndex = 0;
            dd_ottheaters.SelectedIndex = 0;
            txtdatefrom.Text = "";
            txtto.Text = "";
            Gv_Otregistrationlist.Visible = false;
            Gv_Otregistrationlist.DataSource = null;
            btnexport.Visible = false;
            ddlexport.Visible = false;
            btnprints.Attributes["disabled"] = "disabled";
       
            ddl_cases.SelectedIndex = 0;
        }
        protected void bindOtpatientlist(int page)
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
            try
            {

                List<OTRegnData> objdeposit = GetOT_RegistrationListCustom(page);
                if (objdeposit.Count > 0)
                {
                    Gv_Otregistrationlist.DataSource = objdeposit;
                    Gv_Otregistrationlist.DataBind();
                    Gv_Otregistrationlist.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblmessage.Visible = true;
                    div1.Visible = true;
                    Messagealert_.ShowMessage(lblresult2, "Total: " + objdeposit[0].MaximumRows.ToString() + " Record(s) found", 1);
                    div2.Attributes["class"] = "SucessAlert";
                    div2.Visible = true;
                    btnprints.Attributes.Remove("disabled");
                }
                else
                {
                    Gv_Otregistrationlist.DataSource = null;
                    Gv_Otregistrationlist.DataBind();
                    Gv_Otregistrationlist.Visible = true;
                    div1.Visible = false;
                    lblresult2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<OTRegnData> GetOT_RegistrationListCustom(int p)
        {
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            OTRegnData objpat = new OTRegnData();
            OTRegnBO objBO = new OTRegnBO();
            objpat.OTemployeeID = Convert.ToInt64(ddl_doctor.SelectedValue == "" ? "0" : ddl_doctor.SelectedValue);
            objpat.CaseID = Convert.ToInt32(ddl_cases.SelectedValue == "" ? "0" : ddl_cases.SelectedValue);
            objpat.OTtype = Convert.ToInt32(dd_ottheaters.SelectedValue == "" ? "0" : dd_ottheaters.SelectedValue);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? GlobalConstant.MaxdateAddOneYear : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            return objBO.GetOT_RegistrationListReport(objpat);
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
                Messagealert_.ShowMessage(lblresult2, "ExportType", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
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
                    Gv_Otregistrationlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    //gvSummaryList.Columns[4].Visible = false;
                    //gvSummaryList.Columns[5].Visible = false;
                    Gv_Otregistrationlist.Columns[6].Visible = false;
                    Gv_Otregistrationlist.Columns[7].Visible = false;

                    Gv_Otregistrationlist.RenderControl(hw);
                    Gv_Otregistrationlist.HeaderRow.Style.Add("width", "15%");
                    Gv_Otregistrationlist.HeaderRow.Style.Add("font-size", "10px");
                    Gv_Otregistrationlist.Style.Add("text-decoration", "none");
                    Gv_Otregistrationlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    Gv_Otregistrationlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=OTProcedureList.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=OTProcedureList.xlsx");
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
            List<OTRegnData> OtProcedureListDetails = GetOT_RegistrationListCustom(0);
            List<OTRegnReportListDataTOeXCEL> ListexcelData = new List<OTRegnReportListDataTOeXCEL>();
            int i = 0;
            foreach (OTRegnData row in OtProcedureListDetails)
            {
                OTRegnReportListDataTOeXCEL ExcelSevice = new OTRegnReportListDataTOeXCEL();
                ExcelSevice.IPNo = OtProcedureListDetails[i].IPNo;
                ExcelSevice.PatientName = OtProcedureListDetails[i].PatientName;
                ExcelSevice.Description = OtProcedureListDetails[i].Description;
                ExcelSevice.OperationDate = OtProcedureListDetails[i].OperationDate;
                ExcelSevice.CaseName = OtProcedureListDetails[i].CaseName;
                ExcelSevice.OTDoc = OtProcedureListDetails[i].OTDoc;
                ExcelSevice.Amount = OtProcedureListDetails[i].Amount;
                //Gv_Otregistrationlist.Columns[4].Visible = false;
                //Gv_Otregistrationlist.Columns[5].Visible = false;
                //Gv_Otregistrationlist.Columns[6].Visible = false;
                //Gv_Otregistrationlist.Columns[7].Visible = false;
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
        
    }
}