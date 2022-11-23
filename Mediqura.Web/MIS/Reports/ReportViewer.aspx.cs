using CrystalDecisions.CrystalReports.Engine;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mediqura.Utility;

namespace Mediqura.Web.MIS.Reports
{
    public partial class ReportViewer : BasePage
    {

        IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
        ReportDocument crystalReport = new ReportDocument();
        string constr = ConfigurationManager.ConnectionStrings["SqlConnectionString11"].ConnectionString;
        protected void Page_Unload(Object sender, EventArgs evntArgs)
        {
            crystalReport.Close();
            crystalReport.Dispose();
            crystalReport = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["option"] != null)
            {

                switch (Request["option"].ToString())
                {

                    case "CollectionReport":
                        DataTable dt1 = new DataTable();
                        crystalReport.Load(Server.MapPath("Cashcollection.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Mis_GetTransactionList";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.EmpName;
                                    cmd.Parameters.Add("@ledger", SqlDbType.Int).Value = Convert.ToInt64(Request["ledger"].ToString() == "" ? "0" : Request["ledger"].ToString());
                                    cmd.Parameters.Add("@tranactionType", SqlDbType.Int).Value = Request["Ttype"].ToString() == "" ? "0" : Request["Ttype"].ToString();
                                    cmd.Parameters.Add("@CollectedBy", SqlDbType.BigInt).Value = Request["CollectedBy"].ToString() == "" ? "0" : Request["CollectedBy"].ToString();
                                    cmd.Parameters.Add("@accountState", SqlDbType.Int).Value = Request["status"].ToString() == "" ? "3" : Request["status"].ToString();
                                    cmd.Parameters.Add("@dateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Connection = con;
                                    cmd.CommandTimeout = 10000;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt1);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt1);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;
                    case "UserWise":
                        DataTable dt2 = new DataTable();
                        crystalReport.Load(Server.MapPath("UserwiseCashcollection.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Mis_GetTransactionList";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.EmpName;
                                    cmd.Parameters.Add("@ledger", SqlDbType.Int).Value = Convert.ToInt64(Request["ledger"].ToString() == "" ? "0" : Request["ledger"].ToString());
                                    cmd.Parameters.Add("@tranactionType", SqlDbType.Int).Value = Request["Ttype"].ToString() == "" ? "0" : Request["Ttype"].ToString();
                                    cmd.Parameters.Add("@CollectedBy", SqlDbType.BigInt).Value = Request["CollectedBy"].ToString() == "" ? "0" : Request["CollectedBy"].ToString();
                                    cmd.Parameters.Add("@accountState", SqlDbType.Int).Value = Request["status"].ToString() == "" ? "3" : Request["status"].ToString();
                                    cmd.Parameters.Add("@dateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Connection = con;
                                    cmd.CommandTimeout = 10000;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt2);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt2);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;
                    case "summary":
                        DataTable dt3 = new DataTable();
                        crystalReport.Load(Server.MapPath("TransactionSummaryReport.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Mis_GetTransactionSummaryList";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.EmpName;
                                    cmd.Parameters.Add("@ledger", SqlDbType.Int).Value = Convert.ToInt64(Request["ledger"].ToString() == "" ? "0" : Request["ledger"].ToString());
                                    cmd.Parameters.Add("@tranactionType", SqlDbType.Int).Value = Request["Ttype"].ToString() == "" ? "0" : Request["Ttype"].ToString();
                                    cmd.Parameters.Add("@CollectedBy", SqlDbType.BigInt).Value = Request["CollectedBy"].ToString() == "" ? "0" : Request["CollectedBy"].ToString();
                                    cmd.Parameters.Add("@accountState", SqlDbType.Int).Value = Request["status"].ToString() == "" ? "3" : Request["status"].ToString();
                                    cmd.Parameters.Add("@dateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Connection = con;
                                    cmd.CommandTimeout = 10000;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt3);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt3);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                }
            }
        }

    }

}