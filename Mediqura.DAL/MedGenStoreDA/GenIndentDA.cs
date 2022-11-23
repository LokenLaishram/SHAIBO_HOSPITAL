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
    public class GenIndentDA
    {
        public List<GenIndentData> GetItemNameListInStore(GenIndentData objD)
        {
            List<GenIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Get_ItemNameWithID", arParms);
                    List<GenIndentData> lstresult = ORHelper<GenIndentData>.FromDataReaderToList(sqlReader);
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
        public List<GenIndentData> GetItemAvailabilty(GenIndentData objD)
        {
            List<GenIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objD.ID;

                    arParms[1] = new SqlParameter("@GenStockID", SqlDbType.Int);
                    arParms[1].Value = objD.GenStockID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Get_ItemAvailibilty", arParms);
                    List<GenIndentData> lstresult = ORHelper<GenIndentData>.FromDataReaderToList(sqlReader);
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
        public List<GenIndentData> GetItemDetailsByItemID(GenIndentData objD)
        {
            List<GenIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemID", SqlDbType.BigInt);
                    arParms[0].Value = objD.ItemID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_GEN_MDQ_GetStockList", arParms);
                    List<GenIndentData> lstresult = ORHelper<GenIndentData>.FromDataReaderToList(sqlReader);
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
        public List<GenIndentData> GetUserByDeptID(GenIndentData objD)
        {
            List<GenIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@DeptID", SqlDbType.Int);
                    arParms[0].Value = objD.DeptID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetUserOfdeptId", arParms);
                    List<GenIndentData> lstresult = ORHelper<GenIndentData>.FromDataReaderToList(sqlReader);
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
        public List<GenIndentData> GetDeptByEmpID(GenIndentData objD)
        {
            List<GenIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objD.EmployeeID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDepartmentByEmpID", arParms);
                    List<GenIndentData> lstresult = ORHelper<GenIndentData>.FromDataReaderToList(sqlReader);
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
        public List<GenIndentData> UpdateIndentItemDetails(GenIndentData objstock)
        {
            List<GenIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[13];

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

                    arParms[9] = new SqlParameter("@IndentRequestID", SqlDbType.Int);
                    arParms[9].Value = objstock.IndentRequestID;

                    arParms[10] = new SqlParameter("@GenStockID", SqlDbType.Int);
                    arParms[10].Value = objstock.GenStockID;

                    arParms[11] = new SqlParameter("@IndentRaiseDate", SqlDbType.DateTime);
                    arParms[11].Value = objstock.IndentRaiseDate;

                    arParms[12] = new SqlParameter("@RequestedBy", SqlDbType.BigInt);
                    arParms[12].Value = objstock.RequestedBy;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Update_IndentItemDetails", arParms);
                    List<GenIndentData> lstresult = ORHelper<GenIndentData>.FromDataReaderToList(sqlReader);
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
        public List<GenIndentData> UpdateStockReturnDetails(GenIndentData objstock)
        {
            List<GenIndentData> result = null;
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

                    arParms[3] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[3].Value = objstock.FinancialYearID;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objstock.IPaddress;

                    arParms[5] = new SqlParameter("@TotReturnQty", SqlDbType.Int);
                    arParms[5].Value = objstock.TotReturnQty;

                    arParms[6] = new SqlParameter("@ReturnBy", SqlDbType.BigInt);
                    arParms[6].Value = objstock.ReturnBy;

                    arParms[7] = new SqlParameter("@GenStockID", SqlDbType.Int);
                    arParms[7].Value = objstock.GenStockID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Update_StockReturnDetails", arParms);
                    List<GenIndentData> lstresult = ORHelper<GenIndentData>.FromDataReaderToList(sqlReader);
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
        public List<GenIndentData> GetIndentItemList(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@IndentNo", SqlDbType.NVarChar);
                    arParms[0].Value = objbill.IndentNo;

                    arParms[1] = new SqlParameter("@GenStockID", SqlDbType.Int);
                    arParms[1].Value = objbill.GenStockID;

                    arParms[2] = new SqlParameter("@IndentRequestID", SqlDbType.Int);
                    arParms[2].Value = objbill.IndentRequestID;

                    arParms[3] = new SqlParameter("@ReceivedBy", SqlDbType.BigInt);
                    arParms[3].Value = objbill.ReceivedBy;

                    arParms[4] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[4].Value = objbill.DateFrom;

                    arParms[5] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[5].Value = objbill.DateTo;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_GEN_MDQ_GetIndentItemList", arParms);
                    List<GenIndentData> listpatientdetails = ORHelper<GenIndentData>.FromDataReaderToList(sqlReader);
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
        public int UpdateIndent_status(GenIndentData objdisch)
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
        public List<GenIndentData> IndentListDetailsByID(GenIndentData objStoreUnitMaster)
        {
            List<GenIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IndentID", SqlDbType.Int);
                    arParms[0].Value = objStoreUnitMaster.IndentID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetIndentStatusDetailsByID", arParms);
                    List<GenIndentData> lstLabUnitDetails = ORHelper<GenIndentData>.FromDataReaderToList(sqlReader);
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
        public int DeleteIndentReqByID(GenIndentData objbill)
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

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_GEN_MDQ_DeleteIndRequestListbyID", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[0].Value);
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
        public List<GenIndentData> GetIndentList1(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IndentNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.IndentNo;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIndentDetail", arParms);
                    List<GenIndentData> listpatientdetails = ORHelper<GenIndentData>.FromDataReaderToList(sqlReader);
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
        public List<GenIndentData> GetAvailableByID(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_DeptItemAvailibility", arParms);
                    List<GenIndentData> chargelist = ORHelper<GenIndentData>.FromDataReaderToList(sqlReader);
                    result = chargelist;
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
        public List<GenIndentData> GetItemNameListInApprvTemp(GenIndentData objD)
        {
            List<GenIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    arParms[1] = new SqlParameter("@DeptID", SqlDbType.Int);
                    arParms[1].Value = objD.DeptID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Get_availableWithID", arParms);
                    List<GenIndentData> lstresult = ORHelper<GenIndentData>.FromDataReaderToList(sqlReader);
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
        public List<GenIndentData> GetReturnItemList(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@ReturnNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.ReturnNo;

                    arParms[1] = new SqlParameter("@GenStockID", SqlDbType.Int);
                    arParms[1].Value = objbill.GenStockID;

                    arParms[2] = new SqlParameter("@ReturnBy", SqlDbType.BigInt);
                    arParms[2].Value = objbill.ReturnBy;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[3].Value = objbill.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[4].Value = objbill.DateTo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_GEN_MDQ_GetStockReturnList", arParms);
                    List<GenIndentData> listpatientdetails = ORHelper<GenIndentData>.FromDataReaderToList(sqlReader);
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
        public int DeleteStockReurnByID(GenIndentData objbill)
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

                    arParms[3] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[3].Value = objbill.ID;

                    arParms[4] = new SqlParameter("@ReturnNo", SqlDbType.VarChar);
                    arParms[4].Value = objbill.ReturnNo;

                    arParms[5] = new SqlParameter("@ReturnQty", SqlDbType.VarChar);
                    arParms[5].Value = objbill.NoReturn;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_GEN_MDQ_DeleteStockReturnListbyID", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[0].Value);
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
        public List<GenIndentData> GetDeptStockList(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@GenStockID", SqlDbType.Int);
                    arParms[0].Value = objbill.GenStockID;

                    arParms[1] = new SqlParameter("@ItemID", SqlDbType.Int);
                    arParms[1].Value = objbill.ItemID;

                    arParms[2] = new SqlParameter("@StatusID", SqlDbType.Int);
                    arParms[2].Value = objbill.StatusID;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objbill.DateTo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_GEN_MDQ_GetDeptStockList", arParms);
                    List<GenIndentData> listpatientdetails = ORHelper<GenIndentData>.FromDataReaderToList(sqlReader);
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
        public List<GenIndentData> GetNeedtoPurchageitemlist(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@GenStockID", SqlDbType.Int);
                    arParms[0].Value = objbill.GenStockID;

                    arParms[1] = new SqlParameter("@ItemID", SqlDbType.Int);
                    arParms[1].Value = objbill.ItemID;

                    arParms[2] = new SqlParameter("@StatusID", SqlDbType.Int);
                    arParms[2].Value = objbill.StatusID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_GEN_MDQ_GetNeetoPurchageItem", arParms);
                    List<GenIndentData> listpatientdetails = ORHelper<GenIndentData>.FromDataReaderToList(sqlReader);
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
        public int UpdateDeptStock(GenIndentData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objbill.XMLData;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_Update_DeptStock", arParms);
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
    }
}
