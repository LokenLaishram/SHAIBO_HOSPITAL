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
    public class CommonSubGroupMasterDA
    {
        public int UpdateCommonSubGroupDetails(CommonSubGroupMasterData objLabSubGroupTypeMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objLabSubGroupTypeMaster.ID;

                    arParms[1] = new SqlParameter("@GroupID", SqlDbType.Int);
                    arParms[1].Value = objLabSubGroupTypeMaster.GroupID;

                    arParms[2] = new SqlParameter("@SubGroupCode", SqlDbType.VarChar);
                    arParms[2].Value = objLabSubGroupTypeMaster.SubGroupCode;

                    arParms[3] = new SqlParameter("@SubGroupType", SqlDbType.VarChar);
                    arParms[3].Value = objLabSubGroupTypeMaster.SubGroupType;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objLabSubGroupTypeMaster.EmployeeID;

                    arParms[5] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[5].Value = objLabSubGroupTypeMaster.ActionType;

                    arParms[6] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[6].Direction = ParameterDirection.Output;

                    arParms[7] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[7].Value = objLabSubGroupTypeMaster.IsActive;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateCommonSubGroupTypeMST", arParms);
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
        public List<CommonSubGroupMasterData> GetCommonSubGroupTypeDetailsByID(CommonSubGroupMasterData objLabSubGroupTypeMaster)
        {
            List<CommonSubGroupMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objLabSubGroupTypeMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GeTCommonSubGroupTypeDetailsByID", arParms);
                    List<CommonSubGroupMasterData> lstLabSubGroupTypeDetails = ORHelper<CommonSubGroupMasterData>.FromDataReaderToList(sqlReader);
                    result = lstLabSubGroupTypeDetails;
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
        public int DeleteCommonSubGroupTypeDetailsByID(CommonSubGroupMasterData objLabSubGroupTypeMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objLabSubGroupTypeMaster.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objLabSubGroupTypeMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objLabSubGroupTypeMaster.Remarks;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteCommonSubGroupDetailsByID", arParms);
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
        public List<CommonSubGroupMasterData> SearchSubGroupDetails(CommonSubGroupMasterData objLabSubGroupTypeMaster)
        {
            List<CommonSubGroupMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@GroupID", SqlDbType.VarChar);
                    arParms[0].Value = objLabSubGroupTypeMaster.GroupID;

                    arParms[1] = new SqlParameter("@SubGroupCode", SqlDbType.VarChar);
                    arParms[1].Value = objLabSubGroupTypeMaster.SubGroupCode;

                    arParms[2] = new SqlParameter("@SubGroupType", SqlDbType.VarChar);
                    arParms[2].Value = objLabSubGroupTypeMaster.SubGroupType;

                    arParms[3] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[3].Value = objLabSubGroupTypeMaster.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchCommonSubGroupType", arParms);
                    List<CommonSubGroupMasterData> lstLabGroupTypeDetails = ORHelper<CommonSubGroupMasterData>.FromDataReaderToList(sqlReader);
                    result = lstLabGroupTypeDetails;
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
