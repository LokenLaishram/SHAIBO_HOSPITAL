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
    public class PhrAlikeDA
    {
        public int UpdatePhrLookAlikeDetails(AlikeData obj)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = obj.ID;

                    arParms[1] = new SqlParameter("@Code", SqlDbType.VarChar);
                    arParms[1].Value = obj.Code;

                    arParms[2] = new SqlParameter("@Descriptions", SqlDbType.VarChar);
                    arParms[2].Value = obj.Descriptions;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = obj.EmployeeID;

                    arParms[4] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[4].Value = obj.HospitalID;

                    arParms[5] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[5].Value = obj.ActionType;

                    arParms[6] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[6].Direction = ParameterDirection.Output;

                    arParms[7] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[7].Value = obj.IsActive;

                    arParms[8] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[8].Value = obj.FinancialYearID;

                    arParms[9] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[9].Value = obj.IPaddress;



                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_UpdatePhrLookAlikeMST", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[7].Value);
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
        public List<AlikeData> GetPhrLookAlikeDetailsByID(AlikeData objItemMaster)
        {
            List<AlikeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objItemMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_util_GeTLookAlikeDetailsByID", arParms);
                    List<AlikeData> lstDetails = ORHelper<AlikeData>.FromDataReaderToList(sqlReader);
                    result = lstDetails;
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
        public int DeletePhrLookDetailsByID(AlikeData obj)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = obj.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[1].Value = obj.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = obj.Remarks;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_util_DeleteLookAlikeDetailsByID", arParms);
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
        public List<AlikeData> SearchLookAlikeDetails(AlikeData obj)
        {
            List<AlikeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@Code", SqlDbType.VarChar);
                    arParms[0].Value = obj.Code;

                    arParms[1] = new SqlParameter("@Description", SqlDbType.VarChar);
                    arParms[1].Value = obj.Descriptions;

                    arParms[2] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[2].Value = obj.IsActive;

                    arParms[3] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[3].Value = obj.CurrentIndex;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_SearchLookAlikeDetails", arParms);
                    List<AlikeData> lstItemMasterDetails = ORHelper<AlikeData>.FromDataReaderToList(sqlReader);
                    result = lstItemMasterDetails;
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
        public List<AlikeData> SearchLookAlikeType(AlikeData obj)
        {
            List<AlikeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@Code", SqlDbType.VarChar);
                    arParms[0].Value = obj.Code;

                    arParms[1] = new SqlParameter("@Description", SqlDbType.VarChar);
                    arParms[1].Value = obj.Descriptions;

                    arParms[2] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[2].Value = obj.IsActive;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_SearchLookAlikeType", arParms);
                    List<AlikeData> lstItemMasterDetails = ORHelper<AlikeData>.FromDataReaderToList(sqlReader);
                    result = lstItemMasterDetails;
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
        public int UpdatePhrSoundAlikeDetails(AlikeData obj)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = obj.ID;

                    arParms[1] = new SqlParameter("@Code", SqlDbType.VarChar);
                    arParms[1].Value = obj.Code;

                    arParms[2] = new SqlParameter("@Descriptions", SqlDbType.VarChar);
                    arParms[2].Value = obj.Descriptions;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = obj.EmployeeID;

                    arParms[4] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[4].Value = obj.HospitalID;

                    arParms[5] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[5].Value = obj.ActionType;

                    arParms[6] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[6].Direction = ParameterDirection.Output;

                    arParms[7] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[7].Value = obj.IsActive;

                    arParms[8] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[8].Value = obj.FinancialYearID;

                    arParms[9] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[9].Value = obj.IPaddress;



                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_UpdateSoundAlikeMST", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[7].Value);
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
        public List<AlikeData> GetPhrSoundAlikeDetailsByID(AlikeData objItemMaster)
        {
            List<AlikeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objItemMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_util_GeTSoundAlikeDetailsByID", arParms);
                    List<AlikeData> lstItemMasterDetails = ORHelper<AlikeData>.FromDataReaderToList(sqlReader);
                    result = lstItemMasterDetails;
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
        public int DeletePhrSoundDetailsByID(AlikeData obj)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = obj.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[1].Value = obj.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = obj.Remarks;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_util_DeleteSoundAlikeDetailsByID", arParms);
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
        public List<AlikeData> SearchPhrSoundAlikeDetails(AlikeData obj)
        {
            List<AlikeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@Code", SqlDbType.VarChar);
                    arParms[0].Value = obj.Code;

                    arParms[1] = new SqlParameter("@Description", SqlDbType.VarChar);
                    arParms[1].Value = obj.Descriptions;

                    arParms[2] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[2].Value = obj.IsActive;

                    arParms[3] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[3].Value = obj.CurrentIndex;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_SearchSoundAlikeDetails", arParms);
                    List<AlikeData> lstItemMasterDetails = ORHelper<AlikeData>.FromDataReaderToList(sqlReader);
                    result = lstItemMasterDetails;
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
        public List<AlikeData> SearchPhrSoundAlikeType(AlikeData obj)
        {
            List<AlikeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@Code", SqlDbType.VarChar);
                    arParms[0].Value = obj.Code;

                    arParms[1] = new SqlParameter("@Description", SqlDbType.VarChar);
                    arParms[1].Value = obj.Descriptions;

                    arParms[2] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[2].Value = obj.IsActive;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_SearchSoundAlikeType", arParms);
                    List<AlikeData> lstItemMasterDetails = ORHelper<AlikeData>.FromDataReaderToList(sqlReader);
                    result = lstItemMasterDetails;
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
