using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedLabBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedLab;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedReport
{
    public partial class LabReport : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
                ddlbind();
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_patient_type, mstlookup.GetLookupsList(LookupName.PatientCatagory));
            Commonfunction.PopulateDdl(ddl_labgroup, mstlookup.GetLookupsList(LookupName.LabGroups));
            Commonfunction.PopulateDdl(ddl_labsubgroup, mstlookup.GetSubGroupByGroupID(0));
            Commonfunction.PopulateDdl(ddl_labTestName, mstlookup.GetTestNameBySubGroupID(0));
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtDateto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
        protected void ddl_labgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_labgroup.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_labsubgroup, mstlookup.GetSubGroupByGroupID(Convert.ToInt32(ddl_labgroup.SelectedValue)));
               
            }
        }
        protected void ddl_labsubgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_labsubgroup.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_labTestName, mstlookup.GetTestNameBySubGroupID(Convert.ToInt32(ddl_labsubgroup.SelectedValue)));
                
            }
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
        
           bindgrid(0);
        }
        private void bindgrid(int page)
        {
            try
            {

                List<LadReportCollectionData> lstemp = getRadioTestList(page);
                if (lstemp.Count > 0)
                {
                    GvPatientList.DataSource = lstemp;
                    GvPatientList.DataBind();
                    GvPatientList.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvPatientList.DataSource = null;
                    GvPatientList.DataBind();
                    GvPatientList.Visible = true;
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        private List<LadReportCollectionData> getRadioTestList(int p)
        {
            LadReportCollectionData objData = new LadReportCollectionData();
            LabReportCollectionBO objBO = new LabReportCollectionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objData.PatientType = Convert.ToInt32(ddl_patient_type.SelectedValue == "0" ? null : ddl_patient_type.SelectedValue);
            objData.LabGrpID = Convert.ToInt32(ddl_labgroup.SelectedValue == "0" ? null : ddl_labgroup.SelectedValue);
            objData.LabSubGrpID = Convert.ToInt32(ddl_labsubgroup.SelectedValue == "0" ? null : ddl_labsubgroup.SelectedValue);
            objData.TestID = Convert.ToInt32(ddl_labTestName.SelectedValue == "0" ? null : ddl_labTestName.SelectedValue);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtDateto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtDateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objData.DateFrom = from;
            objData.DateTo = To;
            return objBO.GetLabTestList(objData);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ddl_labsubgroup.SelectedIndex = 0;
            ddl_labTestName.SelectedIndex = 0;
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtDateto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            GvPatientList.DataSource = null;
            GvPatientList.DataBind();
            GvPatientList.Visible = true;
            lblresult.Visible = false;
            ddl_labgroup.SelectedIndex = 0;
            ddl_patient_type.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
         }
        protected DataTable GetDatafromDatabase()
        {
            List<LadReportCollectionData> Details = getRadioTestList(0);
            List<LadReportCollectionDataTOeXCEL> ListexcelData = new List<LadReportCollectionDataTOeXCEL>();
            int i = 0;
            foreach (LadReportCollectionData row in Details)
            {
                LadReportCollectionDataTOeXCEL Ecxeclpat = new LadReportCollectionDataTOeXCEL();
                Ecxeclpat.UHID   = Details[i].UHID;
                Ecxeclpat.InVnumber = Details[i].InVnumber;
                Ecxeclpat.PatientName = Details[i].PatientName;
                Ecxeclpat.TestName = Details[i].TestName;
                Ecxeclpat.TestDate = Details[i].TestDate;
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
                    GvPatientList.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox

                    GvPatientList.Columns[11].Visible = false;
                    GvPatientList.Columns[12].Visible = false;
                    GvPatientList.Columns[13].Visible = false;
                    GvPatientList.Columns[14].Visible = false;
                    GvPatientList.RenderControl(hw);
                    GvPatientList.HeaderRow.Style.Add("width", "15%");
                    GvPatientList.HeaderRow.Style.Add("font-size", "10px");
                    GvPatientList.Style.Add("text-decoration", "none");
                    GvPatientList.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvPatientList.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=LabReportDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=LabReportDetails.xlsx");
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