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
using Mediqura.Web.MedCommon;
using Mediqura.CommonData.Common;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.BOL.CommonBO;
using Mediqura.CommonData.PatientData;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.MedBillData;
using Mediqura.Utility;
using Mediqura.BOL.MedOPDBO;

namespace Mediqura.Web.MedOPD
{
    public partial class PatientDiagnosisNote : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                ViewState["DiagnosisID"] = null;
                Session["ICDCodeList"] = null;
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.Department));
            Commonfunction.PopulateDdl(ddl_doctor, mstlookup.GetLookupsList(LookupName.Referal));
            ddl_doctor.Attributes["disabled"] = "disabled";
            ddldepartment.Attributes["disabled"] = "disabled";
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetAutoUHIDIPNo(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.IPNo = prefixText;
            getResult = objInfoBO.GetAutoUHIDIPNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPNo.ToString());
            }
            return list;
        }
        protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldepartment.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_doctor, mstlookup.GetIPDoctorBydepartmentID(Convert.ToInt32(ddldepartment.SelectedValue)));
            }
        }
        protected void txtUHID_TextChanged(object sender, EventArgs e)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.IPNo = txtUHID.Text.Trim() == "" ? "" : txtUHID.Text.Trim();
            getResult = objInfoBO.GetPatientAdmissionDetailsByUHIDIPNo(Objpaic);
            if (getResult.Count > 0)
            {
                // Clearall();
                txtname.Text = getResult[0].PatientName.ToString();
                txt_address.Text = getResult[0].Address.ToString();
                txt_gender.Text = getResult[0].GenderName.ToString();
                txt_age.Text = getResult[0].Agecount.ToString();
                ddldepartment.SelectedValue = getResult[0].DepartmentID.ToString();
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_doctor, mstlookup.GetIPDoctorBydepartmentID(Convert.ToInt32(ddldepartment.SelectedValue)));
                ddl_doctor.SelectedValue = getResult[0].DoctorID.ToString();

            }
            else
            {
                txtname.Text = "";
                txt_address.Text = "";
                txtUHID.Text = "";
                txt_gender.Text = "";
                txt_age.Text = "";
                txtUHID.Focus();
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetICDCode(string prefixText, int count, string contextKey)
        {
            ICDData Objpaic = new ICDData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<ICDData> getResult = new List<ICDData>();
            Objpaic.ICDCode = prefixText;
            getResult = objInfoBO.GetICDCode(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ICDCode.ToString());
            }
            return list;
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (txt_diagnosis.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Charge", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_diagnosis.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            string ID;
            var source = txt_diagnosis.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                // Check Duplicate data 
                foreach (GridViewRow row in GvICDCode.Rows)
                {
                    Label ServiceID = (Label)GvICDCode.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    if (Convert.ToInt32(ServiceID.Text) == Convert.ToInt32(ID))
                    {
                        Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_diagnosis.Text = "";
                        txt_diagnosis.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
            }
            else
            {
                txt_diagnosis.Text = "";
                return;
            }
            List<ICDData> ICDCodeList = Session["ICDCodeList"] == null ? new List<ICDData>() : (List<ICDData>)Session["ICDCodeList"];
            ICDData ObjService = new ICDData();
            ObjService.ICDCode = txt_diagnosis.Text.ToString() == "" ? "" : txt_diagnosis.Text.ToString();
            ObjService.ICDCodeID = Convert.ToInt32(ID);
            ICDCodeList.Add(ObjService);
            if (ICDCodeList.Count > 0)
            {
                GvICDCode.DataSource = ICDCodeList;
                GvICDCode.DataBind();
                GvICDCode.Visible = true;
                Session["ICDCodeList"] = ICDCodeList;
                txt_diagnosis.Text = "";
                txt_diagnosis.Focus();
                txt_diagnosis.ReadOnly = false;
                btnsave.Attributes.Remove("disabled");
            }
            else
            {
                GvICDCode.DataSource = null;
                GvICDCode.DataBind();
                GvICDCode.Visible = true;
                btnsave.Attributes["disabled"] = "disabled";
            }
        }
        protected void txtautoIPNo_TextChanged(object sender, EventArgs e)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.IPNo = txtautoIPNo.Text.Trim() == "" ? "" : txtautoIPNo.Text.Trim();
            getResult = objInfoBO.GetPatientAdmissionDetailsByUHIDIPNo(Objpaic);
            if (getResult.Count > 0)
            {
                // Clearall();
                txtpatientNames.Text = getResult[0].PatientName.ToString();
            }
            else
            {
                txtpatientNames.Text = "";
                txtautoIPNo.Focus();
            }
        }

        protected void GvICDCode_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvICDCode.Rows[i];
                    List<ICDData> ItemList = Session["ICDCodeList"] == null ? new List<ICDData>() : (List<ICDData>)Session["ICDCodeList"];
                    ItemList.RemoveAt(i);
                    Session["ICDCodeList"] = ItemList;
                    GvICDCode.DataSource = ItemList;
                    GvICDCode.DataBind();
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                lblmessage.Visible = true;
                lblmessage.CssClass = "Message";
            }
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txtUHID.Text = "";
            txtname.Text = "";
            txt_gender.Text = "";
            txt_age.Text = "";
            txt_diagnosis.Text = "";
            Session["ICDCodeList"] = null;
            GvICDCode.DataSource = null;
            GvICDCode.DataBind();
            GvICDCode.Visible = false;
            lblmessage.Visible = false;
            div1.Visible = false;
            MasterLookupBO mstlookup = new MasterLookupBO();
            ddl_doctor.SelectedIndex = 0;
            ddldepartment.SelectedIndex = 0;
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
        protected void btnsave_Click(object sender, EventArgs e)
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
            if (txtUHID.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Please enter UHID/IPNo.", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtUHID.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }

            List<ICDData> Listser = new List<ICDData>();
            PatientDiagnosisBO objcBO = new PatientDiagnosisBO();
            ICDData objrec = new ICDData();
            int count = 0;
            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in GvICDCode.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label Code = (Label)GvICDCode.Rows[row.RowIndex].Cells[0].FindControl("lbl_code");
                    Label ID = (Label)GvICDCode.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    ICDData ObjDetails = new ICDData();
                    ObjDetails.ICDCodeID = Convert.ToInt32(ID.Text);
                    ObjDetails.ICDCode = Code.Text;
                    count++;
                    Listser.Add(ObjDetails);
                }
                objrec.XMLData = XmlConvertor.ICDCodeRecordDatatoXML(Listser).ToString();
                objrec.IPNo = txtUHID.Text == "" ? "" : txtUHID.Text;
                objrec.Remarks = txt_Reamrks.Text == "" ? "" : txt_Reamrks.Text;
                objrec.Name = txtname.Text == "" ? "" : txtname.Text;
                objrec.FinancialYearID = LogData.FinancialYearID;
                objrec.EmployeeID = LogData.EmployeeID;
                objrec.HospitalID = LogData.HospitalID;
                objrec.IPaddress = LogData.IPaddress;
                objrec.ActionType = Enumaction.Insert;
                if (count > 0)
                {
                    if (ViewState["DiagnosisID"] != null)
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
                            objrec.ActionType = Enumaction.Update;
                            objrec.DiagnosisID = Convert.ToInt32(ViewState["DiagnosisID"].ToString());
                        }
                    }
                }
                int result = objcBO.UpdateICDCodeList(objrec);
                if (result == 1 || result == 2)
                {
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    GvICDCode.DataSource = null;
                    GvICDCode.DataBind();
                    GvICDCode.Visible = false;
                    Session["ICDCodeList"] = null;
                    txtUHID.Text = "";
                    btnsave.Attributes["disabled"] = "disabled";
                    ViewState["DiagnosisID"] = null;
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    btnsave.Attributes.Remove("disabled");
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
                    Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }

                if (txtautoIPNo.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "Please enter UHID/IPNo.", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    txtautoIPNo.Focus();
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                    divmsg2.Visible = false;
                }

                if (txtdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDatefrom", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txtdatefrom.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                if (txtto.Text != "")
                {
                    if (Commonfunction.isValidDate(txtto.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDateto", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txtto.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                List<ICDData> objdeposit = GetPatientDiagnosisDetails(0);
                if (objdeposit.Count > 0)
                {
                    gvpatientdiagnosislist.DataSource = objdeposit;
                    gvpatientdiagnosislist.DataBind();
                    gvpatientdiagnosislist.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    gvpatientdiagnosislist.DataSource = null;
                    gvpatientdiagnosislist.DataBind();
                    gvpatientdiagnosislist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        public List<ICDData> GetPatientDiagnosisDetails(int curIndex)
        {
            ICDData objpat = new ICDData();
            PatientDiagnosisBO objcBO = new PatientDiagnosisBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.IPNo = txtautoIPNo.Text.Trim() == "" ? "" : txtautoIPNo.Text.Trim();
            objpat.Name = txtpatientNames.Text.Trim() == "" ? "" : txtpatientNames.Text.Trim();
            objpat.DateFrom = from;
            objpat.DateTo = To;
            return objcBO.GetPatientDiagnosisDetails(objpat);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtautoIPNo.Text = "";
            txtpatientNames.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            gvpatientdiagnosislist.DataSource = null;
            gvpatientdiagnosislist.DataBind();
            gvpatientdiagnosislist.Visible = false;
            lblresult.Visible = false;
            lblmessage2.Visible = false;
        }
        protected void gvpatientdiagnosislist_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    ICDData objDepartmentTypeMasterData = new ICDData();
                    PatientDiagnosisBO objDepartmentTypeMasterBO = new PatientDiagnosisBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvpatientdiagnosislist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    objDepartmentTypeMasterData.DiagnosisID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                    objDepartmentTypeMasterData.ActionType = Enumaction.Select;

                    List<ICDData> GetResult = objDepartmentTypeMasterBO.GetPatientDiagnosisDetailsByID(objDepartmentTypeMasterData);
                    if (GetResult.Count > 0)
                    {
                        txtUHID.Text = GetResult[0].IPNo;
                        PatientData Objpaic = new PatientData();
                        RegistrationBO objInfoBO = new RegistrationBO();
                        List<PatientData> getResult1 = new List<PatientData>();
                        Objpaic.IPNo = txtUHID.Text.Trim() == "" ? "" : txtUHID.Text.Trim();
                        getResult1 = objInfoBO.GetPatientAdmissionDetailsByUHIDIPNo(Objpaic);
                        if (getResult1.Count > 0)
                        {
                            // Clearall();
                            txtname.Text = getResult1[0].PatientName.ToString();
                            txt_address.Text = getResult1[0].Address.ToString();
                            txt_gender.Text = getResult1[0].GenderName.ToString();
                            txt_age.Text = getResult1[0].Agecount.ToString();
                            ddldepartment.SelectedValue = getResult1[0].DepartmentID.ToString();
                            MasterLookupBO mstlookup = new MasterLookupBO();
                            Commonfunction.PopulateDdl(ddl_doctor, mstlookup.GetIPDoctorBydepartmentID(Convert.ToInt32(ddldepartment.SelectedValue)));
                            ddl_doctor.SelectedValue = getResult1[0].DoctorID.ToString();

                        }
                        else
                        {
                            txtname.Text = "";
                            txt_address.Text = "";
                            txtUHID.Text = "";
                            txt_gender.Text = "";
                            txt_age.Text = "";
                            txtUHID.Focus();
                        }
                        GvICDCode.DataSource = GetResult;
                        GvICDCode.DataBind();
                        GvICDCode.Visible = true;
                        List<ICDData> ItemList = Session["ICDCodeList"] == null ? new List<ICDData>() : (List<ICDData>)Session["ICDCodeList"];
                        Session["ICDCodeList"] = GetResult;
                        ViewState["DiagnosisID"] = GetResult[0].DiagnosisID;

                        tabcontainerpatientdiagnosis.ActiveTabIndex = 0;
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
                    ICDData objadmin = new ICDData();
                    PatientDiagnosisBO obadminBO = new PatientDiagnosisBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvpatientdiagnosislist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objadmin.Remarks = txtremarks.Text;
                    }

                    objadmin.DiagnosisID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                    objadmin.EmployeeID = LogData.EmployeeID;
                    objadmin.HospitalID = LogData.HospitalID;
                    objadmin.IPaddress = LogData.IPaddress;

                    int Result = obadminBO.DeletePatientDiagnosisDetails(objadmin);
                    if (Result == 1)
                    {
                        bindgrid();
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        divmsg2.Attributes["class"] = "SucessAlert";
                        divmsg2.Visible = true;

                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage2, "system", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
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
        protected DataTable GetDatafromDatabase()
        {
            List<ICDData> ServiceDetails = GetPatientDiagnosisDetails(0);
            List<ICDDataDatatoExcel> ListexcelData = new List<ICDDataDatatoExcel>();
            int i = 0;
            foreach (ICDData row in ServiceDetails)
            {
                ICDDataDatatoExcel Ecxeclpat = new ICDDataDatatoExcel();
                Ecxeclpat.DiagnosisID = Convert.ToInt64(ServiceDetails[i].DiagnosisID.ToString());
                Ecxeclpat.IPNo = ServiceDetails[i].IPNo.ToString();
                Ecxeclpat.Name = ServiceDetails[i].Name.ToString();
                Ecxeclpat.Remarks = ServiceDetails[i].Remarks.ToString();
                Ecxeclpat.EmpName = ServiceDetails[i].EmpName.ToString();
                Ecxeclpat.AddedDate = Convert.ToDateTime(ServiceDetails[i].AddedDate.ToString());
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
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "ExportEnable", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage2.Visible = false;
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
                divmsg3.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        public void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    gvpatientdiagnosislist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvpatientdiagnosislist.RenderControl(hw);
                    gvpatientdiagnosislist.HeaderRow.Style.Add("width", "15%");
                    gvpatientdiagnosislist.HeaderRow.Style.Add("font-size", "10px");
                    gvpatientdiagnosislist.Style.Add("text-decoration", "none");
                    gvpatientdiagnosislist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvpatientdiagnosislist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=PatientDiagnosisDetails.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Patient Diagnosis Report");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=PatientDiagnosisDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblresult, "Exported", 1);
                divmsg3.Attributes["class"] = "SucessAlert";
            }
        }
        protected void gvpatientdiagnosislist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvpatientdiagnosislist.PageIndex = e.NewPageIndex;
            bindgrid();
        }

    }
}