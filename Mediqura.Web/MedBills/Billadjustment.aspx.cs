using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedEmergencyData;
using Mediqura.CommonData.PatientData;
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
    public partial class Billadjustment : BasePage
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
            Commonfunction.PopulateDdl(ddl_patientcategory, mstlookup.GetLookupsList(LookupName.AllPatientType));
            Commonfunction.PopulateDdl(ddl_admissiondoctor, mstlookup.GetLookupsList(LookupName.Doctor));
            AutoCompleteExtender3.ContextKey = "1";
            ddl_patientcategory.Attributes["disabled"] = "disabled";
            ddl_admissiondoctor.Attributes["disabled"] = "disabled";
        }
        protected void ddl_billcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender3.ContextKey = ddl_billcategory.SelectedValue;
            ddl_patientcategory.SelectedIndex = 0;
            ddl_servicecategory.SelectedIndex = 0;
            txt_patientNumber.Text = "";
            txt_admissiontime.Text = "";
            txt_name.Text = "";
            Gv_Ipbilldetails.DataSource = null;
            Gv_Ipbilldetails.Visible = false;
            txt_totalbill.Text = "";
            txt_payable.Text = "";
            ddl_admissiondoctor.SelectedIndex = 0;
            txt_deposited.Text = "";
            lblmessage.Text = "";

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPatientNumber(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientNumber = prefixText;
            Objpaic.BillCategory = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetPatientNumberByBillCategory(Objpaic);

            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientNumber.ToString());
            }
            return list;
        }
        protected void txt_patientNumber_TextChanged(object sender, EventArgs e)
        {
            GetPatientBillDetails();
        }
        protected void GetPatientBillDetails()
        {
            BillAdjustmentData objData = new BillAdjustmentData();
            FInalBillBO objBO = new FInalBillBO();
            List<BillAdjustmentData> getResult = new List<BillAdjustmentData>();
            if (txt_patientNumber.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Please enter Emergency or IP number.", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_patientNumber.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            objData.PatientNumber = txt_patientNumber.Text == "" ? "" : txt_patientNumber.Text.Trim();
            objData.BillCategory = Convert.ToInt32(ddl_billcategory.SelectedValue == "" ? "0" : ddl_billcategory.SelectedValue);
            objData.PatientCategory = Convert.ToInt32(ddl_patientcategory.SelectedValue == "" ? "0" : ddl_patientcategory.SelectedValue);
            objData.ServiceCategoryID = Convert.ToInt32(ddl_servicecategory.SelectedValue == "" ? "0" : ddl_servicecategory.SelectedValue);
            getResult = objBO.GetBilldetails(objData);
            if (getResult.Count > 0)
            {
                Gv_Ipbilldetails.Visible = true;
                txt_name.Text = getResult[0].PatientDetail.ToString();
                txt_totalbill.Text = Commonfunction.Getrounding(getResult[0].TotalAmount.ToString());
                txt_deposited.Text = Commonfunction.Getrounding(getResult[0].TotalBalance.ToString());
                txt_payable.Text = Commonfunction.Getrounding(getResult[0].TotalPayable.ToString());
                ddl_patientcategory.SelectedValue = Commonfunction.Getrounding(getResult[0].PatientCategory.ToString());
                ddl_admissiondoctor.SelectedValue = Commonfunction.Getrounding(getResult[0].DoctorID.ToString());
                txt_admissiontime.Text = getResult[0].AdmissionDate.ToString("dd/MM/yyyy hh:mm:ss tt");
                Gv_Ipbilldetails.DataSource = getResult;
                Gv_Ipbilldetails.DataBind();

            }
            else
            {
                txt_name.Text = "";
                txt_totalbill.Text = "";
                txt_deposited.Text = "";
                txt_payable.Text = "";
                ddl_patientcategory.SelectedIndex = 0;
                ddl_admissiondoctor.SelectedIndex = 0;
                txt_admissiontime.Text = "";
                Gv_Ipbilldetails.DataSource = null;
                Gv_Ipbilldetails.DataBind();
            }
        }
        protected void txt_ServiceCharge_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            TextBox Rate = (TextBox)Gv_Ipbilldetails.Rows[index].Cells[0].FindControl("lbl_rate");
            TextBox Qty = (TextBox)Gv_Ipbilldetails.Rows[index].Cells[0].FindControl("lbl_quantity");
            TextBox NetCharge = (TextBox)Gv_Ipbilldetails.Rows[index].Cells[0].FindControl("lbl_netcharge");
            NetCharge.Text = Commonfunction.Getrounding((Convert.ToDecimal(Qty.Text == "" || Qty.Text == "0" ? "1" : Qty.Text) * Convert.ToDecimal(Rate.Text == "" || Rate.Text == "0" ? "1" : Rate.Text)).ToString());
        }
        protected void txt_qty_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            TextBox Rate = (TextBox)Gv_Ipbilldetails.Rows[index].Cells[0].FindControl("lbl_rate");
            TextBox Qty = (TextBox)Gv_Ipbilldetails.Rows[index].Cells[0].FindControl("lbl_quantity");
            TextBox NetCharge = (TextBox)Gv_Ipbilldetails.Rows[index].Cells[0].FindControl("lbl_netcharge");
            NetCharge.Text = Commonfunction.Getrounding((Convert.ToDecimal(Qty.Text == "" || Qty.Text == "0" ? "1" : Qty.Text) * Convert.ToDecimal(Rate.Text == "" || Rate.Text == "0" ? "1" : Rate.Text)).ToString());
        }
        protected void Gv_Ipbilldetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label isSubHeading = (Label)e.Row.FindControl("lbl_subheading");
                Label ServiceNumber = (Label)e.Row.FindControl("lbl_servicenumber");
                Label servicename = (Label)e.Row.FindControl("lbl_servicename");
                Label ServiceCategory = (Label)e.Row.FindControl("lbl_servicecategory");
                TextBox Rate = (TextBox)e.Row.FindControl("lbl_rate");
                TextBox Netcharge = (TextBox)e.Row.FindControl("lbl_netcharge");
                TextBox servicedate = (TextBox)e.Row.FindControl("lbl_serviceDate");
                TextBox servicedateenddate = (TextBox)e.Row.FindControl("lbl_serviceEndDate");
                TextBox remarks = (TextBox)e.Row.FindControl("txt_remarks");
                Label addedby = (Label)e.Row.FindControl("lbladdedBy");
                LinkButton btn = (LinkButton)e.Row.FindControl("lnkDelete");
                LinkButton print = (LinkButton)e.Row.FindControl("lbl_print");
                TextBox qty = (TextBox)e.Row.FindControl("lbl_quantity");
                if (isSubHeading.Text == "1")
                {
                    e.Row.BackColor = Color.FromName("#33aa99");
                    servicename.ForeColor = Color.FromName("#FFFFFF");
                    Rate.Visible = false;
                    Netcharge.Visible = false;
                    servicedate.Visible = false;
                    servicedateenddate.Visible = false;
                    addedby.Visible = false;
                    btn.Visible = false;
                    qty.Visible = false;
                    remarks.Visible = false;
                    print.Visible = false;
                }
                if (ServiceCategory.Text != "1")
                {
                    servicedateenddate.Visible = false;
                }
                if (servicedate.Text == "01/01/0001 12:00:00 AM" || servicedate.Text == "01/01/01 00:00:00" || servicedate.Text == "1/1/0001 12:00:00 AM")
                {
                    servicedate.Text = "";
                }
                if (servicedateenddate.Text == "01/01/0001 12:00:00 AM" || servicedateenddate.Text == "01/01/01 00:00:00" || servicedateenddate.Text == "1/1/0001 12:00:00 AM")
                {
                    servicedateenddate.Text = "";
                }
                if (ServiceCategory.Text == "7")
                {
                    btn.Visible = false;
                    servicedate.Visible = false;
                    servicedateenddate.Visible = false;
                    addedby.Visible = false;
                    remarks.Visible = false;
                    Rate.ReadOnly = true;
                    Netcharge.ReadOnly = true;
                    qty.ReadOnly = true;
                }
                if (ServiceCategory.Text == "1" && servicedateenddate.Text == "")
                {
                    btn.Visible = false;
                }
            }
        }
        protected void ddl_servicecategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPatientBillDetails();
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            ddl_patientcategory.SelectedIndex = 0;
            ddl_servicecategory.SelectedIndex = 0;
            txt_patientNumber.Text = "";
            txt_admissiontime.Text = "";
            txt_name.Text = "";
            Gv_Ipbilldetails.DataSource = null;
            Gv_Ipbilldetails.Visible = false;
            txt_totalbill.Text = "";
            txt_payable.Text = "";
            ddl_admissiondoctor.SelectedIndex = 0;
            txt_deposited.Text = "";
            lblmessage.Text = "";
            ddl_billcategory.SelectedIndex = 0;
        }
        protected void btnsave_Click(object sender, EventArgs e)
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
            if (txt_patientNumber.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Please enter Patient Number.", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            BillAdjustmentData objData = new BillAdjustmentData();
            FInalBillBO objBO = new FInalBillBO();
            List<BillAdjustmentData> Listbill = new List<BillAdjustmentData>();
            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in Gv_Ipbilldetails.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);

                    Label ID = (Label)Gv_Ipbilldetails.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label ServiceID = (Label)Gv_Ipbilldetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_serviceID");

                    Label HeaderType = (Label)Gv_Ipbilldetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_subheading");
                    TextBox amount = (TextBox)Gv_Ipbilldetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_rate");
                    TextBox qty = (TextBox)Gv_Ipbilldetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_quantity");
                    TextBox NetCharge = (TextBox)Gv_Ipbilldetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_netcharge");

                    TextBox Remarks = (TextBox)Gv_Ipbilldetails.Rows[row.RowIndex].Cells[0].FindControl("txt_remarks");
                    TextBox ServiceStart = (TextBox)Gv_Ipbilldetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_serviceDate");
                    TextBox ServiceEnd = (TextBox)Gv_Ipbilldetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_serviceEndDate");
                    Label ServiceCategory = (Label)Gv_Ipbilldetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_servicecategory");

                    BillAdjustmentData ObjDetails = new BillAdjustmentData();
                    if (HeaderType.Text == "0" && ServiceCategory.Text != "7")
                    {
                        ObjDetails.ID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                        ObjDetails.ServiceID = Convert.ToInt64(ServiceID.Text == "" ? "0" : ServiceID.Text);
                        ObjDetails.Quantity = Convert.ToInt32(qty.Text == "0" ? "1" : qty.Text);
                        ObjDetails.ServiceCharge = Convert.ToDecimal(amount.Text == "" ? "0" : amount.Text);
                        ObjDetails.NetServiceCharge = Convert.ToDecimal(amount.Text == "" ? "0" : amount.Text) * Convert.ToInt32(qty.Text == "0" ? "1" : qty.Text);
                        ObjDetails.Remarks = Remarks.Text == "" ? "null" : Remarks.Text;
                        ObjDetails.ServiceStartDate = ServiceStart.Text == "" ? "" : ServiceStart.Text;
                        ObjDetails.ServiceEndDate = ServiceEnd.Text == "" ? "" : ServiceEnd.Text;
                        Listbill.Add(ObjDetails);
                    }
                }
                objData.XMLData = XmlConvertor.ServiceRecorddDatatoXML(Listbill).ToString();
                objData.BillCategory = Convert.ToInt32(ddl_billcategory.SelectedValue == "" ? "0" : ddl_billcategory.SelectedValue);
                objData.PatientNumber = txt_patientNumber.Text == "" ? "" : txt_patientNumber.Text;
                objData.EmployeeID = LogData.EmployeeID;

                int result = objBO.UpdateServiceRecord(objData);
                if (result == 1)
                {
                    GetPatientBillDetails();
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    div1.Attributes["class"] = "SucessAlert";
                    div1.Visible = true;
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
        protected void gvipservicerecord_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    BillAdjustmentData objData = new BillAdjustmentData();
                    FInalBillBO objBO = new FInalBillBO();

                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = Gv_Ipbilldetails.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    Label serviceid = (Label)gr.Cells[0].FindControl("lbl_serviceID");
                    Label servicenumber = (Label)gr.Cells[0].FindControl("lbl_servicenumber");
                    Label invnumber = (Label)gr.Cells[0].FindControl("lblinvnumber");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txt_remarks");

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
                        objData.Remarks = txtremarks.Text;
                    }
                    objData.ID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                    objData.ServiceID = Convert.ToInt32(serviceid.Text == "" ? "0" : serviceid.Text);
                    objData.PatientNumber = txt_patientNumber.Text == "" ? "" : txt_patientNumber.Text;
                    objData.InvNumber = invnumber.Text == "" ? "" : invnumber.Text;
                    objData.ServiceNumber = servicenumber.Text == "" ? "" : servicenumber.Text;
                    objData.BillCategory = Convert.ToInt32(ddl_billcategory.SelectedValue == "" ? "0" : ddl_billcategory.SelectedValue);
                    objData.EmployeeID = LogData.EmployeeID;
                    objData.HospitalID = LogData.HospitalID;
                    int Result = objBO.DeleteServiceRecordbyID(objData);
                    if (Result == 1)
                    {
                        GetPatientBillDetails();
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
                if (e.CommandName == "Print")
                {
                    int j = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gp = Gv_Ipbilldetails.Rows[j];
                    Label GroupNumber = (Label)gp.Cells[0].FindControl("lbl_groupnumber");

                    string url = "../MedIPD/Reports/ReportViewer.aspx?option=IPservicerecpt&Grpnumber=" + GroupNumber.Text + "&Actiontype=" + 2;
                    string fullURL = "window.open('" + url + "', '_blank');";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);

                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void btnprint_Click(object sender, EventArgs e)
        {
            if (ddl_billcategory.SelectedValue == "1")
            {
                string url = "../MedEmergency/Reports/ReportViewer.aspx?option=EmrgInterimBill&Emrgno=" + txt_patientNumber.Text.ToString();
                string fullURL = "window.open('" + url + "', '_blank');";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
            }
            if (ddl_billcategory.SelectedValue == "2")
            {
                string url = "../MedBills/Reports/ReportViewer.aspx?option=InterimBill&IPno=" + txt_patientNumber.Text.ToString();
                string fullURL = "window.open('" + url + "', '_blank');";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
            }
        }
    }
}