using Mediqura.CommonData.MSBData;
using Mediqura.DAL.MSBDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MSBBO
{
   public class MsbBedAllotedBO
    {
        public List<MsbBedAllotData> GetMsbBedAlloted()
        {
            List<MsbBedAllotData> result = null;
            try
            {
                MsbBedAllotDA objDA = new MsbBedAllotDA();
                result = objDA.GetMsbBedAlloted();
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<MsbBedAllotData> GetMsbBedDetails(MsbBedAllotData objData)
        {
            List<MsbBedAllotData> result = null;
            try
            {
                MsbBedAllotDA objDA = new MsbBedAllotDA();
                result = objDA.GetMsbBedDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateMsbBedAllotation(MsbBedAllotData objData)
        {
            int result = 0;
            try
            {
                MsbBedAllotDA objDA = new MsbBedAllotDA();
                result = objDA.UpdateMsbBedAllotation(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateMsbBedAllotationDetails(MsbBedAllotData objData)
        {
            int result = 0;
            try
            {
                MsbBedAllotDA objDA = new MsbBedAllotDA();
                result = objDA.UpdateMsbBedAllotationDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteMsbBedAllotationDetailsID(MsbBedAllotData objData)
        {
            int result = 0;
            try
            {
                MsbBedAllotDA objDA = new MsbBedAllotDA();
                result = objDA.DeleteMsbBedAllotationDetailsID(objData);
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
