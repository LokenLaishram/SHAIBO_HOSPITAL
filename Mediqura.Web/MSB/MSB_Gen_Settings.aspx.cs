using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MSBBO;
using Mediqura.CommonData.LoginData;
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
    public partial class MSB_Gen_Settings : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindeligibility();
            }
        }
        private void bindeligibility()
        {
            try
            {
                List<EmployeeTypeData> objdeposit = Employeetypelist(0);
                EmployeeTypeBO EmployeetypeBO = new EmployeeTypeBO();
                List<EmployeeDependentData> objDependent = EmployeetypeBO.GetMsbApplicability();
                if (objDependent.Count > 0) {
                    txt_dependent.Text = objDependent[0].dependent.ToString();
                    txt_numberdependent.Text = (1 + objDependent[0].dependent).ToString();
                    if (objDependent[0].IsExclution == 1)
                    {
                        CheckBoxExclution.Checked = true;
                    }
                    else {
                        CheckBoxExclution.Checked = false;
                    }
                }
                if (objdeposit.Count > 0)
                {
                    Gvemployee.DataSource = objdeposit;
                    Gvemployee.DataBind();
                    Gvemployee.Visible = true;
                }
                else
                {
                    Gvemployee.DataSource = null;
                    Gvemployee.DataBind();
                }
                List<BenefitData> objList = BenefitTypeList(0);
                if (objList.Count > 0)
                {
                    GVBenifits.DataSource = objList;
                    GVBenifits.DataBind();
                    GVBenifits.Visible = true;
                }
                else {
                    GVBenifits.DataSource = null;
                    GVBenifits.DataBind();
                
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<EmployeeTypeData> Employeetypelist(int curIndex)
        {
            EmployeeTypeData objpat = new EmployeeTypeData();
            EmployeeTypeBO objbillingBO = new EmployeeTypeBO();
            return objbillingBO.Getemplyeetypelist(objpat);
        }
        public List<BenefitData> BenefitTypeList(int curIndex)
        {
            BenefitData objData = new BenefitData();
            EmployeeTypeBO objBO = new EmployeeTypeBO();
            return objBO.GetMsbBenefit();
        }
        protected void btn_saveemp_Click(object sender, EventArgs e)
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

            List<EmployeeTypeData> Listbill = new List<EmployeeTypeData>();
            EmployeeTypeData objdata = new EmployeeTypeData();
            EmployeeTypeBO EmployeetypeBO = new EmployeeTypeBO();
            try
            {
                foreach (GridViewRow row in Gvemployee.Rows)
                {
                    CheckBox cb = (CheckBox)Gvemployee.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                    if (cb != null)
                    {
                        
                            IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                            Label EmployeetypeID = (Label)Gvemployee.Rows[row.RowIndex].Cells[0].FindControl("lbl_employeetypeID");
                            CheckBox IsMSB = (CheckBox)Gvemployee.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect");
                            EmployeeTypeData ObjDetails = new EmployeeTypeData();
                            ObjDetails.EmplyeetypeID = Convert.ToInt32(EmployeetypeID.Text);
                            ObjDetails.IsMSB = IsMSB.Checked ? 1 : 0;
                            Listbill.Add(ObjDetails);
                       
                    }
                }
                //objdata.XMLData = XmlConvertor.EmployeetypetoXML(Listbill).ToString();
               int result = EmployeetypeBO.UpdateEmployeeMSBeligibility(objdata);
                if (result == 1)
                {
                    bindeligibility();
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
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
        protected void Gvemployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in Gvemployee.Rows)
            {
                CheckBox cb = (CheckBox)Gvemployee.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                Label IsMSB = (Label)Gvemployee.Rows[row.RowIndex].Cells[0].FindControl("lbl_IsMSB");
                if (IsMSB.Text == "1")
                {
                    cb.Checked = true;
                }
                else
                {
                    cb.Checked = false;
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
            if (txt_dependent.Text.Trim() == "")
            {
                Messagealert_.ShowMessage(lblmessage, "dependentNo", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else {
                lblmessage.Visible = false;  
            }
            EmployeeDependentData objdata = new EmployeeDependentData();
            EmployeeTypeBO EmployeetypeBO = new EmployeeTypeBO();
            objdata.IsExclution=CheckBoxExclution.Checked ? 1 : 0;
            objdata.dependent = Convert.ToInt32(txt_dependent.Text);
            objdata.AddedBy = LogData.AddedBy;
            int result= EmployeetypeBO.UpdateEmployeeMSBapplicability(objdata);
             
            if (result == 1)
            {
                bindeligibility();
                Messagealert_.ShowMessage(lblmessage, "update", 1);
                div1.Visible = true;
                div1.Attributes["class"] = "SucessAlert";
            }
        }

        protected void btn_resertemp_Click(object sender, EventArgs e)
        {
            lblmessage.Visible = false;
            div1.Visible = false;
        }

        protected void btnBenefitSave_Click(object sender, EventArgs e)
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

            List<BenefitData> ListBenefit = new List<BenefitData>();
            BenefitData objdata = new BenefitData();
            EmployeeTypeBO EmployeetypeBO = new EmployeeTypeBO();
            try
            {
                foreach (GridViewRow row in GVBenifits.Rows)
                {
                    
                           
                            Label lbl_beneficaryID = (Label)GVBenifits.Rows[row.RowIndex].Cells[0].FindControl("lbl_beneficaryID");
                            TextBox txt_opd = (TextBox)GVBenifits.Rows[row.RowIndex].Cells[0].FindControl("txt_opd");
                            TextBox txt_investigation = (TextBox)GVBenifits.Rows[row.RowIndex].Cells[0].FindControl("txt_investigation");
                            TextBox txt_ipd = (TextBox)GVBenifits.Rows[row.RowIndex].Cells[0].FindControl("txt_ipd");
                            
                       if (txt_opd.Text.Trim()=="")
                            {
                                Messagealert_.ShowMessage(lblmessage, "msbOPD", 0);
                                div1.Visible = true;
                                div1.Attributes["class"] = "FailAlert";
                                txt_opd.Focus();
                                return;
                            }
                            else
                            {
                                lblmessage.Visible = false;
                            }
                     if (txt_investigation.Text.Trim()=="")
                            {
                                Messagealert_.ShowMessage(lblmessage, "msbInvestigation", 0);
                                div1.Visible = true;
                                div1.Attributes["class"] = "FailAlert";
                                txt_investigation.Focus();
                                return;
                            }
                            else
                            {
                                lblmessage.Visible = false;
                            }
                     if (txt_ipd.Text.Trim()=="")
                            {
                                Messagealert_.ShowMessage(lblmessage, "msbIPD", 0);
                                div1.Visible = true;
                                div1.Attributes["class"] = "FailAlert";
                                txt_ipd.Focus();
                                return;
                            }
                            else
                            {
                                lblmessage.Visible = false;
                            }
                        BenefitData ObjDetails = new BenefitData();

                            ObjDetails.BeneficiaryTypeID = Convert.ToInt32(lbl_beneficaryID.Text);
                            ObjDetails.opd = Convert.ToDouble(txt_opd.Text==""?"0":txt_opd.Text);
                            ObjDetails.investigation = Convert.ToDouble(txt_investigation.Text == "" ? "0" : txt_investigation.Text);
                            ObjDetails.ipd = Convert.ToDouble(txt_ipd.Text == "" ? "0" : txt_ipd.Text);
                            ListBenefit.Add(ObjDetails);
                 
                }
                //objdata.XMLData = XmlConvertor.MSBBenefitToXml(ListBenefit).ToString();
                int result = EmployeetypeBO.UpdateMsbBenefit(objdata);
                if (result == 1)
                {
                    bindeligibility();
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

        protected void btnBenefitReset_Click(object sender, EventArgs e)
        {

        }
    }
}