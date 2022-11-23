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

namespace Mediqura.Web.MedReport
{
    public partial class InvestigationReport : BasePage
    {
        string ID1 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIpnoEmrgNo(string prefixText, int count, string contextKey)
        {
            InvDashboardMasterData Objpaic = new InvDashboardMasterData();
            InvDashboardMasterBO objInfoBO = new InvDashboardMasterBO();
            List<InvDashboardMasterData> getResult = new List<InvDashboardMasterData>();
            Objpaic.IPNo = prefixText;
           getResult = objInfoBO.getIPNoNemrgNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPNo.ToString());
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
            //Objpaic.ServNo = contextKey;
            Objpaic.UHID =Convert.ToInt64(contextKey);
            getResult = objInfoBO.GetInvNoWithContext(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].InvNo.ToString());
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
            bindgrid(1);
        }
        private void bindgrid(int page)
        {
            try
            {

                List<LadReportCollectionData> lstemp = getTestList(page);
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
        private List<LadReportCollectionData> getTestList(int p)
        {
            string uhid;
            LadReportCollectionData objData = new LadReportCollectionData();
            LabReportCollectionBO objBO = new LabReportCollectionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);

            objData.IpNo = txt_IpnoEmrgno.Text.Trim() == "" ? "" : txt_IpnoEmrgno.Text.Trim();

            var source1 = txt_IpnoEmrgno.Text.ToString();
            if (source1.Contains(":"))
            {
                uhid = source1.Substring(source1.LastIndexOf(':') + 1);
                objData.UHID = Convert.ToInt64(uhid);

            }
            else
            {
                objData.UHID = 0;
            }

            objData.InVnumber = txt_InvNumber.Text.Trim() == "" ? "" : txt_InvNumber.Text.Trim();
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtDateto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtDateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objData.DateFrom = from;
            objData.DateTo = To;
           
            return objBO.GetPatientListReport(objData);
        }
        protected void GvPatientList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Testname = (Label)e.Row.FindControl("lblTestName");
                Label Urgency = (Label)e.Row.FindControl("lblurgency");
                Label lblCollectionStatus = (Label)e.Row.FindControl("lblCollectionStatus");
                Label lblDeliveryStatus = (Label)e.Row.FindControl("lbldeliverystatus");
                LinkButton lnkprint = (LinkButton)e.Row.FindControl("lnkprint");
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
                if (Convert.ToInt16(lblDeliveryStatus.Text) == 1)
                {
                    lnkprint.Visible = false;
                }
                else
                {

                    lnkprint.Visible = true;
                }


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
           
            txtdatefrom.Text = "";
            txtDateto.Text = "";
            txt_IpnoEmrgno.Text = "";
            txt_InvNumber.Text = "";
            //bindgrid(1);
        }
        protected void GvPatientList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }

        protected void txt_IpnoEmrgno_TextChanged(object sender, EventArgs e)
        {
            var source1 = txt_IpnoEmrgno.Text.ToString();
            if (source1.Contains(":"))
            {
                ID1 = source1.Substring(source1.LastIndexOf(':') + 1);

            }
            AutoCompleteExtender2.ContextKey = ID1.Trim() == "" ? "" : ID1.Trim();
        }
    }
}