using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.PatientData;
using Mediqura.CommonData.MedEmergencyData;
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
using Mediqura.BOL.MedEmergencyBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.AdmissionData;
using Mediqura.BOL.AdmissionBO;

namespace Mediqura.Web.MedReport
{
    public partial class EmergencyAdmissionReport : BasePage
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
           
            Commonfunction.PopulateDdl(ddldeischargestatus, mstlookup.GetLookupsList(LookupName.DischargeStatus));
            Commonfunction.PopulateDdl(ddl_emrgDocList, mstlookup.GetLookupsList(LookupName.EmergencyDoc));
            Commonfunction.PopulateDdl(ddlgender, mstlookup.GetLookupsList(LookupName.Gender));
            //Commonfunction.Insertzeroitemindex(ddldoctor);
            btnprints.Attributes["disabled"] = "disabled";
        }
        //protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddldepartment.SelectedIndex > 0)
        //    {
        //        MasterLookupBO mstlookup = new MasterLookupBO();
        //        Commonfunction.PopulateDdl(ddldoctor, mstlookup.GetDepartmentDoctor(Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue)));
        //    }
        //}
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
            bindgrid();
        }
        protected void bindgrid()
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
                List<EmrgAdmissionData> objemrg = GetEmrgList(0);
                if (objemrg.Count > 0)
                {
                    gvEmrglist.DataSource = objemrg;
                    gvEmrglist.DataBind();
                    gvEmrglist.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objemrg[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    btnprints.Attributes.Remove("disabled");
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    gvEmrglist.DataSource = null;
                    gvEmrglist.DataBind();
                    gvEmrglist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
            }
        }
        public List<EmrgAdmissionData> GetEmrgList(int curIndex)
        {

            EmrgAdmissionData objEmrgdata = new EmrgAdmissionData();
            EmrgAdmissionBO objemrgBO = new EmrgAdmissionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objEmrgdata.DateFrom = from;
            objEmrgdata.DateTo = To;
            //objEmrgdata.DeptID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
            objEmrgdata.DocID = Convert.ToInt64(ddl_emrgDocList.SelectedValue == "" ? "0" : ddl_emrgDocList.SelectedValue);
            objEmrgdata.DischargeStatus = Convert.ToInt32(ddldeischargestatus.SelectedValue == "" ? "0" : ddldeischargestatus.SelectedValue);
            objEmrgdata.GenID = Convert.ToInt32(ddlgender.SelectedValue == "" ? "0" : ddlgender.SelectedValue);
            objEmrgdata.Agefrom = Convert.ToInt32(txtagefrom.Text == "" ? "0" : txtagefrom.Text.Trim());
            objEmrgdata.Ageto = Convert.ToInt32(txtageto.Text == "" ? "200" : txtageto.Text.Trim());
            return objemrgBO.GetEmrgListReport(objEmrgdata);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
          
            //ddldoctor.SelectedIndex = 0;
            ddlgender.SelectedIndex = 0;
            txtdatefrom.Text = "";
            txtto.Text = "";
            txtagefrom.Text = "";
            txtageto.Text = "";
            gvEmrglist.DataSource = null;
            gvEmrglist.DataBind();
            gvEmrglist.Visible = false;
            lblresult.Visible = false;
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage2.Visible = false;
            lblresult.Visible = false;
            div3.Visible = false;
            btnprints.Attributes["disabled"] = "disabled";
            ddldeischargestatus.SelectedIndex = 0;
        }
        protected DataTable GetDatafromDatabase()
        {
            List<EmrgAdmissionData> AdmissionDetails = GetEmrgList(0);
            List<EMRGAdmissionListReportDataTOeXCEL> ListexcelData = new List<EMRGAdmissionListReportDataTOeXCEL>();
            int i = 0;
            foreach (EmrgAdmissionData row in AdmissionDetails)
            {
                EMRGAdmissionListReportDataTOeXCEL Ecxeclpat = new EMRGAdmissionListReportDataTOeXCEL();
                Ecxeclpat.EmrgNo = AdmissionDetails[i].EmrgNo;
                Ecxeclpat.UHID = AdmissionDetails[i].UHID;
                Ecxeclpat.PatientName = AdmissionDetails[i].PatientName;
                Ecxeclpat.GenderName = AdmissionDetails[i].GenderName;
                Ecxeclpat.Age = AdmissionDetails[i].Age;
                Ecxeclpat.Department = AdmissionDetails[i].Department;
                Ecxeclpat.AdmissionDoctor = AdmissionDetails[i].AdmissionDoctor;
                Ecxeclpat.Discharge = AdmissionDetails[i].Discharge;
                Ecxeclpat.AdmissionDate = AdmissionDetails[i].AdmissionDate;
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
                    gvEmrglist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvEmrglist.Columns[6].Visible = false;
                    gvEmrglist.Columns[7].Visible = false;
                    gvEmrglist.Columns[8].Visible = false;

                    gvEmrglist.RenderControl(hw);
                    gvEmrglist.HeaderRow.Style.Add("width", "15%");
                    gvEmrglist.HeaderRow.Style.Add("font-size", "10px");
                    gvEmrglist.Style.Add("text-decoration", "none");
                    gvEmrglist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvEmrglist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=EmergencyAdmissionReport.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=EmergencyAdmissionReport.xlsx");
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