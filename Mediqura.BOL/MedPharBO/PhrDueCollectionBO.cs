using Mediqura.CommonData.MedPharData;
using Mediqura.DAL.MedPharDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedPharBO
{
    public class PhrDueCollectionBO
    {        
      
        public List<PhrDueCollectionData> GetPhrCustomerDueList(PhrDueCollectionData objD)
        {
            List<PhrDueCollectionData> result = null;
            try
            {
                PhrDueCollectionDA objDueDA = new PhrDueCollectionDA();
                result = objDueDA.GetPhrCustomerDueList(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<PhrDueCollectionData> GetPhrBillDetails(PhrDueCollectionData objBillData)
        {
            List<PhrDueCollectionData> result = null;

            try
            {
                PhrDueCollectionDA objBillDA = new PhrDueCollectionDA();
                result = objBillDA.GetPhrBillDetails(objBillData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PhrDueCollectionData> UpdatePhrDueCollection(PhrDueCollectionData objdue)
        {
            List<PhrDueCollectionData> result = null;

            try
            {
                PhrDueCollectionDA objpatientDA = new PhrDueCollectionDA();
                result = objpatientDA.UpdatePhrDueCollection(objdue);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        //----TAB3-----//
        public List<PhrDueCollectionData> GetDuePaidCustomer(PhrDueCollectionData objD)
        {
            List<PhrDueCollectionData> result = null;
            try
            {
                PhrDueCollectionDA objDA = new PhrDueCollectionDA();
                result = objDA.GetDuePaidCustomer(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PhrDueCollectionData> GetPhrDueCollectionList(PhrDueCollectionData objD)
        {
            List<PhrDueCollectionData> result = null;
            try
            {
                PhrDueCollectionDA objDueDA = new PhrDueCollectionDA();
                result = objDueDA.GetPhrDueCollectionList(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int Delete_Phr_DueCollectionRecordByID(PhrDueCollectionData objDueData)
        {
            int result = 0;
            try
            {
                PhrDueCollectionDA objDA = new PhrDueCollectionDA();
                result = objDA.Delete_Phr_DueCollectionRecordByID(objDueData);
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
