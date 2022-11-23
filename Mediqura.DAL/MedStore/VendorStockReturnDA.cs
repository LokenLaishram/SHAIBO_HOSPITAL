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
    public class VendorStockReturnDA
    {
        public List<VendorStockReturnData> GetAutoRecieptNo(VendorStockReturnData objD)
        {
            List<VendorStockReturnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ReceiptNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.ReceiptNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetStockEntryRecieptNo", arParms);
                    List<VendorStockReturnData> lstresult = ORHelper<VendorStockReturnData>.FromDataReaderToList(sqlReader);
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
        public List<VendorStockReturnData> GetReturnStockList(VendorStockReturnData objcondemn)
        {
            List<VendorStockReturnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ReceiptNo", SqlDbType.VarChar);
                    arParms[0].Value = objcondemn.ReceiptNo;

                    arParms[1] = new SqlParameter("@RecievedDate", SqlDbType.Date);
                    arParms[1].Value = objcondemn.RecievedDate;

                  
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GetVendorStockReturnList", arParms);
                    List<VendorStockReturnData> listreturndetails = ORHelper<VendorStockReturnData>.FromDataReaderToList(sqlReader);
                    result = listreturndetails;
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
        public List<VendorStockReturnData> UpdateVendorReturnStockDetails(VendorStockReturnData objReturnStock)
        {
            List<VendorStockReturnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[11];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objReturnStock.XMLData;

                    arParms[1] = new SqlParameter("@ReceiptNo", SqlDbType.Int);
                    arParms[1].Value = objReturnStock.ReceiptNo;

                    arParms[2] = new SqlParameter("@GrandTotalReturnQty", SqlDbType.Int);
                    arParms[2].Value = objReturnStock.GrandTotalReturnQty;

                    arParms[3] = new SqlParameter("@GrandTotalReturnAmount", SqlDbType.Decimal);
                    arParms[3].Value = objReturnStock.GrandTotalReturnAmount;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objReturnStock.EmployeeID;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[5].Value = objReturnStock.HospitalID;

                    arParms[6] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[6].Value = objReturnStock.FinancialYearID;

                    arParms[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[7].Value = objReturnStock.IPaddress;

                    arParms[8] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[8].Value = objReturnStock.IsActive;

                    arParms[9] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[9].Value = objReturnStock.ActionType;

                    arParms[10] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[10].Direction = ParameterDirection.Output;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_Update_VendorReturnStockDetails", arParms);
                    List<VendorStockReturnData> lstresult = ORHelper<VendorStockReturnData>.FromDataReaderToList(sqlReader);
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
