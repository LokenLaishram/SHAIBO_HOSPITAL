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
    public partial class NursingStationDashboard : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                Session["NurseList"] = null;
            }
        }
        protected void ddl_stationtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_stationtype.SelectedIndex > 0)
                {
                    MasterLookupBO mstlookup = new MasterLookupBO();
                    Commonfunction.PopulateDdl(ddl_wardtype, mstlookup.GetWardByStationID(Convert.ToInt32(ddl_stationtype.SelectedValue)));
                    Session["NurseList"] = null;
                    bindgrid();
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetNurseName(string prefixText, int count, string contextKey)
        {
            EmployeeData Objpaic = new EmployeeData();
            EmployeeBO objInfoBO = new EmployeeBO();
            List<EmployeeData> getResult = new List<EmployeeData>();
            Objpaic.EmpName = prefixText;
            getResult = objInfoBO.GetNurseName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmpName.ToString());
            }
            return list;
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.Insertzeroitemindex(ddl_wardtype);
            Commonfunction.PopulateDdl(ddl_stationtype, mstlookup.GetLookupsList(LookupName.StationType));
            Commonfunction.PopulateDdl(ddl_genstock, mstlookup.GetLookupsList(LookupName.NurseGenStock));
            btn_add_nurse.Text = "Add";
            ViewState["ID"] = null;
            List<LookupItem> stocklist = Session["stocklist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["stocklist"];
            Session["stocklist"] = mstlookup.GetLookupsList(LookupName.GenStockType);
        }
        protected void ddl_stocktype_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            Label EmployeeID = (Label)gvnursingstationdetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
            DropDownList StockType = (DropDownList)gvnursingstationdetails.Rows[row.RowIndex].Cells[0].FindControl("ddl_stocktype");
            GenStockEmployeeData objgenstk = new GenStockEmployeeData();
            EmployeeBO objstdBO = new EmployeeBO();
            objgenstk.EmployeeID = Convert.ToInt64(EmployeeID.Text == "" ? "0" : EmployeeID.Text);
            objgenstk.GenSubStockID = Convert.ToInt32(StockType.Text == "" ? "0" : StockType.Text);
            int result = objstdBO.Updategenstockemployee(objgenstk);
            if (result == 1)
            {
                bindgrid();
            }
        }
        protected void gvnursingstationdetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label StockID = (Label)e.Row.FindControl("lblstocktypeID");
                DropDownList ddlstock = (DropDownList)e.Row.FindControl("ddl_stocktype");
                List<LookupItem> stocklist = Session["stocklist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["stocklist"];
                Commonfunction.PopulateDdl(ddlstock, stocklist);
                if (StockID.Text != "0")
                {
                    ddlstock.Items.FindByValue(StockID.Text).Selected = true;
                }
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
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
            bindgrid();
        }
        protected void bindgrid()
        {
            try
            {
                List<NursingStationData> obj = GetNursingStationList(0);
                Session["NurseList"] = obj;
                if (obj.Count > 0)
                {
                    gvnursingstationdetails.DataSource = obj;
                    gvnursingstationdetails.DataBind();
                    gvnursingstationdetails.Visible = true;

                }
                else
                {
                    gvnursingstationdetails.DataSource = null;
                    gvnursingstationdetails.DataBind();
                    gvnursingstationdetails.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Visible = true;
            }
        }
        private List<NursingStationData> GetNursingStationList(int p)
        {
            NursingStationData objpat = new NursingStationData();
            NursingStationBO objBO = new NursingStationBO();
            string ID;
            var source = txtNames.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                objpat.NurseID = Convert.ToInt64(ID);
            }
            objpat.WardID = Convert.ToInt32(ddl_wardtype.SelectedValue == "" ? "0" : ddl_wardtype.SelectedValue);
            objpat.StationTypeID = Convert.ToInt32(ddl_stationtype.SelectedValue == "" ? "0" : ddl_stationtype.SelectedValue);
            objpat.GenStockID = Convert.ToInt32(ddl_genstock.SelectedValue == "" ? "0" : ddl_genstock.SelectedValue);
            return objBO.GetNursingStationList(objpat);
        }
        protected void btnresets_Click(object sender, System.EventArgs e)
        {
            bindgrid();
            lblmessage.Visible = false;
            lblresult.Visible = false;
            lblresult.Text = "";
            lblmessage.Visible = false;
            div1.Visible = false;
            ddl_stationtype.Attributes.Remove("disabled");
            ddl_stationtype.SelectedIndex = 0;
            ddl_genstock.SelectedIndex = 0;
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.Insertzeroitemindex(ddl_wardtype);
            Commonfunction.PopulateDdl(ddl_stationtype, mstlookup.GetLookupsList(LookupName.StationType));
            txtNames.Text = "";
            gvnursingstationdetails.DataSource = null;
            gvnursingstationdetails.DataBind();
            gvnursingstationdetails.Visible = true;
            btn_add_nurse.Text = "Add";
            ViewState["ID"] = null;
        }
        protected void gvnursingstationdetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edits")
                {
                    if (LogData.EditEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "EditEnable", 0);
                        divmsg3.Visible = true;
                        divmsg3.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    NursingStationData objpat = new NursingStationData();
                    NursingStationBO objBO = new NursingStationBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvnursingstationdetails.Rows[i];
                    Label NurseID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    objpat.NurseID = Convert.ToInt64(NurseID.Text == "" ? "0" : NurseID.Text);
                    List<NursingStationData> GetResult = objBO.GetNursingStationList(objpat);
                    if (GetResult.Count > 0)
                    {
                        ddl_stationtype.SelectedValue = GetResult[0].StationTypeID.ToString();
                        MasterLookupBO mstlookup = new MasterLookupBO();
                        Commonfunction.PopulateDdl(ddl_wardtype, mstlookup.GetWardByStationID(Convert.ToInt32(GetResult[0].StationTypeID.ToString() == "" ? "0" : GetResult[0].StationTypeID.ToString())));
                        ddl_wardtype.SelectedValue = GetResult[0].WardID.ToString();
                        ddl_genstock.SelectedValue = GetResult[0].GenStockID.ToString();
                        txtNames.Text = GetResult[0].EmpName.ToString();
                        btn_add_nurse.Text = "Update";
                        ViewState["ID"] = GetResult[0].NurseID;
                    }
                }
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "DeleteEnable", 0);
                        divmsg3.Visible = true;
                        divmsg3.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    NursingStationData objpat = new NursingStationData();
                    NursingStationBO objBO = new NursingStationBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvnursingstationdetails.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    objpat.NurseID = Convert.ToInt32(ID.Text);
                    objpat.EmployeeID = LogData.EmployeeID;
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("lblremarks");
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
                        objpat.Remarks = txtremarks.Text;
                    }
                    int Result = objBO.DeleteNursesByID(objpat);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblresult, "delete", 1);
                        divmsg3.Visible = true;
                        divmsg3.Attributes["class"] = "SucessAlert";
                        bindgrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblresult, "system", 0);
                        divmsg3.Visible = true;
                        divmsg3.Attributes["class"] = "FailAlert";
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg3.Visible = true;
                divmsg3.Attributes["class"] = "FailAlert";
            }
        }
        protected void btn_add_nurse_Click(object sender, EventArgs e)
        {
            addnurse();
        }
        protected void txtNames_TextChanged(object sender, EventArgs e)
        {
            addnurse();
        }
        private void addnurse()
        {
            if (ddl_stationtype.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "NursingStation", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddl_wardtype.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Ward", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddl_genstock.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "GenStock", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            NursingStationBO objbo = new NursingStationBO();
            NursingStationData objdata = new NursingStationData();
            String Nersename = txtNames.Text == "" ? null : txtNames.Text.ToString().Trim();
            Int64 NurseID = 0;
            if (Nersename != null)
            {
                String[] name = Nersename.Split(new[] { ":" }, StringSplitOptions.None);
                NurseID = Convert.ToInt64(name[1]);
            }
            else
            {
                txtNames.Text = "";
            }
            if (NurseID == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Nurse", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            objdata.StationTypeID = Convert.ToInt32(ddl_stationtype.SelectedValue == "" ? "0" : ddl_stationtype.SelectedValue);
            objdata.WardID = Convert.ToInt32(ddl_wardtype.SelectedValue == "" ? "0" : ddl_wardtype.SelectedValue);
            objdata.GenStockID = Convert.ToInt32(ddl_genstock.SelectedValue == "" ? "0" : ddl_genstock.SelectedValue);
            objdata.NurseID = NurseID;
            objdata.FinancialYearID = LogData.FinancialYearID;
            objdata.EmployeeID = LogData.EmployeeID;
            objdata.HospitalID = LogData.HospitalID;
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
                    objdata.NurseID = Convert.ToInt32(ViewState["ID"].ToString());
                }
            }
            int result = objbo.UpdateNursingStationAssignDetails(objdata);
            if (result == 1 || result == 2)
            {
                btn_add_nurse.Text = "Add";
                lblmessage.Visible = true;
                Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                div1.Visible = true;
                div1.Attributes["class"] = "SucessAlert";
                bindgrid();
            }
            if (result == 5)
            {
                Messagealert_.ShowMessage(lblmessage, "Alreadyassign", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
        }
    }
}