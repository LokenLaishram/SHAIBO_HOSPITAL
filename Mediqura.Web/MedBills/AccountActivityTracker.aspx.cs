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

namespace Mediqura.Web.MedBills
{
    public partial class AccountActivityTracker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
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

        protected void txtautoUHID_TextChanged(object sender, EventArgs e)
        {
            ActivityTrackerData Objpaic = new ActivityTrackerData();
            ActivityTrackerBO objInfoBO = new ActivityTrackerBO();
            List<ActivityTrackerData> getResult = new List<ActivityTrackerData>();
            Objpaic.UHID = Convert.ToInt64(txtautoUHID.Text.Trim() == "" ? "0" : txtautoUHID.Text.Trim());
            getResult = objInfoBO.GetPatientAdmissionDetailsByUHID(Objpaic);
            if (getResult.Count > 0)
            {
                txt_name.Text = getResult[0].PatientName.ToString();
                txt_address.Text = getResult[0].Address.ToString();
                txt_contactNo.Text = getResult[0].Number.ToString();
                //bindgrid();

              
            }
            else
            {
                txt_name.Text = "";
                txt_address.Text = "";
                txtautoUHID.Text = "";
                txt_contactNo.Text = "";
                txtautoUHID.Focus();
            }
        }
        protected void bindgrid()
        {
            decimal reftotal = 0;
            decimal adjstotal = 0;
            try
            {
                
               
                List<ActivityTrackerData> objPatientAcvt = GetPatientActivity(0);
                if (objPatientAcvt.Count > 0)
                {
                    if (ViewState["Totalref"] == null)
                    {
                        ////Decimal total = 0;
                        for (int i = 0; i <= objPatientAcvt.Count - 1; i++)
                        {
                            reftotal = reftotal + Convert.ToDecimal(objPatientAcvt[i].Refund.ToString());
                        }
                        ViewState["Totalref"] = reftotal;
                        //txttotalrefund.Text = Convert.ToDecimal(reftotal).ToString();
                        txttotalrefund.Text = Commonfunction.Getrounding(Convert.ToDecimal(reftotal).ToString());
                    }
                    if (ViewState["Totaladjs"] == null)
                    {
                        ////Decimal total = 0;
                        for (int i = 0; i <= objPatientAcvt.Count - 1; i++)
                        {
                            adjstotal = adjstotal + Convert.ToDecimal(objPatientAcvt[i].CancelAmt.ToString());
                        }
                        ViewState["Totaladjs"] = adjstotal;
                        //txttotalrefund.Text = Convert.ToDecimal(reftotal).ToString();
                        txttotaladjusted.Text = Commonfunction.Getrounding(Convert.ToDecimal(adjstotal).ToString());
                    }
                    GvPatientTracker.DataSource = objPatientAcvt;
                    GvPatientTracker.DataBind();
                    GvPatientTracker.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblmessage.Visible = false;
                    divmsg3.Visible = true;
                    //lblresult.Visible = true;
                     //Messagealert_.ShowMessage(lblresult, "Total: " + objPatientAcvt[0].MaximumRows.ToString() + " Record found", 1);
                    //if (t.Text == "")
                    //{
                    //    txt_servName.Text = objdeposit[0].ServiceName.ToString();
                     divsummary.Visible = true;
                     divsummarry2.Visible = true;
                     txttotaldeposied.Text = Commonfunction.Getrounding(objPatientAcvt[0].DepositAmount.ToString());
                     //txttotaladjusted.Text = Commonfunction.Getrounding(objPatientAcvt[0].AdustedAmount.ToString());
                     txttotalbalance.Text = Commonfunction.Getrounding(objPatientAcvt[0].BalanceAmount.ToString());
                     //txttotalrefund.Text = Commonfunction.Getrounding(objPatientAcvt[0].TotalRefund.ToString());

                }
                else
                {


                    GvPatientTracker.DataSource = null;
                    GvPatientTracker.DataBind();
                    GvPatientTracker.Visible = true;
                    GvPatientTracker.Visible = false;

                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }

        }

        public List<ActivityTrackerData> GetPatientActivity(int curIndex)
        {
            ActivityTrackerData objtracker = new ActivityTrackerData();
            ActivityTrackerBO objtrackerBO = new ActivityTrackerBO();
            objtracker.UHID = Convert.ToInt64(txtautoUHID.Text.Trim() == "" ? "0" : txtautoUHID.Text.Trim());
            return objtrackerBO.GetPatientActivity(objtracker);
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }

        protected void btnresets_Click(object sender, EventArgs e)
        {
            clearall();
           
        }

        protected void clearall()
        {
            txt_name.Text = "";
            txt_address.Text = "";
            txtautoUHID.Text = "";
            txt_contactNo.Text = "";
            GvPatientTracker.DataSource = null;
            GvPatientTracker.DataBind();
            GvPatientTracker.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            divmsg3.Visible = false;


            txttotaldeposied.Text = "";
            txttotaladjusted.Text = "";
            txttotalbalance.Text = "";
            txttotalrefund.Text = "";
            divsummary.Visible = false;
            divsummarry2.Visible = false;
            ViewState["Totalref"] = null;
            ViewState["Totaladjs"] = null;
        }

        protected void btnexport_Click(object sender, EventArgs e)
        {
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
                    GvPatientTracker.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    //gvOT_status.Columns[6].Visible = false;
                    //gvOT_status.Columns[8].Visible = false;
                    //gvstockstatus.Columns[10].Visible = false;
                    //gvstockstatus.Columns[11].Visible = false;

                    GvPatientTracker.RenderControl(hw);
                    GvPatientTracker.HeaderRow.Style.Add("width", "15%");
                    GvPatientTracker.HeaderRow.Style.Add("font-size", "10px");
                    GvPatientTracker.Style.Add("text-decoration", "none");
                    GvPatientTracker.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvPatientTracker.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=PatientActivityDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=PatientActivityDetails.xlsx");
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
            List<ActivityTrackerData> activity = GetPatientActivity(0);
            List<ActivityDataTOeXCEL> ListexcelData = new List<ActivityDataTOeXCEL>();
            int i = 0;
            foreach (ActivityTrackerData row in activity)
            {
                ActivityDataTOeXCEL Ecxeclpat = new ActivityDataTOeXCEL();
                Ecxeclpat.UHID = activity[i].UHID;
                Ecxeclpat.PatientName = activity[i].PatientName;
                Ecxeclpat.Deposit = activity[i].Deposit;
                Ecxeclpat.Adjusted = activity[i].Adjusted;
                Ecxeclpat.Refund = activity[i].Refund;
                Ecxeclpat.CancelAmt = activity[i].CancelAmt;
                Ecxeclpat.TbalanceAmt = activity[i].TbalanceAmt;
                Ecxeclpat.TrefAmt = activity[i].TrefAmt;
                Ecxeclpat.TadjustedAmt = activity[i].TadjustedAmt;
                Ecxeclpat.Date = activity[i].Date;
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

        protected void GvPatientTracker_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvPatientTracker.PageIndex = e.NewPageIndex;
            bindgrid();
        }
    }
}