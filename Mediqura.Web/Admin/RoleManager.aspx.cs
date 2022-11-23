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

    public partial class RoleManager : BasePage
    {
          protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = true;
                       }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                RolesData objroles = new RolesData();
                AddRolesBO objAddRolesBO = new AddRolesBO();
                objroles.RoleCode = txtcode.Text;
                objroles.RoleName = txtdescription.Text;
                objroles.AddedBy = LogData.AddedBy;
                objroles.EmployeeID = LogData.LoginID;
                objroles.CompanyID = LogData.CompanyID;
                objroles.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    objroles.ActionType = Enumaction.Update;
                    objroles.RoleID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objAddRolesBO.UpdateRoleDetails(objroles);
                if (result == 1 || result == 2)
                {
                    clearall();
                    bindgrid();
                   
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    ViewState["ID"] = null;
                }
                else if (result == 5)
                {
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                    clearall();
                    bindgrid();
                }
                else
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
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
     
        protected void GvRoledetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edits")
                {
                    RolesData objroles = new RolesData();
                    AddRolesBO objAddRolesBO = new AddRolesBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvRoledetails.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    objroles.RoleID = Convert.ToInt32(ID.Text);
                    objroles.ActionType = Enumaction.Select;

                    List<RolesData> GetResult = objAddRolesBO.GetRoleDetailsByID(objroles);
                    if (GetResult.Count > 0)
                    {
                        txtcode.Text = GetResult[0].RoleCode;
                        txtdescription.Text = GetResult[0].RoleName;
                        ViewState["ID"] = GetResult[0].RoleID;
                        btnsave.Text = "Update";
                    }
                }
                if (e.CommandName == "Deletes")
                {
                    RolesData objroles = new RolesData();
                    AddRolesBO objAddRolesBO = new AddRolesBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvRoledetails.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    objroles.RoleID = Convert.ToInt32(ID.Text);
                    objroles.ActionType = Enumaction.Delete;
                    int Result = objAddRolesBO.DeleteRoleDetailsByID(objroles);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        GvRoledetails.DataSource = GetRoleDetails(0);
                        GvRoledetails.DataBind();
                    }
                    else if (Result == 2)
                    {
                        Messagealert_.ShowMessage(lblmessage, "RoleDelete", 0);
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
            List<RolesData> lstemp = GetRoleDetails(0);
         
            if (lstemp.Count > 0)
            {
                GvRoledetails.DataSource = GetRoleDetails(0);
                GvRoledetails.DataBind();
                GvRoledetails.Visible = true;
            }
            else
            {
                GvRoledetails.DataSource = null;
                GvRoledetails.DataBind();
            }
        }
        public List<RolesData> GetRoleDetails(int curIndex)
        {
            RolesData objroles = new RolesData();
            AddRolesBO objAddRolesBO = new AddRolesBO();
            objroles.RoleCode = txtcode.Text == "" ? null : txtcode.Text;
            objroles.RoleName = txtdescription.Text == "" ? null : txtdescription.Text;
            objroles.ActionType = Enumaction.Select;
            objroles.PageSize = GvRoledetails.PageSize;
            objroles.CurrentIndex = curIndex;
            return objAddRolesBO.SearchRoleDetails(objroles);
        }
        private void clearall()
        {
            txtcode.Text = "";
            txtdescription.Text = "";
            GvRoledetails.DataSource = null;
            GvRoledetails.DataBind();
            GvRoledetails.Visible = false;
            btnsave.Text = "Add";
            lblmessage.Visible = false;
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            clearall();
        }
        protected void GvRoledetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvRoledetails.PageIndex = e.NewPageIndex;
            bindgrid();
        
        }
    }
}