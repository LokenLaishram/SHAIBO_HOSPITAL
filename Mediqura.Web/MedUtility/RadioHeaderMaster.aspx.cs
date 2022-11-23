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
    public partial class RadioHeaderMaster : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
                bindddl();
                ViewState["ID"] = null;
            }
        }

        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_group, mstlookup.GetLookupsList(LookupName.LabGroups));
        }
     
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try{
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

            if (ddl_group.SelectedValue == "0")
            {
                Messagealert_.ShowMessage(lblmessage, "Group", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";

                ddl_group.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (txt_code.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Code", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";

                txt_code.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }

            if (txt_headername.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Please enter header name.", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_headername.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            RadiologyHeaderMasterData objRadioReportMaster = new RadiologyHeaderMasterData();
            RadiologyHeaderMasterBO objRadioBO = new RadiologyHeaderMasterBO();
            objRadioReportMaster.GroupID =Convert.ToInt32(ddl_group.SelectedValue == "" ? "0" : ddl_group.SelectedValue);
            objRadioReportMaster.Template = txtReport.InnerHtml.ToString();
            objRadioReportMaster.HeaderCode = txt_code.Text.Trim() == "" ? null : txt_code.Text.Trim();
            objRadioReportMaster.HeaderName = txt_headername.Text.Trim() == "" ? null : txt_headername.Text.Trim();
            objRadioReportMaster.EmployeeID = LogData.EmployeeID;
            objRadioReportMaster.ActionType = Enumaction.Insert;
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
                    bindgrid();
                    
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
            txt_code.Text = "";
            txt_headername.Text = "";
            ddl_group.SelectedIndex = 0;
            GvRadioHeader.DataSource = null;
            GvRadioHeader.DataBind();
            txtReport.InnerHtml = null;
            lblmessage.Visible = false;

        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
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

                List<RadiologyHeaderMasterData> lstemp = GetRadioHeaderDetaiks(0);
                if (lstemp.Count > 0)
                {
                    GvRadioHeader.DataSource = lstemp;
                    GvRadioHeader.DataBind();
                    GvRadioHeader.Visible = true;
                                        
                }
                else
                {
                    GvRadioHeader.DataSource = null;
                    GvRadioHeader.DataBind();
                    GvRadioHeader.Visible = true;
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

        private List<RadiologyHeaderMasterData> GetRadioHeaderDetaiks(int p)
        {
            RadiologyHeaderMasterData objData = new RadiologyHeaderMasterData();
            RadiologyHeaderMasterBO objBO = new RadiologyHeaderMasterBO();
            objData.HeaderCode = txt_code.Text.Trim() == "" ? null : txt_code.Text.Trim();
            objData.HeaderName = txt_headername.Text.Trim() == "" ? null : txt_headername.Text.Trim();
            objData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objBO.SearchRadioHeaderDetaiks(objData);  
        }

        protected void GvRadioHeader_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edits")
                {
                    if (LogData.EditEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "EditEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    RadiologyHeaderMasterData objData = new RadiologyHeaderMasterData();
                    RadiologyHeaderMasterBO objBO = new RadiologyHeaderMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvRadioHeader.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("lbl_ID");
                    objData.ID = Convert.ToInt32(ID.Text);
                    objData.ActionType = Enumaction.Select;

                    List<RadiologyHeaderMasterData> objdata = objBO.GetRadioHeaderByID(objData);
                    if (objdata.Count > 0)
                    {

                        txtReport.InnerHtml = objdata[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&");
                        txt_code.Text = objdata[0].HeaderCode.ToString();
                        txt_headername.Text = objdata[0].HeaderName.ToString();
                        ddl_group.SelectedValue = objdata[0].GroupID.ToString();
                        ViewState["ID"] = objdata[0].ID.ToString(); 
                        
                    }
                    else
                    {
                        ViewState["ID"] = null;
                        txtReport.InnerText = null;
                    }
                }
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "DeleteEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    RadiologyHeaderMasterData objData = new RadiologyHeaderMasterData();
                    RadiologyHeaderMasterBO objBO = new RadiologyHeaderMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvRadioHeader.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("lbl_ID");
                    objData.ID = Convert.ToInt32(ID.Text);
                    objData.EmployeeID = LogData.EmployeeID;

                    RadiologyHeaderMasterBO objBO1 = new RadiologyHeaderMasterBO();
                    int Result = objBO1.DeleteRadioHeaderByID(objData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        div1.Visible = true;
                        div1.Attributes["class"] = "SucessAlert";
                        bindgrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";

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
      
    }
}