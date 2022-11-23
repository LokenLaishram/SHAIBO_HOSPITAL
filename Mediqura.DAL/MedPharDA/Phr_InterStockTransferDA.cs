using Mediqura.CommonData.MedPharData;
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

namespace Mediqura.DAL.MedPharDA
{
    public class Phr_InterStockTransferDA
    {
        public List<Phr_InterStockTransferData> GetMedSubStockItemName(Phr_InterStockTransferData objD)
        {
            List<Phr_InterStockTransferData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    arParms[1] = new SqlParameter("@MedSubStockID", SqlDbType.Int);
                    arParms[1].Value = objD.MedSubStockID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phr_Get_AutoMedStockItemList", arParms);
                    List<Phr_InterStockTransferData> lstresult = ORHelper<Phr_InterStockTransferData>.FromDataReaderToList(sqlReader);
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
        public List<Phr_InterStockTransferData> Get_Phr_ItemDetailsBySubStockNo(Phr_InterStockTransferData objD)
        {
            List<Phr_InterStockTransferData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@SubStockID", SqlDbType.BigInt);
                    arParms[0].Value = objD.SubStockID;

                    arParms[1] = new SqlParameter("@MedSubStockID", SqlDbType.VarChar);
                    arParms[1].Value = objD.MedSubStockID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_Phr_ItemDetailsBySubStockID", arParms);
                    List<Phr_InterStockTransferData> lstresult = ORHelper<Phr_InterStockTransferData>.FromDataReaderToList(sqlReader);
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
        public List<Phr_InterStockTransferData> Update_Phr_InterStockTransferDetails(Phr_InterStockTransferData objstock)
        {
            List<Phr_InterStockTransferData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[11];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objstock.XMLData;

                    arParms[1] = new SqlParameter("@TransferFromMedSubStockID", SqlDbType.Int);
                    arParms[1].Value = objstock.TransferFromMedSubStockID;

                    arParms[2] = new SqlParameter("@TransferToMedSubStockID", SqlDbType.Int);
                    arParms[2].Value = objstock.TransferToMedSubStockID;

                    arParms[3] = new SqlParameter("@TotalTransferQty", SqlDbType.Int);
                    arParms[3].Value = objstock.TotalTransferQty;

                    arParms[4] = new SqlParameter("@TransferRemarks", SqlDbType.VarChar);
                    arParms[4].Value = objstock.TransferRemarks;

                    arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[5].Value = objstock.EmployeeID;

                    arParms[6] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[6].Value = objstock.HospitalID;

                    arParms[7] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[7].Value = objstock.FinancialYearID;

                    arParms[8] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[8].Value = objstock.IPaddress;

                    arParms[9] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[9].Value = objstock.ActionType;

                    arParms[10] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[10].Direction = ParameterDirection.Output;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phr_Update_InterStockTransferDetails", arParms);
                    List<Phr_InterStockTransferData> lstresult = ORHelper<Phr_InterStockTransferData>.FromDataReaderToList(sqlReader);
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
        public List<Phr_InterStockTransferData> GetTransferStockItemList(Phr_InterStockTransferData objtransfer)
        {
            List<Phr_InterStockTransferData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@InterTransferNo", SqlDbType.VarChar);
                    arParms[0].Value = objtransfer.InterTransferNo;

                    arParms[1] = new SqlParameter("@FromMedSubStockID", SqlDbType.Int);
                    arParms[1].Value = objtransfer.TransferFromMedSubStockID;

                    arParms[2] = new SqlParameter("@ToMedSubStockID", SqlDbType.Int);
                    arParms[2].Value = objtransfer.TransferToMedSubStockID;

                    arParms[3] = new SqlParameter("@TransferBy", SqlDbType.Int);
                    arParms[3].Value = objtransfer.TransferBy;

                    arParms[4] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[4].Value = objtransfer.DateFrom;

                    arParms[5] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[5].Value = objtransfer.DateTo;

                    arParms[6] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[6].Value = objtransfer.IsActive;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phr_Get_InterStockTransferList", arParms);
                    List<Phr_InterStockTransferData> listpatientdetails = ORHelper<Phr_InterStockTransferData>.FromDataReaderToList(sqlReader);
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
        public int DeleteTranferStockByID(Phr_InterStockTransferData objtransfer)
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

                    arParms[3] = new SqlParameter("@TransferFromMedSubStockID", SqlDbType.Int);
                    arParms[3].Value = objtransfer.TransferFromMedSubStockID;

                    arParms[4] = new SqlParameter("@TransferToMedSubStockID", SqlDbType.Int);
                    arParms[4].Value = objtransfer.TransferToMedSubStockID;

                    arParms[5] = new SqlParameter("@TotalTransferQty", SqlDbType.Int);
                    arParms[5].Value = objtransfer.TotalTransferQty;

                    arParms[6] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[6].Value = objtransfer.Remarks;

                    arParms[7] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[7].Value = objtransfer.EmployeeID;

                    arParms[8] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[8].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phar_DeleteTransferStockByTransferNo", arParms);
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
