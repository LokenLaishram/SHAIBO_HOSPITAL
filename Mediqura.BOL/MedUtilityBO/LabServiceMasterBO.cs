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
    public class LabServiceMasterBO
    {
        public int UpdateLabServiceDetails(LabServiceMasterData objLabServiceMasterData)
        {
            int result = 0;
            try
            {
                LabServiceMasterDA objLabServiceMasterDA = new LabServiceMasterDA();
                result = objLabServiceMasterDA.UpdateLabServiceDetails(objLabServiceMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabServiceMasterData> GetLabServicesByserviceTypeID(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                LabServiceMasterDA objServiceDA = new LabServiceMasterDA();
                result = objServiceDA.GetLabServicesByserviceTypeID(objservice);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabServiceMasterData> GetTestNames(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                LabServiceMasterDA objServiceDA = new LabServiceMasterDA();
                result = objServiceDA.GetTestNames(objservice);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabServiceMasterData> GetTestNamesWithID(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                LabServiceMasterDA objServiceDA = new LabServiceMasterDA();
                result = objServiceDA.GetTestNamesWithID(objservice);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabServiceMasterData> GetTestName(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                LabServiceMasterDA objServiceDA = new LabServiceMasterDA();
                result = objServiceDA.GetTestName(objservice);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<LabServiceMasterData> GetRadioTestName(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                LabServiceMasterDA objServiceDA = new LabServiceMasterDA();
                result = objServiceDA.GetRadioTestName(objservice);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabServiceMasterData> GetEndosTestName(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                LabServiceMasterDA objServiceDA = new LabServiceMasterDA();
                result = objServiceDA.GetEndosTestName(objservice);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateLabSubTest(LabServiceMasterData objLabServiceMasterData)
        {
            int result = 0;
            try
            {
                LabServiceMasterDA objLabServiceMasterDA = new LabServiceMasterDA();
                result = objLabServiceMasterDA.UpdateLabSubTest(objLabServiceMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateLabSubTestName(LabServiceMasterData objLabServiceMasterData)
        {
            int result = 0;
            try
            {
                LabServiceMasterDA objLabServiceMasterDA = new LabServiceMasterDA();
                result = objLabServiceMasterDA.UpdateLabSubTestName(objLabServiceMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<LabServiceMasterData> GetLabSubTestDetailsByID(LabServiceMasterData objLabServiceMasterData)
        {
            List<LabServiceMasterData> result = null;

            try
            {
                LabServiceMasterDA objLabServiceMasterDA = new LabServiceMasterDA();
                result = objLabServiceMasterDA.GetLabSubTestDetailsByID(objLabServiceMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<LabServiceMasterData> GetLabServiceDetailsByID(LabServiceMasterData objLabServiceMasterData)
        {
            List<LabServiceMasterData> result = null;

            try
            {
                LabServiceMasterDA objLabServiceMasterDA = new LabServiceMasterDA();
                result = objLabServiceMasterDA.GetLabServiceDetailsByID(objLabServiceMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public int DeleteLabServiceDetailsByID(LabServiceMasterData objLabServiceMasterData)
        {
            int result = 0;
            try
            {
                LabServiceMasterDA objServiceMasterDA = new LabServiceMasterDA();
                result = objServiceMasterDA.DeleteLabServiceDetailsByID(objLabServiceMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public int DeleteLabSubTestDetailsByID(LabServiceMasterData objLabServiceMasterData)
        {
            int result = 0;
            try
            {
                LabServiceMasterDA objServiceMasterDA = new LabServiceMasterDA();
                result = objServiceMasterDA.DeleteLabSubTestDetailsByID(objLabServiceMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<LabServiceMasterData> SearchServiceDetails(LabServiceMasterData objLabServiceMasterData)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                LabServiceMasterDA objLabServiceMasterDA = new LabServiceMasterDA();
                result = objLabServiceMasterDA.SearchServiceDetails(objLabServiceMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabServiceMasterData> SearchLabServiceDetails(LabServiceMasterData objLabServiceMasterData)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                LabServiceMasterDA objLabServiceMasterDA = new LabServiceMasterDA();
                result = objLabServiceMasterDA.SearchLabServiceDetails(objLabServiceMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<LabServiceMasterData> SearchLabSubTestDetails(LabServiceMasterData objLabServiceMasterData)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                LabServiceMasterDA objLabServiceMasterDA = new LabServiceMasterDA();
                result = objLabServiceMasterDA.SearchLabSubTestDetails(objLabServiceMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabServiceMasterData> GetLabServices(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                LabServiceMasterDA objServiceDA = new LabServiceMasterDA();
                result = objServiceDA.GetLabServices(objservice);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabServiceMasterData> Getinvestigationlist(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                LabServiceMasterDA objServiceDA = new LabServiceMasterDA();
                result = objServiceDA.Getinvestigationlist(objservice);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabServiceMasterData> GetOPLabServices(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                LabServiceMasterDA objServiceDA = new LabServiceMasterDA();
                result = objServiceDA.GetOPLabServices(objservice);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabServiceMasterData> GetPackageServices(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;

            try
            {
                LabServiceMasterDA objServiceDA = new LabServiceMasterDA();
                result = objServiceDA.GetPackageServices(objservice);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabServiceMasterData> GetIPServices(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;

            try
            {
                LabServiceMasterDA objServiceDA = new LabServiceMasterDA();
                result = objServiceDA.GetIPServices(objservice);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabServiceMasterData> GetOTServices(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;

            try
            {
                LabServiceMasterDA objServiceDA = new LabServiceMasterDA();
                result = objServiceDA.GetOTServices(objservice);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabServiceMasterData> GetOPPhrServices(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;

            try
            {
                LabServiceMasterDA objServiceDA = new LabServiceMasterDA();
                result = objServiceDA.GetOPPhrServices(objservice);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<LabServiceMasterData> GetserviceListByID(LabServiceMasterData objLabServiceMasterData)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                LabServiceMasterDA objLabServiceMasterDA = new LabServiceMasterDA();
                result = objLabServiceMasterDA.GetserviceListByID(objLabServiceMasterData);
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
