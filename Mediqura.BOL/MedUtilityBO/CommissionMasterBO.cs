using Mediqura.CommonData.MedUtilityData;
using Mediqura.DAL.MedUtilityDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedUtilityBO
{
  public class CommissionMasterBO
    {
      public int SaveCommissionData(CommissionMasterData objMasterData)
        {
            int result = 0;

            try
            {
                CommissionMasterDA objCommission = new CommissionMasterDA();
                result = objCommission.saveCommisionData(objMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
      public List<CommissionMasterData> SearchCommssiontDetails(CommissionMasterData objMasterData)
      {

          List<CommissionMasterData> result = null;

          try
          {
              CommissionMasterDA objCommssionDA = new CommissionMasterDA();
              result = objCommssionDA.SearchCommissionDetails(objMasterData);

          }
          catch (Exception ex)
          {
              PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
              LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
              throw new BusinessProcessException("4000001", ex);
          }
          return result;
      }
      public List<CommissionMasterData> GetCommissionDeatilbyID(CommissionMasterData objMasterData)
      {

          List<CommissionMasterData> result = null;

          try
          {
              CommissionMasterDA objCommssionDA = new CommissionMasterDA();
              result = objCommssionDA.GetCommissionDeatilbyID(objMasterData);

          }
          catch (Exception ex)
          {
              PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
              LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
              throw new BusinessProcessException("4000001", ex);
          }
          return result;

      }
      public int DeleteCommissionDetailsByID(CommissionMasterData objMasterData)
      {
          int result = 0;

          try
          {
              CommissionMasterDA objCommssionDA = new CommissionMasterDA();
              result = objCommssionDA.DeleteCommissionDetailsByID(objMasterData);

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
