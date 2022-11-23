using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedAccount;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedAccount;
using Mediqura.Utility;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Mediqura.Web.MedAccount
{
    public partial class ReceivedTransaction : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                initialize();
                btnPrint.Attributes["disabled"] = "disabled";
            }
        }
        private void initialize()
        {
            string IP = Commonfunction.GetClientIPAddress();
            string URL = "http://" + IP + ":9000";
            Boolean flag = Commonfunction.isValidURL(URL);
            if (flag)
            {
                btnSync.Visible = true;
                txt_tally.Text = "ONLINE";
            }
            else
            {
                btnSync.Visible = false;
                txt_tally.Text = "OFFLINE";
            }

        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_transaction, mstlookup.GetLookupsList(LookupName.PaymentType));
            Commonfunction.PopulateDdl(ddl_ledgers, mstlookup.GetLookupsList(LookupName.AccountLedger));
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }

            bindgrid();
        }
        protected void GVRecdTransaction_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                RecdTransactionData objfinalbill = new RecdTransactionData();
                RecdTransactionBO objstdBO = new RecdTransactionBO();
                GridView gv = (GridView)e.Row.FindControl("GvChild");
                Label isManual = (Label)e.Row.FindControl("lblIsmanual");
                LinkButton lbl_print = (LinkButton)e.Row.FindControl("lbl_print");
                if (isManual.Text == "0")
                {
                    lbl_print.Visible = false;
                }
                else
                {
                    lbl_print.Visible = true;
                }
                string VoucherNo = GVRecdTransaction.DataKeys[e.Row.RowIndex].Value.ToString();
                objfinalbill.VoucherNo = VoucherNo;
                List<RecdTransactionData> Result = objstdBO.Get_TransactionDetailsByVoucherNo(objfinalbill);
                if (Result.Count > 0)
                {
                    gv.DataSource = Result;
                    gv.DataBind();
                    gv.Visible = true;
                }
                else
                {
                    gv.DataSource = null;
                    gv.DataBind();
                    gv.Visible = true;
                }
            }
        }
        protected void bindgrid()
        {
            try
            {

                List<RecdTransactionData> objdeposit = GetTransactionList(0);
                if (objdeposit.Count > 0)
                {
                    GVRecdTransaction.DataSource = objdeposit;
                    GVRecdTransaction.DataBind();
                    GVRecdTransaction.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    txttotalamt.Text = Commonfunction.Getrounding(objdeposit[0].TotalAmount.ToString());
                    txttotalamtpaid.Text = Commonfunction.Getrounding(objdeposit[0].TotalAmountPaid.ToString());
                    txttotalamtcash.Text = Commonfunction.Getrounding(objdeposit[0].TotalCashPaid.ToString());
                    txttotalamtreceive.Text = Commonfunction.Getrounding(objdeposit[0].TotalCashRecieve.ToString());
                    txtContraRecieve.Text = Commonfunction.Getrounding(objdeposit[0].TotalCashContraRecieve.ToString());
                    txtContrapaid.Text = Commonfunction.Getrounding(objdeposit[0].TotalCashContraPaid.ToString());
                    txtTotalCashOutward.Text = Commonfunction.Getrounding((objdeposit[0].TotalCashContraPaid + objdeposit[0].TotalAmountPaid).ToString());
                    txtCashInHand.Text = Commonfunction.Getrounding(((objdeposit[0].TotalCashRecieve + objdeposit[0].TotalCashContraRecieve) - (objdeposit[0].TotalCashContraPaid + objdeposit[0].TotalAmountPaid)).ToString());
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    btnPrint.Attributes.Remove("disabled");

                }
                else
                {
                    btnPrint.Attributes["disabled"] = "disabled";
                    GVRecdTransaction.DataSource = null;
                    GVRecdTransaction.DataBind();
                    GVRecdTransaction.Visible = true;
                    lblresult.Visible = false;
                }
            }

            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<RecdTransactionData> GetTransactionList(int curIndex)
        {
            RecdTransactionData objpat = new RecdTransactionData();
            RecdTransactionBO objbillingBO = new RecdTransactionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.AccountID = Convert.ToInt32(ddl_ledgers.SelectedValue == "" ? "0" : ddl_ledgers.SelectedValue);
            objpat.TransactionTypeID = Convert.ToInt32(ddl_transaction.SelectedValue == "" ? "0" : ddl_transaction.SelectedValue);
            objpat.AccountState = Convert.ToInt32(ddl_account_close.SelectedValue == "" ? "0" : ddl_account_close.SelectedValue);

            string timefrom = txttimepickerfrom.Text.Trim();
            string timeto = txttimepickerto.Text.Trim();
            objpat.DateFrom = Convert.ToDateTime(from.ToString("yyyy-MM-dd") + " " + timefrom);
            objpat.DateTo = Convert.ToDateTime(To.ToString("yyyy-MM-dd") + " " + timeto);

            return objbillingBO.GetTransactionList(objpat);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            lblmessage.Visible = false;
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            ddl_transaction.SelectedIndex = 0;
            ddl_ledgers.SelectedIndex = 0;
            GVRecdTransaction.DataSource = null;
            GVRecdTransaction.DataBind();
            GVRecdTransaction.Visible = true;
            txttimepickerfrom.Text = "";
            txttimepickerto.Text = "";
            btnPrint.Attributes["disabled"] = "disabled";

        }
        protected void btnSync_Click(object sender, EventArgs e)
        {

            MDResponse.Show();
            lblResponse.Text = "Please Wait....";

            string IP = Commonfunction.GetClientIPAddress();
            string URL = "http://" + IP + ":9000";
            Boolean flag = Commonfunction.isValidURL(URL);

            if (flag)
            {

                string voucher = "";
                string xml = "";
                lblmessage.Visible = false;
                int count = 0;
                foreach (GridViewRow row in GVRecdTransaction.Rows)
                {
                    count = count + 1;
                    AccountVoucherData objVoucherData = new AccountVoucherData();
                    List<TransactionDetailData> ListTransactionDetails = new List<TransactionDetailData>();
                    Label lblTransactionType = (Label)GVRecdTransaction.Rows[row.RowIndex].Cells[0].FindControl("lblTransactionType");
                    Label lblVoucherNo = (Label)GVRecdTransaction.Rows[row.RowIndex].Cells[0].FindControl("lblVoucherNo");
                    Label lblParticular = (Label)GVRecdTransaction.Rows[row.RowIndex].Cells[0].FindControl("lblParticular");
                    Label lblRemarks = (Label)GVRecdTransaction.Rows[row.RowIndex].Cells[0].FindControl("lblRemarks");
                    Label lblVoucherDate = (Label)GVRecdTransaction.Rows[row.RowIndex].Cells[0].FindControl("lblVoucherDate");
                    Label lblTransTypeID = (Label)GVRecdTransaction.Rows[row.RowIndex].Cells[0].FindControl("lblTransTypeID");
                    Label lblAcount = (Label)GVRecdTransaction.Rows[row.RowIndex].Cells[0].FindControl("lblAcount");
                    CheckBox checkdata = (CheckBox)GVRecdTransaction.Rows[row.RowIndex].Cells[0].FindControl("checkdata");
                    if (checkdata.Checked)
                    {


                        GridView GvChild = (GridView)GVRecdTransaction.Rows[row.RowIndex].Cells[0].FindControl("GvChild");

                        foreach (GridViewRow Childrow in GvChild.Rows)
                        {

                            TransactionDetailData objData = new TransactionDetailData();
                            Label lblledgertype = (Label)GvChild.Rows[Childrow.RowIndex].Cells[0].FindControl("lblledgertype");
                            Label lblledgername = (Label)GvChild.Rows[Childrow.RowIndex].Cells[0].FindControl("lblledgername");
                            Label lblamt = (Label)GvChild.Rows[Childrow.RowIndex].Cells[0].FindControl("lblamt");

                            objData.LedgerType = lblledgertype.Text;
                            objData.Ledger = lblledgername.Text;
                            objData.amount = Convert.ToDecimal(lblamt.Text);
                            ListTransactionDetails.Add(objData);
                        }
                        objVoucherData.voucher = lblVoucherNo.Text;
                        objVoucherData.voucherType = lblTransactionType.Text;
                        objVoucherData.Narration = "Particular: " + lblParticular.Text + " " + lblRemarks.Text;
                        objVoucherData.TransactionDate = lblVoucherDate.Text;
                        objVoucherData.TransType = Convert.ToInt32(lblTransTypeID.Text);
                        objVoucherData.Partlyledger = lblAcount.Text;
                        objVoucherData.TransactionDetails = ListTransactionDetails;
                        voucher = voucher + XmlConvertor.GenerateVoucherXML(objVoucherData);

                    }
                    if ((count % 2) == 0)
                    {
                        xml = "";
                        xml = XmlConvertor.GenerateCompleteVoucherXML(voucher);
                        String response = SendReqst(xml);

                        String msg = "";
                        string errors = "";
                        try
                        {
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml(response);
                            XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/RESPONSE");
                            foreach (XmlNode node in nodeList)
                            {

                                errors = node.SelectSingleNode("ERRORS").InnerText;

                                if (errors != "0")
                                {
                                    msg = node.SelectSingleNode("LINEERROR").InnerText;
                                    msg = msg + " ERROR: " + errors;
                                }

                            }
                            if (Convert.ToInt32(errors) > 0)
                            {
                                lblResponse.Text = msg;
                                MDResponse.Show();
                                return;
                            }
                        }
                        catch
                        {
                            lblResponse.Text = msg;
                            MDResponse.Show();
                            return;
                        }
                        voucher = "";
                    }
                }
                xml = XmlConvertor.GenerateCompleteVoucherXML(voucher);
                String res = SendReqst(xml);

                lblResponse.Text = Commonfunction.TallyResponse(res);
                MDResponse.Show();
            }
            else
            {
                btnSync.Visible = false;
                txt_tally.Text = "OFFLINE";
                Messagealert_.ShowMessage(lblmessage, "Tally is offline ", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
        }

        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ExportEnable", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
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
            else
            {
                Messagealert_.ShowMessage(lblmessage, "ExportType", 0);
                div1.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "All Transaction Details");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=AllTransactionDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<RecdTransactionData> EmployeeDetails = GetTransactionList(0);
            List<RecdTransactionDatatoExcel> ListexcelData = new List<RecdTransactionDatatoExcel>();
            int i = 0;
            foreach (RecdTransactionData row in EmployeeDetails)
            {
                RecdTransactionDatatoExcel Ecxeclemp = new RecdTransactionDatatoExcel();
                Ecxeclemp.TransactionType = EmployeeDetails[i].TransactionType;
                Ecxeclemp.VoucherNo = EmployeeDetails[i].VoucherNo;
                Ecxeclemp.Particulars = EmployeeDetails[i].Particulars;
                Ecxeclemp.Remarks = EmployeeDetails[i].Remarks;
                Ecxeclemp.Partlyledger = EmployeeDetails[i].Partlyledger;
                Ecxeclemp.AddedDate = EmployeeDetails[i].AddedDate;
                Ecxeclemp.Amount = EmployeeDetails[i].Amount;
                ListexcelData.Add(Ecxeclemp);
                i++;
            }
            RecdTransactionDatatoExcel footerdata;
            footerdata = new RecdTransactionDatatoExcel();
            ListexcelData.Add(footerdata);
            footerdata = new RecdTransactionDatatoExcel();
            footerdata.TransactionType = "Total Transaction";
            footerdata.VoucherNo = "Total Amount Paid";
            footerdata.Particulars = "Total Cash Recieve";
            footerdata.Remarks = "Total Cash Paid";
            ListexcelData.Add(footerdata);

            footerdata = new RecdTransactionDatatoExcel();
            footerdata.TransactionType = EmployeeDetails[0].TotalAmount.ToString();
            footerdata.VoucherNo = EmployeeDetails[0].TotalAmountPaid.ToString();
            footerdata.Particulars = EmployeeDetails[0].TotalCashRecieve.ToString();
            footerdata.Remarks = EmployeeDetails[0].TotalCashPaid.ToString();
            ListexcelData.Add(footerdata);
            footerdata = new RecdTransactionDatatoExcel();
            footerdata.TransactionType = "Total Contra Inward";
            footerdata.VoucherNo = "Total Contra Outward";
            footerdata.Particulars = "Total Cash Outward";
            footerdata.Remarks = "Cash In Hand";
            ListexcelData.Add(footerdata);

            footerdata = new RecdTransactionDatatoExcel();
            footerdata.TransactionType = Commonfunction.Getrounding(EmployeeDetails[0].TotalCashContraRecieve.ToString());
            footerdata.VoucherNo = Commonfunction.Getrounding(EmployeeDetails[0].TotalCashContraPaid.ToString());
            footerdata.Particulars = Commonfunction.Getrounding((EmployeeDetails[0].TotalCashContraPaid + EmployeeDetails[0].TotalAmountPaid).ToString());
            footerdata.Remarks = Commonfunction.Getrounding(((EmployeeDetails[0].TotalCashRecieve + EmployeeDetails[0].TotalCashContraRecieve) - (EmployeeDetails[0].TotalCashContraPaid + EmployeeDetails[0].TotalAmountPaid)).ToString());

            ListexcelData.Add(footerdata);
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
        public string SendReqst(string pWebRequstStr)
        {
            string IP = Commonfunction.GetClientIPAddress();
            String lResponseStr = "";
            String lResult = "";
            string URL = "http://" + IP + ":9000";

            try
            {
                String lTallyLocalHost = URL;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(lTallyLocalHost);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentLength = (long)pWebRequstStr.Length;
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                StreamWriter lStrmWritr = new StreamWriter(httpWebRequest.GetRequestStream());
                lStrmWritr.Write(pWebRequstStr);
                lStrmWritr.Close();
                HttpWebResponse lhttpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream lreceiveStream = lhttpResponse.GetResponseStream();
                StreamReader lStreamReader = new StreamReader(lreceiveStream, Encoding.UTF8);
                lResponseStr = lStreamReader.ReadToEnd();
                lhttpResponse.Close();
                lStreamReader.Close();
            }
            catch (Exception)
            {

                throw;
            }
            lResult = lResponseStr;
            return lResult;
        }
        protected void btnSample_Click(object sender, EventArgs e)
        {

        }

        protected void GVRecdTransaction_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "Print")
                {

                    int j = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gv = GVRecdTransaction.Rows[j];
                    Label voucher = (Label)gv.Cells[0].FindControl("lblVoucherNo");
                    string url = "../MedAccount/Reports/ReportViewer.aspx?option=AccountTransaction&voucherNumber=" + voucher.Text.Trim();
                    string fullURL = "window.open('" + url + "', '_blank');";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {

            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            int account, tranType, accntState;
            DateTime DateFrom, DateTo;
            DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            account = Convert.ToInt32(ddl_ledgers.SelectedValue == "" ? "0" : ddl_ledgers.SelectedValue);
            tranType = Convert.ToInt32(ddl_transaction.SelectedValue == "" ? "0" : ddl_transaction.SelectedValue);
            accntState = Convert.ToInt32(ddl_account_close.SelectedValue == "" ? "0" : ddl_account_close.SelectedValue);

            string timefrom = txttimepickerfrom.Text.Trim();
            string timeto = txttimepickerto.Text.Trim();
            DateFrom = Convert.ToDateTime(from.ToString("yyyy-MM-dd") + " " + timefrom);
            DateTo = Convert.ToDateTime(To.ToString("yyyy-MM-dd") + " " + timeto);
            string url = "../MedAccount/Reports/ReportViewer.aspx?option=TransactionList&Account=" + account + "&DateFrom=" + DateFrom.ToString() + "&DateTo=" + DateTo.ToString() + "&TranType=" + tranType + "&AccountState=" + accntState;
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
    }
}