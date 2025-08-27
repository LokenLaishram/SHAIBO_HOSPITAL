using CrystalDecisions.CrystalReports.Engine;
using Mediqura.CommonData.PatientData;
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

namespace Mediqura.Web.MedLab.Report
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
            Commonfunction common = new Commonfunction();
            string decryptionstring = common.Decrypt(Request["ID"]);
            string baseparam = decryptionstring;
            string reuri = "http://ReportViewer.aspx?" + baseparam + "";
            Uri myUri = new Uri(reuri);


            if (Request["ID"] != null)
            {
                switch (HttpUtility.ParseQueryString(myUri.Query).Get("option"))
                {
                    case "TestRequisition":
                        DataTable dt = new DataTable();
                        crystalReport.Load(Server.MapPath("TestRequisitionform.rpt"));

                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_TestrequisitionformRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@Investigationumber", SqlDbType.VarChar).Value = HttpUtility.ParseQueryString(myUri.Query).Get("Inv") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("Inv");
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
                    case "worksheet":
                        DataTable dt1 = new DataTable();

                        crystalReport.Load(Server.MapPath("Worksheet.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_PrintworksheetRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@Investigationumber", SqlDbType.VarChar).Value = HttpUtility.ParseQueryString(myUri.Query).Get("Inv") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("Inv").ToString();
                                    cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = HttpUtility.ParseQueryString(myUri.Query).Get("UHID").ToString() == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("UHID").ToString();
                                    cmd.Parameters.Add("@SubgroupID", SqlDbType.Int).Value = HttpUtility.ParseQueryString(myUri.Query).Get("SubgrpID").ToString() == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("SubgrpID").ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt1);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt1);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;
                    case "MultipleReport":
                        // ConvertReportToImage();
                        printmultReport();
                        break;
                    case "CultureReport":
                        // ConvertReportToImage();
                        PrintCultureReport();
                        break;
                    case "MultiReport":
                        // ConvertReportToImage();
                        PrintMultiReportwithSameInv();
                        break;
                }
            }
        }
        protected void printmultReport()
        {
            Commonfunction common = new Commonfunction();
            string decryptionstring = common.Decrypt(Request["ID"]);
            string baseparam = decryptionstring;
            string reuri = "http://ReportViewer.aspx?" + baseparam + "";
            Uri myUri = new Uri(reuri);

            DataTable dt2 = new DataTable();
            string TestID = HttpUtility.ParseQueryString(myUri.Query).Get("TestID") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("TestID");
            string TemplateID = HttpUtility.ParseQueryString(myUri.Query).Get("Template") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("Template");

            if (Convert.ToInt32(TemplateID) == 1023)
            {
                crystalReport.Load(Server.MapPath("Culturetemplate.rpt"));
            }
            else if (Convert.ToInt32(TemplateID) == 1024)
            {
                crystalReport.Load(Server.MapPath("Urine.rpt"));
            }
            else
            {
                crystalReport.Load(Server.MapPath("Commontemplate.rpt"));
            }

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_MDQ_Print_Mult_Reports_RPT";
                        cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                        cmd.Parameters.Add("@Investigationumber", SqlDbType.VarChar).Value = HttpUtility.ParseQueryString(myUri.Query).Get("Inv") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("Inv");
                        cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = HttpUtility.ParseQueryString(myUri.Query).Get("UHID") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("UHID");
                        cmd.Parameters.Add("@TestID", SqlDbType.VarChar).Value = HttpUtility.ParseQueryString(myUri.Query).Get("TestID") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("TestID");
                        cmd.Parameters.Add("@IsShowHF", SqlDbType.Int).Value = HttpUtility.ParseQueryString(myUri.Query).Get("showheader") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("showheader");
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt2);
                    }
                }
            }
            crystalReport.SetDataSource(dt2);
            crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");

        }

        protected void PrintMultiReportwithSameInv()
        {
            Commonfunction common = new Commonfunction();
            string decryptionstring = common.Decrypt(Request["ID"]);
            string baseparam = decryptionstring;
            string reuri = "http://ReportViewer.aspx?" + baseparam + "";
            Uri myUri = new Uri(reuri);

            DataTable dt10 = new DataTable();
            string TestID = HttpUtility.ParseQueryString(myUri.Query).Get("TestID") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("TestID");
            string TemplateID = HttpUtility.ParseQueryString(myUri.Query).Get("Template") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("Template");

            
            crystalReport.Load(Server.MapPath("PrintMultipleReport.rpt"));


            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_MDQ_Print_Multiple_ReportsInSinglePaper_RPT";
                        cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                        cmd.Parameters.Add("@Investigationumber", SqlDbType.VarChar).Value = HttpUtility.ParseQueryString(myUri.Query).Get("Inv") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("Inv");
                        cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = HttpUtility.ParseQueryString(myUri.Query).Get("UHID") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("UHID");
                        cmd.Parameters.Add("@TestID", SqlDbType.VarChar).Value = HttpUtility.ParseQueryString(myUri.Query).Get("TestID") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("TestID");
                        cmd.Parameters.Add("@IsShowHF", SqlDbType.Int).Value = HttpUtility.ParseQueryString(myUri.Query).Get("showheader") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("showheader");
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt10);
                    }
                }
            }
            crystalReport.SetDataSource(dt10);
            crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");

        }

        protected void PrintCultureReport()
        {
            Commonfunction common = new Commonfunction();
            string decryptionstring = common.Decrypt(Request["ID"]);
            string baseparam = decryptionstring;
            string reuri = "http://ReportViewer.aspx?" + baseparam + "";
            Uri myUri = new Uri(reuri);

            DataTable dt2 = new DataTable();
            //string template = Request["Template"].ToString() == "" ? null : Request["Template"].ToString().Trim() + ".rpt";
            string template = HttpUtility.ParseQueryString(myUri.Query).Get("Template") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("Template") + ".rpt";
            crystalReport.Load(Server.MapPath(template));
            //crystalReport.Load(Server.MapPath("Growth.rpt"));
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_MDQ_Print_CultureLab_Reports_RPT";
                        cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                        cmd.Parameters.Add("@Investigationumber", SqlDbType.VarChar).Value = HttpUtility.ParseQueryString(myUri.Query).Get("Inv") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("Inv");
                        cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = HttpUtility.ParseQueryString(myUri.Query).Get("UHID") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("UHID");
                        cmd.Parameters.Add("@TestID", SqlDbType.VarChar).Value = HttpUtility.ParseQueryString(myUri.Query).Get("TestID") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("TestID");
                        cmd.Parameters.Add("@IsShowHF", SqlDbType.Int).Value = HttpUtility.ParseQueryString(myUri.Query).Get("showheader") == "" ? null : HttpUtility.ParseQueryString(myUri.Query).Get("showheader");

                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt2);
                    }
                }
            }
            crystalReport.SetDataSource(dt2);
            crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");

        }
    }
}