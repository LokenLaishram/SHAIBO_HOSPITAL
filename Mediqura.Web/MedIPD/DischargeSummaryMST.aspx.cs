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
    public partial class DischargeSummaryMST : BasePage
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
        //public void checkSelect()
        //{
        //    if (ddl_DisType.SelectedIndex == 0)
        //    {
        //        ddl_labTestName.Attributes["disabled"] = "disabled";
        //    }
        //    else
        //    {
        //        ddl_labTestName.Attributes.Remove("disabled");
        //    }
        //    if (ddl_labTestName.SelectedIndex == 0)
        //    {
        //        ddl_gender.Attributes["disabled"] = "disabled";
        //    }
        //    else
        //    {
        //        ddl_gender.Attributes.Remove("disabled");
        //    }


        //}
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_DisType, mstlookup.GetLookupsList(LookupName.DisType));
        }
        protected void ddl_DisType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_DisType.SelectedIndex > 0)
            {
                getReportTemplate();
            }
           
        }
        public void getReportTemplate()
        {
            DischargeData objRadioReportMaster = new DischargeData();
            DischargeBO objRadioBO = new DischargeBO();
            objRadioReportMaster.DischargeTypeID = Convert.ToInt32(ddl_DisType.SelectedValue == "0" ? null : ddl_DisType.SelectedValue);
            //objRadioReportMaster.TypeFeatureID = Convert.ToInt32(ddl_dischargeFeature.SelectedValue == "0" ? null : ddl_dischargeFeature.SelectedValue);
            List<DischargeData> objdata = objRadioBO.GetRadioTemplateByID(objRadioReportMaster);
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

                if (ddl_DisType.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "DischargeType", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";

                    ddl_DisType.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                
                DischargeData objRadioReportMaster = new DischargeData();
                DischargeBO objRadioBO = new DischargeBO();
                objRadioReportMaster.Template = txtReport.InnerHtml.ToString();
                objRadioReportMaster.DischargeTypeID = Convert.ToInt32(ddl_DisType.SelectedValue == "0" ? null : ddl_DisType.SelectedValue);
                //objRadioReportMaster.TypeFeatureID = Convert.ToInt32(ddl_dischargeFeature.SelectedValue == "0" ? null : ddl_dischargeFeature.SelectedValue);
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
                int result = objRadioBO.UpdateDishaegeReport(objRadioReportMaster);
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
          
            ddl_DisType.SelectedIndex = 0;
            lblmessage.Visible = false;
            div1.Visible = false;
            txtReport.InnerHtml = "";
        }

      
    }
}