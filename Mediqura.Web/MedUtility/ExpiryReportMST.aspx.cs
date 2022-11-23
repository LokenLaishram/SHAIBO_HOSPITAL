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

namespace Mediqura.Web.MedUtility
{
    public partial class ExpiryReportMST : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                lblmessage.Visible = false;
             }
        }

        public void getReportTemplate()
        {
            ExpiryReportMasterData objReportMaster = new ExpiryReportMasterData();
            ExpiryReportMasterBO objBO = new ExpiryReportMasterBO();
            List<ExpiryReportMasterData> objdata = objBO.GetExpiryTemplateByID(objReportMaster);
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


                ExpiryReportMasterData objRadioReportMaster = new ExpiryReportMasterData();
                ExpiryReportMasterBO objRadioBO = new ExpiryReportMasterBO();
                objRadioReportMaster.Template = txtReport.InnerHtml.ToString();
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
                int result = objRadioBO.UpdateExpiryReport(objRadioReportMaster);
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
            lblmessage.Visible = false;
           }
    }
}