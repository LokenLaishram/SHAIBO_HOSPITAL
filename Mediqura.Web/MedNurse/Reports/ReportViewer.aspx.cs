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

namespace Mediqura.Web.MedNurse.Reports
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
                    case "NurseNotesList":
                        DataTable dt = new DataTable();
                        crystalReport.Load(Server.MapPath("NurseNotes.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GetNurseNotesListRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@IPNo", SqlDbType.VarChar).Value = Request["IPNo"].ToString() == "" ? "0" : Request["IPNo"].ToString();
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

                    case "DailyNursingAssessment":
                        DataTable dtdna = new DataTable();
                        crystalReport.Load(Server.MapPath("DailyNursingAssessment.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GetDailyNursingAssessmentRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value =Convert.ToInt64( Request["ID"].ToString() == "" ? "0" : Request["ID"].ToString());
                                    cmd.Parameters.Add("@IPNo", SqlDbType.VarChar).Value = null; //Request["IPNo"].ToString() == "" ? "0" : Request["IPNo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dtdna);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dtdna);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                    case "SugarChart":
                        DataTable dt1 = new DataTable();
                        crystalReport.Load(Server.MapPath("BloodSugar.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_PrintPatientSugar_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@IPNo", SqlDbType.VarChar).Value = Request["Ipno"].ToString() == "" ? "0" : Request["Ipno"].ToString();
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
                    case "NurseRecordSheet":
                        DataTable dt3 = new DataTable();
                        crystalReport.Load(Server.MapPath("NurseRecordSheetRpt.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    string Source = null;
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_Medi_NurseRecordSheet_RPT";


                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName; 
                                    
                                    Source = Request["PatientName"].ToString();
                                    if (Source.Contains(":"))
                                    {
                                        ID = Source.Substring(Source.LastIndexOf(':') + 1);
                                        cmd.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = ID == "" ? "" : ID;
                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = "";
                                    }
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
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

                    case "InvestigationChart":
                        DataTable dt4 = new DataTable();
                        crystalReport.Load(Server.MapPath("InvestigationChart.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_SearchInvestPatientDetailRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@Year", SqlDbType.Int).Value = Convert.ToInt32(Request["year"].ToString() == "" ? "0" : Request["year"].ToString());
                                    cmd.Parameters.Add("@Month", SqlDbType.Int).Value = Convert.ToInt32(Request["month"].ToString() == "" ? "0" : Request["month"].ToString());
                                    cmd.Parameters.Add("@IPNo", SqlDbType.VarChar).Value = Request["Ipno"].ToString() == "" ? "0" : Request["Ipno"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt4);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt4);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                    case "DrugMedicationList":
                        DataTable dt5 = new DataTable();
                        crystalReport.Load(Server.MapPath("DrugMedicationList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    string Source = null;
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Medi_DrugMedicationListRPT"; 
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;

                                    Source = Request["PatientName"].ToString();
                                    if (Source.Contains(":"))
                                    {
                                        ID = Source.Substring(Source.LastIndexOf(':') + 1);
                                        cmd.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = ID == "" ? "" : ID;
                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = "";
                                    }

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