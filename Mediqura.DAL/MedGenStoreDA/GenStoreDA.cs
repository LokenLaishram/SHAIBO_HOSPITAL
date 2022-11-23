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
    public class GenStoreDA
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
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GetPONo", arParms);
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
        public List<GENStrData> GetSupplierName(GENStrData objD)
        {
            List<GENStrData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@SupplierName", SqlDbType.VarChar);
                    arParms[0].Value = objD.SupplierName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GetSupplierName", arParms);
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
        public List<GENStrData> GetItemName(GENStrData objD)
        {
            List<GENStrData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_Get_ItemName", arParms);
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
        public List<GENStrData> GetReturnItemBysupplireID(GENStrData objD)
        {
            List<GENStrData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    arParms[1] = new SqlParameter("@SupplierID", SqlDbType.Int);
                    arParms[1].Value = objD.SupplierID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_Get_ReturnItemNameBysupplier", arParms);
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
        public List<GENStrData> GetCompanyName(GENStrData objD)
        {
            List<GENStrData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@CompanyName", SqlDbType.VarChar);
                    arParms[0].Value = objD.CompanyName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GetCompanyName", arParms);
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
        public List<GENStrData> GetItemNames(GENStrData objD)
        {
            List<GENStrData> result = null;
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
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_ItemNames", arParms);
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
        public List<GENStrData> Get_ItemNameByID(GENStrData objD)
        {
            List<GENStrData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemID", SqlDbType.Int);
                    arParms[0].Value = objD.ItemID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_Get_ItemNameByID", arParms);
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
        public List<GENStrData> GetItemNameWithID(GENStrData objD)
        {
            List<GENStrData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;

                    arParms[1] = new SqlParameter("@GroupID", SqlDbType.Int);
                    arParms[1].Value = objD.GroupID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GetItemNameWithID", arParms);
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
        public int UpdateStockItemDetails(GENStrData objstock)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[17];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objstock.XMLData;

                    arParms[1] = new SqlParameter("@Discount", SqlDbType.Money);
                    arParms[1].Value = objstock.Discount;

                    arParms[2] = new SqlParameter("@ReceivedBy", SqlDbType.BigInt);
                    arParms[2].Value = objstock.ReceivedBy;

                    arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[3].Value = objstock.HospitalID;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objstock.EmployeeID;

                    arParms[5] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[5].Direction = ParameterDirection.Output;

                    arParms[6] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[6].Value = objstock.ActionType;

                    arParms[7] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[7].Value = objstock.FinancialYearID;

                    arParms[8] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[8].Value = objstock.IsActive;

                    arParms[9] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[9].Value = objstock.IPaddress;

                    arParms[10] = new SqlParameter("@TrecievedQty", SqlDbType.Int);
                    arParms[10].Value = objstock.TrecievedQty;

                    arParms[11] = new SqlParameter("@TotalFreeQty", SqlDbType.Int);
                    arParms[11].Value = objstock.TotalFreeQty;

                    arParms[12] = new SqlParameter("@TotalrecievedQty", SqlDbType.Int);
                    arParms[12].Value = objstock.TotalrecievedQty;

                    arParms[13] = new SqlParameter("@PurchasetypeID", SqlDbType.Int);
                    arParms[13].Value = objstock.PurchasetypeID;

                    arParms[14] = new SqlParameter("@TotalFreeItemAmount", SqlDbType.Money);
                    arParms[14].Value = objstock.TotalFreeItemAmount;

                    arParms[15] = new SqlParameter("@PaymentTypeID", SqlDbType.Int);
                    arParms[15].Value = objstock.PaymentTypeID;
                    
                    arParms[16] = new SqlParameter("@DueAmt", SqlDbType.Money);
                    arParms[16].Value = objstock.DueAmt;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_Update_StockItemDetails", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[5].Value);
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
        public List<GENStrData> GetStockItemList(GENStrData objbill)
        {
            List<GENStrData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[13];

                    arParms[0] = new SqlParameter("@ReceiptNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.ReceiptNo;

                    arParms[1] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[1].Value = objbill.ItemName;

                    arParms[2] = new SqlParameter("@PONo", SqlDbType.VarChar);
                    arParms[2].Value = objbill.PONo;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[3].Value = objbill.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[4].Value = objbill.DateTo;

                    arParms[5] = new SqlParameter("@GroupID", SqlDbType.Int);
                    arParms[5].Value = objbill.GroupID;

                    arParms[6] = new SqlParameter("@SubGroupID", SqlDbType.Int);
                    arParms[6].Value = objbill.SubGroupID;

                    arParms[7] = new SqlParameter("@ReceivedBy", SqlDbType.BigInt);
                    arParms[7].Value = objbill.ReceivedBy;

                    arParms[8] = new SqlParameter("@mfgcompany", SqlDbType.Int);
                    arParms[8].Value = objbill.CompanyID;

                    arParms[9] = new SqlParameter("@SupplierID", SqlDbType.Int);
                    arParms[9].Value = objbill.SupplierID;

                    arParms[10] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[10].Value = objbill.IsActive;

                    arParms[11] = new SqlParameter("@RackGroup", SqlDbType.Int);
                    arParms[11].Value = objbill.RackGroup;

                    arParms[12] = new SqlParameter("@RackSubGroup", SqlDbType.Int);
                    arParms[12].Value = objbill.RackSubGroup;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GetStockItemList", arParms);
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
        public int DeleteStockItemListByID(GENStrData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@ReceiptNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.ReceiptNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objbill.EmployeeID;

                    arParms[4] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[4].Value = objbill.ItemName;

                    arParms[5] = new SqlParameter("@PONo", SqlDbType.VarChar);
                    arParms[5].Value = objbill.PONo;

                    arParms[6] = new SqlParameter("@ID", SqlDbType.VarChar);
                    arParms[6].Value = objbill.ID;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_DeleteStockItemListbyID", arParms);
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
        public List<GENStrData> GetrecdNo(GENStrData objD)
        {
            List<GENStrData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ReceiptNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.ReceiptNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GetReceiptNo", arParms);
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
        public int UpdateStockCondemnedItemDetails(GENStrData objstock)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objstock.XMLData;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[2].Value = objstock.EmployeeID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_Update_StockCondemnedItemDetails", arParms);
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
        public List<GENStrData> GetUnitByItemID(GENStrData objD)
        {
            List<GENStrData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemID", SqlDbType.Int);
                    arParms[0].Value = objD.ItemID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GetUnitByItemID", arParms);
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

    }
}
