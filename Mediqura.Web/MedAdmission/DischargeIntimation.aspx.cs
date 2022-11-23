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

namespace Mediqura.Web.MedAdmission
{
    public partial class DischargeIntimation : BasePage
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
            Commonfunction.PopulateDdl(ddlintimatedby, mstlookup.GetLookupsList(LookupName.Doctor));
            Commonfunction.PopulateDdl(ddlintimationby, mstlookup.GetLookupsList(LookupName.Doctor));
            btnprint.Attributes["disabled"] = "disabled";
            btnprints.Attributes["disabled"] = "disabled";
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPNo(string prefixText, int count, string contextKey)
        {
            IPData Objpaic = new IPData();
            AdmissionBO objInfoBO = new AdmissionBO();
            List<IPData> getResult = new List<IPData>();
            Objpaic.IPNo = prefixText;
            getResult = objInfoBO.getIPNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPNoList(string prefixText, int count, string contextKey)
        {
            DischargeIntimationData Objpaic = new DischargeIntimationData();
            DischargeIntimationBO objInfoBO = new DischargeIntimationBO();
            List<DischargeIntimationData> getResult = new List<DischargeIntimationData>();
            Objpaic.IPNo = prefixText;
            getResult = objInfoBO.GetIPNoList(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetDocName(string prefixText, int count, string contextKey)
        {
            DischargeIntimationData Objpaic = new DischargeIntimationData();
            DischargeIntimationBO objInfoBO = new DischargeIntimationBO();
            List<DischargeIntimationData> getResult = new List<DischargeIntimationData>();
            Objpaic.EmpName = prefixText;
            getResult = objInfoBO.GetDocName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmpName.ToString());
            }
            return list;
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
        protected void txt_IPNo_TextChanged(object sender, EventArgs e)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.IPNo = txt_IPNo.Text.Trim() == "" ? "" : txt_IPNo.Text.Trim();
            getResult = objInfoBO.GetPatientDetailsByIPNo(Objpaic);
            if (getResult.Count > 0)
            {
                txt_name.Text = getResult[0].PatientName.ToString();
                txt_gender.Text = getResult[0].GenderName.ToString();
                txt_age.Text = getResult[0].Agecount.ToString();
                txt_admissiondoctor.Text = getResult[0].DoctorName.ToString();
                txtdepartment.Text = getResult[0].DepartmentName.ToString();
                txt_address.Text = getResult[0].Address.ToString();
                txt_admissionDate.Text = getResult[0].AdmissionDate.ToString("dd/MM/yyyy");
                btnsave.Attributes.Remove("disabled");
            }
            else
            {
                txt_name.Text = "";
                txt_gender.Text = "";
                txt_age.Text = "";
                txt_admissiondoctor.Text = "";
                txtdepartment.Text = "";
                txt_admissionDate.Text = "";
                txt_IPNo.Text = "";
            }
        }
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            if (txt_patientNames.Text != "")
            {
                bindgridList();
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (LogData.SaveEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "SaveEnable", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                if (txt_IPNo.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "IPNo", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_IPNo.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddlintimatedby.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "OTintimatedby", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_IPNo.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_remarks.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Remarks", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_remarks.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                DischargeIntimationData objDischargeIntimationData = new DischargeIntimationData();
                DischargeIntimationBO objLabGroupTypeMasterBO = new DischargeIntimationBO();
                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                objDischargeIntimationData.IPNo = txt_IPNo.Text == "" ? null : txt_IPNo.Text.Trim();
                objDischargeIntimationData.EmployeeID = LogData.EmployeeID;
                objDischargeIntimationData.DischargeintimatedBy = Convert.ToInt64(ddlintimatedby.SelectedValue == "" ? "0" : ddlintimatedby.SelectedValue);
                objDischargeIntimationData.Remarks = txt_remarks.Text.Trim();
                objDischargeIntimationData.HospitalID = LogData.HospitalID;
                objDischargeIntimationData.FinancialYearID = LogData.FinancialYearID;
                objDischargeIntimationData.ActionType = Enumaction.Insert;
                int result = objLabGroupTypeMasterBO.UpdateDischargeIntimationDetails(objDischargeIntimationData);
                if (result == 1)
                {
                    lblmessage.Visible = true;
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    btnsave.Attributes["disabled"] = "disabled";
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    btnprint.Attributes.Remove("disabled");
                }
                else if (result == 5)
                {
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
        }
        protected void clearall()
        {
            txt_IPNo.Text = "";
            txt_name.Text = "";
            txt_age.Text = "";
            txt_gender.Text = "";
            txt_admissionDate.Text = "";
            txt_address.Text = "";
            ddlintimatedby.SelectedIndex = 0;
            txt_remarks.Text = "";
            txt_admissiondoctor.Text = "";
            txtdepartment.Text = "";
            txt_admissionDate.Text = "";
        }
        protected void bindgridList()
        {
            try
            {
                if (LogData.SearchEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (txtdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "VaidDate.", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
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
                        Messagealert_.ShowMessage(lblmessage2, "VaidDate", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                        txtto.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg3.Visible = false;
                }
                List<DischargeIntimationData> objdeposit = GetDisch_IntimationList2(0);
                if (objdeposit.Count > 0)
                {
                    gvDischargelist.DataSource = objdeposit;
                    gvDischargelist.DataBind();
                    gvDischargelist.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    btnprints.Attributes.Remove("disabled");
                }
                else
                {
                    gvDischargelist.DataSource = null;
                    gvDischargelist.DataBind();
                    gvDischargelist.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div3.Attributes["class"] = "SucessAlert";
                div3.Visible = true;
            }

        }

        private List<DischargeIntimationData> GetDisch_IntimationList(int p)
        {
            DischargeIntimationData objDischargeIntimationData = new DischargeIntimationData();
            DischargeIntimationBO objlabsubgroupBO = new DischargeIntimationBO();
            objDischargeIntimationData.IPNo = txt_IPNo.Text == "" ? null : txt_IPNo.Text;
            objDischargeIntimationData.PatientName = txt_name.Text == "" ? null : txt_name.Text;
            objDischargeIntimationData.GenderName = txt_gender.Text == "" ? null : txt_gender.Text;
            objDischargeIntimationData.PatientAddress = txt_address.Text == "" ? null : txt_address.Text;
            objDischargeIntimationData.Age = Convert.ToInt32(txt_age.Text == "" ? null : txt_age.Text);
            objDischargeIntimationData.AdmissionDate = Convert.ToDateTime(txt_admissionDate.Text == "" ? null : txt_admissionDate.Text);
            return objlabsubgroupBO.SearchDisch_intimationDetails(objDischargeIntimationData);
        }
        public List<DischargeIntimationData> GetDisch_IntimationList2(int curIndex)
        {
            DischargeIntimationData objpat = new DischargeIntimationData();
            DischargeIntimationBO objbillingBO = new DischargeIntimationBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.IPNo = txtautoIPNo.Text == "" ? null : txtautoIPNo.Text;
            objpat.PatientName = txt_patientNames.Text == "" ? null : txt_patientNames.Text.Trim();
            objpat.DischargeintimatedBy = Convert.ToInt64(ddlintimationby.SelectedValue == "" ? "0" : ddlintimationby.SelectedValue);
            objpat.IsActive = ddl_status.SelectedValue == "0" ? true : false;
            objpat.DateFrom = from;
            objpat.DateTo = To;
            return objbillingBO.GetDisch_IntimationList2(objpat);
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            clearall();
            lblmessage.Visible = false;
            lblmessage2.Visible = false;
            lblresult.Visible = false;
            btnprint.Attributes["disabled"] = "disabled";
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgridList();
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            clearIntimationList();
        }
        protected void clearIntimationList()
        {
            txtautoIPNo.Text = "";
            txt_patientNames.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            ddlintimationby.SelectedIndex = 0;
            lblmessage2.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            div1.Visible = false;
            gvDischargelist.DataSource = null;
            gvDischargelist.DataBind();
            gvDischargelist.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            div3.Visible = false;
            ddlintimationby.SelectedIndex = 0;
            btnprints.Attributes["disabled"] = "disabled";
        }
        protected void txtautoIPNo_TextChanged(object sender, EventArgs e)
        {
            if (txtautoIPNo.Text != "")
            {
                bindgridList();
            }
        }
        protected void gvDischargelist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "DeleteEnable", 0);
                        div3.Visible = true;
                        div3.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage2.Visible = false;
                    }
                    DischargeIntimationData objIntimation = new DischargeIntimationData();
                    DischargeIntimationBO objIntimationBO = new DischargeIntimationBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvDischargelist.Rows[i];
                    Label Disch_ID = (Label)gr.Cells[0].FindControl("lblID");
                    Label IPNo = (Label)gr.Cells[0].FindControl("IPNo");
                    Label name = (Label)gr.Cells[0].FindControl("lblname");
                    Label date = (Label)gr.Cells[0].FindControl("lbladmittedon");
                    Label doc = (Label)gr.Cells[0].FindControl("lbladmissiondoc");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objIntimation.Remarks = txtremarks.Text;
                    }
                    objIntimation.DischargeID = Convert.ToInt64(Disch_ID.Text);
                    objIntimation.EmployeeID = LogData.EmployeeID;
                    objIntimation.HospitalID = LogData.HospitalID;
                    objIntimation.IPaddress = LogData.IPaddress;
                    int Result = objIntimationBO.DeleteDischargeIntimationByID(objIntimation);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        lblmessage2.Visible = true;
                        div3.Visible = true;
                        div3.Attributes["class"] = "SucessAlert";
                        bindgridList();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage2, "system", 0);
                        div3.Visible = true;
                        div3.Attributes["class"] = "FailAlert";
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
                div3.Visible = true;
                div3.Attributes["class"] = "FailAlert";
            }
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "ExportEnable", 0);
                divmsg3.Visible = true;
                divmsg3.Attributes["class"] = "FailAlert";
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
                divmsg3.Visible = true;
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
                    gvDischargelist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvDischargelist.Columns[6].Visible = false;
                    gvDischargelist.Columns[7].Visible = false;


                    gvDischargelist.RenderControl(hw);
                    gvDischargelist.HeaderRow.Style.Add("width", "15%");
                    gvDischargelist.HeaderRow.Style.Add("font-size", "10px");
                    gvDischargelist.Style.Add("text-decoration", "none");
                    gvDischargelist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvDischargelist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=DischargeIntimationDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=DischargeIntimationDetails.xlsx");
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
            List<DischargeIntimationData> AdmissionDetails = GetDisch_IntimationList2(0);
            List<DischargeListDataTOeXCEL> ListexcelData = new List<DischargeListDataTOeXCEL>();
            int i = 0;
            foreach (DischargeIntimationData row in AdmissionDetails)
            {
                DischargeListDataTOeXCEL Ecxeclpat = new DischargeListDataTOeXCEL();
                Ecxeclpat.IPNo = AdmissionDetails[i].IPNo;

                Ecxeclpat.PatientName = AdmissionDetails[i].PatientName;
                Ecxeclpat.IntimationDate = AdmissionDetails[i].IntimationDate;
                Ecxeclpat.DischargeDoc = AdmissionDetails[i].DischargeDoc;

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
        protected void gvDischargelist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDischargelist.PageIndex = e.NewPageIndex;
            bindgridList();
        }
        protected void btnprint_Click(object sender, EventArgs e)
        {
            if (LogData.PrintEnable == 0)
            {
                btnprint.Attributes["disabled"] = "disabled";
            }
            else
            {
                btnprint.Attributes.Remove("disabled");
            }
        }
        protected void btnprints_Click(object sender, EventArgs e)
        {
            if (LogData.PrintEnable == 0)
            {
                btnprints.Attributes["disabled"] = "disabled";
            }
            else
            {
                btnprints.Attributes.Remove("disabled");
            }
        }
    }
}