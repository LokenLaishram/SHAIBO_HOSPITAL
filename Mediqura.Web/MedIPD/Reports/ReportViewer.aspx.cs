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

namespace Mediqura.Web.MedIPD.Reports
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
                    case "IPReceived":
                        DataTable dt = new DataTable();
                        crystalReport.Load(Server.MapPath("IPReceived_Regn.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "MDQ_Print_IPindreceived_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@IndentNo", SqlDbType.VarChar).Value = Request["IndentNo"].ToString() == "" ? null : Request["IndentNo"].ToString();
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

                    case "IPreceivedList":
                        DataTable dt1 = new DataTable();
                        crystalReport.Load(Server.MapPath("IPReceivedList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_IPIndentRecvList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@IndStatus", SqlDbType.Int).Value = Convert.ToInt32(Request["IndStatus"].ToString() == "" ? "0" : Request["IndStatus"].ToString());
                                    cmd.Parameters.Add("@ReceivedBy", SqlDbType.Int).Value = Convert.ToInt32(Request["ReceivedBy"].ToString() == "" ? "0" : Request["ReceivedBy"].ToString());
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

                    case "CurrentPatientList":
                        DataTable dt2 = new DataTable();
                        crystalReport.Load(Server.MapPath("CurrentPatientList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_CurrentPatientList";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.EmpName;
                                    cmd.Parameters.Add("@WardID", SqlDbType.Int).Value = Convert.ToInt32(Request["ward"].ToString() == "" ? "0" : Request["ward"].ToString());
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

                    case "DueReceipt":
                        DataTable dt3 = new DataTable();
                        crystalReport.Load(Server.MapPath("DueReceipt.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_DuePayment_RPT";
                                    cmd.Parameters.Add("@ReceiptNo", SqlDbType.VarChar).Value = Request["ReceiptNo"].ToString() == "" ? "0" : Request["ReceiptNo"].ToString();
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
                    case "IPservicerecpt":
                        DataTable dt5 = new DataTable();
                        crystalReport.Load(Server.MapPath("IPservicereceipt.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_IP_ServiceReceipt";
                                    cmd.Parameters.Add("@Actiontype", SqlDbType.Int).Value = Convert.ToInt32(Request["Actiontype"].ToString() == "" ? "0" : Request["Actiontype"].ToString());
                                    cmd.Parameters.Add("@GroupNumber", SqlDbType.VarChar).Value = Request["Grpnumber"].ToString() == "" ? "0" : Request["Grpnumber"].ToString();
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

                    case "DueCollectionList":

                        DataTable dt6 = new DataTable();
                        crystalReport.Load(Server.MapPath("DueReceiptList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GetPatientDueCollection_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@PatientCategory", SqlDbType.Int).Value = Convert.ToInt32(Request["PatientType"].ToString() == "" ? "" : Request["PatientType"].ToString());
                                    cmd.Parameters.Add("@IPNo", SqlDbType.VarChar).Value = Request["IPNo"].ToString() == "" ? "0" : Request["IPNo"].ToString();
                                    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = Request["PatientName"].ToString() == "" ? "0" : Request["PatientName"].ToString();
                                    cmd.Parameters.Add("@billNo", SqlDbType.VarChar).Value = Request["billNo"].ToString() == "" ? "0" : Request["billNo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt6);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt6);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                }
            }
        }

    }
}