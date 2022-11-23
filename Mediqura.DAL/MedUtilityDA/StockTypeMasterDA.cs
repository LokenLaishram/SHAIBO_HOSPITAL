using Mediqura.CommonData.MedUtilityData;
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

namespace Mediqura.DAL.MedUtilityDA
{
   public class StockTypeMasterDA
    {
       public int UpdateStockTypeDetails(StockTypeMasterData objPaymentTypeMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objPaymentTypeMaster.ID;

                    arParms[1] = new SqlParameter("@StockTypeCode", SqlDbType.VarChar);
                    arParms[1].Value = objPaymentTypeMaster.StockTypeCode;

                    arParms[2] = new SqlParameter("@StockType", SqlDbType.VarChar);
                    arParms[2].Value = objPaymentTypeMaster.StockType;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objPaymentTypeMaster.EmployeeID;

                    arParms[4] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[4].Value = objPaymentTypeMaster.HospitalID;

                    arParms[5] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[5].Value = objPaymentTypeMaster.ActionType;

                    arParms[6] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[6].Direction = ParameterDirection.Output;

                    arParms[7] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[7].Value = objPaymentTypeMaster.IsActive;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateStockTypeMST", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[6].Value);
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
       public List<StockTypeMasterData> GetStockTypeDetailsByID(StockTypeMasterData objPaymentTypeMaster)
        {
            List<StockTypeMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objPaymentTypeMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GeTStockTypeDetailsByID", arParms);
                    List<StockTypeMasterData> lstPatientTypeDetails = ORHelper<StockTypeMasterData>.FromDataReaderToList(sqlReader);
                    result = lstPatientTypeDetails;
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
       public int DeleteStockTypeDetailsByID(StockTypeMasterData objPaymentTypeMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objPaymentTypeMaster.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objPaymentTypeMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objPaymentTypeMaster.Remarks;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteStockTypeDetailsByID", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[2].Value);
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
       public List<StockTypeMasterData> SearchStockTypeDetails(StockTypeMasterData objPaymentTypeMaster)
       {
           List<StockTypeMasterData> result = null;
           try
           {
               {
                   SqlParameter[] arParms = new SqlParameter[3];

                   arParms[0] = new SqlParameter("@StockTypeCode", SqlDbType.VarChar);
                   arParms[0].Value = objPaymentTypeMaster.StockTypeCode;

                   arParms[1] = new SqlParameter("@StockType", SqlDbType.VarChar);
                   arParms[1].Value = objPaymentTypeMaster.StockType;

                   arParms[2] = new SqlParameter("@IsActive", SqlDbType.Bit);
                   arParms[2].Value = objPaymentTypeMaster.IsActive;

                   SqlDataReader sqlReader = null;
                   sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchStockType", arParms);
                   List<StockTypeMasterData> lstPaymentTypeDetails = ORHelper<StockTypeMasterData>.FromDataReaderToList(sqlReader);
                   result = lstPaymentTypeDetails;
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
