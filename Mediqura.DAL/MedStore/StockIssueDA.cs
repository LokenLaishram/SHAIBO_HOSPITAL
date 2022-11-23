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
    public class StockIssueDA
    {
        public List<StockIssueData> GetItemDetails(StockIssueData objD)
        {
            List<StockIssueData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIPPhrServices", arParms);
                    List<StockIssueData> lstresult = ORHelper<StockIssueData>.FromDataReaderToList(sqlReader);
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
        public List<StockIssueData> SearchIssueditem(StockIssueData objD)
        {
            List<StockIssueData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIPPhrIssueServices", arParms);
                    List<StockIssueData> lstresult = ORHelper<StockIssueData>.FromDataReaderToList(sqlReader);
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
        public List<StockIssueData> GetItemtoissueList(StockIssueData objD)
        {
            List<StockIssueData> result = null;
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
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_Item_Toissue_toSubstock", arParms);
                    List<StockIssueData> lstresult = ORHelper<StockIssueData>.FromDataReaderToList(sqlReader);
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
        public List<StockIssueData> GetItemNameListinSubStock(StockIssueData objD)
        {
            List<StockIssueData> result = null;
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
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_ItemNameInSubStock", arParms);
                    List<StockIssueData> lstresult = ORHelper<StockIssueData>.FromDataReaderToList(sqlReader);
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
  
        public List<StockIssueData> GetItemName(StockIssueData objD)
        {
            List<StockIssueData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_ItemNameIssued", arParms);
                    List<StockIssueData> lstresult = ORHelper<StockIssueData>.FromDataReaderToList(sqlReader);
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
        public List<StockIssueData> GetItemNameWithStockNO(StockIssueData objD)
        {
            List<StockIssueData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_ItemNameWithStockNo", arParms);
                    List<StockIssueData> lstresult = ORHelper<StockIssueData>.FromDataReaderToList(sqlReader);
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
        public List<StockIssueData> GetStockPrices(StockIssueData objD)
        {
            List<StockIssueData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objD.StockID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_StockPrices", arParms);
                    List<StockIssueData> lstresult = ORHelper<StockIssueData>.FromDataReaderToList(sqlReader);
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
        public int UpdateStockIssueDetails(StockIssueData objstock)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[15];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objstock.XMLData;

                    arParms[1] = new SqlParameter("@TotalQuantity", SqlDbType.Int);
                    arParms[1].Value = objstock.TotalIssueQuantity;

                    arParms[2] = new SqlParameter("@TotalCP", SqlDbType.Money);
                    arParms[2].Value = objstock.TotalCP;

                    arParms[3] = new SqlParameter("@TotalMRP", SqlDbType.Money);
                    arParms[3].Value = objstock.TotalMRP;

                    arParms[4] = new SqlParameter("@HandOverTo", SqlDbType.BigInt);
                    arParms[4].Value = objstock.HandOver;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[5].Value = objstock.HospitalID;

                    arParms[6] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[6].Value = objstock.EmployeeID;

                    arParms[7] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[7].Direction = ParameterDirection.Output;

                    arParms[8] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[8].Value = objstock.ActionType;

                    arParms[9] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[9].Value = objstock.FinancialYearID;

                    arParms[10] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[10].Value = objstock.IsActive;

                    arParms[11] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[11].Value = objstock.IPaddress;

                    arParms[12] = new SqlParameter("@StockTypeID", SqlDbType.Int);
                    arParms[12].Value = objstock.StockTypeID;

                    arParms[13] = new SqlParameter("@IndentNo", SqlDbType.VarChar);
                    arParms[13].Value = objstock.IndentNo;

                    arParms[14] = new SqlParameter("@BatchNo", SqlDbType.VarChar);
                    arParms[14].Value = objstock.BatchNo;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_StockIssuedDetails", arParms);
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
        public List<StockIssueData> GetIssueNo(StockIssueData objD)
        {
            List<StockIssueData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IssueNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IssueNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_IssueNo", arParms);
                    List<StockIssueData> lstresult = ORHelper<StockIssueData>.FromDataReaderToList(sqlReader);
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
        public List<StockIssueData> GetindentNo(StockIssueData objD)
        {
            List<StockIssueData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IndentNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IndentNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_IndentNo", arParms);
                    List<StockIssueData> lstresult = ORHelper<StockIssueData>.FromDataReaderToList(sqlReader);
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
        public List<StockIssueData> GetReturnNo(StockIssueData objD)
        {
            List<StockIssueData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ReturnNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.Return;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_ReturnNo", arParms);
                    List<StockIssueData> lstresult = ORHelper<StockIssueData>.FromDataReaderToList(sqlReader);
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
        public List<StockIssueData> GetStockIssuedList(StockIssueData objbill)
        {
            List<StockIssueData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@IssueNo", SqlDbType.Int);
                    arParms[0].Value = objbill.IssueNo;

                    arParms[1] = new SqlParameter("@IndentNo", SqlDbType.VarChar);
                    arParms[1].Value = objbill.IndentNo;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[3].Value = objbill.DateTo;

                    arParms[4] = new SqlParameter("@StockType", SqlDbType.Int);
                    arParms[4].Value = objbill.StockTypeID;

                    arParms[5] = new SqlParameter("@IssuedByID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.IssuedByID;

                    arParms[6] = new SqlParameter("@Handedto", SqlDbType.BigInt);
                    arParms[6].Value = objbill.HandOver;

                    arParms[7] = new SqlParameter("@Status", SqlDbType.Bit);
                    arParms[7].Value = objbill.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetStockItemIssuedList", arParms);
                    List<StockIssueData> listpatientdetails = ORHelper<StockIssueData>.FromDataReaderToList(sqlReader);
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
        public int DeleteStockIssuedItemListByID(StockIssueData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@IssueID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.ID;

                    arParms[1] = new SqlParameter("@IssueNo", SqlDbType.BigInt);
                    arParms[1].Value = objbill.IssueNo;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objbill.Remarks;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objbill.EmployeeID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteStockItemIssuedListbyID", arParms);
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
        public List<StockIssueData> GetStockReturnList(StockIssueData objbill)
        {
            List<StockIssueData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.StockID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetStockItemReturnList", arParms);
                    List<StockIssueData> listpatientdetails = ORHelper<StockIssueData>.FromDataReaderToList(sqlReader);
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

    }
}
