using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedUtility
{
    public partial class UrlPage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ddlbind();
        }
         private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlpage_Parent, mstlookup.GetLookupsList(LookupName.ParentSiteMap));

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                AddPageMasterData objAddPageCD = new AddPageMasterData();
                AddPageMasterBO objpatBO = new AddPageMasterBO();
                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);

                if (ddlpage_Parent.SelectedIndex == 0)
                {

                    Messagealert_.ShowMessage(lblmessage, "Please Select Parent page.", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddlpage_Parent.Focus();
                    return;

                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_page_title.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter Page title.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txt_page_title.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_page_description.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter Page Description.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txt_page_description.Focus();
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_page_url.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter Page Url.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txt_page_url.Focus();
                }
                else
                {
                    lblmessage.Visible = false;
                }
                objAddPageCD.Title = txt_page_title.Text == "" ? null : txt_page_title.Text.Trim();
                objAddPageCD.Description = txt_page_description.Text == "" ? null : txt_page_description.Text.Trim();
                objAddPageCD.Url = txt_page_url.Text == "" ? null : txt_page_url.Text.Trim();
                objAddPageCD.ParentID = Convert.ToInt32(ddlpage_Parent.SelectedValue == "" ? "0" : ddlpage_Parent.SelectedValue);
                objAddPageCD.AddedBy = LogData.AddedBy;

                int results = objpatBO.UpdatePageDetail(objAddPageCD);
                if (results == 1 || results == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, results == 1 ? "save" : "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
            }
        }
    }
   
}