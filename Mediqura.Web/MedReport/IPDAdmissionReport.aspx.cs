using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
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
using Mediqura.BOL.AdmissionBO;

namespace Mediqura.Web.MedReport
{
    public partial class IPDAdmissionReport : BasePage
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
            Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.OPDepartment));
            Commonfunction.PopulateDdl(ddl_gender, mstlookup.GetLookupsList(LookupName.Gender));
            btnprints.Attributes["disabled"] = "disabled";
            Commonfunction.Insertzeroitemindex(ddl_doctor);
        }
        protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldepartment.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_doctor, mstlookup.GetIPDoctorBydepartmentID(Convert.ToInt32(ddldepartment.SelectedValue)));
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
                div3.Visible = true;
                div3.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage2.Visible = false;
            }

            bindgrid(0);
        }
        protected void bindgrid(int page)
        {
            try
            {
                if (txtdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "VaildDatefrom", 0);
                        div3.Attributes["class"] = "FailAlert";
                        div3.Visible = true;
                        txtdatefrom.Focus();
                        return;
                    }
                }
                else
                {
                    div3.Visible = false;
                }
                if (txtto.Text != "")
                {
                    if (Commonfunction.isValidDate(txtto.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "VaildDateto", 0);
                        div3.Attributes["class"] = "FailAlert";
                        div3.Visible = true;
                        txtto.Focus();
                        return;
                    }
                }
                else
                {
                    div3.Visible = false;
                }
                List<AdmissionData> objdeposit = GetAdmissionList(page);
                if (objdeposit.Count > 0)
                {
                    gvadmissionlist.DataSource = objdeposit;
                    gvadmissionlist.DataBind();
                    gvadmissionlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit.Count + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    btnprints.Attributes.Remove("disabled");
                    divmsg3.Visible = true;
                    // txttotaladmissioncharge.Text = Commonfunction.Getrounding(objdeposit[0].TotalAdmissionCharge.ToString());
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    gvadmissionlist.DataSource = null;
                    gvadmissionlist.DataBind();
                    gvadmissionlist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    //txttotaladmissioncharge.Text = "0.00";
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
            }
        }
        public List<AdmissionData> GetAdmissionList(int curIndex)
        {
            AdmissionData objpat = new AdmissionData();
            AdmissionBO objbillingBO = new AdmissionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.DeptID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
            objpat.DocID = Convert.ToInt64(ddl_doctor.SelectedValue == "" ? "0" : ddl_doctor.SelectedValue);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            objpat.Gender = Convert.ToInt32(ddl_gender.SelectedValue == "" ? "0" : ddl_gender.SelectedValue);
            objpat.AgeFrom = Convert.ToInt32(txt_agefrom.Text == "" ? "0" : txt_agefrom.Text);
            objpat.AgeTo = Convert.ToInt32(txt_ageto.Text == "" ? "200" : txt_ageto.Text);
            objpat.PatientType = Convert.ToInt32(ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue);
            return objbillingBO.GetAdmissionListReport(objpat);
        }
        protected DataTable GetDatafromDatabase()
        {
            List<AdmissionData> AdmissionDetails = GetAdmissionList(0);
            List<AdmissionReportDataTOeXCEL> ListexcelData = new List<AdmissionReportDataTOeXCEL>();
            int i = 0;
            foreach (AdmissionData row in AdmissionDetails)
            {
                AdmissionReportDataTOeXCEL Ecxeclpat = new AdmissionReportDataTOeXCEL();
                Ecxeclpat.IPNo = AdmissionDetails[i].IPNo.ToString();
                Ecxeclpat.UHID = AdmissionDetails[i].UHID.ToString();
                Ecxeclpat.PatientName = AdmissionDetails[i].PatientName.ToString();
                Ecxeclpat.Age = AdmissionDetails[i].Age.ToString();
                Ecxeclpat.GenderID = AdmissionDetails[i].GenderID.ToString();
                Ecxeclpat.Department = AdmissionDetails[i].Department.ToString();
                Ecxeclpat.AdmissionDoctor = AdmissionDetails[i].AdmissionDoctor.ToString();
                Ecxeclpat.Admdate = AdmissionDetails[i].Admdate.ToString();

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
                div3.Visible = true;
                div3.Attributes["class"] = "FailAlert";
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
                    gvadmissionlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvadmissionlist.Columns[7].Visible = false;
                    gvadmissionlist.Columns[8].Visible = false;
                    gvadmissionlist.Columns[9].Visible = false;

                    gvadmissionlist.RenderControl(hw);
                    gvadmissionlist.HeaderRow.Style.Add("width", "15%");
                    gvadmissionlist.HeaderRow.Style.Add("font-size", "10px");
                    gvadmissionlist.Style.Add("text-decoration", "none");
                    gvadmissionlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvadmissionlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=IPDAdmissionlist.pdf");
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
                wb.Worksheets.Add(dt, "Deposit Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=AdmissionDetails.xlsx");
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
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtdatefrom.Text = "";
            txtto.Text = "";
            gvadmissionlist.DataSource = null;
            gvadmissionlist.DataBind();
            gvadmissionlist.Visible = false;
            lblresult.Visible = false;
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage2.Visible = false;
            lblresult.Visible = false;
            div3.Visible = false;
            btnprints.Attributes["disabled"] = "disabled";
            ddldepartment.SelectedIndex = 0;
            ddl_doctor.SelectedIndex = 0;
            ddl_gender.SelectedIndex = 0;
            txt_agefrom.Text = "";
            txt_ageto.Text = "";
            ddl_patienttype.SelectedIndex = 0;
        }
   
    
    }
}