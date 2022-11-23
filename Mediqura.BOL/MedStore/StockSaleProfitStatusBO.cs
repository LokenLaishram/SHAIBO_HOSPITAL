using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.MedStore;
using Mediqura.DAL.MedStore;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;

namespace Mediqura.BOL.MedStore
{
    public class StockSaleProfitStatusBO
    {
        public List<StoreProfitStatusData> GetStockSaleProfitStatusList(StoreProfitStatusData objstockprofit)
        {
            List<StoreProfitStatusData> result = null;
            try
            {
                StockSaleProfitStatusDA objDA = new StockSaleProfitStatusDA();
                result = objDA.GetStockSaleProfitStatusList(objstockprofit);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
    }
}
