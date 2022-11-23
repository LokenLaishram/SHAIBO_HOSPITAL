using Mediqura.BOL.CommonBO;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.PatientData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;
using System.Reflection;
using System.IO;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SourceAFIS.Simple;
using System.Runtime.Serialization;
using System.Data.SqlClient;
using Mediqura.DAL;
using Mediqura.Utility.Util;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml;

namespace Mediqura.Web
{
    public partial class PatientRegistration : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                FPImage.Visible = false;
            }
            initialize();
        }
        private void initialize()
        {
            //string IP = Commonfunction.GetClientIPAddress();
            //string URL = "http://" + IP + ":8080";
            //Boolean flag = Commonfunction.isValidURL(URL);
            //if (flag)
            //{
            //    btnFPScan.Visible = true;
            //}
            //else
            //{
            //    btnFPScan.Visible = false;
            //}
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlgender, mstlookup.GetLookupsList(LookupName.Gender));
            Commonfunction.PopulateDdl(ddlsalute, mstlookup.GetLookupsList(LookupName.Salutation));
            Commonfunction.PopulateDdl(ddlrelationship, mstlookup.GetLookupsList(LookupName.Relationship));
            Commonfunction.PopulateDdl(ddlmarital, mstlookup.GetLookupsList(LookupName.Marital));
            Commonfunction.PopulateDdl(ddlcountry, mstlookup.GetLookupsList(LookupName.Country));
            Commonfunction.PopulateDdl(ddl_bloodgrp, mstlookup.GetLookupsList(LookupName.BloodGroup));
            ddlcountry.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddlstate, mstlookup.GetLookupsList(LookupName.State));
            ddlstate.SelectedValue = "22";
            Commonfunction.PopulateDdl(ddldistrict, mstlookup.GetDistrictByStateD(Convert.ToInt32(ddlstate.SelectedValue == "" ? "0" : ddlstate.SelectedValue)));
            Commonfunction.PopulateDdl(ddlreligion, mstlookup.GetLookupsList(LookupName.Religion));
            Commonfunction.PopulateDdl(ddlpatientType, mstlookup.GetLookupsList(LookupName.PatientType));
            ddlpatientType.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddlprofession, mstlookup.GetLookupsList(LookupName.Profession));
            Commonfunction.PopulateDdl(ddl_compnay, mstlookup.GetLookupsList(LookupName.TPAList));
            Commonfunction.PopulateDdl(ddl_user, mstlookup.GetLookupsList(LookupName.FrontDeskUser));
            Commonfunction.PopulateDdl(ddl_visitto, mstlookup.GetLookupsList(LookupName.VisitTo));
            ddl_visitto.SelectedIndex = 1;
            //txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            //txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            // btnprints.Attributes["disabled"] = "disabled";
            ddl_compnay.Attributes["disabled"] = "disabled";
            hdndateofbirthcal.Value = "0";
            AutoCompleteExtender3.ContextKey = "1";
            btnFPScan.Attributes["disabled"] = "disabled";
            bindgrid(1);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetAutoUHID(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            if (contextKey == "2")
            {
                getResult = objInfoBO.GetDetailUHID(Objpaic);
            }
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].Patientshortdetail.ToString());
            }
            return list;
        }
        protected void ddl_registrationtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender3.ContextKey = ddl_registrationtype.SelectedValue == "" ? "0" : ddl_registrationtype.SelectedValue;
        }
        protected void txtname_TextChanged(object sender, EventArgs e)
        {
            if (ddl_registrationtype.SelectedValue == "2")
            {
                Int64 UHID = Convert.ToInt64(txtname.Text.Contains(":") ? txtname.Text.Substring(txtname.Text.LastIndexOf(':') + 1) : "0");
                if (UHID > 0)
                {
                    ViewState["ID"] = UHID;
                    EditPatient(UHID);
                }
                else
                {
                    txtname.Text = "";
                    Clearpatiendetails();
                }
            }
            else
            {
                txtco.Focus();
            }
        }
        protected void Clearpatiendetails()
        {
            txtdob.Text = "";
            ddlrelationship.SelectedIndex = 0;
            txtco.Text = "";
            ddldistrict.SelectedIndex = 0;
            txtuhid.Text = "";
            txtaddress.Text = "";
            txtage.Text = "";
            txtmonth.Text = "";
            txtday.Text = "";
            ddlmarital.SelectedIndex = 0;
            ddlreligion.SelectedIndex = 0;
            txtpin.Text = "";
            txtemail.Text = "";
            txtcontactno.Text = "";
            txtidmark.Text = "";
            ddlprofession.SelectedIndex = 0;
            ddlpatientType.SelectedIndex = 0;
            lblmessage.Visible = false;
            divmsg1.Visible = false;
            ViewState["ID"] = null;
            ddl_compnay.SelectedIndex = 0;
            ddl_compnay.Attributes["disabled"] = "disabled";
            hdndateofbirthcal.Value = "0";
            ddl_compnay.SelectedIndex = 0;
            txt_aadhar.Text = "";
            txt_referby.Text = "";
            ddlsalute.SelectedIndex = 0;
            ddlgender.SelectedIndex = 0;
            ddl_bloodgrp.SelectedIndex = 0;
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            ddlsalute.SelectedIndex = 0;
            ddlgender.SelectedIndex = 0;
            txtname.Text = "";
            ddl_registrationtype.SelectedIndex = 0;
            txtdob.Text = "";
            ddlrelationship.SelectedIndex = 0;
            ddl_bloodgrp.SelectedIndex = 0;
            txtco.Text = "";
            ddldistrict.SelectedIndex = 0;
            txtuhid.Text = "";
            txtaddress.Text = "";
            txtage.Text = "";
            txtmonth.Text = "";
            txtday.Text = "";
            ddlmarital.SelectedIndex = 0;
            ddlreligion.SelectedIndex = 0;
            txtpin.Text = "";
            txtemail.Text = "";
            txtcontactno.Text = "";
            txtidmark.Text = "";
            btnFPScan.Attributes["disabled"] = "disabled";
            ddlprofession.SelectedIndex = 0;
            ddlpatientType.SelectedIndex = 0;
            lblmessage.Visible = false;
            ViewState["ID"] = null;
            ddl_compnay.SelectedIndex = 0;
            ddl_compnay.Attributes["disabled"] = "disabled";
            hdndateofbirthcal.Value = "0";
            ddl_compnay.SelectedIndex = 0;
            txt_aadhar.Text = "";
            txt_referby.Text = "";
            AutoCompleteExtender3.ContextKey = "1";
            txtdob.ReadOnly = false;
            txtage.ReadOnly = false;
            txtday.ReadOnly = false;
            txtmonth.ReadOnly = false;
            btnsave.Attributes.Remove("disabled");
            ddl_visitto.SelectedIndex = 1;
            ddlcountry.SelectedIndex = 1;
            //ddlstate.SelectedValue = "22";
            //MasterLookupBO mstlookup = new MasterLookupBO();
            //Commonfunction.PopulateDdl(ddldistrict, mstlookup.GetDistrictByStateD(Convert.ToInt32(ddlstate.SelectedValue == "" ? "0" : ddlstate.SelectedValue)));
            btnsave.Text = "Add";
        }
        protected void ddlsalute_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsalute.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                List<LookupItem> GenderList = mstlookup.GetGenderBySalutationID(Convert.ToInt32(ddlsalute.SelectedValue));
                if (GenderList[0].ItemId == 5)
                {
                    Commonfunction.PopulateDdl(ddlgender, mstlookup.GetLookupsList(LookupName.Gender));
                    ddlsalute.Focus();
                }
                else
                {
                    Commonfunction.PopulateDdl(ddlgender, mstlookup.GetLookupsList(LookupName.Gender));
                    ddlgender.SelectedValue = GenderList[0].ItemId.ToString();
                    ddlsalute.Focus();
                }
            }
            else
            {
                ddlgender.SelectedIndex = 0;
            }
        }
        protected void ddlpatientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlpatientType.SelectedValue == "4")
            {
                ddl_compnay.Attributes.Remove("disabled");
                ddl_compnay.Focus();
            }
            else
            {
                ddl_compnay.SelectedIndex = 0;
                ddl_compnay.Attributes["disabled"] = "disabled";
                ddlprofession.Focus();
            }
        }
        protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlstate, mstlookup.GetStateByCountryID(Convert.ToInt32(ddlcountry.SelectedValue == "" ? "0" : ddlcountry.SelectedValue)));
            if (ddlcountry.SelectedValue == "1")
            {
                lblStateMandatory.Visible = true;
                ddlstate.Attributes.Remove("disabled");
                ddldistrict.Attributes.Remove("disabled");
            }
            else
            {
                lblStateMandatory.Visible = false;
                ddlstate.Attributes["disabled"] = "disabled";
                ddldistrict.Attributes["disabled"] = "disabled";
                ddldistrict.SelectedIndex = 0;

            }
        }
        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddldistrict, mstlookup.GetDistrictByStateD(Convert.ToInt32(ddlstate.SelectedValue == "" ? "0" : ddlstate.SelectedValue)));
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                PatientData objpat = new PatientData();
                RegistrationBO objpatBO = new RegistrationBO();
                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
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
                if (ddlsalute.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Salutation", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddlsalute.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtname.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "patientname", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtname.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                //if (txtco.Text.Trim() == "")
                //{
                //    Messagealert_.ShowMessage(lblmessage, "Careof", 0);
                //    divmsg1.Attributes["class"] = "FailAlert";
                //    divmsg1.Visible = true;
                //    txtco.Focus();
                //    return;
                //}
                //else
                //{
                //    lblmessage.Visible = false;
                //}
                if (txtage.Text + txtmonth.Text + txtday.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Age", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtage.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                //if (ddlrelationship.SelectedIndex == 0)
                //{
                //    Messagealert_.ShowMessage(lblmessage, "Relationship", 0);
                //    divmsg1.Attributes["class"] = "FailAlert";
                //    divmsg1.Visible = true;
                //    ddlrelationship.Focus();
                //    return;
                //}
                //else
                //{
                //    lblmessage.Visible = false;
                //}
                if (ddlgender.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Gender", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    ddlgender.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddlmarital.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "MaritalStatus", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    ddlmarital.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtaddress.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "PatientAddress", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtaddress.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddlcountry.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Country", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    ddlcountry.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddlcountry.SelectedValue == "1")
                {
                    if (ddlstate.SelectedIndex == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "State", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        ddlstate.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }
                //if (ddldistrict.SelectedIndex == 0)
                //{
                //    Messagealert_.ShowMessage(lblmessage, "District", 0);
                //    divmsg1.Attributes["class"] = "FailAlert";
                //    divmsg1.Visible = true;
                //    ddldistrict.Focus();
                //    return;
                //}
                //else
                //{
                //    lblmessage.Visible = false;
                //}

                if (txtcontactno.Text.Trim() == "")
                {
                    txtcontactno.Text = "0";
                }
                if (ddlpatientType.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "PatientCategory", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    ddlpatientType.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddlpatientType.SelectedValue == "4")
                {
                    if (ddl_compnay.SelectedIndex == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "TPAcompnay", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        ddlpatientType.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
                if (txtdob.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "DOB", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtdob.Text.Trim() != "")
                {

                    if (Commonfunction.isValidDate(txtdob.Text) == false || Commonfunction.CheckOverDate(txtdob.Text) == true)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtdob.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }
                //if (FPtemplate.Value == "")
                //{
                //    lblmessage.Text = "Fingerprint";
                //    divmsg1.Attributes["class"] = "FailAlert";
                //    divmsg1.Visible = true;

                //    return;
                //}
                //else
                //{
                //    lblmessage.Visible = false;
                //}
                objpat.SalutationID = Convert.ToInt32(ddlsalute.SelectedValue == "" ? "0" : ddlsalute.SelectedValue);
                objpat.PatientName = txtname.Text == "" ? null : txtname.Text.Trim();
                objpat.GenderID = Convert.ToInt32(ddlgender.SelectedValue == "" ? "0" : ddlgender.SelectedValue);
                //  objpat.NationalityID = Convert.ToInt32(ddlnationality.SelectedValue == "" ? "0" : ddlnationality.SelectedValue);
                objpat.Age = Convert.ToInt32(txtage.Text == "" ? "0" : txtage.Text);
                objpat.Month = Convert.ToInt32(txtmonth.Text == "" ? "0" : txtmonth.Text);
                objpat.Days = Convert.ToInt32(txtday.Text == "" ? "0" : txtday.Text);
                if (txtdob.Text.Trim() != "")
                {
                    DateTime DateofBirth = DateTime.Parse(txtdob.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                    objpat.DOB = DateofBirth;
                    lblmessage.Visible = false;
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtdob.Focus();
                    return;
                }
                objpat.MaritalStatusID = Convert.ToInt32(ddlmarital.SelectedValue == "" ? "0" : ddlmarital.SelectedValue);
                objpat.BloodGrp = Convert.ToInt32(ddl_bloodgrp.SelectedValue == "" ? "0" : ddl_bloodgrp.SelectedValue);
                objpat.Address = txtaddress.Text == "" ? null : txtaddress.Text.Trim();
                objpat.StateID = Convert.ToInt32(ddlstate.SelectedValue == "" ? "0" : ddlstate.SelectedValue);
                objpat.DistrictID = Convert.ToInt32(ddldistrict.SelectedValue == "" ? "0" : ddldistrict.SelectedValue);
                objpat.CountryID = Convert.ToInt32(ddlcountry.SelectedValue == "" ? "0" : ddlcountry.SelectedValue);
                String text = txtpin.Text == "" ? "0" : txtpin.Text;
                objpat.ReligionID = Convert.ToInt32(ddlreligion.SelectedValue == "" ? "0" : ddlreligion.SelectedValue);
                objpat.PatRelatives = txtco.Text == "" ? null : txtco.Text.Trim();
                objpat.RelationshipID = Convert.ToInt32(ddlrelationship.SelectedValue == "" ? "0" : ddlrelationship.SelectedValue);
                objpat.EmailID = txtemail.Text == "" ? null : txtemail.Text.Trim();
                objpat.FPtemplate = FPtemplate.Value;
                if (txtemail.Text != "")
                {
                    if (Commonfunction.Checkemail(txtemail.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Email", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtemail.Focus();
                        return;
                    }
                }
                objpat.Pin = Convert.ToInt32(txtpin.Text == "" ? "0" : txtpin.Text.Trim());
                objpat.ContactNo = txtcontactno.Text == "" ? null : txtcontactno.Text.Trim();
                objpat.ProfessionID = Convert.ToInt32(ddlprofession.SelectedValue == "" ? "0" : ddlprofession.SelectedValue);
                objpat.PatientType = Convert.ToInt32(ddlpatientType.SelectedValue == "" ? "0" : ddlpatientType.SelectedValue);
                objpat.TPAcompany = Convert.ToInt32(ddl_compnay.SelectedValue == "" ? "0" : ddl_compnay.SelectedValue);
                objpat.IDmark = txtidmark.Text == "" ? null : txtidmark.Text.Trim();
                objpat.AadhaarNumber = txt_aadhar.Text == "" ? null : txt_aadhar.Text.Trim();
                objpat.ReferBy = txt_referby.Text == "" ? null : txt_referby.Text.Trim();
                objpat.EmployeeID = LogData.EmployeeID;
                objpat.HospitalID = LogData.HospitalID;
                objpat.FinancialYearID = LogData.FinancialYearID;
                objpat.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        ddlsalute.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }

                    objpat.ActionType = Enumaction.Update;
                    objpat.ID = Convert.ToInt64(ViewState["ID"].ToString() == "" ? "0" : ViewState["ID"].ToString());
                }
                int results = objpatBO.UpdatePatientRegistration(objpat);
                if (results > 2017)
                {
                    txtuhid.Text = results.ToString();
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = results;

                    arParms[1] = new SqlParameter("@UHIDBarcode", SqlDbType.Image);
                    arParms[1].Value = Commonfunction.getBarcodeImage(txtuhid.Text == "" ? "0" : txtuhid.Text);

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_Uhid_barcode", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        int result = Convert.ToInt32(arParms[2].Value);
                    }

                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Attributes["class"] = "SucessAlert";
                    divmsg1.Visible = true;
                    btnsave.Attributes["disabled"] = "disabled";
                    if (ddl_visitto.SelectedValue == "1")
                    {
                        Session["PAT_UHID"] = results.ToString();
                        Response.Redirect("/MedBills/OpdBilling.aspx", false);
                    }
                    if (ddl_visitto.SelectedValue == "2")
                    {
                        Session["PAT_UHID"] = results.ToString();
                        Response.Redirect("/MedBills/OpdBilling.aspx", false);
                    }
                    //if (ddl_visitto.SelectedValue == "2")
                    //{
                    //    Session["EMG_UHID"] = results.ToString();
                    //    Response.Redirect("/MedEmergency/EmergencyRegistration.aspx", false);
                    //}
                    if (ddl_visitto.SelectedValue == "3")
                    {
                        Session["LAB_UHID"] = results.ToString();
                        Response.Redirect("/MedBills/OPLabBilling.aspx", false);
                    }
                    if (ddl_visitto.SelectedValue == "4")
                    {
                        Session["IP_UHID"] = results.ToString();
                        Response.Redirect("/MedAdmission/IPDAdmission.aspx", false);
                    }
                }
                if (results == 2)
                {
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    btnsave.Attributes["disabled"] = "disabled";
                    divmsg1.Attributes["class"] = "SucessAlert";
                    divmsg1.Visible = true;
                    if (ddl_visitto.SelectedValue == "1")
                    {
                        Session["PAT_UHID"] = Convert.ToInt64(ViewState["ID"].ToString() == "" ? "0" : ViewState["ID"].ToString());
                        ViewState["ID"] = null;
                        Response.Redirect("/MedBills/OpdBilling.aspx", false);
                    }
                    if (ddl_visitto.SelectedValue == "2")
                    {
                        Session["PAT_UHID"] = Convert.ToInt64(ViewState["ID"].ToString() == "" ? "0" : ViewState["ID"].ToString());
                        ViewState["ID"] = null;
                        Response.Redirect("/MedBills/OpdBilling.aspx", false);
                        // Response.Redirect("/MedEmergency/EmergencyRegistration.aspx", false);
                    }
                    if (ddl_visitto.SelectedValue == "3")
                    {
                        Session["LAB_UHID"] = Convert.ToInt64(ViewState["ID"].ToString() == "" ? "0" : ViewState["ID"].ToString());
                        ViewState["ID"] = null;
                        Response.Redirect("/MedBills/OPLabBilling.aspx", false);
                    }
                    if (ddl_visitto.SelectedValue == "4")
                    {
                        Session["IP_UHID"] = Convert.ToInt64(ViewState["ID"].ToString() == "" ? "0" : ViewState["ID"].ToString());
                        ViewState["ID"] = null;
                        Response.Redirect("/MedAdmission/IPDAdmission.aspx", false);
                    }

                }
                if (results == 1)
                {
                    btnsave.Attributes["disabled"] = "disabled";
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                string msg = ex.ToString();
                Messagealert_.ShowMessage(lblmessage, msg, 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetUHID(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetUHID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].RegDNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetAdvncUHID(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetAdvncUHID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].RegDNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPatientName(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetPatientNameWithUHID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetContactno(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.ContactNo = prefixText;
            getResult = objInfoBO.GetContactno(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ContactNo.ToString());
            }
            return list;
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid(1);
        }
        protected void bindgrid(int page)
        {
            try
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
                    lblmessage2.Visible = false;
                }
             
                List<PatientData> patientdetails = GetPatientData(page);
                if (patientdetails.Count > 0)
                {
                    if (LogData.PrintEnable == 0)
                    {
                        btnprints.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btnprints.Attributes.Remove("disabled");
                    }
                    GvPatientList.VirtualItemCount = patientdetails[0].MaximumRows;//total item is required for custom paging
                    GvPatientList.PageIndex = page - 1;

                    GvPatientList.DataSource = patientdetails;
                    GvPatientList.DataBind();
                    GvPatientList.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + patientdetails[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblmessage2.Visible = false;
                    divmsg2.Visible = false;
                    btnsave.Text = "Add";
                }
                else
                {
                    btnprints.Attributes["disabled"] = "disabled";
                    divmsg3.Visible = false;
                    GvPatientList.DataSource = null;
                    GvPatientList.DataBind();
                    GvPatientList.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    divmsg3.Visible = false;
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<PatientData> GetPatientData(int curIndex)
        {

            PatientData objpat = new PatientData();
            RegistrationBO objstdBO = new RegistrationBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            bool isnumeric = txtpatientNames.Text.All(char.IsDigit);
            if (isnumeric == false)
            {
                if (txtpatientNames.Text.Contains(":"))
                {
                    bool isUHIDnumeric = txtpatientNames.Text.Substring(txtpatientNames.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                    string text = txtpatientNames.Text.Substring(txtpatientNames.Text.LastIndexOf(':') + 1);
                    if (isUHIDnumeric == true && text != "")
                    {
                        objpat.UHID = isUHIDnumeric ? Convert.ToInt64(txtpatientNames.Text.Contains(":") ? txtpatientNames.Text.Substring(txtpatientNames.Text.LastIndexOf(':') + 1) : "0") : 0;
                    }
                    else
                    {
                        txtpatientNames.Text = "";
                        objpat.UHID = 0;
                    }
                }
                else
                {
                    txtpatientNames.Text = "";
                    txtpatientNames.Focus();
                }
            }
            else
            {
                objpat.UHID = Convert.ToInt64(txtpatientNames.Text == "" ? "0" : txtpatientNames.Text);
            }
            objpat.PatientName = txtpatientNames.Text == "" ? null : txtpatientNames.Text.Trim();
            //objpat.ContactNo = txtcontactnos.Text == "" ? "0" : txtcontactnos.Text.Trim();
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objpat.userID = Convert.ToInt32(ddl_user.SelectedValue == "" ? "0" : ddl_user.SelectedValue);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            objpat.CurrentIndex = curIndex;
            objpat.PageSize= Convert.ToInt32(ddl_show.SelectedValue == "10000" ? lbl_totalrecords.Text : ddl_show.SelectedValue);
            GvPatientList.PageSize = Convert.ToInt32(ddl_show.SelectedValue == "10000" ? lbl_totalrecords.Text : ddl_show.SelectedValue);
           
            return objstdBO.SearchPatientList(objpat);
        }

        public List<PatientData> GetPatientDetails(int curIndex)
        {

            PatientData objpat = new PatientData();
            RegistrationBO objstdBO = new RegistrationBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.PatientName = txtpatientNames.Text == "" ? null : txtpatientNames.Text.Trim();
            // objpat.ContactNo = txtcontactnos.Text == "" ? "0" : txtcontactnos.Text.Trim();
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objpat.userID = Convert.ToInt32(ddl_user.SelectedValue == "" ? "0" : ddl_user.SelectedValue);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            return objstdBO.SearchPatientDetails(objpat);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtdatefrom.Text = "";
            txtto.Text = "";
            ddlstatus.SelectedIndex = 0;
            GvPatientList.DataSource = null;
            GvPatientList.DataBind();
            GvPatientList.Visible = false;
            lblresult.Visible = false;
            txtpatientNames.Text = "";
            //txtcontactnos.Text = "";
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            txtmonth.Text = "";
            txtday.Text = "";
            txtage.Text = "";
            lblresult.Visible = false;
            divmsg3.Visible = false;
            ViewState["ID"] = null;
            lblmessage2.Visible = false;
            btnprints.Attributes["disabled"] = "disabled";
            ddl_user.SelectedIndex = 0;
            //txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            //txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
        protected void txtautoUHID_TextChanged(object sender, EventArgs e)
        {
            bindgrid(1);
        }
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            bindgrid(1);
        }
        protected void txtcontactnos_TextChanged(object sender, EventArgs e)
        {
            bindgrid(1);
        }
        protected void txtdob_TextChanged(object sender, EventArgs e)
        {
            bindage();
        }
        protected void bindage()
        {
            if (txtdob.Text.Trim() != "")
            {

                if (Commonfunction.isValidDate(txtdob.Text) == false || Commonfunction.CheckOverDate(txtdob.Text) == true)
                {
                    txtdob.Text = "";
                    Messagealert_.ShowMessage(lblmessage, "Please enter valid date of birth.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtdob.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                txtage.Text = Commonfunction.CalculateY(txtdob.Text);
                txtmonth.Text = Commonfunction.CalculateM(txtdob.Text);
                txtday.Text = Commonfunction.CalculateD(txtdob.Text);
            }
        }
        protected void GvPatientList_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    GridViewRow gr = GvPatientList.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblUHID");
                    Int64 PatID = Convert.ToInt64(ID.Text);
                    EditPatient(PatID);
                    tabcontainerpatient.ActiveTabIndex = 1;
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
                        lblmessage.Visible = false;
                    }

                    PatientData objpatnt = new PatientData();
                    RegistrationBO objstdBO = new RegistrationBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvPatientList.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        //divmsg3.Visible = false;
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objpatnt.Remarks = txtremarks.Text;
                    }
                    objpatnt.ID = Convert.ToInt64(ID.Text);
                    objpatnt.EmployeeID = LogData.UserLoginId;
                    objpatnt.HospitalID = LogData.HospitalID;
                    objpatnt.IPaddress = LogData.IPaddress;
                    int Result = objstdBO.DeletePatientDetailsByID(objpatnt);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        divmsg3.Attributes["class"] = "SucessAlert";
                        divmsg3.Visible = true;
                        bindgrid(1);
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage2, "system", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                    }

                }
                if (e.CommandName == "Pay")
                {
                    if (LogData.AmountEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "You do not have the rights.", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvPatientList.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblUHID");
                    Session["LAB_UHID"] = ID.Text;
                    Response.Redirect("/MedBills/OPLabBilling.aspx", false);
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
        protected void EditPatient(Int64 patID)
        {
            try
            {
                List<PatientData> patientdetails = GetEditPatientDetails(patID);
                if (patientdetails.Count > 0)
                {
                    ddlsalute.SelectedValue = patientdetails[0].SalutationID.ToString();
                    txtname.Text = patientdetails[0].PatientName.ToString();
                    txtco.Text = patientdetails[0].PatRelatives.ToString();
                    txtdob.Text = patientdetails[0].DOB.ToString("dd/MM/yyyy");
                    txtage.Text = patientdetails[0].Age.ToString();
                    txtmonth.Text = patientdetails[0].Month.ToString();
                    txtday.Text = patientdetails[0].Days.ToString();
                    // bindage();
                    txtaddress.Text = patientdetails[0].Address.ToString();
                    txtcontactno.Text = patientdetails[0].ContactNo.ToString();
                    txtemail.Text = patientdetails[0].EmailID.ToString();
                    txtpin.Text = patientdetails[0].Pin.ToString();
                    txtidmark.Text = patientdetails[0].IDmark.ToString();
                    txtco.Text = patientdetails[0].PatRelatives.ToString();
                    txtcontactno.Text = patientdetails[0].ContactNo.ToString();
                    txtuhid.Text = patientdetails[0].UHID.ToString();
                    ddlrelationship.SelectedValue = patientdetails[0].RelationshipID.ToString();
                    ddl_bloodgrp.SelectedValue = patientdetails[0].BloodGrp.ToString();
                    ddlgender.SelectedValue = patientdetails[0].GenderID.ToString();
                    ddlreligion.SelectedValue = patientdetails[0].ReligionID.ToString();
                    ddlmarital.SelectedValue = patientdetails[0].MaritalStatusID.ToString();
                    // ddlnationality.SelectedValue = patientdetails[0].NationalityID.ToString();
                    ddlcountry.SelectedValue = patientdetails[0].CountryID.ToString();
                    MasterLookupBO mstlookup = new MasterLookupBO();
                    Commonfunction.PopulateDdl(ddlstate, mstlookup.GetStateByCountryID(Convert.ToInt32(patientdetails[0].CountryID.ToString() == "" ? "0" : patientdetails[0].CountryID.ToString())));
                    ddlstate.SelectedValue = patientdetails[0].StateID.ToString();
                    Commonfunction.PopulateDdl(ddldistrict, mstlookup.GetDistrictByStateD(Convert.ToInt32(patientdetails[0].StateID.ToString() == "" ? "0" : patientdetails[0].StateID.ToString())));
                    ddldistrict.SelectedValue = patientdetails[0].DistrictID.ToString();
                    ddlprofession.SelectedValue = patientdetails[0].ProfessionID.ToString();
                    txt_aadhar.Text = patientdetails[0].AadhaarNumber.ToString();
                    txt_referby.Text = patientdetails[0].ReferBy.ToString();
                    ddl_compnay.SelectedValue = patientdetails[0].TPAcompany.ToString();
                    ViewState["ID"] = patientdetails[0].UHID.ToString();
                    FPtemplate.Value = patientdetails[0].FPtemplate.ToString();
                    lblmessage.Visible = false;
                    if (patientdetails[0].PatientType.ToString() == "2")
                    {
                        Commonfunction.PopulateDdl(ddlpatientType, mstlookup.GetLookupsList(LookupName.MSbPatientType));
                        ddlpatientType.SelectedIndex = 1;
                    }
                    else
                    {
                        Commonfunction.PopulateDdl(ddlpatientType, mstlookup.GetLookupsList(LookupName.PatientType));
                        ddlpatientType.SelectedValue = patientdetails[0].PatientType.ToString();
                    }
                    btnsave.Attributes.Remove("disabled");
                    btnsave.Text = "Update";
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
            }
        }
        public List<PatientData> GetEditPatientDetails(Int64 ID)
        {
            PatientData objpat = new PatientData();
            RegistrationBO objpatBO = new RegistrationBO();
            objpat.ID = ID;
            return objpatBO.GetPtientDeatilbyID(objpat);
        }
        protected void ExportoExcel()
        {

            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Registration List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Registrationlist.xlsx");
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
            List<PatientData> PatientDetails = GetPatientDetails(0);
            List<PatientDatatoExcel> ListexcelData = new List<PatientDatatoExcel>();
            int i = 0;
            foreach (PatientData row in PatientDetails)
            {
                PatientDatatoExcel Ecxeclpat = new PatientDatatoExcel();
                Ecxeclpat.UHID = PatientDetails[i].UHID;
                Ecxeclpat.PatientName = PatientDetails[i].PatientName;
                Ecxeclpat.PatientType = PatientDetails[i].PatientTypeName;
                Ecxeclpat.Religion = PatientDetails[i].Religion;
                Ecxeclpat.MaritalStatus = PatientDetails[i].MaritalStatus;
                Ecxeclpat.IDmark = PatientDetails[i].IDmark;
                Ecxeclpat.Nationality = PatientDetails[i].Nationality;
                Ecxeclpat.Occupation = PatientDetails[i].Profession;
                Ecxeclpat.PatRelatives = PatientDetails[i].PatRelatives;
                Ecxeclpat.GenderName = PatientDetails[i].GenderName;
                Ecxeclpat.Age = PatientDetails[i].Agecount;
                Ecxeclpat.DOB = PatientDetails[i].DateofBirth.ToString();
                Ecxeclpat.Address = PatientDetails[i].Address;
                Ecxeclpat.Pin = PatientDetails[i].Pin;
                Ecxeclpat.EmailID = PatientDetails[i].EmailID;
                Ecxeclpat.ContactNo = PatientDetails[i].ContactNo;
                Ecxeclpat.AddDate = PatientDetails[i].AddDate;
                Ecxeclpat.AddedBy = PatientDetails[i].EmpName;
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
        public void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvPatientList.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvPatientList.Columns[9].Visible = false;
                    GvPatientList.Columns[10].Visible = false;
                    GvPatientList.Columns[11].Visible = false;
                    GvPatientList.Columns[7].Visible = false;
                    GvPatientList.Columns[6].Visible = false;
                    GvPatientList.RenderControl(hw);
                    GvPatientList.HeaderRow.Style.Add("width", "15%");
                    GvPatientList.HeaderRow.Style.Add("font-size", "10px");
                    GvPatientList.Style.Add("text-decoration", "none");
                    GvPatientList.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvPatientList.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=PatientDetail.pdf");
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
        protected void GvPatientList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }
        //biometric---------------------
        protected void btnCapture_Click(object sender, EventArgs e)
        {
            if (txtname.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Please enter Patient Name.", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                txtname.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            string IP = Commonfunction.GetClientIPAddress();
            string URL = "http://" + IP + ":8080/CallMorphoAPI";
            string response = GET(URL);

            fingerData data = JsonConvert.DeserializeObject<fingerData>(response);

            string fptemp = data.Base64ISOTemplate;
            Bitmap fbimage = Base64StringToBitmap(data.Base64BMPIMage);
            FPImage.Visible = true;
            FPImage.ImageUrl = "data:image/bmp;base64," + data.Base64BMPIMage;
            FPtemplate.Value = data.Base64ISOTemplate;
            matchFP(fptemp, txtname.Text.ToString());

        }
        public Bitmap Base64StringToBitmap(string base64String)
        {
            Bitmap bmpReturn = null;

            byte[] byteBuffer = Convert.FromBase64String(base64String);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);

            memoryStream.Position = 0;

            bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);

            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;

            return bmpReturn;
        }
        public class fingerData
        {
            public string ReturnCode { get; set; }
            public string Base64ISOTemplate { get; set; }
            public string Base64RAWIMage { get; set; }
            public string Base64BMPIMage { get; set; }
            public string NFIQ { get; set; }

        }
        public class PatientfingerData
        {
            public string UHID { get; set; }
            public string FPtemplates { get; set; }


        }
        string GET(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }
        public void matchFP(string base64String, string name)
        {


            Afis = new AfisEngine();

            List<MyPerson> database = new List<MyPerson>();
            //   database = (List<MyPerson>)formatter.Deserialize(stream);
            List<PatientfingerData> fs = GetFPscan(name);
            for (int i = 0; i < fs.Count; i++)
            {

                database.Add(Enroll(fs[i].FPtemplates.ToString(), fs[i].UHID.ToString()));

            }

            // Enroll visitor with unknown identity
            MyPerson probe = Enroll(base64String, "Visitor");

            // Look up the probe using Threshold = 10
            Afis.Threshold = 10;

            MyPerson match = Afis.Identify(probe, database).FirstOrDefault() as MyPerson;
            // Null result means that there is no candidate with similarity score above threshold
            if (match == null)
            {
                lblmessage.Text = "No person found match the FP";
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";

                return;
            }
            else
            {

                float score = Afis.Verify(probe, match);


                if (score > 50)
                {

                    if (ddl_visitto.SelectedValue == "0" || ddl_visitto.SelectedValue == "1")
                    {
                        Session["PAT_UHID"] = match.UHID;
                        Response.Redirect("/MedBills/OpdBilling.aspx", false);
                    }
                    else
                    {
                        Session["PAT_UHID"] = match.UHID;
                        Response.Redirect("/MedBills/OPLabBilling.aspx", false);
                    }
                }
                else
                {
                    lblmessage.Text = "Person found with a low match score of: " + score + " with UHID: " + match.UHID;
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }


            }
        }

        public List<PatientfingerData> GetFPscan(String name)
        {
            List<PatientfingerData> result = null;
            try
            {
                {

                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[0].Value = name;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetallFPscan", arParms);
                    List<PatientfingerData> lstresult = ORHelper<PatientfingerData>.FromDataReaderToList(sqlReader);
                    result = lstresult;
                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }

        [Serializable]
        class MyFingerprint : Fingerprint
        {
            public string Filename;
        }

        // Inherit from Person in order to add Name field
        [Serializable]
        class MyPerson : Person
        {
            [DataMember]
            public string UHID { get; set; }
            [DataMember]
            public string FPtemplates { get; set; }

            //public List<Fingerprint> Fingerprints { get{
            //    List<Fingerprint> fp=new List<Fingerprint>();
            //    MyFingerprint fps = new MyFingerprint();
            //    fps.AsIsoTemplate = Convert.FromBase64String(fptemplate);
            //    fp.Add(fps);
            //    return fp;
            //} set; }

        }

        // Shared AfisEngine instance (cannot be shared between different threads though)
        static AfisEngine Afis;

        // Take fingerprint image file and create Person object from the image
        static MyPerson Enroll(string base64string, string UHID)
        {

            // Initialize empty fingerprint object and set properties
            MyFingerprint fp = new MyFingerprint();
            fp.Filename = UHID;
            fp.AsIsoTemplate = Convert.FromBase64String(base64string);
            // Load image from the file

            //   BitmapImage image = new BitmapImage(new Uri(filename, UriKind.RelativeOrAbsolute));
            // fp.AsBitmapSource = image;

            // Above update of fp.AsBitmapSource initialized also raw image in fp.Image
            // Check raw image dimensions, Y axis is first, X axis is second

            MyPerson person = new MyPerson();
            person.UHID = UHID;
            // Add fingerprint to the person
            person.Fingerprints.Add(fp);

            // Afis.Extract(person);


            return person;
        }

        protected void txtScan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(txtScan.Text == "" ? "NO" : txtScan.Text.ToString());
                XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/PrintLetterBarcodeData");

                foreach (XmlNode node in nodeList)
                {

                    txtname.Text = node.Attributes["name"].InnerText;
                    txt_aadhar.Text = node.Attributes["uid"].InnerText;
                    ddlgender.SelectedValue = node.Attributes["gender"].InnerText == "M" ? "1" : "2";
                    if (node.Attributes != null && node.Attributes["gname"] != null)
                    {
                        txtco.Text = node.Attributes["gname"].InnerText;
                    }
                    else
                    {
                        txtco.Text = node.Attributes["co"].InnerText;
                    }

                    txtaddress.Text = node.Attributes["loc"].InnerText;
                    //ddldistrict.SelectedValue = node.Attributes["dist"].InnerText;
                    //ddlstate.SelectedValue = node.Attributes["state"].InnerText;
                    txtpin.Text = node.Attributes["pc"].InnerText;
                    if (node.Attributes != null && node.Attributes["dob"] != null)
                    {
                        txtdob.Text = node.Attributes["dob"].InnerText;
                    }
                    else
                    {
                        txtdob.Text = node.Attributes["yob"].InnerText + "-01-01";
                    }

                }
                txtScan.Text = "";
                //sbtnFPScan.Attributes.Remove("disabled");
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
            }
        }
        protected void ddl_show_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindgrid(1);
        }
        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindgrid(1);
        }
    }
}