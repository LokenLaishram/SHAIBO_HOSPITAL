using CrystalDecisions.CrystalReports.Engine;
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

namespace Mediqura.Web.MedAdmission.Reports
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

                    case "AdmissionProfile":
                        DataTable dt = new DataTable();
                        crystalReport.Load(Server.MapPath("AdmissionProfile.rpt"));

                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_AdmissionProfile_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@IPNo", SqlDbType.VarChar).Value = Request["IPNo"].ToString() == "" ? null : Request["IPNo"].ToString();
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

                    case "AdmissionList":
                        DataTable dt1 = new DataTable();
                        crystalReport.Load(Server.MapPath("AdmissionList.rpt"));

                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_AdmissionList_RPT";

                                    if (Request["PatientName"].ToString() == "")
                                    {
                                        cmd.Parameters.Add("@IPNo", SqlDbType.VarChar).Value = null;
                                    }
                                    else
                                    {
                                        string IPNo;
                                        var source = Request["PatientName"].ToString() == "" ? null : Request["PatientName"].ToString();
                                        if (source.Contains(":"))
                                        {
                                            IPNo = source.Substring(source.LastIndexOf(':') + 1);
                                            cmd.Parameters.Add("@IPNo", SqlDbType.VarChar).Value = IPNo.Trim();
                                        }
                                        else
                                        {
                                            cmd.Parameters.Add("@IPNo", SqlDbType.VarChar).Value = null;
                                        }
                                    }

                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@UHID", SqlDbType.VarChar).Value = Request["UHID"].ToString() == "" ? "0" : Request["UHID"].ToString();
                                    //cmd.Parameters.Add("@IPNo", SqlDbType.VarChar).Value = Request["IPNo"].ToString() == "" ? null : Request["IPNo"].ToString();
                                    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = null;// Request["PatientName"].ToString() == "" ? null : Request["PatientName"].ToString();
                                    cmd.Parameters.Add("@Datefrom", SqlDbType.VarChar).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.VarChar).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = Request["Status"].ToString() == "0" ? true : false;


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

                    case "BedTransferList":
                        DataTable dt2 = new DataTable();
                        crystalReport.Load(Server.MapPath("BedTransferList.rpt"));

                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_BedTransferList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@IPNo", SqlDbType.VarChar).Value = Request["IPNo"].ToString() == "" ? null : Request["IPNo"].ToString();
                                    cmd.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = Request["Status"].ToString() == "0" ? true : false;


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

                    case "DischargeList":
                        DataTable dt3 = new DataTable();
                        crystalReport.Load(Server.MapPath("IntimationList.rpt"));

                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_DischargeList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@IPNo", SqlDbType.VarChar).Value = Request["IPNo"].ToString() == "" ? null : Request["IPNo"].ToString();
                                    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = Request["PatientName"].ToString() == "" ? null : Request["PatientName"].ToString();
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.VarChar).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.VarChar).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = Request["Status"].ToString() == "0" ? true : false;

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

                    case "DischargeIntimation":
                        DataTable dt4 = new DataTable();
                        crystalReport.Load(Server.MapPath("DischargeIntimation.rpt"));

                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_DischargeIntimation_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@IPNo", SqlDbType.VarChar).Value = Request["IPNo"].ToString() == "" ? null : Request["IPNo"].ToString();

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


                    case "BedOccupancyList":
                        DataTable dt5 = new DataTable();
                        crystalReport.Load(Server.MapPath("BedOccupancyList.rpt"));

                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_BedOccupancyList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@WardID", SqlDbType.VarChar).Value = Request["WardID"].ToString() == "" ? "0" : Request["WardID"].ToString();

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