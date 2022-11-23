using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.MedEmergencyData;
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

namespace Mediqura.DAL.AdmissionDA
{
    public class IPServiceRecordDA
    {
        public List<IPServiceRecordData> getIPNo(IPServiceRecordData objD)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIPNoWithDetails", arParms);
                    List<IPServiceRecordData> lstresult = ORHelper<IPServiceRecordData>.FromDataReaderToList(sqlReader);
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
        public List<IPServiceRecordData> getOTIPNo(IPServiceRecordData objD)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_OT_IPNo", arParms);
                    List<IPServiceRecordData> lstresult = ORHelper<IPServiceRecordData>.FromDataReaderToList(sqlReader);
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
        public List<IPServiceRecordData> GetOTpassnumber(IPServiceRecordData objD)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@Otpassnumber", SqlDbType.VarChar);
                    arParms[0].Value = objD.Otpassnumber;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetOtpassnumber", arParms);
                    List<IPServiceRecordData> lstresult = ORHelper<IPServiceRecordData>.FromDataReaderToList(sqlReader);
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
        public List<IPServiceRecordData> Getipservicenumber(IPServiceRecordData objD)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;

                    arParms[1] = new SqlParameter("@ServiceNumber", SqlDbType.VarChar);
                    arParms[1].Value = objD.ServiceNumber;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_IPServicenumber", arParms);
                    List<IPServiceRecordData> lstresult = ORHelper<IPServiceRecordData>.FromDataReaderToList(sqlReader);
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
        public List<IPServiceRecordData> GetIpinvnumber(IPServiceRecordData objD)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;

                    arParms[1] = new SqlParameter("@InvNumber", SqlDbType.VarChar);
                    arParms[1].Value = objD.InvNumber;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_IPInvnumber", arParms);
                    List<IPServiceRecordData> lstresult = ORHelper<IPServiceRecordData>.FromDataReaderToList(sqlReader);
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
        public List<IPServiceRecordData> getOTRegisteredIPNo(IPServiceRecordData objD)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_getOTRegisteredIPNo", arParms);
                    List<IPServiceRecordData> lstresult = ORHelper<IPServiceRecordData>.FromDataReaderToList(sqlReader);
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
        public List<IPServiceRecordData> GetDepartmentByIPNo(IPServiceRecordData objD)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeptByIPNo", arParms);
                    List<IPServiceRecordData> lstresult = ORHelper<IPServiceRecordData>.FromDataReaderToList(sqlReader);
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
        public List<IPServiceRecordData> UpdateIPServiceRecord(IPServiceRecordData objbill)
        {
            List<IPServiceRecordData> result;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objbill.XMLData;

                    arParms[1] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[1].Value = objbill.IPNo;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[2].Value = objbill.HospitalID;

                    arParms[3] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[3].Value = objbill.FinancialYearID;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objbill.EmployeeID;

                    arParms[5] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.UHID;

                    arParms[6] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[6].Value = objbill.IPaddress;

                    arParms[7] = new SqlParameter("@ServiceCategory", SqlDbType.Int);
                    arParms[7].Value = objbill.ServiceCategoryID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_IPServiceRecordList", arParms);
                    List<IPServiceRecordData> listpatientdetails = ORHelper<IPServiceRecordData>.FromDataReaderToList(sqlReader);
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
        public int UpdateOTServiceRecord(IPServiceRecordData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objbill.XMLData;

                    arParms[1] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[1].Value = objbill.IPNo;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[2].Value = objbill.HospitalID;

                    arParms[3] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[3].Value = objbill.FinancialYearID;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objbill.EmployeeID;

                    arParms[5] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[5].Direction = ParameterDirection.Output;

                    arParms[6] = new SqlParameter("@RefferalDoctorID", SqlDbType.BigInt);
                    arParms[6].Value = objbill.RefferaDocID;

                    arParms[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[7].Value = objbill.IPaddress;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_OTServiceRecordList", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[5].Value);
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
        public List<IPServiceRecordData> UpdateIPLabServiceRecord(IPServiceRecordData objbill)
        {
            List<IPServiceRecordData> result;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objbill.XMLData;

                    arParms[1] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[1].Value = objbill.IPNo;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[2].Value = objbill.HospitalID;

                    arParms[3] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[3].Value = objbill.FinancialYearID;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objbill.EmployeeID;

                    arParms[5] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[5].Value = objbill.IPaddress;

                    arParms[6] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[6].Value = objbill.ServiceTypeID;

                    arParms[7] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[7].Value = objbill.Remarks;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_IPLabServiceRecordList", arParms);
                    List<IPServiceRecordData> listpatientdetails = ORHelper<IPServiceRecordData>.FromDataReaderToList(sqlReader);
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
        public List<IPServiceRecordData> GetIPDServiceList(IPServiceRecordData objbill)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.IPNo;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objbill.PatientName;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateTo;

                    arParms[4] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[4].Value = objbill.IsActive;

                    arParms[5] = new SqlParameter("@ServiceCategoryID", SqlDbType.Int);
                    arParms[5].Value = objbill.ServiceCategoryID;

                    arParms[6] = new SqlParameter("@ServiceNumber", SqlDbType.VarChar);
                    arParms[6].Value = objbill.ServiceNumber;

                    arParms[7] = new SqlParameter("@InvNumber", SqlDbType.VarChar);
                    arParms[7].Value = objbill.InvNumber;

                    arParms[8] = new SqlParameter("@AmountEnable", SqlDbType.Int);
                    arParms[8].Value = objbill.AmountEnable;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIPDServiceRecordList", arParms);
                    List<IPServiceRecordData> listpatientdetails = ORHelper<IPServiceRecordData>.FromDataReaderToList(sqlReader);
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
        public List<IPServiceRecordData> GetIPDLabServiceList(IPServiceRecordData objbill)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.IPNo;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objbill.PatientName;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateTo;

                    arParms[4] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[4].Value = objbill.IsActive;

                    arParms[5] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[5].Value = objbill.ServiceTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIPDLabServiceRecordList", arParms);
                    List<IPServiceRecordData> listpatientdetails = ORHelper<IPServiceRecordData>.FromDataReaderToList(sqlReader);
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
        public int DeleteIPDServiceRecordByIPNo(IPServiceRecordData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[12];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.IPNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@UserId", SqlDbType.BigInt);
                    arParms[3].Value = objbill.EmployeeID;

                    arParms[4] = new SqlParameter("@Charges", SqlDbType.Money);
                    arParms[4].Value = objbill.ServiceCharge;

                    arParms[5] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.UHID;

                    arParms[6] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[6].Value = objbill.HospitalID;

                    arParms[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[7].Value = objbill.IPaddress;

                    arParms[8] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[8].Value = objbill.EmployeeID;

                    arParms[9] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[9].Value = objbill.ID;

                    arParms[10] = new SqlParameter("@InvNumber", SqlDbType.VarChar);
                    arParms[10].Value = objbill.InvNumber;

                    arParms[11] = new SqlParameter("@ServiceNumber", SqlDbType.VarChar);
                    arParms[11].Value = objbill.ServiceNumber;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteIPDServiceRecordbyIPNo", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[1].Value);
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
        public int DeleteIPDLabServiceRecordByIPNo(IPServiceRecordData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[11];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.IPNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@UserId", SqlDbType.BigInt);
                    arParms[3].Value = objbill.EmployeeID;

                    arParms[4] = new SqlParameter("@Charges", SqlDbType.Money);
                    arParms[4].Value = objbill.ServiceCharge;

                    arParms[5] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.UHID;

                    arParms[6] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[6].Value = objbill.HospitalID;

                    arParms[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[7].Value = objbill.IPaddress;

                    arParms[8] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[8].Value = objbill.EmployeeID;

                    arParms[9] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[9].Value = objbill.ID;

                    arParms[10] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[10].Value = objbill.ServiceTypeID;



                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteIPDLabServiceRecordbyIPNo", arParms);
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
        public int UpdateIPIssueRecord(IPServiceRecordData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objbill.XMLData;

                    arParms[1] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[1].Value = objbill.IPNo;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[2].Value = objbill.HospitalID;

                    arParms[3] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[3].Value = objbill.FinancialYearID;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objbill.EmployeeID;

                    arParms[5] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[5].Direction = ParameterDirection.Output;

                    arParms[6] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[6].Value = objbill.IPaddress;

                    arParms[7] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[7].Value = objbill.ServiceTypeID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_IPIssueRecordList", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[5].Value);
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
        public int DeleteIPDIssueRecordByIPNo(IPServiceRecordData objstd)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[14];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objstd.IPNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objstd.Remarks;

                    arParms[3] = new SqlParameter("@UserId", SqlDbType.BigInt);
                    arParms[3].Value = objstd.EmployeeID;

                    arParms[4] = new SqlParameter("@Charges", SqlDbType.Money);
                    arParms[4].Value = objstd.ServiceCharge;

                    arParms[5] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[5].Value = objstd.UHID;

                    arParms[6] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[6].Value = objstd.HospitalID;

                    arParms[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[7].Value = objstd.IPaddress;

                    arParms[8] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[8].Value = objstd.EmployeeID;

                    arParms[9] = new SqlParameter("@SerialID", SqlDbType.BigInt);
                    arParms[9].Value = objstd.SerialID;

                    arParms[10] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[10].Value = objstd.ID;

                    arParms[11] = new SqlParameter("@SubStockID", SqlDbType.BigInt);
                    arParms[11].Value = objstd.SubStockID;

                    arParms[12] = new SqlParameter("@Quantity", SqlDbType.Int);
                    arParms[12].Value = objstd.Quantity;

                    arParms[13] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[13].Value = objstd.ServiceTypeID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteIPDIssueRecordbyIPNo", arParms);
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
        public List<IPServiceRecordData> GetOPServiceList(IPServiceRecordData objbill)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.IPNo;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objbill.PatientName;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[3].Value = objbill.DateTo;

                    arParms[4] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[4].Value = objbill.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIPDServiceRecordList", arParms);
                    List<IPServiceRecordData> listpatientdetails = ORHelper<IPServiceRecordData>.FromDataReaderToList(sqlReader);
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
        public int DeleteOTServiceRecordByIPNo(IPServiceRecordData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.IPNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@UserId", SqlDbType.BigInt);
                    arParms[3].Value = objbill.EmployeeID;

                    arParms[4] = new SqlParameter("@Charges", SqlDbType.Money);
                    arParms[4].Value = objbill.ServiceCharge;

                    arParms[5] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.UHID;

                    arParms[6] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[6].Value = objbill.HospitalID;

                    arParms[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[7].Value = objbill.IPaddress;

                    arParms[8] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[8].Value = objbill.EmployeeID;

                    arParms[9] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[9].Value = objbill.ID;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteOTServiceRecordbyIPNo", arParms);
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
        public List<IPServiceRecordData> GetOTServiceList(IPServiceRecordData objbill)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.IPNo;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objbill.PatientName;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateTo;

                    arParms[4] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[4].Value = objbill.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetOTServiceRecordList", arParms);
                    List<IPServiceRecordData> listpatientdetails = ORHelper<IPServiceRecordData>.FromDataReaderToList(sqlReader);
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
        public List<IPServiceRecordData> GetIPDIssueServiceList(IPServiceRecordData objbill)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.IPNo;

                    arParms[1] = new SqlParameter("@ItemID", SqlDbType.Int);
                    arParms[1].Value = objbill.ItemID;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateTo;

                    arParms[4] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[4].Value = objbill.IsActive;

                    arParms[5] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[5].Value = objbill.ServiceTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIPDIssueRecordList", arParms);
                    List<IPServiceRecordData> listpatientdetails = ORHelper<IPServiceRecordData>.FromDataReaderToList(sqlReader);
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
        public List<IPDBillSettingData> GetIPDServiceListSetting(IPDBillSettingData objbill)
        {
            List<IPDBillSettingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.IPNo;

                    arParms[1] = new SqlParameter("@ServiceCategoryID", SqlDbType.Int);
                    arParms[1].Value = objbill.ServiceCategoryID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "", arParms);
                    List<IPDBillSettingData> listpatientdetails = ORHelper<IPDBillSettingData>.FromDataReaderToList(sqlReader);
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
        public List<IPfinalbillData> GetIPDBillSetting(IPfinalbillData objbill)
        {
            List<IPfinalbillData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.IPNo;

                    arParms[1] = new SqlParameter("@ServiceCategoryID", SqlDbType.Int);
                    arParms[1].Value = objbill.ServiceCategoryID;

                    arParms[2] = new SqlParameter("@PatientCategory", SqlDbType.Int);
                    arParms[2].Value = objbill.PatientCategory;

                    arParms[3] = new SqlParameter("@AmountEnable", SqlDbType.Int);
                    arParms[3].Value = objbill.AmountEnable;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_SearchBillSetting", arParms);
                    List<IPfinalbillData> listpatientdetails = ORHelper<IPfinalbillData>.FromDataReaderToList(sqlReader);
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
        public List<IPDBillSettingData> UpdateIPDBillSettingRecord(IPDBillSettingData objbill)
        {
            List<IPDBillSettingData> result;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objbill.XMLData;

                    arParms[1] = new SqlParameter("@ServiceCategoryID", SqlDbType.Int);
                    arParms[1].Value = objbill.ServiceCategoryID;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[2].Value = objbill.HospitalID;

                    arParms[3] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[3].Value = objbill.FinancialYearID;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objbill.EmployeeID;

                    arParms[5] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[5].Value = objbill.IPaddress;

                    

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "", arParms);
                    List<IPDBillSettingData> listpatientdetails = ORHelper<IPDBillSettingData>.FromDataReaderToList(sqlReader);
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
    
        public int UpdateIPNoServiceCharge(IPDBillSettingData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objbill.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objbill.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateEmpRoleStatus", arParms);
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
