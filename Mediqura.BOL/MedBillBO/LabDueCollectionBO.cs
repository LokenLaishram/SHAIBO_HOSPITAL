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
    public class LabDueCollectionBO
    {
        public List<LabDueCollectionData> GetPhrCustomerDueList(LabDueCollectionData objD)
        {
            List<LabDueCollectionData> result = null;
            try
            {
                LabDueCollectionDA objDueDA = new LabDueCollectionDA();
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
        public List<LabDueCollectionData> GetPhrBillDetails(LabDueCollectionData objBillData)
        {
            List<LabDueCollectionData> result = null;

            try
            {
                LabDueCollectionDA objBillDA = new LabDueCollectionDA();
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
        public List<LabDueCollectionData> UpdatePhrDueCollection(LabDueCollectionData objdue)
        {
            List<LabDueCollectionData> result = null;

            try
            {
                LabDueCollectionDA objpatientDA = new LabDueCollectionDA();
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
        public List<LabDueCollectionData> GetDuePaidCustomer(LabDueCollectionData objD)
        {
            List<LabDueCollectionData> result = null;
            try
            {
                LabDueCollectionDA objDA = new LabDueCollectionDA();
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
        public List<LabDueCollectionData> GetPhrDueCollectionList(LabDueCollectionData objD)
        {
            List<LabDueCollectionData> result = null;
            try
            {
                LabDueCollectionDA objDueDA = new LabDueCollectionDA();
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
        public int Delete_Phr_DueCollectionRecordByID(LabDueCollectionData objDueData)
        {
            int result = 0;
            try
            {
                LabDueCollectionDA objDA = new LabDueCollectionDA();
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
