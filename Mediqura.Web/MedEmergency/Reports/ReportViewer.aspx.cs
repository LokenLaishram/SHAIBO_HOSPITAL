using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using Mediqura.Utility;
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

namespace Mediqura.Web.MedEmergency.Reports
{
    public partial class ReportViewer : BasePage
    {
        IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
        ReportDocument crystalReport = new ReportDocument();
        ReportDocument crSubreportDocument = new ReportDocument();
        Sections crSections;
        SubreportObject crSubreportObject;
        ReportObjects crReportObjects;
        ConnectionInfo crConnectionInfo;
        Database crDatabase;
        Tables crTables;
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
            if (Request["option"] != null)
            {

                switch (Request["option"].ToString())
                {
                    case "EmergencyProfile":
                        DataTable dt = new DataTable();
                        crystalReport.Load(Server.MapPath("EmergencyProfile.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_EmergencyProfile_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@EmergencyNo", SqlDbType.VarChar).Value = Request["EmergencyNo"].ToString() == "" ? null : Request["EmergencyNo"].ToString();
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
                    case "EmrgInterimBill":
                        DataTable dt89 = new DataTable();
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_emergency_Interimbill_DetailsRPT";
                                    cmd.Parameters.Add("@EmrgNumber", SqlDbType.VarChar).Value = Request["Emrgno"].ToString() == "" ? "0" : Request["Emrgno"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt89);
                                }
                            }
                        }
                        crystalReport.Load(Server.MapPath("EmergencyInterimBill.rpt"));
                        crDatabase = crystalReport.Database;
                        crTables = crDatabase.Tables;
                        crConnectionInfo = new ConnectionInfo();
                        crConnectionInfo.ServerName = ReportServerName;
                        crConnectionInfo.DatabaseName = ReportDatabase;
                        crConnectionInfo.UserID = ReportUserId;
                        crConnectionInfo.Password = ReportPassword;
                        foreach (CrystalDecisions.CrystalReports.Engine.Table aTable in crTables)
                        {
                            crTableLogOnInfo = aTable.LogOnInfo;
                            crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                            aTable.ApplyLogOnInfo(crTableLogOnInfo);
                        }
                        // THIS STUFF HERE IS FOR REPORTS HAVING SUBREPORTS 
                        // set the sections object to the current report's section 
                        crSections = crystalReport.ReportDefinition.Sections;
                        // loop through all the sections to find all the report objects 
                        foreach (Section crSection in crSections)
                        {
                            crReportObjects = crSection.ReportObjects;
                            //loop through all the report objects in there to find all subreports 
                            foreach (ReportObject crReportObject in crReportObjects)
                            {
                                if (crReportObject.Kind == ReportObjectKind.SubreportObject)
                                {
                                    crSubreportObject = (SubreportObject)crReportObject;
                                    //open the subreport object and logon as for the general report 
                                    crSubreportDocument = crSubreportObject.OpenSubreport(crSubreportObject.SubreportName);
                                    crDatabase = crSubreportDocument.Database;
                                    crTables = crDatabase.Tables;
                                    foreach (CrystalDecisions.CrystalReports.Engine.Table aTable in crTables)
                                    {
                                        crTableLogOnInfo = aTable.LogOnInfo;
                                        crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                                        aTable.ApplyLogOnInfo(crTableLogOnInfo);
                                    }
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt89);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");

                        break;
                    case "FinalBill":
                        DataTable dt1 = new DataTable();
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_emergency_finalbill_DetailsRPT";
                                    cmd.Parameters.Add("@FinalBill", SqlDbType.VarChar).Value = Request["BillNo"].ToString() == "" ? "0" : Request["BillNo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt1);
                                }
                            }
                        }
                        crystalReport.Load(Server.MapPath("EmergencyFinalBill.rpt"));
                        crDatabase = crystalReport.Database;
                        crTables = crDatabase.Tables;
                        crConnectionInfo = new ConnectionInfo();
                        crConnectionInfo.ServerName = ReportServerName;
                        crConnectionInfo.DatabaseName = ReportDatabase;
                        crConnectionInfo.UserID = ReportUserId;
                        crConnectionInfo.Password = ReportPassword;
                        foreach (CrystalDecisions.CrystalReports.Engine.Table aTable in crTables)
                        {
                            crTableLogOnInfo = aTable.LogOnInfo;
                            crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                            aTable.ApplyLogOnInfo(crTableLogOnInfo);
                        }
                        // THIS STUFF HERE IS FOR REPORTS HAVING SUBREPORTS 
                        // set the sections object to the current report's section 
                        crSections = crystalReport.ReportDefinition.Sections;
                        // loop through all the sections to find all the report objects 
                        foreach (Section crSection in crSections)
                        {
                            crReportObjects = crSection.ReportObjects;
                            //loop through all the report objects in there to find all subreports 
                            foreach (ReportObject crReportObject in crReportObjects)
                            {
                                if (crReportObject.Kind == ReportObjectKind.SubreportObject)
                                {
                                    crSubreportObject = (SubreportObject)crReportObject;
                                    //open the subreport object and logon as for the general report 
                                    crSubreportDocument = crSubreportObject.OpenSubreport(crSubreportObject.SubreportName);
                                    crDatabase = crSubreportDocument.Database;
                                    crTables = crDatabase.Tables;
                                    foreach (CrystalDecisions.CrystalReports.Engine.Table aTable in crTables)
                                    {
                                        crTableLogOnInfo = aTable.LogOnInfo;
                                        crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                                        aTable.ApplyLogOnInfo(crTableLogOnInfo);
                                    }
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt1);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;
                    case "PHRFinalBill":
                        DataTable dt2 = new DataTable();
                        crystalReport.Load(Server.MapPath("PHR_EMRG_Bill.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_EMRGPhr_Bill_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@FinalBill", SqlDbType.VarChar).Value = Convert.ToInt64(Request["BillNo"].ToString() == "" ? "0" : Request["BillNo"].ToString());
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt2);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt2);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;
                    case "EmergencyList":
                        DataTable dt3 = new DataTable();
                        crystalReport.Load(Server.MapPath("EmergencyList.rpt"));

                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_EmrgList_RPT";
                                    string EmgNo;
                                    var source = Request["PatientName"].ToString() == "" ? null : Request["PatientName"].ToString();
                                    if (source.Contains(":"))
                                    {
                                        EmgNo = source.Substring(source.LastIndexOf(':') + 1);
                                        cmd.Parameters.Add("@EMRGNo", SqlDbType.VarChar).Value = EmgNo.Trim();
                                    }
                                    else
                                    {
                                         cmd.Parameters.Add("@EMRGNo", SqlDbType.VarChar).Value  = "";
                                    }

                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    //cmd.Parameters.Add("@EMRGNo", SqlDbType.VarChar).Value = Request["EMRGNo"].ToString() == "" ? null : Request["EMRGNo"].ToString();
                                    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = "";
                                    cmd.Parameters.Add("@Datefrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = Request["Status"].ToString() == "0" ? true : false;
                                    cmd.Parameters.Add("@EmrgDoc", SqlDbType.BigInt).Value = Convert.ToInt64(Request["EmrgDoc"].ToString() == "" ? null : Request["EmrgDoc"].ToString());
                                    cmd.Parameters.Add("@DischargeStatus", SqlDbType.Int).Value = Convert.ToInt32(Request["DischargeStatus"].ToString() == "" ? null : Request["DischargeStatus"].ToString());
                                    cmd.Parameters.Add("@LoginEmployeeID", SqlDbType.BigInt).Value = LogData.EmployeeID;
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt3);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt3);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;
                    case "Emrgfinalbillist":
                        DataTable dt5 = new DataTable();
                        crystalReport.Load(Server.MapPath("EmergencyList.rpt"));

                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_GetEMRGFinalBillList_rpt";
                                    cmd.Parameters.Add("@EmrgNo", SqlDbType.VarChar).Value = Request["Emrgno"].ToString() == "" ? null : Request["Emrgno"].ToString();
                                    cmd.Parameters.Add("@BillNo", SqlDbType.VarChar).Value = Request["BillNo"].ToString() == "" ? null : Request["BillNo"].ToString();
                                    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = Request["PatientName"].ToString() == "" ? null : Request["PatientName"].ToString();
                                    cmd.Parameters.Add("@Datefrom", SqlDbType.DateTime).Value = Request["From"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["From"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["To"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["To"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = Request["Status"].ToString() == "0" ? true : false;
                                    cmd.Parameters.Add("@CollectedByID", SqlDbType.BigInt).Value = Convert.ToInt64(Request["Collected"].ToString() == "" ? "0" : Request["Collected"].ToString());
                                    cmd.Parameters.Add("@Paymode", SqlDbType.Int).Value = Convert.ToInt32(Request["Paymode"].ToString() == "" ? "0" : Request["Paymode"].ToString());
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt5);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt5);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;
                }
            }
        }

    }
}