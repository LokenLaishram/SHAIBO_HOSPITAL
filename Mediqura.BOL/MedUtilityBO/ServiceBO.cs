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
    public class ServiceBO
    {
        public int UpdateServiceDetails(ServicesData objservice)
        {
            int result = 0;
            try
            {
                ServiceDA objServiceDA = new ServiceDA();
                result = objServiceDA.UpdateServiceDetails(objservice);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ServicesData> SearchServiceDetails(ServicesData objservice)
        {
            List<ServicesData> result = null;
            try
            {
                ServiceDA objServiceDA = new ServiceDA();
                result = objServiceDA.SearchServiceDetails(objservice);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ServicesData> GetAutoProcedureName(ServicesData objservice)
        {
            List<ServicesData> result = null;
            try
            {
                ServiceDA objServiceDA = new ServiceDA();
                result = objServiceDA.GetAutoProcedureName(objservice);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ServicesData> SearchServiceDetailsReport(ServicesData objservice)
        {
            List<ServicesData> result = null;
            try
            {
                ServiceDA objServiceDA = new ServiceDA();
                result = objServiceDA.SearchServiceDetailsReport(objservice);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ServicesData> GetServiceDetailsByID(ServicesData objservice)
        {
            List<ServicesData> result = null;

            try
            {
                ServiceDA objServiceDA = new ServiceDA();
                result = objServiceDA.GetServiceDetailsByID(objservice);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteServiceDetailsByID(ServicesData objservice)
        {
            int result = 0;
            try
            {
                ServiceDA objServiceDA = new ServiceDA();
                result = objServiceDA.DeleteServiceDetailsByID(objservice);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ServicesData> Getopservices(ServicesData objservice)
        {
            List<ServicesData> result = null;

            try
            {
                ServiceDA objServiceDA = new ServiceDA();
                result = objServiceDA.Getopservices(objservice);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ServicesData> SearchServiceDetailsByType(ServicesData objservice)
        {
            List<ServicesData> result = null;
            try
            {
                ServiceDA objServiceDA = new ServiceDA();
                result = objServiceDA.SearchServiceDetailsByType(objservice);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ServicesData> Gettestservices(ServicesData objservice)
        {
            List<ServicesData> result = null;

            try
            {
                ServiceDA objServiceDA = new ServiceDA();
                result = objServiceDA.Gettestservices(objservice);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ServicesData> GetCenterwisetestservices(ServicesData objservice)
        {
            List<ServicesData> result = null;

            try
            {
                ServiceDA objServiceDA = new ServiceDA();
                result = objServiceDA.GetCenterwisetestservices(objservice);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ServicesData> GetServiceName(ServicesData objservice)
        {
            List<ServicesData> result = null;

            try
            {
                ServiceDA objServiceDA = new ServiceDA();
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
        public List<ServicesData> GetRemarktestservicesByID(ServicesData objservice)
        {
            List<ServicesData> result = null;

            try
            {
                ServiceDA objServiceDA = new ServiceDA();
                result = objServiceDA.GetRemarktestservicesByID(objservice);

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
