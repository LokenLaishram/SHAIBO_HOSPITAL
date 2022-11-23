using Mediqura.CommonData.MedStore;
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

namespace Mediqura.DAL.MedStore
{
    public class UtilizationReportDA
    {
        public List<UtilizationReportData> GetSubStockItemName(UtilizationReportData objD)
        {
            List<UtilizationReportData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    arParms[1] = new SqlParameter("@SubStockID", SqlDbType.Int);
                    arParms[1].Value = objD.GenStockID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Utilization_Get_SubstockItemNameList", arParms);
                    List<UtilizationReportData> lstresult = ORHelper<UtilizationReportData>.FromDataReaderToList(sqlReader);
                    result = lstresult;
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
        public List<UtilizationReportData> GetUtilizationItemList(UtilizationReportData objutil)
        {
            List<UtilizationReportData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[0].Value = objutil.FinancialYearID;

                    arParms[1] = new SqlParameter("@MonthID", SqlDbType.Int);
                    arParms[1].Value = objutil.MonthID;
                   
                    arParms[2] = new SqlParameter("@GenStockID", SqlDbType.Int);
                    arParms[2].Value = objutil.GenStockID;

                    arParms[3] = new SqlParameter("@ItemID", SqlDbType.Int);
                    arParms[3].Value = objutil.ItemID;

                    arParms[4] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[4].Value = objutil.DateFrom;

                    arParms[5] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[5].Value = objutil.DateTo;

                    arParms[6] = new SqlParameter("@AddedBy", SqlDbType.Int);
                    arParms[6].Value = objutil.EmployeeID;

                    arParms[7] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[7].Value = objutil.HospitalID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Generate_SubstockwiseUtilizationReport", arParms);
                    List<UtilizationReportData> listUtilizationdetails = ORHelper<UtilizationReportData>.FromDataReaderToList(sqlReader);
                    result = listUtilizationdetails;
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
