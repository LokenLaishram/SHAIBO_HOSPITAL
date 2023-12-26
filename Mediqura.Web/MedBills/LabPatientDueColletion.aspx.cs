using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedBillData;
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

namespace Mediqura.Web.MedBills
{
    public partial class LabPatientDueColletion : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtuhid.Attributes["disabled"] = "disabled";
                txt_PatientName.Attributes["disabled"] = "disabled";
                txtbillno.Attributes["disabled"] = "disabled";
                txtbilldate.Attributes["disabled"] = "disabled";
                txtResponsibleBy.Attributes["disabled"] = "disabled";
                txtbillamount.Attributes["disabled"] = "disabled";
                txtdiscount.Attributes["disabled"] = "disabled";
                txtpaidamount.Attributes["disabled"] = "disabled";
                txtDueAmount.Attributes["disabled"] = "disabled";
                txt_LastDuePaid.Attributes["disabled"] = "disabled";
                txt_DueBal.Attributes["disabled"] = "disabled";
                txtdue.Attributes["disabled"] = "disabled";
                txtDisRemark.Attributes["disabled"] = "disabled";
                txtDiscountAmount.Attributes["disabled"] = "disabled";
                txtDueColNo.Attributes["disabled"] = "disabled";
                btn_save.Attributes["disabled"] = "disabled";
                bindddl();
            }
        }
        private void bindddl()
        {
            int CurMonth = Convert.ToInt32(DateTime.Now.Month);
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlPatientType, mstlookup.GetLookupsList(LookupName.CustomerType));
            Commonfunction.PopulateDdl(ddlResponsibleBy, mstlookup.GetLookupsList(LookupName.Employee));
            //----TAB2------//
            Commonfunction.PopulateDdl(ddlpaymentmode, mstlookup.GetLookupsList(LookupName.PaymentMode));
            ddlpaymentmode.SelectedIndex = 1;
            //----TAB3------//
            Commonfunction.PopulateDdl(ddl_PatientType, mstlookup.GetLookupsList(LookupName.CustomerType));
            Commonfunction.PopulateDdl(ddl_DueResponsibleBy, mstlookup.GetLookupsList(LookupName.Employee));
            txtfrom.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Today.ToString("dd/MM/yyyy");

            txt_DateFrom.Text= System.DateTime.Today.ToString("dd/MM/yyyy");
            txt_DateTo.Text = System.DateTime.Today.ToString("dd/MM/yyyy");

        }
        protected void btnsearchs_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void bindgrid()
        {
            try
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
              
                if (txtfrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txtfrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDatefrom", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtfrom.Focus();
                        return;
                    }
                    if (Commonfunction.ChecklowerDate(txtfrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDatefrom", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtfrom.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }

                if (txtto.Text != "")
                {
                    if (Commonfunction.isValidDate(txtto.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDateto", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtto.Focus();
                        return;
                    }
                    if (Commonfunction.ChecklowerDate(txtto.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDateto", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtto.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }
                List<LabDueCollectionData> objresult = GetDueCustomerList(0);
                if (objresult.Count > 0)
                {
                    Messagealert_.ShowMessage(lblresult, "Total:" + objresult[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    txtTotalBill.Text = objresult[0].SumTotalBill.ToString("N");
                    txtTotalDiscount.Text = objresult[0].SumDiscount.ToString("N");
                    txtTotalPaid.Text = objresult[0].SumPaidAmount.ToString("N");
                    txtTotalDue.Text = objresult[0].SumDueAmount.ToString("N");
                    txtLastDuePaid.Text = objresult[0].SumLastDuePaid.ToString("N");
                    txtTotalDueBal.Text = objresult[0].SumDueBalance.ToString("N");


                    divmsg1.Attributes["class"] = "SucessAlert";
                    gvDueCustomer.DataSource = objresult;
                    gvDueCustomer.DataBind();
                    gvDueCustomer.Visible = true;
                }
                else
                {
                    gvDueCustomer.DataSource = null;
                    gvDueCustomer.DataBind();
                    gvDueCustomer.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }
        private List<LabDueCollectionData> GetDueCustomerList(int p)
        {
            LabDueCollectionData obdue = new LabDueCollectionData();
            LabDueCollectionBO objBO = new LabDueCollectionBO();

            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtfrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtfrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? GlobalConstant.MaxdateAddOneYear : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            obdue.DateFrom = from;
            obdue.DateTo = To;
            obdue.PatientTypeID = Convert.ToInt32(ddlPatientType.SelectedValue == "" ? "" : ddlPatientType.SelectedValue);
            obdue.DueReponsibleBy = Convert.ToInt32(ddlResponsibleBy.SelectedValue == "" ? "" : ddlResponsibleBy.SelectedValue);
            return objBO.GetPhrCustomerDueList(obdue);

        }

        protected void btnclear_Click(object sender, System.EventArgs e)
        {
            gvDueCustomer.DataSource = null;
            gvDueCustomer.DataBind();
            gvDueCustomer.Visible = false;
            txtfrom.Text = "";
            txtto.Text = "";
            txtTotalBill.Text = "";
            txtTotalDiscount.Text = "";
            txtTotalPaid.Text = "";
            txtTotalDue.Text = "";
            txtLastDuePaid.Text = "";
            txtTotalDueBal.Text = "";
            ddlResponsibleBy.SelectedIndex = 0;
            divmsg1.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            divmsg1.Visible = false;
            ViewState["ID"] = null;
            Clear();

        }

        protected void gvDueCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {

                    LabDueCollectionData objcondemn = new LabDueCollectionData();
                    LabDueCollectionBO objstdBO = new LabDueCollectionBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvDueCustomer.Rows[i];
                    Label PatientType = (Label)gr.Cells[0].FindControl("lblPatientType");
                    Label billno = (Label)gr.Cells[0].FindControl("lblbillno");
                    hdnBillNo.Value = billno.Text.Trim();
                    BindBillDetails(billno.Text, PatientType.Text);

                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void BindBillDetails(string BillNo, string Ptype)
        {
            LabDueCollectionData objbill = new LabDueCollectionData();
            LabDueCollectionBO objstdBO = new LabDueCollectionBO();
            objbill.BillNo = BillNo.Trim();
            objbill.PatientTypeID = Convert.ToInt32(Ptype);
            List<LabDueCollectionData> List = new List<LabDueCollectionData>();
            List = objstdBO.GetPhrBillDetails(objbill);
            if (List.Count > 0)
            {
                Clear();
                tabDueCollection.ActiveTabIndex = 1;
                txtuhid.Text = List[0].UHID.ToString();
                hdnUHID2.Value = List[0].UHID.ToString();
                hdnpatienttypeID.Value = List[0].PatientTypeID.ToString();
                txt_PatientName.Text = List[0].CustomerName.ToString();
                hdnResponsibleID.Value = List[0].DueReponsibleBy.ToString();
                txtResponsibleBy.Text = List[0].DueReponsibleName.ToString();
                txtbillno.Text = List[0].BillNo.ToString();
                txtbillamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(List[0].TotalBillAmount.ToString())).ToString());
                txtdiscount.Text = Commonfunction.Getrounding((Convert.ToDecimal(List[0].Discount.ToString())).ToString());
                txtpaidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(List[0].PaidAmount.ToString())).ToString());
                txtDueAmount.Text = Commonfunction.Getrounding((Convert.ToDecimal(List[0].DueAmount.ToString())).ToString());
                txt_LastDuePaid.Text = Commonfunction.Getrounding((Convert.ToDecimal(List[0].LastDuePaid.ToString())).ToString());
                txt_DueBal.Text = Commonfunction.Getrounding((Convert.ToDecimal(List[0].DueBalance.ToString())).ToString());
                txt_DueBalance.Text = Commonfunction.Getrounding((Convert.ToDecimal(List[0].DueBalance.ToString())).ToString());
                txt_Paid.Text = Commonfunction.Getrounding((Convert.ToDecimal(List[0].DueBalance.ToString())).ToString());
                txtdue.Text = "0";
                txtDiscountAmount.Text = "0";
                btn_save.Attributes.Remove("disabled");
                txt_Paid.Focus();
            }
            else
            {
                tabDueCollection.ActiveTabIndex = 1;
                gvDueCustomer.DataSource = null;
                gvDueCustomer.DataBind();
                gvDueCustomer.Visible = true;
            }

        }
        protected void btn_Reset_Click(object sender, System.EventArgs e)
        {
            Clear();
        }
        protected void Clear()
        {
            txtuhid.Text = "";
            hdnUHID2.Value = "";
            txt_PatientName.Text = "";
            txtbillno.Text = "";
            txtdiscount.Text = "";
            txtpaidamount.Text = "";
            txtDueAmount.Text = "";
            txt_LastDuePaid.Text = "";
            txt_DueBal.Text = "";
            hdnResponsibleID.Value = "";
            txtResponsibleBy.Text = "";
            txt_Paid.Text = "";
            txtDisRemark.Text = "";
            chkduediscount.Text = "";
            txtdue.Text = "";
            txtDueColNo.Text = "";
            ddlpaymentmode.SelectedIndex = 0;
            hdnbankID.Value = "";
            txtbank.Text = "";
            txt_chequenumber.Text = "";
            txt_DueBalance.Text = "";
            txtinvoicenumber.Text = "";
            btn_save.Attributes["disabled"] = "disabled";
            tabDueCollection.ActiveTabIndex = 0;
            lblmessage2.Text = "";
            divmsg2.Visible = false;
            txtDiscountAmount.Text = "";
            chkduediscount.Checked = false;
            txtbillamount.Text = "";
            txtDisRemark.Attributes["disabled"] = "disabled";
        }

        protected void Paid_OnTextChanged(object sender, System.EventArgs e)
        {
            if (Convert.ToDecimal(txtdue.Text == "" ? "0" : txtdue.Text) > 0)
            {
                //chkduediscount.Attributes.Remove("disabled");
            }
            else
            {
                //chkduediscount.Attributes["disabled"] = "disabled";
            }
        }
        protected void OnCheckDiscount(object sender, System.EventArgs e)
        {
            if (chkduediscount.Checked)
            {
                if (Convert.ToDecimal(txt_Paid.Text == "" ? "0" : txt_Paid.Text) > Convert.ToDecimal(txt_DueBal.Text == "" ? "0" : txt_DueBal.Text))
                {
                    Messagealert_.ShowMessage(lblmessage2, "payment", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {

                    txtDiscountAmount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_DueBal.Text == "" ? "0" : txt_DueBal.Text) - Convert.ToDecimal(txt_Paid.Text == "" ? "0" : txt_Paid.Text)).ToString());
                    txtdue.Text = "0";
                    txtDisRemark.Attributes.Remove("disabled");
                    txtDisRemark.Focus();
                }
            }
            else
            {
                txtdue.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_DueBal.Text == "" ? "0" : txt_DueBal.Text) - Convert.ToDecimal(txt_Paid.Text == "" ? "0" : txt_Paid.Text)).ToString());
                txtDiscountAmount.Text = "0";
                txtDisRemark.Attributes["disabled"] = "disabled";
            }
        }

        protected void ddlpaymentmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlpaymentmode.SelectedIndex > 0)
            {
                lblmessage2.Visible = false;
                divmsg2.Visible = false;

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
                    txt_chequenumber.ReadOnly = false;
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
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                LabDueCollectionData ObjDue = new LabDueCollectionData();
                LabDueCollectionBO objBO = new LabDueCollectionBO();

                if (LogData.SaveEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "SaveEnable", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }

                if (txt_PatientName.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "Custommer", 0);
                    divmsg2.Attributes["class"] = "FailAlert";
                    divmsg2.Visible = true;
                    txt_PatientName.Focus();
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }

                if (txtbillno.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "Billno", 0);
                    divmsg2.Attributes["class"] = "FailAlert";
                    divmsg2.Visible = true;
                    txtbillno.Focus();
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (txt_Paid.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "Paid", 0);
                    divmsg2.Attributes["class"] = "FailAlert";
                    divmsg2.Visible = true;
                    txt_Paid.Focus();
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (Convert.ToDecimal(txt_Paid.Text == "" ? "0" : txt_Paid.Text) > Convert.ToDecimal(txt_DueBal.Text == "" ? "0" : txt_DueBal.Text))
                {
                    Messagealert_.ShowMessage(lblmessage2, "payment", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {

                    lblmessage2.Visible = false;
                    txt_Paid.Focus();
                }

                if (Convert.ToDecimal(txtDiscountAmount.Text == "" ? "0" : txtDiscountAmount.Text) > 0)
                {
                    if (txtDisRemark.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage2, "Remarks", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txtDisRemark.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage2.Visible = false;
                    }
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (ddlpaymentmode.SelectedIndex == 0 && txt_Paid.Text!="0")
                {
                    Messagealert_.ShowMessage(lblmessage2, "Paymode", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                ObjDue.PatientTypeID = Convert.ToInt32(hdnpatienttypeID.Value == "" ? "0" : hdnpatienttypeID.Value);
                ObjDue.UHID = Convert.ToInt32(hdnUHID2.Value == "" ? "0" : hdnUHID2.Value);
                ObjDue.IPNo = hdnIPNO2.Value == "" ? "" : hdnIPNO2.Value;
                ObjDue.BillNo = txtbillno.Text == "" ? "" : txtbillno.Text;
                ObjDue.CustomerName = txt_PatientName.Text == "" ? "" : txt_PatientName.Text;
                ObjDue.DueReponsibleBy = Convert.ToInt32(hdnResponsibleID.Value == "" ? "0" : hdnResponsibleID.Value);
                ObjDue.DueReponsibleName = txtResponsibleBy.Text == "" ? "" : txtResponsibleBy.Text;
                ObjDue.TotalBillAmount = Convert.ToDecimal(txtbillamount.Text == "" ? "0" : txtbillamount.Text);
                ObjDue.Discount = Convert.ToDecimal(txtdiscount.Text == "" ? "0" : txtdiscount.Text);
                ObjDue.PaidAmount = Convert.ToDecimal(txtpaidamount.Text == "" ? "0" : txtpaidamount.Text);
                ObjDue.DueAmount = Convert.ToDecimal(txtDueAmount.Text == "" ? "0" : txtDueAmount.Text);
                ObjDue.LastDuePaid = Convert.ToDecimal(txt_LastDuePaid.Text == "" ? "0" : txt_LastDuePaid.Text);
                ObjDue.DueBalance = Convert.ToDecimal(txt_DueBal.Text == "" ? "0" : txt_DueBal.Text);
                ObjDue.Paid = Convert.ToDecimal(txt_Paid.Text == "" ? "0" : txt_Paid.Text);
                ObjDue.Due = Convert.ToDecimal(txtdue.Text == "" ? "0" : txtdue.Text);
                ObjDue.DueDiscount = Convert.ToDecimal(txtDiscountAmount.Text == "" ? "0" : txtDiscountAmount.Text);
                ObjDue.DiscountRemark = txtDisRemark.Text == "" ? "" : txtDisRemark.Text;
                ObjDue.PaymentModeID = Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue);
                ObjDue.BankID = Convert.ToInt32(hdnbankID.Value == "" || hdnbankID.Value == null ? "0" : hdnbankID.Value);
                ObjDue.BankName = txtbank.Text == "" ? null : txtbank.Text;
                ObjDue.Cheque = txt_chequenumber.Text == "" ? "" : txt_chequenumber.Text;
                ObjDue.InvoiceNo = txtinvoicenumber.Text == "" ? "" : txtinvoicenumber.Text;

                ObjDue.EmployeeID = LogData.EmployeeID;
                ObjDue.HospitalID = LogData.HospitalID;
                ObjDue.FinancialYearID = LogData.FinancialYearID;


                List<LabDueCollectionData> results = objBO.UpdatePhrDueCollection(ObjDue);
                if (results[0].DueCollNo != "")
                {

                    Messagealert_.ShowMessage(lblmessage2, "save", 1);
                    txtDueColNo.Text = results[0].DueCollNo.ToString();
                    divmsg2.Attributes["class"] = "SucessAlert";
                    divmsg2.Visible = true;
                    btn_save.Attributes["disabled"] = "disabled";

                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage2, "system", 0);
                    lblmessage2.Attributes["class"] = "FailAlert";
                    divmsg2.Visible = true;
                }


            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                string msg = ex.ToString();
                Messagealert_.ShowMessage(lblmessage, msg, 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                bindgrid();
            }
        }
        //----TAB3-----//
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetDuePaidCustomer(string prefixText, int count, string contextKey)
        {
            LabDueCollectionData Objdc = new LabDueCollectionData();
            LabDueCollectionBO objmedBO = new LabDueCollectionBO();
            List<LabDueCollectionData> getResult = new List<LabDueCollectionData>();
            Objdc.CustomerName = prefixText;
            getResult = objmedBO.GetDuePaidCustomer(Objdc);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].CustomerName.ToString());
            }
            return list;
        }

        protected void btn_Searchs3_Click(object sender, EventArgs e)
        {
            Tab3bindgrid();
        }
        protected void Tab3bindgrid()
        {
            try
            {
                if (LogData.SearchEnable == 0)
                {
                    Messagealert_.ShowMessage(lbl_message3, "SearchEnable", 0);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lbl_message3.Visible = false;
                }
               
                if (txt_DateFrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_DateFrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lbl_message3, "ValidDatefrom", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                        txt_DateFrom.Focus();
                        return;
                    }
                    if (Commonfunction.ChecklowerDate(txt_DateFrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lbl_message3, "ValidDatefrom", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                        txt_DateFrom.Focus();
                        return;
                    }
                }
                else
                {
                    lbl_message3.Visible = false;
                }

                if (txt_DateTo.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_DateTo.Text) == false)
                    {
                        Messagealert_.ShowMessage(lbl_message3, "ValidDateto", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                        txt_DateTo.Focus();
                        return;
                    }
                    if (Commonfunction.ChecklowerDate(txt_DateTo.Text) == false)
                    {
                        Messagealert_.ShowMessage(lbl_message3, "ValidDateto", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                        txt_DateTo.Focus();
                        return;
                    }
                }
                else
                {
                    lbl_message3.Visible = false;
                }
                List<LabDueCollectionData> objresults = GetDueCollectionList(0);
                if (objresults.Count > 0)
                {
                    Messagealert_.ShowMessage(lbl_Results, "Total:" + objresults[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    //txtTotalBill.Text = objresults[0].SumTotalBill.ToString("N");
                    //txtTotalDiscount.Text = objresults[0].SumDiscount.ToString("N");
                    //txtTotalPaid.Text = objresults[0].SumPaidAmount.ToString("N");
                    //txtTotalDue.Text = objresults[0].SumDueAmount.ToString("N");
                    //txtLastDuePaid.Text = objresults[0].SumLastDuePaid.ToString("N");
                    //txtTotalDueBal.Text = objresults[0].SumDueBalance.ToString("N");

                    gvDueCollectionList.DataSource = objresults;
                    gvDueCollectionList.DataBind();
                    gvDueCollectionList.Visible = true;
                    divmsg4.Visible = true;
                    divmsg3.Visible = false;
                }
                else
                {
                    gvDueCollectionList.DataSource = null;
                    gvDueCollectionList.DataBind();
                    gvDueCollectionList.Visible = true;
                    divmsg4.Visible = false;
                    divmsg3.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lbl_message3, "system", 0);
                divmsg3.Visible = true;
            }
        }
        private List<LabDueCollectionData> GetDueCollectionList(int p)
        {
            LabDueCollectionData obdue = new LabDueCollectionData();
            LabDueCollectionBO objBO = new LabDueCollectionBO();

            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_DateFrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_DateFrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txt_DateTo.Text.Trim() == "" ? GlobalConstant.MaxdateAddOneYear : DateTime.Parse(txt_DateTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            obdue.DateFrom = from;
            obdue.DateTo = To;
            string CusD;
            var source = txtcustomerDetails.Text.ToString();
            if (source.Contains(":"))
            {
                CusD = source.Substring(source.LastIndexOf(':') + 1);
                obdue.BillNo = CusD.Trim();
            }
            else
            {
                obdue.BillNo = "";
            }
            obdue.PatientTypeID = Convert.ToInt32(ddl_PatientType.SelectedValue == "" ? "" : ddl_PatientType.SelectedValue);
            obdue.DueReponsibleBy = Convert.ToInt32(ddl_DueResponsibleBy.SelectedValue == "" ? "" : ddl_DueResponsibleBy.SelectedValue);
            obdue.IsActive = ddl_status.SelectedValue == "0" ? true : false;
            return objBO.GetPhrDueCollectionList(obdue);

        }
        protected void gvDueCollection_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.RoleID == 1 || LogData.RoleID == 40)
                    {
                        if (LogData.DeleteEnable == 0)
                        {
                            Messagealert_.ShowMessage(lbl_message3, "DeleteEnable", 0);
                            divmsg3.Visible = true;
                            divmsg3.Attributes["class"] = "FailAlert";
                            return;
                        }
                        else
                        {
                            lbl_message3.Visible = false;
                        }

                        LabDueCollectionData objData = new LabDueCollectionData();
                        LabDueCollectionBO objDrgBO = new LabDueCollectionBO();
                        int i = Convert.ToInt16(e.CommandArgument.ToString());
                        GridViewRow gr = gvDueCollectionList.Rows[i];
                        Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                        Label PatientType = (Label)gr.Cells[0].FindControl("lbl_PatientType");
                        Label lbl_RecieptNo = (Label)gr.Cells[0].FindControl("lbl_RecieptNo");
                        Label billno3 = (Label)gr.Cells[0].FindControl("lbl_billno3");
                        TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                        objData.ID = Convert.ToInt32(ID.Text);
                        objData.PatientTypeID = Convert.ToInt32(PatientType.Text);
                        objData.RecieptNo = lbl_RecieptNo.Text.Trim();
                        objData.BillNo = billno3.Text.Trim();
                        objData.EmployeeID = LogData.EmployeeID;
                        objData.ActionType = Enumaction.Delete;
                        if (txtremarks.Text == "")
                        {
                            Messagealert_.ShowMessage(lbl_message3, "Remarks", 0);
                            divmsg3.Visible = true;
                            divmsg3.Attributes["class"] = "FailAlert";
                            txtremarks.Focus();
                            return;
                        }
                        else
                        {
                            objData.Remarks = txtremarks.Text;
                        }
                        int Result = objDrgBO.Delete_Phr_DueCollectionRecordByID(objData);
                        if (Result == 1)
                        {
                            Messagealert_.ShowMessage(lbl_message3, "delete", 1);
                            divmsg3.Visible = true;
                            divmsg3.Attributes["class"] = "SucessAlert";
                            Tab3bindgrid();
                        }
                        else
                        {
                            Messagealert_.ShowMessage(lbl_message3, "system", 0);
                            divmsg3.Visible = true;
                            divmsg3.Attributes["class"] = "FailAlert";

                        }
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lbl_message3, "DeleteEnable", 0);
                        divmsg3.Visible = true;
                        divmsg3.Attributes["class"] = "FailAlert";
                        return;
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void btn_Tab3Clear_Click(object sender, EventArgs e)
        {
            Tab3Clear();
        }
        protected void Tab3Clear()
        {
            txtcustomerDetails.Text = "";
            ddl_PatientType.SelectedIndex = 0;
            ddl_DueResponsibleBy.SelectedIndex = 0;
            ddl_status.SelectedIndex = 0;
            gvDueCollectionList.DataSource = null;
            gvDueCollectionList.DataBind();
            gvDueCollectionList.Visible = true;
            lbl_Results.Visible = false;

        }
    }
}