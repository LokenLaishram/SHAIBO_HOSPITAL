using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Mediqura.CommonData.MedStore;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Utility.Util;

namespace Mediqura.DAL.MedStore
{
    public class SupplierSaleTrackerDA 
    {
        public List<SupplierSaleTrackerData> GetItemName(SupplierSaleTrackerData ObjData)
        {
            List<SupplierSaleTrackerData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = ObjData.ItemName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phr_Get_AutoItemList", arParms);
                    List<SupplierSaleTrackerData> lstresult = ORHelper<SupplierSaleTrackerData>.FromDataReaderToList(sqlReader);
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
        public List<SupplierSaleTrackerData> GetSupplierSaleItemList(SupplierSaleTrackerData ObjData)
        {
            List<SupplierSaleTrackerData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@SupplierID", SqlDbType.BigInt);
                    arParms[0].Value = ObjData.SupplierID;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.Date);       
                    arParms[1].Value = ObjData.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[2].Value = ObjData.DateTo;

                    arParms[3] = new SqlParameter("@ItemID", SqlDbType.BigInt);
                    arParms[3].Value = ObjData.ItemID;

                    arParms[4] = new SqlParameter("@BatchNo", SqlDbType.VarChar);
                    arParms[4].Value = ObjData.BatchNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetSupplierSaleItemList", arParms);
                    List<SupplierSaleTrackerData> listSupplierSaleDetails = ORHelper<SupplierSaleTrackerData>.FromDataReaderToList(sqlReader);
                    result = listSupplierSaleDetails;
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
