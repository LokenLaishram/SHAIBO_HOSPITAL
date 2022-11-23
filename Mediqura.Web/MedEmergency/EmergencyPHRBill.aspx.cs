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
//using Mediqura.BOL.MedEmergencyBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.AdmissionData;
using Mediqura.BOL.AdmissionBO;
using Mediqura.BOL.MedEmergencyBO;
namespace Mediqura.Web.MedEmergency
{
    public partial class EmergencyPHRBill : BasePage
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
            //Commonfunction.PopulateDdl(ddl_servicetype, mstlookup.GetLookupsList(LookupName.EmrgService));
            //Commonfunction.PopulateDdl(ddl_servicetypes, mstlookup.GetLookupsList(LookupName.EmerService));
            Commonfunction.PopulateDdl(ddldoctor, mstlookup.GetLookupsList(LookupName.EmergencyDoc));
            Commonfunction.PopulateDdl(ddlpaymentmode, mstlookup.GetLookupsList(LookupName.PaymentMode));
            Commonfunction.PopulateDdl(ddlpaymentmodes, mstlookup.GetLookupsList(LookupName.PaymentMode));
            //Commonfunction.PopulateDdl(ddlcollectedby, mstlookup.GetLookupsList(LookupName.CollectedBy));
            Commonfunction.PopulateDdl(ddlcollectedby, mstlookup.GetLookupsList(LookupName.PHRusers));
            //ddlpaymentmode.SelectedIndex = 1;
            //txt_totalbillamount.Text = "0.0";
            txt_totalbill.Text = "0.0";
            btnsave.Attributes["disabled"] = "disabled";
            //ddldiscountby.Attributes["disabled"] = "disabled";
            //btnprints.Attributes["disabled"] = "disabled";
            //btnprint.Attributes["disabled"] = "disabled";
            billno.Text = "";
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetEmrgNo(string prefixText, int count, string contextKey)
        {
            EmrgAdmissionData Objpaic = new EmrgAdmissionData();
            EmrgAdmissionBO objInfoBO = new EmrgAdmissionBO();
            List<EmrgAdmissionData> getResult = new List<EmrgAdmissionData>();
            Objpaic.EmrgNo = prefixText;
            getResult = objInfoBO.GetEmrgNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmrgNo.ToString());
            }
            return list;
        }
        //TAB 2 //
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetEmgPatientName(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetEmgPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmgPatientName.ToString());
            }
            return list;
        }
        protected void txt_emrgno_TextChanged(object sender, EventArgs e)
        {
            EmrgAdmissionData Objpaic = new EmrgAdmissionData();
            EmrgAdmissionBO objInfoBO = new EmrgAdmissionBO();
            List<EmrgAdmissionData> getResult = new List<EmrgAdmissionData>();
            Objpaic.EmrgNo = txt_emrgno.Text.Trim() == "" ? "" : txt_emrgno.Text.Trim();
            getResult = objInfoBO.GetPatientsDetailsByEmrgNo(Objpaic);
            if (getResult.Count > 0)
            {
                txtname.Text = getResult[0].PatientName.ToString();
                txt_gender.Text = getResult[0].GenderName.ToString();
                txt_age.Text = getResult[0].Agecount.ToString();
                txt_address.Text = getResult[0].Address.ToString();
                ddldoctor.SelectedValue = getResult[0].DocID.ToString();
                lbl_UHIDTemp.Text = getResult[0].UHID.ToString();
                ddldoctor.Attributes["disabled"] = "disabled";
            }
            else
            {
                txtname.Text = "";
                txt_emrgno.Text = "";
                txt_gender.Text = "";
                txt_age.Text = "";
                ddldoctor.SelectedIndex = 0;
            }
            bindgrid();
        }
        protected void bindgrid()
        {
            try
            {

                List<EmrgAdmissionData> objdeposit = GetEMRGPhrDrugList(0);
                if (objdeposit.Count > 0)
                {
                    txt_totalbill.Text = Commonfunction.Getrounding(objdeposit[0].TotalBillAmount.ToString());
                    gvEMRGitemlist.DataSource = objdeposit;
                    gvEMRGitemlist.DataBind();
                    gvEMRGitemlist.Visible = true;
                    //Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    div1.Attributes["class"] = "SucessAlert";
                    btnsave.Attributes.Remove("disabled");

                }
                else
                {
                    gvEMRGitemlist.DataSource = null;
                    gvEMRGitemlist.DataBind();
                    gvEMRGitemlist.Visible = true;

                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;
            }
        }
        public List<EmrgAdmissionData> GetEMRGPhrDrugList(int curIndex)
        {
            EmrgAdmissionData objpat = new EmrgAdmissionData();
            EmrgAdmissionBO objBO = new EmrgAdmissionBO();
            objpat.EmrgNo = txt_emrgno.Text.Trim() == "" ? null : txt_emrgno.Text.Trim();
            return objBO.GetEMRGPhrDrugList(objpat);
        }
        protected void gvEMRGitemlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void gvEMRGitemlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {

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
                if (ddlpaymentmode.SelectedIndex == 0)
                {

                    Messagealert_.ShowMessage(lblmessage, "Doctor", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddlpaymentmode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                EmrgAdmissionData objbill = new EmrgAdmissionData();
                EmrgAdmissionBO objstdBO = new EmrgAdmissionBO();
                List<DiscoutData> Listbill = new List<DiscoutData>();
                objbill.EmrgNo = txt_emrgno.Text.Trim();
                objbill.UHID = Convert.ToInt64(lbl_UHIDTemp.Text == "" ? "0" : lbl_UHIDTemp.Text);
                objbill.TotalBillAmount = Convert.ToDecimal(txt_totalbill.Text == "" ? "0.0" : txt_totalbill.Text);
                objbill.PaymentMode = Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue);
                objbill.BankName = txt_bank.Text == "" ? null : txt_bank.Text;
                objbill.Chequenumber = txt_account.Text == "" ? null : txt_account.Text;
                objbill.FinancialYearID = LogData.FinancialYearID;
                objbill.EmployeeID = LogData.EmployeeID;
                objbill.AddedBy = LogData.AddedBy;
                objbill.HospitalID = LogData.HospitalID;
                objbill.IsActive = LogData.IsActive;
                objbill.IPaddress = LogData.IPaddress;
                int result = objstdBO.Update_EMRGPhr_BillDetails(objbill);
                if (result > 0)
                {
                    billno.Text = result.ToString();
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    Session["DiscountList"] = null;
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    btnsave.Attributes["disabled"] = "disabled";
                    //btnprintsum.Attributes.Remove("disabled");
                    //btnprint.Attributes.Remove("disabled");
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                //Messagealert_.ShowMessage(lblresult, "system", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;
                return;
            }
       }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            gvEMRGitemlist.DataSource = null;
            gvEMRGitemlist.DataBind();
            gvEMRGitemlist.Visible = false;
            lblmessage.Visible = false;
            txt_totalbill.Text = "";
            txt_bank.Text = "";
            txt_account.Text = "";
            txt_emrgno.Text = "";
            txtname.Text = "";
            txt_gender.Text = "";
            txt_age.Text = "";
            txt_address.Text = "";
            ddldoctor.SelectedIndex = 1;
            btnsave.Attributes["disabled"] = "disabled";
            //btnprint.Attributes["disabled"] = "disabled";
            billno.Text = "";
        }
        protected void ddlpaymentmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlpaymentmode.SelectedIndex > 1)
            {
                txt_bank.ReadOnly = false;
                txt_account.ReadOnly = false;
            }
            else
            {
                txt_bank.ReadOnly = true;
                txt_account.ReadOnly = true;
            }
        }
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            if (txtpatientNames.Text != "")
            {
                var source = txtpatientNames.Text.ToString();
                if (source.Contains(":"))
                {
                    bindgridList();
                }
                else
                {
                    txtpatientNames.Text = "";
                    txtpatientNames.Focus();
                    return;
                }
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgridList();
        }
        protected void bindgridList()
        {
            try
            {
                if (LogData.SearchEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
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
                        Messagealert_.ShowMessage(lblmessage2, "VaildDatefrom", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txtdatefrom.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                if (txtto.Text != "")
                {
                    if (Commonfunction.isValidDate(txtto.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "VaildDateto", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txtto.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                List<EmrgAdmissionData> objdeposit = GetEMRGPhrbillList(0);
                if (objdeposit.Count > 0)
                {
                    GvEmrgfinalbill.DataSource = objdeposit;
                    GvEmrgfinalbill.DataBind();
                    GvEmrgfinalbill.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    txttotalbill.Text = Commonfunction.Getrounding(objdeposit[0].TotalBill.ToString());
                    //txtajusted.Text = Commonfunction.Getrounding(objdeposit[0].TotalAdjustedAmount.ToString());
                    //txttotaldiscounted.Text = Commonfunction.Getrounding(objdeposit[0].TotalDiscountAmount.ToString());
                    txttotalpaid.Text = Commonfunction.Getrounding(objdeposit[0].TotalBill.ToString());
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    divmsg2.Visible = false;
                    if (LogData.PrintEnable == 0)
                    {
                        btnprints.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btnprints.Attributes.Remove("disabled");
                    }
                }
                else
                {
                    GvEmrgfinalbill.DataSource = null;
                    GvEmrgfinalbill.DataBind();
                    GvEmrgfinalbill.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    txttotalbill.Text = "0.00";
                    txtajusted.Text = "0.00";
                    txttotaldiscounted.Text = "0.00";
                    txttotalpaid.Text = "0.00";
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
            }
        }
        public List<EmrgAdmissionData> GetEMRGPhrbillList(int curIndex)
        {
            EmrgAdmissionData objbill = new EmrgAdmissionData();
            EmrgAdmissionBO objbillingBO = new EmrgAdmissionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            //objbill.EmrgNo = txt_emrgNoList.Text.Trim() == "" ? null : txt_emrgNoList.Text.Trim();
            objbill.PatientName = null;// txtpatientNames.Text == "" ? null : txtpatientNames.Text.Trim();

            string EmgNo;
            var source = txtpatientNames.Text.ToString();
            if (source.Contains(":"))
            {
                EmgNo = source.Substring(source.LastIndexOf(':') + 1);
                objbill.EmrgNo = EmgNo.Trim();
            }
            else
            {
                objbill.EmrgNo = null;
            }
            objbill.BillNo = txt_billnos.Text == "" ? "0" : txt_billnos.Text.Trim();
            objbill.Paymode = Convert.ToInt32(ddlpaymentmodes.SelectedValue == "" ? "0" : ddlpaymentmodes.SelectedValue);
            objbill.CollectedByID = Convert.ToInt64(ddlcollectedby.SelectedValue == "" ? "0" : ddlcollectedby.SelectedValue);
            string datefrom = from.ToString("yyyy-MM-dd");
            string timefrom = txttimepickerfrom.Text.Trim();
            from = Convert.ToDateTime(datefrom + " " + timefrom);
            objbill.DateFrom = from;
            string dateto = To.ToString("yyyy-MM-dd");
            string timeto = txttimepickerto.Text.Trim();
            To = Convert.ToDateTime(dateto + " " + timeto);
            objbill.DateTo = To;
            objbill.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objbillingBO.GetEMRGPhrbillList(objbill);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txt_emrgNoList.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            ddlstatus.SelectedIndex = 0;
            GvEmrgfinalbill.DataSource = null;
            GvEmrgfinalbill.DataBind();
            GvEmrgfinalbill.Visible = false;
            lblresult.Visible = false;
            txtpatientNames.Text = "";
            ddlpaymentmode.SelectedIndex = 0;
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage2.Visible = false;
            lblresult.Visible = false;
            divmsg2.Visible = false;
            divmsg3.Visible = false;
            txttotalbill.Text = "0.00";
            txtajusted.Text = "0.00";
            txttotaldiscounted.Text = "0.00";
            txttotalpaid.Text = "0.00";
            ddlcollectedby.SelectedIndex = 0;
            btnprints.Attributes["disabled"] = "disabled";
        }
        protected void GvEmrgfinalbill_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void GvEmrgfinalbill_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void GvEmrgfinalbill_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected DataTable GetDatafromDatabase()
        {
            List<EmrgAdmissionData> ServiceDetails = GetEMRGPhrbillList(0);
            List<EMRGServiceListDataTOeXCEL> ListexcelData = new List<EMRGServiceListDataTOeXCEL>();
            int i = 0;
            foreach (EmrgAdmissionData row in ServiceDetails)
            {
                EMRGServiceListDataTOeXCEL Ecxeclpat = new EMRGServiceListDataTOeXCEL();
                Ecxeclpat.EmrgNo = ServiceDetails[i].EmrgNo;
                Ecxeclpat.UHID = ServiceDetails[i].UHID;
                Ecxeclpat.PatientName = ServiceDetails[i].PatientName;
                Ecxeclpat.TotalBillAmount = ServiceDetails[i].TotalBillAmount;
                //Ecxeclpat.TotalDiscount = DepositDetails[i].TotalDiscount;
                //Ecxeclpat.AdjustedAmount = DepositDetails[i].AdjustedAmount;
                Ecxeclpat.PaidAmount = ServiceDetails[i].PaidAmount;
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
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
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
                    GvEmrgfinalbill.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvEmrgfinalbill.Columns[12].Visible = false;
                    GvEmrgfinalbill.Columns[13].Visible = false;
                    GvEmrgfinalbill.RenderControl(hw);
                    GvEmrgfinalbill.HeaderRow.Style.Add("width", "15%");
                    GvEmrgfinalbill.HeaderRow.Style.Add("font-size", "10px");
                    GvEmrgfinalbill.Style.Add("text-decoration", "none");
                    GvEmrgfinalbill.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvEmrgfinalbill.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=EmergencyPharmacyBillDetails.pdf");
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
                wb.Worksheets.Add(dt, "IP service record");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=EmergencyPharmacyBillDetails.xlsx");
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