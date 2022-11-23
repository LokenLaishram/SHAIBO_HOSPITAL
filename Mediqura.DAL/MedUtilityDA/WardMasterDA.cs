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
    public class WardMasterDA
    {
        public int UpdateWardDetails(WardMasterData objBlockMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[14];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objBlockMaster.ID;

                    arParms[1] = new SqlParameter("@BlockID", SqlDbType.Int);
                    arParms[1].Value = objBlockMaster.BlockID;

                    arParms[2] = new SqlParameter("@FloorID", SqlDbType.Int);
                    arParms[2].Value = objBlockMaster.FloorID;

                    arParms[3] = new SqlParameter("@WardTypeCode", SqlDbType.VarChar);
                    arParms[3].Value = objBlockMaster.WardTypeCode;

                    arParms[4] = new SqlParameter("@WardType", SqlDbType.VarChar);
                    arParms[4].Value = objBlockMaster.WardType;

                    arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[5].Value = objBlockMaster.EmployeeID;

                    arParms[6] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[6].Value = objBlockMaster.ActionType;

                    arParms[7] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[7].Direction = ParameterDirection.Output;

                    arParms[8] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[8].Value = objBlockMaster.IsActive;

                    arParms[9] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[9].Value = objBlockMaster.HospitalID;

                    arParms[10] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[10].Value = objBlockMaster.FinancialYearID;

                    arParms[11] = new SqlParameter("@Admnchargeapplied", SqlDbType.Int);
                    arParms[11].Value = objBlockMaster.Admnchargeapplied;

                    arParms[12] = new SqlParameter("@PHRloweLimit", SqlDbType.Money);
                    arParms[12].Value = objBlockMaster.PHRloweLimit;

                    arParms[13] = new SqlParameter("@PHRupperLimit", SqlDbType.Money);
                    arParms[13].Value = objBlockMaster.PHRupperLimit;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateWardTypeMST", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[7].Value);
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
        public List<WardMasterData> GetWardTypeDetailsByID(WardMasterData objBlockTypeMaster)
        {
            List<WardMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objBlockTypeMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GeTWardTypeDetailsByID", arParms);
                    List<WardMasterData> lstDepartmentTypeDetails = ORHelper<WardMasterData>.FromDataReaderToList(sqlReader);
                    result = lstDepartmentTypeDetails;
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
        public int DeleteWardTypeDetailsByID(WardMasterData objBlockTypeMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objBlockTypeMaster.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objBlockTypeMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objBlockTypeMaster.Remarks;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteWardTypeDetailsByID", arParms);
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
        public List<WardMasterData> SearchWardTypeDetails(WardMasterData objBlockTypeMaster)
        {
            List<WardMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@WardTypeCode", SqlDbType.VarChar);
                    arParms[0].Value = objBlockTypeMaster.WardTypeCode;

                    arParms[1] = new SqlParameter("@WardType", SqlDbType.VarChar);
                    arParms[1].Value = objBlockTypeMaster.WardType;

                    arParms[2] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[2].Value = objBlockTypeMaster.IsActive;

                    arParms[3] = new SqlParameter("@FloorID", SqlDbType.Int);
                    arParms[3].Value = objBlockTypeMaster.FloorID;

                    arParms[4] = new SqlParameter("@BlockID", SqlDbType.Int);
                    arParms[4].Value = objBlockTypeMaster.BlockID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchWardType", arParms);
                    List<WardMasterData> lstBlockTypeDetails = ORHelper<WardMasterData>.FromDataReaderToList(sqlReader);
                    result = lstBlockTypeDetails;
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
