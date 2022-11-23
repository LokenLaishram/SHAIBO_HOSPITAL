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

namespace Mediqura.Web.MedUtility
{
    public partial class LabSampleCollection : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlbind();
                lblmessage.Visible = false;
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddltakenby, mstlookup.GetLookupsList(LookupName.LabTech));
            Commonfunction.PopulateDdl(ddl_referal, mstlookup.GetLookupsList(LookupName.Labconsultant));
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
                List<SampleCollectionData> lstemp = GetLabTestSamplelist(0);
                if (lstemp.Count > 0)
                {
                    gvSample.DataSource = lstemp;
                    gvSample.DataBind();
                    gvSample.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    txt_Reamrks.Text = lstemp[0].Comment.ToString();
                }
                else
                {
                    gvSample.DataSource = null;
                    gvSample.DataBind();
                    gvSample.Visible = true;
                    lblresult.Visible = false;
                    txt_Reamrks.Text = "";
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        private List<SampleCollectionData> GetLabTestSamplelist(int p)
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
            objsample.CollectionStatus = Convert.ToInt32(ddl_status.SelectedValue == "" ? "0" : ddl_status.SelectedValue);
            return objlabBO.GetLabSampleCollectedDetails(objsample);
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txt_datefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txt_dateto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            lblresult.Text = "";
            lblmessage.Text = "";
            lblmessage.Visible = false;
            lblresult.Visible = false;
            ddltakenby.SelectedIndex = 0;
            lblresult.Visible = false;
            divmsg3.Visible = false;
            ddl_referal.SelectedIndex = 0;
            txt_name.Text = "";
            ddl_patienttype.SelectedIndex = 0;
            txt_invno.Text = "";
            gvSample.DataSource = null;
            gvSample.DataBind();
            gvSample.Visible = false;
            txt_Reamrks.Text = "";
            btnsave.Attributes.Remove("disabled");
            txt_invno.Attributes.Remove("disabled");
            tabcontainerSampleCollection.ActiveTabIndex = 0;
        }
        protected void txt_BillNo_TextChanged(object sender, EventArgs e)
        {

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
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
                if (ddl_patienttype.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "PatientType", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_patienttype.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
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
                if (txt_Reamrks.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Casehistory", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_Reamrks.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (ddltakenby.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "takenby", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddltakenby.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;

                }
                List<SampleCollectionData> Listbill = new List<SampleCollectionData>();
                LabSampleCollctionBO objLabSampleBO = new LabSampleCollctionBO();
                SampleCollectionData objSampleData = new SampleCollectionData();
                foreach (GridViewRow row in gvSample.Rows)
                {

                    DropDownList testcenterID = (DropDownList)gvSample.Rows[row.RowIndex].Cells[0].FindControl("ddl_testcenter");
                    Label testID = (Label)gvSample.Rows[row.RowIndex].Cells[0].FindControl("lblTestID");
                    Label ID = (Label)gvSample.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label Inv = (Label)gvSample.Rows[row.RowIndex].Cells[0].FindControl("lbl_invnumber");
                    CheckBox chkcollection = (CheckBox)gvSample.Rows[row.RowIndex].Cells[0].FindControl("chk_collection");
                    CheckBox chkoutsourced = (CheckBox)gvSample.Rows[row.RowIndex].Cells[0].FindControl("Chk_Outsourced");
                    SampleCollectionData ObjDetails = new SampleCollectionData();

                    ObjDetails.ID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                    ObjDetails.LabServiceID = Convert.ToInt32(testID.Text == "" ? "0" : testID.Text);
                    ObjDetails.Investigationumber = Inv.Text.Trim();
                    ObjDetails.CollectionStatus = Convert.ToInt32(chkcollection.Checked ? "1" : "0");
                    ObjDetails.IsOutsourcedTest = Convert.ToInt32(chkoutsourced.Checked ? "1" : "0");
                    ObjDetails.TestCenterID = Convert.ToInt32(testcenterID.Text == "" ? "0" : testcenterID.Text);
                    ObjDetails.LabServiceID = Convert.ToInt32(testID.Text == "" ? "0" : testID.Text);
                    Listbill.Add(ObjDetails);
                }
                objSampleData.XMLData = XmlConvertor.SampleDatatoXML(Listbill).ToString();
                objSampleData.TakenBy = Convert.ToInt32(ddltakenby.SelectedValue == "" ? "0" : ddltakenby.SelectedValue);
                objSampleData.EmployeeID = LogData.EmployeeID;
                objSampleData.FinancialYearID = LogData.FinancialYearID;
                objSampleData.HospitalID = LogData.HospitalID;
                objSampleData.Comment = txt_Reamrks.Text.Trim();
                objSampleData.IPaddress = LogData.IPaddress;
                int result = objLabSampleBO.UpdateSampleCollectionDetails(objSampleData);
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    ddl_status.SelectedValue = "1";
                    bindgrid();
                    btnsave.Attributes["disabled"] = "disabled";
                    txt_invno.Attributes["disabled"] = "disabled";
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
            }
        }
        protected void gvSample_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void gvSample_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddl_testcenter = e.Row.FindControl("ddl_testcenter") as DropDownList;
                Label centerID = e.Row.FindControl("lbltestcenterid") as Label;
                Label urgency = e.Row.FindControl("lbl_urgencyid") as Label;
                CheckBox chkcollection = e.Row.FindControl("chk_collection") as CheckBox;
                Label collectionstatus = e.Row.FindControl("lbl_chkcoll_ID") as Label;
                CheckBox Chkoutsourced = e.Row.FindControl("Chk_Outsourced") as CheckBox;
                Label outsourcedstatus = e.Row.FindControl("lbl_TestoutsourcedStatus") as Label;

                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_testcenter, mstlookup.GetLookupsList(LookupName.TestCenter));
                if (centerID.Text != "0")
                {
                    ddl_testcenter.Items.FindByValue(centerID.Text).Selected = true;
                }
                if (collectionstatus.Text == "1")
                {
                    chkcollection.Checked = true;
                }
                else
                {
                    chkcollection.Checked = false;
                }
                if (outsourcedstatus.Text == "1")
                {
                    Chkoutsourced.Checked = true;
                }
                else
                {
                    Chkoutsourced.Checked = false;
                }
                if (urgency.Text == "0" || urgency.Text == "1")
                {
                    e.Row.Cells[9].BackColor = System.Drawing.Color.Green;
                }
                if (urgency.Text == "2")
                {
                    e.Row.Cells[9].BackColor = System.Drawing.Color.Yellow;
                }
                if (urgency.Text == "3")
                {
                    e.Row.Cells[9].BackColor = System.Drawing.Color.Red;
                }
            }
        }
        protected void ddl_patienttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender1.ContextKey = ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue;
            AutoCompleteExtender2.ContextKey = ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue;
        }
    }
}