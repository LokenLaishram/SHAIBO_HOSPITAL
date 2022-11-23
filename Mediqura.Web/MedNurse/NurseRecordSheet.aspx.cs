using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedicationBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedMedication;
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

namespace Mediqura.Web.MedNurse
{
    public partial class NurseRecordSheet : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();                  
            }
            txtwardbedno.Attributes["disabled"] = "disabled";
            txtage.Attributes["disabled"] = "disabled";
            txtgender.Attributes["disabled"] = "disabled";
            txtweight.Attributes["disabled"] = "disabled";
            txtdoa.Attributes["disabled"] = "disabled";
            txtIPNO.Attributes["disabled"] = "disabled";
            btnsave.Text = "Add";
        }
        private void bindddl()
        {
            int CurMonth = Convert.ToInt32(DateTime.Now.Month);
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlGCSEyeOpening, mstlookup.GetLookupsList(LookupName.GCSEyeOpening));
            Commonfunction.PopulateDdl(ddlGCSEVerbal, mstlookup.GetLookupsList(LookupName.GCSVerval));
            Commonfunction.PopulateDdl(ddlGCSMotorResponse, mstlookup.GetLookupsList(LookupName.GCSMortorResponse));
            Commonfunction.PopulateDdl(ddlrightpupilreaction, mstlookup.GetLookupsList(LookupName.RightPupilReaction));
            Commonfunction.PopulateDdl(ddlleftpupilreaction, mstlookup.GetLookupsList(LookupName.LeftPupilReaction));
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPPatientName(string prefixText, int count, string contextKey)
        {
            NureseRecordSheetData Objpaic = new NureseRecordSheetData();
            NurseRecordSheetBO objmedBO = new NurseRecordSheetBO();
            List<NureseRecordSheetData> getResult = new List<NureseRecordSheetData>();
            Objpaic.IPPatientName = prefixText;
            getResult = objmedBO.GetIPPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPPatientName.ToString());
            }
            return list;
        }
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            NureseRecordSheetData ObjMedi = new NureseRecordSheetData();
            NurseRecordSheetBO objmediBO = new NurseRecordSheetBO();
            List<NureseRecordSheetData> getResult = new List<NureseRecordSheetData>();
            string IPNO;
            var source = txtpatientNames.Text.ToString();
            if (source.Contains(":"))
            {
                IPNO = source.Substring(source.LastIndexOf(':') + 1);
                ObjMedi.IPNo = IPNO.Trim();
            }
            else
            {
                ObjMedi.IPNo = "";
            }

            getResult = objmediBO.GetPatientDetailsByIPNO(ObjMedi);

            if (getResult.Count > 0)
            {
                hdnuhid.Value = getResult[0].UHID.ToString();
                hdnipnumber.Value = getResult[0].IPNo.ToString();
                txtwardbedno.Text = getResult[0].WardBedNo.ToString();
                txtage.Text = getResult[0].Age.ToString();
                txtgender.Text = getResult[0].GenderName.ToString();
                txtdoa.Text = getResult[0].DOA.ToString("R");
                txtIPNO.Text = getResult[0].IPNo.ToString();
                bindgrid();
            }
            else
            {
                hdnuhid.Value = "";
                hdnipnumber.Value = "";
                txtwardbedno.Text = "";
                txtage.Text = "";
                txtgender.Text = "";
                txtdoa.Text = "";
                ddlGCSEyeOpening.SelectedIndex = 0;

            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                NureseRecordSheetData ObjNdata = new NureseRecordSheetData();
                NurseRecordSheetBO objOTBO = new NurseRecordSheetBO();
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

               

                if (txtEntryDate.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txttime.Focus();
                    return;
                }
                else if (txtEntryDate.Text != "")
                {
                    if (Commonfunction.isValidDate(txtEntryDate.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtEntryDate.Focus();
                        return;
                    }
                    if (Commonfunction.ChecklowerDate(txtEntryDate.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtEntryDate.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txttime.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Time cannot be empty.", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txttime.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                if (txttempe.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter temperature", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txttempe.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                if (txtpulse.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter pulse", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtpulse.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtbp.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter BP", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtbp.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtrrmin.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter R.R", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtrrmin.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtSpO2.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter SpO2", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtSpO2.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddlGCSEyeOpening.SelectedValue == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter Eye Opening(E)", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddlGCSEyeOpening.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddlGCSEVerbal.SelectedValue == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter Verbal Response (V)", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddlGCSEVerbal.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddlGCSMotorResponse.SelectedValue == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter Motor Response (M)", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddlGCSMotorResponse.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddlrightpupilreaction.SelectedValue == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select Right Pupil Reaction", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddlrightpupilreaction.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddlleftpupilreaction.SelectedValue == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select Left Pupil Reaction", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddlleftpupilreaction.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtinvasiveline.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter Other Invasive Line", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtinvasiveline.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                string IPNO;
                string PatName;
                var source = txtpatientNames.Text.ToString();
                if (source.Contains(":"))
                {
                    IPNO = source.Substring(source.LastIndexOf(':') + 1);
                    int indexStop = source.LastIndexOf('/');
                    PatName = source.Substring(0, indexStop);
                    ObjNdata.IPNo = IPNO.Trim();
                    ObjNdata.IPPatientName = PatName;
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "patientname", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtpatientNames.Focus();
                    return;
                }

               
                ObjNdata.UHID = Convert.ToInt32(hdnuhid.Value == "" ? "0" : hdnuhid.Value);
                ObjNdata.WardBedNo = txtwardbedno.Text == "" ? "" : txtwardbedno.Text;
                DateTime EntryDate = txtEntryDate.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtEntryDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                ObjNdata.EntryDate = EntryDate;
                ObjNdata.EntryTime = txttime.Text == "" ? "" : txttime.Text;
                ObjNdata.Temperature = txttempe.Text == "" ? "" : txttempe.Text;
                ObjNdata.Pulse = txtpulse.Text == "" ? "" : txtpulse.Text;
                ObjNdata.BP = txtbp.Text == "" ? "" : txtbp.Text;
                ObjNdata.RR = txtrrmin.Text == "" ? "" : txtrrmin.Text;
                ObjNdata.SpO2 = txtSpO2.Text == "" ? "" : txtSpO2.Text;
                ObjNdata.GCSEyeOpeningID = Convert.ToInt32(ddlGCSEyeOpening.SelectedValue == "" ? "0" : ddlGCSEyeOpening.SelectedValue);
                ObjNdata.GCSEVerbalID = Convert.ToInt32(ddlGCSEVerbal.SelectedValue == "" ? "0" : ddlGCSEVerbal.SelectedValue);
                ObjNdata.GCSMotorResponseID = Convert.ToInt32(ddlGCSMotorResponse.SelectedValue == "" ? "0" : ddlGCSMotorResponse.SelectedValue);
                ObjNdata.RightPupilValue = Convert.ToInt32(ddlrightpupilreaction.SelectedValue == "" ? "0" : ddlrightpupilreaction.SelectedValue);
                ObjNdata.LeftPupilValue = Convert.ToInt32(ddlleftpupilreaction.SelectedValue == "" ? "0" : ddlleftpupilreaction.SelectedValue);
                ObjNdata.InvasiveLine = txtinvasiveline.Text == "" ? "" : txtinvasiveline.Text;

                ObjNdata.EmployeeID = LogData.EmployeeID;
                ObjNdata.HospitalID = LogData.HospitalID;
                ObjNdata.FinancialYearID = LogData.FinancialYearID;
                ObjNdata.ActionType = Enumaction.Insert;

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

                    ObjNdata.ActionType = Enumaction.Update;
                    ObjNdata.ID = Convert.ToInt64(ViewState["ID"].ToString() == "" ? "0" : ViewState["ID"].ToString());
                }
                int results = objOTBO.UpdateNurseRecordSheet(ObjNdata);
                if (results > 0)
                {
                    if (results == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "save", 1);
                        divmsg1.Attributes["class"] = "SucessAlert";
                        divmsg1.Visible = true;
                        btnsave.Attributes["disabled"] = "disabled";
                        bindgrid();
                    }
                    if (results == 2)
                    {

                        Messagealert_.ShowMessage(lblmessage, "update", 1);
                        divmsg1.Attributes["class"] = "SucessAlert";
                        divmsg1.Visible = true;
                        btnsave.Attributes["disabled"] = "disabled";
                        bindgrid();
                    }
                    if (results == 4)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Patient is not found in IP admission", 0);
                        lblmessage.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;

                    }
                    if (results == 5)
                    {
                        Messagealert_.ShowMessage(lblmessage, "This Drug have already enter", 0);
                        lblmessage.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;

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
                bindgrid();
            }
        }
        protected void btnsearchs_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void bindgrid()
        {
            try
            {
                if (LogData.SearchEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
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
                    var source1 = txtpatientNames.Text.ToString();
                    if (source1.Contains(":"))
                    {
                        lblmessage.Visible = false;
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "patientname", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtpatientNames.Focus();
                        return;
                    }
                }

                List<NureseRecordSheetData> obj = GetNurseRecordList(0);
                if (obj.Count > 0)
                {
                    Messagealert_.ShowMessage(lblresult, "Total:" + obj[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg1.Attributes["class"] = "SucessAlert";
                    gvNurseRecorddetails.DataSource = obj;
                    gvNurseRecorddetails.DataBind();
                    gvNurseRecorddetails.Visible = true;
                }
                else
                {
                    gvNurseRecorddetails.DataSource = null;
                    gvNurseRecorddetails.DataBind();
                    gvNurseRecorddetails.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }
        private List<NureseRecordSheetData> GetNurseRecordList(int p)
        {
            NureseRecordSheetData objmedi = new NureseRecordSheetData();
            NurseRecordSheetBO objBO = new NurseRecordSheetBO();
            string IPNO;
            var source = txtpatientNames.Text.ToString();
            if (source.Contains(":"))
            {
                IPNO = source.Substring(source.LastIndexOf(':') + 1);
                objmedi.IPNo = IPNO.Trim();
            }            

            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtfrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtfrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? GlobalConstant.MaxdateAddOneYear : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objmedi.DateFrom = from;
            objmedi.DateTo = To;
            return objBO.GetNurseRecordList(objmedi);

        }
        protected void gvNurseRecorddetails_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    NureseRecordSheetData objData = new NureseRecordSheetData();
                    NurseRecordSheetBO objBO = new NurseRecordSheetBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gvlist = gvNurseRecorddetails.Rows[i];
                    Label ID = (Label)gvlist.Cells[0].FindControl("lblID");
                    objData.ID = Convert.ToInt32(ID.Text);
                    objData.ActionType = Enumaction.Select;

                    List<NureseRecordSheetData> GetResult = objBO.GetNurseRecordEntryByID(objData);
                    if (GetResult.Count > 0)
                    {
                        bindddl(); 
                        txtpatientNames.Text = GetResult[0].IPPatientName;
                        txtwardbedno.Text = GetResult[0].WardBedNo;
                        txtage.Text = GetResult[0].WardBedNo;
                        txtgender.Text = GetResult[0].WardBedNo;
                        txtdoa.Text = GetResult[0].DOA.ToString("dd/MM/yyyy");
                        txtEntryDate.Text = GetResult[0].EntryDate.ToString("dd/MM/yyyy");
                        txttime.Text = GetResult[0].EntryTime;
                        txttempe.Text = GetResult[0].Temperature;
                        txtpulse.Text = GetResult[0].Pulse;
                        txtbp.Text = GetResult[0].BP;
                        txtrrmin.Text = GetResult[0].RR;
                        txtSpO2.Text = GetResult[0].SpO2;
                        ddlGCSEyeOpening.SelectedValue = GetResult[0].GCSEyeOpeningID.ToString();
                        ddlGCSEVerbal.SelectedValue = GetResult[0].GCSEVerbalID.ToString();
                        ddlGCSMotorResponse.SelectedValue = GetResult[0].GCSMotorResponseID.ToString();
                        ddlrightpupilreaction.SelectedValue = GetResult[0].RightPupilID.ToString();
                        ddlleftpupilreaction.SelectedValue = GetResult[0].LeftPupilID.ToString();
                        txtinvasiveline.Text = GetResult[0].InvasiveLine;
                        ViewState["ID"] = GetResult[0].ID;
                        btnsave.Attributes.Remove("disabled");
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
                    NureseRecordSheetData objData = new NureseRecordSheetData();
                    NurseRecordSheetBO objDrgBO = new NurseRecordSheetBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvNurseRecorddetails.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    objData.ID = Convert.ToInt32(ID.Text);
                    objData.EmployeeID = LogData.EmployeeID;
                    objData.ActionType = Enumaction.Delete;
                    //TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    //txtremarks.Enabled = true;
                    //if (txtremarks.Text == "")
                    //{
                    //    Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                    //    divmsg1.Visible = true;
                    //    divmsg1.Attributes["class"] = "FailAlert";
                    //    txtremarks.Focus();
                    //    return;
                    //}
                    //else
                    //{
                    //    objDrgData.Remarks = txtremarks.Text;
                    //}

                    int Result = objDrgBO.DeleteNurseRecordEntryByID(objData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SucessAlert";
                        bindgrid();
                    }
                    else if (Result == 5)
                    {
                        Messagealert_.ShowMessage(lblmessage, "This drug cannot be delete because medication have already started.", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";

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
        protected void btnclear_Click(object sender, System.EventArgs e)
        {

            gvNurseRecorddetails.DataSource = null;
            gvNurseRecorddetails.DataBind();
            gvNurseRecorddetails.Visible = false;
            txtpatientNames.Text = "";
            txtwardbedno.Text = "";
            txtage.Text = "";
            txtgender.Text = "";
            txtweight.Text = "";
            txtdoa.Text = "";
            txtIPNO.Text = "";
            txtEntryDate.Text = "";
            txttime.Text = "";
            txttempe.Text = "";
            txtpulse.Text = "";
            txtbp.Text = "";
            txtrrmin.Text = "";
            txtSpO2.Text = "";
            ddlGCSEyeOpening.SelectedIndex = 0;
            ddlGCSEVerbal.SelectedIndex = 0;
            ddlGCSMotorResponse.SelectedIndex = 0;
            ddlrightpupilreaction.SelectedIndex = 0;
            ddlleftpupilreaction.SelectedIndex = 0;
            txtinvasiveline.Text = "";
            txtfrom.Text = "";
            txtto.Text = "";
            divmsg1.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            divmsg1.Visible = false;
            ViewState["ID"] = null;
            btnsave.Text = "Add";
          

        }
    }      
}