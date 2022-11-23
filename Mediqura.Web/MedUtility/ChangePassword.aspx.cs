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

namespace Mediqura.Web.MedUtility
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OnChangingPassword(object sender, LoginCancelEventArgs e)
        {

        }

        protected void btnresetPwd_Click(object sender, EventArgs e)
        {
            try
            {
                LogData objCreateUser = new LogData();
                UserBO objCreateUserBO = new UserBO();
                if (txtusername.Text.Trim() != "" && txt_Oldpassword.Text.Trim() != "" && txt_newPswd.Text.Trim() != "" && txt_confirmed.Text.Trim() != "")
                {
                    objCreateUser.UserName = txtusername.Text;
                    objCreateUser.UserPassword = txt_newPswd.Text;
                    objCreateUser.ActionType = Enumaction.Insert;
                    UserData result = new UserData();
                    DateTime Today = DateTime.Today;
                    DateTime ExpiryDate = Convert.ToDateTime("01/01/2019");
                   
                    IPAddress myIP = Commonfunction.GetIPAddress(Dns.GetHostName());
                    objCreateUser.IPaddress = myIP.ToString();

                    result = objCreateUserBO.getLogDatareset(objCreateUser);
                    
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
            string RolesAssigntoUser = "";
            foreach (RolesData objUGrp in objUSerCE.RoleList)
            {
                if (RolesAssigntoUser.Trim() != "")
                {
                    RolesAssigntoUser = RolesAssigntoUser + "," + Convert.ToString(objUGrp.RoleName);
                }
                else
                {
                    RolesAssigntoUser = Convert.ToString(objUGrp.RoleName);
                }
            }
            bool isPersistentLogin = false;
            // Create forms authentication ticket
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
            1, // Ticket version
            objUSerCE.objmeduser.UserName,// Username to be associated with this ticket
            DateTime.Now, // Date/time ticket was issued
            DateTime.Now.AddMinutes(50), // Date and time the cookie will expire
            isPersistentLogin, // if user has chcked rememebr me then create persistent cookie
            RolesAssigntoUser, // store the user data, in this case roles of the user
            FormsAuthentication.FormsCookiePath); // Cookie path specified in the web.config file in <Forms> tag if any.
            // To give more security it is suggested to hash it
            string hashCookies = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashCookies); // Hashed ticket
            // Add the cookie to the response, user browser
            Response.Cookies.Add(cookie);
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
                objLoginToken.RoleID = objUser.objmeduser.RoleID;
                objLoginToken.HospitalID = objUser.objmeduser.HospitalID;
                objLoginToken.EmployeeID = objUser.objmeduser.EmployeeID;
                objLoginToken.ServerDateTime = objUser.objmeduser.ServerDateTime;
                objLoginToken.IPaddress = objUser.objmeduser.IPaddress;
                Session["LoginToken"] = objLoginToken;
                Response.Redirect("~/Dashboard.aspx", false);
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
            objUser.LoginID = Convert.ToInt16(Session["UserID"]);
            objUser.ActionType = Enumaction.Insert;
            LogData objCreateUser = objCreateUserBO.ScheduleLogin(objUser);
            Session["ScheduleId"] = objCreateUser.scheduleID;
        }
    }
}