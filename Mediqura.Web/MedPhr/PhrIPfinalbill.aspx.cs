using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.BOL.MedEmergencyBO;
using Mediqura.BOL.MedStore;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedBillData;
using Mediqura.CommonData.MedEmergencyData;
using Mediqura.CommonData.MedStore;
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

namespace Mediqura.Web.MedPhr
{
    public partial class PhrIPfinalbill : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                if (Session["BillPay"] != null)
                {
                    txt_ipnos.Text = Session["BillPay"].ToString();
                    txt_ipnos.ReadOnly = true;
                    Getfinalbilldetails(0);
                    Session["BillPay"] = null;
                }
                txt_dueamount.Attributes["disabled"] = "disabled";
            }
        }

        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlpaymentmode, mstlookup.GetLookupsList(LookupName.PaymentMode));
            Commonfunction.PopulateDdl(ddlpaymentmodes, mstlookup.GetLookupsList(LookupName.PaymentMode));
            Commonfunction.PopulateDdl(ddlcollectedby, mstlookup.GetLookupsList(LookupName.CollectedBy));
            Commonfunction.PopulateDdl(ddl_responsible, mstlookup.GetLookupsList(LookupName.Employee));
            btnprint.Attributes["disabled"] = "disabled";
            btnsave.Attributes["disabled"] = "disabled";
            btn_refund.Attributes["disabled"] = "disabled";
            btn_refund.Attributes["disabled"] = "disabled";
            ddl_responsible.Attributes["disabled"] = "disabled";
            hdnbillsubmittype.Value = "1";
            hdnreqno.Value = "0";

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetintimtedIPNos(string prefixText, int count, string contextKey)
        {
            IPData objData = new IPData();
            FInalBillBO objBO = new FInalBillBO();
            List<IPData> getResult = new List<IPData>();
            objData.IPNo = prefixText;
            getResult = objBO.GetPHRIntimatedIPNos(objData);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPNo.ToString());
            }
            return list;
        }
        protected void txt_ipnos_TextChanged(object sender, EventArgs e)
        {
            Getfinalbilldetails(0);
        }
        protected void Getfinalbilldetails(Int64 emrgId)
        {
            PHRIPFinalData objData = new PHRIPFinalData();
            EmrgAdmissionBO ObjBO = new EmrgAdmissionBO();
            List<PHRIPFinalData> getResult = new List<PHRIPFinalData>();
            objData.IPNo = txt_ipnos.Text.Trim() == "" ? "" : txt_ipnos.Text.Trim();
            objData.FinancialYearID = LogData.FinancialYearID;
            objData.HospitalID = LogData.HospitalID;
            getResult = ObjBO.Get_PHR_IPfinalBilldetails(objData);
            if (getResult.Count > 0)
            {
                txtname.Text = getResult[0].PatientName.ToString();
                txt_ipnos.Text = getResult[0].IPNo.ToString();
                txt_address.Text = getResult[0].PatientAddress.ToString();
                txt_Age.Text = getResult[0].Agecount.ToString();
                txt_admissionDate.Text = getResult[0].AdmissionDate.ToString("dd/MM/yyyy");
                txt_department.Text = getResult[0].Department.ToString();
                txt_doctor.Text = getResult[0].AdmissionDoctor.ToString();
                txt_careof.Text = getResult[0].PatRelatives.ToString();
                lbl_UHIDTemp.Text = getResult[0].UHID.ToString();
                txtbalanceinac.Text = Commonfunction.Getrounding(getResult[0].BalanceAmount.ToString());
                txttotalamount.Text = Commonfunction.Getrounding(getResult[0].TotalBillAmount.ToString());
                btnsave.Attributes.Remove("disabled");
                hdnbillnumber.Value = null;
                if (Convert.ToDecimal(getResult[0].BalanceAmount.ToString()) <= 0)
                {
                    txt_payableamount.Text = Commonfunction.Getrounding((getResult[0].TotalBillAmount - getResult[0].TotalReturnAmnt).ToString());
                    txt_totalpaid.Text = txt_payableamount.Text;
                    hdnpayable.Value = Commonfunction.Getrounding((getResult[0].TotalBillAmount - getResult[0].TotalReturnAmnt).ToString());
                    txtadjustedamount.Text = "0";
                    txt_refundable.Text = "0";
                    txt_dueamount.Text = "0";
                    txt_totalpaid.ReadOnly = false;
                }
                if (Convert.ToDecimal(getResult[0].BalanceAmount.ToString()) > 0 && Convert.ToDecimal(getResult[0].BalanceAmount.ToString()) < Convert.ToDecimal(getResult[0].TotalBillAmount - getResult[0].TotalReturnAmnt))
                {
                    txt_payableamount.Text = Commonfunction.Getrounding((Convert.ToDecimal((getResult[0].TotalBillAmount - getResult[0].TotalReturnAmnt)) - Convert.ToDecimal(getResult[0].BalanceAmount.ToString())).ToString());
                    txt_totalpaid.Text = txt_payableamount.Text;
                    hdnpayable.Value = Commonfunction.Getrounding((Convert.ToDecimal((getResult[0].TotalBillAmount - getResult[0].TotalReturnAmnt)) - Convert.ToDecimal(getResult[0].BalanceAmount.ToString())).ToString());
                    txtadjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(getResult[0].BalanceAmount.ToString())).ToString()); ;
                    txt_refundable.Text = "0";
                    txt_dueamount.Text = "0";
                    txt_totalpaid.ReadOnly = false;
                }
                if (Convert.ToDecimal(getResult[0].BalanceAmount.ToString()) > 0 && Convert.ToDecimal(getResult[0].BalanceAmount.ToString()) > Convert.ToDecimal(getResult[0].TotalBillAmount - getResult[0].TotalReturnAmnt))
                {
                    txt_payableamount.Text = "0";
                    txt_totalpaid.Text = "0";
                    hdnpayable.Value = "0";
                    txt_totalpaid.ReadOnly = true;
                    txtadjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal((getResult[0].TotalBillAmount - getResult[0].TotalReturnAmnt))).ToString()); ;
                    txt_refundable.Text = Commonfunction.Getrounding((Convert.ToDecimal(getResult[0].BalanceAmount.ToString()) - Convert.ToDecimal((getResult[0].TotalBillAmount - getResult[0].TotalReturnAmnt))).ToString());
                    txt_dueamount.Text = "";
                }
                if (Convert.ToDecimal(getResult[0].BalanceAmount.ToString()) > 0 && Convert.ToDecimal(getResult[0].BalanceAmount.ToString()) ==  Convert.ToDecimal(getResult[0].TotalBillAmount - getResult[0].TotalReturnAmnt))
                {
                    txt_payableamount.Text = "0";
                    hdnpayable.Value = "0";
                    txt_totalpaid.ReadOnly = true;
                    txt_totalpaid.Text = "0";
                    txtadjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal((getResult[0].TotalBillAmount - getResult[0].TotalReturnAmnt))).ToString()); ;
                    txt_refundable.Text = "0";
                    txt_dueamount.Text = "0";
                }
                Gvitemlist.Visible = true;
                Gvitemlist.DataSource = getResult;
                Gvitemlist.DataBind();
            }
            else
            {
                txt_dueamount.Text = "";
                txtname.Text = "";
                txt_address.Text = "";
                txt_Age.Text = "";
                txt_doctor.Text = "";
                txt_careof.Text = "";
                txt_admissionDate.Text = "";
                txt_department.Text = "";
                txt_doctor.Text = "";
                lbl_UHIDTemp.Text = "";
                txttotalamount.Text = "";
                txtbalanceinac.Text = "";
                Gvitemlist.DataSource = null;
                Gvitemlist.DataBind();
                txtname.Text = "";
                txt_ipnos.Text = "";
                txt_payableamount.Text = "";
                btnsave.Attributes["disabled"] = "disabled";
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPatientName(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetPatientNameIP(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        protected void ddlpaymentmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlpaymentmode.SelectedIndex > 0)
            {
                if (ddlpaymentmode.SelectedValue == "1")
                {
                    txtbank.Text = "";
                    txtbank.ReadOnly = true;
                    txt_chequenumber.ReadOnly = true;
                    txtinvoicenumber.ReadOnly = true;
                }
                else if (ddlpaymentmode.SelectedValue == "2")
                {
                    GetBankName(Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue));
                    txtbank.ReadOnly = true;
                    txt_chequenumber.ReadOnly = true;
                    txtinvoicenumber.ReadOnly = false;
                }
                else if (ddlpaymentmode.SelectedValue == "3")
                {
                    GetBankName(Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue));
                    txtbank.ReadOnly = true;
                    txt_chequenumber.ReadOnly = false;
                    txtinvoicenumber.ReadOnly = true;
                }
                else if (ddlpaymentmode.SelectedValue == "4")
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
        protected void btn_reqsearch_Click(object sender, EventArgs e)
        {
            bindrequestlist();
        }
        private void bindrequestlist()
        {
            List<MedOPDSalesData> Liststock = new List<MedOPDSalesData>();
            StockStatusBO ObjBO = new StockStatusBO();
            MedOPDSalesData objsalesData = new MedOPDSalesData();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_reqdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_reqdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txt_reqdateto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_reqdateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objsalesData.ReqNo = txt_reqno.Text.Trim() == "" ? "" : txt_reqno.Text.Trim();
            objsalesData.Datefrom = from;
            objsalesData.Dateto = To;
            objsalesData.RequestStatus = Convert.ToInt32(ddl_RequestStatus.SelectedValue == "" ? "0" : ddl_RequestStatus.SelectedValue);
            objsalesData.IsActive = ddl_status.SelectedValue == "0" ? true : false;
            objsalesData.PatientType = 2;
            objsalesData.FinancialYearID = LogData.FinancialYearID;
            objsalesData.HospitalID = LogData.HospitalID;
            Liststock = ObjBO.GetdiscountRequestList(objsalesData);
            if (Liststock.Count > 0)
            {
                Messagealert_.ShowMessage(lbl_result3, "Total:" + Liststock[0].MaximumRows.ToString() + " Record(s) found.", 1);
                div7.Attributes["class"] = "SucessAlert";
                GvDiscountRequest.Visible = true;
                GvDiscountRequest.DataSource = Liststock;
                GvDiscountRequest.DataBind();
            }
            else
            {
                lbl_result3.Visible = false;
                GvDiscountRequest.DataSource = Liststock;
                GvDiscountRequest.DataBind();
            }
        }
        protected void Gvdiscountreq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label StockStaus = (Label)e.Row.FindControl("lbl_statusID");
                Label Label1 = (Label)e.Row.FindControl("lblstatus");
                LinkButton delete = (LinkButton)e.Row.FindControl("lnkDelete");
                LinkButton Pay = (LinkButton)e.Row.FindControl("lnkpay");
                if (StockStaus.Text == "1")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Yellow;
                    Label1.ForeColor = System.Drawing.Color.Black;
                    delete.Visible = true;
                    Pay.Visible = false;
                }
                if (StockStaus.Text == "2")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Green;
                    Label1.ForeColor = System.Drawing.Color.White;
                    delete.Visible = false;
                    Pay.Visible = true;
                }
                if (StockStaus.Text == "3")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Red;
                    Label1.ForeColor = System.Drawing.Color.White;
                    delete.Visible = false;
                    Pay.Visible = false;
                }
                if (StockStaus.Text == "4")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Green;
                    Label1.ForeColor = System.Drawing.Color.White;
                    delete.Visible = false;
                    Pay.Visible = false;
                }
            }
        }
        protected void GvDiscountRequest_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    StockGRNData objbill = new StockGRNData();
                    StockGRNBO objstdBO = new StockGRNBO();

                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvDiscountRequest.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    Label Transaction = (Label)gr.Cells[0].FindControl("lbl_transID");
                    Label reqNo = (Label)gr.Cells[0].FindControl("lbl_reqno");
                    TextBox Remarks = (TextBox)gr.Cells[0].FindControl("txtremarks");

                    objbill.ID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                    objbill.TransactionID = Convert.ToInt64(Transaction.Text == "" ? "0" : Transaction.Text);
                    objbill.ReqNo = reqNo.Text.Trim();
                    if (Remarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lbl_result3, "Remarks", 0);
                        div7.Attributes["class"] = "FailAlert";
                        div7.Visible = false;
                        Remarks.Focus();
                        return;
                    }
                    else
                    {
                        objbill.Remarks = Remarks.Text;
                    }
                    objbill.MedSubStockTypeID = LogData.MedSubStockID;
                    objbill.EmployeeID = LogData.EmployeeID;
                    objbill.HospitalID = LogData.HospitalID;
                    int Result = objstdBO.DeleteMedDiscountRequestByID(objbill);
                    if (Result == 1)
                    {
                        bindrequestlist();
                        Messagealert_.ShowMessage(lbl_result3, "delete", 1);
                        div7.Attributes["class"] = "SucessAlert";
                        div7.Visible = true;
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lbl_result3, "system", 0);
                        div7.Attributes["class"] = "FailAlert";
                        div7.Visible = true;
                    }
                }
                if (e.CommandName == "Pay")
                {
                    StockGRNData objbill = new StockGRNData();
                    StockGRNBO objstdBO = new StockGRNBO();

                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvDiscountRequest.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    Label Transaction = (Label)gr.Cells[0].FindControl("lbl_transID");
                    Label reqNo = (Label)gr.Cells[0].FindControl("lbl_reqno");

                    objbill.ID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                    objbill.TransactionID = Convert.ToInt64(Transaction.Text == "" ? "0" : Transaction.Text);
                    objbill.ReqNo = reqNo.Text.Trim();
                    objbill.MedSubStockTypeID = LogData.MedSubStockID;
                    List<StockGRNData> Result = objstdBO.GetDiscountreqDetailsforPayment(objbill);
                    if (Result.Count > 0)
                    {
                        hdnbillsubmittype.Value = "3";
                        hdnreqno.Value = Result[0].ReqNo.ToString();
                        txt_discountvalue.Text = Commonfunction.Getrounding(Result[0].ApprovedAmount.ToString());
                        lblmessage.Visible = false;
                        txt_discountvalue.ReadOnly = true;
                        txt_discountPC.ReadOnly = true;
                        txt_totalpaid.ReadOnly = true;
                        txt_ipnos.ReadOnly = true;
                        txt_ipnos.Text = Result[0].IPNo.ToString();
                        txtdiscremoarks.Text = Result[0].Remarks.ToString();
                        btnsave.Attributes.Remove("disabled");
                        Getfinalbilldetails(0);
                        txt_totalpaid.ReadOnly = true;
                        txtdiscremoarks.ReadOnly = true;
                        txt_totalpaid.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_payableamount.Text == "" ? "0" : txt_payableamount.Text) - Convert.ToDecimal(Result[0].ApprovedAmount)).ToString());
                        btn_refund.Attributes["disabled"] = "disabled";
                        btnprint.Attributes["disabled"] = "disabled";
                        btnsave.Attributes.Remove("disabled");
                        ddl_responsible.Attributes["disabled"] = "disabled";
                        tabcontainerpatient.ActiveTabIndex = 0;
                    }
                    else
                    {
                        hdnreqno.Value = "0";
                        btn_refund.Attributes.Remove("disabled");
                        btnprint.Attributes.Remove("disabled");
                        txt_discountvalue.ReadOnly = false;
                        txt_discountPC.ReadOnly = false;
                        txt_totalpaid.ReadOnly = false;
                        txt_ipnos.ReadOnly = false;
                        txt_ipnos.Text = "";
                        txt_totalpaid.Text = "";
                        txtdiscremoarks.Text = "";
                        btnsave.Attributes["disabled"] = "disabled";
                        ddl_responsible.Attributes.Remove("disabled");
                        txt_totalpaid.ReadOnly = false;
                        txtdiscremoarks.ReadOnly = false;
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                lbl_mesaage3.Attributes["class"] = "FailAlert";
                div3.Visible = true;
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
                List<PHRIPFinalData> objdeposit = GetIPbillList(0);
                if (objdeposit.Count > 0)
                {
                    Gvipbilllist.DataSource = objdeposit;
                    Gvipbilllist.DataBind();
                    Gvipbilllist.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    txttotalbillamount.Text = Commonfunction.Getrounding(objdeposit[0].TotalBill.ToString());
                    txtajusted.Text = Commonfunction.Getrounding(objdeposit[0].TotalAdjustedAmount.ToString());
                    txttotaldiscounted.Text = Commonfunction.Getrounding(objdeposit[0].TotalDiscount.ToString());
                    txttotalpaid.Text = Commonfunction.Getrounding(objdeposit[0].TotalPaidAmount.ToString());
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
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
                    txttotalbillamount.Text = "0.00";
                    txtajusted.Text = "0.00";
                    txttotaldiscounted.Text = "0.00";
                    txttotalpaid.Text = "0.00";
                    Gvipbilllist.DataSource = null;
                    Gvipbilllist.DataBind();
                    Gvipbilllist.Visible = true;
                    ddlexport.Visible = false;
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
        public List<PHRIPFinalData> GetIPbillList(int curIndex)
        {
            PHRIPFinalData objbill = new PHRIPFinalData();
            EmrgAdmissionBO objbillingBO = new EmrgAdmissionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objbill.IPNo = txt_ipnolist.Text.Trim() == "" ? null : txt_ipnolist.Text.Trim();
            objbill.PatientName = txtpatientNames.Text == "" ? null : txtpatientNames.Text.Trim();
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
            objbill.AmountEnable = LogData.AmountEnable;
            return objbillingBO.GetPharamacyIPFinalBillList(objbill);
        }
        protected void Gvipbilllist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblresult, "DeleteEnable", 0);
                        divmsg3.Visible = true;
                        divmsg3.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblresult.Visible = false;
                    }
                    PHRIPFinalData objadmin = new PHRIPFinalData();
                    EmrgAdmissionBO obadminBO = new EmrgAdmissionBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = Gvipbilllist.Rows[i];
                    Label Bill = (Label)gr.Cells[0].FindControl("lbl_billno");
                    Label ipnos = (Label)gr.Cells[0].FindControl("lbl_ipno");
                    Label UHID = (Label)gr.Cells[0].FindControl("lbluhid");
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
                        lblresult.Visible = false;
                        objadmin.Remarks = txtremarks.Text;
                    }
                    objadmin.IPNo = ipnos.Text.Trim();
                    objadmin.BillNo = Bill.Text.Trim();
                    objadmin.UHID = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                    objadmin.EmployeeID = LogData.EmployeeID;
                    objadmin.HospitalID = LogData.HospitalID;
                    objadmin.IPaddress = LogData.IPaddress;
                    int Result = obadminBO.DeletePHRipfinalbill(objadmin);
                    if (Result == 1)
                    {
                        bindgridList();
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        divmsg2.Attributes["class"] = "SucessAlert";
                        divmsg2.Visible = true;
                    }
                    else
                    {
                        if (Result == 2)
                        {
                            Messagealert_.ShowMessage(lblmessage2, "AccountClosed", 0);
                            divmsg2.Visible = true;
                            divmsg2.Attributes["class"] = "FailAlert";
                            // bindgrid();
                        }
                        else
                        {
                            Messagealert_.ShowMessage(lblmessage2, "system", 0);
                            divmsg2.Attributes["class"] = "FailAlert";
                            divmsg2.Visible = true;
                        }
                    }

                }
                if (e.CommandName == "Print")
                {
                    if (LogData.PrintEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "PrintEnable", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage2.Visible = false;
                    }
                    int j = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gp = Gvipbilllist.Rows[j];
                    Label billno = (Label)gp.Cells[0].FindControl("lbl_billno");
                    string url = "../MedEmergency/Reports/ReportViewer.aspx?option=FinalBill&BillNo=" + billno.Text.ToString();
                    string fullURL = "window.open('" + url + "', '_blank');";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Visible = true;
            }
        }
        protected void Gvipbilllist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gvipbilllist.PageIndex = e.NewPageIndex;
            bindgridList();              
        }
        protected void btn_refund_Click(object sender, EventArgs e)
        {
            List<RefundData> Listbill = new List<RefundData>();
            RefundData objrefund = new RefundData();
            RefundBO RefundBO = new RefundBO();
            // int index = 0;
            try
            {

                objrefund.FinancialYearID = LogData.FinancialYearID;
                objrefund.EmployeeID = LogData.EmployeeID;
                objrefund.HospitalID = LogData.HospitalID;
                objrefund.RefundAmount = Convert.ToDecimal(txt_refundable.Text == "" ? "0" : txt_refundable.Text);
                objrefund.UHID = Convert.ToInt64(lbl_UHIDTemp.Text == "" ? "0" : lbl_UHIDTemp.Text);
                objrefund.BillNo = txt_billnumber.Text == "" ? "0" : txt_billnumber.Text;
                objrefund.Paymode = 0;
                objrefund.BankName = "";
                Listbill = RefundBO.UpdatePhrRefundDetails(objrefund);
                if (Listbill.Count > 0)
                {
                    btn_refund.Attributes["disabled"] = "disabled";
                    Messagealert_.ShowMessage(lblmessage, "Refund", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    btnprint.Attributes.Remove("disabled");
                }
                else
                {
                    btn_refund.Attributes.Remove("disabled");
                    btnprint.Attributes.Remove("disabled");
                    lbl_bill.InnerText = "Bill Number";
                    txt_billnumber.Text = "";
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
        protected void btnreset_Click(object sender, EventArgs e)
        {
            lbl_bill.InnerText = "Bill Number";
            Gvitemlist.DataSource = null;
            Gvitemlist.DataBind();
            Gvitemlist.Visible = false;
            lblmessage.Visible = false;
            txt_ipnos.Text = "";
            txtname.Text = "";
            txt_address.Text = "";
            btnsave.Attributes["disabled"] = "disabled";
            btnprint.Attributes["disabled"] = "disabled";
            txt_careof.Text = "";
            txt_chequenumber.Text = "";
            txtinvoicenumber.Text = "";
            txtbank.ReadOnly = true;
            txt_chequenumber.ReadOnly = true;
            txtinvoicenumber.ReadOnly = true;
            txt_Age.Text = "";
            txt_department.Text = "";
            txt_doctor.Text = "";
            txt_billnumber.Text = "";
            txtbank.Text = "";
            txttotalamount.Text = "";
            txtbank.ReadOnly = true;
            lblmessage.Visible = false;
            txt_admissionDate.Text = "";
            txttotalamount.Text = "";
            txtbalanceinac.Text = "";
            txtadjustedamount.Text = "";
            ddlpaymentmode.SelectedIndex = 0;
            txt_payableamount.Text = "";
            txt_discountPC.Text = "";
            txt_discountvalue.Text = "";
            btn_refund.Attributes["disabled"] = "disabled";
            hdnreqno.Value = "0";
            hdnbillnumber.Value = null;
            txt_ipnos.ReadOnly = false;
            txt_refundable.Text = "";
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txt_ipnolist.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            ddlstatus.SelectedIndex = 0;
            Gvipbilllist.DataSource = null;
            Gvipbilllist.DataBind();
            Gvipbilllist.Visible = false;
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
            txttotalbillamount.Text = "0.00";
            txtajusted.Text = "0.00";
            txttotaldiscounted.Text = "0.00";
            txttotalpaid.Text = "0.00";
            ddlcollectedby.SelectedIndex = 0;
            btnprints.Attributes["disabled"] = "disabled";
            hdnbillsubmittype.Value = "1";
        }
        protected void btnreserREQ_Click(object sender, EventArgs e)
        {
            txt_reqno.Text = "";
            txt_reqdatefrom.Text = "";
            txt_reqdateto.Text = "";
            GvDiscountRequest.DataSource = null;
            GvDiscountRequest.DataBind();
            GvDiscountRequest.Visible = false;
            lbl_result3.Visible = false;
            lblmessage.Visible = false;
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
                if (ddlpaymentmode.SelectedIndex == 0 && hdnbillsubmittype.Value != "2")
                {
                    Messagealert_.ShowMessage(lblmessage, "Paymode", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (Convert.ToDecimal(txt_dueamount.Text.Trim() == "" ? "0" : txt_dueamount.Text.Trim()) > 0)
                {
                    if (ddl_responsible.SelectedIndex == 0)
                    {
                        ddl_responsible.Attributes.Remove("disabled");
                        Messagealert_.ShowMessage(lblmessage, "Dueresponsible", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        div1.Visible = false;
                    }
                }
                if (Convert.ToDecimal(txt_discountvalue.Text.Trim() == "" ? "0" : txt_discountvalue.Text) > 0 || Convert.ToDecimal(txt_dueamount.Text.Trim() == "" ? "0" : txt_dueamount.Text.Trim()) > 0)
                {
                    if (txtdiscremoarks.Text.Trim() == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "Remarks", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txtdiscremoarks.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        div1.Visible = false;
                    }
                }
                PHRIPFinalData objData = new PHRIPFinalData();
                EmrgAdmissionBO ObjBO = new EmrgAdmissionBO();
                List<PHRIPFinalData> getResult = new List<PHRIPFinalData>();

                objData.IPNo = txt_ipnos.Text.Trim();
                objData.UHID = Convert.ToInt64(lbl_UHIDTemp.Text == "" ? "0" : lbl_UHIDTemp.Text);
                objData.TotalBillAmount = Convert.ToDecimal(txttotalamount.Text == "" ? "0.0" : txttotalamount.Text);
                objData.TotalDiscount = Convert.ToDecimal(txt_discountvalue.Text == "" ? "0.0" : txt_discountvalue.Text);
                objData.AdjustedAmount = Convert.ToDecimal(txtadjustedamount.Text == "" ? "0.0" : txtadjustedamount.Text);
                objData.TotalPaidAmount = Convert.ToDecimal(txt_totalpaid.Text == "" ? "0.0" : txt_totalpaid.Text);
                objData.TotalPayableAmount = Convert.ToDecimal(txt_payableamount.Text == "" ? "0.0" : txt_payableamount.Text);
                objData.TotalDuemanount = Convert.ToDecimal(txt_dueamount.Text == "" ? "0.0" : txt_dueamount.Text);
                objData.Remarks = txtdiscremoarks.Text.Trim();
                objData.Paymode = Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue);
                objData.BankName = txtbank.Text == "" ? null : txtbank.Text;
                objData.Chequenumber = txt_chequenumber.Text == "" ? "" : txt_chequenumber.Text;
                objData.Invoicenumber = txtinvoicenumber.Text == "" ? "" : txtinvoicenumber.Text;
                objData.BankID = Convert.ToInt32(hdnbankID.Value == "" || hdnbankID.Value == null ? "0" : hdnbankID.Value);
                if (hdnbillsubmittype.Value == "0" || hdnbillsubmittype.Value == "1" || hdnbillsubmittype.Value == "3")
                {
                    objData.SubmitType = 1;
                }
                if (hdnbillsubmittype.Value == "2")
                {
                    objData.SubmitType = 2;
                }
                objData.ResponsiblePerson = Convert.ToInt64(ddl_responsible.SelectedValue == "" ? "0" : ddl_responsible.SelectedValue);
                objData.ReqNo = hdnreqno.Value;
                objData.PatientName = txtname.Text.Trim();
                objData.FinancialYearID = LogData.FinancialYearID;
                objData.EmployeeID = LogData.EmployeeID;
                objData.AddedBy = LogData.AddedBy;
                objData.HospitalID = LogData.HospitalID;
                objData.IsActive = LogData.IsActive;
                objData.IPaddress = LogData.IPaddress;

                List<PHRIPFinalData> result = ObjBO.Update_PHR_IPFinal_BillDetails(objData);
                if (result.Count > 0)
                {
                    if (result[0].BillNo.ToString() != "")
                    {
                        hdnbillnumber.Value = result[0].BillNo.ToString();
                        txt_billnumber.Text = result[0].BillNo.ToString();
                        Messagealert_.ShowMessage(lblmessage, "save", 1);
                        div1.Attributes["class"] = "SucessAlert";
                        div1.Visible = true;
                    }
                    if (result[0].ReqNo.ToString() != "")
                    {
                        txt_billnumber.Text = result[0].ReqNo.ToString();
                        Messagealert_.ShowMessage(lblmessage, "Reqsent", 1);
                        div1.Attributes["class"] = "SucessAlert";
                        div1.Visible = true;
                    }
                    btnsave.Attributes["disabled"] = "disabled";
                    if (hdnbillsubmittype.Value == "0" || hdnbillsubmittype.Value == "1" || hdnbillsubmittype.Value == "3")
                    {
                        if (Convert.ToDecimal(txt_refundable.Text == "" ? "0.0" : txt_refundable.Text) > 0)
                        {
                            btn_refund.Attributes.Remove("disabled");
                            btnprint.Attributes["disabled"] = "disabled";
                        }
                        else
                        {
                            btn_refund.Attributes["disabled"] = "disabled";
                            btnprint.Attributes.Remove("disabled");
                        }
                    }
                    else
                    {
                        btn_refund.Attributes["disabled"] = "disabled";
                    }
                }
                else
                {
                    hdnbillnumber.Value = null;
                    lbl_bill.InnerText = "Bill Number";
                    txt_billnumber.Text = "";
                    btnprint.Attributes["disabled"] = "disabled";
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;
                return;
            }
        }

        protected void GvDiscountRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvDiscountRequest.PageIndex = e.NewPageIndex;
            bindrequestlist();
        }  
      
    }
}