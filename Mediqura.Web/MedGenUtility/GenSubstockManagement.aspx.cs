using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedHrData;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedGenUtility
{
    public partial class GenSubstockManagement : BasePage
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
            Commonfunction.PopulateDdl(ddlstocktype, mstlookup.GetLookupsList(LookupName.GenStockType));
            Commonfunction.Insertzeroitemindex(ddlemployee);
            List<LookupItem> stocklist = Session["stocklist"] == null ? new List<LookupItem>() : (List<LookupItem>)Session["stocklist"];
            Session["stocklist"] = mstlookup.GetLookupsList(LookupName.GenStockType);
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
                List<GenStockEmployeeData> employeedetail = GetEmployeeData(page);
                if (employeedetail.Count > 0)
                {
                    Gvsubtockmanagement.DataSource = employeedetail;
                    Gvsubtockmanagement.DataBind();
                    Gvsubtockmanagement.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + employeedetail[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                }
                else
                {
                    Gvsubtockmanagement.DataSource = null;
                    Gvsubtockmanagement.DataBind();
                    Gvsubtockmanagement.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<GenStockEmployeeData> GetEmployeeData(int curIndex)
        {
            GenStockEmployeeData objgenstk = new GenStockEmployeeData();
            EmployeeBO objstdBO = new EmployeeBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objgenstk.EmployeeID = Convert.ToInt64(ddlemployee.SelectedValue == "" ? "0" : ddlemployee.SelectedValue);
            objgenstk.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
            objgenstk.GenSubStockID = Convert.ToInt32(ddlstocktype.SelectedValue == "" ? "0" : ddlstocktype.SelectedValue);
            objgenstk.CurrentIndex = curIndex;
            return objstdBO.GetGenStockEmployees(objgenstk);
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
            GenStockEmployeeData objgenstk = new GenStockEmployeeData();
            EmployeeBO objstdBO = new EmployeeBO();
            objgenstk.EmployeeID = Convert.ToInt64(EmployeeID.Text == "" ? "0" : EmployeeID.Text);
            objgenstk.GenSubStockID = Convert.ToInt32(StockType.Text == "" ? "0" : StockType.Text);
            int result = objstdBO.Updategenstockemployee(objgenstk);
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
            GenStockEmployeeData objgenstk = new GenStockEmployeeData();
            EmployeeBO objstdBO = new EmployeeBO();
            objgenstk.EmployeeID = Convert.ToInt64(EmployeeID.Text == "" ? "0" : EmployeeID.Text);
            objgenstk.GenItemVerifyEnable = chkverify.Checked ? 1 : 0;
            int result = objstdBO.UpdategenstockemployeeVerifyEnable(objgenstk);
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
            GenStockEmployeeData objgenstk = new GenStockEmployeeData();
            EmployeeBO objstdBO = new EmployeeBO();
            objgenstk.EmployeeID = Convert.ToInt64(EmployeeID.Text == "" ? "0" : EmployeeID.Text);
            objgenstk.GenItemRequestEnable = chkrequest.Checked ? 1 : 0;
            int result = objstdBO.UpdategenstockemployeeRequestEnable(objgenstk);
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
            GenStockEmployeeData objgenstk = new GenStockEmployeeData();
            EmployeeBO objstdBO = new EmployeeBO();
            objgenstk.EmployeeID = Convert.ToInt64(EmployeeID.Text == "" ? "0" : EmployeeID.Text);
            objgenstk.GenItemApproveEnable = chkapp.Checked ? 1 : 0;
            int result = objstdBO.UpdategenstockemployeeApproveEnable(objgenstk);
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
            GenStockEmployeeData objgenstk = new GenStockEmployeeData();
            EmployeeBO objstdBO = new EmployeeBO();
            objgenstk.EmployeeID = Convert.ToInt64(EmployeeID.Text == "" ? "0" : EmployeeID.Text);
            objgenstk.GenItemHandoverEnable = chkhand.Checked ? 1 : 0;
            int result = objstdBO.UpdategenstockemployeeHandoverEnable(objgenstk);
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
            ddlstocktype.SelectedIndex = 0;
            ddldepartment.SelectedIndex = 0;
            Commonfunction.Insertzeroitemindex(ddlemployee);
        }
    }
}