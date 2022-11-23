using Mediqura.CommonData.MedBillData;
using Mediqura.DAL.MedBillDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedBillBO
{
   public class DoctorWiseDailyCollectionBO
    {
       public List<DoctorWiseCollectionMasterData> GetDoctorsDailyCollectionList(DoctorWiseCollectionMasterData objbill)
        {
            List<DoctorWiseCollectionMasterData> result = null;
            try
            {
                DoctorWiseDailyCollectionDA objDA = new DoctorWiseDailyCollectionDA();
                result = objDA.GetDoctorsDailyCollectionList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
       public List<DoctorWiseCollectionMasterData> GetDoctorspaymentCollectionList(DoctorWiseCollectionMasterData objbill)
       {
           List<DoctorWiseCollectionMasterData> result = null;
           try
           {
               DoctorWiseDailyCollectionDA objDA = new DoctorWiseDailyCollectionDA();
               result = objDA.GetDoctorspaymentCollectionList(objbill);
           }
           catch (Exception ex)
           {
               PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
               LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
               throw new BusinessProcessException("4000001", ex);
           }
           return result;

       }
       public List<DoctorWiseCollectionMasterData> GetDoctorspaymentHistory(DoctorWiseCollectionMasterData objbill)
       {
           List<DoctorWiseCollectionMasterData> result = null;
           try
           {
               DoctorWiseDailyCollectionDA objDA = new DoctorWiseDailyCollectionDA();
               result = objDA.GetDoctorspaymentHistory(objbill);
           }
           catch (Exception ex)
           {
               PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
               LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
               throw new BusinessProcessException("4000001", ex);
           }
           return result;

       }
     
       public int UpdateCollectionVerification(DoctorWiseCollectionMasterData objbill)
       {
           int result = 0;
           try
           {
               DoctorWiseDailyCollectionDA objDA = new DoctorWiseDailyCollectionDA();
               result = objDA.UpdateCollectionVerification(objbill);
           }
           catch (Exception ex)
           {
               PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
               LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
               throw new BusinessProcessException("4000001", ex);
           }
           return result;
       }
       public int UpdateDoctorPaymentCollection(DoctorWiseCollectionMasterData objbill)
       {
           int result = 0;
           try
           {
               DoctorWiseDailyCollectionDA objDA = new DoctorWiseDailyCollectionDA();
               result = objDA.UpdateDoctorPaymentCollection(objbill);
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
