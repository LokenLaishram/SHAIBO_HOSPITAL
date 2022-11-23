using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.MedUtilityData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedIPD
{
    public partial class DischargeReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String id = Request.QueryString["id"];
            DischargeData objdata = new DischargeData();
            DischargeBO objstdBO = new DischargeBO();
            objdata.IPNo = id;
            List<DischargeData> objresult = objstdBO.GetDischargeTemplate(objdata);
            if (objresult.Count == 1)
            {
                ltReport.Text = generateTemplate(objresult[0].Template.Replace(@"&lt;", @"<").Replace(@"&gt;", @">").Replace(@"&quot;", @"'").Replace(@"&amp;", @"&"), objresult[0]);
               
            }
            else
            {
                ltReport.Text = null;
            }

        }
        public string generateTemplate(string template, DischargeData objdata)
        {
            string Result = template.Replace("[Discharge Date]", objdata.DischargeDate.ToString());
            return Result;
        }
    }
}