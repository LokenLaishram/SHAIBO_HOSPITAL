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
using Mediqura.BOL.AdmissionBO;

namespace Mediqura.Web.MedUtility
{
    public partial class AppointmentSchedulerMaster : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                page_setting();
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_docType, mstlookup.GetLookupsList(LookupName.OPDoctorType));
            Commonfunction.PopulateDdl(ddl_month, mstlookup.GetLookupsList(LookupName.month));
            Commonfunction.PopulateDdl(ddl_year, mstlookup.GetLookupsList(LookupName.Year));
            Commonfunction.Insertzeroitemindex(ddl_doctor);
            Commonfunction.Insertzeroitemindex(ddldepartment);
            Commonfunction.Insertzeroitemindex(ddl_date);
        }
        protected void page_setting()  //  to bind current month and year
        {

            String cmon = DateTime.Now.ToString("MMMM");
            String cyear = DateTime.Now.ToString("yyyy");
            ddl_month.Items.FindByText(cmon).Selected = true;
            ddl_year.Items.FindByText(cyear).Selected = true;

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
        protected void ddl_doctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_doctor.SelectedIndex > 0 && ddl_month.SelectedIndex > 0 && ddl_year.SelectedIndex > 0)
            {
                bindgrid();
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_date, mstlookup.GetAppoinmnetDateByMonthID(Convert.ToInt32(ddl_month.SelectedValue == "" ? "0" : ddl_month.SelectedValue), Convert.ToInt64(ddl_doctor.SelectedValue == "" ? "0" : ddl_doctor.SelectedValue), Convert.ToInt32(ddl_year.SelectedItem.Text == "" ? "0" : ddl_year.SelectedItem.Text)));

            }
            else
            {
                Commonfunction.Insertzeroitemindex(ddl_date);
            }
        }
        protected void ddl_month_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_doctor.SelectedIndex > 0 && ddl_month.SelectedIndex > 0 && ddl_year.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_date, mstlookup.GetAppoinmnetDateByMonthID(Convert.ToInt32(ddl_month.SelectedValue == "" ? "0" : ddl_month.SelectedValue), Convert.ToInt64(ddl_doctor.SelectedValue == "" ? "0" : ddl_doctor.SelectedValue), Convert.ToInt32(ddl_year.SelectedItem.Text == "" ? "0" : ddl_year.SelectedItem.Text)));
                bindgrid();
            }
            else
            {
                Commonfunction.Insertzeroitemindex(ddl_date);
            }
        }
        protected void ddl_date_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindgrid();
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

        protected void btnsearch_Click(object sender, System.EventArgs e)
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
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_docType.SelectedIndex == 0)
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
                if (ddldepartment.SelectedIndex == 0)
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
                if (ddl_doctor.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Doctor", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_month.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Month", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_year.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Year", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                List<AppoinmentScheduleData> objSchedule = GetSchedule(0);
                if (objSchedule.Count > 0)
                {
                    GvAppoinmentSch.DataSource = objSchedule;
                    GvAppoinmentSch.DataBind();
                    GvAppoinmentSch.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + objSchedule[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    btnupdate.Visible = true;
                    btnupdate.Attributes.Remove("disabled");
                }
                else
                {
                    divmsg3.Visible = false;
                    GvAppoinmentSch.DataSource = null;
                    GvAppoinmentSch.DataBind();
                    GvAppoinmentSch.Visible = true;
                    divmsg3.Visible = false;
                    lblresult.Visible = false;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    ddl_docType.Attributes.Remove("disabled");
                    ddldepartment.Attributes.Remove("disabled");
                    ddl_doctor.Attributes.Remove("disabled");
                    ddl_month.Attributes.Remove("disabled");
                    ddl_year.Attributes.Remove("disabled");

                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
        }

        protected void GvAppoinmentSch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Day = (Label)e.Row.FindControl("lbl_day");
                if (Day.Text == "Sunday")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Yellow;
                }
                CheckBox cb = (CheckBox)e.Row.FindControl("chkselect");
                Label available = (Label)e.Row.FindControl("lbl_availability");
                if (available.Text == "1")
                {
                    cb.Checked = true;
                }
                else
                {
                    cb.Checked = false;
                    cb.Enabled = true;
                }
            }
        }

        public List<AppoinmentScheduleData> GetSchedule(int curIndex)
        {
            AppoinmentScheduleData objSchedule = new AppoinmentScheduleData();
            AppoinmentScheduleBO objscheduleBO = new AppoinmentScheduleBO();
            objSchedule.DoctorType = Convert.ToInt32(ddl_docType.SelectedValue == "" ? "0" : ddl_docType.SelectedValue);
            objSchedule.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
            objSchedule.DoctorID = Convert.ToInt32(ddl_doctor.SelectedValue == "" ? "0" : ddl_doctor.SelectedValue);
            objSchedule.MonthID = Convert.ToInt32(ddl_month.SelectedValue == "" ? "0" : ddl_month.SelectedValue);
            objSchedule.DateID = Convert.ToInt64(ddl_date.SelectedValue == "" ? "0" : ddl_date.SelectedValue);
            objSchedule.Year = Convert.ToInt32(ddl_year.SelectedItem.Text == "" ? "0" : ddl_year.SelectedItem.Text);
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
            lblmessage.Visible = false;
            lblresult.Visible = false;
            lblresult.Text = "";
            btnupdate.Visible = false;
            btnexport.Visible = false;
            ddlexport.Visible = false;
            lblmsg.Visible = false;
            page_setting();
            Commonfunction.Insertzeroitemindex(ddl_doctor);
            ddl_docType.Attributes.Remove("disabled");
            ddldepartment.Attributes.Remove("disabled");
            ddl_doctor.Attributes.Remove("disabled");
            ddl_month.Attributes.Remove("disabled");
            ddl_year.Attributes.Remove("disabled");
            Commonfunction.Insertzeroitemindex(ddl_date);
        }

        protected void GvAppoinmentSch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvAppoinmentSch.PageIndex = e.NewPageIndex;
            bindgrid();
        }

        protected void btnupdate_Click(object sender, System.EventArgs e)
        {
            if (LogData.UpdateEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            try
            {
                List<AppoinmentScheduleData> ListAppnmt = new List<AppoinmentScheduleData>();
                AppoinmentScheduleBO objAppnmtBO = new AppoinmentScheduleBO();
                AppoinmentScheduleData objAppnmtData = new AppoinmentScheduleData();
                foreach (GridViewRow row in GvAppoinmentSch.Rows)
                {
                    CheckBox chk = (CheckBox)GvAppoinmentSch.Rows[row.RowIndex].Cells[0].FindControl("chkselect");
                    Label Schd_ID = (Label)GvAppoinmentSch.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                    Label Schd_day = (Label)GvAppoinmentSch.Rows[row.RowIndex].Cells[0].FindControl("lbl_day");
                    Label Schd_date = (Label)GvAppoinmentSch.Rows[row.RowIndex].Cells[0].FindControl("lbl_date");
                    TextBox Schd_morn = (TextBox)GvAppoinmentSch.Rows[row.RowIndex].Cells[0].FindControl("txt_morn");
                    TextBox Schd_mornSlt = (TextBox)GvAppoinmentSch.Rows[row.RowIndex].Cells[0].FindControl("txt_mor_slot");
                    TextBox Schd_aft = (TextBox)GvAppoinmentSch.Rows[row.RowIndex].Cells[0].FindControl("txt_aftern");
                    TextBox Schd_aftSlt = (TextBox)GvAppoinmentSch.Rows[row.RowIndex].Cells[0].FindControl("txt_aft_slot");
                    TextBox Schd_evn = (TextBox)GvAppoinmentSch.Rows[row.RowIndex].Cells[0].FindControl("txt_even");
                    TextBox Schd_evnSlt = (TextBox)GvAppoinmentSch.Rows[row.RowIndex].Cells[0].FindControl("txt_even_slot");
                    AppoinmentScheduleData ObjDetails = new AppoinmentScheduleData();

                    //if (Schd_morn.Text + Schd_evn.Text + Schd_aft.Text == "" && chk.Checked)
                    //{
                    //    Messagealert_.ShowMessage(lblmessage, "BlankSchedule", 0);
                    //    div1.Visible = true;
                    //    div1.Attributes["class"] = "FailAlert";
                    //    Schd_morn.Focus();
                    //    return;
                    //}
                    //else
                    //{
                    //    lblmessage.Visible = false;
                    //    div1.Visible = false;

                    //}
                    ObjDetails.ID = Convert.ToInt64(Schd_ID.Text == "" ? "0" : Schd_ID.Text);
                    ObjDetails.Morning = (Schd_morn.Text == "" ? "-- -- --- " : Schd_morn.Text);
                    ObjDetails.Afternoon = (Schd_aft.Text == "" ? "-- -- --- " : Schd_aft.Text);
                    ObjDetails.Evening = (Schd_evn.Text == "" ? "-- -- --- " : Schd_evn.Text);
                    ObjDetails.Availibility = chk.Checked ? 1 : 0;
                    ObjDetails.MorningSlots = Convert.ToInt32(Schd_mornSlt.Text == "" ? "0" : Schd_mornSlt.Text);
                    ObjDetails.AfternoonSlots = Convert.ToInt32(Schd_aftSlt.Text == "" ? "0" : Schd_aftSlt.Text);
                    ObjDetails.EveningSlots = Convert.ToInt32(Schd_evnSlt.Text == "" ? "0" : Schd_evnSlt.Text);
                    ListAppnmt.Add(ObjDetails);
                }
                objAppnmtData.XMLData = XmlConvertor.AppmntSchdXML(ListAppnmt).ToString();
                objAppnmtData.DoctorType = Convert.ToInt32(ddl_docType.SelectedValue == "" ? "0" : ddl_docType.SelectedValue);
                objAppnmtData.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
                objAppnmtData.DoctorID = Convert.ToInt64(ddl_doctor.SelectedValue.Trim() == "" ? "0" : ddl_doctor.SelectedValue);
                objAppnmtData.MonthID = Convert.ToInt32(ddl_month.SelectedValue == "" ? "0" : ddl_month.SelectedValue);
                objAppnmtData.Year = Convert.ToInt32(ddl_year.SelectedItem.Text == "" ? "0" : ddl_year.SelectedItem.Text);

                int result = objAppnmtBO.UpdateAppoinmentDetails(objAppnmtData);
                if (result == 1 || result == 2)
                {
                    Messagealert_.ShowMessage(lblmsg, result == 1 ? "save" : "update", 1);
                    div1.Attributes["class"] = "SucessAlert";
                    div1.Visible = true;
                    bindgrid();
                    return;
                }
                else if (result == 5)
                {
                    Messagealert_.ShowMessage(lblmsg, "duplicate", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                }
                else
                {
                    Messagealert_.ShowMessage(lblmsg, "system", 0);
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmsg, "system", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
            btnupdate.Attributes["disabled"] = "disabled";
        }

        protected void btnexport_Click(object sender, System.EventArgs e)
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
                    GvAppoinmentSch.BorderStyle = BorderStyle.None;
                    GvAppoinmentSch.Columns[9].Visible = false;
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
                    Response.AddHeader("content-disposition", "attachment;filename=AppoinmentScheduleDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=AppoinmentScheduleDetails.xlsx");
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
            List<AppoinmentScheduleData> appmtSch = GetSchedule(0);
            List<AppoinmentDataTOeXCEL> ListexcelData = new List<AppoinmentDataTOeXCEL>();
            int i = 0;
            foreach (AppoinmentScheduleData row in appmtSch)
            {
                AppoinmentDataTOeXCEL Ecxeclpat = new AppoinmentDataTOeXCEL();
                Ecxeclpat.Day = appmtSch[i].Day;
                Ecxeclpat.Morning = appmtSch[i].Morning;
                Ecxeclpat.MorningSlots = Commonfunction.Getrounding(appmtSch[i].MorningSlots.ToString());
                Ecxeclpat.Afternoon = appmtSch[i].Afternoon;
                Ecxeclpat.AfternoonSlots = Commonfunction.Getrounding(appmtSch[i].AfternoonSlots.ToString());
                Ecxeclpat.Evening = appmtSch[i].Evening;
                Ecxeclpat.EveningSlots = Commonfunction.Getrounding(appmtSch[i].EveningSlots.ToString());
                Ecxeclpat.date = appmtSch[i].date;
                Ecxeclpat.Status = appmtSch[i].Status;
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