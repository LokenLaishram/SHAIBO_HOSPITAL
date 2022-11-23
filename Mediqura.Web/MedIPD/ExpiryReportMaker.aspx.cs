using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
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
using System.Text;
using System.Drawing;
using Mediqura.CommonData.AdmissionData;
using Mediqura.BOL.AdmissionBO;
using Mediqura.CommonData.MedEmergencyData;
using Mediqura.BOL.MedEmergencyBO;
using Mediqura.Utility;

namespace Mediqura.Web.MedIPD
{
    public partial class ExpiryReportMaker : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getReportTemplate();
                ViewState["ID"] = null;
            }
        }
        protected void ddl_patienttype_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddl_patienttype.SelectedIndex == 1)
            {
                txtIPNo.ReadOnly = false;
                txtEmergno.ReadOnly = true;
      
            }
            else if (ddl_patienttype.SelectedIndex == 2)
            {
                txtEmergno.ReadOnly = false;
                txtIPNo.ReadOnly = true;
     
            }
            else
            {
                txtIPNo.ReadOnly = true;
                txtEmergno.ReadOnly = true;
            }

        }
        protected void ddl_patient_type_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddl_patient_type.SelectedIndex == 1)
            {
                txt_emerno.ReadOnly = true; 
                txt_IPNo.ReadOnly = false;
            }
            else if (ddl_patient_type.SelectedIndex == 2)
            {
                txt_emerno.ReadOnly = false;
                txt_IPNo.ReadOnly = true;
         
            }
            else
            {
                txt_IPNo.ReadOnly = true;
                txt_emerno.ReadOnly = true;
            }
         

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPNo(string prefixText, int count, string contextKey)
        {
            IPData Objpaic = new IPData();
            AdmissionBO objInfoBO = new AdmissionBO();
            List<IPData> getResult = new List<IPData>();
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
        public static List<string> GetEmrgNo(string prefixText, int count, string contextKey)
        {
            EmrgAdmissionData Objpaic = new EmrgAdmissionData();
            EmrgAdmissionBO objInfoBO = new EmrgAdmissionBO();
            List<EmrgAdmissionData> getResult = new List<EmrgAdmissionData>();
            Objpaic.EmrgNo = prefixText;
            getResult = objInfoBO.GetEmrgNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmrgNo.ToString());
            }
            return list;
        }
        protected void txt_emerno_TextChanged(object sender, EventArgs e)
        {
            EmrgAdmissionData Objpaic = new EmrgAdmissionData();
            EmrgAdmissionBO objInfoBO = new EmrgAdmissionBO();
            List<EmrgAdmissionData> getResult = new List<EmrgAdmissionData>();
            Objpaic.EmrgNo = txt_emerno.Text.Trim() == "" ? "" : txt_emerno.Text.Trim();
            getResult = objInfoBO.GetPatientsDetailsByEmrgNo(Objpaic);
            if (getResult.Count > 0)
            {
                txt_name.Text = getResult[0].PatientName.ToString();
                txt_address.Text = getResult[0].Address.ToString();
                txt_gender.Text = getResult[0].GenderName.ToString();
                txt_age.Text = getResult[0].Age.ToString();
            }
            else
            {
                txt_name.Text = "";
                txt_address.Text = "";
                txt_gender.Text = "";
                txt_age.Text = "";
            }
            ExpiryReportMasterData objReportMaster = new ExpiryReportMasterData();
            ExpiryReportMasterBO objBO = new ExpiryReportMasterBO();
            objReportMaster.PatientType = Convert.ToInt32(ddl_patient_type.SelectedValue == "0" ? null : ddl_patient_type.SelectedValue);
            objReportMaster.EmergencyNo = txt_emerno.Text.Trim() == "" ? "" : txt_emerno.Text.Trim();
            List<ExpiryReportMasterData> objdata = objBO.GetPatientDetailsByID(objReportMaster);
            if (objdata.Count > 0)
            {
                if (objdata[0].Template == null)
                { txtReport.InnerText = null; }
                else
                {
                    txtReport.InnerHtml = generateTemplate(objdata[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">"), objdata[0]).Replace(@"&quot;", @"'").Replace(@"&amp;", @"&");
                }

            }
        }
        protected void txtEmergno_TextChanged(object sender, EventArgs e)
        {
            EmrgAdmissionData Objpaic = new EmrgAdmissionData();
            EmrgAdmissionBO objInfoBO = new EmrgAdmissionBO();
            List<EmrgAdmissionData> getResult = new List<EmrgAdmissionData>();
            Objpaic.EmrgNo = txtEmergno.Text.Trim() == "" ? "" : txtEmergno.Text.Trim();
            getResult = objInfoBO.GetPatientsDetailsByEmrgNo(Objpaic);
            if (getResult.Count > 0)
            {
                txtname1.Text = getResult[0].PatientName.ToString();
                txtaddress.Text = getResult[0].Address.ToString();
                txtgen.Text = getResult[0].GenderName.ToString();
                txt_Age1.Text = getResult[0].Age.ToString();
            }
            else
            {
                txtname1.Text = "";
                txtaddress.Text = "";
                txtgen.Text = "";
                txt_Age1.Text = "";
            }
          
        }
        protected void txt_IPNo_TextChanged(object sender, EventArgs e)
        {
            DischargeIntimationData Objpaic = new DischargeIntimationData();
            DischargeIntimationBO objInfoBO = new DischargeIntimationBO();
            List<DischargeIntimationData> getResult = new List<DischargeIntimationData>();
            Objpaic.IPNo = txt_IPNo.Text.Trim() == "" ? "0" : txt_IPNo.Text.Trim();
            getResult = objInfoBO.GetPatientAdmissionDetailsByIPNo(Objpaic);
            if (getResult.Count > 0)
            {
                txt_name.Text = getResult[0].PatientName.ToString();
                txt_address.Text = getResult[0].Address.ToString();
                txt_gender.Text = getResult[0].GenderName.ToString();
                txt_age.Text = getResult[0].Age.ToString();
                 }
            else
            {
                txt_name.Text = "";
                txt_address.Text = "";
                txt_IPNo.Text = "";
                txt_gender.Text = "";
                txt_age.Text = "";
                txt_IPNo.Focus();
            }

            ExpiryReportMasterData objReportMaster = new ExpiryReportMasterData();
            ExpiryReportMasterBO objBO = new ExpiryReportMasterBO();
            objReportMaster.PatientType =Convert.ToInt32(ddl_patient_type.SelectedValue == "0" ? null : ddl_patient_type.SelectedValue);
            objReportMaster.IPNo = txt_IPNo.Text.Trim() == "" ? "" : txt_IPNo.Text.Trim();
            List<ExpiryReportMasterData> objdata = objBO.GetPatientDetailsByID(objReportMaster);
            if (objdata.Count > 0)
            {
                if (objdata[0].Template == null)
                { txtReport.InnerText = null; }
                else
                {
                    txtReport.InnerHtml = generateTemplate(objdata[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">"), objdata[0]).Replace(@"&quot;", @"'").Replace(@"&amp;", @"&");
                }

            }

        }
        protected void gGvreportList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "DeleteEnable", 0);
                        div3.Visible = true;
                        div3.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage1.Visible = false;
                    }
                    ExpiryReportMasterData objdata = new ExpiryReportMasterData();
                    ExpiryReportMasterBO objBO = new ExpiryReportMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvreportList.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        div4.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objdata.Remarks = txtremarks.Text;
                    }

                    objdata.ID = Convert.ToInt64(ID.Text);
                    int Result = objBO.DeleteExpiryReport(objdata);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "delete", 1);
                        div3.Attributes["class"] = "SucessAlert";
                        div3.Visible = true;
                        bindgrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage1, "system", 0);
                        div3.Attributes["class"] = "FailAlert";
                        div3.Visible = true;
                    }

                }
                if (e.CommandName == "View")
                {
                    ExpiryReportMasterData objdata = new ExpiryReportMasterData();
                    ExpiryReportMasterBO objBO = new ExpiryReportMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvreportList.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    objdata.ID = Convert.ToInt64(ID.Text);
                    List<ExpiryReportMasterData> objdetails = objBO.GetExpiryReportDetails(objdata);
                    txtReport.InnerText = null;
                    if (objdetails.Count == 1)
                    {
                        txtReport.InnerHtml = objdetails[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&");
                        ViewState["ID"] = objdetails[0].ID.ToString();
                        tabexpiryrep.ActiveTabIndex = 0;
                        txt_IPNo.Text = objdetails[0].IPNo.ToString();
                        txt_emerno.Text = objdetails[0].EmergencyNo.ToString();
            
                    }
                    else
                    {
                        ViewState["ID"] = null;
                        txtReport.InnerText = null;
                    }
                  
                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
            }
        }
        protected void txtIPNo_TextChanged(object sender, EventArgs e)
        {
            DischargeIntimationData Objpaic = new DischargeIntimationData();
            DischargeIntimationBO objInfoBO = new DischargeIntimationBO();
            List<DischargeIntimationData> getResult = new List<DischargeIntimationData>();
            Objpaic.IPNo = txtIPNo.Text.Trim() == "" ? "0" : txtIPNo.Text.Trim();
            getResult = objInfoBO.GetPatientAdmissionDetailsByIPNo(Objpaic);
            if (getResult.Count > 0)
            {
                txtname1.Text = getResult[0].PatientName.ToString();
                txtaddress.Text = getResult[0].Address.ToString();
                txtgen.Text = getResult[0].GenderName.ToString();
                txt_Age1.Text = getResult[0].Age.ToString();
            }
            else
            {
                txtname1.Text = "";
                txtaddress.Text = "";
                txtIPNo.Text = "";
                txtgen.Text = "";
                txt_Age1.Text = "";
                txtIPNo.Focus();
            }
      

        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txt_IPNo.Text = "";
            ddl_patient_type.SelectedIndex = 0;
            txt_emerno.Text = "";
            txt_name.Text = "";
            txt_age.Text = "";
            txt_gender.Text = "";
            txt_address.Text = "";
            ddl_manner.SelectedIndex = 0;
            getReportTemplate();
            lblmessage.Visible = false;
            ViewState["ID"] = null;
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txtIPNo.Text = "";
            ddl_patienttype.SelectedIndex = 0;
            txtEmergno.Text = "";
            txtname1.Text = "";
            txt_Age1.Text = "";
            txtgen.Text = "";
            txtaddress.Text = "";
            ddl_manner.SelectedIndex = 0;
            txtdatefrom.Text = "";
            txtdateto.Text = "";
            GvreportList.DataSource = null;
            GvreportList.DataBind();
            GvreportList.Visible = false;
            lblresult.Visible = false;
            div4.Visible = false;
            lblmessage1.Visible = false;
            div3.Visible = false;
         
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

                if (ddl_patient_type.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "PatientType", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_IPNo.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "IPNo", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                ExpiryReportMasterData objdata = new ExpiryReportMasterData();
                ExpiryReportMasterBO objBO = new ExpiryReportMasterBO();

                objdata.IPNo = txt_IPNo.Text.Trim() == "" ? "" : txt_IPNo.Text.Trim();
                objdata.EmergencyNo = txt_emerno.Text.Trim() == "" ? "" : txt_emerno.Text.Trim();
                objdata.MannerID = Convert.ToInt32(ddl_manner.SelectedValue == "0" ? null : ddl_manner.SelectedValue);
                objdata.PatientType = Convert.ToInt32(ddl_patient_type.SelectedValue == "0" ? null : ddl_patient_type.SelectedValue);
         
                objdata.Template = txtReport.InnerHtml.ToString();
                objdata.EmployeeID = LogData.EmployeeID;
                objdata.HospitalID = LogData.HospitalID;
                objdata.FinancialYearID = LogData.FinancialYearID;
                objdata.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        objdata.ActionType = Enumaction.Update;
                        objdata.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                
                int result = objBO.UpdateExpiryReportMaker(objdata);
                if (result == 1 || result == 2)
                {
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    ViewState["ID"] = null;
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);

            }

        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage1, "SearchEnable", 0);
                div3.Visible = true;
                div3.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage1.Visible = false;
            }

            bindgrid();
        }
        private void bindgrid()
        {
            try
            {
                if (txtdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "ValidDatefrom", 0);
                        div3.Attributes["class"] = "FailAlert";
                        div3.Visible = true;
                        txtdatefrom.Focus();
                        return;
                    }
                }
                else
                {
                    div3.Visible = false;
                }

               
                if (txtdateto.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdateto.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "ValidDateto", 0);
                        div3.Attributes["class"] = "FailAlert";
                        div3.Visible = true;
                        txtdateto.Focus();
                        return;
                    }
                }
                else
                {
                    div3.Visible = false;
                }
                if (ddl_patienttype.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage1, "Please select patient type.", 0);
                    div3.Attributes["class"] = "FailAlert";
                    div3.Visible = true;
                    ddl_patienttype.Focus();
                    return;

                }
                else
                {
                    div3.Visible = false;
                }
                List<ExpiryReportMasterData> details = GetExpiryReportList(0);
                if (details.Count > 0)
                {
                    GvreportList.DataSource = details;
                    GvreportList.DataBind();
                    GvreportList.Visible = true;
                    div3.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + details[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    div4.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    div4.Visible = false;
                }
                else
                {
                    GvreportList.DataSource = null;
                    GvreportList.DataBind();
                    GvreportList.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    div4.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        private List<ExpiryReportMasterData> GetExpiryReportList(int p)
        {
            ExpiryReportMasterData objdata = new ExpiryReportMasterData();
            ExpiryReportMasterBO objBO = new ExpiryReportMasterBO();
            objdata.IPNo = txtIPNo.Text.Trim() == "" ? "" : txtIPNo.Text.Trim();
            objdata.EmergencyNo = txtEmergno.Text.Trim() == "" ? "" : txtEmergno.Text.Trim();
            objdata.PatientType = Convert.ToInt32(ddl_patienttype.SelectedValue == "0" ? null : ddl_patienttype.SelectedValue);
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtdateto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objdata.DateFrom = from;
            objdata.DateTo = To;
        
            return objBO.GetExpiryReportList(objdata);
       
        }
        public void getReportTemplate()
        {
            ExpiryReportMasterData objReportMaster = new ExpiryReportMasterData();
            ExpiryReportMasterBO objBO = new ExpiryReportMasterBO();
            List<ExpiryReportMasterData> objdata = objBO.GetExpiryTemplateByID(objReportMaster);
            if (objdata.Count > 0)
            {

                txtReport.InnerHtml = objdata[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&");
                ViewState["ID"] = objdata[0].ID.ToString(); ;
            }
            else
            {
                ViewState["ID"] = null;
                txtReport.InnerText = null;
            }
        }
        public string generateTemplate(string template, ExpiryReportMasterData objdata)
        {
            DateTime today = System.DateTime.Now;
        

           string header = "<li style=\"width: 183.576px;\">IPNo/Emerg.No:" + objdata.IPNo + "</li> " +
                                "<li style=\"width: 483.576px;\">Name:" + objdata.PatientName + "</li> " +
                                "<li style=\"width: 183.576px;\">Age/sex:" + objdata.AGE + "/" + objdata.GenderName + "</li> " +
                                "<li style=\"width: 483.576px;\">Address:" + objdata.Address + "</li> ";

         

            string Result = template.Replace("[header]", header);

            return Result;
        }
        protected void ExportoExcel()
        {

            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Department Type Detail List");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=ExpiryReportDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private DataTable GetDatafromDatabase()
        {
            List<ExpiryReportMasterData> ExpiryDetails = GetExpiryReportList(0);
            List<ExpiryDetailsDatatoExcel> ListexcelData = new List<ExpiryDetailsDatatoExcel>();
            int i = 0;
            foreach (ExpiryReportMasterData row in ExpiryDetails)
            {
                ExpiryDetailsDatatoExcel ExcelSevice = new ExpiryDetailsDatatoExcel();
                ExcelSevice.ID = ExpiryDetails[i].ID;
                ExcelSevice.IPNo = ExpiryDetails[i].IPNo;
                ExcelSevice.PatientName = ExpiryDetails[i].PatientName;
                ExcelSevice.Address = ExpiryDetails[i].Address;
                ExcelSevice.ReportOn = ExpiryDetails[i].AddedDate;
                ExcelSevice.ReportBy = ExpiryDetails[i].EmpName;
                GvreportList.Columns[6].Visible = false;
                GvreportList.Columns[7].Visible = false;
                GvreportList.Columns[8].Visible = false;
                ListexcelData.Add(ExcelSevice);
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
                Messagealert_.ShowMessage(lblmessage, "ExportEnable", 0);
                div3.Visible = true;
                div3.Attributes["class"] = "FailAlert";
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
            else if (ddlexport.SelectedIndex == 2)
            {
                ExportToPdf();
            }
            else
            {
                Messagealert_.ShowMessage(lblresult, "ExportType", 0);
                div3.Visible = true;
                div3.Attributes["class"] = "FailAlert";

                ddlexport.Focus();
                return;
            }
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvreportList.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvreportList.Columns[6].Visible = false;
                    GvreportList.Columns[7].Visible = false;
                    GvreportList.Columns[8].Visible = false;
                    GvreportList.RenderControl(hw);
                    GvreportList.HeaderRow.Style.Add("width", "15%");
                    GvreportList.HeaderRow.Style.Add("font-size", "10px");
                    GvreportList.Style.Add("text-decoration", "none");
                    GvreportList.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvreportList.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=ExpiryReportDetails.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
    }
}