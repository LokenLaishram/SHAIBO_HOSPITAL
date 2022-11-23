using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedBillData;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.DAL.MedBillDA;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedBills
{
    public partial class DoctorPayment : BasePage
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
            //Commonfunction.PopulateDdl(ddl_paiddoctr, mstlookup.GetLookupsList(LookupName.PaidDoctor));
            txt_totalamount.Text = "";
            txt_discountamount.Text = "";
            txt_paidamount.Text = "";
            txt_paidamount.Text = "";
            txt_paidamount.Attributes["disabled"] = "disabled";
            btn_pay.Attributes["disabled"] = "disabled";
            btn_printrecv.Attributes["disabled"] = "disabled";
            //txtdatefrom.Attributes["disabled"] = "disabled";
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();

        }
        protected void ddl_PC_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void ddl_doctortype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_doctortype.SelectedIndex > 0)
            {
                txt_referal.Text = "";
                AutoCompleteExtender5.ContextKey = ddl_doctortype.SelectedValue == "" ? "0" : ddl_doctortype.SelectedValue;
                txt_referal.Focus();
            }
        }
        protected void ddl2doctortype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl2DoctorType.SelectedIndex > 0)
            {
                txt2Referal.Text = "";
                AutoCompleteExtender1.ContextKey = ddl2DoctorType.SelectedValue == "" ? "0" : ddl2DoctorType.SelectedValue;
                txt2Referal.Focus();
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> Getautoreferals(string prefixText, int count, string contextKey)
        {
            ReferalData objreferal = new ReferalData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<ReferalData> getResult = new List<ReferalData>();
            objreferal.Referal = prefixText;
            objreferal.ID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetPayableReferalDetails(objreferal);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].Referal.ToString());
            }
            return list;
        }
        protected void ddl_servicecategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Gv_Collectionlist.DataSource = null;
            Gv_Collectionlist.DataBind();
            lblresult.Visible = false;
            Gv_Collectionlist.Visible = false;
            txt_totalamount.Text = "";
            txt_discountamount.Text = "";
            txt_paidamount.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            txt_paymentnumber.Text = "";
            btn_pay.Attributes["disabled"] = "disabled";

            if (Commonfunction.SemicolonSeparation_String_64(txt_referal.Text) == 0)
            {
                txt_referal.Text = "";
                txt_referal.Attributes.Remove("disabled");
                Messagealert_.ShowMessage(lblmessage, "Referal", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_referal.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }

            if (ddl_servicecategory.SelectedIndex > 0)
            {
                txtdatefrom.Text = "";
                txtto.Text = "";
                DoctorPayoutData objData = new DoctorPayoutData();
                OPDbillingBO objBO = new OPDbillingBO();
                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                objData.DoctorID = Commonfunction.SemicolonSeparation_String_64(txt_referal.Text.Trim());
                objData.ServiceCategory = Convert.ToInt32(ddl_servicecategory.SelectedValue == "" ? "0" : ddl_servicecategory.SelectedValue);
                objData.PayableCategory = Convert.ToInt32(ddl_doctortype.SelectedValue == "" ? "0" : ddl_doctortype.SelectedValue);
                List<DoctorPayoutData> result = objBO.GetdueDates(objData);
                if (result.Count > 0)
                {
                    txtdatefrom.Text = result[0].DateFrom.ToString("dd/MM/yyyy") == "01/01/0001" ? "" : result[0].DateFrom.ToString("dd/MM/yyyy");
                    //txtto.Text = result[0].DateTo.ToString("dd/MM/yyyy") == "01/01/0001" ? "" : result[0].DateTo.ToString("dd/MM/yyyy");
                    txt_referal.Attributes["disabled"] = "disabled";
                    ddl_doctortype.Attributes["disabled"] = "disabled";
                }
                else
                {
                    txtdatefrom.Text = "";
                    txtto.Text = "";
                    txt_referal.Attributes.Remove("disabled");
                    txt_referal.Focus();
                }
            }
            else
            {
                txt_totalamount.Text = "";
                txt_discountamount.Text = "";
                txt_paidamount.Text = "";
                ddl_servicecategory.SelectedIndex = 0;
                txt_referal.Text = "";
                txt_remarks.Text = "";
                txt_paidamount.Text = "";
                txt_discountamount.Text = "";
                txt_totalamount.Text = "";
                txtdatefrom.Text = "";
                txtto.Text = "";
                Gv_Collectionlist.DataSource = null;
                Gv_Collectionlist.Visible = false;
                lblmessage.Visible = false;
                lblresult.Visible = false;
                txt_paymentnumber.Text = "";
                txt_referal.Attributes.Remove("disabled");
                ddl_doctortype.Attributes.Remove("disabled");
                txt_referal.Focus();
            }
        }
        protected void bindgrid()
        {
            try
            {
                if (ddl_servicecategory.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "ServiceCategory", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_servicecategory.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (Commonfunction.SemicolonSeparation_String_64(txt_referal.Text) == 0)
                {
                    txt_referal.Text = "";
                    Messagealert_.ShowMessage(lblmessage, "Referal", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_referal.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtdatefrom.Text.Trim() == "" && txtto.Text.Trim() == "")
                {
                    btn_pay.Attributes["disabled"] = "disabled";
                    Messagealert_.ShowMessage(lblmessage, "DuePayable", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                List<DoctorPayoutData> Servicelist = GetServiceList(0);
                if (Servicelist.Count > 0)
                {
                    Gv_Collectionlist.DataSource = Servicelist;
                    Gv_Collectionlist.DataBind();
                    Gv_Collectionlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + Servicelist[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    btn_pay.Attributes.Remove("disabled");

                    txt_totalamount.Text = Commonfunction.Getrounding(Servicelist[0].GdTotalBillAmount.ToString());
                    txt_discountamount.Text = Commonfunction.Getrounding(Servicelist[0].GdTotalDiscount.ToString());
                    txt_NetAmount.Text = Commonfunction.Getrounding(Servicelist[0].GdNetAmount.ToString());
                    txtDoctorPayable.Text = Commonfunction.Getrounding(Servicelist[0].GdReferralPayable.ToString());
                    txt_DocDiscount.Text = Commonfunction.Getrounding(Servicelist[0].GdRefDiscount.ToString());
                    txt_paidamount.Text = Commonfunction.Getrounding(Servicelist[0].GdPaid.ToString());

                    btn_pay.Attributes.Remove("disabled");
                }
                else
                {
                    btn_pay.Attributes["disabled"] = "disabled";
                    Gv_Collectionlist.DataSource = null;
                    Gv_Collectionlist.DataBind();
                    Gv_Collectionlist.Visible = true;
                    lblresult.Visible = false;
                    txt_totalamount.Text = "";
                    txt_discountamount.Text = "";
                    txt_NetAmount.Text = "";
                    txtDoctorPayable.Text = "";
                    txt_paidamount.Text = "";

                }
                //calculate();
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
        }
        public List<DoctorPayoutData> GetServiceList(int curIndex)
        {
            DoctorPayoutData objData = new DoctorPayoutData();
            OPDbillingBO objBO = new OPDbillingBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objData.DoctorID = Commonfunction.SemicolonSeparation_String_64(txt_referal.Text.Trim());
            objData.DateFrom = from;
            objData.DateTo = To;
            objData.ServiceCategory = Convert.ToInt32(ddl_servicecategory.SelectedValue == "" ? "0" : ddl_servicecategory.SelectedValue);
            return objBO.GetDoctorsPayableservices(objData);
        }
        protected void Gv_Collectionlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label isSubHeading = (Label)e.Row.FindControl("lbl_headertype");
                //Label UHID = (Label)e.Row.FindControl("lbl_uhid");
                //Label Amount = (Label)e.Row.FindControl("lbl_amount");
                //Label discount = (Label)e.Row.FindControl("lbl_discount");
                //Label netamount = (Label)e.Row.FindControl("lbl_netamount");
                //Label date = (Label)e.Row.FindControl("lbladt");
                //CheckBox chk = (CheckBox)e.Row.FindControl("chk_pay");
                //Label particular = (Label)e.Row.FindControl("lbl_service");
                //TextBox PC = (TextBox)e.Row.FindControl("txt_pc");
            }

        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txt_totalamount.Text = "";
            txt_discountamount.Text = "";
            txt_paidamount.Text = "";
            ddl_servicecategory.SelectedIndex = 0;
            txt_referal.Text = "";
            txt_remarks.Text = "";
            txt_paidamount.Text = "";
            txt_discountamount.Text = "";
            txt_totalamount.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            Gv_Collectionlist.DataSource = null;
            Gv_Collectionlist.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            txt_paymentnumber.Text = "";
            txt_paidamount.Text = "";
            btn_printrecv.Attributes["disabled"] = "disabled";
            btn_pay.Attributes["disabled"] = "disabled";
            ddl_doctortype.Attributes.Remove("disabled");
            txt_referal.Attributes.Remove("disabled");
        }
        protected void btn_pay_Click(object sender, EventArgs e)
        {
            try
            {

                btn_pay.Attributes["disabled"] = "disabled";
                DoctorPayoutData objData = new DoctorPayoutData();
                OPDbillingBO objBO = new OPDbillingBO();
                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                List<DoctorPayoutData> discountlist = new List<DoctorPayoutData>();
                List<DoctorPayoutData> Paidlist = new List<DoctorPayoutData>();
              
                foreach (GridViewRow row in Gv_Collectionlist.Rows)
                {
                    Label ID = (Label)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("lblBillID");
                    Label BillDate = (Label)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("lbladt");
                    Label BillID = (Label)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("lblBillID");
                    Label BillNo = (Label)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_bill");
                    Label UHID = (Label)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_uhid");
                    TextBox TotalBill = (TextBox)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("txt_billamnt");
                    TextBox Discount = (TextBox)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("txt_discount");
                    Label NetBill = (Label)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_netbill");
                    TextBox ReferralPC = (TextBox)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("txt_ReferralPC");
                    TextBox Runnerpc = (TextBox)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("txt_Runnerpc");
                    Label RefPayable = (Label)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_ReferralPayable");
                    Label RunnerPayable = (Label)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_RunnerPayable");
                    DoctorPayoutData ObjService = new DoctorPayoutData();
                    IFormatProvider option2 = new System.Globalization.CultureInfo("en-GB", true);
                    DateTime AddedDate = BillDate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(BillDate.Text.Trim(), option2, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

                    ObjService.ID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                    ObjService.AddedDate = AddedDate;
                    ObjService.UHID = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                    ObjService.BillID = Convert.ToInt64(BillID.Text == "" ? "0" : BillID.Text);
                    ObjService.BillNo = BillNo.Text.Trim() == "" ? "0" : BillNo.Text.Trim();
                    ObjService.TotalAmount = Convert.ToDecimal(TotalBill.Text == "" ? "0" : TotalBill.Text);
                    ObjService.DiscountAmnt = Convert.ToDecimal(Discount.Text == "" ? "0" : Discount.Text);
                    ObjService.NetAmount = Convert.ToDecimal(NetBill.Text == "" ? "0" : NetBill.Text);
                    ObjService.ReferralPC = Convert.ToDecimal(ReferralPC.Text == "" ? "0" : ReferralPC.Text);
                    ObjService.RunnerPC = Convert.ToDecimal(Runnerpc.Text == "" ? "0" : Runnerpc.Text);
                    ObjService.ReferralPayable = Convert.ToDecimal(RefPayable.Text == "" ? "0" : RefPayable.Text);
                    ObjService.RunnerPayable = Convert.ToDecimal(RunnerPayable.Text == "" ? "0" : RunnerPayable.Text);
                    Paidlist.Add(ObjService);
                }

                objData.XMLPaidData = XmlConvertor.PayableservicetoXML(Paidlist).ToString();
                objData.ReferralID = Commonfunction.SemicolonSeparation_String_64(txt_referal.Text.Trim());
                objData.ServiceCategory = Convert.ToInt32(ddl_servicecategory.SelectedValue == "" ? "0" : ddl_servicecategory.SelectedValue);
                objData.GdTotalBillAmount = Convert.ToDecimal(txt_totalamount.Text == "" ? "0" : txt_totalamount.Text);
                objData.GdTotalDiscount = Convert.ToDecimal(txt_discountamount.Text == "" ? "0" : txt_discountamount.Text);
                objData.GdNetAmount = Convert.ToDecimal(txt_NetAmount.Text == "" ? "0" : txt_NetAmount.Text);
                objData.GdReferralPayable = Convert.ToDecimal(txtDoctorPayable.Text == "" ? "0" : txtDoctorPayable.Text);
                objData.GdRefDiscount = Convert.ToDecimal(txt_DocDiscount.Text == "" ? "0" : txt_DocDiscount.Text);
                objData.GdPaid = Convert.ToDecimal(txt_paidamount.Text == "" ? "0" : txt_paidamount.Text);
                objData.Remarks = txt_remarks.Text;
                objData.EmployeeID = LogData.EmployeeID;
                objData.FinancialYearID = LogData.FinancialYearID;
                objData.HospitalID = LogData.HospitalID;
                DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                objData.DateFrom = from;
                objData.DateTo = To;

                List<DoctorPayoutData> result = objBO.PaidDoctorsServices(objData);
                if (result.Count > 0)
                {
                    txt_paymentnumber.Text = result[0].PaymentNumber.ToString();
                    Messagealert_.ShowMessage(lblmessage, "Doctorpay", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    btn_printrecv.Attributes.Remove("disabled");
                    btn_pay.Attributes["disabled"] = "disabled";
                }
                else
                {
                    txt_paymentnumber.Text = "";
                    lblmessage.Visible = false;
                    btn_pay.Attributes.Remove("disabled");
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

        protected void btn_searchhistory_Click(object sender, EventArgs e)
        {
            bindpaymentlist();
        }
        protected void bindpaymentlist()
        {
            try
            {
                if (LogData.SearchEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
                    div3.Visible = true;
                    div3.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }

                List<DoctorPayoutData> Servicelist = GetPaymentlist(0);
                if (Servicelist.Count > 0)
                {
                    Gv_paymenthistory.DataSource = Servicelist;
                    Gv_paymenthistory.DataBind();
                    Gv_paymenthistory.Visible = true;
                    Messagealert_.ShowMessage(lbl_result2, "Total:" + Servicelist[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    div4.Attributes["class"] = "SucessAlert";
                    div4.Visible = true;
                    txt_totalpayableamount.Text = Commonfunction.Getrounding(Servicelist[0].TotalAmount.ToString());
                    txt_totaldiscount.Text = Commonfunction.Getrounding(Servicelist[0].TotalPredDiscount.ToString());
                    txt_totalpaidamount.Text = Commonfunction.Getrounding(Servicelist[0].TotalPaidamount.ToString());
                    if (LogData.PrintEnable == 0)
                    {
                        btn_prinhistory.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btn_prinhistory.Attributes.Remove("disabled");
                    }

                }
                else
                {
                    Gv_paymenthistory.Attributes["disabled"] = "disabled";
                    Gv_paymenthistory.DataSource = null;
                    Gv_paymenthistory.DataBind();
                    Gv_paymenthistory.Visible = true;
                    lblresult.Visible = false;
                    txt_totalpayableamount.Text = "";
                    txt_totaldiscount.Text = "";
                    txt_totalpaidamount.Text = "";
                    lbl_result2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
        }
        public List<DoctorPayoutData> GetPaymentlist(int curIndex)
        {
            DoctorPayoutData objData = new DoctorPayoutData();
            OPDbillingBO objBO = new OPDbillingBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_paidfrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_paidfrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txt_paidto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_paidto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objData.PayableCategory = Convert.ToInt32(ddl2DoctorType.SelectedValue == "" ? "0" : ddl2DoctorType.SelectedValue);
            objData.DateFrom = from;
            objData.DateTo = To;
            objData.ServiceCategory = Convert.ToInt32(ddl2servicecategory.SelectedValue == "" ? "0" : ddl2servicecategory.SelectedValue);
            objData.PaymentNumber = txt_paymentnumbers.Text == "" ? "" : txt_paymentnumbers.Text;
            return objBO.GetPaymentlist(objData);
        }
        public void PendingRecordsGridview_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void Gv_paymenthistory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "Print")
                {
                    if (LogData.PrintEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "PrintEnable", 0);
                        div3.Visible = true;
                        div3.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage2.Visible = false;
                    }
                    int j = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gv = Gv_paymenthistory.Rows[j];
                    Label voucher = (Label)gv.Cells[0].FindControl("lbl_paymentnumber");
                    Label CategoryID = (Label)gv.Cells[0].FindControl("lbl_categoryID");
                    string vnumber = voucher.Text;
                    string caID = CategoryID.Text;
                    string url = "../MedBills/Reports/ReportViewer.aspx?option=DoctorPayment&voucher=" + vnumber + "&Category=" + caID;
                    string fullURL = "window.open('" + url + "', '_blank');";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                }
                if (e.CommandName == "Delete")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "DeleteEnable", 0);
                        div3.Visible = true;
                        div3.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage2.Visible = false;
                    }
                    DoctorPayoutData objData = new DoctorPayoutData();
                    OPDbillingDA objBO = new OPDbillingDA();
                    int j = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gv = Gv_paymenthistory.Rows[j];
                    Label voucher = (Label)gv.Cells[0].FindControl("lbl_paymentnumber");
                    Label CategoryID = (Label)gv.Cells[0].FindControl("lbl_categoryID");
                    Label DoctorID = (Label)gv.Cells[0].FindControl("lbl_doctorID");
                    string vnumber = voucher.Text;
                    string caID = CategoryID.Text;
                    string Doctor = DoctorID.Text == "" ? "0" : DoctorID.Text;
                    objData.PaymentNumber = voucher.Text.Trim() == "" ? "" : voucher.Text.Trim();
                    objData.DoctorID = Convert.ToInt32(DoctorID.Text.Trim() == "" ? "" : DoctorID.Text.Trim());
                    objData.ServiceCategory = Convert.ToInt32(CategoryID.Text.Trim() == "" ? "" : CategoryID.Text.Trim());
                    objData.EmployeeID = LogData.EmployeeID;
                    int result = objBO.DeleteReferralPamentByVoucherNo(objData);
                    if (result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Delete successfully.", 1);
                        bindpaymentlist();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                    }
                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
                div3.Attributes["class"] = "FailAlert";
                div3.Visible = true;
                return;
            }
        }

        protected void btnprint_Click(object sender, EventArgs e)
        {
            string url = "../MedBills/Reports/ReportViewer.aspx?option=DoctorPayment&voucher=" + txt_paymentnumber.Text.ToString() + "&Category=" + ddl_servicecategory.SelectedValue;
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
        protected void ddl_doctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_servicecategory.SelectedIndex = 0;
            txtdatefrom.Text = "";
            txtto.Text = "";
            txt_paymentnumber.Text = "";
        }
        protected void txt_referal_TextChanged(object sender, EventArgs e)
        {
            ddl_servicecategory.SelectedIndex = 0;
            txtdatefrom.Text = "";
            txtto.Text = "";
            txt_paymentnumber.Text = "";
            ddl_servicecategory.Focus();
        }
        protected void btn_historyreset_Click(object sender, EventArgs e)
        {
            //ddl_paiddoctr.SelectedIndex = 0;
            //ddl_payemntcategory.SelectedIndex = 0;
            Gv_paymenthistory.DataSource = null;
            Gv_paymenthistory.Visible = false;
            txt_totalpayableamount.Text = "";
            txt_totaldiscount.Text = "";
            txt_totalpaidamount.Text = "";
            lbl_result2.Visible = false;
        }
        protected void btn_prinhistory_Click(object sender, EventArgs e)
        {
            string PaymentNo = txt_paymentnumbers.Text.Trim() == "" ? "" : txt_paymentnumbers.Text.Trim();
            string Category = ddl2servicecategory.SelectedValue == "" ? "" : ddl2servicecategory.SelectedValue;
            int RefDoctorID = Commonfunction.SemicolonSeparation_String_32(txt2Referal.Text.Trim() == "" ? "" : txt2Referal.Text.Trim());

            string url = "../MedBills/Reports/ReportViewer.aspx?option=Doctorpaymentlist&voucher=" + PaymentNo + "&Category=" + Category + "&DoctorID=" + RefDoctorID + "&Datefrom=" + txt_paidfrom.Text + "&Dateto=" + txt_paidto.Text;
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
        protected void btn_printd_Click(object sender, EventArgs e)
        {
            Int64 doctorID = Commonfunction.SemicolonSeparation_String_64(txt_referal.Text);

            string url = "../MedBills/Reports/ReportViewer.aspx?option=DoctorPaymentDetail&voucher=" + txt_paymentnumber.Text.ToString() + "&Category=" + ddl_servicecategory.SelectedValue + "&DoctorID=" + doctorID;
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
        protected void txt_pc_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            int Lastindex = Gv_Collectionlist.Rows.Count - 1;
            TextBox BillAmount = (TextBox)Gv_Collectionlist.Rows[index].Cells[0].FindControl("txt_billamnt");
            TextBox Discount = (TextBox)Gv_Collectionlist.Rows[index].Cells[0].FindControl("txt_discount");
            Label NetAmount = (Label)Gv_Collectionlist.Rows[index].Cells[0].FindControl("lbl_netbill");
            TextBox RefPC = (TextBox)Gv_Collectionlist.Rows[index].Cells[0].FindControl("txt_ReferralPC");
            TextBox Runnerpc = (TextBox)Gv_Collectionlist.Rows[index].Cells[0].FindControl("txt_Runnerpc");
            Label DoctorShare = (Label)Gv_Collectionlist.Rows[index].Cells[0].FindControl("lbl_ReferralPayable");
            Label RunnerShare = (Label)Gv_Collectionlist.Rows[index].Cells[0].FindControl("lbl_RunnerPayable");

            Decimal BillAmt = Convert.ToDecimal(BillAmount.Text == "" ? "0" : BillAmount.Text);
            Decimal Dis = Convert.ToDecimal(Discount.Text == "" ? "0" : Discount.Text);
            Decimal NetAmt = 0;
            if (BillAmt>= Dis)
            {
                NetAmount.Text = Commonfunction.Getrounding(Convert.ToDecimal(BillAmt - Dis).ToString());
                NetAmt = BillAmt - Dis;
            }
            else
            {
                 NetAmt = Convert.ToDecimal(NetAmount.Text == "" ? "0" : NetAmount.Text);
            }
         
            DoctorShare.Text = Commonfunction.Getrounding((Convert.ToDecimal(RefPC.Text == "" ? "0" : RefPC.Text) / 100 * NetAmt).ToString());
            RunnerShare.Text = Commonfunction.Getrounding((Convert.ToDecimal(Runnerpc.Text == "" ? "0" : Runnerpc.Text) / 100 * NetAmt).ToString());

            if (Convert.ToDecimal(RefPC.Text == "" ? "0" : RefPC.Text) > 100)
            {
                DoctorShare.Text = "0";
                RefPC.Text = "";
                DoctorShare.Text = "";

                calculate();
                RefPC.Focus();
                return;
            }
            else
            {
                if (Lastindex > index)
                {
                    TextBox result1 = (TextBox)Gv_Collectionlist.Rows[index + 1].Cells[0].FindControl("txt_ReferralPC");
                    result1.Focus();
                }
                if (Lastindex == index)
                {
                    TextBox result2 = (TextBox)Gv_Collectionlist.Rows[index].Cells[0].FindControl("txt_ReferralPC");
                    result2.Focus();
                }

            }
            calculate();

        }
        protected void calculate()
        {
            Decimal totbillamnt = 0;
            Decimal totdiscount = 0;
            Decimal totnetbill = 0;
            Decimal RunPayable = 0;
            Decimal RefPayable = 0;
            foreach (GridViewRow row in Gv_Collectionlist.Rows)
            {

                TextBox txt_billamnt = (TextBox)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("txt_billamnt");
                TextBox txt_discount = (TextBox)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("txt_discount");
                Label lbl_netbill = (Label)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_netbill");
                Label RunnerPayable = (Label)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_RunnerPayable");
                Label ReferralPayable = (Label)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_ReferralPayable");

                totbillamnt = totbillamnt + Convert.ToDecimal(txt_billamnt.Text == "" ? "0" : txt_billamnt.Text);
                totdiscount = totdiscount + Convert.ToDecimal(txt_discount.Text == "" ? "0" : txt_discount.Text);
                totnetbill = totnetbill + Convert.ToDecimal(lbl_netbill.Text == "" ? "0" : lbl_netbill.Text);
                RunPayable = RunPayable + Convert.ToDecimal(RunnerPayable.Text == "" ? "0" : RunnerPayable.Text);
                RefPayable = RefPayable + Convert.ToDecimal(ReferralPayable.Text == "" ? "0" : ReferralPayable.Text);
            }
            txt_totalamount.Text = Commonfunction.Getrounding(totbillamnt.ToString());
            txt_discountamount.Text = Commonfunction.Getrounding(totdiscount.ToString());
            txt_NetAmount.Text = Commonfunction.Getrounding(totnetbill.ToString());
            txtDoctorPayable.Text = Commonfunction.Getrounding(RefPayable.ToString());
            txt_paidamount.Text = Commonfunction.Getrounding(RefPayable.ToString());
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
                ExportoExcel();
           
        }
        protected void ExportoExcel()
        {

            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Registration List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Registrationlist.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
                Messagealert_.ShowMessage(lblresult, "Exported", 1);
                divmsg3.Attributes["class"] = "SucessAlert";
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<DoctorPayoutData> PatientDetails = GetServiceList(0);
            List<DoctorPayoutDataToExcel> ListexcelData = new List<DoctorPayoutDataToExcel>();
            int i = 0;
            foreach (DoctorPayoutData row in PatientDetails)
            {
                DoctorPayoutDataToExcel Ecxeclpat = new DoctorPayoutDataToExcel();
                Ecxeclpat.BillNo = PatientDetails[i].BillNo;
                Ecxeclpat.PatientName = PatientDetails[i].PatientName;
                Ecxeclpat.PatAddress = PatientDetails[i].PatAddress;
                Ecxeclpat.TestName = PatientDetails[i].ServiceName;
                Ecxeclpat.TotalAmount = PatientDetails[i].TotalAmount.ToString("N");
                Ecxeclpat.DiscountAmnt = PatientDetails[i].DiscountAmnt.ToString("N");
                Ecxeclpat.NetAmount = PatientDetails[i].NetAmount.ToString("N");
                Ecxeclpat.ReferralName = PatientDetails[i].ReferralName;
                Ecxeclpat.ReferralPC = PatientDetails[i].ReferralPC.ToString("N");
                Ecxeclpat.ReferralPayable = PatientDetails[i].ReferralPayable.ToString("N");
                Ecxeclpat.RunnerPC = PatientDetails[i].RunnerPC.ToString("N");
                Ecxeclpat.RunnerPayable = PatientDetails[i].RunnerPayable.ToString("N");
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
    }
}