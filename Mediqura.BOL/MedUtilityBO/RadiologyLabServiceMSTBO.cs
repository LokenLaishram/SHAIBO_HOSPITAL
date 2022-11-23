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
     public class RadiologyLabServiceMSTBO
    {
         public List<RadilogyLabServiceMSTData> SearchRadiologyLabServicePayOutDetails(RadilogyLabServiceMSTData objData)
        {
            List<RadilogyLabServiceMSTData> result = null;
            try
            {
                RadiologyLabServicePayOutMSTDA objDA = new RadiologyLabServicePayOutMSTDA();
                result = objDA.SearchRadiologyLabServicePayOutDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
         public List<RadilogyLabServiceMSTData> AddLabservicelist(RadilogyLabServiceMSTData objData)
         {
             List<RadilogyLabServiceMSTData> result = null;
             try
             {
                 RadiologyLabServicePayOutMSTDA objDA = new RadiologyLabServicePayOutMSTDA();
                 result = objDA.AddLabservicelist(objData);
             }
             catch (Exception ex)
             {
                 PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                 LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                 throw new BusinessProcessException("4000001", ex);
             }
             return result;
         }
         public int UpdateRadiologyLabPayOut(RadilogyLabServiceMSTData objData)
         {
             int result = 0;
             try
             {
                 RadiologyLabServicePayOutMSTDA objDA = new RadiologyLabServicePayOutMSTDA();
                 result = objDA.UpdateRadiologyLabPayOut(objData);
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
