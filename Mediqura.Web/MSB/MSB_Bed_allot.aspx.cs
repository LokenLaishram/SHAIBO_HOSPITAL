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

namespace Mediqura.Web.MSB
{
    public partial class MSB_Bed_allot :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initialize();

            }
        }
        public void initialize() {
            bindGrid();
        }
        public void bindGrid() {
            MsbBedAllotedBO objBo = new MsbBedAllotedBO();
            List<MsbBedAllotData> objList = objBo.GetMsbBedAlloted();
            if (objList.Count > 0)
            {
                GVmsbBedAllot.DataSource = objList;
                GVmsbBedAllot.DataBind();
                GVmsbBedAllot.Visible = true;
            }
            else {
                GVmsbBedAllot.DataSource = null;
                GVmsbBedAllot.DataBind();
                GVmsbBedAllot.Visible = false;
            }

        
        }
        protected void btn_reset_Click(object sender, EventArgs e)
        {
                   
            div1.Visible = true;
            lblmessage.Visible = false;
            initialize();
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
                    DropDownList ddl_ward = (DropDownList)GVmsbBedAllot.Rows[row.RowIndex].Cells[0].FindControl("ddl_ward");
                    CheckBox checkBoxSelf = (CheckBox)GVmsbBedAllot.Rows[row.RowIndex].Cells[0].FindControl("checkBoxSelf");
                    CheckBox checkBoxDependent = (CheckBox)GVmsbBedAllot.Rows[row.RowIndex].Cells[0].FindControl("checkBoxDependent");

                    MsbBedAllotData ObjDetails = new MsbBedAllotData();

                    ObjDetails.EmployeeGradeID = Convert.ToInt32(lbl_empGradeID.Text);
                    ObjDetails.BedAllotedID = Convert.ToInt32(ddl_ward.SelectedValue == "0" ? "0" : ddl_ward.Text);
                    ObjDetails.isSelf = Convert.ToInt32(checkBoxSelf.Checked == true ? 1 : 0);
                    ObjDetails.isDependent = Convert.ToInt32(checkBoxDependent.Checked == true ? 1 : 0);
                    ListBed.Add(ObjDetails);

                }
                objdata.XMLData = XmlConvertor.MsbBedAllotationToXml(ListBed).ToString();
                objdata.EmployeeID=LogData.EmployeeID;
                objdata.HospitalID=LogData.HospitalID;
                objdata.FinancialYearID = LogData.FinancialYearID;

                int result = EmployeetypeBO.UpdateMsbBedAllotation(objdata);
                if (result == 1)
                {
                    initialize();
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

        protected void GVmsbBedAllot_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in GVmsbBedAllot.Rows)
            {
               
                Label lbl_alloted_bedID = (Label)GVmsbBedAllot.Rows[row.RowIndex].Cells[0].FindControl("lbl_alloted_bedID");
                DropDownList ddl_ward = (DropDownList)GVmsbBedAllot.Rows[row.RowIndex].Cells[0].FindControl("ddl_ward");
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetLookupsList(LookupName.WardType));
                if(lbl_alloted_bedID.Text!="0"){
                    ddl_ward.SelectedValue = lbl_alloted_bedID.Text;
                }
               
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