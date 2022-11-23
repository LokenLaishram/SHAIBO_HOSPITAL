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

namespace Mediqura.Web.MedBills.Reports
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

                    case "DepositReceipt":
                        DataTable dt10 = new DataTable();
                        crystalReport.Load(Server.MapPath("Depositreceipt.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_Deposit_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@DepositNo", SqlDbType.VarChar).Value = Request["DepositNo"].ToString() == "" ? "" : Request["DepositNo"].ToString();
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
                    case "DepositList":
                        DataTable dt9 = new DataTable();
                        crystalReport.Load(Server.MapPath("DepositList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GetDepositListRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = Convert.ToInt64(Request["UHID"].ToString() == "" ? "0" : Request["UHID"].ToString());
                                    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = Request["PatientName"].ToString() == "" ? "" : Request["PatientName"].ToString();
                                    cmd.Parameters.Add("@Paymode", SqlDbType.Int).Value = Convert.ToInt32(Request["Paymode"].ToString() == "" ? "0" : Request["Paymode"].ToString());
                                    cmd.Parameters.Add("@Datefrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = Request["Status"].ToString() == "0" ? true : false;
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
                    case "RefundtReceipt":
                        DataTable dt12 = new DataTable();
                        crystalReport.Load(Server.MapPath("RefundReciept.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_Refund_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@RefundNo", SqlDbType.VarChar).Value = Request["RefundNo"].ToString() == "" ? "" : Request["RefundNo"].ToString();
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
                    case "RefundList":
                        DataTable dt11 = new DataTable();
                        crystalReport.Load(Server.MapPath("RefundList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GetRefundListRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = Convert.ToInt64(Request["UHID"].ToString() == "" ? "0" : Request["UHID"].ToString());
                                    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = Request["PatientName"].ToString() == "" ? "" : Request["PatientName"].ToString();
                                    cmd.Parameters.Add("@Paymode", SqlDbType.Int).Value = Convert.ToInt32(Request["Paymode"].ToString() == "" ? "0" : Request["Paymode"].ToString());
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = Request["Status"].ToString() == "0" ? true : false;
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

                    case "OPDcollection":
                        DataTable dt6 = new DataTable();
                        crystalReport.Load(Server.MapPath("Opcollectionlist.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    var PatientName =  Request["PatientName"].ToString() == "" ? "" : Request["PatientName"].ToString();
                                    bool isnumeric = PatientName.All(char.IsDigit);
                                    if (isnumeric == false)
                                    {
                                        if (PatientName.Contains(":"))
                                        {
                                            bool isUHIDnumeric = PatientName.Substring(PatientName.LastIndexOf(':') + 1).All(char.IsDigit);
                                            cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = isUHIDnumeric ? Convert.ToInt64(PatientName.Contains(":") ? PatientName.Substring(PatientName.LastIndexOf(':') + 1) : "0") : 0;

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
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GetOPDBillList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    //Convert.ToInt64(Request["UHID"].ToString() == "" ? "0" : Request["UHID"].ToString());
                                    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = null;
                                    cmd.Parameters.Add("@Paymode", SqlDbType.Int).Value = Convert.ToInt32(Request["Paymode"].ToString() == "" ? "0" : Request["Paymode"].ToString());
                                    cmd.Parameters.Add("@Datefrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = Request["Status"].ToString() == "0" ? true : false;
                                    cmd.Parameters.Add("@CollectedByID", SqlDbType.BigInt).Value = Request["Collectedby"].ToString() == "" ? "0" : Request["Collectedby"].ToString();
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
                    case "LabCollection":
                        DataTable dt7 = new DataTable();
                        crystalReport.Load(Server.MapPath("LabCollectionlist.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GetLabBillList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;

                                        var source = Request["PatientName"].ToString() == "" ? "" : Request["PatientName"].ToString();
                                        if (source.Contains(":"))
                                        {
                                            ID = source.Substring(source.LastIndexOf(':') + 1);
                                            cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = Convert.ToInt64(ID == "" ? "0" : ID);
                                        }
                                        else
                                        {
                                            cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = 0;
                                        }
                                 
                                    cmd.Parameters.Add("@LoginEmployeeID", SqlDbType.BigInt).Value = LogData.EmployeeID;
                                     
                                    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = null; // Request["PatientName"].ToString() == "" ? "" : Request["PatientName"].ToString();
                                    cmd.Parameters.Add("@InvestigationNo", SqlDbType.VarChar).Value = Request["Invnumber"].ToString() == "" ? "0" : Request["Invnumber"].ToString();
                                    cmd.Parameters.Add("@Paymode", SqlDbType.Int).Value = Convert.ToInt32(Request["Paymode"].ToString() == "" ? "0" : Request["Paymode"].ToString());
                                    cmd.Parameters.Add("@Datefrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
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
                    case "OPDBillReceipt":
                        DataTable dt5 = new DataTable();
                        crystalReport.Load(Server.MapPath("OPDBillReceipt.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_OPDBill_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@BillNo", SqlDbType.VarChar).Value = Request["BillNo"].ToString() == "" ? "" : Request["BillNo"].ToString();
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
                    case "OPDConsultantSheet":
                        DataTable dt15 = new DataTable();
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
                                    cmd.Parameters.Add("@BillNo", SqlDbType.VarChar).Value = Request["BillNo"].ToString() == "" ? "" : Request["BillNo"].ToString();
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
                    case "DoctorPayment":
                        DataTable dt89 = new DataTable();
                        crystalReport.Load(Server.MapPath("Paymentvoucher.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_DoctorPaymentRPT";
                                    cmd.Parameters.Add("@PaymentNumber", SqlDbType.VarChar).Value = Request["voucher"].ToString() == "" ? "" : Request["voucher"].ToString();
                                    cmd.Parameters.Add("@ServiceCategory", SqlDbType.Int).Value = Request["Category"].ToString() == "" ? "" : Request["Category"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt89);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt89);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;
                    case "DoctorPaymentDetail":
                        DataTable dt91 = new DataTable();
                        crystalReport.Load(Server.MapPath("Doctorpaymentlist.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_DoctorPaidServices";
                                    cmd.Parameters.Add("@PaymentNumber", SqlDbType.VarChar).Value = Request["voucher"].ToString() == "" ? "" : Request["voucher"].ToString();
                                    cmd.Parameters.Add("@ServiceCategory", SqlDbType.Int).Value = Request["Category"].ToString() == "" ? "" : Request["Category"].ToString();
                                    cmd.Parameters.Add("@DoctorID", SqlDbType.BigInt).Value = Convert.ToInt64(Request["DoctorID"].ToString() == "" ? "0" : Request["DoctorID"].ToString());
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
                    case "Doctorpaymentlist":
                        DataTable dt90 = new DataTable();
                        crystalReport.Load(Server.MapPath("Doctorpaidlist.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Get_Doctor_Paymenthistorylist_RPT";
                                    cmd.Parameters.Add("@PaymentNumber", SqlDbType.VarChar).Value = Request["voucher"].ToString() == "" ? "" : Request["voucher"].ToString();
                                    cmd.Parameters.Add("@ServiceCategory", SqlDbType.Int).Value = Request["Category"].ToString() == "" ? "" : Request["Category"].ToString();
                                    cmd.Parameters.Add("@DoctorID", SqlDbType.Int).Value = Request["DoctorID"].ToString() == "" ? "" : Request["DoctorID"].ToString();
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
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
                    case "InterimBill":
                        DataTable dt23 = new DataTable();
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_IP_InterimBill_DetailsRPT";
                                    cmd.Parameters.Add("@IPNo", SqlDbType.VarChar).Value = Request["IPno"].ToString() == "" ? "0" : Request["IPno"].ToString();
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt23);
                                }
                            }
                        }
                        crystalReport.Load(Server.MapPath("IntermediateBill.rpt"));
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
                    case "InterimBillList":
                        DataTable dt45 = new DataTable();
                        crystalReport.Load(Server.MapPath("Interim_BillList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Get_IP_Intermediate_Bill_RPT";
                                    cmd.Parameters.Add("@IPNo", SqlDbType.VarChar).Value = Request["IPno"].ToString() == "" ? "" : Request["IPno"].ToString();
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt45);
                                }
                            }
                        }
                        crystalReport.SetDataSource(dt45);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;
                    case "FinalBill":

                        DataTable dt25 = new DataTable();
                        //crystalReport.Load(Server.MapPath("Beddetails.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_IP_finalbill_DetailsRPT";
                                    cmd.Parameters.Add("@FinalBill", SqlDbType.VarChar).Value = Request["BillNo"].ToString() == "" ? "0" : Request["BillNo"].ToString();
                                    cmd.Connection = con;
                                    cmd.CommandTimeout = 20000000;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt25);
                                }
                            }
                        }
                        crystalReport.Load(Server.MapPath("FinalBilling.rpt"));
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
                    case "OPDLabBillReceipt":

                        if (Request["Ispacakge"].ToString() == "0")
                        {
                            DataTable dt8 = new DataTable();
                            crystalReport.Load(Server.MapPath("OPDLabBillReceipt.rpt"));
                            using (SqlConnection con = new SqlConnection(constr))
                            {
                                using (SqlCommand cmd = new SqlCommand())
                                {
                                    using (SqlDataAdapter sda = new SqlDataAdapter())
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.CommandText = "usp_MDQ_Print_OPDLabBill_RPT";
                                        cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                        cmd.Parameters.Add("@BillNo", SqlDbType.VarChar).Value = Request["BillNo"].ToString() == "" ? "" : Request["BillNo"].ToString();
                                        cmd.Connection = con;
                                        sda.SelectCommand = cmd;
                                        sda.Fill(dt8);
                                    }
                                }
                            }
                            crystalReport.SetDataSource(dt8);
                            MediReportViewer.ReportSource = crystalReport;
                            crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        }
                        //if (Request["Ispacakge"].ToString() == "1")
                        //{
                        //    DataTable dt17 = new DataTable();
                        //    crystalReport.Load(Server.MapPath("OPDLabPackageBillReceipt.rpt"));
                        //    using (SqlConnection con = new SqlConnection(constr))
                        //    {
                        //        using (SqlCommand cmd = new SqlCommand())
                        //        {
                        //            using (SqlDataAdapter sda = new SqlDataAdapter())
                        //            {
                        //                cmd.CommandType = CommandType.StoredProcedure;
                        //                cmd.CommandText = "usp_MDQ_Print_OPDLabPackagebill_RPT";
                        //                cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                        //                cmd.Parameters.Add("@BillNo", SqlDbType.VarChar).Value = Request["BillNo"].ToString() == "" ? "" : Request["BillNo"].ToString();
                        //                cmd.Connection = con;
                        //                sda.SelectCommand = cmd;
                        //                sda.Fill(dt17);
                        //            }
                        //        }
                        //    }
                        //    crystalReport.SetDataSource(dt17);
                        //    MediReportViewer.ReportSource = crystalReport;
                        //    crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        //}
                        break;
                    case "OPDPhrBillReceipt":
                        DataTable dt3 = new DataTable();
                        crystalReport.Load(Server.MapPath("OPIssuePhrBillReceipt.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_OPDPhrBill_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@BillNo", SqlDbType.VarChar).Value = Request["BillNo"].ToString() == "" ? "" : Request["BillNo"].ToString();
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
                    case "OPIssue":
                        DataTable dt4 = new DataTable();
                        crystalReport.Load(Server.MapPath("OPIssuePhrBilllist.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GetPhrBillList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@BillNo", SqlDbType.VarChar).Value = Request["Bill"].ToString() == "" ? null : Request["Bill"].ToString();
                                    cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = Convert.ToInt64(Request["UHID"].ToString() == "" ? "0" : Request["UHID"].ToString());
                                    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = Request["PatientName"].ToString() == "" ? "" : Request["PatientName"].ToString();
                                    cmd.Parameters.Add("@Paymode", SqlDbType.Int).Value = Convert.ToInt32(Request["Paymode"].ToString() == "" ? "0" : Request["Paymode"].ToString());
                                    cmd.Parameters.Add("@Collectedby", SqlDbType.BigInt).Value = Convert.ToInt64(Request["Collectedby"].ToString() == "" ? "0" : Request["Collectedby"].ToString());
                                    cmd.Parameters.Add("@Datefrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Customertype", SqlDbType.Int).Value = Convert.ToInt32(Request["Customertype"].ToString() == "" ? "0" : Request["Customertype"].ToString());
                                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = Request["Status"].ToString() == "0" ? true : false;
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
                    case "AdjustmentReceipt":
                        DataTable dt2 = new DataTable();
                        crystalReport.Load(Server.MapPath("AdjustmentReceipt.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_LabServiceCancelAdjustment_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@AdjustmentNo", SqlDbType.VarChar).Value = Request["AdjustmentNo"].ToString() == "" ? "" : Request["AdjustmentNo"].ToString();
                                    cmd.Connection = con;
                                    sda.SelectCommand = cmd;
                                    sda.Fill(dt2);
                                }
                            }
                        }
                        crystalReport.SetDatabaseLogon(ReportUserId, ReportPassword, ReportServerName, ReportDatabase);
                        crystalReport.SetDataSource(dt2);
                        MediReportViewer.ReportSource = crystalReport;
                        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");
                        break;

                    case "AdjustmentList":
                        DataTable dt1 = new DataTable();
                        crystalReport.Load(Server.MapPath("AdjustmentList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_GetAdjustmentListRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@AdjustmentNo", SqlDbType.VarChar).Value = Request["AdjustmentNo"].ToString() == "" ? "" : Request["AdjustmentNo"].ToString();
                                    cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = Convert.ToInt64(Request["UHID"].ToString() == "" ? "0" : Request["UHID"].ToString());
                                    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = Request["PatientName"].ToString() == "" ? null : Request["PatientName"].ToString();
                                    cmd.Parameters.Add("@Datefrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = Request["Status"].ToString() == "0" ? true : false;
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

                    case "Indent_RegnProfile":
                        DataTable dt13 = new DataTable();
                        crystalReport.Load(Server.MapPath("Indent_RegnPHR.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "MDQ_Print_IndentRaisePHR_RPT";
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

                    case "IPRequestList":
                        DataTable dt20 = new DataTable();
                        crystalReport.Load(Server.MapPath("IPIndentList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_PrintIPindentItemListPHR_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@IPNo", SqlDbType.VarChar).Value = Request["IPNo"].ToString() == "" ? "" : Request["IPNo"].ToString();
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = Request["Status"].ToString() == "0" ? true : false;
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
                    case "DiscountRefund":
                        DataTable dt14 = new DataTable();
                        crystalReport.Load(Server.MapPath("DiscountRefundreceipt.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "MDQ_GET_discount_refund_Details";
                                    cmd.Parameters.Add("@RefundNo", SqlDbType.VarChar).Value = Request["RefNo"].ToString() == "" ? "" : Request["RefNo"].ToString();
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
                    case "LabDuePaidReceipt":
                        DataTable dt16 = new DataTable();
                        crystalReport.Load(Server.MapPath("LabDuePaidReciept.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_LAB_Print_DuePaidReciept_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@RecieptNo", SqlDbType.VarChar).Value = Request["RecieptNo"].ToString() == "" ? "" : Request["RecieptNo"].ToString();
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
                    case "LabDueCollectionList":
                        DataTable dt19 = new DataTable();
                        crystalReport.Load(Server.MapPath("LabDueCollectionList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_LAB_GetDueCollectionList_RPT";
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
                    case "LabDuePatientList":
                        DataTable dt21 = new DataTable();
                        crystalReport.Load(Server.MapPath("PrintLabDuePatientList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Lab_GetDueCustomerList_RPT";
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
                }
            }
        }

    }
}