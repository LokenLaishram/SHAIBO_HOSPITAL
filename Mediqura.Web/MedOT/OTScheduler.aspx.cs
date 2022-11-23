using Mediqura.BOL.CommonBO;
using Mediqura.BOL.OTBO;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.PatientData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedOT
{
    public partial class OTScheduler : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
            }
            txtwardbedno.Attributes["disabled"] = "disabled";          
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_ottheater, mstlookup.GetLookupsList(LookupName.OTtheater));
            //Commonfunction.PopulateDdl(ddlpac, mstlookup.GetLookupsList(LookupName.PAC));
            Commonfunction.PopulateDdl(ddlanaesthetist, mstlookup.GetLookupsList(LookupName.AnaesthesiaEmpList));
            Commonfunction.PopulateDdl(ddlOTStatuss, mstlookup.GetLookupsList(LookupName.OTStatusID));
            Commonfunction.PopulateDdlHour(ddlstarthour, mstlookup.GetLookupsList(LookupName.Hour));
            Commonfunction.PopulateDdlMinute(ddlstartminute, mstlookup.GetLookupsList(LookupName.Minute));
            Commonfunction.PopulateDdlHour(ddlendhour, mstlookup.GetLookupsList(LookupName.Hour));
            Commonfunction.PopulateDdlMinute(ddlendminute, mstlookup.GetLookupsList(LookupName.Minute));
            //------Tab2------//
            Commonfunction.PopulateDdl(ddlottheater, mstlookup.GetLookupsList(LookupName.OTtheater));
            Commonfunction.PopulateDdl(ddl_consultant, mstlookup.GetLookupsList(LookupName.Doctor));
            Commonfunction.PopulateDdl(ddlOTstatus, mstlookup.GetLookupsList(LookupName.OTStatusID));
            Session["OTdetailList"] = null;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPatientName(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetPatientNameforOTScheduling(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPatientNameList(string prefixText, int count, string contextKey)
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetDoctorName(string prefixText, int count, string contextKey)
        {
            OTSchedulingData Objpaic = new OTSchedulingData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<OTSchedulingData> getResult = new List<OTSchedulingData>();
            Objpaic.Surgeon = prefixText;
            getResult = objInfoBO.GetDoctorNameForOTScheduling(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].Surgeon.ToString());
            }
            return list;
        }
        private List<OTSchedulingData> GetPatientList(int p)
        {
            OTSchedulingData objpat = new OTSchedulingData();
            OTSchedulingBO objBO = new OTSchedulingBO();
            string ID;
            var source = txtpatientNames.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                objpat.UHID = Convert.ToInt64(ID == "" ? "0" : ID);
            }

            objpat.PACID = Convert.ToInt32(ddlpac.SelectedValue == "" ? "0" : ddlpac.SelectedValue);
            objpat.TheatreID = Convert.ToInt32(ddl_ottheater.SelectedValue == "" ? "0" : ddl_ottheater.SelectedValue);
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime OTDate = txtotdate.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtotdate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.Date = OTDate;
            objpat.OTStatusID = Convert.ToInt32(ddlOTStatuss.SelectedValue == "" ? "0" : ddlOTStatuss.SelectedValue);
            return objBO.GetPatientList(objpat);
        }
        protected void txtUHID_TextChanged(object sender, EventArgs e)
        {
            OTSchedulingData ObjOT = new OTSchedulingData();
            OTSchedulingBO objOTBO = new OTSchedulingBO();
            List<OTSchedulingData> getResult = new List<OTSchedulingData>();
            bool isnumeric = txtpatientNames.Text.All(char.IsDigit);
            if (isnumeric == false)
            {
                if (txtpatientNames.Text.Contains(":"))
                {
                    bool isUHIDnumeric = txtpatientNames.Text.Substring(txtpatientNames.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                    ObjOT.UHID = isUHIDnumeric ? Convert.ToInt64(txtpatientNames.Text.Contains(":") ? txtpatientNames.Text.Substring(txtpatientNames.Text.LastIndexOf(':') + 1) : "0") : 0;
                }
                else
                {
                    
                }
            }
            else
            {
                ObjOT.UHID = Convert.ToInt64(txtpatientNames.Text == "" ? "0" : txtpatientNames.Text);               
            }
            getResult = objOTBO.GetPatientDetailsByUHID(ObjOT);

            if (getResult.Count > 0)
            {
                hdnuhid.Value = getResult[0].UHID.ToString();
                hdnipnumber.Value = getResult[0].IPNo.ToString();
                txtwardbedno.Text = getResult[0].WardBedName.ToString();
            }
            else
            {               
                hdnuhid.Value = "";
                hdnipnumber.Value = "";
                txtwardbedno.Text = "";
               
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                OTSchedulingData ObjOT = new OTSchedulingData();
                OTSchedulingBO objOTBO = new OTSchedulingBO();
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

                if (txtpatientNames.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "patientname", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtpatientNames.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_ottheater.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select operation theater", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_ottheater.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                if (txtotdate.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Careof", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtotdate.Focus();
                    return;
                }
                else if (txtotdate.Text != "")
                {
                    if (Commonfunction.isValidDate(txtotdate.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtotdate.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }

                if (ddlstarthour.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select start hour", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddlstarthour.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                if (ddlstartminute.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select start minute", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddlstartminute.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
               
                /// --- END TIME ----///
                
                if (ddlendhour.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select end hour", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddlendhour.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                if (ddlendminute.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select end minute", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddlendminute.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                //---------START TIME CANNOT GREATER THAN END TIME
                if (ddlstartmeridiem.SelectedIndex == ddlendmeridiem.SelectedIndex)
                {
                    if (ddlstarthour.SelectedIndex > ddlendhour.SelectedIndex)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please check start time and end time", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        ddlstarthour.Focus();
                        return;
                    }
                    else
                    {
                        if (ddlstarthour.SelectedIndex == ddlendhour.SelectedIndex)
                        {
                            if (ddlstartminute.SelectedIndex > ddlendminute.SelectedIndex)
                            {
                                Messagealert_.ShowMessage(lblmessage, "Please check start minute and end minute", 0);
                                divmsg1.Visible = true;
                                divmsg1.Attributes["class"] = "FailAlert";
                                ddlstartminute.Focus();
                                return;
                            }
                        }
                    }
                }

                /////
                if (txtsurgeon.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select Surgeon", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtsurgeon.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddlOTStatuss.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select OT Status.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    ddlOTStatuss.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                if (txtoperationname.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter operation name.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtoperationname.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                string ID;
                string name;
                var source = txtpatientNames.Text.ToString();
                if (source.Contains(":"))
                {
                    ID = source.Substring(source.LastIndexOf(':') + 1);
                    int indexStop = source.LastIndexOf('/');
                    name = source.Substring(0, indexStop);
                    ObjOT.UHID = Convert.ToInt64(ID);
                    ObjOT.PatientName = name;
                }
                else
                {
                    ObjOT.PatientName = txtpatientNames.Text;
                }
                string ID1;
                string DrName;
                var source1 = txtsurgeon.Text.ToString();
                if (source1.Contains(":"))
                {
                    ID1 = source1.Substring(source1.LastIndexOf(':') + 1);
                    int indexStop = source1.LastIndexOf('/');
                    DrName = source1.Substring(0, indexStop);

                    ObjOT.DoctorID = Convert.ToInt64(ID1);
                    ObjOT.Surgeon = DrName;
                }
                else
                {                  
                    ObjOT.Surgeon = txtsurgeon.Text;
                }
                ObjOT.WardBedName = txtwardbedno.Text == "" ? "" : txtwardbedno.Text;
                ObjOT.TheatreID = Convert.ToInt32(ddl_ottheater.SelectedValue == "" ? "0" : ddl_ottheater.SelectedValue);
                DateTime OTDate = txtotdate.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtotdate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                ObjOT.Date = OTDate;
               
                ObjOT.StartHour = Convert.ToInt32(ddlstarthour.SelectedValue == "" ? "0" : ddlstarthour.SelectedValue);
                ObjOT.StartMinute = Convert.ToInt32(ddlstartminute.SelectedValue == "" ? "0" : ddlstartminute.SelectedValue);
                ObjOT.StartMeridiem = Convert.ToInt32(ddlstartmeridiem.SelectedValue == "" ? "0" : ddlstartmeridiem.SelectedValue);
                
                ObjOT.EndHour = Convert.ToInt32(ddlendhour.SelectedValue == "" ? "0" : ddlendhour.SelectedValue);
                ObjOT.EndMinute = Convert.ToInt32(ddlendminute.SelectedValue == "" ? "0" : ddlendminute.SelectedValue);
                ObjOT.EndMeridiem = Convert.ToInt32(ddlendmeridiem.SelectedValue == "" ? "0" : ddlendmeridiem.SelectedValue);
               
                ObjOT.AnaesthetistID = Convert.ToInt32(ddlanaesthetist.SelectedValue == "" ? "0" : ddlanaesthetist.SelectedValue);
                ObjOT.PACID = Convert.ToInt32(ddlpac.SelectedValue == "" ? "0" : ddlpac.SelectedValue);
                ObjOT.OTStatusID = Convert.ToInt32(ddlOTStatuss.SelectedValue == "" ? "0" : ddlOTStatuss.SelectedValue);
                ObjOT.OperationName = txtoperationname.Text == "" ? "" : txtoperationname.Text.Trim();
                ObjOT.Remark = txtremark.Text == "" ? "" : txtremark.Text;

                ObjOT.EmployeeID = LogData.EmployeeID;
                ObjOT.HospitalID = LogData.HospitalID;
                ObjOT.FinancialYearID = LogData.FinancialYearID;
                ObjOT.ActionType = Enumaction.Insert;

                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txtpatientNames.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }

                    ObjOT.ActionType = Enumaction.Update;
                    ObjOT.ID = Convert.ToInt64(ViewState["ID"].ToString() == "" ? "0" : ViewState["ID"].ToString());
                }
                int results = objOTBO.UpdateOTScheduler(ObjOT);
                if (results > 0)
                {
                    if (results == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "save", 1);
                        divmsg1.Attributes["class"] = "SucessAlert";
                        divmsg1.Visible = true;
                        btnsave.Attributes["disabled"] = "disabled";
                        bindOtToday();
                    }
                    if (results == 2)
                    {

                        Messagealert_.ShowMessage(lblmessage, "update", 1);
                        divmsg1.Attributes["class"] = "SucessAlert";
                        divmsg1.Visible = true;
                        btnsave.Attributes["disabled"] = "disabled";
                        bindOtToday();
                    }
                    if (results == 4)
                    {
                        Messagealert_.ShowMessage(lblmessage, "bookedOTSchedule", 0);
                        lblmessage.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;                      
                        bindOtToday();
                    }
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                    lblmessage.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
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
        protected void txtotdate_TextChanged(object sender, EventArgs e)
        {           
            bindOtToday();
        }
        protected void bindOtToday()
        {
            try
            {

                List<OTSchedulingData> obj = GetOTTodayList(0);
                if (obj.Count > 0)
                {
                    Messagealert_.ShowMessage(lblresult1, "Total:" + obj[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg.Attributes["class"] = "SucessAlert";
                    gvtodayotdetails.DataSource = obj;
                    gvtodayotdetails.DataBind();
                    gvtodayotdetails.Visible = true;
                    divmsgs3.Visible = true;
                }
                else
                {
                    gvtodayotdetails.DataSource = null;
                    gvtodayotdetails.DataBind();
                    gvtodayotdetails.Visible = true;
                    divmsgs3.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }
        private List<OTSchedulingData> GetOTTodayList(int p)
        {
            OTSchedulingData objpat = new OTSchedulingData();
            OTSchedulingBO objBO = new OTSchedulingBO();           
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtotdate.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtotdate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.DateFrom = from;
            objpat.TheatreID = Convert.ToInt32(ddl_ottheater.SelectedValue == "" ? "0" : ddl_ottheater.SelectedValue);
            return objBO.GetOTTodayList(objpat);

        }
        protected void Gvtodayotdetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label IsSubHeading1 = (Label)e.Row.FindControl("lblSubHeading1");
                Label lblotdate1 = (Label)e.Row.FindControl("lblotdate1");
                Label lblstarttime1 = (Label)e.Row.FindControl("lblstarttime1");
                Label lblEndtime1 = (Label)e.Row.FindControl("lblEndtime1");
                Label lblpatientname1 = (Label)e.Row.FindControl("lblpatientname1");
                Label lblWardbedno1 = (Label)e.Row.FindControl("lblWardbedno1");
                Label lbloperationName1 = (Label)e.Row.FindControl("lbloperationName1");
                Label lbldoctorname1 = (Label)e.Row.FindControl("lbldoctorname1");
                Label lbltheatrename1 = (Label)e.Row.FindControl("lbltheatrename1");
                Label lblpac1 = (Label)e.Row.FindControl("lblpac1");
                Label lblanaesthtist1 = (Label)e.Row.FindControl("lblanaesthtist1");
                Label lblotstatus1 = (Label)e.Row.FindControl("lblotstatus1");
                Label lblremarks1 = (Label)e.Row.FindControl("lblremarks1");

                if (IsSubHeading1.Text == "1")
                {
                    e.Row.BackColor = Color.FromName("#33aa99");
                    lblpatientname1.ForeColor = System.Drawing.Color.White;                  
                }

            }

        }
        protected void btnresets_Click(object sender, System.EventArgs e)
        {
            lblmessage.Visible = false;
            divmsg1.Visible = false;
            txtpatientNames.Text = "";
            txtwardbedno.Text = "";
            ddl_ottheater.SelectedIndex = 0;
            txtotdate.Text = "";
            ddlstarthour.SelectedIndex = 0;
            ddlstartminute.SelectedIndex = 0;
            ddlstartmeridiem.SelectedIndex = 0;
            ddlendhour.SelectedIndex = 0;
            ddlendminute.SelectedIndex = 0;
            ddlendmeridiem.SelectedIndex = 0;
          
            txtsurgeon.Text = "";
            ddlpac.SelectedIndex = 0;
            ddlanaesthetist.SelectedIndex = 0;
            ddlOTStatuss.SelectedIndex = 0;
            txtoperationname.Text = "";
            txtremark.Text = "";
            ViewState["ID"] = null;
            lblresult1.Text = "";
            gvtodayotdetails.DataSource = null;
            gvtodayotdetails.DataBind();
            gvtodayotdetails.Visible = true;
            btnsave.Attributes.Remove("disabled");
        }
        //------TAB 2 ------//     
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPatientNames(string prefixText, int count, string contextKey)
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
        protected void btnsearchs_Click(object sender, EventArgs e)
        {
            bindOtgrid();
        }
        protected void bindOtgrid()
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
                List<OTSchedulingData> obj = GetOTPatientList(0);
                if (obj.Count > 0)
                {
                    Messagealert_.ShowMessage(lblresult, "Total:" + obj[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg.Attributes["class"] = "SucessAlert";
                    GvSchedulelist.DataSource = obj;
                    GvSchedulelist.DataBind();
                    GvSchedulelist.Visible = true;
                }
                else
                {
                    GvSchedulelist.DataSource = null;
                    GvSchedulelist.DataBind();
                    GvSchedulelist.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }
        private List<OTSchedulingData> GetOTPatientList(int p)
        {
            OTSchedulingData objpat = new OTSchedulingData();
            OTSchedulingBO objBO = new OTSchedulingBO();
            string ID;
            var source = txtpatient.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                objpat.UHID = Convert.ToInt64(ID == "" ? "0" : ID);
            }

            objpat.DoctorID = Convert.ToInt32(ddl_consultant.SelectedValue == "" ? "0" : ddl_consultant.SelectedValue);
            objpat.TheatreID = Convert.ToInt32(ddlottheater.SelectedValue == "" ? "0" : ddlottheater.SelectedValue);
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtotfrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtotfrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtotto.Text.Trim() == "" ? GlobalConstant.MaxdateAddOneYear : DateTime.Parse(txtotto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            objpat.OTStatusID = Convert.ToInt32(ddlOTstatus.SelectedValue == "" ? "0" : ddlOTstatus.SelectedValue);
            return objBO.GetOTPatientList(objpat);

        }
        protected void btnclear_Click(object sender, System.EventArgs e)
        {

            GvSchedulelist.DataSource = null;
            GvSchedulelist.DataBind();
            GvSchedulelist.Visible = false;
            txtpatient.Text = "";
            ddl_consultant.SelectedIndex = 0;
            ddlottheater.SelectedIndex = 0;
            txtotfrom.Text = "";
            txtotto.Text = "";
            divmsg2.Visible = false;
            lblmessage2.Visible = false;
            lblresult.Visible = false;
            divmsg.Visible = false;
            ViewState["ID"] = null;

        }
        protected void GvSchedulelist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label IsSubHeading = (Label)e.Row.FindControl("lblSubHeading");
                Label lblotdate = (Label)e.Row.FindControl("lblotdate");
                Label lblstarttime = (Label)e.Row.FindControl("lblstarttime");
                Label lblEndtime = (Label)e.Row.FindControl("lblEndtime");
                Label lblpatientname = (Label)e.Row.FindControl("lblpatientname");
                Label lblWardbedno = (Label)e.Row.FindControl("lblWardbedno");
                Label lbloperationName = (Label)e.Row.FindControl("lbloperationName");
                Label lbldoctorname = (Label)e.Row.FindControl("lbldoctorname");
                Label lbltheatrename = (Label)e.Row.FindControl("lbltheatrename");
                Label lblanaesthtist = (Label)e.Row.FindControl("lblanaesthtist");
                Label lblotstatus = (Label)e.Row.FindControl("lblotstatus");
                TextBox txtremarks = (TextBox)e.Row.FindControl("txtremarks");
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
              
                if (IsSubHeading.Text == "1")
                {
                    e.Row.BackColor = Color.FromName("#33aa99");
                    lblpatientname.ForeColor = System.Drawing.Color.White;
                    txtremarks.Visible = false;
                    lnkEdit.Visible = false;
                    lnkDelete.Visible = false;
                }

            }

        }
        protected void GvSchedulelist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edits")
                {
                    if (LogData.EditEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "EditEnable", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvSchedulelist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    Int64 OTID = Convert.ToInt64(ID.Text);
                    EditOTSchedule(OTID);
                    tabOTschedule.ActiveTabIndex = 0;
                }
                if (e.CommandName == "Deletes")
                {
                    OTSchedulingData objschedule = new OTSchedulingData();
                    OTSchedulingBO objscheduleBO = new OTSchedulingBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvSchedulelist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    Label OTNo = (Label)gr.Cells[0].FindControl("lblOTNo");
                    TextBox Remarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    if (Remarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage2, "Remarks", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        Remarks.Focus();
                        return;
                    }
                    else
                    {
                        objschedule.Remark = Remarks.Text;
                    }
                    objschedule.ID = Convert.ToInt64(ID.Text);
                    objschedule.OTNo = OTNo.Text.Trim();
                    objschedule.EmployeeID = LogData.EmployeeID;
                    int Result = objscheduleBO.DeleteOTScheduleByID(objschedule);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        lblmessage2.Attributes["class"] = "SucessAlert";
                        divmsg2.Visible = true;
                        bindOtgrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage2, "system", 0);
                        lblmessage2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                lblmessage2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        protected void EditOTSchedule(Int64 OTID)
        {
            try
            {
                List<OTSchedulingData> OTdetails = GetEditOTScheduleDetails(OTID);
                if (OTdetails.Count > 0)
                {
                    ViewState["ID"] = OTdetails[0].ID.ToString();
                    txtpatientNames.Text = OTdetails[0].PatientName.ToString();
                    txtwardbedno.Text = OTdetails[0].WardBedName.ToString();
                    ddl_ottheater.SelectedValue = OTdetails[0].TheatreID.ToString();
                    txtotdate.Text = OTdetails[0].Date.ToString("dd/MM/yyyy");
                    ddlstarthour.SelectedValue = OTdetails[0].StartHour.ToString();
                    ddlstartminute.SelectedValue = OTdetails[0].StartMinute.ToString();
                    ddlstartmeridiem.SelectedValue = OTdetails[0].StartMeridiem.ToString();
                    ddlendhour.SelectedValue = OTdetails[0].EndHour.ToString();
                    ddlendminute.SelectedValue = OTdetails[0].EndMinute.ToString();
                    ddlendmeridiem.SelectedValue = OTdetails[0].EndMeridiem.ToString();
                    txtsurgeon.Text = OTdetails[0].DoctorName.ToString();
                    ddlanaesthetist.SelectedValue = OTdetails[0].AnaesthetistID.ToString();
                    ddlpac.SelectedValue = OTdetails[0].PACID.ToString();
                    ddlOTStatuss.SelectedValue = OTdetails[0].OTStatusID.ToString();
                    txtoperationname.Text = OTdetails[0].OperationName.ToString();
                    txtremark.Text = OTdetails[0].Remark.ToString();                    
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
        public List<OTSchedulingData> GetEditOTScheduleDetails(Int64 ID)
        {
            OTSchedulingData objotedit = new OTSchedulingData();
            OTSchedulingBO objoteditBO = new OTSchedulingBO();
            objotedit.ID = ID;
            return objoteditBO.GetOTScheduleByID(objotedit);
        }
    }
}