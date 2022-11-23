using Mediqura.CommonData.MedUtilityData;
using Mediqura.CommonData.PatientData;
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

namespace Mediqura.DAL.PatientDA
{
    public class RegistrationDA
    {
        public int UpdatePatientRegistration(PatientData objpat)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[34];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objpat.ID;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.VarChar);
                    arParms[1].Value = objpat.UHID;

                    arParms[2] = new SqlParameter("@SalutationID", SqlDbType.Int);
                    arParms[2].Value = objpat.SalutationID;

                    arParms[3] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[3].Value = objpat.PatientName;

                    arParms[4] = new SqlParameter("@PatRelatives", SqlDbType.VarChar);
                    arParms[4].Value = objpat.PatRelatives;

                    arParms[5] = new SqlParameter("@RelationshipID", SqlDbType.Int);
                    arParms[5].Value = objpat.RelationshipID;

                    arParms[6] = new SqlParameter("@NationalityID", SqlDbType.Int);
                    arParms[6].Value = objpat.NationalityID;

                    arParms[7] = new SqlParameter("@ReligionID", SqlDbType.Int);
                    arParms[7].Value = objpat.ReligionID;

                    arParms[8] = new SqlParameter("@GenderID", SqlDbType.Int);
                    arParms[8].Value = objpat.GenderID;

                    arParms[9] = new SqlParameter("@Age", SqlDbType.Int);
                    arParms[9].Value = objpat.Age;

                    arParms[10] = new SqlParameter("@Month", SqlDbType.Int);
                    arParms[10].Value = objpat.Month;

                    arParms[11] = new SqlParameter("@Days", SqlDbType.Int);
                    arParms[11].Value = objpat.Days;

                    arParms[12] = new SqlParameter("@MaritalStatusID", SqlDbType.Int);
                    arParms[12].Value = objpat.MaritalStatusID;

                    arParms[13] = new SqlParameter("@DOB", SqlDbType.DateTime);
                    arParms[13].Value = objpat.DOB;

                    arParms[14] = new SqlParameter("@CountryID", SqlDbType.Int);
                    arParms[14].Value = objpat.CountryID;

                    arParms[15] = new SqlParameter("@StateID", SqlDbType.Int);
                    arParms[15].Value = objpat.StateID;

                    arParms[16] = new SqlParameter("@DistrictID", SqlDbType.Int);
                    arParms[16].Value = objpat.DistrictID;

                    arParms[17] = new SqlParameter("@Pin", SqlDbType.Int);
                    arParms[17].Value = objpat.Pin;

                    arParms[18] = new SqlParameter("@Address", SqlDbType.VarChar);
                    arParms[18].Value = objpat.Address;

                    arParms[19] = new SqlParameter("@ProfessionID", SqlDbType.Int);
                    arParms[19].Value = objpat.ProfessionID;

                    arParms[20] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[20].Value = objpat.PatientType;

                    arParms[21] = new SqlParameter("@EmailID", SqlDbType.VarChar);
                    arParms[21].Value = objpat.EmailID;

                    arParms[22] = new SqlParameter("@ContactNo", SqlDbType.VarChar);
                    arParms[22].Value = objpat.ContactNo;

                    arParms[23] = new SqlParameter("@Output", SqlDbType.BigInt);
                    arParms[23].Direction = ParameterDirection.Output;

                    arParms[24] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[24].Value = objpat.EmployeeID;

                    arParms[25] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[25].Value = objpat.HospitalID;

                    arParms[26] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[26].Value = objpat.FinancialYearID;

                    arParms[27] = new SqlParameter("@ActionType", SqlDbType.SmallInt);
                    arParms[27].Value = objpat.ActionType;

                    arParms[28] = new SqlParameter("@IDmark", SqlDbType.VarChar);
                    arParms[28].Value = objpat.IDmark;

                    arParms[29] = new SqlParameter("@FPscan", SqlDbType.VarChar);
                    arParms[29].Value = objpat.FPtemplate;

                    arParms[30] = new SqlParameter("@AadhaarNumber", SqlDbType.VarChar);
                    arParms[30].Value = objpat.AadhaarNumber;

                    arParms[31] = new SqlParameter("@TPAcompany", SqlDbType.Int);
                    arParms[31].Value = objpat.TPAcompany;

                    arParms[32] = new SqlParameter("@ReferBy", SqlDbType.VarChar);
                    arParms[32].Value = objpat.ReferBy;

                    arParms[33] = new SqlParameter("@BloodGrp", SqlDbType.Int);
                    arParms[33].Value = objpat.BloodGrp;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdatePatientRegistration", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[23].Value);
                    }
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
        public int UpdatePatientAge(int Age, int Month, int Day, Int64 UHID)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@Age", SqlDbType.Int);
                    arParms[0].Value = Age;

                    arParms[1] = new SqlParameter("@Month", SqlDbType.Int);
                    arParms[1].Value = Month;

                    arParms[2] = new SqlParameter("@Day", SqlDbType.Int);
                    arParms[2].Value = Day;

                    arParms[3] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[3].Value = UHID;

                    arParms[4] = new SqlParameter("@Output", SqlDbType.Int);
                    arParms[4].Direction = ParameterDirection.Output;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_CurrentPatientAge", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[4].Value);
                    }
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
        public List<PatientAgeData> GetpatientDOB(PatientAgeData objpat)
        {
            List<PatientAgeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.UHID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_patientDOB", arParms);
                    List<PatientAgeData> listpatientdetails = ORHelper<PatientAgeData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<ReferalData> GetReferalDetails(ReferalData objD)
        {
            List<ReferalData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@Referal", SqlDbType.VarChar);
                    arParms[0].Value = objD.Referal;

                    arParms[1] = new SqlParameter("@SourceID", SqlDbType.Int);
                    arParms[1].Value = objD.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetReferals", arParms);
                    List<ReferalData> lstresult = ORHelper<ReferalData>.FromDataReaderToList(sqlReader);
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
        public List<ReferalData> GetPayableReferalDetails(ReferalData objD)
        {
            List<ReferalData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@Referal", SqlDbType.VarChar);
                    arParms[0].Value = objD.Referal;

                    arParms[1] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[1].Value = objD.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetDoctorByDoctorType", arParms);
                    List<ReferalData> lstresult = ORHelper<ReferalData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetRadioUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objD.UHID;

                    arParms[1] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[1].Value = objD.PatientType;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetRadiologyUHID", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public int InsertBookingdetails(BookingData objpat)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[16];

                    arParms[0] = new SqlParameter("@DoctorTypeID", SqlDbType.Int);
                    arParms[0].Value = objpat.DoctorTypeID;

                    arParms[1] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[1].Value = objpat.DepartmentID;

                    arParms[2] = new SqlParameter("@DoctorID", SqlDbType.Int);
                    arParms[2].Value = objpat.DoctorID;

                    arParms[3] = new SqlParameter("@BookingDate", SqlDbType.Date);
                    arParms[3].Value = objpat.BookingDate;


                    arParms[4] = new SqlParameter("@Address", SqlDbType.VarChar);
                    arParms[4].Value = objpat.Address;

                    arParms[5] = new SqlParameter("@ContactNo", SqlDbType.VarChar);
                    arParms[5].Value = objpat.ContactNo;

                    arParms[6] = new SqlParameter("@Age", SqlDbType.Int);
                    arParms[6].Value = objpat.Age;

                    arParms[7] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[7].Value = objpat.Remarks;

                    arParms[8] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[8].Direction = ParameterDirection.Output;

                    arParms[9] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[9].Value = objpat.EmployeeID;

                    arParms[10] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[10].Value = objpat.HospitalID;

                    arParms[11] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[11].Value = objpat.FinancialYearID;

                    arParms[12] = new SqlParameter("@Patientname", SqlDbType.VarChar);
                    arParms[12].Value = objpat.PatientName;

                    arParms[13] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[13].Value = objpat.IPaddress;

                    arParms[14] = new SqlParameter("@Time", SqlDbType.VarChar);
                    arParms[14].Value = objpat.Time;

                    arParms[15] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[15].Value = objpat.ID;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateBookingDetails", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[8].Value);
                    }
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
        public int CancelAppointment(PatientData objpat)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.ID;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@UserId", SqlDbType.BigInt);
                    arParms[2].Value = objpat.EmployeeID;
                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_CancelAppointment", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[1].Value);
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
        public List<PatientData> GetAdvncUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.VarChar);
                    arParms[0].Value = objD.UHID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetAdvncUHID", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public int DeleteDependent(PatientData objpat)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.ID;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@UserId", SqlDbType.BigInt);
                    arParms[2].Value = objpat.EmployeeID;
                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteEmployeeDependent", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[1].Value);
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
        public int DeleteEmployeeDiscount(PatientData objpat)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.ID;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@UserId", SqlDbType.BigInt);
                    arParms[2].Value = objpat.EmployeeID;
                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteEmployeeDiscount", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[1].Value);
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

        public int AddPatientConsultantSheetDA(PatientData objpat)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[13];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.UHID;

                    arParms[1] = new SqlParameter("@Height", SqlDbType.Float);
                    arParms[1].Value = objpat.Height;

                    arParms[2] = new SqlParameter("@Weight", SqlDbType.Float);
                    arParms[2].Value = objpat.Weight;

                    arParms[3] = new SqlParameter("@PulseRate", SqlDbType.Int);
                    arParms[3].Value = objpat.PulseRate;

                    arParms[4] = new SqlParameter("@BP", SqlDbType.VarChar);
                    arParms[4].Value = objpat.BP;

                    arParms[5] = new SqlParameter("@DoctorTypeID", SqlDbType.Int);
                    arParms[5].Value = objpat.DoctorTypeID;

                    arParms[6] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[6].Value = objpat.DepartmentID;

                    arParms[7] = new SqlParameter("@DoctorID", SqlDbType.Int);
                    arParms[7].Value = objpat.DoctorID;

                    arParms[8] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[8].Value = objpat.Remarks;

                    arParms[9] = new SqlParameter("@Output", SqlDbType.BigInt);
                    arParms[9].Direction = ParameterDirection.Output;

                    arParms[10] = new SqlParameter("@UserLoginID", SqlDbType.BigInt);
                    arParms[10].Value = objpat.EmployeeID;

                    arParms[11] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[11].Value = objpat.HospitalID;

                    arParms[12] = new SqlParameter("@FinancialyearID", SqlDbType.VarChar);
                    arParms[12].Value = objpat.FinancialYearID;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_AddConsultingSheet", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[9].Value);
                    }
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
        public List<PatientData> GetOTTeamList(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetOTTeamList", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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

        public List<PatientData> GetUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objD.UHID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetUHD", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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

        public List<PatientData> GetPatientNameWithUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientNammeWithUHID", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetAvancedepositpatientdetail(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@PatientDetailName", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientDetailName;

                    arParms[1] = new SqlParameter("@DepositType", SqlDbType.Int);
                    arParms[1].Value = objD.DepositType;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDepositPatientDetails", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetPhrAvancedepositpatientdetail(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@PatientDetailName", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientDetailName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_PHR_DepositPatientDetails", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetPatientNumberByBillCategory(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@PatientNumber", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientNumber;

                    arParms[1] = new SqlParameter("@BillCategory", SqlDbType.Int);
                    arParms[1].Value = objD.BillCategory;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientNumberBybillCategory", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetOPDNumber(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@OPnumber", SqlDbType.VarChar);
                    arParms[0].Value = objD.OPnumber;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetOPnumber", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetEmgPatientName(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@EmgPatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetAllEmgPatientName", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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

        public List<PatientData> GetAutoUHIDIPNo(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetautoUHIDIPNo", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetDetailUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];
                    arParms[0] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientDetailswithUHID", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetDetailUHIDwithOpnumber(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientName;

                    arParms[1] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[1].Value = objD.PatientType;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientDetailByUHDwithOPnumber", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetLabUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objD.UHID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabUHID", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetLabDeviceUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objD.UHID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabDeviceUHID", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetLabDevicecompletedUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objD.UHID;

                    arParms[1] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[1].Value = objD.PatientType;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabDeviceCompletdUHID", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetIPDLabIPNo(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIPDIPNo", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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


        public List<PatientData> GetLabIPNo(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabIPNo", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetLabDevicecompletedIPNo(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;

                    arParms[1] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[1].Value = objD.PatientType;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabDeviceCompletdipno", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetPatientOTDetailsByIPNo(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientOTDetailsByIPNo", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetPhrPatientDetailsByUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objD.UHID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPhrPatientDetailsByUHD", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetPatientDetailsByUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objD.UHID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientDetailsByUHD", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetPatientDepositDetailByUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objD.UHID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientDepositDetailUHD", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetPatientDetailsByUHIDwithdeposittype(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objD.UHID;

                    arParms[1] = new SqlParameter("@DepositType", SqlDbType.Int);
                    arParms[1].Value = objD.DepositType;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDepositPatientDetailsByUHID_Deposittype", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetPatientDetailsByUHIDandOpnumber(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objD.UHID;

                    arParms[1] = new SqlParameter("@OPnumber", SqlDbType.VarChar);
                    arParms[1].Value = objD.OPnumber;

                    arParms[2] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[2].Value = objD.PatientType;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientDetailsByUHDandOPnumber", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<OPDdata> GetPatientNumber(OPDdata objD)
        {
            List<OPDdata> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objD.UHID;

                    arParms[1] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[1].Value = objD.PatientTypeID;

                    arParms[2] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[2].Value = objD.DoctorID;

                    arParms[3] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[3].Value = objD.FinancialYearID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientNumber", arParms);
                    List<OPDdata> lstresult = ORHelper<OPDdata>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetLabDeviceInitiatedDetailsByUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objD.UHID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabDeviceInitiatedDetailsByUHID", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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

        public List<PatientData> GetLabPatientDetailsByUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objD.UHID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabPatientDetailsByUHD", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetLabSampleCollectedDetailsByUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objD.UHID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabSampleCollectedDetailsByUHID", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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

        public List<PatientData> GetPatientAdmissionDetailsByUHIDIPNo(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientAdmissionDetailsByUHDIPNo", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetPatientAdmissionDetailsByUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objD.UHID;

                    arParms[1] = new SqlParameter("@EmrgNo", SqlDbType.VarChar);
                    arParms[1].Value = objD.EmrgNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientAdmissionDetailsByUHD", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetPatientDetailByOPnumber(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@OPnumber", SqlDbType.VarChar);
                    arParms[0].Value = objD.OPnumber;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientDetailsByOPnumber", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetPatientName(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientNamme", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetIPpatientName(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIPpatientNamme", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetPatientNameIP(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientNameIP", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetDuePatientName(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientName;

                    arParms[1] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[1].Value = objD.PatientType;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DuePatientName", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetDueBill(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.BillNo;

                    arParms[1] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[1].Value = objD.PatientType;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DuePatientBill", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetotPatientName(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetOTPatientNamme", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetDiscountedPatientName(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDiscountedPatientName", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<OTSchedulingData> GetDoctorNameWithID(OTSchedulingData objD)
        {
            List<OTSchedulingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@DoctorName", SqlDbType.VarChar);
                    arParms[0].Value = objD.Surgeon;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDoctorWithID", arParms);
                    List<OTSchedulingData> lstresult = ORHelper<OTSchedulingData>.FromDataReaderToList(sqlReader);
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

        public List<PatientData> GetDoctorName(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@DoctorName", SqlDbType.VarChar);
                    arParms[0].Value = objD.DoctorName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDoctorName", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<ICDData> GetICDCode(ICDData objD)
        {
            List<ICDData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ICDCode", SqlDbType.VarChar);
                    arParms[0].Value = objD.ICDCode;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetICDCode", arParms);
                    List<ICDData> lstresult = ORHelper<ICDData>.FromDataReaderToList(sqlReader);
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

        public List<PatientData> GetContactno(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ContactNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.ContactNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetContactNos", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> SearchPatientDetails(PatientData objpat)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.UHID;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objpat.PatientName;

                    arParms[2] = new SqlParameter("@ContactNo", SqlDbType.VarChar);
                    arParms[2].Value = objpat.ContactNo;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objpat.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objpat.DateTo;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objpat.IsActive;

                    arParms[6] = new SqlParameter("@userID", SqlDbType.Int);
                    arParms[6].Value = objpat.userID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientDetails", arParms);
                    List<PatientData> listpatientdetails = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<PatientData> SearchPatientList(PatientData objpat)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.UHID;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objpat.PatientName;

                    arParms[2] = new SqlParameter("@ContactNo", SqlDbType.VarChar);
                    arParms[2].Value = objpat.ContactNo;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objpat.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objpat.DateTo;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objpat.IsActive;

                    arParms[6] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[6].Value = objpat.CurrentIndex;

                    arParms[7] = new SqlParameter("@userID", SqlDbType.Int);
                    arParms[7].Value = objpat.userID;

                    arParms[8] = new SqlParameter("@pageSize", SqlDbType.Int);
                    arParms[8].Value = objpat.PageSize;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientDetailsLIST", arParms);
                    List<PatientData> listpatientdetails = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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


        public List<BookingData> GetPatientList(BookingData objpat)
        {
            List<BookingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@Consultant", SqlDbType.BigInt);
                    arParms[0].Value = objpat.DoctorID;

                    arParms[1] = new SqlParameter("@BookingDate", SqlDbType.DateTime);
                    arParms[1].Value = objpat.BookingDate;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientList", arParms);
                    List<BookingData> listpatientdetails = ORHelper<BookingData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<BookingData> GetDocWisePatientList(BookingData objpat)
        {
            List<BookingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@Consultant", SqlDbType.BigInt);
                    arParms[0].Value = objpat.DoctorID;

                    arParms[1] = new SqlParameter("@BookingDate", SqlDbType.DateTime);
                    arParms[1].Value = objpat.BookingDate;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDoctorwisePatient", arParms);
                    List<BookingData> listpatientdetails = ORHelper<BookingData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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

        public List<PatientData> Getpatientlastvisitdetails(PatientData objpat)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.UHID;

                    arParms[1] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[1].Value = objpat.DepartmentID;

                    arParms[2] = new SqlParameter("@DoctorID", SqlDbType.Int);
                    arParms[2].Value = objpat.DoctorID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_LastVisitDetails", arParms);
                    List<PatientData> listpatientdetails = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<PatientData> Getconsultingsheetlist(PatientData objOPD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objOPD.UHID;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objOPD.PatientName;

                    arParms[2] = new SqlParameter("@DoctorTypeID", SqlDbType.Int);
                    arParms[2].Value = objOPD.DoctorTypeID;

                    arParms[3] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[3].Value = objOPD.DepartmentID;

                    arParms[4] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[4].Value = objOPD.DoctorID;

                    arParms[5] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[5].Value = objOPD.DateFrom;

                    arParms[6] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[6].Value = objOPD.DateTo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_OpdVisitHistory", arParms);
                    List<PatientData> listpatientdetails = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<PatientData> OPDVisitHistorydata(PatientData objOPD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objOPD.UHID;

                    arParms[1] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[1].Value = objOPD.DepartmentID;

                    arParms[2] = new SqlParameter("@DoctorID", SqlDbType.Int);
                    arParms[2].Value = objOPD.DoctorID;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objOPD.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objOPD.DateTo;

                    arParms[5] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[5].Value = objOPD.CurrentIndex;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_OpdVisitHistory", arParms);
                    List<PatientData> listpatientdetails = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<PatientData> OPDVisitHistoryDA(PatientData objOPD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objOPD.UHID;

                    arParms[1] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[1].Value = objOPD.DepartmentID;

                    arParms[2] = new SqlParameter("@DoctorID", SqlDbType.Int);
                    arParms[2].Value = objOPD.DoctorID;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objOPD.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objOPD.DateTo;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_OpdVisitHistoryExcel", arParms);
                    List<PatientData> listpatientdetails = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public int DeletePatientDetailsByID(PatientData objpat)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.ID;
                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;
                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objpat.Remarks;
                    arParms[3] = new SqlParameter("@UserId", SqlDbType.BigInt);
                    arParms[3].Value = objpat.EmployeeID;
                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeletePatientbyID", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[1].Value);
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
        public List<PatientData> GetPtientDeatilbyID(PatientData objpat)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientDetailsByID", arParms);
                    List<PatientData> listpatientdetails = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<PatientData> GetPatientDetailsByIPNo(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientDetailsByIPNo", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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

        public List<PatientData> GetAdmittedPatientName(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetAdmittedPatientName", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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


        public List<PatientData> GetotPatientDetailsByIPNo(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetotPatientDetailsByIPNo", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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

        public List<PatientData> GetPatientDetailsByOtpassnumber(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@OTpassnumber", SqlDbType.VarChar);
                    arParms[0].Value = objD.OTpassnumber;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientDetailsByOTpassnumber", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetIpdNestedData(PatientData objpat)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNO", SqlDbType.NVarChar);
                    arParms[0].Value = objpat.IPNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_IpdNestedGridDA", arParms);
                    List<PatientData> listpatientdetails = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<PatientData> IpdPatientEnquiryDA(PatientData objOPD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objOPD.IPNo;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[1].Value = objOPD.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[2].Value = objOPD.DateTo;



                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_IpdPatientEnquiry", arParms);
                    List<PatientData> listpatientdetails = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<PatientData> OPDTreatmentStatusDA(PatientData objOPD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objOPD.UHID;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objOPD.PatientName;

                    arParms[2] = new SqlParameter("@Docname", SqlDbType.VarChar);
                    arParms[2].Value = objOPD.DoctorName;

                    arParms[3] = new SqlParameter("@Date", SqlDbType.DateTime);
                    arParms[3].Value = objOPD.TodayDate;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_OpdTreatment", arParms);
                    List<PatientData> listpatientdetails = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public int UpdateTreatmentStatusDA(PatientData objpat)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.Int);
                    arParms[0].Value = objpat.UHID;

                    arParms[1] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[1].Value = objpat.ID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateTreatmentStatus", arParms);
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
        public List<PatientData> GetPatientDetailsByBillNo(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.BillNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientDetailsByBillNo_for_adjustment", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
        public List<PatientData> GetPatientNameforOTScheduling(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientNameforOTSchedule", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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

        public List<OTSchedulingData> GetDoctorNameForOTScheduling(OTSchedulingData objD)
        {
            List<OTSchedulingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@DoctorName", SqlDbType.VarChar);
                    arParms[0].Value = objD.Surgeon;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDoctorForOTScheduling", arParms);
                    List<OTSchedulingData> lstresult = ORHelper<OTSchedulingData>.FromDataReaderToList(sqlReader);
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

        public List<PatientData> GetRadioIPNo(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;

                    arParms[1] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[1].Value = objD.PatientType;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetRadioipno", arParms);
                    List<PatientData> lstresult = ORHelper<PatientData>.FromDataReaderToList(sqlReader);
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
    }
}
