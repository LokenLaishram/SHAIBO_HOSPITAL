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
using System.Net;
using System.Net.Mail;
using System.Text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Diagnostics;
using ClosedXML.Excel;
using System.Net.NetworkInformation;

namespace Mediqura.Web.MedLab
{
    public partial class LabReportCollection : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
                ddlbind();
                checkSelect();
                bindgrid(1);               
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
            Commonfunction.PopulateDdl(ddl_centre, mstlookup.GetLookupsList(LookupName.RunnerList));
            Commonfunction.PopulateDdl(ddl_labsubgroup, mstlookup.GetSubGroupByGroupID(0));
            Commonfunction.PopulateDdl(ddl_labTestName, mstlookup.GetTestNameBySubGroupID(0));
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtDateto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            AutoCompleteExtender1.ContextKey = "0";
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetLabPatientName(string prefixText, int count, string contextKey)
        {
            SampleCollectionData Objpaic = new SampleCollectionData();
            LabSampleCollctionBO objInfoBO = new LabSampleCollctionBO();
            List<SampleCollectionData> getResult = new List<SampleCollectionData>();
            Objpaic.PatientName = prefixText;
            Objpaic.PatientTypeID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetLabPatientNames(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
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
        protected void ddl_patient_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_patient_type.SelectedIndex > 0)
            {
                AutoCompleteExtender1.ContextKey = ddl_patient_type.SelectedValue == "" ? "0" : ddl_patient_type.SelectedValue;
            }
            else
            {
                AutoCompleteExtender1.ContextKey = "0";
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

            //if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
            //{
            //    Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
            //    txtdatefrom.Text = "";
            //    divmsg1.Attributes["class"] = "FailAlert";
            //    divmsg1.Visible = true;
            //    txtdatefrom.Focus();
            //    return;
            //}
            //else
            //{
            //    lblmessage.Visible = false;
            //}
            //if (Commonfunction.isValidDate(txtDateto.Text) == false)
            //{
            //    Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
            //    txtDateto.Text = "";
            //    divmsg1.Attributes["class"] = "FailAlert";
            //    divmsg1.Visible = true;
            //    txtDateto.Focus();
            //    return;
            //}
            //else
            //{
            //    lblmessage.Visible = false;
            //}
            GVreset();
            bindgrid(1);
        }
        private void bindgrid(int page)
        {
            try
            {
                GV_PatientList.PageSize = Convert.ToInt32(ddl_show.SelectedValue == "10000" ? lbl_totalrecords.Text : ddl_show.SelectedValue);
                List<LadReportCollectionData> lstemp = GetLabTestPatientList(page);
                if (lstemp.Count > 0)
                {
                    GV_PatientList.VirtualItemCount = lstemp[0].MaximumRows;//total item is required for custom paging
                    GV_PatientList.PageIndex = page - 1;
                    GV_PatientList.DataSource = lstemp;
                    GV_PatientList.DataBind();
                    GV_PatientList.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    lbl_totalrecords.Text = lstemp[0].MaximumRows.ToString();
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";

                }
                else
                {
                    GV_PatientList.DataSource = null;
                    GV_PatientList.DataBind();
                    GV_PatientList.Visible = true;
                    lblresult.Visible = false;
                }
                lblmessage.Visible = false;
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        private List<LadReportCollectionData> GetLabTestPatientList(int p)
        {
            LadReportCollectionData objData = new LadReportCollectionData();
            LabReportCollectionBO objBO = new LabReportCollectionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            bool isnumeric = txt_patientName.Text.All(char.IsDigit);
            if (isnumeric == false)
            {
                if (txt_patientName.Text.Contains(":"))
                {
                    bool isUHIDnumeric = txt_patientName.Text.Substring(txt_patientName.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                    objData.UHID = isUHIDnumeric ? Convert.ToInt64(txt_patientName.Text.Contains(":") ? txt_patientName.Text.Substring(txt_patientName.Text.LastIndexOf(':') + 1) : "0") : 0;
                }
                else
                {
                    objData.UHID = 0;
                }
            }
            else
            {
                objData.UHID = 0;
            }

            objData.InVnumber = txt_InvNumber.Text.Trim() == "" ? "" : txt_InvNumber.Text.Trim();
            objData.TestStatus = Convert.ToInt32(ddlstatus.SelectedValue == "0" ? null : ddlstatus.SelectedValue);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtDateto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtDateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objData.DateFrom = from;
            objData.DateTo = To;
            objData.CurrentIndex = p;
            objData.PageSize = Convert.ToInt32(ddl_show.SelectedValue == "10000" ? lbl_totalrecords.Text : ddl_show.SelectedValue);
            GV_PatientList.PageSize = Convert.ToInt32(ddl_show.SelectedValue == "10000" ? lbl_totalrecords.Text : ddl_show.SelectedValue);
            objData.RunnerID = Convert.ToInt32(ddl_centre.SelectedValue == "0" ? null : ddl_centre.SelectedValue);
            return objBO.GetTestPatientList(objData);
        }
        protected void gv_PatientTestlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Print")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GV_PatientList.Rows[i];
                    Label labgroup = (Label)gr.Cells[0].FindControl("lblLabGroup");
                    Label SubgroupID = (Label)gr.Cells[0].FindControl("lbl_subgroup");
                    Label headerID = (Label)gr.Cells[0].FindControl("lblHeaderID");
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_id");
                    Label InvID = (Label)gr.Cells[0].FindControl("lblID");
                    Label BillID = (Label)gr.Cells[0].FindControl("lbl_BillID");
                    Label InvNo = (Label)gr.Cells[0].FindControl("lbl_invnumber");
                    Label UHID = (Label)gr.Cells[0].FindControl("lblUHID");
                    Label TestID = (Label)gr.Cells[0].FindControl("lblTestID");
                    Label Template = (Label)gr.Cells[0].FindControl("lbltemplate");
                    Label TemplateID = (Label)gr.Cells[0].FindControl("lbltemplateID");
                    Label reporttypeid = (Label)gr.Cells[0].FindControl("lblreporttypeid");
                    Label isdelivered = (Label)gr.Cells[0].FindControl("lbldeliverystatus");
                    string template = TemplateID.Text.ToString();
                    //Image Generate//
                    LadReportCollectionData objresult = new LadReportCollectionData();
                    objresult.InVnumber = InvNo.Text.Trim() == "" ? null : InvNo.Text.Trim();
                    objresult.UHID = Convert.ToInt32(UHID.Text.Trim() == "" ? "0" : UHID.Text.Trim());
                    objresult.RoleID = LogData.RoleID;
                    GetTestListByInvNo(objresult);
                    //End Image Generate//
                    if (Convert.ToInt16(isdelivered.Text) != 1)
                    {
                        if (labgroup.Text == "2" || labgroup.Text == "4")
                        {
                            //string url = "../MedLab/LabReportCollectionSample.aspx?id=" + Convert.ToInt64(ID.Text) + "&p=" + Convert.ToInt64(BillID.Text) + "&GroupID=" + Convert.ToInt32(labgroup.Text) + "&HeaderID=" + Convert.ToInt32(headerID.Text);
                            //string fullURL = "window.open('" + url + "', '_blank');";
                            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                            string Invns = InvNo.Text;
                            Int64 UHIDS = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                            int TestIDS = Convert.ToInt32(TestID.Text == "" ? "0" : TestID.Text);
                            string url = "../MedRadTemplate/ReportViewer.aspx?option=RadioReport&Inv=" + Invns + "&UHID=" + UHIDS + "&TestID=" + TestIDS;
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
                                if (SubgroupID.Text == "")
                                {
                                    string param = "option=CultureReport&Inv=" + InvNo.Text + "&UHID=" + UHID.Text + "&TestID=" + TestID.Text + "&showheader=" + "0" + "&Template=" + template + "&Type=" + "2";
                                    //string param = "option=CultureReport&Inv=" + txt_inv3.Text + "&UHID=" + txt_uhid3.Text + "&TestID=" + hdntestID3.Value + "&showheader=" + "0" + "&Template=" + template + "&Type=" + "2";
                                    Commonfunction common = new Commonfunction();
                                    string ecryptstring = common.Encrypt(param);
                                    string baseurl = "../MedLab/Report/ReportViewer.aspx?ID=" + ecryptstring;
                                    string fullURL = "window.open('" + baseurl + "', '_blank');";
                                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                                }
                                else
                                {
                                    string param = "option=MultipleReport&Inv=" + InvNo.Text + "&UHID=" + UHID.Text + "&TestID=" + TestID.Text + "&showheader=" + "0" + "&Template=" + template + "&Type=" + "1";
                                    Commonfunction common = new Commonfunction();
                                    string ecryptstring = common.Encrypt(param);
                                    string baseurl = "../MedLab/Report/ReportViewer.aspx?ID=" + ecryptstring;
                                    string fullURL = "window.open('" + baseurl + "', '_blank');";
                                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                                }
                            }
                        }
                    }
                    else
                    {

                        if (labgroup.Text == "2" || labgroup.Text == "4")
                        {
                            //string url = "../MedLab/LabReportCollectionSample.aspx?id=" + Convert.ToInt64(ID.Text) + "&p=" + Convert.ToInt64(BillID.Text) + "&GroupID=" + Convert.ToInt32(labgroup.Text) + "&HeaderID=" + Convert.ToInt32(headerID.Text);
                            //string fullURL = "window.open('" + url + "', '_blank');";
                            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                            string Invns = InvNo.Text;
                            Int64 UHIDS = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                            int TestIDS = Convert.ToInt32(TestID.Text == "" ? "0" : TestID.Text);
                            string url = "../MedRadTemplate/ReportViewer.aspx?option=RadioReport&Inv=" + Invns + "&UHID=" + UHIDS + "&TestID=" + TestIDS;
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
                                if (SubgroupID.Text == "8")
                                {
                                    string param = "option=MultipleReport&Inv=" + InvNo.Text + "&UHID=" + UHID.Text + "&TestID=" + TestID.Text + "&showheader=" + "1" + "&Template=" + template + "&Type=" + "2";
                                    Commonfunction common = new Commonfunction();
                                    string ecryptstring = common.Encrypt(param);
                                    string baseurl = "../MedLab/Report/ReportViewer.aspx?ID=" + ecryptstring;
                                    string fullURL = "window.open('" + baseurl + "', '_blank');";
                                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                                }
                                else
                                {
                                    string param = "option=MultipleReport&Inv=" + InvNo.Text + "&UHID=" + UHID.Text + "&TestID=" + TestID.Text + "&showheader=" + "1" + "&Template=" + template + "&Type=" + "1";
                                    Commonfunction common = new Commonfunction();
                                    string ecryptstring = common.Encrypt(param);
                                    string baseurl = "../MedLab/Report/ReportViewer.aspx?ID=" + ecryptstring;
                                    string fullURL = "window.open('" + baseurl + "', '_blank');";
                                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                                }
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
                //-----EMAIL PRINT------//
                if (e.CommandName == "EmailPrint")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GV_PatientList.Rows[i];
                    Label labgroup = (Label)gr.Cells[0].FindControl("lblLabGroup");
                    Label SubgroupID = (Label)gr.Cells[0].FindControl("lbl_subgroup");
                    Label headerID = (Label)gr.Cells[0].FindControl("lblHeaderID");
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_id");
                    Label InvID = (Label)gr.Cells[0].FindControl("lblID");
                    Label BillID = (Label)gr.Cells[0].FindControl("lbl_BillID");
                    Label InvNo = (Label)gr.Cells[0].FindControl("lbl_invnumber");
                    Label UHID = (Label)gr.Cells[0].FindControl("lblUHID");
                    Label TestID = (Label)gr.Cells[0].FindControl("lblTestID");
                    Label Template = (Label)gr.Cells[0].FindControl("lbltemplate");
                    Label TemplateID = (Label)gr.Cells[0].FindControl("lbltemplateID");
                    Label reporttypeid = (Label)gr.Cells[0].FindControl("lblreporttypeid");
                    Label isdelivered = (Label)gr.Cells[0].FindControl("lbldeliverystatus");
                    string template = TemplateID.Text;
                    //Image Generate//
                    LadReportCollectionData objresult = new LadReportCollectionData();
                    objresult.InVnumber = InvNo.Text.Trim() == "" ? null : InvNo.Text.Trim();
                    objresult.UHID = Convert.ToInt32(UHID.Text.Trim() == "" ? "0" : UHID.Text.Trim());
                    objresult.RoleID = LogData.RoleID;
                    GetTestListByInvNo(objresult);
                    //End Image Generate//
                    if (Convert.ToInt16(isdelivered.Text) != 1)
                    {
                        if (labgroup.Text == "2" || labgroup.Text == "4")
                        {
                            //string url = "../MedLab/LabReportCollectionSample.aspx?id=" + Convert.ToInt64(ID.Text) + "&p=" + Convert.ToInt64(BillID.Text) + "&GroupID=" + Convert.ToInt32(labgroup.Text) + "&HeaderID=" + Convert.ToInt32(headerID.Text);
                            //string fullURL = "window.open('" + url + "', '_blank');";
                            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                            string Invns = InvNo.Text;
                            Int64 UHIDS = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                            int TestIDS = Convert.ToInt32(TestID.Text == "" ? "0" : TestID.Text);
                            string url = "../MedRadTemplate/ReportViewer.aspx?option=RadioReport&Inv=" + Invns + "&UHID=" + UHIDS + "&TestID=" + TestIDS;
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
                                if (SubgroupID.Text == "")
                                {
                                    string param = "option=CultureReport&Inv=" + InvNo.Text + "&UHID=" + UHID.Text + "&TestID=" + TestID.Text + "&showheader=" + "1" + "&Template=" + template + "&Type=" + "2";
                                    //string param = "option=CultureReport&Inv=" + txt_inv3.Text + "&UHID=" + txt_uhid3.Text + "&TestID=" + hdntestID3.Value + "&showheader=" + "0" + "&Template=" + template + "&Type=" + "2";
                                    Commonfunction common = new Commonfunction();
                                    string ecryptstring = common.Encrypt(param);
                                    string baseurl = "../MedLab/Report/ReportViewer.aspx?ID=" + ecryptstring;
                                    string fullURL = "window.open('" + baseurl + "', '_blank');";
                                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                                }
                                else
                                {
                                    string param = "option=MultipleReport&Inv=" + InvNo.Text + "&UHID=" + UHID.Text + "&TestID=" + TestID.Text + "&showheader=" + "1" + "&Template=" + template + "&Type=" + "1";
                                    Commonfunction common = new Commonfunction();
                                    string ecryptstring = common.Encrypt(param);
                                    string baseurl = "../MedLab/Report/ReportViewer.aspx?ID=" + ecryptstring;
                                    string fullURL = "window.open('" + baseurl + "', '_blank');";
                                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                                }
                            }
                        }
                    }
                    else
                    {

                        if (labgroup.Text == "2" || labgroup.Text == "4")
                        {
                            //string url = "../MedLab/LabReportCollectionSample.aspx?id=" + Convert.ToInt64(ID.Text) + "&p=" + Convert.ToInt64(BillID.Text) + "&GroupID=" + Convert.ToInt32(labgroup.Text) + "&HeaderID=" + Convert.ToInt32(headerID.Text);
                            //string fullURL = "window.open('" + url + "', '_blank');";
                            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                            string Invns = InvNo.Text;
                            Int64 UHIDS = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                            int TestIDS = Convert.ToInt32(TestID.Text == "" ? "0" : TestID.Text);
                            string url = "../MedRadTemplate/ReportViewer.aspx?option=RadioReport&Inv=" + Invns + "&UHID=" + UHIDS + "&TestID=" + TestIDS;
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
                                if (SubgroupID.Text == "8")
                                {
                                    string param = "option=MultipleReport&Inv=" + InvNo.Text + "&UHID=" + UHID.Text + "&TestID=" + TestID.Text + "&showheader=" + "0" + "&Template=" + template + "&Type=" + "2";
                                    Commonfunction common = new Commonfunction();
                                    string ecryptstring = common.Encrypt(param);
                                    string baseurl = "../MedLab/Report/ReportViewer.aspx?ID=" + ecryptstring;
                                    string fullURL = "window.open('" + baseurl + "', '_blank');";
                                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                                }
                                else
                                {
                                    string param = "option=MultipleReport&Inv=" + InvNo.Text + "&UHID=" + UHID.Text + "&TestID=" + TestID.Text + "&showheader=" + "0" + "&Template=" + template + "&Type=" + "1";
                                    Commonfunction common = new Commonfunction();
                                    string ecryptstring = common.Encrypt(param);
                                    string baseurl = "../MedLab/Report/ReportViewer.aspx?ID=" + ecryptstring;
                                    string fullURL = "window.open('" + baseurl + "', '_blank');";
                                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                                }
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
                    GridViewRow gr = GV_PatientList.Rows[i];
                    Label labgroup = (Label)gr.Cells[0].FindControl("lblLabGroup");
                    Label headerID = (Label)gr.Cells[0].FindControl("lblHeaderID");
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_id");
                    Label InvID = (Label)gr.Cells[0].FindControl("lblID");
                    Label BillID = (Label)gr.Cells[0].FindControl("lbl_BillID");
                    Label InvNo = (Label)gr.Cells[0].FindControl("lbl_invnumber");
                    Label UHID = (Label)gr.Cells[0].FindControl("lblUHID");
                    Label TestID = (Label)gr.Cells[0].FindControl("lblTestID");
                    Label TemplateID = (Label)gr.Cells[0].FindControl("lbltemplateID");
                    Label Template = (Label)gr.Cells[0].FindControl("lbltemplate");
                    Label reporttypeid = (Label)gr.Cells[0].FindControl("lblreporttypeid");
                    Label isdelivered = (Label)gr.Cells[0].FindControl("lbldeliverystatus");
                    string template = TemplateID.Text;

                    if (Convert.ToInt16(isdelivered.Text) != 1)
                    {
                        if (labgroup.Text == "2" || labgroup.Text == "4")
                        {
                            string url = "../MedLab/LabReportCollectionSample.aspx?id=" + Convert.ToInt64(ID.Text) + "&p=" + Convert.ToInt64(BillID.Text) + "&GroupID=" + Convert.ToInt32(labgroup.Text) + "&HeaderID=" + Convert.ToInt32(headerID.Text);
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
                            //string url = "../MedLab/LabReportCollectionSample.aspx?id=" + Convert.ToInt64(ID.Text) + "&p=" + Convert.ToInt64(BillID.Text) + "&GroupID=" + Convert.ToInt32(labgroup.Text) + "&HeaderID=" + Convert.ToInt32(headerID.Text);
                            //string fullURL = "window.open('" + url + "', '_blank');";
                            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                            string Invns = InvNo.Text;
                            Int64 UHIDS = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                            int TestIDS = Convert.ToInt32(TestID.Text == "" ? "0" : TestID.Text);
                            string url = "../MedRadTemplate/ReportViewer.aspx?option=RadioReport&Inv=" + Invns + "&UHID=" + UHIDS + "&TestID=" + TestIDS;
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
                if (e.CommandName == "GetTest")
                {
                   
                    LadReportCollectionData objresult = new LadReportCollectionData();
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GV_PatientList.Rows[i];
                    Label UHID = (Label)gr.Cells[0].FindControl("lbl_PatUHID");
                    Label InvNumber = (Label)gr.Cells[0].FindControl("lvl_LabInv");
                    objresult.InVnumber = InvNumber.Text.Trim() == "" ? null : InvNumber.Text.Trim();
                    objresult.UHID = Convert.ToInt32(UHID.Text.Trim() == "" ? "0" : UHID.Text.Trim());
                    objresult.RoleID = LogData.RoleID;
                    GetTestListByInvNo(objresult);
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
                divmsg3.Attributes["class"] = "FailAlert";
                divmsg3.Visible = true;
                return;
            }
        }
        protected void GetTestListByInvNo(LadReportCollectionData result)
        {
            hdnpath.Text = "";
            hdnuhid.Text = "";
            hdntestid.Text = "";
            txt_PatientDetails.Text = "";
            hdnemail.Text = "";
            hdninvnumber.Text = "";

            lblmessage.Visible = false;
            LabReportCollectionBO objlabBO = new LabReportCollectionBO();
            List<LadReportCollectionData> Result = objlabBO.GetPatientTestList(result);

            string Rpath = @"C:\ProcessedReport\" + Result[0].UHID;
            string Root = @"C:\ProcessedReport\" + Result[0].UHID;

            // If directory does not exist, create it. 
            if (Directory.Exists(Root))
            {
                Directory.Delete(Root, true);
                hdnpath.Text = "";
            }
            Directory.CreateDirectory(Root);
            hdnpath.Text = Root;
            hdnuhid.Text = Result[0].UHID.ToString();
            hdnemail.Text = Result[0].Email.ToString();
            hdninvnumber.Text = Result[0].InVnumber.ToString();
            for (int i = 0; i < Result.Count; i++)
            {
                if (Result[i].ReportImage != null && Result[i].isVerified == 1 && Result[i].DeliveryStatus == 0)
                {
                    byte[] data = Convert.FromBase64String(Result[i].ReportImage.ToString());
                    if (Directory.Exists(Rpath))
                    {                       
                        Rpath = Rpath + "\\" + Result[i].EmailTestName + ".pdf";
                        using (FileStream Writer = new System.IO.FileStream(Rpath, FileMode.Create, FileAccess.Write))
                        {
                            Writer.Write(data, 0, data.Length);
                        }
                        hdntestid.Text = hdntestid.Text + "," + Result[i].TestID.ToString();
                        Rpath = Root;
                    }
                    else
                    {
                        throw new System.Exception("PDF Shared Location not found");
                    }
                }
            }
            if (Result.Count > 0)
            {
                txt_PatientDetails.Text = Result[0].PatientName.ToString();
                //GvPatientTestList.DataSource = Result;
                //GvPatientTestList.DataBind();
                //txt_PatientDetails.Visible = true;
                //btn_send.Attributes.Remove("disabled");
                //btn_send.Visible = true;
                //GvPatientTestList.Visible = true;
            }
            else
            {
                //GvPatientTestList.DataSource = null;
                //GvPatientTestList.DataBind();
                //txt_PatientDetails.Visible = false;
                btn_send.Visible = false;
            }
            IsConnectedToInternet(); // Checking Internet Connect or not
        }
        private void GVreset()
        {
            txt_PatientDetails.Text = "";
            GvPatientTestList.DataSource = null;
            GvPatientTestList.DataBind();
            GvPatientTestList.Visible = false;
        }
        protected void GV_PatientList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Invnumber = (Label)e.Row.FindControl("lbl_invnumber");
                Label Testname = (Label)e.Row.FindControl("lblTestName");
                Label lblHeaderID = (Label)e.Row.FindControl("lblHeaderID");
                Label Urgency = (Label)e.Row.FindControl("lblurgency");
                Label lblCollectionStatus = (Label)e.Row.FindControl("lblCollectionStatus");
                Label lblDeliveryStatus = (Label)e.Row.FindControl("lbldeliverystatus");
                LinkButton lnkprint = (LinkButton)e.Row.FindControl("lnkprint");
                LinkButton lnkreprint = (LinkButton)e.Row.FindControl("lnkreprint");
                LinkButton lnkEmailPrint = (LinkButton)e.Row.FindControl("lnkEmailPrint");
                Label RNotEntry = (Label)e.Row.FindControl("lblNotEntry");
                Label NotEntr2 = (Label)e.Row.FindControl("lblNotEntr2");
                Label IsVerified = (Label)e.Row.FindControl("lblIsVerified");
                Label IsReportPrinted = (Label)e.Row.FindControl("lblIsReportPrinted");
                if (Convert.ToInt32(IsVerified.Text) == 1)
                {
                    lnkprint.Enabled = true;
                    lnkprint.Visible = true;
                    RNotEntry.Visible = false;
                    NotEntr2.Visible = false;
                    lnkreprint.Visible = false;
                    lnkEmailPrint.Enabled = true;
                    lnkEmailPrint.Visible = true;
                    if (Convert.ToInt32(lblHeaderID.Text) == 1)
                    {
                        lnkEmailPrint.Text = "";
                        lnkEmailPrint.Enabled = false;
                        lnkEmailPrint.Visible = false;
                    }
                }
                else
                {
                    lnkprint.Enabled = false;
                    lnkprint.Visible = false;
                    lnkreprint.Visible = false;
                    RNotEntry.Visible = true;
                    NotEntr2.Visible = true;                   
                    if (Convert.ToInt32(lblHeaderID.Text) == 1)
                    {
                        lnkEmailPrint.Text = "";
                        lnkEmailPrint.Enabled = false;
                        lnkEmailPrint.Visible = false;
                    }
                }
                if (lblDeliveryStatus.Text == "0")
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Yellow;
                    Testname.ForeColor = System.Drawing.Color.Black;

                }
                if (lblDeliveryStatus.Text == "1")
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Green;
                    Testname.ForeColor = System.Drawing.Color.White;
                    
                }
                if (IsReportPrinted.Text == "0")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Red;
                    Invnumber.ForeColor = System.Drawing.Color.Black;
                }
                if (IsReportPrinted.Text == "1")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Blue;
                    Invnumber.ForeColor = System.Drawing.Color.White;
                }
            }
        }
        protected void GvPatientTestList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Print")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvPatientTestList.Rows[i];
                    Label labgroup = (Label)gr.Cells[0].FindControl("lblLabGroup");
                    Label SubgroupID = (Label)gr.Cells[0].FindControl("lbl_subgroup");
                    Label headerID = (Label)gr.Cells[0].FindControl("lblHeaderID");
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
                            //string url = "../MedLab/LabReportCollectionSample.aspx?id=" + Convert.ToInt64(ID.Text) + "&p=" + Convert.ToInt64(BillID.Text) + "&GroupID=" + Convert.ToInt32(labgroup.Text) + "&HeaderID=" + Convert.ToInt32(headerID.Text);
                            //string fullURL = "window.open('" + url + "', '_blank');";
                            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                            string Invns = InvNo.Text;
                            Int64 UHIDS = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                            int TestIDS = Convert.ToInt32(TestID.Text == "" ? "0" : TestID.Text);
                            string url = "../MedRadTemplate/ReportViewer.aspx?option=RadioReport&Inv=" + Invns + "&UHID=" + UHIDS + "&TestID=" + TestIDS;
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
                                if (SubgroupID.Text == "20")
                                {
                                    string param = "option=CultureReport&Inv=" + InvNo.Text + "&UHID=" + UHID.Text + "&TestID=" + TestID.Text + "&showheader=" + "1" + "&Template=" + template + "&Type=" + "2";
                                    //string param = "option=CultureReport&Inv=" + txt_inv3.Text + "&UHID=" + txt_uhid3.Text + "&TestID=" + hdntestID3.Value + "&showheader=" + "0" + "&Template=" + template + "&Type=" + "2";
                                    Commonfunction common = new Commonfunction();
                                    string ecryptstring = common.Encrypt(param);
                                    string baseurl = "../MedLab/Report/ReportViewer.aspx?ID=" + ecryptstring;
                                    string fullURL = "window.open('" + baseurl + "', '_blank');";
                                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                                }
                                else
                                {
                                    string param = "option=MultipleReport&Inv=" + InvNo.Text + "&UHID=" + UHID.Text + "&TestID=" + TestID.Text + "&showheader=" + "1" + "&Template=" + template + "&Type=" + "1";
                                    Commonfunction common = new Commonfunction();
                                    string ecryptstring = common.Encrypt(param);
                                    string baseurl = "../MedLab/Report/ReportViewer.aspx?ID=" + ecryptstring;
                                    string fullURL = "window.open('" + baseurl + "', '_blank');";
                                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                                }
                            }
                        }
                    }
                    else
                    {

                        if (labgroup.Text == "2" || labgroup.Text == "4")
                        {
                            //string url = "../MedLab/LabReportCollectionSample.aspx?id=" + Convert.ToInt64(ID.Text) + "&p=" + Convert.ToInt64(BillID.Text) + "&GroupID=" + Convert.ToInt32(labgroup.Text) + "&HeaderID=" + Convert.ToInt32(headerID.Text);
                            //string fullURL = "window.open('" + url + "', '_blank');";
                            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                            string Invns = InvNo.Text;
                            Int64 UHIDS = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                            int TestIDS = Convert.ToInt32(TestID.Text == "" ? "0" : TestID.Text);
                            string url = "../MedRadTemplate/ReportViewer.aspx?option=RadioReport&Inv=" + Invns + "&UHID=" + UHIDS + "&TestID=" + TestIDS;
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
                                if (SubgroupID.Text == "8")
                                {
                                    string param = "option=MultipleReport&Inv=" + InvNo.Text + "&UHID=" + UHID.Text + "&TestID=" + TestID.Text + "&showheader=" + "1" + "&Template=" + template + "&Type=" + "2";
                                    Commonfunction common = new Commonfunction();
                                    string ecryptstring = common.Encrypt(param);
                                    string baseurl = "../MedLab/Report/ReportViewer.aspx?ID=" + ecryptstring;
                                    string fullURL = "window.open('" + baseurl + "', '_blank');";
                                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                                }
                                else
                                {
                                    string param = "option=MultipleReport&Inv=" + InvNo.Text + "&UHID=" + UHID.Text + "&TestID=" + TestID.Text + "&showheader=" + "1" + "&Template=" + template + "&Type=" + "1";
                                    Commonfunction common = new Commonfunction();
                                    string ecryptstring = common.Encrypt(param);
                                    string baseurl = "../MedLab/Report/ReportViewer.aspx?ID=" + ecryptstring;
                                    string fullURL = "window.open('" + baseurl + "', '_blank');";
                                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                                }
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
                    GridViewRow gr = GvPatientTestList.Rows[i];
                    Label labgroup = (Label)gr.Cells[0].FindControl("lblLabGroup");
                    Label headerID = (Label)gr.Cells[0].FindControl("lblHeaderID");
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
                            string url = "../MedLab/LabReportCollectionSample.aspx?id=" + Convert.ToInt64(ID.Text) + "&p=" + Convert.ToInt64(BillID.Text) + "&GroupID=" + Convert.ToInt32(labgroup.Text) + "&HeaderID=" + Convert.ToInt32(headerID.Text);
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
                            //string url = "../MedLab/LabReportCollectionSample.aspx?id=" + Convert.ToInt64(ID.Text) + "&p=" + Convert.ToInt64(BillID.Text) + "&GroupID=" + Convert.ToInt32(labgroup.Text) + "&HeaderID=" + Convert.ToInt32(headerID.Text);
                            //string fullURL = "window.open('" + url + "', '_blank');";
                            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                            string Invns = InvNo.Text;
                            Int64 UHIDS = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                            int TestIDS = Convert.ToInt32(TestID.Text == "" ? "0" : TestID.Text);
                            string url = "../MedRadTemplate/ReportViewer.aspx?option=RadioReport&Inv=" + Invns + "&UHID=" + UHIDS + "&TestID=" + TestIDS;
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
            //txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            //txtDateto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            ddlstatus.SelectedIndex = 0;
            ddl_labgroup.SelectedIndex = 1;
            ddl_patient_type.SelectedIndex = 0;
            AutoCompleteExtender1.ContextKey = "0";
            lblresult.Visible = false;
            lblmessage.Visible = false;
            bindgrid(1);
            GVreset();
            hdnpath.Text = "";
            hdnuhid.Text = "";
            hdntestid.Text = "";
            txt_PatientDetails.Text = "";
            hdnemail.Text = "";
            hdninvnumber.Text = "";
            txt_PatientDetails.Visible = false;
            btn_send.Visible = false;
            ddl_centre.SelectedIndex = 0;
            txtdatefrom.Text = "";
            txtDateto.Text = "";
        }
        protected void btnsendmail_Click(object sender, EventArgs e)
        {

            if (hdnpath.Text == "" || hdnuhid.Text == "" || hdntestid.Text == "" || txt_PatientDetails.Text == "" || hdnemail.Text == "" || hdninvnumber.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Pending reports are not ready for delivery.", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }


            MailAddress to = new MailAddress(hdnemail.Text);
            MailAddress from = new MailAddress("downtownkbk@gmail.com");
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Lab Reports(" + txt_PatientDetails.Text + ")";
            message.Body = "Hi Centre, " +
                "Please find the enclosed attachment reports, " +
                "Down Town Clinic.";
            // message.CC.Add(new MailAddress("meghan@westminster.co.uk"));
            // message.Bcc.Add(new MailAddress("charles@westminster.co.uk"));

            string Rpath = hdnpath.Text;
            DirectoryInfo dir = new DirectoryInfo(Rpath);
            foreach (FileInfo file in dir.GetFiles("*.*"))
            {
                if (file.Exists)
                {
                    message.Attachments.Add(new Attachment(file.FullName));
                    lblmessage.Visible = false;
                    // File.Delete(file.FullName);
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Reports are not ready", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
            }
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {               
                Credentials = new NetworkCredential("downtownkbk@gmail.com", "EmailPass@123"),
                EnableSsl = true
            };
            //code in brackets above needed if authentication required
            try
            {
                client.Send(message);
                message.Dispose();
                if (Directory.Exists(Rpath))
                {
                    Directory.Delete(Rpath, true);
                    hdnpath.Text = "";
                }
                LadReportCollectionData objData = new LadReportCollectionData();
                LabReportCollectionBO objBO = new LabReportCollectionBO();
                objData.UHID = Convert.ToInt64(hdnuhid.Text == "" ? "0" : hdnuhid.Text);
                objData.InVnumber = hdninvnumber.Text;
                objData.Testids = hdntestid.Text;
                int result = objBO.UpdateEmaildeliveryStatus(objData);
                if (result == 1)
                {
                    hdnpath.Text = "";
                    hdnuhid.Text = "";
                    hdntestid.Text = "";
                    // txt_PatientDetails.Text = "";
                    hdnemail.Text = "";
                    hdninvnumber.Text = "";
                    btn_send.Attributes["disabled"] = "disabled";
                    Messagealert_.ShowMessage(lblmessage, "Email has sent.", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "There is someting error in local database.", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }
            }
            catch (SmtpException ex)
            {
                Messagealert_.ShowMessage(lblmessage, "Email could not sent. Please check internet connection and try again.", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
            }
        }
        protected void ddl_show_SelectedIndexChanged(object sender, EventArgs e)
        {
            GVreset();
            bindgrid(1);
        }
        protected void GV_PatientList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }
        protected void txt_InvNumber_TextChanged(object sender, EventArgs e)
        {
            GVreset();
            bindgrid(1);
        }
        protected void txt_patientName_TextChanged(object sender, EventArgs e)
        {
            GVreset();
            bindgrid(1);
        }
        protected void ddlstatus_TextChanged(object sender, EventArgs e)
        {
            GVreset();
            bindgrid(1);
        }
        protected void txtdatefrom_TextChanged(object sender, EventArgs e)
        {
            GVreset();
            bindgrid(1);
        }
        protected void txtDateto_TextChanged(object sender, EventArgs e)
        {
            GVreset();
            bindgrid(1);
        }
        protected void ddl_centre_TextChanged(object sender, EventArgs e)
        {
            GVreset();
            bindgrid(1);
        }
        //------Checking Internet Connection------//

        public bool IsConnectedToInternet()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                
                btn_send.Visible = true;
                btn_send.Text = "Send mail";
                btn_send.Attributes.Remove("disabled");
                return true;
            }
            catch
            {
                //btn_send.Visible = false;
                btn_send.Text = "Check Internet connetion to send mail";
                btn_send.Attributes["disabled"] = "disabled";
                return false;
            }
        }
       
    }
}
