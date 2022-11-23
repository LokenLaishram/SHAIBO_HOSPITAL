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

namespace Mediqura.Web.MedOPD.Reports
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
                    case "OPDConsultantSheet":
                        DataTable dt = new DataTable();
                        crystalReport.Load(Server.MapPath("OPDConsultantSheet.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_OPDConsultantSheet_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@BillNo", SqlDbType.VarChar).Value = Convert.ToInt64(Request["BillNo"].ToString() == "" ? "0" : Request["BillNo"].ToString());
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

                    case "OPpatientList":
                        DataTable dt1 = new DataTable();
                        crystalReport.Load(Server.MapPath("OPpatientlist.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_OpdVisitHistoryRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@UHID", SqlDbType.Int).Value = Convert.ToInt64(Request["UHID"].ToString() == "" ? "0" : Request["UHID"].ToString());
                                    cmd.Parameters.Add("@PatientName", SqlDbType.Int).Value = Request["PatientName"].ToString() == "" ? null : Request["PatientName"].ToString();
                                    cmd.Parameters.Add("@DoctorTypeID", SqlDbType.Int).Value = Convert.ToInt32(Request["DoctorType"].ToString() == "" ? "0" : Request["DoctorType"].ToString());
                                    cmd.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = Convert.ToInt32(Request["DepartmentID"].ToString() == "" ? "0" : Request["DepartmentID"].ToString());
                                    cmd.Parameters.Add("@DoctorID", SqlDbType.Int).Value = Convert.ToInt64(Request["DoctorID"].ToString() == "" ? "0" : Request["DoctorID"].ToString());
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
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

                    case "AppoinmentList":
                        DataTable dt2 = new DataTable();
                        crystalReport.Load(Server.MapPath("Appoinmentlist.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_CMS_Get_AppointmentList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@DoctorTypeID", SqlDbType.Int).Value = Convert.ToInt32(Request["DoctorTypeID"].ToString() == "" ? "0" : Request["DoctorTypeID"].ToString());
                                    cmd.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = Convert.ToInt32(Request["DepartmentID"].ToString() == "" ? "0" : Request["DepartmentID"].ToString());
                                    cmd.Parameters.Add("@DoctorID", SqlDbType.Int).Value = Convert.ToInt64(Request["DoctorID"].ToString() == "" ? "0" : Request["DoctorID"].ToString());
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
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

                    case "AppoinmentBookingList":
                        DataTable dt3 = new DataTable();
                        crystalReport.Load(Server.MapPath("AppointmentBooking.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GetPatientList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@Consultant", SqlDbType.Int).Value = Convert.ToInt32(Request["Consultant"].ToString() == "" ? "0" : Request["Consultant"].ToString());
                                    cmd.Parameters.Add("@BookingDate", SqlDbType.DateTime).Value = Request["BookingDate"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["BookingDate"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
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

                    case "HistoryList":
                        DataTable dt4 = new DataTable();
                        crystalReport.Load(Server.MapPath("VisitHistoryList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_PrintOpdVisitHistoryRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@UHID", SqlDbType.Int).Value = Convert.ToInt64(Request["UHID"].ToString() == "" ? "0" : Request["UHID"].ToString());
                                    cmd.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = Convert.ToInt32(Request["DepartmentID"].ToString() == "" ? "0" : Request["DepartmentID"].ToString());
                                    cmd.Parameters.Add("@DoctorID", SqlDbType.Int).Value = Convert.ToInt64(Request["DoctorID"].ToString() == "" ? "0" : Request["DoctorID"].ToString());
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
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

                }
            }
        }
    
    }
}