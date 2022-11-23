using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.PatientData;
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
using Mediqura.BOL.MedUtilityBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.AdmissionData;
using Mediqura.BOL.AdmissionBO;
using System.Drawing;
using Mediqura.CommonData.LoginData;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlClient;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Mediqura.Web.MedLab
{
    public partial class LabResultEntry : BasePage
    {
        IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
        ReportDocument crystalReport = new ReportDocument();
        ReportDocument crSubreportDocument = new ReportDocument();
        Sections crSections;
        SubreportObject crSubreportObject;
        ReportObjects crReportObjects;
        ConnectionInfo crConnectionInfo;
        Database crDatabase;
        Tables crTables;
        TableLogOnInfo crTableLogOnInfo;
        string constr = ConfigurationManager.ConnectionStrings["SqlConnectionString11"].ConnectionString;
        string ReportUserId = ConfigurationManager.AppSettings["ReportUserId"];
        string ReportServerName = ConfigurationManager.AppSettings["ReportServerName"];
        string ReportDatabase = ConfigurationManager.AppSettings["ReportDatabase"];
        string ReportPassword = ConfigurationManager.AppSettings["ReportPassword"];

        protected void Page_Unload(Object sender, EventArgs evntArgs)
        {
            crystalReport.Close();
            crystalReport.Dispose();
            crystalReport = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        protected void bindsubroot()
        {
            crDatabase = crystalReport.Database;
            crTables = crDatabase.Tables;
            crConnectionInfo = new ConnectionInfo();
            crConnectionInfo.ServerName = ReportServerName;
            crConnectionInfo.DatabaseName = ReportDatabase;
            crConnectionInfo.UserID = ReportUserId;
            crConnectionInfo.Password = ReportPassword;
            foreach (CrystalDecisions.CrystalReports.Engine.Table aTable in crTables)
            {
                crTableLogOnInfo = aTable.LogOnInfo;
                crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                aTable.ApplyLogOnInfo(crTableLogOnInfo);
            }
            // THIS STUFF HERE IS FOR REPORTS HAVING SUBREPORTS 
            // set the sections object to the current report's section 
            crSections = crystalReport.ReportDefinition.Sections;
            // loop through all the sections to find all the report objects 
            foreach (CrystalDecisions.CrystalReports.Engine.Section crSection in crSections)
            {
                crReportObjects = crSection.ReportObjects;
                //loop through all the report objects in there to find all subreports 
                foreach (ReportObject crReportObject in crReportObjects)
                {
                    if (crReportObject.Kind == ReportObjectKind.SubreportObject)
                    {
                        crSubreportObject = (SubreportObject)crReportObject;
                        //open the subreport object and logon as for the general report 
                        crSubreportDocument = crSubreportObject.OpenSubreport(crSubreportObject.SubreportName);
                        crDatabase = crSubreportDocument.Database;
                        crTables = crDatabase.Tables;
                        foreach (CrystalDecisions.CrystalReports.Engine.Table aTable in crTables)
                        {
                            crTableLogOnInfo = aTable.LogOnInfo;
                            crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                            aTable.ApplyLogOnInfo(crTableLogOnInfo);
                        }
                    }
                }
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
                ddlbind();
                bindgrid(1);
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_verifiedby, mstlookup.GetLookupsList(LookupName.PathologyDoctor));
            ddl_verifiedby.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddl_template, mstlookup.GetLookupsList(LookupName.LabTemplate));
            Commonfunction.PopulateDdl(ddl_machinename, mstlookup.GetLookupsList(LookupName.MachineName));
            Commonfunction.PopulateDdl(ddl_referal, mstlookup.GetLookupsList(LookupName.Labconsultant));
            Commonfunction.PopulateDdl(ddl_patienttype, mstlookup.GetLookupsList(LookupName.PatientCatagory));
            ///---CULTURE REPORT----//
            //Commonfunction.PopulateDdl(ddl_verified3, mstlookup.GetLookupsList(LookupName.Microbiologist));
            Commonfunction.PopulateDdl(ddl_verified3, mstlookup.GetLookupsList(LookupName.PathologyDoctor));
            Commonfunction.PopulateDdl(ddl_template3, mstlookup.GetLookupsList(LookupName.CulLabTemplate));
            Commonfunction.PopulateDdl(ddl_centre, mstlookup.GetLookupsList(LookupName.RunnerList));
            Commonfunction.PopulateDdl(ddlTestMethod, mstlookup.GetLookupsList(LookupName.Method));

            txtdate_from.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtdate_to.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            if (LogData.RoleID == 1)
            {
                Linknewload.Visible = true;
            }
            else
            {
                Linknewload.Visible = false;
            }
            Session["currenrrow"] = "0";
            Session["SensitivityList"] = null;
            List<LookupItem> unitlist = Session["SensitivityList"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["SensitivityList"];
            Session["SensitivityList"] = mstlookup.GetLookupsList(LookupName.AntiSense);

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
        protected void bindgridfoucs()
        {
            for (int i = 0; i < Gv_LabResult.Rows.Count - 1; i++)
            {
                TextBox curTexbox = Gv_LabResult.Rows[i].Cells[2].FindControl("txtresult") as TextBox;
                TextBox nexTextbox = Gv_LabResult.Rows[i + 1].Cells[2].FindControl("txtresult") as TextBox;
                curTexbox.Attributes.Add("onkeypress", "return clickEnter('" + nexTextbox.ClientID + "', event)");
                int lastindex = Gv_LabResult.Rows.Count - 1;
                //if (i + 2 > lastindex)
                //{
                //    nexTextbox.Attributes.Add("onkeypress", "return clickEnter('" + btnsave.ClientID + "', event)");
                //}
            }
        }
        protected void ddl_patient_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender1.ContextKey = ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue;
            AutoCompleteExtender3.ContextKey = ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue;
            AutoCompleteExtender4.ContextKey = ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue;
        }
        protected void PatientName_OnTextChanged(object sender, EventArgs e)
        {
            GVreset();
            bindgrid(1);
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GVreset();
            bindgrid(1);
        }
        private void bindgrid(int page)
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
                if (txtdate_from.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdate_from.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDatefrom", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txtdate_from.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (txtdate_to.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdate_to.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDateto", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txtdate_to.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                GV_PatientList.PageSize = Convert.ToInt32(ddl_show.SelectedValue == "10000" ? lbl_totalrecords.Text : ddl_show.SelectedValue);
                List<SampleCollectionData> lstemp = GetLabTestPatientList(page);
                if (lstemp.Count > 0)
                {
                    GV_PatientList.VirtualItemCount = lstemp[0].MaximumRows;//total item is required for custom paging
                    GV_PatientList.PageIndex = page - 1;
                    GV_PatientList.DataSource = lstemp;
                    GV_PatientList.DataBind();
                    GV_PatientList.Visible = true;
                    Messagealert_.ShowMessage(lbl_result, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    lbl_totalrecords.Text = lstemp[0].MaximumRows.ToString();
                    div6.Visible = true;
                    div6.Attributes["class"] = "SucessAlert";
                }
                else
                {
                    GV_PatientList.DataSource = null;
                    GV_PatientList.DataBind();
                    GV_PatientList.Visible = true;
                    lbl_result.Visible = false;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        private List<SampleCollectionData> GetLabTestPatientList(int p)
        {
            SampleCollectionData objsample = new SampleCollectionData();
            LabSampleCollctionBO objlabBO = new LabSampleCollctionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objsample.Investigationumber = txt_invnumber.Text.Trim() == "" ? null : txt_invnumber.Text.Trim();
            objsample.PatientTypeID = Convert.ToInt32(ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue);
            objsample.PatientName = txt_patientnames.Text.Trim() == "" ? null : txt_patientnames.Text.Trim();
            DateTime from = txtdate_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdate_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtdate_to.Text.Trim() == "" ? GlobalConstant.MaxSQLDateTime : DateTime.Parse(txtdate_to.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objsample.DateFrom = from;
            objsample.DateTo = To;
            objsample.UHID = Convert.ToInt64(txt_patientnames.Text.Contains(":") ? txt_patientnames.Text.Substring(txt_patientnames.Text.LastIndexOf(':') + 1) : "0");
            objsample.IPNo = txt_ipnumber.Text.Trim() == "" ? null : txt_ipnumber.Text.Trim();
            objsample.LabServiceID = Convert.ToInt32(txt_testname.Text.Contains(":") ? txt_testname.Text.Substring(txt_testname.Text.LastIndexOf(':') + 1) : "0");
            objsample.ConsultantID = Convert.ToInt64(ddl_referal.SelectedValue == "" ? "0" : ddl_referal.SelectedValue);
            objsample.RunnerID = Convert.ToInt32(ddl_centre.SelectedValue == "" ? "0" : ddl_centre.SelectedValue);
            objsample.StatusID = Convert.ToInt32(ddl_status.SelectedValue == "" ? "0" : ddl_status.SelectedValue);
            objsample.CurrentIndex = p;
            objsample.PageSize = Convert.ToInt32(ddl_show.SelectedValue == "10000" ? lbl_totalrecords.Text : ddl_show.SelectedValue);
            GV_PatientList.PageSize = Convert.ToInt32(ddl_show.SelectedValue == "10000" ? lbl_totalrecords.Text : ddl_show.SelectedValue);

            return objlabBO.GetTestPatientList(objsample);
        }
        protected void GV_PatientList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label urgency = e.Row.FindControl("lbl1_urgencyid") as Label;
                Label Status = e.Row.FindControl("lbl1_devicestatus") as Label;

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
            //if (Convert.ToInt32(Session["currenrrow"].ToString() == null ? "0" : Session["currenrrow"].ToString()) == rowcount)
            //{
            //    Label result1 = (Label)gv_labtestlist.Rows[rowcount].Cells[0].FindControl("lblID");
            //    result1.Focus();
            //}
        }

        //-------LAB TEST LIST----------//
        protected void gv_PatientTestlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "GetTest")
                {

                    SampleCollectionData objresult = new SampleCollectionData();
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GV_PatientList.Rows[i];
                    Label UHID = (Label)gr.Cells[0].FindControl("lbl_PatUHID");
                    Label InvNumber = (Label)gr.Cells[0].FindControl("lvl_LabInv");
                    objresult.Investigationumber = InvNumber.Text.Trim() == "" ? null : InvNumber.Text.Trim();
                    objresult.UHID = Convert.ToInt32(UHID.Text.Trim() == "" ? "0" : UHID.Text.Trim());
                    objresult.RoleID = LogData.RoleID;
                    hdninvnumber.Text = "";
                    hdnuhid.Text = "";
                    GetTestListByInvNo(objresult);
                }
                if (e.CommandName == "ResultEntry")
                {
                    lblmessage.Visible = false;
                    LabResultData objresult = new LabResultData();
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GV_PatientList.Rows[i];
                    Label UHID = (Label)gr.Cells[0].FindControl("lbl1_UHID");
                    Label InvNumber = (Label)gr.Cells[0].FindControl("lvl_LabInv");
                    Label TestID = (Label)gr.Cells[0].FindControl("lbl1TestID");
                    Label mtrecivedstatus = (Label)gr.Cells[0].FindControl("lbl1_reciestatus");
                    Label SubgroupID = (Label)gr.Cells[0].FindControl("lbl1_subgrpID");
               
                    rbtn_normal.Checked = true;
                    rbtn_exceptional.Checked = false;
                    objresult.Investigationumber = InvNumber.Text.Trim() == "" ? null : InvNumber.Text.Trim();
                    objresult.LabServiceID = Convert.ToInt32(TestID.Text.Trim() == "" ? "0" : TestID.Text.Trim());
                    objresult.UHID = Convert.ToInt32(UHID.Text.Trim() == "" ? "0" : UHID.Text.Trim());
                    objresult.RoleID = LogData.RoleID;

                    if (SubgroupID.Text == "999")  // to avoid culture result entry page as client doesnt use it they use paramter for entry culture
                    {
                        GetCultureLabresultlist(objresult);
                    }
                    else
                    {
                        GetLabresultlist(objresult);
                    }
                    LinkButton result1 = (LinkButton)GV_PatientList.Rows[i].Cells[0].FindControl("lbl1_test");
                    result1.Focus();
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lbl_result, "system", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;
                return;
            }
        }
        protected void GV_PatientList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }
        protected void GetTestListByInvNo(SampleCollectionData result)
        {
            LabSampleCollctionBO objlabBO = new LabSampleCollctionBO();
            List<SampleCollectionData> Result = objlabBO.GetResultentrytestlist(result);
            if (Result.Count > 0)
            {
                txt_PatientDetails.Text = Result[0].PatientName.ToString();
                hdnuhid.Text = Result[0].UHID.ToString();
                hdninvnumber.Text = Result[0].Investigationumber.ToString();
                gv_labtestlist.DataSource = Result;
                gv_labtestlist.DataBind();
            }
            else
            {
                gv_labtestlist.DataSource = null;
                gv_labtestlist.DataBind();
                hdninvnumber.Text = "";
                hdnuhid.Text = "";
            }


        }
        //-------END OF TEST LIST------//
        protected void btn_refresh_Click(object sender, EventArgs e)
        {
            ddl_patienttype.SelectedIndex = 0;
            txt_ipnumber.Text = "";
            txt_patientnames.Text = "";
            txt_testnames.Text = "";
            txt_invnumber.Text = "";
            ddl_referal.SelectedIndex = 0;

            GV_PatientList.DataSource = null;
            GV_PatientList.DataBind();
            GV_PatientList.Visible = true;
            lbl_result.Visible = false;
            lblmessage1.Visible = false;
            GVreset();

        }
        private void GVreset()
        {
            txt_PatientDetails.Text = "";
            gv_labtestlist.DataSource = null;
            gv_labtestlist.DataBind();
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            Gv_LabResult.DataSource = null;
            Gv_LabResult.DataBind();
            txt_name.Text = "";
            txt_address.Text = "";
            txt_UHID.Text = "";
            txt_invnumbers.Text = "";
            txt_patientnumber.Text = "";
            txt_referral.Text = "";
            txt_testname.Text = "";
            txtrequestedon.Text = "";
            hdntestID.Value = null;
            txt_overallReamrks.Text = "";
            ddl_verifiedby.SelectedIndex = 0;
            lblmessage.Visible = false;
            div1.Visible = false;
            tabcontainerSampleCollection.ActiveTabIndex = 0;
            tab2.Visible = false;
            ddl_centre.SelectedIndex = 0;
            lblmessage1.Visible = false;

        }
        protected void gv_labtestlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Search")
                {
                    lblmessage.Visible = false;
                    LabResultData objresult = new LabResultData();
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gv_labtestlist.Rows[i];
                    Label UHID = (Label)gr.Cells[0].FindControl("lbl_UHID");
                    Label InvNumber = (Label)gr.Cells[0].FindControl("lvl_inv");
                    Label TestID = (Label)gr.Cells[0].FindControl("lblTestID");
                    Label mtrecivedstatus = (Label)gr.Cells[0].FindControl("lbl_reciestatus");
                    Label SubgroupID = (Label)gr.Cells[0].FindControl("lbl_subgrpID");
                    //if (mtrecivedstatus.Text.Trim() == "0")
                    //{
                    //    LinkButton result2 = (LinkButton)gv_labtestlist.Rows[i].Cells[0].FindControl("lbl_test");
                    //    result2.Focus();
                    //    Messagealert_.ShowMessage(lblmessage1, "MTrecivdtime", 0);
                    //    div2.Visible = true;
                    //    div2.Attributes["class"] = "FailAlert";
                    //    return;
                    //}
                    //else
                    //{
                    //    lblmessage1.Visible = false;
                    //}
                    rbtn_normal.Checked = true;
                    rbtn_exceptional.Checked = false;
                    objresult.Investigationumber = InvNumber.Text.Trim() == "" ? null : InvNumber.Text.Trim();
                    objresult.LabServiceID = Convert.ToInt32(TestID.Text.Trim() == "" ? "0" : TestID.Text.Trim());
                    objresult.UHID = Convert.ToInt32(UHID.Text.Trim() == "" ? "0" : UHID.Text.Trim());
                    objresult.RoleID = LogData.RoleID;

                    if (SubgroupID.Text == "20")
                    {
                        GetCultureLabresultlist(objresult);
                    }
                    else
                    {
                        GetLabresultlist(objresult);
                    }
                    LinkButton result1 = (LinkButton)gv_labtestlist.Rows[i].Cells[0].FindControl("lbl_test");
                    result1.Focus();
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lbl_result, "system", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;
                return;
            }
        }
        protected void GetLabresultlist(LabResultData result)
        {
            tab3.Visible = false;
            tab2.Visible = true;
            ddl_verifiedby.SelectedIndex = 0;
            LabSampleCollctionBO objlabBO = new LabSampleCollctionBO();
            List<LabResultData> Result = objlabBO.GetLabResults(result);
            if (Result.Count > 0)
            {
                tab2.Visible = true;
                tabcontainerSampleCollection.ActiveTabIndex = 1;
                txt_name.Text = Result[0].PatientName.ToString();
                txt_address.Text = Result[0].Address.ToString();
                txt_UHID.Text = Result[0].UHID.ToString();
                txt_invnumbers.Text = Result[0].Investigationumber.ToString();
                txt_patientnumber.Text = Result[0].PatientNumber.ToString();
                txt_referral.Text = Result[0].ReferalDoctor.ToString();
                if (ddl_verifiedby.Items.FindByValue(Result[0].VerifiedBy.ToString()) != null && Result[0].VerifiedBy > 0)
                {
                    ddl_verifiedby.SelectedValue = Result[0].VerifiedBy.ToString();
                }
                //if (Convert.ToInt64(Result[0].VerifiedBy) > 0 && LogData.RoleID > 1)
                //{
                //    ddl_verifiedby.Attributes["disabled"] = "disabled";
                //    btnsave.Attributes["disabled"] = "disabled";
                //}
                //else
                //{
                //    ddl_verifiedby.Attributes.Remove("disabled");
                //    btnsave.Attributes.Remove("disabled");
                //}
                txt_testname.Text = Result[0].TestName.ToString();
                hdntestID.Value = Result[0].LabServiceID.ToString();
                txtrequestedon.Text = Result[0].InvRequestedOn.ToString();
                txt_overallReamrks.Text = Result[0].Remarks.ToString();
                
                ddl_template.SelectedValue = Result[0].TemplateType.ToString();
                ddl_machinename.SelectedValue = Result[0].MachineID.ToString();
                Gv_LabResult.DataSource = Result;
                Gv_LabResult.DataBind();
                bindgridfoucs();

            }
            else
            {
                tab2.Visible = false;
                tabcontainerSampleCollection.ActiveTabIndex = 0;
                rbtn_normal.Checked = false;
                ddl_verifiedby.SelectedIndex = 0;
                ddl_template.SelectedIndex = 0;
                txt_name.Text = "";
                hdntestID.Value = null;
                txt_address.Text = "";
                txt_UHID.Text = "";
                txt_invnumbers.Text = "";
                txt_patientnumber.Text = "";
                txt_referral.Text = "";
                txt_testname.Text = "";
                txtrequestedon.Text = "";
                txt_overallReamrks.Text = "";
                Gv_LabResult.DataSource = null;
                Gv_LabResult.DataBind();
                Messagealert_.ShowMessage(lblmessage1, "Please check the parameters for this test and try again", 0);
                div2.Attributes["class"] = "FailAlert";
                div2.Visible = true;
                return;
            }


        }
        protected void GetCultureLabresultlist(LabResultData result)
        {
            tab3.Visible = true;
            tab2.Visible = false;
            LabSampleCollctionBO objlabBO = new LabSampleCollctionBO();
            List<LabResultData> Result = objlabBO.GetMicroLabResults(result);
            if (Result.Count > 0)
            {
                tabcontainerSampleCollection.ActiveTabIndex = 1;
                txt_patientname3.Text = Result[0].PatientName.ToString();
                txt_address3.Text = Result[0].Address.ToString();
                txt_uhid3.Text = Result[0].UHID.ToString();
                txt_inv3.Text = Result[0].Investigationumber.ToString();
                txt_patientnumber3.Text = Result[0].PatientNumber.ToString();
                txt_referal3.Text = Result[0].ReferalDoctor.ToString();
                ddl_verified3.SelectedValue = Result[0].VerifiedBy.ToString();
                txt_testname3.Text = Result[0].TestName.ToString();
                hdntestID3.Value = Result[0].LabServiceID.ToString();
                txt_requested3.Text = Result[0].InvRequestedOn.ToString();
                txt_remark3.Text = Result[0].Remarks.ToString();
                ddl_template3.SelectedValue = Result[0].TemplateType.ToString();
                txt_sample.Text = Result[0].Sample.ToString();
                txt_colonycount.Text = Result[0].Colony.ToString();
                txt_organisimyeilded.Text = Result[0].OrganismYielded.ToString();
                txt_remark3.Text = Result[0].Remarks.ToString();
                ddlTestMethod.SelectedValue = Result[0].MethodID.ToString();
                if (Result[0].ReportType.ToString() == "1")
                {
                    rabtn_growth.Checked = true;
                    rabtn_nogrowth.Checked = false;
                    idg.Visible = true;
                    ddl_growthtype.SelectedValue = Result[0].GrowthType.ToString();
                }
                else
                {
                    rabtn_growth.Checked = false;
                    rabtn_nogrowth.Checked = true;
                }
                tab3.Visible = true;
                Gv_antibioticlist.DataSource = Result;
                Gv_antibioticlist.DataBind();
            }
            else
            {
                tabcontainerSampleCollection.ActiveTabIndex = 0;
                ddl_verified3.SelectedIndex = 0;
                ddl_template3.SelectedIndex = 0;
                txt_testname3.Text = "";
                hdntestID3.Value = null;
                txt_address.Text = "";
                txt_UHID.Text = "";
                txt_sample.Text = "";
                txt_colonycount.Text = "";
                txt_organisimyeilded.Text = "";
                txt_remark3.Text = "";
                txt_invnumbers.Text = "";
                txt_patientnumber.Text = "";
                txt_referral.Text = "";
                txt_testname.Text = "";
                txtrequestedon.Text = "";
                txt_overallReamrks.Text = "";
                Gv_antibioticlist.DataSource = null;
                Gv_antibioticlist.DataBind();
                tab3.Visible = false;
            }


        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        //protected void btnsave_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (LogData.SaveEnable == 0)
        //        {
        //            Messagealert_.ShowMessage(lblmessage, "SaveEnable", 0);
        //            div1.Visible = true;
        //            div1.Attributes["class"] = "FailAlert";
        //            return;
        //        }
        //        else
        //        {
        //            lblmessage.Visible = false;
        //        }

        //        List<LabResultData> Listresult = new List<LabResultData>();
        //        LabResultData objlabresult = new LabResultData();
        //        LabSampleCollctionBO objresultbo = new LabSampleCollctionBO();
        //        int rowcount = 0;
        //        foreach (GridViewRow row in Gv_LabResult.Rows)
        //        {
        //            Label testID = (Label)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("lblTestID");
        //            TextBox Ranges = (TextBox)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("txt_range");
        //            TextBox result = (TextBox)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("txtresult");
        //            TextBox Remarks = (TextBox)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("txt_remarks");
        //            CheckBox chk_normal = (CheckBox)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("chknormal");
        //            Label lblmachineID = (Label)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("lbl_machineID");
        //            Label lblmethodID = (Label)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("lbl_methodID");
        //            Label unit = (Label)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("lblunitID");
        //            Label patienttype = (Label)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("lbl_patienttype");
        //            Label rowtype = (Label)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("lbl_rowtypeID");
        //            Label urgency = (Label)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("lbl_urgencyID");
        //            Label ParameterID = (Label)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("lbl_paramaterID");
        //            LabResultData ObjDetails = new LabResultData();

        //            ObjDetails.Investigationumber = txt_invnumbers.Text.Trim();
        //            ObjDetails.UHID = Convert.ToInt64(txt_UHID.Text == "" ? "0" : txt_UHID.Text);
        //            ObjDetails.PatientTypeID = Convert.ToInt32(patienttype.Text == "" ? "0" : patienttype.Text);
        //            ObjDetails.OPno = patienttype.Text == "1" ? txt_patientnumber.Text.Trim() : "";
        //            ObjDetails.IPno = patienttype.Text == "2" ? txt_patientnumber.Text.Trim() : "";
        //            ObjDetails.Emergno = patienttype.Text == "3" ? txt_patientnumber.Text.Trim() : "";
        //            ObjDetails.LabServiceID = Convert.ToInt32(testID.Text == "" ? "0" : testID.Text);
        //            ObjDetails.MachineID = Convert.ToInt32(lblmachineID.Text == "" ? "0" : lblmachineID.Text);
        //            ObjDetails.UnitID = Convert.ToInt32(unit.Text == "" ? "0" : unit.Text);
        //            ObjDetails.MethodID = Convert.ToInt32(lblmethodID.Text == "" ? "0" : lblmethodID.Text);
        //            ObjDetails.ParmRemarks = Remarks.Text.Trim();
        //            ObjDetails.LabResultValue = result.Text.Trim();
        //            ObjDetails.Ranges = Ranges.Text.Trim();
        //            ObjDetails.RowType = Convert.ToInt32(rowtype.Text == "" ? "0" : rowtype.Text);

        //            ObjDetails.IsNormal = chk_normal.Checked ? 1 : 0;
        //            ObjDetails.UrgencyID = Convert.ToInt32(urgency.Text == "" ? "0" : urgency.Text);
        //            ObjDetails.ParameterID = Convert.ToInt64(ParameterID.Text == "" ? "0" : ParameterID.Text);
        //            if (result.Text != "")
        //            {
        //                rowcount = rowcount + 1;
        //            }
        //            Listresult.Add(ObjDetails);

        //        }
        //        if (rowcount == 0)
        //        {
        //            Messagealert_.ShowMessage(lblmessage, "Labresultcount", 0);
        //            div1.Visible = true;
        //            div1.Attributes["class"] = "FailAlert";
        //            return;
        //        }
        //        else
        //        {
        //            lblmessage.Visible = false;
        //        }
        //        if (ddl_template.SelectedIndex == 0)
        //        {
        //            Messagealert_.ShowMessage(lblmessage, "Labtemplate", 0);
        //            div1.Visible = true;
        //            div1.Attributes["class"] = "FailAlert";
        //            return;
        //        }
        //        else
        //        {
        //            lblmessage.Visible = false;
        //        }
        //        objlabresult.XMLData = XmlConvertor.LabResultEntryDatatoXML(Listresult).ToString();
        //        objlabresult.EmployeeID = LogData.EmployeeID;
        //        objlabresult.FinancialYearID = LogData.FinancialYearID;
        //        objlabresult.ReportType = rbtn_normal.Checked ? 1 : rbtn_exceptional.Checked ? 2 : 0;
        //        objlabresult.EmployeeID = LogData.EmployeeID;
        //        objlabresult.VerifiedBy = Convert.ToInt64(ddl_verifiedby.SelectedValue == "" ? "0" : ddl_verifiedby.SelectedValue);
        //        objlabresult.TemplateType = Convert.ToInt32(ddl_template.SelectedValue == "" ? "0" : ddl_template.SelectedValue);
        //        objlabresult.HospitalID = LogData.HospitalID;
        //        objlabresult.Remarks = txt_overallReamrks.Text.Trim();
        //        objlabresult.IPaddress = LogData.IPaddress;

        //        int resultstatus = objresultbo.UpdateLabResultEntryDetails(objlabresult);
        //        if (resultstatus == 1 || resultstatus == 2)
        //        {
        //            LabResultData objresult = new LabResultData();
        //            objresult.LabServiceID = Convert.ToInt32(hdntestID.Value == "" ? "0" : hdntestID.Value);
        //            objresult.UHID = Convert.ToInt64(txt_UHID.Text == "" ? "0" : txt_UHID.Text);
        //            objresult.Investigationumber = txt_invnumbers.Text.Trim();
        //            GetLabresultlist(objresult);
        //            Messagealert_.ShowMessage(lblmessage, resultstatus == 1 ? "save" : "update", 1);
        //            div1.Visible = true;
        //            div1.Attributes["class"] = "SucessAlert";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
        //        LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
        //        lblmessage.Text = ExceptionMessage.GetMessage(ex);
        //    }
        //}
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (LogData.SaveEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage3, "SaveEnable", 0);
                    div5.Visible = true;
                    div5.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage3.Visible = false;
                }
                if (rabtn_growth.Checked == false && rabtn_nogrowth.Checked == false)
                {
                    Messagealert_.ShowMessage(lblmessage3, "Please check result type.(Growth or No growth.)", 0);
                    div5.Visible = true;
                    div5.Attributes["class"] = "FailAlert";
                    rabtn_growth.Focus();
                    return;
                }
                else
                {
                    lblmessage3.Visible = false;
                }
                if (rabtn_growth.Checked == true)
                {
                    if (ddl_growthtype.SelectedIndex == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage3, "Please select growth type.", 0);
                        div5.Visible = true;
                        div5.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage3.Visible = false;
                    }
                }

                if (txt_sample.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblmessage3, "Please enter sample type", 0);
                    div5.Visible = true;
                    div5.Attributes["class"] = "FailAlert";
                    txt_sample.Focus();
                    return;
                }
                else
                {
                    lblmessage3.Visible = false;
                }
                if (txt_colonycount.Text.Trim() == "" && rabtn_growth.Checked == true)
                {
                    Messagealert_.ShowMessage(lblmessage3, "Please enter colony count.", 0);
                    div5.Visible = true;
                    div5.Attributes["class"] = "FailAlert";
                    txt_colonycount.Focus();
                    return;
                }
                else
                {
                    lblmessage3.Visible = false;
                }
                if (txt_organisimyeilded.Text.Trim() == "" && rabtn_growth.Checked == true)
                {
                    Messagealert_.ShowMessage(lblmessage3, "Please enter organism yield.", 0);
                    div5.Visible = true;
                    div5.Attributes["class"] = "FailAlert";
                    txt_organisimyeilded.Focus();
                    return;
                }
                else
                {
                    lblmessage3.Visible = false;
                }
                if (txt_organisimyeilded.Text.Trim() == "" && rabtn_nogrowth.Checked == true)
                {
                    Messagealert_.ShowMessage(lblmessage3, "Please enter result.", 0);
                    div5.Visible = true;
                    div5.Attributes["class"] = "FailAlert";
                    txt_organisimyeilded.Focus();
                    return;
                }
                else
                {
                    lblmessage3.Visible = false;
                }
                List<LabResultData> Listresult = new List<LabResultData>();
                LabResultData objlabresult = new LabResultData();
                LabSampleCollctionBO objresultbo = new LabSampleCollctionBO();
                int rowcount = 0;
                if (rabtn_growth.Checked == true)
                {
                    foreach (GridViewRow row in Gv_antibioticlist.Rows)
                    {
                        Label AntibioticID = (Label)Gv_antibioticlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_antibiotocID");
                        DropDownList SensitivityID = (DropDownList)Gv_antibioticlist.Rows[row.RowIndex].Cells[0].FindControl("ddl_sensitivity");
                        LabResultData ObjDetails = new LabResultData();
                        if (SensitivityID.SelectedIndex > 0)
                        {
                            rowcount = rowcount + 1;
                            ObjDetails.AntibioticID = Convert.ToInt32(AntibioticID.Text == "" ? "0" : AntibioticID.Text);
                            ObjDetails.AntibioticSensitiveTypeID = Convert.ToInt32(SensitivityID.SelectedValue == "" ? "0" : SensitivityID.SelectedValue);
                            Listresult.Add(ObjDetails);
                        }
                    }
                }
                if (rowcount == 0 && rabtn_growth.Checked == true)
                {
                    Messagealert_.ShowMessage(lblmessage3, "please select antibioctic sensitivity.", 0);
                    div5.Visible = true;
                    div5.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage3.Visible = false;
                }
                if (ddl_template3.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage3, "Labtemplate", 0);
                    div5.Visible = true;
                    div5.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage3.Visible = false;
                }
                objlabresult.XMLData = XmlConvertor.LabCultureDatatoXML(Listresult).ToString();
                objlabresult.EmployeeID = LogData.EmployeeID;
                objlabresult.FinancialYearID = LogData.FinancialYearID;
                objlabresult.EmployeeID = LogData.EmployeeID;
                objlabresult.VerifiedBy = Convert.ToInt64(ddl_verified3.SelectedValue == "" ? "0" : ddl_verified3.SelectedValue);
                objlabresult.TemplateType = Convert.ToInt32(ddl_template3.SelectedValue == "" ? "0" : ddl_template3.SelectedValue);
                objlabresult.Sample = txt_sample.Text.Trim();
                objlabresult.Colony = txt_colonycount.Text.Trim();
                objlabresult.OrganismYielded = txt_organisimyeilded.Text.Trim();
                objlabresult.HospitalID = LogData.HospitalID;
                objlabresult.ReportType = rabtn_growth.Checked ? 1 : 2;
                objlabresult.GrowthType = Convert.ToInt32(ddl_growthtype.SelectedValue == "" ? "0" : ddl_growthtype.SelectedValue);
                objlabresult.Remarks = txt_remark3.Text.Trim();
                objlabresult.IPaddress = LogData.IPaddress;
                objlabresult.UHID = Convert.ToInt64(txt_uhid3.Text == "" ? "0" : txt_uhid3.Text);
                objlabresult.LabServiceID = Convert.ToInt32(hdntestID3.Value == "" ? "0" : hdntestID3.Value);
                objlabresult.Investigationumber = txt_inv3.Text.Trim();
                objlabresult.MethodID = Convert.ToInt32(ddlTestMethod.SelectedValue == "" ? "0" : ddlTestMethod.SelectedValue);

                int resultstatus = objresultbo.UpdateLabCultureResultEntryDetails(objlabresult);
                if (resultstatus == 1 || resultstatus == 2)
                {
                    LabResultData objresult = new LabResultData();
                    objresult.LabServiceID = Convert.ToInt32(hdntestID3.Value == "" ? "0" : hdntestID3.Value);
                    objresult.UHID = Convert.ToInt64(txt_uhid3.Text == "" ? "0" : txt_uhid3.Text);
                    objresult.Investigationumber = txt_inv3.Text.Trim();
                    GetCultureLabresultlist(objresult);
                    gettestlist();
                    string template = ddl_template3.SelectedItem.Text.Substring(ddl_template3.SelectedItem.Text.LastIndexOf(':') + 1);
                    string inv = txt_inv3.Text;
                    int TestID = Convert.ToInt32(hdntestID3.Value == null ? "0" : hdntestID3.Value);
                    Int64 UHID = Convert.ToInt64(txt_uhid3.Text == "" ? "0" : txt_uhid3.Text);
                    ConvertReportToImage(template, 2, TestID, UHID, inv);
                    Messagealert_.ShowMessage(lblmessage3, resultstatus == 1 ? "save" : "update", 1);
                    div5.Visible = true;
                    div5.Attributes["class"] = "SucessAlert";
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
            }
        }
        protected void Bulk_Update(object sender, EventArgs e)
        {
            Int32 selectedmachineID = Convert.ToInt32(ddl_machinename.SelectedValue == "" ? "0" : ddl_machinename.SelectedValue);
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[44] {
             new DataColumn("PID", typeof(Int64)),
             new DataColumn("UHID", typeof(Int64)),
             new DataColumn("InvNumber", typeof(string)),
             new DataColumn("IP_No", typeof(string)),
             new DataColumn("OP_No", typeof(string)),
             new DataColumn("Emrg_No", typeof(string)),
             new DataColumn("PatientType", typeof(Int64)),
             new DataColumn("TestID", typeof(Int32)),
             new DataColumn("ParameterID", typeof(Int32)),
             new DataColumn("Result", typeof(string)),
             new DataColumn("UnitID", typeof(Int32)),
             new DataColumn("ReagentID", typeof(Int32)),
             new DataColumn("MethodID", typeof(Int32)),
             new DataColumn("MachineID", typeof(Int32)),
             new DataColumn("ContainerID", typeof(Int32)),
             new DataColumn("Ranges", typeof(string)),
             new DataColumn("RangeFrom", typeof(decimal)),
             new DataColumn("RangeTo", typeof(decimal)),
             new DataColumn("IsNormal", typeof(Int32)),
             new DataColumn("RowType", typeof(Int32)),
             new DataColumn("ReportType", typeof(Int32)),
             new DataColumn("TemplateType", typeof(Int32)),
             new DataColumn("UrgencyID", typeof(Int32)),
             new DataColumn("SampleName", typeof(string)),
             new DataColumn("Colony", typeof(string)),
             new DataColumn("OrganismYielded", typeof(string)),
             new DataColumn("AntibioticID", typeof(Int32)),
             new DataColumn("AntibioticSensitiveTypeID", typeof(Int32)),
             new DataColumn("GrowthType", typeof(Int32)),
             new DataColumn("AddedBy", typeof(string)),
             new DataColumn("AddedDate", typeof(DateTime)),
             new DataColumn("VeriefiedOn", typeof(DateTime)),
             new DataColumn("VerifiedBy", typeof(Int64)),
             new DataColumn("IsHideParameter", typeof(Int32)),
             new DataColumn("IsHideRemark", typeof(Int32)),
             new DataColumn("ParmRemarks", typeof(string)),
             new DataColumn("RangeWording", typeof(string)),
             new DataColumn("Remarks", typeof(string)),
             new DataColumn("Barcode", typeof(string)),
             new DataColumn("ModifiedDate", typeof(DateTime)),
             new DataColumn("ModifiedBy", typeof(string)),
             new DataColumn("HospitalID", typeof(Int32)),
             new DataColumn("FinancialYearID", typeof(Int32)),
             new DataColumn("IsActive", typeof(string))
               });
            int rowcount = 0;
            foreach (GridViewRow row in Gv_LabResult.Rows)
            {
                Label testID = (Label)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("lblTestID");
                Label ID = (Label)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                TextBox Normalrange = (TextBox)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("txt_range");
                TextBox rangeWording = (TextBox)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("txt_remarks");
                TextBox results = (TextBox)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("txtresult");
                TextBox PRemarks = (TextBox)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("txt_remarks");
                CheckBox chk_normal = (CheckBox)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("chknormal");
                Label lblmachineID = (Label)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("lbl_machineID");
                Label lblmethodID = (Label)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("lbl_methodID");
                Label unit = (Label)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("lblunitID");
                Label patienttype = (Label)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("lbl_patienttype");
                Label rowtype = (Label)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("lbl_rowtypeID");
                Label urgency = (Label)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("lbl_urgencyID");
                Label ParaID = (Label)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("lbl_paramaterID");
                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                DateTime pardate = System.DateTime.Now;

                Int64 PID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                Int64 UHID = Convert.ToInt64(txt_UHID.Text == "" ? "0" : txt_UHID.Text);
                string InvNumber = txt_invnumbers.Text.Trim();
                string IP_No = "";
                string OP_No = "";
                string Emrg_No = "";
                int PatientType = Convert.ToInt32(patienttype.Text == "" ? "0" : patienttype.Text);
                int TestID = Convert.ToInt32(hdntestID.Value == "" ? "0" : hdntestID.Value);
                string Result = results.Text.Trim();

                int UnitID = Convert.ToInt32(unit.Text == "" ? "0" : unit.Text);
                int ReagentID = 0;
                int MethodID = Convert.ToInt32(lblmethodID.Text == "" ? "0" : lblmethodID.Text);
                // int MachineID = Convert.ToInt32(lblmachineID.Text == "" ? "0" : lblmachineID.Text);
                int MachineID = selectedmachineID;
                int ContainerID = 0;
                int ParameterID = Convert.ToInt32(ParaID.Text == "" ? "0" : ParaID.Text);
                string Ranges = Normalrange.Text;
                int RangeFrom = 0;
                int RangeTo = 0;
                int IsNormal = chk_normal.Checked ? 1 : 0;
                int RowType = Convert.ToInt32(rowtype.Text == "" ? "0" : rowtype.Text);
                int ReportType = rbtn_normal.Checked ? 1 : rbtn_exceptional.Checked ? 2 : 0;
                int TemplateType = Convert.ToInt32(ddl_template.Text == "" ? "0" : ddl_template.Text);
                int UrgencyID = Convert.ToInt32(urgency.Text == "" ? "0" : urgency.Text);
                DateTime VeriefiedOn = pardate;
                Int64 VerifiedBy = Convert.ToInt64(ddl_verifiedby.SelectedValue == "" ? "0" : ddl_verifiedby.SelectedValue);
                int IsHideParameter = 0;
                int IsHideRemark = 0;
                string ParmRemarks = "";
                Int64 UserLoginID = LogData.EmployeeID;
                int FinancialYearID = LogData.FinancialYearID;
                int HospitalID = LogData.HospitalID;
                string AddedBy = LogData.EmployeeID.ToString();
                DateTime AddedDate = pardate;
                DateTime ModifiedDate = pardate;
                string ModifiedBy = LogData.EmployeeID.ToString();
                int IsActive = 1;
                string Barcode = "";
                string RangeWording = rangeWording.Text;
                string Remarks = txt_overallReamrks.Text.Trim();
                string SampleName = "";
                string Colony = "";
                string OrganismYielded = "";
                Int32 AntibioticID = 0;
                Int32 AntibioticSensitiveTypeID = 0;
                Int32 GrowthType = 0;
                dt.Rows.Add(PID, UHID, InvNumber, IP_No, OP_No, Emrg_No, PatientType, TestID, ParameterID, Result, UnitID, ReagentID,
                MethodID, MachineID, ContainerID, Ranges, RangeFrom, RangeTo, IsNormal, RowType, ReportType, TemplateType, UrgencyID,
                SampleName, Colony, OrganismYielded, AntibioticID, AntibioticSensitiveTypeID, GrowthType,
                AddedBy, AddedDate, VeriefiedOn, VerifiedBy, IsHideParameter, IsHideRemark, ParmRemarks,RangeWording, Remarks, Barcode, ModifiedDate,
                ModifiedBy, HospitalID, FinancialYearID, IsActive);
                if (results.Text != "")
                {
                    rowcount = rowcount + 1;
                }
            }
            if (rowcount == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Labresultcount", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            //if (ddl_template.SelectedIndex == 0)
            //{
            //    Messagealert_.ShowMessage(lblmessage, "Labtemplate", 0);
            //    div1.Visible = true;
            //    div1.Attributes["class"] = "FailAlert";
            //    return;
            //}
            //else
            //{
            //    lblmessage.Visible = false;
            //}
            //if (ddl_verifiedby.SelectedIndex == 0)
            //{
            //    Messagealert_.ShowMessage(lblmessage, "verifyby", 0);
            //    div1.Visible = true;
            //    div1.Attributes["class"] = "FailAlert";
            //    ddl_verifiedby.Focus();
            //    return;
            //}
            //else
            //{
            //    lblmessage.Visible = false;
            //}
            string constr = ConfigurationManager.ConnectionStrings["SqlConnectionString11"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
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
                using (SqlCommand cmd = new SqlCommand("usp_MDQ_util_UpdateLabResultDetails"))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@tlabresult", dt);
                        cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = Convert.ToInt64(txt_UHID.Text == "" ? "0" : txt_UHID.Text);
                        cmd.Parameters.Add("@VerifiedBy", SqlDbType.BigInt).Value = Convert.ToInt64(ddl_verifiedby.SelectedValue == "" ? "0" : ddl_verifiedby.SelectedValue);
                        cmd.Parameters.Add("@EmployeeID", SqlDbType.BigInt).Value = LogData.EmployeeID;
                        cmd.Parameters.Add("@HospitalID", SqlDbType.Int).Value = LogData.HospitalID;
                        cmd.Parameters.Add("@FinancialYearID", SqlDbType.Int).Value = LogData.FinancialYearID;
                        cmd.Parameters.Add("@TestID", SqlDbType.Int).Value = Convert.ToInt32(hdntestID.Value == "" ? "0" : hdntestID.Value);
                        cmd.Parameters.Add("@InvNumber", SqlDbType.VarChar).Value = txt_invnumbers.Text.Trim();
                        cmd.Parameters.Add("@Output", SqlDbType.SmallInt).Direction = ParameterDirection.Output;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        int result = Convert.ToInt32(cmd.Parameters.Add("@Output", SqlDbType.SmallInt).Direction);
                        if (result == 1)
                        {
                            bindgrid(1);
                            gettestlist();
                            string template = ddl_template.SelectedItem.Text.Substring(ddl_template.SelectedItem.Text.LastIndexOf(':') + 1);
                            string inv = txt_invnumbers.Text;
                            int TestID = Convert.ToInt32(hdntestID.Value == null ? "0" : hdntestID.Value);
                            Int64 UHID = Convert.ToInt64(txt_UHID.Text == "" ? "0" : txt_UHID.Text);
                            ConvertReportToImage(template, 1, TestID, UHID, inv);
                            Messagealert_.ShowMessage(lblmessage, "save", 1);
                            div1.Visible = true;
                            div1.Attributes["class"] = "SucessAlert";
                        }
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                        LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                    }
                }
            }
        }
        protected void gettestlist()
        {
            SampleCollectionData objresult = new SampleCollectionData();
            objresult.Investigationumber = hdninvnumber.Text;
            objresult.UHID = Convert.ToInt32(hdnuhid.Text.Trim() == "" ? "0" : hdnuhid.Text.Trim());
            objresult.RoleID = LogData.RoleID;
            GetTestListByInvNo(objresult);
        }
        protected void txt_result_TextChanged(object sender, EventArgs e)
        {
            int Lastindex = Gv_LabResult.Rows.Count - 1;
            TextBox txt = sender as TextBox;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            if (Lastindex > index)
            {
                Label rowtype = (Label)Gv_LabResult.Rows[index + 1].Cells[0].FindControl("lbl_rowtypeID");
                if (rowtype.Text == "1")
                {
                    TextBox result2 = (TextBox)Gv_LabResult.Rows[index + 2].Cells[0].FindControl("txtresult");
                    result2.Focus();
                }
                else
                {
                    TextBox result1 = (TextBox)Gv_LabResult.Rows[index + 1].Cells[0].FindControl("txtresult");
                    result1.Focus();
                }
            }
            else if (Lastindex == index)
            {
                TextBox result2 = (TextBox)Gv_LabResult.Rows[index].Cells[0].FindControl("txtresult");
                result2.Focus();
            }

        }
        protected void gv_labtestlist_RowDataBound(object sender, GridViewRowEventArgs e)
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
            //if (Convert.ToInt32(Session["currenrrow"].ToString() == null ? "0" : Session["currenrrow"].ToString()) == rowcount)
            //{
            //    Label result1 = (Label)gv_labtestlist.Rows[rowcount].Cells[0].FindControl("lblID");
            //    result1.Focus();
            //}
        }
        protected void Gv_LabResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txt_unit = e.Row.FindControl("txt_unit") as TextBox;
                    TextBox txt_reference = e.Row.FindControl("txt_range") as TextBox;
                    Label rowtype = e.Row.FindControl("lbl_rowtypeID") as Label;
                    TextBox txt_result = e.Row.FindControl("txtresult") as TextBox;
                    TextBox txt_remarks = e.Row.FindControl("txt_remarks") as TextBox;
                    CheckBox chk_normal = e.Row.FindControl("chknormal") as CheckBox;
                    Label ResultStatus = e.Row.FindControl("lbl_resultstatus") as Label;
                    Label ReportType = e.Row.FindControl("lbl_reporttype") as Label;

                    if (rowtype.Text == "1")
                    {
                        txt_unit.Enabled = false;
                        txt_reference.Enabled = false;
                        txt_result.Enabled = false;
                        txt_remarks.Enabled = false;
                        chk_normal.Enabled = false;
                    }
                    else
                    {
                        chk_normal.Enabled = true;
                    }
                    if (ResultStatus.Text == "1")
                    {
                        chk_normal.Checked = true;
                    }
                    else
                    {
                        chk_normal.Checked = false;
                    }
                    if (ReportType.Text == "1")
                    {
                        rbtn_normal.Checked = true;
                        rbtn_exceptional.Checked = false;
                        txt_unit.Enabled = false;
                        txt_reference.Enabled = false;
                    }
                    if (ReportType.Text == "2")
                    {
                        rbtn_normal.Checked = false;
                        rbtn_exceptional.Checked = true;
                        txt_unit.Enabled = false;
                        txt_reference.Enabled = true;
                    }
                    if (rowtype.Text == "4")
                    {
                        txt_unit.Enabled = false;
                        txt_reference.Enabled = false;
                        txt_result.Enabled = false;
                        txt_remarks.Enabled = false;
                        chk_normal.Enabled = false;
                    }
                }
            }
        }
        protected void gv_labtestlist_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (LogData.UpdateEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage1, "UpdateEnable", 0);
                div2.Visible = true;
                div2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage1.Visible = false;
            }
            Int64 ID = Convert.ToInt32(gv_labtestlist.DataKeys[e.RowIndex].Values["ID"].ToString());
            System.Web.UI.WebControls.Label invnumber = (System.Web.UI.WebControls.Label)gv_labtestlist.Rows[e.RowIndex].FindControl("lvl_inv");
            System.Web.UI.WebControls.Label TestID = (System.Web.UI.WebControls.Label)gv_labtestlist.Rows[e.RowIndex].FindControl("lblTestID");

            Session["currenrrow"] = e.RowIndex;

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
                bindgrid(1);
                Messagealert_.ShowMessage(lblmessage1, "update", 1);
                div2.Visible = true;
                div2.Attributes["class"] = "SucessAlert";
            }
            else
            {
                lblmessage1.Visible = false;
            }

        }
        protected void rbtn_normal_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in Gv_LabResult.Rows)
            {
                TextBox Ranges = (TextBox)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("txt_range");
                Ranges.Enabled = false;
            }
        }
        protected void rbtn_exceptional_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in Gv_LabResult.Rows)
            {
                TextBox Ranges = (TextBox)Gv_LabResult.Rows[row.RowIndex].Cells[0].FindControl("txt_range");
                Ranges.Enabled = true;
            }
        }
        protected void btnprint_Click(object sender, EventArgs e)
        {
            if (ddl_template.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Labtemplate", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            //   string template = ddl_template.SelectedItem.Text.Substring(ddl_template.SelectedItem.Text.LastIndexOf(':') + 1);
            string template = ddl_template.SelectedValue.ToString();
            string param = "option=MultipleReport&Inv=" + txt_invnumbers.Text + "&UHID=" + txt_UHID.Text + "&TestID=" + hdntestID.Value + "&showheader=" + "0" + "&Template=" + template + "&Type=" + "1";

            Commonfunction common = new Commonfunction();
            string ecryptstring = common.Encrypt(param);
            string baseurl = "../MedLab/Report/ReportViewer.aspx?ID=" + ecryptstring;

            string fullURL = "window.open('" + baseurl + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
        protected void Linknewload_Click(object sender, EventArgs e)
        {
            LabResultData objresult = new LabResultData();
            objresult.Investigationumber = txt_invnumbers.Text.Trim() == "" ? null : txt_invnumbers.Text.Trim();
            objresult.LabServiceID = Convert.ToInt32(hdntestID.Value == "" ? "0" : hdntestID.Value);
            objresult.UHID = Convert.ToInt32(txt_UHID.Text.Trim() == "" ? "0" : txt_UHID.Text.Trim());
            objresult.RoleID = LogData.RoleID;
            objresult.IsRefresh = 1;
            GetLabresultlist(objresult);
        }
        //Culture//
        protected void rabtn_growth_CheckedChanged(object sender, EventArgs e)
        {
            lbl_growthsttus.Text = "Organism yeilded";
            idg.Visible = true;
        }
        protected void rabtn_nogrowth_CheckedChanged(object sender, EventArgs e)
        {
            lbl_growthsttus.Text = "Result";
            idg.Visible = false;
        }
        protected void Gv_antibioticlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            List<LookupItem> SensitivityList = Session["SensitivityList"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["SensitivityList"];
            foreach (GridViewRow row in Gv_antibioticlist.Rows)
            {
                try
                {
                    DropDownList ddl1 = (DropDownList)Gv_antibioticlist.Rows[row.RowIndex].Cells[2].FindControl("ddl_sensitivity");
                    Label ID = (Label)Gv_antibioticlist.Rows[row.RowIndex].Cells[2].FindControl("lbl_sensitivityID");
                    Commonfunction.PopulateDdl(ddl1, SensitivityList);
                    if (ID.Text != "0")
                    {
                        ddl1.Items.FindByValue(ID.Text).Selected = true;
                    }
                }
                catch (Exception ex)
                {
                    PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                    LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                }
            }


        }
        protected void btnprint3_Click(object sender, EventArgs e)
        {
            if (ddl_template3.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Labtemplate", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            string template = ddl_template3.SelectedItem.Text.Substring(ddl_template3.SelectedItem.Text.LastIndexOf(':') + 1);
            string param = "option=CultureReport&Inv=" + txt_inv3.Text + "&UHID=" + txt_uhid3.Text + "&TestID=" + hdntestID3.Value + "&showheader=" + "0" + "&Template=" + template + "&Type=" + "2";

            Commonfunction common = new Commonfunction();
            string ecryptstring = common.Encrypt(param);

            string baseurl = "../MedLab/Report/ReportViewer.aspx?ID=" + ecryptstring;
            string fullURL = "window.open('" + baseurl + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);

        }
        protected void btnresets3_Click(object sender, EventArgs e)
        {
            Gv_antibioticlist.DataSource = null;
            Gv_antibioticlist.DataBind();
            txt_patientname3.Text = "";
            txt_address3.Text = "";
            txt_uhid3.Text = "";
            txt_inv3.Text = "";
            txt_patientnumber.Text = "";
            txt_referal3.Text = "";
            txt_testname3.Text = "";
            txt_requested3.Text = "";
            hdntestID3.Value = null;
            txt_remark3.Text = "";
            ddl_verified3.SelectedIndex = 0;
            lblmessage3.Visible = false;
            div5.Visible = false;
            tabcontainerSampleCollection.ActiveTabIndex = 0;
            lblmessage1.Visible = false;
        }
        protected void ddl_show_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindgrid(1);
            GVreset();
        }
        protected void ddl_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindgrid(1);
            GVreset();
        }
        protected void ConvertReportToImage(string template, int type, int TestID, Int64 UHID, string invno)
        {
            DataTable dt3 = new DataTable();
            if (type == 1)
            {
                crystalReport.Load(Server.MapPath("~/MedLab/Report/Commontemplate.rpt"));
            }
            else
            {
                crystalReport.Load(Server.MapPath("~/MedLab/Report/Growth.rpt"));
            }
           
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (type == 1)
                        {
                            cmd.CommandText = "usp_MDQ_Print_Mult_Reports_RPT";
                        }
                        else
                        {
                            cmd.CommandText = "usp_MDQ_Print_CultureLab_Reports_RPT";
                        }
                        cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                        cmd.Parameters.Add("@Investigationumber", SqlDbType.VarChar).Value = invno;
                        cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = UHID;
                        cmd.Parameters.Add("@TestID", SqlDbType.Int).Value = TestID;
                        cmd.Parameters.Add("@IsShowHF", SqlDbType.Int).Value = 1;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt3);
                    }
                }
            }
            // bindsubroot();
            crystalReport.SetDataSource(dt3);
            MediReportViewer.ReportSource = crystalReport;
            SaveConvertLabReports(crystalReport, UHID, invno, TestID);
        }
        protected void SaveConvertLabReports(ReportDocument crystalReport, Int64 UHID, string InvNo, int TestID)
        {
            DataTable dt4 = new DataTable();
            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, @"C:\OnlineReport\Report.pdf");
            byte[] pdfBytes = File.ReadAllBytes(@"C:\OnlineReport\Report.pdf");

            string pdfBase64 = Convert.ToBase64String(pdfBytes);
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_MDQ_Update_LabReport_Image";
                        cmd.Parameters.Add("@ReportImage", SqlDbType.VarChar).Value = pdfBase64;
                        cmd.Parameters.Add("@InvNo", SqlDbType.VarChar).Value = InvNo;
                        cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = UHID;
                        cmd.Parameters.Add("@TestID", SqlDbType.Int).Value = TestID;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt4);
                    }
                }
            }
        }
    }
}