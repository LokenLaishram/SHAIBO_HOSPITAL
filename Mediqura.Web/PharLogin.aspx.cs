using Mediqura.BOL.UserBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web
{
    public partial class PharLogin : System.Web.UI.Page
    {
        static string password;
        DateTime ExpiryDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime Today = DateTime.Today;
            ExpiryDate = Convert.ToDateTime("2019/11/20 12:00:00");
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
            //Set login token in context session as key name/id - LoginToken
            SetLoginToken(objUSerCE);
        }
        private void SetLoginToken(UserData objUser)
        {
            try
            {
                LogData objLoginToken = new LogData();
                objLoginToken.UserLoginId = objUser.objmeduser.LoginID;
                objLoginToken.UserName = objUser.objmeduser.UserName;
                objLoginToken.CompanyID = objUser.objmeduser.CompanyID;
                objLoginToken.FinancialYearID = objUser.objmeduser.FinancialYearID;
                objLoginToken.FinancialYearCode = objUser.objmeduser.FinancialYearCode;
                objLoginToken.FinacialYear = objUser.objmeduser.FinacialYear;
                objLoginToken.RoleID = objUser.objmeduser.RoleID;
                objLoginToken.RoleName = objUser.objmeduser.RoleName;
                objLoginToken.HospitalID = objUser.objmeduser.HospitalID;
                objLoginToken.EmployeeID = objUser.objmeduser.EmployeeID;
                objLoginToken.ServerDateTime = objUser.objmeduser.ServerDateTime;
                String myIP = Commonfunction.GetClientIPAddress();
                objLoginToken.IPaddress = myIP.ToString();
                objLoginToken.isNotify = objUser.objmeduser.isNotify;
                objLoginToken.DashBoardUrl = objUser.DashBoardUrl;
                objLoginToken.FPData = objUser.FPData;
                objLoginToken.BillSetting = objUser.BillSetting;
                Session["LoginToken"] = objLoginToken;

                Session["LoginID"] = objUser.objmeduser.EmployeeID;
                if (password == "123456")
                {
                    Session["pass"] = "123456";
                    Response.Redirect("~/PasswordChange.aspx");
                }
                else
                {
                    Response.Redirect("~/" + objLoginToken.DashBoardUrl, false);
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