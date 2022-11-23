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
    public class GenInterStockTransferDA
    {
        public List<GenInterStockTransferData> GetSubStockItemName(GenInterStockTransferData objD)
        {
            List<GenInterStockTransferData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    arParms[1] = new SqlParameter("@SubStockID", SqlDbType.Int);
                    arParms[1].Value = objD.GenStockID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_InterStkTrnfer_Get_SubstockItemNameList", arParms);
                    List<GenInterStockTransferData> lstresult = ORHelper<GenInterStockTransferData>.FromDataReaderToList(sqlReader);
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
        public List<GenInterStockTransferData> GetInterItemDetailsByStockNumbers(GenInterStockTransferData objD)
        {
            List<GenInterStockTransferData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objD.ID;

                    arParms[1] = new SqlParameter("@StockNo", SqlDbType.VarChar);
                    arParms[1].Value = objD.StockNo;

                    arParms[2] = new SqlParameter("@SubStockID", SqlDbType.Int);
                    arParms[2].Value = objD.GenStockID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_TransferItemDetailsByStockNumber", arParms);
                    List<GenInterStockTransferData> lstresult = ORHelper<GenInterStockTransferData>.FromDataReaderToList(sqlReader);
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
        public List<GenInterStockTransferData> UpdateInterStockTransferDetails(GenInterStockTransferData objstock)
        {
            List<GenInterStockTransferData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[11];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objstock.XMLData;

                    arParms[1] = new SqlParameter("@TransferFromGenStockID", SqlDbType.Int);
                    arParms[1].Value = objstock.GenStockID;

                    arParms[2] = new SqlParameter("@TransferToGenStockID", SqlDbType.Int);
                    arParms[2].Value = objstock.TransferToGenStockID;

                    arParms[3] = new SqlParameter("@TotalTransferQty", SqlDbType.Int);
                    arParms[3].Value = objstock.TotalTransferQty;                   

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objstock.EmployeeID;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[5].Value = objstock.HospitalID;

                    arParms[6] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[6].Value = objstock.FinancialYearID;                   

                    arParms[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[7].Value = objstock.IPaddress;

                    arParms[8] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[8].Value = objstock.IsActive;

                    arParms[9] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[9].Value = objstock.ActionType;

                    arParms[10] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[10].Direction = ParameterDirection.Output;
                                      

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_Update_InterStockItemTransferDetails", arParms);
                    List<GenInterStockTransferData> lstresult = ORHelper<GenInterStockTransferData>.FromDataReaderToList(sqlReader);
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
        //----TAB 2------
        public List<GenInterStockTransferData> GetTransferStockItemList(GenInterStockTransferData objtransfer)
        {
            List<GenInterStockTransferData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@InterTransferNo", SqlDbType.VarChar);
                    arParms[0].Value = objtransfer.InterTransferNo;

                    arParms[1] = new SqlParameter("@TransferFromGenStockID", SqlDbType.Int);
                    arParms[1].Value = objtransfer.TransferFromGenStockID;

                    arParms[2] = new SqlParameter("@TransferToGenStockID", SqlDbType.Int);
                    arParms[2].Value = objtransfer.TransferToGenStockID;

                    arParms[3] = new SqlParameter("@TransferBy", SqlDbType.Int);
                    arParms[3].Value = objtransfer.TransferBy;

                    arParms[4] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[4].Value = objtransfer.DateFrom;

                    arParms[5] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[5].Value = objtransfer.DateTo;
                   
                    arParms[6] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[6].Value = objtransfer.IsActive;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_Get_InterStockTransferList", arParms);
                    List<GenInterStockTransferData> listpatientdetails = ORHelper<GenInterStockTransferData>.FromDataReaderToList(sqlReader);
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
        public int DeleteTranferStockByID(GenInterStockTransferData objtransfer)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objtransfer.ID;

                    arParms[1] = new SqlParameter("@InterTransferNo", SqlDbType.VarChar);
                    arParms[1].Value = objtransfer.InterTransferNo;

                    arParms[2] = new SqlParameter("@StockNo", SqlDbType.VarChar);
                    arParms[2].Value = objtransfer.StockNo;

                    arParms[3] = new SqlParameter("@TransferFromGenStockID", SqlDbType.Int);
                    arParms[3].Value = objtransfer.TransferFromGenStockID;

                    arParms[4] = new SqlParameter("@TransferToGenStockID", SqlDbType.Int);
                    arParms[4].Value = objtransfer.TransferToGenStockID;

                    arParms[5] = new SqlParameter("@TotalTransferQty", SqlDbType.Int);
                    arParms[5].Value = objtransfer.TotalTransferQty;

                    arParms[6] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[6].Value = objtransfer.Remarks;

                    arParms[7] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[7].Value = objtransfer.EmployeeID;

                    arParms[8] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[8].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_DeleteTransferStockByTransferNo", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[8].Value);
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
    }
}
