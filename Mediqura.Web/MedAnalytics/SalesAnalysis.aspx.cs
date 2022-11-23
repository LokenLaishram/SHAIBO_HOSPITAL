using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedAnalytics;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedAnalytics;
using Mediqura.Utility;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedAnalytics
{
    public partial class SalesAnalysis : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ddlbind();

            }
  
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();

            Commonfunction.PopulateDdl(ddl_month, mstlookup.GetLookupsList(LookupName.month));
       
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
         
           renderData_from_database();
        }
        protected void renderData_from_database()
        {
            StringBuilder strItem = new StringBuilder();
            StringBuilder strQty = new StringBuilder();
            List<SaleAnalyticsGetData> salesAnalytics = GetAnalyticsData(0);
            int i = 0;
            foreach (SaleAnalyticsGetData row in salesAnalytics)
            {
                if (i != 0) {
                    strItem.Append(",");
                    strQty.Append(",");
                }
                strItem.Append("\"" + salesAnalytics[i].ItemName.ToString() + "\"");
                strQty.Append("\"" + salesAnalytics[i].ItemQty.ToString() + "\"");
                i++;
            }
            ArrayLiterals.Text = "<script language=\"javascript\">" +
         "var ItemArray = [" + strItem + "];" +
         " var QtyArray=[" + strQty + "];" +
         " window.onload = function () {"+
          "drawgrap()"+
        "};"+
          "</script>";
           
        }
        public List<SaleAnalyticsGetData> GetAnalyticsData(int curIndex)
        {

            SalesAnalyticsData objAnalyticData = new SalesAnalyticsData();
            SalesAnalyticsBO objAnalyticBO = new SalesAnalyticsBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);

            objAnalyticData.Month = Convert.ToInt32(ddl_month.SelectedValue == "" ? "0" : ddl_month.SelectedValue);
             DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            string datefrom = from.ToString("yyyy-MM-dd");
            string dateto = To.ToString("yyyy-MM-dd");
            from = Convert.ToDateTime(datefrom + " " + "12:01:00 AM");
            To = Convert.ToDateTime(dateto + " " + "11:59:00 PM");
            objAnalyticData.DateFrom = from;
            objAnalyticData.DateTo = To;
            return objAnalyticBO.GetSalesAnalytics(objAnalyticData);

        }

        protected void btnresets_Click(object sender, EventArgs e)
        {
            ddl_month.SelectedIndex = 0;
            txtdatefrom.Text = "";
            txtto.Text = "";
        }
      
    }
}