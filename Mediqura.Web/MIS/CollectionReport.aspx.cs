using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedAccount;
using Mediqura.BOL.MIS;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.MIS;
using Mediqura.DAL;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Utility.Util;
using Mediqura.Web.MedCommon;
using Newtonsoft.Json;
using SourceAFIS.Simple;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MIS
{
    public partial class CollectionReport : BasePage
    {
        static int bio;
        static string vouchers;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
            }
       //  initialize();
        }
        private void initialize()
        {
            string IP = Commonfunction.GetClientIPAddress();
            string URL = "http://" + IP + ":8080";
            Boolean flag = Commonfunction.isValidURL(URL);
            if (flag)
            {
                bio = 1;
                btnClosedAccount.Visible = true;
            }
            else
            {
                bio = 0;
                btnClosedAccount.Visible = false;
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_transaction, mstlookup.GetLookupsList(LookupName.PaymentType));
            Commonfunction.PopulateDdl(ddl_collectedby, mstlookup.GetLookupsList(LookupName.CollectionByAccount));
            Commonfunction.PopulateDdl(ddl_accounthead, mstlookup.GetLookupsList(LookupName.CollectionLedger));
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            if (LogData.PrintEnable == 0)
            {
                btn_print.Attributes["disabled"] = "disabled";
            }
            else
            {
                btn_print.Attributes.Remove("disabled");
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
    
        protected void bindgrid()
        {
            try
            {

                List<MisReportData> objdeposit = GetTransactionList(0);
                if (objdeposit.Count > 0)
                {
                    Gv_collectionreport.DataSource = objdeposit;
                    Gv_collectionreport.DataBind();
                    Gv_collectionreport.Visible = true;
                    txtTotalIncome.Text = Commonfunction.Getrounding(objdeposit[0].TotalIncome.ToString());
                    txtTotalExpenses.Text = Commonfunction.Getrounding(objdeposit[0].TotalExpense.ToString());
                    txtcashIncome.Text = Commonfunction.Getrounding(objdeposit[0].CashIncome.ToString());
                    txtCashExpense.Text = Commonfunction.Getrounding(objdeposit[0].cashExpense.ToString());
                    txtIncomeExpDiff.Text = Commonfunction.Getrounding(objdeposit[0].IncomeExpDiff.ToString());
                    txtbankDiposit.Text = Commonfunction.Getrounding(objdeposit[0].TotalDiposit.ToString());
                    txtBankWithdraw.Text = Commonfunction.Getrounding((objdeposit[0].TotalWithdraw).ToString());
                    txtCashInHand.Text = Commonfunction.Getrounding((objdeposit[0].CashInHand).ToString());
                    txtTotalReceivable.Text = Commonfunction.Getrounding((objdeposit[0].TotalRecievable).ToString());
                    txtbankIncome.Text = Commonfunction.Getrounding((objdeposit[0].BankIncome).ToString());
                    txtBankExpense.Text = Commonfunction.Getrounding((objdeposit[0].BankExpense).ToString());
                    txtTotalCashOutward.Text = Commonfunction.Getrounding((objdeposit[0].cashExpense+ objdeposit[0].TotalDiposit).ToString());
                    txtCashIncomeExpDiff.Text = Commonfunction.Getrounding((objdeposit[0].TotalCashDiff).ToString());
                     txtCashExpenseTot.Text = Commonfunction.Getrounding((objdeposit[0].cashExpense + objdeposit[0].TotalDiposit).ToString());
                    txtCashIncomeTotal.Text = Commonfunction.Getrounding((objdeposit[0].TotalCashIncomeAndAdvance).ToString());
                    txtCashOnlyCash.Text = Commonfunction.Getrounding((objdeposit[0].TotalCash).ToString());

                }
                else
                {
                    Gv_collectionreport.DataSource = null;
                    Gv_collectionreport.DataBind();
                    Gv_collectionreport.Visible = true;
                    lblresult.Visible = false;
                }
            }

            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<MisReportData> GetTransactionList(int curIndex)
        {
            MisReportData ObjData = new MisReportData();
            MisReportBO objgBO = new MisReportBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            ObjData.LedgerID = Convert.ToInt32(ddl_accounthead.SelectedValue == "" ? "0" : ddl_accounthead.SelectedValue);
            ObjData.TransactionType = Convert.ToInt32(ddl_transaction.SelectedValue == "" ? "0" : ddl_transaction.SelectedValue);
            ObjData.CollectedByID = Convert.ToInt32(ddl_collectedby.SelectedValue == "" ? "0" : ddl_collectedby.SelectedValue);
            ObjData.AccountState = Convert.ToInt32(ddl_account_close.SelectedValue == "" ? "0" : ddl_account_close.SelectedValue);
            ObjData.EmpName = LogData.EmpName;

            string timefrom = txttimepickerfrom.Text.Trim();
            string timeto = txttimepickerto.Text.Trim();
            ObjData.DateFrom = Convert.ToDateTime(from.ToString("yyyy-MM-dd") + " " + timefrom);
            ObjData.DateTo = Convert.ToDateTime(To.ToString("yyyy-MM-dd") + " " + timeto);

            return objgBO.GetTransactionList(ObjData);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            lblmessage.Visible = false;
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            ddl_transaction.SelectedIndex = 0;
            ddl_accounthead.SelectedIndex = 0;
            ddl_collectedby.SelectedIndex = 0;
            Gv_collectionreport.DataSource = null;
            Gv_collectionreport.DataBind();
            Gv_collectionreport.Visible = true;
            txttimepickerfrom.Text = "";
            txttimepickerto.Text = "";
            txtTotalIncome.Text = "0.00";
            txtTotalExpenses.Text = "0.00";
            txtcashIncome.Text = "0.00";
            txtCashExpense.Text = "0.00";
            txtIncomeExpDiff.Text = "0.00";
            txtbankDiposit.Text = "0.00";
            txtBankWithdraw.Text = "0.00";
            txtCashInHand.Text = "0.00";
            txtTotalReceivable.Text = "0.00";
            txtbankIncome.Text = "0.00";
            txtBankExpense.Text = "0.00";
            txtTotalCashOutward.Text = "0.00";

        }
        protected void Gv_collectionreport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label isSubHeading = (Label)e.Row.FindControl("lblIsSubHeading");
                Label lblVoucher = (Label)e.Row.FindControl("lblVoucher");
                Label lblTransactionAmount = (Label)e.Row.FindControl("lblTransactionAmount");
                
                    if (Convert.ToInt32(isSubHeading.Text) == 1)
                {
                    if (Convert.ToDecimal(lblTransactionAmount.Text) > 0)
                    {
                        e.Row.BackColor = Color.FromName("#cfede3");
                        lblVoucher.ForeColor = Color.FromName("#fd0808");
                        lblTransactionAmount.ForeColor = Color.FromName("#fd0808");
                        GridView editGrid = sender as GridView;

                        e.Row.Cells[0].Controls.Clear();

                        e.Row.Cells[1].ColumnSpan = 2;
                        e.Row.Cells[2].Visible = false;

                        e.Row.Cells[3].Controls.Clear();

                        e.Row.Cells[4].Controls.Clear();

                        e.Row.Cells[5].Controls.Clear();

                        e.Row.Cells[6].Controls.Clear();
                    }
                    else {
                        e.Row.Visible = false;
                    }
                    
                    

                }
                else if (Convert.ToInt32(isSubHeading.Text) == 2) {
                    if (Convert.ToDecimal(lblTransactionAmount.Text) > 0)
                    {
                        e.Row.BackColor = Color.FromName("#33aa99");
                        lblVoucher.ForeColor = Color.FromName("#FFFFFF");
                        GridView editGrid = sender as GridView;

                        e.Row.Cells[0].Controls.Clear();
                        e.Row.Cells[1].ColumnSpan = 2;
                        e.Row.Cells[2].Visible = false;

                        e.Row.Cells[3].Controls.Clear();

                        e.Row.Cells[4].Controls.Clear();

                        e.Row.Cells[5].Controls.Clear();

                        e.Row.Cells[6].Controls.Clear();

                        e.Row.Cells[7].Controls.Clear();
                    }
                    else {
                        e.Row.Visible = false;
                    }
                }
            
               
            }
           
        }
        protected void btn_print_Click(object sender, EventArgs e)
        {
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            int ledger = Convert.ToInt32(ddl_accounthead.SelectedValue == "" ? "0" : ddl_accounthead.SelectedValue);
            int tType = Convert.ToInt32(ddl_transaction.SelectedValue == "" ? "0" : ddl_transaction.SelectedValue);
            Int64 collectedBy=Convert.ToInt32(ddl_collectedby.SelectedValue == "" ? "0" : ddl_collectedby.SelectedValue);
            int AcntStat = Convert.ToInt32(ddl_account_close.SelectedValue == "" ? "0" : ddl_account_close.SelectedValue);
            string name = LogData.EmpName;

            string timefrom = txttimepickerfrom.Text.Trim();
            string timeto = txttimepickerto.Text.Trim();
            string url = "../MIS/Reports/ReportViewer.aspx?option=CollectionReport&ledger=" + ledger.ToString() + "&Ttype=" + tType.ToString() + "&CollectedBy=" + collectedBy.ToString() + "&status=" + AcntStat.ToString() + "&Datefrom=" + (from.ToString("yyyy-MM-dd") + " " + timefrom).ToString() + "&Dateto=" + (To.ToString("yyyy-MM-dd") + " " + timeto).ToString();
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
        static  DataTable dt;
        protected void btnClosedAccount_Click(object sender, EventArgs e)
        {
            lblAuthenticationMsg.Text = "";
            dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[1] {
             new DataColumn("VoucherNo", typeof(string))
            });
            foreach (GridViewRow row in Gv_collectionreport.Rows)
            {
                Label lblIsSubHeading = (Label)Gv_collectionreport.Rows[row.RowIndex].Cells[0].FindControl("lblIsSubHeading");
                Label lblVoucher = (Label)Gv_collectionreport.Rows[row.RowIndex].Cells[0].FindControl("lblVoucher");

                if (lblIsSubHeading.Text == "0") {
                    dt.Rows.Add(lblVoucher.Text);
                }
                
            }
            this.MDResponse.Show();

        }
        //biometric---------------------

        public void loadVerification()
        {
            string IP = Commonfunction.GetClientIPAddress();
            string URL = "http://" + IP + ":8080/CallMorphoAPI";
            string response = GET(URL);

            fingerData data = JsonConvert.DeserializeObject<fingerData>(response);

            string fptemp = data.Base64ISOTemplate;
            Bitmap fbimage = Base64StringToBitmap(data.Base64BMPIMage);
            FPImage.Visible = true;
            FPImage.ImageUrl = "data:image/bmp;base64," + data.Base64BMPIMage;
            int result = matchFP(fptemp, LogData.FPData);
            if (result == 1)
            {
               
                int flag = UpdateAccountClosed(dt, LogData.EmployeeID);
                if (flag == 1)
                {
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    divmsg1.Attributes["class"] = "SucessAlert";
                    divmsg1.Visible = true;
                    this.MDResponse.Hide();
                }
                else {
                    Messagealert_.ShowMessage(lblAuthenticationMsg, "system", 0);
                    this.MDResponse.Show();
                }
                

            }
            else
            {
                Messagealert_.ShowMessage(lblAuthenticationMsg, "Authentication Failed", 0);
               
                this.MDResponse.Show();

            }
        }
        public int UpdateAccountClosed(DataTable vouchers, Int64 emp)
        {
            int result = 0;
            string constr = ConfigurationManager.ConnectionStrings["SqlConnectionString11"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("usp_MDQ_Update_AccountClosed"))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Vouchers", vouchers);
                        cmd.Parameters.Add("@addededBy", SqlDbType.BigInt).Value = LogData.EmployeeID;
                        cmd.Parameters.Add("@Output", SqlDbType.SmallInt).Direction = ParameterDirection.Output;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        result = Convert.ToInt32(cmd.Parameters.Add("@Output", SqlDbType.SmallInt).Direction);
                     
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                        LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                    }
                }
            }
            return result;
        }
        public Bitmap Base64StringToBitmap(string base64String)
        {
            Bitmap bmpReturn = null;

            byte[] byteBuffer = Convert.FromBase64String(base64String);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);

            memoryStream.Position = 0;

            bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);

            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;

            return bmpReturn;
        }
        public class fingerData
        {
            public string ReturnCode { get; set; }
            public string Base64ISOTemplate { get; set; }
            public string Base64RAWIMage { get; set; }
            public string Base64BMPIMage { get; set; }
            public string NFIQ { get; set; }

        }
        public class PatientfingerData
        {
            public string UHID { get; set; }
            public string FPtemplates { get; set; }


        }
        string GET(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                Messagealert_.ShowMessage(lblAuthenticationMsg, "No Device found", 0);
                this.MDResponse.Show();
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }
        public int matchFP(string base64String, string Fpdata)
        {
            int result = 0;

            Afis = new AfisEngine();

            List<MyPerson> database = new List<MyPerson>();
            
            database.Add(Enroll(Fpdata, LogData.EmployeeID.ToString()));
            
            // Enroll visitor with unknown identity
            MyPerson probe = Enroll(base64String, "Visitor");

            // Look up the probe using Threshold = 10
            Afis.Threshold = 10;

            MyPerson match = Afis.Identify(probe, database).FirstOrDefault() as MyPerson;
            // Null result means that there is no candidate with similarity score above threshold
         
                float score = Afis.Verify(probe, match);


                if (score > 50)
                {
                    result = 1;


                }
                else
                {
                    lblmessage.Text = "Person found with a low match score of: " + score + " with UHID: " + match.UHID;
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    result = 0;
                }


            
            return result;
        }

   

        [Serializable]
        class MyFingerprint : Fingerprint
        {
            public string Filename;
        }

        // Inherit from Person in order to add Name field
        [Serializable]
        class MyPerson : Person
        {
            [DataMember]
            public string UHID { get; set; }
            [DataMember]
            public string FPtemplates { get; set; }

        }

        // Shared AfisEngine instance (cannot be shared between different threads though)
        static AfisEngine Afis;


        // Take fingerprint image file and create Person object from the image
        static MyPerson Enroll(string base64string, string UHID)
        {
            MyFingerprint fp = new MyFingerprint();
            fp.Filename = UHID;
            fp.AsIsoTemplate = Convert.FromBase64String(base64string);

            MyPerson person = new MyPerson();
            person.UHID = UHID;
            // Add fingerprint to the person
            person.Fingerprints.Add(fp);
            return person;
        }

        protected void ddl_account_close_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_account_close.SelectedIndex == 0) {
                if (bio == 1)
                {
                    btnClosedAccount.Visible = true;
                }
                else {
                    btnClosedAccount.Visible = false;
                }
            }
            else
            {
                btnClosedAccount.Visible = false;
            }
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            this.MDResponse.Show();
            loadVerification();
        }

        protected void btnSample_Click(object sender, EventArgs e)
        {

        }
    }
}