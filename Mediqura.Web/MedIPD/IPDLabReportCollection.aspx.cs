using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedLabBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedLab;
using Mediqura.CommonData.MedUtilityData;
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
namespace Mediqura.Web.MedIPD
{
    public partial class IPDLabReportCollection : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
                ddlbind();
                checkSelect();
            }
        }
        public void checkSelect()
        {
            if (ddl_labgroup.SelectedIndex == 0)
            {
                ddl_labsubgroup.Attributes["disabled"] = "disabled";
            }
            else
            {
                ddl_labsubgroup.Attributes.Remove("disabled");
            }
            if (ddl_labsubgroup.SelectedIndex == 0)
            {
                ddl_labTestName.Attributes["disabled"] = "disabled";
            }
            else
            {
                ddl_labTestName.Attributes.Remove("disabled");
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_patient_type, mstlookup.GetLookupsList(LookupName.PatientCatagory));
            Commonfunction.PopulateDdl(ddl_labgroup, mstlookup.GetLookupsList(LookupName.LabGroups));
            Commonfunction.PopulateDdl(ddl_labsubgroup, mstlookup.GetSubGroupByGroupID(0));
            Commonfunction.PopulateDdl(ddl_labTestName, mstlookup.GetTestNameBySubGroupID(0));
            ddl_patient_type.SelectedIndex = 2;
            ddl_patient_type.Attributes["disabled"] = "disabled";
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtDateto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetLabPatientName(string prefixText, int count, string contextKey)
        {
            SampleCollectionData Objpaic = new SampleCollectionData();
            LabSampleCollctionBO objInfoBO = new LabSampleCollctionBO();
            List<SampleCollectionData> getResult = new List<SampleCollectionData>();
            Objpaic.PatientName = prefixText;
            Objpaic.PatientTypeID = Convert.ToInt32(2);
            getResult = objInfoBO.GetLabPatientNames(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
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
            Objpaic.PatientTypeID = Convert.ToInt32(2);
            getResult = objInfoBO.GetLabInvestigationForIP(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].Investigationumber.ToString());
            }
            return list;
        }
        protected void ddl_patient_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_patient_type.SelectedIndex > 0)
            {
                AutoCompleteExtender1.ContextKey = ddl_patient_type.SelectedValue == "" ? "0" : ddl_patient_type.SelectedValue;
            }
            else
            {
                AutoCompleteExtender1.ContextKey = null;
            }
        }
        protected void ddl_labgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_labgroup.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_labsubgroup, mstlookup.GetSubGroupByGroupID(Convert.ToInt32(ddl_labgroup.SelectedValue)));
                checkSelect();
            }
        }
        protected void ddl_labsubgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_labsubgroup.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_labTestName, mstlookup.GetTestNameBySubGroupID(Convert.ToInt32(ddl_labsubgroup.SelectedValue)));
                checkSelect();
            }
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
            if (ddl_patient_type.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Please select patient type.", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddl_labgroup.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Please select lab group.", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
            {
                Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                txtdatefrom.Text = "";
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                txtdatefrom.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (Commonfunction.isValidDate(txtDateto.Text) == false)
            {
                Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                txtDateto.Text = "";
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                txtDateto.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            } bindgrid(1);
        }
        private void bindgrid(int page)
        {
            try
            {

                List<LadReportCollectionData> lstemp = getRadioTestList(page);
                if (lstemp.Count > 0)
                {
                    GvPatientList.VirtualItemCount = lstemp[0].MaximumRows;//total item is required for custom paging
                    GvPatientList.PageIndex = page - 1;
                    GvPatientList.DataSource = lstemp;
                    GvPatientList.DataBind();
                    GvPatientList.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                }
                else
                {
                    GvPatientList.DataSource = null;
                    GvPatientList.DataBind();
                    GvPatientList.Visible = true;
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        private List<LadReportCollectionData> getRadioTestList(int p)
        {
            LadReportCollectionData objData = new LadReportCollectionData();
            LabReportCollectionBO objBO = new LabReportCollectionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objData.PatientType = Convert.ToInt32(ddl_patient_type.SelectedValue == "0" ? null : ddl_patient_type.SelectedValue);
            objData.LabGrpID = Convert.ToInt32(ddl_labgroup.SelectedValue == "0" ? null : ddl_labgroup.SelectedValue);
            objData.LabSubGrpID = Convert.ToInt32(ddl_labsubgroup.SelectedValue == "0" ? null : ddl_labsubgroup.SelectedValue);
            objData.TestID = Convert.ToInt32(ddl_labTestName.SelectedValue == "0" ? null : ddl_labTestName.SelectedValue);
            if (txt_patientName.Text != "")
            {
                string UHID;
                var source = txt_patientName.Text.ToString();
                if (source.Contains(":"))
                {
                    UHID = source.Substring(source.LastIndexOf(':') + 1);
                    objData.UHID = Convert.ToInt64(UHID.ToString());
                }

                else
                {
                    objData.UHID = Convert.ToInt64(txt_patientName.Text.Trim() == "" ? "0" : txt_patientName.Text.Trim());
                }
            }
            else
            {
                objData.UHID = Convert.ToInt64(txt_patientName.Text.Trim() == "" ? "0" : txt_patientName.Text.Trim());
            }
            objData.InVnumber = txt_InvNumber.Text.Trim() == "" ? "" : txt_InvNumber.Text.Trim();
            objData.TestStatus = Convert.ToInt32(ddlstatus.SelectedValue == "0" ? null : ddlstatus.SelectedValue);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtDateto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtDateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objData.DateFrom = from;
            objData.DateTo = To;
            objData.CurrentIndex = p;
            return objBO.GetPatientTestList(objData);
        }
        protected void GvPatientList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Testname = (Label)e.Row.FindControl("lblTestName");
                Label Urgency = (Label)e.Row.FindControl("lblurgency");
                Label lblCollectionStatus = (Label)e.Row.FindControl("lblCollectionStatus");
                Label lblDeliveryStatus = (Label)e.Row.FindControl("lbldeliverystatus");
                //LinkButton lnkprint = (LinkButton)e.Row.FindControl("lnkprint");
                if (Urgency.Text == "3")
                {
                    Testname.CssClass = "border-left-red";
                }
                else if (Urgency.Text == "2")
                {
                    Testname.CssClass = "border-left-yellow";
                }
                else
                {
                    Testname.CssClass = "border-left-green";
                }
                //if (Convert.ToInt16(lblDeliveryStatus.Text) == 1)
                //{
                //    lnkprint.Visible = false;
                //}
                //else
                //{

                //    lnkprint.Visible = true;
                //}


            }
        }
        protected void GvPatientList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Print")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvPatientList.Rows[i];
                    Label labgroup = (Label)gr.Cells[0].FindControl("lblLabGroup");
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_id");
                    Label InvID = (Label)gr.Cells[0].FindControl("lblID");
                    Label BillID = (Label)gr.Cells[0].FindControl("lbl_BillID");
                    Label InvNo = (Label)gr.Cells[0].FindControl("lbl_invnumber");
                    Label UHID = (Label)gr.Cells[0].FindControl("lblUHID");
                    Label TestID = (Label)gr.Cells[0].FindControl("lblTestID");
                    Label Template = (Label)gr.Cells[0].FindControl("lbltemplate");
                    Label reporttypeid = (Label)gr.Cells[0].FindControl("lblreporttypeid");
                    Label isdelivered = (Label)gr.Cells[0].FindControl("lbldeliverystatus");
                    string template = Template.Text;
                    if (Convert.ToInt16(isdelivered.Text) != 1)
                    {
                        if (labgroup.Text == "2" || labgroup.Text == "4")
                        {
                            string url = "../MedLab/LabReportCollectionSample.aspx?id=" + Convert.ToInt64(ID.Text) + "&p=" + Convert.ToInt64(BillID.Text);
                            string fullURL = "window.open('" + url + "', '_blank');";
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                        }
                        else if (labgroup.Text == "1")
                        {
                            if (reporttypeid.Text == "2")
                            {
                                string url = "../MedLab/LabCommentPrint.aspx?id=" + Convert.ToInt64(TestID.Text) + "&p=" + InvNo.Text;
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
                    else
                    {

                        if (labgroup.Text == "2" || labgroup.Text == "4")
                        {
                            string url = "../MedLab/LabReportCollectionSample.aspx?id=" + Convert.ToInt64(ID.Text) + "&p=" + Convert.ToInt64(BillID.Text);
                            string fullURL = "window.open('" + url + "', '_blank');";
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                        }
                        else if (labgroup.Text == "1")
                        {
                            if (reporttypeid.Text == "2")
                            {
                                string url = "../MedLab/LabCommentPrint.aspx?id=" + Convert.ToInt64(TestID.Text) + "&p=" + InvNo.Text;
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

                    LadReportCollectionData objData = new LadReportCollectionData();
                    LabReportCollectionBO objBO = new LabReportCollectionBO();
                    objData.DeliveryStatus = Convert.ToInt32(hdnValue.Value);
                    objData.billID = Convert.ToInt64(BillID.Text == "" ? "0" : BillID.Text);
                    objData.EmployeeID = LogData.EmployeeID;
                    objData.ID = Convert.ToInt64(InvID.Text == "" ? "0" : InvID.Text);
                    int result = objBO.UpdateRadioReportVerification(objData);
                }
                if (e.CommandName == "View")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvPatientList.Rows[i];
                    Label labgroup = (Label)gr.Cells[0].FindControl("lblLabGroup");
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_id");
                    Label InvID = (Label)gr.Cells[0].FindControl("lblID");
                    Label BillID = (Label)gr.Cells[0].FindControl("lbl_BillID");
                    Label InvNo = (Label)gr.Cells[0].FindControl("lbl_invnumber");
                    Label UHID = (Label)gr.Cells[0].FindControl("lblUHID");
                    Label TestID = (Label)gr.Cells[0].FindControl("lblTestID");
                    Label Template = (Label)gr.Cells[0].FindControl("lbltemplate");
                    Label reporttypeid = (Label)gr.Cells[0].FindControl("lblreporttypeid");
                    Label isdelivered = (Label)gr.Cells[0].FindControl("lbldeliverystatus");
                    string template = Template.Text;
                    if (Convert.ToInt16(isdelivered.Text) != 1)
                    {
                        if (labgroup.Text == "2" || labgroup.Text == "4")
                        {
                            string url = "../MedLab/LabReportCollectionSample.aspx?id=" + Convert.ToInt64(ID.Text) + "&p=" + Convert.ToInt64(BillID.Text);
                            string fullURL = "window.open('" + url + "', '_blank');";
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                        }
                        else if (labgroup.Text == "1")
                        {
                            if (reporttypeid.Text == "2")
                            {
                                string url = "../MedLab/LabCommentPrint.aspx?id=" + Convert.ToInt64(TestID.Text) + "&p=" + InvNo.Text;
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
                    else
                    {

                        if (labgroup.Text == "2" || labgroup.Text == "4")
                        {
                            string url = "../MedLab/LabReportCollectionSample.aspx?id=" + Convert.ToInt64(ID.Text) + "&p=" + Convert.ToInt64(BillID.Text);
                            string fullURL = "window.open('" + url + "', '_blank');";
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                        }
                        else if (labgroup.Text == "1")
                        {
                            if (reporttypeid.Text == "2")
                            {
                                string url = "../MedLab/LabCommentPrint.aspx?id=" + Convert.ToInt64(TestID.Text) + "&p=" + InvNo.Text;
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
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ddl_labsubgroup.SelectedIndex = 0;
            ddl_labTestName.SelectedIndex = 0;
            txt_patientName.Text = "";
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtDateto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            ddlstatus.SelectedIndex = 0;
            ddl_labgroup.SelectedIndex = 1;
            ddl_patient_type.SelectedIndex = 2;
            ddl_patient_type.Attributes["disabled"] = "disabled";
            txt_InvNumber.Text = "";
            ddl_labgroup.SelectedIndex = 0;
            bindgrid(1);
        }
        protected void GvPatientList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }
    }
}