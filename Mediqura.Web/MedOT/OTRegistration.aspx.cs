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
    public partial class OTRegistration : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                if (Request["IP"] != null && Request["IP"] != "")
                {
                    tabcontainerpatient.ActiveTabIndex = 1;
                    txt_Ipno.Text = Request["IP"].ToString();
                    if (txt_Ipno.Text != "")
                    {
                        bindOtpatientlist(1);
                    }
                }
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_ottheater, mstlookup.GetLookupsList(LookupName.OTtheater));
            Commonfunction.PopulateDdl(dd_ottheaters, mstlookup.GetLookupsList(LookupName.OTtheater));
            Commonfunction.PopulateDdl(ddl_otrole, mstlookup.GetLookupsList(LookupName.OTroles));
            Commonfunction.PopulateDdl(ddl_case, mstlookup.GetLookupsList(LookupName.OTcase));
            Commonfunction.PopulateDdl(ddl_doctor, mstlookup.GetLookupsList(LookupName.OTpayabledoctors));
            Commonfunction.PopulateDdl(ddl_otstatus, mstlookup.GetLookupsList(LookupName.OT_statusType));
            Commonfunction.PopulateDdl(ddl_anestesiatype, mstlookup.GetLookupsList(LookupName.Anaesthesia));
            Commonfunction.Insertzeroitemindex(ddl_employee);
            Session["OTdetailList"] = null;
            Session["Procedurelist"] = null;
            Session["Anaesthesialist"] = null;
            btnprint.Attributes["disabled"] = "disabled";
            btnprints.Attributes["disabled"] = "disabled";
            txtautoIPNo.ReadOnly = true;
            txt_otpass.ReadOnly = false;
            txt_Otdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txt_Otdate.ReadOnly = true;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPNo(string prefixText, int count, string contextKey)
        {
            IPServiceRecordData Objpaic = new IPServiceRecordData();
            IPServiceRecordBO objInfoBO = new IPServiceRecordBO();
            List<IPServiceRecordData> getResult = new List<IPServiceRecordData>();
            Objpaic.IPNo = prefixText;
            getResult = objInfoBO.getIPNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetOTpassNumber(string prefixText, int count, string contextKey)
        {
            IPServiceRecordData Objpaic = new IPServiceRecordData();
            IPServiceRecordBO objInfoBO = new IPServiceRecordBO();
            List<IPServiceRecordData> getResult = new List<IPServiceRecordData>();
            Objpaic.Otpassnumber = prefixText;
            getResult = objInfoBO.GetOTpassnumber(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].Otpassnumber.ToString());
            }
            return list;
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPatientName(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        protected void txt_otpass_TextChanged(object sender, EventArgs e)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.OTpassnumber = txt_otpass.Text.Trim() == "" ? "" : txt_otpass.Text.Trim();
            getResult = objInfoBO.GetPatientDetailsByOtpassnumber(Objpaic);
            if (getResult.Count > 0)
            {
                txtautoIPNo.Text = getResult[0].IPNo.ToString();
                txt_patientNames.Text = getResult[0].PatientName.ToString() + " | Sex :" + getResult[0].Gender.ToString();
                txt_address.Text = getResult[0].Address.ToString();
                txt_admissiondatetime.Text = getResult[0].AdmissionDate.ToString();
                txtautoIPNo.ReadOnly = true;
            }
            else
            {
                txt_patientNames.Text = "";
                txt_address.Text = "";
                txt_admissiondatetime.Text = "";
                txtautoIPNo.Focus();
                txtautoIPNo.ReadOnly = true;
            }
        }
        protected void txtautoIPNo_TextChanged(object sender, EventArgs e)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.IPNo = txtautoIPNo.Text.Trim() == "" ? "" : txtautoIPNo.Text.Trim();
            getResult = objInfoBO.GetPatientDetailsByIPNo(Objpaic);
            if (getResult.Count > 0)
            {
                txt_patientNames.Text = getResult[0].PatientName.ToString() + " | Sex :" + getResult[0].Gender.ToString();
                txt_address.Text = getResult[0].Address.ToString();
                txt_admissiondatetime.Text = getResult[0].AdmissionDate.ToString();
            }
            else
            {
                txt_patientNames.Text = "";
                txt_address.Text = "";
                txt_admissiondatetime.Text = "";
                txtautoIPNo.Focus();
            }
        }
        protected void bindgrid()
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
            try
            {
                List<OTRegnData> objdeposit = GetOT_Registration(0);
                if (objdeposit.Count > 0)
                {
                    GvOTregn.DataSource = objdeposit;
                    GvOTregn.DataBind();
                    GvOTregn.Visible = true;
                    lblmessage.Visible = true;
                    div1.Visible = true;
                    //Messagealert_.ShowMessage(lblresult, "Total: " + objdeposit[0].MaximumRows.ToString() + " Record found", 1);
                    //divmsg3.Attributes["class"] = "SucessAlert";
                    //divmsg3.Visible = true;
                    if (txt_patientNames.Text == "")
                    {
                        txt_patientNames.Text = objdeposit[0].PatientName.ToString();
                    }
                }
                else
                {
                    GvOTregn.DataSource = null;
                    GvOTregn.DataBind();
                    GvOTregn.Visible = true;
                    GvOTregn.Visible = false;
                    //div1.Visible = false;
                    //lblresult.Visible = false;

                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }

        }
        protected void bindOtpatientlist(int page)
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
            try
            {

                List<OTRegnData> objdeposit = GetOT_RegistrationListCustom(page);
                if (objdeposit.Count > 0)
                {
                    Gv_Otregistrationlist.VirtualItemCount = objdeposit[0].MaximumRows;//total item is required for custom paging
                    Gv_Otregistrationlist.PageIndex = page - 1;
                    Gv_Otregistrationlist.DataSource = objdeposit;
                    Gv_Otregistrationlist.DataBind();
                    Gv_Otregistrationlist.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblmessage.Visible = true;
                    div1.Visible = true;
                    Messagealert_.ShowMessage(lblresult2, "Total: " + objdeposit[0].MaximumRows.ToString() + " Record(s) found", 1);
                    div2.Attributes["class"] = "SucessAlert";
                    div2.Visible = true;
                    btnprints.Attributes.Remove("disabled");
                }
                else
                {
                    Gv_Otregistrationlist.DataSource = null;
                    Gv_Otregistrationlist.DataBind();
                    Gv_Otregistrationlist.Visible = true;
                    div1.Visible = false;
                    lblresult2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<OTRegnData> GetOT_Registration(int curIndex)
        {
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            OTRegnData objpat = new OTRegnData();
            OTRegnBO objbillingBO = new OTRegnBO();
            objpat.IPNo = txtautoIPNo.Text == "" ? null : txtautoIPNo.Text;
            return objbillingBO.GetOT_Registration(objpat);
        }
        public List<OTRegnData> GetOT_RegistrationList(int curIndex)
        {
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            OTRegnData objpat = new OTRegnData();
            OTRegnBO objbillingBO = new OTRegnBO();
            objpat.IPNo = txt_Ipno.Text == "" ? null : txt_Ipno.Text;
            objpat.OTpassnumber = txt_Otpassnumber.Text == "" ? null : txt_Otpassnumber.Text;
            objpat.PatientName = txtpatientNames.Text == "" ? null : txtpatientNames.Text;
            objpat.OTemployeeID = Convert.ToInt32(ddl_doctor.SelectedValue == "" ? "0" : ddl_doctor.SelectedValue);
            objpat.OTtype = Convert.ToInt32(dd_ottheaters.SelectedValue == "" ? "0" : dd_ottheaters.SelectedValue);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? GlobalConstant.MaxdateAddOneYear : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            objpat.OTstatus = Convert.ToInt32(ddl_otstatus.SelectedValue == "" ? "0" : ddl_otstatus.SelectedValue);
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objbillingBO.GetOT_RegistrationList(objpat);
        }
        public List<OTRegnData> GetOT_RegistrationListCustom(int p)
        {
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            OTRegnData objpat = new OTRegnData();
            OTRegnBO objbillingBO = new OTRegnBO();
            objpat.IPNo = txt_Ipno.Text == "" ? null : txt_Ipno.Text;
            objpat.OTpassnumber = txt_Otpassnumber.Text == "" ? null : txt_Otpassnumber.Text;
            objpat.PatientName = txtpatientNames.Text == "" ? null : txtpatientNames.Text;
            objpat.OTemployeeID = Convert.ToInt32(ddl_doctor.SelectedValue == "" ? "0" : ddl_doctor.SelectedValue);
            objpat.OTtype = Convert.ToInt32(dd_ottheaters.SelectedValue == "" ? "0" : dd_ottheaters.SelectedValue);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? GlobalConstant.MaxdateAddOneYear : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            objpat.OTstatus = Convert.ToInt32(ddl_otstatus.SelectedValue == "" ? "0" : ddl_otstatus.SelectedValue);
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objpat.CurrentIndex = p;
            return objbillingBO.GetOT_RegistrationListCustom(objpat);
        }
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            if (txt_patientNames.Text != "")
            {
                bindOtpatientlist(1);
            }
        }
        protected void GvOTregn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvOTregn.PageIndex = e.NewPageIndex;
            bindgrid();
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

                if (txtautoIPNo.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "IPNo", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txtautoIPNo.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_ottheater.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "OTtheater", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_ottheater.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (txt_Otdate.Text == "" || Commonfunction.isValidDate(txt_Otdate.Text) == false)
                {
                    Messagealert_.ShowMessage(lblmessage, "Operationdate", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_Otdate.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }

                OTRegnData objotreg = new OTRegnData();
                OTRegnBO objregBO = new OTRegnBO();
                List<OTRegnData> Listbill = new List<OTRegnData>();
                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                // get all the record from the gridview

                int otrolecount = 0; int CheckMaiSurgeon = 0;
                foreach (GridViewRow row in GvOTregn.Rows)
                {
                    Label employeeID = (Label)GvOTregn.Rows[row.RowIndex].Cells[0].FindControl("lbl_employeeID");
                    Label Employeetype = (Label)GvOTregn.Rows[row.RowIndex].Cells[0].FindControl("lbl_employeetype");
                    Label RoleID = (Label)GvOTregn.Rows[row.RowIndex].Cells[0].FindControl("lblroleID");
                    CheckBox Checkmain = (CheckBox)GvOTregn.Rows[row.RowIndex].Cells[0].FindControl("chk_main");
                    OTRegnData ObjDetails = new OTRegnData();
                    otrolecount = otrolecount + 1;
                    if (RoleID.Text == "1" && Checkmain.Checked == true)
                    {
                        CheckMaiSurgeon = CheckMaiSurgeon + 1;
                        ObjDetails.IsMainSurgeon = 1;
                    }
                    else
                    {
                        ObjDetails.IsMainSurgeon = 0;
                    }
                    ObjDetails.OTroleID = Convert.ToInt32(RoleID.Text == "" ? "0" : RoleID.Text);
                    ObjDetails.OTemployeeTypeID = Convert.ToInt32(Employeetype.Text == "" ? "0" : Employeetype.Text);
                    ObjDetails.OTemployeeID = Convert.ToInt64(employeeID.Text == "" ? "0" : employeeID.Text);
                    Listbill.Add(ObjDetails);
                }
                objotreg.XMLData = XmlConvertor.OtregistrationtoXML(Listbill).ToString();

                int Casecount = 0;
                List<OTRegnData> listprocedure = new List<OTRegnData>();
                foreach (GridViewRow row in Gv_procedure.Rows)
                {
                    Label caseID = (Label)Gv_procedure.Rows[row.RowIndex].Cells[0].FindControl("lbl_procedureID");
                    OTRegnData ObjDetails = new OTRegnData();
                    Casecount = Casecount + 1;
                    ObjDetails.CaseID = Convert.ToInt32(caseID.Text == "" ? "0" : caseID.Text);
                    listprocedure.Add(ObjDetails);
                }
                objotreg.Proce_XMLData = XmlConvertor.OtproceduretoXML(listprocedure).ToString();
                List<OTRegnData> listanaesthesia = new List<OTRegnData>();
                int Anaesthesiacount = 0;
                foreach (GridViewRow row in Gv_Anaesthesia.Rows)
                {
                    Label AnaesthID = (Label)Gv_Anaesthesia.Rows[row.RowIndex].Cells[0].FindControl("lbl_anaesthesiaID");
                    OTRegnData ObjDetails = new OTRegnData();
                    Anaesthesiacount = Anaesthesiacount + 1;
                    ObjDetails.AnaesthesiaType = Convert.ToInt32(AnaesthID.Text == "" ? "0" : AnaesthID.Text);
                    listanaesthesia.Add(ObjDetails);
                }
                objotreg.Anas_XMLData = XmlConvertor.OtanaesthesiatoXML(listanaesthesia).ToString();
                objotreg.IPNo = txtautoIPNo.Text == "" ? null : txtautoIPNo.Text;
                objotreg.OTNo = txt_OTno.Text == "" ? "" : txt_OTno.Text;
                objotreg.OTpassnumber = txt_otpass.Text == "" ? null : txt_otpass.Text;
                DateTime Otdate = txt_Otdate.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_Otdate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                objotreg.OpernDate = Otdate;
                objotreg.OTtype = Convert.ToInt32(ddl_ottheater.SelectedValue == "" ? "0" : ddl_ottheater.SelectedValue);
                objotreg.EmployeeID = LogData.EmployeeID;
                objotreg.HospitalID = LogData.HospitalID;
                objotreg.FinancialYearID = LogData.FinancialYearID;
                if (otrolecount == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "OTrole", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (CheckMaiSurgeon != 1)
                {
                    Messagealert_.ShowMessage(lblmessage, "mainsurgeon", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (Casecount == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Procedure", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (Casecount > 1)
                {
                    Messagealert_.ShowMessage(lblmessage, "Procedurecount", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (Anaesthesiacount == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Anaesthesiatype", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                List<OTRegnData> result = objregBO.UpdateOTRegnDetails(objotreg);
                if (result[0].OTNo != "5")
                {
                    txt_OTno.Text = result[0].OTNo.ToString();
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    div1.Attributes["class"] = "SucessAlert";
                    div1.Visible = true;
                    Session["OTdetailList"] = null;
                    Session["Procedurelist"] = null;
                    Session["Anaesthesialist"] = null;
                    btnsave.Attributes["disabled"] = "disabled";
                    btnprint.Attributes.Remove("disabled");
                }
                if (result[0].OTNo == "5")
                {
                    txt_OTno.Text = "";
                    Messagealert_.ShowMessage(lblmessage, "DuplicateOtregd", 0);
                    div1.Attributes["class"] = "FailAlert";
                    div1.Visible = true;
                    btnsave.Attributes.Remove("disabled");
                    btnprint.Attributes["disabled"] = "disabled";
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;
            }
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            lblmessage.Visible = false;
            txtautoIPNo.Text = "";
            txt_patientNames.Text = "";
            txt_address.Text = "";
            div1.Visible = false;
            ddl_employee.SelectedIndex = 0;
            ddl_case.SelectedIndex = 0;
            ddl_otrole.SelectedIndex = 0;
            ddl_ottheater.SelectedIndex = 0;
            txt_Otdate.Text = "";
            GvOTregn.Visible = false;
            GvOTregn.DataSource = null;
            txt_OTno.Text = "";
            txt_otpass.ReadOnly = false;
            Session["OTdetailList"] = null;
            Session["Procedurelist"] = null;
            Session["Anaesthesialist"] = null;
            Gv_Anaesthesia.Visible = false;
            Gv_Anaesthesia.DataSource = null;
            Gv_procedure.Visible = false;
            Gv_procedure.DataSource = null;
            btnprint.Attributes["disabled"] = "disabled";
            txt_otpass.Text = "";
            ddl_anestesiatype.SelectedIndex = 0;
            ddlstatus.SelectedIndex = 0;
            txt_Otdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
        protected void Gv_Otregistrationlist_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Int64 ID = Convert.ToInt32(Gv_Otregistrationlist.DataKeys[e.RowIndex].Values["OTRegnID"].ToString());
            System.Web.UI.WebControls.Label Otnumber = (System.Web.UI.WebControls.Label)Gv_Otregistrationlist.Rows[e.RowIndex].FindControl("lblotno");
            System.Web.UI.WebControls.Label StatusID = (System.Web.UI.WebControls.Label)Gv_Otregistrationlist.Rows[e.RowIndex].FindControl("lbl_otstatusID");
            OTRegnData objdata = new OTRegnData();
            OTRegnBO objIntimationBO = new OTRegnBO();
            objdata.OTRegnID = ID;
            objdata.OTNo = Otnumber.Text.Trim();
            objdata.EmployeeID = LogData.EmployeeID;
            objdata.HospitalID = LogData.HospitalID;
            objdata.OT_status = Convert.ToInt16(StatusID.Text == "" ? "0" : StatusID.Text);
            objdata.IPaddress = LogData.IPaddress;
            int Result = objIntimationBO.UpdateOtstatus(objdata);
            if (Result == 1)
            {
                bindOtpatientlist(1);
            }
        }
        protected void ddl_employee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_otrole.SelectedIndex > 0)
            {
                Addnew();
            }
        }
        protected void Addnew()
        {
            if (txtautoIPNo.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "IPNo", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtautoIPNo.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
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
                div1.Visible = false;
            }

            List<OTRegnData> OTdetailList = Session["OTdetailList"] == null ? new List<OTRegnData>() : (List<OTRegnData>)Session["OTdetailList"];
            OTRegnData ObjService = new OTRegnData();

            ObjService.PatientName = txt_patientNames.Text.Trim();
            ObjService.IPNo = txtautoIPNo.Text.Trim();
            ObjService.OTemployee = ddl_employee.SelectedItem.Text;

            ObjService.OTemployeeID = Convert.ToInt64(ddl_employee.SelectedValue == "" ? "0" : ddl_employee.SelectedValue);
            string ID;
            var source = ddl_employee.SelectedItem.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                ObjService.OTemployeeTypeID = Convert.ToInt32(ID.ToString() == "Internal" ? 1 : 2);
            }
            foreach (GridViewRow row in GvOTregn.Rows)
            {
                Label EmployeeID = (Label)GvOTregn.Rows[row.RowIndex].Cells[0].FindControl("lbl_employeeID");
                Label employetype = (Label)GvOTregn.Rows[row.RowIndex].Cells[0].FindControl("lbl_employeetype");

                if (Convert.ToInt64(EmployeeID.Text == "" ? "0" : EmployeeID.Text) == Convert.ToInt64(ddl_employee.SelectedValue == "" ? "0" : ddl_employee.SelectedValue) && Convert.ToInt32(employetype.Text == "" ? "0" : employetype.Text) == Convert.ToInt32(source.Substring(source.LastIndexOf(':') + 1) == "Internal" ? 1 : 2))
                {
                    Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
            }
            ObjService.OTroleID = Convert.ToInt32(ddl_otrole.SelectedValue == "" ? "0" : ddl_otrole.SelectedValue);
            ObjService.OTrole = ddl_otrole.SelectedItem.Text.Trim();
            ObjService.IsMainSurgeon = 0;
            OTdetailList.Add(ObjService);
            if (OTdetailList.Count > 0)
            {
                GvOTregn.DataSource = OTdetailList;
                GvOTregn.DataBind();
                GvOTregn.Visible = true;
                Session["OTdetailList"] = OTdetailList;
                btnsave.Attributes.Remove("disabled");
            }
            else
            {
                btnsave.Attributes["disabled"] = "disabled";
                GvOTregn.DataSource = null;
                GvOTregn.DataBind();
                GvOTregn.Visible = true;
            }
        }
        protected void GvOTregn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvOTregn.Rows[i];
                    List<OTRegnData> ItemList = Session["OTdetailList"] == null ? new List<OTRegnData>() : (List<OTRegnData>)Session["OTdetailList"];
                    ItemList.RemoveAt(i);
                    Session["OTdetailList"] = ItemList;
                    GvOTregn.DataSource = ItemList;
                    GvOTregn.DataBind();
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
        }
        protected void ddl_case_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_case.SelectedIndex > 0)
            {
                AddProcedure();
            }
        }
        protected void AddProcedure()
        {
            if (txtautoIPNo.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "IPNo", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtautoIPNo.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            List<OTRegnData> Procedurelist = Session["Procedurelist"] == null ? new List<OTRegnData>() : (List<OTRegnData>)Session["Procedurelist"];
            OTRegnData ObjService = new OTRegnData();
            ObjService.CaseID = Convert.ToInt32(ddl_case.SelectedValue == "" ? "0" : ddl_case.SelectedValue);
            ObjService.CaseName = ddl_case.SelectedItem.Text;
            foreach (GridViewRow row in Gv_procedure.Rows)
            {
                Label CaseID = (Label)Gv_procedure.Rows[row.RowIndex].Cells[0].FindControl("lbl_procedureID");
                if (Convert.ToInt32(CaseID.Text == "" ? "0" : CaseID.Text) == Convert.ToInt32(ddl_case.SelectedValue == "" ? "0" : ddl_case.SelectedValue))
                {
                    Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
            }
            Procedurelist.Add(ObjService);
            if (Procedurelist.Count > 0)
            {
                Gv_procedure.DataSource = Procedurelist;
                Gv_procedure.DataBind();
                Gv_procedure.Visible = true;
                Session["Procedurelist"] = Procedurelist;
                btnsave.Attributes.Remove("disabled");
            }
            else
            {
                btnsave.Attributes["disabled"] = "disabled";
                Gv_procedure.DataSource = null;
                Gv_procedure.DataBind();
                Gv_procedure.Visible = true;
            }
        }
        protected void Gv_procedure_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = Gv_procedure.Rows[i];
                    List<OTRegnData> ItemList = Session["Procedurelist"] == null ? new List<OTRegnData>() : (List<OTRegnData>)Session["Procedurelist"];
                    ItemList.RemoveAt(i);
                    Session["Procedurelist"] = ItemList;
                    Gv_procedure.DataSource = ItemList;
                    Gv_procedure.DataBind();
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
        }
        protected void ddl_anestesiatype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_anestesiatype.SelectedIndex > 0)
            {
                Addanaesthesia();
            }
        }
        protected void Addanaesthesia()
        {
            if (txtautoIPNo.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "IPNo", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtautoIPNo.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            List<OTRegnData> Anaesthesialist = Session["Anaesthesialist"] == null ? new List<OTRegnData>() : (List<OTRegnData>)Session["Anaesthesialist"];
            OTRegnData ObjService = new OTRegnData();

            ObjService.AnaesthesiaType = Convert.ToInt32(ddl_anestesiatype.SelectedValue == "" ? "0" : ddl_anestesiatype.SelectedValue);
            ObjService.AnaesthesiaName = ddl_anestesiatype.SelectedItem.Text == "" ? "0" : ddl_anestesiatype.SelectedItem.Text;
            foreach (GridViewRow row in Gv_Anaesthesia.Rows)
            {
                Label AnaesthesiaID = (Label)Gv_Anaesthesia.Rows[row.RowIndex].Cells[0].FindControl("lbl_anaesthesiaID");
                if (Convert.ToInt32(AnaesthesiaID.Text == "" ? "0" : AnaesthesiaID.Text) == Convert.ToInt32(ddl_anestesiatype.SelectedValue == "" ? "0" : ddl_anestesiatype.SelectedValue))
                {
                    Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
            }
            Anaesthesialist.Add(ObjService);
            if (Anaesthesialist.Count > 0)
            {
                Gv_Anaesthesia.DataSource = Anaesthesialist;
                Gv_Anaesthesia.DataBind();
                Gv_Anaesthesia.Visible = true;
                Session["Anaesthesialist"] = Anaesthesialist;
                btnsave.Attributes.Remove("disabled");
            }
            else
            {
                btnsave.Attributes["disabled"] = "disabled";
                Gv_Anaesthesia.DataSource = null;
                Gv_Anaesthesia.DataBind();
                Gv_Anaesthesia.Visible = true;
            }
        }
        protected void Gv_Anaesthesia_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = Gv_Anaesthesia.Rows[i];
                    List<OTRegnData> ItemList = Session["Anaesthesialist"] == null ? new List<OTRegnData>() : (List<OTRegnData>)Session["Anaesthesialist"];
                    ItemList.RemoveAt(i);
                    Session["Anaesthesialist"] = ItemList;
                    Gv_Anaesthesia.DataSource = ItemList;
                    Gv_Anaesthesia.DataBind();
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
        }
        protected void Gv_Otregistrationlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label StatusID = (Label)e.Row.FindControl("lbl_otstatusID");
                Label Status = (Label)e.Row.FindControl("lbl_status");
                Button btn_update = (Button)e.Row.FindControl("btn_update");
                if (StatusID.Text == "0")
                {
                    Status.Text = "Start";
                    btn_update.Visible = true;
                }
                if (StatusID.Text == "1")
                {
                    Status.Text = "Complete";
                    btn_update.Visible = true;
                }
                if (StatusID.Text == "2")
                {
                    Status.Text = "Completed";
                    btn_update.Visible = false;
                }
            }
        }
        protected void GvOTregn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label StatusID = (Label)e.Row.FindControl("lbl_main");
                CheckBox Chek_main = (CheckBox)e.Row.FindControl("chk_main");

                if (StatusID.Text == "1")
                {
                    Chek_main.Checked = true;
                }
                else
                {
                    Chek_main.Checked = false;
                }
            }
        }
        public void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    Gv_Otregistrationlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    Gv_Otregistrationlist.Columns[9].Visible = false;
                    Gv_Otregistrationlist.Columns[10].Visible = false;
                    Gv_Otregistrationlist.RenderControl(hw);
                    Gv_Otregistrationlist.HeaderRow.Style.Add("width", "15%");
                    Gv_Otregistrationlist.HeaderRow.Style.Add("font-size", "10px");
                    Gv_Otregistrationlist.Style.Add("text-decoration", "none");
                    Gv_Otregistrationlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    Gv_Otregistrationlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=OTRegistrationDetails.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Deposit Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=OTRegistrationDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<OTRegnData> OTRegnDetails = GetOT_RegistrationList(0);
            List<OTRegnListDataTOeXCEL> ListexcelData = new List<OTRegnListDataTOeXCEL>();
            int i = 0;
            foreach (OTRegnData row in OTRegnDetails)
            {
                OTRegnListDataTOeXCEL Ecxeclpat = new OTRegnListDataTOeXCEL();
                Ecxeclpat.OTpassnumber = OTRegnDetails[i].OTpassnumber.ToString();
                Ecxeclpat.OTNo = OTRegnDetails[i].OTNo.ToString();
                Ecxeclpat.IPNo = OTRegnDetails[i].IPNo.ToString();
                Ecxeclpat.PatientName = OTRegnDetails[i].PatientName.ToString();
                Ecxeclpat.Description = OTRegnDetails[i].Description.ToString();
                Ecxeclpat.OTDate = OTRegnDetails[i].OperationDate.ToString();
                if (OTRegnDetails[i].OperationTime == null)
                {
                    Ecxeclpat.OperationTime = "";
                }
                else
                {
                    Ecxeclpat.OperationTime = OTRegnDetails[i].OperationTime.ToString();
                }
                if (OTRegnDetails[i].OperationEndTime == null)
                {
                    Ecxeclpat.OperationEndTime = "";
                }
                else
                {
                    Ecxeclpat.OperationEndTime = OTRegnDetails[i].OperationEndTime.ToString();
                }
                if (OTRegnDetails[i].OT_status == 0)
                {
                    Ecxeclpat.OTStatus = "Not started";
                }
                else if (OTRegnDetails[i].OT_status == 1)
                {
                    Ecxeclpat.OTStatus = "Started";
                }
                else if (OTRegnDetails[i].OT_status == 2)
                {
                    Ecxeclpat.OTStatus = "Completed";

                }
                else
                {
                    Ecxeclpat.OTStatus = " ";
                }


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

                // Get all the properties

                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo prop in Props)
                {

                    //  Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);

                }

                foreach (T item in items)
                {

                    var values = new object[Props.Length];

                    for (int i = 0; i < Props.Length; i++)
                    {

                        //       inserting property values to datatable rows

                        values[i] = Props[i].GetValue(item, null);

                    }

                    dataTable.Rows.Add(values);

                }

                //     put a breakpoint here and check datatable

                return dataTable;

            }
        }
        protected void btnprint_Click(object sender, EventArgs e)
        {
            if (LogData.PrintEnable == 0)
            {
                btnprint.Attributes["disabled"] = "disabled";
            }
            else
            {
                btnprint.Attributes.Remove("disabled");
            }
        }
        protected void ddl_otrole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_otrole.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_employee, mstlookup.GetOTemployees(Convert.ToInt32(ddl_otrole.SelectedValue == "" ? "0" : ddl_otrole.SelectedValue)));
            }
            else
            {
                ddl_employee.ClearSelection();
                Commonfunction.Insertzeroitemindex(ddl_employee);
            }
        }
        protected void txt_Ipno_TextChanged(object sender, EventArgs e)
        {
            if (txt_Ipno.Text != "")
            {
                bindOtpatientlist(1);
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindOtpatientlist(1);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            lblmessage2.Visible = false;
            lblresult2.Visible = false;
            txt_Ipno.Text = "";
            txtpatientNames.Text = "";
            div2.Visible = false;
            ddl_doctor.SelectedIndex = 0;
            dd_ottheaters.SelectedIndex = 0;
            ddlstatus.SelectedIndex = 0;
            txtdatefrom.Text = "";
            txtto.Text = "";
            txt_Otpassnumber.Text = "";
            Gv_Otregistrationlist.Visible = false;
            Gv_Otregistrationlist.DataSource = null;
            btnexport.Visible = false;
            ddlexport.Visible = false;
            btnprints.Attributes["disabled"] = "disabled";
        }
        protected void Gv_Otregistrationlist_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    GridViewRow gr = Gv_Otregistrationlist.Rows[i];
                    Label otNo = (Label)gr.Cells[0].FindControl("lblotno");
                    Label IPNo = (Label)gr.Cells[0].FindControl("lblIPno");
                    string Otnmber = otNo.Text.Trim();
                    string Ipnumber = IPNo.Text.Trim();
                    EditRegistrationDetails(Otnmber, Ipnumber);
                    tabcontainerpatient.ActiveTabIndex = 0;
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
                    OTRegnData objIntimation = new OTRegnData();
                    OTRegnBO objIntimationBO = new OTRegnBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = Gv_Otregistrationlist.Rows[i];
                    Label otNo = (Label)gr.Cells[0].FindControl("lblotno");
                    Label IPNo = (Label)gr.Cells[0].FindControl("lblIPno");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult2, "Remarks", 0);
                        div2.Attributes["class"] = "FailAlert";
                        div2.Visible = true;
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objIntimation.Remarks = txtremarks.Text;
                    }
                    objIntimation.OTNo = otNo.Text;
                    objIntimation.EmployeeID = LogData.EmployeeID;
                    objIntimation.HospitalID = LogData.HospitalID;
                    objIntimation.IPaddress = LogData.IPaddress;
                    int Result = objIntimationBO.DeleteOTByID(objIntimation);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        lblmessage2.Visible = true;
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "SucessAlert";
                        bindOtpatientlist(1);
                    }
                    if (Result == 2)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "OTcancel", 0);
                        lblmessage2.Visible = true;
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        bindOtpatientlist(1);
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
        }
        protected void EditRegistrationDetails(string Otnumber, string IPnumber)
        {
            OTRegnData objregd = new OTRegnData();
            OTRegnBO objBO = new OTRegnBO();
            objregd.OTNo = Otnumber;
            objregd.IPNo = IPnumber;
            List<OTRegnData> Result = objBO.Edit_RegistrationList(objregd);
            if (Result.Count > 0)
            {
                if (Result[0].PayoutStatus.ToString() == "1")
                {
                    Messagealert_.ShowMessage(lblmessage2, "OTpayment", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                txt_otpass.ReadOnly = true;
                txt_otpass.Text = Result[0].OTpassnumber.ToString();
                txtautoIPNo.Text = Result[0].IPNo.ToString();
                txt_patientNames.Text = Result[0].PatientName.ToString();
                txt_address.Text = Result[0].Address.ToString();
                txt_OTno.Text = Result[0].OTNo.ToString();
                txt_Otdate.Text = Result[0].OperationDate.ToString("dd/MM/yyy");
                ddl_ottheater.SelectedValue = Result[0].OTtype.ToString();
                txt_admissiondatetime.Text = Result[0].AdmissionDate.ToString("dd/MM/yyy");
                bindotrolesdetails(Otnumber, IPnumber);
                bindproceduredetails(Otnumber, IPnumber);
                bindanasthesiadetails(Otnumber, IPnumber);
                btnsave.Attributes.Remove("disabled");
            }
            else
            {
                txt_otpass.ReadOnly = false;
                txt_otpass.Text = "";
                txtautoIPNo.Text = "";
                txt_address.Text = "";
                txt_OTno.Text = "";
                txt_Otdate.Text = "";
                ddl_ottheater.SelectedIndex = 0;
                txt_admissiondatetime.Text = "";
                btnsave.Attributes["disabled"] = "disabled";
            }
        }
        protected void bindotrolesdetails(string otnumber, string Ipnumber)
        {
            OTRegnData objregd = new OTRegnData();
            OTRegnBO objBO = new OTRegnBO();
            objregd.OTNo = otnumber;
            objregd.IPNo = Ipnumber;
            List<OTRegnData> Result = objBO.Edit_Otroledetails(objregd);
            if (Result.Count > 0)
            {
                List<OTRegnData> ItemList = Session["OTdetailList"] == null ? new List<OTRegnData>() : (List<OTRegnData>)Session["OTdetailList"];
                Session["OTdetailList"] = Result;
                GvOTregn.Visible = true;
                GvOTregn.DataSource = Result;
                GvOTregn.DataBind();
            }
            else
            {
                GvOTregn.Visible = true;
                Session["OTdetailList"] = null;
                GvOTregn.DataSource = null;
                GvOTregn.DataBind();
            }
        }
        protected void bindproceduredetails(string otnumber, string Ipnumber)
        {
            OTRegnData objregd = new OTRegnData();
            OTRegnBO objBO = new OTRegnBO();
            objregd.OTNo = otnumber;
            objregd.IPNo = Ipnumber;
            List<OTRegnData> Result = objBO.Edit_OtProceduredetails(objregd);
            if (Result.Count > 0)
            {
                List<OTRegnData> ItemList = Session["Procedurelist"] == null ? new List<OTRegnData>() : (List<OTRegnData>)Session["Procedurelist"];
                Session["Procedurelist"] = Result;
                Gv_procedure.Visible = true;
                Gv_procedure.DataSource = Result;
                Gv_procedure.DataBind();
            }
            else
            {
                Session["Procedurelist"] = null;
                Gv_procedure.Visible = true;
                Gv_procedure.DataSource = null;
                Gv_procedure.DataBind();
            }
        }
        protected void bindanasthesiadetails(string otnumber, string Ipnumber)
        {
            OTRegnData objregd = new OTRegnData();
            OTRegnBO objBO = new OTRegnBO();
            objregd.OTNo = otnumber;
            objregd.IPNo = Ipnumber;
            List<OTRegnData> Result = objBO.Edit_Otanasthesiadetails(objregd);
            if (Result.Count > 0)
            {
                List<OTRegnData> ItemList = Session["Anaesthesialist"] == null ? new List<OTRegnData>() : (List<OTRegnData>)Session["Anaesthesialist"];
                Session["Anaesthesialist"] = Result;
                Gv_Anaesthesia.Visible = true;
                Gv_Anaesthesia.DataSource = Result;
                Gv_Anaesthesia.DataBind();
            }
            else
            {
                Session["Anaesthesialist"] = null;
                Gv_Anaesthesia.Visible = true;
                Gv_Anaesthesia.DataSource = null;
                Gv_Anaesthesia.DataBind();
            }
        }
        protected void btnexport_Click1(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "ExportEnable", 0);
                div2.Visible = true;
                div2.Attributes["class"] = "FailAlert";
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
                Messagealert_.ShowMessage(lblmessage2, "ExportType", 0);
                div2.Visible = true;
                div2.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        protected void btnprints_Click(object sender, EventArgs e)
        {
            if (LogData.PrintEnable == 0)
            {
                btnprints.Attributes["disabled"] = "disabled";
            }
            else
            {
                btnprints.Attributes.Remove("disabled");
            }
        }
        protected void Gv_Otregistrationlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindOtpatientlist(Convert.ToInt32(e.NewPageIndex + 1));
        }
    }
}