using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedHrData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedPhr
{
    public partial class StockTypeManager : BasePage
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
            Session["stocklist"] = null;
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.Department));
            ddldepartment.SelectedValue = "47";
            ddldepartment.Attributes["disabled"] = "disabled";
            Commonfunction.PopulateDdl(ddlstocktype, mstlookup.GetLookupsList(LookupName.StockType));
            Commonfunction.Insertzeroitemindex(ddlemployee);
            List<LookupItem> stocklist = Session["stocklist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["stocklist"];
            Session["stocklist"] = mstlookup.GetLookupsList(LookupName.StockType);
            Commonfunction.PopulateDdl(ddlemployee, mstlookup.GetEmployeeByDep(47));
        }
        protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlemployee, mstlookup.GetEmployeeByDep(Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue)));
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

            bindgrid(1);
        }
        protected void bindgrid(int page)
        {
            try
            {
                if (ddldepartment.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Department", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                List<MedStockEmployeeData> employeedetail = GetEmployeeData(page);
                if (employeedetail.Count > 0)
                {
                    Gvsubtockmanagement.DataSource = employeedetail;
                    Gvsubtockmanagement.DataBind();
                    Gvsubtockmanagement.Visible = true;
                    btn_update.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + employeedetail[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                }
                else
                {
                    Gvsubtockmanagement.DataSource = null;
                    Gvsubtockmanagement.DataBind();
                    Gvsubtockmanagement.Visible = true;
                    btn_update.Visible = false;
                    lblresult.Visible = false;
                    divmsg3.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<MedStockEmployeeData> GetEmployeeData(int curIndex)
        {
            MedStockEmployeeData objgenstk = new MedStockEmployeeData();
            EmployeeBO objstdBO = new EmployeeBO();

            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objgenstk.EmployeeID = Convert.ToInt64(ddlemployee.SelectedValue == "" ? "0" : ddlemployee.SelectedValue);
            objgenstk.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
            objgenstk.MedSubStockID = Convert.ToInt32(ddlstocktype.SelectedValue == "" ? "0" : ddlstocktype.SelectedValue);
            objgenstk.CurrentIndex = curIndex;
            return objstdBO.GetMedStockEmployees(objgenstk);
            
        }
        protected void Gvsubtockmanagement_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label RequestdID = (Label)e.Row.FindControl("lblrequestenableID");
                CheckBox chkrequestID = (CheckBox)e.Row.FindControl("chkrequestenable");
                Label ApproveID = (Label)e.Row.FindControl("lblapproveenableID");
                CheckBox chkapprovedID = (CheckBox)e.Row.FindControl("chkapproveenable");
                Label HandoverID = (Label)e.Row.FindControl("lblhandoverenableID");
                CheckBox chkhandover = (CheckBox)e.Row.FindControl("chkhandoverenable");
                Label veirfyID = (Label)e.Row.FindControl("lblverifyenableID");
                CheckBox chkverify = (CheckBox)e.Row.FindControl("chkverifyenable");
                Label StockID = (Label)e.Row.FindControl("lblstocktypeID");
                DropDownList ddlstock = (DropDownList)e.Row.FindControl("ddl_stocktype");
                if (RequestdID.Text == "1")
                {
                    chkrequestID.Checked = true;
                }
                else
                {
                    chkrequestID.Checked = false;
                }
                if (ApproveID.Text == "1")
                {
                    chkapprovedID.Checked = true;
                }
                else
                {
                    chkapprovedID.Checked = false;
                }
                if (HandoverID.Text == "1")
                {
                    chkhandover.Checked = true;
                }
                else
                {
                    chkhandover.Checked = false;
                }
                if (veirfyID.Text == "1")
                {
                    chkverify.Checked = true;
                }
                else
                {
                    chkverify.Checked = false;
                }
                List<LookupItem> stocklist = Session["stocklist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["stocklist"];
                Commonfunction.PopulateDdl(ddlstock, stocklist);
                if (StockID.Text != "0")
                {
                    ddlstock.Items.FindByValue(StockID.Text).Selected = true;
                }
            }
        }
        protected void ddl_stocktype_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            Label EmployeeID = (Label)Gvsubtockmanagement.Rows[row.RowIndex].Cells[0].FindControl("lblemployeeID");
            DropDownList StockType = (DropDownList)Gvsubtockmanagement.Rows[row.RowIndex].Cells[0].FindControl("ddl_stocktype");
            MedStockEmployeeData objgenstk = new MedStockEmployeeData();
            EmployeeBO objstdBO = new EmployeeBO();
            objgenstk.EmployeeID = Convert.ToInt64(EmployeeID.Text == "" ? "0" : EmployeeID.Text);
            objgenstk.MedSubStockID = Convert.ToInt32(StockType.Text == "" ? "0" : StockType.Text);
            int result = objstdBO.Updatemedstockemployee(objgenstk);
            if (result == 1)
            {
                bindgrid(1);
            }
        }
        protected void chkverifyenable_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            Label EmployeeID = (Label)Gvsubtockmanagement.Rows[row.RowIndex].Cells[0].FindControl("lblemployeeID");
            CheckBox chkverify = (CheckBox)Gvsubtockmanagement.Rows[row.RowIndex].Cells[0].FindControl("chkverifyenable");
            MedStockEmployeeData objgenstk = new MedStockEmployeeData();
            EmployeeBO objstdBO = new EmployeeBO();
            objgenstk.EmployeeID = Convert.ToInt64(EmployeeID.Text == "" ? "0" : EmployeeID.Text);
            objgenstk.MedItemVerifyEnable = chkverify.Checked ? 1 : 0;
            int result = objstdBO.UpdatemedstockemployeeVerifyEnable(objgenstk);
            if (result == 1)
            {
                bindgrid(1);
            }
        }
        protected void chkrequestenable_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            Label EmployeeID = (Label)Gvsubtockmanagement.Rows[row.RowIndex].Cells[0].FindControl("lblemployeeID");
            CheckBox chkrequest = (CheckBox)Gvsubtockmanagement.Rows[row.RowIndex].Cells[0].FindControl("chkrequestenable");
            MedStockEmployeeData objgenstk = new MedStockEmployeeData();
            EmployeeBO objstdBO = new EmployeeBO();
            objgenstk.EmployeeID = Convert.ToInt64(EmployeeID.Text == "" ? "0" : EmployeeID.Text);
            objgenstk.MedItemRequestEnable = chkrequest.Checked ? 1 : 0;
            int result = objstdBO.UpdatemedstockemployeeRequestEnable(objgenstk);
            if (result == 1)
            {
                bindgrid(1);
            }
        }
        protected void chkapproveenable_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            Label EmployeeID = (Label)Gvsubtockmanagement.Rows[row.RowIndex].Cells[0].FindControl("lblemployeeID");
            CheckBox chkapp = (CheckBox)Gvsubtockmanagement.Rows[row.RowIndex].Cells[0].FindControl("chkapproveenable");
            MedStockEmployeeData objgenstk = new MedStockEmployeeData();
            EmployeeBO objstdBO = new EmployeeBO();
            objgenstk.EmployeeID = Convert.ToInt64(EmployeeID.Text == "" ? "0" : EmployeeID.Text);
            objgenstk.MedItemApproveEnable = chkapp.Checked ? 1 : 0;
            int result = objstdBO.UpdatemedstockemployeeApproveEnable(objgenstk);
            if (result == 1)
            {
                bindgrid(1);
            }
        }
        protected void chkhandoverenable_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            Label EmployeeID = (Label)Gvsubtockmanagement.Rows[row.RowIndex].Cells[0].FindControl("lblemployeeID");
            CheckBox chkhand = (CheckBox)Gvsubtockmanagement.Rows[row.RowIndex].Cells[0].FindControl("chkhandoverenable");
            MedStockEmployeeData objgenstk = new MedStockEmployeeData();
            EmployeeBO objstdBO = new EmployeeBO();
            objgenstk.EmployeeID = Convert.ToInt64(EmployeeID.Text == "" ? "0" : EmployeeID.Text);
            objgenstk.MedItemHandoverEnable = chkhand.Checked ? 1 : 0;
            int result = objstdBO.UpdatemedstockemployeeHandoverEnable(objgenstk);
            if (result == 1)
            {
                bindgrid(1);
            }
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            Gvsubtockmanagement.DataSource = null;
            Gvsubtockmanagement.DataBind();
            lblmessage.Visible = false;
            lblresult.Visible = false;
            btn_update.Visible = false;
            ddlstocktype.SelectedIndex = 0;
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlemployee, mstlookup.GetEmployeeByDep(47));
        }
        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (LogData.SaveEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SaveEnable", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            List<MedStockEmployeeData> listemployee = new List<MedStockEmployeeData>();
            MedStockEmployeeData objemployee = new MedStockEmployeeData();
            EmployeeBO objstdBO = new EmployeeBO();
            try
            {

                foreach (GridViewRow row in Gvsubtockmanagement.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    CheckBox Chkreqenable = (CheckBox)Gvsubtockmanagement.Rows[row.RowIndex].Cells[0].FindControl("chkrequestenable");
                    CheckBox Chkapprovenable = (CheckBox)Gvsubtockmanagement.Rows[row.RowIndex].Cells[0].FindControl("chkapproveenable");
                    CheckBox Chkverifyenable = (CheckBox)Gvsubtockmanagement.Rows[row.RowIndex].Cells[0].FindControl("chkverifyenable");
                    CheckBox Chkhaovereanble = (CheckBox)Gvsubtockmanagement.Rows[row.RowIndex].Cells[0].FindControl("chkhandoverenable");
                    Label employeeID = (Label)Gvsubtockmanagement.Rows[row.RowIndex].Cells[0].FindControl("lblemployeeID");
                    DropDownList ddlstocktype = (DropDownList)Gvsubtockmanagement.Rows[row.RowIndex].Cells[0].FindControl("ddl_stocktype");

                    MedStockEmployeeData objemp = new MedStockEmployeeData();
                    objemp.MedItemRequestEnable = Chkreqenable.Checked ? 1 : 0;
                    objemp.MedItemApproveEnable = Chkapprovenable.Checked ? 1 : 0;
                    objemp.MedItemHandoverEnable = Chkhaovereanble.Checked ? 1 : 0;
                    objemp.MedItemVerifyEnable = Chkverifyenable.Checked ? 1 : 0;
                    objemp.EmployeeID = Convert.ToInt32(employeeID.Text == "" ? "0" : employeeID.Text);
                    objemp.MedSubStockID = Convert.ToInt32(ddlstocktype.SelectedValue == "" ? "0" : ddlstocktype.SelectedValue);
                    listemployee.Add(objemp);
                }
                objemployee.XMLData = XmlConvertor.MedEmployeetoXML(listemployee).ToString();
                objemployee.HospitalID = LogData.HospitalID;
                objemployee.EmployeeID = LogData.EmployeeID;
                objemployee.FinancialYearID = LogData.FinancialYearID;
                int result = objstdBO.UpdateMedEmployees(objemployee);
                if (result == 1)
                {
                    bindgrid(1);
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    divmsg1.Attributes["class"] = "SucessAlert";
                    divmsg1.Visible = true;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                string msg = ex.ToString();
                Messagealert_.ShowMessage(lblmessage, msg, 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
            }
        }
    }
}