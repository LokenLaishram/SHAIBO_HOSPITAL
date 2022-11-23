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
   public class DoctorMasterBO
    {
       public List<DoctorMasterData> SearchDoctorByType(DoctorMasterData objDoctorMasterData)
        {
            List<DoctorMasterData> result = null;
            try
            {
                DoctorMasterDA objDoctorMasteDA = new DoctorMasterDA();
                result = objDoctorMasteDA.SearchDoctorByType(objDoctorMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
       public List<DoctorMasterData> SearchDoctorByTypeAndDept(DoctorMasterData objDoctorMasterData)
       {
           List<DoctorMasterData> result = null;
           try
           {
               DoctorMasterDA objDoctorMasteDA = new DoctorMasterDA();
               result = objDoctorMasteDA.SearchDoctorByTypeAndDept(objDoctorMasterData);
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
