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
    public class ServiceDA
    {
        public int UpdateServiceDetails(ServicesData objservice)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[12];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objservice.ID;

                    arParms[1] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[1].Value = objservice.ServiceTypeID;

                    arParms[2] = new SqlParameter("@ServiceName", SqlDbType.VarChar);
                    arParms[2].Value = objservice.ServiceName;

                    arParms[3] = new SqlParameter("@ServiceCharge", SqlDbType.Money);
                    arParms[3].Value = objservice.ServiceCharge;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objservice.EmployeeID;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[5].Value = objservice.HospitalID;

                    arParms[6] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[6].Value = objservice.ActionType;

                    arParms[7] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[7].Direction = ParameterDirection.Output;

                    arParms[8] = new SqlParameter("@Code", SqlDbType.VarChar);
                    arParms[8].Value = objservice.Code;

                    arParms[9] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[9].Value = objservice.IsActive;

                    arParms[10] = new SqlParameter("@FinancialYear", SqlDbType.Int);
                    arParms[10].Value = objservice.FinancialYearID;

                    arParms[11] = new SqlParameter("@SubServiceTypeID", SqlDbType.Int);
                    arParms[11].Value = objservice.SubServiceTypeID;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateServiceMST", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[7].Value);
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
        public List<ServicesData> SearchServiceDetails(ServicesData objservice)
        {
            List<ServicesData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@Code", SqlDbType.VarChar);
                    arParms[0].Value = objservice.Code;

                    arParms[1] = new SqlParameter("@ServiceName", SqlDbType.VarChar);
                    arParms[1].Value = objservice.ServiceName;

                    arParms[2] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[2].Value = objservice.ServiceTypeID;

                    arParms[3] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[3].Value = objservice.IsActive;

                    arParms[4] = new SqlParameter("@SubServiceTypeID", SqlDbType.Int);
                    arParms[4].Value = objservice.SubServiceTypeID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_SearchServices", arParms);
                    List<ServicesData> lstServiceDetails = ORHelper<ServicesData>.FromDataReaderToList(sqlReader);
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
        public List<ServicesData> GetAutoProcedureName(ServicesData objservice)
        {
            List<ServicesData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ServiceName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.ServiceName;

                    arParms[1] = new SqlParameter("@SubServiceTypeID", SqlDbType.Int);
                    arParms[1].Value = objservice.SubServiceTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetAutoprocedureList", arParms);
                    List<ServicesData> lstServiceDetails = ORHelper<ServicesData>.FromDataReaderToList(sqlReader);
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
        public List<ServicesData> SearchServiceDetailsReport(ServicesData objservice)
        {
            List<ServicesData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[0].Value = objservice.ServiceTypeID;

                    arParms[1] = new SqlParameter("@SubServiceTypeID", SqlDbType.Int);
                    arParms[1].Value = objservice.SubServiceTypeID;

                    arParms[2] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[2].Value = objservice.ID;
                    
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_SearchServicesReport", arParms);
                    List<ServicesData> lstServiceDetails = ORHelper<ServicesData>.FromDataReaderToList(sqlReader);
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
        public List<ServicesData> GetServiceDetailsByID(ServicesData objservice)
        {
            List<ServicesData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objservice.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GeTServicesByID", arParms);
                    List<ServicesData> lstServiceDetails = ORHelper<ServicesData>.FromDataReaderToList(sqlReader);
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
        public List<ServicesData> GetServiceName(ServicesData objservice)
        {
            List<ServicesData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ServiceName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.ServiceName;

                    arParms[1] = new SqlParameter("@SubServiceTypeID", SqlDbType.Int);
                    arParms[1].Value = objservice.SubServiceTypeID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GeTServiceName", arParms);
                    List<ServicesData> lstServiceDetails = ORHelper<ServicesData>.FromDataReaderToList(sqlReader);
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
        public List<ServicesData> Getopservices(ServicesData objservice)
        {
            List<ServicesData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ServiceName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.ServiceName;

                    arParms[1] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[1].Value = objservice.ServiceTypeID;

                    arParms[2] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[2].Value = objservice.DoctorID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_op_Services", arParms);
                    List<ServicesData> lstServiceDetails = ORHelper<ServicesData>.FromDataReaderToList(sqlReader);
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
        public int DeleteServiceDetailsByID(ServicesData objservice)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objservice.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objservice.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objservice.Remarks;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteServicesByID", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[2].Value);
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
        public List<ServicesData> SearchServiceDetailsByType(ServicesData objservice)
        {
            List<ServicesData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ServiceName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.ServiceName;

                    arParms[1] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[1].Value = objservice.ServiceTypeID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_SearchServicesByType", arParms);
                    List<ServicesData> lstServiceDetails = ORHelper<ServicesData>.FromDataReaderToList(sqlReader);
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
        public List<ServicesData> Gettestservices(ServicesData objservice)
        {
            List<ServicesData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ServiceName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.ServiceName;

                    arParms[1] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[1].Value = objservice.ServiceTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_labServices", arParms);
                    List<ServicesData> lstServiceDetails = ORHelper<ServicesData>.FromDataReaderToList(sqlReader);
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
        public List<ServicesData> GetCenterwisetestservices(ServicesData objservice)
        {
            List<ServicesData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ServiceName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.ServiceName;

                    arParms[1] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[1].Value = objservice.ServiceTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_CenterwiselabServices", arParms);
                    List<ServicesData> lstServiceDetails = ORHelper<ServicesData>.FromDataReaderToList(sqlReader);
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
        public List<ServicesData> GetRemarktestservicesByID(ServicesData objservice)
        {
            List<ServicesData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ServiceName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.ServiceName;

                    arParms[1] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[1].Value = objservice.ServiceTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_RemraklabServicesByID", arParms);
                    List<ServicesData> lstServiceDetails = ORHelper<ServicesData>.FromDataReaderToList(sqlReader);
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
    }
}
