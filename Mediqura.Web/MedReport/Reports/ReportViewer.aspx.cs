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

namespace Mediqura.Web.MedReport.Reports
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
               
                     case "OTChargeList":

                      DataTable dt1 = new DataTable();
                        crystalReport.Load(Server.MapPath("OTChargesList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_SearchServicesReport_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@ServiceTypeID", SqlDbType.Int).Value = Convert.ToInt32(Request["ServiceType"].ToString() == "" ? "" : Request["ServiceType"].ToString());
                                    cmd.Parameters.Add("@SubServiceTypeID", SqlDbType.Int).Value = Convert.ToInt32(Request["SubServiceType"].ToString() == "" ? "" : Request["SubServiceType"].ToString());
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

                     case "DischargeList":

                        DataTable dt2 = new DataTable();
                        crystalReport.Load(Server.MapPath("DischargeList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GET_DischargeList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@WardID", SqlDbType.Int).Value = Convert.ToInt32(Request["Ward"].ToString() == "" ? "" : Request["Ward"].ToString());
                                    cmd.Parameters.Add("@DischargeDocID", SqlDbType.BigInt).Value = Convert.ToInt64(Request["DischargeBy"].ToString() == "" ? "" : Request["DischargeBy"].ToString());
                                    cmd.Parameters.Add("@DischargeTypeID", SqlDbType.Int).Value = Convert.ToInt32(Request["DischargeType"].ToString() == "" ? "" : Request["DischargeType"].ToString());
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

                     case "StaffFundList":

                        DataTable dt4 = new DataTable();
                        crystalReport.Load(Server.MapPath("StaffFundReport.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "MDQ_get_StaffFund_details_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Now : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
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
                     case "AdmissionList":

                        DataTable dt3 = new DataTable();
                        crystalReport.Load(Server.MapPath("AdmissionList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GetAdmissionListReport_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@DeptID", SqlDbType.Int).Value = Convert.ToInt32(Request["Dept"].ToString() == "" ? "0" : Request["Dept"].ToString());
                                    cmd.Parameters.Add("@DocID", SqlDbType.Int).Value = Convert.ToInt64(Request["Doctor"].ToString() == "" ? "0" : Request["Doctor"].ToString());
                                    cmd.Parameters.Add("@Gender", SqlDbType.Int).Value = Convert.ToInt32(Request["Gender"].ToString() == "" ? "0" : Request["Gender"].ToString());
                                    cmd.Parameters.Add("@AgeFrom", SqlDbType.Int).Value = Convert.ToInt32(Request["AgeFrom"].ToString() == "" ? "0" : Request["AgeFrom"].ToString());
                                    cmd.Parameters.Add("@AgeTo", SqlDbType.Int).Value = Convert.ToInt32(Request["AgeTo"].ToString() == "" ? "200" : Request["AgeTo"].ToString());
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@PatientType", SqlDbType.Int).Value = Convert.ToInt32(Request["PatientType"].ToString() == "" ? "0" : Request["PatientType"].ToString());
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


                     case "OTList":

                        DataTable dt6 = new DataTable();

                     crystalReport.Load(Server.MapPath("OT_procedureListReport.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_OT_ListReport_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@OTtype", SqlDbType.Int).Value = Convert.ToInt32(Request["OtTheater"].ToString() == "" ? null : Request["OtTheater"].ToString());
                                    cmd.Parameters.Add("@OTemployeeID", SqlDbType.Int).Value = Convert.ToInt64(Request["OtDoc"].ToString() == "" ? "0" : Request["OtDoc"].ToString());
                                    cmd.Parameters.Add("@otCase", SqlDbType.Int).Value = Convert.ToInt32(Request["otCase"].ToString() == "" ? "0" : Request["otCase"].ToString());
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


                     case "LabBillingList":
                        DataTable dt7 = new DataTable();
                        crystalReport.Load(Server.MapPath("LabBillingReport.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GetLabBillListReport_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Now : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Paymode", SqlDbType.Int).Value = Convert.ToInt32(Request["Paymode"].ToString() == "" ? "0" : Request["Paymode"].ToString());
                                    cmd.Parameters.Add("@AmountEnable", SqlDbType.Int).Value = LogData.AmountEnable;
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
  

                     case "EmergencyList":
                        DataTable dt8 = new DataTable();
                        crystalReport.Load(Server.MapPath("EmergencyListReport.rpt"));

                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_Print_EmrgListReport_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@Datefrom", SqlDbType.DateTime).Value = Request["Datefrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["Datefrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Dateto", SqlDbType.DateTime).Value = Request["Dateto"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["Dateto"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

                                    //cmd.Parameters.Add("@DeptID", SqlDbType.Int).Value = Convert.ToInt32(Request["Dept"].ToString() == "" ? null : Request["Dept"].ToString());
                                    cmd.Parameters.Add("@DocID", SqlDbType.Int).Value = Convert.ToInt64(Request["Doc"].ToString() == "" ? null : Request["Doc"].ToString());
                                    cmd.Parameters.Add("@DischargeStatus", SqlDbType.Int).Value = Convert.ToInt32(Request["Discharge"].ToString() == "" ? null : Request["Discharge"].ToString());
                                    cmd.Parameters.Add("@GenID", SqlDbType.Int).Value = Convert.ToInt32(Request["gender"].ToString() == "" ? null : Request["gender"].ToString());
                                    cmd.Parameters.Add("@Agefrom", SqlDbType.Int).Value = Convert.ToInt32(Request["agefrom"].ToString() == "" ? "0" : Request["agefrom"].ToString());
                                    cmd.Parameters.Add("@Ageto", SqlDbType.Int).Value = Convert.ToInt32(Request["ageto"].ToString() == "" ? "200" : Request["ageto"].ToString());
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

                     case "DischargeReadyList":

                        DataTable dt9 = new DataTable();
                        crystalReport.Load(Server.MapPath("DischargeReady.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GET_DischrgReadyList_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@WardID", SqlDbType.Int).Value = Convert.ToInt32(Request["Ward"].ToString() == "" ? "" : Request["Ward"].ToString());
                                    cmd.Parameters.Add("@DischargeDocID", SqlDbType.BigInt).Value = Convert.ToInt64(Request["DischargeBy"].ToString() == "" ? "" : Request["DischargeBy"].ToString());
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

                     case "ServiceBillingReport":

                        DataTable dt10 = new DataTable();
                        crystalReport.Load(Server.MapPath("ServiceBillingReport.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GetOPDBillListReport_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Now : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@Paymode", SqlDbType.Int).Value = Convert.ToInt32(Request["Paymode"].ToString() == "" ? "0" : Request["Paymode"].ToString());
                                    cmd.Parameters.Add("@AmountEnable", SqlDbType.Int).Value = LogData.AmountEnable;
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

                     case "NursingCareList":

                        DataTable dt11 = new DataTable();
                        crystalReport.Load(Server.MapPath("NursingCareReport.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "MDQ_get_NursingCareDetails_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Now : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@serviceID", SqlDbType.Int).Value = Convert.ToInt32(Request["ServiceID"].ToString() == "" ? "0" : Request["ServiceID"].ToString());
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
                     case "ServicesDetailsList":

                        DataTable dt12 = new DataTable();
                        crystalReport.Load(Server.MapPath("ServicesReport.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "MDQ_get_ServiceDetailsReport_RPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.UserName;
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Now : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@serviceID", SqlDbType.Int).Value = Convert.ToInt32(Request["ServiceID"].ToString() == "" ? "0" : Request["ServiceID"].ToString());

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

                     case "PatientListDepartmentWise":
                        DataTable dt13 = new DataTable();
                        crystalReport.Load(Server.MapPath("DeptWisePatientList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GetAdmissionListDeptWiseRPT";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.EmpName;
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DeptID", SqlDbType.Int).Value = Convert.ToInt32(Request["DeptID"].ToString() == "" ? "0" : Request["DeptID"].ToString());
                                    cmd.Parameters.Add("@PatientType", SqlDbType.Int).Value = Convert.ToInt32(Request["PatientType"].ToString() == "" ? "0" : Request["PatientType"].ToString());
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

                     case "PatientListDoctorWise":
                        DataTable dt14 = new DataTable();
                        crystalReport.Load(Server.MapPath("DocWisePatientList.rpt"));
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_MDQ_GetAdmissionListDoctorwiseWise";
                                    cmd.Parameters.Add("@LoginName", SqlDbType.VarChar).Value = LogData.EmpName;
                                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Request["DateFrom"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["DateFrom"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Request["DateTo"].ToString() == "" ? System.DateTime.Today : DateTime.Parse(Request["DateTo"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                    cmd.Parameters.Add("@DocID", SqlDbType.Int).Value = Convert.ToInt32(Request["DocID"].ToString() == "" ? "0" : Request["DocID"].ToString());
                                    cmd.Parameters.Add("@PatientType", SqlDbType.Int).Value = Convert.ToInt32(Request["PatientType"].ToString() == "" ? "0" : Request["PatientType"].ToString());
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

                }
            }
        }
     }
}