using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.MedBillData;
using Mediqura.DAL.MedBillDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.DAL.AdmissionDA;


namespace Mediqura.BOL.MedBillBO
{
    public class TestWiseBO
    {
        public List<TestWiseCollectionData> GetServiceName(TestWiseCollectionData objservice)
        {
            List<TestWiseCollectionData> result = null;

            try
            {
                TestWiseDA objServiceDA = new TestWiseDA();
                result = objServiceDA.GetServiceName(objservice);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<TestWiseCollectionData> GetNestedData(TestWiseCollectionData objpat)
        {
            List<TestWiseCollectionData> result = null;

            try
            {
                TestWiseDA objpatientDA = new TestWiseDA();
                result = objpatientDA.GetNestedData(objpat);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;


        }
        public List<TestWiseCollectionData> GetServicesList(TestWiseCollectionData objbill)
        {
            List<TestWiseCollectionData> result = null;
            try
            {
                TestWiseDA objDA = new TestWiseDA();
                result = objDA.GetServicesList(objbill);
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
