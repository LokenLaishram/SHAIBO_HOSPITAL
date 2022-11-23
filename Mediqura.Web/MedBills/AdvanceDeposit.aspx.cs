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
using Mediqura.BOL.MedBillBO;

namespace Mediqura.Web.MedBills
{
    public partial class AdvanceDeposit : BasePage
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
            Commonfunction.PopulateDdl(ddlpaymentmode, mstlookup.GetLookupsList(LookupName.PaymentMode));
            Commonfunction.PopulateDdl(ddlpaymentmodes, mstlookup.GetLookupsList(LookupName.PaymentMode));
            txttoraldeposited.Text = "0.00";
            txtajusted.Text = "0.00";
            txtbalance.Text = "0.00";
            UHID.Text = "UHID";
            AutoCompleteExtender3.ContextKey = "1";
            txt_otpassnumber.Visible = false;
            txt_otpassnumber.Text = "";
            btnsave.Attributes["disabled"] = "disabled";
            Session["DepositList"] = null;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetUHID(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientDetailName = prefixText;
            Objpaic.DepositType = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetAvancedepositpatientdetail(Objpaic);

            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientDetailName.ToString());
            }
            return list;
        }
        protected void ddl_deposittype_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUHID.Text = "";
            if (ddl_deposittype.SelectedValue == "0")
            {
                UHID.Text = "UHID";
                AutoCompleteExtender3.ContextKey = "0";
            }
            if (ddl_deposittype.SelectedValue == "1" || ddl_deposittype.SelectedValue == "5")
            {
                UHID.Text = "UHID";
                AutoCompleteExtender3.ContextKey = "1";
            }
            if (ddl_deposittype.SelectedValue == "2")
            {
                UHID.Text = "Emrg.No";
                AutoCompleteExtender3.ContextKey = "2";
            }
            if (ddl_deposittype.SelectedValue == "3" || ddl_deposittype.SelectedValue == "4")
            {
                UHID.Text = "IPNo.";
                AutoCompleteExtender3.ContextKey = "3";
            }
        }
        protected void txtUHID_TextChanged(object sender, EventArgs e)
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
                    return;
                }
            }
            else
            {
                Objpaic.UHID = Convert.ToInt64(txtUHID.Text == "" ? "0" : txtUHID.Text);
            }
            Objpaic.DepositType = Convert.ToInt32(ddl_deposittype.SelectedValue == "" ? "0" : ddl_deposittype.SelectedValue);
            getResult = objInfoBO.GetPatientDetailsByUHIDwithdeposittype(Objpaic);
            if (getResult.Count > 0)
            {
                txtaddress.Text = getResult[0].Address.ToString();
                txt_contactnumber.Text = getResult[0].ContactNo.ToString();
                hdnemrgnumber.Value = getResult[0].EmrgNo.ToString();
                hdnuhid.Value = getResult[0].UHID.ToString();
                hdnipnumber.Value = getResult[0].IPNo.ToString();
                txtdepositetype.Focus();
                Session["DepositList"] = null;
            }
            else
            {
                txtaddress.Text = "";
                txtUHID.Text = "";
                txt_contactnumber.Text = "";
                hdnemrgnumber.Value = "";
                hdnuhid.Value = "";
                hdnipnumber.Value = "";
                txtUHID.Focus();
            }
        }
        protected void ddlpaymentmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbank.Text = "";
            txt_chequenumber.Text = "";
            txtinvoicenumber.Text = "";
            if (ddlpaymentmode.SelectedIndex > 0)
            {
                if (ddlpaymentmode.SelectedValue == "1")
                {
                    txtbank.Text = "";
                    txtbank.ReadOnly = true;
                    txt_chequenumber.ReadOnly = true;
                    txtinvoicenumber.ReadOnly = true;
                }
                if (ddlpaymentmode.SelectedValue == "2")
                {
                    GetBankName(Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue));
                    txtbank.ReadOnly = true;
                    txt_chequenumber.ReadOnly = true;
                    txtinvoicenumber.ReadOnly = false;
                }
                if (ddlpaymentmode.SelectedValue == "3")
                {
                    GetBankName(Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue));
                    txtbank.ReadOnly = true;
                    txt_chequenumber.ReadOnly = false;
                    txtinvoicenumber.ReadOnly = true;
                }
                if (ddlpaymentmode.SelectedValue == "4")
                {
                    txtbank.Text = "";
                    txtbank.ReadOnly = false;
                    txt_chequenumber.ReadOnly = false;
                    txtinvoicenumber.ReadOnly = true;
                }
            }
            else
            {
                txtbank.Text = "";
                txtbank.ReadOnly = true;
                txt_chequenumber.ReadOnly = true;
                txtinvoicenumber.ReadOnly = true;
            }

        }
        protected void GetBankName(int paymode)
        {
            OPDbillingBO objbillingBO = new OPDbillingBO();
            BankDetail objbankdetail = new BankDetail();
            objbankdetail.PaymodeID = paymode;
            List<BankDetail> banklist = objbillingBO.Getbanklist(objbankdetail);
            if (banklist.Count > 0)
            {
                txtbank.Text = banklist[0].BankName.ToString();
                hdnbankID.Value = banklist[0].BankID.ToString();
            }
            else
            {
                txtbank.Text = "";
                hdnbankID.Value = null;
            }
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (txtUHID.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "UHID", 0);
                div2.Visible = true;
                div2.Attributes["class"] = "FailAlert";
                txtUHID.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div2.Visible = false;
            }
            if (txtdepositetype.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Particulars", 0);
                div2.Visible = true;
                div2.Attributes["class"] = "FailAlert";
                txtdepositetype.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div2.Visible = false;
            }
            if (txtamount.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Depositamount", 0);
                div2.Visible = true;
                div2.Attributes["class"] = "FailAlert";
                txtamount.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div2.Visible = false;
            }

            IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
            List<DepositData> DepositList = Session["DepositList"] == null ? new List<DepositData>() : (List<DepositData>)Session["DepositList"];
            DepositData objdeposit = new DepositData();
            objdeposit.DepositParticulars = txtdepositetype.Text.Trim();
            objdeposit.Amount = Convert.ToDecimal(txtamount.Text.ToString() == "" ? "0" : txtamount.Text.ToString());
            txttotalAmount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txttotalAmount.Text == "" ? "0" : txttotalAmount.Text) + Convert.ToDecimal(txtamount.Text.ToString() == "" ? "0" : txtamount.Text.ToString())).ToString());
            DepositList.Add(objdeposit);

            if (DepositList.Count > 0)
            {
                gvdeposit.DataSource = DepositList;
                gvdeposit.DataBind();
                gvdeposit.Visible = true;
                Session["DepositList"] = DepositList;
                txtdepositetype.Text = "";
                txtamount.Text = "";
                ddl_deposittype.Attributes["disabled"] = "disabled";
                btnsave.Attributes.Remove("disabled");
                txtdepositetype.Focus();
            }
            else
            {
                ddl_deposittype.Attributes.Remove("disabled");
                btnsave.Attributes.Remove("disabled");
                gvdeposit.DataSource = null;
                gvdeposit.DataBind();
                gvdeposit.Visible = true;
            }
        }
        protected void gvdeposit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSerial = (Label)e.Row.FindControl("lblserialID");
                lblSerial.Text = ((gvdeposit.PageIndex * gvdeposit.PageSize) + e.Row.RowIndex + 1).ToString();
            }
        }
        protected void gvdeposit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvdeposit.Rows[i];
                    List<DepositData> ItemList = Session["DepositList"] == null ? new List<DepositData>() : (List<DepositData>)Session["DepositList"];
                    if (ItemList.Count > 0)
                    {
                        Decimal totalamount = ItemList[i].Amount;
                        txttotalAmount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txttotalAmount.Text == "" ? "0" : txttotalAmount.Text) - totalamount).ToString());
                    }
                    ItemList.RemoveAt(i);
                    Session["DepositList"] = ItemList;
                    gvdeposit.DataSource = ItemList;
                    gvdeposit.DataBind();
                    if (ItemList.Count == 0)
                    {
                        btnsave.Attributes["disabled"] = "dsiabled";
                    }
                    else
                    {
                        btnsave.Attributes.Remove("disabled");
                    }

                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                lblmessage.Visible = true;
                lblmessage.CssClass = "Message";
            }
        }
        protected void txtdepositetype_TextChanged(object sender, EventArgs e)
        {
            if (txtdepositetype.Text == "")
            {
                txtdepositetype.Focus();
            }
            else
            {
                txtamount.Focus();
            }
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txtUHID.Text = "";
            txt_contactnumber.Text = "";
            txtaddress.Text = "";
            txtdepositno.Text = "";
            txtdepositetype.Text = "";
            txt_otpassnumber.Visible = false;
            txt_otpassnumber.Text = "";
            txtamount.Text = "";
            ddlpaymentmode.SelectedIndex = 0;
            txtbank.Text = "";
            txtinvoicenumber.Text = "";
            txtbank.ReadOnly = true;
            txt_chequenumber.ReadOnly = true;
            txttotalAmount.Text = "";
            Session["DepositList"] = null;
            gvdeposit.Visible = false;
            lblmessage.Visible = false;
            div2.Visible = false;
            hdnbankID.Value = null;
            UHID.Text = "UHID";
            ddl_deposittype.Attributes.Remove("disabled");
            hdnemrgnumber.Value = "";
            hdnuhid.Value = "";
            hdnipnumber.Value = "";
            btnsave.Attributes["disabled"] = "disabled";
            txtUHID.ReadOnly = false;
            txtdepositetype.ReadOnly = false;
            txtamount.ReadOnly = false;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (txtUHID.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "UHID", 0);
                div2.Visible = true;
                div2.Attributes["class"] = "FailAlert";
                txtUHID.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div2.Visible = false;
            }
            if (ddlpaymentmode.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Paymode", 0);
                div2.Visible = true;
                div2.Attributes["class"] = "FailAlert";
                ddlpaymentmode.Focus();
                return;
            }
            if (ddlpaymentmode.SelectedIndex > 1)
            {
                if (ddlpaymentmode.SelectedValue == "2")
                {
                    if (txtinvoicenumber.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "Invoicenumber", 0);
                        div2.Visible = true;
                        div2.Attributes["class"] = "FailAlert";
                        txtinvoicenumber.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        div2.Visible = false;
                    }
                }
                if (ddlpaymentmode.SelectedValue == "3")
                {
                    if (txt_chequenumber.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "Chequenumber", 0);
                        div2.Visible = true;
                        div2.Attributes["class"] = "FailAlert";
                        txt_chequenumber.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        div2.Visible = false;
                    }
                }
                if (ddlpaymentmode.SelectedValue == "4")
                {
                    if (txtbank.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "BankName", 0);
                        div2.Visible = true;
                        div2.Attributes["class"] = "FailAlert";
                        txtbank.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        div2.Visible = false;
                    }
                    if (txt_chequenumber.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "Chequenumber", 0);
                        div2.Visible = true;
                        div2.Attributes["class"] = "FailAlert";
                        txt_chequenumber.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        div2.Visible = false;
                    }
                }
            }
            else
            {
                lblmessage.Visible = false;
                div2.Visible = false;
            }
            List<DepositData> Listbill = new List<DepositData>();
            DepositData objdeposit = new DepositData();
            DepositBO objstdBO = new DepositBO();
            // int index = 0;
            int COUNT = 0;
            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in gvdeposit.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label Particulars = (Label)gvdeposit.Rows[row.RowIndex].Cells[0].FindControl("lblparticulars");
                    Label amount = (Label)gvdeposit.Rows[row.RowIndex].Cells[0].FindControl("lblamount");
                    Label SerialID = (Label)gvdeposit.Rows[row.RowIndex].Cells[0].FindControl("lblserialID");
                    DepositData ObjDetails = new DepositData();
                    COUNT = COUNT + 1;
                    ObjDetails.DepositParticulars = Particulars.Text == "" ? null : Particulars.Text;
                    ObjDetails.Amount = Convert.ToDecimal(amount.Text == "" ? "0" : amount.Text);
                    ObjDetails.SerialID = Convert.ToInt32(SerialID.Text == "" ? "0" : SerialID.Text);
                    Listbill.Add(ObjDetails);
                }
                objdeposit.XMLData = XmlConvertor.DepositDatatoXML(Listbill).ToString();
                objdeposit.TotalAmount = Convert.ToDecimal(txttotalAmount.Text == "" ? "0" : txttotalAmount.Text);
                objdeposit.UHID = Convert.ToInt64(hdnuhid.Value == "" ? "0" : hdnuhid.Value);
                objdeposit.IPNo = hdnipnumber.Value == "" ? "0" : hdnipnumber.Value;
                objdeposit.EmrgNo = hdnemrgnumber.Value == "" ? "0" : hdnemrgnumber.Value;
                objdeposit.Deposittype = Convert.ToInt32(ddl_deposittype.SelectedValue == "" ? "0" : ddl_deposittype.SelectedValue);
                objdeposit.Paymode = Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue);
                objdeposit.BankName = txtbank.Text == "" ? null : txtbank.Text;
                objdeposit.Cheque = txt_chequenumber.Text == "" ? null : txt_chequenumber.Text;
                objdeposit.Invoicenumber = txtinvoicenumber.Text == "" ? null : txtinvoicenumber.Text;
                objdeposit.BankID = Convert.ToInt32(hdnbankID.Value == "" || hdnbankID.Value == null ? "0" : hdnbankID.Value);
                objdeposit.Deposittype = Convert.ToInt32(ddl_deposittype.SelectedValue == "" ? "0" : ddl_deposittype.SelectedValue);
                objdeposit.FinancialYearID = LogData.FinancialYearID;
                objdeposit.EmployeeID = LogData.EmployeeID;
                objdeposit.HospitalID = LogData.HospitalID;
                if (COUNT == 0 || COUNT > 1)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please take only one avance.", 0);
                    div2.Visible = true;
                    div2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    div2.Visible = true;
                }

                Listbill = objstdBO.UpdateDepositDetails(objdeposit);
                if (Listbill.Count > 0)
                {
                    Session["DepositList"] = null;
                    txtdepositno.Text = Listbill[0].DepositNo.ToString();
                    Session["DoubleDesposit"] = Listbill[0].DepositNo.ToString();
                    if (ddl_deposittype.SelectedValue == "4")
                    {
                        txt_otpassnumber.Visible = true;
                        txt_otpassnumber.Text = Listbill[0].OTpassnumber.ToString();
                    }
                    else
                    {
                        txt_otpassnumber.Visible = false;
                        txt_otpassnumber.Text = "";
                    }
                    txtUHID.Text = "";
                    btnsave.Attributes["disabled"] = "disabled";
                    AutoCompleteExtender3.ContextKey = "0";
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    div2.Visible = true;
                    div2.Attributes["class"] = "SucessAlert";
                }
                else
                {
                    txtdepositno.Text = "";
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    div2.Visible = true;
                    div2.Attributes["class"] = "FailAlert";
                    btnsave.Attributes.Remove("disabled");
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                div2.Visible = true;
                div2.Attributes["class"] = "FailAlert";
            }
        }
        private void GenerateBacode(string _data, string _filename)
        {

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
            if (txtdatefrom.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage2, "DateRange", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
                return;
            }
            if (txtdatefrom.Text != "")
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
            if (txtto.Text != "")
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
                List<DepositData> objdeposit = GetDepositList(0);
                if (objdeposit.Count > 0)
                {
                    gvdepositlist.DataSource = objdeposit;
                    gvdepositlist.DataBind();
                    gvdepositlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;

                    txttoraldeposited.Text = Commonfunction.Getrounding(objdeposit[0].TotalAmount.ToString());
                    txtajusted.Text = Commonfunction.Getrounding(objdeposit[0].AdustedAmount.ToString());
                    txtotaltrefunded.Text = Commonfunction.Getrounding(objdeposit[0].RefundAmount.ToString());
                    txtbalance.Text = Commonfunction.Getrounding(objdeposit[0].BalanceAmount.ToString());

                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblmessage2.Visible = false;
                    divmsg2.Visible = false;
                }
                else
                {
                    gvdepositlist.DataSource = null;
                    gvdepositlist.DataBind();
                    gvdepositlist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    txtotaltrefunded.Text = "0.00";
                    txttoraldeposited.Text = "0.00";
                    txtajusted.Text = "0.00";
                    txtbalance.Text = "0.00";
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
        public List<DepositData> GetDepositList(int curIndex)
        {
            DepositData objpat = new DepositData();
            DepositBO objstdBO = new DepositBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.UHID = Convert.ToInt64(txtautoUHID.Text == "" ? "0" : txtautoUHID.Text);
            objpat.PatientName = txtpatientNames.Text == "" ? null : txtpatientNames.Text.Trim();
            objpat.Paymode = Convert.ToInt32(ddlpaymentmodes.SelectedValue == "" ? "0" : ddlpaymentmodes.SelectedValue);
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objpat.DateFrom = from;
            objpat.DateTo = To;
            return objstdBO.GetDepositList(objpat);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtautoUHID.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            ddlstatus.SelectedIndex = 0;
            gvdepositlist.DataSource = null;
            gvdepositlist.DataBind();
            gvdepositlist.Visible = false;
            lblresult.Visible = false;
            txtpatientNames.Text = "";
            ddlpaymentmode.SelectedIndex = 0;
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblresult.Visible = false;
            lblresult.Visible = false;
            divmsg2.Visible = false;
            divmsg3.Visible = false;
            txttoraldeposited.Text = "0.00";
            txtajusted.Text = "0.00";
            txtbalance.Text = "0.00";
        }
        protected void gvdepositlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    DepositData objbill = new DepositData();
                    DepositBO objstdBO = new DepositBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvdepositlist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    Label UHID = (Label)gr.Cells[0].FindControl("lbluhid");
                    Label Amount = (Label)gr.Cells[0].FindControl("lblamount");
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
                        objbill.Remarks = txtremarks.Text;
                    }
                    objbill.DepositNo = ID.Text.Trim();
                    objbill.UHID = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                    objbill.EmployeeID = LogData.UserLoginId;
                    objbill.Amount = Convert.ToDecimal(Amount.Text == "" ? "0" : Amount.Text);
                    int Result = objstdBO.DeleteDepositByID(objbill);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblresult, "delete", 1);
                        divmsg3.Attributes["class"] = "SucessAlert";
                        divmsg3.Visible = true;
                        bindgrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblresult, "system", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                    }

                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<DepositData> DepositDetails = GetDepositList(0);
            List<DepositDataTOeXCEL> ListexcelData = new List<DepositDataTOeXCEL>();
            int i = 0;
            foreach (DepositData row in DepositDetails)
            {
                DepositDataTOeXCEL Ecxeclpat = new DepositDataTOeXCEL();
                Ecxeclpat.UHID = DepositDetails[i].UHID;
                Ecxeclpat.PatientName = DepositDetails[i].PatientName;
                Ecxeclpat.Address = DepositDetails[i].Address;
                Ecxeclpat.DepositAmount = DepositDetails[i].DepositAmount;
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
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
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
                    gvdepositlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvdepositlist.Columns[5].Visible = false;
                    gvdepositlist.Columns[6].Visible = false;
                    gvdepositlist.Columns[7].Visible = false;
                    gvdepositlist.Columns[8].Visible = false;

                    gvdepositlist.RenderControl(hw);
                    gvdepositlist.HeaderRow.Style.Add("width", "15%");
                    gvdepositlist.HeaderRow.Style.Add("font-size", "10px");
                    gvdepositlist.Style.Add("text-decoration", "none");
                    gvdepositlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvdepositlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Deposit.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                    Messagealert_.ShowMessage(lblmessage2, "Exported", 1);
                    divmsg2.Attributes["class"] = "SucessAlert";
                    divmsg2.Visible = true;
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
                Response.AddHeader("content-disposition", "attachment;filename=DepositDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblmessage2, "Exported", 1);
                divmsg2.Attributes["class"] = "SucessAlert";
                divmsg2.Visible = true;
            }
        }
        protected void txtautoUHID_TextChanged(object sender, EventArgs e)
        {
            if (txtautoUHID.Text != "")
            {
                bindgrid();
            }
        }
        protected void gvdeposit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvdeposit.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            if (txtpatientNames.Text != "")
            {
                bindgrid();
            }
        }
    }
}