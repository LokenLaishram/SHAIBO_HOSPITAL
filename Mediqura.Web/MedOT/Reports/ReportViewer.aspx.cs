using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using Mediqura.Utility;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedOT.Reports
{
    public partial class ReportViewer : BasePage
    {
        ReportDocument reportDocument = new ReportDocument();
        ParameterFields paramFields = new ParameterFields();
        CrystalReportSource crystalReportSource = new CrystalReportSource();
        protected void Page_Unload(Object sender, EventArgs evntArgs)
        {
            reportDocument.Close();
            reportDocument.Dispose();
            reportDocument = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["option"] != null)
            {

                //For First Parameter
                ParameterField paramField1 = new ParameterField();
                ParameterDiscreteValue paramDiscreteValue1 = new ParameterDiscreteValue();

                ParameterField paramField2 = new ParameterField();
                ParameterDiscreteValue paramDiscreteValue2 = new ParameterDiscreteValue();

                ParameterField paramField3 = new ParameterField();
                ParameterDiscreteValue paramDiscreteValue3 = new ParameterDiscreteValue();


                ParameterField paramField4 = new ParameterField();
                ParameterDiscreteValue paramDiscreteValue4 = new ParameterDiscreteValue();


                ParameterField paramField5 = new ParameterField();
                ParameterDiscreteValue paramDiscreteValue5 = new ParameterDiscreteValue();

                ParameterField paramField6 = new ParameterField();
                ParameterDiscreteValue paramDiscreteValue6 = new ParameterDiscreteValue();

                ParameterField paramField7 = new ParameterField();
                ParameterDiscreteValue paramDiscreteValue7 = new ParameterDiscreteValue();

                ParameterField paramField8 = new ParameterField();
                ParameterDiscreteValue paramDiscreteValue8 = new ParameterDiscreteValue();

                ParameterField paramField9 = new ParameterField();
                ParameterDiscreteValue paramDiscreteValue9 = new ParameterDiscreteValue();

                ParameterField paramField10 = new ParameterField();
                ParameterDiscreteValue paramDiscreteValue10 = new ParameterDiscreteValue();

                ParameterField paramField11 = new ParameterField();
                ParameterDiscreteValue paramDiscreteValue11 = new ParameterDiscreteValue();

                ParameterField paramField12 = new ParameterField();
                ParameterDiscreteValue paramDiscreteValue12 = new ParameterDiscreteValue();

                ParameterField paramField13 = new ParameterField();
                ParameterDiscreteValue paramDiscreteValue13 = new ParameterDiscreteValue();

                ParameterField paramField14 = new ParameterField();
                ParameterDiscreteValue paramDiscreteValue14 = new ParameterDiscreteValue();


                ParameterField paramLoginName = new ParameterField();
                ParameterDiscreteValue paramDiscreteLoginName = new ParameterDiscreteValue();

                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                paramLoginName.Name = "@LoginName";
                paramDiscreteLoginName.Value = LogData.UserName;
                paramLoginName.CurrentValues.Add(paramDiscreteLoginName);
                paramFields.Add(paramLoginName);

                MediReportViewer.RefreshReport();
                switch (Request["option"].ToString())
                {
                    case "OTStatus":

                        paramField1.Name = "@IPNo";
                        paramDiscreteValue1.Value = Request["IPNo"].ToString() == "" ? null : Request["IPNo"].ToString();
                        paramField1.CurrentValues.Add(paramDiscreteValue1);
                        paramFields.Add(paramField1);

                        paramField2.Name = "@Name";
                        paramDiscreteValue2.Value = Request["Name"].ToString() == "" ? null : Request["Name"].ToString();
                        paramField2.CurrentValues.Add(paramDiscreteValue2);
                        paramFields.Add(paramField2);

                        paramField3.Name = "@OperationDate";
                        paramDiscreteValue3.Value = Request["OperationDate"].ToString() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(Request["OperationDate"].ToString(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                        paramField3.CurrentValues.Add(paramDiscreteValue3);
                        paramFields.Add(paramField3);



                        reportDocument.Load(Server.MapPath("OT_StatusList.rpt"));

                        MediReportViewer.ParameterFieldInfo = paramFields;
                        MediReportViewer.ReportSource = SetDatabaseInfo(reportDocument);
                        MediReportViewer.DisplayToolbar = true;
                        MediReportViewer.HasCrystalLogo = false;
                        MediReportViewer.HasToggleGroupTreeButton = false;
                        MediReportViewer.HasSearchButton = false;
                        MediReportViewer.HasViewList = false;
                        MediReportViewer.HasDrillUpButton = false;
                        MediReportViewer.HasZoomFactorList = false;
                        MediReportViewer.DisplayGroupTree = false;
                        MediReportViewer.EnableParameterPrompt = false;
                        break;

                    case "OT_RegnProfile":

                        paramField1.Name = "@IPNo";
                        paramDiscreteValue1.Value = Request["IPNo"].ToString() == "" ? null : Request["IPNo"].ToString();
                        paramField1.CurrentValues.Add(paramDiscreteValue1);
                        paramFields.Add(paramField1);




                        reportDocument.Load(Server.MapPath("OT_regnProfileNew.rpt"));

                        MediReportViewer.ParameterFieldInfo = paramFields;
                        MediReportViewer.ReportSource = SetDatabaseInfo(reportDocument);
                        MediReportViewer.DisplayToolbar = true;
                        MediReportViewer.HasCrystalLogo = false;
                        MediReportViewer.HasToggleGroupTreeButton = false;
                        MediReportViewer.HasSearchButton = false;
                        MediReportViewer.HasViewList = false;
                        MediReportViewer.HasDrillUpButton = false;
                        MediReportViewer.HasZoomFactorList = false;
                        MediReportViewer.DisplayGroupTree = false;
                        MediReportViewer.EnableParameterPrompt = false;
                        break;
                }
            }
        }
        public ReportDocument SetDatabaseInfo(ReportDocument crReportDocument)
        {

            try
            {
                // CR variables		
                Database crDatabase;
                Tables crTables;
                TableLogOnInfo crTableLogOnInfo;
                ConnectionInfo crConnectionInfo;
                crConnectionInfo = new ConnectionInfo();

                ReportObjects crReportObjects;
                Sections crSections;
                ReportDocument crSubreportDocument;
                SubreportObject crSubreportObject;

                crConnectionInfo.ServerName = System.Configuration.ConfigurationManager.AppSettings["ReportServerName"]; ;
                crConnectionInfo.DatabaseName = System.Configuration.ConfigurationManager.AppSettings["ReportDatabase"];
                crConnectionInfo.UserID = System.Configuration.ConfigurationManager.AppSettings["ReportUserId"];
                crConnectionInfo.Password = System.Configuration.ConfigurationManager.AppSettings["ReportPassword"];
                //Get the tables collection from the report object
                crDatabase = crReportDocument.Database;
                crTables = crDatabase.Tables;
                //Apply the logon information to each table in the collection
                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    crTableLogOnInfo = crTable.LogOnInfo;
                    crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                    crTable.ApplyLogOnInfo(crTableLogOnInfo);
                }


                crSections = crReportDocument.ReportDefinition.Sections;
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

            }
            catch
            {
                throw;
            }

            return crReportDocument;
        }
    }
}