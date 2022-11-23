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
    public partial class NurseNotes : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {  
                Session["NurseNotesDataList"] = null;
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
                NurseNotesData objpat = new NurseNotesData();
                NurseNotesBO objBO = new NurseNotesBO();
                var source = txtpatientNames.Text.ToString();
                if (source.Contains(":"))
                {
                    string IPNo = source.Substring(source.LastIndexOf(':') + 1);
                    objpat.IPNo =(IPNo== "" ? "0" : IPNo);
                    
                }
                List<NurseNotesData> result = objBO.GetPatientDetailByID(objpat);
                if (result.Count > 0)
                {
                    txtage.Text = result[0].AgeCount.ToString();
                    txtsex.Text = result[0].Sex.ToString();
                    txtdoa.Text = result[0].AdmissionDate.ToString("dd/MM/yyyy hh:mm tt");
                    txtbedroom.Text = result[0].WardBedName.ToString();
                    txtipno.Text = result[0].IPNo.ToString();
                    bindgrid();
                }
                else
                {
                    clearall(); 
                } 
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetAdmittedPatientsDetails(string prefixText, int count, string contextKey)
        {
            NurseNotesData Objpaic = new NurseNotesData();
            NurseNotesBO objInfoBO = new NurseNotesBO();
            List<NurseNotesData> getResult = new List<NurseNotesData>();
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
            addrow();
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
                    divmsg1.Visible = false;
                }
                if (txtpatientNames.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "patientname", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                List<NurseNotesData> obj = GetNurseNotesList(0);
                if (obj.Count > 0)
                {
                    List<NurseNotesData> NurseNotesDataList = Session["NurseNotesDataList"] == null ? new List<NurseNotesData>() : (List<NurseNotesData>)Session["NurseNotesDataList"];
                    Session["NurseNotesDataList"] = obj;
                    lblresult.Visible = true;
                   
                    GVNurseNotes.DataSource = Session["OTSchedulistList"];
                    Messagealert_.ShowMessage(lblresult, "Total:" + obj[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    btnadd.Attributes.Remove("disabled");
                    GVNurseNotes.DataSource = obj;
                    GVNurseNotes.DataBind();
                    GVNurseNotes.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    txtdate.Text = "";
                    txttime.Text = "";
                    txtqrn.Text = "";
                    txtdate.ReadOnly = true;
                    txttime.ReadOnly = true;
                }
                else
                {
                    txtdate.Text = "";
                    txttime.Text = "";
                    txtqrn.Text = "";
                    txtdate.ReadOnly = true;
                    txttime.ReadOnly = true;
                    lblresult.Visible = false;
                    GVNurseNotes.DataSource = null;
                    GVNurseNotes.DataBind();
                    GVNurseNotes.Visible = true;
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
        private List<NurseNotesData> GetNurseNotesList(int p)
        {
            NurseNotesData objpat = new NurseNotesData();
            NurseNotesBO objBO = new NurseNotesBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objpat.IPNo = txtipno.Text.ToString();
            DateTime DateFrom = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime DateTo = txtdateto.Text.Trim() == "" ? GlobalConstant.MaxdateAddOneYear : DateTime.Parse(txtdateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.DateFrom = DateFrom;
            objpat.DateTo = DateTo;
            
            return objBO.GetNurseProgressSheet(objpat);

        }
        private void addrow()
        {
            List<NurseNotesData> NurseNotesDataList = Session["NurseNotesDataList"] == null ? new List<NurseNotesData>() : (List<NurseNotesData>)Session["NurseNotesDataList"];
            NurseNotesData ObjService = new NurseNotesData();
            NurseNotesBO objServiceBO = new NurseNotesBO();
            if (ViewState["ID"] != null)
            {
                if (LogData.UpdateEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                ObjService.ID = Convert.ToInt64(ViewState["ID"]);
                ObjService.ActionType = Enumaction.Update;
                ObjService.ID = Convert.ToInt64(ViewState["ID"].ToString() == "" ? "0" : ViewState["ID"].ToString());
                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                DateTime NoteDates = txtdate.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                ObjService.NotedDate = NoteDates;
                ObjService.NotedTimes = txttime.Text.ToString(); 
                ObjService.Particular = txtqrn.Text.ToString();
                int res = objServiceBO.UpdateNurseProgressSheet(ObjService);
                if (res == 2)
                {  
                    btnadd.Attributes["disabled"]="disabled";
                    txtqrn.Text = "";
                    GVNurseNotes.DataSource = NurseNotesDataList;
                    bindgrid();
                    GVNurseNotes.Visible = true;
                    Session["NurseNotesDataList"] = NurseNotesDataList;
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    divmsg1.Attributes["class"] = "SucessAlert";
                    divmsg1.Visible = true;

                }
                else
                {
                    btnadd.Attributes.Remove("disabled");
                    txtqrn.Text = "";
                    GVNurseNotes.DataSource = null;
                    GVNurseNotes.DataBind();
                    GVNurseNotes.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                }
            }
            else
            {
                string Name;
                var source = txtpatientNames.Text.ToString();
                if (source.Contains(":"))
                {
                    int indexStop = source.LastIndexOf('|') - 1;
                    Name = source.Substring(0, indexStop);
                    ObjService.PatientName = Name;
                }
                ObjService.IPNo = txtipno.Text.ToString();
                if (txtqrn.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Alert! QRN cann't be Empty", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    ObjService.Particular = txtqrn.Text.ToString();
                }
                ObjService.EmployeeID = LogData.EmployeeID;
                ObjService.HospitalID = LogData.HospitalID;
                ObjService.FinancialYearID = LogData.FinancialYearID;
                ObjService.ActionType = Enumaction.Insert;
                int results = objServiceBO.InsertNurseProgressSheet(ObjService);


                //NurseNotesDataList.Add(ObjService);
                if (results ==1)
                {
                   
                    btnadd.Attributes["disabled"] = "disabled";
                    txtqrn.Text = "";
                    GVNurseNotes.DataSource = NurseNotesDataList;
                    bindgrid();
                    GVNurseNotes.Visible = true;
                    Session["NurseNotesDataList"] = NurseNotesDataList;
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Attributes["class"] = "SucessAlert";
                    divmsg1.Visible = true;
                }
                else
                {
                    btnadd.Attributes.Remove("disabled");
                    txtqrn.Text = "";
                    GVNurseNotes.DataSource = null;
                    GVNurseNotes.DataBind();
                    GVNurseNotes.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                }
            }

        }
        public List<NurseNotesData> GetEditNurseProgressSheet(Int64 ID)
        {
            NurseNotesData objpat = new NurseNotesData();
            NurseNotesBO objpatBO = new NurseNotesBO();
            objpat.ID = ID;
            ViewState["ID"] = ID;
            return objpatBO.GetNurseProgressSheetByID(objpat);
        }
        protected void EditNurseProgressSheet(Int64 ID)
        {
            try
            {
                List<NurseNotesData> patientdetails = GetEditNurseProgressSheet(ID);
                if (patientdetails.Count > 0)
                {
                    txtdate.ReadOnly = false;
                    txttime.ReadOnly = false;
                    txtdate.Text = patientdetails[0].NotedDate.ToString("dd/MM/yyyy");
                    txttime.Text = String.Format("{0:hh:mm:ss tt}", patientdetails[0].NotedTime);
                    txtqrn.Text = patientdetails[0].Particular.ToString();
                    btnadd.Attributes.Remove("disabled");
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
            }
        }
        protected void GVNurseNotes_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GVNurseNotes.Rows[i];
                    Label PatID = (Label)gr.Cells[0].FindControl("lblID");
                    Int64 ID = Convert.ToInt64(PatID.Text);
                    EditNurseProgressSheet(ID); 
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
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GVNurseNotes.Rows[i];

                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    if (ID.Text == "0")
                    {
                        List<NurseNotesData> NurseNotesDataList = Session["NurseNotesDataList"] == null ? new List<NurseNotesData>() : (List<NurseNotesData>)Session["NurseNotesDataList"];
                        NurseNotesDataList.RemoveAt(i);
                        if (NurseNotesDataList.Count > 0)
                        {
                            Session["NurseNotesDataList"] = NurseNotesDataList;
                            GVNurseNotes.DataSource = NurseNotesDataList;
                            GVNurseNotes.DataBind();
                        }
                        else
                        {
                            Session["NurseNotesDataList"] = null;
                            GVNurseNotes.DataSource = null;
                            GVNurseNotes.DataBind();
                        }
                    }
                    else
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


                        NurseNotesData obj = new NurseNotesData();
                        NurseNotesBO objBO = new NurseNotesBO();

                        obj.ID = Convert.ToInt32(ID.Text);

                        obj.EmployeeID = LogData.EmployeeID;
                        int Result = objBO.DeleteNurseProgressSheet(obj);
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
        protected void btnresets_Click(object sender, System.EventArgs e)
        {
            clearall(); 
        }
        private void clearall()
        {
            lblmessage.Visible = false;
            lblresult.Visible = false;
            divmsg1.Visible = false;
            txtpatientNames.Text = "";
            txtipno.Text = "";
            txtbedroom.Text = "";
            txtage.Text = "";
            txtsex.Text = "";
            txtdoa.Text = "";
            txtdatefrom.Text = "";
            txtdateto.Text = "";
            txtdate.Text = "";
            txtqrn.Text = "";
            txttime.Text = "";
            GVNurseNotes.DataSource = null;
            GVNurseNotes.DataBind();
            GVNurseNotes.Visible = false;
            Session["NurseNotesDataList"] = null;
            btnexport.Visible = false;
            ddlexport.Visible = false;
            btnadd.Enabled = true;
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
                Response.AddHeader("content-disposition", "attachment;filename=NurseNotesDetails.xlsx");
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
            List<NurseNotesData> DepositDetails = GetNurseNotesList(0);
            List<NurseNotesDataTOeXCEL> ListexcelData = new List<NurseNotesDataTOeXCEL>();
            int i = 0;
            foreach (NurseNotesData row in DepositDetails)
            {
                NurseNotesDataTOeXCEL Ecxeclpat = new NurseNotesDataTOeXCEL();
                Ecxeclpat.IPNo = DepositDetails[i].IPNo.ToString();
                Ecxeclpat.PatientName = DepositDetails[i].PatientName.ToString();
                Ecxeclpat.Particular = DepositDetails[i].Particular.ToString();
                Ecxeclpat.NotedDate = DepositDetails[i].NotedDate.ToString();
                Ecxeclpat.NotedTime = DepositDetails[i].NotedTime.ToString();
                Ecxeclpat.AddedBy = DepositDetails[i].AddedBy.ToString();

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
        protected void btn_print_Click(object sender, EventArgs e)
        {
            NurseNotesData objData = new NurseNotesData();
            NurseNotesBO objBO = new NurseNotesBO();
            string IPNo = txtipno.Text == "" ? "" : txtipno.Text.Trim();
            string url = "../MedNurse/Reports/ReportViewer.aspx?option=NurseProgressSheet&Ipno=" + IPNo.ToString();
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }

    }
}