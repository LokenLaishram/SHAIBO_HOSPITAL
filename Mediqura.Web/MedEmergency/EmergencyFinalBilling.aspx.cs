using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.PatientData;
using Mediqura.CommonData.MedEmergencyData;
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
using Mediqura.CommonData.MedUtilityData;
using Mediqura.BOL.MedBillBO;
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.AdmissionData;
using Mediqura.BOL.AdmissionBO;
using Mediqura.BOL.MedEmergencyBO;
using Mediqura.CommonData.MedBillData;
using System.Drawing;

namespace Mediqura.Web.MedEmergency
{
    public partial class EmergencyFinalBilling : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                if (Session["EMRGNO"] != null)
                {
                    Int64 ID = Convert.ToInt32(Session["EMRGNO"].ToString());
                    Session["EMRGNO"] = null;
                    Getfinalbilldetails(ID);
                }
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlpaymentmode, mstlookup.GetLookupsList(LookupName.PaymentMode));
            Commonfunction.PopulateDdl(ddlpaymentmodes, mstlookup.GetLookupsList(LookupName.PaymentMode));
            Commonfunction.PopulateDdl(ddlcollectedby, mstlookup.GetLookupsList(LookupName.CollectedBy));
            Commonfunction.PopulateDdl(ddl_responsiblestaff, mstlookup.GetLookupsList(LookupName.Employee));
            ddlpaymentmode.SelectedIndex = 1;
            Session["DiscountReqList"] = null;
            Session["DiscountList"] = null;
            btn_refund.Attributes["disabled"] = "disabled";
            btnsave.Attributes["disabled"] = "disabled";
            btnprint.Attributes["disabled"] = "disabled";
            btnaddresponsibility.Visible = false;
            txt_totaldueamount.Text = "";
            Session["Responsibilty"] = null;
            txttotalbillamount.Text = "0.00";
            txtajusted.Text = "0.00";
            txttotaldiscounted.Text = "0.00";
            txttotalpaid.Text = "0.00";
            btnlinkdiscount.Visible = false;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetEmrgNo(string prefixText, int count, string contextKey)
        {
            EmrgAdmissionData Objpaic = new EmrgAdmissionData();
            EmrgAdmissionBO objInfoBO = new EmrgAdmissionBO();
            List<EmrgAdmissionData> getResult = new List<EmrgAdmissionData>();
            Objpaic.EmrgNo = prefixText;
            getResult = objInfoBO.GetEmrgNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmrgNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetFEmrgNo(string prefixText, int count, string contextKey)
        {
            EmrgAdmissionData Objpaic = new EmrgAdmissionData();
            EmrgAdmissionBO objInfoBO = new EmrgAdmissionBO();
            List<EmrgAdmissionData> getResult = new List<EmrgAdmissionData>();
            Objpaic.EmrgNo = prefixText;
            getResult = objInfoBO.GetFEmrgNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmrgNo.ToString());
            }
            return list;
        }
        //TAB 2 //
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetEmgPatientName(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetEmgPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmgPatientName.ToString());
            }
            return list;
        }
        protected void txt_emrgno_TextChanged(object sender, EventArgs e)
        {
            Getfinalbilldetails(0);
        }
        protected void Getfinalbilldetails(Int64 emrgId)
        {
            Session["DiscountReqList"] = null;
            Session["DiscountList"] = null;
            Session["Responsibilty"] = null;
            EmrgAdmissionData Objpaic = new EmrgAdmissionData();
            EmrgAdmissionBO objInfoBO = new EmrgAdmissionBO();
            List<EmrgAdmissionData> getResult = new List<EmrgAdmissionData>();
            Objpaic.EmrgNo = txt_emrgno.Text.Trim() == "" ? "" : txt_emrgno.Text.Trim();
            Objpaic.EmrgID = emrgId;
            Objpaic.FinancialYearID = LogData.FinancialYearID;
            Objpaic.HospitalID = LogData.HospitalID;
            Objpaic.AmountEnable = LogData.AmountEnable;

            getResult = objInfoBO.GetemrgfinalbillDetail(Objpaic);
            if (getResult.Count > 0)
            {
                if (getResult[0].ChkPhrBillClear.ToString() == "1")
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                    txtname.Text = getResult[0].PatientName.ToString();
                    txt_emrgno.Text = getResult[0].EmrgNo.ToString();
                    txt_address.Text = getResult[0].Address.ToString();
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
                        txt_payableamount.Text = Commonfunction.Getrounding(getResult[0].TotalBillAmount.ToString());
                        txt_totalpaid.Text = txt_payableamount.Text;
                        txtadjustedamount.Text = "0";
                        txt_refundable.Text = "0";
                        txt_dueamount.Text = "0";
                        txt_totalpaid.ReadOnly = false;
                    }
                    if (Convert.ToDecimal(getResult[0].BalanceAmount.ToString()) > 0 && Convert.ToDecimal(getResult[0].BalanceAmount.ToString()) < Convert.ToDecimal(getResult[0].TotalBillAmount.ToString()))
                    {
                        txt_payableamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(getResult[0].TotalBillAmount.ToString()) - Convert.ToDecimal(getResult[0].BalanceAmount.ToString())).ToString());
                        txt_totalpaid.Text = txt_payableamount.Text;
                        txtadjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(getResult[0].BalanceAmount.ToString())).ToString()); ;
                        txt_refundable.Text = "0";
                        txt_dueamount.Text = "0";
                        txt_totalpaid.ReadOnly = false;
                    }
                    if (Convert.ToDecimal(getResult[0].BalanceAmount.ToString()) > 0 && Convert.ToDecimal(getResult[0].BalanceAmount.ToString()) > Convert.ToDecimal(getResult[0].TotalBillAmount.ToString()))
                    {
                        txt_payableamount.Text = "0";
                        txt_totalpaid.Text = "0";
                        txt_totalpaid.ReadOnly = true;
                        txtadjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(getResult[0].TotalBillAmount.ToString())).ToString()); ;
                        txt_refundable.Text = Commonfunction.Getrounding((Convert.ToDecimal(getResult[0].BalanceAmount.ToString()) - Convert.ToDecimal(getResult[0].TotalBillAmount.ToString())).ToString());
                        txt_dueamount.Text = "";
                        btnlinkdiscount.Visible = false;
                    }
                    if (Convert.ToDecimal(getResult[0].BalanceAmount.ToString()) > 0 && Convert.ToDecimal(getResult[0].BalanceAmount.ToString()) == Convert.ToDecimal(getResult[0].TotalBillAmount.ToString()))
                    {
                        txt_payableamount.Text = "0";
                        txt_totalpaid.ReadOnly = true;
                        txt_totalpaid.Text = "0";
                        txtadjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(getResult[0].TotalBillAmount.ToString())).ToString()); ;
                        txt_refundable.Text = "0";
                        txt_dueamount.Text = "0";
                    }
                    gvEMRGitemlist.Visible = true;
                    gvEMRGitemlist.DataSource = getResult;
                    gvEMRGitemlist.DataBind();
                    List<EMRDiscountListData> DiscountReqList = Session["DiscountReqList"] == null ? new List<EMRDiscountListData>() : (List<EMRDiscountListData>)Session["DiscountReqList"];
                    if (DiscountReqList.Count > 0)
                    {
                        GVDiscountReq.DataSource = DiscountReqList;
                        GVDiscountReq.DataBind();
                        GVDiscountReq.Visible = true;
                        txtDisTotalBillAmount.Text = DiscountReqList[0].TotalBill.ToString();
                    }
                    else
                    {
                        EmrgAdmissionBO objBO = new EmrgAdmissionBO();
                        EmrgAdmissionData objData = new EmrgAdmissionData();
                        objData.EmrgNo = txt_emrgno.Text;
                        List<EMRDiscountListData> result = objBO.GetDiscountListForEmergency(objData);
                        GVDiscountReq.DataSource = result;
                        GVDiscountReq.DataBind();
                        GVDiscountReq.Visible = true;
                        txtDisTotalBillAmount.Text = result[0].TotalBill.ToString();
                        totalCalculate();
                    }
                }
                else
                {
                    txt_emrgno.Text = "";
                    txt_emrgno.Focus();
                    Messagealert_.ShowMessage(lblmessage, "PHRbillStatus", 0);
                    div1.Attributes["class"] = "FailAlert";
                    div1.Visible = true;
                }
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
                gvEMRGitemlist.DataSource = null;
                gvEMRGitemlist.DataBind();
                txtname.Text = "";
                txt_emrgno.Text = "";
                txt_payableamount.Text = "";
                btnsave.Attributes["disabled"] = "disabled";
                Session["DiscountReqList"] = null;
                Session["DiscountList"] = null;
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetDiscountBy(string prefixText, int count, string contextKey)
        {
            EmployeeData Objpaic = new EmployeeData();
            EmployeeBO objInfoBO = new EmployeeBO();
            List<EmployeeData> getResult = new List<EmployeeData>();
            Objpaic.EmpName = prefixText;
            getResult = objInfoBO.GetdiscountBy(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmpName.ToString());
            }
            return list;
        }
        decimal Subtotal = 0;
        int qty_subtotal = 0;
        int CategoryID = 0;
        int rowIndex = 1;
        protected void gvEMRGitemlist_RowCreated(object sender, GridViewRowEventArgs e)
        {

            bool newRow = false;
            if ((CategoryID > 0) && (DataBinder.Eval(e.Row.DataItem, "ServiceCategoryID") != null))
            {
                if (CategoryID != Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ServiceCategoryID").ToString()))
                    newRow = true;
            }
            if ((CategoryID > 0) && (DataBinder.Eval(e.Row.DataItem, "ServiceCategoryID") == null))
            {
                newRow = true;
                rowIndex = 0;
            }
            if (newRow)
            {
                GridView GridView1 = (GridView)sender;
                GridViewRow NewTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                NewTotalRow.Font.Bold = true;
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Sub Total";
                HeaderCell.HorizontalAlign = HorizontalAlign.Left;
                HeaderCell.ColumnSpan = 7;
                NewTotalRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                HeaderCell.Text = qty_subtotal.ToString();
                HeaderCell.Text = Subtotal.ToString("N2");
                NewTotalRow.Cells.Add(HeaderCell);
                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
                rowIndex++;
                Subtotal = 0;
            }
        }
        protected void gvEMRGitemlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //CategoryID = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ServiceCategoryID").ToString());
                //decimal tmpTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetServiceCharge").ToString());
                //int subtotalqty = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Quantity").ToString());
                //Subtotal += tmpTotal;
                //GrandTotal += tmpTotal;
                //subtotalqty += subtotalqty;
                Label ActualcategoryID = (Label)e.Row.FindControl("lbl_actualcategoryID");
                LinkButton delete = (LinkButton)e.Row.FindControl("lnkDelete");
                if (ActualcategoryID.Text == "5" || ActualcategoryID.Text == "6" || ActualcategoryID.Text == "7")
                {
                    delete.Visible = false;
                }
                else
                {
                    delete.Visible = true;
                }
            }
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    Label lblsubtotal = (Label)e.Row.FindControl("lbl_subtotal");
            //    Label lbl_subtotqty = (Label)e.Row.FindControl("lbl_totalqty");


            //    lblsubtotal.Text = Subtotal.ToString("N2");
            //    lbl_subtotqty.Text = lbl_subtotqty.Text.ToString();

            //}
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            lbl_bill.InnerText = "Bill Number";
            gvEMRGitemlist.DataSource = null;
            gvEMRGitemlist.DataBind();
            gvEMRGitemlist.Visible = false;
            lblmessage.Visible = false;
            txt_emrgno.Text = "";
            txtname.Text = "";
            txt_address.Text = "";
            Session["Responsibilty"] = null;
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
            txt_emrgno.Text = "";
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
            txtdiscount.Text = "";
            btn_refund.Attributes["disabled"] = "disabled";
            hdnbillnumber.Value = null;
            Session["DiscountReqList"] = null;
            Session["DiscountList"] = null;
            ddl_settlementmode.SelectedIndex = 0;
        }
        protected void btnaddresponsibility_Click(object sender, EventArgs e)
        {
            this.mddueresponsible.Show();
        }
        protected void gvEMRGservicerecord_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "DeleteEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    EmrgAdmissionData objadmin = new EmrgAdmissionData();
                    EmrgAdmissionBO obadminBO = new EmrgAdmissionBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvEMRGitemlist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_recordID");
                    Label invnumber = (Label)gr.Cells[0].FindControl("lbl_invnumber");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    Label servicenumber = (Label)gr.Cells[0].FindControl("lbl_servicenumber");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "Remarks", 0);
                        div1.Attributes["class"] = "FailAlert";
                        div1.Visible = true;
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objadmin.Remarks = txtremarks.Text;
                    }
                    objadmin.EmrgNo = txt_emrgno.Text == "" ? "" : txt_emrgno.Text;
                    objadmin.InvNumber = invnumber.Text == "" ? "" : invnumber.Text;
                    objadmin.ServiceNumber = servicenumber.Text == "" ? "" : servicenumber.Text;
                    objadmin.ID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                    objadmin.EmployeeID = LogData.EmployeeID;
                    objadmin.HospitalID = LogData.HospitalID;
                    objadmin.IPaddress = LogData.IPaddress;
                    int Result = obadminBO.DeleteEMGServiceRecordByEMRGNo(objadmin);
                    if (Result == 1)
                    {
                        Getfinalbilldetails(0);
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        div1.Attributes["class"] = "SucessAlert";
                        div1.Visible = true;
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        div1.Attributes["class"] = "FailAlert";
                        div1.Visible = true;
                    }
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
        protected void GvEmrgfinalbill_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    EmrgAdmissionData objadmin = new EmrgAdmissionData();
                    EmrgAdmissionBO obadminBO = new EmrgAdmissionBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvEmrgfinalbill.Rows[i];
                    Label Bill = (Label)gr.Cells[0].FindControl("lbl_billno");
                    Label emrgno = (Label)gr.Cells[0].FindControl("lbl_emrgno");
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
                    objadmin.EmrgNo = emrgno.Text.Trim();
                    objadmin.BillNo = Bill.Text.Trim();
                    objadmin.UHID = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                    objadmin.EmployeeID = LogData.EmployeeID;
                    objadmin.HospitalID = LogData.HospitalID;
                    objadmin.IPaddress = LogData.IPaddress;
                    int Result = obadminBO.Deleteemrgfinalbill(objadmin);
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
                    GridViewRow gp = GvEmrgfinalbill.Rows[j];
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
                if (ddlpaymentmode.SelectedIndex == 0)
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
                if (ddl_settlementmode.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "settlementmode", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_settlementmode.SelectedValue == "2")
                {
                    Session["Emg_UHID"] = lbl_UHIDTemp.Text;
                    Session["EmgNumber"] = txt_emrgno.Text;
                    Response.Redirect("/MedAdmission/IPDAdmission.aspx", false);
                    return;
                }
                EmrgAdmissionData objbill = new EmrgAdmissionData();
                List<Discount> ListDiscount = new List<Discount>();
                EmrgAdmissionBO objstdBO = new EmrgAdmissionBO();
                List<EmployeeData> emplist = new List<EmployeeData>();
                foreach (GridViewRow row in gv_responsibledetais.Rows)
                {
                    Label empID = (Label)gv_responsibledetais.Rows[row.RowIndex].Cells[0].FindControl("lbl_employeeID");
                    TextBox Amount = (TextBox)gv_responsibledetais.Rows[row.RowIndex].Cells[0].FindControl("lbl_amnt");
                    EmployeeData objempdata = new EmployeeData();
                    
                    objempdata.EmployeeID = Convert.ToInt64(empID.Text == "" ? "0" : empID.Text);
                    objempdata.Amount = Convert.ToDecimal(Amount.Text == "" ? "0" : Amount.Text);
                    emplist.Add(objempdata);
                }
                objbill.XMLData = XmlConvertor.Dueresponsiblemployee(emplist).ToString();
                objbill.EmrgNo = txt_emrgno.Text.Trim();
                objbill.UHID = Convert.ToInt64(lbl_UHIDTemp.Text == "" ? "0" : lbl_UHIDTemp.Text);
                objbill.TotalBillAmount = Convert.ToDecimal(txttotalamount.Text == "" ? "0.0" : txttotalamount.Text);
                objbill.TotalDiscount = Convert.ToDecimal(txtdiscount.Text == "" ? "0.0" : txtdiscount.Text);
                objbill.AdjustedAmount = Convert.ToDecimal(txtadjustedamount.Text == "" ? "0.0" : txtadjustedamount.Text);
                objbill.TotalPaidAmount = Convert.ToDecimal(txt_totalpaid.Text == "" ? "0.0" : txt_totalpaid.Text);
                objbill.TotalPayableAmount = Convert.ToDecimal(txt_payableamount.Text == "" ? "0.0" : txt_payableamount.Text);
                objbill.TotalDuemanount = Convert.ToDecimal(txt_dueamount.Text == "" ? "0.0" : txt_dueamount.Text);
                objbill.Remarks = txtdiscremoarks.Text.Trim();
                objbill.PaymentMode = Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue);
                objbill.BankName = txtbank.Text == "" ? null : txtbank.Text;
                objbill.Chequenumber = txt_chequenumber.Text == "" ? "" : txt_chequenumber.Text;
                objbill.Invoicenumber = txtinvoicenumber.Text == "" ? "" : txtinvoicenumber.Text;
                objbill.BankID = Convert.ToInt32(hdnbankID.Value == "" || hdnbankID.Value == null ? "0" : hdnbankID.Value);
                objbill.FinancialYearID = LogData.FinancialYearID;
                objbill.EmployeeID = LogData.EmployeeID;
                objbill.AddedBy = LogData.AddedBy;
                objbill.HospitalID = LogData.HospitalID;
                objbill.IsActive = LogData.IsActive;
                objbill.IPaddress = LogData.IPaddress;
                int flag = 0;
                foreach (GridViewRow row in GVTPAList.Rows)
                {
                    Label lblCatID = (Label)GVTPAList.Rows[row.RowIndex].Cells[0].FindControl("lblCatID");
                    Label lblSubCatID = (Label)GVTPAList.Rows[row.RowIndex].Cells[0].FindControl("lblSubCatID");
                    Label lblDisAmount = (Label)GVTPAList.Rows[row.RowIndex].Cells[0].FindControl("lblDisAmount");

                    Discount ObjDetails = new Discount();


                    ObjDetails.PatientCatID = Convert.ToInt32(lblCatID.Text == "" ? "0" : lblCatID.Text);
                    ObjDetails.SubCatID = Convert.ToInt32(lblSubCatID.Text == "" ? "0" : lblSubCatID.Text);
                    ObjDetails.DiscountAmount = Convert.ToDecimal(lblDisAmount.Text == "" ? "0" : lblDisAmount.Text);

                    ListDiscount.Add(ObjDetails);
                    flag = 1;
                }
                objbill.extraDiscountXML = XmlConvertor.ExtraDiscountDatatoXML(ListDiscount).ToString();
                objbill.isExtradiscount = flag;
                if (Convert.ToDecimal(txt_totalpaid.Text == "" ? "0" : txt_totalpaid.Text) > Convert.ToDecimal(txt_payableamount.Text == "" ? "0" : txt_payableamount.Text))
                {
                    Messagealert_.ShowMessage(lblmessage, "ExceedAmount", 0);
                    txt_totalpaid.Focus();
                    btnaddresponsibility.Visible = true;
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (Convert.ToDecimal(txt_totalpaid.Text == "" ? "0" : txt_totalpaid.Text) < Convert.ToDecimal(txt_payableamount.Text == "" ? "0" : txt_payableamount.Text))
                {
                    txt_dueamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_payableamount.Text == "" ? "0" : txt_payableamount.Text) - Convert.ToDecimal(txt_totalpaid.Text == "" ? "0" : txt_totalpaid.Text)).ToString());
                }
                if (Session["Responsibilty"] == null && Convert.ToDecimal(txt_dueamount.Text == "" ? "0" : txt_dueamount.Text) > 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Dueresponsible", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {

                }
                List<EmrgAdmissionData> result = objstdBO.Update_EMRGFinal_BillDetails(objbill);
                if (result.Count > 0)
                {
                    lbl_bill.InnerText = "Bill Number";
                    txt_billnumber.Text = result[0].BillNo.ToString();
                    hdnbillnumber.Value = result[0].BillNo.ToString();
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    btnsave.Attributes["disabled"] = "disabled";
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
                    Session["Responsibilty"] = null;
                    Session["DiscountReqList"] = null;
                    Session["DiscountList"] = null;
                    ddl_settlementmode.SelectedIndex = 0;
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

       
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txt_emrgNoList.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            ddlstatus.SelectedIndex = 0;
            GvEmrgfinalbill.DataSource = null;
            GvEmrgfinalbill.DataBind();
            GvEmrgfinalbill.Visible = false;
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
        protected DataTable GetDatafromDatabase()
        {
            List<EmrgAdmissionData> ServiceDetails = GetEMRGbillList(0);
            List<EMRGServiceListDataTOeXCEL> ListexcelData = new List<EMRGServiceListDataTOeXCEL>();
            int i = 0;
            foreach (EmrgAdmissionData row in ServiceDetails)
            {
                EMRGServiceListDataTOeXCEL Ecxeclpat = new EMRGServiceListDataTOeXCEL();
                Ecxeclpat.EmrgNo = ServiceDetails[i].EmrgNo;

                Ecxeclpat.UHID = ServiceDetails[i].UHID;
                Ecxeclpat.PatientName = ServiceDetails[i].PatientName;
                Ecxeclpat.TotalBillAmount = ServiceDetails[i].TotalBillAmount;
                Ecxeclpat.PaidAmount = ServiceDetails[i].PaidAmount;
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
                // Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo prop in Props)
                {
                    //  Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];

                    for (int i = 0; i < Props.Length; i++)
                    {
                        //       inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //     put a breakpoint here and check datatable
                return dataTable;
            }
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "ExportEnable", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage2.Visible = false;
            }
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
                divmsg3.Attributes["class"] = "FailAlert";
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
                    GvEmrgfinalbill.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvEmrgfinalbill.Columns[12].Visible = false;
                    GvEmrgfinalbill.Columns[13].Visible = false;
                    GvEmrgfinalbill.RenderControl(hw);
                    GvEmrgfinalbill.HeaderRow.Style.Add("width", "15%");
                    GvEmrgfinalbill.HeaderRow.Style.Add("font-size", "10px");
                    GvEmrgfinalbill.Style.Add("text-decoration", "none");
                    GvEmrgfinalbill.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvEmrgfinalbill.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=EmergencyFinalBillDetails.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
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
                wb.Worksheets.Add(dt, "IP service record");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=EmergencyFinalBillDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblresult, "Exported", 1);
                divmsg3.Attributes["class"] = "SucessAlert";
            }
        }
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            if (txtpatientNames.Text != "")
            {
                var source = txtpatientNames.Text.ToString();
                if (source.Contains(":"))
                {
                    bindgridList();
                }
                else
                {
                    txtpatientNames.Text = "";
                    txtpatientNames.Focus();
                }
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
                List<EmrgAdmissionData> objdeposit = GetEMRGbillList(0);
                if (objdeposit.Count > 0)
                {
                    GvEmrgfinalbill.DataSource = objdeposit;
                    GvEmrgfinalbill.DataBind();
                    GvEmrgfinalbill.Visible = true;
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
                    GvEmrgfinalbill.DataSource = null;
                    GvEmrgfinalbill.DataBind();
                    GvEmrgfinalbill.Visible = true;
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
        public List<EmrgAdmissionData> GetEMRGbillList(int curIndex)
        {
            EmrgAdmissionData objbill = new EmrgAdmissionData();
            EmrgAdmissionBO objbillingBO = new EmrgAdmissionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            //objbill.EmrgNo = txt_emrgNoList.Text.Trim() == "" ? null : txt_emrgNoList.Text.Trim();
            objbill.PatientName = null; //txtpatientNames.Text == "" ? null : txtpatientNames.Text.Trim();

            string EmgNo;
            var source = txtpatientNames.Text.ToString();
            if (source.Contains(":"))
            {
                EmgNo = source.Substring(source.LastIndexOf(':') + 1);
                objbill.EmrgNo = EmgNo.Trim();
            }
            else
            {
                objbill.EmrgNo = null;
            }
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
            return objbillingBO.GetEMRGbillList(objbill);
        }
        protected void txt_totalpaid_TextChanged(object sender, EventArgs e)
        {
            paiyableAmountChange();
        }
        private void paiyableAmountChange()
        {
            if (Convert.ToDecimal(txt_payableamount.Text == "" ? "0.0" : txt_payableamount.Text) > 0 && Convert.ToDecimal(txt_totalpaid.Text == "" ? "0.0" : txt_totalpaid.Text) <= Convert.ToDecimal(txttotalamount.Text == "" ? "0.0" : txttotalamount.Text))
            {
                txt_dueamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_payableamount.Text == "" ? "0.0" : txt_payableamount.Text) - Convert.ToDecimal(txt_totalpaid.Text == "" ? "0.0" : txt_totalpaid.Text)).ToString());
                if (Convert.ToDecimal(txt_dueamount.Text == "" || txt_dueamount.Text == "0" ? "0" : txt_dueamount.Text) > 0)
                {
                    txt_totaldueamount.Text = txt_dueamount.Text;
                    btnaddresponsibility.Visible = true;
                }
                else
                {
                    btnaddresponsibility.Visible = false;
                }
                txt_totalpaid.Focus();
            }
            else
            {
                txt_payableamount.Text = txt_payableamount.Text;
                txt_totalpaid.Text = txt_payableamount.Text;
                txt_dueamount.Text = "";
                txt_totaldueamount.Text = "";
            }
        }
        protected void dddl_responsiblestaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gv_responsibledetais.Rows)
            {
                Label EmployeeID = (Label)gv_responsibledetais.Rows[row.RowIndex].Cells[0].FindControl("lbl_employeeID");
                if (Convert.ToInt32(EmployeeID.Text) == Convert.ToInt32(ddl_responsiblestaff.SelectedValue == "" ? "0" : ddl_responsiblestaff.SelectedValue))
                {
                    Messagealert_.ShowMessage(message, "duplicate", 0);
                    this.mddueresponsible.Show();
                    return;
                }
                else
                {
                    message.Visible = false;
                    this.mddueresponsible.Show();
                }
            }
            List<EmployeeData> Employeelist = Session["Responsibilty"] == null ? new List<EmployeeData>() : (List<EmployeeData>)Session["Responsibilty"];
            EmployeeData objemployee = new EmployeeData();
            objemployee.EmployeeID = Convert.ToInt64(ddl_responsiblestaff.SelectedValue == "" ? "0" : ddl_responsiblestaff.SelectedValue);
            objemployee.EmpName = ddl_responsiblestaff.SelectedItem.Text;
            objemployee.Amount = Convert.ToDecimal(txt_totaldueamount.Text.Trim() == "" ? "0" : txt_totaldueamount.Text.Trim());
            Employeelist.Add(objemployee);
            if (Employeelist.Count > 0)
            {
                gv_responsibledetais.DataSource = Employeelist;
                gv_responsibledetais.DataBind();
                gv_responsibledetais.Visible = true;
                ddl_responsiblestaff.SelectedIndex = 0;
                Session["Responsibilty"] = Employeelist;
                btnaddresponsibility.Visible = true;
            }
            else
            {
                gv_responsibledetais.DataSource = Employeelist;
                gv_responsibledetais.DataBind();
                gv_responsibledetais.Visible = true;
            }
            this.mddueresponsible.Show();
        }
        protected void ddl_settlementmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_settlementmode.SelectedValue == "0")
            {
                btnsave.Text = "Pay";
                txt_totalpaid.Text = txt_payableamount.Text;
                txt_totalpaid.ReadOnly = false;
                btnlinkdiscount.Visible = true;
            }
            if (ddl_settlementmode.SelectedValue == "1")
            {
                btnsave.Text = "Pay";
                //txt_totalpaid.Text = txt_payableamount.Text;
                txt_totalpaid.ReadOnly = false;
                btnlinkdiscount.Visible = true;
                if (Convert.ToDecimal(txt_dueamount.Text == "" || txt_dueamount.Text == "0" ? "0" : txt_dueamount.Text) > 0)
                {
                    txt_totaldueamount.Text = txt_dueamount.Text;
                    btnaddresponsibility.Visible = true;
                }
                else
                {
                    btnaddresponsibility.Visible = false;
                }
            }
            if (ddl_settlementmode.SelectedValue == "2")
            {
                btnsave.Text = "Transfer";
                txt_dueamount.Text = "";
                txt_totalpaid.Text = "";
                txt_totalpaid.ReadOnly = true;
                btnlinkdiscount.Visible = false;
                btnaddresponsibility.Visible = false;
                btn_refund.Attributes["disabled"] = "disabled";
            }
        }
        protected void gv_responsibledetais_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gv_responsibledetais.Rows[i];
                    List<EmployeeData> Employeelist = Session["Responsibilty"] == null ? new List<EmployeeData>() : (List<EmployeeData>)Session["Responsibilty"];
                    Employeelist.RemoveAt(i);
                    message.Visible = false;
                    Session["DiscountList"] = Employeelist;
                    gv_responsibledetais.DataSource = Employeelist;
                    gv_responsibledetais.DataBind();
                    this.mddueresponsible.Show();
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(message, "system", 0);
            }
        }
        protected void btnprint_Click(object sender, EventArgs e)
        {
            string url = "../MedEmergency/Reports/ReportViewer.aspx?option=FinalBill&BillNo=" + hdnbillnumber.Value.ToString();
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
        protected void btnprints_Click(object sender, EventArgs e)
        {
            string patientname = txtpatientNames.Text.Trim() == "" ? "" : txtpatientNames.Text.Trim();
            string emrgnumber = txt_emrgNoList.Text.Trim() == "" ? "" : txt_emrgNoList.Text.Trim();
            string status = ddlstatus.SelectedValue;
            string billnumber = txt_billnos.Text.Trim() == "" ? "" : txt_billnos.Text.Trim();
            Int64 collectedby = Convert.ToInt64(ddlcollectedby.SelectedValue == "" ? "0" : ddlcollectedby.SelectedValue);
            string datefrom = txtdatefrom.Text.Trim() == "" ? "" : txtdatefrom.Text.Trim();
            string dateto = txtto.Text.Trim() == "" ? "" : txtto.Text.Trim();
            int paymode = Convert.ToInt32(ddlpaymentmodes.SelectedValue == "" ? "0" : ddlpaymentmodes.SelectedValue);
            string url = "../MedEmergency/Reports/ReportViewer.aspx?option=Emrgfinalbillist&BillNo=" + billnumber + "Emrgno=" + emrgnumber + "Collected=" + collectedby + "From=" + datefrom + "To=" + dateto + "Paymode=" + paymode + "PatientName=" + patientname + "Status=" + status;
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
        protected void txtdiscount_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txt_payableamount.Text == "" ? "0.0" : txt_payableamount.Text) > 0 && Convert.ToDecimal(txtdiscount.Text == "" ? "0.0" : txtdiscount.Text) <= Convert.ToDecimal(txt_payableamount.Text == "" ? "0.0" : txt_payableamount.Text))
            {
                txt_totalpaid.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_payableamount.Text == "" ? "0.0" : txt_payableamount.Text) - Convert.ToDecimal(txtdiscount.Text == "" ? "0.0" : txtdiscount.Text)).ToString());
            }
            else
            {
                txt_totalpaid.Text = txt_payableamount.Text;
                txtdiscount.Text = "";
            }
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
                objrefund.Paymode = Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue);
                objrefund.BankName = txtbank.Text.Trim();
                Listbill = RefundBO.UpdateRefundDetails(objrefund);
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
        protected void btnAddTpa_Click(object sender, EventArgs e)
        {
            Decimal TotalDiscount = 0;
            if (ddl_tpa_patient_cat.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblPopUpmsg, "Please Select Category!", 0);
                divPopMsg.Visible = true;
                ddl_tpa_patient_cat.Focus();
                this.MDTPA.Show();
                return;
            }
            else
            {
                lblPopUpmsg.Visible = false;
                divPopMsg.Visible = false;
                this.MDTPA.Show();
            }
            if (ddl_tpa_patient_cat.SelectedValue == "4")
            {
                if (ddl_tpa_patient_sub_cat.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblPopUpmsg, "Please Select sub Category!", 0);
                    divPopMsg.Visible = true;
                    ddl_tpa_patient_sub_cat.Focus();
                    this.MDTPA.Show();
                    return;
                }
                else
                {
                    lblPopUpmsg.Visible = false;
                    divPopMsg.Visible = false;
                    this.MDTPA.Show();
                }
            }
            else
            {
                lblPopUpmsg.Visible = false;
                divPopMsg.Visible = false;
                this.MDTPA.Show();
            }

            if (Convert.ToDecimal(txt_tpa_amount.Text == "" ? "0" : txt_tpa_amount.Text) <= 0)
            {
                Messagealert_.ShowMessage(lblPopUpmsg, "Discount", 0);
                divPopMsg.Visible = true;
                txt_tpa_amount.Focus();
                this.MDTPA.Show();
                return;
            }
            else
            {
                lblPopUpmsg.Visible = false;
                divPopMsg.Visible = false;
                this.MDTPA.Show();
            }
            Decimal total_dis = Convert.ToDecimal(txtdiscount.Text == "" ? "0" : txtdiscount.Text) + Convert.ToDecimal(txt_tpa_amount.Text == "" ? "0" : txt_tpa_amount.Text);

            if (Convert.ToDecimal(txt_payableamount.Text == "" ? "0.00" : txt_payableamount.Text) + Convert.ToDecimal(txtdiscount.Text == "" ? "0.00" : txtdiscount.Text) < total_dis)
            {

                txtdiscount.Focus();
                ddl_tpa_patient_cat.SelectedIndex = 0;
                ddl_tpa_patient_sub_cat.SelectedIndex = 0;
                Messagealert_.ShowMessage(lblPopUpmsg, "DiscountOver", 0);
                divPopMsg.Visible = true;
                txtdiscount.Focus();
                this.MDTPA.Show();
                return;
            }
            else
            {
                txtdiscount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txtdiscount.Text == "" ? "0" : txtdiscount.Text) + Convert.ToDecimal(txt_tpa_amount.Text == "" ? "0.0" : txt_tpa_amount.Text)).ToString());
                txt_payableamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_payableamount.Text == "" ? "0" : txt_payableamount.Text) - Convert.ToDecimal(txt_tpa_amount.Text == "" ? "0.0" : txt_tpa_amount.Text)).ToString());
                txt_totalpaid.Text = txt_payableamount.Text;
                divPopMsg.Visible = false;
                lblPopUpmsg.Visible = false;
                this.MDTPA.Show();
            }
            List<Discount> DiscountList = Session["DiscountList"] == null ? new List<Discount>() : (List<Discount>)Session["DiscountList"];
            Discount objdiscount = new Discount();
            objdiscount.PatientCatID = Convert.ToInt32(ddl_tpa_patient_cat.SelectedValue == "" ? "0" : ddl_tpa_patient_cat.SelectedValue);
            objdiscount.PatCat = ddl_tpa_patient_cat.SelectedItem.Text;


            if (ddl_tpa_patient_cat.SelectedValue == "4")
            {
                objdiscount.SubCatID = Convert.ToInt32(ddl_tpa_patient_sub_cat.SelectedValue == "" ? "0" : ddl_tpa_patient_sub_cat.SelectedValue);
                objdiscount.subCat = ddl_tpa_patient_sub_cat.SelectedItem.Text;
            }
            else
            {
                objdiscount.SubCatID = 0;
                objdiscount.subCat = "";
            }
            objdiscount.DiscountAmount = Convert.ToDecimal(txt_tpa_amount.Text == "" ? "0.0" : txt_tpa_amount.Text);

            TotalDiscount = TotalDiscount + Convert.ToDecimal(txt_tpa_amount.Text == "" ? "0.0" : txt_tpa_amount.Text);

            DiscountList.Add(objdiscount);
            if (DiscountList.Count > 0)
            {
                GVTPAList.DataSource = DiscountList;
                GVTPAList.DataBind();
                GVTPAList.Visible = true;
                txt_tpa_amount.Text = "";
                txt_tpa_amount.Focus();
                ddl_tpa_patient_cat.SelectedIndex = 0;
                ddl_tpa_patient_sub_cat.SelectedIndex = 0;
                Session["DiscountList"] = DiscountList;

            }
            else
            {
                GVTPAList.DataSource = null;
                GVTPAList.DataBind();
                GVTPAList.Visible = true;
            }
            this.MDTPA.Show();
        }
        protected void btnlinkdiscount_Click(object sender, EventArgs e)
        {
            MasterLookupBO mstlookup = new MasterLookupBO();

            Commonfunction.PopulateDdl(ddl_tpa_patient_cat, mstlookup.GetLookupsList(LookupName.GetAllInsurance));

            Commonfunction.PopulateDdl(ddl_tpa_patient_sub_cat, mstlookup.GetLookupsList(LookupName.TPAList));
            ddl_tpa_patient_sub_cat.Attributes["disabled"] = "disabled";



            List<EMRDiscountListData> DiscountReqList = Session["DiscountReqList"] == null ? new List<EMRDiscountListData>() : (List<EMRDiscountListData>)Session["DiscountReqList"];
            if (DiscountReqList.Count > 0)
            {
                GVDiscountReq.DataSource = DiscountReqList;
                GVDiscountReq.DataBind();
                GVDiscountReq.Visible = true;
                txtDisTotalBillAmount.Text = txt_payableamount.Text;
            }
            else
            {
                EmrgAdmissionBO objBO = new EmrgAdmissionBO();
                EmrgAdmissionData objData = new EmrgAdmissionData();
                objData.EmrgNo = txt_emrgno.Text;
                List<EMRDiscountListData> result = objBO.GetDiscountListForEmergency(objData);
                GVDiscountReq.DataSource = result;
                GVDiscountReq.DataBind();
                GVDiscountReq.Visible = true;
                txtDisTotalBillAmount.Text = result[0].TotalBill.ToString();
                totalCalculate();
            }
            this.MDTPA.Show();
        }
        protected void GVTPAList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GVTPAList.Rows[i];
                    List<Discount> DiscountList = Session["DiscountList"] == null ? new List<Discount>() : (List<Discount>)Session["DiscountList"];
                    if (DiscountList.Count > 0)
                    {
                        Decimal Discount = DiscountList[i].DiscountAmount;
                        txtdiscount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txtdiscount.Text == "" ? "0" : txtdiscount.Text) - Discount).ToString());
                        txt_payableamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_payableamount.Text == "" ? "0" : txt_payableamount.Text) + Discount).ToString());
                        txt_totalpaid.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_payableamount.Text == "" ? "0" : txt_payableamount.Text) - Convert.ToDecimal(txt_tpa_amount.Text == "" ? "0.0" : txt_tpa_amount.Text)).ToString());
                    }
                    DiscountList.RemoveAt(i);
                    Session["DiscountList"] = DiscountList;
                    GVTPAList.DataSource = DiscountList;
                    GVTPAList.DataBind();

                    this.MDTPA.Show();
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblPopUpmsg, "system", 0);
            }
        }
        protected void ddl_tpa_patient_cat_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            if (ddl_tpa_patient_cat.SelectedIndex == 0)
            {
                ddl_tpa_patient_sub_cat.SelectedIndex = 0;
                ddl_tpa_patient_sub_cat.Attributes["disabled"] = "disabled";
            }
            else
            {
                if (ddl_tpa_patient_cat.SelectedValue == "4")
                {
                    ddl_tpa_patient_sub_cat.Attributes.Remove("disabled");
                    Commonfunction.PopulateDdl(ddl_tpa_patient_sub_cat, mstlookup.GetLookupsList(LookupName.TPAList));
                }
                else
                {
                    ddl_tpa_patient_sub_cat.SelectedIndex = 0;
                    ddl_tpa_patient_sub_cat.Attributes["disabled"] = "disabled";
                }

            }
            this.MDTPA.Show();
        }
        protected void ddl_discount_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = ((GridViewRow)((DropDownList)sender).NamingContainer);
            discountChange(sender, currentRow);
        }
        public void discountChange(object sender, GridViewRow currentRow)
        {

            DropDownList ddl_discount_type = (DropDownList)currentRow.FindControl("ddl_discount_type");
            TextBox txt_dis_value = (TextBox)currentRow.FindControl("txt_dis_value");
            Label lblNetAmount = (Label)currentRow.FindControl("lblAmount");
            Label lbl_discount_amt = (Label)currentRow.FindControl("lbl_discount_amt");
            decimal value = Convert.ToDecimal(txt_dis_value.Text == "" ? "0" : txt_dis_value.Text);
            decimal NetAmount = Convert.ToDecimal(lblNetAmount.Text == "" ? "0" : lblNetAmount.Text);
            if (ddl_discount_type.SelectedIndex == 0)
            {
                if (value > NetAmount)
                {
                    txt_dis_value.Text = "";
                    lbl_discount_amt.Text = "";
                    Messagealert_.ShowMessage(lblPopUpmsg, "DiscountAmount", 0);
                    divPopMsg.Visible = true;
                    divPopMsg.Attributes["class"] = "FailAlert";
                    totalCalculate();
                }
                else
                {
                    Decimal total_dis = Convert.ToDecimal(txtdiscount.Text == "" ? "0" : txtdiscount.Text) + value;

                    if (total_dis > Convert.ToDecimal(txt_payableamount.Text == "" ? "0" : txt_payableamount.Text) + Convert.ToDecimal(txtdiscount.Text == "" ? "0.00" : txtdiscount.Text))
                    {
                        txt_dis_value.Text = "";
                        lbl_discount_amt.Text = "";
                        Messagealert_.ShowMessage(lblPopUpmsg, "DiscountAmount", 0);
                        divPopMsg.Visible = true;
                        divPopMsg.Attributes["class"] = "FailAlert";
                        totalCalculate();
                    }
                    else
                    {
                        lbl_discount_amt.Text = Commonfunction.Getrounding(value.ToString());
                        totalCalculate();
                    }
                }
            }
            else
            {
                if (value > 100)
                {
                    txt_dis_value.Text = "";
                    lbl_discount_amt.Text = "";
                    Messagealert_.ShowMessage(lblPopUpmsg, "Percentage", 0);
                    divPopMsg.Visible = true;
                    divPopMsg.Attributes["class"] = "FailAlert";
                    totalCalculate();
                }
                else
                {
                    decimal pcValue = 0;
                    pcValue = ((value / 100) * NetAmount);
                    Decimal total_dis = Convert.ToDecimal(txtdiscount.Text == "" ? "0" : txtdiscount.Text) + pcValue;

                    if (total_dis > Convert.ToDecimal(txt_payableamount.Text == "" ? "0" : txt_payableamount.Text) + Convert.ToDecimal(txtdiscount.Text == "" ? "0.00" : txtdiscount.Text))
                    {
                        txt_dis_value.Text = "";
                        lbl_discount_amt.Text = "";
                        Messagealert_.ShowMessage(lblPopUpmsg, "DiscountAmount", 0);
                        divPopMsg.Visible = true;
                        divPopMsg.Attributes["class"] = "FailAlert";
                        totalCalculate();
                    }
                    else
                    {
                        lbl_discount_amt.Text = Commonfunction.Getrounding(pcValue.ToString());
                        totalCalculate();
                    }

                }
            }
            this.MDTPA.Show();
        }
        public void totalCalculate()
        {
            decimal totalDiscount = 0;
            List<EMRDiscountListData> DiscountReqList = new List<EMRDiscountListData>();
            foreach (GridViewRow row in GVDiscountReq.Rows)
            {
                EMRDiscountListData objData = new EMRDiscountListData();
                Label lbl_discount_amt = (Label)GVDiscountReq.Rows[row.RowIndex].Cells[0].FindControl("lbl_discount_amt");

                Label lblDoctor = (Label)GVDiscountReq.Rows[row.RowIndex].Cells[0].FindControl("lblDoctor");
                Label lblDoctorID = (Label)GVDiscountReq.Rows[row.RowIndex].Cells[0].FindControl("lblDoctorID");
                Label lblAmount = (Label)GVDiscountReq.Rows[row.RowIndex].Cells[0].FindControl("lblAmount");
                Label lblDiscountStatus = (Label)GVDiscountReq.Rows[row.RowIndex].Cells[0].FindControl("lblDiscountStatus");
                DropDownList ddl_discount_type = (DropDownList)GVDiscountReq.Rows[row.RowIndex].Cells[0].FindControl("ddl_discount_type");
                TextBox txt_dis_value = (TextBox)GVDiscountReq.Rows[row.RowIndex].Cells[0].FindControl("txt_dis_value");

                totalDiscount = totalDiscount + (Convert.ToDecimal(lbl_discount_amt.Text == "" ? "0" : lbl_discount_amt.Text));

                objData.ShareID = Convert.ToInt64(lblDoctorID.Text == "" ? "0" : lblDoctorID.Text);
                objData.DoctorName = lblDoctor.Text;
                objData.ShareAmount = Convert.ToDecimal(lblAmount.Text == "" ? "0" : lblAmount.Text);
                objData.disType = Convert.ToInt32(ddl_discount_type.SelectedValue == "" ? "0" : ddl_discount_type.SelectedValue);
                objData.disValue = Convert.ToDecimal(txt_dis_value.Text == "" ? "0" : txt_dis_value.Text);
                objData.discountAmount = Convert.ToDecimal(lbl_discount_amt.Text == "" ? "0" : lbl_discount_amt.Text);
                objData.TotalBill = Convert.ToDecimal(txtDisTotalBillAmount.Text == "" ? "0" : txtDisTotalBillAmount.Text);
                objData.DiscountStatus = Convert.ToInt32(lblDiscountStatus.Text == "" ? "0" : lblDiscountStatus.Text);
                DiscountReqList.Add(objData);
            }
            txtTotalDiscount.Text = totalDiscount.ToString();
            decimal TotalAmount = Convert.ToDecimal(txttotalamount.Text == "" ? "0" : txttotalamount.Text.ToString());
            List<Discount> DiscountList = Session["DiscountList"] == null ? new List<Discount>() : (List<Discount>)Session["DiscountList"];
            if (DiscountList.Count > 0)
            {

                foreach (Discount row in DiscountList)
                {
                    totalDiscount = totalDiscount + row.DiscountAmount;
                }
            }
            txtdiscount.Text = Commonfunction.Getrounding(totalDiscount.ToString());
            txt_payableamount.Text = Commonfunction.Getrounding(((TotalAmount - Convert.ToDecimal(txtadjustedamount.Text == "" ? "0" : txtadjustedamount.Text)) - Convert.ToDecimal(txtdiscount.Text == "" ? "0" : txtdiscount.Text)).ToString());
            txt_totalpaid.Text = txt_payableamount.Text;
            Session["DiscountReqList"] = DiscountReqList;
            if (DiscountReqList[0].DiscountStatus == 0)
            {
                if (Convert.ToDecimal(txtTotalDiscount.Text == "" ? "0" : txtTotalDiscount.Text) > 0)
                {
                    txtdiscount.ReadOnly = true;
                    btnsave.Visible = false;
                    btnDisReq.Visible = true;
                }
                else
                {
                    btnsave.Visible = true;
                    btnDisReq.Visible = false;
                }

                //this.MDTPA.Show();


            }
            else
            {
                if (DiscountReqList[0].DiscountStatus == 3)
                {
                    btnsave.Visible = true;
                    btnDisReq.Visible = false;
                }
                else
                {
                    txtdiscount.ReadOnly = true;
                    btnsave.Visible = false;
                    btnDisReq.Visible = false;
                }
            }

        }
        protected void txt_dis_value_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
            discountChange(sender, currentRow);
        }
        protected void GVDiscountReq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDisCountType = (Label)e.Row.FindControl("lblDisCountType");
                TextBox txt_dis_value = (TextBox)e.Row.FindControl("txt_dis_value");
                Label lblDiscountStatus = (Label)e.Row.FindControl("lblDiscountStatus");
                DropDownList ddl_discount_type = (DropDownList)e.Row.FindControl("ddl_discount_type");
                ddl_discount_type.SelectedValue = lblDisCountType.Text;
                if (Convert.ToInt32(lblDiscountStatus.Text == "" ? "0" : lblDiscountStatus.Text) > 0)
                {
                    ddl_discount_type.Attributes["disabled"] = "disabled";
                    txt_dis_value.ReadOnly = true;
                }
            }
        }
        protected void btnDisReq_Click(object sender, EventArgs e)
        {
            List<EMRDiscountListData> DiscountReqList = Session["DiscountReqList"] == null ? new List<EMRDiscountListData>() : (List<EMRDiscountListData>)Session["DiscountReqList"];
            if (DiscountReqList.Count > 0)
            {
                if (txtDiscountRemark.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblPopUpmsg, "Remarks", 0);
                    divPopMsg.Visible = true;
                    this.MDTPA.Show();
                    return;
                }
                else
                {

                    divPopMsg.Visible = false;
                    this.MDTPA.Show();

                }
            }
            else
            {
                Messagealert_.ShowMessage(lblPopUpmsg, "NoDiscount", 0);
                divPopMsg.Visible = true;
                this.MDTPA.Show();
                return;
            }
            DiscountBO objBO = new DiscountBO();
            AdmissionDiscountData objData = new AdmissionDiscountData();
            objData.XMLData = XmlConvertor.AdmissionDiscountToXML(DiscountReqList).ToString();
            objData.IPNo = txt_emrgno.Text;
            objData.UHID = Convert.ToInt64(lbl_UHIDTemp.Text);
            objData.Remarks = txtDiscountRemark.Text;
            objData.TotalRequestAmount = Convert.ToDecimal(txtTotalDiscount.Text);
            objData.TotalAmount = Convert.ToDecimal(txtDisTotalBillAmount.Text);
            objData.ServiceType = 4;
            objData.BillingType = 0;
            objData.BillNo = "";
            objData.EmployeeID = LogData.EmployeeID;
            objData.HospitalID = LogData.HospitalID;
            objData.FinancialYearID = LogData.FinancialYearID;

            List<DiscountOutput> result = objBO.UpdateDiscountRequestForAdmission(objData);
            if (result[0].Resultoutput > 0)
            {
                if (result[0].Resultoutput == 1)
                {
                    Messagealert_.ShowMessage(lblPopUpmsg, "DiscountReq", 1);
                    divPopMsg.Visible = true;
                    divPopMsg.Attributes["class"] = "SucessAlert";
                    btnDisReq.Visible = false;
                    ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>pushMessage('" + txtDiscountRemark.Text + "','" + result[0].ID + "');</script>", false);

                }
                else
                {
                    if (result[0].Resultoutput == 5)
                    {
                        btnDisReq.Visible = false;
                        Messagealert_.ShowMessage(lblPopUpmsg, "DiscountDuplicate", 0);
                        divPopMsg.Visible = true;
                        divPopMsg.Attributes["class"] = "FailAlert";
                    }
                }
            }
            else
            {

                Messagealert_.ShowMessage(lblPopUpmsg, "Error", 0);
                divPopMsg.Visible = true;
                divPopMsg.Attributes["class"] = "FailAlert";
            }
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Decimal TotalDue = 0; int count = 0;
            foreach (GridViewRow row in gv_responsibledetais.Rows)
            {
                TextBox lbl_amnt = (TextBox)gv_responsibledetais.Rows[row.RowIndex].Cells[0].FindControl("lbl_amnt");
                if (Convert.ToDecimal(lbl_amnt.Text == "" ? "0" : lbl_amnt.Text) <= 0)
                {
                    lbl_amnt.Focus();
                    Messagealert_.ShowMessage(message, "Respamount", 0);
                    this.mddueresponsible.Show();
                    return;
                }
                else
                {
                    message.Visible = false;
                }
                count = count + 1;
                TotalDue = TotalDue + Convert.ToDecimal(lbl_amnt.Text == "" ? "0" : lbl_amnt.Text);
            }
            if (TotalDue != Convert.ToDecimal(txt_totaldueamount.Text == "" ? "0" : txt_totaldueamount.Text) && count > 0)
            {
                this.mddueresponsible.Show();
                Messagealert_.ShowMessage(message, "Dueamount", 0);
                return;
            }
            else
            {
                message.Visible = false;
            }
        }
    }
}