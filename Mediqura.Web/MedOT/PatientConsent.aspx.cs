using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.PatientData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using OnBarcode.Barcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Reflection;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.OTData;
using Mediqura.BOL.AdmissionBO;
using Mediqura.BOL.OTBO;

namespace Mediqura.Web.MedOT
{
    public partial class PatientConsent : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
                Commonfunction.Insertzeroitemindex(ddl_employee);
                ddlConsent();
                checkSelect();
            }
       
       }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_employee, mstlookup.GetOtDocByIPNo(txt_IPNo.Text));
            
        }
        private void ddlConsent()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_consentType, mstlookup.GetLookupsList(LookupName.ConsentType));
            Commonfunction.PopulateDdl(ddlrelationship, mstlookup.GetLookupsList(LookupName.Relationship));
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetOTIPNo(string prefixText, int count, string contextKey)
        {
            OTStatusData Objpaic = new OTStatusData();
            OTStatusBO objInfoBO = new OTStatusBO();
            List<OTStatusData> getResult = new List<OTStatusData>();
            Objpaic.IPNo = prefixText;
            getResult = objInfoBO.getIPNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPNo.ToString());
            }
            return list;
            
        }
        public void checkSelect()
        {
            if (txt_IPNo.Text == "")
            {
                ddl_employee.Attributes["disabled"] = "disabled";
            }
            else
            {
                ddl_employee.Attributes.Remove("disabled");
            }
            if (ddl_employee.SelectedIndex == 0)
            {
                ddlrelationship.Attributes["disabled"] = "disabled";
            }
            else
            {
                ddlrelationship.Attributes.Remove("disabled");
            }
            if (ddlrelationship.SelectedIndex == 0)
            {
                ddl_consentType.Attributes["disabled"] = "disabled";
            }
            else
            {
                ddl_consentType.Attributes.Remove("disabled");
            }

        }
        protected void txt_IPNo_TextChanged(object sender, EventArgs e)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.IPNo = txt_IPNo.Text.Trim() == "" ? "" : txt_IPNo.Text.Trim();
            getResult = objInfoBO.GetPatientDetailsByIPNo(Objpaic);
            if (getResult.Count > 0)
            {
                //txt_name.Text = getResult[0].PatientName.ToString();
                txt_name.Text = getResult[0].PatientName.ToString() + " | Gender : " + getResult[0].Gender.ToString() + " | Age-" + getResult[0].Agecount.ToString();
                checkSelect();
                bindddl();
                
            }
            else 
            {
                txt_name.Text = "";
                txt_IPNo.Focus();
                Messagealert_.ShowMessage(lblmessage, "discharge", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
           
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchConsent();
        }
        public void SearchConsent()
        {
            try
            {
                if (LogData.SearchEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_IPNo.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "IPNo", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_IPNo.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                getIPconsent();
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;
            }
        }
        public void getIPconsent()
        {
            PatientConsentData objData = new PatientConsentData();
            PatientConsentTypeBO objBO = new PatientConsentTypeBO();
            //objData.TypeID = Convert.ToInt32(ddl_consentType.SelectedValue == "0" ? null : ddl_consentType.SelectedValue);
            objData.IPNo = (txt_IPNo.Text == "" ? null : txt_IPNo.Text.ToString().Trim());
            List<PatientConsentData> objresult = objBO.SearchConsentTemplateByIPNO(objData);
            if (objresult.Count > 0)
            {
                txt_relative.Text = objresult[0].RelativeName.ToString();
                ddlrelationship.SelectedValue = objresult[0].RelationID.ToString();
                ddl_consentType.SelectedValue=  objresult[0].TypeID.ToString();
                txtReport.InnerHtml = generateTemplateForIPNo(objresult[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">"), objresult[0]).Replace(@"&quot;", @"'").Replace(@"&amp;", @"&");
            }
            else
            {
                Messagealert_.ShowMessage(lblmessage, "consent", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }


        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (LogData.SaveEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "SaveEnable", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_IPNo.Text=="")
                {
                    Messagealert_.ShowMessage(lblmessage, "IPNo", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";

                    txt_IPNo.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_employee.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "OTemployee", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";

                    ddl_employee.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_relative.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Relative", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";

                    txt_relative.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddlrelationship.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Relationship", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";

                    ddlrelationship.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_consentType.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "consentType", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";

                    ddl_consentType.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                PatientConsentData objData = new PatientConsentData();
                PatientConsentTypeBO objBO = new PatientConsentTypeBO();
                objData.TypeID = Convert.ToInt32(ddl_consentType.SelectedValue == "0" ? null : ddl_consentType.SelectedValue);
                objData.IPNo = (txt_IPNo.Text == "" ? null : txt_IPNo.Text.ToString().Trim());
                objData.RelativeName = (txt_relative.Text == "" ? null : txt_relative.Text.ToString().Trim());
                objData.RelationID = Convert.ToInt32(ddlrelationship.SelectedValue == "0" ? null : ddlrelationship.SelectedValue);
                objData.ActionType = Enumaction.Update;
                objData.Template = txtReport.InnerHtml.ToString();
                objData.EmployeeID = LogData.EmployeeID;
                objData.HospitalID = LogData.HospitalID;
                objData.FinancialYearID = LogData.FinancialYearID;
                int result = objBO.UpdatePatientConsent(objData);
                if (result == 1 || result == 2)
                {
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    //reset();

                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);

            }



        }

        protected void btnresets_Click(object sender, EventArgs e)
        {
            txt_IPNo.Text = "";
            txt_name.Text = "";
            ddl_employee.SelectedIndex = 0;
            txt_relative.Text = "";
            ddlrelationship.SelectedIndex = 0;
            ddl_consentType.SelectedIndex = 0;
            txtReport.InnerHtml = "";
            div1.Visible = false;
            lblmessage.Visible = false;
        }

        protected void gvConsent_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnsearchList_Click(object sender, EventArgs e)
        {

        }

        protected void btnresetList_Click(object sender, EventArgs e)
        {

        }

        protected void btnexport_Click(object sender, EventArgs e)
        {

        }

        protected void ddl_consentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_consentType.SelectedIndex > 0)
            {

                if (txt_IPNo.Text != "")
                {
                    getHeader();
                }
                else
                {
                    getReportTemplate();
                }
            }
        }
        public void getHeader()
        {
            PatientConsentData objData = new PatientConsentData();
            PatientConsentTypeBO objBO = new PatientConsentTypeBO();
            objData.TypeID = Convert.ToInt32(ddl_consentType.SelectedValue == "0" ? null : ddl_consentType.SelectedValue);
            objData.IPNo = (txt_IPNo.Text == "" ? null : txt_IPNo.Text.ToString().Trim());
            List<PatientConsentData> objresult = objBO.GetConsentTemplateByIPNO(objData);
            if (objresult.Count > 0)
            {
                txtReport.InnerHtml = generateTemplate(objresult[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">"), objresult[0]).Replace(@"&quot;", @"'").Replace(@"&amp;", @"&");
            }
            else
            {
                txtReport.InnerHtml = objresult[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&");
            }

                               
        }
        public void getReportTemplate()
        {
            PatientConsentData objData = new PatientConsentData();
            PatientConsentTypeBO objBO = new PatientConsentTypeBO();
            objData.TypeID = Convert.ToInt32(ddl_consentType.SelectedValue == "0" ? null : ddl_consentType.SelectedValue);
            //objData.TypeFeatureID = Convert.ToInt32(ddl_dischargeFeature.SelectedValue == "0" ? null : ddl_dischargeFeature.SelectedValue);
            List<PatientConsentData> objdata = objBO.GetConsentTemplateByID(objData);
            if (objdata.Count > 0)
            {

                txtReport.InnerHtml = objdata[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&");
                ViewState["ID"] = objdata[0].ID.ToString(); ;
            }
            else
            {
                ViewState["ID"] = null;
                txtReport.InnerText = null;
            }
        }
        public string generateTemplate(string template, PatientConsentData objdata)
        {
            DateTime today = System.DateTime.Now;

            string header = "<table style=\"height: 86px;\" border=\"0\">" +
                            "<tbody><tr><td style=\"width: 183.576px;\">IPNo:</td>" +
                            "<td style=\"width: 433.576px;\">" + objdata.IPNo + "</td>" +
                            "<td style=\"width: 104.688px;\">Discharge Type:</td>" +
                            "<td style=\"width: 229.132px;\">" + objdata.ConsentType + "</td></tr><tr>" +
                            "<td style=\"width: 183.576px;\">Pat. Name:</td>" +
                            "<td style=\"width: 433.576px;\">" + objdata.PatientName + "</td>" +
                            "<td style=\"width: 104.688px;\">Pat.PatientAddress:</td>" +
                            "<td style=\"width: 229.132px;\">" + objdata.PatientAddress + "</td></tr><tr>" +
                            "<td style=\"width: 183.576px;\">Age:</td>" +
                            "<td style=\"width: 433.576px;\">" + objdata.Agecount + "/" +
                            "<td style=\"width: 104.688px;\">AdmissionDate:</td>" +
                            "<td style=\"width: 229.132px;\">" + objdata.AdmissionDate + "</td></tr><tr>" +
                            "<td style=\"width: 183.576px;\">emp.EmpName:</td>" +
                            "<td style=\"width: 433.576px;\">" + objdata.DoctorName + "</td>" +
                            "<td style=\"width: 104.688px;\">DepartmentName:</td>" +
                            "<td style=\"width: 229.132px;\">"+objdata.DepartmentName+"</td>" +
                            "</tr></tbody></table>";

            string Result = template.Replace("[Header]", header);

            return Result;
        }
        public string generateTemplateForIPNo(string template, PatientConsentData objdata)
        {
            DateTime today = System.DateTime.Now;

            string header = "<table style=\"height: 86px;\" border=\"0\">" +
                            "<tbody><tr><td style=\"width: 183.576px;\">IPNo:</td>" +
                            "<td style=\"width: 433.576px;\">" + objdata.IPNo + "</td>" +
                            "<td style=\"width: 104.688px;\">Discharge Type:</td>" +
                            "<td style=\"width: 229.132px;\">" + objdata.ConsentType + "</td></tr><tr>" +
                            "<td style=\"width: 183.576px;\">Pat. Name:</td>" +
                            "<td style=\"width: 433.576px;\">" + objdata.PatientName + "</td>" +
                            "<td style=\"width: 104.688px;\">Pat.PatientAddress:</td>" +
                            "<td style=\"width: 229.132px;\">" + objdata.PatientAddress + "</td></tr><tr>" +
                            "<td style=\"width: 183.576px;\">Age:</td>" +
                            "<td style=\"width: 433.576px;\">" + objdata.Agecount + "/" +
                            "<td style=\"width: 104.688px;\">AdmissionDate:</td>" +
                            "<td style=\"width: 229.132px;\">" + objdata.AdmissionDate + "</td></tr><tr>" +
                            "<td style=\"width: 183.576px;\">emp.EmpName:</td>" +
                            "<td style=\"width: 433.576px;\">" + objdata.DoctorName + "</td>" +
                            "<td style=\"width: 104.688px;\">DepartmentName:</td>" +
                            "<td style=\"width: 229.132px;\">" + objdata.DepartmentName + "</td>" +
                              "<td style=\"width: 183.576px;\">RelativeName:</td>" +
                            "<td style=\"width: 433.576px;\">" + objdata.RelativeName + "</td>" +
                             "<td style=\"width: 104.688px;\">Relation:</td>" +
                            "<td style=\"width: 229.132px;\">" + objdata.Relation + "</td></tr><tr>" +
                            "</tr></tbody></table>";

            string Result = template.Replace("[Header]", header);

            return Result;
        }

        protected void gvConsentList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvConsentList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void txt_IPNoList_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddl_employee_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkSelect();
        }

        protected void ddlrelationship_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkSelect();
        }
    }
}