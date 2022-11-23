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
using System.Drawing;

namespace Mediqura.Web.MedOT
{
    public partial class OTScheduling : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                supplementoryvalues();
                Session["OTSchedulistList"] = null;
                btnUpdate.Attributes["disabled"] = "disabled";
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_ottheater, mstlookup.GetLookupsList(LookupName.OTtheater));
            Commonfunction.PopulateDdl(ddlconsultant, mstlookup.GetLookupsList(LookupName.Doctor));
            Commonfunction.PopulateDdl(ddlOTStatuss, mstlookup.GetLookupsList(LookupName.OTStatusID));
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
        protected void btnresets_Click(object sender, System.EventArgs e)
        {
            lblmessage.Visible = false;
            lblresult.Visible = false;
            divmsg1.Visible = false;
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlconsultant, mstlookup.GetLookupsList(LookupName.Doctor));
            txtpatientNames.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            gvotschedulingdetails.DataSource = null;
            gvotschedulingdetails.DataBind();
            gvotschedulingdetails.Visible = false;
            Session["OTSchedulistList"] = null;
            btnexport.Visible = false;
            ddlexport.Visible = false;
            btnUpdate.Attributes["disabled"] = "disabled";
            ddl_ottheater.SelectedIndex = 0;
            ddlOTStatuss.SelectedIndex = 0;
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            List<OTSchedulingData> OTSchedulistList = Session["OTSchedulistList"] == null ? new List<OTSchedulingData>() : (List<OTSchedulingData>)Session["OTSchedulistList"];
            OTSchedulingData ObjService = new OTSchedulingData();
            ObjService.PatientName = txtpatientNames.Text.ToString();
            ObjService.ID = 0;
            ObjService.RowNo = ((gvotschedulingdetails.Rows.Count) + 1);
            OTSchedulistList.Add(ObjService);
            if (OTSchedulistList.Count > 0)
            {
                gvotschedulingdetails.DataSource = OTSchedulistList;
                gvotschedulingdetails.DataBind();
                gvotschedulingdetails.Visible = true;
                Session["OTSchedulistList"] = OTSchedulistList;
                txtpatientNames.Text = "";
                btnUpdate.Attributes.Remove("disabled");
            }
            else
            {
                gvotschedulingdetails.DataSource = null;
                gvotschedulingdetails.DataBind();
                gvotschedulingdetails.Visible = true;
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
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
                List<OTSchedulingData> obj = GetPatientList(0);
                if (obj.Count > 0)
                {
                    List<OTSchedulingData> OTSchedulistList = Session["OTSchedulistList"] == null ? new List<OTSchedulingData>() : (List<OTSchedulingData>)Session["OTSchedulistList"];
                    Session["OTSchedulistList"] = obj;
                    gvotschedulingdetails.DataSource = Session["OTSchedulistList"];
                    Messagealert_.ShowMessage(lblresult, "Total:" + obj[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";

                    gvotschedulingdetails.DataSource = obj;
                    gvotschedulingdetails.DataBind();
                    gvotschedulingdetails.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    btnUpdate.Attributes.Remove("disabled");
                    btnUpdate.Enabled = true;

                }
                else
                {
                    gvotschedulingdetails.DataSource = null;
                    gvotschedulingdetails.DataBind();
                    gvotschedulingdetails.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;

                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }
        protected void gvotschedulingdetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
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
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvotschedulingdetails.Rows[i];

                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    if (ID.Text == "0")
                    {
                        List<OTSchedulingData> OTSchedulistList = Session["OTSchedulistList"] == null ? new List<OTSchedulingData>() : (List<OTSchedulingData>)Session["OTSchedulistList"];
                        OTSchedulistList.RemoveAt(i);
                        if (OTSchedulistList.Count > 0)
                        {
                            Session["OTSchedulistList"] = OTSchedulistList;
                            gvotschedulingdetails.DataSource = OTSchedulistList;
                            gvotschedulingdetails.DataBind();
                        }
                        else
                        {
                            Session["OTSchedulistList"] = null;
                            gvotschedulingdetails.DataSource = null;
                            gvotschedulingdetails.DataBind();
                        }
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
                    }
                    else
                    {
                        OTSchedulingData objpat = new OTSchedulingData();
                        OTSchedulingBO objBO = new OTSchedulingBO();

                        objpat.ID = Convert.ToInt32(ID.Text);

                        objpat.EmployeeID = LogData.EmployeeID;
                        int Result = objBO.CancelOTScheduling(objpat);
                        if (Result == 1)
                        {
                            bindgrid();

                            Messagealert_.ShowMessage(lblmessage, "cancel", 1);
                            divmsg1.Attributes["class"] = "SucessAlert";
                            divmsg1.Visible = true;
                        }
                        else
                        {
                            Messagealert_.ShowMessage(lblmessage, "system", 0);
                            divmsg1.Attributes["class"] = "FailAlert";
                            divmsg1.Visible = true;
                        }
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

            objpat.DoctorID = Convert.ToInt32(ddlconsultant.SelectedValue == "" ? "0" : ddlconsultant.SelectedValue);
            objpat.TheatreID = Convert.ToInt32(ddl_ottheater.SelectedValue == "" ? "0" : ddl_ottheater.SelectedValue);
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? GlobalConstant.MaxdateAddOneYear : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            objpat.OTStatusID = Convert.ToInt32(ddlOTStatuss.SelectedValue == "" ? "0" : ddlOTStatuss.SelectedValue);
            return objBO.GetPatientList(objpat);

        }
        protected void supplementoryvalues()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Session["unitlist"] = null;
            Session["methodlist"] = null;
            Session["reagentlist"] = null;
            Session["samplelist"] = null;
            Session["rowtypelist"] = null;
            Session["containerlist"] = null;
            Session["anaesthetist"] = null;
            Session["otstatus"] = null;
            List<LookupItem> theatrelist = Session["theatrelist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["theatrelist"];
            Session["theatrelist"] = mstlookup.GetLookupsList(LookupName.OTtheater);
            List<LookupItem> anaesthetist = Session["anaesthetist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["anaesthetist"];
            Session["anaesthetist"] = mstlookup.GetLookupsList(LookupName.AnaesthesiaEmpList);
            List<LookupItem> otstatus = Session["otstatus"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["otstatus"];
            Session["otstatus"] = mstlookup.GetLookupsList(LookupName.OTStatusID);
        }
        protected void gvotschedulingdetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            List<LookupItem> theatrelist = Session["theatrelist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["theatrelist"];
            List<LookupItem> anaesthetistlist = Session["anaesthetist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["anaesthetist"];
            List<LookupItem> otstatus = Session["otstatus"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["otstatus"];
            foreach (GridViewRow row in gvotschedulingdetails.Rows)
            {
                try
                {
                    TextBox OTdate = (TextBox)gvotschedulingdetails.Rows[row.RowIndex].Cells[2].FindControl("txtotdate");
                    TextBox OTStartTime = (TextBox)gvotschedulingdetails.Rows[row.RowIndex].Cells[3].FindControl("otstarttime");
                    TextBox OTEndTime = (TextBox)gvotschedulingdetails.Rows[row.RowIndex].Cells[4].FindControl("otendtime");
                    TextBox PatientName = (TextBox)gvotschedulingdetails.Rows[row.RowIndex].Cells[5].FindControl("txtName");
                    TextBox OperationName = (TextBox)gvotschedulingdetails.Rows[row.RowIndex].Cells[7].FindControl("txt_operationName");
                    TextBox Surgeon = (TextBox)gvotschedulingdetails.Rows[row.RowIndex].Cells[8].FindControl("txtSurgeon");
                    DropDownList ddl1 = (DropDownList)gvotschedulingdetails.Rows[row.RowIndex].Cells[9].FindControl("ddl_theater");
                    DropDownList ddl3 = (DropDownList)gvotschedulingdetails.Rows[row.RowIndex].Cells[10].FindControl("ddl_anaesthtist");
                    DropDownList ddl4 = (DropDownList)gvotschedulingdetails.Rows[row.RowIndex].Cells[11].FindControl("ddl_ddlstatus");
                    Label TheatreID = (Label)gvotschedulingdetails.Rows[row.RowIndex].Cells[9].FindControl("lbl_TheatreID");
                    Label anaesthtistID = (Label)gvotschedulingdetails.Rows[row.RowIndex].Cells[10].FindControl("lbl_anaesthtist");
                    Label OTStatusID = (Label)gvotschedulingdetails.Rows[row.RowIndex].Cells[12].FindControl("lbl_OTStatusID");
                    TextBox Remark = (TextBox)gvotschedulingdetails.Rows[row.RowIndex].Cells[12].FindControl("txt_remark");

                    Commonfunction.PopulateDdl(ddl1, theatrelist);
                    Commonfunction.PopulateDdl(ddl3, anaesthetistlist);
                    Commonfunction.PopulateDdl(ddl4, otstatus);
                    if (TheatreID.Text != "0")
                    {
                        ddl1.Items.FindByValue(TheatreID.Text).Selected = true;
                    }
                    else
                    {
                        ddl1.SelectedItem.Text = "--Select--";
                    }

                    if (anaesthtistID.Text != "0")
                    {
                        ddl3.Items.FindByValue(anaesthtistID.Text).Selected = true;
                    }
                    else
                    {
                        ddl3.SelectedItem.Text = "--Select--";
                    }
                    if (OTStatusID.Text != "0")
                    {
                        ddl4.Items.FindByValue(OTStatusID.Text).Selected = true;
                    }
                    else
                    {
                        ddl4.SelectedItem.Text = "--Select--";
                    }

                    if (OTStatusID.Text == "3")
                    {
                        OTdate.Enabled = false;
                        OTStartTime.Enabled = false;
                        OTEndTime.Enabled = false;
                        PatientName.Enabled = false;
                        OperationName.Enabled = false;
                        Surgeon.Enabled = false;
                        ddl1.Enabled = false;
                        ddl3.Enabled = false;
                        TheatreID.Enabled = false;
                        anaesthtistID.Enabled = false;
                        OTStatusID.Enabled = false;
                        Remark.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                    LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                }
            }

        }
        protected void btupdate_Click(object sender, EventArgs e)
        {
            List<OTSchedulingData> objlist = new List<OTSchedulingData>();
            OTSchedulingBO objbo = new OTSchedulingBO();
            OTSchedulingData objdata = new OTSchedulingData();
            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in gvotschedulingdetails.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label lblOTNo = (Label)gvotschedulingdetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_OTNo");
                    TextBox otdate = (TextBox)gvotschedulingdetails.Rows[row.RowIndex].Cells[0].FindControl("txtotdate");
                    TextBox OTStartTime = (TextBox)gvotschedulingdetails.Rows[row.RowIndex].Cells[0].FindControl("otstarttime");
                    TextBox OTEndTime = (TextBox)gvotschedulingdetails.Rows[row.RowIndex].Cells[0].FindControl("otendtime");
                    TextBox PatName = (TextBox)gvotschedulingdetails.Rows[row.RowIndex].Cells[0].FindControl("txtName");
                    Label WardBedNo = (Label)gvotschedulingdetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_wardbedno");
                    TextBox OperationName = (TextBox)gvotschedulingdetails.Rows[row.RowIndex].Cells[0].FindControl("txt_operationName");
                    Label DoctorID = (Label)gvotschedulingdetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_DoctorID");
                    TextBox docname = (TextBox)gvotschedulingdetails.Rows[row.RowIndex].Cells[0].FindControl("txtSurgeon");
                    DropDownList theatreid = (DropDownList)gvotschedulingdetails.Rows[row.RowIndex].Cells[0].FindControl("ddl_theater");
                    DropDownList Anaesthtist = (DropDownList)gvotschedulingdetails.Rows[row.RowIndex].Cells[0].FindControl("ddl_anaesthtist");
                    DropDownList ddlstatus = (DropDownList)gvotschedulingdetails.Rows[row.RowIndex].Cells[0].FindControl("ddl_ddlstatus");
                    TextBox Remark = (TextBox)gvotschedulingdetails.Rows[row.RowIndex].Cells[0].FindControl("txt_remark");
                    OTSchedulingData ObjDetails = new OTSchedulingData();
                    if (PatName.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please enter patient name.", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        PatName.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    string ID;
                    string name;
                    var source = PatName.Text.ToString();
                    if (source.Contains(":"))
                    {
                        ID = source.Substring(source.LastIndexOf(':') + 1);
                        int indexStop = source.LastIndexOf('/');
                        name = source.Substring(0, indexStop);
                        ObjDetails.UHID = Convert.ToInt64(ID);
                        ObjDetails.PatientName = name;
                    }
                    else
                    {
                        ObjDetails.PatientName = PatName.Text;
                    }
                    if (OTStartTime.Text == "" || OTEndTime.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please enter operation start and end time.", 0);
                        OTStartTime.Text = "";
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        OTStartTime.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    if (docname.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please enter surgeon.", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        docname.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }


                    string ID1;
                    string name1;
                    var source1 = docname.Text.ToString();
                    if (source1.Contains(":"))
                    {
                        ID1 = source1.Substring(source1.LastIndexOf(':') + 1);
                        int indexStop = source1.LastIndexOf('/');
                        name1 = source1.Substring(0, indexStop);

                        ObjDetails.DoctorID = Convert.ToInt64(ID1);
                        ObjDetails.Surgeon = name1;
                    }
                    else
                    {
                        ObjDetails.DoctorID = Convert.ToInt32(DoctorID.Text);
                        ObjDetails.Surgeon = docname.Text;
                    }

                    if (Commonfunction.isValidDate(otdate.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                        txtdatefrom.Text = "";
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        otdate.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    if (theatreid.SelectedIndex == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please select theatre.", 0);                       
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        theatreid.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    if (OperationName.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please enter operation name.", 0);                       
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        OperationName.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    if (ddlstatus.SelectedIndex == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please select OT Status.", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        ddlstatus.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    DateTime Otdate = otdate.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(otdate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                    ObjDetails.OTNo = lblOTNo.Text.Trim();
                    ObjDetails.Date = Otdate;
                    ObjDetails.OTStartTime = OTStartTime.Text;
                    ObjDetails.OTEndTime = OTEndTime.Text;
                    ObjDetails.WardBedName = WardBedNo.Text;
                    ObjDetails.OperationName = OperationName.Text == "" ? "" : OperationName.Text;
                    ObjDetails.TheatreID = Convert.ToInt32(theatreid.Text == "" ? "0" : theatreid.Text);
                    ObjDetails.AnaesthetistID = Convert.ToInt32(Anaesthtist.Text == "" ? "0" : Anaesthtist.Text);
                    ObjDetails.OTStatusID = Convert.ToInt32(ddlstatus.Text == "" ? "0" : ddlstatus.Text);
                    ObjDetails.Remark = Remark.Text == "" ? "" : Remark.Text;
                    objlist.Add(ObjDetails);
                }
                objdata.XMLData = XmlConvertor.OTSchedulingRecordDatatoXML(objlist).ToString();
                objdata.FinancialYearID = LogData.FinancialYearID;
                objdata.EmployeeID = LogData.EmployeeID;
                objdata.HospitalID = LogData.HospitalID;
                objdata.IPaddress = LogData.IPaddress;
                objdata.ActionType = Enumaction.Insert;
                int result = objbo.UpdateOTSchedulingDetails(objdata);
                if (result > 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    gvotschedulingdetails.DataSource = null;
                    gvotschedulingdetails.DataBind();
                    gvotschedulingdetails.Visible = false;
                    bindgrid();
                    lblmessage.Visible = true;
                    btnUpdate.Attributes["disabled"] = "disabled";
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
            }
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ExportEnable", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddlexport.SelectedIndex == 1)
            {
                ExportoExcel();
            }

            else
            {
                Messagealert_.ShowMessage(lblmessage, "ExportType", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Item CheckList");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=OTSchedulingDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblmessage, "Exported", 1);
                divmsg1.Attributes["class"] = "SucessAlert";
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<OTSchedulingData> DepositDetails = GetPatientList(0);
            List<OTSchedulingDataTOeXCEL> ListexcelData = new List<OTSchedulingDataTOeXCEL>();
            int i = 0;
            foreach (OTSchedulingData row in DepositDetails)
            {
                OTSchedulingDataTOeXCEL Ecxeclpat = new OTSchedulingDataTOeXCEL();
                Ecxeclpat.PatientName = DepositDetails[i].PatientName;
                Ecxeclpat.Surgeon = DepositDetails[i].Surgeon;
                Ecxeclpat.OpernDate = DepositDetails[i].OpernDate;
                Ecxeclpat.OpernTime = DepositDetails[i].OpernTime;
                Ecxeclpat.Theatre = DepositDetails[i].Theatre;
                Ecxeclpat.Cases = DepositDetails[i].Cases;

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
                    Messagealert_.ShowMessage(lblresult2, "Total:" + obj[0].MaximumRows.ToString() + " Record(s) found.", 1);
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
            lblresult2.Visible = false;
            divmsg.Visible = false;


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
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                if (IsSubHeading.Text == "1")
                {
                    e.Row.BackColor = Color.FromName("#33aa99");
                    lblpatientname.ForeColor = System.Drawing.Color.White;
                    txtremarks.Visible = false;
                    lnkDelete.Visible = false;
                }

            }

        }
        protected void GvSchedulelist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
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
                Messagealert_.ShowMessage(lblresult, "system", 0);
                lblmessage.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
            }
        }
    }
}
