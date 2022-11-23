using Mediqura.CommonData.MedAccount;
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

namespace Mediqura.DAL.MedAccount
{
    public class LabIncomeCollectionDA
    {
        public List<LabIncomeCollectionData> GetLabIncomeList(LabIncomeCollectionData objbill)
        {
            List<LabIncomeCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.ID;

                    arParms[1] = new SqlParameter("@TransactionTypeID", SqlDbType.Int);
                    arParms[1].Value = objbill.TransactionTypeID;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[3].Value = objbill.DateTo;

                    arParms[4] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[4].Value = objbill.IsActive;

                    arParms[5] = new SqlParameter("@CollectedByID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.CollectedByID;

                    arParms[6] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[6].Value = objbill.CurrentIndex;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabIncomeList", arParms);
                    List<LabIncomeCollectionData> listLabIncomedetails = ORHelper<LabIncomeCollectionData>.FromDataReaderToList(sqlReader);
                    result = listLabIncomedetails;
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

        public List<LabIncomeCollectionData> GetLabExpenditureList(LabIncomeCollectionData objbill)
        {
            List<LabIncomeCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.ID;

                    arParms[1] = new SqlParameter("@TransactionTypeID", SqlDbType.Int);
                    arParms[1].Value = objbill.TransactionTypeID;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[3].Value = objbill.DateTo;

                    arParms[4] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[4].Value = objbill.IsActive;

                    arParms[5] = new SqlParameter("@CollectedByID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.CollectedByID;

                    arParms[6] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[6].Value = objbill.CurrentIndex;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabExpenditureList", arParms);
                    List<LabIncomeCollectionData> listLabIncomedetails = ORHelper<LabIncomeCollectionData>.FromDataReaderToList(sqlReader);
                    result = listLabIncomedetails;
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
