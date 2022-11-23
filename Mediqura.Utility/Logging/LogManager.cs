using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Mediqura.Utility.Logging
{  /****************************************************
   Description of the class	    : ORHelper
   Created Date					: 20-07-2016
   Developer					: Loken
   Modify Date					: 
   Modified By Developer		: 
   Comments						: ()
   ***************************************************/
    public static class LogManager
    {
        /// <summary>
        /// This is used to give the Method Name
        /// </summary>
        /// <returns></returns>
        public static string WhoCalledMe()
        {
            try
            {
                System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
                return stackTrace.GetFrame(1).GetMethod().Name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// This is used to give the NameSpace for Presentation Layer
        /// </summary>
        /// <returns></returns>
        public static string ClassName()
        {
            string rtnValue = "";
            try
            {
                System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
                rtnValue = stackTrace.GetFrame(1).GetMethod().DeclaringType.AssemblyQualifiedName;
                rtnValue = rtnValue.Substring(0, rtnValue.IndexOf(","));
            }
            catch (Exception ex)
            {
                rtnValue = "";
                throw ex;
            }
            return rtnValue;
        }
        public static void WriteLog(LogSource objLogSource)
        {
            string message = "";
            LogEntry logEntry = new LogEntry();

            if (objLogSource.Sender == null)
            {
                message = "Class Name: " + objLogSource.ClassName + " Method called: " + objLogSource.Message;
            }
            else
            {
                message = "Class Name: " + objLogSource.Sender.GetType().FullName + " Method called: " + objLogSource.Message;
            }

            logEntry.Message = message;

            logEntry.Priority = (int)objLogSource.Priority;
            //Adding one beacuse 0 is reserved for exception handling logging
            logEntry.Priority = logEntry.Priority + 1;

            logEntry.EventId = 100;

            if (objLogSource.EventType == EnumLogEvenType.Error)
                logEntry.Severity = TraceEventType.Error;
            else if (objLogSource.EventType == EnumLogEvenType.Warning)
                logEntry.Severity = TraceEventType.Warning;
            else if (objLogSource.EventType == EnumLogEvenType.Information)
                logEntry.Severity = TraceEventType.Information;


            if (objLogSource.LogCategory == EnumLogCategory.UIEvents)
            {
                logEntry.Categories.Add("UI Events");
                logEntry.Title = "UI Events";
            }
            else if (objLogSource.LogCategory == EnumLogCategory.ServiceAgentEvents)
            {
                logEntry.Categories.Add("Service Agent Events");
                logEntry.Title = "Service Agent Events";
            }
            else if (objLogSource.LogCategory == EnumLogCategory.BusinessProcessEvents)
            {
                logEntry.Categories.Add("Business Process Events");
                logEntry.Title = "Business Process Events";
            }
            else if (objLogSource.LogCategory == EnumLogCategory.DataAccessEvents)
            {
                logEntry.Categories.Add("Data Access Events");
                logEntry.Title = "Data Access Events";
            }
            else if (objLogSource.LogCategory == EnumLogCategory.ServiceEvents)
            {
                logEntry.Categories.Add("Service Events");
                logEntry.Title = "Service Events";
            }
            else if (objLogSource.LogCategory == EnumLogCategory.Debug) 
            {
                logEntry.Categories.Add("Debug");
                logEntry.Title = "Debug";
            }
            else if (objLogSource.LogCategory == EnumLogCategory.Troubleshooting)
            {
                logEntry.Categories.Add("Troubleshooting");
                logEntry.Title = "Troubleshooting";
            }

            if (Logger.ShouldLog(logEntry))
            {
                // Perform possibly expensive operations gather information for the  
                // event to be logged.
                Logger.Write(logEntry);
            }
        }
        public static void LogMedError(Exception ex)
        {
            LogMedError(ex, EnumErrorLogSourceTier.DB);
        }
        public static void LogMedError(Exception ex, EnumErrorLogSourceTier LogSourceTier)
        {
            if (ex != null)
            {
                if (ex is SqlException)
                {
                    SqlException sqlex = (SqlException)ex;
                    StringBuilder errorMessages = new StringBuilder();
                    for (int i = 0; i < sqlex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index # " + i + "\n" +
                            "Message : " + sqlex.Errors[i].Message + "\n" +
                            "LineNumber : " + sqlex.Errors[i].LineNumber + "\n" +
                            "Source : " + sqlex.Errors[i].Source + "\n" +
                            "Procedure : " + sqlex.Errors[i].Procedure + "\n");
                    }

                    LogManager.LogMedError(new ErorrLog(LogSourceTier, sqlex.LineNumber, sqlex.Number, (int)sqlex.State, sqlex.Procedure, errorMessages.ToString()));
                }
                else
                {
                    LogManager.LogMedError(new ErorrLog(LogSourceTier, 0, 0, 0, "", ex.ToString()));
                }
            }
        }
        public static void LogMedError(string ErrorMessageDetails, EnumErrorLogSourceTier LogSourceTier)
        {
            LogManager.LogMedError(new ErorrLog(LogSourceTier, 0, 0, 0, "", ErrorMessageDetails));
        }
        public static void LogMedError (ErorrLog objErorrLog)
        {
            if (objErorrLog != null)
            {
                SqlConnection con = new SqlConnection(GlobalConstant.ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_MDQ_UpdateErrorLogDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parm1 = new SqlParameter("@ERROR_LINE_Number", SqlDbType.Int);
                parm1.Value = objErorrLog.ErrorLineNumber;
                cmd.Parameters.Add(parm1);

                SqlParameter parm2 = new SqlParameter("@ERROR_STATE_Status", SqlDbType.Int);
                parm2.Value = objErorrLog.ErrorStateStatus;
                cmd.Parameters.Add(parm2);

                SqlParameter parm3 = new SqlParameter("@ERROR_MESSAGE_Details", SqlDbType.VarChar);
                parm3.Value = objErorrLog.ErrorMessageDetails;
                cmd.Parameters.Add(parm3);

                SqlParameter parm4 = new SqlParameter("@ERROR_SEVERITY_Number", SqlDbType.Int);
                parm4.Value = objErorrLog.ErrorSeverityNumber;
                cmd.Parameters.Add(parm4);

                SqlParameter parm5 = new SqlParameter("@ProcedureName", SqlDbType.VarChar);
                parm5.Value = objErorrLog.ProcedureName;
                cmd.Parameters.Add(parm5);

                SqlParameter parm6 = new SqlParameter("@LogSourceTier", SqlDbType.VarChar);
                parm6.Value = objErorrLog.LogSourceTier.ToString();
                cmd.Parameters.Add(parm6);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }

        }
    }
}
