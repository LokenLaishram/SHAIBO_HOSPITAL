using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedBillData;
using Mediqura.CommonData.PatientData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using SignalRChat;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedBills
{
    public partial class DiscountRequest : BasePage{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ddlbind();
                btnSendRequest.Visible = false;
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_discount_status, mstlookup.GetLookupsList(LookupName.DiscountStatus));
            Commonfunction.PopulateDdl(ddl_requested_by, mstlookup.GetLookupsList(LookupName.DiscountReqBy));
        }
      

        protected void btnresets_Click(object sender, EventArgs e)
        {
           
            reset();
            
        }
        public void reset() {
            lblUHID.Text = "";
            ddl_service_type.SelectedIndex = 0;
            ddl_doctor.SelectedIndex = 0;
            txt_bill_no.Text = "";
            txt_name.Text = "";
            txt_address.Text = "";
            txt_age.Text = "";
            txt_total_Amount.Text = "";
            txt_bill_no.ReadOnly = false;
            txt_remarks.Text = "";
            btnSendRequest.Visible = false;
            bindGrid();

        }
        protected void ddl_service_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_service_type.SelectedIndex == 0)
            {
                txt_bill_no.ReadOnly = true;
            }
            else {
                txt_bill_no.ReadOnly = false;
                AutoCompleteBillNo.ContextKey = ddl_service_type.SelectedValue;
            }
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

        protected void txt_bill_no_TextChanged(object sender, EventArgs e)
        {
            DiscountRequestData ObjData = new DiscountRequestData();
             DiscountBO objBO = new DiscountBO();
            ObjData.serviceTypeID = Convert.ToInt32(ddl_service_type.SelectedValue);
            ObjData.BillNo = txt_bill_no.Text;
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_doctor, mstlookup.GetDoctorNameByServiceTypeForBill(ObjData));

            List<DiscountRequestData> getResult = new List<DiscountRequestData>();
            getResult=objBO.GetPatientDetailsByBillNo(ObjData);
            if (getResult.Count > 0)
            {
                lblUHID.Text = getResult[0].UHID.ToString();
                txt_bill_no.ReadOnly = true;
                txt_name.Text = getResult[0].PatName;
                txt_billID.Text = getResult[0].BillID.ToString();
                txt_address.Text = getResult[0].PatientAddress;
                txt_age.Text = getResult[0].PatientAge.ToString();
                txt_total_Amount.Text = Commonfunction.Getrounding(getResult[0].TotalAmount.ToString());
                bindGrid();
            }
            else {
                
                    btnSendRequest.Visible = false;
                    Messagealert_.ShowMessage(lblmessage, "DiscountDuplicate", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                lblUHID.Text = "";
                txt_name.Text = "";
                txt_address.Text = "";
                txt_age.Text = "";
                txt_total_Amount.Text = "";
                divmsg3.Visible = false;
                GVDiscount.DataSource = null;
                GVDiscount.DataBind();
                GVDiscount.Visible = true;

                lblresult.Visible = false;
            }

        }
        public void bindGrid(){
            try
            {

                List<DiscountRequestData> ServiceList = new List<DiscountRequestData>();
                ServiceList = GetServiceList(0);
                if (ServiceList.Count > 0)
                {
                    GVDiscount.DataSource = ServiceList;
                    GVDiscount.DataBind();
                    GVDiscount.Visible = true;
                    totalCalculate();



                }
                else
                {

                    divmsg3.Visible = false;
                    GVDiscount.DataSource = null;
                    GVDiscount.DataBind();
                    GVDiscount.Visible = true;
              
                    lblresult.Visible = false;
              
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }

        public List<DiscountRequestData> GetServiceList(int curIndex)
        {

            DiscountRequestData ObjData = new DiscountRequestData();
            DiscountBO objBO = new DiscountBO();
            ObjData.serviceTypeID = Convert.ToInt32(ddl_service_type.SelectedValue==""?"0": ddl_service_type.SelectedValue);
            ObjData.DoctorID = Convert.ToInt32(ddl_doctor.SelectedValue==""?"0": ddl_doctor.SelectedValue);
            ObjData.BillNo = txt_bill_no.Text;
            return objBO.GetServiceListByBillNo(ObjData);

        }
        protected void ddl_doctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void ddl_discount_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = ((GridViewRow)((DropDownList)sender).NamingContainer);
            discountChange(sender, currentRow);

        }
        public void discountChange(object sender, GridViewRow currentRow) {

            
            DropDownList ddl_discount_type = (DropDownList)currentRow.FindControl("ddl_discount_type");
            TextBox txt_dis_value = (TextBox)currentRow.FindControl("txt_dis_value");
            Label lblNetAmount = (Label)currentRow.FindControl("lblNetAmount");
            Label lbl_discount_amt = (Label)currentRow.FindControl("lbl_discount_amt");
            decimal value = Convert.ToDecimal(txt_dis_value.Text==""?"0": txt_dis_value.Text);
            decimal NetAmount = Convert.ToDecimal(lblNetAmount.Text==""?"0": lblNetAmount.Text);
          
            if (ddl_discount_type.SelectedIndex == 0)
            {
                if (value > NetAmount)
                {
                    txt_dis_value.Text = "";
                    lbl_discount_amt.Text = "";
                    Messagealert_.ShowMessage(lblmessage, "DiscountAmount", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    totalCalculate();
                }
                else
                {
                    lbl_discount_amt.Text = Commonfunction.Getrounding(value.ToString());
                    totalCalculate();  }
            }
            else
            {
                if (value > 100)
                {
                    txt_dis_value.Text = "";
                    lbl_discount_amt.Text = "";
                    Messagealert_.ShowMessage(lblmessage, "Percentage", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
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
                Label lbl_discount_amt = (Label)GVDiscount.Rows[row.RowIndex].Cells[0].FindControl("lbl_discount_amt");
                totalDiscount = totalDiscount + (Convert.ToDecimal(lbl_discount_amt.Text == "" ? "0" : lbl_discount_amt.Text));
            }
          
            decimal TotalAmount = Convert.ToDecimal(txt_total_Amount.Text == "" ? "0" : txt_total_Amount.Text.ToString());
            txt_total_discount.Text = Commonfunction.Getrounding(totalDiscount.ToString());
            txt_total_net_amount.Text = Commonfunction.Getrounding((TotalAmount - Convert.ToDecimal(txt_total_discount.Text == "" ? "0" : txt_total_discount.Text)).ToString());
            if (totalDiscount > 0)
            {
                btnSendRequest.Visible = true;
            }
            else { btnSendRequest.Visible = false; }

        }
        protected void txt_dis_value_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
            discountChange(sender, currentRow);
        }

        protected void btnSendRequest_Click(object sender, EventArgs e)
        {
            if (LogData.SaveEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SaveEnable", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (txt_remarks.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Please enter remarks for discount", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                txt_remarks.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
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
                    Label lblServiceName = (Label)GVDiscount.Rows[row.RowIndex].Cells[0].FindControl("lblServiceName");
                    Label lblQnty = (Label)GVDiscount.Rows[row.RowIndex].Cells[0].FindControl("lblQnty");
                    Label lblServiceCharge = (Label)GVDiscount.Rows[row.RowIndex].Cells[0].FindControl("lblServiceCharge");
                    Label lblNetAmount = (Label)GVDiscount.Rows[row.RowIndex].Cells[0].FindControl("lblNetAmount");
                    DropDownList ddl_discount_type = (DropDownList)GVDiscount.Rows[row.RowIndex].Cells[0].FindControl("ddl_discount_type");
                    TextBox txt_dis_value = (TextBox)GVDiscount.Rows[row.RowIndex].Cells[0].FindControl("txt_dis_value");
                    Label lbl_discount_amt = (Label)GVDiscount.Rows[row.RowIndex].Cells[0].FindControl("lbl_discount_amt");
                    Label lblDoctorID = (Label)GVDiscount.Rows[row.RowIndex].Cells[0].FindControl("lblDoctorID");



                    DiscountRequestServiceData objsubdata = new DiscountRequestServiceData();
                    objsubdata.serviceTypeID = Convert.ToInt32(ddl_service_type.SelectedValue == "" ? "0" : ddl_service_type.SelectedValue);
                    objsubdata.ServiceID = Convert.ToInt32(lblServiceID.Text == "" ? "0" : lblServiceID.Text);
                    objsubdata.ServiceName = lblServiceName.Text;
                    objsubdata.Quantity = Convert.ToInt32(lblQnty.Text == "" ? "0" : lblQnty.Text);
                    objsubdata.amount = Convert.ToDecimal(lblServiceCharge.Text == "" ? "0" : lblServiceCharge.Text);
                    objsubdata.NetAmount = Convert.ToDecimal(lblNetAmount.Text == "" ? "0" : lblNetAmount.Text);
                    objsubdata.DisType = Convert.ToInt32(ddl_discount_type.SelectedValue == "" ? "0" : ddl_discount_type.SelectedValue);
                    objsubdata.DisValue = Convert.ToDecimal(txt_dis_value.Text == "" ? "0" : txt_dis_value.Text);
                    objsubdata.isDis = Convert.ToInt32(Convert.ToDecimal(lbl_discount_amt.Text.Trim() == "" ? "0" : lbl_discount_amt.Text.Trim()) == 0 ? 0 : 1);
                    objsubdata.DisAmount = Convert.ToDecimal(lbl_discount_amt.Text == "" ? "0" : lbl_discount_amt.Text);
                    objsubdata.DoctorID = Convert.ToInt32(lblDoctorID.Text == "" ? "0" : lblDoctorID.Text);

                    Listobjdata.Add(objsubdata);

                }
                objdata.XMLData = XmlConvertor.DiscountRequestToXML(Listobjdata).ToString();
                objdata.UHID = Convert.ToInt64(lblUHID.Text==""?"0": lblUHID.Text);
                objdata.serviceTypeID= Convert.ToInt32(ddl_service_type.SelectedValue == "" ? "0" : ddl_service_type.SelectedValue);
                objdata.TotalAmount = Convert.ToDecimal(txt_total_Amount.Text == "" ? "0" : txt_total_Amount.Text);
                objdata.TotalDiscount = Convert.ToDecimal(txt_total_discount.Text == "" ? "0" : txt_total_discount.Text);
                objdata.BillNo = txt_bill_no.Text == "" ? "0" : txt_bill_no.Text;
                objdata.BillID = Convert.ToInt64(txt_billID.Text == "" ? "0" : txt_billID.Text);
                objdata.BillType = 1;
                objdata.FinancialYearID = LogData.FinancialYearID;
                objdata.HospitalID = LogData.HospitalID;
                objdata.EmployeeID = LogData.EmployeeID;
                objdata.Remarks = txt_remarks.Text;

                List<DiscountOutput> result = objstdBO.UpdateDiscoutRequest(objdata);
                if (result[0].Resultoutput > 0)
                {
                    if (result[0].Resultoutput == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "DiscountReq", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SucessAlert";
                        btnSendRequest.Visible = false;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>pushMessage('"+txt_remarks.Text+"','"+ result[0].ID + "');</script>", false);

                    }
                    else {
                        if (result[0].Resultoutput == 5)
                        {
                            btnSendRequest.Visible = false;
                            Messagealert_.ShowMessage(lblmessage, "DiscountDuplicate", 0);
                            divmsg1.Visible = true;
                            divmsg1.Attributes["class"] = "FailAlert";
                        }
                    }
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

            }
        }

        protected void ddl_tab2_serviceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_tab2_serviceType.SelectedIndex == 0)
            {
                txt_tab2_billNo.ReadOnly = true;
            }
            else
            {
                txt_tab2_billNo.ReadOnly = false;
                autoConpleteTab2Bill.ContextKey = ddl_tab2_serviceType.SelectedValue;
            }

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

        protected void btn_tab2_search_Click(object sender, EventArgs e)
        {
            bindgridtab2();
        }

        protected void btn_tab2_reset_Click(object sender, EventArgs e)
        {

            ddl_tab2_serviceType.SelectedIndex = 0;
            txt_tab2_billNo.Text = "";
            txt_tab2_billNo.ReadOnly = true;
            txt_tab2_UHID.Text = "";
            txt_tab2_name.Text = "";
            txt_tab2_address.Text = "";
            ddl_discount_status.SelectedIndex = 0;
            txtdatefrom.Text = "";
            txtto.Text = "";
            lblmessagetab2.Visible = false;
            divtab2.Visible = false;
            ddl_requested_by.SelectedIndex = 0;
            GVDiscountList.DataSource = null;
            GVDiscountList.DataBind();
            GVDiscountList.Visible = true;
        }

        protected void btn_tab2_print_Click(object sender, EventArgs e)
        {

        }
        public void bindgridtab2() {
            DiscountBO objBo = new DiscountBO();
            DiscountRequestListData objData = new DiscountRequestListData();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objData.serviceTypeID = Convert.ToInt32(ddl_tab2_serviceType.SelectedValue == "0" ? "0" : ddl_tab2_serviceType.SelectedValue);
            objData.BillNo = txt_tab2_billNo.Text.Trim() == "" ? "" : txt_tab2_billNo.Text;
            objData.UHID= Convert.ToInt64(txt_tab2_UHID.Text.Trim()==""?"0": txt_tab2_UHID.Text);
            objData.PatName = txt_tab2_name.Text.Trim() == "" ? "" : txt_tab2_name.Text;
            objData.PatientAddress = txt_tab2_address.Text.Trim() == "" ? "" : txt_tab2_address.Text;
            objData.DisStatus = Convert.ToInt32(ddl_discount_status.SelectedValue);
            objData.RequestedBy= Convert.ToInt32(ddl_requested_by.SelectedValue);
            DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            string datefrom = from.ToString("yyyy-MM-dd");
            string dateto = To.ToString("yyyy-MM-dd");
            from = Convert.ToDateTime(datefrom + " " + "12:01:00 AM");
            To = Convert.ToDateTime(dateto + " " + "11:59:00 PM");
            objData.Datefrom = from;
            objData.DateTo = To;

            List<DiscountRequestListData> listDiscount = objBo.GetDiscountList(objData);
            if (listDiscount.Count > 0)
            {

                GVDiscountList.DataSource = listDiscount;
                GVDiscountList.DataBind();
                GVDiscountList.Visible = true;
                divtab2.Visible = true;
                lblmessagetab2.Visible = true;
                Messagealert_.ShowMessage(lblmessagetab2, "Total:" + listDiscount[0].MaximumRows.ToString() + " Record(s) found", 1);
                divtab2.Attributes["class"] = "SucessAlert";
                txt_tab2_total_approve_amount.Text = Commonfunction.Getrounding(listDiscount[0].TotalApprove.ToString());
                txt_tab2_total_requested.Text = Commonfunction.Getrounding(listDiscount[0].TotalDiscount.ToString());
                txt_tab2_total_on_req.Text= listDiscount[0].TotalonRequest.ToString();
                txt_tab2_total_approval.Text = listDiscount[0].TotalonApprove.ToString();
            }
            else {
                GVDiscountList.DataSource = null;
                GVDiscountList.DataBind();
                GVDiscountList.Visible = true;
                divtab2.Visible = false;
                lblmessagetab2.Visible = false;
            }

        }

        protected void GVDiscountList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDisStatus = (Label)e.Row.FindControl("lblDisStatus");
                LinkButton delete = (LinkButton)e.Row.FindControl("lnkDelete");
                Label lblRequestNo = (Label)e.Row.FindControl("lblRequestNo");
                LinkButton lblRequest = (LinkButton)e.Row.FindControl("lblRequest");
                Label lblBillType = (Label)e.Row.FindControl("lblBillType");
                Label lblRequestType = (Label)e.Row.FindControl("lblRequestType");
                int flag = Convert.ToInt32(lblBillType.Text == "" ? "0" : lblBillType.Text);
                if (flag == 0)
                {
                    lblRequestType.Text = "Before";
                }
                else
                {
                    lblRequestType.Text = "After";
                }
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
                if (Convert.ToInt32(lblDisStatus.Text) ==1 || Convert.ToInt32(lblDisStatus.Text)==2)
                {
                    delete.Visible = true;
                }
                else
                {
                    delete.Visible = false;
                }

            }
        }

        protected void GVDiscountList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblMessageTab2Header, "DeleteEnable", 0);
                        divTab2header.Visible = true;
                        divTab2header.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }

                    DiscountRequestListData objData = new DiscountRequestListData();
                    DiscountBO objBO = new DiscountBO();

                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GVDiscountList.Rows[i];
                    Label lblRequestNo = (Label)gr.Cells[0].FindControl("lblRequestNo");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblMessageTab2Header, "Remarks", 0);
                        divTab2header.Attributes["class"] = "FailAlert";
                        divTab2header.Visible = false;
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objData.Remarks = txtremarks.Text;
                    }
                    objData.RequestNo = Convert.ToInt32(lblRequestNo.Text);
                    objData.EmployeeID = LogData.UserLoginId;
                    objData.HospitalID = LogData.HospitalID;
                    objData.IPaddress = LogData.IPaddress;
                    int Result = objBO.DeleteDiscountRequestByID(objData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblMessageTab2Header, "delete", 1);
                        divTab2header.Attributes["class"] = "SucessAlert";
                        divTab2header.Visible = true;
                        bindgridtab2();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblMessageTab2Header, "system", 0);
                        divTab2header.Attributes["class"] = "FailAlert";
                        divTab2header.Visible = true;
                    }


                }
                if (e.CommandName == "View")
                {
                    string billID, Reqno;
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GVDiscountList.Rows[i];
                    Label lblBillID = (Label)gr.Cells[0].FindControl("lblBillID");
                    billID = lblBillID.Text;
                    Label lblRequestNo = (Label)gr.Cells[0].FindControl("lblRequestNo");
                    Reqno = lblRequestNo.Text;
                    Label lblBillType = (Label)gr.Cells[0].FindControl("lblBillType");
                    Label lblServiceType = (Label)gr.Cells[0].FindControl("lblServiceType");
                    Label lblDisStatus = (Label)gr.Cells[0].FindControl("lblDisStatus");
                    if (lblDisStatus.Text == "3")
                    {
                        if (lblBillType.Text == "1")
                        {
                            Session["ReqNo"] = Reqno;
                            Response.Redirect("~/MedBills/DiscountRefund.aspx", false);
                        }
                        else
                        {
                            switch (lblServiceType.Text)
                            {
                                case "1":
                                  //  op services
                                    Session["BILLID"] = billID;
                                    Response.Redirect("~/MedBills/OpdBilling.aspx", false);
                                    break;
                                case "2":
                                    Session["BILLID"] = billID;
                                    Response.Redirect("~/MedBills/OPLabBilling.aspx", false);
                                    //op investigation
                                    break;
                                case "3":
                                    //ip services
                                 
                                    Response.Redirect("~/MedBills/IPfinalbill.aspx", false);
                                    break;
                                case "4":
                                    //emg 
                                    Session["EMRGNO"] = billID;
                                    Response.Redirect("~/MedEmergency/EmergencyFinalBilling.aspx", false);
                                 break;
                           }
                        }
                    }
                    


                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblMessageTab2Header, "system", 0);
                divTab2header.Attributes["class"] = "FailAlert";
                divTab2header.Visible = true;
            }
        }
    }
}