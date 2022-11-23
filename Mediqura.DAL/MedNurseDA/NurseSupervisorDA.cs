using Mediqura.CommonData.MedNurseData;
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

namespace Mediqura.DAL.MedNurseDA
{
    public class NurseSupervisorDA
    {
        public int UpdateSupervisorDetails(SupervisorData objSupervisorMSTData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[0].Value = objSupervisorMSTData.EmployeeID;

                    arParms[1] = new SqlParameter("@WardID", SqlDbType.VarChar);
                    arParms[1].Value = objSupervisorMSTData.WardID;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[2].Value = objSupervisorMSTData.HospitalID;

                    arParms[3] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[3].Value = objSupervisorMSTData.ActionType;

                    arParms[4] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[4].Direction = ParameterDirection.Output;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objSupervisorMSTData.IsActive;

                    arParms[6] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[6].Value = objSupervisorMSTData.IPaddress;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateNurseSupervisorMST", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[4].Value);
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
        public List<SupervisorData> SearchNurseSupervisorDetails(SupervisorData objSupervisorMSTData)
        {
            List<SupervisorData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objSupervisorMSTData.EmployeeID;

                    arParms[1] = new SqlParameter("@WardID", SqlDbType.Int);
                    arParms[1].Value = objSupervisorMSTData.WardID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchNurseSupervisor", arParms);
                    List<SupervisorData> lstNurseSupervisorDetails = ORHelper<SupervisorData>.FromDataReaderToList(sqlReader);
                    result = lstNurseSupervisorDetails;
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
        // 
        public int UpdateAsstSupervisorDetails(SupervisorData objSupervisorMSTData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@AsstSuperitendantID", SqlDbType.BigInt);
                    arParms[0].Value = objSupervisorMSTData.AsstSuperitendantID;

                    arParms[1] = new SqlParameter("@SupervisorID", SqlDbType.BigInt);
                    arParms[1].Value = objSupervisorMSTData.SupervisorID;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[2].Value = objSupervisorMSTData.HospitalID;

                    arParms[3] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[3].Value = objSupervisorMSTData.ActionType;

                    arParms[4] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[4].Direction = ParameterDirection.Output;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objSupervisorMSTData.IsActive;

                    arParms[6] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[6].Value = objSupervisorMSTData.IPaddress;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateNurseSuperintendent", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[4].Value);
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
        public int CancelSupervisorBoundstock(SupervisorData objSupervisorMSTData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objSupervisorMSTData.EmployeeID;

                    arParms[1] = new SqlParameter("@WardID", SqlDbType.Int);
                    arParms[1].Value = objSupervisorMSTData.WardID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_CancelSupervisorboundStocks", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[2].Value);
                    }
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
        public List<SupervisorData> SearchAsstNurseSupervisorDetails(SupervisorData objSupervisorMSTData)
        {
            List<SupervisorData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@AsstSuperitendantID", SqlDbType.BigInt);
                    arParms[0].Value = objSupervisorMSTData.AsstSuperitendantID;

                    arParms[1] = new SqlParameter("@SupervisorID", SqlDbType.BigInt);
                    arParms[1].Value = objSupervisorMSTData.SupervisorID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchAsstNursingSuperitendant", arParms);
                    List<SupervisorData> lstNurseSupervisorDetails = ORHelper<SupervisorData>.FromDataReaderToList(sqlReader);
                    result = lstNurseSupervisorDetails;
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
