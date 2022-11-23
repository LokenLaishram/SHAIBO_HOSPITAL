using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Drawing.Charts;
using Mediqura.BOL.UserBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System.Net;
using Mediqura.Web.MedCommon;
using System.Web.Security;

namespace Mediqura.Web
{
    public partial class Login : System.Web.UI.Page
    {
        static string password;
        DateTime ExpiryDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            DateTime Today = DateTime.Today;
            ExpiryDate = Convert.ToDateTime("2021/11/20 12:00:00");
            if ((ExpiryDate - Today).TotalDays <= 15)
            {
                lblmessage.Visible = true;
                lblmessage.Text = "Mediqura Licence only: " + Math.Ceiling((ExpiryDate - Today).TotalDays) + " days remaining";
                lblmessage.CssClass = "MessageFailed";
            }
        }
        protected void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                LogData objCreateUser = new LogData();
                UserBO objCreateUserBO = new UserBO();
                if (txtusername.Text.Trim() != "" && txtpassword.Text.Trim() != "")
                {
                    objCreateUser.UserName = txtusername.Text;
                    objCreateUser.UserPassword = txtpassword.Text;
                    password = txtpassword.Text;
                    objCreateUser.ActionType = Enumaction.Insert;
                    UserData result = new UserData();
                    DateTime Today = DateTime.Today;
                    if ((ExpiryDate - Today).TotalDays <= 0)
                    {
                        lblmessage.Visible = true;
                        lblmessage.Text = "!Mediqura has expired on " + ExpiryDate + ". Please contact to Mediqura support team for licence renewal.";
                        lblmessage.CssClass = "MessageFailed";
                        return;
                    }
                    String myIP = Commonfunction.GetClientIPAddress();
                    objCreateUser.IPaddress = myIP.ToString();

                    result = objCreateUserBO.getLogData(objCreateUser);
                    if (result != null)
                    {
                        CreateAuthenticationTicket(result);

                    }
                    else
                    {
                        lblmessage.Visible = true;
                        lblmessage.Text = "Wrong credential entered or deactivated.Please try again.";
                        lblmessage.CssClass = "MessageFailed";
                    }
                }
            }
            catch (Exception ex) //Exception of the layer(itself)/unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
            }
        }
        private void CreateAuthenticationTicket(UserData objUSerCE)
        {
            //string RolesAssigntoUser = "";
            //foreach (RolesData objUGrp in objUSerCE.RoleList)
            //{
            //    if (RolesAssigntoUser.Trim() != "")
            //    {
            //        RolesAssigntoUser = RolesAssigntoUser + "," + Convert.ToString(objUGrp.RoleName);
            //    }
            //    else
            //    {
            //        RolesAssigntoUser = Convert.ToString(objUGrp.RoleName);
            //    }
            //}
            //bool isPersistentLogin = false;
            //// Create forms authentication ticket
            //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
            //1, // Ticket version
            //objUSerCE.objmeduser.UserName,// Username to be associated with this ticket
            //DateTime.Now, // Date/time ticket was issued
            //DateTime.Now.AddMinutes(50), // Date and time the cookie will expire
            //isPersistentLogin, // if user has chcked rememebr me then create persistent cookie
            //RolesAssigntoUser, // store the user data, in this case roles of the user
            //FormsAuthentication.FormsCookiePath); // Cookie path specified in the web.config file in <Forms> tag if any.
            //// To give more security it is suggested to hash it
            //string hashCookies = FormsAuthentication.Encrypt(ticket);
            //HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashCookies); // Hashed ticket
            //// Add the cookie to the response, user browser
            //Response.Cookies.Add(cookie);
            //// Set login token in context session as key name/id - LoginToken
            SetLoginToken(objUSerCE);
        }
        private void SetLoginToken(UserData objUser)
        {
            try
            {
                LogData objLoginToken = new LogData();
                objLoginToken.UserLoginId = objUser.objmeduser.LoginID;
                objLoginToken.UserName = objUser.objmeduser.UserName;
                objLoginToken.UserPassword = objUser.objmeduser.UserPassword;
                objLoginToken.CompanyID = objUser.objmeduser.CompanyID;
                objLoginToken.FinancialYearID = objUser.objmeduser.FinancialYearID;
                objLoginToken.FinancialYearCode = objUser.objmeduser.FinancialYearCode;
                objLoginToken.FinacialYear = objUser.objmeduser.FinacialYear;
                objLoginToken.RoleID = objUser.objmeduser.RoleID;
                objLoginToken.DesignationID = objUser.objmeduser.DesignationID;
                objLoginToken.RoleName = objUser.objmeduser.RoleName;
                objLoginToken.HospitalID = objUser.objmeduser.HospitalID;
                objLoginToken.EmployeeID = objUser.objmeduser.EmployeeID;
                objLoginToken.ServerDateTime = objUser.objmeduser.ServerDateTime;
                String myIP = Commonfunction.GetClientIPAddress();
                objLoginToken.IPaddress = myIP.ToString();
                objLoginToken.isNotify = objUser.objmeduser.isNotify;
                objLoginToken.DashBoardUrl = objUser.DashBoardUrl;
                objLoginToken.PhrPatientlistdUrl = objUser.objmeduser.PhrPatientlistdUrl;
                objLoginToken.FPData = objUser.FPData;
                objLoginToken.BillSetting = objUser.BillSetting;
                objLoginToken.GenSubStockID = objUser.objmeduser.GenSubStockID;
                objLoginToken.GenItemApproveEnable = objUser.objmeduser.GenItemApproveEnable;
                objLoginToken.GenItemRequestEnable = objUser.objmeduser.GenItemRequestEnable;
                objLoginToken.GenItemHandoverEnable = objUser.objmeduser.GenItemHandoverEnable;
                objLoginToken.GenItemVerifyEnable = objUser.objmeduser.GenItemVerifyEnable;
                objLoginToken.MedSubStockID = objUser.objmeduser.MedSubStockID;
                objLoginToken.MedItemApproveEnable = objUser.objmeduser.MedItemApproveEnable;
                objLoginToken.MedItemRequestEnable = objUser.objmeduser.MedItemRequestEnable;
                objLoginToken.MedItemHandoverEnable = objUser.objmeduser.MedItemHandoverEnable;
                objLoginToken.MedItemVerifyEnable = objUser.objmeduser.MedItemVerifyEnable;
                objLoginToken.LeaveApproveEnable = objUser.objmeduser.LeaveApproveEnable;
                objLoginToken.LeaveRequestEnable = objUser.objmeduser.LeaveRequestEnable;
                objLoginToken.RosterUpdateEnable = objUser.objmeduser.RosterUpdateEnable;
                objLoginToken.RosterChangeApproveEnable = objUser.objmeduser.RosterChangeApproveEnable;
                objLoginToken.RosterChangeRequestEnable = objUser.objmeduser.RosterChangeRequestEnable;
                objLoginToken.DepartmentID = objUser.objmeduser.DepartmentID;
                objLoginToken.IsActiveLogin = objUser.objmeduser.IsActiveLogin;
                objLoginToken.EnableMultiLogin = objUser.objmeduser.EnableMultiLogin;
                objLoginToken.IPaddress = objUser.objmeduser.IPaddress;
                objLoginToken.DepartmentName = objUser.objmeduser.DepartmentName;
                Session["LoginToken"] = objLoginToken;
                Session["LoginID"] = objUser.objmeduser.EmployeeID;
                if (objUser.objmeduser.EnableMultiLogin == 0)
                {
                    if (objUser.objmeduser.IsActiveLogin == 1 && Commonfunction.GetClientIPAddress() != objUser.objmeduser.IPaddress)
                    {
                        lblmessage.Visible = true;
                        lblmessage.Text = "User is currently active at another system of IP//" + objUser.objmeduser.IPaddress + ". So please logout and login again.";
                        lblmessage.CssClass = "MessageFailed";
                    }
                    if (objUser.objmeduser.IsActiveLogin == 1 && Commonfunction.GetClientIPAddress() == objUser.objmeduser.IPaddress)
                    {
                        LogData ObjLogdata = new LogData();
                        UserBO LogdataBO = new UserBO();
                        ObjLogdata.IPaddress = Commonfunction.GetClientIPAddress();
                        ObjLogdata.IsActiveLogin = 1;
                        ObjLogdata.UserName = objUser.objmeduser.UserName;
                        ObjLogdata.UserPassword = objUser.objmeduser.UserPassword;
                        ObjLogdata.RoleID = objUser.objmeduser.RoleID;
                        bool result = LogdataBO.UpdateLoginStatus(ObjLogdata);
                        if (result == true)
                        {
                            objLoginToken.IsActiveLogin = 1;
                            Response.Redirect("~/" + objLoginToken.DashBoardUrl, false);
                        }
                    }
                    if (objUser.objmeduser.IsActiveLogin == 0)
                    {
                        if (password == "123456")
                        {
                            Session["pass"] = "123456";
                            Response.Redirect("~/PasswordChange.aspx");
                        }
                        else
                        {
                            LogData ObjLogdata = new LogData();
                            UserBO LogdataBO = new UserBO();
                            ObjLogdata.IPaddress = Commonfunction.GetClientIPAddress();
                            ObjLogdata.IsActiveLogin = 1;
                            ObjLogdata.UserName = objUser.objmeduser.UserName;
                            ObjLogdata.UserPassword = objUser.objmeduser.UserPassword;
                            ObjLogdata.RoleID = objUser.objmeduser.RoleID;
                            bool result = LogdataBO.UpdateLoginStatus(ObjLogdata);
                            if (result == true)
                            {
                                objLoginToken.IsActiveLogin = 1;
                                Response.Redirect("~/" + objLoginToken.DashBoardUrl, false);

                            }
                        }
                    }
                }
                if (objUser.objmeduser.EnableMultiLogin == 1)
                {
                    if (password == "123456")
                    {
                        Session["pass"] = "123456";
                        Response.Redirect("~/PasswordChange.aspx");
                    }
                    else
                    {
                        //Response.Redirect("~/CurrentPatientlist.aspx", false);
                       // Response.Redirect("~/" + objLoginToken.PhrPatientlistdUrl, false);
                        Response.Redirect("~/" + objLoginToken.DashBoardUrl, false);
                    }
                }
                if (objUser.objmeduser.MedSubStockID == 2)
                {
                    bool result = Commonfunction.Clear_PHR_Uncompletetransactions(objUser.objmeduser.FinancialYearID, objUser.objmeduser.EmployeeID, objUser.objmeduser.MedSubStockID);
                    if (result == false)
                    {
                        Response.Redirect("~/Login.aspx", false);
                    }
                }
            }
            catch (Exception ex) //Exception of the layer(itself)/unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
            }
        }
        private void ScheduleLogin()
        {
            LogData objUser = new LogData();
            UserBO objCreateUserBO = new UserBO();
            objUser.LoginDate = DateTime.Now.ToShortDateString();
            objUser.LoginTime = DateTime.Now.ToShortTimeString();
            objUser.ActionType = Enumaction.Insert;
            LogData objCreateUser = objCreateUserBO.ScheduleLogin(objUser);

        }
    }
}