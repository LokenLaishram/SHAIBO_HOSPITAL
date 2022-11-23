using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedLabBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedLab;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedLab
{
    public partial class EndoscopyReport : System.Web.UI.Page
    {
        public string id;
        public string billID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddlverifyby, mstlookup.GetLookupsList(LookupName.EndoscopyDoctor));

                msgDiv.Visible = false;
            }
            id = Request.QueryString["id"];
            billID = Request.QueryString["billID"];
            RadioLabReportVerificationData objData = new RadioLabReportVerificationData();
            RadioLabReportVerificationBO objBO = new RadioLabReportVerificationBO();
            objData.LabID = Convert.ToInt64(id);


            List<RadioLabReportVerificationData> GetResult = objBO.GetRadioTemplateByID(objData);
            if (GetResult.Count > 0)
            {
                if (GetResult[0].isVerified == 1)
                {
                    btn_verify.Visible = false;
                    ddlverifyby.Visible = false;
                }
                else
                {
                    btn_verify.Visible = true;
                    ddlverifyby.Visible = true;
                }
                ltReport.Text = GetResult[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&").Replace("[sign]", GetResult[0].Designation).Replace("[Report-Date]", GetResult[0].ReportDate);
                
            }
        }

        protected void btn_verify_Click(object sender, EventArgs e)
        {
            RadioLabReportVerificationData objData = new RadioLabReportVerificationData();
            RadioLabReportVerificationBO objBO = new RadioLabReportVerificationBO();

            objData.ID = Convert.ToInt32(id);
            objData.billID = Convert.ToInt64(billID);
            objData.VerifyBy = Convert.ToInt64(ddlverifyby.SelectedValue == "" ? "0" : ddlverifyby.SelectedValue);
            int result = objBO.UpdateRadioReportVerification(objData);
            if (result == 1 || result == 2)
            {
                Messagealert_.ShowMessage(lblMessage, result == 1 ? "save" : "update", 1);
                msgDiv.Visible = true;
                btn_verify.Visible = false;
                ddlverifyby.Visible = false;


            }
        }
    }
}