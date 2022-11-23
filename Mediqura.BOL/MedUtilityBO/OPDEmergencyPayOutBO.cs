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
      public class OPDEmergencyPayOutBO
    {
          public List<OPDEmergencyPayOutData> GetDoctorName(OPDEmergencyPayOutData objD)
        {
            List<OPDEmergencyPayOutData> result = null;
            try
            {
                OPDEmergencyPayOutDA objpatientDA = new OPDEmergencyPayOutDA();
                result = objpatientDA.GetDoctorName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
          public int UpdateOPDEmergencyData(OPDEmergencyPayOutData objData)
          {
              int result = 0;
              try
              {
                  OPDEmergencyPayOutDA objDA = new OPDEmergencyPayOutDA();
                  result = objDA.UpdateOPDEmergencyData(objData);
              }
              catch (Exception ex)
              {
                  PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                  LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                  throw new BusinessProcessException("4000001", ex);
              }
              return result;
          }
          public int UpdateOPDEmergency(OPDEmergencyPayOutData objData)
          {
              int result = 0;
              try
              {
                  OPDEmergencyPayOutDA objDA = new OPDEmergencyPayOutDA();
                  result = objDA.UpdateOPDEmergency(objData);
              }
              catch (Exception ex)
              {
                  PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                  LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                  throw new BusinessProcessException("4000001", ex);
              }
              return result;
          }
          public int UpdateIPDDoctorRound(OPDEmergencyPayOutData objData)
          {
              int result = 0;
              try
              {
                  OPDEmergencyPayOutDA objDA = new OPDEmergencyPayOutDA();
                  result = objDA.UpdateIPDDoctorRound(objData);
              }
              catch (Exception ex)
              {
                  PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                  LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                  throw new BusinessProcessException("4000001", ex);
              }
              return result;
          }
  
          public List<OPDEmergencyPayOutData> SearchOPDEmergencyPayOutDetails(OPDEmergencyPayOutData objData)
          {
              List<OPDEmergencyPayOutData> result = null;
              try
              {
                  OPDEmergencyPayOutDA objDA = new OPDEmergencyPayOutDA();
                  result = objDA.SearchOPDEmergencyPayOutDetails(objData);
              }
              catch (Exception ex)
              {
                  PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                  LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                  throw new BusinessProcessException("4000001", ex);
              }
              return result;
          }
          public List<OPDEmergencyPayOutData> SearchIPDRoundPayOutDetails(OPDEmergencyPayOutData objData)
          {
              List<OPDEmergencyPayOutData> result = null;
              try
              {
                  OPDEmergencyPayOutDA objDA = new OPDEmergencyPayOutDA();
                  result = objDA.SearchIPDRoundPayOutDetails(objData);
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
