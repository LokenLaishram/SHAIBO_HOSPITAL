using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Mediqura.Web.MedCommon;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace Mediqura.Web.Reports
{
    public partial class ReportViewerInvoice : System.Web.UI.Page
    {
        IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
        ReportDocument crystalReport = new ReportDocument();
        ReportDocument crSubreportDocument = new ReportDocument();
        Sections crSections;
        SubreportObject crSubreportObject;
        ReportObjects crReportObjects;
        CrystalDecisions.ReportAppServer.DataDefModel.ConnectionInfo crConnectionInfo;
        System.Data.Entity.Database crDatabase;
        CrystalDecisions.CrystalReports.Engine.Tables crTables;
        TableLogOnInfo crTableLogOnInfo;
        string constr = ConfigurationManager.ConnectionStrings["SqlConnectionString11"].ConnectionString;
        string ReportUserId = ConfigurationManager.AppSettings["ReportUserId"];
        string ReportServerName = ConfigurationManager.AppSettings["ReportServerName"];
        string ReportDatabase = ConfigurationManager.AppSettings["ReportDatabase"];
        string ReportPassword = ConfigurationManager.AppSettings["ReportPassword"];

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
            Commonfunction common = new Commonfunction();
            string decryptionstring = common.Decrypt(Request["ID"]);
            string baseparam = decryptionstring;
            string reuri = "http://ReportViewer.aspx?" + baseparam + "";
            Uri myUri = new Uri(reuri);


            if (Request["ID"] != null)
            {
                switch (HttpUtility.ParseQueryString(myUri.Query).Get("option"))
                {
                    case "PrintInvoice":
                        DataTable dt = new DataTable();
                        crystalReport.Load(Server.MapPath("Invoice.rpt"));

                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Get_Centres_invoicesRPT";
                                    cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = HttpUtility.ParseQueryString(myUri.Query).Get("from") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("from");
                                    cmd.Parameters.Add("@To", SqlDbType.DateTime).Value = HttpUtility.ParseQueryString(myUri.Query).Get("To") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("To");
                                    cmd.Parameters.Add("@CentreID", SqlDbType.Int).Value = HttpUtility.ParseQueryString(myUri.Query).Get("CentreID") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("CentreID");
                                    cmd.Parameters.Add("@YearID", SqlDbType.Int).Value = HttpUtility.ParseQueryString(myUri.Query).Get("YearID") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("YearID");
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                }
            }
        }
    }
}