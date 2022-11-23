using Mediqura.BOL.MedLabBO;
using Mediqura.CommonData.MedLab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedLab
{
    public partial class InvestigationReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String id = Request.QueryString["id"];
            String print = Request.QueryString["p"];
            InvDashboardMasterData objDesignationTypeMasterData = new InvDashboardMasterData();
            InvDashboardMasterBO objDesignationTypeMasterBO = new InvDashboardMasterBO();
            objDesignationTypeMasterData.ID = Convert.ToInt32(id);

            List<InvDashboardMasterData> GetResult = objDesignationTypeMasterBO.GetInvestigationReport(objDesignationTypeMasterData);
            if (GetResult.Count > 0)
            {
                ltReport.Text = generateTemplate(GetResult[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&"), GetResult[0]);
            }

        }
        public string generateTemplate(string template, InvDashboardMasterData objdata)
        {
            string Result = template.Replace("[Report-Date]", objdata.ReportDate.ToString());
            return Result;
        }
   
    }
}