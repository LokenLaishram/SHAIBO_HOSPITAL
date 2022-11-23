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
    public partial class Treatmentstatus : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txt_date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
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

            bindgrid();
        }
        protected void txtname_TextChanged(object sender, EventArgs e)
        {
            if (txtname.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "patientname", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                txtname.Focus();
                return;
            }
            else
            {
                bindgrid();
            }
        }
        protected void txtdocname_TextChanged(object sender, EventArgs e)
        {
            if (txt_docname.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "patientname", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                txt_docname.Focus();
                return;
            }
            else
            {
                bindgrid();
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetDoctorName(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.DoctorName = prefixText;
            getResult = objInfoBO.GetDoctorName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].DoctorName.ToString());
            }
            return list;
        }
        protected void bindgrid()
        {
            try
            {
                List<PatientData> patientdetails = GetTreatmentStatus(0);
                if (patientdetails.Count > 0)
                {
                    GvOpdPatientTreatment.DataSource = patientdetails;
                    GvOpdPatientTreatment.DataBind();
                    GvOpdPatientTreatment.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + patientdetails[0].MaximumRows + " Record(s) found", 1);
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    div2.Attributes["class"] = "SucessAlert";
                    div2.Visible = true;
                
                }
                else
                {
                    GvOpdPatientTreatment.DataSource = null;
                    GvOpdPatientTreatment.DataBind();
                    GvOpdPatientTreatment.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult.Visible = false;
                    div2.Visible = false;
                    lblmessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
             
            }
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

        protected void txtUHID_TextChanged(object sender, EventArgs e)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.UHID = Convert.ToInt64(txtUHID.Text.Trim() == "" ? "0" : txtUHID.Text.Trim());
            getResult = objInfoBO.GetPatientDetailsByUHID(Objpaic);
            if (getResult.Count > 0)
            {
                txtname.Text = getResult[0].PatientName.ToString();
                txtaddress.Text = getResult[0].Address.ToString();
               }
            else
            {
                txtname.Text = "";
                txtaddress.Text = "";
                txtUHID.Text = "";
                txtUHID.Focus();
            }
            bindgrid();

        }
        public List<PatientData> GetTreatmentStatus(int curIndex)
        {

            PatientData objOPD = new PatientData();
            RegistrationBO objstdBO = new RegistrationBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objOPD.UHID = Convert.ToInt64(txtUHID.Text == "" ? "0" : txtUHID.Text);
            objOPD.PatientName = txtname.Text.Trim() == "" ? null : txtname.Text.Trim();
            objOPD.DoctorName = txt_docname.Text.Trim() == "" ? null : txt_docname.Text.Trim();
            DateTime date = txt_date.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_date.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objOPD.TodayDate = date;
    
            return objstdBO.OPDTreatmentStatus(objOPD);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtUHID.Text = "";
            txtname.Text = "";
            txt_docname.Text = "";
            txtaddress.Text = "";
            GvOpdPatientTreatment.DataSource = null;
            GvOpdPatientTreatment.DataBind();
            GvOpdPatientTreatment.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            btnexport.Visible = false;
            ddlexport.Visible = false;
            txt_date.Text = System.DateTime.Now.ToString("dd/MM/yyyy"); 
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
                div2.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        protected void GvOpdPatientTreatment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button status = (Button)e.Row.FindControl("btnstatus");
                if (status.Text == "1")
                {
                    status.Text = "Examined";
                }
                else
                {
                    status.Text = "Pending";
                }

            }
        }
        protected void GvOpdPatientTreatment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "statusupdate")
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    PatientData objOPD = new PatientData();
                    RegistrationBO objstdBO = new RegistrationBO();
                    int i = Convert.ToInt32(e.CommandArgument.ToString());
                    GridViewRow pt = GvOpdPatientTreatment.Rows[i];
                    Label UhidNo = (Label)pt.Cells[0].FindControl("lblvOpdUHID");
                    Label ID = (Label)pt.Cells[0].FindControl("lblID");
                    Label status = (Label)pt.Cells[0].FindControl("lbl_status");
                    Button btnstatus = (Button)pt.Cells[0].FindControl("btnstatus");
                   
                    objOPD.UHID = Convert.ToInt64(UhidNo.Text);
                    objOPD.ID = Convert.ToInt64(ID.Text);
                    int result = objstdBO.UpdateTreatmentStatus(objOPD);
                   
                    if (result == 1)
                    {
                        bindgrid();
                        btnstatus.Text = "Examined";
                    }
                    else
                    {
                        bindgrid();
                        status.Text = "Pending";
                    }
                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
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
                Response.AddHeader("content-disposition", "attachment;filename=TreatmentStatus.xlsx");
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
        protected DataTable GetExcelData()
        {
            List<PatientData> PatientDetails = GetTreatmentStatus(0);
            List<IPDTreatmentStatusDatatoExcel> ListexcelData = new List<IPDTreatmentStatusDatatoExcel>();
            int i = 0;
            foreach (PatientData row in PatientDetails)
            {
                IPDTreatmentStatusDatatoExcel Ecxeclpat = new IPDTreatmentStatusDatatoExcel();
                Ecxeclpat.UHID = PatientDetails[i].UHID;
                Ecxeclpat.PatientName = PatientDetails[i].PatientName;
                Ecxeclpat.Height = PatientDetails[i].Height;
                Ecxeclpat.Weight = PatientDetails[i].Weight;
                Ecxeclpat.BP = PatientDetails[i].BP;
                Ecxeclpat.EmpName = PatientDetails[i].EmpName;
                PatientData obj = new PatientData();
                if (PatientDetails[i].Status == 1)
                {
                    PatientDetails[i].treatmentstatus = "Examined";
                    Ecxeclpat.treatmentstatus = PatientDetails[i].treatmentstatus;
                }
                else
                {
                    PatientDetails[i].treatmentstatus = "Pending";
                    Ecxeclpat.treatmentstatus = PatientDetails[i].treatmentstatus;
           
                }


                ListexcelData.Add(Ecxeclpat);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }

        public void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvOpdPatientTreatment.BorderStyle = BorderStyle.None;
                    GvOpdPatientTreatment.RenderControl(hw);
                    GvOpdPatientTreatment.HeaderRow.Style.Add("width", "15%");
                    GvOpdPatientTreatment.HeaderRow.Style.Add("font-size", "10px");
                    GvOpdPatientTreatment.Style.Add("text-decoration", "none");
                    GvOpdPatientTreatment.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvOpdPatientTreatment.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=TreatmentStatus.pdf");
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
       }
}
