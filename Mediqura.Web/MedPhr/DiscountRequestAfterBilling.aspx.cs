using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.BOL.MedStore;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedBillData;
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
    public partial class DiscountRequestAfterBilling : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txt_BillNo.Attributes["disabled"] = "disabled";
                btnTab1Save.Attributes["disabled"] = "disabled";
                btnTab3Save.Attributes["disabled"] = "disabled";
                btnTab3Print.Attributes["disabled"] = "disabled";
                btnTab3Reset.Attributes["disabled"] = "disabled";
                bindddl();
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlpaymentmode, mstlookup.GetLookupsList(LookupName.PaymentMode));

        }
        protected void ddl_PatientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            StockGRNData ObjDisReqData = new StockGRNData();
            if (ddl_PatientType.SelectedValue == "1")
            {
                ObjDisReqData.PatientTypeID = 1;
                txt_BillNo.Attributes.Remove("disabled");
                ddl_PatientType.Attributes["disabled"] = "disabled";
                txt_BillNo.Text = "";
                txt_BillNo.Focus();
                AutoCompleteExtender_BillNo.ContextKey = ddl_PatientType.SelectedValue;
            }
            else if (ddl_PatientType.SelectedValue == "2")
            {
                ObjDisReqData.PatientTypeID = 2;
                txt_BillNo.Attributes.Remove("disabled");
                ddl_PatientType.Attributes["disabled"] = "disabled";
                txt_BillNo.Text = "";
                txt_BillNo.Focus();
                AutoCompleteExtender_BillNo.ContextKey = ddl_PatientType.SelectedValue;
            }
            else if (ddl_PatientType.SelectedValue == "3")
            {
                ObjDisReqData.PatientTypeID = 3;
                txt_BillNo.Attributes.Remove("disabled");
                ddl_PatientType.Attributes["disabled"] = "disabled";
                txt_BillNo.Text = "";
                txt_BillNo.Focus();
                AutoCompleteExtender_BillNo.ContextKey = ddl_PatientType.SelectedValue;
            }
            else
            {
                txt_BillNo.Attributes["disabled"] = "disabled";
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetBillNumberAuto(string prefixText, int count, string contextKey)
        {
            StockGRNData ObjDisReqData = new StockGRNData();
            StockGRNBO ObjDisReqBO = new StockGRNBO();
            List<StockGRNData> getResult = new List<StockGRNData>();
            ObjDisReqData.BillNo = prefixText;
            ObjDisReqData.PatientTypeID = Convert.ToInt32(contextKey);
            getResult = ObjDisReqBO.GetBillNumberAuto(ObjDisReqData);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].BillNo.ToString());
            }
            return list;
        }

        protected void txt_BillNo_TextChanged(object sender, EventArgs e)
        {
            GetPatientDetails();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GetPatientDetails();
        }

        protected void GetPatientDetails()
        {
            StockGRNData ObjDisReqData = new StockGRNData();
            StockGRNBO ObjDisReqBO = new StockGRNBO();
            List<StockGRNData> getResult = new List<StockGRNData>();
            bool IsNumeric = txt_BillNo.Text.All(char.IsDigit);
            if (IsNumeric == false)
            {
                if (txt_BillNo.Text.Contains(">"))
                {
                    //bool IsBillNoNumeric = txt_BillNo.Text.Substring(txt_BillNo.Text.LastIndexOf('>') + 1).All(char.IsDigit);
                    ObjDisReqData.BillNo = txt_BillNo.Text.Contains(">") ? txt_BillNo.Text.Substring(txt_BillNo.Text.LastIndexOf('>') + 1).Trim() : "0";
                    ObjDisReqData.PatientTypeID = Convert.ToInt32(ddl_PatientType.SelectedValue);
                }
                else
                {
                    txt_BillNo.Text = "";
                    txt_BillNo.Focus();
                    return;
                }
            }
            getResult = ObjDisReqBO.GetPatientDetailsByBillNo(ObjDisReqData);
            if (getResult.Count > 0)
            {
                txt_BillNo.Attributes["disabled"] = "disabled";
                btnTab1Save.Attributes.Remove("disabled");
                lblTransID.Text = getResult[0].TransactionID.ToString();
                lblUHID.Text = getResult[0].UHID.ToString();
                lblIPNo.Text = getResult[0].IPNo.ToString() == "0" ? "NULL" : getResult[0].IPNo.ToString();
                lblEMG.Text = getResult[0].EmrgNo.ToString() == "0" ? "NULL" : getResult[0].EmrgNo.ToString();
                lblPaidAmount.Text = getResult[0].PaidAmount.ToString();
                txtTab1_RequestAmt.Text = "";
                txtTab1_Remark.Text = "";
                GvDiscountRequestAfterBilling.DataSource = getResult;
                GvDiscountRequestAfterBilling.DataBind();
                GvDiscountRequestAfterBilling.Visible = true;
            }
            else
            {
                GvDiscountRequestAfterBilling.DataSource = null;
                GvDiscountRequestAfterBilling.DataBind();
                GvDiscountRequestAfterBilling.Visible = true;
            }
        }
        protected void GvDiscountRequestAfterBilling_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label BillAmount = (Label)e.Row.FindControl("lblTotalBillAmount");
                Label PaidAmount = (Label)e.Row.FindControl("lblPaidAmount");
                Label RefundedAmount = (Label)e.Row.FindControl("lblRefundedAmount");

                BillAmount.Text = Commonfunction.Getrounding(BillAmount.Text);
                PaidAmount.Text = Commonfunction.Getrounding(PaidAmount.Text);   
                RefundedAmount.Text = Commonfunction.Getrounding(RefundedAmount.Text);

                Decimal RefundableAmount;
                RefundableAmount = Convert.ToDecimal(PaidAmount.Text == "" ? "0" : PaidAmount.Text) - Convert.ToDecimal(RefundedAmount.Text == "" ? "0" : RefundedAmount.Text);
                lblTab1_RefundableAmount.Text = RefundableAmount.ToString();

                if (Convert.ToDecimal(PaidAmount.Text == "" ? "0" : PaidAmount.Text) == Convert.ToDecimal(RefundedAmount.Text == "" ? "0" : RefundedAmount.Text))
                {
                    txtTab1_RequestAmt.Attributes["disabled"] = "disabled";
                    txtTab1_Remark.Attributes["disabled"] = "disabled";
                    btnTab1Save.Attributes["disabled"] = "disabled";
                    Messagealert_.ShowMessage(lblmessage, "Amount Already Refunded.", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }
                else
                {
                    txtTab1_RequestAmt.Attributes.Remove("disabled");
                    txtTab1_Remark.Attributes.Remove("disabled");
                    btnTab1Save.Attributes.Remove("disabled");
                    divmsg1.Visible = false;
                    lblmessage.Text = "";
                }
            }
        }
        protected void txtTab1_RequestAmt_TextChanged(object sender, EventArgs e)
        {                
            if (Convert.ToDecimal(lblTab1_RefundableAmount.Text) < Convert.ToDecimal(txtTab1_RequestAmt.Text))
            {
                txtTab1_RequestAmt.ForeColor = System.Drawing.Color.FromName("#ee4e42");
                txtTab1_RequestAmt.Focus();
                Messagealert_.ShowMessage(lblmessage, "Request Amount shouldn't be greater than Refundable Amount. ", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Text = "";
                divmsg1.Visible = false;
            }
        }
 
        protected void btnTab1Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTab1_RequestAmt.Text == "")
                {
                    txtTab1_RequestAmt.BackColor = System.Drawing.Color.FromName("#ee4e42");
                    txtTab1_RequestAmt.Focus();
                    Messagealert_.ShowMessage(lblmessage, "Please enter Request Amount. ", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                if (Convert.ToDecimal(lblTab1_RefundableAmount.Text) < Convert.ToDecimal(txtTab1_RequestAmt.Text))
                {
                    txtTab1_RequestAmt.ForeColor = System.Drawing.Color.FromName("#ee4e42");
                    txtTab1_RequestAmt.Focus();
                    Messagealert_.ShowMessage(lblmessage, "Request Amount shouldn't be greater than Refundable Amount. ", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }

                StockGRNData ObjDisReqData = new StockGRNData();
                StockGRNBO ObjDisReqBO = new StockGRNBO();
                List<StockGRNData> getResult = new List<StockGRNData>();

                ObjDisReqData.PatientTypeID = Convert.ToInt32(ddl_PatientType.SelectedValue);
                ObjDisReqData.PatientName = txt_BillNo.Text.Trim();
                if (GvDiscountRequestAfterBilling.Rows.Count != 0)
                {
                    foreach (GridViewRow row in GvDiscountRequestAfterBilling.Rows)
                    {
                        IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                        Label BillNo = (Label)GvDiscountRequestAfterBilling.Rows[row.RowIndex].Cells[0].FindControl("lblBillNo");
                        Label TotalBillAmount = (Label)GvDiscountRequestAfterBilling.Rows[row.RowIndex].Cells[0].FindControl("lblTotalBillAmount");
                        Label PaidAmount = (Label)GvDiscountRequestAfterBilling.Rows[row.RowIndex].Cells[0].FindControl("lblPaidAmount");

                        ObjDisReqData.BillNo = BillNo.Text.Trim();
                        ObjDisReqData.TotalBillAmount = Convert.ToDecimal(TotalBillAmount.Text.Trim());

                    }
                }
                if (txtTab1_Remark.Text == "")
                {
                    txtTab1_Remark.BackColor = System.Drawing.Color.FromName("#ee4e42");
                    txtTab1_Remark.Focus();
                    Messagealert_.ShowMessage(lblmessage, "Please enter Remark. ", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }

                ObjDisReqData.RequestedAmount = Convert.ToDecimal(txtTab1_RequestAmt.Text.Trim());
                ObjDisReqData.Remarks = txtTab1_Remark.Text.Trim();
                ObjDisReqData.TransactionID = Convert.ToInt64(lblTransID.Text.Trim());
                ObjDisReqData.UHID = Convert.ToInt64(lblUHID.Text.Trim());
                ObjDisReqData.IPNo = lblIPNo.Text.Trim();
                ObjDisReqData.EmrgNo = lblEMG.Text.Trim();
                ObjDisReqData.PaidAmount = Convert.ToDecimal(lblPaidAmount.Text.Trim());
                ObjDisReqData.EmployeeID = LogData.EmployeeID;
                ObjDisReqData.HospitalID = LogData.HospitalID;
                ObjDisReqData.FinancialYearID = LogData.FinancialYearID;

                string Result = ObjDisReqBO.SaveDiscountRequestAfterBilling(ObjDisReqData);

                if (Result == "6")
                {
                    Messagealert_.ShowMessage(lblmessage, "Amount Already Refunded.", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }
                else if (Result != "5" && Result != "6")
                {
                    lblRequestNo.Text = Result;
                    btnTab1Save.Attributes["disabled"] = "disabled";
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Attributes["class"] = "SucessAlert";
                    divmsg1.Visible = true;
                }
                else if (Result == "5")
                {
                    Messagealert_.ShowMessage(lblmessage, "One Request is already in progress", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                return;
            }
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            Tab1Reset();
        }

        protected void Tab1Reset()
        {
            ddl_PatientType.SelectedValue = "0";
            ddl_PatientType.Attributes.Remove("disabled");
            txt_BillNo.Text = "";
            txt_BillNo.Attributes["disabled"] = "disabled";
            lblRequestNo.Text = "";
            lblTransID.Text = "";
            lblUHID.Text = "";
            lblIPNo.Text = "";
            lblEMG.Text = "";
            lblmessage.Text = "";
            lblResult.Text = "";
            divmsg1.Visible = false;
            GvDiscountRequestAfterBilling.DataSource = null;
            GvDiscountRequestAfterBilling.DataBind();
            GvDiscountRequestAfterBilling.Visible = false;
            lblTab1_RefundableAmount.Text = "";
            txtTab1_RequestAmt.Text = "";
            txtTab1_Remark.Text = "";
            btnTab1Save.Attributes["disabled"] = "disabled";
        }
        //--------------------------------END TAB 1-------------------------------
        //--------------------------------START TAB 2-------------------------------
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetRQNumberAuto(string prefixText, int count, string contextKey)
        {
            StockGRNData ObjDisReqList = new StockGRNData();
            StockGRNBO ObjDisReqBO = new StockGRNBO();
            List<StockGRNData> getResult = new List<StockGRNData>();
            ObjDisReqList.ReqNo = prefixText;
            getResult = ObjDisReqBO.GetRQNumberAuto(ObjDisReqList);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ReqNo.ToString());
            }
            return list;
        }
        protected void txtTab2_RequestNo_TextChanged(object sender, EventArgs e)
        {
            BindGridViewTab2(1);
        }
        protected void ddlTab2_PatientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridViewTab2(1);
        }

        protected void ddlTab2_RequestStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridViewTab2(1);
        }

        protected void btnTab2_Search_Click(object sender, EventArgs e)
        {
            BindGridViewTab2(1);
        }
        protected void GvDiscountRequestList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LblStatusID = (Label)e.Row.FindControl("lblTab2StatusID");
                TextBox Remark = (TextBox)e.Row.FindControl("txtTab2Remark");
                TextBox RemarkApproved = (TextBox)e.Row.FindControl("txtTab2RemarkApproved");
                TextBox RemarkRejected = (TextBox)e.Row.FindControl("txtTab2RemarkRejected");
                LinkButton lbtnRefund = (LinkButton)e.Row.FindControl("lbtnTab2Refund");
                LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnTab2Delete");

                if (LblStatusID.Text == "1")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.FromName("#fec337");
                    Remark.Visible = true;
                    RemarkApproved.Visible = false;
                    RemarkRejected.Visible = false;
                    lbtnRefund.Visible = false;
                    lbtnDelete.Visible = true;
                }
                if (LblStatusID.Text == "2")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.FromName("#63d590");
                    Remark.Visible = false;
                    RemarkApproved.Visible = true;
                    RemarkRejected.Visible = false;
                    lbtnRefund.Visible = true;
                    lbtnDelete.Visible = false;
                }
                if (LblStatusID.Text == "3")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.FromName("#ee4e42");
                    Remark.Visible = false;
                    RemarkApproved.Visible = false;
                    RemarkRejected.Visible = true;
                    lbtnRefund.Visible = false;
                    lbtnDelete.Visible = false;
                }
                if (LblStatusID.Text == "4")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Aqua;
                    Remark.Visible = false;
                    RemarkApproved.Visible = true;
                    RemarkRejected.Visible = false;
                    lbtnRefund.Visible = false;
                    lbtnDelete.Visible = false;
                }
            }
        }

        protected void BindGridViewTab2(int page)
        {
            try
            {
                List<StockGRNData> ObjDisReqList = GetDiscountRequestListAfterBilling(page);
                if (ObjDisReqList.Count > 0)
                {
                    GvDiscountRequestList.VirtualItemCount = ObjDisReqList[0].MaximumRows;//total item is required for custom paging
                    GvDiscountRequestList.PageIndex = page - 1;
                    GvDiscountRequestList.DataSource = ObjDisReqList;
                    GvDiscountRequestList.DataBind();
                    GvDiscountRequestList.Visible = true;
                    Messagealert_.ShowMessage(lblResultTab2, "Total:" + ObjDisReqList[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    div2.Attributes["class"] = "SucessAlert";
                    div2.Visible = true;

                }
                else
                {
                    GvDiscountRequestList.DataSource = null;
                    GvDiscountRequestList.DataBind();
                    GvDiscountRequestList.Visible = true;
                    lblResultTab2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessageTab2, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        public List<StockGRNData> GetDiscountRequestListAfterBilling(int curIndex)
        {
            StockGRNData ObjDisReqData = new StockGRNData();
            StockGRNBO ObjDisReqBO = new StockGRNBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            ObjDisReqData.ReqNo = txtTab2_RequestNo.Text == "" ? "0" : txtTab2_RequestNo.Text.Trim();
            DateTime From = txtTab2_DateFrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtTab2_DateFrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtTab2_DateTo.Text.Trim() == "" ? GlobalConstant.MaxSQLDateTime : DateTime.Parse(txtTab2_DateTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            ObjDisReqData.DateFrom = From;
            ObjDisReqData.DateTo = To;
            ObjDisReqData.PatientTypeID = Convert.ToInt32(ddlTab2_PatientType.SelectedValue == "" ? "0" : ddlTab2_PatientType.SelectedValue);
            ObjDisReqData.StatusID = Convert.ToInt32(ddlTab2_RequestStatus.SelectedValue == "" ? "0" : ddlTab2_RequestStatus.SelectedValue);
            ObjDisReqData.IsActive = ddlTab2_Status.SelectedValue == "1" ? true : false;
            return ObjDisReqBO.GetDiscountRequestListAfterBilling(ObjDisReqData);
        }
        protected void GvDiscountRequestList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindGridViewTab2(Convert.ToInt32(e.NewPageIndex + 1));
        }
        protected void btnTab2_Reset_Click(object sender, EventArgs e)
        {
            ResetTab2();
        }

        protected void ResetTab2()
        {
            txtTab2_RequestNo.Text = "";
            txtTab2_DateFrom.Text = "";
            txtTab2_DateTo.Text = "";
            ddlTab2_PatientType.SelectedValue = "0";
            ddlTab2_RequestStatus.SelectedValue = "0";
            ddlTab2_Status.SelectedValue = "1";
            GvDiscountRequestList.DataSource = null;
            GvDiscountRequestList.DataBind();
            GvDiscountRequestList.Visible = true;
            lblResultTab2.Visible = false;
            div2.Visible = false;
            divmsg2.Visible = false;
            lblmessageTab2.Visible = false;

        }

        protected void GvDiscountRequestList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Refund")
                {
                    StockGRNData ObjDisReqData = new StockGRNData();
                    StockGRNBO ObjDisReqBO = new StockGRNBO();

                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvDiscountRequestList.Rows[i];
                    Label ReqNo = (Label)gr.Cells[0].FindControl("lblTab2ReqNo");
                    Label UHID = (Label)gr.Cells[0].FindControl("lblTab2UHID");
                    Label BillNo = (Label)gr.Cells[0].FindControl("lblTab2BillNo");

                    ObjDisReqData.ReqNo = ReqNo.Text.Trim() == "" ? "0" : ReqNo.Text.Trim();
                    ObjDisReqData.UHID = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                    ObjDisReqData.BillNo = BillNo.Text.Trim();
                    List<StockGRNData> Result = ObjDisReqBO.GetDiscountApproveDetailsForRefund(ObjDisReqData);
                    if (Result.Count > 0)
                    {
                        divTab3.Visible = false;
                        lblTab3Message.Text = "";
                        lblTab3ReqNo.Text = Result[0].ReqNo.ToString();
                        lblTab3UHID.Text = Result[0].UHID.ToString();
                        lblTab3BillNo.Text = Result[0].BillNo.ToString();
                        lblTab3PatientName.Text = Result[0].PatientName.ToString();
                        lblTab3ReqBy.Text = Result[0].RequestedBy.ToString();
                        lblTab3ReqDate.Text = Result[0].RequestedDate.ToShortDateString();
                        lblTab3ApprvBy.Text = Result[0].ApprovedBy.ToString();
                        lblTab3ApprvDate.Text = Result[0].ApprovedDate.ToShortDateString();
                        lblTab3BillAmount.Text = Commonfunction.Getrounding(Result[0].TotalBillAmount.ToString());
                        lblTab3ApprvAmount.Text = Commonfunction.Getrounding(Result[0].ApprovedAmount.ToString());

                        tabcontainerDiscountRequestAfterBilling.ActiveTabIndex = 2;
                        btnTab3Save.Attributes.Remove("disabled");
                    }
                    else
                    {
                        lblTab3ReqNo.Text = "";
                        lblTab3BillNo.Text = "";
                        lblTab3PatientName.Text = "";
                        lblTab3ReqBy.Text = "";
                        lblTab3ReqDate.Text = "";
                        lblTab3ApprvBy.Text = "";
                        lblTab3ApprvDate.Text = "";
                        lblTab3BillAmount.Text = "";
                        lblTab3ApprvAmount.Text = "";
                    }
                }
                if (e.CommandName == "Deleted")
                {
                    StockGRNData ObjDisReqData = new StockGRNData();
                    StockGRNBO ObjDisReqBO = new StockGRNBO();

                    if (LogData.RoleID == 1 || LogData.RoleID == 40)
                    {
                        int i = Convert.ToInt16(e.CommandArgument.ToString());
                        GridViewRow gr = GvDiscountRequestList.Rows[i];
                        Label ReqNo = (Label)gr.Cells[0].FindControl("lblTab2ReqNo");
                        Label UHID = (Label)gr.Cells[0].FindControl("lblTab2UHID");
                        Label BillNo = (Label)gr.Cells[0].FindControl("lblTab2BillNo");
                        TextBox Remark = (TextBox)gr.Cells[0].FindControl("txtTab2Remark");

                        if (Remark.Text == "")
                        {
                            Messagealert_.ShowMessage(lblmessageTab2, "Remarks", 0);
                            divmsg2.Attributes["class"] = "FailAlert";
                            divmsg2.Visible = true;
                            Remark.Focus();
                            return;
                        }
                        else
                        {
                            ObjDisReqData.RemarksCancel = Remark.Text.Trim();
                        }
                        ObjDisReqData.ReqNo = ReqNo.Text.Trim() == "" ? "0" : ReqNo.Text.Trim();
                        ObjDisReqData.UHID = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                        ObjDisReqData.BillNo = BillNo.Text.Trim();
                        ObjDisReqData.EmployeeID = LogData.EmployeeID;
                        ObjDisReqData.HospitalID = LogData.HospitalID;

                        int Result = ObjDisReqBO.DeleteDiscountRequestByReqNo(ObjDisReqData);
                        if (Result == 1)
                        {
                            BindGridViewTab2(1);
                            Messagealert_.ShowMessage(lblmessageTab2, "delete", 1);
                            divmsg2.Attributes["class"] = "SucessAlert";
                            divmsg2.Visible = true;
                        }
                        else
                        {
                            Messagealert_.ShowMessage(lblmessageTab2, "system", 0);
                            divmsg2.Attributes["class"] = "FailAlert";
                            divmsg2.Visible = true;
                        }
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessageTab2, "DeleteEnable", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessageTab2, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }


        //--------------------------------END TAB 2-------------------------------
        //--------------------------------START TAB 3-------------------------------

        protected void btnTab3Save_Click(object sender, EventArgs e)
        {
            try
            {
                StockGRNData ObjDisReqData = new StockGRNData();
                StockGRNBO ObjDisReqBO = new StockGRNBO();

                if (ddlpaymentmode.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblTab3Message, "Paymode", 0);
                    divTab3.Visible = true;
                    divTab3.Attributes["class"] = "FailAlert";
                    txtinvoicenumber.Focus();
                    return;
                }
                if (ddlpaymentmode.SelectedIndex > 1)
                {
                    if (ddlpaymentmode.SelectedValue == "2")
                    {
                        if (txtinvoicenumber.Text == "")
                        {
                            Messagealert_.ShowMessage(lblTab3Message, "Invoicenumber", 0);
                            divTab3.Visible = true;
                            divTab3.Attributes["class"] = "FailAlert";
                            txtinvoicenumber.Focus();
                            return;
                        }
                        else
                        {
                            lblTab3Message.Visible = false;
                            divTab3.Visible = false;
                        }
                    }
                    if (ddlpaymentmode.SelectedValue == "3")
                    {
                        if (txt_chequenumber.Text == "")
                        {
                            Messagealert_.ShowMessage(lblTab3Message, "Chequenumber", 0);
                            divTab3.Visible = true;
                            divTab3.Attributes["class"] = "FailAlert";
                            txt_chequenumber.Focus();
                            return;
                        }
                        else
                        {
                            lblTab3Message.Visible = false;
                            divTab3.Visible = false;
                        }
                    }
                    if (ddlpaymentmode.SelectedValue == "4")
                    {
                        if (txtbank.Text == "")
                        {
                            Messagealert_.ShowMessage(lblTab3Message, "BankName", 0);
                            divTab3.Visible = true;
                            divTab3.Attributes["class"] = "FailAlert";
                            txtbank.Focus();
                            return;
                        }
                        else
                        {
                            lblTab3Message.Visible = false;
                            divTab3.Visible = false;
                        }
                        if (txt_chequenumber.Text == "")
                        {
                            Messagealert_.ShowMessage(lblTab3Message, "Chequenumber", 0);
                            divTab3.Visible = true;
                            divTab3.Attributes["class"] = "FailAlert";
                            txt_chequenumber.Focus();
                            return;
                        }
                        else
                        {
                            lblTab3Message.Visible = false;
                            divTab3.Visible = false;
                        }
                    }
                }
                if (txtTab3Remark.Text == "")
                {
                    Messagealert_.ShowMessage(lblTab3Message, "Remarks", 0);
                    divTab3.Visible = true;
                    divTab3.Attributes["class"] = "FailAlert";
                    txtTab3Remark.Focus();
                    return;
                }
                else
                {
                    lblTab3Message.Visible = false;
                    divTab3.Visible = false;
                }

                ObjDisReqData.ReqNo = lblTab3ReqNo.Text.Trim();
                ObjDisReqData.UHID = Convert.ToInt64(lblTab3UHID.Text.Trim());
                ObjDisReqData.BillNo = lblTab3BillNo.Text.Trim();
                ObjDisReqData.PatientName = lblTab3PatientName.Text.Trim();
                ObjDisReqData.RequestedBy = lblTab3ReqBy.Text.Trim();
                ObjDisReqData.RequestedDate = Convert.ToDateTime(lblTab3ReqDate.Text.Trim());
                ObjDisReqData.ApprovedBy = lblTab3ApprvBy.Text.Trim();
                ObjDisReqData.ApprovedDate = Convert.ToDateTime(lblTab3ApprvDate.Text.Trim());
                ObjDisReqData.TotalBillAmount = Convert.ToDecimal(lblTab3BillAmount.Text.Trim());
                ObjDisReqData.ApprovedAmount = Convert.ToDecimal(lblTab3ApprvAmount.Text.Trim());

                ObjDisReqData.Paymode = Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue);
                ObjDisReqData.BankName = txtbank.Text == "" ? null : txtbank.Text;
                ObjDisReqData.Cheque = txt_chequenumber.Text == "" ? null : txt_chequenumber.Text;
                ObjDisReqData.Invoicenumber = txtinvoicenumber.Text == "" ? null : txtinvoicenumber.Text;
                ObjDisReqData.Remarks = txtTab3Remark.Text.Trim();

                ObjDisReqData.EmployeeID = LogData.EmployeeID;
                ObjDisReqData.HospitalID = LogData.HospitalID;
                ObjDisReqData.FinancialYearID = LogData.FinancialYearID;

                string Result = ObjDisReqBO.SaveRefundDiscountRequest(ObjDisReqData);
                if (Result != "0")
                {
                    lblTab3RefundNo.Text = Result;
                    btnTab3Save.Attributes["disabled"] = "disabled";
                    btnTab3Print.Attributes.Remove("disabled");
                    BindGridViewTab2(1);
                    Messagealert_.ShowMessage(lblTab3Message, "Refund Successfully", 1);
                    divTab3.Attributes["class"] = "SucessAlert";
                    divTab3.Visible = true;
                }
                else
                {
                    Messagealert_.ShowMessage(lblTab3Message, "system", 0);
                    divTab3.Attributes["class"] = "FailAlert";
                    divTab3.Visible = true;
                }

            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblTab3Message, "system", 0);
                divTab3.Attributes["class"] = "FailAlert";
                divTab3.Visible = true;
            }
        }
        protected void btnTab3Print_Click(object sender, EventArgs e)
        {
            if (LogData.PrintEnable == 0)
            {
                Messagealert_.ShowMessage(lblTab3Message, "PrintEnable", 0);
                divTab3.Visible = true;
                divTab3.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }

            String RefundNo = lblTab3RefundNo.Text.Trim() == "" ? "0" : lblTab3RefundNo.Text.Trim();
            String ReqNo = lblTab3ReqNo.Text.Trim() == "" ? "0" : lblTab3ReqNo.Text.Trim();
            string url = "../MedPhr/Reports/ReportViewer.aspx?option=PHRRefundAfterBilling&RefundNo=" + RefundNo + "&ReqNo=" + ReqNo;
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
        protected void btnTab3Reset_Click(object sender, EventArgs e)
        {
            ResetTab3();
        }

        protected void ResetTab3()
        {
            lblTab3ReqNo.Text = "";
            lblTab3UHID.Text = "";
            lblTab3BillNo.Text = "";
            lblTab3PatientName.Text = "";
            lblTab3ReqBy.Text = "";
            lblTab3ReqDate.Text = "";
            lblTab3ApprvBy.Text = "";
            lblTab3ApprvDate.Text = "";
            lblTab3BillAmount.Text = "";
            lblTab3ApprvAmount.Text = "";
            lblTab3RefundNo.Text = "";
            divTab3.Visible = false;
            lblTab3Message.Text = "";
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
        //--------------------------------END TAB 3-------------------------------
        //--------------------------------START TAB 4-------------------------------
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetRFNumberAuto(string prefixText, int count, string contextKey)
        {
            StockGRNData ObjDisReqList = new StockGRNData();
            StockGRNBO ObjDisReqBO = new StockGRNBO();
            List<StockGRNData> getResult = new List<StockGRNData>();
            ObjDisReqList.RefundNo = prefixText;
            getResult = ObjDisReqBO.GetRFNumberAuto(ObjDisReqList);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].RefundNo.ToString());
            }
            return list;
        }
        protected void txtTab4_RefundNo_TextChanged(object sender, EventArgs e)
        {
            StockGRNData ObjDisReqData = new StockGRNData();
            StockGRNBO ObjDisReqBO = new StockGRNBO();
            List<StockGRNData> getResult = new List<StockGRNData>();
            bool IsNumeric = txtTab4_RefundNo.Text.All(char.IsDigit);
            if (IsNumeric == false)
            {
                if (txtTab4_RefundNo.Text.Contains(">"))
                {
                    ObjDisReqData.RefundNo = txtTab4_RefundNo.Text.Contains(">") ? txtTab4_RefundNo.Text.Substring(txtTab4_RefundNo.Text.LastIndexOf('>') + 1).Trim() : "0";                    
                }
                else
                {
                    txtTab4_RefundNo.Text = "";
                    txtTab4_RefundNo.Focus();
                    return;
                }
            }
            BindGridViewTab4(1);
        }
        protected void ddlTab4_PatientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridViewTab4(1);
        }

        protected void btnTab4_Search_Click(object sender, EventArgs e)
        {
            BindGridViewTab4(1);
        }
        protected void BindGridViewTab4(int page)
        {
            try
            {
                List<StockGRNData> ObjDisReqList = GetRefundListForAfterBiling(page);
                if (ObjDisReqList.Count > 0)
                {
                    GvRefundList.VirtualItemCount = ObjDisReqList[0].MaximumRows;//total item is required for custom paging
                    GvRefundList.PageIndex = page - 1;
                    GvRefundList.DataSource = ObjDisReqList;
                    GvRefundList.DataBind();
                    GvRefundList.Visible = true;
                    Messagealert_.ShowMessage(lblTab4Result, "Total:" + ObjDisReqList[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divTab4Result.Attributes["class"] = "SucessAlert";
                    divTab4Result.Visible = true;

                }
                else
                {
                    GvRefundList.DataSource = null;
                    GvRefundList.DataBind();
                    GvRefundList.Visible = true;
                    divTab4Result.Visible = false;
                    lblTab4Result.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblTab4Result, "system", 0);
                divTab4Result.Attributes["class"] = "FailAlert";
                divTab4Result.Visible = true;
            }
        }
        public List<StockGRNData> GetRefundListForAfterBiling(int curIndex)
        {
            StockGRNData ObjDisReqData = new StockGRNData();
            StockGRNBO ObjDisReqBO = new StockGRNBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            if (txtTab4_RefundNo.Text.Contains(">"))
            {
                ObjDisReqData.RefundNo = txtTab4_RefundNo.Text.Contains(">") ? txtTab4_RefundNo.Text.Substring(txtTab4_RefundNo.Text.LastIndexOf('>') + 1).Trim() : "0";
            }
            else
            {
                ObjDisReqData.RefundNo = "0";
            }
            DateTime From = txtTab4_DateFrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtTab4_DateFrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtTab4_DateTo.Text.Trim() == "" ? GlobalConstant.MaxSQLDateTime : DateTime.Parse(txtTab4_DateTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            ObjDisReqData.DateFrom = From;
            ObjDisReqData.DateTo = To;
            ObjDisReqData.PatientTypeID = Convert.ToInt32(ddlTab4_PatientType.SelectedValue == "" ? "0" : ddlTab4_PatientType.SelectedValue);
            ObjDisReqData.IsActive = ddlTab4_Status.SelectedValue == "1" ? true : false;
            return ObjDisReqBO.GetRefundListForAfterBiling(ObjDisReqData);
        }
        protected void GvRefundList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindGridViewTab4(Convert.ToInt32(e.NewPageIndex + 1));
        }

        protected void GvRefundList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    StockGRNData ObjDisReqData = new StockGRNData();
                    StockGRNBO ObjDisReqBO = new StockGRNBO();
                    if (LogData.RoleID == 1 || LogData.RoleID == 40)
                    {
                        int i = Convert.ToInt16(e.CommandArgument.ToString());
                        GridViewRow gr = GvRefundList.Rows[i];
                        Label RefundNo = (Label)gr.Cells[0].FindControl("lblTab4RefundNo");
                        Label ReqNo = (Label)gr.Cells[0].FindControl("lblTab4ReqNo");
                        Label UHID = (Label)gr.Cells[0].FindControl("lblTab4UHID");
                        Label BillNo = (Label)gr.Cells[0].FindControl("lblTab4BillNo");
                        TextBox Remark = (TextBox)gr.Cells[0].FindControl("txtTab4Remark");

                        if (Remark.Text == "")
                        {
                            Messagealert_.ShowMessage(lblTab4Message, "Remarks", 0);
                            divTab4Message.Attributes["class"] = "FailAlert";
                            divTab4Message.Visible = true;
                            Remark.Focus();
                            return;
                        }
                        else
                        {
                            ObjDisReqData.Remarks = Remark.Text.Trim();
                        }
                        ObjDisReqData.RefundNo = RefundNo.Text.Trim() == "" ? "0" : RefundNo.Text.Trim();
                        ObjDisReqData.ReqNo = ReqNo.Text.Trim() == "" ? "0" : ReqNo.Text.Trim();
                        ObjDisReqData.UHID = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                        ObjDisReqData.BillNo = BillNo.Text.Trim();
                        ObjDisReqData.EmployeeID = LogData.EmployeeID;
                        ObjDisReqData.HospitalID = LogData.HospitalID;

                        int Result = ObjDisReqBO.DeleteRefundDiscountAfterBillingByReqNo(ObjDisReqData);
                        if (Result == 1)
                        {
                            BindGridViewTab2(1);
                            Messagealert_.ShowMessage(lblTab4Message, "delete", 1);
                            divTab4Message.Attributes["class"] = "SucessAlert";
                            divTab4Message.Visible = true;
                        }
                        else
                        {
                            Messagealert_.ShowMessage(lblTab4Message, "system", 0);
                            divTab4Message.Attributes["class"] = "FailAlert";
                            divTab4Message.Visible = true;
                        }
                    }
                }
                if (e.CommandName == "Print")
                {
                    StockGRNData ObjDisReqData = new StockGRNData();
                    StockGRNBO ObjDisReqBO = new StockGRNBO();

                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvRefundList.Rows[i];
                    Label RefundNumber = (Label)gr.Cells[0].FindControl("lblTab4RefundNo");
                    Label ReqNumber = (Label)gr.Cells[0].FindControl("lblTab4ReqNo");

                    if (LogData.PrintEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblTab3Message, "PrintEnable", 0);
                        divTab3.Visible = true;
                        divTab3.Attributes["class"] = "FailAlert";
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }

                    String RefundNo = RefundNumber.Text.Trim() == "" ? "0" : RefundNumber.Text.Trim();
                    String ReqNo = ReqNumber.Text.Trim() == "" ? "0" : ReqNumber.Text.Trim();
                    string url = "../MedPhr/Reports/ReportViewer.aspx?option=PHRRefundAfterBilling&RefundNo=" + RefundNo + "&ReqNo=" + ReqNo;
                    string fullURL = "window.open('" + url + "', '_blank');";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblTab4Message, "system", 0);
                divTab4Message.Attributes["class"] = "FailAlert";
                divTab4Message.Visible = true;
            }
        }


        //--------------------------------END TAB 4-------------------------------
    }
}