using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.MedLab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedLab
{
    public partial class LabCommentPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String id = Request.QueryString["id"];
            String print = Request.QueryString["p"];
            RadioLabReportVerificationData objData = new RadioLabReportVerificationData();
            LabSampleCollctionBO objresultbo = new LabSampleCollctionBO();
            objData.LabID = Convert.ToInt64(id);
            objData.InVnumber = print;

 

            List<RadioLabReportVerificationData> GetResult = objresultbo.GetLabCommentTemplateByID(objData);
            if (GetResult.Count > 0)
            {
                ltReport.Text = generateTemplate(GetResult[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&"), GetResult[0]);
            }
        }
        public string generateTemplate(string template, RadioLabReportVerificationData objdata)
        {
            string Result = template.Replace("[Report-Date]", objdata.ReportDate.ToString());
            return Result;
        }
    }
}