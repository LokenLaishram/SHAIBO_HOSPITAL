using Mediqura.CommonData.MedUtilityData;
using Mediqura.DAL;
using Mediqura.DAL.MedUtilityDA;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedUtilityBO
{
    public class ServiceMappingMasterBO
    {
        public List<ServiceMappingMasterData> GetServices(ServiceMappingMasterData objservice)
        {
            List<ServiceMappingMasterData> result = null;
            try
            {
                ServiceMappingMasterDA objServiceDA = new ServiceMappingMasterDA();
                 result = objServiceDA.GetServices(objservice);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateServiceMappingDetails(ServiceMappingMasterData obj)
        {
            int result = 0;
            try
            {
                ServiceMappingMasterDA objDA = new ServiceMappingMasterDA();
                result = objDA.UpdateServiceMappingDetails(obj);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ServiceMappingMasterData> GetServiceMappingDetails(ServiceMappingMasterData objData)
        {
            List<ServiceMappingMasterData> result = null;
            try
            {
                ServiceMappingMasterDA objDA = new ServiceMappingMasterDA();
                result = objDA.GetServiceMappingDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteServiceMappingDetailsByID(ServiceMappingMasterData objLabServiceMasterData)
        {
            int result = 0;
            try
            {
                ServiceMappingMasterDA objServiceMasterDA = new ServiceMappingMasterDA();
                result = objServiceMasterDA.DeleteServiceMappingDetailsByID(objLabServiceMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteLabSubTestDetailsByID(LabServiceMasterData objLabServiceMaster)
        {
            int result = 0;
            try
            {

                SqlParameter[] arParms = new SqlParameter[3];

                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = objLabServiceMaster.ID;

                arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                arParms[1].Value = objLabServiceMaster.EmployeeID;

                arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                arParms[2].Direction = ParameterDirection.Output;


                int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteLabSubTestDetailsByID", arParms);
                if (result_ > 0 || result_ == -1)
                    result = Convert.ToInt32(arParms[2].Value);
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;

        }
    }
}
