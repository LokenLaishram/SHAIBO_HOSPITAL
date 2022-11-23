using Mediqura.CommonData.MIS;
using Mediqura.DAL;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Utility.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MIS
{
    public class MisReportDA
    {
        public List<MisReportData> GetTransactionList(MisReportData objbill)
        {
            List<MisReportData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@ledger", SqlDbType.Int);
                    arParms[0].Value = objbill.LedgerID;

                    arParms[1] = new SqlParameter("@dateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objbill.DateFrom;

                    arParms[2] = new SqlParameter("@dateto", SqlDbType.DateTime);
                    arParms[2].Value = objbill.DateTo;

                    arParms[3] = new SqlParameter("@tranactionType", SqlDbType.Int);
                    arParms[3].Value = objbill.TransactionType;

                    arParms[4] = new SqlParameter("@CollectedBy", SqlDbType.BigInt);
                    arParms[4].Value = objbill.CollectedByID;

                    arParms[5] = new SqlParameter("@accountState", SqlDbType.Int);
                    arParms[5].Value = objbill.AccountState;

                    arParms[6] = new SqlParameter("@LoginName", SqlDbType.VarChar);
                    arParms[6].Value = objbill.EmpName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Mis_GetTransactionList", arParms);
                    List<MisReportData> listpatientdetails = ORHelper<MisReportData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public List<MisSummaryReport> GetTransactionSummaryList(MisReportData objbill)
        {
            List<MisSummaryReport> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@ledger", SqlDbType.Int);
                    arParms[0].Value = objbill.LedgerID;

                    arParms[1] = new SqlParameter("@dateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objbill.DateFrom;

                    arParms[2] = new SqlParameter("@dateto", SqlDbType.DateTime);
                    arParms[2].Value = objbill.DateTo;

                    arParms[3] = new SqlParameter("@tranactionType", SqlDbType.Int);
                    arParms[3].Value = objbill.TransactionType;

                    arParms[4] = new SqlParameter("@CollectedBy", SqlDbType.BigInt);
                    arParms[4].Value = objbill.CollectedByID;

                    arParms[5] = new SqlParameter("@accountState", SqlDbType.Int);
                    arParms[5].Value = objbill.AccountState;

                    arParms[6] = new SqlParameter("@LoginName", SqlDbType.VarChar);
                    arParms[6].Value = objbill.EmpName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Mis_GetTransactionSummaryList", arParms);
                    List<MisSummaryReport> listpatientdetails = ORHelper<MisSummaryReport>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }

    }
}