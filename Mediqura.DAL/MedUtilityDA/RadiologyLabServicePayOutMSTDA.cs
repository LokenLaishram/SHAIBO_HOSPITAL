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
    public class RadiologyLabServicePayOutMSTDA
    {
        public List<RadilogyLabServiceMSTData> SearchRadiologyLabServicePayOutDetails(RadilogyLabServiceMSTData objData)
        {
            List<RadilogyLabServiceMSTData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@GroupID", SqlDbType.Int);
                    arParms[0].Value = objData.GroupID;

                    arParms[1] = new SqlParameter("@SubGroupID", SqlDbType.Int);
                    arParms[1].Value = objData.SubGroupID;

                    arParms[2] = new SqlParameter("@ServiceID", SqlDbType.Int);
                    arParms[2].Value = objData.ServiceID;

                    arParms[3] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[3].Value = objData.PatientTypeID;

                    arParms[4] = new SqlParameter("@BedClassID", SqlDbType.Int);
                    arParms[4].Value = objData.BedClassID;

                    arParms[5] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[5].Value = objData.EmployeeID;

                    arParms[6] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[6].Value = objData.FinancialYearID;

                    arParms[7] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[7].Value = objData.HospitalID;

                    arParms[8] = new SqlParameter("@TestcenterID", SqlDbType.Int);
                    arParms[8].Value = objData.TestcenterID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchRadiologyLabServicePayOutDetails", arParms);
                    List<RadilogyLabServiceMSTData> lstDetails = ORHelper<RadilogyLabServiceMSTData>.FromDataReaderToList(sqlReader);
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
        public List<RadilogyLabServiceMSTData> AddLabservicelist(RadilogyLabServiceMSTData objData)
        {
            List<RadilogyLabServiceMSTData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@ServiceID", SqlDbType.Int);
                    arParms[0].Value = objData.ServiceID;

                    arParms[1] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[1].Value = objData.PatientTypeID;

                    arParms[2] = new SqlParameter("@BedClassID", SqlDbType.Int);
                    arParms[2].Value = objData.BedClassID;

                    arParms[3] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[3].Value = objData.EmployeeID;

                    arParms[4] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[4].Value = objData.FinancialYearID;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[5].Value = objData.HospitalID;

                    arParms[6] = new SqlParameter("@TestcenterID", SqlDbType.Int);
                    arParms[6].Value = objData.TestcenterID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_AddserivicetoLabServicePayOutDetails", arParms);
                    List<RadilogyLabServiceMSTData> lstDetails = ORHelper<RadilogyLabServiceMSTData>.FromDataReaderToList(sqlReader);
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
        public int UpdateRadiologyLabPayOut(RadilogyLabServiceMSTData objData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@XmlData", SqlDbType.Xml);
                    arParms[0].Value = objData.XMLData;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objData.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[3].Value = objData.HospitalID;

                    arParms[4] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[4].Value = objData.FinancialYearID;

                    arParms[5] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[5].Value = objData.IPaddress;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateRadiologyLabPayOutMST", arParms);
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
