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
    public class IndentRecvHandOverDA
    {
        public List<IndentRecvHandOverData> GetItemNameListInStock(IndentRecvHandOverData objD)
        {
            List<IndentRecvHandOverData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_IndentItemNameID", arParms);
                    List<IndentRecvHandOverData> lstresult = ORHelper<IndentRecvHandOverData>.FromDataReaderToList(sqlReader);
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
        public List<IndentRecvHandOverData> GetItemDetailsByStockID(IndentRecvHandOverData objD)
        {
            List<IndentRecvHandOverData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@StockID", SqlDbType.BigInt);
                    arParms[0].Value = objD.StockID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetStockList", arParms);
                    List<IndentRecvHandOverData> lstresult = ORHelper<IndentRecvHandOverData>.FromDataReaderToList(sqlReader);
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
        public int UpdateIndentItemDetails(IndentRecvHandOverData objstock)
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

                    arParms[4] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[4].Value = objstock.ActionType;

                    arParms[5] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[5].Value = objstock.FinancialYearID;

                    arParms[6] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[6].Value = objstock.IsActive;

                    arParms[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[7].Value = objstock.IPaddress;

                    //arParms[8] = new SqlParameter("@SubStockID", SqlDbType.Int);
                    //arParms[8].Value = objstock.SubStockID;

                    //arParms[9] = new SqlParameter("@IndentRequestID", SqlDbType.Int);
                    //arParms[9].Value = objstock.IndentRequestID;

                    //arParms[10] = new SqlParameter("@IssueDate", SqlDbType.DateTime);
                    //arParms[10].Value = objstock.IssueDate;

                    //arParms[11] = new SqlParameter("@ReqdQty", SqlDbType.Int);
                    //arParms[11].Value = objstock.ReqdQty;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_IndenttemDetails", arParms);
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
        public List<IndentRecvHandOverData> GetIndentList(IndentRecvHandOverData objbill)
        {
            List<IndentRecvHandOverData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                  
                    arParms[0] = new SqlParameter("@SubStockID", SqlDbType.Int);
                    arParms[0].Value = objbill.SubStockID;

                    arParms[1] = new SqlParameter("@IndentRequestID", SqlDbType.Int);
                    arParms[1].Value = objbill.IndentRequestID;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[3].Value = objbill.DateTo;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIndentRecvList", arParms);
                    List<IndentRecvHandOverData> listpatientdetails = ORHelper<IndentRecvHandOverData>.FromDataReaderToList(sqlReader);
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
        public List<IndentRecvHandOverData> bindIndentRecvList(IndentRecvHandOverData objbill)
        {
            List<IndentRecvHandOverData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];


                    arParms[0] = new SqlParameter("@SubStockID", SqlDbType.Int);
                    arParms[0].Value = objbill.SubStockID;

                    arParms[1] = new SqlParameter("@IndentRequestID", SqlDbType.Int);
                    arParms[1].Value = objbill.IndentRequestID;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[3].Value = objbill.DateTo;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIndentHndOvList", arParms);
                    List<IndentRecvHandOverData> listpatientdetails = ORHelper<IndentRecvHandOverData>.FromDataReaderToList(sqlReader);
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
        public List<IndentRecvHandOverData> GetRecvList(IndentRecvHandOverData objbill)
        {
            List<IndentRecvHandOverData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];


                    arParms[0] = new SqlParameter("@SubStockID", SqlDbType.Int);
                    arParms[0].Value = objbill.SubStockID;

                    arParms[1] = new SqlParameter("@IndentRequestID", SqlDbType.Int);
                    arParms[1].Value = objbill.IndentRequestID;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[3].Value = objbill.DateTo;

                    arParms[4] = new SqlParameter("@IndStatus", SqlDbType.Int);
                    arParms[4].Value = objbill.IndStatus;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIRecvList", arParms);
                    List<IndentRecvHandOverData> listpatientdetails = ORHelper<IndentRecvHandOverData>.FromDataReaderToList(sqlReader);
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
        public List<IndentRecvHandOverData> GetApprvList(IndentRecvHandOverData objbill)
        {
            List<IndentRecvHandOverData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];


                    arParms[0] = new SqlParameter("@SubStockID", SqlDbType.Int);
                    arParms[0].Value = objbill.SubStockID;

                    arParms[1] = new SqlParameter("@IndentRequestID", SqlDbType.Int);
                    arParms[1].Value = objbill.IndentRequestID;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[3].Value = objbill.DateTo;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIndentApprvList", arParms);
                    List<IndentRecvHandOverData> listpatientdetails = ORHelper<IndentRecvHandOverData>.FromDataReaderToList(sqlReader);
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
        public int UpdateIndent_status(IndentRecvHandOverData objdisch)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[0].Direction = ParameterDirection.Output;

                    arParms[1] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[1].Value = objdisch.XMLData;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateIndent_status", arParms);
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
        public List<IndentRecvHandOverData> IndentListDetailsByID(IndentRecvHandOverData objStoreUnitMaster)
        {
            List<IndentRecvHandOverData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IndentID", SqlDbType.Int);
                    arParms[0].Value = objStoreUnitMaster.IndentID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetIndentStatusDetailsByID", arParms);
                    List<IndentRecvHandOverData> lstLabUnitDetails = ORHelper<IndentRecvHandOverData>.FromDataReaderToList(sqlReader);
                    result = lstLabUnitDetails;
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
        public List<IndentRecvHandOverData> GetIndentList1(IndentRecvHandOverData objbill)
        {
            List<IndentRecvHandOverData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IndentNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.IndentNo;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIndentDetail", arParms);
                    List<IndentRecvHandOverData> listpatientdetails = ORHelper<IndentRecvHandOverData>.FromDataReaderToList(sqlReader);
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
        public List<IndentRecvHandOverData> GetApprvDetail(IndentRecvHandOverData objbill)
        {
            List<IndentRecvHandOverData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IndentNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.IndentNo;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIndentApprvDetail", arParms);
                    List<IndentRecvHandOverData> listpatientdetails = ORHelper<IndentRecvHandOverData>.FromDataReaderToList(sqlReader);
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
        public List<IndentRecvHandOverData> GetHndoverDetail(IndentRecvHandOverData objbill)
        {
            List<IndentRecvHandOverData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IndentNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.IndentNo;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIndentRecvDetail", arParms);
                    List<IndentRecvHandOverData> listpatientdetails = ORHelper<IndentRecvHandOverData>.FromDataReaderToList(sqlReader);
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
        public int UpdateIndentDetail(IndentRecvHandOverData objstock)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objstock.XMLData;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@TotApproved", SqlDbType.Int);
                    arParms[2].Value = objstock.TotApproved;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objstock.EmployeeID;

                    arParms[4] = new SqlParameter("@ApprvBy", SqlDbType.BigInt);
                    arParms[4].Value = objstock.ApprvBy;

                    arParms[5] = new SqlParameter("@HandOverTo", SqlDbType.BigInt);
                    arParms[5].Value = objstock.HandOverTo;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_IndentDetails", arParms);
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
        public int UpdateHandoverDetail(IndentRecvHandOverData objstock)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objstock.XMLData;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@TotHandOver", SqlDbType.Int);
                    arParms[2].Value = objstock.TotHandOver;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objstock.EmployeeID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_IndHandoverDetails", arParms);
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
        public int UpdateReceivedDetail(IndentRecvHandOverData objstock)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objstock.XMLData;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@TotReceived", SqlDbType.Int);
                    arParms[2].Value = objstock.TotReceived;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objstock.EmployeeID;

                    arParms[3] = new SqlParameter("@ReceivedBy", SqlDbType.BigInt);
                    arParms[3].Value = objstock.ReceivedBy;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_IndReceivedDetails", arParms);
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
        public List<IndentRecvHandOverData> GetHandOverList(IndentRecvHandOverData objbill)
        {
            List<IndentRecvHandOverData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];


                    arParms[0] = new SqlParameter("@SubStockID", SqlDbType.Int);
                    arParms[0].Value = objbill.SubStockID;


                    arParms[1] = new SqlParameter("@HandOverTo", SqlDbType.BigInt);
                    arParms[1].Value = objbill.HandOverTo;

                    arParms[2] = new SqlParameter("@ApprvBy", SqlDbType.BigInt);
                    arParms[2].Value = objbill.ApprvBy;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[3].Value = objbill.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[4].Value = objbill.DateTo;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIndentHndOverList", arParms);
                    List<IndentRecvHandOverData> listpatientdetails = ORHelper<IndentRecvHandOverData>.FromDataReaderToList(sqlReader);
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
