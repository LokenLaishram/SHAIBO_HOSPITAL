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
    public class TestCenterMasterBO
    {
        public int UpdateTestCenterDetails(TestCenterMasterData objTestCenterMasterData)
        {
            int result = 0;
            try
            {
                TestCenterMasterDA objTestCenterMasterDA = new TestCenterMasterDA();
                result = objTestCenterMasterDA.UpdateTestCenterDetails(objTestCenterMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<TestCenterMasterData> GetTestCenterDetailsByID(TestCenterMasterData objTestCenterMasterData)
        {
            List<TestCenterMasterData> result = null;

            try
            {
                TestCenterMasterDA objTestCenterMasteDA = new TestCenterMasterDA();
                result = objTestCenterMasteDA.GetTestCenterDetailsByID(objTestCenterMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteTestCenterDetailsByID(TestCenterMasterData objTestCenterMasterData)
        {
            int result = 0;
            try
            {
                TestCenterMasterDA objTestCenterMasteDA = new TestCenterMasterDA();
                result = objTestCenterMasteDA.DeleteTestCenterDetailsByID(objTestCenterMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<TestCenterMasterData> SearchTestCenterDetails(TestCenterMasterData objTestCenterMasterData)
        {
            List<TestCenterMasterData> result = null;
            try
            {
                TestCenterMasterDA objTestCenterMasteDA = new TestCenterMasterDA();
                result = objTestCenterMasteDA.SearchTestCenterDetails(objTestCenterMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public int DeleteTestCenterTypeDetailsByID(TestCenterMasterData objTestCenterMasterData)
        {
            throw new NotImplementedException();
        }
    }
}
