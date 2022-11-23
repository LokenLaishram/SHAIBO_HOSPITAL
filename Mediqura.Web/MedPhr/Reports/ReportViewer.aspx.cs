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

namespace Mediqura.Web.MedPhr.Reports
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
                    case "EmrgInterimBill":
                        DataTable dt23 = new DataTable();
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_PHR_emergency_Interimbill_DetailsRPT";
                                    cmd.Parameters.Add("@EmrgNumber", SqlDbType.VarChar).Value = Request["EmrgNumber"].ToString() == "" ? "0" : Request["EmrgNumber"].ToString();
                                    //cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt23);
                                }
                            }
                        }
                        crystalReport.Load(Server.MapPath("EmgFinalbillpharma.rpt"));
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
                        crystalReport.SetDataSource(dt23);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;
                    case "EmrgFinalBill":
                        DataTable dt24 = new DataTable();
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_PHR_emergency_finalbill_DetailsRPT";
                                    cmd.Parameters.Add("@BillNo", SqlDbType.VarChar).Value = Request["BillNo"].ToString() == "" ? "0" : Request["BillNo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt24);
                                }
                            }
                        }
                        crystalReport.Load(Server.MapPath("PhrEmgfinalbillpharma.rpt"));
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
                        crystalReport.SetDataSource(dt24);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;
                    case "IPinterimBill":
                        DataTable dt25 = new DataTable();
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_IP_PHR_InterimBill_DetailsRPT";
                                    cmd.Parameters.Add("@IPNo", SqlDbType.VarChar).Value = Request["IPNo"].ToString() == "" ? "0" : Request["IPNo"].ToString();
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt25);
                                }
                            }
                        }
                        crystalReport.Load(Server.MapPath("PharIPinterimbill.rpt"));
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
                        crystalReport.SetDataSource(dt25);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;
                    case "IPFinalBill":
                        DataTable dt26 = new DataTable();
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_IP_PHR_finalbill_DetailsRPT";
                                    cmd.Parameters.Add("@FinalBill", SqlDbType.VarChar).Value = Request["BillNo"].ToString() == "" ? "0" : Request["BillNo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt26);
                                }
                            }
                        }
                        crystalReport.Load(Server.MapPath("PharIPFinalbillist.rpt"));
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
                        crystalReport.SetDataSource(dt26);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;
                    case "StockStatus":
                        DataTable dt90 = new DataTable();
                        crystalReport.Load(Server.MapPath("Mainstorestatus.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GetStockStatus";
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
                    case "SubStockStatus":
                        DataTable dt91 = new DataTable();
                        crystalReport.Load(Server.MapPath("StockStatus.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_MedGetSubStockStatus";
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
                                    cmd.Parameters.Add("@MedSubStockID", SqlDbType.Int).Value = Request["StockType"].ToString() == "" ? "0" : Request["StockType"].ToString();
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Now : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@StatusID", SqlDbType.Int).Value = Request["StockStatus"].ToString() == "" ? "0" : Request["StockStatus"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt91);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt91);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;
                    case "PhrDepositReceipt":
                        DataTable dt6 = new DataTable();
                        crystalReport.Load(Server.MapPath("PHRDepositreceipt.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_PHR_Print_Deposit_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@DepositNo", SqlDbType.VarChar).Value = Request["DepositNo"].ToString() == "" ? "" : Request["DepositNo"].ToString();

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
                    case "PhrDepositList":
                        DataTable dt8 = new DataTable();
                        crystalReport.Load(Server.MapPath("PHRDepositList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_PHR_GetDepositListRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;

                                    string source = Request["PatientName"].ToString() == "" ? "" : Request["PatientName"].ToString();

                                    bool isnumeric = source.All(char.IsDigit);
                                    if (isnumeric == false)
                                    {
                                        if (source.Contains(":"))
                                        {
                                            bool isUHIDnumeric = source.Substring(source.LastIndexOf(':') + 1).All(char.IsDigit);
                                            cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = isUHIDnumeric ? Convert.ToInt64(source.Contains(":") ? source.Substring(source.LastIndexOf(':') + 1) : "0") : 0;
                                        }
                                        else
                                        {
                                            cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = 0;
                                        }
                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = 0;
                                    }
                                    cmd.Parameters.Add("@Paymode", SqlDbType.Int).Value = Convert.ToInt32(Request["Paymode"].ToString() == "" ? "0" : Request["Paymode"].ToString());
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = Request["Status"].ToString() == "0" ? true : false;
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
                    case "PhrRefundReceipt":
                        DataTable dt5 = new DataTable();
                        crystalReport.Load(Server.MapPath("PHRRefundReciept.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_PHR_Print_Refund_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@RefundNo", SqlDbType.VarChar).Value = Request["RefundNo"].ToString() == "" ? "" : Request["RefundNo"].ToString();
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
                    case "PHROPbill":
                        DataTable dt28 = new DataTable();
                        crystalReport.Load(Server.MapPath("phropbill.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_PHR_Print_OP_bill_RPT";
                                    cmd.Parameters.Add("@BillNo", SqlDbType.VarChar).Value = Request["BillNo"].ToString() == "" ? "" : Request["BillNo"].ToString();
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
                    case "PHROPbillList":
                        DataTable dt27 = new DataTable();
                        crystalReport.Load(Server.MapPath("Opbillist.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Get_PHR_Op_BillList";
                                    cmd.Parameters.Add("@BillNo", SqlDbType.VarChar).Value = Request["BillNo"].ToString() == "" ? "" : Request["BillNo"].ToString();
                                    cmd.Parameters.Add("@Paymode", SqlDbType.Int).Value = Convert.ToInt32(Request["Paymode"].ToString() == "" ? "0" : Request["Paymode"].ToString());
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@CollectedByID", SqlDbType.BigInt).Value = Convert.ToInt64(Request["CollectedBy"].ToString() == "" ? "0" : Request["CollectedBy"].ToString());
                                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = Request["Status"].ToString() == "0" ? true : false;

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
                    case "MedRequestList":
                        DataTable dt14 = new DataTable();
                        crystalReport.Load(Server.MapPath("RequestList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Med_PrintIndentItemListRPT";
                                    cmd.Parameters.Add("@MedStockID", SqlDbType.Int).Value = Convert.ToInt32(Request["StockID"].ToString() == "" ? "0" : Request["StockID"].ToString());
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
                    case "Indent_RegnProfile":
                        DataTable dt13 = new DataTable();
                        crystalReport.Load(Server.MapPath("Indent_Form.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "MDQ_Med_Print_Indentform_RPT";
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
                    case "PhrRefundList":
                        DataTable dt7 = new DataTable();
                        crystalReport.Load(Server.MapPath("PHRRefundList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_PHR_GetRefundListRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    string source = Request["PatientName"].ToString() == "" ? "" : Request["PatientName"].ToString();
                                    bool isnumeric = source.All(char.IsDigit);
                                    if (isnumeric == false)
                                    {
                                        if (source.Contains(":"))
                                        {
                                            bool isUHIDnumeric = source.Substring(source.LastIndexOf(':') + 1).All(char.IsDigit);
                                            cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = isUHIDnumeric ? Convert.ToInt64(source.Contains(":") ? source.Substring(source.LastIndexOf(':') + 1) : "0") : 0;
                                        }
                                        else
                                        {
                                            cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = 0;
                                        }
                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = 0;
                                    }
                                    cmd.Parameters.Add("@Paymode", SqlDbType.Int).Value = Convert.ToInt32(Request["Paymode"].ToString() == "" ? "0" : Request["Paymode"].ToString());
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = Request["Status"].ToString() == "0" ? true : false;

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
                    case "PhrIPDrugIssueList":
                        DataTable dt10 = new DataTable();
                        crystalReport.Load(Server.MapPath("PharIPDrugIssueList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_PHR_GetIPDrugIssueList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    string source = Request["SubStockID"].ToString() == "" ? "" : Request["SubStockID"].ToString();
                                    bool isnumeric = source.All(char.IsDigit);
                                    if (isnumeric == false)
                                    {
                                        if (source.Contains(":"))
                                        {
                                            bool isUHIDnumeric = source.Substring(source.LastIndexOf(':') + 1).All(char.IsDigit);
                                            cmd.Parameters.Add("@SubStockID", SqlDbType.Int).Value = isUHIDnumeric ? Convert.ToInt64(source.Contains(":") ? source.Substring(source.LastIndexOf(':') + 1) : "0") : 0;
                                        }
                                        else
                                        {
                                            cmd.Parameters.Add("@SubStockID", SqlDbType.Int).Value = 0;
                                        }
                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@SubStockID", SqlDbType.Int).Value = 0;
                                    }
                                    cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = Convert.ToInt32(Request["UHID"].ToString() == "" ? "0" : Request["UHID"].ToString());
                                    cmd.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = Request["IPNO"].ToString() == "" ? "0" : Request["IPNO"].ToString();
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);


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
                    case "PhrEmgDrugIssueList":
                        DataTable dt11 = new DataTable();
                        crystalReport.Load(Server.MapPath("PharEmgDrugIssueList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_PHR_GetEmgDrugIssueList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    string source = Request["SubStockID"].ToString() == "" ? "" : Request["SubStockID"].ToString();
                                    bool isnumeric = source.All(char.IsDigit);
                                    if (isnumeric == false)
                                    {
                                        if (source.Contains(":"))
                                        {
                                            bool isUHIDnumeric = source.Substring(source.LastIndexOf(':') + 1).All(char.IsDigit);
                                            cmd.Parameters.Add("@SubStockID", SqlDbType.Int).Value = isUHIDnumeric ? Convert.ToInt64(source.Contains(":") ? source.Substring(source.LastIndexOf(':') + 1) : "0") : 0;
                                        }
                                        else
                                        {
                                            cmd.Parameters.Add("@SubStockID", SqlDbType.Int).Value = 0;
                                        }
                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@SubStockID", SqlDbType.Int).Value = 0;
                                    }
                                    cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = Convert.ToInt32(Request["UHID"].ToString() == "" ? "0" : Request["UHID"].ToString());
                                    cmd.Parameters.Add("@EmgNo", SqlDbType.VarChar).Value = Request["EmgNo"].ToString() == "" ? "0" : Request["EmgNo"].ToString();
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);


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
                    case "PhrDuePaidReceipt":
                        DataTable dt12 = new DataTable();
                        crystalReport.Load(Server.MapPath("PHRDuePaidReciept.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_PHR_Print_DuePaidReciept_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@RecieptNo", SqlDbType.VarChar).Value = Request["RecieptNo"].ToString() == "" ? "" : Request["RecieptNo"].ToString();
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

                    case "Phrvendordetails":
                        DataTable dt15 = new DataTable();
                        crystalReport.Load(Server.MapPath("Phrvendordetails.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_PHR_PrintVendorPaymentDetails";
                                    cmd.Parameters.Add("@SupplierID", SqlDbType.Int).Value = Convert.ToInt32(Request["SupplierID"].ToString() == "" ? "0" : Request["SupplierID"].ToString());
                                    cmd.Parameters.Add("@From", SqlDbType.DateTime).Value = Request["From"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["From"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@To", SqlDbType.DateTime).Value = Request["To"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["To"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = Convert.ToInt32(Request["Status"].ToString() == "" ? "0" : Request["Status"].ToString());
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt15);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt15);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Vendor_Payment");
                        break;

                    case "Vendorpayment":
                        DataTable dt16 = new DataTable();
                        crystalReport.Load(Server.MapPath("Vendorpayment.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_PHR_PrintVendorPaymentReciept";
                                    cmd.Parameters.Add("@PaymentNo", SqlDbType.VarChar).Value = Request["PaymentNo"].ToString() == "" ? "0" : Request["PaymentNo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt16);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt16);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Vendor_Payment_Reciept");
                        break;


                    case "Vendorpaymentlist":
                        DataTable dt17 = new DataTable();
                        crystalReport.Load(Server.MapPath("Vendorpaymentlist.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_PHR_PrintVendorPaymentList";
                                    cmd.Parameters.Add("@PaymentNo", SqlDbType.VarChar).Value = Request["PaymentNo"].ToString() == "" ? "0" : Request["PaymentNo"].ToString();
                                    cmd.Parameters.Add("@From", SqlDbType.DateTime).Value = Request["From"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["From"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@To", SqlDbType.DateTime).Value = Request["To"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["To"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@PaymentMode", SqlDbType.Int).Value = Convert.ToInt32(Request["PaymentMode"].ToString() == "" ? "0" : Request["PaymentMode"].ToString());
                                    cmd.Parameters.Add("@SupplierID", SqlDbType.Int).Value = Convert.ToInt32(Request["SupplierID"].ToString() == "" ? "0" : Request["SupplierID"].ToString());
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt17);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt17);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Vendor_PaymentList");
                        break;

                    case "PHRRefundAfterBilling":
                        DataTable dt18 = new DataTable();
                        crystalReport.Load(Server.MapPath("PhaRefunafterbilling.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_PHR_PrintRefundAfterBillingDiscount_RPT";
                                    cmd.Parameters.Add("@RefundNo", SqlDbType.VarChar).Value = Request["RefundNo"].ToString() == "" ? "0" : Request["RefundNo"].ToString();
                                    cmd.Parameters.Add("@ReqNo", SqlDbType.VarChar).Value = Request["ReqNo"].ToString() == "" ? "0" : Request["ReqNo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt18);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt18);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "PharmacyRefundAfterBilling");
                        break;

                    case "PhrDueCollectionList":
                        DataTable dt19 = new DataTable();
                        crystalReport.Load(Server.MapPath("PhrDueCollectionlist.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Phr_GetDueCollectionList_RPT";
                                    string Bill;
                                    var source = Request["CusDetails"].ToString() == "" ? "0" : Request["CusDetails"].ToString();
                                    if (source.Contains(":") || source != "")
                                    {
                                        Bill = source.Substring(source.LastIndexOf(':') + 1);
                                        cmd.Parameters.Add("@BillNo", SqlDbType.VarChar).Value = Bill.Trim();
                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@BillNo", SqlDbType.VarChar).Value = "";
                                    }
                                    cmd.Parameters.Add("@PatientTypeID", SqlDbType.Int).Value = Convert.ToInt32(Request["PatientTypeID"].ToString() == "" ? "0" : Request["PatientTypeID"].ToString());
                                    cmd.Parameters.Add("@DueReponsibleBy", SqlDbType.Int).Value = Convert.ToInt32(Request["DueRespBy"].ToString() == "" ? "0" : Request["DueRespBy"].ToString());
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = Convert.ToInt32(Request["Status"].ToString() == "" ? "0" : Request["Status"].ToString());
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt19);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt19);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "DueCollection");
                        break;
                    case "PhrIPDrugIssueReciept":
                        DataTable dt20 = new DataTable();
                        crystalReport.Load(Server.MapPath("PharIPitemissuereceipt.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Phar_IPDrugRecordReciept_RPT";
                                    cmd.Parameters.Add("@IPIssueNo", SqlDbType.VarChar).Value = Request["IPIssueNo"].ToString() == "" ? "0" : Request["IPIssueNo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt20);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt20);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Vendor_Payment_Reciept");
                        break;

                    case "PhrDuePatientList":
                        DataTable dt21 = new DataTable();
                        crystalReport.Load(Server.MapPath("PrintPhrDuePatientList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Phr_GetDueCustomerList_RPT";
                                    cmd.Parameters.Add("@PatientTypeID", SqlDbType.Int).Value = Convert.ToInt32(Request["PatientTypeID"].ToString() == "" ? "0" : Request["PatientTypeID"].ToString());
                                    cmd.Parameters.Add("@DueReponsibleBy", SqlDbType.Int).Value = Convert.ToInt32(Request["DueRespBy"].ToString() == "" ? "0" : Request["DueRespBy"].ToString());
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt21);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt21);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "DueCustomerList");
                        break;
					case "TransactionStatement":
						DataTable dt22 = new DataTable();
						crystalReport.Load(Server.MapPath("PharTransactionsummary.rpt"));
						using (SqlConnection con = new SqlConnection(constr))
						{
							using (SqlCommand cmd = new SqlCommand())
							{
								using (SqlDataAdapter sda = new SqlDataAdapter())
								{
									cmd.CommandType = CommandType.StoredProcedure;
									cmd.CommandText = "usp_MDQ_PHR_PrintTransactionList";
									cmd.Parameters.Add("@TransactionID", SqlDbType.Int).Value = Convert.ToInt32(Request["TransactionID"].ToString() == "" ? "0" : Request["TransactionID"].ToString());
									cmd.Parameters.Add("@CollectedByID", SqlDbType.Int).Value = Convert.ToInt32(Request["CollectedByID"].ToString() == "" ? "0" : Request["CollectedByID"].ToString());
									cmd.Parameters.Add("@accountState", SqlDbType.Int).Value = Convert.ToInt32(Request["AccountState"].ToString() == "" ? "0" : Request["AccountState"].ToString());
									cmd.Parameters.Add("@accountid", SqlDbType.Int).Value = Convert.ToInt32(Request["AccountID"].ToString() == "" ? "0" : Request["AccountID"].ToString());
									cmd.Parameters.Add("@dateFrom", SqlDbType.DateTime).Value = Request["from"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["from"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
									cmd.Parameters.Add("@dateto", SqlDbType.DateTime).Value = Request["To"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["To"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
															
									cmd.Connection = con;
									sda.SelectCommand = cmd;
									sda.Fill(dt22);
								}
							}
						}
						crystalReport.SetDataSource(dt22);
						MediReportViewer.ReportSource = crystalReport;
						crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Transaction_Statement");
						break;
                    case "PhrEmgDrugIssueReciept":
                        DataTable dt34 = new DataTable();
                        crystalReport.Load(Server.MapPath("PharEmgitemissuereceipt.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.CommandText = "usp_MDQ_Phar_EmgDrugRecordDetailsList_RPT";
                                    cmd.Parameters.Add("@EmgIssueNo", SqlDbType.VarChar).Value = Request["EmgIssueNo"].ToString() == "" ? "0" : Request["EmgIssueNo"].ToString();
                                   cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt34);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt34);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Vendor_Payment_Reciept");
                        break;

                    case "PrintPhrInterStockByTransferNo":
                        DataTable dt35 = new DataTable();
                        crystalReport.Load(Server.MapPath("PrintPhrTransferStockByTransferNo.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Phr_Print_TransferStock_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@PhrTransferNo", SqlDbType.VarChar).Value = Request["PhrTransferNo"].ToString() == "" ? "" : Request["PhrTransferNo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt35);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt35);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                    case "PhrSaleTracking":
                        DataTable dt36 = new DataTable();
                        crystalReport.Load(Server.MapPath("PhrItemSaleTrackList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Phr_Get_ItemSaleTrackList_RPT";      
                                    var ItemName = Request["ItemName"].ToString() == "" ? "" : Request["ItemName"].ToString();
                                    bool isnumeric = ItemName.All(char.IsDigit);
                                    if (isnumeric == false)
                                    {
                                        if (ItemName.Contains(":"))
                                        {
                                            bool isUHIDnumeric = ItemName.Substring(ItemName.LastIndexOf(':') + 1).All(char.IsDigit);
                                            cmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = isUHIDnumeric ? Convert.ToInt64(ItemName.Contains(":") ? ItemName.Substring(ItemName.LastIndexOf(':') + 1) : "0") : 0;

                                        }
                                        else
                                        {
                                            cmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = 0;

                                        }
                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = 0;

                                    }
                                    cmd.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = Request["BatchNo"].ToString() == "" ? "0" : Request["BatchNo"].ToString();                             
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt36);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt36);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "SaltTrackingList");
                        break;
                    case "PhrSaleTrackDetails":
                        DataTable dt37 = new DataTable();
                        crystalReport.Load(Server.MapPath("PhrItemSaleTrackDetails.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Phr_SaleItemTrackerDetails_RPT";                                    
                                    cmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = Request["ItemID"].ToString() == "" ? "0" : Request["ItemID"].ToString();
                                    cmd.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = Request["BatchNo"].ToString() == "" ? "0" : Request["BatchNo"].ToString();
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt37);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt37);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "SaltTrackingList");
                        break;

					case "ManualAccountTransaction":
						DataTable dt38 = new DataTable();
						crystalReport.Load(Server.MapPath("ManualTransaction.rpt"));
						using (SqlConnection con = new SqlConnection(constr))
						{
							using (SqlCommand cmd = new SqlCommand())
							{
								using (SqlDataAdapter sda = new SqlDataAdapter())
								{
									cmd.CommandType = CommandType.StoredProcedure;
									cmd.CommandText = "usp_MDQ_PHR_GetManualTransaction_receipt";
									cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
									cmd.Parameters.Add("@voucherNumber", SqlDbType.VarChar).Value = Request["voucherNumber"].ToString() == "" ? "0" : Request["voucherNumber"].ToString();
									cmd.Connection = con;
									sda.SelectCommand = cmd;
									sda.Fill(dt38);
								}
							}
						}
						crystalReport.SetDataSource(dt38);
						MediReportViewer.ReportSource = crystalReport;
						crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
						break;

					case "TransactionSummary":
						DataTable dt39 = new DataTable();
						crystalReport.Load(Server.MapPath("MonthlyTransactionSummary.rpt"));
						using (SqlConnection con = new SqlConnection(constr))
						{
							using (SqlCommand cmd = new SqlCommand())
							{
								using (SqlDataAdapter sda = new SqlDataAdapter())
								{
									cmd.CommandType = CommandType.StoredProcedure;
									cmd.CommandText = "usp_MDQ_PHR_GetTransactionSummary";
									cmd.Parameters.Add("@dateFrom", SqlDbType.DateTime).Value = Request["from"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["from"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
									cmd.Parameters.Add("@dateto", SqlDbType.DateTime).Value = Request["To"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["To"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
									cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
									cmd.Connection = con;
									sda.SelectCommand = cmd;
									sda.Fill(dt39);
								}
							}
						}
						crystalReport.SetDataSource(dt39);
						MediReportViewer.ReportSource = crystalReport;
						crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Transaction_Summary");
						break;

                    case "VendorItemReturnList":
                        DataTable dt40 = new DataTable();
                        crystalReport.Load(Server.MapPath("VendorItemReturnReciept.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_MED_PrintStockReturnList";
                                    cmd.Parameters.Add("@ReturnNo", SqlDbType.VarChar).Value = Request["ReturnNo"].ToString() == "" ? "0" : Request["ReturnNo"].ToString();
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt40);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt40);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

					case "VendorPurchasepayment":
						DataTable dt41 = new DataTable();
						crystalReport.Load(Server.MapPath("VendorPurchasepayment.rpt"));
						using (SqlConnection con = new SqlConnection(constr))
						{
							using (SqlCommand cmd = new SqlCommand())
							{
								using (SqlDataAdapter sda = new SqlDataAdapter())
								{
									cmd.CommandType = CommandType.StoredProcedure;
									cmd.CommandText = "usp_MDQ_PHR_PrintVendorPurchasePaymentReciept";
									cmd.Parameters.Add("@InVoiceNo", SqlDbType.VarChar).Value = Request["InVoiceNo"].ToString() == "" ? "0" : Request["InVoiceNo"].ToString();
									cmd.Connection = con;
									sda.SelectCommand = cmd;
									sda.Fill(dt41);
								}
							}
						}
						crystalReport.SetDataSource(dt41);
						MediReportViewer.ReportSource = crystalReport;
						crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Vendor_Payment_Reciept");
						break;
                   
					
                }
            }
        }

    }
}