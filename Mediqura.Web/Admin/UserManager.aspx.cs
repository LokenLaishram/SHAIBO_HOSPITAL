using Mediqura.BOL.CommonBO;
using Mediqura.BOL.UserBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using Mediqura.Web.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.Admin
{

    public partial class UserManager : BasePage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                lblmessage.Visible = true;
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlemp, mstlookup.GetLookupsList(LookupName.Employee));
            Commonfunction.PopulateDdl(ddlrole, mstlookup.GetLookupsList(LookupName.Roles));

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                AddUsersData objusers = new AddUsersData();
                AddUsersBO objAddUsersBO = new AddUsersBO();

                objusers.RoleID = Convert.ToInt32(ddlrole.SelectedValue == "" ? "0" : ddlrole.SelectedValue);
                objusers.EmployeeID = Convert.ToInt64(ddlemp.SelectedValue == "" ? "0" : ddlemp.SelectedValue);
                objusers.UserPassword = txtcpassword.Text;
                objusers.UserName = txtusername.Text;
                objusers.enableNotification = Convert.ToInt32(ddl_dis_notification.SelectedValue);
                objusers.AddedBy = LogData.AddedBy;
                objusers.CompanyID = LogData.HospitalID;
                objusers.FinancialYearID = LogData.FinancialYearID;
                objusers.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    objusers.ActionType = Enumaction.Update;
                    objusers.RoleID = Convert.ToInt32(ddlrole.SelectedValue);
                }
                int result = objAddUsersBO.UpdateUserDetails(objusers);
                if (result == 1 || result == 2)
                {
                    clearall();
                    bindgrid();
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    ViewState["ID"] = null;
                }
                else if (result == 5)
                {
                    Messagealert_.ShowMessage(lblmessage, "Already created your user credential.So you can edit the existing  one", 0);
                    clearall();
                    bindgrid();
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                lblmessage.Visible = true;
                lblmessage.CssClass = "MessageFailed";
            }
        }
      
        protected void GvUserdetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edits")
                {
                    AddUsersData objusers = new AddUsersData();
                    AddUsersBO objAddUsersBO = new AddUsersBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvUserdetails.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    objusers.LoginID = Convert.ToInt32(ID.Text);
                    objusers.ActionType = Enumaction.Select;

                    List<AddUsersData> GetResult = objAddUsersBO.GetUserDetailsByID(objusers);
                    if (GetResult.Count > 0)
                    {
                        txtusername.Text = GetResult[0].UserName;
                        txtpassword.Text = GetResult[0].UserPassword;
                        ddlemp.SelectedValue = GetResult[0].EmployeeID.ToString();
                        ddlrole.SelectedValue = GetResult[0].RoleID.ToString();
                        ddl_dis_notification.SelectedValue= GetResult[0].enableNotification.ToString();
                        ViewState["ID"] = GetResult[0].LoginID;
                        btnsave.Text = "Update";
                    }
                }
                if (e.CommandName == "Deletes")
                {
                    AddUsersData objusers = new AddUsersData();
                    AddUsersBO objAddUsersBO = new AddUsersBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvUserdetails.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    objusers.LoginID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    objusers.ActionType = Enumaction.Delete;
                    int Result = objAddUsersBO.DeleteUserDetailsByID(objusers);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        GvUserdetails.DataSource = GetUserDetails(0);
                        GvUserdetails.DataBind();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                lblmessage.Visible = true;
                lblmessage.CssClass = "Message";
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        private void bindgrid()
        {
            List<AddUsersData> lstemp = GetUserDetails(0);
            GvUserdetails.DataSource = lstemp;
            GvUserdetails.DataBind();
            GvUserdetails.Visible = true;
            btnsave.Text = "Add";

        }
        public List<AddUsersData> GetUserDetails(int curIndex)
        {
            AddUsersData objusers = new AddUsersData();
            AddUsersBO objAddUsersBO = new AddUsersBO();
            objusers.UserName = txtusername.Text == "" ? null : txtusername.Text;
            objusers.RoleID = Convert.ToInt32(ddlrole.SelectedValue == "" ? "0" : ddlrole.SelectedValue);
            objusers.EmployeeID = Convert.ToInt32(ddlemp.SelectedValue == "" ? "0" : ddlemp.SelectedValue);
            objusers.ActionType = Enumaction.Select;
            objusers.PageSize = GvUserdetails.PageSize;
            objusers.CurrentIndex = curIndex;
            return objAddUsersBO.SearchUserDetails(objusers);
        }
        private void clearall()
        {
            txtusername.Text = "";
            txtpassword.Text = "";
            txtcpassword.Text = "";
            ddlemp.SelectedIndex = 0;
            ddlrole.SelectedIndex = 0;
            GvUserdetails.DataSource = null;
            GvUserdetails.DataBind();
            GvUserdetails.Visible = false;
            btnsave.Text = "Add";
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            clearall();
        }
        protected void GvUserdetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvUserdetails.PageIndex = e.NewPageIndex;
            bindgrid();
        }
    }
}