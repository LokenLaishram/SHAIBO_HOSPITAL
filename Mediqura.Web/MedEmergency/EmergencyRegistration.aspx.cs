using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.PatientData;
using Mediqura.CommonData.MedEmergencyData;
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
using Mediqura.BOL.MedEmergencyBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.AdmissionData;
using Mediqura.BOL.AdmissionBO;

namespace Mediqura.Web.MedEmergency
{
    public partial class EmergencyRegistration : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EMG_UHID"] != null)
                {
                    txt_UHID.Text = Session["EMG_UHID"].ToString();
                    GetPatientdetails(Convert.ToInt64(txt_UHID.Text == "" ? "0" : txt_UHID.Text));
                    Session["EMG_UHID"] = null;
                }
                bindddl();
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_block, mstlookup.GetLookupsList(LookupName.BlockType));
            Commonfunction.PopulateDdl(ddldeischargestatus, mstlookup.GetLookupsList(LookupName.DischargeStatus));
            Commonfunction.PopulateDdl(ddl_emrgDocList, mstlookup.GetLookupsList(LookupName.EmergencyDoc));
            ddl_block.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddl_floor, mstlookup.GetLookupsList(LookupName.FloorType));
            ddl_floor.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetLookupsList(LookupName.WardType));
            ddl_ward.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.OPDepartment));
            Commonfunction.Insertzeroitemindex(ddldoctor);
            Commonfunction.Insertzeroitemindex(ddl_referal);
            btnprint.Attributes["disabled"] = "disabled";
            btnprints.Attributes["disabled"] = "disabled";
            ddl_block.Attributes["disabled"] = "disabled";
            ddl_floor.Attributes["disabled"] = "disabled";
            ddl_ward.Attributes["disabled"] = "disabled";
            getemergencybedlist();
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetUHID(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.UHID = Convert.ToInt64(prefixText == "" ? "0" : prefixText);
            getResult = objInfoBO.GetUHID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].RegDNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetOpnumber(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.OPnumber = prefixText;
            getResult = objInfoBO.GetOPDNumber(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].OPnumber.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetEmgPatientName(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetEmgPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmgPatientName.ToString());
            }
            return list;
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
            }

        }
        protected void txt_UHID_TextChanged(object sender, EventArgs e)
        {
            GetPatientdetails(Convert.ToInt64(txt_UHID.Text == "" ? "0" : txt_UHID.Text));
        }
        protected void GetPatientdetails(Int64 UHID)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.UHID = UHID;
            getResult = objInfoBO.GetPatientAdmissionDetailsByUHID(Objpaic);
            if (getResult.Count > 0)
            {
                txt_name.Text = getResult[0].PatientName.ToString();
                txt_address.Text = getResult[0].Address.ToString();
                txt_gender.Text = getResult[0].GenderName.ToString();
                txt_age.Text = getResult[0].Agecount.ToString();
                txt_contactnumber.Text = getResult[0].ContactNo.ToString();
            }
            else
            {
                txt_name.Text = "";
                txt_address.Text = "";
                txt_gender.Text = "";
                txt_age.Text = "";
                txt_UHID.Text = "";
                txt_contactnumber.Text = "";
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.OPDepartment));
                Commonfunction.Insertzeroitemindex(ddldoctor);
            }
        }
        protected void ddl_block_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_block.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_floor, mstlookup.GetfloorByblockID(Convert.ToInt32(ddl_block.SelectedValue)));
            }
        }
        protected void ddl_floor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_floor.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetWardByFloorID(Convert.ToInt32(ddl_floor.SelectedValue)));
                ddl_ward.SelectedIndex = 1;
                ddl_ward.Attributes["disabled"] = "disabled";

            }
        }
        protected void getemergencybedlist()
        {
            int status = Convert.ToInt32(ddl_status.SelectedValue == "" ? "0" : ddl_status.SelectedValue);
            List<AdmissionData> objdeposit = GetBedList(0, status);
            if (objdeposit.Count > 0)
            {
                GvBedAssign.DataSource = objdeposit;
                GvBedAssign.DataBind();
                GvBedAssign.Visible = true;
                btnsave.Attributes.Remove("disabled");
                div3.Visible = false;
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
        protected void ddl_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            getemergencybedlist();
        }
        protected void ddl_ward_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddl_ward.SelectedIndex > 0)
            //{
            //    List<AdmissionData> objdeposit = GetBedList(0);
            //    if (objdeposit.Count > 0)
            //    {
            //        GvBedAssign.DataSource = objdeposit;
            //        GvBedAssign.DataBind();
            //        GvBedAssign.Visible = true;
            //        //Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + "Record(s) found.", 1);
            //        //divmsg3.Attributes["class"] = "SucessAlert";
            //        //btnsave.Attributes.Remove("disabled");
            //        //div3.Visible = false;
            //    }
            //    else
            //    {
            //        GvBedAssign.DataSource = null;
            //        GvBedAssign.DataBind();
            //        GvBedAssign.Visible = true;
            //        //lblresult.Visible = false;
            //        //div3.Visible = false;
            //    }

            // }
        }
        private List<AdmissionData> GetBedList(int p, int status)
        {
            AdmissionData objpat = new AdmissionData();
            AdmissionBO objbillingBO = new AdmissionBO();
            objpat.BlockID = 1; // Convert.ToInt16(ddl_block.SelectedValue == "0" ? null : ddl_block.SelectedValue);
            objpat.FloorID = 1; //Convert.ToInt16(ddl_floor.SelectedValue == "0" ? null : ddl_floor.SelectedValue);
            objpat.WardID = 1; //Convert.ToInt16(ddl_ward.SelectedValue == "0" ? null : ddl_ward.SelectedValue);
            objpat.BedStatus = status;
            return objbillingBO.GetBedList(objpat);
        }
        protected void txt_case_TextChanged(object sender, EventArgs e)
        {

        }
        protected void GvBedAssign_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Bedstatus = (Label)e.Row.FindControl("lbl_bedstatus");
                CheckBox checkbed = (CheckBox)e.Row.FindControl("chekboxselect");

                if (Bedstatus.Text == "1")
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Green;
                    checkbed.Enabled = true;
                }
                if (Bedstatus.Text == "2")
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Red;
                    checkbed.Enabled = false;
                }
                if (Bedstatus.Text == "3")
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Yellow;
                    checkbed.Enabled = false;
                }
                if (Bedstatus.Text == "4")
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Blue;
                    checkbed.Enabled = false;
                }
            }
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
            if (txt_UHID.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "OPnumber", 0);
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
            if (ddldoctor.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "EmergencyDoctor", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddldoctor.Focus();
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
            if (ddl_block.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Block", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddl_block.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (ddl_floor.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Floor", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddl_floor.Focus();
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
            if (ddl_ward.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Ward", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddldoctor.Focus();
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
            List<EmrgAdmissionData> Listbill = new List<EmrgAdmissionData>();
            EmrgAdmissionData objEmrgdata = new EmrgAdmissionData();
            EmrgAdmissionBO objEmrgBO = new EmrgAdmissionBO();
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
                            EmrgAdmissionData ObjDetails = new EmrgAdmissionData();
                            CheckBox Checkbed = (CheckBox)GvBedAssign.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect");
                            if (Checkbed.Checked == true)
                            {
                                ObjDetails.Room = Room.Text == "" ? null : Room.Text;
                                ObjDetails.BedNo = bedno.Text == "" ? "0" : bedno.Text;
                                ObjDetails.Charges = Convert.ToDecimal(charges.Text == "" ? "0" : charges.Text);
                                ObjDetails.BedID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                                countbed = countbed + 1;
                                Listbill.Add(ObjDetails);
                            }
                        }
                    }
                }
                if (countbed != 1)
                {
                    Messagealert_.ShowMessage(lblmessage, "Bedno", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                objEmrgdata.XMLData = XmlConvertor.EmrgBedDatatoXML(Listbill).ToString();
                objEmrgdata.UHID = Convert.ToInt32(txt_UHID.Text.Trim() == "" ? "0" : txt_UHID.Text.Trim());
                objEmrgdata.DocID = Convert.ToInt64(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
                objEmrgdata.DeptID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
                objEmrgdata.Cases = txt_case.Text == "" ? null : txt_case.Text;
                objEmrgdata.BlockID = Convert.ToInt16(ddl_block.SelectedValue == "0" ? null : ddl_block.SelectedValue);
                objEmrgdata.FloorID = Convert.ToInt16(ddl_floor.SelectedValue == "0" ? null : ddl_floor.SelectedValue);
                objEmrgdata.WardID = Convert.ToInt16(ddl_ward.SelectedValue == "0" ? null : ddl_ward.SelectedValue);
                objEmrgdata.Informehk = Convert.ToInt16(ddl_housekeepingstatus.SelectedValue == "0" ? "0" : ddl_housekeepingstatus.SelectedValue);
                objEmrgdata.Informeward = Convert.ToInt16(ddl_wardstatus.SelectedValue == "0" ? "0" : ddl_wardstatus.SelectedValue);
                objEmrgdata.SourceID = Convert.ToInt32(ddl_source.SelectedValue == "0" ? "0" : ddl_source.SelectedValue);
                objEmrgdata.ReferalID = Convert.ToInt64(ddl_referal.SelectedValue == "0" ? "0" : ddl_referal.SelectedValue);
                objEmrgdata.FinancialYearID = LogData.FinancialYearID;
                objEmrgdata.EmployeeID = LogData.EmployeeID;
                objEmrgdata.HospitalID = LogData.HospitalID;
                objEmrgdata.IPaddress = LogData.IPaddress;
                objEmrgdata.ActionType = Enumaction.Insert;
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
                    }
                    objEmrgdata.ActionType = Enumaction.Update;
                    objEmrgdata.ID = Convert.ToInt32(ViewState["ID"].ToString() == "" ? "0" : ViewState["ID"].ToString());
                }
                Listbill = objEmrgBO.UpdateEmrgAdmissionDetails(objEmrgdata);
                if (Listbill[0].EmergencyNo != "5" || Listbill[0].EmergencyNo != "2")
                {
                    txt_EmergencyNo.Text = Listbill[0].EmergencyNo.ToString();
                    hdnopnumber.Value = Listbill[0].OPnumber.ToString();
                    hdnbillnumber.Value = Listbill[0].BillNo.ToString();
                    getemergencybedlist();
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    btnprint.Attributes.Remove("disabled");

                    EmrgAdmissionData objData = new EmrgAdmissionData();
                    EmrgAdmissionBO objBO = new EmrgAdmissionBO();
                    OPDbillingBO objbillingBO = new OPDbillingBO();
                    objData.BarcodeImage = Commonfunction.getBarcodeImage(Listbill[0].EmergencyNo.ToString());
                    //QR image generation//
                    List<PatientQRdata> listQr = objbillingBO.GetPatientQRData(Convert.ToInt64(txt_UHID.Text));
                    PatientQRdata qrData = new PatientQRdata();
                    qrData = listQr[0];
                    qrData.IPNo = Listbill[0].EmergencyNo.ToString();
                    string qrxml = XmlConvertor.PatientQRDataXML(qrData);
                    objData.QRImage = Commonfunction.getQRImage(qrxml);
                    objData.EmrgNo = Listbill[0].EmergencyNo.ToString();
                    objBO.UpdateEmrgCodes(objData);
                }
                if (Listbill[0].EmergencyNo == "2")
                {
                    hdnopnumber.Value = null;
                    hdnbillnumber.Value = null;
                    div1.Attributes["class"] = "SucessAlert";
                    div1.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                }
                if (Listbill[0].EmergencyNo == "5")
                {
                    hdnopnumber.Value = null;
                    hdnbillnumber.Value = null;
                    Messagealert_.ShowMessage(lblmessage, "AdmissionStatus", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
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
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txt_UHID.Text = "";
            txt_name.Text = "";
            txt_address.Text = "";
            txt_EmergencyNo.Text = "";
            txt_gender.Text = "";
            txt_age.Text = "";
            txt_case.Text = "";
            ddl_block.SelectedIndex = 0;
            ddl_floor.SelectedIndex = 0;
            ddl_ward.SelectedIndex = 0;
            lblmessage.Visible = false;
            div1.Visible = false;
            div1.Attributes["class"] = "Blank";
            GvBedAssign.DataSource = null;
            GvBedAssign.DataBind();
            GvBedAssign.Visible = false;
            btnprint.Attributes["disabled"] = "disabled";
            btnsave.Attributes["disabled"] = "disabled";
            ddl_wardstatus.SelectedIndex = 0;
            ddl_housekeepingstatus.SelectedIndex = 0;
            Commonfunction.Insertzeroitemindex(ddldoctor);
            hdnbillnumber.Value = null;
            hdnopnumber.Value = null;
            bindddl();
            ViewState["ID"] = null;
            Commonfunction.Insertzeroitemindex(ddl_referal);
            ddl_source.SelectedIndex = 0;
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

            bindgrid();
        }
        protected void bindgrid()
        {
            try
            {
                if (txt_patientNames.Text != "")
                {
                    var source1 = txt_patientNames.Text.ToString();
                    if (source1.Contains(":"))
                    {
                        lblmessage.Visible = false;
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
                List<EmrgAdmissionData> objdeposit = GetEmrgList(0);
                if (objdeposit.Count > 0)
                {
                    gvEmrglist.DataSource = objdeposit;
                    gvEmrglist.DataBind();
                    gvEmrglist.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    btnprints.Attributes.Remove("disabled");
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    gvEmrglist.DataSource = null;
                    gvEmrglist.DataBind();
                    gvEmrglist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
            }
        }
        public List<EmrgAdmissionData> GetEmrgList(int curIndex)
        {

            EmrgAdmissionData objpat = new EmrgAdmissionData();
            EmrgAdmissionBO objbillingBO = new EmrgAdmissionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            //objpat.EmrgNo = txtEmrgNo.Text.Trim() == "" ? "" : txtEmrgNo.Text.Trim();
            objpat.PatientName = ""; // txt_patientNames.Text == "" ? "" : txt_patientNames.Text.Trim();

            string EmgNo;
            var source = txt_patientNames.Text.ToString();
            if (source.Contains(":"))
            {
                EmgNo = source.Substring(source.LastIndexOf(':') + 1);
                objpat.EmrgNo = EmgNo.Trim();
            }
            else
            {
                objpat.EmrgNo = "";
            }

            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objpat.DateFrom = from;
            objpat.DateTo = To;
            objpat.DocID = Convert.ToInt64(ddl_emrgDocList.SelectedValue == "" ? "0" : ddl_emrgDocList.SelectedValue);
            objpat.DischargeStatus = Convert.ToInt32(ddldeischargestatus.SelectedValue == "" ? "0" : ddldeischargestatus.SelectedValue);
            return objbillingBO.GetEmrgList(objpat);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtEmrgNo.Text = "";
            txt_patientNames.Text = "";
            ddl_emrgDocList.SelectedIndex = 0;
            txtdatefrom.Text = "";
            txtto.Text = "";
            ddlstatus.SelectedIndex = 0;
            gvEmrglist.DataSource = null;
            gvEmrglist.DataBind();
            gvEmrglist.Visible = false;
            lblresult.Visible = false;
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage2.Visible = false;
            lblresult.Visible = false;
            div3.Visible = false;
            btnprints.Attributes["disabled"] = "disabled";
            ddldeischargestatus.SelectedIndex = 0;
        }

        protected void gvEmrglist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
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
                    //EmrgAdmissionData objEmrgData = new EmrgAdmissionData();
                    //EmrgAdmissionBO objEmrgBO = new EmrgAdmissionBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = gvEmrglist.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("lblEmrgID");
                    Int64 PatID = Convert.ToInt64(ID.Text);
                    EditPatient(PatID);
                    tabcontaineradmission.ActiveTabIndex = 0;
                }
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
                        lblmessage.Visible = false;
                    }
                    EmrgAdmissionData objEmrgData = new EmrgAdmissionData();
                    EmrgAdmissionBO objEmrgBO = new EmrgAdmissionBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvEmrglist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    Label UHID = (Label)gr.Cells[0].FindControl("lbluhid");
                    Label IPNo = (Label)gr.Cells[0].FindControl("IPNo");
                    Label name = (Label)gr.Cells[0].FindControl("lblname");
                    Label block = (Label)gr.Cells[0].FindControl("lblblock");
                    Label doctor = (Label)gr.Cells[0].FindControl("lbladmissiondoc");
                    Label date = (Label)gr.Cells[0].FindControl("lbladmittedon");
                    Label cases = (Label)gr.Cells[0].FindControl("lblcase");
                    Label BillNo = (Label)gr.Cells[0].FindControl("lbl_billnumber");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        divmsg3.Visible = true;
                        divmsg3.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objEmrgData.Remarks = txtremarks.Text;
                    }
                    objEmrgData.EmrgNo = ID.Text;
                    objEmrgData.UHID = Convert.ToInt32(UHID.Text == "" ? "0" : UHID.Text);
                    objEmrgData.EmployeeID = LogData.EmployeeID;
                    objEmrgData.HospitalID = LogData.HospitalID;
                    objEmrgData.IPaddress = LogData.IPaddress;
                    int Result = objEmrgBO.DeleteEmrgRegnDetailsByID(objEmrgData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        div3.Visible = true;
                        div3.Attributes["class"] = "SucessAlert";
                        bindgrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
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
        protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldepartment.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddldoctor, mstlookup.GetDepartmentDoctor(Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue)));
            }
        }
        protected void EditPatient(Int64 patID)
        {
            try
            {
                EmrgAdmissionData objEmrgData = new EmrgAdmissionData();
                EmrgAdmissionBO objEmrgBO = new EmrgAdmissionBO();
                List<EmrgAdmissionData> GetResult = GetEmrgDetailsByID(patID);
                if (GetResult.Count > 0)
                {
                    //tabcontaineradmission.TabIndex = 0;
                    txt_EmergencyNo.Text = GetResult[0].EmrgNo;
                    txt_case.Text = GetResult[0].Cases;
                    txt_name.Text = GetResult[0].PatientName.ToString();
                    txt_address.Text = GetResult[0].Address.ToString();
                    txt_gender.Text = GetResult[0].GenderName.ToString();
                    txt_age.Text = GetResult[0].Agecount.ToString();
                    ddldoctor.SelectedValue = GetResult[0].DocID.ToString();
                    ddl_block.SelectedValue = GetResult[0].BlockID.ToString();
                    ddl_block.Attributes["disabled"] = "disabled";
                    ddl_floor.SelectedValue = GetResult[0].FloorID.ToString();
                    ddl_floor.Attributes["disabled"] = "disabled";
                    ddl_ward.SelectedValue = GetResult[0].WardID.ToString();
                    ddl_ward.Attributes["disabled"] = "disabled";
                    ViewState["ID"] = GetResult[0].ID.ToString();
                    lblmessage.Visible = false;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
            }
        }
        public List<EmrgAdmissionData> GetEmrgDetailsByID(Int64 ID)
        {
            EmrgAdmissionData objpat = new EmrgAdmissionData();
            EmrgAdmissionBO objpatBO = new EmrgAdmissionBO();
            objpat.ID = ID;
            return objpatBO.GetEmrgDetailsByID(objpat);
        }
        protected void tabcontaineradmission_ActiveTabChanged(object sender, EventArgs e)
        {
        }

        protected void gvEmrglist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEmrglist.PageIndex = e.NewPageIndex;
            bindgrid();
        }

        protected void txtEmrgNo_TextChanged(object sender, EventArgs e)
        {
            //if (txtEmrgNo.Text != "")
            //{
            //    bindgrid();
            //}

        }

        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            if (txt_patientNames.Text != "")
            {
                bindgrid();
            }
        }

        protected DataTable GetDatafromDatabase()
        {
            List<EmrgAdmissionData> AdmissionDetails = GetEmrgList(0);
            List<EMRGAdmissionListDataTOeXCEL> ListexcelData = new List<EMRGAdmissionListDataTOeXCEL>();
            int i = 0;
            foreach (EmrgAdmissionData row in AdmissionDetails)
            {
                EMRGAdmissionListDataTOeXCEL Ecxeclpat = new EMRGAdmissionListDataTOeXCEL();
                Ecxeclpat.EmrgNo = AdmissionDetails[i].EmrgNo;
                Ecxeclpat.UHID = AdmissionDetails[i].UHID;
                Ecxeclpat.PatientName = AdmissionDetails[i].PatientName;
                Ecxeclpat.Address = AdmissionDetails[i].Address;
                //Ecxeclpat.Department = AdmissionDetails[i].Department;
                Ecxeclpat.AdmissionDoctor = AdmissionDetails[i].AdmissionDoctor;
                Ecxeclpat.AdmissionDate = AdmissionDetails[i].AdmissionDate;
                //Ecxeclpat.AdmissionCharge = AdmissionDetails[i].AdmissionCharge;
                //Ecxeclpat.Cases = AdmissionDetails[i].Cases;
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
                    gvEmrglist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvEmrglist.Columns[6].Visible = false;
                    gvEmrglist.Columns[7].Visible = false;
                    gvEmrglist.Columns[8].Visible = false;

                    gvEmrglist.RenderControl(hw);
                    gvEmrglist.HeaderRow.Style.Add("width", "15%");
                    gvEmrglist.HeaderRow.Style.Add("font-size", "10px");
                    gvEmrglist.Style.Add("text-decoration", "none");
                    gvEmrglist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvEmrglist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=EmergencyAdmissionlist.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=EmergencyAdmissionlist.xlsx");
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
    }
}