using Mediqura.BOL.CommonBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedAnalytics;
using Mediqura.DAL;
using Mediqura.Utility;
using Mediqura.Utility.Util;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedDashboard
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getdata();
            getdashboarddata();
        }
        protected void getdashboarddata()
        {
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = GlobalConstant.MinSQLDateTime;
            DateTime To = System.DateTime.Now;
            SqlParameter[] arParms = new SqlParameter[2];

            arParms[0] = new SqlParameter("@from", SqlDbType.DateTime);
            arParms[0].Value = from;

            arParms[1] = new SqlParameter("@To", SqlDbType.DateTime);
            arParms[1].Value = To;

            SqlDataReader sqlReader = null;
            sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_NHM_Get_Admin_dashboard_information", arParms);
            List<MaindashboardData> ListChartData = ORHelper<MaindashboardData>.FromDataReaderToList(sqlReader);
            if (ListChartData.Count > 0)
            {
                lbl_totalpatienttilldate.Text = ListChartData[0].TotalpatienttillDate.ToString();
                lbl_totalcurrentdate.Text = ListChartData[0].TotaltodayPatient.ToString();
                lbl_currentdayinvoice.Text = ListChartData[0].Totaltodayinvoice.ToString();
                lbl_tilldateinvoice.Text = ListChartData[0].TotalinvoicettillDate.ToString();
                lbl_currentdaytest.Text = ListChartData[0].Totaltodaytest.ToString();
                lbl_tilldatetest.Text = ListChartData[0].TotaltestttillDate.ToString();
                lbl_totalcentre.Text = ListChartData[0].TotalCentre.ToString();
                lbl_totalgenerated.Text = ListChartData[0].TotalReportGenerated.ToString();
                lbl_totalpending.Text = ListChartData[0].TotalReportPending.ToString();
                lbl_totalbillamount.Text = Commonfunction.Getroundingo(ListChartData[0].TotalBillAmount.ToString());
                lbl_totalcollectedamount.Text = Commonfunction.Getroundingo(ListChartData[0].TotalPaidAmount.ToString());
                lbl_totaldueamount.Text = Commonfunction.Getroundingo(ListChartData[0].TotalDueAmount.ToString());
                lbl_dailycollectedamount.Text = Commonfunction.Getroundingo(ListChartData[0].TodayPaidAmount.ToString());
                lbl_dailydueamount.Text = Commonfunction.Getroundingo(ListChartData[0].TodayDueAmount.ToString());
                if (ListChartData[0].TotalReportPending > 0)
                {
                    lbl_totalpending.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lbl_totalpatienttilldate.Text = "0";
                lbl_totalcurrentdate.Text = "0";
                lbl_currentdayinvoice.Text = "0";
                lbl_tilldateinvoice.Text = "0";
                lbl_currentdaytest.Text = "0";
                lbl_tilldatetest.Text = "0";
                lbl_totalcentre.Text = "0";
                lbl_totalgenerated.Text = "0";
                lbl_totalpending.Text = "0";
                lbl_totalbillamount.Text = "0";
                lbl_totalcollectedamount.Text = "0";
                lbl_totaldueamount.Text = "0";
                lbl_dailycollectedamount.Text = "0";
                lbl_dailydueamount.Text = "0";
            }
        }
        protected void getdata()
        {
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = GlobalConstant.MinSQLDateTime;
            DateTime To = System.DateTime.Now;

            SqlParameter[] arParms = new SqlParameter[0];

            SqlDataReader sqlReader = null;
            sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_NHM_daily_hourly_patientcount", arParms);
            List<CommonanalyticData> ListChartData = ORHelper<CommonanalyticData>.FromDataReaderToList(sqlReader);

            StringBuilder hublist = new StringBuilder();
            StringBuilder Count = new StringBuilder();
            int i = 0;
            foreach (CommonanalyticData row in ListChartData)
            {
                if (i != 0)
                {
                    hublist.Append(",");
                    Count.Append(",");
                }
                hublist.Append("\"" + ListChartData[i].Name.ToString() + "\"");
                Count.Append("\"" + ListChartData[i].TotalCount.ToString() + "\"");
                i++;
            }
            ArrayLiterals.Text = "<script language=\"javascript\">" +
           "var ItemArray = [" + hublist + "];" +
           "var QtyArray=[" + Count + "];" +
           "window.onload = function () {" +
           "drawgrap()" +
           "};" +
           "</script>";


        }
    }
}