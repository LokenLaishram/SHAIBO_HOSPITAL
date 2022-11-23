using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using Mediqura.Utility;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Mediqura.Web.MedOT.Reports
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
                    case "OTStatus":
                        DataTable dt = new DataTable();
                        crystalReport.Load(Server.MapPath("OT_StatusList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_OT_StatusList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@IPNo", SqlDbType.VarChar).Value = Request["IPNo"].ToString() == "" ? null : Request["IPNo"].ToString();
                                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Request["Name"].ToString() == "" ? null : Request["Name"].ToString();
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
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

                    case "OT_RegnProfile":
                        DataTable dt1 = new DataTable();
                        crystalReport.Load(Server.MapPath("OT_regnProfile.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_OT_regn_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@OTNo", SqlDbType.VarChar).Value = Request["OTNo"].ToString() == "" ? null : Request["OTNo"].ToString();
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

                    case "OTList":
                        DataTable dt2 = new DataTable();
                        crystalReport.Load(Server.MapPath("OT_regnList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_OT_List_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@IPNo", SqlDbType.VarChar).Value = Request["IPNo"].ToString() == "" ? null : Request["IPNo"].ToString();
                                    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = Request["PatientName"].ToString() == "" ? null : Request["PatientName"].ToString();
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@OTpassnumber", SqlDbType.VarChar).Value = Request["Otpass"].ToString() == "" ? null : Request["Otpass"].ToString();
                                    cmd.Parameters.Add("@OTtype", SqlDbType.Int).Value = Convert.ToInt32(Request["OtTheater"].ToString() == "" ? null : Request["OtTheater"].ToString());
                                    cmd.Parameters.Add("@OTstatus", SqlDbType.Int).Value = Convert.ToInt32(Request["OtStatus"].ToString() == "" ? "0" : Request["OtStatus"].ToString());
                                    cmd.Parameters.Add("@OTemployeeID", SqlDbType.Int).Value = Convert.ToInt64(Request["OtDoc"].ToString() == "" ? "0" : Request["OtDoc"].ToString());
                                    cmd.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = Request["Otactive"].ToString() == "0" ? true : false;
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

                    case "OTScheduleList":
                        DataTable dt3 = new DataTable();
                        crystalReport.Load(Server.MapPath("OTScheduleList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    string Source = null;
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_OTSchedule_List_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName; 
                                    
                                    Source = Request["PatientName"].ToString();
                                    if (Source.Contains(":"))
                                    {
                                        ID = Source.Substring(Source.LastIndexOf(':') + 1);
                                        cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = Convert.ToInt64(ID == "" ? "0" : ID);
                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = "0";
                                    }
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DoctorID", SqlDbType.Int).Value = Convert.ToInt32(Request["Consultant"].ToString() == "" ? "0" : Request["Consultant"].ToString());
                                    cmd.Parameters.Add("@TheatreID", SqlDbType.Int).Value = Convert.ToInt32(Request["OtTheater"].ToString() == "" ? null : Request["OtTheater"].ToString());
                                    cmd.Parameters.Add("@OTstatus", SqlDbType.Int).Value = Convert.ToInt32(Request["OTStatus"].ToString() == "" ? "0" : Request["OTStatus"].ToString());
                                    //cmd.Parameters.Add("@OTemployeeID", SqlDbType.Int).Value = Convert.ToInt64(Request["OtDoc"].ToString() == "" ? "0" : Request["OtDoc"].ToString());
                                    //cmd.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = Request["Otactive"].ToString() == "0" ? true : false;
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


                }
            }
        }

    }
}