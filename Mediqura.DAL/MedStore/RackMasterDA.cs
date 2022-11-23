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
    public class RackMasterDA
    {
        public int UpdateRackDetails(RackMasterData objItemMasterData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[11];

                    arParms[0] = new SqlParameter("@RackID", SqlDbType.Int);
                    arParms[0].Value = objItemMasterData.RackID;

                    arParms[1] = new SqlParameter("@StockTypeID", SqlDbType.Int);
                    arParms[1].Value = objItemMasterData.StockTypeID;

                    arParms[2] = new SqlParameter("@RackNumber", SqlDbType.VarChar);
                    arParms[2].Value = objItemMasterData.RackNumber;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objItemMasterData.EmployeeID;

                    arParms[4] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[4].Value = objItemMasterData.HospitalID;

                    arParms[5] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[5].Value = objItemMasterData.ActionType;

                    arParms[6] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[6].Direction = ParameterDirection.Output;

                    arParms[7] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[7].Value = objItemMasterData.IsActive;

                    arParms[8] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[8].Value = objItemMasterData.FinancialYearID;

                    arParms[9] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[9].Value = objItemMasterData.IPaddress;

                    arParms[10] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[10].Value = objItemMasterData.Remarks;

                 
                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateRackMST", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[6].Value);
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
        public int UpdateItemSubRackDetails(RackMasterData objItemMasterData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[12];

                    arParms[0] = new SqlParameter("@RackID", SqlDbType.Int);
                    arParms[0].Value = objItemMasterData.RackID;

                    arParms[1] = new SqlParameter("@StockTypeID", SqlDbType.Int);
                    arParms[1].Value = objItemMasterData.StockTypeID;

                    arParms[2] = new SqlParameter("@SubRackID", SqlDbType.Int);
                    arParms[2].Value = objItemMasterData.SubRackID;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objItemMasterData.EmployeeID;

                    arParms[4] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[4].Value = objItemMasterData.HospitalID;

                    arParms[5] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[5].Value = objItemMasterData.ActionType;

                    arParms[6] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[6].Direction = ParameterDirection.Output;

                    arParms[7] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[7].Value = objItemMasterData.IsActive;

                    arParms[8] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[8].Value = objItemMasterData.FinancialYearID;

                    arParms[9] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[9].Value = objItemMasterData.IPaddress;
                    
                    arParms[10] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[10].Value = objItemMasterData.Remarks;

                    arParms[11] = new SqlParameter("@SubRack", SqlDbType.VarChar);
                    arParms[11].Value = objItemMasterData.SubRack;




                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateSubRackMST", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[6].Value);
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
        public List<RackMasterData> GetRackTypeDetailsByID(RackMasterData objItemMaster)
        {
            List<RackMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@RackID", SqlDbType.Int);
                    arParms[0].Value = objItemMaster.RackID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GeTRackDetailsByID", arParms);
                    List<RackMasterData> lstItemMasterDetails = ORHelper<RackMasterData>.FromDataReaderToList(sqlReader);
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
        public List<RackMasterData> GetItemSubRackTypeDetailsByID(RackMasterData objItemMaster)
        {
            List<RackMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@SubRackID", SqlDbType.Int);
                    arParms[0].Value = objItemMaster.SubRackID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GeTSubRackDetailsByID", arParms);
                    List<RackMasterData> lstItemMasterDetails = ORHelper<RackMasterData>.FromDataReaderToList(sqlReader);
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
        public int DeleteRackTypeDetailsByID(RackMasterData objItemMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@RackID", SqlDbType.Int);
                    arParms[0].Value = objItemMaster.RackID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[1].Value = objItemMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objItemMaster.Remarks;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteRackDetailsByID", arParms);
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
        public List<RackMasterData> SearchRackTypeDetails(RackMasterData objItemMaster)
        {
            List<RackMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@StockTypeID", SqlDbType.Int);
                    arParms[0].Value = objItemMaster.StockTypeID;

                    arParms[1] = new SqlParameter("@RackNumber", SqlDbType.VarChar);
                    arParms[1].Value = objItemMaster.RackNumber;

                    arParms[2] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[2].Value = objItemMaster.IsActive;

              
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_SearchRackMaster", arParms);
                    List<RackMasterData> lstItemMasterDetails = ORHelper<RackMasterData>.FromDataReaderToList(sqlReader);
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
        public List<RackMasterData> SearchSubRackDetails(RackMasterData objItemMaster)
        {
            List<RackMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@StockTypeID", SqlDbType.Int);
                    arParms[0].Value = objItemMaster.StockTypeID;

                    arParms[1] = new SqlParameter("@RackID", SqlDbType.Int);
                    arParms[1].Value = objItemMaster.RackID;

                    arParms[2] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[2].Value = objItemMaster.IsActive;

                    arParms[3] = new SqlParameter("@SubRack", SqlDbType.VarChar);
                    arParms[3].Value = objItemMaster.SubRack;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_SearchSubRackMaster", arParms);
                    List<RackMasterData> lstItemMasterDetails = ORHelper<RackMasterData>.FromDataReaderToList(sqlReader);
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
        public int DeleteSubRackTypeDetailsByID(RackMasterData objItemMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@SubRackID", SqlDbType.Int);
                    arParms[0].Value = objItemMaster.SubRackID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[1].Value = objItemMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objItemMaster.Remarks;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteSubRackDetailsByID", arParms);
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
        public List<RackMasterData> SearchItemLocationDetails(RackMasterData objItemMaster)
        {
            List<RackMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@StockType", SqlDbType.Int);
                    arParms[0].Value = objItemMaster.StockTypeID;

                    arParms[1] = new SqlParameter("@RackID", SqlDbType.Int);
                    arParms[1].Value = objItemMaster.RackID;

                    arParms[2] = new SqlParameter("@SubRackID", SqlDbType.Int);
                    arParms[2].Value = objItemMaster.SubRackID;

                    arParms[3] = new SqlParameter("@ItemID", SqlDbType.BigInt);
                    arParms[3].Value = objItemMaster.ItemID;

                    arParms[4] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[4].Value = objItemMaster.DateFrom;

                    arParms[5] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[5].Value = objItemMaster.DateTo;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_SearchItemLocationDetails", arParms);
                    List<RackMasterData> lstItemMasterDetails = ORHelper<RackMasterData>.FromDataReaderToList(sqlReader);
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
        public int UpdateItemLocationDetails(RackMasterData objItemMasterData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@StockType", SqlDbType.BigInt);
                    arParms[0].Value = objItemMasterData.StockTypeID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objItemMasterData.EmployeeID;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[2].Value = objItemMasterData.HospitalID;

                    arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[3].Direction = ParameterDirection.Output;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objItemMasterData.IPaddress;

                    arParms[5] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[5].Value = objItemMasterData.XMLData;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_UpdateItemLocationDetails", arParms);
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
