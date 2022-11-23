using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Drawing;
using Mediqura.CommonData.AdmissionData;
using Mediqura.BOL.AdmissionBO;
using Mediqura.CommonData.MedEmergencyData;
using Mediqura.BOL.MedEmergencyBO;
using Mediqura.Utility;

namespace Mediqura.Web.MedIPD
{
    public partial class IPCreditLimits : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPNo(string prefixText, int count, string contextKey)
        {
            IPData Objpaic = new IPData();
            AdmissionBO objInfoBO = new AdmissionBO();
            List<IPData> getResult = new List<IPData>();
            Objpaic.IPNo = prefixText;
            getResult = objInfoBO.getIPNoWithNameAgeNAddress(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        protected void chekboxselect_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GvCredit.Rows)
            {
                CheckBox cb = (CheckBox)GvCredit.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                if (cb.Checked)
                {
                    TextBox txt = (TextBox)GvCredit.Rows[row.RowIndex].Cells[0].FindControl("txtcredit");
                    txt.ReadOnly = false;
                    txt.Focus();
                }
                else if (cb.Checked == false)
                {
                    TextBox txt = (TextBox)GvCredit.Rows[row.RowIndex].Cells[0].FindControl("txtcredit");
                    txt.ReadOnly = true;
                }
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
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
            bindgrid();
        }
        protected void bindgrid()
        {
            AdmissionData objpat = new AdmissionData();
            AdmissionBO objbillingBO = new AdmissionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.AdmissionNo = txt_IPNo.Text == "" ? " " : txt_IPNo.Text.Substring(txt_IPNo.Text.LastIndexOf(':') + 1);
            objpat.PatientName = "0";
            objpat.DateFrom = from;
            objpat.DateTo = To;
            objpat.DischargeStatus = 1;
            objpat.IsActive = true;
            List<AdmissionData> result = objbillingBO.GetAdmissionList(objpat);
            if (result.Count > 0)
            {
                Messagealert_.ShowMessage(lblresult, "Total:" + result[0].MaximumRows.ToString() + " record(s) found.", 1);
                div4.Attributes["class"] = "SucessAlert";
                div4.Visible = true;
                GvCredit.Visible = true;
                GvCredit.DataSource = result;
                GvCredit.DataBind();
            }
            else
            {
                GvCredit.DataSource = null;
                GvCredit.DataBind();
            }
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
            List<IPCreditMasterData> Listdata = new List<IPCreditMasterData>();
            IPCreditMasterData objdata = new IPCreditMasterData();
            IPCreditMasterBO objBO = new IPCreditMasterBO();

            try
            {   // get all the record from the gridview

                foreach (GridViewRow row in GvCredit.Rows)
                {
                    int count = 0;
                    CheckBox cb = (CheckBox)GvCredit.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                    if (cb != null)
                    {
                        if (cb.Checked)
                        {
                            IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                            Label IPNo = (Label)GvCredit.Rows[row.RowIndex].Cells[0].FindControl("lblipno");
                            Label PatientName = (Label)GvCredit.Rows[row.RowIndex].Cells[0].FindControl("lblname");
                            Label PatientAddress = (Label)GvCredit.Rows[row.RowIndex].Cells[0].FindControl("lbladdress");
                            Label DepositAmount = (Label)GvCredit.Rows[row.RowIndex].Cells[0].FindControl("lbldep");
                            Label TotalOutstandingBill = (Label)GvCredit.Rows[row.RowIndex].Cells[0].FindControl("lblbill");
                            TextBox TxtCredit = (TextBox)GvCredit.Rows[row.RowIndex].Cells[0].FindControl("txtcredit");
                            IPCreditMasterData ObjDetails = new IPCreditMasterData();

                            ObjDetails.IPNo = IPNo.Text == "" ? null : IPNo.Text;
                            ObjDetails.PatientName = PatientName.Text == "" ? null : PatientName.Text;
                            ObjDetails.PatientAddress = PatientAddress.Text == "" ? null : PatientAddress.Text;
                            ObjDetails.DepositAmount = Convert.ToDecimal(DepositAmount.Text == "" ? "0.0" : DepositAmount.Text);
                            ObjDetails.TotalOutstandingBill = Convert.ToDecimal(TotalOutstandingBill.Text == "" ? "0.0" : TotalOutstandingBill.Text);
                            if (TxtCredit.Text == "00")
                            {
                                Messagealert_.ShowMessage(lblmessage, "Please enter credit limit.", 0);
                                div1.Visible = true;
                                div1.Attributes["class"] = "FailAlert";
                                return;
                            }
                            else
                            {
                                ObjDetails.CreditLimit = Convert.ToDecimal(TxtCredit.Text == "" ? "0" : TxtCredit.Text);

                            }

                            Listdata.Add(ObjDetails);
                            count++;
                            if (count == 0)
                            {
                                Messagealert_.ShowMessage(lblmessage, "Please select patient.", 0);
                                div1.Visible = true;
                                div1.Attributes["class"] = "FailAlert";
                                return;
                            }
                        }
                    }
                }
                objdata.XMLData = XmlConvertor.IPCreditDatatoXML(Listdata).ToString();
                objdata.FinancialYearID = LogData.FinancialYearID;
                objdata.EmployeeID = LogData.EmployeeID;
                objdata.HospitalID = LogData.HospitalID;
                objdata.IPaddress = LogData.IPaddress;
                objdata.ActionType = Enumaction.Insert;

                int result = objBO.UpdateIPCreditLimitDetails(objdata);
                if (result == 1)
                {
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    ViewState["ID"] = null;
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                }
            }

            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);

            }
        }
        protected void GvCredit_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Int64 ID = Convert.ToInt32(GvCredit.DataKeys[e.RowIndex].Values["AdmissionID"].ToString());
            System.Web.UI.WebControls.Label Ipnumber = (System.Web.UI.WebControls.Label)GvCredit.Rows[e.RowIndex].FindControl("lblipno");
            System.Web.UI.WebControls.TextBox credit = (System.Web.UI.WebControls.TextBox)GvCredit.Rows[e.RowIndex].FindControl("txtcredit");
            AdmissionData objpat = new AdmissionData();
            AdmissionBO objBO = new AdmissionBO();
            objpat.ID = ID;
            objpat.IPNo = Ipnumber.Text.Trim();
            objpat.CreditLimit = Convert.ToDecimal(credit.Text == "" ? "0" : credit.Text);
            objpat.EmployeeID = LogData.EmployeeID;
            objpat.HospitalID = LogData.HospitalID;
            objpat.IPaddress = LogData.IPaddress;
            int Result = objBO.Updatecreditlimit(objpat);
            if (Result == 1)
            {
                bindgrid();
                Messagealert_.ShowMessage(lblmessage, "save", 1);
                div1.Attributes["class"] = "SucessAlert";
                div1.Visible = true;
            }
            else
            {
                lblmessage.Visible = false;
            }
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txt_IPNo.Text = "";
            txtdatefrom.Text = "";
            txtdateto.Text = "";
            GvCredit.DataSource = null;
            GvCredit.DataBind();
            GvCredit.Visible = false;
            lblresult.Visible = false;
            div4.Visible = false;
            lblmessage.Visible = false;
            div1.Visible = false;

        }

    }
}