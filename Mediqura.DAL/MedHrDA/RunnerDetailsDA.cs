using Mediqura.CommonData.MedHrData;
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

namespace Mediqura.DAL.MedHrDA
{
    public class RunnerDetailsDA
    {
        public int UpdateRunnerDetails(RunnerDetailsData objData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[12];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objData.ID;

                    arParms[1] = new SqlParameter("@RunnerCode", SqlDbType.VarChar);
                    arParms[1].Value = objData.RunnerCode;

                    arParms[2] = new SqlParameter("@RunnerName", SqlDbType.VarChar);
                    arParms[2].Value = objData.RunnerName;

                    arParms[3] = new SqlParameter("@RunnerAddress", SqlDbType.VarChar);
                    arParms[3].Value = objData.RunnerAddress;

                    arParms[4] = new SqlParameter("@ContactNo", SqlDbType.VarChar);
                    arParms[4].Value = objData.ContactNo;

                    arParms[5] = new SqlParameter("@RunnerTax", SqlDbType.Decimal);
                    arParms[5].Value = objData.RunnerTax;

                    arParms[6] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[6].Value = objData.EmployeeID;

                    arParms[7] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[7].Value = objData.HospitalID;

                    arParms[8] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[8].Value = objData.ActionType;

                    arParms[9] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[9].Direction = ParameterDirection.Output;

                    arParms[10] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[10].Value = objData.IsActive;

                    arParms[11] = new SqlParameter("@EmailID", SqlDbType.VarChar);
                    arParms[11].Value = objData.EmailID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateRunnerDetailsMST", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[9].Value);
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
        public List<RunnerDetailsData> SearchRunnerTaxDetails(RunnerDetailsData objData)
        {
            List<RunnerDetailsData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@RunnerCode", SqlDbType.VarChar);
                    arParms[0].Value = objData.RunnerCode;

                    arParms[1] = new SqlParameter("@RunnerName", SqlDbType.VarChar);
                    arParms[1].Value = objData.RunnerName;

                    arParms[2] = new SqlParameter("@RunnerAddress", SqlDbType.VarChar);
                    arParms[2].Value = objData.RunnerAddress;

                    arParms[3] = new SqlParameter("@ContactNo", SqlDbType.VarChar);
                    arParms[3].Value = objData.ContactNo;

                    arParms[4] = new SqlParameter("@RunnerTax", SqlDbType.Decimal);
                    arParms[4].Value = objData.RunnerTax;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objData.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_SearchRunnerDetails", arParms);
                    List<RunnerDetailsData> lstRunnerDetails = ORHelper<RunnerDetailsData>.FromDataReaderToList(sqlReader);
                    result = lstRunnerDetails;
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
        public List<RunnerDetailsData> GetRunnerDetailsByID(RunnerDetailsData objdata)
        {
            List<RunnerDetailsData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objdata.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetRunnerDetailsByID", arParms);
                    List<RunnerDetailsData> lstLabUnitDetails = ORHelper<RunnerDetailsData>.FromDataReaderToList(sqlReader);
                    result = lstLabUnitDetails;
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
        public int DeleteRunnerDetailsByID(RunnerDetailsData objdata)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objdata.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objdata.EmployeeID;                          

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objdata.Remarks;

                    arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[3].Direction = ParameterDirection.Output;   

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteRunnerDetailsByID", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[3].Value);
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
