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

namespace Mediqura.Web.MedOPD
{
    public partial class Consultingsheet : BasePage
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
            Commonfunction.PopulateDdl(ddldoctortype, mstlookup.GetLookupsList(LookupName.OPDoctorType));
            Commonfunction.Insertzeroitemindex(ddlconsultant);
            btnprints.Attributes["disabled"] = "disabled";
            btnprintcs.Attributes["disabled"] = "disabled";
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> Getopdbillauto(string prefixText, int count, string contextKey)
        {
            OPDbillingData Objpaic = new OPDbillingData();
            OPDbillingBO objInfoBO = new OPDbillingBO();
            List<OPDbillingData> getResult = new List<OPDbillingData>();
            Objpaic.BillNo = prefixText;
            getResult = objInfoBO.GetautoOPbills(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].BillNo.ToString());
            }
            return list;
        }
        protected void txtbillno_TextChanged(object sender, EventArgs e)
        {
            OPDbillingData Objpaic = new OPDbillingData();
            OPDbillingBO objInfoBO = new OPDbillingBO();
            List<OPDbillingData> Result = new List<OPDbillingData>();
            Objpaic.BillNo = txtbill.Text.Trim();
            Result = objInfoBO.GetPatientDetailbybillNo(Objpaic);
            if (Result.Count > 0)
            {
                txtname.Text = Result[0].PatientName.ToString();
                txtUHID.Text = Result[0].UHID.ToString();
                txtGender.Text = Result[0].Gender.ToString();
                txtAge.Text = Result[0].Ages.ToString();
                txtAddress.Text = Result[0].Address.ToString();
                ddldepartment.SelectedValue = Result[0].DeptID.ToString();
                ddldoctortype.SelectedValue = Result[0].DoctorTypeID.ToString();
                hdndoctortype.Value = Result[0].DoctorTypeID.ToString();
                hdndepartmentID.Value = Result[0].DeptID.ToString();
                hdndoctorID.Value = Result[0].DocID.ToString();
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddlconsultant, mstlookup.GetDoctorBydepartmentID(Convert.ToInt32(Result[0].DeptID.ToString()), Convert.ToInt32(Result[0].DoctorTypeID.ToString())));
                ddlconsultant.SelectedValue = Result[0].DocID.ToString();
                divmsg1.Visible = false;
                lblmessage.Visible = false;
                hdnopno.Value = null;
                btnsave.Attributes.Remove("disabled");
            }
            else
            {
                hdndoctortype.Value = null;
                hdndepartmentID.Value = null;
                hdndoctorID.Value = null;
                txtname.Text = "";
                txtUHID.Text = "";
                txtGender.Text = "";
                txtAddress.Text = "";
                txtAge.Text = "";
                bindddl();
                Messagealert_.ShowMessage(lblmessage, "ConsultattionBill", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txtbill.Focus();
                return;
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
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
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddldoctortype.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "DoctorType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddldoctortype.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddldepartment.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Department", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddldepartment.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddlconsultant.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Consultant", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddlconsultant.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtdate.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdate.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDatefrom", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtdate.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (txtdateto.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdateto.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDateto", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtdateto.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                List<PatientData> patientdetails = GetPatientConsultingDataOPD(0);
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
                    GvOpdPatientConsulting.DataSource = patientdetails;
                    GvOpdPatientConsulting.DataBind();
                    GvOpdPatientConsulting.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + patientdetails[0].MaximumRows + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    lblmessage.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;

                }
                else
                {
                    GvOpdPatientConsulting.DataSource = null;
                    GvOpdPatientConsulting.DataBind();
                    GvOpdPatientConsulting.Visible = true;
                    lblresult.Visible = false;
                    divmsg3.Visible = false;
                    lblmessage.Visible = true;
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPatientName(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        public List<PatientData> GetPatientConsultingDataOPD(int curIndex)
        {
            PatientData objOPD = new PatientData();
            RegistrationBO objstdBO = new RegistrationBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime Datefrom = txtdate.Text.Trim() == "" ? System.DateTime.Today : DateTime.Parse(txtdate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime Dateto = txtdateto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objOPD.PatientName = txtname.Text.Trim();
            objOPD.UHID = Convert.ToInt64(txtUHID.Text == "" ? "0" : txtUHID.Text);
            objOPD.DoctorTypeID = Convert.ToInt32(ddldoctortype.SelectedValue == "" ? "0" : ddldoctortype.SelectedValue);
            objOPD.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
            objOPD.DoctorID = Convert.ToInt32(ddlconsultant.SelectedValue == "" ? "0" : ddlconsultant.SelectedValue);
            objOPD.DateFrom = Datefrom;
            objOPD.DateTo = Dateto;
            return objstdBO.Getconsultingsheetlist(objOPD);
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                PatientData objpat = new PatientData();
                RegistrationBO objpatBO = new RegistrationBO();
                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);

                if (LogData.SaveEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "SaveEnable", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (txtbill.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Billno", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtbill.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (txtHeight.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Height", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtHeight.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (txtWeight.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Weight", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtWeight.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (txtBP.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "BP", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtWeight.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                if (txtpulse.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Pulse", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtpulse.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                } if (ddldoctortype.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "DoctorType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddldoctortype.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddldepartment.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Department", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddldepartment.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddlconsultant.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Consultant", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddlconsultant.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                objpat.UHID = Convert.ToInt64(txtUHID.Text);
                objpat.Height = float.Parse(txtHeight.Text == "" ? "0.0" : txtHeight.Text);
                objpat.Weight = float.Parse(txtWeight.Text == "" ? "0.0" : txtWeight.Text);
                objpat.BP = txtBP.Text.Trim();
                objpat.PulseRate = Convert.ToInt32(txtpulse.Text == "" ? "0" : txtpulse.Text);
                objpat.DoctorTypeID = Convert.ToInt32(hdndoctortype.Value == null ? "0" : hdndoctortype.Value);
                objpat.DepartmentID = Convert.ToInt32(hdndepartmentID.Value == null ? "0" : hdndepartmentID.Value);
                objpat.DoctorID = Convert.ToInt32(hdndoctorID.Value == null ? "0" : hdndoctorID.Value);
                objpat.Remarks = txtremarks.Text.Trim();
                objpat.FinancialYearID = LogData.FinancialYearID;
                objpat.HospitalID = LogData.HospitalID;
                objpat.EmployeeID = LogData.EmployeeID;

                int results = objpatBO.AddPatientConsultantSheet(objpat);
                if (results >= 1)
                {
                    if (LogData.PrintEnable == 0)
                    {
                        btnprintcs.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btnprintcs.Attributes.Remove("disabled");
                    }
                    hdndepartmentID.Value = null;
                    hdndoctorID.Value = null;
                    hdndoctortype.Value = null;
                    btnsave.Attributes["disabled"] = "disabled";
                    hdnopno.Value = results.ToString();
                    bindgrid();
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    return;
                }
                else if (results == 0)
                {
                    btnsave.Attributes.Remove("disabled");
                    Messagealert_.ShowMessage(lblmessage, "DupConsulatation", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }

        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtUHID.Text = "";
            GvOpdPatientConsulting.DataSource = null;
            GvOpdPatientConsulting.DataBind();
            GvOpdPatientConsulting.Visible = false;
            txtAge.Text = "";
            txtAddress.Text = "";
            txtname.Text = "";
            txtGender.Text = "";
            lblmessage.Visible = false;
            lblresult.Visible = false;
            bindddl();
            ddldoctortype.SelectedIndex = 0;
            ddldepartment.SelectedIndex = 0;
            ddlconsultant.SelectedIndex = 0;
            divmsg1.Visible = false;
            txtHeight.Text = "";
            txtWeight.Text = "";
            txtbill.Text = "";
            txtBP.Text = "";
            txtpulse.Text = "";
            txtdateto.Text = "";
            txtdate.Text = "";
            lblmessage.Visible = false;
            divmsg1.Visible = false;
            lblresult.Visible = false;
            divmsg3.Visible = false;
            btnprints.Attributes["disabled"] = "disabled";
            btnprintcs.Attributes["disabled"] = "disabled";
            btnsave.Attributes.Remove("disabled");
            hdndepartmentID.Value = null;
            hdndoctorID.Value = null;
            hdndoctortype.Value = null;
            hdnopno.Value = null;
            btnexport.Visible = false;
            ddlexport.Visible = false;

        }
        protected void ExportoExcel()
        {

            DataTable dt = GetDatafromDatabaseOPD();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Patient List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=PatientList.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblmessage, "save", 1);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "SucessAlert";
                return;

            }
        }
        protected DataTable GetDatafromDatabaseOPD()
        {
            List<PatientData> PatientDetails = GetPatientConsultingDataOPD(0);
            List<ConsultingshhettoExcel> ListexcelData = new List<ConsultingshhettoExcel>();
            int i = 0;
            foreach (PatientData row in PatientDetails)
            {
                ConsultingshhettoExcel Ecxeclpat = new ConsultingshhettoExcel();
                Ecxeclpat.Serial = PatientDetails[i].Serial;
                Ecxeclpat.UHID = PatientDetails[i].UHID;
                Ecxeclpat.PatientName = PatientDetails[i].PatientName;
                Ecxeclpat.Address = PatientDetails[i].Address;
                Ecxeclpat.Height = PatientDetails[i].Height;
                Ecxeclpat.Weight = PatientDetails[i].Weight;
                Ecxeclpat.BP = PatientDetails[i].BP;
                Ecxeclpat.PulseRate = PatientDetails[i].PulseRate;
                ListexcelData.Add(Ecxeclpat);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldepartment.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddlconsultant, mstlookup.GetDoctorBydepartmentID(Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue), Convert.ToInt32(ddldoctortype.SelectedValue == "" ? "0" : ddldoctortype.SelectedValue)));
            }
            else
            {
                ddldepartment.SelectedValue = "0";
            }
        }
        protected void ddldoctortye_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldepartment.SelectedIndex > 0)
            {
                ddldepartment.SelectedIndex = 0;
            }
            else
            {
                ddldepartment.SelectedIndex = 0;
            }
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
                divmsg1.Attributes["class"] = "FailAlert";
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
                    GvOpdPatientConsulting.BorderStyle = BorderStyle.None;
                    GvOpdPatientConsulting.RenderControl(hw);
                    GvOpdPatientConsulting.HeaderRow.Style.Add("width", "15%");
                    GvOpdPatientConsulting.HeaderRow.Style.Add("font-size", "10px");
                    GvOpdPatientConsulting.Style.Add("text-decoration", "none");
                    GvOpdPatientConsulting.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvOpdPatientConsulting.Style.Add("font-size", "8px");
                    GvOpdPatientConsulting.Columns[9].Visible = false;
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=ConusltingList.pdf");
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