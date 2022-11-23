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
    public class SampleContainerBO
    {
        public int UpdatecontainerDetails(SampleContainerData objSampleContainerMasterData)
        {
            int result = 0;
            try
            {
                SampleContainerDA objSampleContainerMasterDA = new SampleContainerDA();
                result = objSampleContainerMasterDA.UpdatecontainerDetails(objSampleContainerMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleContainerData> SearchcontainerDetails(SampleContainerData objSampleContainerMasterData)
        {
            List<SampleContainerData> result = null;
            try
            {
                SampleContainerDA objSampleContainerMasterDA = new SampleContainerDA();
                result = objSampleContainerMasterDA.SearchcontainerDetails(objSampleContainerMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleContainerData> GetcontainerDetailsByID(SampleContainerData objSampleContainerMasterData)
        {
            List<SampleContainerData> result = null;

            try
            {
                SampleContainerDA objSampleContainerMasterDA = new SampleContainerDA();
                result = objSampleContainerMasterDA.GetcontainerDetailsByID(objSampleContainerMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeletecontainerDetailsByID(SampleContainerData objSampleContainerMasterData)
        {
            int result = 0;
            try
            {
                SampleContainerDA objSampleContainerMasterDA = new SampleContainerDA();
                result = objSampleContainerMasterDA.DeletecontainerDetailsByID(objSampleContainerMasterData);
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
