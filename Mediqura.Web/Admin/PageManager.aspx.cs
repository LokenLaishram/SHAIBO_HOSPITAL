using Mediqura.BOL.CommonBO;
using Mediqura.BOL.UserBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Utility.Util;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.Admin
{

    public partial class PageManager : BasePage
    {
        System.Data.DataSet dsSiteMap;
        System.Data.DataTable applySitemapRoleDT;
        int roleID;
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                SiteMapData objSiteMap = new SiteMapData();
                MasterLookupBO mstlookup = new MasterLookupBO();
                GetAllSiteMapItem(objSiteMap);
                CreateSiteMapItem();
                if (!Page.IsPostBack)
                {
                    BindDropDownControl(drpRole, mstlookup.GetLookupsList(LookupName.Roles));
                    roleID = Convert.ToInt32(drpRole.SelectedValue);
                    GetAppliedSiteMapForSelectedRole();
                    ViewState["IsEdit"] = false;
                }
                else
                {
                    roleID = Convert.ToInt32(drpRole.SelectedValue);
                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblError.Text = ExceptionMessage.GetMessage(ex);
                lblError.Visible = true;
            }
        }
        protected void PagePermission()
        {
            if (LogData != null)
            {
                btnSubmit.Enabled = LogData.IsAddOn;
            }
        }
        protected void drpRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError.Visible = true;
            //get applied sitemap for selected role
            GetAppliedSiteMapForSelectedRole();

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if ((bool)ViewState["IsEdit"] == true && !LogData.IsModify)
            {
                lblError.Text = "You can not modify the records.";
                return;

            }

            try
            {
                StringBuilder objBuilder = GetCheckedSitemap();
                RolesData objRole = new RolesData();
                objRole.AddedBy = LogData.AddedBy;
                objRole.LastModifiedBy = LogData.UserLoginId;
                objRole.RoleID = roleID;
                objRole.XMLData = objBuilder.ToString();
                UserBO useradm = new UserBO();
                bool status = useradm.SaveSiteMapRole(objRole);
                if (status == true)
                {

                    lblError.Text = "User page permission has been updated.";
                    lblError.CssClass = "MsgSuccess";
                    lblError.Visible = true;
                    ViewState["IsEdit"] = false;
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblError.Text = ExceptionMessage.GetMessage(ex);
                lblError.CssClass = "MessageFailed";
                lblError.Visible = true;
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            GetAppliedSiteMapForSelectedRole();
            lblError.Text = "";
            drpRole.SelectedIndex = 0;
            lblError.Visible = true;
            ViewState["IsEdit"] = false;

        }
        private bool IsSiteMapCheched(string siteMapID)
        {
            DataRow[] filterRows = applySitemapRoleDT.Select("SiteMapID=" + siteMapID, "");
            if (filterRows.Length > 0)
                return true;
            else
                return false;
        }
        private void BindDropDownControl(DropDownList objDD, List<LookupItem> lstLookups)
        {
            objDD.DataSource = lstLookups;
            objDD.DataValueField = "ItemId";
            objDD.DataTextField = "ItemName";
            objDD.DataBind();
        }
        private void GetAllSiteMapItem(SiteMapData objSiteMap)
        {
            //Call service operation to get data from database source

            UserBO useradm = new UserBO();

            List<SiteMapData> lstSiteMap = useradm.GetAllSiteMapItem(objSiteMap);
            System.Data.DataTable dt = ORHelper<SiteMapData>.GenericListToDataTable(lstSiteMap);
            dsSiteMap = new System.Data.DataSet();
            dsSiteMap.Tables.Add(dt);
        }
        private void GetAppliedSiteMapForSelectedRole()
        {
            RolesData objRole = new RolesData();
            UserBO useradm = new UserBO();
            objRole.RoleID = roleID;
            List<SiteMapData> lstSitemap = useradm.GetSiteMapRoleByRoleID(objRole);
            applySitemapRoleDT = ORHelper<SiteMapData>.GenericListToDataTable(lstSitemap);

            AppliedRoleSetting();
        }
        private void CreateSiteMapItem()
        {
            tblPermission.Controls.Clear();

            TableRow r1;
            TableCell c1, c2, c3, c4, c5;
            CheckBox chk1;

            DataRow[] filterRows1 = dsSiteMap.Tables[0].Select("ParentID=0", "");
            foreach (DataRow row1 in filterRows1)
            {
                //For Level 1
                r1 = new TableRow();
                c1 = new TableCell();
                c1.ColumnSpan = 5;

                chk1 = new CheckBox();
                chk1.ID = "chk" + row1["SiteMapID"];
                chk1.Text = (string)row1["Title"];
                //chk1.Checked = IsSiteMapCheched(row1["SiteMapID"].ToString());
                c1.Controls.Add(chk1);

                r1.Cells.Add(c1);

                tblPermission.Rows.Add(r1);

                DataRow[] filterRows2 = dsSiteMap.Tables[0].Select("ParentID=" + row1["SiteMapID"], "");
                foreach (DataRow row2 in filterRows2)
                {
                    //For Level 2
                    r1 = new TableRow();
                    c1 = new TableCell();
                    c2 = new TableCell();
                    c2.ColumnSpan = 4;

                    chk1 = new CheckBox();
                    chk1.ID = "chk" + row2["SiteMapID"];
                    chk1.Text = (string)row2["Title"];
                    // chk1.Checked = IsSiteMapCheched(row2["SiteMapID"].ToString());
                    c2.Controls.Add(chk1);

                    r1.Cells.Add(c1);
                    r1.Cells.Add(c2);

                    tblPermission.Rows.Add(r1);

                    DataRow[] filterRows3 = dsSiteMap.Tables[0].Select("ParentID=" + row2["SiteMapID"], "");
                    foreach (DataRow row3 in filterRows3)
                    {

                        r1 = new TableRow();
                        c1 = new TableCell();
                        c2 = new TableCell();
                        c3 = new TableCell();
                        c3.ColumnSpan = 3;

                        chk1 = new CheckBox();
                        chk1.ID = "chk" + row3["SiteMapID"];
                        chk1.Text = (string)row3["Title"];

                        c3.Controls.Add(chk1);

                        r1.Cells.Add(c1);
                        r1.Cells.Add(c2);
                        r1.Cells.Add(c3);

                        tblPermission.Rows.Add(r1);

                        DataRow[] filterRows4 = dsSiteMap.Tables[0].Select("ParentID=" + row3["SiteMapID"], "");
                        foreach (DataRow row4 in filterRows4)
                        {
                            //For Level 4
                            r1 = new TableRow();
                            c1 = new TableCell();
                            c2 = new TableCell();
                            c3 = new TableCell();
                            c4 = new TableCell();
                            c4.ColumnSpan = 2;

                            c1.Width = 20;
                            c2.Width = 20;
                            c3.Width = 20;
                            c4.Width = 20;

                            chk1 = new CheckBox();
                            chk1.ID = "chk" + row4["SiteMapID"];
                            chk1.Text = (string)row4["Title"];

                            c4.Controls.Add(chk1);

                            r1.Cells.Add(c1);
                            r1.Cells.Add(c2);
                            r1.Cells.Add(c3);
                            r1.Cells.Add(c4);

                            tblPermission.Rows.Add(r1);

                            DataRow[] filterRows5 = dsSiteMap.Tables[0].Select("ParentID=" + row4["SiteMapID"], "");
                            foreach (DataRow row5 in filterRows5)
                            {

                                r1 = new TableRow();
                                c1 = new TableCell();
                                c2 = new TableCell();
                                c3 = new TableCell();
                                c4 = new TableCell();
                                c5 = new TableCell();

                                c1.Width = 20;
                                c2.Width = 20;
                                c3.Width = 20;
                                c4.Width = 20;
                                c5.Width = 20;

                                chk1 = new CheckBox();
                                chk1.ID = "chk" + row5["SiteMapID"];
                                chk1.Text = (string)row5["Title"];


                                c5.Controls.Add(chk1);

                                r1.Cells.Add(c1);
                                r1.Cells.Add(c2);
                                r1.Cells.Add(c3);
                                r1.Cells.Add(c4);
                                r1.Cells.Add(c5);

                                tblPermission.Rows.Add(r1);
                            }
                        }
                    }
                }
            }
        }
        private StringBuilder GetCheckedSitemap()
        {
            CheckBox chk1;

            StringBuilder objBuilder = new StringBuilder();
            objBuilder.Append("<?xml version=\"1.0\"?>");
            objBuilder.Append("<Root>");

            DataRow[] filterRows1 = dsSiteMap.Tables[0].Select("ParentID=0", "");
            foreach (DataRow row1 in filterRows1)
            {
                //For Level 1     
                chk1 = (CheckBox)tblPermission.FindControl("chk" + row1["SiteMapID"]);
                if (chk1 != null)
                {
                    if (chk1.Checked)
                    {
                        objBuilder.Append("<sitemaprole roleid=\"" + roleID + "\" sitemapid=\"" + row1["SiteMapID"] + "\"/>");
                    }
                }

                DataRow[] filterRows2 = dsSiteMap.Tables[0].Select("ParentID=" + row1["SiteMapID"], "");
                foreach (DataRow row2 in filterRows2)
                {
                    //For Level 2     
                    chk1 = (CheckBox)tblPermission.FindControl("chk" + row2["SiteMapID"]);
                    if (chk1 != null)
                    {
                        if (chk1.Checked)
                        {
                            objBuilder.Append("<sitemaprole roleid=\"" + roleID + "\" sitemapid=\"" + row2["SiteMapID"] + "\"/>");
                        }
                    }

                    DataRow[] filterRows3 = dsSiteMap.Tables[0].Select("ParentID=" + row2["SiteMapID"], "");
                    foreach (DataRow row3 in filterRows3)
                    {
                        //For Level 3
                        chk1 = (CheckBox)tblPermission.FindControl("chk" + row3["SiteMapID"]);
                        if (chk1 != null)
                        {
                            if (chk1.Checked)
                            {
                                objBuilder.Append("<sitemaprole roleid=\"" + roleID + "\" sitemapid=\"" + row3["SiteMapID"] + "\"/>");
                            }
                        }

                        DataRow[] filterRows4 = dsSiteMap.Tables[0].Select("ParentID=" + row3["SiteMapID"], "");
                        foreach (DataRow row4 in filterRows4)
                        {
                            //For Level 4
                            chk1 = (CheckBox)tblPermission.FindControl("chk" + row4["SiteMapID"]);
                            if (chk1 != null)
                            {
                                if (chk1.Checked)
                                {
                                    objBuilder.Append("<sitemaprole roleid=\"" + roleID + "\" sitemapid=\"" + row4["SiteMapID"] + "\"/>");
                                }
                            }

                            DataRow[] filterRows5 = dsSiteMap.Tables[0].Select("ParentID=" + row4["SiteMapID"], "");
                            foreach (DataRow row5 in filterRows5)
                            {
                                //For Level 5
                                chk1 = (CheckBox)tblPermission.FindControl("chk" + row5["SiteMapID"]);
                                if (chk1 != null)
                                {
                                    if (chk1.Checked)
                                    {
                                        objBuilder.Append("<sitemaprole roleid=\"" + roleID + "\" sitemapid=\"" + row5["SiteMapID"] + "\"/>");
                                    }
                                }
                            }
                        }
                    }
                }
            }

            objBuilder.Append("</Root>");

            return objBuilder;
        }
        private void AppliedRoleSetting()
        {
            CheckBox chk1;
            DataRow[] filterRows1 = dsSiteMap.Tables[0].Select("ParentID=0", "");
            foreach (DataRow row1 in filterRows1)
            {
                //For Level 1     
                chk1 = (CheckBox)tblPermission.FindControl("chk" + row1["SiteMapID"]);
                if (chk1 != null)
                {
                    chk1.Checked = IsSiteMapCheched(row1["SiteMapID"].ToString());
                }

                DataRow[] filterRows2 = dsSiteMap.Tables[0].Select("ParentID=" + row1["SiteMapID"], "");
                foreach (DataRow row2 in filterRows2)
                {
                    //For Level 2     
                    chk1 = (CheckBox)tblPermission.FindControl("chk" + row2["SiteMapID"]);
                    if (chk1 != null)
                    {
                        chk1.Checked = IsSiteMapCheched(row2["SiteMapID"].ToString());
                    }

                    DataRow[] filterRows3 = dsSiteMap.Tables[0].Select("ParentID=" + row2["SiteMapID"], "");
                    foreach (DataRow row3 in filterRows3)
                    {
                        //For Level 3
                        chk1 = (CheckBox)tblPermission.FindControl("chk" + row3["SiteMapID"]);
                        if (chk1 != null)
                        {
                            chk1.Checked = IsSiteMapCheched(row3["SiteMapID"].ToString());
                        }

                        DataRow[] filterRows4 = dsSiteMap.Tables[0].Select("ParentID=" + row3["SiteMapID"], "");
                        foreach (DataRow row4 in filterRows4)
                        {
                            //For Level 4
                            chk1 = (CheckBox)tblPermission.FindControl("chk" + row4["SiteMapID"]);
                            if (chk1 != null)
                            {
                                chk1.Checked = IsSiteMapCheched(row4["SiteMapID"].ToString());
                            }

                            DataRow[] filterRows5 = dsSiteMap.Tables[0].Select("ParentID=" + row4["SiteMapID"], "");
                            foreach (DataRow row5 in filterRows5)
                            {
                                //For Level 5
                                chk1 = (CheckBox)tblPermission.FindControl("chk" + row5["SiteMapID"]);
                                if (chk1 != null)
                                {
                                    chk1.Checked = IsSiteMapCheched(row5["SiteMapID"].ToString());
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}