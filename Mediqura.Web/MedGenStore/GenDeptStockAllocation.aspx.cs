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
using Mediqura.CommonData.MedStore;
using Mediqura.BOL.MedStore;
using Mediqura.Utility;
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;
using Mediqura.BOL.MedGenStoreBO;

namespace Mediqura.Web.MedGenStore
{
    public partial class GenDeptStockAllocation : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                bindgrid();
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_dept, mstlookup.GetLookupsList(LookupName.Gen_Dept));
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

                //if (ddl_stationtype.SelectedIndex == 0)
                //{
                //    Messagealert_.ShowMessage(lblmessage, "Please select nurse station", 0);
                //    div1.Visible = true;
                //    div1.Attributes["class"] = "FailAlert";

                //    txtNames.Focus();
                //    return;
                //}
                //else
                //{
                //    lblmessage.Visible = false;
                //}
                List<GenDeptStockAvailibilityData> obj = GetStockAvailList(0);

                if (obj.Count > 0)
                {
                    gvdeptstock.DataSource = obj;
                    gvdeptstock.DataBind();
                    gvdeptstock.Visible = true;

                }
                else
                {
                    gvdeptstock.DataSource = null;
                    gvdeptstock.DataBind();
                    gvdeptstock.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Visible = true;
            }
        }
        private List<GenDeptStockAvailibilityData> GetStockAvailList(int p)
        {
            GenDeptStockAvailibilityData objpat = new GenDeptStockAvailibilityData();
            GenDeptStockAvailBO objBO = new GenDeptStockAvailBO();
            objpat.deptID = Convert.ToInt32(ddl_dept.SelectedValue == "" ? "0" : ddl_dept.SelectedValue);
            return objBO.GetStockAvailList(objpat);

        }
        protected void btnresets_Click(object sender, System.EventArgs e)
        {
            bindgrid();
            lblmessage.Visible = false;
            lblresult.Visible = false;
            lblresult.Text = "";
            lblmessage.Visible = false;
            div1.Visible = false;
            ddl_dept.SelectedIndex = 0;
            gvdeptstock.DataSource = null;
            gvdeptstock.DataBind();
            gvdeptstock.Visible = true;
            btnUpdate.Attributes.Remove("disabled");


        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            List<GenDeptStockAvailibilityData> objlist = new List<GenDeptStockAvailibilityData>();
            GenDeptStockAvailBO objbo = new GenDeptStockAvailBO();
            GenDeptStockAvailibilityData objdata = new GenDeptStockAvailibilityData();

            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in gvdeptstock.Rows)
                {

                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label id = (Label)gvdeptstock.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label deptid = (Label)gvdeptstock.Rows[row.RowIndex].Cells[0].FindControl("lbl_deptID");
                    CheckBox avail = (CheckBox)gvdeptstock.Rows[row.RowIndex].Cells[0].FindControl("chkselectIsStockAvail");
                    GenDeptStockAvailibilityData ObjDetails = new GenDeptStockAvailibilityData();
                    ObjDetails.ID = Convert.ToInt32(id.Text == "" ? "0" : id.Text);
                    ObjDetails.deptID = Convert.ToInt32(deptid.Text == "" ? "0" : deptid.Text);
                    if (avail.Checked == true)
                    {
                        ObjDetails.stockAvail = 1;
                    }
                    else
                    {
                        ObjDetails.stockAvail = 0;
                    }
                    objlist.Add(ObjDetails);

                }
                objdata.XMLData = XmlConvertor.DeptStockAvailDatatoXML(objlist).ToString();

                objdata.FinancialYearID = LogData.FinancialYearID;
                objdata.EmployeeID = LogData.EmployeeID;
                objdata.HospitalID = LogData.HospitalID;
                objdata.ActionType = Enumaction.Insert;
                int result = objbo.UpdateDeptStockAvails(objdata);
                if (result > 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    btnUpdate.Attributes["disabled"] = "disabled";


                }
                else
                {
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
        protected void gvdeptstock_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cb = (CheckBox)e.Row.FindControl("chkselectIsStockAvail");
                Label Isavail = (Label)e.Row.FindControl("lblavail");
                if (Isavail.Text == "1")
                {
                    cb.Checked = true;
                    //cb.Enabled = false;
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }
    }
}