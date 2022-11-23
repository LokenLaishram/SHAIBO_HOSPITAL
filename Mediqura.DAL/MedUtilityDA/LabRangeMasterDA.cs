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
    public class LabRangeMasterDA
    {
        public int UpdateLabRangeDetails(LabRangeMasterData objLabRangeMasterData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[12];

                    arParms[0] = new SqlParameter("@RangeID", SqlDbType.Int);
                    arParms[0].Value = objLabRangeMasterData.ID;

                    arParms[1] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[1].Value = objLabRangeMasterData.TestName;

                    arParms[2] = new SqlParameter("@Units", SqlDbType.VarChar);
                    arParms[2].Value = objLabRangeMasterData.Units;

                    arParms[3] = new SqlParameter("@Age", SqlDbType.VarChar);
                    arParms[3].Value = objLabRangeMasterData.Age;

                    arParms[4] = new SqlParameter("@Gender", SqlDbType.VarChar);
                    arParms[4].Value = objLabRangeMasterData.Gender;

                    arParms[5] = new SqlParameter("@NormalRange", SqlDbType.VarChar);
                    arParms[5].Value = objLabRangeMasterData.NormalRange;

                    arParms[6] = new SqlParameter("@MachineName", SqlDbType.VarChar);
                    arParms[6].Value = objLabRangeMasterData.MachineName;

                    arParms[7] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[7].Value = objLabRangeMasterData.EmployeeID;

                    arParms[8] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[8].Value = objLabRangeMasterData.HospitalID;

                    arParms[9] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[9].Value = objLabRangeMasterData.ActionType;

                    arParms[10] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[10].Direction = ParameterDirection.Output;

                    arParms[11] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[11].Value = objLabRangeMasterData.IPaddress;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateLabRangeMST", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[10].Value);
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



        public List<LabRangeMasterData> GetLabRangeDetailsByID(LabRangeMasterData objLabRangeMaster)
        {
            List<LabRangeMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@RangeID", SqlDbType.Int);
                    arParms[0].Value = objLabRangeMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetLabRangeDetailsByID", arParms);
                    List<LabRangeMasterData> lstPatientTypeDetails = ORHelper<LabRangeMasterData>.FromDataReaderToList(sqlReader);
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




        public int DeleteLabRangeDetailsByID(LabRangeMasterData objLabRangeMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@RangeID", SqlDbType.Int);
                    arParms[0].Value = objLabRangeMaster.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objLabRangeMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objLabRangeMaster.Remarks;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objLabRangeMaster.IPaddress;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteLabRangeDetailsByID", arParms);
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
        public List<LabRangeMasterData> SearchLabRangeDetails(LabRangeMasterData objLabRangeMaster)
        {
            List<LabRangeMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[0].Value = objLabRangeMaster.TestName;

                    arParms[1] = new SqlParameter("@MachineName", SqlDbType.VarChar);
                    arParms[1].Value = objLabRangeMaster.MachineName;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchLabRange", arParms);
                    List<LabRangeMasterData> lstPaymentTypeDetails = ORHelper<LabRangeMasterData>.FromDataReaderToList(sqlReader);
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

        public List<LabSampleData> SearchSampleTypeDetails(LabSampleData objPaymentTypeMaster)
        {
            List<LabSampleData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@Code", SqlDbType.VarChar);
                    arParms[0].Value = objPaymentTypeMaster.LabSampleTypeCode;

                    arParms[1] = new SqlParameter("@Descriptions", SqlDbType.VarChar);
                    arParms[1].Value = objPaymentTypeMaster.LabSampleType;

                    arParms[2] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[2].Value = objPaymentTypeMaster.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchLabRange", arParms);
                    List<LabSampleData> lstPaymentTypeDetails = ORHelper<LabSampleData>.FromDataReaderToList(sqlReader);
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

        public List<LabRangeMasterData> GetTestName(LabRangeMasterData objD)
        {
            List<LabRangeMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[0].Value = objD.TestName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetTestName", arParms);
                    //sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_OPD_Service_ChargeList", arParms);
                    List<LabRangeMasterData> lstresult = ORHelper<LabRangeMasterData>.FromDataReaderToList(sqlReader);
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

        public int UpdateLabRangeTypeDetailsByID(LabRangeMasterData objLabRangeMasterData)
        {
            throw new NotImplementedException();
        }
        public List<LabRangeMasterData> GetLabServiceChargeByID(LabRangeMasterData objnormalRange)
        {
            List<LabRangeMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objnormalRange.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_OPD_LabService_ChargeList", arParms);
                    List<LabRangeMasterData> testlist = ORHelper<LabRangeMasterData>.FromDataReaderToList(sqlReader);
                    result = testlist;
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
