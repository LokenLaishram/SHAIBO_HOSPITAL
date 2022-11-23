using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedNurseBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedNurseData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Drawing;
using Mediqura.CommonData.AdmissionData;
using Mediqura.BOL.AdmissionBO;
using Mediqura.CommonData.PatientData;
using Mediqura.BOL.PatientBO;
using Mediqura.Utility;

namespace Mediqura.Web.MedNurse
{
    public partial class DailyNightCensus : BasePage
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
            Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetLookupsList(LookupName.IPDWardType));
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnreset_Click(object sender, EventArgs e)
        {

        }

        protected void gvWardNightCensus_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvWardNightCensust_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void ddl_ward_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}