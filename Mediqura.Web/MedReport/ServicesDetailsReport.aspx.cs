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
using Mediqura.CommonData.MIS;
using Mediqura.BOL.MIS;

namespace Mediqura.Web.MedReport
{
    public partial class ServicesDetailsReport : BasePage
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
            Commonfunction.PopulateDdl(ddlservices, mstlookup.GetLookupsList(LookupName.Services));
      
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindlist(0);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            lblmessage2.Visible = false;
            lblresult.Visible = false;
            divmsg2.Visible = false;
            txtdatefrom.Text = "";
            txtto.Text = "";
            gvnursingcare.Visible = false;
            gvnursingcare.DataSource = null;
            btnexport.Visible = false;
            ddlexport.Visible = false;
            //btnprints.Attributes["disabled"] = "disabled";
            ddlservices.SelectedIndex = 0;
          }
        protected void bindlist(int page)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage2.Visible = false;
            }
            try
            {
                txttotalamount.Text = "0.0";
                txt_share.Text = "0.0";
            
                List<ServicesReportData> objdeposit = GetServicesDetailList(page);
                if (objdeposit.Count > 0)
                {
                    gvnursingcare.DataSource = objdeposit;
                    gvnursingcare.DataBind();
                    gvnursingcare.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblmessage2.Visible = true;
                    divmsg2.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + objdeposit.Count + " Record(s) found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    foreach (GridViewRow row1 in gvnursingcare.Rows)
                    {
                        Label SC = (Label)gvnursingcare.Rows[row1.RowIndex].Cells[0].FindControl("lblservicecharge");
                        Label Share = (Label)gvnursingcare.Rows[row1.RowIndex].Cells[0].FindControl("lblshare");

                        txttotalamount.Text = (Convert.ToDecimal(txttotalamount.Text) + Convert.ToDecimal(SC.Text)).ToString();
                        txt_share.Text = (Convert.ToDecimal(txt_share.Text) + Convert.ToDecimal(Share.Text)).ToString();
                    }  //    btnprints.Attributes.Remove("disabled");
                }
                else
                {
                    gvnursingcare.DataSource = null;
                    gvnursingcare.DataBind();
                    gvnursingcare.Visible = true;
                    divmsg2.Visible = false;
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
            }
        }
        public List<ServicesReportData> GetServicesDetailList(int p)
        {
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            ServicesReportData objpat = new ServicesReportData();
            ServicesReportBO objBO = new ServicesReportBO();
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            string datefrom = from.ToString("yyyy-MM-dd");
            string timefrom = txttimepickerfrom.Text.Trim();
            from = Convert.ToDateTime(datefrom + " " + timefrom);
            objpat.DateFrom = from;
            string dateto = To.ToString("yyyy-MM-dd");
            string timeto = txttimepickerto.Text.Trim();
            To = Convert.ToDateTime(dateto + " " + timeto);
            objpat.DateTo = To;
            objpat.ServiceID = Convert.ToInt32(ddlservices.SelectedValue == "" ? "0" : ddlservices.SelectedValue);
            return objBO.GetServicesDetailList(objpat);
        }
        protected DataTable GetDatafromDatabase()
        {
            List<ServicesReportData> Details = GetServicesDetailList(0);
            List<ServicesReportDataTOeXCEL> ListexcelData = new List<ServicesReportDataTOeXCEL>();
            int i = 0;
            foreach (ServicesReportData row in Details)
            {
                ServicesReportDataTOeXCEL Ecxeclpat = new ServicesReportDataTOeXCEL();
                Ecxeclpat.IPNo = Details[i].IPNo;
                Ecxeclpat.PatientName = Details[i].PatientName;
                Ecxeclpat.ServiceName = Details[i].ServiceName;
                Ecxeclpat.Quantity = Details[i].Quantity;
                Ecxeclpat.ServiceCharge = Details[i].ServiceCharge;
                Ecxeclpat.NetServiceCharge = Details[i].NetServiceCharge;
                Ecxeclpat.Share = Details[i].Share;
                Ecxeclpat.AddedDTM = Details[i].AddedDTM;
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
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "ExportEnable", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage2.Visible = false;
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
                    gvnursingcare.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox

                    gvnursingcare.Columns[11].Visible = false;
                    gvnursingcare.Columns[12].Visible = false;
                    gvnursingcare.Columns[13].Visible = false;
                    gvnursingcare.Columns[14].Visible = false;
                    gvnursingcare.RenderControl(hw);
                    gvnursingcare.HeaderRow.Style.Add("width", "15%");
                    gvnursingcare.HeaderRow.Style.Add("font-size", "10px");
                    gvnursingcare.Style.Add("text-decoration", "none");
                    gvnursingcare.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvnursingcare.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=NursingCareDetails.pdf");
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
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Nursing Care Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=NursingCareDetails.xlsx");
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

     }
}
