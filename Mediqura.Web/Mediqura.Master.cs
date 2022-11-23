using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mediqura.CommonData.LoginData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.BOL.CommonBO;
using Mediqura.Utility.Util;
using Mediqura.CommonData.Common;
using Mediqura.BOL.MedBillBO;
using Mediqura.CommonData.MedBillData;
using Mediqura.BOL.UserBO;
using Mediqura.Web.MedCommon;
using Mediqura.CommonData.MedStore;
using Mediqura.BOL.MedStore;

namespace Mediqura.Web
{
    public partial class Mediqura : System.Web.UI.MasterPage
    {
        LogData objLoginToken;
        DataTable Menus = new DataTable();
        string baseurl;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                objLoginToken = (LogData)Session["LoginToken"];
                if (objLoginToken != null)
                {

                    lbl_activeuser.Text = "Welcome, " + objLoginToken.UserName;
                    if (objLoginToken.DepartmentID == 47)
                    {
                        lbl_ActiveDept.Visible = true;
                        lbl_ActiveDept.Text = objLoginToken.DepartmentName.ToString();
                    }
                    else
                    {
                        lbl_ActiveDept.Visible = false;
                    }
                    lbl_notify.Value = objLoginToken.isNotify.ToString();
                    lbl_Role.Value = objLoginToken.RoleName.ToString();
                    lbluser.Value = objLoginToken.UserName;
                    phrrequestenable.Value = objLoginToken.MedItemRequestEnable.ToString();
                    phrapproveenable.Value = objLoginToken.MedItemApproveEnable.ToString();
                    Dept.Value = objLoginToken.DepartmentID.ToString();
                    txtNickName.Value = objLoginToken.UserName + "(" + objLoginToken.RoleName + ")";
                    AutoCompleteExtender1.ContextKey = objLoginToken.RoleID.ToString();
                    baseurl = Request.Url.GetLeftPart(UriPartial.Authority);
                    lblFinancial.Text = objLoginToken.FinacialYear.ToString();
                    BindMenu();
                    GetControll_Enable();

                    int ID = 0;

                    if (objLoginToken.MedItemApproveEnable == 1)
                    {
                        massupdate.Visible = true;

                        List<MedIndentData> Indent = GetPHRnotification();
                        if (Indent.Count > 0)
                        {
                            int count = Indent[0].MaximumRows;
                            if (count == 0)
                            {
                                n_count.Value = "";
                            }
                            else
                            {
                                StringBuilder UIstring = new StringBuilder();
                                for (int i = 0; i < Indent.Count; i++)
                                {
                                    string temp = " <li>" +
                                                   "<a onClick=\"openPhrAlert('" + Indent[i].IndentNo + "','" + Indent[i].phrbillno + "','" + Indent[i].phrreqtype + "')\" ><span>" + Indent[i].ReqBy + "</span>" +
                                                    "<span style='position:absolute !important' class='time'>" + Indent[i].AlertTime + "</span>" +
                                                   "<span class='message'>" + Indent[i].Remarks +
                                                   "</span> </a ></li > ";



                                    UIstring.Append(temp);
                                }
                                String ending = " <li>" +
                                                "<div class=\"text-center\">" +
                                                    "<a href=\"../medPhr/IndentApproval.aspx\">" +
                                                       "<strong>See All Alerts</strong>" +
                                                        "<i class=\"fa fa-angle-right\"></i>" +
                                                    "</a>" +
                                                "</div>" +
                                           " </li>";
                                UIstring.Append(ending);
                                lit_alert.Text = UIstring.ToString();
                                n_count.Value = count.ToString();
                            }
                        }
                    }

                    if (objLoginToken.isNotify == 1)
                    {
                        massupdate.Visible = true;
                        ID = 0;
                        List<DiscountCount> discount = GetAllnotification(ID);
                        if (discount.Count > 0)
                        {
                            int count = discount[0].MaximumRows;
                            if (count == 0)
                            {
                                n_count.Value = "";
                            }
                            else
                            {
                                StringBuilder UIstring = new StringBuilder();
                                for (int i = 0; i < discount.Count; i++)
                                {
                                    string temp = " <li>" +
                                                   "<a onClick=\"openAlert('" + discount[i].ID + "','" + discount[i].BillType + "','" + discount[i].serviceType + "')\" ><span>" + discount[i].ReqBy + "</span>" +
                                                    "<span style='position:absolute !important' class='time'>" + discount[i].AlertTime + "</span>" +
                                                   "<span class='message'>" + discount[i].Remarks +
                                                   "</span> </a ></li > ";



                                    UIstring.Append(temp);
                                }
                                String ending = " <li>" +
                                                "<div class=\"text-center\">" +
                                                    "<a href=\"../Admin/DiscountApproval.aspx\">" +
                                                       "<strong>See All Alerts</strong>" +
                                                        "<i class=\"fa fa-angle-right\"></i>" +
                                                    "</a>" +
                                                "</div>" +
                                           " </li>";
                                UIstring.Append(ending);
                                lit_alert.Text = UIstring.ToString();
                                n_count.Value = count.ToString();
                            }
                        }
                    }
                    else if (objLoginToken.RoleID == 3)
                    {
                        massupdate.Visible = false;
                        ID = 3;
                        List<DiscountCount> discount = GetAllnotification(ID);
                        if (discount.Count > 0)
                        {
                            int count = discount[0].MaximumRows;
                            if (count == 0)
                            {
                                n_count.Value = "";
                            }
                            else
                            {
                                StringBuilder UIstring = new StringBuilder();
                                for (int i = 0; i < discount.Count; i++)
                                {
                                    string temp = " <li>" +
                                                   "<a onClick=\"openAlert('" + discount[i].ID + "','" + discount[i].BillType + "','" + discount[i].serviceType + "')\" ><span>" + discount[i].ReqBy + "</span>" +
                                                    "<span style='position:absolute !important' class='time'>" + discount[i].AlertTime + "</span>" +
                                                   "<span class='message'>" + discount[i].Remarks +
                                                   "</span> </a ></li > ";



                                    UIstring.Append(temp);
                                }
                                String ending = " <li>" +
                                                "<div class=\"text-center\">" +
                                                    "<a href=\"../MedBills/DiscountRequest.aspx\">" +
                                                       "<strong>See All Alerts</strong>" +
                                                        "<i class=\"fa fa-angle-right\"></i>" +
                                                    "</a>" +
                                                "</div>" +
                                           " </li>";
                                UIstring.Append(ending);
                                lit_alert.Text = UIstring.ToString();
                                n_count.Value = count.ToString();
                            }
                        }
                    }
                    else
                    {
                        massupdate.Visible = false;
                    }

                }

                else
                {
                    Response.Redirect("~/LabLogin.aspx", false);
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex);
            }
            Chat_filiupload_Click();
        }
        protected void rptMenu_OnItemBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    if (Menus != null)
                    {
                        DataRowView drv = e.Item.DataItem as DataRowView;
                        string ID = drv["SiteMapID"].ToString();
                        string Title = drv["Title"].ToString();

                        DataRow[] rows = Menus.Select("ParentID=" + ID);
                        if (rows.Length > 0)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append("<ul class='nav child_menu'>");
                            foreach (var item in rows)
                            {

                                string parentId = item["SiteMapID"].ToString();
                                string parentTitle = item["Title"].ToString();
                                string url = baseurl + '/' + item["Url"].ToString();

                                DataRow[] parentRow = Menus.Select("ParentID=" + parentId);

                                if (parentRow.Count() > 0)
                                {
                                    sb.Append("<li class='menu_style_sub_menu'><a href='" + url + "'> " + parentTitle + " <span class='fa fa-chevron-down'></span></a>");
                                    sb.Append("</li>");
                                }
                                else
                                {

                                    sb.Append("<li class='menu_style_sub_menu'><a href='" + url + "'> " + parentTitle + "</a>");
                                    sb.Append("</li>");
                                }
                                sb = CreateChild(sb, parentId, parentTitle, parentRow);
                            }
                            sb.Append("</ul>");
                            (e.Item.FindControl("ltrlSubMenu") as Literal).Text = sb.ToString();
                        }
                    }
                }
            }
        }
        private StringBuilder CreateChild(StringBuilder sb, string parentId, string parentTitle, DataRow[] parentRows)
        {
            if (parentRows.Length > 0)
            {
                sb.Append("<ul class='nav child_menu'>");
                foreach (var item in parentRows)
                {
                    string childId = item["SiteMapID"].ToString();
                    string childTitle = item["Title"].ToString();
                    string childUrl = baseurl + '/' + item["Url"].ToString();
                    DataRow[] childRow = Menus.Select("ParentID=" + childId);

                    if (childRow.Count() > 0)
                    {
                        sb.Append("<li class='menu_style_sub_menu'><a href='" + childUrl + "'> " + childTitle + "</a>");
                        sb.Append("</li>");
                    }
                    else
                    {
                        sb.Append("<li class='menu_style_sub_menu'><a href='" + childUrl + "'> " + childTitle + "</a>");
                        sb.Append("</li>");
                    }
                    CreateChild(sb, childId, childTitle, childRow);
                }
                sb.Append("</ul>");

            }
            return sb;
        }
        private void BindMenu()
        {
            Menus = GetData();
            List<SiteMapData> objSiteMap = ORHelper<SiteMapData>.FromDataTableToList(Menus);

            DataView view = new DataView(Menus);
            view.RowFilter = "ParentID=0";
            this.rptCategories.DataSource = view;
            this.rptCategories.DataBind();

            List<SiteMapData> List = new List<SiteMapData>();
            List = objSiteMap;
            Session["SiteMap"] = List;

        }
        private DataTable GetData()
        {
            DataTable dt = new DataTable();
            string constr = ConfigurationManager.ConnectionStrings["SqlConnectionString11"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_CMS_Adm_GetAllSiteMaps";
                        cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = objLoginToken.RoleID;
                        cmd.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = objLoginToken.EmployeeID;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }
                return dt;
            }
        }
        private List<DiscountCount> GetAllnotification(int ID)
        {
            DiscountBO objBo = new DiscountBO();
            List<DiscountCount> discountData = new List<DiscountCount>();
            return discountData = objBo.GetDiscountNotification(ID);
        }
        private List<MedIndentData> GetPHRnotification()
        {
            MedStoreIndentBO objBo = new MedStoreIndentBO();
            List<MedIndentData> indentData = new List<MedIndentData>();
            return indentData = objBo.GetIndentnotification();
        }
        private void GetControll_Enable()
        {
            PageControlsData objcontrols = new PageControlsData();
            PageControlBO objcontrolBO = new PageControlBO();
            List<PageControlsData> controllist = new List<PageControlsData>();
            objcontrols.RoleID = objLoginToken.RoleID;
            objcontrols.EmployeeID = objLoginToken.EmployeeID;
            objcontrols.url = Request.Url.AbsolutePath.Remove(0, 1).Trim();
            controllist = objcontrolBO.GetControlEnabledetails(objcontrols);
            if (controllist.Count > 0)
            {
                LogData LoginToken = new LogData();
                LoginToken.UserName = objLoginToken.UserName;
                LoginToken.FinancialYearID = objLoginToken.FinancialYearID;
                LoginToken.FinacialYear = objLoginToken.FinacialYear;
                LoginToken.RoleID = objLoginToken.RoleID;
                LoginToken.isNotify = objLoginToken.isNotify;
                LoginToken.DashBoardUrl = objLoginToken.DashBoardUrl;
                LoginToken.PhrPatientlistdUrl = objLoginToken.PhrPatientlistdUrl;
                LoginToken.FPData = objLoginToken.FPData;
                LoginToken.BillSetting = objLoginToken.BillSetting;
                LoginToken.RoleName = objLoginToken.RoleName;
                LoginToken.AddedBy = objLoginToken.AddedBy;
                LoginToken.HospitalID = objLoginToken.HospitalID;
                LoginToken.EmployeeID = objLoginToken.EmployeeID;
                LoginToken.ServerDateTime = objLoginToken.ServerDateTime;
                LoginToken.GenSubStockID = objLoginToken.GenSubStockID;
                LoginToken.GenItemRequestEnable = objLoginToken.GenItemRequestEnable;
                LoginToken.GenItemApproveEnable = objLoginToken.GenItemApproveEnable;
                LoginToken.GenItemHandoverEnable = objLoginToken.GenItemHandoverEnable;
                LoginToken.GenItemVerifyEnable = objLoginToken.GenItemVerifyEnable;
                LoginToken.MedSubStockID = objLoginToken.MedSubStockID;
                LoginToken.MedItemRequestEnable = objLoginToken.MedItemRequestEnable;
                LoginToken.MedItemApproveEnable = objLoginToken.MedItemApproveEnable;
                LoginToken.MedItemHandoverEnable = objLoginToken.MedItemHandoverEnable;
                LoginToken.MedItemVerifyEnable = objLoginToken.MedItemVerifyEnable;
                LoginToken.LeaveApproveEnable = objLoginToken.LeaveApproveEnable;
                LoginToken.LeaveRequestEnable = objLoginToken.LeaveRequestEnable;
                LoginToken.RosterUpdateEnable = objLoginToken.RosterUpdateEnable;
                LoginToken.RosterChangeApproveEnable = objLoginToken.RosterChangeApproveEnable;
                LoginToken.RosterChangeRequestEnable = objLoginToken.RosterChangeRequestEnable;
                LoginToken.IPaddress = objLoginToken.IPaddress;
                LoginToken.SaveEnable = Convert.ToInt32(controllist[0].SaveEnable);
                LoginToken.UpdateEnable = Convert.ToInt32(controllist[0].UpdateEnable);
                LoginToken.SearchEnable = Convert.ToInt32(controllist[0].SearchEnable);
                LoginToken.EditEnable = Convert.ToInt32(controllist[0].EditEnable);
                LoginToken.DeleteEnable = Convert.ToInt32(controllist[0].DeleteEnable);
                LoginToken.PrintEnable = Convert.ToInt32(controllist[0].PrintEnable);
                LoginToken.ExportEnable = Convert.ToInt32(controllist[0].ExportEnable);
                LoginToken.AmountEnable = Convert.ToInt32(controllist[0].AmountEnable);
                LoginToken.UserPassword = objLoginToken.UserPassword;
                LoginToken.EnableMultiLogin = objLoginToken.EnableMultiLogin;
                LoginToken.IsActiveLogin = objLoginToken.IsActiveLogin;
                LoginToken.DesignationID = objLoginToken.DesignationID;
                LoginToken.DepartmentID = objLoginToken.DepartmentID;
                LoginToken.IPaddress = objLoginToken.IPaddress;
                LoginToken.DepartmentName = objLoginToken.DepartmentName;
                Session["LoginToken"] = LoginToken;
            }
            else
            {
                LogData LoginToken = new LogData();
                LoginToken.UserName = objLoginToken.UserName;
                LoginToken.FinancialYearID = objLoginToken.FinancialYearID;
                LoginToken.FinacialYear = objLoginToken.FinacialYear;
                LoginToken.RoleID = objLoginToken.RoleID;
                LoginToken.AddedBy = objLoginToken.AddedBy;
                LoginToken.isNotify = objLoginToken.isNotify;
                LoginToken.DashBoardUrl = objLoginToken.DashBoardUrl;
                LoginToken.FPData = objLoginToken.FPData;
                LoginToken.BillSetting = objLoginToken.BillSetting;
                LoginToken.RoleName = objLoginToken.RoleName;
                LoginToken.HospitalID = objLoginToken.HospitalID;
                LoginToken.EmployeeID = objLoginToken.EmployeeID;
                LoginToken.ServerDateTime = objLoginToken.ServerDateTime;
                LoginToken.IPaddress = objLoginToken.IPaddress;
                LoginToken.GenSubStockID = objLoginToken.GenSubStockID;
                LoginToken.GenItemRequestEnable = objLoginToken.GenItemRequestEnable;
                LoginToken.GenItemApproveEnable = objLoginToken.GenItemApproveEnable;
                LoginToken.GenItemHandoverEnable = objLoginToken.GenItemHandoverEnable;
                LoginToken.GenItemVerifyEnable = objLoginToken.GenItemVerifyEnable;
                LoginToken.MedSubStockID = objLoginToken.MedSubStockID;
                LoginToken.PhrPatientlistdUrl = objLoginToken.PhrPatientlistdUrl;
                LoginToken.MedItemRequestEnable = objLoginToken.MedItemRequestEnable;
                LoginToken.MedItemApproveEnable = objLoginToken.MedItemApproveEnable;
                LoginToken.MedItemHandoverEnable = objLoginToken.MedItemHandoverEnable;
                LoginToken.MedItemVerifyEnable = objLoginToken.MedItemVerifyEnable;
                LoginToken.LeaveApproveEnable = objLoginToken.LeaveApproveEnable;
                LoginToken.LeaveRequestEnable = objLoginToken.LeaveRequestEnable;
                LoginToken.RosterUpdateEnable = objLoginToken.RosterUpdateEnable;
                LoginToken.RosterChangeApproveEnable = objLoginToken.RosterChangeApproveEnable;
                LoginToken.RosterChangeRequestEnable = objLoginToken.RosterChangeRequestEnable;
                LoginToken.UserPassword = objLoginToken.UserPassword;
                LoginToken.EnableMultiLogin = objLoginToken.EnableMultiLogin;
                LoginToken.IsActiveLogin = objLoginToken.IsActiveLogin;
                LoginToken.SaveEnable = 1;
                LoginToken.UpdateEnable = 1;
                LoginToken.SearchEnable = 1;
                LoginToken.EditEnable = 1;
                LoginToken.DeleteEnable = 1;
                LoginToken.PrintEnable = 1;
                LoginToken.AmountEnable = 1;
                LoginToken.DesignationID = objLoginToken.DesignationID;
                LoginToken.DepartmentID = objLoginToken.DepartmentID;
                LoginToken.IPaddress = objLoginToken.IPaddress;
                LoginToken.DepartmentName = objLoginToken.DepartmentName;
                Session["LoginToken"] = LoginToken;

            }
        }
        protected void txt_Search_TextChanged(object sender, EventArgs e)
        {
            PageControlsData objcontrols = new PageControlsData();
            PageControlBO objcontrolBO = new PageControlBO();
            List<PageControlsData> controllist = new List<PageControlsData>();
            objcontrols.RoleID = objLoginToken.RoleID;

            bool isnumeric = txt_Search.Text.All(char.IsDigit);
            if (isnumeric == false)
            {
                if (txt_Search.Text.Contains(":"))
                {
                    bool isIDnumeric = txt_Search.Text.Substring(txt_Search.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                    objcontrols.PageID = isIDnumeric ? Convert.ToInt32(txt_Search.Text.Contains(":") ? txt_Search.Text.Substring(txt_Search.Text.LastIndexOf(':') + 1) : "0") : 0;
                }
                else
                {
                    txt_Search.Text = "";
                    txt_Search.Focus();
                }
            }
            else
            {
                objcontrols.PageID = 0;
            }
            controllist = objcontrolBO.GetPageurls(objcontrols);
            if (controllist.Count > 0)
            {
                Response.Redirect("~/" + controllist[0].url, false);
            }
            else
            {
                txt_Search.Text = "";
            }
        }
        protected void lnkdashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/" + objLoginToken.DashBoardUrl, false);
        }
        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            try
            {
                LogData ObjLogdata = new LogData();
                UserBO LogdataBO = new UserBO();
                ObjLogdata.IPaddress = "";
                ObjLogdata.IsActiveLogin = 0;
                ObjLogdata.UserName = objLoginToken.UserName;
                ObjLogdata.UserPassword = objLoginToken.UserPassword;
                ObjLogdata.RoleID = objLoginToken.RoleID;
                bool result = LogdataBO.UpdateLoginStatus(ObjLogdata);
                if (result == false)
                {
                    if (objLoginToken.RoleName == "Pharmacy")
                    {
                        Session["SiteMap"] = null;
                        Session["LoginToken"] = null;
                        Response.Redirect("~/PharLogin.aspx", false);
                    }
                    else
                    {
                        Session["SiteMap"] = null;
                        Session["LoginToken"] = null;
                        Response.Redirect("~/LabLogin.aspx", true);
                    }
                    Session.Abandon();
                    string text = txt_Search.Text;
                    Response.ExpiresAbsolute = DateTime.Now;
                    Response.Expires = 0;
                    Response.CacheControl = "no-cache";
                    Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Cache.SetNoStore();
                    System.Web.Security.FormsAuthentication.SignOut();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/LabLogin.aspx", false);

            }
        }
        protected void Chat_filiupload_Click()
        {
            if (IsPostBack && AttachFile.PostedFile != null)
            {
                if (AttachFile.PostedFile.FileName.Length > 0)
                {
                    string baseUrl = Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.UriEscaped);
                    AttachFile.SaveAs(Server.MapPath("~/upload/") + AttachFile.PostedFile.FileName);
                    txtMessage.InnerHtml = "<a href=" + baseUrl + "/upload/" + AttachFile.PostedFile.FileName + " >" + AttachFile.PostedFile.FileName + "</a>";
                }
            }


        }
        protected void link_pswd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PasswordChange.aspx");
        }
        protected void btnChatSend_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LabLogin.aspx", false);
        }
        protected void btnReqSubmit_Click(object sender, EventArgs e)
        {


            if (objLoginToken.RoleID == 3)
            {
                if (lblReqType.Value == "0")
                {

                    switch (lblServiceType.Value)
                    {

                        case "1":
                            Session["BILLID"] = lblReqID.Value;
                            Response.Redirect("~/MedBills/OpdBilling.aspx");
                            break;
                        case "2":
                            Session["BILLID"] = lblReqID.Value;
                            Response.Redirect("~/MedBills/OPLabBilling.aspx");
                            break;
                        case "3":
                            Session["IPNo"] = lblReqID.Value;
                            Response.Redirect("~/MedBills/IPfinalbill.aspx");
                            break;

                        case "4":
                            Session["EMRGNO"] = lblReqID.Value;
                            Response.Redirect("~/MedEmergency/EmergencyFinalBilling.aspx");
                            break;

                        default:
                            Session["BILLID"] = lblReqID.Value;
                            Response.Redirect("~/MedBills/DiscountRequest.aspx");
                            break;

                    }

                }
                else
                {
                    Session["ReqNo"] = lblReqID.Value;
                    Response.Redirect("~/MedBills/DiscountRefund.aspx");
                }
            }
            else
            {
                Session["REQID"] = lblReqID.Value;
                Response.Redirect("~/Admin/DiscountApproval.aspx");
            }
        }
        protected void btnphrReqSubmit_Click(object sender, EventArgs e)
        {


            switch (lblphrreqtype.Value)
            {

                case "1":
                    Session["PHRIndentID"] = lblphrreqID.Value;
                    Response.Redirect("~/MedPhr/IndentRequest.aspx");
                    break;
                case "2":
                    Session["PHRIndentID"] = lblphrreqID.Value;
                    Response.Redirect("~/MedPhr/IndentApproval.aspx");
                    break;
                case "3":
                    Session["ReqNo"] = lblphrreqID.Value;
                    Response.Redirect("~/MedPhr/IndentApproval.aspx");
                    break;

                default:
                    Session["PHRIndentID"] = lblReqID.Value;
                    Response.Redirect("~/~/MedPhr/IndentRequest.aspx");
                    break;

            }

        }
    }
}