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

namespace Mediqura.Web.MedHR
{
    public partial class EmployeeDesignationAssigner :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                Session["designationList"] = null;
            }

        }
     
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetDesgnName(string prefixText, int count, string contextKey)
        {
            GradeDesgnData Objpaic = new GradeDesgnData();
            GradeDesgnBO objInfoBO = new GradeDesgnBO();
            List<GradeDesgnData> getResult = new List<GradeDesgnData>();
            Objpaic.Designation = prefixText;
            getResult = objInfoBO.GetDesgnName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].Designation.ToString());
            }
            return list;
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_grade, mstlookup.GetLookupsList(LookupName.EmpGrade));

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

                //if (ddl_grade.SelectedIndex == 0)
                //{
                //    Messagealert_.ShowMessage(lblmessage, "Please select grade", 0);
                //    div1.Visible = true;
                //    div1.Attributes["class"] = "FailAlert";

                //    txtDesgn.Focus();
                //    return;
                //}
                //else
                //{
                //    lblmessage.Visible = false;
                //}
                List<GradeDesgnData> obj = GetGradeList(0);
                Session["designationList"] = obj;
                if (obj.Count > 0)
                {
                    gvdesignationdetails.DataSource = obj;
                    gvdesignationdetails.DataBind();
                    gvdesignationdetails.Visible = true;

                }
                else
                {
                    gvdesignationdetails.DataSource = null;
                    gvdesignationdetails.DataBind();
                    gvdesignationdetails.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Visible = true;
            }
        }
        private List<GradeDesgnData> GetGradeList(int p)
        {
            GradeDesgnData objpat = new GradeDesgnData();
            GradeDesgnBO objBO = new GradeDesgnBO();
            string ID;
            var source = txtDesgn.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                objpat.DesignID = Convert.ToInt32(ID);
            }

            //objpat.WardID = Convert.ToInt32(ddl_wardtype.SelectedValue == "" ? "0" : ddl_wardtype.SelectedValue);
            objpat.GradeID = Convert.ToInt32(ddl_grade.SelectedValue == "" ? "0" : ddl_grade.SelectedValue);
            return objBO.GetDesignationList(objpat);

        }
        protected void btnresets_Click(object sender, System.EventArgs e)
        {
            Session["designationList"] = null;
            bindgrid();
            lblmessage.Visible = false;
            lblresult.Visible = false;
            lblresult.Text = "";
            lblmessage.Visible = false;
            div1.Visible = false;
            ddl_grade.Attributes.Remove("disabled");
            ddl_grade.SelectedIndex = 0;
            MasterLookupBO mstlookup = new MasterLookupBO();
            //Commonfunction.Insertzeroitemindex(ddl_wardtype);
            Commonfunction.PopulateDdl(ddl_grade, mstlookup.GetLookupsList(LookupName.EmpGrade));
            txtDesgn.Text = "";
            gvdesignationdetails.DataSource = null;
            gvdesignationdetails.DataBind();
            gvdesignationdetails.Visible = true;


        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            List<GradeDesgnData> objlist = new List<GradeDesgnData>();
            GradeDesgnBO objbo = new GradeDesgnBO();
            GradeDesgnData objdata = new GradeDesgnData();

            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in gvdesignationdetails.Rows)
                {

                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label desgnid = (Label)gvdesignationdetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_desgnID");
                    GradeDesgnData ObjDetails = new GradeDesgnData();

                    ObjDetails.DesignID = Convert.ToInt32(desgnid.Text == "" ? "0" : desgnid.Text);
                    objlist.Add(ObjDetails);

                }
                objdata.XMLData = XmlConvertor.DesignationRecordDatatoXML(objlist).ToString();
                objdata.GradeID = Convert.ToInt32(ddl_grade.SelectedValue == "" ? "0" : ddl_grade.SelectedValue);
                //objdata.WardID = Convert.ToInt32(ddl_wardtype.SelectedValue == "" ? "0" : ddl_wardtype.SelectedValue);
                objdata.FinancialYearID = LogData.FinancialYearID;
                objdata.EmployeeID = LogData.EmployeeID;
                objdata.HospitalID = LogData.HospitalID;
                objdata.ActionType = Enumaction.Insert;
                int result = objbo.UpdateDesignationAssignDetails(objdata);
                if (result > 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    Session["designationList"] = null;
                }
                //else if (result == 5)
                //{
                //    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                //    div1.Visible = true;
                //    div1.Attributes["class"] = "FailAlert";

                //}
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
        protected void gvdesignationdetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvdesignationdetails.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    if (Convert.ToInt64(ID.Text) == 0)
                    {
                        List<GradeDesgnData> designationList = Session["designationList"] == null ? new List<GradeDesgnData>() : (List<GradeDesgnData>)Session["designationList"];
                        designationList.RemoveAt(i);
                        Session["designationList"] = designationList;
                        gvdesignationdetails.DataSource = designationList;
                        gvdesignationdetails.DataBind();
                    }
                    else
                    {
                        if (LogData.DeleteEnable == 0)
                        {
                            Messagealert_.ShowMessage(lblmessage, "DeleteEnable", 0);
                            div1.Visible = true;
                            div1.Attributes["class"] = "FailAlert";
                            return;
                        }
                        else
                        {
                            lblmessage.Visible = false;
                        }
                        GradeDesgnBO objbo = new GradeDesgnBO();
                        GradeDesgnData objdata = new GradeDesgnData();
                        objdata.ID = Convert.ToInt32(ID.Text);
                        objdata.EmployeeID = LogData.EmployeeID;
                        objdata.ActionType = Enumaction.Delete;
                        int Result = objbo.DeleteDesgnDetailsByID(objdata);
                        if (Result == 1)
                        {
                            Messagealert_.ShowMessage(lblmessage, "delete", 1);
                            div1.Visible = true;
                            div1.Attributes["class"] = "SucessAlert";
                            bindgrid();
                        }
                        else
                        {
                            Messagealert_.ShowMessage(lblmessage, "system", 0);
                            div1.Visible = true;
                            div1.Attributes["class"] = "FailAlert";

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
        protected void btn_add_Click(object sender, EventArgs e)
        {
            adddesignation();
        }
        protected void txtDesgn_TextChanged(object sender, EventArgs e)
        {
            adddesignation();
        }
        private void adddesignation()
        {

            if (ddl_grade.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Please select grade", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            List<GradeDesgnData> designationList = Session["designationList"] == null ? new List<GradeDesgnData>() : (List<GradeDesgnData>)Session["designationList"];
            GradeDesgnData objData = new GradeDesgnData();
            Int32 designID = 0;

            String design = txtDesgn.Text == "" ? null : txtDesgn.Text.ToString().Trim();
            if (design != null)
            {
                String[] name = design.Split(new[] { ":" }, StringSplitOptions.None);
                designID = Convert.ToInt32(name[1]);
            }
            else
            {
                txtDesgn.Text = "";
            }
            if (designID == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Please select a staff!", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            foreach (GridViewRow row in gvdesignationdetails.Rows)
            {
                Label lbl_ID = (Label)gvdesignationdetails.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                if (Convert.ToInt64(lbl_ID.Text) == designID)
                {
                    txtDesgn.Text = "";
                    Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txtDesgn.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
            }
            objData.DesignID = designID;
            objData.Grade = ddl_grade.SelectedItem.Text;
            objData.Designation = txtDesgn.Text;

            designationList.Add(objData);
            if (designationList.Count > 0)
            {
                gvdesignationdetails.DataSource = designationList;
                gvdesignationdetails.DataBind();
                gvdesignationdetails.Visible = true;
                txtDesgn.Text = "";
                ddl_grade.Attributes["disabled"] = "disabled";
                Session["designationList"] = designationList;
                txtDesgn.Focus();

            }
            else
            {
                gvdesignationdetails.DataSource = null;
                gvdesignationdetails.DataBind();
                gvdesignationdetails.Visible = true;
            }
        }
    }
}