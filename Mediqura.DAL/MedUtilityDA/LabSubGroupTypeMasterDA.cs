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
    public class LabSubGroupTypeMasterDA
    {
        public int UpdateLabSubGroupTypeDetails(LabSubGroupMasterData objLabSubGroupTypeMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objLabSubGroupTypeMaster.ID;

                    arParms[1] = new SqlParameter("@LabGroupID", SqlDbType.Int);
                    arParms[1].Value = objLabSubGroupTypeMaster.LabGroupID;

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

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateLabSubGroupTypeMST", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[6].Value);
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
        public List<LabSubGroupMasterData> GetLabSubGroupTypeDetailsByID(LabSubGroupMasterData objLabSubGroupTypeMaster)
        {
            List<LabSubGroupMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objLabSubGroupTypeMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GeTLabSubGroupTypeDetailsByID", arParms);
                    List<LabSubGroupMasterData> lstLabSubGroupTypeDetails = ORHelper<LabSubGroupMasterData>.FromDataReaderToList(sqlReader);
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
        public int DeleteLabSubGroupTypeDetailsByID(LabSubGroupMasterData objLabSubGroupTypeMaster)
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

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteLabSubGroupDetailsByID", arParms);
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
        public List<LabSubGroupMasterData> SearchSubGroupDetails(LabSubGroupMasterData objLabSubGroupTypeMaster)
        {
            List<LabSubGroupMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@LabGroupID", SqlDbType.VarChar);
                    arParms[0].Value = objLabSubGroupTypeMaster.LabGroupID;

                    arParms[1] = new SqlParameter("@SubGroupCode", SqlDbType.VarChar);
                    arParms[1].Value = objLabSubGroupTypeMaster.SubGroupCode;

                    arParms[2] = new SqlParameter("@SubGroupType", SqlDbType.VarChar);
                    arParms[2].Value = objLabSubGroupTypeMaster.SubGroupType;

                    arParms[3] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[3].Value = objLabSubGroupTypeMaster.IsActive;

                    arParms[4] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[4].Value = objLabSubGroupTypeMaster.CurrentIndex;



                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchLabSubGroupType", arParms);
                    List<LabSubGroupMasterData> lstLabGroupTypeDetails = ORHelper<LabSubGroupMasterData>.FromDataReaderToList(sqlReader);
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
        public List<LabSubGroupMasterData> SearchSubGroupTypeDetails(LabSubGroupMasterData objLabSubGroupTypeMaster)
        {
            List<LabSubGroupMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@LabGroupID", SqlDbType.VarChar);
                    arParms[0].Value = objLabSubGroupTypeMaster.LabGroupID;

                    arParms[1] = new SqlParameter("@SubGroupCode", SqlDbType.VarChar);
                    arParms[1].Value = objLabSubGroupTypeMaster.SubGroupCode;

                    arParms[2] = new SqlParameter("@SubGroupType", SqlDbType.VarChar);
                    arParms[2].Value = objLabSubGroupTypeMaster.SubGroupType;

                    arParms[3] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[3].Value = objLabSubGroupTypeMaster.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchLabSubGroupDetails", arParms);
                    List<LabSubGroupMasterData> lstLabGroupTypeDetails = ORHelper<LabSubGroupMasterData>.FromDataReaderToList(sqlReader);
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
