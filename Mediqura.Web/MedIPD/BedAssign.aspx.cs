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

namespace Mediqura.Web.MedIPD
{

    public partial class BedAssign : BasePage
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
            Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.Department));
            Commonfunction.PopulateDdl(ddl_block, mstlookup.GetLookupsList(LookupName.BlockType));
            Commonfunction.PopulateDdl(ddl_floor, mstlookup.GetLookupsList(LookupName.FloorType));
            Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetLookupsList(LookupName.WardType));
            //txt_admincharges.Text = "500.00";
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
                txt_admissionDate.Text = getResult[0].AdmissionDate.ToString();
                //txt_consultant.Text = getResult[0].EmpName.ToString();
            }
            else
            {
                txt_name.Text = "";
                txt_address.Text = "";
                txt_IPNo.Text = "";
                txt_gender.Text = "";
                txt_age.Text = "";
                txt_admissionDate.Text = "";
                txt_IPNo.Focus();
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
            if (txt_admincharges.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Charge", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_admincharges.Focus();
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
            if (ddl_doctor.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Floor", 0);
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
            List<BedAssignData> Listbill = new List<BedAssignData>();
            BedAssignData objdata = new BedAssignData();
          //  BedAssignedBO objadmissionBO = new BedAssignedBO();
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
                            BedAssignData ObjDetails = new BedAssignData();
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
              //  objdata.XMLData = XmlConvertor.BedDatatoXML(Listbill).ToString();
                objdata.IPNo = txt_IPNo.Text == "" ? null : txt_IPNo.Text;
                objdata.DischargeStatus = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
                objdata.DocID = Convert.ToInt32(ddl_doctor.SelectedValue == "" ? "0" : ddl_doctor.SelectedValue);
                objdata.TotalAdmissionCharge = Convert.ToDecimal(txt_admincharges.Text == "" ? null : txt_admincharges.Text);
                objdata.BlockID = Convert.ToInt16(ddl_block.SelectedValue == "0" ? null : ddl_block.SelectedValue);
                objdata.FloorID = Convert.ToInt16(ddl_floor.SelectedValue == "0" ? null : ddl_floor.SelectedValue);
                objdata.WardID = Convert.ToInt16(ddl_ward.SelectedValue == "0" ? null : ddl_ward.SelectedValue);
                objdata.Cases = txt_case.Text == "" ? null : txt_case.Text;
                objdata.FinancialYearID = LogData.FinancialYearID;
                objdata.EmployeeID = LogData.EmployeeID;
                objdata.HospitalID = LogData.HospitalID;
                objdata.IPaddress = LogData.IPaddress;
                objdata.IsActive = ddl_status.SelectedValue == "0" ? true : false; ;
                objdata.ActionType = Enumaction.Insert;

                //int result = objadmissionBO.UpdateBedAdmissionDetails(objdata);
                //if (result == 1)
                //{
                //    IPData Objpaic = new IPData();
                //    AdmissionBO objInfoBO = new AdmissionBO();
                //    List<IPData> getResult = new List<IPData>();
                  
                //    getResult = objInfoBO.GetIPNo(Objpaic);
                //    if (getResult.Count == 1)
                //    {
                //        //txt_AdmissionNo.Text = getResult[0].IPNo.ToString();
                //    }
                //    Messagealert_.ShowMessage(lblmessage, "update", 1);
                //    div1.Visible = true;
                //    div1.Attributes["class"] = "SucessAlert";
                  
                //    lblresult.Visible = false;
                  
                //}
                //else if (result == 5)
                //{
                //    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                //    div1.Visible = true;
                //    div1.Attributes["class"] = "FailAlert";
                //}
                //else
                //{
                   
                //    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                //    div1.Visible = true;
                //    div1.Attributes["class"] = "FailAlert";
                //}
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

        protected void btnresets_Click(object sender, EventArgs e)
        {
           
            txt_name.Text = "";
            txt_address.Text = "";
          
            txt_gender.Text = "";
            txt_age.Text = "";
            txt_admincharges.Text = "";
            txt_case.Text = "";
            txt_admincharges.Text = "500.00";
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
            GvBedAssign.DataSource = null;
            GvBedAssign.DataBind();
            GvBedAssign.Visible = false;
           
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {

        }

        protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldepartment.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_doctor, mstlookup.GetIPDoctorBydepartmentID(Convert.ToInt32(ddldepartment.SelectedValue)));
            }
        }

        protected void txt_admincharges_TextChanged(object sender, EventArgs e)
        {
            if (txt_admincharges.Text == "500.00")
            {
                //lbl_remarks.Visible = false;
                //txt_remarks.Visible = false;
            }
            else
            {
                //lbl_remarks.Visible = true;
                //txt_remarks.Visible = true;
                //txt_remarks.Focus();
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
            }
        }

        //protected void ddl_ward_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddl_ward.SelectedIndex > 0)
        //    {
        //        List<AdmissionData> objdeposit = GetBedList(0);
        //        if (objdeposit.Count > 0)
        //        {
        //            GvBedAssign.DataSource = objdeposit;
        //            GvBedAssign.DataBind();
        //            GvBedAssign.Visible = true;
        //            Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + "Record found", 1);
        //            divmsg3.Attributes["class"] = "SucessAlert";
        //            btnsave.Attributes.Remove("disabled");
        //            div1.Visible = false;
        //        }
        //        else
        //        {
        //            GvBedAssign.DataSource = objdeposit;
        //            GvBedAssign.DataBind();
        //            GvBedAssign.Visible = true;
        //            lblresult.Visible = false;
        //            div1.Visible = false;
        //        }

        //    }
        //}
        private List<AdmissionData> GetBedList(int p)
        {
            AdmissionData objpat = new AdmissionData();
            AdmissionBO objbillingBO = new AdmissionBO();
            objpat.BlockID = Convert.ToInt16(ddl_block.SelectedValue == "0" ? null : ddl_block.SelectedValue);
            objpat.FloorID = Convert.ToInt16(ddl_floor.SelectedValue == "0" ? null : ddl_floor.SelectedValue);
            objpat.WardID = Convert.ToInt16(ddl_ward.SelectedValue == "0" ? null : ddl_ward.SelectedValue);
            return objbillingBO.GetBedList(objpat);
        }

        //protected void txtnursecharge_TextChanged(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow row in GvBedAssign.Rows)
        //    {

        //        Label lblroom = (Label)GvBedAssign.Rows[row.RowIndex].Cells[0].FindControl("lbl_room");
        //        Label lblbedno = (Label)GvBedAssign.Rows[row.RowIndex].Cells[0].FindControl("lbl_bedno");
        //        if (txt_bedno.Text == lblbedno.Text && txt_room.Text ==lblroom.Text)
        //        {
        //            txt_bedno.Text = "";
        //            txt_room.Text = "";
        //            txt_charges.Text = "";
        //            txtnursecharge.Text = "";
        //            Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
        //            div1.Visible = true;
        //            div1.Attributes["class"] = "FailAlert";
        //            txt_bedno.Focus();
        //            return;
        //        }
        //        else
        //        {
        //            lblmessage.Visible = false;
        //        }
        //    }
        //    List<BedAssignData> fileList = Session["fileList"] == null ? new List<BedAssignData>() : (List<BedAssignData>)Session["fileList"];
        //    BedAssignData Objfile = new BedAssignData();
        //    Objfile.BedNo = txt_bedno.Text.ToString() == "" ? "0" : txt_bedno.Text.ToString();
        //    Objfile.Room = txt_room.Text.Trim() == "" ? "0" : txt_room.Text.Trim();
        //    Objfile.Charges =Convert.ToDecimal(txt_charges.Text.Trim() == "" ? "0" : txt_charges.Text.Trim()); 
        //    Objfile.NuCharges =Convert.ToDecimal(txtnursecharge.Text.Trim() == "" ? "0" : txtnursecharge.Text.Trim()); 
            

        //    fileList.Add(Objfile);
        //    if (fileList.Count > 0)
        //    {
        //        GvBedAssign.DataSource = fileList;
        //        GvBedAssign.DataBind();
        //        GvBedAssign.Visible = true;
        //        Session["fileList"] = fileList;
        //        txt_bedno.Text = "";
        //        txt_room.Text = "";
        //        txt_charges.Text = "";
        //        txtnursecharge.Text = "";
        //        btnsave.Attributes.Remove("disabled");
               
        //    }
        //    else
        //    {
        //        GvBedAssign.DataSource = null;
        //        GvBedAssign.DataBind();
        //        GvBedAssign.Visible = true;
        //    }



        //}

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
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + "Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    btnsave.Attributes.Remove("disabled");
                    div1.Visible = false;
                }
                else
                {
                    GvBedAssign.DataSource = objdeposit;
                    GvBedAssign.DataBind();
                    GvBedAssign.Visible = true;
                    lblresult.Visible = false;
                    div1.Visible = false;
                }

            }
        }
    }
}