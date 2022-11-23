using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedBillData;
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

namespace Mediqura.Web.MedReport
{
    public partial class OPServiceBillingReport : BasePage
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
            Commonfunction.PopulateDdl(ddlpaymentmodes, mstlookup.GetLookupsList(LookupName.PaymentMode));
            btnprints.Attributes["disabled"] = "disabled";
            ddlpaymentmodes.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddlpatientType, mstlookup.GetLookupsList(LookupName.PatientType));
            Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.OPDepartment));
            Commonfunction.PopulateDdl(ddlservicetype, mstlookup.GetLookupsList(LookupName.OPServiceType));
            Commonfunction.Insertzeroitemindex(ddldoctor);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetAutoUHID(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetDetailUHID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid(0);
        }
        protected void bindgrid(int page)
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
                List<OPDbillingData> objdeposit = GetOPBillList(page);
                if (objdeposit.Count > 0)
                {
                    gvopcollection.DataSource = objdeposit;
                    gvopcollection.DataBind();
                    gvopcollection.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    txttotalbill.Text = Commonfunction.Getrounding(objdeposit[0].TotalBill.ToString());
                    txttotaldiscounted.Text = Commonfunction.Getrounding(objdeposit[0].TotalDiscountAmount.ToString());
                    txttotalpaid.Text = Commonfunction.Getrounding(objdeposit[0].TotalPaidAmount.ToString());
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
                    gvopcollection.DataSource = null;
                    gvopcollection.DataBind();
                    gvopcollection.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    txttotalbill.Text = "0.00";
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
        protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldepartment.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddldoctor, mstlookup.GetDepartmentDoctor(Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue)));
            }
        }
        public List<OPDbillingData> GetOPBillList(int curIndex)
        {
            OPDbillingData objpat = new OPDbillingData();
            OPDbillingBO objbillingBO = new OPDbillingBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.Paymode = Convert.ToInt32(ddlpaymentmodes.SelectedValue == "" ? "0" : ddlpaymentmodes.SelectedValue);
            string datefrom = from.ToString("yyyy-MM-dd");
            string timefrom = txttimepickerfrom.Text.Trim();
            from = Convert.ToDateTime(datefrom + " " + timefrom);
            objpat.DateFrom = from;
            string dateto = To.ToString("yyyy-MM-dd");
            string timeto = txttimepickerto.Text.Trim();
            To = Convert.ToDateTime(dateto + " " + timeto);
            objpat.DateTo = To;
            objpat.AmountEnable = LogData.AmountEnable;
            if (txt_name.Text.Contains(":"))
            {
                bool isUHIDnumeric = txt_name.Text.Substring(txt_name.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                objpat.UHID = isUHIDnumeric ? Convert.ToInt64(txt_name.Text.Contains(":") ? txt_name.Text.Substring(txt_name.Text.LastIndexOf(':') + 1) : "0") : 0;
            }
            else
            {
                objpat.UHID = 0;
            }
            objpat.patientType = Convert.ToInt32(ddlpatientType.SelectedValue == "" ? "0" : ddlpatientType.SelectedValue);
            objpat.ServiceTypeID = Convert.ToInt32(ddlservicetype.SelectedValue == "" ? "0" : ddlservicetype.SelectedValue);
            if (txtservices.Text.Contains(":"))
            {
                bool isnumserv = txtservices.Text.Substring(txtservices.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                objpat.ServiceID = isnumserv ? Convert.ToInt32(txtservices.Text.Contains(":") ? txtservices.Text.Substring(txtservices.Text.LastIndexOf(':') + 1) : "0") : 0;
            }
            else
            {
                objpat.ServiceID = 0;
            }
            objpat.DeptID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
            objpat.DocID = Convert.ToInt64(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
            return objbillingBO.GetOPBillListReport(objpat);
        }
        protected void ddldoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldoctor.SelectedIndex > 0)
            {
                AutoCompleteExtender2.CompletionSetCount = Convert.ToInt32(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
            }
        }
        protected void ddlservicetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlservicetype.SelectedIndex > 0)
            {
                AutoCompleteExtender2.ContextKey = ddlservicetype.SelectedValue == "" ? "0" : ddlservicetype.SelectedValue;
            }
            else
            {
                AutoCompleteExtender2.ContextKey = null;
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetServices(string prefixText, int count, string contextKey)
        {
            ServicesData Objpaic = new ServicesData();
            ServiceBO objInfoBO = new ServiceBO();
            List<ServicesData> getResult = new List<ServicesData>();
            Objpaic.ServiceName = prefixText;
            Objpaic.ServiceTypeID = Convert.ToInt32(contextKey);
            Objpaic.DoctorID = count;
            getResult = objInfoBO.Getopservices(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ServiceName.ToString());
            }
            return list;
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ddlpaymentmodes.SelectedIndex = 1;
            txtdatefrom.Text = "";
            txtto.Text = "";
            gvopcollection.DataSource = null;
            gvopcollection.DataBind();
            gvopcollection.Visible = false;
            lblresult.Visible = false;
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage2.Visible = false;
            lblresult.Visible = false;
            divmsg2.Visible = false;
            divmsg3.Visible = false;
            txttotalbill.Text = "0.00";
            txttotaldiscounted.Text = "0.00";
            txttotalpaid.Text = "0.00";
            btnprints.Attributes["disabled"] = "disabled";
            txt_name.Text = "";
            ddldepartment.SelectedIndex = 0;
            ddlpatientType.SelectedIndex = 0;
            ddlservicetype.SelectedIndex = 0;
            Commonfunction.Insertzeroitemindex(ddldoctor);
        }
        protected DataTable GetDatafromDatabase()
        {
            List<OPDbillingData> DepositDetails = GetOPBillList(0);
            List<OPDbillingDataTOeXCEL> ListexcelData = new List<OPDbillingDataTOeXCEL>();
            int i = 0;
            foreach (OPDbillingData row in DepositDetails)
            {
                OPDbillingDataTOeXCEL Ecxeclpat = new OPDbillingDataTOeXCEL();
                Ecxeclpat.BillNo = DepositDetails[i].BillNo;
                Ecxeclpat.PatientName = DepositDetails[i].PatientName;
                Ecxeclpat.DocName = DepositDetails[i].DocName;
                Ecxeclpat.PatientName = DepositDetails[i].PatientName;
                Ecxeclpat.ServiceName = DepositDetails[i].ServiceName;
                Ecxeclpat.BillAmount = DepositDetails[i].NetServiceCharge;
                Ecxeclpat.Discount = DepositDetails[i].DisAmount;
                Ecxeclpat.PaidAmount = DepositDetails[i].PaidAmount;
                Ecxeclpat.TotalPaidAmount = DepositDetails[i].TotalPaidAmount;
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
                    gvopcollection.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox

                    gvopcollection.Columns[11].Visible = false;
                    gvopcollection.Columns[12].Visible = false;
                    gvopcollection.Columns[13].Visible = false;
                    gvopcollection.Columns[14].Visible = false;
                    gvopcollection.RenderControl(hw);
                    gvopcollection.HeaderRow.Style.Add("width", "15%");
                    gvopcollection.HeaderRow.Style.Add("font-size", "10px");
                    gvopcollection.Style.Add("text-decoration", "none");
                    gvopcollection.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvopcollection.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=OPDCollectionDetails.pdf");
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
                wb.Worksheets.Add(dt, "OPServiceBill Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=OPServiceBill.xlsx");
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
        protected void txt_name_TextChanged(object sender, EventArgs e)
        {
            bindgrid(0);
        }
        protected void gvopcollection_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Print")
                {
                    if (LogData.PrintEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblresult, "PrintEnable", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblresult.Visible = false;
                    }
                    int j = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gp = gvopcollection.Rows[j];
                    LinkButton UHID = (LinkButton)gp.Cells[0].FindControl("lbluhid");
                    string url = "../Reports/ReportViewer.aspx?option=PatientProfile&UHID=" + UHID.Text.ToString();
                    string fullURL = "window.open('" + url + "', '_blank');";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);

                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                divmsg2.Visible = true;
            }
        }
    }
}