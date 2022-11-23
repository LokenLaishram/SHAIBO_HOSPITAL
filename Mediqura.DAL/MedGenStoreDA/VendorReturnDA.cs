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
    public class VendorReturnDA
    {
        public List<GENStrData> GetPONo(GENStrData objD)
        {
            List<GENStrData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@PONo", SqlDbType.VarChar);
                    arParms[0].Value = objD.PONo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GetautoPONo", arParms);
                    List<GENStrData> lstresult = ORHelper<GENStrData>.FromDataReaderToList(sqlReader);
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
        public List<GENStrData> GetStockistItemList(GENStrData objbill)
        {
            List<GENStrData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@SupplierID", SqlDbType.Int);
                    arParms[0].Value = objbill.SupplierID;

                    arParms[1] = new SqlParameter("@ItemID", SqlDbType.BigInt);
                    arParms[1].Value = objbill.ItemID;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[3].Value = objbill.DateTo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GetStockistItemList", arParms);
                    List<GENStrData> listpatientdetails = ORHelper<GENStrData>.FromDataReaderToList(sqlReader);
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
        public int UpdateStockistReturn(GENStrData objstock)
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

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_Update_StockistReturnItemDetails", arParms);
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
        public List<GENStrData> GetStockistItemReturnList(GENStrData objbill)
        {
            List<GENStrData> result = null;
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
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GetStockistItemReturnList", arParms);
                    List<GENStrData> listpatientdetails = ORHelper<GENStrData>.FromDataReaderToList(sqlReader);
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
        public int DeleteStockistReturnItemListByID(GENStrData objbill)
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


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_DeleteStockistItemReturnListbyID", arParms);
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
