using Mediqura.BOL.CommonBO;
using Mediqura.BOL.UserBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Mediqura.Web.Admin
{
    public partial class AssignPageRights : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(drpRole, mstlookup.GetLookupsList(LookupName.Roles));
            //drpRole.SelectedIndex = 1;
            //bindsitemaps();
            Commonfunction.PopulateDdl(ddl_menuheader, mstlookup.GetLookupsList(LookupName.MenuHeader));
            Commonfunction.Insertzeroitemindex(ddl_subheader);
        }
        protected void drpRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_menuheader.SelectedIndex = 0;
            Commonfunction.Insertzeroitemindex(ddl_subheader);
            Gvpagemanager.DataSource = null;
            Gvpagemanager.DataBind();
            Gvpagemanager.Visible = false;
            lblmessage.Visible = false;
            btnupdate.Visible = false;
        }
        protected void ddl_menuheader_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpRole.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "role", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddl_menuheader.SelectedIndex > 0 && drpRole.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_subheader, mstlookup.GetMenuSubheaderByHeaderID(Convert.ToInt32(ddl_menuheader.SelectedValue == "" ? "0" : ddl_menuheader.SelectedValue)));
                bindsitemaps();
            }
            else
            {
                Commonfunction.Insertzeroitemindex(ddl_subheader);
                Gvpagemanager.DataSource = null;
                Gvpagemanager.DataBind();
                btnupdate.Visible = false;
                Gvpagemanager.Visible = false;
                //bindsitemaps();
                lblmessage.Visible = false;

            }
        }
        protected void ddl_subheader_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindsitemaps();
        }
        protected void bindsitemaps()
        {
            lblmessage.Visible = false;
            RolesData objRole = new RolesData();
            UserBO useradm = new UserBO();
            objRole.RoleID = Convert.ToInt32(drpRole.SelectedValue == "" ? "0" : drpRole.SelectedValue);
            objRole.MenuHeaderID = Convert.ToInt32(ddl_menuheader.SelectedValue == "" ? "0" : ddl_menuheader.SelectedValue);
            objRole.PageID = Convert.ToInt32(ddl_subheader.SelectedValue == "" ? "0" : ddl_subheader.SelectedValue);
            List<SiteMapData> lstSitemap = useradm.GetMedPagesbyRoleID(objRole);
            if (lstSitemap.Count > 0)
            {
                Gvpagemanager.DataSource = lstSitemap;
                Gvpagemanager.DataBind();
                Gvpagemanager.Visible = true;
                btnupdate.Visible = true;
            }
            else
            {
                Gvpagemanager.DataSource = null;
                Gvpagemanager.DataBind();
                Gvpagemanager.Visible = false;
                btnupdate.Visible = false;
            }
        }
        protected void Gvpagemanager_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Status = e.Row.FindControl("lbl_menustatus") as Label;
                Label Menuheader = e.Row.FindControl("lbl_menuHeader") as Label;
                CheckBox Chk = e.Row.FindControl("chkselect") as CheckBox;
                if (Status.Text == "1")
                {
                    Chk.Checked = true;
                }
                else
                {
                    Chk.Checked = false;
                }
                if (Menuheader.Text == "1")
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    //Chk.Checked = true;
                    //Chk.Enabled = false;
                }
                if (Menuheader.Text == "0")
                {
                    e.Row.BackColor = System.Drawing.Color.White;
                    e.Row.ForeColor = System.Drawing.Color.Black;
                }
            }

        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (drpRole.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "role", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                List<RolesData> list = new List<RolesData>();
                UserBO useradm = new UserBO();
                RolesData obj = new RolesData();
                foreach (GridViewRow row in Gvpagemanager.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label PageID = (Label)Gvpagemanager.Rows[row.RowIndex].Cells[0].FindControl("lbl_sitemapID");
                    CheckBox chk = (CheckBox)Gvpagemanager.Rows[row.RowIndex].Cells[0].FindControl("chkselect");
                    Label menuheader = (Label)Gvpagemanager.Rows[row.RowIndex].Cells[0].FindControl("lbl_menuHeader");
                    RolesData ObjDetails = new RolesData();
                    ObjDetails.PageID = Convert.ToInt32(PageID.Text == "" ? "0" : PageID.Text);
                    ObjDetails.PageStatus = chk.Checked ? 1 : 0;

                    //if (menuheader.Text == "1" && chk.Checked == false)
                    //{
                    //    Messagealert_.ShowMessage(lblmessage, "menuheader", 0);
                    //    divmsg1.Attributes["class"] = "FailAlert";
                    //    divmsg1.Visible = true;
                    //    return;
                    //}
                    //else
                    //{
                    //    lblmessage.Visible = false;
                    //}
                   
                    list.Add(ObjDetails);
                }
                obj.XMLData = XmlConvertor.PageDatatoXML(list).ToString();
                obj.LastModifiedBy = LogData.UserLoginId;
                obj.RoleID = Convert.ToInt32(drpRole.SelectedValue == "" ? "0" : drpRole.SelectedValue);

                bool status = useradm.SavePageRole(obj);
                if (status == true)
                {
                    bindsitemaps();
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Attributes["class"] = "SucessAlert";
                    divmsg1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                string msg = ex.ToString();
                Messagealert_.ShowMessage(lblmessage, msg, 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
            }
        }
    }
}