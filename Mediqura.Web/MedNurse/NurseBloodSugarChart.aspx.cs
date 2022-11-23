using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedNurseBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedNurseData;
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
using Mediqura.CommonData.PatientData;
using Mediqura.BOL.PatientBO;
using Mediqura.Utility;


namespace Mediqura.Web.MedNurse
{
    public partial class NurseBloodSugarChart : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetInitialRow();
                AddNewRowToGrid();
             
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPNo(string prefixText, int count, string contextKey)
        {
            IPData Objpaic = new IPData();
            AdmissionBO objInfoBO = new AdmissionBO();
            List<IPData> getResult = new List<IPData>();
            Objpaic.IPNo = prefixText;
            getResult = objInfoBO.getIPNoByBedID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPNo.ToString());
            }
            return list;
        }
        protected void txtautoIPNo_TextChanged(object sender, EventArgs e)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.IPNo = txtautoIPNo.Text.Trim() == "" ? null : txtautoIPNo.Text.Trim();
            getResult = objInfoBO.GetPatientDetailsByIPNo(Objpaic);
            if (getResult.Count > 0)
            {
                txt_patientNames.Text = getResult[0].PatientName.ToString() + " | Sex :" + getResult[0].Gender.ToString();
                txt_address.Text = getResult[0].Address.ToString();
                txt_age.Text = getResult[0].Agecount.ToString();
                txt_Doc.Text = getResult[0].DoctorName.ToString();

            }
            else
            {
                txt_patientNames.Text = "";
                txt_address.Text = "";
                txt_age.Text = "";
                txt_Doc.Text = "";

            }
        }
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("ID", typeof(Int64)));
            dt.Columns.Add(new DataColumn("RecordDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("RBSmgDl", typeof(string)));
            dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
            dt.Columns.Add(new DataColumn("Signature", typeof(string)));
           //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;

            gvSugarChart.DataSource = dt;
            gvSugarChart.DataBind();

        }
        private void AddNewRowToGrid()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add("ID");
                dt.Columns.Add("RecordDate");
                dt.Columns.Add("RBSmgDl");
                dt.Columns.Add("Remarks");
                dt.Columns.Add("Signature");
               

                foreach (GridViewRow gvRow in gvSugarChart.Rows)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["ID"] = ((Label)gvRow.FindControl("lblID")).Text;
                    dr1["RecordDate"] = ((TextBox)gvRow.FindControl("txtdate")).Text;
                    dr1["RBSmgDl"] = ((TextBox)gvRow.FindControl("txtRbs")).Text;
                    dr1["Remarks"] = ((TextBox)gvRow.FindControl("txt_remarks")).Text;
                    //dr1["Signature"] = ((TextBox)gvRow.FindControl("txt_signature")).Text;
                   

                    dt.Rows.Add(dr1);
                }
                DataRow dr2 = dt.NewRow();
                dr2["ID"] = "";
                dr2["RecordDate"] = System.DateTime.Now.ToString("dd/MM/yyyy");
                dr2["RBSmgDl"] = "";
                dr2["Remarks"] = "";
                //dr2["Signature"] = "";
                dt.Rows.Add(dr2);

                gvSugarChart.DataSource = dt;
                gvSugarChart.DataBind();

            }
            else
            {
                SetInitialRow();

            }

        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            gvSugarChart.DataSource = null;
            gvSugarChart.DataBind();
            gvSugarChart.Visible = true;
            lblmessage.Visible = false;
            divmsg1.Visible = false;
            txtautoIPNo.Text = "";
            txt_patientNames.Text = "";
            txt_age.Text = "";
            txt_Doc.Text = "";
            txt_address.Text = "";
            SetInitialRow();
            AddNewRowToGrid();
        }
        protected void btnsearch_Click(object sender, EventArgs e)
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
            bindgrid();
        }

        protected void gvSugarChart_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    PatientSugarChartData objData = new PatientSugarChartData();
                    PatientSugarChartBO objBO = new PatientSugarChartBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvSugarChart.Rows[i];

                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                   
                    if (ID.Text != "")
                    {
                        objData.ID = Convert.ToInt32(ID.Text);
                        objData.EmployeeID = LogData.EmployeeID;
                        int Result = objBO.CancelSugarDetails(objData);
                        if (Result == 1)
                        {
                            bindgrid();
                            AddNewRowToGrid();

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
                    else
                    {
                        //GridViewRow currentrow = (GridViewRow)((LinkButton)sender).Parent.Parent;
                        //int rowIndex = currentrow.RowIndex;
                        gvSugarChart.DeleteRow(i);
                        
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

        protected void gvSugarChart_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox rbs = (TextBox)e.Row.FindControl("txtRbs");
                Label ID = (Label)e.Row.FindControl("lblID");
                if (ID.Text == "0")
                {
                    rbs.Focus();
                }

               
            }
        }
        protected void txt_remarks_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            GridViewRow currentrow = (GridViewRow)((TextBox)sender).Parent.Parent;
            int rowIndex = currentrow.RowIndex;
            PatientSugarChartData objData = new PatientSugarChartData();
            PatientSugarChartBO objBO = new PatientSugarChartBO();
            foreach (GridViewRow row in gvSugarChart.Rows)
            {
                TextBox box1 = (TextBox)gvSugarChart.Rows[rowIndex].Cells[1].FindControl("txtdate");
                TextBox box2 = (TextBox)gvSugarChart.Rows[rowIndex].Cells[2].FindControl("txtRbs");
                TextBox box3 = (TextBox)gvSugarChart.Rows[rowIndex].Cells[3].FindControl("txt_remarks");
                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                DateTime AddedDate = box1.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(box1.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                objData.AddedDate = AddedDate;
                objData.RBSmgDl = box2.Text;
                objData.remarks = box3.Text;
               
            }
            objData.Ipno = txtautoIPNo.Text == "" ? "" : txtautoIPNo.Text.Trim();
            objData.FinancialYearID = LogData.FinancialYearID;
            objData.HospitalID = LogData.HospitalID;
            objData.EmployeeID = LogData.EmployeeID;
            objData.IPaddress = LogData.IPaddress;

            int results = objBO.UpdatePatientSugar(objData);
            if (results > 0)
            {
                lblmessage.Visible = true;
                Messagealert_.ShowMessage(lblmessage, "save", 1);
                divmsg1.Attributes["class"] = "SucessAlert";
                divmsg1.Visible = true;
                gvSugarChart.DataSource = null;
                gvSugarChart.DataBind();
                gvSugarChart.Visible = true;

                bindgrid();
                //AddNewRowToGrid();
            }
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
                if (txtautoIPNo.Text=="")
                {
                    Messagealert_.ShowMessage(lblmessage, "DoctorType", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txtautoIPNo.Focus();
                    return;

                }
                else
                {
                    divmsg1.Visible = false;
                }

                List<PatientSugarChartData> obj = GetSugarDetailByIPNo(0);
                if (obj.Count > 0)
                {

                    gvSugarChart.DataSource = obj;
                    gvSugarChart.DataBind();
                    gvSugarChart.Visible = true;


                }
                else
                {
                    gvSugarChart.DataSource = null;
                    gvSugarChart.DataBind();
                    gvSugarChart.Visible = true;
                }
                //SetInitialRow();
                AddNewRowToGrid();

            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }
        private List<PatientSugarChartData> GetSugarDetailByIPNo(int p)
        {
            PatientSugarChartData objData = new PatientSugarChartData();
            PatientSugarChartBO objBO = new PatientSugarChartBO();
            objData.Ipno = txtautoIPNo.Text == "" ? "" : txtautoIPNo.Text.Trim();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            return objBO.GetSugarDetailByIPNo(objData);

        }

        protected void gvSugarChart_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            bindgrid();
        }
        protected void btn_print_Click(object sender, EventArgs e)
        {
            CurrentAdmissionListData objData = new CurrentAdmissionListData();
            CurrentAdmissionListBO objBO = new CurrentAdmissionListBO();
            string IPNo = txtautoIPNo.Text == "" ? "" : txtautoIPNo.Text.Trim();
            string url = "../MedNurse/Reports/ReportViewer.aspx?option=SugarChart&Ipno=" + IPNo.ToString();
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
    }
}