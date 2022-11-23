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
    public class ItemMasterDA
    {
        public int UpdateItemMasterDetails(ItemMasterData objItemMasterData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[19];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objItemMasterData.ID;

                    arParms[1] = new SqlParameter("@GroupID", SqlDbType.Int);
                    arParms[1].Value = objItemMasterData.GroupID;

                    arParms[2] = new SqlParameter("@SubGroupID", SqlDbType.Int);
                    arParms[2].Value = objItemMasterData.SubGroupID;

                    arParms[3] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[3].Value = objItemMasterData.ItemName;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objItemMasterData.EmployeeID;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[5].Value = objItemMasterData.HospitalID;

                    arParms[6] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[6].Value = objItemMasterData.ActionType;

                    arParms[7] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[7].Direction = ParameterDirection.Output;

                    arParms[8] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[8].Value = objItemMasterData.IsActive;

                    arParms[9] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[9].Value = objItemMasterData.FinancialYearID;

                    arParms[10] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[10].Value = objItemMasterData.IPaddress;

                    arParms[11] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[11].Value = objItemMasterData.Remarks;

                    arParms[12] = new SqlParameter("@DaycountStart", SqlDbType.Int);
                    arParms[12].Value = objItemMasterData.DaycountStart;

                    arParms[13] = new SqlParameter("@DaycountEnd", SqlDbType.Int);
                    arParms[13].Value = objItemMasterData.DaycountEnd;

                    arParms[14] = new SqlParameter("@PhrUnit", SqlDbType.Int);
                    arParms[14].Value = objItemMasterData.PhrUnitID;

                    arParms[15] = new SqlParameter("@PHRSoundsID", SqlDbType.Int);
                    arParms[15].Value = objItemMasterData.PHRSoundsID;

                    arParms[16] = new SqlParameter("@PHRLooksID", SqlDbType.Int);
                    arParms[16].Value = objItemMasterData.PHRLooksID;

                    arParms[17] = new SqlParameter("@MfgCompanyID", SqlDbType.Int);
                    arParms[17].Value = objItemMasterData.MfgCompanyID;

                    arParms[18] = new SqlParameter("@VEDcatgID", SqlDbType.Int);
                    arParms[18].Value = objItemMasterData.VEDcatgID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateItemMasterMST", arParms);
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
        public List<ItemMasterData> GetItemMasterDetailsByID(ItemMasterData objItemMaster)
        {
            List<ItemMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objItemMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GeTItemMasterDetailsByID", arParms);
                    List<ItemMasterData> lstItemMasterDetails = ORHelper<ItemMasterData>.FromDataReaderToList(sqlReader);
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
        public int DeleteItemMasterDetailsByID(ItemMasterData objItemMaster)
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

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteItemMasterDetailsByID", arParms);
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
        public List<ItemMasterData> SearchItemMasterDetails(ItemMasterData objItemMaster)
        {
            List<ItemMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

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

                    arParms[6] = new SqlParameter("@PHRSoundsID", SqlDbType.Int);
                    arParms[6].Value = objItemMaster.PHRSoundsID;

                    arParms[7] = new SqlParameter("@PHRLooksID", SqlDbType.Int);
                    arParms[7].Value = objItemMaster.PHRLooksID;

                    arParms[8] = new SqlParameter("@MfgCompanyID", SqlDbType.Int);
                    arParms[8].Value = objItemMaster.MfgCompanyID;

                    arParms[9] = new SqlParameter("@VEDcatgID", SqlDbType.Int);
                    arParms[9].Value = objItemMaster.VEDcatgID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_SearchItemMaster", arParms);
                    List<ItemMasterData> lstItemMasterDetails = ORHelper<ItemMasterData>.FromDataReaderToList(sqlReader);
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
        public List<ItemMasterData> SearchItemDetails(ItemMasterData objItemMaster)
        {
            List<ItemMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

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

                    arParms[5] = new SqlParameter("@PHRSoundsID", SqlDbType.Int);
                    arParms[5].Value = objItemMaster.PHRSoundsID;

                    arParms[6] = new SqlParameter("@PHRLooksID", SqlDbType.Int);
                    arParms[6].Value = objItemMaster.PHRLooksID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_SearchItemDetails", arParms);
                    List<ItemMasterData> lstItemMasterDetails = ORHelper<ItemMasterData>.FromDataReaderToList(sqlReader);
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
