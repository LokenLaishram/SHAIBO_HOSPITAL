using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Utility;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Drawing;
using ClosedXML.Excel;
using Mediqura.CommonData.PatientData;
using Mediqura.BOL.PatientBO;

namespace Mediqura.Web.MedLab
{
    public partial class LabTestComment : BasePage
    {
        public static Int64 labtestID = 0;
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
            Commonfunction.PopulateDdl(ddl_cverifiedby, mstlookup.GetLookupsList(LookupName.PathologyDoctor));
            Commonfunction.PopulateDdl(ddl_referal, mstlookup.GetLookupsList(LookupName.Labconsultant));
            Commonfunction.PopulateDdl(ddl_patienttype, mstlookup.GetLookupsList(LookupName.PatientCatagory));
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetUHID(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.UHID = Convert.ToInt64(prefixText);
            Objpaic.PatientType = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetLabDevicecompletedUHID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].UHID.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetInv(string prefixText, int count, string contextKey)
        {
            SampleCollectionData Objpaic = new SampleCollectionData();
            LabSampleCollctionBO objInfoBO = new LabSampleCollctionBO();
            List<SampleCollectionData> getResult = new List<SampleCollectionData>();
            Objpaic.Investigationumber = prefixText;
            Objpaic.PatientTypeID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetDevicecompletedInvestigationno(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].Investigationumber.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPNo(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.IPNo = prefixText;
            Objpaic.PatientType = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetLabDevicecompletedIPNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetLabPatientName(string prefixText, int count, string contextKey)
        {
            SampleCollectionData Objpaic = new SampleCollectionData();
            LabSampleCollctionBO objInfoBO = new LabSampleCollctionBO();
            List<SampleCollectionData> getResult = new List<SampleCollectionData>();
            Objpaic.PatientName = prefixText;
            Objpaic.PatientTypeID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetDeviceCompletedPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetTestNames(string prefixText, int count, string contextKey)
        {
            SampleCollectionData Objpaic = new SampleCollectionData();
            LabSampleCollctionBO objInfoBO = new LabSampleCollctionBO();
            List<SampleCollectionData> getResult = new List<SampleCollectionData>();
            Objpaic.TestName = prefixText;
            Objpaic.PatientTypeID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetDeviceCompletedTestNames(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].TestName.ToString());
            }
            return list;
        }
        protected void ddl_patient_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender1.ContextKey = ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue;
            AutoCompleteExtender2.ContextKey = ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue;
            AutoCompleteExtender3.ContextKey = ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue;
            AutoCompleteExtender4.ContextKey = ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue;
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lbl_result, "SearchEnable", 0);
                div6.Visible = true;
                div6.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lbl_result.Visible = false;
            }
            if (txtdate_from.Text != "")
            {
                if (Commonfunction.isValidDate(txtdate_from.Text) == false)
                {
                    Messagealert_.ShowMessage(lbl_result, "ValidDatefrom", 0);
                    div6.Visible = true;
                    div6.Attributes["class"] = "FailAlert";
                    txtdate_from.Focus();
                    return;
                }
            }
            else
            {
                lbl_result.Visible = false;
                div6.Visible = false;
            }
            if (txtdate_to.Text != "")
            {
                if (Commonfunction.isValidDate(txtdate_to.Text) == false)
                {
                    Messagealert_.ShowMessage(lbl_result, "ValidDateto", 0);
                    div6.Visible = true;
                    div6.Attributes["class"] = "FailAlert";
                    txtdate_to.Focus();
                    return;
                }
            }
            else
            {
                lbl_result.Visible = false;
                div6.Visible = false;
            }
            bindgrid();
        }
        private void bindgrid()
        {
            try
            {
                List<SampleCollectionData> lstemp = GetLatestList(0);
                if (lstemp.Count > 0)
                {
                    gv_labtestlist.DataSource = lstemp;
                    gv_labtestlist.DataBind();
                    gv_labtestlist.Visible = true;
                    Messagealert_.ShowMessage(lbl_result, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    div6.Visible = true;
                    div6.Attributes["class"] = "SucessAlert";
                }
                else
                {
                    gv_labtestlist.DataSource = null;
                    gv_labtestlist.DataBind();
                    gv_labtestlist.Visible = true;
                    lbl_result.Visible = false;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lbl_result, "system", 0);
            }
        }
        private List<SampleCollectionData> GetLatestList(int p)
        {
            SampleCollectionData objsample = new SampleCollectionData();
            LabSampleCollctionBO objlabBO = new LabSampleCollctionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objsample.Investigationumber = txt_invnumber.Text.Trim() == "" ? null : txt_invnumber.Text.Trim();
            objsample.PatientTypeID = Convert.ToInt32(ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue);

            objsample.PatientName = txt_patientnames.Text.Trim() == "" ? null : txt_patientnames.Text.Trim();
            DateTime from = txtdate_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdate_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtdate_to.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdate_to.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objsample.DateFrom = from;
            objsample.DateTo = To;
            bool isnumeric = txt_patientnames.Text.All(char.IsDigit);
            if (isnumeric == false)
            {
                if (txt_patientnames.Text.Contains(":"))
                {
                    bool isUHIDnumeric = txt_patientnames.Text.Substring(txt_patientnames.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                    objsample.UHID = isUHIDnumeric ? Convert.ToInt64(txt_patientnames.Text.Contains(":") ? txt_patientnames.Text.Substring(txt_patientnames.Text.LastIndexOf(':') + 1) : "0") : 0;
                }
                else
                {
                    txt_patientnames.Text = "";
                    txt_patientnames.Focus();
                }
            }
            else
            {
                objsample.UHID = Convert.ToInt64(txt_patientnames.Text == "" ? "0" : txt_patientnames.Text);
            }
            objsample.IPNo = txt_ipnumber.Text.Trim() == "" ? null : txt_ipnumber.Text.Trim();
            objsample.LabServiceID = Convert.ToInt32(txt_testnames.Text.Contains(":") ? txt_testnames.Text.Substring(txt_testnames.Text.LastIndexOf(':') + 1) : "0");
            objsample.ConsultantID = Convert.ToInt64(ddl_referal.SelectedValue == "" ? "0" : ddl_referal.SelectedValue);
            objsample.StatusID = Convert.ToInt32(ddl_status.SelectedValue == "" ? "0" : ddl_status.SelectedValue);
            return objlabBO.GetCommentResultentrytestlist(objsample);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            hdntestID.Value = "";
            hdnUHID.Value = "";
            hdnInvNumber.Value = "";
            ddl_cverifiedby.SelectedIndex = 0;
            tabcontainerTestComment.ActiveTabIndex = 0;
        }
        protected void gv_labtestlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Search")
                {
                    LabResultData objresult = new LabResultData();
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gv_labtestlist.Rows[i];
                    Label UHID = (Label)gr.Cells[0].FindControl("lbl_UHID");
                    Label InvNumber = (Label)gr.Cells[0].FindControl("lvl_inv");
                    Label TestID = (Label)gr.Cells[0].FindControl("lblTestID");
                    Label ReportType = (Label)gr.Cells[0].FindControl("lbl_reportType");

                    LabSampleCollctionBO objlabBO = new LabSampleCollctionBO();
                    objresult.Investigationumber = InvNumber.Text.Trim() == "" ? null : InvNumber.Text.Trim();
                    objresult.LabServiceID = Convert.ToInt32(TestID.Text.Trim() == "" ? "0" : TestID.Text.Trim());
                    objresult.UHID = Convert.ToInt32(UHID.Text.Trim() == "" ? "0" : UHID.Text.Trim());
                    hdntestID.Value = TestID.Text.Trim() == "" ? "0" : TestID.Text.Trim();
                    hdnUHID.Value = UHID.Text.Trim() == "" ? "0" : UHID.Text.Trim();
                    hdnInvNumber.Value = InvNumber.Text.Trim() == "" ? null : InvNumber.Text.Trim();
                    List<LabResultData> Result = objlabBO.GetCommentedLabResults(objresult);
                    if (Result.Count > 0)
                    {
                        tabcontainerTestComment.ActiveTabIndex = 1;
                        txt_template.InnerHtml = generateTemplate(Result[0].ResultTemplate.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&"), Result[0]);
                        if (Result[0].isVerified == 1)
                        {
                            Cbtn_print.Visible = true;
                            labtestID = Result[0].ID;
                        }
                        else
                        {
                            Cbtn_print.Visible = false;
                        }



                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lbl_result, "system", 0);
                div6.Attributes["class"] = "FailAlert";
                div6.Visible = true;
                return;
            }
        }
        public string generateTemplate(string template, LabResultData objdata)
        {
            DateTime today = System.DateTime.Now;

            string header = "<table style=\"height:47px;width:751px;margin-left:auto;margin-right:auto;\"><tbody><tr style=\"height:19px;\" >" +
                            "<td style=\"width:110px;height:19px;\"><span style=\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\"><strong> UHID:</strong></span></td>" +
                            "<td style=\"width:270px;height:19px;\"><span style=\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\">" + objdata.UHID + "</span></td>" +
                            "<td style=\"width:133px;height:19px;\"><span style=\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\"><strong>Refered By:</strong></span></td>" +
                            "<td style=\"width:226px;height:19px;\"><span style=\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\">" + objdata.ReferalDoctor + "</span></td>" +
                            "</tr>" +
                            "<tr style=\"height:18px;\">" +
                            "<td style=\"width: 110px; height: 18px;\"><span style=\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\"><strong>Pat.Name:</strong></span></td>" +
                            "<td style=\"width: 270px; height: 18px;\"><span style=\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\">" + objdata.PatientName + "</span></td>" +
                            "<td style=\"width: 359px; height: 18px;\" colspan=\"2\"><strong><span style=\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\">" + objdata.PatientNumber + "</span></strong></td>" +
                            "</tr>" +
                            "<tr style=\"height: 18px;\">" +
                            "<td style=\"width: 110px; height: 18px;\"><span style=\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\"><strong>Age:</strong></span></td>" +
                            "<td style=\"width: 270px; height: 18px;\"><span style=\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\">" + objdata.AgeCount + "</span></td>" +
                            "<td style=\"width: 133px; height: 18px;\"><span style=\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\"><strong > Sex:</strong></span></td>" +
                            "<td style=\"width: 226px; height: 18px;\"><span style=\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\">" + objdata.GenderName + "</span></td>" +
                            "</tr>" +
                            "<tr style=\"height:18px;\">" +
                            "<td style=\"width: 110px; height: 18px;\"><span style=\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\"><strong>Pat.Address:</strong></span></td>" +
                            "<td style=\"width: 270px; height: 18px;\"><span style=\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\">" + objdata.PatientAddress
                            + "</span></td>" +
                            "<td style=\"width: 133px; height: 18px;\"><span style=\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\"><strong>Sample Recd Dt:</strong></span></td>" +
                            "<td style=\"width: 226px; height: 18px;\"><span style=\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\">" + objdata.InvRequestedOn + "</span></td>" +
                            "</tr>" +
                            "<tr style=\"height: 18px;\">" +
                            "<td style=\"width: 110px; height: 18px;\"><span style=\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\"><strong> Contact No.:</strong></span></td>" +
                            "<td style=\"width: 270px; height: 18px;\"><span style=\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\">" + objdata.PatContact + "</span></td>" +
                            "<td style=\"width: 133px; height: 18px;\"><span style=\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\"><strong>INV No:</strong></span></td>" +
                            "<td style=\"width: 226px; height: 18px;\"><span style=\"color: #000000; font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;\">" + objdata.Investigationumber + "</span></td>" +
                            "</tr>" +
                            "</tbody>" +
                            "</table>";

            string code = Commonfunction.getBarcode(objdata.UHID.ToString());
            string barcode = "<img style=\"height:35px;\" src=\"" + code + "\"/>";
            string Result = template.Replace("[header]", header);
            Result = Result.Replace("[barcode]", barcode);
            return Result;
        }
        protected void gv_labtestlist_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (LogData.UpdateEnable == 0)
            {
                Messagealert_.ShowMessage(lbl_result, "UpdateEnable", 0);
                div6.Visible = true;
                div6.Attributes["class"] = "FailAlert";
            }
            else
            {
                lbl_result.Visible = false;
            }
            Int64 ID = Convert.ToInt32(gv_labtestlist.DataKeys[e.RowIndex].Values["ID"].ToString());
            System.Web.UI.WebControls.Label invnumber = (System.Web.UI.WebControls.Label)gv_labtestlist.Rows[e.RowIndex].FindControl("lvl_inv");
            System.Web.UI.WebControls.Label TestID = (System.Web.UI.WebControls.Label)gv_labtestlist.Rows[e.RowIndex].FindControl("lblTestID");

            SampleCollectionData objdevice = new SampleCollectionData();
            LabSampleCollctionBO objInfoBO = new LabSampleCollctionBO();
            objdevice.ID = ID;
            objdevice.LabServiceID = Convert.ToInt32(TestID.Text == "" ? "0" : TestID.Text);
            objdevice.Investigationumber = invnumber.Text.Trim();
            objdevice.EmployeeID = LogData.EmployeeID;
            int result = objInfoBO.UpdateMLTrecievingtime(objdevice);
            if (result > 0)
            {
                gv_labtestlist.DataSource = null;
                gv_labtestlist.DataBind();
                bindgrid();
                Messagealert_.ShowMessage(lbl_result, "update", 1);
                div6.Visible = true;
                div6.Attributes["class"] = "SucessAlert";
            }
        }
        protected void gv_labtestlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label urgency = e.Row.FindControl("lbl_urgencyid") as Label;
                    Label Status = e.Row.FindControl("lbl_devicestatus") as Label;


                    if (urgency.Text == "0" || urgency.Text == "1")
                    {
                        e.Row.Cells[1].BackColor = System.Drawing.Color.Green;
                    }
                    if (urgency.Text == "2")
                    {
                        e.Row.Cells[1].BackColor = System.Drawing.Color.Yellow;
                    }
                    if (urgency.Text == "3")
                    {
                        e.Row.Cells[1].BackColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (LogData.SaveEnable == 0)
                {
                    Messagealert_.ShowMessage(lbl_message1, "SaveEnable", 0);
                    div4.Visible = true;
                    div4.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lbl_message1.Visible = false;
                }

                List<LabResultData> Listresult = new List<LabResultData>();
                LabResultData objlabresult = new LabResultData();
                LabSampleCollctionBO objresultbo = new LabSampleCollctionBO();

                objlabresult.ResultTemplate = txt_template.InnerHtml.ToString();
                objlabresult.UHID = Convert.ToInt64(hdnUHID.Value == "" ? "0" : hdnUHID.Value);
                objlabresult.Investigationumber = hdnInvNumber.Value == "" ? "0" : hdnInvNumber.Value;
                objlabresult.LabServiceID = Convert.ToInt32(hdntestID.Value == "" ? "0" : hdntestID.Value);
                objlabresult.EmployeeID = LogData.EmployeeID;
                objlabresult.FinancialYearID = LogData.FinancialYearID;
                objlabresult.EmployeeID = LogData.EmployeeID;
                objlabresult.VerifiedBy = Convert.ToInt64(ddl_cverifiedby.SelectedValue == "" ? "0" : ddl_cverifiedby.SelectedValue);
                objlabresult.HospitalID = LogData.HospitalID;
                objlabresult.IPaddress = LogData.IPaddress;
                int resultstatus = objresultbo.UpdateCommentedLabResult(objlabresult);
                if (resultstatus > 1)
                {
                    labtestID = resultstatus;
                    Cbtn_print.Visible = true;
                    Messagealert_.ShowMessage(lbl_message1, resultstatus == 1 ? "save" : "update", 1);
                    div4.Visible = true;
                    div4.Attributes["class"] = "SucessAlert";
                }
                else
                {
                    Cbtn_print.Visible = false;
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lbl_message1.Text = ExceptionMessage.GetMessage(ex);
            }
        }

        protected void Cbtn_print_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MedLab/LabCommentPrint.aspx?id=" + labtestID + "&p=0", false);
        }
    }
}