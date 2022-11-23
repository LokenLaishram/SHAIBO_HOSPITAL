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
    public class GenStockStatusDA
    {
        public List<StockStatusData> Get_StockStatus(StockStatusData objLabGroupTypeMaster)
        {
            List<StockStatusData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[19];

                    arParms[0] = new SqlParameter("@BatchNo", SqlDbType.VarChar);
                    arParms[0].Value = objLabGroupTypeMaster.BatchNo;

                    arParms[1] = new SqlParameter("@ItemID", SqlDbType.Int);
                    arParms[1].Value = objLabGroupTypeMaster.ItemID;

                    arParms[2] = new SqlParameter("@StockType", SqlDbType.Int);
                    arParms[2].Value = objLabGroupTypeMaster.StockTypeID;

                    arParms[3] = new SqlParameter("@ReceiptNo", SqlDbType.VarChar);
                    arParms[3].Value = objLabGroupTypeMaster.ReceiptNo;

                    arParms[4] = new SqlParameter("@PO", SqlDbType.VarChar);
                    arParms[4].Value = objLabGroupTypeMaster.POno;

                    arParms[5] = new SqlParameter("@StockNo", SqlDbType.VarChar);
                    arParms[5].Value = objLabGroupTypeMaster.StockNo;

                    arParms[6] = new SqlParameter("@Group", SqlDbType.Int);
                    arParms[6].Value = objLabGroupTypeMaster.GroupID;

                    arParms[7] = new SqlParameter("@Subgroup", SqlDbType.Int);
                    arParms[7].Value = objLabGroupTypeMaster.SubGroupID;

                    arParms[8] = new SqlParameter("@Availbalancefrom", SqlDbType.Int);
                    arParms[8].Value = objLabGroupTypeMaster.Availbalancefrom;

                    arParms[9] = new SqlParameter("@Availbalanceto", SqlDbType.Int);
                    arParms[9].Value = objLabGroupTypeMaster.Availbalanceto;

                    arParms[10] = new SqlParameter("@ExpiryDayfrom", SqlDbType.Int);
                    arParms[10].Value = objLabGroupTypeMaster.ExpiryDayfrom;

                    arParms[11] = new SqlParameter("@ExpiryDayto", SqlDbType.Int);
                    arParms[11].Value = objLabGroupTypeMaster.ExpiryDayto;

                    arParms[12] = new SqlParameter("@StockStatus", SqlDbType.Int);
                    arParms[12].Value = objLabGroupTypeMaster.StockStatus;

                    arParms[13] = new SqlParameter("@Recievedyear", SqlDbType.Int);
                    arParms[13].Value = objLabGroupTypeMaster.Recievedyear;

                    arParms[14] = new SqlParameter("@Recievedmonth", SqlDbType.Int);
                    arParms[14].Value = objLabGroupTypeMaster.Recievedmonth;

                    arParms[15] = new SqlParameter("@RecievedFrom", SqlDbType.DateTime);
                    arParms[15].Value = objLabGroupTypeMaster.DateFrom;

                    arParms[16] = new SqlParameter("@RecievedTo", SqlDbType.DateTime);
                    arParms[16].Value = objLabGroupTypeMaster.DateTo;

                    arParms[17] = new SqlParameter("@MfgCompany", SqlDbType.Int);
                    arParms[17].Value = objLabGroupTypeMaster.CompanyID;

                    arParms[18] = new SqlParameter("@Supplier", SqlDbType.Int);
                    arParms[18].Value = objLabGroupTypeMaster.SupplierID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GetStockStatus", arParms);
                    List<StockStatusData> lstLabGroupTypeDetails = ORHelper<StockStatusData>.FromDataReaderToList(sqlReader);
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
        public List<StockStatusData> GetBatchNo(StockStatusData objD)
        {
            List<StockStatusData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@BatchNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.BatchNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GetBatchNo", arParms);
                    List<StockStatusData> lstresult = ORHelper<StockStatusData>.FromDataReaderToList(sqlReader);
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
        public List<StockStatusData> GetRecNumber(StockStatusData objD)
        {
            List<StockStatusData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ReceiptNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.ReceiptNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GetRecieptNo", arParms);
                    List<StockStatusData> lstresult = ORHelper<StockStatusData>.FromDataReaderToList(sqlReader);
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
        public List<StockStatusData> GetPoNumbers(StockStatusData objD)
        {
            List<StockStatusData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@POno", SqlDbType.VarChar);
                    arParms[0].Value = objD.POno;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GetPoNos", arParms);
                    List<StockStatusData> lstresult = ORHelper<StockStatusData>.FromDataReaderToList(sqlReader);
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
        public List<StockStatusData> GetStockNumbers(StockStatusData objD)
        {
            List<StockStatusData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@StockNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.StockNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GetStockNumbers", arParms);
                    List<StockStatusData> lstresult = ORHelper<StockStatusData>.FromDataReaderToList(sqlReader);
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
        public int UpdateStockIssueDetails(StockStatusData objdisch)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objdisch.XMLData;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_UpdateCondemnQty", arParms);
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

    }
}
