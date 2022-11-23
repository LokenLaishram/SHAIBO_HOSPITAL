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
    public class StockReturnDA
    {
        public int UpdateStockReturnDetails(StockReturnData objstock)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objstock.XMLData;

                    arParms[1] = new SqlParameter("@HandOver", SqlDbType.Int);
                    arParms[1].Value = objstock.HandOver;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    arParms[3] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[3].Value = objstock.FinancialYearID;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objstock.IPaddress;

                    arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[5].Value = objstock.EmployeeID;

                    arParms[6] = new SqlParameter("@TotalReturnQty", SqlDbType.Int);
                    arParms[6].Value = objstock.TotalReturnQty;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_StockReturnDetails", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[2].Value);
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
        public List<StockReturnData> GetStockReturnList(StockReturnData objbill)
        {
            List<StockReturnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@ReturnNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.ReturnNo;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[1].Value = objbill.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[2].Value = objbill.DateTo;

                    arParms[3] = new SqlParameter("@ReturnByID", SqlDbType.BigInt);
                    arParms[3].Value = objbill.ReturnByID;

                    arParms[4] = new SqlParameter("@HandedTo", SqlDbType.BigInt);
                    arParms[4].Value = objbill.HandOver;

                    arParms[5] = new SqlParameter("@Status", SqlDbType.Bit);
                    arParms[5].Value = objbill.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetStockReturnList", arParms);
                    List<StockReturnData> listpatientdetails = ORHelper<StockReturnData>.FromDataReaderToList(sqlReader);
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
        public List<StockReturnData> Getstock_ReturnDetails(StockReturnData objstockdetails)
        {
            List<StockReturnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@StockID", SqlDbType.BigInt);
                    arParms[0].Value = objstockdetails.StockID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetStockReturnItem_Details", arParms);
                    List<StockReturnData> listpatientdetails = ORHelper<StockReturnData>.FromDataReaderToList(sqlReader);
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
        public int DeleteStockReturnItemListByID(StockReturnData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@ReturnID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.ReturnID;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objbill.EmployeeID;

                    arParms[4] = new SqlParameter("@ReturnNo", SqlDbType.VarChar);
                    arParms[4].Value = objbill.ReturnNo;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteStockItemReturnListbyID", arParms);
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

        //---------------------------------------------Start Medical Stock Return----------------------------------------------

        public List<StockReturnData> GetStockReturnListItem(StockReturnData ObjStockReturnData)
        {
            List<StockReturnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@ItemID", SqlDbType.Int);
                    arParms[0].Value = ObjStockReturnData.ItemID;

                    arParms[1] = new SqlParameter("@SupplierID", SqlDbType.Int);
                    arParms[1].Value = ObjStockReturnData.SupplierID;

                    arParms[2] = new SqlParameter("@FilterTypeID", SqlDbType.Int);
                    arParms[2].Value = ObjStockReturnData.FilterTypeID;

                    arParms[3] = new SqlParameter("@ExpiredFrom", SqlDbType.DateTime);
                    arParms[3].Value = ObjStockReturnData.ExpiredFrom;

                    arParms[4] = new SqlParameter("@ExpiredTo", SqlDbType.DateTime);
                    arParms[4].Value = ObjStockReturnData.ExpiredTo;
                     
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetStockReturnListItem", arParms);
                    List<StockReturnData> listpatientdetails = ORHelper<StockReturnData>.FromDataReaderToList(sqlReader);
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
        public string SaveStockReturnList(StockReturnData ObjStockReturnData)
        {
            string result = null;  
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[12];

                    arParms[0] = new SqlParameter("@SupplierID", SqlDbType.Int);
                    arParms[0].Value = ObjStockReturnData.SupplierID;

                    arParms[1] = new SqlParameter("@SupplierName", SqlDbType.VarChar);
                    arParms[1].Value = ObjStockReturnData.SupplierName;

                    arParms[2] = new SqlParameter("@FilterTypeID", SqlDbType.Int);
                    arParms[2].Value = ObjStockReturnData.FilterTypeID;

                    arParms[3] = new SqlParameter("@FilterTypeName", SqlDbType.VarChar);
                    arParms[3].Value = ObjStockReturnData.FilterTypeName;

                    arParms[4] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[4].Value = ObjStockReturnData.XMLData;

                    arParms[5] = new SqlParameter("@TotalReturnQty", SqlDbType.Int);
                    arParms[5].Value = ObjStockReturnData.TotalReturnQty;

                    arParms[6] = new SqlParameter("@TotalReturnPrice", SqlDbType.Money);
                    arParms[6].Value = ObjStockReturnData.TotalReturnPrice;

                    arParms[7] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[7].Value = ObjStockReturnData.EmployeeID;

                    arParms[8] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[8].Value = ObjStockReturnData.FinancialYearID;

                    arParms[9] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[9].Value = ObjStockReturnData.HospitalID;

                    arParms[10] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[10].Value = ObjStockReturnData.ActionType;

                    arParms[11] = new SqlParameter("@Output", SqlDbType.VarChar, 20);
                    arParms[11].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_MED_SaveStockReturnList", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToString(arParms[11].Value);
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
        //---------------------------------------------End Medical Stock Return----------------------------------------------

    }
}
