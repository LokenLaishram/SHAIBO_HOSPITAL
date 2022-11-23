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
using Mediqura.BOL.AdmissionBO;

namespace Mediqura.Web.MedIPD
{
    public partial class Patientenquiry : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPNo(string prefixText, int count, string contextKey)
        {
            IPServiceRecordData Objpaic = new IPServiceRecordData();
            IPServiceRecordBO objInfoBO = new IPServiceRecordBO();
            List<IPServiceRecordData> getResult = new List<IPServiceRecordData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.getIPNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        protected void txt_autoipno_TextChanged(object sender, EventArgs e)
        {
            if (txt_autoipno.Text != "")
            {
                bindgrid();

            }
        }
 
        protected void bindgrid()
        {
            try
            {
                if (txt_autoipno.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "IPNo", 0);
                    div6.Attributes["class"] = "FailAlert";
                    div6.Visible = true;
                    txt_autoipno.Focus();
                    return;
                }
                List<PatientData> patientdetails = GetPatientDataOPD(0);
                if (patientdetails.Count > 0)
                {
                    GvIpdPatientEnq.DataSource = patientdetails;
                    GvIpdPatientEnq.DataBind();
                    GvIpdPatientEnq.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + patientdetails[0].MaximumRows + " Record found", 1);
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    lblmessage.Visible = true;

                }
                else
                {
                    GvIpdPatientEnq.DataSource = null;
                    GvIpdPatientEnq.DataBind();
                    GvIpdPatientEnq.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult.Visible = false;
                    divmsg3.Visible = false;
                    lblmessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }

        public List<PatientData> GetPatientDataOPD(int curIndex)
        {

            PatientData objOPD = new PatientData();
            RegistrationBO objstdBO = new RegistrationBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            var source = txt_autoipno.Text.Trim();
            string IPNo;
            if (source.Contains(':'))
            {
                IPNo = source.Substring(source.LastIndexOf(':') + 1);
                objOPD.IPNo = IPNo;
    
            }

            DateTime from = txt_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txt_To.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_To.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objOPD.DateFrom = from;
            objOPD.DateTo = To;
            return objstdBO.IpdPatientEnquiry(objOPD);
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
                div6.Visible = true;
                div6.Attributes["class"] = "FailAlert";
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
            txt_autoipno.Text = "";
             GvIpdPatientEnq.DataSource = null;
            GvIpdPatientEnq.DataBind();
            GvIpdPatientEnq.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }

        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ExportEnable", 0);
                div6.Visible = true;
                div6.Attributes["class"] = "FailAlert";
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
        protected DataTable GetExcelData()
        {
            List<PatientData> PatientDetails = GetPatientDataOPD(0);
            List<IPDPatientEnqDatatoExcel> ListexcelData = new List<IPDPatientEnqDatatoExcel>();
            int i = 0;
            foreach (PatientData row in PatientDetails)
            {
                IPDPatientEnqDatatoExcel Ecxeclpat = new IPDPatientEnqDatatoExcel();
                Ecxeclpat.IPNo = PatientDetails[i].IPNo;
                Ecxeclpat.PatientName = PatientDetails[i].PatientName;
                Ecxeclpat.Address = PatientDetails[i].Address;
                Ecxeclpat.ContactNo = PatientDetails[i].ContactNo;
                Ecxeclpat.BedDetails = PatientDetails[i].BedDetails;
                Ecxeclpat.AssignedDate = PatientDetails[i].AssignedDate;


                ListexcelData.Add(Ecxeclpat);
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
        protected void ExportoExcel()
        {

            DataTable dt = GetExcelData();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "PatientDetails");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=IPDPatientEnquiry.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblresult, "Exported", 1);
                //divmsg3.Attributes["class"] = "SucessAlert";
            }
        }

        public void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvIpdPatientEnq.BorderStyle = BorderStyle.None;
                    GvIpdPatientEnq.RenderControl(hw);
                    GvIpdPatientEnq.HeaderRow.Style.Add("width", "15%");
                    GvIpdPatientEnq.HeaderRow.Style.Add("font-size", "10px");
                    GvIpdPatientEnq.Style.Add("text-decoration", "none");
                    GvIpdPatientEnq.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvIpdPatientEnq.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=OPDPatientVisit.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                    Messagealert_.ShowMessage(lblresult, "Exported", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }


    }
}