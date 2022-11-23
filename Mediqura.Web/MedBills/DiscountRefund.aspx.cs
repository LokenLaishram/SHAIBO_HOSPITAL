using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedBillData;
using Mediqura.CommonData.PatientData;
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
    public partial class DiscountRefund : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                if (Session["ReqNo"] != null)
                {
                    Int64 ID = Convert.ToInt32(Session["ReqNo"].ToString());
                    Session["ReqNo"] = null;
                    getDiscountData(ID);
                }
            }
        }
        public void getDiscountData(Int64 ID) {
            DiscountBO objBO = new DiscountBO();
            List<DiscountRefundData> result = objBO.GetDiscountDetailsByBillID(ID);
            if (result.Count > 0)
            {
                txtBillNo.Text = result[0].BillNo.ToString();
                txtUHID.Text = result[0].UHID.ToString();
                txtname.Text = result[0].PatientName.ToString();
                ddl_service_type.SelectedValue = result[0].PatientType.ToString();
                txt_refundAmount.Text = Commonfunction.Getrounding(result[0].RefundAmount.ToString());
                txt_total_amount.Text = Commonfunction.Getrounding(result[0].TotalAmount.ToString());
                txttotalrefundamount.Text= Commonfunction.Getrounding(result[0].RefundAmount.ToString());
                txtBillNo.ReadOnly = true;
                txtUHID.ReadOnly = true;
                txtname.ReadOnly = true;
                txtdatefrom.ReadOnly = true;
                txtto.ReadOnly = true;
                ddl_service_type.Attributes["disabled"] = "disabled";
                btnsearch.Visible = false;
                btnprints.Visible = false;
                btnSave.Visible = true;
            }
            else {
                Messagealert_.ShowMessage(lblmessage, "Cannot perform refund for this bill no.", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            txttotalrefundamount.Text = "0.00";
        
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
        protected void txtUHID_TextChanged(object sender, EventArgs e)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.UHID = Convert.ToInt64(txtUHID.Text.Trim() == "" ? "0" : txtUHID.Text.Trim());
            getResult = objInfoBO.GetPatientDetailsByUHID(Objpaic);
            if (getResult.Count > 0)
            {
                txtname.Text = getResult[0].PatientName.ToString();
                div1.Visible = false;
            }
            else
            {
                txtname.Text = "";
                txtUHID.Text = "";
            }
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txtUHID.Text = "";
            txtname.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            txtBillNo.Text = "";
            txt_refund_no.Text = "";
            txt_refundAmount.Text = "";
            txt_total_amount.Text = "";
            ddl_service_type.SelectedIndex = 0;
            txtBillNo.ReadOnly = false;
            txtUHID.ReadOnly = false;
            txtname.ReadOnly = false;
            txtdatefrom.ReadOnly = false;
            txtto.ReadOnly = false;
            ddl_service_type.Attributes.Remove("disabled");
            btnsearch.Visible = true;
            btnprints.Visible = false;
            btnSave.Visible = false;
            div1.Visible = false;
            gvrefundlist.DataSource = null;
            gvrefundlist.DataBind();
            gvrefundlist.Visible = true;
            txttotalrefundamount.Text = "0.00";
            lblresult.Visible = false;
            divmsg3.Visible = false;



        }
        protected void btnSave_Click(object sender, EventArgs e)
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
            if (txtUHID.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "UHID is Required.", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
             
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (txtname.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Patient Nameis required.", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
               
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (txtBillNo.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "IP/OP number is required.", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";

                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (ddl_service_type.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "PatientType is required.", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";

                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (txt_total_amount.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Total amount is required.", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";

                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (txt_refundAmount.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Refund amount is required.", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";

                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            List<DiscountRefundData> ListData = new List<DiscountRefundData>();
            DiscountRefundData objData = new DiscountRefundData();
            RefundBO objBo = new RefundBO();
            // int index = 0;
            try
            {

                objData.FinancialYearID = LogData.FinancialYearID;
                objData.EmployeeID = LogData.EmployeeID;
                objData.HospitalID = LogData.HospitalID;
                objData.EmployeeID = LogData.EmployeeID;

                objData.UHID = Convert.ToInt64(txtUHID.Text == "" ? "0" : txtUHID.Text);
                objData.PatientName = txtname.Text == "" ? "" : txtname.Text.Trim();
                objData.BillNo = txtBillNo.Text == "" ? "" : txtBillNo.Text.Trim();
                objData.RefundAmount = Convert.ToDecimal(txt_refundAmount.Text == "" ? "0" : txt_refundAmount.Text.Trim());
                objData.TotalAmount = Convert.ToDecimal(txt_total_amount.Text == "" ? "0" : txt_total_amount.Text.Trim());
                objData.PatientType = Convert.ToInt32(ddl_service_type.SelectedValue == "" ? "0" : ddl_service_type.SelectedValue);


                List<DiscountRefundData> results = objBo.UpdateDiscountRefundDetails(objData);
                if (results.Count > 0)
                {
                    txt_refund_no.Text = results[0].RefundNo.ToString();
                    Messagealert_.ShowMessage(lblmessage, "Refund", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    btnSave.Visible = false;
                    btnprints.Visible = true;
                }
                else
                {
                    txt_refund_no.Text = "";
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    div1.Visible = true;
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
        protected void btnsearch_Click(object sender, EventArgs e)
        {
           
            bindgrid();
        }
        protected void bindgrid()
        {
            try
            {
                List<DiscountRefundData> objdeposit = GetRefundList(0);
                if (objdeposit.Count > 0)
                {
                    gvrefundlist.DataSource = objdeposit;
                    gvrefundlist.DataBind();
                    gvrefundlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    txttotalrefundamount.Text = Commonfunction.Getrounding(objdeposit[0].TotalRefundAmount.ToString());
                    divmsg3.Visible = true;
                }
                else
                {
                    gvrefundlist.DataSource = null;
                    gvrefundlist.DataBind();
                    gvrefundlist.Visible = true;
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
        public List<DiscountRefundData> GetRefundList(int curIndex)
        {
            DiscountRefundData Objdata = new DiscountRefundData();
            RefundBO objBo = new RefundBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            Objdata.UHID = Convert.ToInt64(txtUHID.Text == "" ? "0" : txtUHID.Text);
            Objdata.PatientName = txtname.Text == "" ? "" : txtname.Text.Trim();
            Objdata.BillNo = txtBillNo.Text == "" ? "" : txtBillNo.Text.Trim();
            Objdata.RefundAmount = Convert.ToDecimal(txt_refundAmount.Text == "" ? "0" : txt_refundAmount.Text.Trim());
            Objdata.TotalAmount = Convert.ToDecimal(txt_total_amount.Text == "" ? "0" : txt_total_amount.Text.Trim());
            Objdata.PatientType = Convert.ToInt32(ddl_service_type.SelectedValue == "" ? "0" : ddl_service_type.SelectedValue);
            Objdata.DateFrom = from;
            Objdata.DateTo = To;
            return objBo.GetDiscountRefundList(Objdata);
        }
        protected void gvrefundlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvrefundlist.PageIndex = e.NewPageIndex;
            bindgrid();
        }

        protected void btnprints_Click(object sender, EventArgs e)
        {
            string url = "../MedBills/Reports/ReportViewer.aspx?option=DiscountRefund&RefNo=" + txt_refund_no.Text.ToString();
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
    }
}