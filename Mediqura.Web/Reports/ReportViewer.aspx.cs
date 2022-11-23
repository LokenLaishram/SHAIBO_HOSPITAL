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
namespace Mediqura.Web.Reports
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

                    case "RegistrationList":
                        DataTable dt1 = new DataTable();
                        crystalReport.Load(Server.MapPath("PatientList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    var PatientName = Request["PatientName"].ToString() == "" ? null : Request["PatientName"].ToString();

                                    if (PatientName != null)
                                    {
                                        bool isnumeric = PatientName.All(char.IsDigit);
                                        if (isnumeric == false)
                                        {
                                            if (PatientName.Contains(":"))
                                            {
                                                bool isUHIDnumeric = PatientName.Substring(PatientName.LastIndexOf(':') + 1).All(char.IsDigit);
                                                cmd.Parameters.Add("@UHID", SqlDbType.Int).Value = isUHIDnumeric ? Convert.ToInt64(PatientName.Contains(":") ? PatientName.Substring(PatientName.LastIndexOf(':') + 1) : "0") : 0;

                                            }
                                            else
                                            {
                                                cmd.Parameters.Add("@UHID", SqlDbType.Int).Value = 0;

                                            }
                                        }
                                        else
                                        {
                                            cmd.Parameters.Add("@UHID", SqlDbType.Int).Value = 0;

                                        }
                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@UHID", SqlDbType.Int).Value = 0;
                                    }
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GetPatientDetailsRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@LoginEmployeeID", SqlDbType.VarChar).Value = LogData.EmployeeID;
                                    //Convert.ToInt64(Request["UHID"].ToString() == "" ? "0" : Request["UHID"].ToString());
                                    cmd.Parameters.Add("@PatientName", SqlDbType.Int).Value = null;
                                    //cmd.Parameters.Add("@ContactNo", SqlDbType.Int).Value = Request["ContactNo"].ToString() == "" ? "0" : Request["ContactNo"].ToString();
                                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = Request["Status"].ToString() == "0" ? true : false;
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@userID", SqlDbType.Int).Value = Convert.ToInt64(Request["UserID"].ToString() == "" ? "0" : Request["UserID"].ToString());
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
                    case "RegdCard":
                        DataTable dt2 = new DataTable();
                        crystalReport.Load(Server.MapPath("Patientregistrationcard.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_PatientRegistrationCard";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = Convert.ToInt64(Request["UHID"].ToString() == "" ? "0" : Request["UHID"].ToString());
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
                    case "PatientProfile":
                        DataTable dt3 = new DataTable();
                        crystalReport.Load(Server.MapPath("Patientprofile.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "MDQ_Print_PatientProfile";
                                    cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = Convert.ToInt64(Request["UHID"].ToString() == "" ? "0" : Request["UHID"].ToString());
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