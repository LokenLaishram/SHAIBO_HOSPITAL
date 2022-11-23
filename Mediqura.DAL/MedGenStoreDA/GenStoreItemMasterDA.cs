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

namespace Mediqura.DAL.MedGenStoreDA
{
    public class GenStoreItemMasterDA
    {
        public int UpdateItemMasterDetails(GenItemMasterData objGenItemMasterData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[20];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objGenItemMasterData.ID;

                    arParms[1] = new SqlParameter("@GroupID", SqlDbType.Int);
                    arParms[1].Value = objGenItemMasterData.GroupID;

                    arParms[2] = new SqlParameter("@SubGroupID", SqlDbType.Int);
                    arParms[2].Value = objGenItemMasterData.SubGroupID;

                    arParms[3] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[3].Value = objGenItemMasterData.ItemName;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objGenItemMasterData.EmployeeID;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[5].Value = objGenItemMasterData.HospitalID;

                    arParms[6] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[6].Value = objGenItemMasterData.ActionType;

                    arParms[7] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[7].Direction = ParameterDirection.Output;

                    arParms[8] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[8].Value = objGenItemMasterData.IsActive;

                    arParms[9] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[9].Value = objGenItemMasterData.FinancialYearID;

                    arParms[10] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[10].Value = objGenItemMasterData.IPaddress;

                    arParms[11] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[11].Value = objGenItemMasterData.Remarks;

                    arParms[12] = new SqlParameter("@DaycountStart", SqlDbType.Int);
                    arParms[12].Value = objGenItemMasterData.DaycountStart;

                    arParms[13] = new SqlParameter("@DaycountEnd", SqlDbType.Int);
                    arParms[13].Value = objGenItemMasterData.DaycountEnd;

                    arParms[14] = new SqlParameter("@PhrUnit", SqlDbType.Int);
                    arParms[14].Value = objGenItemMasterData.PhrUnitID;

                    arParms[15] = new SqlParameter("@GenRackID", SqlDbType.Int);
                    arParms[15].Value = objGenItemMasterData.GenRackID;
                    
                    arParms[16] = new SqlParameter("@GenSubRackID", SqlDbType.Int);
                    arParms[16].Value = objGenItemMasterData.GenSubRackID;

                    arParms[17] = new SqlParameter("@GenSoundsID", SqlDbType.Int);
                    arParms[17].Value = objGenItemMasterData.GenSoundsID;

                    arParms[18] = new SqlParameter("@GenLooksID", SqlDbType.Int);
                    arParms[18].Value = objGenItemMasterData.GenLooksID;

                    arParms[19] = new SqlParameter("@ItemCategoryID", SqlDbType.Int);
                    arParms[19].Value = objGenItemMasterData.ItemCategoryID;
                    
                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_GEN_MDQ_UpdateItemMasterMST", arParms);
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
        public List<GenItemMasterData> GetItemMasterDetailsByID(GenItemMasterData objItemMaster)
        {
            List<GenItemMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objItemMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_GEN_MDQ_GeTItemMasterDetailsByID", arParms);
                    List<GenItemMasterData> lstItemMasterDetails = ORHelper<GenItemMasterData>.FromDataReaderToList(sqlReader);
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
        public int DeleteItemMasterDetailsByID(GenItemMasterData objItemMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objItemMaster.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[1].Value = objItemMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objItemMaster.Remarks;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_GEN_MDQ_DeleteItemMasterDetailsByID", arParms);
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
        public List<GenItemMasterData> SearchItemMasterDetails(GenItemMasterData objItemMaster)
        {
            List<GenItemMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@GroupID", SqlDbType.Int);
                    arParms[0].Value = objItemMaster.GroupID;

                    arParms[1] = new SqlParameter("@SubGroupID", SqlDbType.Int);
                    arParms[1].Value = objItemMaster.SubGroupID;

                    arParms[2] = new SqlParameter("@ItemName", SqlDbType.NVarChar);
                    arParms[2].Value = objItemMaster.ItemName;

                    arParms[3] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[3].Value = objItemMaster.IsActive;

                    arParms[4] = new SqlParameter("@PhrUnitID", SqlDbType.Int);
                    arParms[4].Value = objItemMaster.PhrUnitID;

                    arParms[5] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[5].Value = objItemMaster.CurrentIndex;

                    //arParms[6] = new SqlParameter("@GenRackID", SqlDbType.Int);
                    //arParms[6].Value = objItemMaster.GenRackID;
                    
                    //arParms[7] = new SqlParameter("@GenSubRackID", SqlDbType.Int);
                    //arParms[7].Value = objItemMaster.GenSubRackID;

                    arParms[6] = new SqlParameter("@GenSoundsID", SqlDbType.Int);
                    arParms[6].Value = objItemMaster.GenSoundsID;

                    arParms[7] = new SqlParameter("@GenLooksID", SqlDbType.Int);
                    arParms[7].Value = objItemMaster.GenLooksID;

                    arParms[8] = new SqlParameter("@ItemCategoryID", SqlDbType.Int);
                    arParms[8].Value = objItemMaster.ItemCategoryID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_GEN_MDQ_SearchItemMaster", arParms);
                    List<GenItemMasterData> lstItemMasterDetails = ORHelper<GenItemMasterData>.FromDataReaderToList(sqlReader);
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
        public List<GenItemMasterData> SearchItemDetails(GenItemMasterData objItemMaster)
        {
            List<GenItemMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@GroupID", SqlDbType.Int);
                    arParms[0].Value = objItemMaster.GroupID;

                    arParms[1] = new SqlParameter("@SubGroupID", SqlDbType.Int);
                    arParms[1].Value = objItemMaster.SubGroupID;

                    arParms[2] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[2].Value = objItemMaster.ItemName;

                    arParms[3] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[3].Value = objItemMaster.IsActive;

                    arParms[4] = new SqlParameter("@PhrUnitID", SqlDbType.Int);
                    arParms[4].Value = objItemMaster.PhrUnitID;

                    arParms[5] = new SqlParameter("@GenSoundsID", SqlDbType.Int);
                    arParms[5].Value = objItemMaster.GenSoundsID;

                    arParms[6] = new SqlParameter("@GenLooksID", SqlDbType.Int);
                    arParms[6].Value = objItemMaster.GenLooksID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_GEN_MDQ_SearchItemDetails", arParms);
                    List<GenItemMasterData> lstItemMasterDetails = ORHelper<GenItemMasterData>.FromDataReaderToList(sqlReader);
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
