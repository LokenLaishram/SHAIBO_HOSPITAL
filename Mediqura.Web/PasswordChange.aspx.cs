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
using System.Text.RegularExpressions;

namespace Mediqura.Web
{
    public partial class PasswordChange : System.Web.UI.Page
    {
        LogData objLoginToken;
        protected void Page_Load(object sender, EventArgs e)
        {
            string Password = txt_Oldpassword.Text;
            txt_Oldpassword.Attributes.Add("value", Password);
            string newpwsd = txt_newPswd.Text;
            txt_newPswd.Attributes.Add("value", newpwsd);
            string confpwsd = txt_confirmed.Text;
            txt_confirmed.Attributes.Add("value", confpwsd);

           
            bindusername();
        }
        protected void bindusername()
        {
           
            objLoginToken = (LogData)Session["LoginToken"];
            if (objLoginToken != null)
            {
                txtusername.Text = objLoginToken.UserName;
            }
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

        protected void btnresetPwd_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtusername.Text.Trim() != "" && txt_Oldpassword.Text.Trim() != "" && txt_newPswd.Text.Trim() != "" && txt_confirmed.Text.Trim() != "")
                {
                   
                    // 
                    string pswd = txt_Oldpassword.Text;
                    ChangePswdData objChpswddata = new ChangePswdData();
                    ChangePswdBO objChpswdBO = new ChangePswdBO();
                    List<ChangePswdData> getResult = new List<ChangePswdData>();
                    objChpswddata.UserName = txt_Oldpassword.Text.Trim() == "" ? "0" : txtusername.Text.Trim();
                    getResult = objChpswdBO.GetPswd(objChpswddata);
                    objLoginToken = (LogData)Session["LoginToken"];
                    if (objLoginToken != null)
                    {
                        if (txt_Oldpassword.Text != getResult[0].oldpassword.ToString())
                        {
                            //ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "alert('Password Does not match with database record.')", true);
                            Messagealert_.ShowMessage(lblmessage, "PasswordError", 0);
                            div1.Visible = true;
                            div1.Attributes["class"] = "FailAlert";
                            txt_Oldpassword.Focus();
                            return;
                        }

                    }
                    if (txt_newPswd.Text.Length < 5)
                    //if (!Regex.IsMatch(txt_newPswd.Text.Trim(), pattern))
                    {
                        Messagealert_.ShowMessage(lblmessage, "PswdStrongType", 0);
                        div1.Attributes["class"] = "FailAlert";
                        div1.Visible = true;
                        txt_newPswd.Focus();
                        return;
                    }
                    if (txt_confirmed.Text != txt_newPswd.Text)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ConfirmedPswd", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_confirmed.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    //ChangePswdData objChpswddata = new ChangePswdData();
                    //ChangePswdBO objChpswdBO = new ChangePswdBO();

                    objChpswddata.UserName = txtusername.Text;
                    objChpswddata.newPassword = txt_newPswd.Text;


                    objChpswddata.ActionType = Enumaction.Insert;
                    if (ViewState["ID"] != null)
                    {
                        objChpswddata.ActionType = Enumaction.Update;
                        objChpswddata.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                    int result = objChpswdBO.getLogDatareset(objChpswddata);  // funtion at DAL
                    if (result == 1 || result == 2)
                    {
                        lblmessage.Visible = true;
                        Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                        div1.Visible = true;
                        div1.Attributes["class"] = "SucessAlert";
                        ViewState["ID"] = null;

                    }
                    else if (result == 5)
                    {
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                    }


                }
            }
            catch (Exception ex) //Exception of the layer(itself)/unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
            }
        }

        
        //protected void txt_Oldpassword_TextChanged(object sender, EventArgs e)
        //{
        //    string pswd = txt_Oldpassword.Text;
        //    ChangePswdData objChpswddata = new ChangePswdData();
        //    ChangePswdBO objChpswdBO = new ChangePswdBO();
        //    List<ChangePswdData> getResult = new List<ChangePswdData>();
        //    objChpswddata.UserName = txt_Oldpassword.Text.Trim() == "" ? "0" : txtusername.Text.Trim();
        //    getResult = objChpswdBO.GetPswd(objChpswddata);
        //    objLoginToken = (LogData)Session["LoginToken"];
        //    if (objLoginToken != null)
        //    {
        //        if (txt_Oldpassword.Text != getResult[0].oldpassword.ToString())
        //        {
        //            //ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "alert('Password Does not match with database record.')", true);
        //            Messagealert_.ShowMessage(lblmessage, "PasswordError", 0);
        //            div1.Visible = true;
        //            div1.Attributes["class"] = "FailAlert";
        //            txt_Oldpassword.Focus();
        //            return;
        //        }
               
        //    }
           
        //}

        //protected void txt_newPswd_TextChanged(object sender, EventArgs e)
        //{
        //    if (lblmessage.Text != "")
        //    {
        //        div1.Visible = false;    
        //    }
        //    //string pattern = null;
        //    //pattern = "^[/s/S]{5,}$";
        //    if(txt_newPswd.Text.Length < 5)
        //    //if (!Regex.IsMatch(txt_newPswd.Text.Trim(), pattern))
        //    {
        //        Messagealert_.ShowMessage(lblmessage, "PswdStrongType", 0);
        //        div1.Attributes["class"] = "FailAlert";
        //        div1.Visible = true;
        //        txt_newPswd.Focus();
        //        return;
        //    }
        //}

        //protected void txt_confirmed_TextChanged(object sender, EventArgs e)
        //{
        //    if (txt_confirmed.Text != txt_newPswd.Text)
        //    {
        //        Messagealert_.ShowMessage(lblmessage, "ConfirmedPswd", 0);
        //        div1.Visible = true;
        //        div1.Attributes["class"] = "FailAlert";
        //        txt_confirmed.Focus();
        //        return;
        //    }
        //    else
        //    {
        //        lblmessage.Visible = false;
        //    }
        //}

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            //string Password = txt_Oldpassword.Text;
            //txt_Oldpassword.Attributes.Remove(Password);
            //string newpwsd = txt_newPswd.Text;
            //txt_newPswd.Attributes.Remove(newpwsd);
           
            txt_Oldpassword.Text = "";
            txt_newPswd.Text = "";
            txt_confirmed.Text = "";
        }

    }
}