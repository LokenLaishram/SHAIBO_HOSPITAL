using Mediqura.CommonData.MIS;
using Mediqura.DAL.MIS;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MIS
{
   public class StaffFundBO
    {

        public List<StaffFundReportData> GetTransactionList(StaffFundReportData objData)
        {
            List<StaffFundReportData> result = null;
            try
            {
                StaffFundDA objDA = new StaffFundDA();
                 result = objDA.GetTransactionList(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StaffFundReportData> GetStaffFundList(StaffFundReportData objData)
        {
            List<StaffFundReportData> result = null;
            try
            {
                StaffFundDA objDA = new StaffFundDA();
                result = objDA.GetStaffFundList(objData);
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
