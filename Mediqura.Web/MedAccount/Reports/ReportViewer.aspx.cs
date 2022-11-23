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

namespace Mediqura.Web.MedAccount.Reports
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
                    case "LedgerBookList":
                        DataTable dt = new DataTable();
                        crystalReport.Load(Server.MapPath("LedgerBookList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GetLedgerBookList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@Branch", SqlDbType.Int).Value = Convert.ToInt32(Request["Branch"].ToString() == "" ? "0" : Request["Branch"].ToString());
                                    cmd.Parameters.Add("@Ledger", SqlDbType.Int).Value = Convert.ToInt32(Request["Ledger"].ToString() == "" ? "0" : Request["Ledger"].ToString());
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
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


                    case "DayBookList":
                        DataTable dt1 = new DataTable();
                        crystalReport.Load(Server.MapPath("DayBookList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GetDayBookList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@Branch", SqlDbType.Int).Value = Convert.ToInt32(Request["Branch"].ToString() == "" ? "0" : Request["Branch"].ToString());
                                    cmd.Parameters.Add("@Ledger", SqlDbType.Int).Value = Convert.ToInt32(Request["Ledger"].ToString() == "" ? "0" : Request["Ledger"].ToString());
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

                    case "AccountTransaction":
                        DataTable dt3 = new DataTable();
                        crystalReport.Load(Server.MapPath("VoucherReceipt.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GetTransaction_receipt";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@voucherNumber", SqlDbType.VarChar).Value = Request["voucherNumber"].ToString() == "" ? "0" : Request["voucherNumber"].ToString();
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
                    case "VehiclePass":
                        DataTable dt6 = new DataTable();
                        crystalReport.Load(Server.MapPath("vehiclePass.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GetVehiclePass";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@PassNo", SqlDbType.VarChar).Value = Request["PassNo"].ToString() == "" ? "0" : Request["PassNo"].ToString();
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
                    case "TransactionList":
                        DataTable dt4 = new DataTable();
                        crystalReport.Load(Server.MapPath("TransactionList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GetTransactionList";
                                    cmd.Parameters.Add("@AccountID", SqlDbType.Int).Value = Request["Account"].ToString() == "" ? "0" : Request["Account"].ToString();
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? "0" : Request["DateFrom"].ToString();
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? "0" : Request["DateTo"].ToString();
                                    cmd.Parameters.Add("@Ttype", SqlDbType.Int).Value = Request["TranType"].ToString() == "" ? "0" : Request["TranType"].ToString();
                                    cmd.Parameters.Add("@AccountState", SqlDbType.Int).Value = Request["AccountState"].ToString() == "" ? "0" : Request["AccountState"].ToString();

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
                    case "LabIncomeDetails":
                        DataTable dt7 = new DataTable();
                        crystalReport.Load(Server.MapPath("LabAccountTransactionRpt.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_LAB_AccountTransactionRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@TransactionTypeID", SqlDbType.Int).Value = Convert.ToInt32(Request["TransactionTypeID"].ToString() == "" ? "0" : Request["TransactionTypeID"].ToString());
                                    cmd.Parameters.Add("@Datefrom", SqlDbType.Date).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.Date).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = Request["Status"].ToString() == "0" ? true : false;
                                    cmd.Parameters.Add("@CollectedByID", SqlDbType.BigInt).Value = Convert.ToInt64(Request["Collectedby"].ToString() == "" ? "0" : Request["Collectedby"].ToString());
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt7);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt7);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                }
            }
        }
 
           
   }
}