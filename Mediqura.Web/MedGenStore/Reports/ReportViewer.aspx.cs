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

namespace Mediqura.Web.MedGenStore.Reports
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
                                    cmd.CommandText = "usp_MDQ_GEN_STR_Print_PurchaseItemCheckList_RPT";
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
                                    cmd.CommandText = "MDQ_GEN_STR_Print_PurchaseOrder_RPT";
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
                                    cmd.CommandText = "usp_MDQ_GEN_STR_Print_GetPurchaseList";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@PONo", SqlDbType.VarChar).Value = Request["PONo"].ToString() == "" ? "" : Request["PONo"].ToString();
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
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
                                    cmd.CommandText = "usp_MDQ_GEN_STR_Print_GetPOCheckListRPT";
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
                                    cmd.CommandText = "usp_MDQ_GEN_STR_PrintStockistItemReturnList_RPT";
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
                                    cmd.CommandText = "usp_MDQ_GEN_STR_GetStockistReturn_RPT";
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
                    case "StockStatus":
                        DataTable dt90 = new DataTable();
                        crystalReport.Load(Server.MapPath("Stockstatus.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GEN_STR_GetStockStatus";
                                    cmd.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = Request["BatchNo"].ToString() == "" ? null : Request["BatchNo"].ToString();
                                    string ID;
                                    var source = Request["Item"].ToString() == "" ? "0" : Request["Item"].ToString();
                                    if (source.Contains(":") || source != "0")
                                    {
                                        ID = source.Substring(source.LastIndexOf(':') + 1);
                                        cmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = Convert.ToInt32(ID);
                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = 0;
                                    }
                                    cmd.Parameters.Add("@StockType", SqlDbType.Int).Value = Request["StockType"].ToString() == "" ? "0" : Request["StockType"].ToString();
                                    cmd.Parameters.Add("@PO", SqlDbType.VarChar).Value = Request["PO"].ToString() == "" ? null : Request["PO"].ToString();
                                    cmd.Parameters.Add("@StockNo", SqlDbType.VarChar).Value = Request["StockNo"].ToString() == "" ? null : Request["StockNo"].ToString();
                                    cmd.Parameters.Add("@ReceiptNo", SqlDbType.VarChar).Value = Request["ReceiptNo"].ToString() == "" ? null : Request["ReceiptNo"].ToString();
                                    cmd.Parameters.Add("@Group", SqlDbType.Int).Value = Request["Group"].ToString() == "" ? "0" : Request["Group"].ToString();
                                    cmd.Parameters.Add("@Subgroup", SqlDbType.Int).Value = Request["Subgroup"].ToString() == "" ? "0" : Request["Subgroup"].ToString();
                                    cmd.Parameters.Add("@Availbalancefrom", SqlDbType.Int).Value = Request["Availbalancefrom"].ToString() == "" ? "0" : Request["Availbalancefrom"].ToString();
                                    cmd.Parameters.Add("@Availbalanceto", SqlDbType.Int).Value = Request["Availbalanceto"].ToString() == "" ? "100000000" : Request["Availbalanceto"].ToString();
                                    cmd.Parameters.Add("@ExpiryDayfrom", SqlDbType.Int).Value = Request["ExpiryDayfrom"].ToString() == "" ? "0" : Request["ExpiryDayfrom"].ToString();
                                    cmd.Parameters.Add("@ExpiryDayto", SqlDbType.Int).Value = Request["ExpiryDayto"].ToString() == "" ? "10000000" : Request["ExpiryDayto"].ToString();
                                    cmd.Parameters.Add("@Recievedyear", SqlDbType.Int).Value = Request["Recievedyear"].ToString() == "" ? "0" : Request["Recievedyear"].ToString();
                                    cmd.Parameters.Add("@Recievedmonth", SqlDbType.Int).Value = Request["Recievedmonth"].ToString() == "" ? "0" : Request["Recievedmonth"].ToString();
                                    cmd.Parameters.Add("@RecievedFrom", SqlDbType.DateTime).Value = Request["RecievedFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["RecievedFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@RecievedTo", SqlDbType.DateTime).Value = Request["RecievedFrom"].ToString() == "" ? System.DateTime.Now : DateTime.Parse(Request["RecievedTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@MfgCompany", SqlDbType.Int).Value = Request["MfgCompany"].ToString() == "" ? "0" : Request["MfgCompany"].ToString();
                                    cmd.Parameters.Add("@Supplier", SqlDbType.Int).Value = Request["Supplier"].ToString() == "" ? "0" : Request["Supplier"].ToString();
                                    cmd.Parameters.Add("@StockStatus", SqlDbType.Int).Value = Request["StockStatus"].ToString() == "" ? "0" : Request["StockStatus"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt90);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt90);
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
                                    cmd.CommandText = "MDQ_GEN_Print_IndentRaise_RPT";
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

                    case "GenRequestList":
                        DataTable dt14 = new DataTable();
                        crystalReport.Load(Server.MapPath("RequestList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_GEN_MDQ_PrintIndentItemListRPT";
                                    cmd.Parameters.Add("@GenStockID", SqlDbType.Int).Value = Convert.ToInt32(Request["StockID"].ToString() == "" ? "0" : Request["StockID"].ToString());
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@IndentRequestID", SqlDbType.Int).Value = Convert.ToInt32(Request["RequestType"].ToString() == "" ? "0" : Request["RequestType"].ToString());
                                    cmd.Parameters.Add("@IndentNo", SqlDbType.VarChar).Value = Request["IndentNo"].ToString() == "" ? "" : Request["IndentNo"].ToString();
                                    cmd.Parameters.Add("@ReceivedBy", SqlDbType.BigInt).Value = Convert.ToInt64(Request["RequestBy"].ToString() == "" ? null : Request["RequestBy"].ToString());
                                    cmd.Parameters.Add("@VerificationStatus", SqlDbType.Int).Value = Convert.ToInt32(Request["verstatus"].ToString() == "" ? "0" : Request["verstatus"].ToString());
                                    cmd.Parameters.Add("@IndentStatus", SqlDbType.Int).Value = Convert.ToInt32(Request["indentStatus"].ToString() == "" ? "0" : Request["indentStatus"].ToString());
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

                    case "Handover":
                        DataTable dt15 = new DataTable();
                        crystalReport.Load(Server.MapPath("HandOver_Regn.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "MDQ_Print_GENIndentHndOver_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@IndentNo", SqlDbType.VarChar).Value = Request["IndentNo"].ToString() == "" ? "" : Request["IndentNo"].ToString();
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
                                    cmd.CommandText = "usp_MDQ_Print_GENIndentHndOverList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@DeptID", SqlDbType.Int).Value = Convert.ToInt32(Request["Dept"].ToString() == "" ? "0" : Request["Dept"].ToString());
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@ApprvBy", SqlDbType.BigInt).Value = Convert.ToInt64(Request["Approved"].ToString() == "" ? null : Request["Approved"].ToString());
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
                    case "GenDepWiseItemList":
                        DataTable dt17 = new DataTable();
                        crystalReport.Load(Server.MapPath("DeptWiseItemUsedList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_GEN_MDQ_PrintDeptWiseUsedItemListRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@RecordNo", SqlDbType.VarChar).Value = Request["RecordNo"].ToString() == "" ? "" : Request["RecordNo"].ToString();
                                    cmd.Parameters.Add("@Substock", SqlDbType.Int).Value = Convert.ToInt32(Request["Substock"].ToString() == "" ? "0" : Request["Substock"].ToString());
                                    string source = Request["ItemName"].ToString() == "" ? "0" : Request["ItemName"].ToString();
                                    cmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = source.Substring(source.LastIndexOf(':') + 1);
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = Request["PatName"].ToString() == "" ? "" : Request["PatName"].ToString();
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
                    case "RecordWiseItemUsed":
                        DataTable dt18 = new DataTable();
                        crystalReport.Load(Server.MapPath("ItemUsedRecordWise.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_GEN_MDQ_PrintDeptRecordWiseUsedItemListRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@RecordNo", SqlDbType.VarChar).Value = Request["RecordNo"].ToString() == "" ? "" : Request["RecordNo"].ToString();
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
                    case "ReturnProfile":
                        DataTable dt20 = new DataTable();
                        crystalReport.Load(Server.MapPath("Stock_Return.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "MDQ_GEN_Print_StockReturn_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@ReturnNo", SqlDbType.VarChar).Value = Request["ReturnNo"].ToString() == "" ? "" : Request["ReturnNo"].ToString();
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
                    case "StockReurnList":
                        DataTable dt21 = new DataTable();
                        crystalReport.Load(Server.MapPath("StockReturnList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_GEN_MDQ_PrintStoskReturnListRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@GenStockID", SqlDbType.Int).Value = Convert.ToInt32(Request["StockID"].ToString() == "" ? "0" : Request["StockID"].ToString());
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@ReturnNo", SqlDbType.VarChar).Value = Request["ReturnNo"].ToString() == "" ? "" : Request["ReturnNo"].ToString();
                                    cmd.Parameters.Add("@user", SqlDbType.BigInt).Value = Convert.ToInt64(Request["user"].ToString() == "" ? null : Request["user"].ToString());
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
                    case "DepartmentWiseStock":
                        DataTable dt22 = new DataTable();
                        crystalReport.Load(Server.MapPath("DepartmentWiseSTock.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_GEN_MDQ_PrintDeptwisestockRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@GenStockID", SqlDbType.Int).Value = Convert.ToInt32(Request["Stock"].ToString() == "" ? "0" : Request["Stock"].ToString());
                                    cmd.Parameters.Add("@StatusID", SqlDbType.Int).Value = Convert.ToInt32(Request["StatusID"].ToString() == "" ? "0" : Request["StatusID"].ToString());
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    var source = Request["Item"].ToString();
                                    if (source.Contains(":"))
                                    {
                                        string ID = source.Substring(source.LastIndexOf(':') + 1);
                                        cmd.Parameters.Add("@ItemID", SqlDbType.BigInt).Value = Convert.ToInt32(ID);
                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@ItemID", SqlDbType.BigInt).Value = 0;
                                    }
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
                    case "NonAvail":
                        DataTable dt23 = new DataTable();
                        crystalReport.Load(Server.MapPath("NonavailItem.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_GEN_MDQ_GetNeetoPurchageItem";
                                    cmd.Parameters.Add("@GenStockID", SqlDbType.Int).Value = Convert.ToInt32(Request["Stock"].ToString() == "" ? "0" : Request["Stock"].ToString());
                                    cmd.Parameters.Add("@StatusID", SqlDbType.Int).Value = Convert.ToInt32(Request["StatusID"].ToString() == "" ? "0" : Request["StatusID"].ToString());
                                    var source = Request["Item"].ToString();
                                    if (source.Contains(":"))
                                    {
                                        string ID = source.Substring(source.LastIndexOf(':') + 1);
                                        cmd.Parameters.Add("@ItemID", SqlDbType.BigInt).Value = Convert.ToInt32(ID);
                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@ItemID", SqlDbType.BigInt).Value = 0;
                                    }
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt23);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt23);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                    //---CONDEMN REQUEST & APPROVAL REPORT
                    case "CondemnApproval":
                        DataTable dt24 = new DataTable();
                        crystalReport.Load(Server.MapPath("CondemnItemApproved.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "MDQ_GEN_Print_IndentRaise_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@CondemnRequestNo", SqlDbType.VarChar).Value = Request["CondemnRequestNo"].ToString() == "" ? "" : Request["CondemnRequestNo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt24);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt24);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;
                    //---END OF REQUEST & APPROVAL REPORT
                    //--- START OF STOCK UTILIZATION STATUS----
                    case "GenStockUtilizationList":
                        DataTable dt25 = new DataTable();
                        crystalReport.Load(Server.MapPath("GenStockUtilizationList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GEN_Generate_SubstockwiseUtilizationRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@FinancialYearID", SqlDbType.Int).Value = Convert.ToInt32(Request["FinancialYrID"].ToString() == "" ? "0" : Request["FinancialYrID"].ToString());
                                    cmd.Parameters.Add("@MonthID", SqlDbType.Int).Value = Convert.ToInt32(Request["MonthID"].ToString() == "" ? "0" : Request["MonthID"].ToString());
                                    cmd.Parameters.Add("@GenStockID", SqlDbType.Int).Value = Convert.ToInt32(Request["GenStockID"].ToString() == "" ? "0" : Request["GenStockID"].ToString());
                                    cmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = Convert.ToInt32(Request["ItemID"].ToString() == "" ? "0" : Request["ItemID"].ToString());
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt25);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt25);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;
                    //---END OF STOCK UTILIZATION STATUS----
                    case "TransferGenStockList":
                        DataTable dt26 = new DataTable();
                        crystalReport.Load(Server.MapPath("TransferGenStockList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GEN_STR_Get_InterStockTransferListRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@InterTransferNo", SqlDbType.VarChar).Value = Request["InterTransferNo"].ToString() == "" ? "" : Request["InterTransferNo"].ToString();
                                    cmd.Parameters.Add("@TransferFromGenStockID", SqlDbType.Int).Value = Convert.ToInt32(Request["FromGenStockID"].ToString() == "" ? "0" : Request["FromGenStockID"].ToString());
                                    cmd.Parameters.Add("@TransferToGenStockID", SqlDbType.Int).Value = Convert.ToInt32(Request["ToGenStockID"].ToString() == "" ? "0" : Request["ToGenStockID"].ToString());
                                    cmd.Parameters.Add("@TransferBy", SqlDbType.Int).Value = Convert.ToInt32(Request["TransferByID"].ToString() == "" ? "0" : Request["TransferByID"].ToString());
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt26);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt26);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;
                    case "InterStockByTransferNo":
                        DataTable dt27 = new DataTable();
                        crystalReport.Load(Server.MapPath("TransferGenStockByTransferNo.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GEN_STR_Get_InterStockTransferItem_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@InterTransferNo", SqlDbType.VarChar).Value = Request["InterTransferNo"].ToString() == "" ? "" : Request["InterTransferNo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt27);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt27);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                    case "PhrItemReturnToMain":
                        DataTable dt28 = new DataTable();
                        crystalReport.Load(Server.MapPath("PhrItemReturnToMain.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "MDQ_Phr_Print_StockReturnToMainDetails_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@ReturnNo", SqlDbType.VarChar).Value = Request["ReturnNo"].ToString() == "" ? "" : Request["ReturnNo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt28);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt28);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;
                }

            }
        }
    }


}
