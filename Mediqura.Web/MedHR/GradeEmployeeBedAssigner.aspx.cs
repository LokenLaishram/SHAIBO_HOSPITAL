using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MSBBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MSBData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedHR
{
    public partial class GradeEmployeeBedAssigner : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                ddlbind();
                Session["BedList"] = null;
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
            bindGrid();

        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetLookupsList(LookupName.WardType));
            Commonfunction.PopulateDdl(ddl_emp_grade, mstlookup.GetLookupsList(LookupName.EmpGrade));
        }
      
        public void bindGrid()
        {
            MsbBedAllotedBO objBo = new MsbBedAllotedBO();
            MsbBedAllotData objData = new MsbBedAllotData();
            objData.EmployeeGradeID = Convert.ToInt32(ddl_emp_grade.SelectedValue == "" ? "0" : ddl_emp_grade.SelectedValue);
            objData.BedAllotedID = Convert.ToInt32(ddl_ward.SelectedValue == "" ? "0" : ddl_ward.SelectedValue);
            List<MsbBedAllotData> objList = objBo.GetMsbBedDetails(objData);
            if (objList.Count > 0)
            {
                GVmsbBedAllot.DataSource = objList;
                GVmsbBedAllot.DataBind();
                GVmsbBedAllot.Visible = true;
            }
            else
            {
                GVmsbBedAllot.DataSource = null;
                GVmsbBedAllot.DataBind();
                GVmsbBedAllot.Visible = false;
            }


        }
        protected void btn_reset_Click(object sender, EventArgs e)
        {

            div1.Visible = true;
            lblmessage.Visible = false;
            GVmsbBedAllot.DataSource = null;
            GVmsbBedAllot.DataBind();
            GVmsbBedAllot.Visible = false;
            ddl_emp_grade.SelectedIndex = 0;
            ddl_ward.SelectedIndex = 0;
            Session["BedList"] = null;
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            addemployee();
        }
        private void addemployee()
        {

            if (ddl_ward.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Please select ward", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddl_emp_grade.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Please select employee grade", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            List<MsbBedAllotData> BedList = Session["BedList"] == null ? new List<MsbBedAllotData>() : (List<MsbBedAllotData>)Session["BedList"];
            MsbBedAllotData objData = new MsbBedAllotData();
            foreach (GridViewRow row in GVmsbBedAllot.Rows)
            {
                Label lbl_BedID = (Label)GVmsbBedAllot.Rows[row.RowIndex].Cells[0].FindControl("lbl_BedID");
                Label lbl_empGradeID = (Label)GVmsbBedAllot.Rows[row.RowIndex].Cells[0].FindControl("lbl_empGradeID");
                if (Convert.ToInt32(lbl_BedID.Text) == Convert.ToInt32(ddl_ward.SelectedValue) && Convert.ToInt32(lbl_empGradeID.Text) == Convert.ToInt32(ddl_emp_grade.SelectedValue))
                {
                    ddl_emp_grade.SelectedValue = "0";
                    Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_emp_grade.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
            }
            objData.EmployeeGrade = ddl_emp_grade.SelectedItem.Text;
            objData.BedAlloted = ddl_ward.SelectedItem.Text;
            objData.EmployeeGradeID = Convert.ToInt32(ddl_emp_grade.SelectedValue == "" ? "0" : ddl_emp_grade.SelectedValue);
            objData.BedAllotedID = Convert.ToInt32(ddl_ward.SelectedValue == "" ? "0" : ddl_ward.SelectedValue);
            BedList.Add(objData);
            if (BedList.Count > 0)
            {
                GVmsbBedAllot.DataSource = BedList;
                GVmsbBedAllot.DataBind();
                GVmsbBedAllot.Visible = true;
                Session["BedList"] = BedList;
            }
            else
            {
                GVmsbBedAllot.DataSource = null;
                GVmsbBedAllot.DataBind();
                GVmsbBedAllot.Visible = true;
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
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

            List<MsbBedAllotData> ListBed = new List<MsbBedAllotData>();
            MsbBedAllotData objdata = new MsbBedAllotData();
            MsbBedAllotedBO EmployeetypeBO = new MsbBedAllotedBO();
            try
            {
                foreach (GridViewRow row in GVmsbBedAllot.Rows)
                {


                    Label lbl_empGradeID = (Label)GVmsbBedAllot.Rows[row.RowIndex].Cells[0].FindControl("lbl_empGradeID");
                    Label lbl_BedID = (Label)GVmsbBedAllot.Rows[row.RowIndex].Cells[0].FindControl("lbl_BedID");
                    CheckBox checkBoxSelf = (CheckBox)GVmsbBedAllot.Rows[row.RowIndex].Cells[0].FindControl("checkBoxSelf");
                    CheckBox checkBoxDependent = (CheckBox)GVmsbBedAllot.Rows[row.RowIndex].Cells[0].FindControl("checkBoxDependent");

                    MsbBedAllotData ObjDetails = new MsbBedAllotData();

                    ObjDetails.EmployeeGradeID = Convert.ToInt32(lbl_empGradeID.Text);
                    ObjDetails.BedAllotedID = Convert.ToInt32(lbl_BedID.Text);
                    ObjDetails.isSelf = Convert.ToInt32(checkBoxSelf.Checked == true ? 1 : 0);
                    ObjDetails.isDependent = Convert.ToInt32(checkBoxDependent.Checked == true ? 1 : 0);
                    ListBed.Add(ObjDetails);

                }
                objdata.XMLData = XmlConvertor.MsbBedAllotationToXml(ListBed).ToString();
                objdata.EmployeeID = LogData.EmployeeID;
                objdata.HospitalID = LogData.HospitalID;
                objdata.FinancialYearID = LogData.FinancialYearID;
                objdata.EmployeeGradeID = Convert.ToInt32(ddl_emp_grade.SelectedValue ==""?"0":ddl_emp_grade.SelectedValue);
         
                int result = EmployeetypeBO.UpdateMsbBedAllotationDetails(objdata);
                if (result == 1)
                {
               
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;
            }
        }
        protected void GVmsbBedAllot_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GVmsbBedAllot.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    if (ID.Text == "0")
                    {

                        List<MsbBedAllotData> BedList = Session["BedList"] == null ? new List<MsbBedAllotData>() : (List<MsbBedAllotData>)Session["BedList"];
                        BedList.RemoveAt(i);
                        if (BedList.Count > 0)
                        {
                            Session["BedList"] = BedList;
                            GVmsbBedAllot.DataSource = BedList;
                            GVmsbBedAllot.DataBind();
                        }
                        else
                        {
                            Session["BedList"] = null;
                            GVmsbBedAllot.DataSource = null;
                            GVmsbBedAllot.DataBind();
                        }
                    }
                    else
                    {
                        MsbBedAllotData objData = new MsbBedAllotData();
                        MsbBedAllotedBO objBO = new MsbBedAllotedBO();
                        objData.ID = Convert.ToInt32(ID.Text);
                        objData.EmployeeID = LogData.EmployeeID;
                        objData.ActionType = Enumaction.Delete;
                        int Result = objBO.DeleteMsbBedAllotationDetailsID(objData);
                        if (Result == 1)
                        {
                            bindGrid();
                        }


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

        protected void GVmsbBedAllot_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in GVmsbBedAllot.Rows)
            {

                Label lbl_alloted_bedID = (Label)GVmsbBedAllot.Rows[row.RowIndex].Cells[0].FindControl("lbl_alloted_bedID");
                Label lbl_empGradeID = (Label)GVmsbBedAllot.Rows[row.RowIndex].Cells[0].FindControl("lbl_empGradeID");
             
                CheckBox checkBoxSelf = (CheckBox)GVmsbBedAllot.Rows[row.RowIndex].Cells[0].FindControl("checkBoxSelf");
                Label lbl_self_check = (Label)GVmsbBedAllot.Rows[row.RowIndex].Cells[0].FindControl("lbl_self_check");

                CheckBox checkBoxDependent = (CheckBox)GVmsbBedAllot.Rows[row.RowIndex].Cells[0].FindControl("checkBoxDependent");
                Label lbl_dependent_check = (Label)GVmsbBedAllot.Rows[row.RowIndex].Cells[0].FindControl("lbl_dependent_check");
                if (lbl_self_check.Text == "1")
                {
                    checkBoxSelf.Checked = true;
                }
                else
                {
                    checkBoxSelf.Checked = false;
                }
                if (lbl_dependent_check.Text == "1")
                {
                    checkBoxDependent.Checked = true;
                }
                else
                {
                    checkBoxDependent.Checked = false;
                }
            }
        }
    }
}