using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedBillData;
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
using Mediqura.CommonData.MedLab;
using Mediqura.BOL.MedLabBO;
using System.Drawing;
using System.Web.UI.HtmlControls;

namespace Mediqura.Web.MedLab
{
    public partial class RadiologyDashboard : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                bindgrid();
                ddlbind();

            }
        }

        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_patient_type, mstlookup.GetLookupsList(LookupName.PatientCatagory));
            Commonfunction.PopulateDdl(ddl_testcenter, mstlookup.GetLookupsList(LookupName.TestCenter));
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetInvNo(string prefixText, int count, string contextKey)
        {
            InvDashboardMasterData Objpaic = new InvDashboardMasterData();
            InvDashboardMasterBO objInfoBO = new InvDashboardMasterBO();
            List<InvDashboardMasterData> getResult = new List<InvDashboardMasterData>();
            Objpaic.InvNo = prefixText;
            getResult = objInfoBO.GetInvNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].InvNo.ToString());
            }
            return list;
        }
        protected void txtUHID_TextChanged(object sender, EventArgs e)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.UHID = Convert.ToInt64(txt_patientName.Text.Trim() == "" ? "0" : txt_patientName.Text.Trim());
            getResult = objInfoBO.GetPatientDetailsByUHID(Objpaic);
            if (getResult.Count > 0)
            {
                txt_patientName.Text = getResult[0].PatientName.ToString();
            }
            else
            {
                txt_patientName.Text = "";
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPatientName(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetLabServices(string prefixText, int count, string contextKey)
        {
            LabServiceMasterData Objpaic = new LabServiceMasterData();
            LabServiceMasterBO objInfoBO = new LabServiceMasterBO();
            List<LabServiceMasterData> getResult = new List<LabServiceMasterData>();
            Objpaic.TestName = prefixText;
            getResult = objInfoBO.GetRadioTestName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].TestName.ToString());
            }
            return list;
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            bindgrid();
        }
        private void bindgrid()
        {
            try
            {

                List<InvDashboardMasterData> lstemp = GetRadioDetails(0);

                if (lstemp.Count > 0)
                {
                    GvInvestigation.DataSource = lstemp;
                    GvInvestigation.DataBind();
                    GvInvestigation.Visible = true;

                }
                else
                {
                    GvInvestigation.DataSource = null;
                    GvInvestigation.DataBind();
                    GvInvestigation.Visible = true;

                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
        }
        private List<InvDashboardMasterData> GetRadioDetails(int p)
        {
            InvDashboardMasterData objItemMasterData = new InvDashboardMasterData();
            InvDashboardMasterBO objitemMasterBO = new InvDashboardMasterBO();
            objItemMasterData.PatientType = Convert.ToInt32(ddl_patient_type.SelectedValue == "" ? "0" : ddl_patient_type.SelectedValue);
            objItemMasterData.UHID = Convert.ToInt64(txt_patientName.Text.Contains(":") ? txt_patientName.Text.Substring(txt_patientName.Text.LastIndexOf(':') + 1) : "0");
            objItemMasterData.InvNo = txt_invno.Text.Trim() == "" ? "" : txt_invno.Text.Trim();
            var source = txt_testname.Text.Trim();
            if (source.Contains(":"))
            {
                string ID1 = source.Substring(source.LastIndexOf(':') + 1);
                objItemMasterData.TestID = Convert.ToInt32(ID1);
            }
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            string datefrom = from.ToString("yyyy-MM-dd");
            string dateto = To.ToString("yyyy-MM-dd");
            from = Convert.ToDateTime(datefrom + " " + "12:01:00 AM");
            To = Convert.ToDateTime(dateto + " " + "11:59:00 PM");
            objItemMasterData.DateFrom = from;
            objItemMasterData.DateTo = To;
            objItemMasterData.TestCenterID = Convert.ToInt32(ddl_testcenter.SelectedValue == "" ? "0" : ddl_testcenter.SelectedValue);

            return objitemMasterBO.GetRadioDetails(objItemMasterData);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetAutoUHID(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetDetailUHID(Objpaic);

            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].Patientshortdetail.ToString());
            }
            return list;
        }
        protected void GvInvestigation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvInvestigation.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        protected void GvInvestigation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label outsourcevalue = (Label)e.Row.FindControl("lbl_outsource");
                    Label outsource = (Label)e.Row.FindControl("lbl_outsourced");
                    Label samplecollected = (Label)e.Row.FindControl("lbl_samplecollected");
                    Label sample = (Label)e.Row.FindControl("lbl_sample");
                    Label sample_send = (Label)e.Row.FindControl("lbl_samplesend");
                    Label outsend = (Label)e.Row.FindControl("lbl_outsend");
                    Label reportreceived = (Label)e.Row.FindControl("lbl_reportreceived");
                    Label reportrev = (Label)e.Row.FindControl("lbl_reportrec");
                    //Label deviceinitiated = (Label)e.Row.FindControl("lbl_deviceinitiated");
                    //Label device = (Label)e.Row.FindControl("lbl_device");
                    Label reportgenerated = (Label)e.Row.FindControl("lbl_reportgenerated");
                    Label report = (Label)e.Row.FindControl("lbl_report");
                    Label verified = (Label)e.Row.FindControl("lbl_verified");
                    Label verify = (Label)e.Row.FindControl("lbl_verify");
                    Label delivered = (Label)e.Row.FindControl("lbl_reportdelivered");
                    Label deliv = (Label)e.Row.FindControl("lbl_delivered");
                    //Label initon = (Label)e.Row.FindControl("lbl_InitiatedOn");
                    Label groupid = (Label)e.Row.FindControl("lblLabGroup");
                    Label urgency = (Label)e.Row.FindControl("lbl_urgencyid");
                    Label Invnymber = (Label)e.Row.FindControl("Label2");

                    LinkButton lnkview = (LinkButton)e.Row.FindControl("lnkview");

                    if (urgency.Text == "0" || urgency.Text == "1")
                    {
                        e.Row.Cells[2].BackColor = System.Drawing.Color.Green;
                        Invnymber.ForeColor = System.Drawing.Color.White;
                    }
                    if (urgency.Text == "2")
                    {
                        e.Row.Cells[2].BackColor = System.Drawing.Color.Yellow;
                        Invnymber.ForeColor = System.Drawing.Color.Black;
                    }
                    if (urgency.Text == "3")
                    {
                        e.Row.Cells[2].BackColor = System.Drawing.Color.Red;
                        Invnymber.ForeColor = System.Drawing.Color.Black;
                    }

                    if (outsourcevalue.Text == "0")
                    {
                        outsource.Text = "-";
                        outsource.ForeColor = Color.Red;
                        outsource.Font.Bold = true;
                        outsend.Text = "-";
                        outsend.ForeColor = Color.Red;
                        outsend.Font.Bold = true;
                        reportrev.Text = "-";
                        reportrev.ForeColor = Color.Red;
                        reportrev.Font.Bold = true;
                    }
                    else
                    {
                        outsource.Text = "<i class='fa fa-check'></i>";
                        outsource.ForeColor = Color.Green;
                        outsource.Font.Bold = true;
                        if (sample_send.Text == "0")
                        {
                            outsend.Text = "<i class='fa fa-close'></i>";
                            outsend.ForeColor = Color.Red;
                            outsend.Font.Bold = true;
                        }
                        else
                        {
                            outsend.Text = "<i class='fa fa-check'></i>";
                            outsend.ForeColor = Color.Green;
                            outsend.Font.Bold = true;
                        }
                        if (reportreceived.Text == "0")
                        {
                            reportrev.Text = "<i class='fa fa-close'></i>";
                            reportrev.ForeColor = Color.Red;
                            reportrev.Font.Bold = true;
                        }
                        else
                        {
                            reportrev.Text = "<i class='fa fa-check'></i>";
                            reportrev.ForeColor = Color.Green;
                            reportrev.Font.Bold = true;
                        }
                        if (delivered.Text == "0")
                        {
                            deliv.Text = "<i class='fa fa-close'></i>";
                            deliv.ForeColor = Color.Red;
                            deliv.Font.Bold = true;
                        }
                        else
                        {
                            deliv.Text = "<i class='fa fa-check'></i>";
                            deliv.ForeColor = Color.Green;
                            deliv.Font.Bold = true;
                        }
                    }
                    if (samplecollected.Text == "0")
                    {
                        sample.Text = "-";
                        sample.ForeColor = Color.Red;
                        sample.Font.Bold = true;
                        //device.Text = "-";
                        //device.ForeColor = Color.Red;
                        //device.Font.Bold = true;
                        report.Text = "-";
                        report.ForeColor = Color.Red;
                        report.Font.Bold = true;
                        verify.Text = "-";
                        verify.ForeColor = Color.Red;
                        verify.Font.Bold = true;
                        deliv.Text = "-";
                        deliv.ForeColor = Color.Red;
                        deliv.Font.Bold = true;
                    }
                    else
                    {
                        sample.Text = "<i class='fa fa-check'></i>";
                        sample.ForeColor = Color.Green;
                        sample.Font.Bold = true;
                        //if (deviceinitiated.Text == "0")
                        //{
                        //    device.Text = "<i class='fa fa-close'></i>";
                        //    device.ForeColor = Color.Red;
                        //    device.Font.Bold = true;
                        //}
                        //else
                        //{
                        //    device.Text = "<i class='fa fa-check'></i>";
                        //    device.ForeColor = Color.Green;
                        //    device.Font.Bold = true;
                        //    for (int i = 0; i < GvInvestigation.Columns.Count; i++)
                        //    {
                        //        e.Row.Cells[i].ToolTip = initon.Text;
                        //    }
                        //}
                        if (verified.Text == "0")
                        {
                            verify.Text = "<i class='fa fa-close'></i>";
                            verify.ForeColor = Color.Red;
                            verify.Font.Bold = true;
                        }
                        else
                        {
                            verify.Text = "<i class='fa fa-check'></i>";
                            verify.ForeColor = Color.Green;
                            verify.Font.Bold = true;
                        }
                        if (reportgenerated.Text == "0")
                        {
                            report.Text = "<i class='fa fa-close'></i>";
                            report.ForeColor = Color.Red;
                            report.Font.Bold = true;
                        }
                        else
                        {
                            report.Text = "<i class='fa fa-check'></i>";
                            report.ForeColor = Color.Green;
                            report.Font.Bold = true;
                            lnkview.Visible = true;

                        }
                        if (delivered.Text == "0")
                        {
                            deliv.Text = "<i class='fa fa-close'></i>";
                            deliv.ForeColor = Color.Red;
                            deliv.Font.Bold = true;
                        }
                        else
                        {
                            deliv.Text = "<i class='fa fa-check'></i>";
                            deliv.ForeColor = Color.Green;
                            deliv.Font.Bold = true;
                        }
                    }
                    if (groupid.Text == "2" || groupid.Text == "4")
                    {

                        if (verified.Text == "0")
                        {
                            verify.Text = "<i class='fa fa-close'></i>";
                            verify.ForeColor = Color.Red;
                            verify.Font.Bold = true;
                        }
                        else
                        {
                            verify.Text = "<i class='fa fa-check'></i>";
                            verify.ForeColor = Color.Green;
                            verify.Font.Bold = true;
                        }
                        if (reportgenerated.Text == "0")
                        {
                            report.Text = "<i class='fa fa-close'></i>";
                            report.ForeColor = Color.Red;
                            report.Font.Bold = true;
                        }
                        else
                        {
                            report.Text = "<i class='fa fa-check'></i>";
                            report.ForeColor = Color.Green;
                            report.Font.Bold = true;
                            lnkview.Visible = true;
                        }
                        if (delivered.Text == "0")
                        {
                            deliv.Text = "<i class='fa fa-close'></i>";
                            deliv.ForeColor = Color.Red;
                            deliv.Font.Bold = true;
                        }
                        else
                        {
                            deliv.Text = "<i class='fa fa-check'></i>";
                            deliv.ForeColor = Color.Green;
                            deliv.Font.Bold = true;
                        }

                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            clear_all();
        }
        private void clear_all()
        {
            lblmessage.Visible = false;
            txt_testname.Text = "";
            txt_patientName.Text = "";
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            GvInvestigation.DataSource = null;
            GvInvestigation.DataBind();
            GvInvestigation.Visible = false;
            txt_invno.Text = "";
            bindgrid();
            ddl_patient_type.SelectedIndex = 0;
        }
        protected void GvInvestigation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "View")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvInvestigation.Rows[i];
                    Label labgroup = (Label)gr.Cells[0].FindControl("lblLabGroup");
                    Label headerID = (Label)gr.Cells[0].FindControl("lblHeaderID");
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_id");
                    Label BillID = (Label)gr.Cells[0].FindControl("lbl_BillID");
                    Label InvNo = (Label)gr.Cells[0].FindControl("Label2");
                    Label UHID = (Label)gr.Cells[0].FindControl("lblUHID");
                    Label TestID = (Label)gr.Cells[0].FindControl("lblTestID");
                    Label Template = (Label)gr.Cells[0].FindControl("lbltemplate");
                    string template = Template.Text;
                    if (labgroup.Text == "2" || labgroup.Text == "4")
                    {
                        string url = "../MedLab/RadioReportSample.aspx?id=" + Convert.ToInt64(ID.Text) + "&p=" + Convert.ToInt64(BillID.Text) + "&GroupID=" + Convert.ToInt32(labgroup.Text) + "&HeaderID=" + Convert.ToInt32(headerID.Text);
                        string fullURL = "window.open('" + url + "', '_blank');";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                    }
                    else
                    {
                        string url = "../MedLab/Report/ReportViewer.aspx?option=ReportTemplate&Inv=" + InvNo.Text + "&UHID=" + UHID.Text + "&TestID=" + TestID.Text + "&Template=" + template;
                        string fullURL = "window.open('" + url + "', '_blank');";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);

                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public string getTemplate()
        {
            return txtreportTemp.Value;
        }
    }
}