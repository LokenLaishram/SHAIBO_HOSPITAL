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
    public class OPDEmergencyPayOutDA
    {
        public List<OPDEmergencyPayOutData> GetDoctorName(OPDEmergencyPayOutData objD)
        {
            List<OPDEmergencyPayOutData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@DoctorName", SqlDbType.VarChar);
                    arParms[0].Value = objD.DoctorName;

                    arParms[1] = new SqlParameter("@DoctorType", SqlDbType.Int);
                    arParms[1].Value = objD.DoctorTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDoctorNameByDoctorType", arParms);
                    //sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_OPD_Service_ChargeList", arParms);
                    List<OPDEmergencyPayOutData> lstresult = ORHelper<OPDEmergencyPayOutData>.FromDataReaderToList(sqlReader);
                    result = lstresult;
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
        public int UpdateOPDEmergencyData(OPDEmergencyPayOutData objData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[11];

                    arParms[0] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[0].Value = objData.ServiceTypeID;

                    arParms[1] = new SqlParameter("@ServiceID", SqlDbType.Int);
                    arParms[1].Value = objData.ServiceID;

                    arParms[2] = new SqlParameter("@DoctorTypeID", SqlDbType.Int);
                    arParms[2].Value = objData.DoctorTypeID;

                    arParms[3] = new SqlParameter("@DoctorID", SqlDbType.Int);
                    arParms[3].Value = objData.DoctorID;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objData.EmployeeID;

                    arParms[5] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[5].Value = objData.ActionType;

                    arParms[6] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[6].Direction = ParameterDirection.Output;

                    arParms[7] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[7].Value = objData.IsActive;

                    arParms[8] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[8].Value = objData.HospitalID;

                    arParms[9] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[9].Value = objData.FinancialYearID;

                    arParms[10] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[10].Value = objData.IPaddress;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateOPDEmergencyPayOutMST", arParms);
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
        public int UpdateOPDEmergency(OPDEmergencyPayOutData objData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

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

                    arParms[6] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[6].Value = objData.DepartmentID;

                    arParms[7] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[7].Value = objData.DoctorID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateOPDEmergencyPayOutMST", arParms);
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
        public int UpdateIPDDoctorRound(OPDEmergencyPayOutData objData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

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

                    arParms[6] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[6].Value = objData.DepartmentID;

                    arParms[7] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[7].Value = objData.DoctorID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateIPDDoctorRoundPayOutMST", arParms);
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

        public List<OPDEmergencyPayOutData> SearchOPDEmergencyPayOutDetails(OPDEmergencyPayOutData objData)
        {
            List<OPDEmergencyPayOutData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@GroupID", SqlDbType.Int);
                    arParms[0].Value = objData.GroupID;

                    arParms[1] = new SqlParameter("@ServiceID", SqlDbType.Int);
                    arParms[1].Value = objData.ServiceID;

                    arParms[2] = new SqlParameter("@DoctorTypeID", SqlDbType.Int);
                    arParms[2].Value = objData.DoctorTypeID;

                    arParms[3] = new SqlParameter("@DoctorID", SqlDbType.Int);
                    arParms[3].Value = objData.DoctorID;

                    arParms[4] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[4].Value = objData.EmployeeID;

                    arParms[5] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[5].Value = objData.FinancialYearID;

                    arParms[6] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[6].Value = objData.HospitalID;

                    arParms[7] = new SqlParameter("@SubGroupID", SqlDbType.Int);
                    arParms[7].Value = objData.SubGroupID;

                    arParms[8] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[8].Value = objData.DepartmentID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchOPDEmergencyPayOutDetails", arParms);
                    List<OPDEmergencyPayOutData> lstLabGroupTypeDetails = ORHelper<OPDEmergencyPayOutData>.FromDataReaderToList(sqlReader);
                    result = lstLabGroupTypeDetails;
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

        public List<OPDEmergencyPayOutData> SearchIPDRoundPayOutDetails(OPDEmergencyPayOutData objData)
        {
            List<OPDEmergencyPayOutData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@GroupID", SqlDbType.Int);
                    arParms[0].Value = objData.GroupID;

                    arParms[1] = new SqlParameter("@ServiceID", SqlDbType.Int);
                    arParms[1].Value = objData.ServiceID;

                    arParms[2] = new SqlParameter("@DoctorTypeID", SqlDbType.Int);
                    arParms[2].Value = objData.DoctorTypeID;

                    arParms[3] = new SqlParameter("@DoctorID", SqlDbType.Int);
                    arParms[3].Value = objData.DoctorID;

                    arParms[4] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[4].Value = objData.EmployeeID;

                    arParms[5] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[5].Value = objData.FinancialYearID;

                    arParms[6] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[6].Value = objData.HospitalID;

                    arParms[7] = new SqlParameter("@SubGroupID", SqlDbType.Int);
                    arParms[7].Value = objData.SubGroupID;

                    arParms[8] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[8].Value = objData.DepartmentID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchIPDRoundPayOutDetails", arParms);
                    List<OPDEmergencyPayOutData> lstLabGroupTypeDetails = ORHelper<OPDEmergencyPayOutData>.FromDataReaderToList(sqlReader);
                    result = lstLabGroupTypeDetails;
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
