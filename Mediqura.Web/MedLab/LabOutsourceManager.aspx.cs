using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedLabBO;
using Mediqura.BOL.PatientBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.PatientData;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.CommonData.MedLabData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Utility;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using Mediqura.CommonData.MedLab;
using System.Text;
using System.Drawing;

namespace Mediqura.Web.MedLab
{
    public partial class LabOutsourceManager : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                page_setting();
                lblmessage.Visible = false;
             
            }
        }
        protected void page_setting()  //  to bind current month and year
        {
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            btnprints.Attributes["disabled"] = "disabled";
            List<LabOutsourceManagerData> objSchedule = GetOutsourcePageload(0);
            if (objSchedule.Count > 0)
            {
                gvLabTestList.DataSource = objSchedule;
                gvLabTestList.DataBind();
                gvLabTestList.Visible = true;
                Messagealert_.ShowMessage(lblresult, "Total: " + objSchedule[0].MaximumRows.ToString() + " Record(s) found.", 1);
                divmsg3.Attributes["class"] = "SucessAlert";
                divmsg3.Visible = true;
            
            }
            else
            {
                divmsg3.Visible = false;
                gvLabTestList.DataSource = null;
                gvLabTestList.DataBind();
                gvLabTestList.Visible = true;
                divmsg3.Visible = false;
                lblresult.Visible = false;
            }
        }
        public List<LabOutsourceManagerData> GetOutsourcePageload(int curIndex)
        {

            LabOutsourceManagerData objSchedule = new LabOutsourceManagerData();
            LabOutsourceManagerBO objscheduleBO = new LabOutsourceManagerBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objSchedule.DateFrom = from;
            objSchedule.DateTo = To;

            return objscheduleBO.GetOutsourcePageload(objSchedule);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetUHID(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.UHID = Convert.ToInt64(prefixText);
            getResult = objInfoBO.GetUHID(Objpaic);
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
            getResult = objInfoBO.GetPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetLabServices(string prefixText, int count, string contextKey)
        {
            LabServiceMasterData Objpaic = new LabServiceMasterData();
            LabServiceMasterBO objInfoBO = new LabServiceMasterBO();
            List<LabServiceMasterData> getResult = new List<LabServiceMasterData>();
            Objpaic.TestName = prefixText;
            getResult = objInfoBO.GetLabServices(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].TestName.ToString());
            }
            return list;
        }
        protected void txt_UHID_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {

        }
        protected void txt_labservices_TextChanged(object sender, EventArgs e)
        {

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
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                List<LabOutsourceManagerData> objdeposit = GetLabList(0);
                if (objdeposit.Count > 0)
                {
                    gvLabTestList.DataSource = objdeposit;
                    gvLabTestList.DataBind();
                    gvLabTestList.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    txtdatefrom.Attributes["disabled"] = "disabled";
                    txtto.Attributes["disabled"] = "disabled";

                }
                else
                {
                    gvLabTestList.DataSource = null;
                    gvLabTestList.DataBind();
                    gvLabTestList.Visible = true;
                    lblresult.Visible = false;
                    txtdatefrom.Attributes.Remove("disabled");
                    txtto.Attributes.Remove("disabled");
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;
            }
        }
        public List<LabOutsourceManagerData> GetLabList(int curIndex)
        {
            LabOutsourceManagerData objpat = new LabOutsourceManagerData();
            LabOutsourceManagerBO objBO = new LabOutsourceManagerBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.UHID = Convert.ToInt64(txt_UHID.Text.Trim() == "" ? "0" : txt_UHID.Text.Trim());
            objpat.PatientName = txt_patientNames.Text == "" ? null : txt_patientNames.Text.Trim();
            //objpat.TestID =Convert.ToInt32(txt_labservices.Text.Trim() == "" ? "0" : txt_labservices.Text.Trim());
            var source = txt_labservices.Text.Trim();
            if (source.Contains(":"))
            {
                string ID1 = source.Substring(source.LastIndexOf(':') + 1);
                objpat.TestID = Convert.ToInt32(ID1);
            }
            objpat.DateFrom = from;
            objpat.DateTo = To;
            return objBO.GetLabList(objpat);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            gvLabTestList.DataSource = null;
            gvLabTestList.DataBind();
            gvLabTestList.Visible = false;
            txt_UHID.Text = "";
            txt_patientNames.Text = "";
            txt_labservices.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            lblresult.Visible = false;
            lblresult.Text = "";
            lblmessage.Visible = false;
            page_setting();
            txtdatefrom.Attributes.Remove("disabled");
            txtto.Attributes.Remove("disabled");
        }

        protected void gvLabTestList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLabTestList.PageIndex = e.NewPageIndex;
            bindgrid();
        }

        protected void gvLabTestList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cb1 = (CheckBox)e.Row.FindControl("chkselectIsSampleCollcteded");
                Label IsSampleCollcteded = (Label)e.Row.FindControl("lbl_IsSampleCollcteded");
                CheckBox cb2 = (CheckBox)e.Row.FindControl("chkselectIsOutsourcedSampleSend");
                Label IsOutsourcedSampleSend = (Label)e.Row.FindControl("lbl_IsOutsourcedSampleSend");
                CheckBox cb3 = (CheckBox)e.Row.FindControl("chkselectISReportDelivered");
                Label IsOutsourcedReportReceived = (Label)e.Row.FindControl("lbl_IsOutsourcedReportReceived");
                CheckBox cb4 = (CheckBox)e.Row.FindControl("chkselectIsOutsourcedTest");
                Label IsOutsourcedTest = (Label)e.Row.FindControl("lbl_IsOutsourcedTest");
                Button btnsave = (Button)e.Row.FindControl("btnSave");
                //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gvLabTestList, "Select$" + e.Row.RowIndex);
                if (IsSampleCollcteded.Text == "1")
                {
                    cb1.Checked = true;
                    cb1.Enabled = false;
                }
                else
                {
                    cb1.Checked = false;
                }
                if (IsOutsourcedSampleSend.Text == "1")
                {
                    cb2.Checked = true;
                    cb2.Enabled = false;
                }
                else
                {
                    cb2.Checked = false;
                }
                if (IsOutsourcedReportReceived.Text == "1")
                {
                    cb3.Checked = true;
                    cb3.Enabled = false;
                }
                else
                {
                    cb3.Checked = false;
                  
                }
                if (IsOutsourcedTest.Text == "1")
                {
                    cb4.Checked = true;
                    cb4.Enabled = false;
                }
                else
                {
                    cb4.Checked = false;
                  
                }

            }
        }

        protected void gvLabTestList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
             if (e.CommandName == "save")
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
                  
                    List<LabOutsourceManagerData> List = new List<LabOutsourceManagerData>();
                    LabOutsourceManagerBO objBO = new LabOutsourceManagerBO();
                    LabOutsourceManagerData objData = new LabOutsourceManagerData();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    Session["current"] = i;
                    Session["count"] = gvLabTestList.Rows.Count;
                    GridViewRow gr = gvLabTestList.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    CheckBox cb1 = (CheckBox)gr.Cells[0].FindControl("chkselectIsSampleCollcteded");
                    CheckBox cb2 = (CheckBox)gr.Cells[0].FindControl("chkselectIsOutsourcedSampleSend");
                    CheckBox cb3 = (CheckBox)gr.Cells[0].FindControl("chkselectISReportDelivered");
                    CheckBox cb4 = (CheckBox)gr.Cells[0].FindControl("chkselectIsOutsourcedTest");
                    Button btn = (Button)gr.Cells[0].FindControl("btnsave");
                    LabOutsourceManagerData ObjDetails = new LabOutsourceManagerData();
                    if ( i!=null)
                       {
                         if (cb1.Checked == true)
                             {
                               ObjDetails.IsSampleCollcteded = 1;
                             }
                             else
                              {
                                ObjDetails.IsSampleCollcteded = 0;
                              }
                              if (cb2.Checked == true)
                               {
                                 ObjDetails.IsOutsourcedSampleSend = 1;
                               }
                               else
                               {
                                 ObjDetails.IsOutsourcedSampleSend = 0;
                               }
                              if (cb3.Checked == true)
                               {
                                 ObjDetails.IsOutsourcedReportReceived = 1;
                               }
                               else
                               {
                                 ObjDetails.IsOutsourcedReportReceived = 0;
                               }
                               if (cb4.Checked == true)
                                 {
                                     ObjDetails.IsOutsourcedTest = 1;
                                 }
                                 else
                                 {
                                     ObjDetails.IsOutsourcedTest = 0;
                                 }
                                 ObjDetails.ID = Convert.ToInt64(ID.Text);
                                 List.Add(ObjDetails);
                             }
                             
                         objData.XMLData = XmlConvertor.LabOutsourceRecordDatatoXML(List).ToString();
                         objData.EmployeeID = LogData.EmployeeID;
                         objData.FinancialYearID = LogData.FinancialYearID;
                         objData.IPaddress = LogData.IPaddress;
                         objData.HospitalID = LogData.HospitalID;
                         objData.ActionType = Enumaction.Insert;
                         int result = objBO.UpdateLabOutsource(objData);
                         gr = gvLabTestList.SelectedRow;
                         //gvLabTestList.SelectedRow = row;
                         if (result == 1)
                         {
                             lblmessage.Visible = true;
                             Messagealert_.ShowMessage(lblmessage, "update", 1);
                             div1.Attributes["class"] = "SucessAlert";
                             div1.Visible = true;
                             btn.Attributes["disabled"] = "disabled";
                         }
                        
                     }
                 
                }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
          
        }

        protected void gvLabTestList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Session["current"] = gvLabTestList.SelectedIndex;//index before insertion
            Session["count"] = gvLabTestList.Rows.Count;//row count before insertion
        //Add new Rows
            gvLabTestList.SelectedIndex = (Int32)(Session["current"]) + (gvLabTestList.Rows.Count - (Int32)(Session["count"]));
            Session["current"] = gvLabTestList.SelectedIndex;//restore the index into session
        }
    
 
    }
}