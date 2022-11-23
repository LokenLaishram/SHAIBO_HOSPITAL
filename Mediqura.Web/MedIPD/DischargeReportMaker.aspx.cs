using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedIPD
{
    public partial class DischargeReportMaker : BasePage
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
            if (ddl_labTestName.SelectedIndex == 0)
            {
                ddl_gender.Attributes["disabled"] = "disabled";
            }
            else
            {
                ddl_gender.Attributes.Remove("disabled");
            }


        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();

            Commonfunction.PopulateDdl(ddl_labsubgroup, mstlookup.GetSubGroupByGroupID(2));
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
            getReportTemplate();
        }
        protected void ddl_labTestName_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkSelect();

        }

        public void getReportTemplate()
        {
            RadiologyReportMasterData objRadioReportMaster = new RadiologyReportMasterData();
            RadiologyReportMasterBO objRadioBO = new RadiologyReportMasterBO();
            objRadioReportMaster.TestId = Convert.ToInt32(ddl_labTestName.SelectedValue == "0" ? null : ddl_labTestName.SelectedValue); ;
            objRadioReportMaster.LabSubGroupId = Convert.ToInt32(ddl_labsubgroup.SelectedValue == "0" ? null : ddl_labsubgroup.SelectedValue);
            objRadioReportMaster.Gender = Convert.ToInt32(ddl_gender.SelectedValue == "0" ? null : ddl_gender.SelectedValue);
            List<RadiologyReportMasterData> objdata = objRadioBO.GetRadioTemplateByID(objRadioReportMaster);
            if (objdata.Count > 0)
            {

                txtReport.InnerHtml = objdata[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&");
                ViewState["ID"] = objdata[0].ID.ToString(); ;
            }
            else
            {
                ViewState["ID"] = null;
                txtReport.InnerText = null;
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

                if (ddl_labsubgroup.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Subgroup", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";

                    ddl_labsubgroup.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_labTestName.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Group", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";

                    ddl_labTestName.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_gender.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Gender", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";

                    ddl_gender.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                RadiologyReportMasterData objRadioReportMaster = new RadiologyReportMasterData();
                RadiologyReportMasterBO objRadioBO = new RadiologyReportMasterBO();
                objRadioReportMaster.Template = txtReport.InnerHtml.ToString();
                objRadioReportMaster.LabSubGroupId = Convert.ToInt32(ddl_labsubgroup.SelectedValue == "0" ? null : ddl_labsubgroup.SelectedValue);
                objRadioReportMaster.TestId = Convert.ToInt32(ddl_labTestName.SelectedValue == "0" ? null : ddl_labTestName.SelectedValue);
                objRadioReportMaster.Gender = Convert.ToInt32(ddl_gender.SelectedValue == "0" ? null : ddl_gender.SelectedValue);
                objRadioReportMaster.EmployeeID = LogData.EmployeeID;

                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        objRadioReportMaster.ActionType = Enumaction.Update;
                        objRadioReportMaster.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objRadioBO.UpdateRadioReport(objRadioReportMaster);
                if (result == 1 || result == 2)
                {
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";

                    getReportTemplate();

                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);

            }

        }

        protected void btnresets_Click(object sender, EventArgs e)
        {
            ddl_labTestName.Attributes["disabled"] = "disabled";
            ddl_gender.Attributes["disabled"] = "disabled";
            ddl_gender.SelectedIndex = 0;
            ddl_labsubgroup.SelectedIndex = 0;
        }

        protected void ddl_gender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_gender.SelectedIndex > 0)
            {
                getReportTemplate();
            }
            else
            {
                ViewState["ID"] = null;
                getReportTemplate();
            }
        }
    }
}