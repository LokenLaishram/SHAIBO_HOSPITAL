using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedicationBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
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
    public partial class MedicationChart : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                txtmedicationdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

            }
            txtwardbedno.Attributes["disabled"] = "disabled";
            txtage.Attributes["disabled"] = "disabled";
            txtgender.Attributes["disabled"] = "disabled";
            txtweight.Attributes["disabled"] = "disabled";
            txtdoa.Attributes["disabled"] = "disabled";
            btnsave.Text = "Add";
            //-----TAB2--------//
            txtuhid.Attributes["disabled"] = "disabled";
            txtipno.Attributes["disabled"] = "disabled";
            txtIPpatient2.Attributes["disabled"] = "disabled";
            txtwardbedNo2.Attributes["disabled"] = "disabled";
            txtdrug.Attributes["disabled"] = "disabled";
            txtstartdate2.Attributes["disabled"] = "disabled";
            txtfrequency2.Attributes["disabled"] = "disabled";
            txtroute2.Attributes["disabled"] = "disabled";
            //btn_save.Text = "Add";
            //btn_save.Attributes["disabled"] = "disabled";

        }
        private void bindddl()
        {
            int CurMonth = Convert.ToInt32(DateTime.Now.Month);
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlroute, mstlookup.GetLookupsList(LookupName.Route));
            Commonfunction.PopulateDdl(ddlmonth, mstlookup.GetLookupsList(LookupName.month));
            ddlmonth.SelectedIndex = CurMonth;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPPatientName(string prefixText, int count, string contextKey)
        {
            MedicationChartData Objpaic = new MedicationChartData();
            MedicationChartBO objmedBO = new MedicationChartBO();
            List<MedicationChartData> getResult = new List<MedicationChartData>();
            Objpaic.IPPatientName = prefixText;
            getResult = objmedBO.GetIPPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPPatientName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetDrugName(string prefixText, int count, string contextKey)
        {
            MedicationChartData Objdrg = new MedicationChartData();
            MedicationChartBO objmedBO = new MedicationChartBO();
            List<MedicationChartData> getResult = new List<MedicationChartData>();
            Objdrg.DrugName = prefixText;
            getResult = objmedBO.GetDrugName(Objdrg);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].DrugName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetDoctorName(string prefixText, int count, string contextKey)
        {
            MedicationChartData Objdr = new MedicationChartData();
            MedicationChartBO objmedBO = new MedicationChartBO();
            List<MedicationChartData> getResult = new List<MedicationChartData>();
            Objdr.DoctorName = prefixText;
            getResult = objmedBO.GetDoctorName(Objdr);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].DoctorName.ToString());
            }
            return list;
        }
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            MedicationChartData ObjMedi = new MedicationChartData();
            MedicationChartBO objmediBO = new MedicationChartBO();
            List<MedicationChartData> getResult = new List<MedicationChartData>();
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

            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                MedicationChartData ObjDrug = new MedicationChartData();
                MedicationChartBO objOTBO = new MedicationChartBO();
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

                if (txtdrugname.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Drug name cannot be blank!", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtdrugname.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                if (txtstartdate.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Careof", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtstartdate.Focus();
                    return;
                }
                else if (txtstartdate.Text != "")
                {
                    if (Commonfunction.isValidDate(txtstartdate.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtstartdate.Focus();
                        return;
                    }
                    if (Commonfunction.ChecklowerDate(txtstartdate.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txtstartdate.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }

                if (txtfrequency.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter Frequency", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtfrequency.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                if (ddlroute.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select route", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddlroute.Focus();
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
                    ObjDrug.IPNo = IPNO.Trim();
                    ObjDrug.IPPatientName = PatName;
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "patientname", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtpatientNames.Focus();
                    return;
                }

                string DrgID;
                string DrgName;
                var source1 = txtdrugname.Text.ToString();
                if (source1.Contains(":"))
                {
                    DrgID = source1.Substring(source1.LastIndexOf(':') + 1);
                    int indexStop = source1.LastIndexOf('|');
                    DrgName = source1.Substring(0, indexStop);

                    ObjDrug.DrugID = Convert.ToInt32(DrgID);
                    ObjDrug.DrugName = DrgName;
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Drug name cannot be blank!", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtdrugname.Focus();
                    return;
                }
                ObjDrug.UHID = Convert.ToInt32(hdnuhid.Value == "" ? "0" : hdnuhid.Value);
                ObjDrug.WardBedNo = txtwardbedno.Text == "" ? "" : txtwardbedno.Text;
                DateTime StartDate = txtstartdate.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtstartdate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                ObjDrug.StartDate = StartDate;
                ObjDrug.Frequency = txtfrequency.Text == "" ? "" : txtfrequency.Text;
                ObjDrug.RouteID = Convert.ToInt32(ddlroute.SelectedValue == "" ? "0" : ddlroute.SelectedValue);
                string DoctorID;
                string DoctorName;
                var Dr = txtDoctorName.Text.ToString();
                if (Dr.Contains(":"))
                {
                    DoctorID = Dr.Substring(Dr.LastIndexOf(':') + 1);
                    int indexStop = Dr.LastIndexOf('/');
                    DoctorName = Dr.Substring(0, indexStop);

                    ObjDrug.DoctorID = Convert.ToInt64(DoctorID);
                    ObjDrug.DoctorName = DoctorName;
                }
                else
                {
                    ObjDrug.DoctorID = 0;
                    ObjDrug.DoctorName = "";
                }
                ObjDrug.EmployeeID = LogData.EmployeeID;
                ObjDrug.HospitalID = LogData.HospitalID;
                ObjDrug.FinancialYearID = LogData.FinancialYearID;
                ObjDrug.ActionType = Enumaction.Insert;

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

                    ObjDrug.ActionType = Enumaction.Update;
                    ObjDrug.ID = Convert.ToInt64(ViewState["ID"].ToString() == "" ? "0" : ViewState["ID"].ToString());
                }
                int results = objOTBO.UpdateMedicationChart(ObjDrug);
                if (results > 0)
                {
                    if (results == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "save", 1);
                        divmsg1.Attributes["class"] = "SucessAlert";
                        divmsg1.Visible = true;
                        btnsave.Attributes["disabled"] = "disabled";
                        btnsave.Text = "Add";
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

                List<MedicationChartData> obj = GetMedicationChartList(0);
                if (obj.Count > 0)
                {
                    Messagealert_.ShowMessage(lblresult, "Total:" + obj[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg1.Attributes["class"] = "SucessAlert";
                    gvmedichartdetails.DataSource = obj;
                    gvmedichartdetails.DataBind();
                    gvmedichartdetails.Visible = true;
                }
                else
                {
                    gvmedichartdetails.DataSource = null;
                    gvmedichartdetails.DataBind();
                    gvmedichartdetails.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }
        private List<MedicationChartData> GetMedicationChartList(int p)
        {
            MedicationChartData objmedi = new MedicationChartData();
            MedicationChartBO objBO = new MedicationChartBO();
            string IPNO;
            var source = txtpatientNames.Text.ToString();
            if (source.Contains(":"))
            {
                IPNO = source.Substring(source.LastIndexOf(':') + 1);
                objmedi.IPNo = IPNO.Trim();
            }
            string DrgID;
            string DrgName;
            var source1 = txtdrugname.Text.ToString();
            if (source1.Contains(":"))
            {
                DrgID = source1.Substring(source1.LastIndexOf(':') + 1);
                int indexStop = source1.LastIndexOf('|');
                DrgName = source1.Substring(0, indexStop);

                objmedi.DrugID = Convert.ToInt32(DrgID);
                objmedi.DrugName = DrgName;
            }
            else
            {
                objmedi.DrugID = 0;
                objmedi.DrugName = "";
            }

            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtfrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtfrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? GlobalConstant.MaxdateAddOneYear : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objmedi.DateFrom = from;
            objmedi.DateTo = To;
            return objBO.GetMedicationList(objmedi);

        }

        protected void btnclear_Click(object sender, System.EventArgs e)
        {

            gvmedichartdetails.DataSource = null;
            gvmedichartdetails.DataBind();
            gvmedichartdetails.Visible = false;
            txtpatientNames.Text = "";
            txtwardbedno.Text = "";
            txtage.Text = "";
            txtgender.Text = "";
            txtweight.Text = "";
            txtdoa.Text = "";
            txtdrugname.Text = "";
            txtstartdate.Text = "";
            txtfrequency.Text = "";
            ddlroute.SelectedIndex = 0;
            txtDoctorName.Text = "";
            txtfrom.Text = "";
            txtto.Text = "";
            divmsg1.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            divmsg1.Visible = false;
            ViewState["ID"] = null;
            btnsave.Text = "Add";
            btnsave.Attributes.Remove("disabled");
            Clear();

        }
        // --------TAB1 + TAB2------------//
        protected void gvmedichart_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    MedicationChartData objData = new MedicationChartData();
                    MedicationChartBO objBO = new MedicationChartBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gvlist = gvmedichartdetails.Rows[i];
                    Label ID = (Label)gvlist.Cells[0].FindControl("lblID");
                    Label MediNo = (Label)gvlist.Cells[0].FindControl("lblmcno");
                    objData.ID = Convert.ToInt32(ID.Text);
                    objData.MedCNo = MediNo.Text.Trim();
                    objData.ActionType = Enumaction.Select;

                    List<MedicationChartData> GetResult = objBO.GetIPPatientDrugEntryByID(objData);
                    if (GetResult.Count > 0)
                    {
                        txtdrugname.Text = GetResult[0].DrugName;
                        txtstartdate.Text = GetResult[0].StartDate.ToString("dd/MM/yyyy");
                        txtfrequency.Text = GetResult[0].Frequency;
                        ddlroute.SelectedValue = GetResult[0].RouteID.ToString();
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
                    MedicationChartData objDrgData = new MedicationChartData();
                    MedicationChartBO objDrgBO = new MedicationChartBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvmedichartdetails.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    objDrgData.ID = Convert.ToInt32(ID.Text);
                    objDrgData.EmployeeID = LogData.EmployeeID;
                    objDrgData.ActionType = Enumaction.Delete;
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

                    int Result = objDrgBO.DeleteIPPatientDrugEntryByID(objDrgData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SucessAlert";
                        bindgrid();
                    }
                    else if (Result == 5)
                    {
                        Messagealert_.ShowMessage(lblmessage, "This drug cannot be delete because medication have already started.", 0);
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
                if (e.CommandName == "Select")
                {

                    MedicationChartData objcondemn = new MedicationChartData();
                    MedicationChartBO objstdBO = new MedicationChartBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvmedichartdetails.Rows[i];
                    Label MediNo = (Label)gr.Cells[0].FindControl("lblmcno");
                    Label lbldrugID = (Label)gr.Cells[0].FindControl("lbldrugID");
                    hdnMediCNo.Value = MediNo.Text.Trim();
                    bindMedicationdetails(MediNo.Text);

                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void bindMedicationdetails(string medino)
        {
            MedicationChartData objmedi = new MedicationChartData();
            MedicationChartBO objstdBO = new MedicationChartBO();
            objmedi.MedCNo = medino.Trim();
            List<MedicationChartData> List = new List<MedicationChartData>();
            List = objstdBO.GetMedicationDetailsList(objmedi);
            if (List.Count > 0)
            {
                Clear();
                tabMedicationChart.ActiveTabIndex = 1;
                hdnUHID2.Value = List[0].UHID.ToString();
                txtuhid.Text = List[0].UHID.ToString();
                txtipno.Text = List[0].IPNo.ToString();
                hdnIPNO2.Value = List[0].IPNo.ToString();
                txtIPpatient2.Text = List[0].IPPatientName.ToString();
                txtwardbedNo2.Text = List[0].WardBedNo.ToString();
                hdnDrugID2.Value = List[0].DrugID.ToString();
                txtdrug.Text = List[0].DrugName.ToString();
                txtstartdate2.Text = List[0].StartDate2.ToString();
                txtfrequency2.Text = List[0].Frequency.ToString();
                hdnRouteID.Value = List[0].RouteID.ToString();
                txtroute2.Text = List[0].RouteName.ToString();
                bindgrid2();
                txtmedicationdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                btn_save.Attributes.Remove("disabled");

            }
            else
            {
                tabMedicationChart.ActiveTabIndex = 1;
                gvmedichartdetails.DataSource = null;
                gvmedichartdetails.DataBind();
                gvmedichartdetails.Visible = true;
            }

        }
        protected void btn_save_OnClick(object sender, EventArgs e)
        {
            try
            {
                MedicationChartData ObjList = new MedicationChartData();
                MedicationChartBO objOTBO = new MedicationChartBO();
                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);

                if (LogData.SaveEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "SaveEnable", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }

                if (hdnMediCNo.Value == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "Patient Drug entry number not found. System couldn't be process your request. ", 0);
                    divmsg2.Attributes["class"] = "FailAlert";
                    divmsg2.Visible = true;
                    txtIPpatient2.Focus();
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (txtuhid.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "UHID is blank, System couldn't be process your request.", 0);
                    divmsg2.Attributes["class"] = "FailAlert";
                    divmsg2.Visible = true;
                    txtuhid.Focus();
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (txtipno.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "IPNo is blank, System couldn't be process your request.", 0);
                    divmsg2.Attributes["class"] = "FailAlert";
                    divmsg2.Visible = true;
                    txtipno.Focus();
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (txtdrug.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "Drug name cannot be blank!", 0);
                    divmsg2.Attributes["class"] = "FailAlert";
                    divmsg2.Visible = true;
                    txtdrug.Focus();
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }

                if (txtmedicationdate.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "Medication date cannot be empty.", 0);
                    divmsg2.Attributes["class"] = "FailAlert";
                    divmsg2.Visible = true;
                    txtmedicationdate.Focus();
                    return;
                }
                else if (txtmedicationdate.Text != "")
                {
                    if (Commonfunction.isValidDate(txtmedicationdate.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txtmedicationdate.Focus();
                        return;
                    }

                    if (Commonfunction.ChecklowerDate(txtmedicationdate.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txtmedicationdate.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage2.Visible = false;
                }

                if (txtmeditime.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "Medication time cannot be empty.", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    txtmeditime.Focus();
                    return;
                }
                else if (txtmeditime.Text != "")
                {
                    //    if (Commonfunction.isValidTime(txtmeditime.Text) == false)
                    //    {
                    //        Messagealert_.ShowMessage(lblmessage2, "ValidTime", 0);
                    //        divmsg2.Attributes["class"] = "FailAlert";
                    //        divmsg2.Visible = true;
                    //        txtmeditime.Focus();
                    //        return;
                    //    }

                    //    if (Commonfunction.ChecklowerTime(txtmeditime.Text) == false)
                    //    {
                    //        Messagealert_.ShowMessage(lblmessage, "ExcessTime", 0);
                    //        divmsg2.Attributes["class"] = "FailAlert";
                    //        divmsg2.Visible = true;
                    //        txtmeditime.Focus();
                    //        return;
                    //    }
                }
                else
                {
                    lblmessage2.Visible = false;
                }

                ObjList.MedicationNo = hdnMediCNo.Value == "" ? "0" : hdnMediCNo.Value;
                ObjList.UHID = Convert.ToInt64(hdnUHID2.Value == "" ? "0" : hdnUHID2.Value);
                ObjList.IPNo = hdnIPNO2.Value == "" ? "0" : hdnIPNO2.Value;
                ObjList.IPPatientName = txtIPpatient2.Text == "" ? "" : txtIPpatient2.Text;
                ObjList.WardBedNo = txtwardbedNo2.Text == "" ? "" : txtwardbedNo2.Text;
                ObjList.DrugID = Convert.ToInt32(hdnDrugID2.Value == "" ? "0" : hdnDrugID2.Value);
                ObjList.DrugName = txtdrug.Text == "" ? "" : txtdrug.Text;
                ObjList.StartDate2 = txtstartdate2.Text == "" ? "" : txtstartdate2.Text;
                ObjList.RouteID = Convert.ToInt32(hdnRouteID.Value == "" ? "0" : hdnRouteID.Value);
                DateTime MediDate = txtmedicationdate.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtmedicationdate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                ObjList.MedicationDate = MediDate;
                ObjList.MedicationTime = txtmeditime.Text == "" ? "0" : txtmeditime.Text;
                ObjList.EmployeeID = LogData.EmployeeID;
                ObjList.HospitalID = LogData.HospitalID;
                ObjList.FinancialYearID = LogData.FinancialYearID;
                ObjList.ActionType = Enumaction.Insert;

                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "UpdateEnable", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        txtmeditime.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage2.Visible = false;
                    }

                    ObjList.ActionType = Enumaction.Update;
                    ObjList.ID = Convert.ToInt64(ViewState["ID"].ToString() == "" ? "0" : ViewState["ID"].ToString());
                }
                int results = objOTBO.UpdateDrugMedication(ObjList);
                if (results > 0)
                {
                    if (results == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "save", 1);
                        divmsg2.Attributes["class"] = "SucessAlert";
                        divmsg2.Visible = true;
                        btn_save.Attributes["disabled"] = "disabled";
                        bindgrid2();
                    }
                    if (results == 2)
                    {

                        Messagealert_.ShowMessage(lblmessage2, "update", 1);
                        divmsg2.Attributes["class"] = "SucessAlert";
                        divmsg2.Visible = true;
                        btn_save.Attributes["disabled"] = "disabled";
                        bindgrid2();
                    }
                    if (results == 3)
                    {

                        Messagealert_.ShowMessage(lblmessage2, "GreaterFrequency", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;

                    }
                    if (results == 4)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "Patient is not found in IP admission", 0);
                        lblmessage2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;

                    }
                    if (results == 5)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "This IP Patient had decharged", 0);
                        lblmessage2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;

                    }
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage2, "system", 0);
                    lblmessage2.Attributes["class"] = "FailAlert";
                    divmsg2.Visible = true;
                }


            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                string msg = ex.ToString();
                Messagealert_.ShowMessage(lblmessage2, msg, 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        protected void ddlmonth_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            bindgrid2();
        }
        protected void btnsearchs2_Click(object sender, EventArgs e)
        {
            bindgrid2();
        }
        protected void bindgrid2()
        {
            try
            {
                if (LogData.SearchEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }

                if (txtuhid.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "UHID", 0);
                    divmsg2.Attributes["class"] = "FailAlert";
                    divmsg2.Visible = true;
                    txtuhid.Focus();
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (txtipno.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "IPNO", 0);
                    divmsg2.Attributes["class"] = "FailAlert";
                    divmsg2.Visible = true;
                    txtipno.Focus();
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (hdnDrugID2.Value == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "IPNO", 0);
                    divmsg2.Attributes["class"] = "FailAlert";
                    divmsg2.Visible = true;
                    txtdrug.Focus();
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }

                List<MedicationChartData> obj = GetDrugMedicationList(0);
                if (obj.Count > 0)
                {
                    Messagealert_.ShowMessage(lblresults, "Total:" + obj[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg.Visible = true;
                    divmsg.Attributes["class"] = "SucessAlert";
                    GvDrugMedicationlist.DataSource = obj;
                    GvDrugMedicationlist.DataBind();
                    GvDrugMedicationlist.Visible = true;
                }
                else
                {
                    GvDrugMedicationlist.DataSource = null;
                    GvDrugMedicationlist.DataBind();
                    GvDrugMedicationlist.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                divmsg2.Visible = true;
            }
        }
        private List<MedicationChartData> GetDrugMedicationList(int p)
        {
            MedicationChartData objdrugmedi = new MedicationChartData();
            MedicationChartBO objBO = new MedicationChartBO();
            objdrugmedi.UHID = Convert.ToInt64(txtuhid.Text == "" ? "0" : txtuhid.Text);
            objdrugmedi.IPNo = txtipno.Text == "" ? "" : txtipno.Text;
            objdrugmedi.DrugID = Convert.ToInt32(hdnDrugID2.Value == "" ? "" : hdnDrugID2.Value);
            objdrugmedi.MonthID = Convert.ToInt32(ddlmonth.SelectedValue == "" ? "0" : ddlmonth.SelectedValue);
            return objBO.GetDrugMedicationList(objdrugmedi);

        }
        protected void GvDrugMedicationlist_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        lblmessage2.Visible = false;
                    }
                    MedicationChartData objDrgData = new MedicationChartData();
                    MedicationChartBO objBO = new MedicationChartBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gvlist = GvDrugMedicationlist.Rows[i];
                    Label ID = (Label)gvlist.Cells[0].FindControl("lblMediID");
                    Label DrgMediNo = (Label)gvlist.Cells[0].FindControl("lblmediNo");
                    objDrgData.ID = Convert.ToInt32(ID.Text);
                    objDrgData.MedicationNo = DrgMediNo.Text.Trim();
                    objDrgData.ActionType = Enumaction.Select;

                    List<MedicationChartData> GetResult = objBO.GetDrugMedicationEntryByID(objDrgData);
                    if (GetResult.Count > 0)
                    {
                        txtdrugname.Text = GetResult[0].DrugName;
                        txtmedicationdate.Text = GetResult[0].MedicationDate.ToString("dd/MM/yyyy");
                        txtmeditime.Text = GetResult[0].MedicationTime;
                        ViewState["ID"] = GetResult[0].ID;
                        btn_save.Attributes.Remove("disabled");
                        btn_save.Text = "Update";

                    }
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
                    MedicationChartData objDrgData = new MedicationChartData();
                    MedicationChartBO objDrgBO = new MedicationChartBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvDrugMedicationlist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblMediID");
                    objDrgData.ID = Convert.ToInt32(ID.Text);
                    objDrgData.EmployeeID = LogData.EmployeeID;
                    objDrgData.ActionType = Enumaction.Delete;
                    //TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    //txtremarks.Enabled = true;
                    //if (txtremarks.Text == "")
                    //{
                    //    Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                    //    divmsg2.Visible = true;
                    //    divmsg2.Attributes["class"] = "FailAlert";
                    //    txtremarks.Focus();
                    //    return;
                    //}
                    //else
                    //{
                    //    objDrgData.Remarks = txtremarks.Text;
                    //}

                    int Result = objDrgBO.DeleteDrugMedicationEntryByID(objDrgData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "SucessAlert";
                        bindgrid2();
                    }
                    else if (Result == 5)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "This drug cannot be delete because medication have already started.", 1);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";

                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage2, "system", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";

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
        protected void btn_AddNew_Click(object sender, System.EventArgs e)
        {
            txtmeditime.Text = "";
            btn_save.Attributes.Remove("disabled");
        }
        protected void btn_Reset_Click(object sender, System.EventArgs e)
        {
            Clear();
        }

        protected void Clear()
        {
            hdnUHID2.Value = "";
            txtuhid.Text = "";
            txtipno.Text = "";
            hdnIPNO2.Value = "";
            txtIPpatient2.Text = "";
            txtwardbedNo2.Text = "";
            hdnDrugID2.Value = "";
            txtdrug.Text = "";
            txtstartdate2.Text = "";
            txtfrequency2.Text = "";
            hdnRouteID.Value = "";
            txtroute2.Text = "";
            txtmedicationdate.Text = "";
            txtmeditime.Text = "";
            GvDrugMedicationlist.DataSource = null;
            GvDrugMedicationlist.DataBind();
            GvDrugMedicationlist.Visible = true;
            divmsg.Visible = false;
            lblresults.Text = "";
            tabMedicationChart.ActiveTabIndex = 0;
            btn_save.Text = "Add";
            btn_save.Attributes["disabled"] = "disabled";
        }
    }
}