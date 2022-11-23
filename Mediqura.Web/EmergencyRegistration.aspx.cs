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
    public partial class EmergencyRegistration :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_block, mstlookup.GetLookupsList(LookupName.BlockType));
            Commonfunction.PopulateDdl(ddl_floor, mstlookup.GetLookupsList(LookupName.FloorType));
            Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetLookupsList(LookupName.WardType));
            Commonfunction.PopulateDdl(ddl_doctor, mstlookup.GetLookupsList(LookupName.EmergencyDoc));
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
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.UHID = Convert.ToInt64(txt_UHID.Text.Trim() == "" ? "0" : txt_UHID.Text.Trim());
            getResult = objInfoBO.GetPatientAdmissionDetailsByUHID(Objpaic);
            if (getResult.Count > 0)
            {
                // Clearall();
                txt_name.Text = getResult[0].PatientName.ToString();
                txt_address.Text = getResult[0].Address.ToString();
                txt_gender.Text = getResult[0].GenderName.ToString();
                txt_age.Text = getResult[0].Agecount.ToString();
            }
            else
            {
                txt_name.Text = "";
                txt_address.Text = "";
                txt_UHID.Text = "";
                txt_gender.Text = "";
                txt_age.Text = "";
                txt_UHID.Focus();
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
                ddl_ward.SelectedIndex = 0;
            }
        }
        protected void ddl_ward_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_ward.SelectedIndex > 0)
            {
                List<AdmissionData> objdeposit = GetBedList(0);
                if (objdeposit.Count > 0)
                {
                    GvBedAssign.DataSource = objdeposit;
                    GvBedAssign.DataBind();
                    GvBedAssign.Visible = true;
                    //Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + "Record(s) found.", 1);
                    //divmsg3.Attributes["class"] = "SucessAlert";
                    //btnsave.Attributes.Remove("disabled");
                    //div3.Visible = false;
                }
                else
                {
                    GvBedAssign.DataSource = null;
                    GvBedAssign.DataBind();
                    GvBedAssign.Visible = true;
                    //lblresult.Visible = false;
                    //div3.Visible = false;
                }

            }
        }
        private List<AdmissionData> GetBedList(int p)
        {
            AdmissionData objpat = new AdmissionData();
            AdmissionBO objbillingBO = new AdmissionBO();
            objpat.BlockID = Convert.ToInt16(ddl_block.SelectedValue == "0" ? null : ddl_block.SelectedValue);
            objpat.FloorID = Convert.ToInt16(ddl_floor.SelectedValue == "0" ? null : ddl_floor.SelectedValue);
            objpat.WardID = Convert.ToInt16(ddl_ward.SelectedValue == "0" ? null : ddl_ward.SelectedValue);
            return objbillingBO.GetBedList(objpat);
        }
        protected void txt_case_TextChanged(object sender, EventArgs e)
        {

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
            if (ddl_doctor.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "EmergencyDoctor", 0);
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
            if (ddl_ward.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Ward", 0);
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
                if (countbed == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select atleast one bed.", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_doctor.Focus();
                    return;
                }
                objEmrgdata.XMLData = XmlConvertor.EmrgBedDatatoXML(Listbill).ToString();
                objEmrgdata.UHID = Convert.ToInt32(txt_UHID.Text.Trim() == "" ? "0" : txt_UHID.Text.Trim());
                objEmrgdata.DocID = Convert.ToInt64(ddl_doctor.SelectedValue == "" ? "0" : ddl_doctor.SelectedValue);
                objEmrgdata.Cases = txt_case.Text == "" ? null : txt_case.Text;
                objEmrgdata.BlockID = Convert.ToInt16(ddl_block.SelectedValue == "0" ? null : ddl_block.SelectedValue);
                objEmrgdata.FloorID = Convert.ToInt16(ddl_floor.SelectedValue == "0" ? null : ddl_floor.SelectedValue);
                objEmrgdata.WardID = Convert.ToInt16(ddl_ward.SelectedValue == "0" ? null : ddl_ward.SelectedValue);
             
                objEmrgdata.FinancialYearID = LogData.FinancialYearID;
                objEmrgdata.EmployeeID = LogData.EmployeeID;
                objEmrgdata.HospitalID = LogData.HospitalID;
                objEmrgdata.IPaddress = LogData.IPaddress;
                //objEmrgdata.IsActive = ddl_status.SelectedValue == "0" ? true : false; ;
                objEmrgdata.ActionType = Enumaction.Insert;

                int result = objEmrgBO.UpdateEmrgAdmissionDetails(objEmrgdata);
                if (result == 1)
                {
                    EmrgAdmissionData Objpaic = new EmrgAdmissionData();
                    EmrgAdmissionBO objInfoBO = new EmrgAdmissionBO();
                    List<EmrgAdmissionData> getResult = new List<EmrgAdmissionData>();
                    Objpaic.UHID = Convert.ToInt32(txt_UHID.Text.Trim() == "" ? "0" : txt_UHID.Text.Trim());
                    getResult = objInfoBO.GetEMRGNo(Objpaic);
                    if (getResult.Count == 1)
                    {
                        txt_EmergencyNo.Text = getResult[0].EmrgNo.ToString();
                    }
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    btnprint.Attributes.Remove("disabled");
                    //lblresult.Visible = false;
                    txt_UHID.Text = "";
                }
                else if (result == 5)
                {
                    Messagealert_.ShowMessage(lblmessage, "AdmissionStatus", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                }
                else
                {
                    txt_EmergencyNo.Text = "";
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
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
            //txt_admincharges.Text = "";
            txt_case.Text = "";
            //txt_admincharges.Text = "500.00";
            ddl_block.SelectedIndex = 0;
            ddl_floor.SelectedIndex = 0;
            ddl_ward.SelectedIndex = 0;
            MasterLookupBO mstlookup = new MasterLookupBO();
            ddl_doctor.SelectedItem.Text = "";
            lblmessage.Visible = false;
            div1.Visible = false;
            div1.Attributes["class"] = "Blank";
            GvBedAssign.DataSource = null;
            GvBedAssign.DataBind();
            GvBedAssign.Visible = false;
            btnprint.Attributes["disabled"] = "disabled";
            btnsave.Attributes["disabled"] = "disabled";
            ddl_doctor.ClearSelection();
            Commonfunction.Insertzeroitemindex(ddl_doctor);
        }
    }
}