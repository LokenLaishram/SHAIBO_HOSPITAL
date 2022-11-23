using Mediqura.CommonData.MedUtilityData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Utility.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.DAL.MedUtilityDA
{
    public class ServiceMappingMasterDA
    {
        public List<ServiceMappingMasterData> GetServices(ServiceMappingMasterData objservice)
        {
            List<ServiceMappingMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ServiceName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.ServiceName;

                    arParms[1] = new SqlParameter("@GroupType", SqlDbType.Int);
                    arParms[1].Value = objservice.GroupType;
                    
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GeTServicesByServiceTypeID", arParms);
                    List<ServiceMappingMasterData> lstServiceDetails = ORHelper<ServiceMappingMasterData>.FromDataReaderToList(sqlReader);
                    result = lstServiceDetails;
                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public int UpdateServiceMappingDetails(ServiceMappingMasterData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objbill.XMLData;

                    arParms[1] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[1].Value = objbill.FinancialYearID;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objbill.EmployeeID;

                    arParms[3] = new SqlParameter("@Output", SqlDbType.Int);
                    arParms[3].Direction = ParameterDirection.Output;

                    arParms[4] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[4].Value = objbill.HospitalID;

                    arParms[5] = new SqlParameter("@GroupType", SqlDbType.Int);
                    arParms[5].Value = objbill.GroupType;

                    arParms[6] = new SqlParameter("@ServicetypeID", SqlDbType.Int);
                    arParms[6].Value = objbill.ServicetypeID;

                    arParms[7] = new SqlParameter("@MappingType", SqlDbType.Int);
                    arParms[7].Value = objbill.MappingType;

                    arParms[8] = new SqlParameter("@SubServicetype", SqlDbType.Int);
                    arParms[8].Value = objbill.SubServicetype;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateServiceMappingDetails", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[3].Value);
                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public List<ServiceMappingMasterData> GetServiceMappingDetails(ServiceMappingMasterData obj)
        {
            List<ServiceMappingMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@ServiceID", SqlDbType.Int);
                    arParms[0].Value = obj.ServiceID;

                    arParms[1] = new SqlParameter("@GroupType", SqlDbType.Int);
                    arParms[1].Value = obj.GroupType;

                    arParms[2] = new SqlParameter("@ServicetypeID", SqlDbType.Int);
                    arParms[2].Value = obj.ServicetypeID;

                    arParms[3] = new SqlParameter("@MappingType", SqlDbType.Int);
                    arParms[3].Value = obj.MappingType;

                    arParms[4] = new SqlParameter("@SubServicetype", SqlDbType.Int);
                    arParms[4].Value = obj.SubServicetype;



                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetServiceMappingDetailList", arParms);
                    List<ServiceMappingMasterData> lstDetails = ORHelper<ServiceMappingMasterData>.FromDataReaderToList(sqlReader);
                    result = lstDetails;
                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public int DeleteServiceMappingDetailsByID(ServiceMappingMasterData objMaster)
        {
            int result = 0;
            try
            {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objMaster.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteServiceMappingDetailsByID", arParms);
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
