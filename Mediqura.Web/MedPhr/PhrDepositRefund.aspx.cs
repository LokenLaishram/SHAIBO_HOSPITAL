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
using Mediqura.BOL.MedBillBO;
using Mediqura.BOL.CommonBO;

namespace Mediqura.Web.MedPhr
{
    public partial class PhrDepositRefund : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                if (Session["EmgRefund"] != null)
                {
                    txtUHID.Text = Session["EmgRefund"].ToString();
                    getpatientdetails();
                    txtUHID.ReadOnly = true;
                    Session["EmgRefund"] = null;
                }

            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlpaymentmode, mstlookup.GetLookupsList(LookupName.PaymentMode));
            ddlpaymentmode.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddlpaymentmodes, mstlookup.GetLookupsList(LookupName.PaymentMode));
            txttoraldeposited.Text = "0.00";
            txttoraldeposited.Text = "0.00";
            txtrefundamount.Text = "";
            hdnverifystatus.Value = "0";
            txtUHID.ReadOnly = false;
            txtrefundamount.ReadOnly = false;
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
        protected void txtUHID_TextChanged(object sender, EventArgs e)
        {
            getpatientdetails();
        }
        private void getpatientdetails()
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            bool isnumeric = txtUHID.Text.All(char.IsDigit);
            if (isnumeric == false)
            {
                if (txtUHID.Text.Contains(":"))
                {
                    bool isUHIDnumeric = txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                    Objpaic.UHID = isUHIDnumeric ? Convert.ToInt64(txtUHID.Text.Contains(":") ? txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1) : "0") : 0;
                }
                else
                {
                    txtUHID.Text = "";
                    txtUHID.Focus();
                }
            }
            else
            {
                Objpaic.UHID = Convert.ToInt64(txtUHID.Text == "" ? "0" : txtUHID.Text);
            }
            getResult = objInfoBO.GetPhrPatientDetailsByUHID(Objpaic);
            if (getResult.Count > 0)
            {
                txtaddress.Text = getResult[0].Address.ToString();
                txttotaldeposied.Text = Commonfunction.Getrounding(getResult[0].DepositAmount.ToString());
                txttotalbalance.Text = Commonfunction.Getrounding(getResult[0].BalanceAmount.ToString());
                txtlastrefund.Text = Commonfunction.Getrounding(getResult[0].LastRefundedAmount.ToString());
                txtrefundamount.Focus();
                div1.Visible = false;
            }
            else
            {
                txtaddress.Text = "";
                txtUHID.Text = "";
                txttotaldeposied.Text = "";
                txttotalbalance.Text = "";
                txtlastrefund.Text = "";
                // txtrefundamount.Focus();
            }
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txtUHID.Text = "";
            txtaddress.Text = "";
            hdnverifystatus.Value = "0";
            txttotaldeposied.Text = "";
            txttotalbalance.Text = "";
            txtrefundamount.Text = "";
            txtrefundno.Text = "";
            txtremarks.Text = "";
            txtdue.Text = "";
            ddlpaymentmode.SelectedIndex = 1;
            txtbank.Text = "";
            txtcheque.Text = "";
            btnverify.Text = "Verify";
            div1.Visible = false;
            txtUHID.ReadOnly = false;
            txtrefundamount.ReadOnly = false;
            btnsave.Attributes.Remove("disabled");
            txtUHID.ReadOnly = false;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {

            if (txtUHID.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "UHID", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtUHID.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (Convert.ToDecimal(txtrefundamount.Text == "" ? "0" : txtrefundamount.Text) < 1)
            {
                Messagealert_.ShowMessage(lblmessage, "Refundamount", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtrefundamount.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (hdnverifystatus.Value == "0")
            {
                Messagealert_.ShowMessage(lblmessage, "Verifyrefund", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtrefundamount.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            List<RefundData> Listbill = new List<RefundData>();
            RefundData objrefund = new RefundData();
            RefundBO RefundBO = new RefundBO();
            // int index = 0;
            try
            {

                objrefund.FinancialYearID = LogData.FinancialYearID;
                objrefund.EmployeeID = LogData.EmployeeID;
                objrefund.HospitalID = LogData.HospitalID;
                Linear barcode = new Linear();
                barcode.Type = BarcodeType.CODE11;
                barcode.Data = txtUHID.Text == "" ? "0" : txtUHID.Text;
                byte[] barcodeInBytes = barcode.drawBarcodeAsBytes();
                objrefund.UHIDtoBarcode = barcodeInBytes;
                objrefund.RefundAmount = Convert.ToDecimal(txtrefundamount.Text == "" ? "0" : txtrefundamount.Text);
                objrefund.LastRefundedAmount = Convert.ToDecimal(txtlastrefund.Text == "" ? "0" : txtlastrefund.Text);
                objrefund.DueAmount = Convert.ToDecimal(txtdue.Text == "" ? "0" : txtdue.Text);
                bool isnumeric = txtUHID.Text.All(char.IsDigit);
                if (isnumeric == false)
                {
                    if (txtUHID.Text.Contains(":"))
                    {
                        bool isUHIDnumeric = txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                        objrefund.UHID = isUHIDnumeric ? Convert.ToInt64(txtUHID.Text.Contains(":") ? txtUHID.Text.Substring(txtUHID.Text.LastIndexOf(':') + 1) : "0") : 0;
                    }
                    else
                    {
                        txtUHID.Text = "";
                        txtUHID.Focus();
                    }
                }
                else
                {
                    objrefund.UHID = Convert.ToInt64(txtUHID.Text == "" ? "0" : txtUHID.Text);
                }
                objrefund.Paymode = Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue);
                objrefund.BankName = txtbank.Text.Trim();
                objrefund.Cheque = txtcheque.Text.Trim();

                Listbill = RefundBO.UpdatePhrRefundDetails(objrefund);
                if (Listbill.Count > 0)
                {
                    txtrefundno.Text = Listbill[0].RefundNo.ToString();
                    Messagealert_.ShowMessage(lblmessage, "Refund", 1);
                    div1.Visible = true;
                    btnsave.Attributes["disabled"] = "disabled";
                    div1.Attributes["class"] = "SucessAlert";
                    txtUHID.Text = "";
                }
                else
                {
                    txtrefundno.Text = "";
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    div1.Visible = true;
                    btnsave.Attributes.Remove("disabled");
                    div1.Attributes["class"] = "FailAlert";
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetAutoUHID(string prefixText, int count, string contextKey)
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
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            //if (txtdatefrom.Text == "")
            //{
            //    Messagealert_.ShowMessage(lblmessage2, "DateRange", 0);
            //    divmsg2.Attributes["class"] = "FailAlert";
            //    divmsg2.Visible = true;
            //    return;
            //}
            if (txtdatefrom.Text.Trim() != "")
            {
                if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
                {
                    Messagealert_.ShowMessage(lblmessage2, "ValidDatefrom", 0);
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
            if (txtto.Text.Trim() != "")
            {
                if (Commonfunction.isValidDate(txtto.Text) == false)
                {
                    Messagealert_.ShowMessage(lblmessage2, "ValidDateto", 0);
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
            bindgrid();
        }
        protected void bindgrid()
        {
            try
            {
                List<RefundData> objdeposit = GetPhrRefundList(0);
                if (objdeposit.Count > 0)
                {
                    gvrefundlist.DataSource = objdeposit;
                    gvrefundlist.DataBind();
                    gvrefundlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    txttoraldeposited.Text = Commonfunction.Getrounding(objdeposit[0].TotalDepositedAmount.ToString());
                    txttotalrefundamount.Text = Commonfunction.Getrounding(objdeposit[0].TotalRefundAmount.ToString());
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblmessage2.Visible = false;
                    divmsg2.Visible = false;
                }
                else
                {
                    gvrefundlist.DataSource = null;
                    gvrefundlist.DataBind();
                    gvrefundlist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    txttoraldeposited.Text = "0.00";
                    txttotalrefundamount.Text = "0.00";
                    lblresult.Visible = false;
                    divmsg3.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<RefundData> GetPhrRefundList(int curIndex)
        {
            RefundData objbill = new RefundData();
            RefundBO RefundBO = new RefundBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            bool isnumeric = txtpatientNames.Text.All(char.IsDigit);
            if (isnumeric == false)
            {
                if (txtpatientNames.Text.Contains(":"))
                {
                    bool isUHIDnumeric = txtpatientNames.Text.Substring(txtpatientNames.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                    objbill.UHID = isUHIDnumeric ? Convert.ToInt64(txtpatientNames.Text.Contains(":") ? txtpatientNames.Text.Substring(txtpatientNames.Text.LastIndexOf(':') + 1) : "0") : 0;
                }
                else
                {
                    txtpatientNames.Text = "";
                    txtpatientNames.Focus();
                }
            }
            else
            {
                objbill.UHID = Convert.ToInt64(txtpatientNames.Text == "" ? "0" : txtpatientNames.Text);
            }
            objbill.Paymode = Convert.ToInt32(ddlpaymentmodes.SelectedValue == "" ? "0" : ddlpaymentmodes.SelectedValue);
            objbill.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objbill.DateFrom = from;
            objbill.DateTo = To;
            return RefundBO.GetPhrRefundList(objbill);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtdatefrom.Text = "";
            txtto.Text = "";
            ddlstatus.SelectedIndex = 0;
            gvrefundlist.DataSource = null;
            gvrefundlist.DataBind();
            gvrefundlist.Visible = false;
            lblresult.Visible = false;
            txtpatientNames.Text = "";
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage2.Visible = false;
            lblresult.Visible = false;
            divmsg2.Visible = false;
            divmsg3.Visible = false;
            txttoraldeposited.Text = "0.00";
            txttotalrefundamount.Text = "0.00";
        }
        protected void gvdepositlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    RefundData objbill = new RefundData();
                    RefundBO RefundBO = new RefundBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvrefundlist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    Label UHID = (Label)gr.Cells[0].FindControl("lbluhid");
                    Label Amount = (Label)gr.Cells[0].FindControl("lblrefundamount");
                    Label lastrefund = (Label)gr.Cells[0].FindControl("lbllasterefund");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage2, "Remarks", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objbill.Remarks = txtremarks.Text;
                    }
                    objbill.RefundNo = ID.Text.Trim();
                    objbill.UHID = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                    objbill.EmployeeID = LogData.UserLoginId;
                    objbill.RefundAmount = Convert.ToDecimal(Amount.Text == "" ? "0" : Amount.Text);
                    objbill.LastRefundedAmount = Convert.ToDecimal(lastrefund.Text == "" ? "0" : lastrefund.Text);
                    int Result = RefundBO.DeletePHRRefundByID(objbill);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        divmsg2.Attributes["class"] = "SucessAlert";
                        divmsg2.Visible = true;
                        bindgrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage2, "system", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                    }
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
        protected DataTable GetDatafromDatabase()
        {
            List<RefundData> DepositDetails = GetPhrRefundList(0);
            List<RefundDatatoExcel> ListexcelData = new List<RefundDatatoExcel>();
            int i = 0;
            foreach (RefundData row in DepositDetails)
            {
                RefundDatatoExcel Ecxeclpat = new RefundDatatoExcel();
                Ecxeclpat.UHID = DepositDetails[i].UHID;
                Ecxeclpat.PatientName = DepositDetails[i].PatientName;
                Ecxeclpat.Address = DepositDetails[i].Address;
                Ecxeclpat.RefundAmount = DepositDetails[i].RefundAmount;
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
                    gvrefundlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvrefundlist.Columns[8].Visible = false;
                    gvrefundlist.Columns[9].Visible = false;
                    gvrefundlist.Columns[10].Visible = false;

                    gvrefundlist.RenderControl(hw);
                    gvrefundlist.HeaderRow.Style.Add("width", "15%");
                    gvrefundlist.HeaderRow.Style.Add("font-size", "10px");
                    gvrefundlist.Style.Add("text-decoration", "none");
                    gvrefundlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvrefundlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Refund.pdf");
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
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Refund Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=RefundDetails.xlsx");
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
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            if (txtpatientNames.Text.Trim() != "")
            {
                bindgrid();
            }
        }
        protected void ddlpaymentmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlpaymentmode.SelectedIndex > 1)
            {
                txtbank.ReadOnly = false;
                txtcheque.ReadOnly = false;
            }
            else
            {
                txtbank.ReadOnly = true;
                txtcheque.ReadOnly = true;
            }
        }
        protected void txtrefundamount_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtrefundamount.Text == "" ? "0" : txtrefundamount.Text) > 0 && Convert.ToDecimal(txtrefundamount.Text == "" ? "0" : txtrefundamount.Text) <= Convert.ToDecimal(txttotalbalance.Text == "" ? "0" : txttotalbalance.Text))
            {
                txtdue.Text = Commonfunction.Getrounding((Convert.ToDecimal(txttotalbalance.Text) - Convert.ToDecimal(txtrefundamount.Text)).ToString());
                div1.Visible = false;
                hdnverifystatus.Value = "0";
                btnverify.Text = "Verify";
            }
            else
            {
                txtdue.Text = "0.00";
                Messagealert_.ShowMessage(lblmessage, "Refund amount is greater than deposited amount.", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtrefundamount.Focus();
                return;
            }
        }
        protected void gvrefundlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvrefundlist.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        protected void btnverify_Click(object sender, EventArgs e)
        {
            if (txtUHID.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Please enter UHID.", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtUHID.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (Convert.ToDecimal(txtrefundamount.Text == "" ? "0" : txtrefundamount.Text) < 1)
            {
                Messagealert_.ShowMessage(lblmessage, "Please enter Refund Amount.", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtrefundamount.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (Convert.ToDecimal(txtrefundamount.Text == "" ? "0" : txtrefundamount.Text) > 0 && Convert.ToDecimal(txtrefundamount.Text == "" ? "0" : txtrefundamount.Text) <= Convert.ToDecimal(txttotalbalance.Text == "" ? "0" : txttotalbalance.Text))
            {
                txtdue.Text = Commonfunction.Getrounding((Convert.ToDecimal(txttotalbalance.Text) - Convert.ToDecimal(txtrefundamount.Text)).ToString());
                div1.Visible = false;
                hdnverifystatus.Value = "1";
                btnverify.Text = "Verified";
                txtUHID.ReadOnly = true;
                txtrefundamount.ReadOnly = true;
            }
            else
            {
                txtUHID.ReadOnly = false;
                txtrefundamount.ReadOnly = false;
                txtdue.Text = "0.00";
                hdnverifystatus.Value = "0";
                btnverify.Text = "Verify";
                Messagealert_.ShowMessage(lblmessage, "Refund amount is greater than deposited amount.", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtrefundamount.Focus();
                return;
            }
        }
    }
}