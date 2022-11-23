using Mediqura.CommonData.MedBillData;
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
namespace Mediqura.DAL.MedBillDA
{
    public class IPDrugIndentDA
    {
        public List<IPDrugIndentData> GetItemDetailsByStockID(IPDrugIndentData objD)
        {
            List<IPDrugIndentData> result = null;
            try
            {
                {

                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@StockID", SqlDbType.BigInt);
                    arParms[0].Value = objD.StockID;

                    //arParms[0] = new SqlParameter("@NoOfQty", SqlDbType.Int);
                    //arParms[0].Value = objD.NoOfQty;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetStockListPhr", arParms);
                    List<IPDrugIndentData> lstresult = ORHelper<IPDrugIndentData>.FromDataReaderToList(sqlReader);
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
        public int UpdateIndentItemDetails(IPDrugIndentData objstock)
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

                    arParms[8] = new SqlParameter("@TotRequestQty", SqlDbType.Int);
                    arParms[8].Value = objstock.TotRequestQty;

                    arParms[9] = new SqlParameter("@IPNo", SqlDbType.NVarChar);
                     arParms[9].Value = objstock.IPNo;



                     arParms[10] = new SqlParameter("@IndentRequestID", SqlDbType.Int);
                     arParms[10].Value = objstock.IndentRequestID;

                    //arParms[10] = new SqlParameter("@SubStockID", SqlDbType.Int);
                    //arParms[10].Value = objstock.SubStockID;

                    //arParms[11] = new SqlParameter("@ReqdQty", SqlDbType.Int);
                    //arParms[11].Value = objstock.ReqdQty;


                     int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_IndenttemDetailsPhr", arParms);
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
        public List<IPDrugIndentData> GetIndentItemList(IPDrugIndentData objbill)
        {
            List<IPDrugIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.NVarChar);
                    arParms[0].Value = objbill.IPNo;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[1].Value = objbill.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[2].Value = objbill.DateTo;

                    arParms[3] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[3].Value = objbill.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIndentItemListPHR", arParms);
                  //  sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIndentHndOverListPHR", arParms);

                    List<IPDrugIndentData> listpatientdetails = ORHelper<IPDrugIndentData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle   usp_MDQ_GetIndentHndOverListPHR
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public int DeleteIndentReqByID(IPDrugIndentData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];


                    arParms[0] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[0].Direction = ParameterDirection.Output;

                    arParms[1] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[1].Value = objbill.Remarks;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objbill.EmployeeID;

                    arParms[3] = new SqlParameter("@IndentID", SqlDbType.BigInt);
                    arParms[3].Value = objbill.IndentID;

                    arParms[4] = new SqlParameter("@IndentNo", SqlDbType.NVarChar);
                    arParms[4].Value = objbill.IndentNo;



                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteIndRequestPHRbyID", arParms);
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
        public List<IPDrugIndentData> GetIndentList(IPDrugIndentData objbill)
        {
            List<IPDrugIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                                    
                   

                    arParms[0] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[0].Value = objbill.DateFrom;

                    arParms[1] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[1].Value = objbill.DateTo;

                    arParms[2] = new SqlParameter("@IPNo", SqlDbType.NVarChar);
                    arParms[2].Value = objbill.IPNo;

                    arParms[3] = new SqlParameter("@IndentRequestID", SqlDbType.Int);
                    arParms[3].Value = objbill.IndentRequestID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIndentRecvListPHR", arParms);
                    List<IPDrugIndentData> listpatientdetails = ORHelper<IPDrugIndentData>.FromDataReaderToList(sqlReader);
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
        public List<IPDrugIndentData> GetIndentList1(IPDrugIndentData objbill)
        {
            List<IPDrugIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IndentNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.IndentNo;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIndentDetailPHR", arParms);
                    List<IPDrugIndentData> listpatientdetails = ORHelper<IPDrugIndentData>.FromDataReaderToList(sqlReader);
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
        public int UpdateIndentDetailPHR(IPDrugIndentData objstock)
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


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_IndentDetailsPHR", arParms);
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
        public List<IPDrugIndentData> GetHandOverList(IPDrugIndentData objbill)
        {
            List<IPDrugIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];


                    arParms[0] = new SqlParameter("@HandOverTo", SqlDbType.BigInt);
                    arParms[0].Value = objbill.HandOverTo;

                    arParms[1] = new SqlParameter("@ApprvBy", SqlDbType.BigInt);
                    arParms[1].Value = objbill.ApprvBy;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[3].Value = objbill.DateTo;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIndentHndOverListPHR", arParms);
                    List<IPDrugIndentData> listpatientdetails = ORHelper<IPDrugIndentData>.FromDataReaderToList(sqlReader);
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
        public List<IPDrugIndentData> bindIndentRecvList(IPDrugIndentData objbill)
        {
            List<IPDrugIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];


                    arParms[0] = new SqlParameter("@IndentRequestID", SqlDbType.Int);
                    arParms[0].Value = objbill.IndentRequestID;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[1].Value = objbill.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[2].Value = objbill.DateTo;


                    arParms[3] = new SqlParameter("@IPNo", SqlDbType.NVarChar);
                    arParms[3].Value = objbill.IPNo;




                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIndentHndOvListPHR", arParms);
                    List<IPDrugIndentData> listpatientdetails = ORHelper<IPDrugIndentData>.FromDataReaderToList(sqlReader);
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
        public List<IPDrugIndentData> GetHndoverDetail(IPDrugIndentData objbill)
        {
            List<IPDrugIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IndentNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.IndentNo;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIndentRecvDetailPHR", arParms);
                    List<IPDrugIndentData> listpatientdetails = ORHelper<IPDrugIndentData>.FromDataReaderToList(sqlReader);
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
        public int UpdateReceivedDetail(IPDrugIndentData objstock)
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

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_IndReceivedDetailsPHR", arParms);
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
        public List<IPDrugIndentData> GetRecvList(IPDrugIndentData objbill)
        {
            List<IPDrugIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];


                    //arParms[0] = new SqlParameter("@SubStockID", SqlDbType.Int);
                    //arParms[0].Value = objbill.SubStockID;

                    arParms[0] = new SqlParameter("@ReceivedBy", SqlDbType.BigInt);
                    arParms[0].Value = objbill.ReceivedBy;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[1].Value = objbill.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[2].Value = objbill.DateTo;

                    arParms[3] = new SqlParameter("@IndStatus", SqlDbType.Int);
                    arParms[3].Value = objbill.IndStatus;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIRecvListPHR", arParms);
                    List<IPDrugIndentData> listpatientdetails = ORHelper<IPDrugIndentData>.FromDataReaderToList(sqlReader);
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
