using System;
using System.Collections.Generic;
using System.Linq;
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
using Mediqura.CommonData.MedOPDData;
using Mediqura.BOL.AdmissionBO;
using Mediqura.BOL.MedOPDBO;


namespace Mediqura.Web.MedOPD
{
    public partial class DoctorAppoinmentment : BasePage
    {
        String cmon = DateTime.Now.ToString("MMMM");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                page_setting();
                //Timer1.Enabled = false;
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_docType, mstlookup.GetLookupsList(LookupName.OPDoctorType));
            Commonfunction.Insertzeroitemindex(ddl_doctor);
            Commonfunction.Insertzeroitemindex(ddldepartment);
        }
        protected void page_setting()  //  to bind current month and year
        {
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            //btnprints.Attributes["disabled"] = "disabled";
            List<DocAppoinmentData> objSchedule = GetSchedulePageload(0);
            if (objSchedule.Count > 0)
            {
                GvAppoinmentSch.DataSource = objSchedule;
                GvAppoinmentSch.DataBind();
                GvAppoinmentSch.Visible = true;
                Messagealert_.ShowMessage(lblresult, "Total: " + objSchedule[0].MaximumRows.ToString() + " Record(s) found.", 1);
                divmsg3.Attributes["class"] = "SucessAlert";
                divmsg3.Visible = true;
                Timer1.Enabled = true;
            }
            else
            {
                divmsg3.Visible = false;
                GvAppoinmentSch.DataSource = null;
                GvAppoinmentSch.DataBind();
                GvAppoinmentSch.Visible = true;
                divmsg3.Visible = false;
                lblresult.Visible = false;
            }
        }
        protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldepartment.SelectedIndex > 0 && ddl_docType.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_doctor, mstlookup.GetDoctorBydepartmentID(Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue), Convert.ToInt32(ddl_docType.SelectedValue == "" ? "0" : ddl_docType.SelectedValue)));
            }
            else
            {
                Commonfunction.Insertzeroitemindex(ddl_doctor);
            }
        }
        protected void ddldoctortype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_docType.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.OPDepartment));
            }
            else
            {
                Commonfunction.Insertzeroitemindex(ddldepartment);
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
            hdndocType.Value = ddl_docType.SelectedValue;
            hdndocId.Value = ddl_doctor.SelectedValue;
            hdndeptID.Value = ddldepartment.SelectedValue;
            hdndatefrom.Value = txtdatefrom.Text;
            hdndateTo.Value = txtto.Text;
        }
        protected void bindgrid()
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
                if (ddl_docType.SelectedIndex <= 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "DoctorType", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddldepartment.SelectedIndex <= 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Department", 0);
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
                        Messagealert_.ShowMessage(lblmessage, "VaildDatefrom", 0);
                        div1.Attributes["class"] = "FailAlert";
                        div1.Visible = true;
                        txtdatefrom.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg3.Visible = false;
                }
                if (txtto.Text != "")
                {
                    if (Commonfunction.isValidDate(txtto.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDateto", 0);
                        div1.Attributes["class"] = "FailAlert";
                        div1.Visible = true;
                        txtto.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg3.Visible = false;
                }


                List<DocAppoinmentData> objSchedule = GetSchedule(0);
                if (objSchedule.Count > 0)
                {
                    GvAppoinmentSch.DataSource = objSchedule;
                    GvAppoinmentSch.DataBind();
                    GvAppoinmentSch.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + objSchedule[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    Timer1.Enabled = true;
                    ddl_docType.Attributes["disabled"] = "disabled";
                    ddldepartment.Attributes["disabled"] = "disabled";
                    ddl_doctor.Attributes["disabled"] = "disabled";
                    txtdatefrom.Attributes["disabled"] = "disabled";
                    txtto.Attributes["disabled"] = "disabled";
                    btnprints.Attributes.Remove("disabled");
                    ddlexport.Visible = true;
                    btnexport.Visible = true;

                }
                else
                {
                    divmsg3.Visible = false;
                    GvAppoinmentSch.DataSource = null;
                    GvAppoinmentSch.DataBind();
                    GvAppoinmentSch.Visible = true;
                    divmsg3.Visible = false;
                    lblresult.Visible = false;
                    ddl_docType.Attributes.Remove("disabled");
                    ddldepartment.Attributes.Remove("disabled");
                    ddl_doctor.Attributes.Remove("disabled");
                    txtdatefrom.Attributes.Remove("disabled");
                    txtto.Attributes.Remove("disabled");
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<DocAppoinmentData> GetSchedule(int curIndex)
        {

            DocAppoinmentData objSchedule = new DocAppoinmentData();
            DocAppointmentBO objscheduleBO = new DocAppointmentBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);

            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objSchedule.DoctorType = Convert.ToInt32(ddl_docType.SelectedValue == "" ? "0" : ddl_docType.SelectedValue);
            objSchedule.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
            objSchedule.DoctorID = Convert.ToInt32(ddl_doctor.SelectedValue == "" ? "0" : ddl_doctor.SelectedValue);
            objSchedule.DateFrom = from;
            objSchedule.DateTo = To;

            return objscheduleBO.GetSchedule(objSchedule);
        }

        public List<DocAppoinmentData> GetSchedulePageload(int curIndex)
        {

            DocAppoinmentData objSchedule = new DocAppoinmentData();
            DocAppointmentBO objscheduleBO = new DocAppointmentBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            //DateTime from = Convert.ToDateTime(txtdatefrom.Text == "" ? null : txtdatefrom.Text);
            //DateTime To = Convert.ToDateTime(txtto.Text == "" ? null : txtto.Text);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objSchedule.DateFrom = from;
            objSchedule.DateTo = To;

            return objscheduleBO.GetSchedulePageload(objSchedule);
        }
        public List<DocAppoinmentData> GetScheduleExport(int curIndex)
        {

            DocAppoinmentData objSchedule = new DocAppoinmentData();
            DocAppointmentBO objscheduleBO = new DocAppointmentBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = hdndatefrom.Value.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(hdndatefrom.Value.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = hdndateTo.Value.Trim() == "" ? System.DateTime.Now : DateTime.Parse(hdndateTo.Value.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objSchedule.DoctorType = Convert.ToInt32(hdndocType.Value);
            objSchedule.DepartmentID = Convert.ToInt32(hdndeptID.Value);
            objSchedule.DoctorID = Convert.ToInt32(hdndocId.Value);
            objSchedule.DateFrom = from;
            objSchedule.DateTo = To;

            return objscheduleBO.GetSchedule(objSchedule);
        }
        protected void btnreset_Click(object sender, System.EventArgs e)
        {
            GvAppoinmentSch.DataSource = null;
            GvAppoinmentSch.DataBind();
            GvAppoinmentSch.Visible = false;
            ddl_docType.SelectedIndex = 0;
            ddldepartment.SelectedIndex = 0;
            ddl_doctor.SelectedIndex = 0;
            txtdatefrom.Text = "";
            txtto.Text = "";
            //btnexport.Visible = false;
            //ddlexport.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            lblresult.Text = "";
            lblmessage.Visible = false;
            page_setting();
            Commonfunction.Insertzeroitemindex(ddl_doctor);
            ddl_docType.Attributes.Remove("disabled");
            ddldepartment.Attributes.Remove("disabled");
            ddl_doctor.Attributes.Remove("disabled");
            txtdatefrom.Attributes.Remove("disabled");
            txtto.Attributes.Remove("disabled");
            btnprints.Attributes["disabled"] = "disabled";
            lblrefresh.Text = "";
            Timer1.Enabled = false;
            Commonfunction.Insertzeroitemindex(ddl_doctor);
            Commonfunction.Insertzeroitemindex(ddldepartment);
        }

        protected void GvAppoinmentSch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cb = (CheckBox)e.Row.FindControl("chkselect");
                Label available = (Label)e.Row.FindControl("lbl_availability");
                CheckBox cb1 = (CheckBox)e.Row.FindControl("chkCkeckIn");
                Label checkin = (Label)e.Row.FindControl("lbl_checkin");
                if (available.Text == "1")
                {
                    cb.Checked = true;
                    cb.Enabled = false;
                }
                else
                {
                    cb.Checked = false;
                    cb.Enabled = false;
                }
                if (checkin.Text == "1")
                {
                    cb1.Checked = true;
                    cb1.Enabled = false;
                }
                else
                {
                    cb1.Checked = false;
                    cb1.Enabled = false;
                }
            }
        }

        protected void Timer1_Tick(object sender, System.EventArgs e)
        {

            Upautorefresh.Update();
            bindgrid();
            lblrefresh.Text = "Grid Refreshed at: " + DateTime.Now.ToString();

        }

        protected void btnexport_Click(object sender, System.EventArgs e)
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
            if (ddlexport.SelectedIndex == 1)
            {
                //txtdatefrom.Attributes.Remove("disabled");
                //txtto.Attributes.Remove("disabled");
                //Timer1.Enabled = false;
                //Upautorefresh.EnableViewState = true;
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

        protected void GvAppoinmentSch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvAppoinmentSch.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        public void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvAppoinmentSch.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvAppoinmentSch.Columns[10].Visible = false;
                    //GvAppoinmentSch.Columns[10].Visible = false;
                    //gvstockstatus.Columns[10].Visible = false;
                    //gvstockstatus.Columns[11].Visible = false;

                    GvAppoinmentSch.RenderControl(hw);
                    GvAppoinmentSch.HeaderRow.Style.Add("width", "15%");
                    GvAppoinmentSch.HeaderRow.Style.Add("font-size", "10px");
                    GvAppoinmentSch.Style.Add("text-decoration", "none");
                    GvAppoinmentSch.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvAppoinmentSch.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=AppoinmentDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=AppoinmentDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    //ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblresult, "Exported", 1);
                divmsg3.Attributes["class"] = "SucessAlert";
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<DocAppoinmentData> appmtSch = GetScheduleExport(0);
            List<DocAppoinmentDataTOeXCEL> ListexcelData = new List<DocAppoinmentDataTOeXCEL>();
            int i = 0;
            foreach (DocAppoinmentData row in appmtSch)
            {
                DocAppoinmentDataTOeXCEL Ecxeclpat = new DocAppoinmentDataTOeXCEL();
                Ecxeclpat.EmpName = appmtSch[i].EmpName;
                Ecxeclpat.Day = appmtSch[i].Day;
                Ecxeclpat.Morning = appmtSch[i].Morning;
                Ecxeclpat.Evening = appmtSch[i].Evening;
                Ecxeclpat.Afternoon = appmtSch[i].Afternoon;
                Ecxeclpat.MorningSlots = appmtSch[i].MorningSlots;
                Ecxeclpat.EveningSlots = appmtSch[i].EveningSlots;
                Ecxeclpat.AfternoonSlots = appmtSch[i].AfternoonSlots;
                Ecxeclpat.date = appmtSch[i].date;
                Ecxeclpat.Availibility = appmtSch[i].Availibility;
                Ecxeclpat.Checkin = appmtSch[i].Checkin;
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


    }
}