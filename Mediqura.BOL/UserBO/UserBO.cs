using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.LoginData;
using Mediqura.DAL.UserDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;

namespace Mediqura.BOL.UserBO
{
    public class UserBO
    {
        public bool UpdateLogData(LogData Objmeduser)
        {
            bool result = false;
            try
            {
                UserDA objUserDA = new UserDA();
                result = objUserDA.UpdateLogData(Objmeduser);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public bool UpdateLoginStatus(LogData Objmeduser)
        {
            bool result = false;
            try
            {
                UserDA objUserDA = new UserDA();
                result = objUserDA.UpdateLoginStatus(Objmeduser);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SiteMapData> GetMedPagesbyRoleID(RolesData objRole)
        {
            List<SiteMapData> objUserCE = null;
            try
            {
                UserDA objUserAdminDA = new UserDA();
                objUserCE = objUserAdminDA.GetMedPagesbyRoleID(objRole);
            }
            catch (Exception ex) //Exception of the layer(itself)/unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return objUserCE;
        }
        public bool SavePageRole(RolesData objRole)
        {
            bool status = false;
            try
            {
                LogManager.WriteLog(new LogSource(this, LogManager.WhoCalledMe(), EnumLogCategory.BusinessProcessEvents, EnumPriority.High, EnumLogEvenType.Information));
                UserDA objUserAdminDA = new UserDA();
                status = objUserAdminDA.SavePageRole(objRole);
            }
            catch (Exception ex) //Exception of the layer(itself)/unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return status;
        }
        public bool ClearuncompleteTransactions(LogData Objmeduser)
        {
            bool result = false;
            try
            {
                UserDA objUserDA = new UserDA();
                result = objUserDA.ClearuncompleteTransactions(Objmeduser);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SiteMapData> GetAllSiteMapItem(SiteMapData objSiteMap)
        {
            List<SiteMapData> objUserCE = null;
            try
            {
                UserDA objUserAdminDA = new UserDA();
                objUserCE = objUserAdminDA.GetAllSiteMapItem(objSiteMap);
            }
            catch (Exception ex) //Exception of the layer(itself)/unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return objUserCE;
        }
        public bool SaveSiteMapRole(RolesData objRole)
        {
            bool status = false;
            try
            {
                LogManager.WriteLog(new LogSource(this, LogManager.WhoCalledMe(), EnumLogCategory.BusinessProcessEvents, EnumPriority.High, EnumLogEvenType.Information));
                UserDA objUserAdminDA = new UserDA();
                status = objUserAdminDA.SaveSiteMapRole(objRole);
            }
            catch (Exception ex) //Exception of the layer(itself)/unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return status;
        }
        public List<SiteMapData> GetSiteMapRoleByRoleID(RolesData objRole)
        {
            List<SiteMapData> objUserCE = null;
            try
            {
                UserDA objUserAdminDA = new UserDA();
                objUserCE = objUserAdminDA.GetSiteMapRoleByRoleID(objRole);
            }
            catch (Exception ex) //Exception of the layer(itself)/unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return objUserCE;
        }
        public UserData getLogData(LogData Objmeduser)
        { 
            UserData result = null; 
            try
            {
                UserDA objUserDA = new UserDA();
                result = objUserDA.getLogData(Objmeduser);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public UserData getLogDatareset(LogData Objmeduser)
        {
            UserData result = null;
            try
            {
                UserDA objUserDA = new UserDA();
                result = objUserDA.getLogDatareset(Objmeduser);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<LogData> BindLogin(LogData Objmeduser)
        {

            List<LogData> result = null;

            try
            {
                UserDA objUserDA = new UserDA();
                result = objUserDA.BindLogin(Objmeduser);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;



        }
        public bool DeleteLogin(LogData Objmeduser)
        {
            bool result = false;
            try
            {
                UserDA objUserDA = new UserDA();
                result = objUserDA.DeleteLogin(Objmeduser);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public LogData ScheduleLogin(LogData Objmeduser)
        {
            LogData result;

            try
            {
                UserDA objUserDA = new UserDA();
                result = objUserDA.ScheduleLogin(Objmeduser);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public bool ScheduleLogOut(LogData Objmeduser)
        {
            bool result = false;
            try
            {
                UserDA objUserDA = new UserDA();
                result = objUserDA.ScheduleLogOut(Objmeduser);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public bool CheckLogout(LogData Objmeduser)
        {
            bool result = false;
            try
            {
                UserDA objUserDA = new UserDA();
                result = objUserDA.CheckLogout(Objmeduser);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
    }
}
