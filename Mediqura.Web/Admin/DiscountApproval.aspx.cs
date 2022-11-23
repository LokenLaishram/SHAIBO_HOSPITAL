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
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.Admin
{
    public partial class DiscountApproval : BasePage
    {
        public static int reqNo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ddlbind();

            }
        }

        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_discount_status, mstlookup.GetLookupsList(LookupName.DiscountStatus));
            Commonfunction.PopulateDdl(ddl_requested_by, mstlookup.GetLookupsList(LookupName.DiscountReqBy));
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txt_billNo.Text = "";
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetBillNoByServiceType(string prefixText, int count, string contextKey)
        {
            DiscountRequestData ObjData = new DiscountRequestData();
            DiscountBO objBO = new DiscountBO();
            List<DiscountRequestData> getResult = new List<DiscountRequestData>();
            ObjData.serviceTypeID = Convert.ToInt32(contextKey);
            ObjData.BillNo = prefixText;
            getResult = objBO.GetBillNoByServiceType(ObjData);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].BillNo.ToString());
            }
            return list;
        }
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
        protected void ddl_serviceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_serviceType.SelectedIndex == 0)
            {
                txt_billNo.ReadOnly = true;
            }
            else
            {
                txt_billNo.ReadOnly = false;
                autoConpleteBill.ContextKey = ddl_serviceType.SelectedValue;
            }
        }
        public void bindgrid()
        {
            DiscountBO objBo = new DiscountBO();
            DiscountRequestListData objData = new DiscountRequestListData();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objData.serviceTypeID = Convert.ToInt32(ddl_serviceType.SelectedValue == "0" ? "0" : ddl_serviceType.SelectedValue);
            objData.BillNo = txt_billNo.Text.Trim() == "" ? "" : txt_billNo.Text;
            objData.UHID = Convert.ToInt64(txt_UHID.Text.Trim() == "" ? "0" : txt_UHID.Text);
            objData.PatName = txt_name.Text.Trim() == "" ? "" : txt_name.Text;
            objData.PatientAddress = txt_address.Text.Trim() == "" ? "" : txt_address.Text;
            objData.DisStatus = Convert.ToInt32(ddl_discount_status.SelectedValue);
            objData.RequestedBy = Convert.ToInt32(ddl_requested_by.SelectedValue);
            DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            string datefrom = from.ToString("yyyy-MM-dd");
            string dateto = To.ToString("yyyy-MM-dd");

            from = Convert.ToDateTime(datefrom + " " + "12:01:00 AM");
            To = Convert.ToDateTime(dateto + " " + "11:59:00 PM");
            objData.Datefrom = from;
            objData.DateTo = To;

            List<DiscountRequestListData> listDiscount = objBo.GetDiscountListAdmin(objData);
            if (listDiscount.Count > 0)
            {

                GVDiscountList.DataSource = listDiscount;
                GVDiscountList.DataBind();
                GVDiscountList.Visible = true;
                div2.Visible = true;
                lblmessage2.Visible = true;
                Messagealert_.ShowMessage(lblmessage2, "Total:" + listDiscount[0].MaximumRows.ToString() + " Record(s) found", 1);
                div2.Attributes["class"] = "SucessAlert";
                txt_total_approve_amount.Text = Commonfunction.Getrounding(listDiscount[0].TotalApprove.ToString());
                txt_total_requested.Text = Commonfunction.Getrounding(listDiscount[0].TotalDiscount.ToString());
                txt_total_on_req.Text = listDiscount[0].TotalonRequest.ToString();
                txt_total_approval.Text = listDiscount[0].TotalonApprove.ToString();
            }
            else
            {
                GVDiscountList.DataSource = null;
                GVDiscountList.DataBind();
                GVDiscountList.Visible = true;
                div2.Visible = false;
                lblmessage2.Visible = false;
            }

        }
        protected void GVDiscountList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDisStatus = (Label)e.Row.FindControl("lblDisStatus");
                Label lblRequestNo = (Label)e.Row.FindControl("lblRequestNo");
                LinkButton lblRequest = (LinkButton)e.Row.FindControl("lblRequest");

                int reqLength = 5;
                int reqNo = Convert.ToInt32(lblRequestNo.Text);
                String RequestNo = "RQ";
                String zero = "";
                for (int i = reqNo.ToString().Length; i <= reqLength; i++)
                {
                    zero = zero + "0";
                }
                RequestNo = RequestNo + zero + reqNo.ToString();
                lblRequest.Text = RequestNo;

                switch (Convert.ToInt32(lblDisStatus.Text))
                {
                    case 1:
                        lblRequest.BackColor = Color.Aqua;
                        break;
                    case 2:
                        lblRequest.BackColor = Color.Yellow;
                        break;
                    case 3:
                        lblRequest.BackColor = Color.LightGreen;
                        break;
                    case 4:
                        lblRequest.BackColor = Color.Red;
                        break;

                }

            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            bindgrid();
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            txt_address.Text = "";
            ddl_serviceType.SelectedIndex = 0;
            txt_billNo.Text = "";
            txt_UHID.Text = "";
            txt_name.Text = "";
            ddl_discount_status.SelectedIndex = 0;
            ddl_requested_by.SelectedIndex = 0;
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            GVDiscountList.DataSource = null;
            GVDiscountList.DataBind();
            txt_total_on_req.Text = "";
            txt_total_requested.Text = "0.00";
            txt_total_approval.Text = "";
            txt_total_approve_amount.Text = "0.00";
            lblMessage.Visible = false;

        }
        public void bindGridPopup(Int32 reqNo)
        {
            ddl_set_discount_status.SelectedIndex = 0;
            txt_remarks.Text = "";
            divpopup.Visible = false;
            try
            {
                List<DiscountRequestServiceData> ServiceList = new List<DiscountRequestServiceData>();
                ServiceList = GetServiceList(reqNo);
                if (ServiceList.Count > 0)
                {
                    GVDiscount.DataSource = ServiceList;
                    GVDiscount.DataBind();
                    GVDiscount.Visible = true;
                    txt_pop_uhid.Text = ServiceList[0].UHID.ToString();
                    txt_pop_name.Text = ServiceList[0].PatName.ToString();
                    txt_pop_address.Text = ServiceList[0].PatientAddress.ToString();
                    txt_pop_remarks.Text = ServiceList[0].Remarks.ToString();
                    txt_total_Amount.Text = Commonfunction.Getrounding(ServiceList[0].TotalAmount.ToString());
                    txt_total_discount.Text = Commonfunction.Getrounding(ServiceList[0].TotalDiscount.ToString());
                    txt_total_net_amount.Text = Commonfunction.Getrounding(ServiceList[0].TotalNetAmount.ToString());
                }
                else
                {
                    GVDiscount.DataSource = null;
                    GVDiscount.DataBind();
                    GVDiscount.Visible = true;


                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblMessage, "system", 0);
            }
        }
        public List<DiscountRequestServiceData> GetServiceList(Int32 curIndex)
        {

            DiscountRequestData ObjData = new DiscountRequestData();
            DiscountBO objBO = new DiscountBO();
            ObjData.RequestNo = curIndex;
            return objBO.GetServiceListByReqNo(ObjData);

        }
        protected void ddl_discount_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.MDDiscount.Show();
            GridViewRow currentRow = ((GridViewRow)((DropDownList)sender).NamingContainer);
            discountChange(sender, currentRow);

        }
        public void discountChange(object sender, GridViewRow currentRow)
        {

            divpopup.Visible = false;
            DropDownList ddl_discount_type = (DropDownList)currentRow.FindControl("ddl_discount_type");
            TextBox txt_dis_value = (TextBox)currentRow.FindControl("txt_dis_value");
            Label lblNetAmount = (Label)currentRow.FindControl("lblNetAmount");
            Label lbl_discount_amt = (Label)currentRow.FindControl("lbl_discount_amt");
            decimal value = Convert.ToDecimal(txt_dis_value.Text == "" ? "0" : txt_dis_value.Text);
            decimal NetAmount = Convert.ToDecimal(lblNetAmount.Text == "" ? "0" : lblNetAmount.Text);

            if (ddl_discount_type.SelectedIndex == 0)
            {
                if (value > NetAmount)
                {
                    txt_dis_value.Text = "";
                    lbl_discount_amt.Text = "";
                    Messagealert_.ShowMessage(lblMessagePopup, "DiscountAmount", 0);
                    divpopup.Visible = true;
                    divpopup.Attributes["class"] = "FailAlert";
                    totalCalculate();
                }
                else
                {
                    lbl_discount_amt.Text = Commonfunction.Getrounding(value.ToString());
                    totalCalculate();
                }
            }
            else
            {
                if (value > 100)
                {
                    txt_dis_value.Text = "";
                    lbl_discount_amt.Text = "";
                    Messagealert_.ShowMessage(lblMessagePopup, "Percentage", 0);
                    divpopup.Visible = true;
                    divpopup.Attributes["class"] = "FailAlert";
                    totalCalculate();

                }
                else
                {

                    decimal pcValue = 0;

                    pcValue = ((value / 100) * NetAmount);
                    lbl_discount_amt.Text = Commonfunction.Getrounding(pcValue.ToString());
                    totalCalculate();
                }

            }

        }
        public void totalCalculate()
        {
            decimal totalDiscount = 0;
            foreach (GridViewRow row in GVDiscount.Rows)
            {
                CheckBox discountCheck = (CheckBox)GVDiscount.Rows[row.RowIndex].Cells[0].FindControl("discountCheck");
                if (discountCheck.Checked == true)
                {
                    Label lbl_discount_amt = (Label)GVDiscount.Rows[row.RowIndex].Cells[0].FindControl("lbl_discount_amt");
                    totalDiscount = totalDiscount + (Convert.ToDecimal(lbl_discount_amt.Text == "" ? "0" : lbl_discount_amt.Text));
                }
            }

            decimal TotalAmount = Convert.ToDecimal(txt_total_Amount.Text == "" ? "0" : txt_total_Amount.Text.ToString());
            txt_total_discount.Text = Commonfunction.Getrounding(totalDiscount.ToString());
            txt_total_net_amount.Text = Commonfunction.Getrounding((TotalAmount - Convert.ToDecimal(txt_total_discount.Text == "" ? "0" : txt_total_discount.Text)).ToString());


        }
        protected void txt_dis_value_TextChanged(object sender, EventArgs e)
        {

            GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
            discountChange(sender, currentRow);
            this.MDDiscount.Show();
        }
        protected void GVDiscountList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "View")
                {

                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GVDiscountList.Rows[i];
                    Label lblRequestNo = (Label)gr.Cells[0].FindControl("lblRequestNo");
                    reqNo = Convert.ToInt32(lblRequestNo.Text);
                    Label lblDisStatus = (Label)gr.Cells[0].FindControl("lblDisStatus");
                    if (lblDisStatus.Text == "1")
                    {
                        this.MDDiscount.Show();
                        bindGridPopup(Convert.ToInt32(lblRequestNo.Text));
                    }
                    else if (lblDisStatus.Text == "2")
                    {
                        this.MDApproval.Show();
                        bindGridPopupAppr(Convert.ToInt32(lblRequestNo.Text));
                    }
                    else
                    {


                    }


                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                div2.Attributes["class"] = "FailAlert";
                div2.Visible = true;
            }
        }

        protected void GVDiscount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDisType = (Label)e.Row.FindControl("lblDisType");
                Label lblisDis = (Label)e.Row.FindControl("lblisDis");
                CheckBox discountCheck = (CheckBox)e.Row.FindControl("discountCheck");
                TextBox txt_dis_value = (TextBox)e.Row.FindControl("txt_dis_value");
                DropDownList ddl_discount_type = (DropDownList)e.Row.FindControl("ddl_discount_type");
                ddl_discount_type.SelectedValue = Commonfunction.Getrounding(lblDisType.Text);
                if (lblisDis.Text == "0")
                {
                    ddl_discount_type.Attributes["disabled"] = "disabled";
                    discountCheck.Checked = false;
                    txt_dis_value.ReadOnly = true;
                    txt_dis_value.Text = "";

                }
                else
                {
                    ddl_discount_type.Attributes.Remove("disabled");
                    discountCheck.Checked = true;
                    txt_dis_value.ReadOnly = false;
                }
            }
        }

        protected void btnPopReset_Click(object sender, EventArgs e)
        {
            bindGridPopup(reqNo);
            this.MDDiscount.Show();
        }

        protected void btnPopSave_Click(object sender, EventArgs e)
        {
            this.MDDiscount.Show();
            if (ddl_set_discount_status.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblMessagePopup, "RequestStatus", 0);
                divpopup.Visible = true;
                divpopup.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                divpopup.Visible = false;
                lblMessagePopup.Visible = false;
            }
            if (txt_remarks.Text == "")
            {
                Messagealert_.ShowMessage(lblMessagePopup, "Please enter remarks for approval", 0);
                divpopup.Attributes["class"] = "FailAlert";
                divpopup.Visible = true;
                txt_remarks.Focus();
                return;
            }
            else
            {
                divpopup.Visible = false;
                lblMessagePopup.Visible = false;
            }
            List<DiscountRequestServiceData> Listobjdata = new List<DiscountRequestServiceData>();
            DiscountRequestServiceData objdata = new DiscountRequestServiceData();
            DiscountBO objstdBO = new DiscountBO();

            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in GVDiscount.Rows)
                {
                    Label lblServiceID = (Label)GVDiscount.Rows[row.RowIndex].Cells[0].FindControl("lblServiceID");
                    Label lblBillNo = (Label)GVDiscount.Rows[row.RowIndex].Cells[0].FindControl("lblBillNo");
                    Label lblUHID = (Label)GVDiscount.Rows[row.RowIndex].Cells[0].FindControl("lblUHID");
                    Label lblisDis = (Label)GVDiscount.Rows[row.RowIndex].Cells[0].FindControl("lblisDis");
                    Label lblServiceType = (Label)GVDiscount.Rows[row.RowIndex].Cells[0].FindControl("lblServiceType");
                    DropDownList ddl_discount_type = (DropDownList)GVDiscount.Rows[row.RowIndex].Cells[0].FindControl("ddl_discount_type");
                    TextBox txt_dis_value = (TextBox)GVDiscount.Rows[row.RowIndex].Cells[0].FindControl("txt_dis_value");
                    Label lbl_discount_amt = (Label)GVDiscount.Rows[row.RowIndex].Cells[0].FindControl("lbl_discount_amt");
                    CheckBox discountCheck = (CheckBox)GVDiscount.Rows[row.RowIndex].Cells[0].FindControl("discountCheck");
                    DiscountRequestServiceData objsubdata = new DiscountRequestServiceData();
                    objsubdata.serviceTypeID = Convert.ToInt32(lblServiceType.Text.Trim() == "" ? "0" : lblServiceType.Text.Trim());
                    objsubdata.BillNo = lblBillNo.Text.Trim() == "" ? "0" : lblBillNo.Text.Trim();
                    objsubdata.UHID = Convert.ToInt64(lblUHID.Text.Trim() == "" ? "0" : lblUHID.Text.Trim());
                    txt_UHID.Text = lblUHID.Text.Trim();
                    objsubdata.isDis = Convert.ToInt32(discountCheck.Checked == true ? 1 : 0);
                    objsubdata.ServiceID = Convert.ToInt32(lblServiceID.Text == "" ? "0" : lblServiceID.Text);
                    objsubdata.DisType = Convert.ToInt32(ddl_discount_type.SelectedValue == "" ? "0" : ddl_discount_type.SelectedValue);
                    objsubdata.DisValue = Convert.ToDecimal(txt_dis_value.Text == "" ? "0" : txt_dis_value.Text);
                    objsubdata.DisAmount = Convert.ToDecimal(lbl_discount_amt.Text == "" ? "0" : lbl_discount_amt.Text);
                    txt_billNo.Text = lblBillNo.Text;
                    Listobjdata.Add(objsubdata);

                }
                objdata.XMLData = XmlConvertor.DiscountApprovalToXML(Listobjdata).ToString();
                objdata.TotalAmount = Convert.ToDecimal(txt_total_Amount.Text == "" ? "0" : txt_total_Amount.Text);
                objdata.TotalDiscount = Convert.ToDecimal(txt_total_discount.Text == "" ? "0" : txt_total_discount.Text);
                objdata.EmployeeID = LogData.EmployeeID;
                objdata.Remarks = txt_remarks.Text;
                objdata.DisStatus = Convert.ToInt32(ddl_set_discount_status.SelectedValue);
                objdata.RequestNo = reqNo;
                int result = objstdBO.UpdateDiscoutApproval(objdata);
                if (result > 0)
                {
                    if (result == 1)
                    {
                        Messagealert_.ShowMessage(lblMessagePopup, "save", 1);
                        divpopup.Visible = true;
                        divpopup.Attributes["class"] = "SucessAlert";
                        bindgrid();
                        txt_UHID.Text = "";
                        //ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>pushMessage('" + txt_remarks.Text + "');</script>", false);

                    }
                }
                else
                {

                    Messagealert_.ShowMessage(lblMessagePopup, "Error", 0);
                    divpopup.Visible = true;
                    divpopup.Attributes["class"] = "FailAlert";
                }

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);

            }

        }

        protected void discountCheck_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = ((GridViewRow)((CheckBox)sender).NamingContainer);
            discountChange(sender, currentRow);
            DropDownList ddl_discount_type = (DropDownList)currentRow.FindControl("ddl_discount_type");
            TextBox txt_dis_value = (TextBox)currentRow.FindControl("txt_dis_value");
            CheckBox discountCheck = (CheckBox)currentRow.FindControl("discountCheck");

            if (discountCheck.Checked == true)
            {
                ddl_discount_type.Attributes.Remove("disabled");
                txt_dis_value.ReadOnly = false;
            }
            else
            {
                txt_dis_value.Text = "";
                ddl_discount_type.Attributes["disabled"] = "disabled";
                txt_dis_value.ReadOnly = true;
            }

            this.MDDiscount.Show();
        }

        protected void btnOnLoad_Click(object sender, EventArgs e)
        {
            if (Session["REQID"] != null)
            {
                int ID = Convert.ToInt32(Session["REQID"].ToString());
                reqNo = ID;
                Session["REQID"] = null;
                this.MDDiscount.Show();
                bindGridPopup(ID);
            }
        }

        protected void btnPopapprReset_Click(object sender, EventArgs e)
        {
            bindGridPopupAppr(reqNo);
            totalShareCalculate();
            this.MDApproval.Show();
            Response.Redirect("~/DiscountApproval.aspx", false);
        }

        protected void btnPopApprSave_Click(object sender, EventArgs e)
        {
            this.MDApproval.Show();
            string billId = "0";
            string billType = "0";
            string ServiceType = "0";
            decimal totalDiscount = Convert.ToDecimal(txt_appr_tot_discount.Text.Trim() == "" ? "0" : txt_appr_tot_discount.Text);
            decimal TotalShare = Convert.ToDecimal(txt_total_share.Text.Trim() == "" ? "0" : txt_total_share.Text);
            if (Decimal.Round(totalDiscount, 2) != Decimal.Round(TotalShare, 2))
            {
                Messagealert_.ShowMessage(lblmessagepop1, "Total Share must equal to total discount!", 0);
                divpop1.Visible = true;
                divpop1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                divpop1.Visible = false;
                lblmessagepop1.Visible = false;
            }

            List<DiscountRequestServiceData> Listobjdata = new List<DiscountRequestServiceData>();
            DiscountRequestServiceData objdata = new DiscountRequestServiceData();
            DiscountBO objstdBO = new DiscountBO();

            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in GVDiscountApproveList.Rows)
                {
                    Label lblApprServiceID = (Label)GVDiscountApproveList.Rows[row.RowIndex].Cells[0].FindControl("lblApprServiceID");
                    Label lblApprBillNo = (Label)GVDiscountApproveList.Rows[row.RowIndex].Cells[0].FindControl("lblApprBillNo");
                    Label lblApprUHID = (Label)GVDiscountApproveList.Rows[row.RowIndex].Cells[0].FindControl("lblApprUHID");
                    Label lblApprisDis = (Label)GVDiscountApproveList.Rows[row.RowIndex].Cells[0].FindControl("lblApprisDis");
                    Label lblApprServiceType = (Label)GVDiscountApproveList.Rows[row.RowIndex].Cells[0].FindControl("lblApprServiceType");
                    Label lblApprNetAmount = (Label)GVDiscountApproveList.Rows[row.RowIndex].Cells[0].FindControl("lblApprAmount");
                    Label lblBilltype = (Label)GVDiscountApproveList.Rows[row.RowIndex].Cells[0].FindControl("lblBilltype");
                    DropDownList ddl_appr_discount_type = (DropDownList)GVDiscountApproveList.Rows[row.RowIndex].Cells[0].FindControl("ddl_appr_discount_type");
                    TextBox txt_appr_hos_value = (TextBox)GVDiscountApproveList.Rows[row.RowIndex].Cells[0].FindControl("txt_appr_hos_value");
                    TextBox txt_appr_doc_value = (TextBox)GVDiscountApproveList.Rows[row.RowIndex].Cells[0].FindControl("txt_appr_doc_value");
                    TextBox txt_appr_ref_value = (TextBox)GVDiscountApproveList.Rows[row.RowIndex].Cells[0].FindControl("txt_appr_ref_value");

                    DiscountRequestServiceData objsubdata = new DiscountRequestServiceData();
                    objsubdata.serviceTypeID = Convert.ToInt32(lblApprServiceType.Text.Trim() == "" ? "0" : lblApprServiceType.Text.Trim());
                    objsubdata.BillNo = lblApprBillNo.Text.Trim() == "" ? "0" : lblApprBillNo.Text.Trim();
                    objsubdata.UHID = Convert.ToInt64(lblApprUHID.Text.Trim() == "" ? "0" : lblApprUHID.Text.Trim());
                    objsubdata.isDis = Convert.ToInt32(lblApprisDis.Text == "" ? "0" : lblApprisDis.Text);
                    objsubdata.ServiceID = Convert.ToInt32(lblApprServiceID.Text == "" ? "0" : lblApprServiceID.Text);
                    objsubdata.DisType = Convert.ToInt32(ddl_appr_discount_type.SelectedValue == "" ? "0" : ddl_appr_discount_type.SelectedValue);
                    objsubdata.HosShare = Convert.ToDecimal(txt_appr_hos_value.Text == "" ? "0" : txt_appr_hos_value.Text);
                    objsubdata.DocShare = Convert.ToDecimal(txt_appr_doc_value.Text == "" ? "0" : txt_appr_doc_value.Text);
                    objsubdata.RefShare = Convert.ToDecimal(txt_appr_ref_value.Text == "" ? "0" : txt_appr_ref_value.Text);
                    objsubdata.NetAmount = Convert.ToDecimal(lblApprNetAmount.Text == "" ? "0" : lblApprNetAmount.Text);
                    Listobjdata.Add(objsubdata);
                    billId = lblApprBillNo.Text.Trim() == "" ? "0" : lblApprBillNo.Text.Trim();
                    billType = lblBilltype.Text;
                    ServiceType = lblApprServiceType.Text;
                }
                objdata.XMLData = XmlConvertor.DiscountApprovalListToXML(Listobjdata).ToString();
                objdata.EmployeeID = LogData.EmployeeID;
                objdata.RequestNo = reqNo;
                int result = objstdBO.UpdateApprovalShare(objdata);
                if (result > 0)
                {
                    if (result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessagepop1, "save", 1);
                        divpop1.Visible = true;
                        divpop1.Attributes["class"] = "SucessAlert";
                        if (ddl_set_discount_status.SelectedIndex > 0)
                            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>pushMessageToCounter('Approved','Front Desk','" + txt_remarks.Text + "','" + ServiceType + "','" + billId + "','" + billType + "');</script>", false);

                    }
                }
                else
                {

                    Messagealert_.ShowMessage(lblmessagepop1, "Error", 0);
                    divpop1.Visible = true;
                    divpop1.Attributes["class"] = "FailAlert";
                }

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);

            }
        }

        protected void txt_appr_ref_value_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
            ShareChange(sender, currentRow);
            this.MDApproval.Show();
            this.Focus();
        }

        protected void txt_appr_doc_value_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
            ShareChange(sender, currentRow);
            this.MDApproval.Show();
            this.Focus();
        }

        protected void txt_appr_hos_value_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
            ShareChange(sender, currentRow);
            this.MDApproval.Show();
            this.Focus();
        }

        protected void GVDiscountApproveList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblApprisDis = (Label)e.Row.FindControl("lblApprisDis");
                TextBox txt_appr_hos_value = (TextBox)e.Row.FindControl("txt_appr_hos_value");
                TextBox txt_appr_doc_value = (TextBox)e.Row.FindControl("txt_appr_doc_value");
                TextBox txt_appr_ref_value = (TextBox)e.Row.FindControl("txt_appr_ref_value");
                DropDownList ddl_appr_discount_type = (DropDownList)e.Row.FindControl("ddl_appr_discount_type");
                if (lblApprisDis.Text == "0")
                {
                    txt_appr_hos_value.ReadOnly = true;
                    txt_appr_doc_value.ReadOnly = true;
                    txt_appr_ref_value.ReadOnly = true;
                    ddl_appr_discount_type.Attributes["disabled"] = "disabled";
                }
            }
        }
        public void bindGridPopupAppr(Int32 reqNo)
        {

            divpop1.Visible = false;
            try
            {
                List<DiscountRequestServiceData> ServiceList = new List<DiscountRequestServiceData>();
                ServiceList = GetApprovalList(reqNo);
                if (ServiceList.Count > 0)
                {
                    GVDiscountApproveList.DataSource = ServiceList;
                    GVDiscountApproveList.DataBind();
                    GVDiscountApproveList.Visible = true;
                    txt_pop_appr_uhid.Text = ServiceList[0].UHID.ToString();
                    txt_pop_appr_name.Text = ServiceList[0].PatName.ToString();
                    txt_pop_appr_address.Text = ServiceList[0].PatientAddress.ToString();
                    txt_pop_appr_req_remarks.Text = ServiceList[0].Remarks.ToString();
                    txt_pop_appr_ap_remark.Text = ServiceList[0].AckRemarks.ToString();
                    txt_appr_tot_amount.Text = Commonfunction.Getrounding(ServiceList[0].TotalAmount.ToString());
                    txt_appr_tot_discount.Text = Commonfunction.Getrounding(ServiceList[0].TotalDiscount.ToString());
                    txt_appr_tot_net_amount.Text = Commonfunction.Getrounding(ServiceList[0].TotalNetAmount.ToString());
                }
                else
                {
                    GVDiscountApproveList.DataSource = null;
                    GVDiscountApproveList.DataBind();
                    GVDiscountApproveList.Visible = true;


                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblMessage, "system", 0);
            }
        }
        public List<DiscountRequestServiceData> GetApprovalList(Int32 curIndex)
        {

            DiscountRequestData ObjData = new DiscountRequestData();
            DiscountBO objBO = new DiscountBO();
            ObjData.RequestNo = curIndex;
            return objBO.GetApprovalServiceListByReqNo(ObjData);

        }
        public void ShareChange(object sender, GridViewRow currentRow)
        {

            divpop1.Visible = false;
            DropDownList ddl_appr_discount_type = (DropDownList)currentRow.FindControl("ddl_appr_discount_type");
            TextBox txt_appr_hos_value = (TextBox)currentRow.FindControl("txt_appr_hos_value");
            TextBox txt_appr_doc_value = (TextBox)currentRow.FindControl("txt_appr_doc_value");
            TextBox txt_appr_ref_value = (TextBox)currentRow.FindControl("txt_appr_ref_value");
            Label lblApprAmount = (Label)currentRow.FindControl("lblApprAmount");
            Label lbl_appr_discount_amt = (Label)currentRow.FindControl("lbl_appr_discount_amt");
            decimal value = 0;
            decimal NetAmount = Convert.ToDecimal(lblApprAmount.Text.Trim() == "" ? "0" : lblApprAmount.Text);
            decimal hospital = Convert.ToDecimal(txt_appr_hos_value.Text.Trim() == "" ? "0" : txt_appr_hos_value.Text);
            decimal doctor = Convert.ToDecimal(txt_appr_doc_value.Text.Trim() == "" ? "0" : txt_appr_doc_value.Text);
            decimal repoting = Convert.ToDecimal(txt_appr_ref_value.Text.Trim() == "" ? "0" : txt_appr_ref_value.Text);
            value = hospital + doctor + repoting;
            if (ddl_appr_discount_type.SelectedIndex == 0)
            {
                if (value > NetAmount)
                {
                    txt_appr_hos_value.Text = "";
                    txt_appr_doc_value.Text = "";
                    txt_appr_ref_value.Text = "";
                    lbl_appr_discount_amt.Text = "";
                    Messagealert_.ShowMessage(lblmessagepop1, "Share cannot exceed net ammount!", 0);
                    divpop1.Visible = true;
                    divpop1.Attributes["class"] = "FailAlert";
                    totalShareCalculate();
                }
                else
                {
                    lbl_appr_discount_amt.Text = Commonfunction.Getrounding(value.ToString());
                    totalShareCalculate();
                }
            }
            else
            {
                if (value > 100)
                {
                    txt_appr_hos_value.Text = "";
                    txt_appr_doc_value.Text = "";
                    txt_appr_ref_value.Text = "";
                    lbl_appr_discount_amt.Text = "";
                    Messagealert_.ShowMessage(lblmessagepop1, "Percentage", 0);
                    divpop1.Visible = true;
                    divpop1.Attributes["class"] = "FailAlert";
                    totalShareCalculate();
                }
                else
                {

                    decimal pcValue = 0;

                    pcValue = ((value / 100) * NetAmount);
                    lbl_appr_discount_amt.Text = Commonfunction.Getrounding(pcValue.ToString());
                    totalShareCalculate();
                }

            }

        }
        public void totalShareCalculate()
        {
            decimal totalHospital = 0;
            decimal totalDoctor = 0;
            decimal totalReport = 0;
            foreach (GridViewRow row in GVDiscountApproveList.Rows)
            {
                Label lblApprisDis = (Label)GVDiscountApproveList.Rows[row.RowIndex].Cells[0].FindControl("lblApprisDis");
                if (lblApprisDis.Text == "1")
                {
                    Label lblApprAmount = (Label)GVDiscountApproveList.Rows[row.RowIndex].Cells[0].FindControl("lblApprAmount");
                    decimal netAmount = Convert.ToDecimal(lblApprAmount.Text);

                    DropDownList ddl_appr_discount_type = (DropDownList)GVDiscountApproveList.Rows[row.RowIndex].Cells[0].FindControl("ddl_appr_discount_type");
                    TextBox txt_appr_hos_value = (TextBox)GVDiscountApproveList.Rows[row.RowIndex].Cells[0].FindControl("txt_appr_hos_value");
                    TextBox txt_appr_doc_value = (TextBox)GVDiscountApproveList.Rows[row.RowIndex].Cells[0].FindControl("txt_appr_doc_value");
                    TextBox txt_appr_ref_value = (TextBox)GVDiscountApproveList.Rows[row.RowIndex].Cells[0].FindControl("txt_appr_ref_value");
                    if (ddl_appr_discount_type.SelectedIndex == 0)
                    {
                        totalHospital = totalHospital + (Convert.ToDecimal(txt_appr_hos_value.Text.Trim() == "" ? "0" : txt_appr_hos_value.Text));
                        totalDoctor = totalDoctor + (Convert.ToDecimal(txt_appr_doc_value.Text.Trim() == "" ? "0" : txt_appr_doc_value.Text));
                        totalReport = totalReport + (Convert.ToDecimal(txt_appr_ref_value.Text.Trim() == "" ? "0" : txt_appr_ref_value.Text));

                    }
                    else
                    {
                        totalHospital = totalHospital + ((Convert.ToDecimal(txt_appr_hos_value.Text.Trim() == "" ? "0" : txt_appr_hos_value.Text)) / 100 * netAmount);
                        totalDoctor = totalDoctor + ((Convert.ToDecimal(txt_appr_doc_value.Text.Trim() == "" ? "0" : txt_appr_doc_value.Text)) / 100 * netAmount);
                        totalReport = totalReport + ((Convert.ToDecimal(txt_appr_ref_value.Text.Trim() == "" ? "0" : txt_appr_ref_value.Text)) / 100 * netAmount);
                    }
                }
            }

            txt_appr_tot_hos_share.Text = Commonfunction.Getrounding(totalHospital.ToString());
            txt_appr_tot_doc_share.Text = Commonfunction.Getrounding(totalDoctor.ToString());
            txt_appr_tot_rpt_share.Text = Commonfunction.Getrounding(totalReport.ToString());
            txt_total_share.Text = Commonfunction.Getrounding((totalHospital + totalDoctor + totalReport).ToString());

        }
        protected void ddl_appr_discount_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.MDApproval.Show();
            GridViewRow currentRow = ((GridViewRow)((DropDownList)sender).NamingContainer);
            ShareChange(sender, currentRow);
        }

        protected void btnSample_Click(object sender, EventArgs e)
        {
            this.MDApproval.Show();
        }

        protected void btnDefault_Click(object sender, EventArgs e)
        {
            this.MDDiscount.Show();
        }
    }
}