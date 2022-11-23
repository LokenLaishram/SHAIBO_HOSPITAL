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

namespace Mediqura.Web.MedOPD
{
    public partial class Visithistory :BasePage
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
            Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.Department));
            Commonfunction.Insertzeroitemindex(ddl_doctor);
            btnprints.Attributes["disabled"] = "disabled";

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
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid(1);
        }
        protected void bindgrid(int page)
        {
            try
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
                if (txtdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please enter valid from date.", 0);
                        div1.Attributes["class"] = "FailAlert";
                        div1.Visible = true;
                        txtdatefrom.Focus();
                        return;
                    }
                }
                else
                {
                    div1.Visible = false;
                }
                if (txtto.Text != "")
                {
                    if (Commonfunction.isValidDate(txtto.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please enter valid to date.", 0);
                        div1.Attributes["class"] = "FailAlert";
                        div1.Visible = true;
                        txtto.Focus();
                        return;
                    }
                }
                else
                {
                    div1.Visible = false;
                }
                List<PatientData> patientdetails = GetPatientOPDdata(page); ;
                if (patientdetails.Count > 0)
                {
                    if (LogData.PrintEnable == 0)
                    {
                        btnprints.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btnprints.Attributes.Remove("disabled");
                    }
                    GvOpdVisitHistory.VirtualItemCount = patientdetails[0].MaximumRows;//total item is required for custom paging
                    GvOpdVisitHistory.PageIndex = page - 1;
                    GvOpdVisitHistory.DataSource = patientdetails;
                    GvOpdVisitHistory.DataBind();
                    GvOpdVisitHistory.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + patientdetails[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblmessage.Visible = false;
                    div1.Visible = false;


                }
                else
                {
                    btnprints.Attributes["disabled"] = "disabled";
                    divmsg3.Visible = false;
                    GvOpdVisitHistory.DataSource = null;
                    GvOpdVisitHistory.DataBind();
                    GvOpdVisitHistory.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    divmsg3.Visible = false;
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<PatientData> GetPatientOPDdata(int p)
        {

            PatientData objOPD = new PatientData();
            RegistrationBO objstdBO = new RegistrationBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objOPD.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
            objOPD.DoctorID = Convert.ToInt32(ddl_doctor.SelectedValue == "" ? "0" : ddl_doctor.SelectedValue);
            objOPD.UHID = Convert.ToInt64(txt_UHID.Text == "" ? "0" : txt_UHID.Text);
           
            objOPD.DateFrom = from;
            objOPD.DateTo = To;
            objOPD.CurrentIndex = p;
            return objstdBO.OPDVisitHistorydata(objOPD);
        }
        public List<PatientData> GetPatientDataOPD(int curIndex)
        {

            PatientData objOPD = new PatientData();
            RegistrationBO objstdBO = new RegistrationBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objOPD.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
            objOPD.DoctorID = Convert.ToInt32(ddl_doctor.SelectedValue == "" ? "0" : ddl_doctor.SelectedValue);
            objOPD.UHID = Convert.ToInt64(txt_UHID.Text == "" ? "0" : txt_UHID.Text);

            objOPD.DateFrom = from;
            objOPD.DateTo = To;
            //objOPD.CurrentIndex = p;
            return objstdBO.GetPatientOPDdata(objOPD);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txt_UHID.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            ddl_doctor.SelectedIndex = 0;
            ddldepartment.SelectedIndex = 0;
            GvOpdVisitHistory.DataSource = null;
            GvOpdVisitHistory.DataBind();
            GvOpdVisitHistory.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            btnprints.Attributes["disabled"] = "disabled";
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
                Response.AddHeader("content-disposition", "attachment;filename=OPDPatientVisit.xlsx");
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
        protected DataTable GetDatafromDatabase()
        {
            List<PatientData> PatientDetails = GetPatientDataOPD(0);
            List<VisitHistorytoExcel> ListexcelData = new List<VisitHistorytoExcel>();
            int i = 0;
            foreach (PatientData row in PatientDetails)
            {
                VisitHistorytoExcel Ecxeclpat = new VisitHistorytoExcel();
                Ecxeclpat.UHID = PatientDetails[i].UHID;
                Ecxeclpat.PatientName = PatientDetails[i].PatientName;
                Ecxeclpat.DoctorName = PatientDetails[i].EmpName;
                Ecxeclpat.DepartmentName = PatientDetails[i].DepartmentName;
                Ecxeclpat.AddDate = PatientDetails[i].AddDate;
                //Ecxeclpat.ContactNo = PatientDetails[i].ContactNo;
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
        public void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvOpdVisitHistory.BorderStyle = BorderStyle.None;
                    GvOpdVisitHistory.RenderControl(hw);
                    GvOpdVisitHistory.HeaderRow.Style.Add("width", "15%");
                    GvOpdVisitHistory.HeaderRow.Style.Add("font-size", "10px");
                    GvOpdVisitHistory.Style.Add("text-decoration", "none");
                    GvOpdVisitHistory.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvOpdVisitHistory.Style.Add("font-size", "8px");
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

        protected void txt_UHID_TextChanged(object sender, EventArgs e)
        {
            if (txt_UHID.Text != "")
            {
                bindgrid(1);
            }
        }

        protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldepartment.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_doctor, mstlookup.GetIPDoctorBydepartmentID(Convert.ToInt32(ddldepartment.SelectedValue)));
            }
        }

        protected void btnexport_Click1(object sender, EventArgs e)
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

        protected void GvOpdVisitHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }
    }

}