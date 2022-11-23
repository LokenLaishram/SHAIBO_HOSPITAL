using Mediqura.CommonData.MedPharData;
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

namespace Mediqura.DAL.MedPharDA
{
    public class ItemSalesTrackerDA
    {
        public List<ItemSalesTrackerData> GetItemName(ItemSalesTrackerData objD)
        {
            List<ItemSalesTrackerData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phr_Get_AutoItemList", arParms);
                    List<ItemSalesTrackerData> lstresult = ORHelper<ItemSalesTrackerData>.FromDataReaderToList(sqlReader);
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
        public List<ItemSalesTrackerData> GetSaleItemList(ItemSalesTrackerData objtransfer)
        {
            List<ItemSalesTrackerData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@ItemID", SqlDbType.BigInt);
                    arParms[0].Value = objtransfer.ItemID;

                    arParms[1] = new SqlParameter("@BatchNo", SqlDbType.VarChar);
                    arParms[1].Value = objtransfer.BatchNo;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[2].Value = objtransfer.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[3].Value = objtransfer.DateTo;

                    arParms[4] = new SqlParameter("@SupplierID", SqlDbType.BigInt);
                    arParms[4].Value = objtransfer.SupplierID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phr_Get_ItemSaleTracker", arParms);
                    List<ItemSalesTrackerData> listpatientdetails = ORHelper<ItemSalesTrackerData>.FromDataReaderToList(sqlReader);
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
        public List<ItemSalesTrackerData> GetItemSaleDetailsList(ItemSalesTrackerData objsale)
        {
            List<ItemSalesTrackerData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@ItemID", SqlDbType.Int);
                    arParms[0].Value = objsale.ItemID;

                    arParms[1] = new SqlParameter("@BatchNo", SqlDbType.VarChar);
                    arParms[1].Value = objsale.BatchNo;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[2].Value = objsale.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[3].Value = objsale.DateTo;

					arParms[4] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
					arParms[4].Value = objsale.PatientTypeID;
					

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phr_SaleItemTrackerDetails", arParms);
                    List<ItemSalesTrackerData> listSaleDetails = ORHelper<ItemSalesTrackerData>.FromDataReaderToList(sqlReader);
                    result = listSaleDetails;
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
