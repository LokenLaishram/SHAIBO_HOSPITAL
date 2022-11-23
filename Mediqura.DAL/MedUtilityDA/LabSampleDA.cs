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
    public class LabSampleDA
    {
        public int UpdateSampleTypeDetails(LabSampleData objPatientTypeMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@SampleID", SqlDbType.Int);
                    arParms[0].Value = objPatientTypeMaster.ID;

                    arParms[1] = new SqlParameter("@Code", SqlDbType.VarChar);
                    arParms[1].Value = objPatientTypeMaster.LabSampleTypeCode;

                    arParms[2] = new SqlParameter("@Descriptions", SqlDbType.VarChar);
                    arParms[2].Value = objPatientTypeMaster.LabSampleType;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objPatientTypeMaster.EmployeeID;

                    arParms[4] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[4].Value = objPatientTypeMaster.HospitalID;

                    arParms[5] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[5].Value = objPatientTypeMaster.ActionType;

                    arParms[6] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[6].Direction = ParameterDirection.Output;

                    arParms[7] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[7].Value = objPatientTypeMaster.IsActive;

                    arParms[8] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[8].Value = objPatientTypeMaster.IPaddress;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateSampleTypeMST", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[6].Value);
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
        public List<LabSampleData> SearchSampleTypeDetails(LabSampleData objPaymentTypeMaster)
        {
            List<LabSampleData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@Code", SqlDbType.VarChar);
                    arParms[0].Value = objPaymentTypeMaster.LabSampleTypeCode;

                    arParms[1] = new SqlParameter("@Descriptions", SqlDbType.VarChar);
                    arParms[1].Value = objPaymentTypeMaster.LabSampleType;

                    arParms[2] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[2].Value = objPaymentTypeMaster.IsActive;

                    arParms[3] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[3].Value = objPaymentTypeMaster.CurrentIndex;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchSampleType", arParms);
                    List<LabSampleData> lstPaymentTypeDetails = ORHelper<LabSampleData>.FromDataReaderToList(sqlReader);
                    result = lstPaymentTypeDetails;
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
        public List<LabSampleData> GetSampleTypeDetailsByID(LabSampleData objPaymentTypeMaster)
        {
            List<LabSampleData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@SampleID", SqlDbType.Int);
                    arParms[0].Value = objPaymentTypeMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetSampleTypeDetailsByID", arParms);
                    List<LabSampleData> lstPatientTypeDetails = ORHelper<LabSampleData>.FromDataReaderToList(sqlReader);
                    result = lstPatientTypeDetails;
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
        public List<LabSampleData> SearchSampleDetails(LabSampleData objPaymentTypeMaster)
        {
            List<LabSampleData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@Code", SqlDbType.VarChar);
                    arParms[0].Value = objPaymentTypeMaster.LabSampleTypeCode;

                    arParms[1] = new SqlParameter("@Descriptions", SqlDbType.VarChar);
                    arParms[1].Value = objPaymentTypeMaster.LabSampleType;

                    arParms[2] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[2].Value = objPaymentTypeMaster.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchSampleDetails", arParms);
                    List<LabSampleData> lstPaymentTypeDetails = ORHelper<LabSampleData>.FromDataReaderToList(sqlReader);
                    result = lstPaymentTypeDetails;
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

        public List<LabSampleData> GetSampleType(LabSampleData objPaymentTypeMaster)
        {
            List<LabSampleData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@SampleType", SqlDbType.VarChar);
                    arParms[0].Value = objPaymentTypeMaster.LabSampleType;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetSampleType", arParms);
                    List<LabSampleData> lstPatientTypeDetails = ORHelper<LabSampleData>.FromDataReaderToList(sqlReader);
                    result = lstPatientTypeDetails;
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
        public List<LabSampleData> GetLabTestUnits(LabSampleData objPaymentTypeMaster)
        {
            List<LabSampleData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@LabUnits", SqlDbType.VarChar);
                    arParms[0].Value = objPaymentTypeMaster.LabUnits;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetLabTestUnits", arParms);
                    List<LabSampleData> lstPatientTypeDetails = ORHelper<LabSampleData>.FromDataReaderToList(sqlReader);
                    result = lstPatientTypeDetails;
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
   
        public int DeleteSampleTypeDetailsByID(LabSampleData objPaymentTypeMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@SampleID", SqlDbType.Int);
                    arParms[0].Value = objPaymentTypeMaster.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objPaymentTypeMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objPaymentTypeMaster.Remarks;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objPaymentTypeMaster.IPaddress;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteSampletypeDetailsByID", arParms);
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
    }
}
