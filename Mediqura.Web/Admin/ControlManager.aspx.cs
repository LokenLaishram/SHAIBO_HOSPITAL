using Mediqura.BOL.CommonBO;
using Mediqura.CommonData.Common;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.Admin
{
    public partial class ControlManager : BasePage
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
            Commonfunction.PopulateDdl(ddlroleID, mstlookup.GetLookupsList(LookupName.Roles));
            Commonfunction.Insertzeroitemindex(ddlemployee);
            Commonfunction.Insertzeroitemindex(ddlpages);
        }
        protected void ddlroleID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlroleID.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddlemployee, mstlookup.GetEmployeeByRoleID(Convert.ToInt32(ddlroleID.SelectedValue)));
                Commonfunction.PopulateDdl(ddlpages, mstlookup.GetPagesByRoleID(Convert.ToInt32(ddlroleID.SelectedValue)));

            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        private void bindgrid()
        {
            if (ddlroleID.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Please select Role ID.", 0);
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddlemployee.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Please select Employee.", 0);
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            List<PageControlsData> listcontrols = GetControls(0);
            if (listcontrols.Count > 0)
            {
                GvcontrolList.DataSource = listcontrols;
                GvcontrolList.DataBind();
                GvcontrolList.Visible = true;
                btnupdate.Visible = true;
            }
            else
            {
                GvcontrolList.DataSource = null;
                GvcontrolList.DataBind();
                btnupdate.Visible = false;
            }
        }
        public List<PageControlsData> GetControls(int curIndex)
        {
            PageControlsData objcontrols = new PageControlsData();
            PageControlBO objcontrolBO = new PageControlBO();
            objcontrols.RoleID = Convert.ToInt32(ddlroleID.SelectedValue == "" ? "0" : ddlroleID.SelectedValue);
            objcontrols.EmployeeID = Convert.ToInt32(ddlemployee.SelectedValue == "" ? "0" : ddlemployee.SelectedValue);
            objcontrols.SitemapID = Convert.ToInt32(ddlpages.SelectedValue == "" ? "0" : ddlpages.SelectedValue);
            return objcontrolBO.GetControlList(objcontrols);
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            ddlroleID.SelectedIndex = 0;
            Commonfunction.Insertzeroitemindex(ddlemployee);
            Commonfunction.Insertzeroitemindex(ddlpages);
            GvcontrolList.Visible = false;
            btnupdate.Visible = false;
            lblmessage.Visible = false;

        }
        protected void GvcontrolList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label SaveID = (Label)e.Row.FindControl("lblsave");
                CheckBox chk_save = (CheckBox)e.Row.FindControl("chekboxselect_Save");
                Label UpadateID = (Label)e.Row.FindControl("lblupdate");
                CheckBox chk_update = (CheckBox)e.Row.FindControl("chekboxselect_Update");
                Label EditID = (Label)e.Row.FindControl("lbledit");
                CheckBox chk_edit = (CheckBox)e.Row.FindControl("chekboxselect_Edit");
                Label DeleteID = (Label)e.Row.FindControl("lbldelete");
                CheckBox chk_delete = (CheckBox)e.Row.FindControl("chekboxselect_Delete");
                Label SearchID = (Label)e.Row.FindControl("lblsearch");
                CheckBox chk_search = (CheckBox)e.Row.FindControl("chekboxselect_Search");
                Label PrintID = (Label)e.Row.FindControl("lblprint");
                CheckBox chk_print = (CheckBox)e.Row.FindControl("chekboxselect_Print");
                Label ExportID = (Label)e.Row.FindControl("lblexport");
                CheckBox chk_export = (CheckBox)e.Row.FindControl("chekboxselect_Export");
                Label AmountID = (Label)e.Row.FindControl("lbl_serviceamount");
                CheckBox chk_amount = (CheckBox)e.Row.FindControl("chekboxselect_Amount");

                if (SaveID.Text == "1")
                {
                    chk_save.Checked = true;
                }
                else
                {
                    chk_save.Checked = false;
                }

                if (UpadateID.Text == "1")
                {
                    chk_update.Checked = true;
                }
                else
                {
                    chk_update.Checked = false;
                }
                if (EditID.Text == "1")
                {
                    chk_edit.Checked = true;
                }
                else
                {
                    chk_edit.Checked = false;
                }
                if (DeleteID.Text == "1")
                {
                    chk_delete.Checked = true;
                }
                else
                {
                    chk_delete.Checked = false;
                }
                if (SearchID.Text == "1")
                {
                    chk_search.Checked = true;
                }
                else
                {
                    chk_search.Checked = false;
                }
                if (PrintID.Text == "1")
                {
                    chk_print.Checked = true;
                }
                else
                {
                    chk_print.Checked = false;
                }
                if (ExportID.Text == "1")
                {
                    chk_export.Checked = true;
                }
                else
                {
                    chk_export.Checked = false;
                }
                if (AmountID.Text == "1")
                {
                    chk_amount.Checked = true;
                }
                else
                {
                    chk_amount.Checked = false;
                }
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {

            if (ddlroleID.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Please select RoleID.", 0);
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddlemployee.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Please select Employee.", 0);
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            List<PageControlsData> listcontrols = new List<PageControlsData>();
            PageControlBO objcontrolsBO = new PageControlBO();
            PageControlsData objcontrols = new PageControlsData();
            //DepositBO objstdBO = new DepositBO();
            // int index = 0;
            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in GvcontrolList.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label ID = (Label)GvcontrolList.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    CheckBox Chk_save = (CheckBox)GvcontrolList.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect_Save");
                    CheckBox Chk_update = (CheckBox)GvcontrolList.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect_Update");
                    CheckBox Chk_search = (CheckBox)GvcontrolList.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect_Search");
                    CheckBox Chk_edit = (CheckBox)GvcontrolList.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect_Edit");
                    CheckBox Chk_delete = (CheckBox)GvcontrolList.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect_Delete");
                    CheckBox Chk_print = (CheckBox)GvcontrolList.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect_Print");
                    CheckBox Chk_export = (CheckBox)GvcontrolList.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect_Export");
                    CheckBox Chk_amount = (CheckBox)GvcontrolList.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect_Amount");

                    PageControlsData ObjDetails = new PageControlsData();
                    ObjDetails.SitemapID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    ObjDetails.SaveEnable = Convert.ToInt32(Chk_save.Checked == true ? "1" : "0");
                    ObjDetails.UpdateEnable = Convert.ToInt32(Chk_update.Checked == true ? "1" : "0");
                    ObjDetails.SearchEnable = Convert.ToInt32(Chk_search.Checked == true ? "1" : "0");
                    ObjDetails.EditEnable = Convert.ToInt32(Chk_edit.Checked == true ? "1" : "0");
                    ObjDetails.DeleteEnable = Convert.ToInt32(Chk_delete.Checked == true ? "1" : "0");
                    ObjDetails.PrintEnable = Convert.ToInt32(Chk_print.Checked == true ? "1" : "0");
                    ObjDetails.ExportEnable = Convert.ToInt32(Chk_export.Checked == true ? "1" : "0");
                    ObjDetails.AmountEnable = Convert.ToInt32(Chk_amount.Checked == true ? "1" : "0");
                    listcontrols.Add(ObjDetails);
                }
                objcontrols.XMLData = XmlConvertor.ControllisttoXML(listcontrols).ToString();
                objcontrols.FinancialYearID = LogData.FinancialYearID;
                objcontrols.RoleID = Convert.ToInt32(ddlroleID.SelectedValue == "" ? "0" : ddlroleID.SelectedValue);
                objcontrols.EmployeeID = Convert.ToInt32(ddlemployee.SelectedValue == "" ? "0" : ddlemployee.SelectedValue);
                //objcontrols.EmployeeID = LogData.EmployeeID;
                objcontrols.AddedBy = LogData.AddedBy;
                objcontrols.HospitalID = LogData.HospitalID;
                objcontrols.IsActive = LogData.IsActive;
                objcontrols.IPaddress = LogData.IPaddress;

                int result = objcontrolsBO.Updatepagecontrols(objcontrols);
                if (result > 0)
                {
                    bindgrid();
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
            }
        }

    }
}