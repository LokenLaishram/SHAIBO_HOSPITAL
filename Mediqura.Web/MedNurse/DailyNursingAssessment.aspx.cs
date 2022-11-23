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
using Mediqura.CommonData.MedNurseData;
using Mediqura.BOL.MedNurseBO;

namespace Mediqura.Web.MedNurse
{
    public partial class DailyNursingAssessment : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            if (txtpatientNames.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "patientname", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else if (txtpatientNames.Text != "")
            {
                lblmessage.Visible = false;
                DailyNursingAssessmentData objpat = new DailyNursingAssessmentData();
                DailyNursingAssessmentBO objBO = new DailyNursingAssessmentBO();
                var source = txtpatientNames.Text.ToString();
                if (source.Contains(":"))
                {
                    string IPNo = source.Substring(source.LastIndexOf(':') + 1);
                    objpat.IPNo = (IPNo == "" ? "0" : IPNo);
                }
                List<DailyNursingAssessmentData> result = objBO.GetPatientDetailByID(objpat);
                if (result.Count > 0)
                {
                    txtage.Text = result[0].AgeCount.ToString();
                    txtsex.Text = result[0].Sex.ToString();
                    txtdoa.Text = result[0].AdmissionDate.ToString("dd/MM/yyyy hh:mm tt");
                    txtbedroom.Text = result[0].WardBedName.ToString();
                    txtipno.Text = result[0].IPNo.ToString();
                    txtuhid.Text = result[0].UHID.ToString();
                    txtdischargestatus.Text = result[0].DischargeStatus.ToString();
                    clearnurseshift();
                    List<DailyNursingAssessmentData> res = objBO.GetNursingAssementByIPNo(objpat);
                    if (res.Count > 0)
                    {
                        ddlnurseshift.SelectedIndex = Convert.ToInt32(res[0].NurseShift.ToString());
                        txtid.Text = res[0].ID.ToString();
                        Int64 ID = Convert.ToInt64(txtid.Text);
                        Int32 NurShiftID = Convert.ToInt32(ddlnurseshift.SelectedIndex);
                        ViewState["ID"] = ID;
                        if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour < 12)
                        {
                            ddlnurseshift.SelectedValue = "1";
                        }
                        else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
                        {
                            ddlnurseshift.SelectedValue = "2";
                        }
                        else if (DateTime.Now.Hour >= 18)
                        {
                            ddlnurseshift.SelectedValue = "3";
                        }
                        else if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 6)
                        {
                            ddlnurseshift.SelectedValue = "3";
                        }
                        EditPatient(ID, NurShiftID);
                        return;
                    }
                    else
                    {
                        ddlnurseshift.SelectedIndex = 0;
                    }
                    if (txtdischargestatus.Text != "1")
                    {
                        Messagealert_.ShowMessage(lblmessage, "PatientDischarge", 0);
                        divmsg1.Visible = true;
                        btnsave.Attributes["disabled"] = "disabled";
                        txtpatientNames.Focus();
                    }
                }
                else
                {
                    clearall();
                    ddlnurseshift.SelectedIndex = 0;
                }
            }
        }
        protected void ddlnurseshift_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlnurseshift.SelectedIndex != 0)
            {
                DailyNursingAssessmentData objpat = new DailyNursingAssessmentData();
                DailyNursingAssessmentBO objBO = new DailyNursingAssessmentBO();
                objpat.IPNo = txtipno.Text.ToString();
                Int64 ID = Convert.ToInt64(txtid.Text == "" ? "0" : txtid.Text.ToString());
                Int32 NurShiftID = Convert.ToInt32(ddlnurseshift.Text == "" ? "0" : ddlnurseshift.Text.ToString());
                if (ddlnurseshift.SelectedIndex == 3)
                {
                    EditPatient(ID, NurShiftID);
                }
                else if (ddlnurseshift.SelectedIndex == 2)
                {
                    EditPatient(ID, NurShiftID);
                }
                else if (ddlnurseshift.SelectedIndex == 1)
                {
                    EditPatient(ID, NurShiftID);
                }
            }
            else
            {
                clearnurseshift();
            }


        }
        protected void txtpatientsNames_TextChanged(object sender, EventArgs e)
        {
            if (txtpatientdetails.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "patientname", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else if (txtpatientdetails.Text != "")
            {

                lblmessage.Visible = false;
                DailyNursingAssessmentData objpat = new DailyNursingAssessmentData();
                DailyNursingAssessmentBO objBO = new DailyNursingAssessmentBO();
                var source = txtpatientdetails.Text.ToString();
                if (source.Contains(":"))
                {
                    string IPNo = source.Substring(source.LastIndexOf(':') + 1);
                    objpat.IPNo = (IPNo == "" ? "0" : IPNo);
                }
                List<DailyNursingAssessmentData> result = objBO.GetPatientDetailByID(objpat);
                if (result.Count > 0)
                {
                    txtage.Text = result[0].Age.ToString();
                    txtsex.Text = result[0].Sex.ToString();
                    txtdoa.Text = result[0].AdmissionDate.ToString("dd/MM/yyyy hh:mm tt");
                    txtbedroom.Text = result[0].WardBedName.ToString();
                    txtipno.Text = result[0].IPNo.ToString();
                    txtipnos.Text = result[0].IPNo.ToString();
                    txtuhid.Text = result[0].UHID.ToString();
                    bindgrid(1);
                }
                else
                {
                    //clearall();
                }
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetAdmittedPatientsDetails(string prefixText, int count, string contextKey)
        {
            DailyNursingAssessmentData Objpaic = new DailyNursingAssessmentData();
            DailyNursingAssessmentBO objInfoBO = new DailyNursingAssessmentBO();
            List<DailyNursingAssessmentData> getResult = new List<DailyNursingAssessmentData>();
            Objpaic.PatientDetails = prefixText;
            getResult = objInfoBO.GetAdmittedPatientDetails(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            DailyNursingAssessmentData ObjService = new DailyNursingAssessmentData();
            DailyNursingAssessmentBO objServiceBO = new DailyNursingAssessmentBO();
            ObjService.PatientName = txtpatientNames.Text.ToString();
            ObjService.UHID = Convert.ToInt64(txtuhid.Text.ToString());
            ObjService.IPNo = txtipno.Text.ToString();
            ObjService.WardBedName = txtbedroom.Text;
            ObjService.AgeCount = txtage.Text;
            ObjService.Sex = txtsex.Text;
            ObjService.AdmissionDate = Convert.ToDateTime(txtdoa.Text);
            if (ddlnurseshift.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "NurseShift", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                return;
            }
            else
            {
                lblmessage.Visible = false;
                ObjService.NurseShift = Convert.ToInt32(ddlnurseshift.SelectedValue == "" ? "0" : ddlnurseshift.SelectedValue);
                ObjService.EmotionalState = Convert.ToInt32(ddlemotionalstate.SelectedValue == "" ? "0" : ddlemotionalstate.SelectedValue);
                ObjService.Consciousness = Convert.ToInt32(ddlconsciousness.SelectedValue == "" ? "0" : ddlconsciousness.SelectedValue);
                ObjService.Speech = Convert.ToInt32(ddlspeech.SelectedValue == "" ? "0" : ddlspeech.SelectedValue);
                ObjService.PhysicalType = Convert.ToInt32(ddlphysicaltype.SelectedValue == "" ? "0" : ddlphysicaltype.SelectedValue);
                if (rborientedtimeyes.Checked)
                {
                    ObjService.RbTime = Convert.ToInt32(1);
                }
                else
                {
                    ObjService.RbTime = Convert.ToInt32(0);
                }
                if (Rborientedplaceyes.Checked)
                {
                    ObjService.RbPlace = Convert.ToInt32(1);
                }
                else
                {
                    ObjService.RbPlace = Convert.ToInt32(0);
                }
                if (Rborientedpersonyes.Checked)
                {
                    ObjService.RbPerson = Convert.ToInt32(1);
                }
                else
                {
                    ObjService.RbPerson = Convert.ToInt32(0);
                }
                ObjService.Respiratory = Convert.ToInt32(ddlrespiratory.SelectedValue == "" ? "0" : ddlrespiratory.SelectedValue);
                ObjService.Pulse = txtpulse.Text.Trim() == "" ? null : txtpulse.Text.Trim();
                ObjService.Breath = txtbreath.Text.Trim() == "" ? null : txtbreath.Text.Trim();
                ObjService.Cough = txtcough.Text.Trim() == "" ? null : txtcough.Text.Trim();
                ObjService.Oxygen = txtoxygen.Text.Trim() == "" ? null : txtoxygen.Text.Trim();
                ObjService.ChestDrain = Convert.ToInt32(ddlchestdrain.SelectedValue == "" ? "0" : ddlchestdrain.SelectedValue);
                if (RbL_Plueralyes.Checked)
                {
                    ObjService.RbL_Plueral = Convert.ToInt32(1);
                }
                else
                {
                    ObjService.RbL_Plueral = Convert.ToInt32(0);
                }
                if (RbR_Plueralyes.Checked)
                {
                    ObjService.RbR_Plueral = Convert.ToInt32(1);
                }
                else
                {
                    ObjService.RbR_Plueral = Convert.ToInt32(0);
                }
                ObjService.Cardio = Convert.ToInt32(ddlcardio.SelectedValue == "" ? "0" : ddlcardio.SelectedValue);
                ObjService.Tension = Convert.ToInt32(ddltension.SelectedValue == "" ? "0" : ddltension.SelectedValue);
                ObjService.Peripheral = Convert.ToInt32(ddlperipheral.SelectedValue == "" ? "0" : ddlperipheral.SelectedValue);

                if (rbneckdistensionyes.Checked)
                {
                    ObjService.RbDistension = Convert.ToInt32(1);
                }
                else
                {
                    ObjService.RbDistension = Convert.ToInt32(0);
                }
                if (rbchestpainyes.Checked)
                {
                    ObjService.RbChestpain = Convert.ToInt32(1);
                }
                else
                {
                    ObjService.RbChestpain = Convert.ToInt32(0);
                }
                ObjService.Gastrointestinalmouth = Convert.ToInt32(ddlgastrointestinalmouth.SelectedValue == "" ? "0" : ddlgastrointestinalmouth.SelectedValue);
                ObjService.Gastrointestinalteeth = Convert.ToInt32(ddlgastrointestinalteeth.SelectedValue == "" ? "0" : ddlgastrointestinalteeth.SelectedValue);
                ObjService.Gastrointestinaltongue = Convert.ToInt32(ddlgastrointestinaltongue.SelectedValue == "" ? "0" : ddlgastrointestinaltongue.SelectedValue);
                if (rboralulcersyes.Checked)
                {
                    ObjService.RbOralulcers = Convert.ToInt32(1);
                }
                else
                {
                    ObjService.RbOralulcers = Convert.ToInt32(0);
                }
                if (rbdistensionyes.Checked)
                {
                    ObjService.RbAbndistension = Convert.ToInt32(1);
                }
                else
                {
                    ObjService.RbAbndistension = Convert.ToInt32(0);
                }
                if (rbnauseayes.Checked)
                {
                    ObjService.RbNausea = Convert.ToInt32(1);
                }
                else
                {
                    ObjService.RbNausea = Convert.ToInt32(0);
                }
                if (rbvomittingyes.Checked)
                {
                    ObjService.RbVomitting = Convert.ToInt32(1);
                }
                else
                {
                    ObjService.RbVomitting = Convert.ToInt32(0);
                }
                if (rbnpoyes.Checked)
                {
                    ObjService.RbNpo = Convert.ToInt32(1);
                }
                else
                {
                    ObjService.RbNpo = Convert.ToInt32(0);
                }
                ObjService.Nutrition = Convert.ToInt32(ddlnutrition.SelectedValue == "" ? "0" : ddlnutrition.SelectedValue);
                if (ddlnutrition.SelectedIndex != 0)
                {
                    if (ddlnutrition.SelectedIndex == 1)
                    {
                        if (rbnutritionyes.Checked)
                        {
                            ObjService.RbOral = Convert.ToInt32(1);
                        }
                        else
                        {
                            ObjService.RbOral = Convert.ToInt32(0);
                        }
                    }
                    if (ddlnutrition.SelectedIndex == 2)
                    {
                        if (rbnutritionyes.Checked)
                        {
                            ObjService.RbTubefeeding = Convert.ToInt32(1);
                        }
                        else
                        {
                            ObjService.RbTubefeeding = Convert.ToInt32(0);
                        }
                    }
                    if (ddlnutrition.SelectedIndex == 3)
                    {
                        if (rbnutritionyes.Checked)
                        {
                            ObjService.RbParenteral = Convert.ToInt32(1);
                        }
                        else
                        {
                            ObjService.RbParenteral = Convert.ToInt32(0);
                        }
                    }
                }
                else
                {
                    ObjService.RbOral = Convert.ToInt32(0);
                    ObjService.RbTubefeeding = Convert.ToInt32(0);
                    ObjService.RbParenteral = Convert.ToInt32(0);
                }

                if (rbbowelyes.Checked)
                {
                    ObjService.RbBowel = Convert.ToInt32(1);
                }
                else
                {
                    ObjService.RbBowel = Convert.ToInt32(0);
                }
                if (rbconstipationyes.Checked)
                {
                    ObjService.RbConstipation = Convert.ToInt32(1);
                }
                else
                {
                    ObjService.RbConstipation = Convert.ToInt32(0);
                }
                if (rbdiarrhoeayes.Checked)
                {
                    ObjService.RbDiarrhoea = Convert.ToInt32(1);
                }
                else
                {
                    ObjService.RbDiarrhoea = Convert.ToInt32(0);
                }
                if (rbmalenayes.Checked)
                {
                    ObjService.RbMalena = Convert.ToInt32(1);
                }
                else
                {
                    ObjService.RbMalena = Convert.ToInt32(0);
                }
                ObjService.Mouth = Convert.ToInt32(ddlmouth.SelectedValue == "" ? "0" : ddlmouth.SelectedValue);
                if (rbUrineyes.Checked)
                {
                    ObjService.RbUrine = Convert.ToInt32(1);
                }
                else
                {
                    ObjService.RbUrine = Convert.ToInt32(0);
                }
                if (rbHematuriayes.Checked)
                {
                    ObjService.RbHematuria = Convert.ToInt32(1);
                }
                else
                {
                    ObjService.RbHematuria = Convert.ToInt32(0);
                }
                ObjService.Skin = Convert.ToInt32(ddlskin.SelectedValue == "" ? "0" : ddlskin.SelectedValue);
                ObjService.Cyanosis = Convert.ToInt32(ddlcyanosis.SelectedValue == "" ? "0" : ddlcyanosis.SelectedValue);
                ObjService.Peripheries = Convert.ToInt32(ddlperipheries.SelectedValue == "" ? "0" : ddlperipheries.SelectedValue);
                ObjService.Oedemasite = txtoedemasite.Text.Trim() == "" ? null : txtoedemasite.Text.Trim();
                ObjService.Temperature = Convert.ToInt32(ddltemperature.SelectedValue == "" ? "0" : ddltemperature.SelectedValue);
                ObjService.Scalp = Convert.ToInt32(ddlscalp.SelectedValue == "" ? "0" : ddlscalp.SelectedValue);
                ObjService.Eyes = Convert.ToInt32(ddleyes.SelectedValue == "" ? "0" : ddleyes.SelectedValue);
                ObjService.Nose = Convert.ToInt32(ddlnose.SelectedValue == "" ? "0" : ddlnose.SelectedValue);
                ObjService.Ear = Convert.ToInt32(ddlear.SelectedValue == "" ? "0" : ddlear.SelectedValue);
                ObjService.Sleep = Convert.ToInt32(ddlsleep.SelectedValue == "" ? "0" : ddlsleep.SelectedValue);
                ObjService.Joint = Convert.ToInt32(ddljoint.SelectedValue == "" ? "0" : ddljoint.SelectedValue);
                ObjService.Ambulate = Convert.ToInt32(ddlambulate.SelectedValue == "" ? "0" : ddlambulate.SelectedValue);
                ObjService.Sitecentralline = txtsitecentralline.Text.Trim() == "" ? null : txtsitecentralline.Text.Trim();
                ObjService.Conditioncentralline = txtconditioncentralline.Text.Trim() == "" ? null : txtconditioncentralline.Text.Trim();
                ObjService.Siteperipheralline = txtsiteperipheralline.Text.Trim() == "" ? null : txtsiteperipheralline.Text.Trim();
                ObjService.Conditionperipheralline = txtconditionperipheralline.Text.Trim() == "" ? null : txtconditionperipheralline.Text.Trim();
                ObjService.Sitearterialline = txtsitearterialline.Text.Trim() == "" ? null : txtsitearterialline.Text.Trim();
                ObjService.Conditionarterialline = txtconditionarterialline.Text.Trim() == "" ? null : txtconditionarterialline.Text.Trim();
                ObjService.Siteanyotherline = txtsiteanyotherline.Text.Trim() == "" ? null : txtsiteanyotherline.Text.Trim();
                ObjService.Conditionanyotherline = txtconditionanyotherline.Text.Trim();
                if (rbhealthyyes.Checked)
                {
                    ObjService.RbHealthy = Convert.ToInt32(1);
                }
                else
                {
                    ObjService.RbHealthy = Convert.ToInt32(0); ;
                }
                if (rbdressingyes.Checked)
                {
                    ObjService.RbDressing = Convert.ToInt32(1);
                }
                else
                {
                    ObjService.RbDressing = Convert.ToInt32(0);
                }
                ObjService.PainScale = Convert.ToInt32(ddlPainScale.SelectedValue == "" ? "0" : ddlPainScale.SelectedValue);
                ObjService.AddedBy = LogData.AddedBy;
                ObjService.EmployeeID = LogData.EmployeeID;
                ObjService.HospitalID = LogData.HospitalID;
                ObjService.FinancialYearID = LogData.FinancialYearID;
                ObjService.ActionType = Enumaction.Insert;
                ViewState["ID"] = txtid.Text == "" ? null : txtid.Text;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        divmsg1.Visible = true;
                        txtpatientNames.Focus();
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }

                    ObjService.ActionType = Enumaction.Update;
                    ObjService.ID = Convert.ToInt64(ViewState["ID"].ToString() == "" ? "0" : ViewState["ID"].ToString());
                }
                int results = objServiceBO.UpdateDailyNursingAssessment(ObjService);
                if (results == 1)
                {
                    txtpatientNames.Focus();
                    btnsave.Attributes["disabled"] = "disabled";
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Attributes["class"] = "SucessAlert";
                    divmsg1.Visible = true;
                }
                else if (results == 2)
                {
                    txtpatientNames.Focus();
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    btnsave.Attributes["disabled"] = "disabled";
                    divmsg1.Attributes["class"] = "SucessAlert";
                    divmsg1.Visible = true;
                }
                else
                {
                    // txtpatientNames.Focus();
                    btnsave.Attributes.Remove("disabled");
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                }
            }
        }

        protected void btnresets_Click(object sender, System.EventArgs e)
        {
            clearall();
        }
        private void clearnurseshift()
        {
            ddlemotionalstate.SelectedIndex = 0;
            ddlconsciousness.SelectedIndex = 0;
            ddlspeech.SelectedIndex = 0;
            ddlphysicaltype.SelectedIndex = 0;
            rborientedtimeyes.Checked = false;
            rborientedtimeno.Checked = false;
            Rborientedplaceyes.Checked = false;
            Rborientedplaceno.Checked = false;
            Rborientedpersonyes.Checked = false;
            Rborientedpersonno.Checked = false;
            ddlrespiratory.SelectedIndex = 0;
            txtpulse.Text = "";
            txtbreath.Text = "";
            txtcough.Text = "";
            txtoxygen.Text = "";
            ddlchestdrain.SelectedIndex = 0;
            RbL_Plueralyes.Checked = false;
            RbL_Plueralno.Checked = false;
            RbR_Plueralyes.Checked = false;
            RbR_Plueralno.Checked = false;
            ddlcardio.SelectedIndex = 0;
            ddltension.SelectedIndex = 0;
            ddlperipheral.SelectedIndex = 0;
            rbneckdistensionyes.Checked = false;
            rbneckdistensionno.Checked = false;
            rbchestpainyes.Checked = false;
            rbchestpainno.Checked = false;
            ddlgastrointestinalmouth.SelectedIndex = 0;
            ddlgastrointestinalteeth.SelectedIndex = 0;
            ddlgastrointestinaltongue.SelectedIndex = 0;
            rboralulcersyes.Checked = false;
            rboralulcersno.Checked = false;
            rbdistensionyes.Checked = false;
            rbdistensionno.Checked = false;
            rbnauseayes.Checked = false;
            rbnauseano.Checked = false;
            rbvomittingyes.Checked = false;
            rbvomittingno.Checked = false;
            rbnpoyes.Checked = false;
            rbnpono.Checked = false;
            ddlnutrition.SelectedIndex = 0;
            rbnutritionyes.Checked = false;
            rbnutritionno.Checked = false;
            rbbowelyes.Checked = false;
            rbbowelno.Checked = false;
            rbconstipationyes.Checked = false;
            rbconstipationno.Checked = false;
            rbdiarrhoeayes.Checked = false;
            rbdiarrhoeano.Checked = false;
            rbmalenayes.Checked = false;
            rbmalenano.Checked = false;
            ddlmouth.SelectedIndex = 0;
            rbUrineyes.Checked = false;
            rbUrineno.Checked = false;
            rbHematuriayes.Checked = false;
            rbHematuriano.Checked = false;
            ddlskin.SelectedIndex = 0;
            ddlcyanosis.SelectedIndex = 0;
            ddlperipheries.SelectedIndex = 0;
            txtoedemasite.Text = "";
            ddltemperature.SelectedIndex = 0;
            ddlscalp.SelectedIndex = 0;
            ddleyes.SelectedIndex = 0;
            ddlnose.SelectedIndex = 0;
            ddlear.SelectedIndex = 0;
            ddlsleep.SelectedIndex = 0;
            ddljoint.SelectedIndex = 0;
            ddlambulate.SelectedIndex = 0;
            txtsitecentralline.Text = "";
            txtconditioncentralline.Text = "";
            txtsiteperipheralline.Text = "";
            txtconditionperipheralline.Text = "";
            txtsitearterialline.Text = "";
            txtconditionarterialline.Text = "";
            txtsiteanyotherline.Text = "";
            txtconditionanyotherline.Text = "";
            rbhealthyyes.Checked = false;
            rbhealthyno.Checked = false;
            rbdressingyes.Checked = false;
            rbdressingno.Checked = false;
            ddlPainScale.SelectedIndex = 0;

        }
        private void clearall()
        {
            lblmessage.Visible = false;
            lblresult.Visible = false;
            divmsg1.Visible = false;
            txtpatientNames.Text = "";
            txtipno.Text = "";
            txtuhid.Text = "";
            txtid.Text = "";
            txtbedroom.Text = "";
            txtage.Text = "";
            txtsex.Text = "";
            txtdoa.Text = "";
            ddlnurseshift.SelectedIndex = 0;
            clearnurseshift();
            txtpatientdetails.Text = "";
            txtipnos.Text = "";
            txtdatefrom.Text = "";
            txtdateto.Text = "";
            //ddl_user.SelectedIndex = 0;
            ddlstatus.SelectedIndex = 0;
            GvNursingAssessment.DataSource = null;
            GvNursingAssessment.DataBind();
            GvNursingAssessment.Visible = false;
            btnexport.Visible = false;
            ddlexport.Visible = false;
            btnsave.Attributes.Remove("disabled");
        }

        protected void GvNursingAssessment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid(1);
        }
        private List<DailyNursingAssessmentData> GetNurseNotesList(int p)
        {
            DailyNursingAssessmentData objpat = new DailyNursingAssessmentData();
            DailyNursingAssessmentBO objBO = new DailyNursingAssessmentBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objpat.IPNo = txtipno.Text.ToString();
            DateTime DateFrom = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime DateTo = txtdateto.Text.Trim() == "" ? GlobalConstant.MaxdateAddOneYear : DateTime.Parse(txtdateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.DateFrom = DateFrom;
            objpat.DateTo = DateTo;

            return objBO.GetNurseProgressSheet(objpat);

        }
        protected void GvNursingAssessment_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    GridViewRow gr = GvNursingAssessment.Rows[i];
                    Label PatID = (Label)gr.Cells[0].FindControl("lblID");
                    Label NurseShiftID = (Label)gr.Cells[0].FindControl("lblNurseShiftID");
                    Int64 ID = Convert.ToInt64(PatID.Text);
                    Int32 NurShiftID = Convert.ToInt32(NurseShiftID.Text);
                    txtid.Text = Convert.ToString(ID);
                    EditPatient(ID, NurShiftID);
                    tabcontainerNursingAssesment.ActiveTabIndex = 0;
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

                    DailyNursingAssessmentData objpatnt = new DailyNursingAssessmentData();
                    DailyNursingAssessmentBO objstdBO = new DailyNursingAssessmentBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvNursingAssessment.Rows[i];
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
        protected void EditPatient(Int64 patID, Int32 NurShiftID)
        {
            try
            {
                List<DailyNursingAssessmentData> patientdetails = GetEditPatientDetails(patID, NurShiftID);
                if (patientdetails.Count > 0)
                {
                    txtpatientNames.Text = patientdetails[0].PatientName.ToString();
                    txtipno.Text = patientdetails[0].IPNo.ToString();
                    txtuhid.Text = patientdetails[0].UHID.ToString();
                    txtbedroom.Text = patientdetails[0].WardBedName.ToString();
                    txtage.Text = patientdetails[0].AgeCount.ToString();
                    txtsex.Text = patientdetails[0].Sex.ToString();
                    txtdoa.Text = patientdetails[0].AdmissionDate.ToString();
                    if (patientdetails[0].NurseShift == 1)
                    {
                        ddlnurseshift.SelectedValue = patientdetails[0].NurseShift.ToString();
                        ddlemotionalstate.SelectedValue = patientdetails[0].EmotionalState.ToString();
                        ddlconsciousness.SelectedValue = patientdetails[0].Consciousness.ToString();
                        ddlspeech.SelectedValue = patientdetails[0].Speech.ToString();
                        ddlphysicaltype.SelectedValue = patientdetails[0].PhysicalType.ToString();
                        ddlrespiratory.SelectedValue = patientdetails[0].Respiratory.ToString();
                        txtpulse.Text = patientdetails[0].Pulse.ToString();
                        txtbreath.Text = patientdetails[0].Breath.ToString();
                        txtcough.Text = patientdetails[0].Cough.ToString();
                        txtoxygen.Text = patientdetails[0].Oxygen.ToString();
                        ddlchestdrain.SelectedValue = patientdetails[0].ChestDrain.ToString();
                        ddlcardio.SelectedValue = patientdetails[0].Cardio.ToString();
                        ddltension.SelectedValue = patientdetails[0].Tension.ToString();
                        ddlperipheral.SelectedValue = patientdetails[0].Peripheral.ToString();
                        ddlgastrointestinalmouth.SelectedValue = patientdetails[0].Gastrointestinalmouth.ToString();
                        ddlgastrointestinalteeth.SelectedValue = patientdetails[0].Gastrointestinalteeth.ToString();
                        ddlgastrointestinaltongue.SelectedValue = patientdetails[0].Gastrointestinaltongue.ToString();
                        ddlnutrition.SelectedValue = patientdetails[0].Nutrition.ToString();
                        ddlmouth.SelectedValue = patientdetails[0].Mouth.ToString();
                        ddlskin.SelectedValue = patientdetails[0].Skin.ToString();
                        ddlcyanosis.SelectedValue = patientdetails[0].Cyanosis.ToString();
                        ddlperipheries.SelectedValue = patientdetails[0].Peripheries.ToString();
                        txtoedemasite.Text = patientdetails[0].Oedemasite.ToString();
                        ddltemperature.SelectedValue = patientdetails[0].Temperature.ToString();
                        ddlscalp.SelectedValue = patientdetails[0].Scalp.ToString();
                        ddleyes.SelectedValue = patientdetails[0].Eyes.ToString();
                        ddlnose.SelectedValue = patientdetails[0].Nose.ToString();
                        ddlear.SelectedValue = patientdetails[0].Ear.ToString();
                        ddlsleep.SelectedValue = patientdetails[0].Sleep.ToString();
                        ddljoint.SelectedValue = patientdetails[0].Joint.ToString();
                        ddlambulate.SelectedValue = patientdetails[0].Ambulate.ToString();
                        txtsitecentralline.Text = patientdetails[0].Sitecentralline.ToString();
                        txtconditioncentralline.Text = patientdetails[0].Conditioncentralline.ToString();
                        txtsiteperipheralline.Text = patientdetails[0].Siteperipheralline.ToString();
                        txtconditionperipheralline.Text = patientdetails[0].Conditionperipheralline.ToString();
                        txtsitearterialline.Text = patientdetails[0].Sitearterialline.ToString();
                        txtconditionarterialline.Text = patientdetails[0].Conditionarterialline.ToString();
                        txtsiteanyotherline.Text = patientdetails[0].Siteanyotherline.ToString();
                        txtconditionanyotherline.Text = patientdetails[0].Conditionanyotherline.ToString();
                        ddlPainScale.SelectedValue = patientdetails[0].PainScale.ToString();
                        if (patientdetails[0].RbTime == 1)
                        {
                            rborientedtimeyes.Checked = true;
                            rborientedtimeno.Checked = false;
                        }
                        if (patientdetails[0].RbTime == 0)
                        {
                            rborientedtimeyes.Checked = false;
                            rborientedtimeno.Checked = true;
                        }
                        if (patientdetails[0].RbPlace == 1)
                        {
                            Rborientedplaceyes.Checked = true;
                            Rborientedplaceno.Checked = false;
                        }
                        if (patientdetails[0].RbPlace == 0)
                        {
                            Rborientedplaceyes.Checked = false;
                            Rborientedplaceno.Checked = true;
                        }
                        if (patientdetails[0].RbPerson == 1)
                        {
                            Rborientedpersonyes.Checked = true;
                            Rborientedpersonno.Checked = false;
                        }
                        if (patientdetails[0].RbPerson == 0)
                        {
                            Rborientedpersonyes.Checked = false;
                            Rborientedpersonno.Checked = true;
                        }
                        if (patientdetails[0].RbL_Plueral == 1)
                        {
                            RbL_Plueralyes.Checked = true;
                            RbL_Plueralno.Checked = false;
                        }
                        if (patientdetails[0].RbL_Plueral == 0)
                        {
                            RbL_Plueralyes.Checked = false;
                            RbL_Plueralno.Checked = true;
                        }
                        if (patientdetails[0].RbR_Plueral == 1)
                        {
                            RbR_Plueralyes.Checked = true;
                            RbR_Plueralno.Checked = false;
                        }
                        if (patientdetails[0].RbR_Plueral == 0)
                        {
                            RbR_Plueralyes.Checked = false;
                            RbR_Plueralno.Checked = true;
                        }
                        if (patientdetails[0].RbDistension == 1)
                        {
                            rbneckdistensionyes.Checked = true;
                            rbneckdistensionno.Checked = false;
                        }
                        if (patientdetails[0].RbDistension == 0)
                        {
                            rbneckdistensionyes.Checked = false;
                            rbneckdistensionno.Checked = true;
                        }
                        if (patientdetails[0].RbChestpain == 1)
                        {
                            rbchestpainyes.Checked = true;
                            rbchestpainno.Checked = false;
                        }
                        if (patientdetails[0].RbChestpain == 0)
                        {
                            rbchestpainyes.Checked = false;
                            rbchestpainno.Checked = true;
                        }
                        if (patientdetails[0].RbOralulcers == 1)
                        {
                            rboralulcersyes.Checked = true;
                            rboralulcersno.Checked = false;
                        }
                        if (patientdetails[0].RbOralulcers == 0)
                        {
                            rboralulcersyes.Checked = false;
                            rboralulcersno.Checked = true;
                        }
                        if (patientdetails[0].RbAbndistension == 1)
                        {
                            rbdistensionyes.Checked = true;
                            rbdistensionno.Checked = false;
                        }
                        if (patientdetails[0].RbAbndistension == 0)
                        {
                            rbdistensionyes.Checked = false;
                            rbdistensionno.Checked = true;
                        }
                        if (patientdetails[0].RbNausea == 1)
                        {
                            rbnauseayes.Checked = true;
                            rbnauseano.Checked = false;
                        }
                        if (patientdetails[0].RbNausea == 0)
                        {
                            rbnauseayes.Checked = false;
                            rbnauseano.Checked = true;
                        }
                        if (patientdetails[0].RbVomitting == 1)
                        {
                            rbvomittingyes.Checked = true;
                            rbvomittingno.Checked = false;
                        }
                        if (patientdetails[0].RbVomitting == 0)
                        {
                            rbvomittingyes.Checked = false;
                            rbvomittingno.Checked = true;
                        }
                        if (patientdetails[0].RbNpo == 1)
                        {
                            rbnpoyes.Checked = true;
                            rbnpono.Checked = false;
                        }
                        if (patientdetails[0].RbNpo == 0)
                        {
                            rbnpoyes.Checked = false;
                            rbnpono.Checked = true;
                        }
                        if (patientdetails[0].Nutrition == 1)
                        {
                            if (patientdetails[0].RbOral == 1)
                            {
                                rbnutritionyes.Checked = true;
                                rbnutritionno.Checked = false;
                            }

                            if (patientdetails[0].RbOral == 0)
                            {
                                rbnutritionyes.Checked = false;
                                rbnutritionno.Checked = true;
                            }
                        }
                        else if (patientdetails[0].Nutrition == 2)
                        {
                            if (patientdetails[0].RbTubefeeding == 1)
                            {
                                rbnutritionyes.Checked = true;
                                rbnutritionno.Checked = false;
                            }
                            if (patientdetails[0].RbTubefeeding == 0)
                            {
                                rbnutritionyes.Checked = false;
                                rbnutritionno.Checked = true;
                            }
                        }
                        else if (patientdetails[0].Nutrition == 3)
                        {
                            if (patientdetails[0].RbParenteral == 1)
                            {
                                rbnutritionyes.Checked = true;
                                rbnutritionno.Checked = false;
                            }
                            if (patientdetails[0].RbParenteral == 0)
                            {
                                rbnutritionyes.Checked = false;
                                rbnutritionno.Checked = true;
                            }
                        }
                        else
                        {
                            rbnutritionyes.Checked = false;
                            rbnutritionno.Checked = false;
                        }
                        if (patientdetails[0].RbBowel == 1)
                        {
                            rbbowelyes.Checked = true;
                            rbbowelno.Checked = false;
                        }
                        if (patientdetails[0].RbBowel == 0)
                        {
                            rbbowelyes.Checked = false;
                            rbbowelno.Checked = true;
                        }
                        if (patientdetails[0].RbConstipation == 1)
                        {
                            rbconstipationyes.Checked = true;
                            rbconstipationno.Checked = false;
                        }
                        if (patientdetails[0].RbConstipation == 0)
                        {
                            rbconstipationyes.Checked = false;
                            rbconstipationno.Checked = true;
                        }
                        if (patientdetails[0].RbDiarrhoea == 1)
                        {
                            rbdiarrhoeayes.Checked = true;
                            rbdiarrhoeano.Checked = false;
                        }
                        if (patientdetails[0].RbDiarrhoea == 0)
                        {
                            rbdiarrhoeayes.Checked = false;
                            rbdiarrhoeano.Checked = true;
                        }
                        if (patientdetails[0].RbMalena == 1)
                        {
                            rbmalenayes.Checked = true;
                            rbmalenano.Checked = false;
                        }
                        if (patientdetails[0].RbMalena == 0)
                        {
                            rbmalenayes.Checked = false;
                            rbmalenano.Checked = true;
                        }
                        if (patientdetails[0].RbUrine == 1)
                        {
                            rbUrineyes.Checked = true;
                            rbUrineno.Checked = false;
                        }
                        if (patientdetails[0].RbUrine == 0)
                        {
                            rbUrineyes.Checked = false;
                            rbUrineno.Checked = true;
                        }
                        if (patientdetails[0].RbHematuria == 1)
                        {
                            rbHematuriayes.Checked = true;
                            rbHematuriano.Checked = false;
                        }
                        if (patientdetails[0].RbHematuria == 0)
                        {
                            rbHematuriayes.Checked = false;
                            rbHematuriano.Checked = true;
                        }
                        if (patientdetails[0].RbHealthy == 1)
                        {
                            rbhealthyyes.Checked = true;
                            rbhealthyno.Checked = false;
                        }
                        if (patientdetails[0].RbHealthy == 0)
                        {
                            rbhealthyyes.Checked = false;
                            rbhealthyno.Checked = true;
                        }
                        if (patientdetails[0].RbDressing == 1)
                        {
                            rbdressingyes.Checked = true;
                            rbdressingno.Checked = false;
                        }
                        if (patientdetails[0].RbDressing == 0)
                        {
                            rbdressingyes.Checked = false;
                            rbdressingno.Checked = true;
                        }
                    }
                    if (patientdetails[0].NurseShift == 2)
                    {
                        ddlnurseshift.SelectedValue = patientdetails[0].NurseShift.ToString();
                        ddlemotionalstate.SelectedValue = patientdetails[0].EmotionalState.ToString();
                        ddlconsciousness.SelectedValue = patientdetails[0].Consciousness.ToString();
                        ddlspeech.SelectedValue = patientdetails[0].Speech.ToString();
                        ddlphysicaltype.SelectedValue = patientdetails[0].PhysicalType.ToString();
                        ddlrespiratory.SelectedValue = patientdetails[0].Respiratory.ToString();
                        txtpulse.Text = patientdetails[0].Pulse.ToString();
                        txtbreath.Text = patientdetails[0].Breath.ToString();
                        txtcough.Text = patientdetails[0].Cough.ToString();
                        txtoxygen.Text = patientdetails[0].Oxygen.ToString();
                        ddlchestdrain.SelectedValue = patientdetails[0].ChestDrain.ToString();
                        ddlcardio.SelectedValue = patientdetails[0].Cardio.ToString();
                        ddltension.SelectedValue = patientdetails[0].Tension.ToString();
                        ddlperipheral.SelectedValue = patientdetails[0].Peripheral.ToString();
                        ddlgastrointestinalmouth.SelectedValue = patientdetails[0].Gastrointestinalmouth.ToString();
                        ddlgastrointestinalteeth.SelectedValue = patientdetails[0].Gastrointestinalteeth.ToString();
                        ddlgastrointestinaltongue.SelectedValue = patientdetails[0].Gastrointestinaltongue.ToString();
                        ddlnutrition.SelectedValue = patientdetails[0].Nutrition.ToString();
                        ddlmouth.SelectedValue = patientdetails[0].Mouth.ToString();
                        ddlskin.SelectedValue = patientdetails[0].Skin.ToString();
                        ddlcyanosis.SelectedValue = patientdetails[0].Cyanosis.ToString();
                        ddlperipheries.SelectedValue = patientdetails[0].Peripheries.ToString();
                        txtoedemasite.Text = patientdetails[0].Oedemasite.ToString();
                        ddltemperature.SelectedValue = patientdetails[0].Temperature.ToString();
                        ddlscalp.SelectedValue = patientdetails[0].Scalp.ToString();
                        ddleyes.SelectedValue = patientdetails[0].Eyes.ToString();
                        ddlnose.SelectedValue = patientdetails[0].Nose.ToString();
                        ddlear.SelectedValue = patientdetails[0].Ear.ToString();
                        ddlsleep.SelectedValue = patientdetails[0].Sleep.ToString();
                        ddljoint.SelectedValue = patientdetails[0].Joint.ToString();
                        ddlambulate.SelectedValue = patientdetails[0].Ambulate.ToString();
                        txtsitecentralline.Text = patientdetails[0].Sitecentralline.ToString();
                        txtconditioncentralline.Text = patientdetails[0].Conditioncentralline.ToString();
                        txtsiteperipheralline.Text = patientdetails[0].Siteperipheralline.ToString();
                        txtconditionperipheralline.Text = patientdetails[0].Conditionperipheralline.ToString();
                        txtsitearterialline.Text = patientdetails[0].Sitearterialline.ToString();
                        txtconditionarterialline.Text = patientdetails[0].Conditionarterialline.ToString();
                        txtsiteanyotherline.Text = patientdetails[0].Siteanyotherline.ToString();
                        txtconditionanyotherline.Text = patientdetails[0].Conditionanyotherline.ToString();
                        ddlPainScale.SelectedValue = patientdetails[0].PainScale.ToString();
                        if (patientdetails[0].RbTime == 1)
                        {
                            rborientedtimeyes.Checked = true;
                            rborientedtimeno.Checked = false;
                        }
                        if (patientdetails[0].RbTime == 0)
                        {
                            rborientedtimeyes.Checked = false;
                            rborientedtimeno.Checked = true;
                        }
                        if (patientdetails[0].RbPlace == 1)
                        {
                            Rborientedplaceyes.Checked = true;
                            Rborientedplaceno.Checked = false;
                        }
                        if (patientdetails[0].RbPlace == 0)
                        {
                            Rborientedplaceyes.Checked = false;
                            Rborientedplaceno.Checked = true;
                        }
                        if (patientdetails[0].RbPerson == 1)
                        {
                            Rborientedpersonyes.Checked = true;
                            Rborientedpersonno.Checked = false;
                        }
                        if (patientdetails[0].RbPerson == 0)
                        {
                            Rborientedpersonyes.Checked = false;
                            Rborientedpersonno.Checked = true;
                        }
                        if (patientdetails[0].RbL_Plueral == 1)
                        {
                            RbL_Plueralyes.Checked = true;
                            RbL_Plueralno.Checked = false;
                        }
                        if (patientdetails[0].RbL_Plueral == 0)
                        {
                            RbL_Plueralyes.Checked = false;
                            RbL_Plueralno.Checked = true;
                        }
                        if (patientdetails[0].RbR_Plueral == 1)
                        {
                            RbR_Plueralyes.Checked = true;
                            RbR_Plueralno.Checked = false;
                        }
                        if (patientdetails[0].RbR_Plueral == 0)
                        {
                            RbR_Plueralyes.Checked = false;
                            RbR_Plueralno.Checked = true;
                        }
                        if (patientdetails[0].RbDistension == 1)
                        {
                            rbneckdistensionyes.Checked = true;
                            rbneckdistensionno.Checked = false;
                        }
                        if (patientdetails[0].RbDistension == 0)
                        {
                            rbneckdistensionyes.Checked = false;
                            rbneckdistensionno.Checked = true;
                        }
                        if (patientdetails[0].RbChestpain == 1)
                        {
                            rbchestpainyes.Checked = true;
                            rbchestpainno.Checked = false;
                        }
                        if (patientdetails[0].RbChestpain == 0)
                        {
                            rbchestpainyes.Checked = false;
                            rbchestpainno.Checked = true;
                        }
                        if (patientdetails[0].RbOralulcers == 1)
                        {
                            rboralulcersyes.Checked = true;
                            rboralulcersno.Checked = false;
                        }
                        if (patientdetails[0].RbOralulcers == 0)
                        {
                            rboralulcersyes.Checked = false;
                            rboralulcersno.Checked = true;
                        }
                        if (patientdetails[0].RbAbndistension == 1)
                        {
                            rbdistensionyes.Checked = true;
                            rbdistensionno.Checked = false;
                        }
                        if (patientdetails[0].RbAbndistension == 0)
                        {
                            rbdistensionyes.Checked = false;
                            rbdistensionno.Checked = true;
                        }
                        if (patientdetails[0].RbNausea == 1)
                        {
                            rbnauseayes.Checked = true;
                            rbnauseano.Checked = false;
                        }
                        if (patientdetails[0].RbNausea == 0)
                        {
                            rbnauseayes.Checked = false;
                            rbnauseano.Checked = true;
                        }
                        if (patientdetails[0].RbVomitting == 1)
                        {
                            rbvomittingyes.Checked = true;
                            rbvomittingno.Checked = false;
                        }
                        if (patientdetails[0].RbVomitting == 0)
                        {
                            rbvomittingyes.Checked = false;
                            rbvomittingno.Checked = true;
                        }
                        if (patientdetails[0].RbNpo == 1)
                        {
                            rbnpoyes.Checked = true;
                            rbnpono.Checked = false;
                        }
                        if (patientdetails[0].RbNpo == 0)
                        {
                            rbnpoyes.Checked = false;
                            rbnpono.Checked = true;
                        }
                        if (patientdetails[0].RbOral == 1)
                        {
                            rbnutritionyes.Checked = true;
                            rbnutritionno.Checked = false;
                        }
                        if (patientdetails[0].RbOral == 0)
                        {
                            rbnutritionyes.Checked = false;
                            rbnutritionno.Checked = true;
                        }
                        if (patientdetails[0].RbTubefeeding == 1)
                        {
                            rbnutritionyes.Checked = true;
                            rbnutritionno.Checked = false;
                        }
                        if (patientdetails[0].RbTubefeeding == 0)
                        {
                            rbnutritionyes.Checked = false;
                            rbnutritionno.Checked = true;
                        }
                        if (patientdetails[0].RbParenteral == 1)
                        {
                            rbnutritionyes.Checked = true;
                            rbnutritionno.Checked = false;
                        }
                        if (patientdetails[0].RbParenteral == 0)
                        {
                            rbnutritionyes.Checked = false;
                            rbnutritionno.Checked = true;
                        }
                        if (patientdetails[0].RbBowel == 1)
                        {
                            rbbowelyes.Checked = true;
                            rbbowelno.Checked = false;
                        }
                        if (patientdetails[0].RbBowel == 0)
                        {
                            rbbowelyes.Checked = false;
                            rbbowelno.Checked = true;
                        }
                        if (patientdetails[0].RbConstipation == 1)
                        {
                            rbconstipationyes.Checked = true;
                            rbconstipationno.Checked = false;
                        }
                        if (patientdetails[0].RbConstipation == 0)
                        {
                            rbconstipationyes.Checked = false;
                            rbconstipationno.Checked = true;
                        }
                        if (patientdetails[0].RbDiarrhoea == 1)
                        {
                            rbdiarrhoeayes.Checked = true;
                            rbdiarrhoeano.Checked = false;
                        }
                        if (patientdetails[0].RbDiarrhoea == 0)
                        {
                            rbdiarrhoeayes.Checked = false;
                            rbdiarrhoeano.Checked = true;
                        }
                        if (patientdetails[0].RbMalena == 1)
                        {
                            rbmalenayes.Checked = true;
                            rbmalenano.Checked = false;
                        }
                        if (patientdetails[0].RbMalena == 0)
                        {
                            rbmalenayes.Checked = false;
                            rbmalenano.Checked = true;
                        }
                        if (patientdetails[0].RbUrine == 1)
                        {
                            rbUrineyes.Checked = true;
                            rbUrineno.Checked = false;
                        }
                        if (patientdetails[0].RbUrine == 1)
                        {
                            rbUrineyes.Checked = false;
                            rbUrineno.Checked = true;
                        }
                        if (patientdetails[0].RbHematuria == 1)
                        {
                            rbHematuriayes.Checked = true;
                            rbHematuriano.Checked = false;
                        }
                        if (patientdetails[0].RbHematuria == 0)
                        {
                            rbHematuriayes.Checked = false;
                            rbHematuriano.Checked = true;
                        }
                        if (patientdetails[0].RbHealthy == 1)
                        {
                            rbhealthyyes.Checked = true;
                            rbhealthyno.Checked = false;
                        }
                        if (patientdetails[0].RbHealthy == 0)
                        {
                            rbhealthyyes.Checked = false;
                            rbhealthyno.Checked = true;
                        }
                        if (patientdetails[0].RbDressing == 1)
                        {
                            rbdressingyes.Checked = true;
                            rbdressingno.Checked = false;
                        }
                        if (patientdetails[0].RbDressing == 0)
                        {
                            rbdressingyes.Checked = false;
                            rbdressingno.Checked = true;
                        }
                    }
                    if (patientdetails[0].NurseShift == 3)
                    {
                        ddlnurseshift.SelectedValue = patientdetails[0].NurseShift.ToString();
                        ddlemotionalstate.SelectedValue = patientdetails[0].EmotionalState.ToString();
                        ddlconsciousness.SelectedValue = patientdetails[0].Consciousness.ToString();
                        ddlspeech.SelectedValue = patientdetails[0].Speech.ToString();
                        ddlphysicaltype.SelectedValue = patientdetails[0].PhysicalType.ToString();
                        ddlrespiratory.SelectedValue = patientdetails[0].Respiratory.ToString();
                        txtpulse.Text = patientdetails[0].Pulse.ToString();
                        txtbreath.Text = patientdetails[0].Breath.ToString();
                        txtcough.Text = patientdetails[0].Cough.ToString();
                        txtoxygen.Text = patientdetails[0].Oxygen.ToString();
                        ddlchestdrain.SelectedValue = patientdetails[0].ChestDrain.ToString();
                        ddlcardio.SelectedValue = patientdetails[0].Cardio.ToString();
                        ddltension.SelectedValue = patientdetails[0].Tension.ToString();
                        ddlperipheral.SelectedValue = patientdetails[0].Peripheral.ToString();
                        ddlgastrointestinalmouth.SelectedValue = patientdetails[0].Gastrointestinalmouth.ToString();
                        ddlgastrointestinalteeth.SelectedValue = patientdetails[0].Gastrointestinalteeth.ToString();
                        ddlgastrointestinaltongue.SelectedValue = patientdetails[0].Gastrointestinaltongue.ToString();
                        ddlnutrition.SelectedValue = patientdetails[0].Nutrition.ToString();
                        ddlmouth.SelectedValue = patientdetails[0].Mouth.ToString();
                        ddlskin.SelectedValue = patientdetails[0].Skin.ToString();
                        ddlcyanosis.SelectedValue = patientdetails[0].Cyanosis.ToString();
                        ddlperipheries.SelectedValue = patientdetails[0].Peripheries.ToString();
                        txtoedemasite.Text = patientdetails[0].Oedemasite.ToString();
                        ddltemperature.SelectedValue = patientdetails[0].Temperature.ToString();
                        ddlscalp.SelectedValue = patientdetails[0].Scalp.ToString();
                        ddleyes.SelectedValue = patientdetails[0].Eyes.ToString();
                        ddlnose.SelectedValue = patientdetails[0].Nose.ToString();
                        ddlear.SelectedValue = patientdetails[0].Ear.ToString();
                        ddlsleep.SelectedValue = patientdetails[0].Sleep.ToString();
                        ddljoint.SelectedValue = patientdetails[0].Joint.ToString();
                        ddlambulate.SelectedValue = patientdetails[0].Ambulate.ToString();
                        txtsitecentralline.Text = patientdetails[0].Sitecentralline.ToString();
                        txtconditioncentralline.Text = patientdetails[0].Conditioncentralline.ToString();
                        txtsiteperipheralline.Text = patientdetails[0].Siteperipheralline.ToString();
                        txtconditionperipheralline.Text = patientdetails[0].Conditionperipheralline.ToString();
                        txtsitearterialline.Text = patientdetails[0].Sitearterialline.ToString();
                        txtconditionarterialline.Text = patientdetails[0].Conditionarterialline.ToString();
                        txtsiteanyotherline.Text = patientdetails[0].Siteanyotherline.ToString();
                        txtconditionanyotherline.Text = patientdetails[0].Conditionanyotherline.ToString();
                        ddlPainScale.SelectedValue = patientdetails[0].PainScale.ToString();
                        if (patientdetails[0].RbTime == 1)
                        {
                            rborientedtimeyes.Checked = true;
                            rborientedtimeno.Checked = false;
                        }
                        if (patientdetails[0].RbTime == 0)
                        {
                            rborientedtimeyes.Checked = false;
                            rborientedtimeno.Checked = true;
                        }
                        if (patientdetails[0].RbPlace == 1)
                        {
                            Rborientedplaceyes.Checked = true;
                            Rborientedplaceno.Checked = false;
                        }
                        if (patientdetails[0].RbPlace == 0)
                        {
                            Rborientedplaceyes.Checked = false;
                            Rborientedplaceno.Checked = true;
                        }
                        if (patientdetails[0].RbPerson == 1)
                        {
                            Rborientedpersonyes.Checked = true;
                            Rborientedpersonno.Checked = false;
                        }
                        if (patientdetails[0].RbPerson == 0)
                        {
                            Rborientedpersonyes.Checked = false;
                            Rborientedpersonno.Checked = true;
                        }
                        if (patientdetails[0].RbL_Plueral == 1)
                        {
                            RbL_Plueralyes.Checked = true;
                            RbL_Plueralno.Checked = false;
                        }
                        if (patientdetails[0].RbL_Plueral == 0)
                        {
                            RbL_Plueralyes.Checked = false;
                            RbL_Plueralno.Checked = true;
                        }
                        if (patientdetails[0].RbR_Plueral == 1)
                        {
                            RbR_Plueralyes.Checked = true;
                            RbR_Plueralno.Checked = false;
                        }
                        if (patientdetails[0].RbR_Plueral == 0)
                        {
                            RbR_Plueralyes.Checked = false;
                            RbR_Plueralno.Checked = true;
                        }
                        if (patientdetails[0].RbDistension == 1)
                        {
                            rbneckdistensionyes.Checked = true;
                            rbneckdistensionno.Checked = false;
                        }
                        if (patientdetails[0].RbDistension == 0)
                        {
                            rbneckdistensionyes.Checked = false;
                            rbneckdistensionno.Checked = true;
                        }
                        if (patientdetails[0].RbChestpain == 1)
                        {
                            rbchestpainyes.Checked = true;
                            rbchestpainno.Checked = false;
                        }
                        if (patientdetails[0].RbChestpain == 0)
                        {
                            rbchestpainyes.Checked = false;
                            rbchestpainno.Checked = true;
                        }
                        if (patientdetails[0].RbOralulcers == 1)
                        {
                            rboralulcersyes.Checked = true;
                            rboralulcersno.Checked = false;
                        }
                        if (patientdetails[0].RbOralulcers == 0)
                        {
                            rboralulcersyes.Checked = false;
                            rboralulcersno.Checked = true;
                        }
                        if (patientdetails[0].RbAbndistension == 1)
                        {
                            rbdistensionyes.Checked = true;
                            rbdistensionno.Checked = false;
                        }
                        if (patientdetails[0].RbAbndistension == 0)
                        {
                            rbdistensionyes.Checked = false;
                            rbdistensionno.Checked = true;
                        }
                        if (patientdetails[0].RbNausea == 1)
                        {
                            rbnauseayes.Checked = true;
                            rbnauseano.Checked = false;
                        }
                        if (patientdetails[0].RbNausea == 0)
                        {
                            rbnauseayes.Checked = false;
                            rbnauseano.Checked = true;
                        }
                        if (patientdetails[0].RbVomitting == 1)
                        {
                            rbvomittingyes.Checked = true;
                            rbvomittingno.Checked = false;
                        }
                        if (patientdetails[0].RbVomitting == 0)
                        {
                            rbvomittingyes.Checked = false;
                            rbvomittingno.Checked = true;
                        }
                        if (patientdetails[0].RbNpo == 1)
                        {
                            rbnpoyes.Checked = true;
                            rbnpono.Checked = false;
                        }
                        if (patientdetails[0].RbNpo == 0)
                        {
                            rbnpoyes.Checked = false;
                            rbnpono.Checked = true;
                        }
                        if (patientdetails[0].RbOral == 1)
                        {
                            rbnutritionyes.Checked = true;
                            rbnutritionno.Checked = false;
                        }
                        if (patientdetails[0].RbOral == 0)
                        {
                            rbnutritionyes.Checked = false;
                            rbnutritionno.Checked = true;
                        }
                        if (patientdetails[0].RbTubefeeding == 1)
                        {
                            rbnutritionyes.Checked = true;
                            rbnutritionno.Checked = false;
                        }
                        if (patientdetails[0].RbTubefeeding == 0)
                        {
                            rbnutritionyes.Checked = false;
                            rbnutritionno.Checked = true;
                        }
                        if (patientdetails[0].RbParenteral == 1)
                        {
                            rbnutritionyes.Checked = true;
                            rbnutritionno.Checked = false;
                        }
                        if (patientdetails[0].RbParenteral == 0)
                        {
                            rbnutritionyes.Checked = false;
                            rbnutritionno.Checked = true;
                        }
                        if (patientdetails[0].RbBowel == 1)
                        {
                            rbbowelyes.Checked = true;
                            rbbowelno.Checked = false;
                        }
                        if (patientdetails[0].RbBowel == 0)
                        {
                            rbbowelyes.Checked = false;
                            rbbowelno.Checked = true;
                        }
                        if (patientdetails[0].RbConstipation == 1)
                        {
                            rbconstipationyes.Checked = true;
                            rbconstipationno.Checked = false;
                        }
                        if (patientdetails[0].RbConstipation == 0)
                        {
                            rbconstipationyes.Checked = false;
                            rbconstipationno.Checked = true;
                        }
                        if (patientdetails[0].RbDiarrhoea == 1)
                        {
                            rbdiarrhoeayes.Checked = true;
                            rbdiarrhoeano.Checked = false;
                        }
                        if (patientdetails[0].RbDiarrhoea == 0)
                        {
                            rbdiarrhoeayes.Checked = false;
                            rbdiarrhoeano.Checked = true;
                        }
                        if (patientdetails[0].RbMalena == 1)
                        {
                            rbmalenayes.Checked = true;
                            rbmalenano.Checked = false;
                        }
                        if (patientdetails[0].RbMalena == 0)
                        {
                            rbmalenayes.Checked = false;
                            rbmalenano.Checked = true;
                        }
                        if (patientdetails[0].RbUrine == 1)
                        {
                            rbUrineyes.Checked = true;
                            rbUrineno.Checked = false;
                        }
                        if (patientdetails[0].RbUrine == 1)
                        {
                            rbUrineyes.Checked = false;
                            rbUrineno.Checked = true;
                        }
                        if (patientdetails[0].RbHematuria == 1)
                        {
                            rbHematuriayes.Checked = true;
                            rbHematuriano.Checked = false;
                        }
                        if (patientdetails[0].RbHematuria == 0)
                        {
                            rbHematuriayes.Checked = false;
                            rbHematuriano.Checked = true;
                        }
                        if (patientdetails[0].RbHealthy == 1)
                        {
                            rbhealthyyes.Checked = true;
                            rbhealthyno.Checked = false;
                        }
                        if (patientdetails[0].RbHealthy == 0)
                        {
                            rbhealthyyes.Checked = false;
                            rbhealthyno.Checked = true;
                        }
                        if (patientdetails[0].RbDressing == 1)
                        {
                            rbdressingyes.Checked = true;
                            rbdressingno.Checked = false;
                        }
                        if (patientdetails[0].RbDressing == 0)
                        {
                            rbdressingyes.Checked = false;
                            rbdressingno.Checked = true;
                        }
                    }
                    if (patientdetails[0].NurseShift == 0)
                    {
                        clearnurseshift();
                    }

                    btnsave.Attributes.Remove("disabled");

                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
            }
        }
        public List<DailyNursingAssessmentData> GetEditPatientDetails(Int64 ID, Int32 NurShiftID)
        {
            DailyNursingAssessmentData objpat = new DailyNursingAssessmentData();
            DailyNursingAssessmentBO objpatBO = new DailyNursingAssessmentBO();
            objpat.ID = ID;
            objpat.NurseShift = NurShiftID;
            return objpatBO.GetPtientDeatilbyID(objpat);
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

                List<DailyNursingAssessmentData> patientdetails = GetPatientData(page);
                if (patientdetails.Count > 0)
                {
                    if (patientdetails[0].DischargeStatus != 1)
                    {
                        btnsave.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        txtdischargestatus.Text = Convert.ToString(patientdetails[0].DischargeStatus);
                        btnsave.Attributes.Remove("disabled");
                    }
                    if (LogData.PrintEnable == 0)
                    {
                        btnprints.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btnprints.Attributes.Remove("disabled");
                    }
                    GvNursingAssessment.VirtualItemCount = patientdetails[0].MaximumRows;//total item is required for custom paging
                    GvNursingAssessment.PageIndex = page - 1;

                    GvNursingAssessment.DataSource = patientdetails;
                    GvNursingAssessment.DataBind();
                    GvNursingAssessment.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + patientdetails[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblmessage2.Visible = false;
                    divmsg2.Visible = false;
                }
                else
                {
                    btnprints.Attributes["disabled"] = "disabled";
                    divmsg3.Visible = false;
                    GvNursingAssessment.DataSource = null;
                    GvNursingAssessment.DataBind();
                    GvNursingAssessment.Visible = true;
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
        public List<DailyNursingAssessmentData> GetPatientData(int curIndex)
        {

            DailyNursingAssessmentData objpat = new DailyNursingAssessmentData();
            DailyNursingAssessmentBO objstdBO = new DailyNursingAssessmentBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtdateto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.IPNo = txtipnos.Text.ToString();
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            //objpat.userID = Convert.ToInt32(ddl_user.SelectedValue == "" ? "0" : ddl_user.SelectedValue);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            objpat.CurrentIndex = curIndex;
            return objstdBO.SearchPatientList(objpat);

        }


        protected void txtParticular_TextChanged(object sender, EventArgs e)
        {
            List<DailyNursingAssessmentData> NurseNotesDataList = Session["NurseNotesDataList"] == null ? new List<DailyNursingAssessmentData>() : (List<DailyNursingAssessmentData>)Session["NurseNotesDataList"];
            DailyNursingAssessmentData ObjService = new DailyNursingAssessmentData();

            var source = txtParticulars.Text.ToString();
            if (source.Contains(":"))
            {
                ObjService.ID = Convert.ToInt32(source.Substring(source.LastIndexOf(':') + 1));
            }
            ObjService.Particular = txtParticulars.Text.ToString();
            NurseNotesDataList.Add(ObjService);
            if (NurseNotesDataList.Count > 0)
            {
                GvDue.DataSource = NurseNotesDataList;
                GvDue.DataBind();
                GvDue.Visible = true;
                Session["NurseNotesDataList"] = NurseNotesDataList;
                txtParticulars.Text = "";
                txtParticulars.Focus();
            }
            else
            {
                GvDue.DataSource = null;
                GvDue.DataBind();
                GvDue.Visible = true;
            }
        }
        protected void btn_print_Click(object sender, EventArgs e)
        {
            DailyNursingAssessment objData = new DailyNursingAssessment();
            DailyNursingAssessmentBO objBO = new DailyNursingAssessmentBO();
            Int64 ID = Convert.ToInt64(txtid.Text == "" ? "0" : txtid.Text.Trim());
            string IPNo = txtipno.Text == "" ? "" : txtipno.Text.Trim();
            string url = "../MedNurse/Reports/ReportViewer.aspx?option=DailyNursingAssessment&ID=" + ID.ToString() + "&IPNo=" + IPNo.ToString();
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
        protected void GVDue_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //try
            //{
            //if (e.CommandName == "Edits")
            //{
            //    if (LogData.EditEnable == 0)
            //    {
            //        Messagealert_.ShowMessage(lblmessage2, "EditEnable", 0);
            //        divmsg2.Visible = true;
            //        divmsg2.Attributes["class"] = "FailAlert";
            //        return;
            //    }
            //    else
            //    {
            //        lblmessage.Visible = false;
            //    }
            //    int i = Convert.ToInt16(e.CommandArgument.ToString());
            //    GridViewRow gr = GvNursingAssessment.Rows[i];
            //    Label ID = (Label)gr.Cells[0].FindControl("lblUHID");
            //    Int64 PatID = Convert.ToInt64(ID.Text);
            //    //EditPatient(PatID);
            //    tabcontainerNursingAssesment.ActiveTabIndex = 0;
            //}
            //if (e.CommandName == "Deletes")
            //{

            //else
            //{
            //    Messagealert_.ShowMessage(lblmessage2, "system", 0);
            //    divmsg3.Attributes["class"] = "FailAlert";
            //    divmsg3.Visible = true;
            //}

            //}

            //}
            //catch (Exception ex) //Exception in agent layer itself
            //{
            //    PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
            //    LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
            //    Messagealert_.ShowMessage(lblresult, "system", 0);
            //    divmsg2.Attributes["class"] = "FailAlert";
            //    divmsg2.Visible = true;
            //}
        }
        protected void GvDue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvDue.PageIndex = e.NewPageIndex;
            //bindgrid();
        }
        protected void GvDue_RowDataBound(object sender, GridViewRowEventArgs e)
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
        protected void GvDue_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvDue.Rows[i];
                    List<OTRegnData> ItemList = Session["OTdetailList"] == null ? new List<OTRegnData>() : (List<OTRegnData>)Session["OTdetailList"];
                    ItemList.RemoveAt(i);
                    Session["OTdetailList"] = ItemList;
                    GvDue.DataSource = ItemList;
                    GvDue.DataBind();
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetLabServices(string prefixText, int count, string contextKey)
        {
            LabServiceMasterData Objpaic = new LabServiceMasterData();
            LabServiceMasterBO objInfoBO = new LabServiceMasterBO();
            List<LabServiceMasterData> getResult = new List<LabServiceMasterData>();
            Objpaic.TestName = prefixText;
            Objpaic.LabSubGroupID = Convert.ToInt32(contextKey == "" ? "0" : contextKey);
            getResult = objInfoBO.Getinvestigationlist(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].TestName.ToString());
            }
            return list;
        }


    }
}