using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedBillData;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.Web.MedCommon;

using System;
using System.Collections.Generic;

namespace Mediqura.Web.MedBills
{
    public partial class ChangeRefferal : BasePage
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
            Commonfunction.PopulateDdl(ddl_patienttype, mstlookup.GetLookupsList(LookupName.OPLabPatientcategory));
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetBillNo(string prefixText, int count, string contextKey)
        {
            LabBillingData Objpaic = new LabBillingData();
            OPDbillingBO objInfoBO = new OPDbillingBO();
            List<LabBillingData> getResult = new List<LabBillingData>();
            Objpaic.BillNo = prefixText;
            getResult = objInfoBO.GetBillNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].BillNo);
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> Getautoreferals(string prefixText, int count, string contextKey)
        {
            ReferalData objreferal = new ReferalData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<ReferalData> getResult = new List<ReferalData>();
            objreferal.Referal = prefixText;
            objreferal.ID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetReferalDetails(objreferal);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].Referal.ToString());
            }
            return list;
        }
        protected void txtbillNo_OnTextChanged(object sender, EventArgs e)
        {           
            OPDbillingBO objBO = new OPDbillingBO();
            LabBillingData objData = new LabBillingData();
            objData.PatientType = Convert.ToInt32(ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue);
            objData.BillNo = txtbillNo.Text.Trim() == "" ? "0" : txtbillNo.Text.Trim();
            List<LabBillingData> result = objBO.GetBillDetailsByBillNo(objData);
            if (result.Count > 0)
            {
                txtPatientDetails.Text = result[0].PatientName;
                txtSourceType.Text = result[0].SourceName;
                txtReferalName.Text = result[0].ReferalName;
            }
            else
            {
                txtPatientDetails.Text = "";
                txtSourceType.Text = "";
                txtReferalName.Text = "";
            }
        }
        protected void ddl_source_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender5.ContextKey = ddl_source.SelectedValue;
            if (ddl_source.SelectedIndex > 0)
            {
                if (ddl_source.SelectedIndex == 1)
                {
                    txt_referal.Text = "Self:1";
                    txt_referal.Attributes["disabled"] = "disabled";
                }
                else
                {
                    txt_referal.Text = "";
                    txt_referal.Attributes.Remove("disabled");
                    txt_referal.Focus();
                }
            }
            else
            {
                txt_referal.Text = "";
                txt_referal.Attributes["disabled"] = "disabled";
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {           
            if (txtbillNo.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Please enter bill number.", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddl_source.SelectedValue == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Please select source type", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if(txt_referal.Text=="")
            {
                Messagealert_.ShowMessage(lblmessage, "Please select referal name", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            OPDbillingBO objBO = new OPDbillingBO();
            LabBillingData objData = new LabBillingData();
            objData.PatientType = Convert.ToInt32(ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue);
            objData.BillNo = txtbillNo.Text.Trim() == "" ? "0" : txtbillNo.Text.Trim();
            objData.SourceID = Convert.ToInt32(ddl_source.SelectedValue == "" ? "0" : ddl_source.SelectedValue);
            objData.ReferalID = Commonfunction.SemicolonSeparation_String_32(txt_referal.Text.Trim() == "" ? "0" : txt_referal.Text.Trim());
            List <LabBillingData> result = objBO.UpdateReferalByBillNo(objData);
            if(result.Count>0)
            {
                Messagealert_.ShowMessage(lblmessage, "Update Successfully", 1);

            }

        }
    }
}