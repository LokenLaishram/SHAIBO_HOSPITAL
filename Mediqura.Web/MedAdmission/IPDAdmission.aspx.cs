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


namespace Mediqura.Web.MedAdmission
{
    public partial class IPDAdmission : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                bindbedlist();
                hdnemrgnumber.Value = null;
                if (Session["Emg_UHID"] != null && Session["EmgNumber"] != null)
                {
                    txt_UHID.ReadOnly = true;
                    txt_UHID.Text = Session["Emg_UHID"].ToString();
                    hdnemrgnumber.Value = Session["EmgNumber"].ToString();
                    Int64 UHID = Convert.ToInt64(Session["Emg_UHID"].ToString() == "" ? "0" : Session["Emg_UHID"].ToString());
                    bindpatientdetails(UHID);
                    Session["Emg_UHID"] = null;
                    Session["EmgNumber"] = null;
                }
                else
                {
                    txt_UHID.ReadOnly = false;
                    hdnemrgnumber.Value = null;
                }
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.OPDepartment));
            Commonfunction.PopulateDdl(ddl_seconddoctor, mstlookup.GetLookupsList(LookupName.Doctor));
            Commonfunction.PopulateDdl(ddl_thirddoctor, mstlookup.GetLookupsList(LookupName.Doctor));
            Commonfunction.PopulateDdl(ddl_block, mstlookup.GetLookupsList(LookupName.BlockType));
            Commonfunction.PopulateDdl(ddl_floor, mstlookup.GetLookupsList(LookupName.FloorType));
            Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetLookupsList(LookupName.IPDWardType));
            Commonfunction.PopulateDdl(ddldeischargestatus, mstlookup.GetLookupsList(LookupName.DischargeStatus));
            btnprint.Attributes["disabled"] = "disabled";
            btnsave.Attributes["disabled"] = "disabled";
            btnprints.Attributes["disabled"] = "disabled";
            Commonfunction.Insertzeroitemindex(ddl_doctor);
            Commonfunction.Insertzeroitemindex(ddl_referal);
            Disableward();
        }
        private void Disableward()
        {
            foreach (System.Web.UI.WebControls.ListItem item in ddl_ward.Items)
            {
                if (item.Value == "10")
                {
                    item.Attributes.Add("disabled", "disabled");
                }
                if (item.Value == "11")
                {
                    item.Attributes.Add("disabled", "disabled");
                }
                if (item.Value == "12")
                {
                    item.Attributes.Add("disabled", "disabled");
                }
                if (item.Value == "13")
                {
                    item.Attributes.Add("disabled", "disabled");
                }
                if (item.Value == "14")
                {
                    item.Attributes.Add("disabled", "disabled");
                }

            }
        }
        protected void ddl_block_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            MasterLookupBO mstlookup = new MasterLookupBO();
            if (ddl_block.SelectedIndex > 0)
            {

                Commonfunction.PopulateDdl(ddl_floor, mstlookup.GetfloorByblockID(Convert.ToInt32(ddl_block.SelectedValue)));
            }
            else
            {
                Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetLookupsList(LookupName.WardType));
            }
            Disableward();
        }
        protected void ddl_floor_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            MasterLookupBO mstlookup = new MasterLookupBO();
            if (ddl_floor.SelectedIndex > 0)
            {

                Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetIPDWardByFloorID(Convert.ToInt32(ddl_floor.SelectedValue)));
            }
            else
            {
                Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetLookupsList(LookupName.WardType));
            }
            Disableward();
        }
        protected void ddl_ward_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_ward.SelectedIndex > 0)
            {
                bindbedlist();
            }
        }
        protected void bindbedlist()
        {
            List<AdmissionData> objdeposit = GetBedList(0);
            if (objdeposit.Count > 0)
            {
                GvBedAssign.DataSource = objdeposit;
                GvBedAssign.DataBind();
                GvBedAssign.Visible = true;
            }
            else
            {
                GvBedAssign.DataSource = null;
                GvBedAssign.DataBind();
                GvBedAssign.Visible = true;
                lblresult.Visible = false;
                div3.Visible = false;
            }
        }
        private List<AdmissionData> GetBedList(int p)
        {
            AdmissionData objpat = new AdmissionData();
            AdmissionBO objbillingBO = new AdmissionBO();
            objpat.BlockID = Convert.ToInt16(ddl_block.SelectedValue == "0" ? null : ddl_block.SelectedValue);
            objpat.FloorID = Convert.ToInt16(ddl_floor.SelectedValue == "0" ? null : ddl_floor.SelectedValue);
            objpat.WardID = Convert.ToInt16(ddl_ward.SelectedValue == "0" ? null : ddl_ward.SelectedValue);
            objpat.BedStatus = Convert.ToInt16(ddl_status.SelectedValue == "0" ? null : ddl_status.SelectedValue);
            return objbillingBO.GetIPDBedList(objpat);
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
        protected void txt_UHID_TextChanged(object sender, EventArgs e)
        {
            Int64 UHID = Convert.ToInt64(txt_UHID.Text.Trim() == "" ? "0" : txt_UHID.Text.Trim());
            bindpatientdetails(UHID);
        }
        protected void ddl_source_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_source.SelectedIndex > 0)
            {
                ddl_referal.Attributes.Remove("disabled");
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_referal, mstlookup.GetReferalBySourceID(Convert.ToInt32(ddl_source.SelectedValue == "" ? "0" : ddl_source.SelectedValue)));
                if (ddl_source.SelectedValue == "1")
                {
                    ddl_referal.SelectedIndex = 1;
                }
                else
                {
                    ddl_referal.SelectedIndex = 0;
                }
            }
            else
            {
                ddl_referal.Attributes["disabled"] = "disabled";
                ddl_referal.SelectedIndex = 0;
            }

        }
        protected void bindpatientdetails(Int64 UHID)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.UHID = UHID;
            Objpaic.EmrgNo = hdnemrgnumber.Value == null ? "" : hdnemrgnumber.Value;
            getResult = objInfoBO.GetPatientAdmissionDetailsByUHID(Objpaic);
            if (getResult.Count > 0)
            {
                txt_name.Text = getResult[0].PatientName.ToString();
                txt_address.Text = getResult[0].Address.ToString();
                txt_gender.Text = getResult[0].GenderName.ToString();
                txt_age.Text = getResult[0].Agecount.ToString();
                txt_contactnumber.Text = getResult[0].ContactNo.ToString();
                btnsave.Attributes.Remove("disabled");
            }
            else
            {
                txt_name.Text = "";
                txt_address.Text = "";
                txt_UHID.Text = "";
                txt_gender.Text = "";
                txt_age.Text = "";
                txt_contactnumber.Text = "";
                btnsave.Attributes["disabled"] = "disabled";
                txt_UHID.Focus();
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetDoctorName(string prefixText, int count, string contextKey)
        {
            EmployeeData Objpaic = new EmployeeData();
            EmployeeBO objInfoBO = new EmployeeBO();
            List<EmployeeData> getResult = new List<EmployeeData>();
            Objpaic.EmpName = prefixText;
            getResult = objInfoBO.GetEmployeeName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmpName.ToString());
            }
            return list;
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
        public static List<string> GetIPPatientName(string prefixText, int count, string contextKey)
        {
            IPData Objpaic = new IPData();
            AdmissionBO objInfoBO = new AdmissionBO();
            List<IPData> getResult = new List<IPData>();
            Objpaic.IPPatientName = prefixText;
            getResult = objInfoBO.GetIPPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPPatientName.ToString());
            }
            return list;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (ViewState["ID"] != null)
            {
                if (LogData.UpdateEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_UHID.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_UHID.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "UHID", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_UHID.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }

                if (ddldepartment.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Department", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddldepartment.Focus();
                    return;
                }
                if (ddl_doctor.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "AdmissionDoctor", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_doctor.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }

                if (txt_case.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Case", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_case.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                string Ipno = ViewState["ID"].ToString();

                AdmissionData objData = new AdmissionData();
                AdmissionBO objBO = new AdmissionBO();
                objData.DocID = Convert.ToInt64(ddl_doctor.SelectedValue == "" ? "0" : ddl_doctor.SelectedValue);
                objData.AdmissionDoc_II = Convert.ToInt64(ddl_seconddoctor.SelectedValue == "" ? "0" : ddl_seconddoctor.SelectedValue);
                objData.AdmissionDoc_III = Convert.ToInt64(ddl_thirddoctor.SelectedValue == "" ? "0" : ddl_thirddoctor.SelectedValue);
                objData.DeptID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
                objData.ReferalDoctorName = txt_referal.Text.ToString() == "" ? "" : txt_referal.Text.ToString();
                objData.ActionType = Enumaction.Update;
                objData.Cases = txt_case.Text == "" ? null : txt_case.Text;
                objData.FinancialYearID = LogData.FinancialYearID;
                objData.EmployeeID = LogData.EmployeeID;
                objData.HospitalID = LogData.HospitalID;
                objData.IPaddress = LogData.IPaddress;
                objData.IPNo = Ipno;
                int result = objBO.UpdateIPadmission(objData);
                if (result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    ViewState["ID"] = null;

                }
            }
            else
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
                if (txt_UHID.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "UHID", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_UHID.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }

                if (ddldepartment.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Department", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddldepartment.Focus();
                    return;
                }
                if (ddl_doctor.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "AdmissionDoctor", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_doctor.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }

                if (txt_case.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Case", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_case.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }

                if (ddl_ward.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Ward", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_ward.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (ddl_housekeepingstatus.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "informhk", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_housekeepingstatus.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (ddl_wardstatus.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "informward", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_wardstatus.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (ddl_source.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Source", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_source.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (ddl_referal.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "ReferBy", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_referal.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }

                List<AdmissionData> Listadm = new List<AdmissionData>();
                AdmissionData objdata = new AdmissionData();
                AdmissionBO objadmissionBO = new AdmissionBO();
                try
                {
                    // get all the record from the gridview
                    int countbed = 0;
                    foreach (GridViewRow row in GvBedAssign.Rows)
                    {
                        CheckBox cb = (CheckBox)GvBedAssign.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                        if (cb != null)
                        {
                            if (cb.Checked)
                            {
                                IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                                Label Room = (Label)GvBedAssign.Rows[row.RowIndex].Cells[0].FindControl("lbl_room");
                                Label bedno = (Label)GvBedAssign.Rows[row.RowIndex].Cells[0].FindControl("lbl_bedno");
                                Label charges = (Label)GvBedAssign.Rows[row.RowIndex].Cells[0].FindControl("lbl_charges");
                                Label ID = (Label)GvBedAssign.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                                AdmissionData ObjDetails = new AdmissionData();
                                CheckBox Checkbed = (CheckBox)GvBedAssign.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect");
                                if (Checkbed.Checked == true)
                                {
                                    ObjDetails.Room = Room.Text == "" ? null : Room.Text;
                                    ObjDetails.BedNo = bedno.Text == "" ? "0" : bedno.Text;
                                    ObjDetails.Charges = Convert.ToDecimal(charges.Text == "" ? "0" : charges.Text);
                                    ObjDetails.BedID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                                    countbed = countbed + 1;
                                    Listadm.Add(ObjDetails);
                                }
                            }
                        }

                    }
                    if (countbed == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Bedno", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        ddl_doctor.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    if (countbed > 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Admbedcount", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        ddl_doctor.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    objdata.XMLData = XmlConvertor.AdmBedDatatoXML(Listadm).ToString();
                    objdata.UHID = Convert.ToInt32(txt_UHID.Text.Trim() == "" ? "0" : txt_UHID.Text.Trim());
                    objdata.EmrgNo = hdnemrgnumber.Value == null ? "" : hdnemrgnumber.Value;
                    objdata.DocID = Convert.ToInt64(ddl_doctor.SelectedValue == "" ? "0" : ddl_doctor.SelectedValue);
                    objdata.AdmissionDoc_II = Convert.ToInt64(ddl_seconddoctor.SelectedValue == "" ? "0" : ddl_seconddoctor.SelectedValue);
                    objdata.AdmissionDoc_III = Convert.ToInt64(ddl_thirddoctor.SelectedValue == "" ? "0" : ddl_thirddoctor.SelectedValue);
                    objdata.DeptID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
                    objdata.ReferalDoctorName = txt_referal.Text;
                    objdata.BlockID = Convert.ToInt16(ddl_block.SelectedValue == "0" ? null : ddl_block.SelectedValue);
                    objdata.FloorID = Convert.ToInt16(ddl_floor.SelectedValue == "0" ? null : ddl_floor.SelectedValue);
                    objdata.WardID = Convert.ToInt16(ddl_ward.SelectedValue == "0" ? null : ddl_ward.SelectedValue);
                    objdata.Informeward = Convert.ToInt32(ddl_wardstatus.SelectedValue == "" ? "0" : ddl_wardstatus.SelectedValue);
                    objdata.Informehk = Convert.ToInt32(ddl_housekeepingstatus.SelectedValue == "" ? "0" : ddl_housekeepingstatus.SelectedValue);
                    objdata.Cases = txt_case.Text == "" ? null : txt_case.Text;
                    objdata.SourceID = Convert.ToInt32(ddl_source.SelectedValue == "0" ? "0" : ddl_source.SelectedValue);
                    objdata.ReferalID = Convert.ToInt64(ddl_referal.SelectedValue == "0" ? "0" : ddl_referal.SelectedValue);
                    objdata.FinancialYearID = LogData.FinancialYearID;
                    objdata.EmployeeID = LogData.EmployeeID;
                    objdata.HospitalID = LogData.HospitalID;
                    objdata.IPaddress = LogData.IPaddress;
                    objdata.ActionType = Enumaction.Insert;
                    Listadm = objadmissionBO.UpdateIPDAdmissionDetails(objdata);
                    if (Listadm[0].AdmissionNo != "5")
                    {
                        txt_AdmissionNo.Text = Listadm[0].AdmissionNo.ToString();
                        Messagealert_.ShowMessage(lblmessage, "Admitted", 1);
                        div1.Visible = true;
                        div1.Attributes["class"] = "SucessAlert";
                        btnprint.Attributes.Remove("disabled");

                        btnsave.Attributes["disabled"] = "disabled";

                        AdmissionData objData = new AdmissionData();
                        AdmissionBO objBO = new AdmissionBO();
                        OPDbillingBO objbillingBO = new OPDbillingBO();
                        objData.BarcodeImage = Commonfunction.getBarcodeImage(Listadm[0].AdmissionNo.ToString());
                        //QR image generation//
                        List<PatientQRdata> listQr = objbillingBO.GetPatientQRData(Convert.ToInt64(txt_UHID.Text));
                        PatientQRdata qrData = new PatientQRdata();
                        qrData = listQr[0];
                        qrData.IPNo = Listadm[0].AdmissionNo.ToString();
                        string qrxml = XmlConvertor.PatientQRDataXML(qrData);
                        objData.QRImage = Commonfunction.getQRImage(qrxml);
                        objData.AdmissionNo = Listadm[0].AdmissionNo.ToString();
                        objBO.UpdateIPDCode(objData);
                        txt_UHID.Text = "";
                    }
                    if (Listadm[0].AdmissionNo == "5")
                    {
                        Messagealert_.ShowMessage(lblmessage, "AdmissionStatus", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                    }

                    hdnemrgnumber.Value = null;
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
        }
        protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldepartment.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_doctor, mstlookup.GetIPDoctorBydepartmentID(Convert.ToInt32(ddldepartment.SelectedValue)));
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetAutoUHID(string prefixText, int count, string contextKey)
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
        protected void GvBedAssign_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Bedstatus = (Label)e.Row.FindControl("lbl_bedstatus");
                CheckBox checkbed = (CheckBox)e.Row.FindControl("chekboxselect");

                if (Bedstatus.Text == "1")
                {
                    e.Row.Cells[5].BackColor = System.Drawing.Color.Green;
                    checkbed.Enabled = true;
                }
                if (Bedstatus.Text == "2")
                {
                    e.Row.Cells[5].BackColor = System.Drawing.Color.Red;
                    checkbed.Enabled = false;
                }
                if (Bedstatus.Text == "3")
                {
                    e.Row.Cells[5].BackColor = System.Drawing.Color.Yellow;
                    checkbed.Enabled = false;
                }
                if (Bedstatus.Text == "4")
                {
                    e.Row.Cells[5].BackColor = System.Drawing.Color.Blue;
                    checkbed.Enabled = false;
                }
            }
        }
        protected void ddl_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindbedlist();
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            Clearall();
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtautoIPNo.Text = "";
            txtUHID.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            ddlstatus.SelectedIndex = 0;
            gvadmissionlist.DataSource = null;
            gvadmissionlist.DataBind();
            gvadmissionlist.Visible = false;
            lblresult.Visible = false;
            txt_patientNames.Text = "";
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage2.Visible = false;
            lblresult.Visible = false;
            txt_contactnumber.Text = "";
            div3.Visible = false;
            btnprints.Attributes["disabled"] = "disabled";
            btnsave.Attributes.Remove("disabled");
            ddldeischargestatus.SelectedIndex = 0;
            txt_contactnumber.Text = "";

        }
        protected void Clearall()
        {
            txt_UHID.Text = "";
            txt_name.Text = "";
            txt_address.Text = "";
            txt_AdmissionNo.Text = "";
            txt_gender.Text = "";
            txt_age.Text = "";
            bindbedlist();
            txt_case.Text = "";
            txt_referal.Text = "";
            ddldepartment.SelectedIndex = 0;
            ddl_block.SelectedIndex = 0;
            ddl_floor.SelectedIndex = 0;
            ddl_ward.SelectedIndex = 0;
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_doctor, mstlookup.GetIPDoctorBydepartmentID(Convert.ToInt32(ddldepartment.SelectedValue)));
            ddl_doctor.SelectedItem.Text = "";
            lblmessage.Visible = false;
            div1.Visible = false;
            div1.Attributes["class"] = "Blank";
            btnprint.Attributes["disabled"] = "disabled";
            btnsave.Attributes["disabled"] = "disabled";
            ddl_doctor.ClearSelection();
            Commonfunction.Insertzeroitemindex(ddl_doctor);
            hdnemrgnumber.Value = null;
            ddl_seconddoctor.SelectedIndex = 0;
            ddl_thirddoctor.SelectedIndex = 0;
            Commonfunction.Insertzeroitemindex(ddl_referal);
            ddl_source.SelectedIndex = 0;
            txt_contactnumber.Text = "";
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
        protected void txtautoIPNo_TextChanged(object sender, EventArgs e)
        {
            if (txtautoIPNo.Text != "")
            {
                bindgrid(1);
            }
        }
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            if (txt_patientNames.Text != "")
            {                 
                var source = txt_patientNames.Text.ToString();
                if (source.Contains(":"))
                {
                    bindgrid(1);
                }
                else
                {
                    txt_patientNames.Text = "";
                    txt_patientNames.Focus();
                    return;
                }
              
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
                divmsg3.Visible = true;
                divmsg3.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage2.Visible = false;
            }
            bindgrid(1);            
         
        }
        protected void bindgrid(int page)
        {
            try
            {
                if (txt_patientNames.Text != "")
                {
                    var source = txt_patientNames.Text.ToString();
                    if (source.Contains(":"))
                    {
                        lblmessage2.Visible = false;
                    }
                    else
                    {
                        txt_patientNames.Text = "";
                        txt_patientNames.Focus();
                        return;
                    }

                }
                if (txtdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "VaildDatefrom", 0);
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
                if (txtto.Text != "")
                {
                    if (Commonfunction.isValidDate(txtto.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "VaildDateto", 0);
                        div3.Attributes["class"] = "FailAlert";
                        div3.Visible = true;
                        txtto.Focus();
                        return;
                    }
                }
                else
                {
                    div3.Visible = false;
                }
                List<AdmissionData> objdeposit = GetAdmissionDetailList(page);
                if (objdeposit.Count > 0)
                {
                    gvadmissionlist.VirtualItemCount = objdeposit[0].MaximumRows;//total item is required for custom paging
                    gvadmissionlist.PageIndex = page - 1;

                    gvadmissionlist.DataSource = objdeposit;
                    gvadmissionlist.DataBind();
                    gvadmissionlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    btnprints.Attributes.Remove("disabled");
                    divmsg3.Visible = true;
                    // txttotaladmissioncharge.Text = Commonfunction.Getrounding(objdeposit[0].TotalAdmissionCharge.ToString());
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    gvadmissionlist.DataSource = null;
                    gvadmissionlist.DataBind();
                    gvadmissionlist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    //txttotaladmissioncharge.Text = "0.00";
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<AdmissionData> GetAdmissionDetailList(int curIndex)
        {
            AdmissionData objpat = new AdmissionData();
            AdmissionBO objbillingBO = new AdmissionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.UHID = Convert.ToInt64(txtUHID.Text.Trim() == "" ? "0" : txtUHID.Text.Trim());
            //objpat.AdmissionNo = txtautoIPNo.Text == "" ? "" : txtautoIPNo.Text;
            objpat.PatientName = ""; // txt_patientNames.Text == "" ? "" : txt_patientNames.Text.Trim();

            string IPNo;
            var source = txt_patientNames.Text.ToString();
            if (source.Contains(":"))
            {
                IPNo = source.Substring(source.LastIndexOf(':') + 1);
                objpat.AdmissionNo = IPNo.Trim();
            }
            else
            {
                objpat.AdmissionNo = "";
            }
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objpat.DischargeStatus = Convert.ToInt32(ddldeischargestatus.SelectedValue == "" ? "0" : ddldeischargestatus.SelectedValue);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            objpat.CurrentIndex = curIndex;
            return objbillingBO.GetAdmissionDetailList(objpat);
        }

        public List<AdmissionData> GetAdmissionList(int curIndex)
        {
            AdmissionData objpat = new AdmissionData();
            AdmissionBO objbillingBO = new AdmissionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.AdmissionNo = txtautoIPNo.Text == "" ? "0" : txtautoIPNo.Text;
            objpat.PatientName = txt_patientNames.Text == "" ? "0" : txt_patientNames.Text.Trim();
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objpat.DischargeStatus = Convert.ToInt32(ddldeischargestatus.SelectedValue == "" ? "0" : ddldeischargestatus.SelectedValue);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            return objbillingBO.GetAdmissionList(objpat);
        }
        protected void gvadmissionlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "DeleteEnable", 0);
                        div3.Visible = true;
                        div3.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage2.Visible = false;
                    }
                    AdmissionData objadmin = new AdmissionData();
                    AdmissionBO obadminBO = new AdmissionBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvadmissionlist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    Label UHID = (Label)gr.Cells[0].FindControl("lbluhid");
                    Label IPNo = (Label)gr.Cells[0].FindControl("IPNo");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objadmin.Remarks = txtremarks.Text;
                    }
                    objadmin.IPNo = ID.Text.Trim();
                    objadmin.UHID = Convert.ToInt32(UHID.Text == "" ? "0" : UHID.Text);
                    objadmin.EmployeeID = LogData.EmployeeID;
                    objadmin.HospitalID = LogData.HospitalID;
                    objadmin.IPaddress = LogData.IPaddress;
                    int Result = obadminBO.DeleteIPDAdmissionByID(objadmin);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        div3.Attributes["class"] = "SucessAlert";
                        div3.Visible = true;
                        bindgrid(1);
                    }
                    if (Result == 2)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "Admissioncancel", 0);
                        div3.Attributes["class"] = "FailAlert";
                        div3.Visible = true;
                    }
                }
                if (e.CommandName == "Edits")
                {
                    if (LogData.EditEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "EditEnable", 0);
                        div3.Visible = true;
                        div3.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    AdmissionData objadmin = new AdmissionData();
                    AdmissionBO obadminBO = new AdmissionBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvadmissionlist.Rows[i];
                    Label Ipno = (Label)gr.Cells[0].FindControl("lblID");
                    objadmin.IPNo = Ipno.Text;
                    objadmin.ActionType = Enumaction.Select;
                    List<AdmissionData> GetResult = obadminBO.GetIPadmissionDetailsByIPNo(objadmin);
                    if (GetResult.Count > 0)
                    {
                        txt_AdmissionNo.Text = GetResult[0].AdmissionNo.ToString();
                        txt_UHID.Text = GetResult[0].UHID.ToString();
                        txt_name.Text = GetResult[0].PatientName.ToString();
                        txt_address.Text = GetResult[0].Address.ToString();
                        txt_gender.Text = GetResult[0].GenderID.ToString();
                        txt_age.Text = GetResult[0].Agecount.ToString();
                        txt_contactnumber.Text = GetResult[0].ContactNo.ToString();
                        ddldepartment.SelectedValue = GetResult[0].DeptID.ToString();
                        MasterLookupBO mstlookup = new MasterLookupBO();
                        Commonfunction.PopulateDdl(ddl_doctor, mstlookup.GetIPDoctorBydepartmentID(Convert.ToInt32(GetResult[0].DeptID)));
                        ddl_doctor.SelectedValue = GetResult[0].DocID.ToString();
                        ddl_seconddoctor.SelectedValue = GetResult[0].AdmissionDoc_II.ToString();
                        ddl_thirddoctor.SelectedValue = GetResult[0].AdmissionDoc_III.ToString();
                        txt_referal.Text = GetResult[0].ReferalDoctorName.ToString();
                        txt_case.Text = GetResult[0].Cases.ToString();
                        ddl_block.Attributes["disabled"] = "disabled";
                        ddl_floor.Attributes["disabled"] = "disabled";
                        ddl_ward.Attributes["disabled"] = "disabled";
                        ddl_housekeepingstatus.Attributes["disabled"] = "disabled";
                        ddl_wardstatus.Attributes["disabled"] = "disabled";
                        ddl_status.Attributes["disabled"] = "disabled";
                        ViewState["ID"] = GetResult[0].IPNo;
                        btnsave.Attributes.Remove("disabled");
                        GvBedAssign.DataSource = null;
                        GvBedAssign.DataBind();
                        GvBedAssign.Visible = false;

                    }
                    tabcontaineradmission.ActiveTabIndex = 0;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
                div3.Attributes["class"] = "FailAlert";
                div3.Visible = true;
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<AdmissionData> AdmissionDetails = GetAdmissionList(0);
            List<AdmissionListDataTOeXCEL> ListexcelData = new List<AdmissionListDataTOeXCEL>();
            int i = 0;
            foreach (AdmissionData row in AdmissionDetails)
            {
                AdmissionListDataTOeXCEL Ecxeclpat = new AdmissionListDataTOeXCEL();
                Ecxeclpat.IPNo = AdmissionDetails[i].IPNo;
                Ecxeclpat.UHID = AdmissionDetails[i].UHID;
                Ecxeclpat.PatientName = AdmissionDetails[i].PatientName;
                Ecxeclpat.Department = AdmissionDetails[i].Department;
                Ecxeclpat.AdmissionDoctor = AdmissionDetails[i].AdmissionDoctor;
                Ecxeclpat.AdmissionCharge = Convert.ToDecimal((AdmissionDetails[i].AdmissionCharge.ToString()));
                Ecxeclpat.Cases = AdmissionDetails[i].Cases;
                Ecxeclpat.Admdate = AdmissionDetails[i].Admdate.ToString();

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
                div3.Visible = true;
                div3.Attributes["class"] = "FailAlert";
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
                    gvadmissionlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvadmissionlist.Columns[7].Visible = false;
                    gvadmissionlist.Columns[8].Visible = false;
                    gvadmissionlist.Columns[9].Visible = false;

                    gvadmissionlist.RenderControl(hw);
                    gvadmissionlist.HeaderRow.Style.Add("width", "15%");
                    gvadmissionlist.HeaderRow.Style.Add("font-size", "10px");
                    gvadmissionlist.Style.Add("text-decoration", "none");
                    gvadmissionlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvadmissionlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=IPDAdmissionlist.pdf");
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
                wb.Worksheets.Add(dt, "Deposit Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=AdmissionDetails.xlsx");
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
        protected void GvAdmissionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }

    }
}