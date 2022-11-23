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
using Mediqura.CommonData.MedBillData;
using Mediqura.BOL.MedBillBO;
using System.Collections.Specialized;

namespace Mediqura.Web.MedOPD
{
    public partial class DoctorWisePatient : BasePage
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
            Commonfunction.PopulateDdl(ddlconsultant, mstlookup.GetLookupsList(LookupName.Doctor));
            btnprint.Attributes["disabled"] = "disabled";
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
              if (ddlconsultant.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Consultant", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    return;

                }
                else
                {
                    divmsg1.Visible = false;
                }
                if (txtdate.Text == "")
                {
                    if (Commonfunction.isValidDate(txtdate.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtdate.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg1.Visible = false;
                }
                List<BookingData> obj = GetDocWisePatientList(0);
                if (obj.Count > 0)
                {
                    if (LogData.PrintEnable == 0)
                    {
                        btnprint.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btnprint.Attributes.Remove("disabled");
                    }

                    gvbookingdetails.DataSource = obj;
                    gvbookingdetails.DataBind();
                    gvbookingdetails.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + obj[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;

                }
                else
                {
                    btnprint.Attributes["disabled"] = "disabled";
                    gvbookingdetails.DataSource = null;
                    gvbookingdetails.DataBind();
                    gvbookingdetails.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    divmsg3.Visible = false;
                }
               

            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }
        private List<BookingData> GetDocWisePatientList(int p)
        {
            BookingData objstock = new BookingData();
            RegistrationBO objBO = new RegistrationBO();
            objstock.DoctorID = Convert.ToInt32(ddlconsultant.SelectedValue == "" ? "0" : ddlconsultant.SelectedValue);
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime bookingdate = txtdate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.BookingDate = bookingdate;
            return objBO.GetDocWisePatientList(objstock);

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
        protected void btnreset_Click(object sender, EventArgs e)
        {
            gvbookingdetails.DataSource = null;
            gvbookingdetails.DataBind();
            gvbookingdetails.Visible = false;
            txtdate.Text = "";
            lblresult.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage.Visible = false;
            divmsg1.Visible = false;
             MasterLookupBO mstlookup = new MasterLookupBO();
             Commonfunction.PopulateDdl(ddlconsultant, mstlookup.GetLookupsList(LookupName.Doctor));
          }
        protected DataTable GetDatafromDatabase()
        {
            List<BookingData> DepositDetails = GetDocWisePatientList(0);
            List<PatientListDataTOeXCEL> ListexcelData = new List<PatientListDataTOeXCEL>();
            int i = 0;
            foreach (BookingData row in DepositDetails)
            {
                PatientListDataTOeXCEL Ecxeclpat = new PatientListDataTOeXCEL();
                Ecxeclpat.PatientName = DepositDetails[i].PatientName;
                Ecxeclpat.Address = DepositDetails[i].Address;
                Ecxeclpat.ContactNo = DepositDetails[i].ContactNo;
                Ecxeclpat.Age = DepositDetails[i].Age;
                Ecxeclpat.Remarks = DepositDetails[i].Remarks;
                Ecxeclpat.BookingDate = DepositDetails[i].BookingDate;
                Ecxeclpat.Time = DepositDetails[i].Time;
                Ecxeclpat.AddedBy = DepositDetails[i].EmpName;

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
                Messagealert_.ShowMessage(lblmessage, "ExportType", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Item CheckList");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=DoctorWisePatientDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblmessage, "Exported", 1);
                divmsg1.Attributes["class"] = "SucessAlert";
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        public void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    gvbookingdetails.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    //gvOT_status.Columns[6].Visible = false;
                    //gvOT_status.Columns[8].Visible = false;
                    //gvstockstatus.Columns[10].Visible = false;
                    //gvstockstatus.Columns[11].Visible = false;

                    gvbookingdetails.RenderControl(hw);
                    gvbookingdetails.HeaderRow.Style.Add("width", "15%");
                    gvbookingdetails.HeaderRow.Style.Add("font-size", "10px");
                    gvbookingdetails.Style.Add("text-decoration", "none");
                    gvbookingdetails.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvbookingdetails.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=DoctorWisePatientDetails.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        protected void gvbookingdetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvbookingdetails.PageIndex = e.NewPageIndex;
            bindgrid();
        }
       
    }
}