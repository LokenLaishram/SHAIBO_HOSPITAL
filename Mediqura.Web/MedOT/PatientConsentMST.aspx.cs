using Mediqura.BOL.CommonBO;
using Mediqura.BOL.OTBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.OTData;
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

namespace Mediqura.Web.MedOT
{
    public partial class PatientConsentMST : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                lblmessage.Visible = false;
                ddlbind();
                //checkSelect();
            }
        }
     
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_ConsentType, mstlookup.GetLookupsList(LookupName.ConsentType));
           
        }
        protected void ddl_ConsentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_ConsentType.SelectedIndex > 0)
            {
                getReportTemplate();
            }

        }
        public void getReportTemplate()
        {
            PatientConsentData objData = new PatientConsentData();
            PatientConsentTypeBO objBO = new PatientConsentTypeBO();
            objData.TypeID = Convert.ToInt32(ddl_ConsentType.SelectedValue == "0" ? null : ddl_ConsentType.SelectedValue);
            //objData.TypeFeatureID = Convert.ToInt32(ddl_dischargeFeature.SelectedValue == "0" ? null : ddl_dischargeFeature.SelectedValue);
            List<PatientConsentData> objdata = objBO.GetConsentTemplateByID(objData);
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

                if (ddl_ConsentType.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "DischargeType", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";

                    ddl_ConsentType.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                PatientConsentData objData = new PatientConsentData();
                PatientConsentTypeBO objBO = new PatientConsentTypeBO();
                objData.Template = txtReport.InnerHtml.ToString();
                objData.TypeID = Convert.ToInt32(ddl_ConsentType.SelectedValue == "0" ? null : ddl_ConsentType.SelectedValue);
                //objData.TypeFeatureID = Convert.ToInt32(ddl_dischargeFeature.SelectedValue == "0" ? null : ddl_dischargeFeature.SelectedValue);
                objData.EmployeeID = LogData.EmployeeID;
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
                        objData.ActionType = Enumaction.Update;
                        objData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objBO.UpdateConsentReport(objData);
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

            ddl_ConsentType.SelectedIndex = 0;
            lblmessage.Visible = false;
            div1.Visible = false;
            txtReport.InnerHtml = "";
        }
    }
}