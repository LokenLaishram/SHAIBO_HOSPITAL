using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.MedHrData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedHR
{
    public partial class RunnerDetailsMst : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
            }
        }
        protected void btnsave_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (LogData.SaveEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "SaveEnable", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_RunnerCode.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please Enter Runner Code", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_RunnerCode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_runnerName.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please Enter Runner Name", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_runnerName.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_tax.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please Enter Runner Tax", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_tax.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_contactNo.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please Enter Runner Contact No.", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_contactNo.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_runnerAddress.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please Enter Runner Address", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_runnerAddress.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_email.Text.Trim() != "")
                {
                    if (Commonfunction.Checkemail(txt_email.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Email", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txt_email.Focus();
                        return;
                    }
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Please email address", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_email.Focus();
                    return;

                }
                RunnerDetailsData objData = new RunnerDetailsData();
                RunnerDetailsBO objTaxBO = new RunnerDetailsBO();
                objData.RunnerCode = txt_RunnerCode.Text.Trim() == "" ? null : txt_RunnerCode.Text.Trim();
                objData.RunnerName = txt_runnerName.Text == "" ? null : txt_runnerName.Text;
                objData.RunnerAddress = txt_runnerAddress.Text == "" ? null : txt_runnerAddress.Text;
                objData.EmailID = txt_email.Text == "" ? null : txt_email.Text;
                objData.RunnerTax = Convert.ToDecimal(txt_tax.Text == "" ? null : txt_tax.Text);
                objData.ContactNo = txt_contactNo.Text.Trim() == "" ? "0" : txt_contactNo.Text.Trim();
                objData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objData.EmployeeID = LogData.EmployeeID;
                objData.HospitalID = LogData.HospitalID;
                objData.FinancialYearID = LogData.FinancialYearID;
                objData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    objData.ActionType = Enumaction.Update;
                    objData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objTaxBO.UpdateRunnerDetails(objData);  // funtion at DAL
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    ViewState["ID"] = null;
                    bindgrid();
                    btnsave.Text = "Add";
                }
                if (result == 5)
                {
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
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
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void btnsearch_Click(object sender, System.EventArgs e)
        {
            bindgrid();
        }
        private void bindgrid()
        {
            try
            {
                List<RunnerDetailsData> lstemp = GetDocTax(0);
                if (lstemp.Count > 0)
                {
                    GvRunnerTax.DataSource = lstemp;
                    GvRunnerTax.DataBind();
                    GvRunnerTax.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                }
                else
                {
                    GvRunnerTax.DataSource = null;
                    GvRunnerTax.DataBind();
                    GvRunnerTax.Visible = true;
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
        private List<RunnerDetailsData> GetDocTax(int p)
        {
            RunnerDetailsData objRunnerData = new RunnerDetailsData();
            RunnerDetailsBO objDocTaxBO = new RunnerDetailsBO();
            objRunnerData.RunnerCode = txt_RunnerCode.Text.Trim() == "" ? null : txt_RunnerCode.Text.Trim();
            objRunnerData.RunnerName = txt_runnerName.Text == "" ? null : txt_runnerName.Text;
            objRunnerData.RunnerAddress = txt_runnerAddress.Text == "" ? null : txt_runnerAddress.Text;
            objRunnerData.ContactNo = txt_contactNo.Text.Trim() == "" ? "0" : txt_contactNo.Text.Trim();
            objRunnerData.RunnerTax = Convert.ToDecimal(txt_tax.Text == "" ? null : txt_tax.Text);
            objRunnerData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objDocTaxBO.SearchRunnerTaxDetails(objRunnerData);
        }
        protected void btnresets_Click(object sender, System.EventArgs e)
        {
            ViewState["ID"] = null;
            clearall();
            lblmessage.Visible = false;
            lblresult.Visible = false;
        }
        private void clearall()
        {
            txt_tax.Text = "";
            txt_RunnerCode.Text = "";
            txt_runnerName.Text = "";
            txt_runnerAddress.Text = "";
            txt_tax.Text = "";
            txt_contactNo.Text = "";
            ddlstatus.SelectedIndex = 0;
            GvRunnerTax.DataSource = null;
            GvRunnerTax.DataBind();
            GvRunnerTax.Visible = false;
            txt_email.Text = "";
            btnsave.Text = "Add";
        }
        protected void GvDocTax_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            GvRunnerTax.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        protected void GvDocTax_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edits")
                {
                    if (LogData.EditEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "EditEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    RunnerDetailsData objeditData = new RunnerDetailsData();
                    RunnerDetailsBO objBO = new RunnerDetailsBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvRunnerTax.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("lblID");
                    objeditData.ID = Convert.ToInt32(ID.Text);
                    objeditData.ActionType = Enumaction.Select;

                    List<RunnerDetailsData> GetResult = objBO.GetRunnerDetailsByID(objeditData);
                    if (GetResult.Count > 0)
                    {
                        txt_RunnerCode.Text = GetResult[0].RunnerCode.ToString();
                        txt_runnerName.Text = GetResult[0].RunnerName.ToString();
                        txt_runnerAddress.Text = GetResult[0].RunnerAddress.ToString();
                        txt_contactNo.Text = GetResult[0].ContactNo.ToString();
                        txt_email.Text = GetResult[0].EmailID.ToString();
                        txt_tax.Text = Commonfunction.Getrounding((Convert.ToDecimal(GetResult[0].RunnerTax.ToString() == "" ? "0" : GetResult[0].RunnerTax.ToString())).ToString());
                        ViewState["ID"] = GetResult[0].ID;
                        btnsave.Text = "Update";
                    }
                }
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "DeleteEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    RunnerDetailsData objData = new RunnerDetailsData();
                    RunnerDetailsBO objBO = new RunnerDetailsBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvRunnerTax.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    objData.ID = Convert.ToInt32(ID.Text);
                    objData.EmployeeID = LogData.EmployeeID;
                    objData.ActionType = Enumaction.Delete;
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        divmsg3.Visible = true;
                        divmsg3.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objData.Remarks = txtremarks.Text;
                    }

                    int Result = objBO.DeleteRunnerDetailsByID(objData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SucessAlert";
                        bindgrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";

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