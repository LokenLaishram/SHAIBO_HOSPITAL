using Mediqura.BOL.MedLabBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.MedLab;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.CommonData.PatientData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedLab
{
    public partial class Tat : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetUHID(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.UHID = Convert.ToInt64(prefixText);
            getResult = objInfoBO.GetUHID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].RegDNo.ToString());
            }
            return list;
        }
        protected void txtUHID_TextChanged(object sender, EventArgs e)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.UHID = Convert.ToInt64(txtUHID.Text.Trim() == "" ? "0" : txtUHID.Text.Trim());
            getResult = objInfoBO.GetPatientDetailsByUHID(Objpaic);
            if (getResult.Count > 0)
            {
                txtname.Text = getResult[0].PatientName.ToString();
            }
            else
            {
                txtname.Text = "";
                txtUHID.Focus();
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPatientName(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetLabServices(string prefixText, int count, string contextKey)
        {
            LabServiceMasterData Objpaic = new LabServiceMasterData();
            LabServiceMasterBO objInfoBO = new LabServiceMasterBO();
            List<LabServiceMasterData> getResult = new List<LabServiceMasterData>();
            Objpaic.TestName = prefixText;
            getResult = objInfoBO.GetTestName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].TestName.ToString());
            }
            return list;
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
            bindgrid();
        }
        private void bindgrid()
        {
            try
            {

                List<InvTatData> lstemp = GetInvestigationTat(0);

                if (lstemp.Count > 0)
                {
                    GvInvestigation.DataSource = lstemp;
                    GvInvestigation.DataBind();
                    GvInvestigation.Visible = true;

                }
                else
                {
                    GvInvestigation.DataSource = null;
                    GvInvestigation.DataBind();
                    GvInvestigation.Visible = true;

                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetInv(string prefixText, int count, string contextKey)
        {
            SampleCollectionData Objpaic = new SampleCollectionData();
            LabSampleCollctionBO objInfoBO = new LabSampleCollctionBO();
            List<SampleCollectionData> getResult = new List<SampleCollectionData>();
            Objpaic.Investigationumber = prefixText;
            Objpaic.PatientTypeID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetLabInvestigationno(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].Investigationumber.ToString());
            }
            return list;
        }
        private List<InvTatData> GetInvestigationTat(int p)
        {
            InvTatData objData = new InvTatData();
            InvDashboardMasterBO objitemMasterBO = new InvDashboardMasterBO();
            objData.UHID = Convert.ToInt64(txtUHID.Text == "" ? "0" : txtUHID.Text);
            objData.PatientName = txtname.Text.Trim() == "" ? null : txtname.Text.Trim();
            objData.InvNumber = txt_invno.Text.Trim() == "" ? "" : txt_invno.Text.Trim();
            var source = txt_testname.Text.Trim();
            if (source.Contains(":"))
            {
                string ID1 = source.Substring(source.LastIndexOf(':') + 1);
                objData.TestID = Convert.ToInt32(ID1);
            }
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objData.DateFrom = from;
            objData.DateTo = to;

            return objitemMasterBO.GetInvestigationTat(objData);
        }
        protected void GvInvestigation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvInvestigation.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            clear_all();
        }
        private void clear_all()
        {
            lblmessage.Visible = false;
            txtUHID.Text = "";
            txtname.Text = "";
            txt_testname.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            txt_invno.Text = "";
            GvInvestigation.DataSource = null;
            GvInvestigation.DataBind();
            GvInvestigation.Visible = false;
        }

        protected void GvInvestigation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public string getTemplate()
        {
            return txtreportTemp.Value;
        }
    }
}