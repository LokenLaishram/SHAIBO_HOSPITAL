using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.MedStore;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Utility.Util;

namespace Mediqura.DAL.MedStore
{
    public class StockSaleProfitStatusDA
    {
        public List<StoreProfitStatusData> GetStockSaleProfitStatusList(StoreProfitStatusData objstockprofit)
        {

            List<StoreProfitStatusData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@CustomerTypeID", SqlDbType.Int);
                    arParms[0].Value = objstockprofit.CustomerTypeID;

                    arParms[1] = new SqlParameter("@StockStatusID", SqlDbType.Int);
                    arParms[1].Value = objstockprofit.StockStatusID;
                  
                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[2].Value = objstockprofit.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[3].Value = objstockprofit.DateTo;

                    arParms[4] = new SqlParameter("@Month", SqlDbType.Int);
                    arParms[4].Value = objstockprofit.Month;

                    arParms[5] = new SqlParameter("@Year", SqlDbType.Int);
                    arParms[5].Value = objstockprofit.Year;

                    arParms[6] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[6].Value = objstockprofit.IsActive;  

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetStockSaleProfitLessList", arParms);
                    List<StoreProfitStatusData> liststoreprofitdetials = ORHelper<StoreProfitStatusData>.FromDataReaderToList(sqlReader);
                    result = liststoreprofitdetials;
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
