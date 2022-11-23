using Mediqura.BOL.MedDashboardBO;
using Mediqura.CommonData.MedDashboardData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedWebservices
{
    public partial class DashboardWebservices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            CompanyDashboardBO objBo=new CompanyDashboardBO();

            List<CompanyDashboardData> dashData = objBo.LoadDashboard();
            var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(dashData); 
            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(json);
            Response.End();
        }
    }
}