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
    public class ItemSubGroupMasterDA
    {
        public int UpdateItemSubGroupDetails(ItemSubGroupMasterData objitemsubgroupData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objitemsubgroupData.ID;

                    arParms[1] = new SqlParameter("@GroupID", SqlDbType.BigInt);
                    arParms[1].Value = objitemsubgroupData.GroupID;

                    arParms[2] = new SqlParameter("@Code", SqlDbType.VarChar);
                    arParms[2].Value = objitemsubgroupData.Code;

                    arParms[3] = new SqlParameter("@ItemSubGroupType", SqlDbType.VarChar);
                    arParms[3].Value = objitemsubgroupData.ItemSubGroupType;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objitemsubgroupData.EmployeeID;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[5].Value = objitemsubgroupData.HospitalID;

                    arParms[6] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[6].Value = objitemsubgroupData.ActionType;

                    arParms[7] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[7].Direction = ParameterDirection.Output;

                    arParms[8] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[8].Value = objitemsubgroupData.IsActive;

                    arParms[9] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[9].Value = objitemsubgroupData.FinancialYearID;

              

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateItemSubGroupMST", arParms);
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
        public List<ItemSubGroupMasterData> GetItemSubGroupTypeDetailsByID(ItemSubGroupMasterData objItemSubGroupTypeMaster)
        {
            List<ItemSubGroupMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objItemSubGroupTypeMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GeTItemSubGroupTypeDetailsByID", arParms);
                    List<ItemSubGroupMasterData> lstItemSubGroupTypeDetails = ORHelper<ItemSubGroupMasterData>.FromDataReaderToList(sqlReader);
                    result = lstItemSubGroupTypeDetails;
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
        public int DeleteItemSubGroupTypeDetailsByID(ItemSubGroupMasterData objItemSubGroupTypeMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objItemSubGroupTypeMaster.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[1].Value = objItemSubGroupTypeMaster.ID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objItemSubGroupTypeMaster.Remarks;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteItemSubGroupTypeDetailsByID", arParms);
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
        public List<ItemSubGroupMasterData> SearchItemSubGroupTypeDetails(ItemSubGroupMasterData objItemSubGroupTypeMaster)
        {
            List<ItemSubGroupMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@GroupID", SqlDbType.VarChar);
                    arParms[0].Value = objItemSubGroupTypeMaster.GroupID;

                    arParms[1] = new SqlParameter("@Code", SqlDbType.VarChar);
                    arParms[1].Value = objItemSubGroupTypeMaster.Code;

                    arParms[2] = new SqlParameter("@ItemSubGroupType", SqlDbType.VarChar);
                    arParms[2].Value = objItemSubGroupTypeMaster.ItemSubGroupType;

                    arParms[3] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[3].Value = objItemSubGroupTypeMaster.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_SearchItemSubGroupType", arParms);
                    List<ItemSubGroupMasterData> lstItemSubGroupTypeDetails = ORHelper<ItemSubGroupMasterData>.FromDataReaderToList(sqlReader);
                    result = lstItemSubGroupTypeDetails;
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
