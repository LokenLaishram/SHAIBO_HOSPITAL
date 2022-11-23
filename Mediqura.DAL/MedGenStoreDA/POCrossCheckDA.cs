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
    public class POCrossCheckDA
    {
        public List<StockGRNData> GetPurchaseOrderList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@PONo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.PONo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GetPurchaseOrderList", arParms);
                    List<StockGRNData> listpatientdetails = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
        public int UpdatePOCrossChecK(StockGRNData objstock)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

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

                    arParms[5] = new SqlParameter("@TotalOrderedQty", SqlDbType.Int);
                    arParms[5].Value = objstock.TotalQuantity;

                    arParms[6] = new SqlParameter("@TotalRecdQty", SqlDbType.Int);
                    arParms[6].Value = objstock.TotalRecdQty;

                    arParms[7] = new SqlParameter("@Verifyby", SqlDbType.BigInt);
                    arParms[7].Value = objstock.EmployeeID;

                    arParms[8] = new SqlParameter("@TotalCP", SqlDbType.Money);
                    arParms[8].Value = objstock.TotalCP;

                    arParms[9] = new SqlParameter("@PONo", SqlDbType.VarChar);
                    arParms[9].Value = objstock.PONo;

                



                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_Update_POCrossCheck", arParms);
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
        public List<StockGRNData> GetPOCheckedList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@PONo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.PONo;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[1].Value = objbill.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[2].Value = objbill.DateTo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GetPOCheckedList", arParms);
                    List<StockGRNData> listpatientdetails = ORHelper<StockGRNData>.FromDataReaderToList(sqlReader);
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
