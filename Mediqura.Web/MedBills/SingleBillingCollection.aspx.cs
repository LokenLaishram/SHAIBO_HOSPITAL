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

namespace Mediqura.Web.MedBills
{
    public partial class SingleBillingCollection : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                if (Session["PAT_UHID"] != null)
                {
                    Session["PAT_UHID"] = null;
                }
                ViewState["BillID"] = null;
                if (Session["BILLID"] != null)
                {
                    Int64 ID = Convert.ToInt32(Session["BILLID"].ToString());
                    Session["BILLID"] = null;
                    ViewState["BillID"] = ID;
                }
                Session["DiscountList"] = null;
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlpaymentmodes, mstlookup.GetLookupsList(LookupName.PaymentMode));
            Commonfunction.PopulateDdl(ddlpaymentmode, mstlookup.GetLookupsList(LookupName.PaymentMode));
            ddlpaymentmodes.SelectedIndex = 1;
            ddlpaymentmode.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddlcollectedby, mstlookup.GetLookupsList(LookupName.CollectedBy));
            Commonfunction.PopulateDdl(ddllabcollectedby, mstlookup.GetLookupsList(LookupName.CollectedBy));
            txttotalbill.Text = "0.00";
            txtajusted.Text = "0.00";
            txttotaldiscounted.Text = "0.00";
            txttotalpaid.Text = "0.00";
            Session["ServiceList"] = null;
            Session["DiscountList"] = null;
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtfrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtdateto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txttotalbillamt.Text = "0.00";
            txttotaladj.Text = "0.00";
            txttotladis.Text = "0.00";
            txttotalpd.Text = "0.00";
             
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetBillNo(string prefixText, int count, string contextKey)
        {
            OPDbillingData Objpaic = new OPDbillingData();
            OPDbillingBO objInfoBO = new OPDbillingBO();
            List<OPDbillingData> getResult = new List<OPDbillingData>();
            Objpaic.BillNo = prefixText;
            getResult = objInfoBO.GetautoOPbills(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].BillNo);
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
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid(1);
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
                    txtajusted.Text = Commonfunction.Getrounding(objdeposit[0].TotalAdjustedAmount.ToString());
                    txttotaldiscounted.Text = Commonfunction.Getrounding(objdeposit[0].TotalDiscountAmount.ToString());
                    txttotalpaid.Text = Commonfunction.Getrounding(objdeposit[0].TotalPaidAmount.ToString());
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    divmsg2.Visible = false;
                   
                }
                else
                {
                    gvopcollection.DataSource = null;
                    gvopcollection.DataBind();
                    gvopcollection.Visible = true;
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
        public List<OPDbillingData> GetOPBillList(int curIndex)
        {
            OPDbillingData objpat = new OPDbillingData();
            OPDbillingBO objbillingBO = new OPDbillingBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.BillNo = txtBillNo.Text == "" ? " " : txtBillNo.Text;
            objpat.PatientName = txtpatientNames.Text == "" ? null : txtpatientNames.Text.Trim();
            objpat.Paymode = Convert.ToInt32(ddlpaymentmodes.SelectedValue == "" ? "0" : ddlpaymentmodes.SelectedValue);
            objpat.CollectedByID = Convert.ToInt64(ddlcollectedby.SelectedValue == "" ? "0" : ddlcollectedby.SelectedValue);
            string datefrom = from.ToString("yyyy-MM-dd");
            string timefrom = txttimepickerfrom.Text.Trim();
            from = Convert.ToDateTime(datefrom + " " + timefrom);
            objpat.DateFrom = from;
            string dateto = To.ToString("yyyy-MM-dd");
            string timeto = txttimepickerto.Text.Trim();
            To = Convert.ToDateTime(dateto + " " + timeto);
            objpat.DateTo = To;
            objpat.AmountEnable = LogData.AmountEnable;
            return objbillingBO.GetOPPaymentList(objpat);
        }
        protected void txtBillNo_TextChanged(object sender, EventArgs e)
        {
            OPDbillingData Objpaic = new OPDbillingData();
            OPDbillingBO objInfoBO = new OPDbillingBO();
            List<OPDbillingData> getResult = new List<OPDbillingData>();
            Objpaic.BillNo = txtBillNo.Text.Trim() == "" ? "" : txtBillNo.Text.Trim();
            getResult = objInfoBO.GetPatientDetailbybillNo(Objpaic);
            if (getResult.Count > 0)
            {
                txtBillNo.Text = getResult[0].BillNo.ToString();
                txtpatientNames.Text = getResult[0].PatientName.ToString();
                bindgrid(1);
            }
            else
            {
                txtBillNo.Text = "";
                txtpatientNames.Text = "";
                txtBillNo.Focus();
            }

        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            bindgrid(0);
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtBillNo.Text = "";
            ddlpaymentmodes.SelectedIndex = 0;
            gvopcollection.DataSource = null;
            gvopcollection.DataBind();
            gvopcollection.Visible = false;
            lblresult.Visible = false;
            txtpatientNames.Text = "";
            ddlpaymentmodes.SelectedIndex = 0;
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
            if (LogData.RoleID == 1)
            {
                ddlcollectedby.SelectedIndex = 0;
            }
            else
            {
                ddlcollectedby.SelectedIndex = 1;
            }
        }
        protected void gvopcollection_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "statusupdate")
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "UpdateEnable", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage2.Visible = false;
                    }
                    OPDbillingData obj = new OPDbillingData();
                    OPDbillingBO objInfoBO = new OPDbillingBO();
                    int i = Convert.ToInt32(e.CommandArgument.ToString());
                    GridViewRow pt = gvopcollection.Rows[i];
                    Label BillNo = (Label)pt.Cells[0].FindControl("lblBillNo");
                    Label verify = (Label)pt.Cells[0].FindControl("lblverify");
                    Button btnverify = (Button)pt.Cells[0].FindControl("btnverify");

                    obj.BillNo = BillNo.Text;
                    obj.EmployeeID = LogData.EmployeeID;



                    int result = objInfoBO.UpdatePaymentVerification(obj);
                    if (result > 0)
                    {

                        Messagealert_.ShowMessage(lblresult, "update", 1);
                        divmsg3.Visible = true;
                        divmsg3.Attributes["class"] = "SucessAlert";
                        bindgrid(0);
                    }
                    else
                    {
                        gvopcollection.DataSource = null;
                        gvopcollection.DataBind();
                        divmsg3.Visible = false;

                    }
                }
                if (e.CommandName == "Print")
                {
                    //if (LogData.RoleID > 1)
                    //{
                    //    Messagealert_.ShowMessage(lblmessage2, "PrintEnable", 0);
                    //    divmsg2.Visible = true;
                    //    divmsg2.Attributes["class"] = "FailAlert";j 
                    //    return;
                    //}
                    //else
                    //{
                    //    lblmessage2.Visible = false;
                    //}
                    int j = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gv = gvopcollection.Rows[j];
                    Label regd = (Label)gv.Cells[0].FindControl("lbluhid");
                    string url = "../Reports/ReportViewer.aspx?option=RegdCard&UHID=" + regd.Text.Trim();
                    string fullURL = "window.open('" + url + "', '_blank');";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
            }
        }
        protected void gvopcollection_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label verify = (Label)e.Row.FindControl("lblverify");
                Button verifybutton = (Button)e.Row.FindControl("btnverify");

                if (verify.Text == "0")
                {
                    verifybutton.Text = "Verify";
                    verifybutton.Enabled = true;

                    e.Row.Cells[12].ColumnSpan = 4;
                    e.Row.Cells[13].Visible = false;
                    e.Row.Cells[14].Visible = false;
                    e.Row.Cells[15].Visible = false;
                   
                    

                }
                else if (verify.Text == "1")
                {
                    verifybutton.Text = "Verified";
                    verifybutton.Enabled = false;
                    e.Row.Cells[13].Visible = true;
                    e.Row.Cells[14].Visible = true;
                    e.Row.Cells[15].Visible = true;
                }

            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<OPDbillingData> DepositDetails = GetOPBillList(0);
            List<OPDbillingDataTOeXCEL> ListexcelData = new List<OPDbillingDataTOeXCEL>();
            int i = 0;
            foreach (OPDbillingData row in DepositDetails)
            {
                OPDbillingDataTOeXCEL Ecxeclpat = new OPDbillingDataTOeXCEL();
                Ecxeclpat.UHID = DepositDetails[i].UHID;
                Ecxeclpat.PatientName = DepositDetails[i].PatientName;
                Ecxeclpat.Address = DepositDetails[i].Address;
                Ecxeclpat.DocName = DepositDetails[i].DocName;
                Ecxeclpat.BillAmount = DepositDetails[i].TotalBillAmount;
                Ecxeclpat.TotalAdjustedAmount = DepositDetails[i].TotalAdjustedAmount;
                Ecxeclpat.TotalDiscountedAmount = DepositDetails[i].TotalDiscountedAmount;
                Ecxeclpat.TotalPaidAmount = DepositDetails[i].TotalPaidAmount;
                Ecxeclpat.AddedBy = DepositDetails[i].EmpName;
                if (DepositDetails[i].VerifyID == 1)
                {
                    Ecxeclpat.Verify = "Verified";
                }
                else
                {
                    Ecxeclpat.Verify = "Not Verified";
                }
                Ecxeclpat.VerifyBy = DepositDetails[i].VerifyByName;
             
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
                ExportoExcel1();
            }
            else if (ddlexport.SelectedIndex == 2)
            {
                ExportToPdf1();
            }
            else
            {
                Messagealert_.ShowMessage(lblresult, "ExportType", 0);
                divmsg3.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        public void ExportToPdf1()
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
        protected void ExportoExcel1()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "OPDCollectionDetails Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=OPDCollectionDetails.xlsx");
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetLabBillNo(string prefixText, int count, string contextKey)
        {
            LabBillingData Objpaic = new LabBillingData();
            OPDbillingBO objInfoBO = new OPDbillingBO();
            List<LabBillingData> getResult = new List<LabBillingData>();
            Objpaic.BillNo = prefixText;

            getResult = objInfoBO.GetBillNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].BillNo.ToString());
            }
            return list;
        }
        protected void txtlabBillNo_TextChanged(object sender, EventArgs e)
        {
            LabBillingData Objpaic = new LabBillingData();
            OPDbillingBO objInfoBO = new OPDbillingBO();
            List<LabBillingData> getResult = new List<LabBillingData>();
            Objpaic.BillNo = txtlabBillNo.Text.Trim() == "" ? "" : txtlabBillNo.Text.Trim();
            getResult = objInfoBO.GetPatientDetailbyLabbillNo(Objpaic);
            if (getResult.Count > 0)
            {
                txtlabBillNo.Text = getResult[0].BillNo.ToString();
                txtpatientName.Text = getResult[0].PatientName.ToString();
                bindgrid1(0);
            }
            else
            {
                txtBillNo.Text = "";
                txtpatientNames.Text = "";
                txtBillNo.Focus();
            }

        }
        protected void btnsearch1_Click(object sender, EventArgs e)
        {
            bindgrid1(0);
        }
        protected void bindgrid1(int page)
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
                txttotalbillamt.Text = "0.00";
                txttotaladj.Text = "0.00";
                txttotladis.Text = "0.00";
                txttotalpd.Text = "0.00";
                
                
                List<LabBillingData> objdeposit = GetLabBillList(page);
                if (objdeposit.Count > 0)
                {

                    gvlabcollectionlist.DataSource = objdeposit;
                    gvlabcollectionlist.DataBind();
                    gvlabcollectionlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult1, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    div3.Attributes["class"] = "SucessAlert";
                    div3.Visible = true;
                    txttotalbillamt.Text = Commonfunction.Getrounding(objdeposit[0].TotalBill.ToString());
                    txttotaladj.Text = Commonfunction.Getrounding(objdeposit[0].TotalAdjustedAmount.ToString());
                    txttotladis.Text = Commonfunction.Getrounding(objdeposit[0].TotalDiscountedAmount.ToString());
                    txttotalpd.Text = Commonfunction.Getrounding(objdeposit[0].TotalPaidAmount.ToString());
                    ddlexport1.Visible = true;
                    btnexport1.Visible = true;
                }
                else
                {
                    gvlabcollectionlist.DataSource = null;
                    gvlabcollectionlist.DataBind();
                    gvlabcollectionlist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    txttotalbillamt.Text = "0.00";
                    txttotaladj.Text = "0.00";
                    txttotladis.Text = "0.00";
                    txttotalpd.Text = "0.00";
             
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        public List<LabBillingData> GetLabBillList(int curIndex)
        {
            LabBillingData objlabbill = new LabBillingData();
            OPDbillingBO objbillingBO = new OPDbillingBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtfrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtfrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtdateto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objlabbill.BillNo = txtlabBillNo.Text.Trim() == "" ? "" : txtlabBillNo.Text.Trim();
            objlabbill.PatientName = txtpatientName.Text.Trim() == "" ? null : txtpatientName.Text.Trim();
            objlabbill.Paymode = Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue);
            objlabbill.CollectedByID = Convert.ToInt64(ddlcollectedby.SelectedValue == "" ? "0" : ddllabcollectedby.SelectedValue);
            string datefrom = from.ToString("yyyy-MM-dd");
            string timefrom = txttimepickerfrom.Text.Trim();
            from = Convert.ToDateTime(datefrom + " " + timefrom);
            objlabbill.DateFrom = from;
            string dateto = To.ToString("yyyy-MM-dd");
            string timeto = txttimepickerto.Text.Trim();
            To = Convert.ToDateTime(dateto + " " + timeto);
            objlabbill.DateTo = To;
            objlabbill.AmountEnable = LogData.AmountEnable;
            return objbillingBO.GetLabPaymentList(objlabbill);
        }
        protected void btnresets1_Click(object sender, EventArgs e)
        {
            txtlabBillNo.Text = "";
            txtfrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtdateto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            gvlabcollectionlist.DataSource = null;
            gvlabcollectionlist.DataBind();
            gvlabcollectionlist.Visible = false;
            lblresult1.Visible = false;
            txtpatientNames.Text = "";
            ddlpaymentmode.SelectedIndex = 0;
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage2.Visible = false;
            lblresult1.Visible = false;
            divmsg2.Visible = false;
            //divmsg3.Visible = false;
            txttotalbillamt.Text = "0.00";
            txttotaladj.Text = "0.00";
            txttotladis.Text = "0.00";
            txttotalpd.Text = "0.00";
            ddlpaymentmode.SelectedIndex = 1;
        }
        protected void gvlabcollectionlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "statusupdate")
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "UpdateEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage1.Visible = false;
                    }
                    LabBillingData obj = new LabBillingData();
                    OPDbillingBO objInfoBO = new OPDbillingBO();
                    int i = Convert.ToInt32(e.CommandArgument.ToString());
                    GridViewRow pt = gvlabcollectionlist.Rows[i];
                    Label BillNo = (Label)pt.Cells[0].FindControl("lblLabBillNo");
                    Label verify = (Label)pt.Cells[0].FindControl("lblverify");
                    Button btnverify = (Button)pt.Cells[0].FindControl("btnverify");

                    obj.BillNo = BillNo.Text;
                    obj.EmployeeID = LogData.EmployeeID;



                    int result = objInfoBO.UpdateLabPaymentVerification(obj);
                    if (result > 0)
                    {

                        Messagealert_.ShowMessage(lblresult1, "update", 1);
                        div3.Visible = true;
                        div3.Attributes["class"] = "SucessAlert";
                        bindgrid1(0);
                    }
                    else
                    {
                        gvopcollection.DataSource = null;
                        gvopcollection.DataBind();
                        divmsg3.Visible = false;

                    }
                }
                if (e.CommandName == "Print")
                {
                    //if (LogData.RoleID > 1)
                    //{
                    //    Messagealert_.ShowMessage(lblmessage1, "PrintEnable", 0);
                    //    div1.Visible = true;
                    //    div1.Attributes["class"] = "FailAlert";
                    //    return;
                    //}
                    //else
                    //{
                    //    lblmessage1.Visible = false;
                    //}
                    int j = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gv = gvopcollection.Rows[j];
                    Label regd = (Label)gv.Cells[0].FindControl("lbluhid");
                    string url = "../Reports/ReportViewer.aspx?option=RegdCard&UHID=" + regd.Text.Trim();
                    string fullURL = "window.open('" + url + "', '_blank');";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
            }
        }
        protected void gvlabcollectionlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label verify = (Label)e.Row.FindControl("lblverify");
                Button verifybutton = (Button)e.Row.FindControl("btnverify");

                if (verify.Text == "0")
                {
                    verifybutton.Text = "Verify";
                    verifybutton.Enabled = true;
                    e.Row.Cells[12].ColumnSpan = 4;
                    e.Row.Cells[13].Visible = false;
                }
                else if (verify.Text == "1")
                {
                    verifybutton.Text = "Verified";
                    verifybutton.Enabled = false;
                    e.Row.Cells[13].Visible = true;
                }

            }
        }
        protected DataTable GetDatafromDatabase1()
        {
            List<LabBillingData> DepositDetails = GetLabBillList(0);
            List<OPDbillingDataTOeXCEL> ListexcelData = new List<OPDbillingDataTOeXCEL>();
            int i = 0;
            foreach (LabBillingData row in DepositDetails)
            {
                OPDbillingDataTOeXCEL Ecxeclpat = new OPDbillingDataTOeXCEL();
                Ecxeclpat.UHID = DepositDetails[i].UHID;
                Ecxeclpat.PatientName = DepositDetails[i].PatientName;
                Ecxeclpat.Address = DepositDetails[i].Address;
                Ecxeclpat.DocName = DepositDetails[i].DocName;
                Ecxeclpat.BillAmount = DepositDetails[i].TotalBillAmount;
                Ecxeclpat.TotalAdjustedAmount = DepositDetails[i].TotalAdjustedAmount;
                Ecxeclpat.TotalDiscountedAmount = DepositDetails[i].TotalDiscountedAmount;
                Ecxeclpat.TotalPaidAmount = DepositDetails[i].TotalPaidAmount;
                Ecxeclpat.AddedBy = DepositDetails[i].EmpName;
                if (DepositDetails[i].VerifyID == 1)
                {
                    Ecxeclpat.Verify = "Verified";
                }
                else
                {
                    Ecxeclpat.Verify = "Not Verified";
                }
                Ecxeclpat.VerifyBy = DepositDetails[i].VerifyByName;

                ListexcelData.Add(Ecxeclpat);
                i++;
            }
            ListtoDataTableConverter1 converter = new ListtoDataTableConverter1();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        public class ListtoDataTableConverter1
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
        protected void btnexport1_Click(object sender, EventArgs e)
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
            if (ddlexport1.SelectedIndex == 1)
            {
                ExportoExcel();
            }
            else if (ddlexport1.SelectedIndex == 2)
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
                    Response.AddHeader("content-disposition", "attachment;filename=LabCollectionDetails.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase1();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Lab CollectionDetails Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=LabCollectionDetails.xlsx");
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

   

    
    
