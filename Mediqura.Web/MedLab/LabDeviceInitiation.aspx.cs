
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
using Mediqura.BOL.AdmissionBO;
using System.Drawing;
using Mediqura.CommonData.LoginData;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.PatientData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using OnBarcode.Barcode;

namespace Mediqura.Web.MedLab
{
    public partial class LabDeviceInitiation : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlbind();
                lblmessage.Visible = false;
                txt_invno.Focus();
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_referal, mstlookup.GetLookupsList(LookupName.Labconsultant));
            Commonfunction.PopulateDdl(ddl_patienttype, mstlookup.GetLookupsList(LookupName.PatientCatagory));
            txt_datefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txt_dateto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");


        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetInv(string prefixText, int count, string contextKey)
        {
            SampleCollectionData Objpaic = new SampleCollectionData();
            LabSampleCollctionBO objInfoBO = new LabSampleCollctionBO();
            List<SampleCollectionData> getResult = new List<SampleCollectionData>();
            Objpaic.Investigationumber = prefixText;
            Objpaic.PatientTypeID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetLabInvestigationno(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].Investigationumber.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetLabPatientName(string prefixText, int count, string contextKey)
        {
            SampleCollectionData Objpaic = new SampleCollectionData();
            LabSampleCollctionBO objInfoBO = new LabSampleCollctionBO();
            List<SampleCollectionData> getResult = new List<SampleCollectionData>();
            Objpaic.PatientName = prefixText;
            Objpaic.PatientTypeID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetLabPatientNames(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        protected void btnsearch_Click(object sender, EventArgs e)
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

            //if (ddl_patienttype.SelectedIndex == 0)
            //{
            //    Messagealert_.ShowMessage(lblmessage, "PatientType", 0);
            //    div1.Visible = true;
            //    div1.Attributes["class"] = "FailAlert";
            //    ddl_patienttype.Focus();
            //    return;
            //}
            //else
            //{
            //    lblmessage.Visible = false;
            //    div1.Visible = false;
            //}
            if (txt_invno.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "InvNo", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_invno.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (txt_datefrom.Text != "")
            {
                if (Commonfunction.isValidDate(txt_datefrom.Text) == false)
                {
                    Messagealert_.ShowMessage(lblmessage, "ValidDatefrom", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_invno.Focus();
                    return;
                }
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (txt_dateto.Text != "")
            {
                if (Commonfunction.isValidDate(txt_dateto.Text) == false)
                {
                    Messagealert_.ShowMessage(lblmessage, "ValidDateto", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_invno.Focus();
                    return;
                }
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            bindgrid();
        }
        private void bindgrid()
        {
            try
            {
                List<SampleCollectionData> lstemp = Getlabdeviceinitiation(0);
                if (lstemp.Count > 0)
                {
                    gvlabdeciceinitiation.DataSource = lstemp;
                    gvlabdeciceinitiation.DataBind();
                    gvlabdeciceinitiation.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                }
                else
                {
                    gvlabdeciceinitiation.DataSource = null;
                    gvlabdeciceinitiation.DataBind();
                    gvlabdeciceinitiation.Visible = true;
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
        private List<SampleCollectionData> Getlabdeviceinitiation(int p)
        {
            SampleCollectionData objsample = new SampleCollectionData();
            LabSampleCollctionBO objlabBO = new LabSampleCollctionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objsample.Investigationumber = txt_invno.Text.Trim() == "" ? null : txt_invno.Text.Trim();
            objsample.PatientTypeID = Convert.ToInt32(ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue);
            objsample.PatientName = txt_name.Text.Trim() == "" ? null : txt_name.Text.Trim();
            DateTime from = txt_datefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_datefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txt_dateto.Text.Trim() == "" ? GlobalConstant.MaxSQLDateTime : DateTime.Parse(txt_dateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objsample.DateFrom = from;
            objsample.DateTo = To;
            objsample.ConsultantID = Convert.ToInt64(ddl_referal.SelectedValue == "" ? "0" : ddl_referal.SelectedValue);
            objsample.StatusID = Convert.ToInt32(ddl_status.SelectedValue == "" ? "0" : ddl_status.SelectedValue);
            return objlabBO.GetLadeviceInitiationDetail(objsample);
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            lblresult.Text = "";
            lblmessage.Text = "";
            lblmessage.Visible = false;
            lblresult.Visible = false;
            lblresult.Visible = false;
            divmsg3.Visible = false;
            ddl_referal.SelectedIndex = 0;
            txt_name.Text = "";
            ddl_patienttype.SelectedIndex = 0;
            txt_invno.Text = "";
            gvlabdeciceinitiation.DataSource = null;
            gvlabdeciceinitiation.DataBind();
            gvlabdeciceinitiation.Visible = false;
            txt_invno.Attributes.Remove("disabled");
        }
        protected void gvlabdeciceinitiation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Update")
                {
                    SampleCollectionData objdevice = new SampleCollectionData();
                    LabSampleCollctionBO objInfoBO = new LabSampleCollctionBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvlabdeciceinitiation.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    Label InvNumber = (Label)gr.Cells[0].FindControl("lbl_invnumber");
                    DropDownList ddl_devicestatus = (DropDownList)gr.Cells[0].FindControl("ddl_devicestatus");
                    objdevice.ID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                    objdevice.StatusID = Convert.ToInt32(ddl_devicestatus.SelectedValue == "" ? "0" : ddl_devicestatus.SelectedValue);
                    objdevice.Investigationumber = InvNumber.Text.Trim();
                    objdevice.EmployeeID = LogData.EmployeeID;
                    int result = objInfoBO.UpdateDeviceInitiation(objdevice);
                    if (result > 0)
                    {
                        gvlabdeciceinitiation.DataSource = null;
                        gvlabdeciceinitiation.DataBind();
                        bindgrid();
                        Messagealert_.ShowMessage(lblresult, "update", 1);
                        divmsg3.Visible = true;
                        divmsg3.Attributes["class"] = "SucessAlert";
                    }
                }
                if (e.CommandName == "Print")
                {
                    //window.open("Report/ReportViewer.aspx?option=worksheet&Inv=" + InvNu + "&UHID=" + UHID + "&SubgrpID=" + Subgroup)

                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvlabdeciceinitiation.Rows[i];
                    Label InvNumber = (Label)gr.Cells[0].FindControl("lbl_Inv");
                    Label UHID = (Label)gr.Cells[0].FindControl("lbl_uhid");
                    Label SubgroupID = (Label)gr.Cells[0].FindControl("lbl_subgroup");

                    string Invns = InvNumber.Text;
                    Int64 UHIDS = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                    int TestIDS = Convert.ToInt32(SubgroupID.Text == "" ? "0" : SubgroupID.Text);

                    string param = "option=worksheet&Inv=" + Invns + "&UHID=" + UHIDS + "&SubgrpID=" + TestIDS;
                    Commonfunction common = new Commonfunction();
                    string ecryptstring = common.Encrypt(param);
                    string baseurl = "../MedLab/Report/ReportViewer.aspx?ID=" + ecryptstring;
                    string fullURL = "window.open('" + baseurl + "', '_blank');";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);

                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;
            }
        }
        protected void gvlabdeciceinitiation_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Int64 ID = Convert.ToInt32(gvlabdeciceinitiation.DataKeys[e.RowIndex].Values["ID"].ToString());
            System.Web.UI.WebControls.Label invnumber = (System.Web.UI.WebControls.Label)gvlabdeciceinitiation.Rows[e.RowIndex].FindControl("lbl_invnumber");
            System.Web.UI.WebControls.DropDownList status = (System.Web.UI.WebControls.DropDownList)gvlabdeciceinitiation.Rows[e.RowIndex].FindControl("ddl_devicestatus");
            SampleCollectionData objdevice = new SampleCollectionData();
            LabSampleCollctionBO objInfoBO = new LabSampleCollctionBO();
            objdevice.ID = ID;
            objdevice.StatusID = Convert.ToInt32(status.SelectedValue == "" ? "0" : status.SelectedValue);
            objdevice.Investigationumber = invnumber.Text.Trim();
            objdevice.EmployeeID = LogData.EmployeeID;
            objdevice.ReceivedBy = ddl_status.SelectedValue == "2" ? LogData.EmployeeID : 0;
            int result = objInfoBO.UpdateDeviceInitiation(objdevice);
            if (result > 0)
            {
                gvlabdeciceinitiation.DataSource = null;
                gvlabdeciceinitiation.DataBind();
                bindgrid();

                Messagealert_.ShowMessage(lblresult, "update", 1);
                divmsg3.Visible = true;
                divmsg3.Attributes["class"] = "SucessAlert";
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void gvlabdeciceinitiation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label urgency = e.Row.FindControl("lbl_urgencyid") as Label;
                Label Status = e.Row.FindControl("lbl_devicestatus") as Label;
                Label Recievstatatus = e.Row.FindControl("lbl_recvstatus") as Label;
                DropDownList ddl_device = e.Row.FindControl("ddl_devicestatus") as DropDownList;


                if (urgency.Text == "0" || urgency.Text == "1")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Green;
                }
                if (urgency.Text == "2")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Yellow;
                }
                if (urgency.Text == "3")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Red;
                }
                if (Recievstatatus.Text == "0")
                {
                    ddl_device.Items[0].Attributes["disabled"] = "disabled";
                    ddl_device.Items[1].Attributes.Remove("disabled");
                    ddl_device.Items[2].Attributes["disabled"] = "disabled";
                    ddl_device.Items[3].Attributes["disabled"] = "disabled";
                }
                if (Status.Text == "1" && Recievstatatus.Text == "1")
                {
                    ddl_device.Items[0].Attributes["disabled"] = "disabled";
                    ddl_device.Items[1].Attributes["disabled"] = "disabled";
                    ddl_device.Items[2].Attributes.Remove("disabled");
                    ddl_device.Items[3].Attributes["disabled"] = "disabled";
                }
                if (Status.Text == "2" && Recievstatatus.Text == "1")
                {
                    ddl_device.Items[0].Attributes["disabled"] = "disabled";
                    ddl_device.Items[1].Attributes["disabled"] = "disabled";
                    ddl_device.Items[2].Attributes["disabled"] = "disabled";
                    ddl_device.Items[3].Attributes.Remove("disabled");
                }
                if (Status.Text == "3" && Recievstatatus.Text == "1")
                {
                    ddl_device.Items[0].Attributes["disabled"] = "disabled";
                    ddl_device.Items[1].Attributes["disabled"] = "disabled";
                    ddl_device.Items[2].Attributes["disabled"] = "disabled";
                    ddl_device.Items[3].Attributes.Remove("disabled");
                    ddl_device.Items[3].Text = "Completed";
                    ddl_device.Attributes["disabled"] = "disabled";
                }

            }
        }
        protected void ddl_patienttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender1.ContextKey = ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue;
            AutoCompleteExtender2.ContextKey = ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue;
        }

        protected void txt_invno_TextChanged(object sender, EventArgs e)
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

            //if (ddl_patienttype.SelectedIndex == 0)
            //{
            //    Messagealert_.ShowMessage(lblmessage, "PatientType", 0);
            //    div1.Visible = true;
            //    div1.Attributes["class"] = "FailAlert";
            //    ddl_patienttype.Focus();
            //    return;
            //}
            //else
            //{
            //    lblmessage.Visible = false;
            //    div1.Visible = false;
            //}
            if (txt_invno.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "InvNo", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_invno.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (txt_datefrom.Text != "")
            {
                if (Commonfunction.isValidDate(txt_datefrom.Text) == false)
                {
                    Messagealert_.ShowMessage(lblmessage, "ValidDatefrom", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_invno.Focus();
                    return;
                }
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (txt_dateto.Text != "")
            {
                if (Commonfunction.isValidDate(txt_dateto.Text) == false)
                {
                    Messagealert_.ShowMessage(lblmessage, "ValidDateto", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_invno.Focus();
                    return;
                }
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            bindgrid();
        }
    }
}