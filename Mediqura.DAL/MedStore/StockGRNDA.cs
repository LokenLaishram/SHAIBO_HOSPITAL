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
    public class StockGRNDA
    {
        public List<StockGRNData> GetCompanyName(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@CompanyName", SqlDbType.VarChar);
                    arParms[0].Value = objD.CompanyName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetCompanyName", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> GetSupplierName(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@SupplierName", SqlDbType.VarChar);
                    arParms[0].Value = objD.SupplierName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetSupplierName", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> GetSupplier(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@SupplierName", SqlDbType.VarChar);
                    arParms[0].Value = objD.SupplierName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetSupplier", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> Get_ItemNameByID(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemID", SqlDbType.Int);
                    arParms[0].Value = objD.ItemID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_ItemNameByID", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> GetItemName(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    arParms[1] = new SqlParameter("@GroupID", SqlDbType.Int);
                    arParms[1].Value = objD.GroupID;

                    arParms[2] = new SqlParameter("@SubGroupID", SqlDbType.Int);
                    arParms[2].Value = objD.SubGroupID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_ItemNameWithID", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> GetReturnItemName(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;
					arParms[1] = new SqlParameter("@SupplierID", SqlDbType.Int);
					arParms[1].Value = objD.SupplierID;

                    SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Med_ReturnItemNameBySupplierID", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> GetStockItemNames(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    arParms[1] = new SqlParameter("@GroupID", SqlDbType.Int);
                    arParms[1].Value = objD.GroupID;

                    arParms[2] = new SqlParameter("@SubGroupID", SqlDbType.Int);
                    arParms[2].Value = objD.SubGroupID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_StockItemNames", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> GetGenItemName(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    arParms[1] = new SqlParameter("@GroupID", SqlDbType.Int);
                    arParms[1].Value = objD.GroupID;

                    arParms[2] = new SqlParameter("@SubGroupID", SqlDbType.Int);
                    arParms[2].Value = objD.SubGroupID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_GenItemNameWithID", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> GetItemNameGEN(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    arParms[1] = new SqlParameter("@GroupID", SqlDbType.Int);
                    arParms[1].Value = objD.GroupID;

                    arParms[2] = new SqlParameter("@SubGroupID", SqlDbType.Int);
                    arParms[2].Value = objD.SubGroupID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Get_ItemNameWithID", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> GetPhrItemName(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    arParms[1] = new SqlParameter("@RackID", SqlDbType.Int);
                    arParms[1].Value = objD.GroupID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_ItemNameWithRackID", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> GetStockItemTransferList(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    arParms[1] = new SqlParameter("@GroupID", SqlDbType.Int);
                    arParms[1].Value = objD.GroupID;

                    arParms[2] = new SqlParameter("@SubGroupID", SqlDbType.Int);
                    arParms[2].Value = objD.SubGroupID;

                    arParms[3] = new SqlParameter("@StockType", SqlDbType.Int);
                    arParms[3].Value = objD.StockType;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetItemTransferList", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> GetStockTransferList(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[0].Value = objD.DateFrom;

                    arParms[1] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[1].Value = objD.DateTo;

                    arParms[2] = new SqlParameter("@StockFrom", SqlDbType.Int);
                    arParms[2].Value = objD.StockFrom;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetStockTransferList", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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

        public List<StockGRNData> GetItemNames(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_StockItemNames", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> GetItemNameWithID(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetItemNameWithID", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> GetStockItemDetailsBySubStockID(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objD.ID;

                    arParms[1] = new SqlParameter("@MedSubStockID", SqlDbType.Int);
                    arParms[1].Value = objD.SubStockID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetMedDrugDetailsByStockID", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> GenerateMedStoreTransactionID(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@CustomerName", SqlDbType.VarChar);
                    arParms[0].Value = objD.CustomerName;

                    arParms[1] = new SqlParameter("@MedSubStockID", SqlDbType.Int);
                    arParms[1].Value = objD.SubStockID;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objD.EmployeeID;

                    arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[3].Value = objD.HospitalID;

                    arParms[4] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[4].Value = objD.FinancialYearID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Generate_MedStoreTransactionID", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> UpdateMedStoreTransactionID(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@CustomerName", SqlDbType.VarChar);
                    arParms[0].Value = objD.CustomerName;

                    arParms[1] = new SqlParameter("@MedSubStockID", SqlDbType.Int);
                    arParms[1].Value = objD.SubStockID;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objD.EmployeeID;

                    arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[3].Value = objD.HospitalID;

                    arParms[4] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[4].Value = objD.FinancialYearID;

                    arParms[5] = new SqlParameter("@TransactionID", SqlDbType.BigInt);
                    arParms[5].Value = objD.TransactionID;

                    arParms[6] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[6].Value = objD.UHID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_MedStoreTransactionID", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> UpdateMedStockTransactiondetails(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[12];

                    arParms[0] = new SqlParameter("@CustomerName", SqlDbType.VarChar);
                    arParms[0].Value = objD.CustomerName;

                    arParms[1] = new SqlParameter("@MedSubStockID", SqlDbType.Int);
                    arParms[1].Value = objD.SubStockID;

                    arParms[2] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[2].Value = objD.ID;

                    arParms[3] = new SqlParameter("@NoUnit", SqlDbType.Money);
                    arParms[3].Value = objD.NoUnit;

                    arParms[4] = new SqlParameter("@EquivalentQty", SqlDbType.Int);
                    arParms[4].Value = objD.EquivalentQty;

                    arParms[5] = new SqlParameter("@MRPPerQty", SqlDbType.Decimal);
                    arParms[5].Value = objD.MRPPerQty;

                    arParms[6] = new SqlParameter("@NetCharge", SqlDbType.Decimal);
                    arParms[6].Value = objD.NetCharge;

                    arParms[7] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[7].Value = objD.UHID;

                    arParms[8] = new SqlParameter("@TransactionID", SqlDbType.BigInt);
                    arParms[8].Value = objD.TransactionID;

                    arParms[9] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[9].Value = objD.EmployeeID;

                    arParms[10] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[10].Value = objD.HospitalID;

                    arParms[11] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[11].Value = objD.FinancialYearID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_MedStoreTransactionDetails", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> GetMedStockTransactiondetails(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[12];

                    arParms[0] = new SqlParameter("@CustomerName", SqlDbType.VarChar);
                    arParms[0].Value = objD.CustomerName;

                    arParms[1] = new SqlParameter("@MedSubStockID", SqlDbType.Int);
                    arParms[1].Value = objD.SubStockID;

                    arParms[2] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[2].Value = objD.ID;

                    arParms[3] = new SqlParameter("@NoUnit", SqlDbType.Money);
                    arParms[3].Value = objD.NoUnit;

                    arParms[4] = new SqlParameter("@EquivalentQty", SqlDbType.Int);
                    arParms[4].Value = objD.EquivalentQty;

                    arParms[5] = new SqlParameter("@MRPPerQty", SqlDbType.Decimal);
                    arParms[5].Value = objD.MRPPerQty;

                    arParms[6] = new SqlParameter("@NetCharge", SqlDbType.Decimal);
                    arParms[6].Value = objD.NetCharge;

                    arParms[7] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[7].Value = objD.UHID;

                    arParms[8] = new SqlParameter("@TransactionID", SqlDbType.BigInt);
                    arParms[8].Value = objD.TransactionID;

                    arParms[9] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[9].Value = objD.EmployeeID;

                    arParms[10] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[10].Value = objD.HospitalID;

                    arParms[11] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[11].Value = objD.FinancialYearID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_MedStoreTransactionDetails", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> Get_ItemName(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_ItemName", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> GetItemDetails(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_ItemDetails", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> GetrecdNo(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ReceiptNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.ReceiptNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetReceiptNo", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> GetPONo(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@PONo", SqlDbType.VarChar);
                    arParms[0].Value = objD.PONo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPONo", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public List<StockGRNData> GetautoPONo(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@PONo", SqlDbType.VarChar);
                    arParms[0].Value = objD.PONo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetautoPONo", arParms);
                    List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public int UpdateStockItemDetails(StockGRNData objstock)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[25];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objstock.XMLData;

                    arParms[1] = new SqlParameter("@Discount", SqlDbType.Money);
                    arParms[1].Value = objstock.Discount;

                    arParms[2] = new SqlParameter("@ReceivedBy", SqlDbType.BigInt);
                    arParms[2].Value = objstock.ReceivedBy;

                    arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[3].Value = objstock.HospitalID;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objstock.EmployeeID;

                    arParms[5] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[5].Value = objstock.FinancialYearID;

                    arParms[6] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[6].Value = objstock.IPaddress;

                    arParms[7] = new SqlParameter("@TrecievedQty", SqlDbType.Int);
                    arParms[7].Value = objstock.TrecievedQty;

                    arParms[8] = new SqlParameter("@TotalFreeQty", SqlDbType.Int);
                    arParms[8].Value = objstock.TotalFreeQty;

                    arParms[9] = new SqlParameter("@TotalrecievedQty", SqlDbType.Int);
                    arParms[9].Value = objstock.TotalrecievedQty;

                    arParms[10] = new SqlParameter("@PurchasetypeID", SqlDbType.Int);
                    arParms[10].Value = objstock.PurchasetypeID;

                    arParms[11] = new SqlParameter("@TotalFreeItemAmount", SqlDbType.Money);
                    arParms[11].Value = objstock.TotalFreeItemAmount;

                    arParms[12] = new SqlParameter("@DueAmt", SqlDbType.Money);
                    arParms[12].Value = objstock.DueAmt;

                    arParms[13] = new SqlParameter("@PaidAmount", SqlDbType.Money);
                    arParms[13].Value = objstock.PaidAmount;

                    arParms[14] = new SqlParameter("@SubTotalCP", SqlDbType.Money);
                    arParms[14].Value = objstock.SubTotalCP;

                    arParms[15] = new SqlParameter("@SubTotalMRP", SqlDbType.Money);
                    arParms[15].Value = objstock.SubTotalMRP;

                    arParms[16] = new SqlParameter("@PoNumber", SqlDbType.VarChar);
                    arParms[16].Value = objstock.PONo;

                    arParms[17] = new SqlParameter("@RecieptNo", SqlDbType.VarChar);
                    arParms[17].Value = objstock.ReceiptNo;

                    arParms[18] = new SqlParameter("@SupplireID", SqlDbType.Int);
                    arParms[18].Value = objstock.SupplierID;

                    arParms[19] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[19].Direction = ParameterDirection.Output;

                    arParms[20] = new SqlParameter("@NetTotalCP", SqlDbType.Money);
                    arParms[20].Value = objstock.NetTotalCP;

					arParms[21] = new SqlParameter("@TotalReturnQty", SqlDbType.Int);
					arParms[21].Value = objstock.TotalReturnQty;

					arParms[22] = new SqlParameter("@Payableamt", SqlDbType.Money);
					arParms[22].Value = objstock.Payableamt;

					arParms[23] = new SqlParameter("@Returnamt", SqlDbType.Money);
					arParms[23].Value = objstock.Returnamt;

					arParms[24] = new SqlParameter("@returnXMLData", SqlDbType.Xml);
					arParms[24].Value = objstock.returnXMLData;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_StockItemDetails", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[19].Value);
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
        public int UpdateStockTransfer(StockGRNData objstock)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objstock.XMLData;

                    arParms[1] = new SqlParameter("@StockFrom", SqlDbType.Int);
                    arParms[1].Value = objstock.StockFrom;

                    arParms[2] = new SqlParameter("@StockTo", SqlDbType.Int);
                    arParms[2].Value = objstock.StockTo;

                    arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[3].Direction = ParameterDirection.Output;

                    arParms[4] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[4].Value = objstock.HospitalID;

                    arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[5].Value = objstock.EmployeeID;

                    arParms[6] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[6].Value = objstock.IPaddress;

                    arParms[7] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[7].Value = objstock.FinancialYearID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_StockTransferDetails", arParms);
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

        public List<StockGRNData> GetStockItemList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@ItemID", SqlDbType.Int);
                    arParms[0].Value = objbill.ItemID;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[1].Value = objbill.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[2].Value = objbill.DateTo;

                    arParms[3] = new SqlParameter("@ReceivedBy", SqlDbType.BigInt);
                    arParms[3].Value = objbill.ReceivedBy;

                    arParms[4] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[4].Value = objbill.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetStockItemList", arParms);
                    List<StockGRNData> listpatientdetails = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<StockGRNData> GetItemCheckList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemID", SqlDbType.VarChar);
                    arParms[0].Value = objbill.ItemID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetCheckItemList", arParms);
                    List<StockGRNData> listpatientdetails = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<StockGRNData> GetItemApprovalNoList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[0].Value = objbill.DateFrom;

                    arParms[1] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[1].Value = objbill.DateTo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetItemApprovalNoList", arParms);
                    List<StockGRNData> listpatientdetails = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<StockGRNData> GetItemApprovalList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ApprovalListNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.ApprovalListNo;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetItemApprovalList", arParms);
                    List<StockGRNData> listpatientdetails = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<StockGRNData> GetStockistItemList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@Supplier", SqlDbType.VarChar);
                    arParms[0].Value = objbill.Supplier;

                    arParms[1] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[1].Value = objbill.ItemName;

                    arParms[2] = new SqlParameter("@PONo", SqlDbType.VarChar);
                    arParms[2].Value = objbill.PONo;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[3].Value = objbill.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[4].Value = objbill.DateTo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetStockistItemList", arParms);
                    List<StockGRNData> listpatientdetails = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<StockGRNData> GetStockistItemReturnList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@Supplier", SqlDbType.VarChar);
                    arParms[0].Value = objbill.Supplier;

                    arParms[1] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[1].Value = objbill.ItemName;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[3].Value = objbill.DateTo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetStockistItemReturnList", arParms);
                    List<StockGRNData> listpatientdetails = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<StockGRNData> GetPurchaseList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@PONo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.PONo;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[1].Value = objbill.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[2].Value = objbill.DateTo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPurchaseList", arParms);
                    List<StockGRNData> listpatientdetails = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<StockGRNData> GetPOCheckedList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@PONo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.PONo;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[1].Value = objbill.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[2].Value = objbill.DateTo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPOCheckedList", arParms);
                    List<StockGRNData> listpatientdetails = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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

        public int DeleteStockReturnItemListByID(StockGRNData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@ReturnNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.ReturnNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objbill.EmployeeID;

                    arParms[4] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[4].Value = objbill.ItemName;

                    arParms[5] = new SqlParameter("@StockID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.StockID;

                    arParms[6] = new SqlParameter("@ID", SqlDbType.VarChar);
                    arParms[6].Value = objbill.ID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteStockItemReturnListbyID", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[1].Value);
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
        public int DeleteStockItemListByID(StockGRNData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@ReceiptNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.ReceiptNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objbill.EmployeeID;

                    arParms[4] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[4].Value = objbill.ItemName;

                    arParms[5] = new SqlParameter("@PONo", SqlDbType.VarChar);
                    arParms[5].Value = objbill.PONo;

                    arParms[6] = new SqlParameter("@ID", SqlDbType.VarChar);
                    arParms[6].Value = objbill.ID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteStockItemListbyID", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[1].Value);
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
        public int DeleteMedstoreTransactionByID(StockGRNData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.ID;

                    arParms[1] = new SqlParameter("@TransactionID", SqlDbType.BigInt);
                    arParms[1].Value = objbill.TransactionID;

                    arParms[2] = new SqlParameter("@SubStockID", SqlDbType.BigInt);
                    arParms[2].Value = objbill.SubStockID;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objbill.EmployeeID;

                    arParms[4] = new SqlParameter("@NoUnit", SqlDbType.Money);
                    arParms[4].Value = objbill.NoUnit;

                    arParms[5] = new SqlParameter("@EquivalentQty", SqlDbType.Int);
                    arParms[5].Value = objbill.EquivalentQty;

                    arParms[6] = new SqlParameter("@MedSubStockTypeID", SqlDbType.Int);
                    arParms[6].Value = objbill.MedSubStockTypeID;

                    arParms[7] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[7].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteMedStockTransactionByID", arParms);
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
        public int DeleteMedDiscountRequestByID(StockGRNData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.ID;

                    arParms[1] = new SqlParameter("@TransactionID", SqlDbType.BigInt);
                    arParms[1].Value = objbill.TransactionID;

                    arParms[2] = new SqlParameter("@ReqNo", SqlDbType.VarChar);
                    arParms[2].Value = objbill.ReqNo;

                    arParms[3] = new SqlParameter("@MedSubStockTypeID", SqlDbType.Int);
                    arParms[3].Value = objbill.MedSubStockTypeID;

                    arParms[4] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[4].Value = objbill.Remarks;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[5].Value = objbill.HospitalID;

                    arParms[6] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[6].Value = objbill.EmployeeID;

                    arParms[7] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[7].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Delete_MedDeisacountRequest", arParms);
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
        public int DeleteMedOPDbill(StockGRNData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.ID;

                    arParms[1] = new SqlParameter("@TransactionID", SqlDbType.BigInt);
                    arParms[1].Value = objbill.TransactionID;

                    arParms[2] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[2].Value = objbill.BillNo;

                    arParms[3] = new SqlParameter("@MedSubStockTypeID", SqlDbType.Int);
                    arParms[3].Value = objbill.MedSubStockTypeID;

                    arParms[4] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[4].Value = objbill.Remarks;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[5].Value = objbill.HospitalID;

                    arParms[6] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[6].Value = objbill.EmployeeID;

                    arParms[7] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[7].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Delete_MedOPDbill", arParms);
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
        public int UpdateStockCondemnedItemDetails(StockGRNData objstock)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objstock.XMLData;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[2].Value = objstock.EmployeeID;



                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_StockCondemnedItemDetails", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[1].Value);
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
        public int UpdateStockistReturn(StockGRNData objstock)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objstock.XMLData;

                    arParms[1] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[1].Value = objstock.HospitalID;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[2].Value = objstock.EmployeeID;


                    arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[3].Direction = ParameterDirection.Output;

                    arParms[4] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[4].Value = objstock.FinancialYearID;

                    arParms[5] = new SqlParameter("@TotalReturnQty", SqlDbType.Int);
                    arParms[5].Value = objstock.TotalQuantity;

                    arParms[6] = new SqlParameter("@TotalCP", SqlDbType.Money);
                    arParms[6].Value = objstock.CP;

                    arParms[7] = new SqlParameter("@SupplierID", SqlDbType.Int);
                    arParms[7].Value = objstock.SupplierID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_StockistReturnItemDetails", arParms);
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
        public int DeleteStockistReturnItemListByID(StockGRNData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@ReturnNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.ReturnNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objbill.EmployeeID;

                    arParms[4] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[4].Value = objbill.ItemName;

                    arParms[5] = new SqlParameter("@StockID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.StockID;

                    arParms[6] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[6].Value = objbill.ID;

                    arParms[7] = new SqlParameter("@TotalReturnQty", SqlDbType.Int);
                    arParms[7].Value = objbill.NoReturn;

                    arParms[8] = new SqlParameter("@TotalCP", SqlDbType.Money);
                    arParms[8].Value = objbill.CP;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteStockistItemReturnListbyID", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[1].Value);
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
        public int DeleteItemListByID(StockGRNData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];


                    arParms[0] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[0].Direction = ParameterDirection.Output;

                    arParms[1] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[1].Value = objbill.Remarks;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objbill.EmployeeID;

                    arParms[3] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[3].Value = objbill.ItemName;

                    arParms[4] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[4].Value = objbill.ID;

                    arParms[5] = new SqlParameter("@Available", SqlDbType.Int);
                    arParms[5].Value = objbill.BalStock;

                    arParms[6] = new SqlParameter("@TotalCP", SqlDbType.Money);
                    arParms[6].Value = objbill.CP;

                    arParms[7] = new SqlParameter("@ApprovalListNo", SqlDbType.VarChar);
                    arParms[7].Value = objbill.ApprovalListNo;



                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeletePurchaseItemListbyID", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[0].Value);
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
        public int DeletePurchaseLIstByID(StockGRNData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];


                    arParms[0] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[0].Direction = ParameterDirection.Output;

                    arParms[1] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[1].Value = objbill.Remarks;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objbill.EmployeeID;

                    arParms[3] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[3].Value = objbill.ItemName;

                    arParms[4] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[4].Value = objbill.ID;

                    arParms[5] = new SqlParameter("@ApprovedQty", SqlDbType.Int);
                    arParms[5].Value = objbill.ApprovedQty;

                    arParms[6] = new SqlParameter("@TotalCP", SqlDbType.Money);
                    arParms[6].Value = objbill.CP;

                    arParms[7] = new SqlParameter("@PONo", SqlDbType.VarChar);
                    arParms[7].Value = objbill.PONo;



                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeletePurchaseListbyID", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[0].Value);
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
        public int DeletePOCheckLIstByID(StockGRNData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];


                    arParms[0] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[0].Direction = ParameterDirection.Output;

                    arParms[1] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[1].Value = objbill.Remarks;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objbill.EmployeeID;

                    arParms[4] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[4].Value = objbill.ID;

                    arParms[5] = new SqlParameter("@OrderedQty", SqlDbType.Int);
                    arParms[5].Value = objbill.ApprovedQty;

                    arParms[6] = new SqlParameter("@TotalCP", SqlDbType.Money);
                    arParms[6].Value = objbill.CP;

                    arParms[7] = new SqlParameter("@PONo", SqlDbType.VarChar);
                    arParms[7].Value = objbill.PONo;

                    arParms[5] = new SqlParameter("@RecdQty", SqlDbType.Int);
                    arParms[5].Value = objbill.TotalRecdQty;




                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeletePOCheckedListbyID", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[0].Value);
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
        public int DeleteStockTransferByIssueno(StockGRNData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];


                    arParms[0] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[0].Direction = ParameterDirection.Output;

                    arParms[1] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[1].Value = objbill.Remarks;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objbill.EmployeeID;

                    arParms[4] = new SqlParameter("@IssueNo", SqlDbType.Int);
                    arParms[4].Value = objbill.IssueNo;

                    arParms[5] = new SqlParameter("@TransferQty", SqlDbType.Int);
                    arParms[5].Value = objbill.TotalQuantity;

                    arParms[6] = new SqlParameter("@ItemID", SqlDbType.Int);
                    arParms[6].Value = objbill.ItemID;




                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_CancelInterStockTransfer", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[0].Value);
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

        public int UpdatePurchaseCheckItemList(StockGRNData objstock)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objstock.XMLData;

                    arParms[1] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[1].Value = objstock.HospitalID;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[2].Value = objstock.EmployeeID;


                    arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[3].Direction = ParameterDirection.Output;

                    arParms[4] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[4].Value = objstock.FinancialYearID;

                    arParms[5] = new SqlParameter("@TotalUsable", SqlDbType.Int);
                    arParms[5].Value = objstock.TotalUsable;

                    arParms[6] = new SqlParameter("@TotalCondemnedQty", SqlDbType.Int);
                    arParms[6].Value = objstock.TotalCondemnedQty;

                    arParms[7] = new SqlParameter("@TotalExpiry", SqlDbType.Int);
                    arParms[7].Value = objstock.TotalExpiry;

                    arParms[8] = new SqlParameter("@TotalCP", SqlDbType.Money);
                    arParms[8].Value = objstock.CP;

                    arParms[9] = new SqlParameter("@TotalQtyReqd", SqlDbType.Int);
                    arParms[9].Value = objstock.TotalQuantity;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_Purchase_ItemCheckList", arParms);
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

        public int UpdatePOCrossChecK(StockGRNData objstock)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[11];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objstock.XMLData;

                    arParms[1] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[1].Value = objstock.HospitalID;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[2].Value = objstock.EmployeeID;


                    arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[3].Direction = ParameterDirection.Output;

                    arParms[4] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[4].Value = objstock.FinancialYearID;

                    arParms[5] = new SqlParameter("@TotalOrderedQty", SqlDbType.Int);
                    arParms[5].Value = objstock.TotalQuantity;

                    arParms[6] = new SqlParameter("@TotalRecdQty", SqlDbType.Int);
                    arParms[6].Value = objstock.TotalRecdQty;

                    arParms[7] = new SqlParameter("@Verifyby", SqlDbType.BigInt);
                    arParms[7].Value = objstock.EmployeeID;

                    arParms[8] = new SqlParameter("@TotalCP", SqlDbType.Money);
                    arParms[8].Value = objstock.TotalCP;

                    arParms[9] = new SqlParameter("@PONo", SqlDbType.VarChar);
                    arParms[9].Value = objstock.PONo;

                    arParms[10] = new SqlParameter("@DueAmt", SqlDbType.Money);
                    arParms[10].Value = objstock.DueAmt;




                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_POCrossCheck", arParms);
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

        public List<StockGRNData> GetItemList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objbill.ItemName;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[1].Value = objbill.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[2].Value = objbill.DateTo;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetItemList", arParms);
                    List<StockGRNData> listpatientdetails = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<StockGRNData> GetDiscountreqDetailsforPayment(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.ID;

                    arParms[1] = new SqlParameter("@TransactionID", SqlDbType.BigInt);
                    arParms[1].Value = objbill.TransactionID;

                    arParms[2] = new SqlParameter("@ReqNo", SqlDbType.VarChar);
                    arParms[2].Value = objbill.ReqNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_PHR_AprovediscountdetailforPayment", arParms);
                    List<StockGRNData> listpatientdetails = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<StockGRNData> GetItemTransferList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ItemID", SqlDbType.VarChar);
                    arParms[0].Value = objbill.ItemID;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[1].Value = objbill.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[2].Value = objbill.DateTo;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetItemList", arParms);
                    List<StockGRNData> listpatientdetails = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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

        public List<StockGRNData> GetPurchaseOrderList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@PONo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.PONo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPurchaseOrderList", arParms);
                    List<StockGRNData> listpatientdetails = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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

        public int UpdatePurchaseOrder(StockGRNData objstock)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objstock.XMLData;

                    arParms[1] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[1].Value = objstock.HospitalID;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objstock.EmployeeID;

                    arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[3].Direction = ParameterDirection.Output;

                    arParms[4] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[4].Value = objstock.FinancialYearID;

                    arParms[5] = new SqlParameter("@TotalCP", SqlDbType.Money);
                    arParms[5].Value = objstock.CP;

                    arParms[6] = new SqlParameter("@TotalApprovedQty", SqlDbType.Int);
                    arParms[6].Value = objstock.TotalQuantity;

                    arParms[7] = new SqlParameter("@SupplierID", SqlDbType.BigInt);
                    arParms[7].Value = objstock.SupplierID;



                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Genarate_Purchase_Order", arParms);
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

        //------------------------------------Start Discount Request List Tab 1--------------------------------

        public List<StockGRNData> GetRQNumberAuto(StockGRNData ObjDisReqData)
        {
            List<StockGRNData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ReqNo", SqlDbType.VarChar);
                arParms[0].Value = ObjDisReqData.ReqNo;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetRQNumberAuto", arParms);
                List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
                result = lstresult;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public List<StockGRNData> GetDiscountRequestList(StockGRNData ObjDisReqData)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@ReqNo", SqlDbType.VarChar);
                    arParms[0].Value = ObjDisReqData.ReqNo;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = ObjDisReqData.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[2].Value = ObjDisReqData.DateTo;

                    arParms[3] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[3].Value = ObjDisReqData.PatientTypeID;

                    arParms[4] = new SqlParameter("@StatusID", SqlDbType.Int);
                    arParms[4].Value = ObjDisReqData.StatusID;

                    arParms[5] = new SqlParameter("@RequestTypeID", SqlDbType.Int);
                    arParms[5].Value = ObjDisReqData.RequestTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetDiscountRequestList", arParms);
                    List<StockGRNData> listpatientdetails = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<StockGRNData> GetRequestDetailListByRQNumber(StockGRNData ObjDisReqData)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ReqNo", SqlDbType.VarChar);
                    arParms[0].Value = ObjDisReqData.ReqNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetRequestDetailListByRQNumber", arParms);
                    List<StockGRNData> RequestDetailList = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
                    result = RequestDetailList;
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

        //------------------------------------End Discount Request List Tab 1--------------------------------

        //------------------------------------Start Discount Request List Tab 2--------------------------------

        public int UpdateDiscountApprovedList(StockGRNData ObjDisReqData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@ApprovedAmount", SqlDbType.Money);
                    arParms[0].Value = ObjDisReqData.ApprovedAmount;

                    arParms[1] = new SqlParameter("@StatusID", SqlDbType.Int);
                    arParms[1].Value = ObjDisReqData.StatusID;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = ObjDisReqData.Remarks;

                    arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[3].Direction = ParameterDirection.Output;

                    arParms[4] = new SqlParameter("@ReqNo", SqlDbType.VarChar);
                    arParms[4].Value = ObjDisReqData.ReqNo;

                    arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[5].Value = ObjDisReqData.EmployeeID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_UpdateDiscountApprovedList", arParms);
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
        //------------------------------------End Discount Request List Tab 2--------------------------------

        //------------------------------------Start Discount Request After Billing Tab 1--------------------------------
        public List<StockGRNData> GetBillNumberAuto(StockGRNData ObjDisReqData)
        {
            List<StockGRNData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[2];
                arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                arParms[0].Value = ObjDisReqData.BillNo;

                arParms[1] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                arParms[1].Value = ObjDisReqData.PatientTypeID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetBillNumberAuto", arParms);
                List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
                result = lstresult;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }

        public List<StockGRNData> GetPatientDetailsByBillNo(StockGRNData ObjDisReqData)
        {
            List<StockGRNData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[2];
                arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                arParms[0].Value = ObjDisReqData.BillNo;

                arParms[1] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                arParms[1].Value = ObjDisReqData.PatientTypeID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetPatientDetailsByBillNo", arParms);
                List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
                result = lstresult;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public string SaveDiscountRequestAfterBilling(StockGRNData ObjDisReqData)
        {
            string result = null;

            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[15];

                    arParms[0] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[0].Value = ObjDisReqData.PatientTypeID;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = ObjDisReqData.PatientName;

                    arParms[2] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[2].Value = ObjDisReqData.BillNo;

                    arParms[3] = new SqlParameter("@TotalBillAmount", SqlDbType.Money);
                    arParms[3].Value = ObjDisReqData.TotalBillAmount;

                    arParms[4] = new SqlParameter("@RequestedAmount", SqlDbType.Money);
                    arParms[4].Value = ObjDisReqData.RequestedAmount;

                    arParms[5] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[5].Value = ObjDisReqData.Remarks;

                    arParms[6] = new SqlParameter("@TransactionID", SqlDbType.BigInt);
                    arParms[6].Value = ObjDisReqData.TransactionID;

                    arParms[7] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[7].Value = ObjDisReqData.UHID;

                    arParms[8] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[8].Value = ObjDisReqData.IPNo;

                    arParms[9] = new SqlParameter("@EmrgNo", SqlDbType.VarChar);
                    arParms[9].Value = ObjDisReqData.EmrgNo;

                    arParms[10] = new SqlParameter("@PaidAmount", SqlDbType.Money);
                    arParms[10].Value = ObjDisReqData.PaidAmount;

                    arParms[11] = new SqlParameter("@Output", SqlDbType.VarChar, 50);
                    arParms[11].Direction = ParameterDirection.Output;

                    arParms[12] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[12].Value = ObjDisReqData.FinancialYearID;

                    arParms[13] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[13].Value = ObjDisReqData.EmployeeID;

                    arParms[14] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[14].Value = ObjDisReqData.HospitalID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_SaveDiscountRequestAfterBilling", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToString(arParms[11].Value);
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

        //------------------------------------End Discount Request After Billing Tab 1--------------------------------
        //------------------------------------Start Discount Request After Billing Tab 2--------------------------------

        public List<StockGRNData> GetDiscountRequestListAfterBilling(StockGRNData ObjDisReqData)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@ReqNo", SqlDbType.VarChar);
                    arParms[0].Value = ObjDisReqData.ReqNo;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = ObjDisReqData.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[2].Value = ObjDisReqData.DateTo;

                    arParms[3] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[3].Value = ObjDisReqData.PatientTypeID;

                    arParms[4] = new SqlParameter("@StatusID", SqlDbType.Int);
                    arParms[4].Value = ObjDisReqData.StatusID;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = ObjDisReqData.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetDiscountRequestListAfterBilling", arParms);
                    List<StockGRNData> listpatientdetails = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<StockGRNData> GetDiscountApproveDetailsForRefund(StockGRNData ObjDisReqData)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ReqNo", SqlDbType.VarChar);
                    arParms[0].Value = ObjDisReqData.ReqNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetDiscountApproveDetailsForRefund", arParms);
                    List<StockGRNData> listpatientdetails = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public int DeleteDiscountRequestByReqNo(StockGRNData ObjDisReqData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ReqNo", SqlDbType.VarChar);
                    arParms[0].Value = ObjDisReqData.ReqNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.Int);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = ObjDisReqData.EmployeeID;

                    arParms[3] = new SqlParameter("@RemarksCancel", SqlDbType.VarChar);
                    arParms[3].Value = ObjDisReqData.RemarksCancel;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_DeleteDiscountRequestByReqNo", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[1].Value);
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

        //------------------------------------End Discount Request After Billing Tab 2--------------------------------

        //------------------------------------Start Discount Request After Billing Tab 3--------------------------------
        public string SaveRefundDiscountRequest(StockGRNData ObjDisReqData)
        {
            string result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[19];

                    arParms[0] = new SqlParameter("@ReqNo", SqlDbType.VarChar);
                    arParms[0].Value = ObjDisReqData.ReqNo;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = ObjDisReqData.UHID;

                    arParms[2] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[2].Value = ObjDisReqData.BillNo;

                    arParms[3] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[3].Value = ObjDisReqData.PatientName;

                    arParms[4] = new SqlParameter("@RequestedBy", SqlDbType.VarChar);
                    arParms[4].Value = ObjDisReqData.RequestedBy;

                    arParms[5] = new SqlParameter("@RequestedDate", SqlDbType.DateTime);
                    arParms[5].Value = ObjDisReqData.RequestedDate;

                    arParms[6] = new SqlParameter("@ApprovedBy", SqlDbType.VarChar);
                    arParms[6].Value = ObjDisReqData.ApprovedBy;

                    arParms[7] = new SqlParameter("@ApprovedDate", SqlDbType.DateTime);
                    arParms[7].Value = ObjDisReqData.ApprovedDate;

                    arParms[8] = new SqlParameter("@TotalBillAmount", SqlDbType.Money);
                    arParms[8].Value = ObjDisReqData.TotalBillAmount;

                    arParms[9] = new SqlParameter("@ApprovedAmount", SqlDbType.Money);
                    arParms[9].Value = ObjDisReqData.ApprovedAmount;

                    arParms[10] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[10].Value = ObjDisReqData.EmployeeID;

                    arParms[11] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[11].Value = ObjDisReqData.HospitalID;

                    arParms[12] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[12].Value = ObjDisReqData.FinancialYearID;

                    arParms[13] = new SqlParameter("@Output", SqlDbType.VarChar, 50);
                    arParms[13].Direction = ParameterDirection.Output;

                    arParms[14] = new SqlParameter("@Paymode", SqlDbType.Int);
                    arParms[14].Value = ObjDisReqData.Paymode;

                    arParms[15] = new SqlParameter("@BankName", SqlDbType.VarChar);
                    arParms[15].Value = ObjDisReqData.BankName;

                    arParms[16] = new SqlParameter("@Cheque", SqlDbType.VarChar);
                    arParms[16].Value = ObjDisReqData.Cheque;

                    arParms[17] = new SqlParameter("@Invoicenumber", SqlDbType.VarChar);
                    arParms[17].Value = ObjDisReqData.Invoicenumber;

                    arParms[18] = new SqlParameter("@RemarksRefund", SqlDbType.VarChar);
                    arParms[18].Value = ObjDisReqData.RemarksRefund;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_SaveRefundDiscountRequest", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToString(arParms[13].Value);
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


        //------------------------------------End Discount Request After Billing Tab 3--------------------------------

        //------------------------------------Start Discount Request After Billing Tab 4--------------------------------

        public List<StockGRNData> GetRFNumberAuto(StockGRNData ObjDisReqData)
        {
            List<StockGRNData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@RefundNo", SqlDbType.VarChar);
                arParms[0].Value = ObjDisReqData.RefundNo;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetRFNumberAuto", arParms);
                List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
                result = lstresult;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public List<StockGRNData> GetRefundListForAfterBiling(StockGRNData ObjDisReqData)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@RefundNo", SqlDbType.VarChar);
                    arParms[0].Value = ObjDisReqData.RefundNo;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = ObjDisReqData.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[2].Value = ObjDisReqData.DateTo;

                    arParms[3] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[3].Value = ObjDisReqData.PatientTypeID;

                    arParms[4] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[4].Value = ObjDisReqData.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetRefundListForAfterBiling", arParms);
                    List<StockGRNData> listpatientdetails = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public int DeleteRefundDiscountAfterBillingByReqNo(StockGRNData ObjDisReqData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@ReqNo", SqlDbType.VarChar);
                    arParms[0].Value = ObjDisReqData.ReqNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.Int);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = ObjDisReqData.EmployeeID;

                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = ObjDisReqData.Remarks;

                    arParms[4] = new SqlParameter("@RefundNo", SqlDbType.VarChar);
                    arParms[4].Value = ObjDisReqData.RefundNo;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_DeleteRefundDiscountAfterBillingByReqNo", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[1].Value);
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

        //------------------------------------End Discount Request After Billing Tab 4--------------------------------

		public List<StockGRNData> GetStockItemDetailsByStockID(StockGRNData objStock)
		{
			List<StockGRNData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[1];

					arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
					arParms[0].Value = objStock.ID;

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetreturnDrugDetailsByStockID", arParms);
					List<StockGRNData> lstresult = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
	}
}
