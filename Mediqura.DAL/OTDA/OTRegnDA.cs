using Mediqura.CommonData.AdmissionData;
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
using Mediqura.CommonData.OTData;

namespace Mediqura.DAL.OTDA
{
    public class OTRegnDA
    {
        public List<OTRegnData> GetCase(OTRegnData objD)
        {
            List<OTRegnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@CaseName", SqlDbType.VarChar);
                    arParms[0].Value = objD.CaseName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetOTCasees", arParms);
                    List<OTRegnData> lstresult = ORHelper<OTRegnData>.FromDataReaderToList(sqlReader);
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
        public List<OTRegnData> GetNurseName(OTRegnData objD)
        {
            List<OTRegnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];
                    arParms[0] = new SqlParameter("@EmpName", SqlDbType.VarChar);
                    arParms[0].Value = objD.EmpName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_NurseName", arParms);
                    List<OTRegnData> lstresult = ORHelper<OTRegnData>.FromDataReaderToList(sqlReader);
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
        public int DeleteOT_RegnByID(OTRegnData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@OTRegnID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.OTRegnID;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[2].Value = objbill.HospitalID;

                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objbill.Remarks;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objbill.IPaddress;

                    arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.EmployeeID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_Delete_OtRegn_ByID", arParms);
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
        public int UpdateOTshare(OTRegnData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.VarChar);
                    arParms[0].Value = objbill.XMLData;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objbill.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateOTBreakUp", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[2].Value);
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
        public List<OTRegnData> UpdateOTRegnDetails(OTRegnData objotregd)
        {
            List<OTRegnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[12];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objotregd.IPNo;

                    arParms[1] = new SqlParameter("@OTpassnumber", SqlDbType.VarChar);
                    arParms[1].Value = objotregd.OTpassnumber;

                    arParms[2] = new SqlParameter("@OpernDate", SqlDbType.Date);
                    arParms[2].Value = objotregd.OpernDate;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[3].Value = objotregd.EmployeeID;

                    arParms[4] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[4].Value = objotregd.HospitalID;

                    arParms[5] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[5].Value = objotregd.IPaddress;

                    arParms[6] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[6].Value = objotregd.XMLData;

                    arParms[7] = new SqlParameter("@Proce_XMLData", SqlDbType.Xml);
                    arParms[7].Value = objotregd.Proce_XMLData;

                    arParms[8] = new SqlParameter("@Anas_XMLData", SqlDbType.Xml);
                    arParms[8].Value = objotregd.Anas_XMLData;

                    arParms[9] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[9].Value = objotregd.FinancialYearID;

                    arParms[10] = new SqlParameter("@OTtype", SqlDbType.Int);
                    arParms[10].Value = objotregd.OTtype;

                    arParms[11] = new SqlParameter("@OTnumber", SqlDbType.VarChar);
                    arParms[11].Value = objotregd.OTNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_OTRegistration", arParms);
                    List<OTRegnData> listOTdetails = ORHelper<OTRegnData>.FromDataReaderToList(sqlReader);
                    result = listOTdetails;
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
        public List<OTRegnData> GetOT_Registration(OTRegnData objpat)
        {
            List<OTRegnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objpat.IPNo;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objpat.PatientName;

                    arParms[2] = new SqlParameter("@CaseID", SqlDbType.Int);
                    arParms[2].Value = objpat.CaseID;

                    arParms[3] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[3].Value = objpat.OTemployeeID;

                    arParms[4] = new SqlParameter("@DoctorType", SqlDbType.BigInt);
                    arParms[4].Value = objpat.OTemployeeTypeID;

                    arParms[5] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[5].Value = objpat.DateFrom;

                    arParms[6] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[6].Value = objpat.DateTo;

                    arParms[7] = new SqlParameter("@OTstatus", SqlDbType.Int);
                    arParms[7].Value = objpat.OTstatus;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_OT_Regn", arParms);
                    List<OTRegnData> listOTdetails = ORHelper<OTRegnData>.FromDataReaderToList(sqlReader);
                    result = listOTdetails;
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

        public List<OTRegnData> GetOT_RegistrationList(OTRegnData objpat)
        {
            List<OTRegnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@OTpassnumber", SqlDbType.VarChar);
                    arParms[0].Value = objpat.OTpassnumber;

                    arParms[1] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[1].Value = objpat.IPNo;

                    arParms[2] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[2].Value = objpat.PatientName;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[3].Value = objpat.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[4].Value = objpat.DateTo;

                    arParms[5] = new SqlParameter("@OTemployeeID", SqlDbType.BigInt);
                    arParms[5].Value = objpat.OTemployeeID;

                    arParms[6] = new SqlParameter("@OTstatus", SqlDbType.Int);
                    arParms[6].Value = objpat.OTstatus;

                    arParms[7] = new SqlParameter("@OTtype", SqlDbType.Int);
                    arParms[7].Value = objpat.OTtype;

                    arParms[8] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[8].Value = objpat.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_Ot_registrationList", arParms);
                    List<OTRegnData> listOTdetails = ORHelper<OTRegnData>.FromDataReaderToList(sqlReader);
                    result = listOTdetails;
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
        public List<OTRegnData> GetOT_RegistrationListCustom(OTRegnData objpat)
        {
            List<OTRegnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@OTpassnumber", SqlDbType.VarChar);
                    arParms[0].Value = objpat.OTpassnumber;

                    arParms[1] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[1].Value = objpat.IPNo;

                    arParms[2] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[2].Value = objpat.PatientName;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[3].Value = objpat.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[4].Value = objpat.DateTo;

                    arParms[5] = new SqlParameter("@OTemployeeID", SqlDbType.BigInt);
                    arParms[5].Value = objpat.OTemployeeID;

                    arParms[6] = new SqlParameter("@OTstatus", SqlDbType.Int);
                    arParms[6].Value = objpat.OTstatus;

                    arParms[7] = new SqlParameter("@OTtype", SqlDbType.Int);
                    arParms[7].Value = objpat.OTtype;

                    arParms[8] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[8].Value = objpat.IsActive;

                    arParms[9] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[9].Value = objpat.CurrentIndex;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_Ot_registrationListCustom", arParms);
                    List<OTRegnData> listOTdetails = ORHelper<OTRegnData>.FromDataReaderToList(sqlReader);
                    result = listOTdetails;
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
        public List<OTRegnData> GetOT_RegistrationListReport(OTRegnData objpat)
        {
            List<OTRegnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[0].Value = objpat.DateFrom;

                    arParms[1] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[1].Value = objpat.DateTo;

                    arParms[2] = new SqlParameter("@OTemployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objpat.OTemployeeID;

                    arParms[3] = new SqlParameter("@OTtype", SqlDbType.Int);
                    arParms[3].Value = objpat.OTtype;

                    arParms[4] = new SqlParameter("@CaseID", SqlDbType.Int);
                    arParms[4].Value = objpat.CaseID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_Ot_registrationListReport", arParms);
                    List<OTRegnData> listOTdetails = ORHelper<OTRegnData>.FromDataReaderToList(sqlReader);
                    result = listOTdetails;
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
        public List<OTRegnData> GetOT_CompletedOtlist(OTRegnData objpat)
        {
            List<OTRegnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[0].Value = objpat.DateFrom;

                    arParms[1] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[1].Value = objpat.DateTo;

                    arParms[2] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[2].Value = objpat.OTemployeeID;

                    arParms[3] = new SqlParameter("@OTNo", SqlDbType.VarChar);
                    arParms[3].Value = objpat.OTNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_OT_Completd_OTlist", arParms);
                    List<OTRegnData> listOTdetails = ORHelper<OTRegnData>.FromDataReaderToList(sqlReader);
                    result = listOTdetails;
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
        public List<OTRegnData> GetCompletedOtdetailByID(OTRegnData objpat)
        {
            List<OTRegnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@OTnumber", SqlDbType.VarChar);
                    arParms[0].Value = objpat.OTNo;

                    arParms[1] = new SqlParameter("@CaseID", SqlDbType.Int);
                    arParms[1].Value = objpat.CaseID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Completed_OT_Details", arParms);
                    List<OTRegnData> listOTdetails = ORHelper<OTRegnData>.FromDataReaderToList(sqlReader);
                    result = listOTdetails;
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
        public List<OTRegnData> Edit_RegistrationList(OTRegnData objpat)
        {
            List<OTRegnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@OTNo", SqlDbType.VarChar);
                    arParms[0].Value = objpat.OTNo;

                    arParms[1] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[1].Value = objpat.IPNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Edit_Otdetails", arParms);
                    List<OTRegnData> listOTdetails = ORHelper<OTRegnData>.FromDataReaderToList(sqlReader);
                    result = listOTdetails;
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
        public List<OTRegnData> Edit_Otroledetails(OTRegnData objpat)
        {
            List<OTRegnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@OTNo", SqlDbType.VarChar);
                    arParms[0].Value = objpat.OTNo;

                    arParms[1] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[1].Value = objpat.IPNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Edit_OtRolesdetails", arParms);
                    List<OTRegnData> listOTdetails = ORHelper<OTRegnData>.FromDataReaderToList(sqlReader);
                    result = listOTdetails;
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
        public List<OTRegnData> Edit_OtProceduredetails(OTRegnData objpat)
        {
            List<OTRegnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@OTNo", SqlDbType.VarChar);
                    arParms[0].Value = objpat.OTNo;

                    arParms[1] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[1].Value = objpat.IPNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Edit_Otproceduredetails", arParms);
                    List<OTRegnData> listOTdetails = ORHelper<OTRegnData>.FromDataReaderToList(sqlReader);
                    result = listOTdetails;
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
        public List<OTRegnData> Edit_Otanasthesiadetails(OTRegnData objpat)
        {
            List<OTRegnData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@OTNo", SqlDbType.VarChar);
                    arParms[0].Value = objpat.OTNo;

                    arParms[1] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[1].Value = objpat.IPNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Edit_OtAnasthesiadetails", arParms);
                    List<OTRegnData> listOTdetails = ORHelper<OTRegnData>.FromDataReaderToList(sqlReader);
                    result = listOTdetails;
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
        public int DeleteOTByID(OTRegnData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@OTNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.OTNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[2].Value = objbill.HospitalID;

                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objbill.Remarks;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objbill.IPaddress;

                    arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.EmployeeID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteOTDetailsByID", arParms);
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
        public int UpdateOtstatus(OTRegnData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@OTNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.OTNo;

                    arParms[1] = new SqlParameter("@OTRegnID", SqlDbType.BigInt);
                    arParms[1].Value = objbill.OTRegnID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[3].Value = objbill.HospitalID;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objbill.EmployeeID;

                    arParms[5] = new SqlParameter("@Status", SqlDbType.Int);
                    arParms[5].Value = objbill.OT_status;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateOtStatus", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[2].Value);
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
    }
}
