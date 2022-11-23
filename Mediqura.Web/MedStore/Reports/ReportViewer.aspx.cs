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

namespace Mediqura.Web.MedStore.Reports
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
                    case "StockIssuedList":
                        DataTable dt = new DataTable();
                        crystalReport.Load(Server.MapPath("StockIssuedList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_StockIssuedList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@IssueNo", SqlDbType.BigInt).Value = Convert.ToInt64(Request["IssueNo"].ToString() == "" ? "0" : Request["IssueNo"].ToString());
                                    cmd.Parameters.Add("@IndentNo", SqlDbType.VarChar).Value = Request["IndentNo"].ToString() == "" ? "" : Request["IndentNo"].ToString();
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Stocktype", SqlDbType.Int).Value = Convert.ToInt32(Request["Stocktype"].ToString() == "" ? "0" : Request["Stocktype"].ToString());
                                    cmd.Parameters.Add("@IssuedBy", SqlDbType.BigInt).Value = Convert.ToInt64(Request["IssuedBy"].ToString() == "" ? "0" : Request["IssuedBy"].ToString());
                                    cmd.Parameters.Add("@Handedto", SqlDbType.BigInt).Value = Convert.ToInt64(Request["Handedto"].ToString() == "" ? "0" : Request["Handedto"].ToString());
                                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = Request["Status"].ToString() == "" ? null : Request["Status"].ToString();
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

                    case "StockReturnList":
                        DataTable dt1 = new DataTable();
                        crystalReport.Load(Server.MapPath("StockReturn.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_StockItemReturnRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@ReturnNo", SqlDbType.VarChar).Value = Request["ReturnNo"].ToString() == "" ? null : Request["ReturnNo"].ToString();
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

                    case "StockItemReturnList":
                        DataTable dt2 = new DataTable();
                        crystalReport.Load(Server.MapPath("StockReturnList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_StockReturnList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName; 
                                    cmd.Parameters.Add("@ReturnNo", SqlDbType.VarChar).Value = Request["ReturnNo"].ToString() == "" ? "" : Request["ReturnNo"].ToString();
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@ReturnBy", SqlDbType.BigInt).Value = Convert.ToInt64(Request["ReturnBy"].ToString() == "" ? "0" : Request["ReturnBy"].ToString());
                                    cmd.Parameters.Add("@Handedto", SqlDbType.BigInt).Value = Convert.ToInt64(Request["Handedto"].ToString() == "" ? "0" : Request["Handedto"].ToString());
                                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = Request["Status"].ToString() == "1" ? true : false;
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

                    case "StockIssueReceipt":
                        DataTable dt3 = new DataTable();
                        crystalReport.Load(Server.MapPath("StockIssueReceipt.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_StockIssueReceipt_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@IssueNo", SqlDbType.BigInt).Value = Convert.ToInt64(Request["IssueNo"].ToString() == "" ? "0" : Request["IssueNo"].ToString());
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

                    case "StockStatus":
                        DataTable dt4 = new DataTable();
                        crystalReport.Load(Server.MapPath("StockStatusList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_StockStatusList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = Request["ItemName"].ToString() == "" ? "" : Request["ItemName"].ToString();
                                    cmd.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = Request["BatchNo"].ToString() == "" ? null : Request["BatchNo"].ToString();
                                    cmd.Parameters.Add("@SubStock", SqlDbType.VarChar).Value = Request["SubStock"].ToString() == "" ? "0" : Request["SubStock"].ToString();
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

                    case "OPReturnList":
                        DataTable dt5 = new DataTable();
                        crystalReport.Load(Server.MapPath("OPReturnReceipt.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_OPReturnList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@ReturnNo", SqlDbType.VarChar).Value = Request["ReturnNo"].ToString() == "" ? "" : Request["ReturnNo"].ToString();
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

                    case "OPReturnList1":
                        DataTable dt6 = new DataTable();
                        crystalReport.Load(Server.MapPath("OPReturnList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_OPReturnList1_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@ReturnNo", SqlDbType.VarChar).Value = Request["ReturnNo"].ToString() == "" ? "" : Request["ReturnNo"].ToString();
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = Request["Status"].ToString() == "0" ? true : false;
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

                    case "StockistItemReturnList":
                        DataTable dt7 = new DataTable();
                        crystalReport.Load(Server.MapPath("StockistItemReturnList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_PrintStockistItemReturnList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@Supplier", SqlDbType.VarChar).Value = Request["Supplier"].ToString() == "" ? "" : Request["Supplier"].ToString();
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
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

                    case "StockistReturn":
                        DataTable dt8 = new DataTable();
                        crystalReport.Load(Server.MapPath("StockistReturn.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_STR_GetStockistReturn_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@ReturnNo", SqlDbType.VarChar).Value = Request["ReturnNo"].ToString() == "" ? null : Request["ReturnNo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt8);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt8);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                    case "PurchaseItemCheckList":
                        DataTable dt9 = new DataTable();
                        crystalReport.Load(Server.MapPath("PurchaseItemCheckList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "MDQ_Print_PurchaseItemCheckList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = Request["ItemName"].ToString() == "" ? "" : Request["ItemName"].ToString();
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt9);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt9);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                    case "PurchaseOrder":
                        DataTable dt10 = new DataTable();
                        crystalReport.Load(Server.MapPath("GeneratePurchaseOrder.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "MDQ_Print_PurchaseOrder_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@PONo", SqlDbType.VarChar).Value = Request["PONo"].ToString() == "" ? "" : Request["PONo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt10);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt10);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                    case "POCheckList":
                        DataTable dt11 = new DataTable();
                        crystalReport.Load(Server.MapPath("POCHeckList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_GetPOCheckListRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@PONo", SqlDbType.VarChar).Value = Request["PONo"].ToString() == "" ? "" : Request["PONo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt11);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt11);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                    case "PurchaseOrderList":
                        DataTable dt12 = new DataTable();
                        crystalReport.Load(Server.MapPath("PurchaseOrderList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_GetPurchaseList";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@PONo", SqlDbType.VarChar).Value = Request["PONo"].ToString() == "" ? "" : Request["PONo"].ToString();
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt12);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt12);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                    case "Indent_RegnProfile":
                        DataTable dt13 = new DataTable();
                        crystalReport.Load(Server.MapPath("Indent_Regn.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "MDQ_Print_IndentRaise_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@IndentNo", SqlDbType.VarChar).Value = Request["IndentNo"].ToString() == "" ? "" : Request["IndentNo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt13);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt13);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                    case "Indent_ApprovedProfile":
                        DataTable dt14 = new DataTable();
                        crystalReport.Load(Server.MapPath("Indent_Approved.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "MDQ_Print_IndentRaise_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@IndentNo", SqlDbType.VarChar).Value = Request["IndentNo"].ToString() == "" ? "" : Request["IndentNo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt14);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt14);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                    case "RequestList":
                        DataTable dt15 = new DataTable();
                        crystalReport.Load(Server.MapPath("RequestList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_PrintIndentItemListRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@SubStockID", SqlDbType.Int).Value =Convert.ToInt32(Request["Substock"].ToString() == "" ? "0" : Request["Substock"].ToString());
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@IndentRequestID", SqlDbType.Int).Value =Convert.ToInt32(Request["RequestType"].ToString() == "" ? "0" : Request["RequestType"].ToString());
                                    cmd.Parameters.Add("@IndentNo", SqlDbType.VarChar).Value = Request["IndentNo"].ToString() == "" ? "" : Request["IndentNo"].ToString();
                                    cmd.Parameters.Add("@RequestBy", SqlDbType.BigInt).Value =Convert.ToInt64(Request["RequestBy"].ToString() == "" ? null : Request["RequestBy"].ToString());
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt15);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt15);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                    case "HandOverList":
                        DataTable dt16 = new DataTable();
                        crystalReport.Load(Server.MapPath("HandOvList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_IndentHndOverList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@SubStockID", SqlDbType.Int).Value =Convert.ToInt32(Request["SubStock"].ToString() == "" ? "" : Request["SubStock"].ToString());
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@HandOverTo", SqlDbType.BigInt).Value =Convert.ToInt64(Request["HandOverTo"].ToString() == "" ? null : Request["HandOverTo"].ToString());
                                    cmd.Parameters.Add("@ApprvBy", SqlDbType.BigInt).Value =Convert.ToInt64(Request["Approved"].ToString() == "" ? null : Request["Approved"].ToString());
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt16);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt16);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                    case "Handover":
                        DataTable dt17 = new DataTable();
                        crystalReport.Load(Server.MapPath("HandOver_Regn.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "MDQ_Print_IndentHndOver_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@IndentNo", SqlDbType.VarChar).Value = Request["IndentNo"].ToString() == "" ? "" : Request["IndentNo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt17);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt17);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                    case "IPHandover":
                        DataTable dt18 = new DataTable();
                        crystalReport.Load(Server.MapPath("IPHandOver_Regn.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_IPIndentHndOverRegn_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@IndentNo", SqlDbType.VarChar).Value = Request["IndentNo"].ToString() == "" ? "" : Request["IndentNo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt18);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt18);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                        case "Received":
                        DataTable dt19 = new DataTable();
                        crystalReport.Load(Server.MapPath("Received_Regn.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "MDQ_Print_IndentRecv_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@IndentNo", SqlDbType.VarChar).Value = Request["IndentNo"].ToString() == "" ? "" : Request["IndentNo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt19);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt19);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                        case "IPhandOverList":
                        DataTable dt20 = new DataTable();
                        crystalReport.Load(Server.MapPath("IPIndentApprvList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_IndentHndOverListPHR_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@ApprvBy", SqlDbType.BigInt).Value = Convert.ToInt64(Request["Approved"].ToString() == "" ? null : Request["Approved"].ToString());
                                    cmd.Parameters.Add("@HandOverTo", SqlDbType.BigInt).Value = Convert.ToInt64(Request["HandOverTo"].ToString() == "" ? null : Request["HandOverTo"].ToString());
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt20);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt20);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                        case "ReceivedList":
                        DataTable dt21 = new DataTable();
                        //crystalReport.Load(Server.MapPath("ReceivedList.rpt"));
                        crystalReport.Load(Server.MapPath("IndentReceivedList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_IndentRecvList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@SubStock", SqlDbType.Int).Value = Convert.ToInt32(Request["SubStock"].ToString() == "" ? "" : Request["SubStock"].ToString());
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Received", SqlDbType.BigInt).Value = Convert.ToInt64(Request["Received"].ToString() == "" ? null : Request["Received"].ToString());
                                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = Request["Status"].ToString() == "1" ? true : false;
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt21);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt21);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                        //---------------------------Start Purchase---------------------
                        case "PurchaseRequisitionList":
                        DataTable dt22 = new DataTable();
                        crystalReport.Load(Server.MapPath("PurchaseRequisitionList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GEN_Purchase_PrintPurchaseRequisitionListRPT";
                                    cmd.Parameters.Add("@PurchaseRequisitionTypeID", SqlDbType.Int).Value = Convert.ToInt32(Request["PurchaseRequisitionTypeID"].ToString() == "" ? "" : Request["PurchaseRequisitionTypeID"].ToString());
                                    cmd.Parameters.Add("@RQNumber", SqlDbType.VarChar).Value = Request["RQNumber"].ToString() == "" ? "" : Request["RQNumber"].ToString();
                                    cmd.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = Request["ItemName"].ToString() == "" ? "" : Request["ItemName"].ToString();
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@RQStatusID", SqlDbType.Int).Value = Convert.ToInt32(Request["RQStatusID"].ToString() == "" ? "0" : Request["RQStatusID"].ToString());
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt22);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt22);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                    //---------------------------End Purchase---------------------

						case "IPReturnList":
						DataTable dt23 = new DataTable();
						crystalReport.Load(Server.MapPath("IPReturnReciept.rpt"));
						using (SqlConnection con = new SqlConnection(constr))
						{
							using (SqlCommand cmd = new SqlCommand())
							{
								using (SqlDataAdapter sda = new SqlDataAdapter())
								{
									cmd.CommandType = CommandType.StoredProcedure;
									cmd.CommandText = "usp_MDQ_Print_IPReturnList_RPT";
									cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
									cmd.Parameters.Add("@ReturnNo", SqlDbType.VarChar).Value = Request["ReturnNo"].ToString() == "" ? "" : Request["ReturnNo"].ToString();
									cmd.Connection = con;
									sda.SelectCommand = cmd;
									sda.Fill(dt23);
								}
							}
						}
						crystalReport.SetDataSource(dt23);
						MediReportViewer.ReportSource = crystalReport;
						crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "IPReturnList");
						break;

						case "EmgReturnList":
						DataTable dt24 = new DataTable();
						crystalReport.Load(Server.MapPath("EmergReturnReciept.rpt"));
						using (SqlConnection con = new SqlConnection(constr))
						{
							using (SqlCommand cmd = new SqlCommand())
							{
								using (SqlDataAdapter sda = new SqlDataAdapter())
								{
									cmd.CommandType = CommandType.StoredProcedure;
									cmd.CommandText = "usp_MDQ_Print_EmergReturnList_RPT";
									cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
									cmd.Parameters.Add("@ReturnNo", SqlDbType.VarChar).Value = Request["ReturnNo"].ToString() == "" ? "" : Request["ReturnNo"].ToString();
									cmd.Connection = con;
									sda.SelectCommand = cmd;
									sda.Fill(dt24);
								}
							}
						}
						crystalReport.SetDataSource(dt24);
						MediReportViewer.ReportSource = crystalReport;
						crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "EmgReturnList");
						break;

						case "AfterBillingIPReturnList":
						DataTable dt25 = new DataTable();
						crystalReport.Load(Server.MapPath("AfterBillingIPReturn.rpt"));
						using (SqlConnection con = new SqlConnection(constr))
						{
							using (SqlCommand cmd = new SqlCommand())
							{
								using (SqlDataAdapter sda = new SqlDataAdapter())
								{
									cmd.CommandType = CommandType.StoredProcedure;
									cmd.CommandText = "usp_MDQ_Print_AfterBillingIPReturnList_RPT";
									cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
									cmd.Parameters.Add("@ReturnNo", SqlDbType.VarChar).Value = Request["ReturnNo"].ToString() == "" ? "" : Request["ReturnNo"].ToString();
									cmd.Connection = con;
									sda.SelectCommand = cmd;
									sda.Fill(dt25);
								}
							}
						}
						crystalReport.SetDataSource(dt25);
						MediReportViewer.ReportSource = crystalReport;
						crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "IPReturnList");
						break;

                        case "AfterBillingEmergReturnList":
                        DataTable dt26 = new DataTable();
                        crystalReport.Load(Server.MapPath("AfterBillingEmergReturn.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_AfterBillingEmergReturnList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@ReturnNo", SqlDbType.VarChar).Value = Request["ReturnNo"].ToString() == "" ? "" : Request["ReturnNo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt26);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt26);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "IPReturnList");
                        break;

                }
            }
        }
                   
   }
}