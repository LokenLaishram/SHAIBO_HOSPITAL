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
using Mediqura.CommonData.PatientData;
using Mediqura.BOL.PatientBO;
using Mediqura.Utility;

namespace Mediqura.Web.MedAdmission
{
    public partial class IPDBedTransfer : BasePage
    {
        DateTime entrydate = System.DateTime.Now;
        decimal lastBedCharge;
        int wardId = 0;
        int LastBedId = 0;
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
            Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetLookupsList(LookupName.IPDWardType));
            Commonfunction.Insertzeroitemindex(ddl_floor);
            btnsave.Attributes["disabled"] = "disabled";
            Session["gridrow"] = 0;
            lbl_transfer.Text = "Occupy";
            Session["BedStatus"] = 1;
            btnsave.Attributes["disabled"] = "disabled";
            Session["nobed"] = 0;

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
                Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetIPDWardByFloorID(Convert.ToInt32(ddl_floor.SelectedValue)));
            }
        }
        protected void ddl_ward_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_ward.SelectedIndex > 0)
            {

                List<AdmissionData> objdeposit = GetBedList1(0);
                if (objdeposit.Count > 0)
                {
                    if (ddl_ward.SelectedValue == "9" || ddl_ward.SelectedValue == "8" || ddl_ward.SelectedValue == "7" || ddl_ward.SelectedValue == "10" || ddl_ward.SelectedValue == "11" || ddl_ward.SelectedValue == "12" || ddl_ward.SelectedValue == "13" || ddl_ward.SelectedValue == "14")
                    {
                        ddl_transfertype.SelectedIndex = 1;
                        ddl_transfertype.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        ddl_transfertype.Attributes.Remove("disabled");
                    }
                    GvBedTransfer.DataSource = objdeposit;
                    GvBedTransfer.DataBind();
                    GvBedTransfer.Visible = true;
                    btnsave.Attributes.Remove("disabled");
                    if (Session["BedStatus"].ToString() == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Are you sure to occupied new bed ?.');", true);

                    }
                    if (Session["BedStatus"].ToString() == "2")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Are you sure to release bed ?.');", true);
                    }
                    if (Session["BedStatus"].ToString() == "3")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Are you sure to release the current bed and  occupy new ?.');", true);
                    }
                    btnsave.Attributes.Remove("disabled");
                }
                else
                {
                    btnsave.Attributes["disabled"] = "disabled";
                    GvBedTransfer.DataSource = null;
                    GvBedTransfer.DataBind();
                    GvBedTransfer.Visible = true;
                    btnsave.Attributes["disabled"] = "disabled";
                }
            }
            else
            {
                GvBedTransfer.DataSource = null;
                GvBedTransfer.DataBind();
                GvBedTransfer.Visible = true;
            }
        }
        protected void txt_autoipno_TextChanged(object sender, EventArgs e)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.IPNo = txt_autoipno.Text.Trim() == "" ? null : txt_autoipno.Text.Trim();
            getResult = objInfoBO.GetPatientDetailsByIPNo(Objpaic);
            if (getResult.Count > 0)
            {
                txtname.Text = getResult[0].PatientName.ToString();
                txt_address.Text = getResult[0].Address.ToString();
                txt_gender.Text = getResult[0].GenderName.ToString();
                txt_age.Text = getResult[0].Agecount.ToString();
                txt_contactnumber.Text = getResult[0].ContactNo.ToString();
            }
            else
            {
                txtname.Text = "";
                txt_address.Text = "";
                txt_autoipno.Text = "";
                txt_gender.Text = "";
                txt_age.Text = "";
                txt_contactnumber.Text = "";
                txt_autoipno.Focus();
            }
            bindoccupiedbed();
        }
        protected void bindoccupiedbed()
        {
            List<AdmissionData> objdeposit = GetBedList(0);
            if (objdeposit.Count > 0)
            {
                Session["nobed"] = objdeposit.Count;
                if (objdeposit[0].IsAdmittedToWard == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "WadrRecievd", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                GvBedAssign.DataSource = objdeposit;
                GvBedAssign.DataBind();
                GvBedAssign.Visible = true;
                ddl_block.Attributes.Remove("disabled");
                ddl_floor.Attributes.Remove("disabled");
                ddl_ward.Attributes.Remove("disabled");
            }
            else
            {
                GvBedAssign.DataSource = null;
                GvBedAssign.DataBind();
                GvBedAssign.Visible = true;
                ddl_block.Attributes["disabled"] = "disabled";
                ddl_floor.Attributes["disabled"] = "disabled";
                ddl_ward.Attributes["disabled"] = "disabled";
            }
        }
        protected void ddl_beddetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList chekboxselect = (DropDownList)sender;
            GridViewRow row = (GridViewRow)chekboxselect.NamingContainer;
            DropDownList ddl_beddetail = (DropDownList)GvBedAssign.Rows[row.RowIndex].Cells[6].FindControl("ddl_beddetails");
            Label Rate = (Label)GvBedAssign.Rows[row.RowIndex].Cells[6].FindControl("lbl_charges");
            Button btn_change = (Button)GvBedAssign.Rows[row.RowIndex].Cells[6].FindControl("btn_update");
            string BedCharge = ddl_beddetail.SelectedItem.Text.Substring(ddl_beddetail.SelectedItem.Text.LastIndexOf(':') + 1);
            DropDownList ddl_status = (DropDownList)GvBedAssign.Rows[row.RowIndex].Cells[6].FindControl("ddl_status");
            if (Convert.ToDecimal(Rate.Text == "" ? "0" : Rate.Text) == Convert.ToDecimal(BedCharge == "" ? "0" : BedCharge))
            {
                ddl_status.SelectedValue = "1";
                ddl_status.Attributes["disabled"] = "disabled";
                btn_change.Visible = true;
            }
            else
            {
                ddl_status.SelectedValue = "1";
                ddl_status.Attributes.Remove("disabled");
                btn_change.Visible = false;
            }
        }
        protected void ddl_patactive_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            Label BedID = (Label)GvBedAssign.Rows[row.RowIndex].Cells[0].FindControl("lbl_bedID");
            DropDownList patstatus = (DropDownList)GvBedAssign.Rows[row.RowIndex].Cells[0].FindControl("ddl_patactive");
            AdmissionData objadmdata = new AdmissionData();
            AdmissionBO objInfoBO = new AdmissionBO();
            objadmdata.IPNo = txt_autoipno.Text.Trim();
            objadmdata.EmployeeID = LogData.EmployeeID;
            objadmdata.BedID = Convert.ToInt32(BedID.Text == "" ? "0" : BedID.Text);
            objadmdata.BedStatus = Convert.ToInt32(patstatus.SelectedValue == "1" ? "1" : "0");
            int result = objInfoBO.UpdatepatientbedStatus(objadmdata);
            if (result == 1)
            {
                bindoccupiedbed();
            }
        }
        protected void ddl_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList chekboxselect = (DropDownList)sender;
            GridViewRow row = (GridViewRow)chekboxselect.NamingContainer;
            //int index = GvBedAssign.Rows[row.RowIndex];
            btnsave.Attributes["disabled"] = "disabled";
            if (Convert.ToInt32(Session["nobed"]) < 3)
            {
                if (Session["gridrow"].ToString() == "1")
                {
                    Button btn_change = (Button)GvBedAssign.Rows[0].Cells[6].FindControl("btn_update");
                    DropDownList status = (DropDownList)GvBedAssign.Rows[0].Cells[6].FindControl("ddl_status");
                    Session["BedStatus"] = status.SelectedValue == "" ? "0" : status.SelectedValue;
                    if (status.SelectedValue == "0")
                    {
                        btn_change.Visible = false;
                        lbl_transfer.Text = "Occupied";
                        btn_change.Visible = false;
                        ddl_block.Attributes["disabled"] = "disabled";
                        ddl_floor.Attributes["disabled"] = "disabled";
                        ddl_ward.Attributes["disabled"] = "disabled";
                        btnsave.Attributes["disabled"] = "disabled";
                        ddl_transfertype.SelectedValue = "0";

                    }
                    if (status.SelectedValue == "1")
                    {
                        btn_change.Visible = false;
                        lbl_transfer.Text = "Occupied";
                        status.Attributes.Remove("disabled");
                        ddl_block.Attributes.Remove("disabled");
                        ddl_floor.Attributes.Remove("disabled");
                        ddl_ward.Attributes.Remove("disabled");
                        ddl_transfertype.Attributes.Remove("disabled");
                        ddl_transfertype.SelectedValue = "0";

                    }
                    if (status.SelectedValue == "2")
                    {
                        btn_change.Visible = false;
                        lbl_transfer.Text = "Transfer";
                        status.Attributes.Remove("disabled");
                        ddl_block.Attributes["disabled"] = "disabled";
                        ddl_floor.Attributes["disabled"] = "disabled";
                        ddl_ward.Attributes["disabled"] = "disabled";
                        ddl_transfertype.Attributes.Remove("disabled");
                        ddl_transfertype.SelectedValue = "0";
                    }
                    if (status.SelectedValue == "3")
                    {
                        btn_change.Visible = false;
                        lbl_transfer.Text = "Transfer";
                        status.Attributes.Remove("disabled");
                        ddl_block.Attributes.Remove("disabled");
                        ddl_floor.Attributes.Remove("disabled");
                        ddl_ward.Attributes.Remove("disabled");
                        ddl_transfertype.Attributes["disabled"] = "disabled";
                        ddl_transfertype.SelectedValue = "1";
                    }
                }
                if (Session["gridrow"].ToString() == "2")
                {
                    Button btn_change = (Button)GvBedAssign.Rows[0].Cells[6].FindControl("btn_update");
                    DropDownList status = (DropDownList)GvBedAssign.Rows[0].Cells[6].FindControl("ddl_status");
                    Button btn_change1 = (Button)GvBedAssign.Rows[1].Cells[6].FindControl("btn_update");
                    DropDownList status1 = (DropDownList)GvBedAssign.Rows[1].Cells[6].FindControl("ddl_status");
                    Label Active = (Label)GvBedAssign.Rows[0].Cells[6].FindControl("lbl_active");
                    Label Active1 = (Label)GvBedAssign.Rows[1].Cells[6].FindControl("lbl_active");
                    if (status.SelectedValue == "0")
                    {
                        btn_change.Visible = false;
                        lbl_transfer.Text = "Occupied";
                        btn_change.Visible = false;
                        ddl_block.Attributes["disabled"] = "disabled";
                        ddl_floor.Attributes["disabled"] = "disabled";
                        ddl_ward.Attributes["disabled"] = "disabled";
                        btnsave.Attributes["disabled"] = "disabled";
                        ddl_transfertype.SelectedValue = "0";

                    }
                    if (status.SelectedValue == "2" && Active.Text == "1")
                    {
                        btn_change.Visible = true;
                        btn_change1.Visible = false;
                        lbl_transfer.Text = "Transfer";
                        status1.SelectedIndex = 1;
                        status.Attributes.Remove("disabled");
                        status1.Attributes["disabled"] = "disabled";
                        ddl_block.Attributes["disabled"] = "disabled";
                        ddl_floor.Attributes["disabled"] = "disabled";
                        ddl_ward.Attributes["disabled"] = "disabled";
                        Session["BedStatus"] = status.SelectedValue == "" ? "0" : status.SelectedValue;
                        ddl_transfertype.Attributes["disabled"] = "disabled";
                        ddl_transfertype.SelectedValue = "1";
                    }
                    if (status1.SelectedValue == "2" && Active1.Text == "0")
                    {
                        btn_change.Visible = false;
                        btn_change1.Visible = false;
                        lbl_transfer.Text = "Occupied";
                        status.SelectedIndex = 1;
                        status1.Attributes.Remove("disabled");
                        status.Attributes["disabled"] = "disabled";
                        ddl_block.Attributes["disabled"] = "disabled";
                        ddl_floor.Attributes["disabled"] = "disabled";
                        ddl_ward.Attributes["disabled"] = "disabled";
                        Session["BedStatus"] = status1.SelectedValue == "" ? "0" : status1.SelectedValue;
                        ddl_transfertype.Attributes["disabled"] = "disabled";
                        ddl_transfertype.SelectedValue = "2";
                    }
                    if (status.SelectedValue == "3" && Active.Text == "1")
                    {
                        btn_change.Visible = false;
                        btn_change1.Visible = false;
                        status1.SelectedIndex = 1;
                        lbl_transfer.Text = "Transfer";
                        status1.Attributes["disabled"] = "disabled";
                        ddl_block.Attributes.Remove("disabled");
                        ddl_floor.Attributes.Remove("disabled");
                        ddl_ward.Attributes.Remove("disabled");
                        Session["BedStatus"] = status.SelectedValue == "" ? "0" : status.SelectedValue;
                        ddl_transfertype.Attributes["disabled"] = "disabled";
                        ddl_transfertype.SelectedValue = "1";
                    }
                    if (status1.SelectedValue == "3" && Active1.Text == "0")
                    {
                        btn_change.Visible = false;
                        btn_change1.Visible = false;
                        status.SelectedIndex = 1;
                        lbl_transfer.Text = "Transfer";
                        status.Attributes["disabled"] = "disabled";
                        ddl_block.Attributes.Remove("disabled");
                        ddl_floor.Attributes.Remove("disabled");
                        ddl_ward.Attributes.Remove("disabled");
                        Session["BedStatus"] = status1.SelectedValue == "" ? "0" : status1.SelectedValue;
                        ddl_transfertype.Attributes["disabled"] = "disabled";
                        ddl_transfertype.SelectedValue = "2";
                    }
                    if (status1.SelectedValue == "1" && status.SelectedValue == "1")
                    {
                        btn_change.Visible = false;
                        btn_change1.Visible = false;
                        lbl_transfer.Text = "Transfer";
                        status.Attributes.Remove("disabled");
                        status1.Attributes.Remove("disabled");
                        ddl_block.Attributes.Remove("disabled");
                        ddl_floor.Attributes.Remove("disabled");
                        ddl_ward.Attributes.Remove("disabled");
                        Session["BedStatus"] = status.SelectedValue == "" ? "0" : status.SelectedValue;
                        ddl_transfertype.Attributes.Remove("disabled");
                        ddl_transfertype.SelectedValue = "0";
                    }
                }
            }
            else
            {

                Button btn_change = (Button)GvBedAssign.Rows[row.RowIndex].Cells[6].FindControl("btn_update");
                DropDownList status = (DropDownList)GvBedAssign.Rows[row.RowIndex].Cells[6].FindControl("ddl_status");
                Label Active = (Label)GvBedAssign.Rows[row.RowIndex].Cells[6].FindControl("lbl_active");
                btn_change.Visible = false;
                if (status.SelectedValue == "0")
                {
                    btn_change.Visible = false;
                    ddl_block.Attributes["disabled"] = "disabled";
                    ddl_floor.Attributes["disabled"] = "disabled";
                    ddl_ward.Attributes["disabled"] = "disabled";
                    btnsave.Attributes["disabled"] = "disabled";
                }
                else
                {
                    ddl_block.Attributes.Remove("disabled");
                    ddl_floor.Attributes.Remove("disabled");
                    ddl_ward.Attributes.Remove("disabled");
                    btnsave.Attributes.Remove("disabled");
                }
                if (status.SelectedValue == "2" && LogData.RoleID == 1)
                {
                    btn_change.Visible = true;
                }
                if (status.SelectedValue == "2" && LogData.RoleID != 1 && Active.Text == "1")
                {
                    btn_change.Visible = true;
                }
            }
        }
        protected void GvBedAssign_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Int64 ID = Convert.ToInt32(GvBedAssign.DataKeys[e.RowIndex].Values["ID"].ToString());
            System.Web.UI.WebControls.Label BedID = (System.Web.UI.WebControls.Label)GvBedAssign.Rows[e.RowIndex].FindControl("lbl_bedid");
            AdmissionData objadmdata = new AdmissionData();
            AdmissionBO objInfoBO = new AdmissionBO();
            objadmdata.ID = ID;
            objadmdata.BedID = Convert.ToInt32(BedID.Text == "" ? "0" : BedID.Text);
            objadmdata.IPNo = txt_autoipno.Text.Trim();
            objadmdata.EmployeeID = LogData.EmployeeID;
            int result = objInfoBO.UpdateCurrentBedstatus(objadmdata);
            if (result == 1)
            {
                GvBedAssign.DataSource = null;
                GvBedAssign.DataBind();
                ddl_block.Attributes.Remove("disabled");
                ddl_floor.Attributes.Remove("disabled");
                ddl_ward.Attributes.Remove("disabled");
                bindoccupiedbed();
                Messagealert_.ShowMessage(lblmessage, "Change", 1);
                div1.Visible = true;
                div1.Attributes["class"] = "SucessAlert";
            }
            if (result == 2)
            {
                GvBedAssign.DataSource = null;
                GvBedAssign.DataBind();
                bindoccupiedbed();
                Messagealert_.ShowMessage(lblmessage, "Alreadyoccupy", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
        }
        int gridviewrow = 0;
        protected void GvBedAssign_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                gridviewrow = gridviewrow + 1;
                Session["gridrow"] = gridviewrow;
                Label bedID = e.Row.FindControl("lbl_bedID") as Label;
                DropDownList status = e.Row.FindControl("ddl_status") as DropDownList;
                Label occuppancystatus = e.Row.FindControl("lbl_releasedstatus") as Label;
                Button btn_change = e.Row.FindControl("btn_update") as Button;
                Label Active = e.Row.FindControl("lbl_active") as Label;
                DropDownList patactive = e.Row.FindControl("ddl_patactive") as DropDownList;
                status.SelectedIndex = 1;
                if (Active.Text == "1")
                {
                    patactive.SelectedValue = "1";
                    patactive.ForeColor = System.Drawing.Color.White;
                    patactive.BackColor = System.Drawing.Color.Green;
                }
                if (Active.Text == "0")
                {
                    patactive.SelectedValue = "2";
                    patactive.ForeColor = System.Drawing.Color.Red;
                    patactive.BackColor = System.Drawing.Color.Yellow;
                }
                if (LogData.RoleID == 1)
                {
                    patactive.Attributes.Remove("disabled");

                }
                else
                {
                    patactive.Attributes["disabled"] = "disabled";
                }
            }
        }
        protected void chekboxselect_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GvBedAssign.Rows)
            {
                CheckBox cb = (CheckBox)GvBedAssign.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                if (cb.Checked)
                {
                    lbl_block.Visible = true;
                    lbl_floor.Visible = true;
                    lbl_ward.Visible = true;
                    ddl_block.Visible = true;
                    ddl_floor.Visible = true;
                    ddl_ward.Visible = true;
                }
            }
        }
        private List<AdmissionData> GetBedList1(int p)
        {
            AdmissionData objpat = new AdmissionData();
            AdmissionBO objbillingBO = new AdmissionBO();
            objpat.BlockID = Convert.ToInt16(ddl_block.SelectedValue == "0" ? null : ddl_block.SelectedValue);
            objpat.FloorID = Convert.ToInt16(ddl_floor.SelectedValue == "0" ? null : ddl_floor.SelectedValue);
            objpat.WardID = Convert.ToInt16(ddl_ward.SelectedValue == "0" ? null : ddl_ward.SelectedValue);
            return objbillingBO.GetIPDavailablebedList(objpat);
        }
        private List<AdmissionData> GetBedList(int p)
        {
            AdmissionData objpat = new AdmissionData();
            AdmissionBO objbillingBO = new AdmissionBO();
            objpat.IPNo = txt_autoipno.Text.Trim() == "" ? null : txt_autoipno.Text.Trim();
            return objbillingBO.GetBedListByIPNo(objpat);
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
                    div14.Visible = true;
                    div14.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (txtautoIPNo.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "IPNo", 0);
                    div14.Attributes["class"] = "FailAlert";
                    div14.Visible = true;
                    txtautoIPNo.Focus();
                    return;
                }
                else
                {
                    div14.Visible = false;
                }

                List<AdmissionData> objdeposit = GetBedTransferList(0);
                if (objdeposit.Count > 0)
                {
                    gvbedtransferlist.DataSource = objdeposit;
                    gvbedtransferlist.DataBind();
                    gvbedtransferlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult1, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    gvbedtransferlist.DataSource = null;
                    gvbedtransferlist.DataBind();
                    gvbedtransferlist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult1.Visible = false;
                }
            }

            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                div14.Attributes["class"] = "FailAlert";
                div14.Visible = true;
            }
        }
        public List<AdmissionData> GetBedTransferList(int curIndex)
        {
            AdmissionData objpat = new AdmissionData();
            AdmissionBO objbillingBO = new AdmissionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objpat.AdmissionNo = txtautoIPNo.Text.Trim() == "" ? null : txtautoIPNo.Text.Trim();
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objbillingBO.GetBedTransferList(objpat);
        }
        protected void gvbedtransferlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "DeleteEnable", 0);
                        div14.Visible = true;
                        div14.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    AdmissionData objadmin = new AdmissionData();
                    AdmissionBO obadminBO = new AdmissionBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvbedtransferlist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    Label UHID = (Label)gr.Cells[0].FindControl("lbl_UHID");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage2, "Remarks", 0);
                        div14.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objadmin.Remarks = txtremarks.Text;
                    }
                    objadmin.ID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    objadmin.UHID = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                    objadmin.EmployeeID = LogData.EmployeeID;
                    objadmin.HospitalID = LogData.HospitalID;
                    objadmin.IPaddress = LogData.IPaddress;
                    int Result = obadminBO.Deleleteoccupiedbed(objadmin);
                    if (Result == 1)
                    {
                        bindgrid();
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        div14.Attributes["class"] = "SucessAlert";
                        div14.Visible = true;

                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage2, "system", 0);
                        div14.Attributes["class"] = "FailAlert";
                        div14.Visible = true;
                    }

                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult1, "system", 0);
                div14.Attributes["class"] = "FailAlert";
                div14.Visible = true;
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<AdmissionData> AdmissionDetails = GetBedTransferList(0);
            List<BedTransferListDataTOeXCEL> ListexcelData = new List<BedTransferListDataTOeXCEL>();
            int i = 0;
            foreach (AdmissionData row in AdmissionDetails)
            {
                BedTransferListDataTOeXCEL Ecxeclpat = new BedTransferListDataTOeXCEL();
                Ecxeclpat.IPNo = AdmissionDetails[i].IPNo;
                Ecxeclpat.UHID = AdmissionDetails[i].UHID;
                Ecxeclpat.PatientName = AdmissionDetails[i].PatientName;
                Ecxeclpat.BedDetails = AdmissionDetails[i].BedDetails;
                Ecxeclpat.AssignedDate = AdmissionDetails[i].AssignedDate;
                Ecxeclpat.EndDate = AdmissionDetails[i].EndDate;
                Ecxeclpat.NoDays = AdmissionDetails[i].NoDays;
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
                div14.Visible = true;
                div14.Attributes["class"] = "FailAlert";
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
                Messagealert_.ShowMessage(lblresult1, "ExportType", 0);
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
                    gvbedtransferlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvbedtransferlist.Columns[7].Visible = false;
                    gvbedtransferlist.Columns[8].Visible = false;

                    gvbedtransferlist.RenderControl(hw);
                    gvbedtransferlist.HeaderRow.Style.Add("width", "15%");
                    gvbedtransferlist.HeaderRow.Style.Add("font-size", "10px");
                    gvbedtransferlist.Style.Add("text-decoration", "none");
                    gvbedtransferlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvbedtransferlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=OPDBillDetails.pdf");
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
                wb.Worksheets.Add(dt, "Bed Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=BedDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblresult1, "Exported", 1);
                divmsg3.Attributes["class"] = "SucessAlert";
            }
        }
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {

            bindgrid();

        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtautoIPNo.Text = "";
            ddlstatus.SelectedIndex = 0;
            gvbedtransferlist.DataSource = null;
            gvbedtransferlist.DataBind();
            gvbedtransferlist.Visible = false;
            lblresult1.Visible = false;
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage2.Visible = false;
            lblresult1.Visible = false;
            div14.Visible = false;
            div14.Visible = false;
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txt_autoipno.Text = "";
            txtname.Text = "";
            txt_address.Text = "";
            txt_gender.Text = "";
            txt_age.Text = "";
            lblmessage.Visible = false;
            div1.Visible = false;
            lbl_transfer.Text = "Occupy";
            ddl_transfertype.Attributes.Remove("disabled");
            ddl_transfertype.SelectedValue = "0";
            ddl_block.SelectedIndex = 0;
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_block, mstlookup.GetLookupsList(LookupName.BlockType));
            Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetLookupsList(LookupName.IPDWardType));
            Commonfunction.Insertzeroitemindex(ddl_floor);
            div1.Attributes["class"] = "Blank";
            GvBedAssign.DataSource = null;
            GvBedAssign.DataBind();
            GvBedAssign.Visible = false;
            GvBedTransfer.DataSource = null;
            GvBedTransfer.DataBind();
            GvBedTransfer.Visible = false;
            btnsave.Attributes["disabled"] = "disabled";
            txt_contactnumber.Text = "";
            Session["BedStatus"] = 1;

        }
        protected void txtautoIPNo_TextChanged(object sender, EventArgs e)
        {
            if (txtautoIPNo.Text != "")
            {
                bindgrid();
            }
        }
        protected void gvbedtransferlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label enddate = (Label)e.Row.FindControl("lbl_enddate");
                if (enddate.Text == "01-01-0001:12:00:00 AM")
                {
                    enddate.Text = "";
                }
                else
                {
                    enddate.Text = enddate.Text;
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
            if (txt_autoipno.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "IPNo", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_autoipno.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            List<AdmissionData> Listdata = new List<AdmissionData>();
            List<AdmissionData> TransListdata = new List<AdmissionData>();
            AdmissionData objdata = new AdmissionData();
            AdmissionBO objadmissionBO = new AdmissionBO();
            int isRelease = 0;
            try
            {   // get all the record from the gridview
                int rowcount = 0;
                foreach (GridViewRow row in GvBedAssign.Rows)
                {
                    DropDownList ddl_status = (DropDownList)GvBedAssign.Rows[row.RowIndex].Cells[0].FindControl("ddl_status");
                    if (ddl_status.SelectedValue == "1")
                    {
                        rowcount = rowcount + 1;
                    }
                    if (ddl_status.SelectedValue == "2" || ddl_status.SelectedValue == "3")
                    {
                        isRelease = 1;
                        IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);

                        Label Active = (Label)GvBedAssign.Rows[row.RowIndex].Cells[0].FindControl("lbl_active");
                        DropDownList status = (DropDownList)GvBedAssign.Rows[row.RowIndex].Cells[0].FindControl("ddl_status");
                        Label BedID = (Label)GvBedAssign.Rows[row.RowIndex].Cells[0].FindControl("lbl_bedID");
                        Label lbl_entryDate = (Label)GvBedAssign.Rows[row.RowIndex].Cells[0].FindControl("lbl_entryDate");
                        Label lbl_charges = (Label)GvBedAssign.Rows[row.RowIndex].Cells[0].FindControl("lbl_charges");
                        Label lbl_ward = (Label)GvBedAssign.Rows[row.RowIndex].Cells[0].FindControl("lbl_ward");

                        entrydate = lbl_entryDate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(lbl_entryDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);


                        wardId = Convert.ToInt32(lbl_ward.Text == "" ? "0" : lbl_ward.Text);
                        LastBedId = Convert.ToInt32(BedID.Text == "" ? "0" : BedID.Text);
                        lastBedCharge = Convert.ToDecimal(lbl_charges.Text == "" ? "0" : lbl_charges.Text);
                        AdmissionData ObjDetails = new AdmissionData();
                        ObjDetails.BedID = Convert.ToInt32(BedID.Text == "" ? "0" : BedID.Text);
                        ObjDetails.IsReleased = Convert.ToInt32(ddl_status.SelectedValue);
                        ObjDetails.Patient_Active = Convert.ToInt32(Active.Text == "" ? "0" : Active.Text);

                        TransListdata.Add(ObjDetails);
                    }
                }
                objdata.BedTransferXML = XmlConvertor.TransferedBedDatatoXML(TransListdata).ToString();
                // get all the record from the gridview
                int countcheck = 0;
                foreach (GridViewRow row in GvBedTransfer.Rows)
                {
                    CheckBox cb = (CheckBox)GvBedTransfer.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                    if (cb != null)
                    {
                        if (cb.Checked)
                        {
                            IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                            Label Room = (Label)GvBedTransfer.Rows[row.RowIndex].Cells[0].FindControl("lbl_room");
                            Label bedno = (Label)GvBedTransfer.Rows[row.RowIndex].Cells[0].FindControl("lbl_bedno");
                            Label charges = (Label)GvBedTransfer.Rows[row.RowIndex].Cells[0].FindControl("lbl_charges");
                            Label ID = (Label)GvBedTransfer.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                            AdmissionData ObjDetails = new AdmissionData();
                            countcheck = countcheck + 1;
                            ObjDetails.Room = Room.Text == "" ? null : Room.Text;
                            ObjDetails.BedNo = bedno.Text == "" ? "0" : bedno.Text;
                            ObjDetails.Charges = Convert.ToDecimal(charges.Text == "" ? "0" : charges.Text);
                            ObjDetails.BedID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                            ObjDetails.AssignedDate = entrydate;
                            ObjDetails.LastBedCharges = lastBedCharge;
                            ObjDetails.WardID = wardId;
                            ObjDetails.LastBedId = LastBedId;
                            Listdata.Add(ObjDetails);
                        }
                    }
                }
                if (ddl_transfertype.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select bed " + lbl_transfer.Text + " type.", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                objdata.XMLData = XmlConvertor.BedDatatoXML(Listdata).ToString();
                objdata.IPNo = txt_autoipno.Text.Trim() == "" ? null : txt_autoipno.Text.Trim();
                objdata.BlockID = Convert.ToInt16(ddl_block.SelectedValue == "0" ? null : ddl_block.SelectedValue);
                objdata.FloorID = Convert.ToInt16(ddl_floor.SelectedValue == "0" ? null : ddl_floor.SelectedValue);
                objdata.WardID = Convert.ToInt16(ddl_ward.SelectedValue == "0" ? null : ddl_ward.SelectedValue);
                objdata.Patient_Active = Convert.ToInt32(ddl_transfertype.SelectedValue == "1" ? "1" : "0");
                objdata.OccupyBy = ddl_transfertype.SelectedItem.Text.Trim();
                objdata.IsReleased = isRelease;
                objdata.FinancialYearID = LogData.FinancialYearID;
                objdata.EmployeeID = LogData.EmployeeID;
                objdata.HospitalID = LogData.HospitalID;
                objdata.IPaddress = LogData.IPaddress;
                objdata.ActionType = Enumaction.Insert;
                if (countcheck == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "BedTransfer", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (countcheck > 1)
                {
                    Messagealert_.ShowMessage(lblmessage, "Admbedcount", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (rowcount >= 3)
                {
                    Messagealert_.ShowMessage(lblmessage, "BedCount", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                int result = objadmissionBO.UpdateIPDBedTransferDetails(objdata);
                if (result == 1)
                {
                    List<AdmissionData> objdeposit = GetBedList(0);
                    if (objdeposit.Count > 0)
                    {
                        Session["nobed"] = objdeposit.Count;
                        GvBedAssign.DataSource = objdeposit;
                        GvBedAssign.DataBind();
                        GvBedAssign.Visible = true;
                    }
                    else
                    {
                        GvBedAssign.DataSource = null;
                        GvBedAssign.DataBind();
                        GvBedAssign.Visible = true;
                    }
                    Messagealert_.ShowMessage(lblmessage, "bedtransfer", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    GvBedTransfer.DataSource = null;
                    GvBedTransfer.Visible = false;
                    lbl_transfer.Text = "Occupy";
                    ddl_ward.SelectedIndex = 0;
                    btnsave.Attributes["disabled"] = "disabled";
                    ddl_transfertype.SelectedIndex = 0;
                    Session["BedStatus"] = 1;
                    ddl_transfertype.Attributes.Remove("disabled");
                    ddl_transfertype.SelectedValue = "0";
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
        }
        protected void gvbedtransferlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvbedtransferlist.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        protected void gvbedtransferlist_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (LogData.RoleID == 1)
            {
                Int64 ID = Convert.ToInt32(gvbedtransferlist.DataKeys[e.RowIndex].Values["ID"].ToString());
                System.Web.UI.WebControls.Label Ipnumber = (System.Web.UI.WebControls.Label)gvbedtransferlist.Rows[e.RowIndex].FindControl("lbl_ipno");
                List<AdmissionData> Listdata = new List<AdmissionData>();
                AdmissionData objdata = new AdmissionData();
                AdmissionBO objadmissionBO = new AdmissionBO();
                objdata.ID = ID;
                objdata.IPNo = Ipnumber.Text;
                objdata.HospitalID = LogData.HospitalID;
                objdata.EmployeeID = LogData.EmployeeID;
                int result = objadmissionBO.UpdateBedpost(objdata);
                if (result == 1)
                {
                    Messagealert_.ShowMessage(lblmessage2, "BedPost", 1);
                    div14.Visible = true;
                    div14.Attributes["class"] = "SucessAlert";
                }
                if (result == 2)
                {
                    Messagealert_.ShowMessage(lblmessage2, "ABedPost", 0);
                    div14.Visible = true;
                    div14.Attributes["class"] = "FailAlert";
                }
            }
            else
            {
                Messagealert_.ShowMessage(lblmessage2, "BedPostEnable", 0);
                div14.Visible = true;
                div14.Attributes["class"] = "FailAlert";
            }
        }

    }
}