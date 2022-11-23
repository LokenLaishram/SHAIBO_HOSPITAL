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
    public class OPReturnDA
    {
        public List<OPReturnData> GetAutoPhrOPBills(OPReturnData objD)
        {
            List<OPReturnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@OPBillNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.OPBillNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phr_GetAutoOPBillNo", arParms);
                    List<OPReturnData> lstresult = ORHelper<OPReturnData>.FromDataReaderToList(sqlReader);
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
        public List<OPReturnData> GetOPIssueListByBillNo(OPReturnData objbill)
        {
            List<OPReturnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@OPBillNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.OPBillNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetOPIssueListByBillNo", arParms);
                    List<OPReturnData> listpatientdetails = ORHelper<OPReturnData>.FromDataReaderToList(sqlReader);
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
        public string UpdateOPReturnDetails(OPReturnData objstock)
        {
           string result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[15];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objstock.XMLData;

                    arParms[1] = new SqlParameter("@CustommerName", SqlDbType.VarChar);
                    arParms[1].Value = objstock.PatientName;

                    arParms[2] = new SqlParameter("@OPBillNo", SqlDbType.VarChar);
                    arParms[2].Value = objstock.OPBillNo;

                    arParms[3] = new SqlParameter("@totalreturnQty", SqlDbType.Int);
                    arParms[3].Value = objstock.totalreturnQty;

                    arParms[4] = new SqlParameter("@TotalReturnAmount", SqlDbType.Decimal);
                    arParms[4].Value = objstock.TotalReturnAmount;

                    arParms[5] = new SqlParameter("@DeductedPC", SqlDbType.Decimal);
                    arParms[5].Value = objstock.DeductedPC;

                    arParms[6] = new SqlParameter("@TotalActualReturnAmount", SqlDbType.Decimal);
                    arParms[6].Value = objstock.TotalActualReturnAmount;
                                                                             
                    arParms[7] = new SqlParameter("@HandOver", SqlDbType.Int);
                    arParms[7].Value = objstock.HandOver;

                    arParms[8] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[8].Value = objstock.Remarks;

                    arParms[9] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[9].Value = objstock.ActionType;

                    arParms[10] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[10].Value = objstock.FinancialYearID;

                    arParms[11] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[11].Value = objstock.IsActive;

                    arParms[12] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[12].Value = objstock.IPaddress;

                    arParms[13] = new SqlParameter("@EmployeeID", SqlDbType.VarChar);
                    arParms[13].Value = objstock.EmployeeID;

                    arParms[14] = new SqlParameter("@Output", SqlDbType.VarChar,50);
                    arParms[14].Direction = ParameterDirection.Output;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_OPReturnDetails", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToString(arParms[14].Value);
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
        public List<OPReturnData> GetOPReturnList(OPReturnData objbill)
        {
            List<OPReturnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ReturnNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.ReturnNo;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[1].Value = objbill.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[2].Value = objbill.DateTo;

                    arParms[3] = new SqlParameter("@Status", SqlDbType.Bit);
                    arParms[3].Value = objbill.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetOPReturnList", arParms);
                    List<OPReturnData> listpatientdetails = ORHelper<OPReturnData>.FromDataReaderToList(sqlReader);
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
        public int DeleteOPReturnItemListByID(OPReturnData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ReturnNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.ReturnNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objbill.EmployeeID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteOPItemReturnListbyID", arParms);
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
