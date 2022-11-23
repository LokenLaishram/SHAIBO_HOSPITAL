using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedLabBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedLab;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedLab
{
    public partial class EndoscopyReportVerification : BasePage
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
            Commonfunction.PopulateDdl(ddl_labsubgroup, mstlookup.GetSubGroupByGroupID(4));
            Commonfunction.PopulateDdl(ddl_labTestName, mstlookup.GetTestNameBySubGroupID(0));
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

            bindgrid(1);

        }
        private void bindgrid(int page)
        {
            try
            {


                List<RadioLabReportVerificationData> lstemp = getRadioTestList(page);

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
        private List<RadioLabReportVerificationData> getRadioTestList(int p)
        {
            RadioLabReportVerificationData objData = new RadioLabReportVerificationData();
            RadioLabReportVerificationBO objBO = new RadioLabReportVerificationBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objData.PatientType = Convert.ToInt32(ddl_patient_type.SelectedValue == "0" ? null : ddl_patient_type.SelectedValue);
            objData.LabSubGrpID = Convert.ToInt32(ddl_labsubgroup.SelectedValue == "0" ? null : ddl_labsubgroup.SelectedValue);
            objData.TestID = Convert.ToInt32(ddl_labTestName.SelectedValue == "0" ? null : ddl_labTestName.SelectedValue);
            objData.UHID = Convert.ToInt32(txt_UHID.Text == "" ? null : txt_UHID.Text);
            objData.LabTestID = Convert.ToInt32(txt_lab_test_id.Text == "" ? null : txt_lab_test_id.Text);
            objData.PatientName = txt_patientName.Text == "" ? "" : txt_patientName.Text;
            objData.TestStatus = Convert.ToInt32(ddlstatus.SelectedValue == "0" ? null : ddlstatus.SelectedValue);
            DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            string datefrom = from.ToString("yyyy-MM-dd");
            string dateto = To.ToString("yyyy-MM-dd");
            from = Convert.ToDateTime(datefrom + " " + "12:01:00 AM");
            To = Convert.ToDateTime(dateto + " " + "11:59:00 PM");
            objData.DateFrom = from;
            objData.DateTo = To;
            objData.LabGrpID = 4;
            objData.CurrentIndex = p;
            return objBO.GetPatientList(objData);
        }
        protected void GvPatientList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList verify = (DropDownList)e.Row.FindControl("ddlverifyby");
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(verify, mstlookup.GetLookupsList(LookupName.EndoscopyDoctor));

                Label verifiedby = (Label)e.Row.FindControl("lblverifyby");
                if (verifiedby.Text != "0")
                {
                    verify.Items.FindByValue(verifiedby.Text).Selected = true;
                }
         
                Label Testname = (Label)e.Row.FindControl("lblTestName");
                Label Urgency = (Label)e.Row.FindControl("lblurgency");
                Label isVerified = (Label)e.Row.FindControl("lblisVerified");
                LinkButton lnkverify = (LinkButton)e.Row.FindControl("lnkVerify");
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
                if (isVerified.Text == "1")
                {
                    lnkverify.Visible = false;
                    // lblVerified.Visible = true;
                    verify.Visible = true;
                }
                else
                {
                    lnkverify.Visible = true;
                    //   lblVerified.Visible = false;
                    verify.Visible = true;
                }
            }
        }
        protected void GvPatientList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "verify")
                {
                    RadioLabReportVerificationData objData = new RadioLabReportVerificationData();
                    RadioLabReportVerificationBO objBO = new RadioLabReportVerificationBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvPatientList.Rows[i];
                    Label BillID = (Label)pt.Cells[0].FindControl("lblBillID");
                    Label lblReportID = (Label)pt.Cells[0].FindControl("lblReportID");
                    DropDownList verifyby = (DropDownList)pt.Cells[0].FindControl("ddlverifyby");
                    objData.VerifyBy = Convert.ToInt64(verifyby.Text);
                    objData.billID = Convert.ToInt64(BillID.Text);
                    objData.ID = Convert.ToInt64(lblReportID.Text);
                    if (objData.VerifyBy == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please select verify by.", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        return;
                    }
                    else
                    {
                        divmsg1.Visible = false;
                    }
                    int result = objBO.UpdateRadioReportVerification(objData);
                    if (result == 1 || result == 2)
                    {
                        Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SucessAlert";
                        bindgrid(1);

                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
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
            txt_lab_test_id.Text = "";
            txt_patientName.Text = "";
            txt_UHID.Text = "";
             ddlstatus.SelectedIndex = 0;
            bindgrid(1);
            txtdatefrom.Text = "";
            txtto.Text = "";

        }

        protected void GvPatientList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }
    }
}