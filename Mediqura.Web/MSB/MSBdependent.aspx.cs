using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedHrBO;
using Mediqura.BOL.MSBBO;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.MedHrData;
using Mediqura.CommonData.MSBData;
using Mediqura.CommonData.PatientData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MSB
{
    public partial class MSBdependent : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                Remarks.Visible = false;
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_dp_sex, mstlookup.GetLookupsList(LookupName.Gender));
            Commonfunction.PopulateDdl(ddl_emp_marital, mstlookup.GetLookupsList(LookupName.Marital));
            Commonfunction.PopulateDdl(ddl_dp_marital, mstlookup.GetLookupsList(LookupName.Marital));
            Commonfunction.PopulateDdl(ddl_dp_state, mstlookup.GetLookupsList(LookupName.State));
            ddl_dp_state.SelectedValue = "22";
            Commonfunction.PopulateDdl(ddl_dp_district, mstlookup.GetLookupsList(LookupName.District));
            Commonfunction.PopulateDdl(ddl_emp_grade, mstlookup.GetLookupsList(LookupName.EmpGrade));
            Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.Department));
            Commonfunction.PopulateDdl(ddldesignation, mstlookup.GetLookupsList(LookupName.Designation));
            Commonfunction.PopulateDdl(ddl_msb_type, mstlookup.GetLookupsList(LookupName.MsbBenifit));
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetUHID(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.UHID = Convert.ToInt64(prefixText);
            getResult = objInfoBO.GetUHID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].RegDNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetEmpName(string prefixText, int count, string contextKey)
        {
            EmployeeData Objpaic = new EmployeeData();
            EmployeeBO objInfoBO = new EmployeeBO();
            List<EmployeeData> getResult = new List<EmployeeData>();
            Objpaic.EmpName = prefixText;
            getResult = objInfoBO.GetEmployeeName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmpName.ToString());
            }
            return list;
        }
        public static List<string> GetEmployeeName(string prefixText, int count, string contextKey)
        {
            EmployeeData Objpaic = new EmployeeData();
            EmployeeBO objInfoBO = new EmployeeBO();
            List<EmployeeData> getResult = new List<EmployeeData>();
            Objpaic.EmpName = prefixText;
            getResult = objInfoBO.GetEmployeeName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmpName.ToString());
            }
            return list;
        }
        public void UHID_change()
        {

            MsbDepandentData Objpaic = new MsbDepandentData();
            DependentBO objInfoBO = new DependentBO();
            List<MsbDepandentData> getResult = new List<MsbDepandentData>();
            Objpaic.UHID = Convert.ToInt64(txt_dp_UHID.Text.Trim() == "" ? "0" : txt_dp_UHID.Text.Trim());
            getResult = objInfoBO.GetDependentDetailByUHID(Objpaic);
            if (getResult.Count > 0)
            {
                txt_dp_name.Text = getResult[0].DependentName;
                txt_dp_address.Text = getResult[0].DependentAddress;
                txt_dp_age.Text = getResult[0].DependentAge;
                txtdob.Text = getResult[0].DependentDob;
                ddl_dp_sex.SelectedValue = getResult[0].DependentSex.ToString();
                ddl_dp_marital.SelectedValue = getResult[0].DependentMarital.ToString();
                ddl_dp_state.SelectedValue = getResult[0].DependentState.ToString();
                ddl_dp_district.SelectedValue = getResult[0].DependentDistrict.ToString();
                txt_dp_pin.Text = getResult[0].DependentPin.ToString();
                txt_dp_contact.Text = getResult[0].DependentContact.ToString();

                txt_dp_name.ReadOnly = true;
                txt_dp_address.ReadOnly = true;
                txt_dp_age.ReadOnly = true;
                txtdob.ReadOnly = true;
                ddl_dp_sex.Attributes["disabled"] = "disabled";
                ddl_dp_marital.Attributes["disabled"] = "disabled";
                ddl_dp_state.Attributes["disabled"] = "disabled";
                ddl_dp_district.Attributes["disabled"] = "disabled";
                txt_dp_pin.ReadOnly = true;
                txt_dp_contact.ReadOnly = true;

            }
            else
            {
                txt_dp_name.Text = "";
                txt_dp_address.Text = "";
                txt_dp_age.Text = "";
                txtdob.Text = "";
                ddl_dp_sex.SelectedIndex = 0;
                ddl_dp_marital.SelectedIndex = 0;
                ddl_dp_state.SelectedIndex = 0;
                ddl_dp_district.SelectedIndex = 0;
                txt_dp_pin.Text = "";
                txt_dp_contact.Text = "";
            }
        }
        protected void txt_dp_UHID_TextChanged(object sender, EventArgs e)
        {
            UHID_change();
        }
        public void empSelect()
        {

            if (!txtEmpName.Text.ToString().Contains("ID:"))
            {

                txtEmpName.Text = "";
                txtEmpName.Focus();
                return;
            }
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_dp_relation, mstlookup.GetMsbRelation(Convert.ToInt32(ddl_emp_marital.SelectedValue)));
            MsbDepandentData Objpaic = new MsbDepandentData();
            DependentBO objInfoBO = new DependentBO();
            List<MsbDepandentData> getResult = new List<MsbDepandentData>();
            string EmpID;
            String empText = txtEmpName.Text == "" ? null : txtEmpName.Text.ToString().Trim();
            if (empText != null)
            {
                String[] emp = empText.Split(new[] { ":" }, StringSplitOptions.None);
                EmpID = emp[1];
                Objpaic.EmpID = Convert.ToInt64(EmpID);
            }
            getResult = objInfoBO.GetEmpDetailByEmpID(Objpaic);
            if (getResult.Count > 0)
            {
                Messagealert_.ShowMessage(lblmessage, "No. of Dependent for this Employee is " + getResult[0].DependentNo, 1);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "SucessAlert";


                txtEmpAge.Text = getResult[0].EmpAge;
                ddl_emp_sex.SelectedValue = getResult[0].EmpSex.ToString();
                txtaddress.Text = getResult[0].EmpAddress;
                ddl_emp_marital.SelectedValue = getResult[0].EmpMarital.ToString();
                ddldesignation.SelectedValue = getResult[0].EmpDesignation.ToString();
                ddldepartment.SelectedValue = getResult[0].EmpDepartment.ToString();
                ddl_emp_grade.SelectedValue = getResult[0].EmpGrade.ToString();
                txtcontactno.Text = getResult[0].EmpContact;

                txtEmpAge.ReadOnly = true;
                ddl_emp_sex.Attributes["disabled"] = "disabled";
                txtaddress.ReadOnly = true;
                ddl_emp_marital.Attributes["disabled"] = "disabled";
                ddldesignation.Attributes["disabled"] = "disabled";
                ddldepartment.Attributes["disabled"] = "disabled";
                ddl_emp_grade.Attributes["disabled"] = "disabled";
                txtcontactno.ReadOnly = true;
                Commonfunction.PopulateDdl(ddl_dp_relation, mstlookup.GetMsbRelation(Convert.ToInt32(ddl_emp_marital.SelectedValue)));


            }
            else
            {
                divmsg1.Visible = false;
                lblmessage.Visible = false;
                txtEmpAge.Text = "";
                ddl_emp_sex.SelectedIndex = 0;
                txtaddress.Text = "";
                ddl_emp_marital.SelectedIndex = 0;
                ddldesignation.SelectedIndex = 0;
                ddldepartment.SelectedIndex = 0;
                ddl_emp_grade.SelectedIndex = 0;
                txtcontactno.Text = "";
            }
        }
        protected void txtEmpName_TextChanged(object sender, EventArgs e)
        {
            empSelect();

        }


        protected void btnreset_Click(object sender, EventArgs e)
        {
            lblmessage.Visible = false;
            txt_dp_UHID.Text = "";
            txtEmpName.Text = "";
            ddl_dp_relation.SelectedIndex = 0;
            txt_dp_income.Text = "";
            txt_dp_occupation.Text = "";
            txt_dp_apply_date.Text = "";
            txt_dp_issue_date.Text = "";
            txt_dp_valid_upto.Text = "";
            ddl_condition.SelectedIndex = 0;
            txt_remarks.Text = "";
            UHID_change();
            empSelect();
            ddl_active.SelectedIndex = 0;
            GVdependentList.DataSource = null;
            GVdependentList.Visible = false;
            ViewState["ID"] = null;
            lblMessage4.Visible = false;
        }

        protected void ddl_dp_relation_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkvalidation();
        }
        public void checkvalidation()
        {

            MsbDepandentData Objpaic = new MsbDepandentData();
            DependentBO objInfoBO = new DependentBO();
            List<MsbDepandentData> getResult = new List<MsbDepandentData>();
            Objpaic.DependentRelation = Convert.ToInt32(ddl_dp_relation.SelectedValue == "0" ? "0" : ddl_dp_relation.SelectedValue);
            Objpaic.UHID = Convert.ToInt64(txt_dp_UHID.Text.Trim() == "" ? "0" : txt_dp_UHID.Text.Trim());
            getResult = objInfoBO.CheckMsbDependentEligibility(Objpaic);
            if (getResult.Count > 0)
            {
                ViewState["IsValid"] = getResult[0].IsValid;
                if (getResult[0].IsValid > 0)
                {
                    Messagealert_.ShowMessage(lblMessage4, "Minimum Age limit Valid. LIMIT: " + getResult[0].EligibilityAge + " Yrs AND Dependent Age: " + txt_dp_age.Text, 1);
                    div2.Visible = true;
                    div2.Attributes["class"] = "SucessAlert";
                    ddl_condition.Attributes["disabled"] = "disabled";
                }
                else
                {
                    Messagealert_.ShowMessage(lblMessage4, "Exceed Minimum Age. LIMIT: " + getResult[0].EligibilityAge + " Yrs AND Dependent Age: " + txt_dp_age.Text, 0);
                    div2.Visible = true;
                    div2.Attributes["class"] = "FailAlert";
                    ddl_condition.Attributes.Remove("disabled");
                }
            }
        }

        protected void ddl_condition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_condition.SelectedIndex == 0)
            {
                Remarks.Visible = false;
            }
            else
            {
                Remarks.Visible = true;
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
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
            MsbDepandentData objData = new MsbDepandentData();
            DependentBO objBO = new DependentBO();

            string EmpID;
            String empText = txtEmpName.Text == "" ? null : txtEmpName.Text.ToString().Trim();
            if (empText != null)
            {
                if (!txtEmpName.Text.ToString().Contains("ID:"))
                {
                    Messagealert_.ShowMessage(lblmessage, "msbEMP", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtEmpName.Text = "";
                    txtEmpName.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                if (txt_dp_UHID.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "msbDependent", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_dp_UHID.Text = "";
                    txt_dp_UHID.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_dp_relation.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "MsbRelation", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_dp_relation.Focus();
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_dp_occupation.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "MsbOccupation", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_dp_occupation.Text = "";
                    txt_dp_occupation.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_dp_income.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "MsbIncome", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_dp_income.Text = "";
                    txt_dp_income.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_dp_apply_date.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "MsbApplydate", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_dp_apply_date.Text = "";
                    txt_dp_apply_date.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_dp_issue_date.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "MsbIssueDate", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_dp_issue_date.Text = "";
                    txt_dp_issue_date.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_dp_valid_upto.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "MsbValidDate", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_dp_valid_upto.Text = "";
                    txt_dp_valid_upto.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_condition.SelectedIndex == 1)
                {

                    if (txt_remarks.Text.Trim() == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "MsbRemarks", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txt_remarks.Text = "";
                        txt_remarks.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
                if (ddl_msb_type.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "MsbType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    ddl_msb_type.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                int isValid = Convert.ToInt32(ViewState["IsValid"]);
                if (isValid == 1)
                {

                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    String[] emp = empText.Split(new[] { ":" }, StringSplitOptions.None);
                    EmpID = emp[1];
                    objData.EmpID = Convert.ToInt64(EmpID);
                    objData.UHID = Convert.ToInt64(txt_dp_UHID.Text.Trim() == "" ? "0" : txt_dp_UHID.Text.Trim());
                    objData.DependentRelation = Convert.ToInt32(ddl_dp_relation.SelectedValue == "0" ? "0" : ddl_dp_relation.SelectedValue);
                    objData.DependentIncome = Convert.ToDecimal(txt_dp_income.Text.Trim() == "" ? "0" : txt_dp_income.Text.Trim());
                    objData.Applydate = txt_dp_apply_date.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_dp_apply_date.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                    objData.IssueDate = txt_dp_issue_date.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_dp_issue_date.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                    objData.ValidUpto = txt_dp_valid_upto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_dp_valid_upto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                    objData.isSpecial = Convert.ToInt32(ddl_condition.SelectedValue == "0" ? "0" : ddl_condition.SelectedValue);
                    objData.Remarks = txt_remarks.Text.Trim() == "" ? "" : txt_remarks.Text.Trim();
                    objData.DependentOccupation = txt_dp_occupation.Text.Trim() == "" ? "0" : txt_dp_occupation.Text.Trim();
                    objData.MsbType = Convert.ToInt32(ddl_msb_type.SelectedValue == "0" ? "0" : ddl_msb_type.SelectedValue);
                    objData.Activation = Convert.ToInt32(ddl_activate.SelectedValue);
                    objData.EmployeeID = LogData.EmployeeID;
                    objData.HospitalID = LogData.HospitalID;
                    objData.FinancialYearID = LogData.FinancialYearID;
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
                        objData.ID = Convert.ToInt32(ViewState["ID"].ToString() == "" ? "0" : ViewState["ID"].ToString());
                    }
                    else
                    {
                        objData.ID = 0;
                    }
                }
                else
                {
                    if (ddl_condition.SelectedIndex == 1)
                    {

                        IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                        String[] emp = empText.Split(new[] { ":" }, StringSplitOptions.None);
                        EmpID = emp[1];
                        objData.EmpID = Convert.ToInt64(EmpID);
                        objData.UHID = Convert.ToInt64(txt_dp_UHID.Text.Trim() == "" ? "0" : txt_dp_UHID.Text.Trim());
                        objData.DependentRelation = Convert.ToInt32(ddl_dp_relation.SelectedValue == "0" ? "0" : ddl_dp_relation.SelectedValue);
                        objData.DependentIncome = Convert.ToDecimal(txt_dp_income.Text.Trim() == "" ? "0" : txt_dp_income.Text.Trim());
                        objData.Applydate = txt_dp_apply_date.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_dp_apply_date.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                        objData.IssueDate = txt_dp_issue_date.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_dp_issue_date.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                        objData.ValidUpto = txt_dp_valid_upto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_dp_valid_upto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                        objData.isSpecial = Convert.ToInt32(ddl_condition.SelectedValue == "0" ? "0" : ddl_condition.SelectedValue);
                        objData.Remarks = txt_remarks.Text.Trim() == "" ? "0" : txt_remarks.Text.Trim();
                        objData.DependentOccupation = txt_dp_occupation.Text.Trim() == "" ? "0" : txt_dp_occupation.Text.Trim();
                        objData.EmployeeID = LogData.EmployeeID;
                        objData.HospitalID = LogData.HospitalID;
                        objData.FinancialYearID = LogData.FinancialYearID;
                        objData.MsbType = Convert.ToInt32(ddl_msb_type.SelectedValue == "0" ? "0" : ddl_msb_type.SelectedValue);
                        objData.Activation = Convert.ToInt32(ddl_activate.SelectedValue);
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
                            objData.ID = Convert.ToInt32(ViewState["ID"].ToString() == "" ? "0" : ViewState["ID"].ToString());
                        }
                        else
                        {
                            objData.ID = 0;
                        }

                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "MsbSpecial", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";

                        return;
                    }

                }
                int result = objBO.UpdateMsbDependent(objData);
                if (result == 3)
                {
                    Messagealert_.ShowMessage(lblmessage, "MsbDuplicate", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }
                else
                    if (result == 1 || result == 2)
                    {
                        Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SucessAlert";
                        //reset();

                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                    }
           }
        }

        protected void GVdependentList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                if (e.CommandName == "Edits")
                {
                    if (LogData.EditEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "EditEnable", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GVdependentList.Rows[i];
                    Label lblID = (Label)gr.Cells[0].FindControl("lblID");
                    Label lblEmpID = (Label)gr.Cells[0].FindControl("lblEmpID");
                    Label lblEmpName = (Label)gr.Cells[0].FindControl("lblEmpName");
                    Label lblUHID = (Label)gr.Cells[0].FindControl("lblUHID");
                    Label lblRelation = (Label)gr.Cells[0].FindControl("lblRelation");
                    Label lblOccupation = (Label)gr.Cells[0].FindControl("lblOccupation");
                    Label lblIncome = (Label)gr.Cells[0].FindControl("lblIncome");
                    Label lblApplydate = (Label)gr.Cells[0].FindControl("lblApplydate");
                    Label lblIssueDate = (Label)gr.Cells[0].FindControl("lblIssueDate");
                    Label lblValidUpto = (Label)gr.Cells[0].FindControl("lblValidUpto");
                    Label lblActivation = (Label)gr.Cells[0].FindControl("lblActivation");
                    Label lblCondition = (Label)gr.Cells[0].FindControl("lblCondition");
                    Label lblMsbType = (Label)gr.Cells[0].FindControl("lblMsbType");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");

                    tabDependent.ActiveTabIndex = 0;
                    string emp = lblEmpName.Text + " ID:" + lblEmpID.Text;
                    txtEmpName.Text = emp;
                    empSelect();
                    txt_dp_UHID.Text = lblUHID.Text;
                    UHID_change();
                    ddl_dp_relation.SelectedValue = lblRelation.Text;
                    txt_dp_occupation.Text = lblOccupation.Text;
                    txt_dp_income.Text = lblIncome.Text;
                    txt_dp_apply_date.Text = lblApplydate.Text;
                    txt_dp_issue_date.Text = lblIssueDate.Text;
                    txt_dp_valid_upto.Text = lblValidUpto.Text;
                    ddl_activate.SelectedValue = lblActivation.Text;
                    ddl_condition.SelectedValue = lblCondition.Text;
                    ddl_msb_type.SelectedValue = lblMsbType.Text;


                    if (ddl_condition.SelectedIndex == 0)
                    {
                        Remarks.Visible = false;
                    }
                    else
                    {
                        Remarks.Visible = true;
                        txt_remarks.Text = txtremarks.Text;
                    }
                    ViewState["ID"] = lblID.Text;
                    checkvalidation();

                }
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "DeleteEnable", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage2.Visible = false;
                    }

                    MsbDepandentData objData = new MsbDepandentData();
                    DependentBO objBO = new DependentBO();

                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GVdependentList.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = false;
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objData.Remarks = txtremarks.Text;
                    }
                    objData.ID = Convert.ToInt32(ID.Text);
                    objData.EmployeeID = LogData.UserLoginId;
                    objData.HospitalID = LogData.HospitalID;
                    objData.IPaddress = LogData.IPaddress;
                    int Result = objBO.DeleteMSBDependentByID(objData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        divmsg3.Attributes["class"] = "SucessAlert";
                        divmsg3.Visible = true;
                        bindGrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage2, "system", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                    }

                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        public void bindGrid()
        {

            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            MsbDepandentData objData = new MsbDepandentData();
            DependentBO objBO = new DependentBO();

            objData.EmployeeID = Convert.ToInt64(txtEmpNameList.Text == "" ? "0" : txtEmpNameList.Text.Substring(txtEmpNameList.Text.LastIndexOf(':') + 1));
            objData.DependentName = txtDependentName.Text.Trim() == "" ? "" : txtDependentName.Text.Trim();
            objData.Activation = Convert.ToInt32(ddl_active.SelectedValue);
            List<MsbDepandentData> DependentList = objBO.GetDependentList(objData);
            if (DependentList.Count > 0)
            {
                GVdependentList.DataSource = DependentList;
                GVdependentList.DataBind();
                GVdependentList.Visible = true;
                Messagealert_.ShowMessage(lblresult, "Total:" + DependentList[0].MaximumRows.ToString() + " record(s) found.", 1);
                divmsg3.Attributes["class"] = "SucessAlert";
                divmsg3.Visible = true;
                ddlexport.Visible = true;
                btnexport.Visible = true;
                lblresult.Visible = true;
                divmsg1.Visible = false;
            }
            else
            {

                divmsg3.Visible = false;
                GVdependentList.DataSource = null;
                GVdependentList.DataBind();
                GVdependentList.Visible = true;
                ddlexport.Visible = false;
                btnexport.Visible = false;
                divmsg3.Visible = false;
                lblresult.Visible = false;
            }

        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtDependentName.Text = "";
            txtEmpNameList.Text = "";
            bindGrid();
        }

        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "ExportEnable", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage2.Visible = false;
            }
            if (ddlexport.SelectedIndex == 1)
            {
                ExportoExcel();
            }
            else if (ddlexport.SelectedIndex == 2)
            {
                ExportToPdf();
            }
            else
            {
                Messagealert_.ShowMessage(lblresult, "ExportType", 0);
                divmsg3.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        protected void ExportoExcel()
        {

            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Dependent Details");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Dependent Details.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblresult, "Exported", 1);
                divmsg3.Attributes["class"] = "SucessAlert";
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            MsbDepandentData objData = new MsbDepandentData();
            DependentBO objBO = new DependentBO();

            objData.EmployeeID = Convert.ToInt64(txtEmpNameList.Text == "" ? "" : txtEmpNameList.Text.Substring(txtEmpNameList.Text.LastIndexOf(':') + 1));
            objData.DependentName = txtDependentName.Text.Trim() == "" ? "" : txtDependentName.Text.Trim();
            objData.Activation = Convert.ToInt32(ddl_active.SelectedValue);

            List<MsbDepandentData> DependentList = objBO.GetDependentList(objData);
            List<MsbDepandentDataExcel> ListexcelData = new List<MsbDepandentDataExcel>();
            int i = 0;
            foreach (MsbDepandentData row in DependentList)
            {
                MsbDepandentDataExcel Ecxeclpat = new MsbDepandentDataExcel();
                Ecxeclpat.UHID = DependentList[i].UHID;
                Ecxeclpat.DependentName = DependentList[i].DependentName;
                Ecxeclpat.DependentAddress = DependentList[i].DependentAddress;
                Ecxeclpat.DependentRelation = DependentList[i].DependentRelation;
                Ecxeclpat.EmpName = DependentList[i].EmpName;
                Ecxeclpat.sApplydate = DependentList[i].sApplydate;
                Ecxeclpat.sIssueDate = DependentList[i].sIssueDate;
                Ecxeclpat.sValidUpto = DependentList[i].sValidUpto;
                ListexcelData.Add(Ecxeclpat);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        public class ListtoDataTableConverter
        {

            public DataTable ToDataTable<T>(List<T> items)
            {

                DataTable dataTable = new DataTable(typeof(T).Name);

                //Get all the properties

                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo prop in Props)
                {

                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);

                }

                foreach (T item in items)
                {

                    var values = new object[Props.Length];

                    for (int i = 0; i < Props.Length; i++)
                    {

                        //inserting property values to datatable rows

                        values[i] = Props[i].GetValue(item, null);

                    }

                    dataTable.Rows.Add(values);

                }

                //put a breakpoint here and check datatable

                return dataTable;

            }
        }
        public void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GVdependentList.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GVdependentList.Columns[4].Visible = false;
                    GVdependentList.Columns[5].Visible = false;
                    GVdependentList.Columns[6].Visible = false;

                    GVdependentList.RenderControl(hw);
                    GVdependentList.HeaderRow.Style.Add("width", "15%");
                    GVdependentList.HeaderRow.Style.Add("font-size", "10px");
                    GVdependentList.Style.Add("text-decoration", "none");
                    GVdependentList.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GVdependentList.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=DependentDetails.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                    Messagealert_.ShowMessage(lblresult, "Exported", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}
