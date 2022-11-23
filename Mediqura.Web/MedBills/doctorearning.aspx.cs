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
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedBills
{
    public partial class doctorearning : BasePage
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
            Commonfunction.PopulateDdl(ddl_doctor, mstlookup.GetLookupsList(LookupName.Doctor));
            txt_totalamount.Text = "";
            txt_discountamount.Text = "";
            txt_discaftersettlement.Text = "";
            txt_paidamount.Text = "";
            txt_paidamount.Text = "";
            ddlstatus.Attributes["disabled"] = "disabled";
            btn_pay.Attributes["disabled"] = "disabled";
            btn_printd.Attributes["disabled"] = "disabled";
            btn_printrecv.Attributes["disabled"] = "disabled";
        }
        protected void btnsearch_Click(object sender, EventArgs e)
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
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_doctor.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Doctor", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_doctor.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
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
                //if (txtdatefrom.Text == "" && txtto.Text == "")
                //{
                //    btn_pay.Attributes["disabled"] = "disabled";
                //    Messagealert_.ShowMessage(lblmessage, "DuePayable", 0);
                //    div1.Visible = true;
                //    div1.Attributes["class"] = "FailAlert";
                //    return;
                //}
                //else
                //{
                //    lblmessage.Visible = false;
                //}
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
                    //if (ddl_servicecategory.SelectedValue == "5" || ddl_servicecategory.SelectedValue == "6")
                    //{
                    //    txt_totalamount.Text = "";
                    //    txt_payableamount.Text = "";
                    //    txt_discountamount.Text = "";
                    //    txt_discaftersettlement.Text = "";
                    //    txt_paidamount.Text = "";
                    //}
                    //else
                    //{
                        txt_totalamount.Text = Commonfunction.Getrounding(Servicelist[0].TotalAmount.ToString());
                        txt_payableamount.Text = Commonfunction.Getrounding(Servicelist[0].TotalPayable.ToString());
                        txt_discountamount.Text = Commonfunction.Getrounding(Servicelist[0].TotalPredDiscount.ToString());
                        txt_discaftersettlement.Text = Commonfunction.Getrounding(Servicelist[0].TotalPostDiscount.ToString());
                        txt_paidamount.Text = Commonfunction.Getrounding(Servicelist[0].TotalPayable.ToString());
                    //}
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
                    txt_discaftersettlement.Text = "";
                    txt_paidamount.Text = "";
                    txt_payableamount.Text = "";
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
        public List<DoctorPayoutData> GetServiceList(int curIndex)
        {
            DoctorPayoutData objData = new DoctorPayoutData();
            OPDbillingBO objBO = new OPDbillingBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objData.DoctorID = Convert.ToInt64(ddl_doctor.SelectedValue == "" ? "0" : ddl_doctor.SelectedValue);
            objData.DateFrom = from;
            objData.DateTo = To;
            objData.PaymentStatus = Convert.ToInt32(ddlstatus.SelectedValue == "" ? "0" : ddlstatus.SelectedValue);
            objData.ServiceCategory = Convert.ToInt32(ddl_servicecategory.SelectedValue == "" ? "0" : ddl_servicecategory.SelectedValue);
            return objBO.GetDoctorsEarnings(objData);
        }
        protected void Gv_Collectionlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label isSubHeading = (Label)e.Row.FindControl("lbl_headertype");
                Label UHID = (Label)e.Row.FindControl("lbl_uhid");
                Label Amount = (Label)e.Row.FindControl("lbl_amount");
                Label discount = (Label)e.Row.FindControl("lbl_discount");
                Label netamount = (Label)e.Row.FindControl("lbl_netamount");
                Label date = (Label)e.Row.FindControl("lbladt");
                CheckBox chk = (CheckBox)e.Row.FindControl("chk_pay");
                Label particular = (Label)e.Row.FindControl("lbl_service");

                if (isSubHeading.Text == "1")
                {
                    e.Row.BackColor = Color.FromName("#cfede3");
                    netamount.ForeColor = Color.FromName("#fd0808");
                    discount.ForeColor = Color.FromName("#fd0808");
                    Amount.ForeColor = Color.FromName("#fd0808");
                    UHID.Text = "";
                    date.Text = "";
                }

                if (ddl_servicecategory.SelectedValue == "5" || ddl_servicecategory.SelectedValue == "6")
                {
                    chk.Enabled = true;
                }
                else
                {
                    chk.Enabled = false;
                }
                if (particular.Text == "Discount(IP)" || particular.Text == "Discount(Emergency)" || particular.Text == "Extra Discount")
                {
                    chk.Enabled = true;
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }

            }
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txt_totalamount.Text = "";
            txt_discountamount.Text = "";
            txt_discaftersettlement.Text = "";
            txt_paidamount.Text = "";
            ddl_servicecategory.SelectedIndex = 0;
            ddlstatus.SelectedIndex = 0;
            ddl_doctor.SelectedIndex = 0;
            txt_remarks.Text = "";
            txt_paidamount.Text = "";
            txt_discaftersettlement.Text = "";
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
            btn_printd.Attributes["disabled"] = "disabled";
            btn_printrecv.Attributes["disabled"] = "disabled";
            btn_pay.Attributes["disabled"] = "disabled";
            txt_payableamount.Text = "";
        }
        protected void btn_pay_Click(object sender, EventArgs e)
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
                btn_pay.Attributes["disabled"] = "disabled";
                DoctorPayoutData objData = new DoctorPayoutData();
                OPDbillingBO objBO = new OPDbillingBO();
                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                List<DoctorPayoutData> discountlist = new List<DoctorPayoutData>();
                int Otcount = 0;
                foreach (GridViewRow row in Gv_Collectionlist.Rows)
                {
                    CheckBox Chk_otpay = (CheckBox)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("chk_pay");
                    Label caseID = (Label)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_serviceID");
                    Label uhids = (Label)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_uhid");
                    Label Otno = (Label)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_bill");
                    Label particular = (Label)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_service");
                    Label BillID = (Label)Gv_Collectionlist.Rows[row.RowIndex].Cells[0].FindControl("lblBillID");

                    if (Chk_otpay.Checked == true && (ddl_servicecategory.SelectedValue == "5" || ddl_servicecategory.SelectedValue == "6"))
                    {
                        objData.ServiceID = Convert.ToInt32(caseID.Text == "" ? "0" : caseID.Text);
                        objData.UHID = Convert.ToInt64(uhids.Text == "" ? "0" : uhids.Text);
                        objData.Otnumber = Otno.Text.Trim();
                        Otcount = Otcount + 1;
                    }
                    if (Chk_otpay.Checked == true && (particular.Text == "Discount(IP)" || particular.Text == "Discount(Emergency)" || particular.Text == "Extra Discount"))
                    {
                        DoctorPayoutData ObjDetails = new DoctorPayoutData();
                        ObjDetails.DoctorID = Convert.ToInt64(ddl_doctor.SelectedValue == "" ? "0" : ddl_doctor.SelectedValue);
                        ObjDetails.ServiceID = Convert.ToInt32(caseID.Text == "" ? "0" : caseID.Text);
                        ObjDetails.BillID = Convert.ToInt32(BillID.Text == "" ? "0" : BillID.Text);
                        ObjDetails.AdjustementType = Convert.ToInt32(particular.Text == "Discount(Emergency)" ? "1" : particular.Text == "Discount(IP)" ? "2" : particular.Text == "Extra Discount" ? "3" : "0");
                        discountlist.Add(ObjDetails);
                    }
                }
                objData.XMLData = XmlConvertor.DiscountAdjustedlist(discountlist).ToString();
                objData.DoctorID = Convert.ToInt64(ddl_doctor.SelectedValue == "" ? "0" : ddl_doctor.SelectedValue);
                objData.ServiceCategory = Convert.ToInt32(ddl_servicecategory.SelectedValue == "" ? "0" : ddl_servicecategory.SelectedValue);
                objData.TotalPredDiscount = Convert.ToDecimal(txt_discountamount.Text == "" ? "0" : txt_discountamount.Text);
                objData.TotalPostDiscount = Convert.ToDecimal(txt_discaftersettlement.Text == "" ? "0" : txt_discaftersettlement.Text);
                objData.TotalAmount = Convert.ToDecimal(txt_totalamount.Text == "" ? "0" : txt_totalamount.Text);
                objData.PaidAmount = Convert.ToDecimal(txt_paidamount.Text == "" ? "0" : txt_paidamount.Text);
                objData.Remarks = txt_remarks.Text.Trim();
                objData.EmployeeID = LogData.EmployeeID;
                objData.FinancialYearID = LogData.FinancialYearID;
                objData.HospitalID = LogData.HospitalID;
                DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                objData.DateFrom = from;
                objData.DateTo = To;

                if ((ddl_servicecategory.SelectedValue == "5" || ddl_servicecategory.SelectedValue == "6") && (Otcount > 1 || Otcount == 0))
                {
                    Messagealert_.ShowMessage(lblmessage, "otcount", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                List<DoctorPayoutData> result = objBO.PaidDoctorsServices(objData);
                if (result.Count > 0)
                {
                    bindgrid();
                    txt_paymentnumber.Text = result[0].PaymentNumber.ToString();
                    Messagealert_.ShowMessage(lblmessage, "Doctorpay", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    btn_printd.Attributes.Remove("disabled");
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
        protected void ddl_servicecategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Gv_Collectionlist.DataSource = null;
            Gv_Collectionlist.DataBind();
            lblresult.Visible = false;
            Gv_Collectionlist.Visible = false;
            txt_totalamount.Text = "";
            txt_discountamount.Text = "";
            txt_discaftersettlement.Text = "";
            txt_tax.Text = "";
            txt_paidamount.Text = "";
            txt_payableamount.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            txt_paymentnumber.Text = "";
            btn_pay.Attributes["disabled"] = "disabled";

            if (ddl_servicecategory.SelectedIndex > 0)
            {
                txtdatefrom.Text = "";
                txtto.Text = "";
                DoctorPayoutData objData = new DoctorPayoutData();
                OPDbillingBO objBO = new OPDbillingBO();
                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                objData.DoctorID = Convert.ToInt64(ddl_doctor.SelectedValue == "" ? "0" : ddl_doctor.SelectedValue);
                objData.PaymentStatus = Convert.ToInt32(ddlstatus.SelectedValue == "" ? "0" : ddlstatus.SelectedValue);
                objData.ServiceCategory = Convert.ToInt32(ddl_servicecategory.SelectedValue == "" ? "0" : ddl_servicecategory.SelectedValue);
                List<DoctorPayoutData> result = objBO.GetdueDates(objData);
                //if (result.Count > 0)
                //{
                //    txtdatefrom.Text = result[0].DateFrom.ToString("dd/MM/yyyy") == "01/01/0001" ? "" : result[0].DateFrom.ToString("dd/MM/yyyy");
                //    txtto.Text = result[0].DateTo.ToString("dd/MM/yyyy") == "01/01/0001" ? "" : result[0].DateTo.ToString("dd/MM/yyyy");
                //}
                //else
                //{
                //    txtdatefrom.Text = "";
                //    txtto.Text = "";
                //}
            }
            else
            {
                txt_totalamount.Text = "";
                txt_discountamount.Text = "";
                txt_discaftersettlement.Text = "";
                txt_paidamount.Text = "";
                ddl_servicecategory.SelectedIndex = 0;
                ddlstatus.SelectedIndex = 0;
                ddl_doctor.SelectedIndex = 0;
                txt_remarks.Text = "";
                txt_paidamount.Text = "";
                txt_discaftersettlement.Text = "";
                txt_discountamount.Text = "";
                txt_totalamount.Text = "";
                txtdatefrom.Text = "";
                txtto.Text = "";
                Gv_Collectionlist.DataSource = null;
                Gv_Collectionlist.Visible = false;
                lblmessage.Visible = false;
                lblresult.Visible = false;
                txt_paymentnumber.Text = "";
            }
        }
       protected void chk_pay_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox txt = sender as CheckBox;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            Label netamount = (Label)Gv_Collectionlist.Rows[index].Cells[0].FindControl("lbl_netamount");
            Label discamount = (Label)Gv_Collectionlist.Rows[index].Cells[0].FindControl("lbl_discount");
            Label service = (Label)Gv_Collectionlist.Rows[index].Cells[0].FindControl("lbl_service");
            Label date = (Label)Gv_Collectionlist.Rows[index].Cells[0].FindControl("lbladt");
            Label ServiceID = (Label)Gv_Collectionlist.Rows[index].Cells[0].FindControl("lbl_serviceID");
            CheckBox chk_pay = (CheckBox)Gv_Collectionlist.Rows[index].Cells[0].FindControl("chk_pay");
            Label Particular = (Label)Gv_Collectionlist.Rows[index].Cells[0].FindControl("lbl_service");

            if (ddl_servicecategory.SelectedValue == "5" || ddl_servicecategory.SelectedValue == "6")
            {
                if (chk_pay.Checked == true)
                {
                    hdnCaseID.Value = ServiceID.Text;
                    txt_totalamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalamount.Text == "" ? "0" : txt_totalamount.Text) + Convert.ToDecimal(netamount.Text == "" ? "0" : netamount.Text)).ToString());
                    txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_paidamount.Text == "" ? "0" : txt_paidamount.Text) + Convert.ToDecimal(netamount.Text == "" ? "0" : netamount.Text)).ToString());
                    txt_payableamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_payableamount.Text == "" ? "0" : txt_payableamount.Text) + Convert.ToDecimal(netamount.Text == "" ? "0" : netamount.Text)).ToString());
                }
                if (chk_pay.Checked == false)
                {
                    hdnCaseID.Value = "0";
                    txt_totalamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalamount.Text == "" ? "0" : txt_totalamount.Text) - Convert.ToDecimal(netamount.Text == "" ? "0" : netamount.Text)).ToString());
                    txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_paidamount.Text == "" ? "0" : txt_paidamount.Text) - Convert.ToDecimal(netamount.Text == "" ? "0" : netamount.Text)).ToString());
                    txt_payableamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_payableamount.Text == "" ? "0" : txt_payableamount.Text) - Convert.ToDecimal(netamount.Text == "" ? "0" : netamount.Text)).ToString());
                }
                btn_pay.Attributes.Remove("disabled");
            }
            if (Particular.Text == "Discount(IP)" || Particular.Text == "Discount(Emergency)" || Particular.Text == "Extra Discount")
            {
                if (chk_pay.Checked == true)
                {
                    txt_discaftersettlement.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_discaftersettlement.Text == "" ? "0" : txt_discaftersettlement.Text) + Convert.ToDecimal(discamount.Text == "" ? "0" : discamount.Text)).ToString());
                    if ((Convert.ToDecimal(txt_discaftersettlement.Text == "" ? "0" : txt_discaftersettlement.Text) <= (Convert.ToDecimal(txt_payableamount.Text == "" ? "0" : txt_payableamount.Text))))
                    {
                        txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_payableamount.Text == "" ? "0" : txt_payableamount.Text) - Convert.ToDecimal(txt_discaftersettlement.Text == "" ? "0" : txt_discaftersettlement.Text)).ToString());
                        lblmessage.Visible = false;
                        btn_pay.Attributes.Remove("disabled");
                    }
                    else
                    {
                        btn_pay.Attributes["disabled"] = "disabled";
                        Messagealert_.ShowMessage(lblmessage, "Discount could not be adjusted as payable is less.", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                }
                if (chk_pay.Checked == false)
                {
                    lblmessage.Visible = false;
                    btn_pay.Attributes.Remove("disabled");
                    txt_discaftersettlement.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_discaftersettlement.Text == "" ? "0" : txt_discaftersettlement.Text) - Convert.ToDecimal(discamount.Text == "" ? "0" : discamount.Text)).ToString());
                    txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_payableamount.Text == "" ? "0" : txt_payableamount.Text) - Convert.ToDecimal(txt_discaftersettlement.Text == "" ? "0" : txt_discaftersettlement.Text)).ToString());
                }
                date.Focus();
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
            //txtdatefrom.Text = "";
            //txtto.Text = "";
            txt_paymentnumber.Text = "";
        }
      }
}