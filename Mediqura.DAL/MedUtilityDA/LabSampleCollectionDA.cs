using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.MedLab;
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
    public class LabSampleCollectionDA
    {
        public int UpdateSampleCollectionDetails(SampleCollectionData objSampleCollectionDataData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];


                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objSampleCollectionDataData.XMLData;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objSampleCollectionDataData.EmployeeID;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[2].Value = objSampleCollectionDataData.HospitalID;

                    arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[3].Direction = ParameterDirection.Output;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objSampleCollectionDataData.IPaddress;

                    arParms[5] = new SqlParameter("@TakenBy", SqlDbType.VarChar);
                    arParms[5].Value = objSampleCollectionDataData.TakenBy;

                    arParms[6] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[6].Value = objSampleCollectionDataData.FinancialYearID;

                    arParms[7] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[7].Value = objSampleCollectionDataData.Comment;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateLabSampleCollectionNew", arParms);
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
        public List<SampleCollectionData> InvDetailByInvNo(SampleCollectionData objbill)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@Investigationumber", SqlDbType.VarChar);
                    arParms[0].Value = objbill.Investigationumber;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabSampleCollectedDetailsBYInvNo", arParms);
                    List<SampleCollectionData> listpatientdetails = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public int UpdateIPSampleCollectionDetails(SampleCollectionData objSampleCollectionDataData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@SampleID", SqlDbType.BigInt);
                    arParms[0].Value = objSampleCollectionDataData.ID;

                    arParms[1] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[1].Value = objSampleCollectionDataData.XMLData;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objSampleCollectionDataData.EmployeeID;

                    arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[3].Value = objSampleCollectionDataData.HospitalID;

                    arParms[4] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[4].Value = objSampleCollectionDataData.ActionType;

                    arParms[5] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[5].Direction = ParameterDirection.Output;

                    arParms[6] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[6].Value = objSampleCollectionDataData.IPaddress;

                    arParms[7] = new SqlParameter("@TakenBy", SqlDbType.VarChar);
                    arParms[7].Value = objSampleCollectionDataData.TakenBy;

                    arParms[8] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[8].Value = objSampleCollectionDataData.FinancialYearID;

                    arParms[9] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[9].Value = objSampleCollectionDataData.IPNo;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateIPLabSampleCollectionNew", arParms);
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

        public int UpdateDeviceInitiationDetails(SampleCollectionData objSampleCollectionDataData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objSampleCollectionDataData.XMLData;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objSampleCollectionDataData.EmployeeID;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[2].Value = objSampleCollectionDataData.HospitalID;

                    arParms[3] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[3].Value = objSampleCollectionDataData.ActionType;

                    arParms[4] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[4].Direction = ParameterDirection.Output;

                    arParms[5] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[5].Value = objSampleCollectionDataData.IPaddress;

                    arParms[6] = new SqlParameter("@TakenBy", SqlDbType.VarChar);
                    arParms[6].Value = objSampleCollectionDataData.TakenBy;

                    arParms[7] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[7].Value = objSampleCollectionDataData.FinancialYearID;



                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateLabDeviceInitiation", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[4].Value);
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
        public int UpdateLabResultEntryDetails(LabResultData objresult)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objresult.XMLData;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objresult.EmployeeID;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[2].Value = objresult.HospitalID;

                    arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[3].Direction = ParameterDirection.Output;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objresult.IPaddress;

                    arParms[5] = new SqlParameter("@VerifiedBy", SqlDbType.BigInt);
                    arParms[5].Value = objresult.VerifiedBy;

                    arParms[6] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[6].Value = objresult.FinancialYearID;

                    arParms[7] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[7].Value = objresult.Remarks;

                    arParms[8] = new SqlParameter("@ReportType", SqlDbType.Int);
                    arParms[8].Value = objresult.ReportType;

                    arParms[9] = new SqlParameter("@TemplateType", SqlDbType.Int);
                    arParms[9].Value = objresult.TemplateType;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateLabResultEntryDetails", arParms);
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
        public int UpdateCommentedLabResult(LabResultData objresult)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objresult.UHID;

                    arParms[1] = new SqlParameter("@TestID", SqlDbType.Int);
                    arParms[1].Value = objresult.LabServiceID;

                    arParms[2] = new SqlParameter("@InvNumber", SqlDbType.VarChar);
                    arParms[2].Value = objresult.Investigationumber;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objresult.EmployeeID;

                    arParms[4] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[4].Value = objresult.HospitalID;

                    arParms[5] = new SqlParameter("@Output", SqlDbType.Int);
                    arParms[5].Direction = ParameterDirection.Output;

                    arParms[6] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[6].Value = objresult.IPaddress;

                    arParms[7] = new SqlParameter("@VerifiedBy", SqlDbType.BigInt);
                    arParms[7].Value = objresult.VerifiedBy;

                    arParms[8] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[8].Value = objresult.FinancialYearID;

                    arParms[9] = new SqlParameter("@ResultTemplate", SqlDbType.VarChar);
                    arParms[9].Value = objresult.ResultTemplate;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateCommentedLabResultEntryDetails", arParms);
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
        public List<SampleCollectionData> GetSampleCollectionDetailsByID(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@RangeID", SqlDbType.Int);
                    arParms[0].Value = objSampleCollectionData.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetLabRangeDetailsByID", arParms);
                    List<SampleCollectionData> lstPatientTypeDetails = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public int DeleteSampleCollectionDetailsByID(SampleCollectionData objSampleCollectionData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@RangeID", SqlDbType.Int);
                    arParms[0].Value = objSampleCollectionData.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objSampleCollectionData.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objSampleCollectionData.ReferalDoctor;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objSampleCollectionData.IPaddress;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteLabRangeDetailsByID", arParms);
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
        public int UpdateDeviceInitiation(SampleCollectionData objdevice)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objdevice.ID;

                    arParms[1] = new SqlParameter("@Investigationumber", SqlDbType.VarChar);
                    arParms[1].Value = objdevice.Investigationumber;

                    arParms[2] = new SqlParameter("@StatusID", SqlDbType.Int);
                    arParms[2].Value = objdevice.StatusID;

                    arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[3].Direction = ParameterDirection.Output;

                    arParms[4] = new SqlParameter("@DeviceInitiatedBy", SqlDbType.BigInt);
                    arParms[4].Value = objdevice.EmployeeID;

                    arParms[5] = new SqlParameter("@RecievedBy", SqlDbType.BigInt);
                    arParms[5].Value = objdevice.EmployeeID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_Device_Inititation", arParms);
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
        public int UpdateMLTrecievingtime(SampleCollectionData objdevice)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objdevice.ID;

                    arParms[1] = new SqlParameter("@Investigationumber", SqlDbType.VarChar);
                    arParms[1].Value = objdevice.Investigationumber;

                    arParms[2] = new SqlParameter("@MLTrecievedBy", SqlDbType.BigInt);
                    arParms[2].Value = objdevice.EmployeeID;

                    arParms[3] = new SqlParameter("@LabServiceID", SqlDbType.Int);
                    arParms[3].Value = objdevice.LabServiceID;

                    arParms[4] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[4].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_MLT_recievingtime", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[4].Value);
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
        public List<SampleCollectionData> SearchOPSampleCollectionDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];


                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objSampleCollectionData.BillNo;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objSampleCollectionData.UHID;



                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchOPLabSampleCollectionGrid", arParms);
                    List<SampleCollectionData> lstDetails = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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

        public List<SampleCollectionData> SearchSampleCollectionDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];


                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objSampleCollectionData.BillNo;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objSampleCollectionData.UHID;



                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchLabSampleCollectionGrid", arParms);
                    List<SampleCollectionData> lstDetails = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> SearchIPSampleCollectionDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];


                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objSampleCollectionData.IPNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchIPLabSampleCollectionGrid", arParms);
                    List<SampleCollectionData> lstDetails = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> SearchIPSampleCollectedDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];


                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objSampleCollectionData.IPNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchIPLabSampleCollectedDetails", arParms);
                    List<SampleCollectionData> lstDetails = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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

        public List<SampleCollectionData> SearchIPDSampleCollectionDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];


                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objSampleCollectionData.LabServiceID;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.Int);
                    arParms[1].Value = objSampleCollectionData.UHID;


                    arParms[2] = new SqlParameter("@OrderID", SqlDbType.BigInt);
                    arParms[2].Value = objSampleCollectionData.OrderID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchIPDLabSampleCollectionGrid", arParms);
                    List<SampleCollectionData> lstPaymentTypeDetails = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> SearchLabDeviceDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];


                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objSampleCollectionData.LabServiceID;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.Int);
                    arParms[1].Value = objSampleCollectionData.UHID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchLabDeviceDetails", arParms);
                    List<SampleCollectionData> lstPaymentTypeDetails = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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

        public List<SampleCollectionData> GetLabSampleDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objSampleCollectionData.UHID;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objSampleCollectionData.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[2].Value = objSampleCollectionData.DateTo;

                    arParms[3] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[3].Value = objSampleCollectionData.PatientTypeID;

                    arParms[4] = new SqlParameter("@Name", SqlDbType.VarChar);
                    arParms[4].Value = objSampleCollectionData.PatientName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetLabSampleDetails", arParms);
                    List<SampleCollectionData> lstPaymentTypeDetails = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> GetIPDLabSampleDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objSampleCollectionData.UHID;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objSampleCollectionData.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[2].Value = objSampleCollectionData.DateTo;

                    arParms[3] = new SqlParameter("@Name", SqlDbType.VarChar);
                    arParms[3].Value = objSampleCollectionData.PatientName;

                    arParms[4] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[4].Value = objSampleCollectionData.IPNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetIPDLabSampleDetails", arParms);
                    List<SampleCollectionData> lstPaymentTypeDetails = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> GetLabSampleCollectedDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@Investigationumber", SqlDbType.VarChar);
                    arParms[0].Value = objSampleCollectionData.Investigationumber;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objSampleCollectionData.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[2].Value = objSampleCollectionData.DateTo;

                    arParms[3] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[3].Value = objSampleCollectionData.PatientTypeID;

                    arParms[4] = new SqlParameter("@Name", SqlDbType.VarChar);
                    arParms[4].Value = objSampleCollectionData.PatientName;

                    arParms[5] = new SqlParameter("@ConsultantID", SqlDbType.BigInt);
                    arParms[5].Value = objSampleCollectionData.ConsultantID;

                    arParms[6] = new SqlParameter("@Status", SqlDbType.Int);
                    arParms[6].Value = objSampleCollectionData.CollectionStatus;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabSampleCollectedDetails", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<LabResultData> GetLabResults(LabResultData objSampleCollectionData)
        {
            List<LabResultData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@Investigationumber", SqlDbType.VarChar);
                    arParms[0].Value = objSampleCollectionData.Investigationumber;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.Int);
                    arParms[1].Value = objSampleCollectionData.UHID;

                    arParms[2] = new SqlParameter("@TestID", SqlDbType.Int);
                    arParms[2].Value = objSampleCollectionData.LabServiceID;

                    arParms[3] = new SqlParameter("@RoleID", SqlDbType.Int);
                    arParms[3].Value = objSampleCollectionData.RoleID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Create_LabReports", arParms);
                    List<LabResultData> lstresult = ORHelper<LabResultData>.FromDataReaderToList(sqlReader);
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
        public List<LabResultData> GetCommentedLabResults(LabResultData objSampleCollectionData)
        {
            List<LabResultData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@Investigationumber", SqlDbType.VarChar);
                    arParms[0].Value = objSampleCollectionData.Investigationumber;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.Int);
                    arParms[1].Value = objSampleCollectionData.UHID;

                    arParms[2] = new SqlParameter("@TestID", SqlDbType.Int);
                    arParms[2].Value = objSampleCollectionData.LabServiceID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Create_CommentedLabReports", arParms);
                    List<LabResultData> lstresult = ORHelper<LabResultData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> GetLadeviceInitiationDetail(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@Investigationumber", SqlDbType.VarChar);
                    arParms[0].Value = objSampleCollectionData.Investigationumber;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objSampleCollectionData.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[2].Value = objSampleCollectionData.DateTo;

                    arParms[3] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[3].Value = objSampleCollectionData.PatientTypeID;

                    arParms[4] = new SqlParameter("@Name", SqlDbType.VarChar);
                    arParms[4].Value = objSampleCollectionData.PatientName;

                    arParms[5] = new SqlParameter("@ConsultantID", SqlDbType.BigInt);
                    arParms[5].Value = objSampleCollectionData.ConsultantID;

                    arParms[6] = new SqlParameter("@Status", SqlDbType.Int);
                    arParms[6].Value = objSampleCollectionData.StatusID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabDeviceinitiateddDetails", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        ///--------Lab Result Enty--------------- ///
        public List<SampleCollectionData> GetTestPatientList(SampleCollectionData objTestPatient)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[13];

                    arParms[0] = new SqlParameter("@Investigationumber", SqlDbType.VarChar);
                    arParms[0].Value = objTestPatient.Investigationumber;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objTestPatient.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[2].Value = objTestPatient.DateTo;

                    arParms[3] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[3].Value = objTestPatient.PatientTypeID;

                    arParms[4] = new SqlParameter("@Name", SqlDbType.VarChar);
                    arParms[4].Value = objTestPatient.PatientName;

                    arParms[5] = new SqlParameter("@ConsultantID", SqlDbType.BigInt);
                    arParms[5].Value = objTestPatient.ConsultantID;

                    arParms[6] = new SqlParameter("@Status", SqlDbType.Int);
                    arParms[6].Value = objTestPatient.StatusID;

                    arParms[7] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[7].Value = objTestPatient.UHID;

                    arParms[8] = new SqlParameter("@LabServiceID", SqlDbType.Int);
                    arParms[8].Value = objTestPatient.LabServiceID;

                    arParms[9] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[9].Value = objTestPatient.IPNo;

                    arParms[10] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[10].Value = objTestPatient.CurrentIndex;

                    arParms[11] = new SqlParameter("@pageSize", SqlDbType.Int);
                    arParms[11].Value = objTestPatient.PageSize;

                    arParms[12] = new SqlParameter("@RunnerID", SqlDbType.Int);
                    arParms[12].Value = objTestPatient.RunnerID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabTestPatientList ", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> GetResultentrytestlist(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@Investigationumber", SqlDbType.VarChar);
                    arParms[0].Value = objSampleCollectionData.Investigationumber;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objSampleCollectionData.UHID;

                    arParms[2] = new SqlParameter("@Status", SqlDbType.Int);
                    arParms[2].Value = objSampleCollectionData.StatusID;

                    //arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    //arParms[3].Value = objSampleCollectionData.DateFrom;

                    //arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    //arParms[2].Value = objSampleCollectionData.DateTo;

                    //arParms[3] = new SqlParameter("@PatientType", SqlDbType.Int);
                    //arParms[3].Value = objSampleCollectionData.PatientTypeID;

                    //arParms[4] = new SqlParameter("@Name", SqlDbType.VarChar);
                    //arParms[4].Value = objSampleCollectionData.PatientName;

                    //arParms[5] = new SqlParameter("@ConsultantID", SqlDbType.BigInt);
                    //arParms[5].Value = objSampleCollectionData.ConsultantID;

                    //arParms[8] = new SqlParameter("@LabServiceID", SqlDbType.Int);
                    //arParms[8].Value = objSampleCollectionData.LabServiceID;

                    //arParms[9] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    //arParms[9].Value = objSampleCollectionData.IPNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabResultentrytestList ", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        // Culture //
        public List<LabResultData> GetMicroLabResults(LabResultData objSampleCollectionData)
        {
            List<LabResultData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@Investigationumber", SqlDbType.VarChar);
                    arParms[0].Value = objSampleCollectionData.Investigationumber;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.Int);
                    arParms[1].Value = objSampleCollectionData.UHID;

                    arParms[2] = new SqlParameter("@TestID", SqlDbType.Int);
                    arParms[2].Value = objSampleCollectionData.LabServiceID;

                    arParms[3] = new SqlParameter("@RoleID", SqlDbType.Int);
                    arParms[3].Value = objSampleCollectionData.RoleID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Create_MicroLabReports", arParms);
                    List<LabResultData> lstresult = ORHelper<LabResultData>.FromDataReaderToList(sqlReader);
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
        public int UpdateLabCultureResultEntryDetails(LabResultData objresult)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[22];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objresult.XMLData;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objresult.EmployeeID;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[2].Value = objresult.HospitalID;

                    arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[3].Direction = ParameterDirection.Output;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objresult.IPaddress;

                    arParms[5] = new SqlParameter("@VerifiedBy", SqlDbType.BigInt);
                    arParms[5].Value = objresult.VerifiedBy;

                    arParms[6] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[6].Value = objresult.FinancialYearID;

                    arParms[7] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[7].Value = objresult.Remarks;

                    arParms[8] = new SqlParameter("@ReportType", SqlDbType.Int);
                    arParms[8].Value = objresult.ReportType;

                    arParms[9] = new SqlParameter("@TemplateType", SqlDbType.Int);
                    arParms[9].Value = objresult.TemplateType;

                    arParms[10] = new SqlParameter("@Sample", SqlDbType.VarChar);
                    arParms[10].Value = objresult.Sample;

                    arParms[11] = new SqlParameter("@Colony", SqlDbType.VarChar);
                    arParms[11].Value = objresult.Colony;

                    arParms[12] = new SqlParameter("@OrganismYielded", SqlDbType.VarChar);
                    arParms[12].Value = objresult.OrganismYielded;

                    arParms[13] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[13].Value = objresult.UHID;

                    arParms[14] = new SqlParameter("@Investigationumber", SqlDbType.VarChar);
                    arParms[14].Value = objresult.Investigationumber;

                    arParms[15] = new SqlParameter("@LabServiceID", SqlDbType.Int);
                    arParms[15].Value = objresult.LabServiceID;

                    arParms[16] = new SqlParameter("@IPno", SqlDbType.VarChar);
                    arParms[16].Value = objresult.IPno;

                    arParms[17] = new SqlParameter("@OPno", SqlDbType.VarChar);
                    arParms[17].Value = objresult.OPno;

                    arParms[18] = new SqlParameter("@Emergno", SqlDbType.VarChar);
                    arParms[18].Value = objresult.Emergno;

                    arParms[19] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[19].Value = objresult.PatientTypeID;

                    arParms[20] = new SqlParameter("@GrowthType", SqlDbType.Int);
                    arParms[20].Value = objresult.GrowthType;

                    arParms[21] = new SqlParameter("@MethodID", SqlDbType.Int);
                    arParms[21].Value = objresult.MethodID;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateLabCultureResultEntryDetails", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[3].Value);
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
        // End Culture //
        ///------END OF Result Entry-------///
        ///
        public List<SampleCollectionData> GetCommentResultentrytestlist(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@Investigationumber", SqlDbType.VarChar);
                    arParms[0].Value = objSampleCollectionData.Investigationumber;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objSampleCollectionData.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[2].Value = objSampleCollectionData.DateTo;

                    arParms[3] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[3].Value = objSampleCollectionData.PatientTypeID;

                    arParms[4] = new SqlParameter("@Name", SqlDbType.VarChar);
                    arParms[4].Value = objSampleCollectionData.PatientName;

                    arParms[5] = new SqlParameter("@ConsultantID", SqlDbType.BigInt);
                    arParms[5].Value = objSampleCollectionData.ConsultantID;

                    arParms[6] = new SqlParameter("@Status", SqlDbType.Int);
                    arParms[6].Value = objSampleCollectionData.StatusID;

                    arParms[7] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[7].Value = objSampleCollectionData.UHID;

                    arParms[8] = new SqlParameter("@LabServiceID", SqlDbType.Int);
                    arParms[8].Value = objSampleCollectionData.LabServiceID;

                    arParms[9] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[9].Value = objSampleCollectionData.IPNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetCommentedLabResultentrytestList ", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> GetLabREsultCollectedDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objSampleCollectionData.UHID;

                    arParms[1] = new SqlParameter("@PatientType", SqlDbType.BigInt);
                    arParms[1].Value = objSampleCollectionData.PatientTypeID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabResultCollectedDetails", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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

        public List<SampleCollectionData> GetLabSampleCollectedDataDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objSampleCollectionData.UHID;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objSampleCollectionData.DateFrom;

                    arParms[2] = new SqlParameter("@PatientType", SqlDbType.BigInt);
                    arParms[2].Value = objSampleCollectionData.PatientTypeID;

                    arParms[3] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[3].Value = objSampleCollectionData.BillNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabSampleCollectedDataDetails", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> GetLabDeviceInitiatedDataDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objSampleCollectionData.UHID;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objSampleCollectionData.DateFrom;

                    arParms[2] = new SqlParameter("@PatientType", SqlDbType.BigInt);
                    arParms[2].Value = objSampleCollectionData.PatientTypeID;

                    arParms[3] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[3].Value = objSampleCollectionData.BillNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabDeviceInitiatedDataDetails", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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

        public List<SampleCollectionData> SearchLabSampleCollectionDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];


                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objSampleCollectionData.LabServiceID;

                    arParms[1] = new SqlParameter("@OrderID", SqlDbType.BigInt);
                    arParms[1].Value = objSampleCollectionData.OrderID;

                    arParms[2] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[2].Value = objSampleCollectionData.UHID;



                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchLabSampleCollectedDetails", arParms);
                    List<SampleCollectionData> lstPaymentTypeDetails = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> SearchLabResultCollectionDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];


                    arParms[1] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[1].Value = objSampleCollectionData.LabServiceID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchLabResultCollectedDetail", arParms);
                    List<SampleCollectionData> lstPaymentTypeDetails = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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

        public List<SampleCollectionData> GetTestName(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[0].Value = objD.TestName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetTestName", arParms);
                    //sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_OPD_Service_ChargeList", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> GetSubTestName(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[0].Value = objD.TestName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetSubTestName", arParms);
                    //sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_OPD_Service_ChargeList", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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


        public List<SampleCollectionData> GetTestNameDeviceinitiated(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[0].Value = objD.TestName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetTestNameDeviceInitiated", arParms);
                    //sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_OPD_Service_ChargeList", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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

        public List<SampleCollectionData> GetLabID(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@BillID", SqlDbType.VarChar);
                    arParms[0].Value = objD.BillNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabID", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> GetLabInvestigationForIP(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@Investigationumber", SqlDbType.VarChar);
                    arParms[0].Value = objD.Investigationumber;

                    arParms[1] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[1].Value = objD.PatientTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabInvetgigationNoForIP", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> GetLabInvestigationno(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@Investigationumber", SqlDbType.VarChar);
                    arParms[0].Value = objD.Investigationumber;

                    arParms[1] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[1].Value = objD.PatientTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabInvetgigationumber", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> GetOPInvnumber(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@Investigationumber", SqlDbType.VarChar);
                    arParms[0].Value = objD.Investigationumber;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetOPInvetgigationumber", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> GetDevicecompletedInvestigationno(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@Investigationumber", SqlDbType.VarChar);
                    arParms[0].Value = objD.Investigationumber;

                    arParms[1] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[1].Value = objD.PatientTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDevicecompletedInvetgigationumber", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> GetsamplecollectedInvestigationno(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@Investigationumber", SqlDbType.VarChar);
                    arParms[0].Value = objD.Investigationumber;

                    arParms[1] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[1].Value = objD.PatientTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabInvetgigationumber", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> GetLabPatientNames(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientName;

                    arParms[1] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[1].Value = objD.PatientTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabReportPatientName", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> GetDeviceCompletedPatientName(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientName;

                    arParms[1] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[1].Value = objD.PatientTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDevicecompletedPatientNames", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> GetDeviceCompletedTestNames(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[0].Value = objD.TestName;

                    arParms[1] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[1].Value = objD.PatientTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDevicecompletedTestNames", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<IPData> getIPNo(IPData objD)
        {
            List<IPData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_IPNo", arParms);
                    List<IPData> lstresult = ORHelper<IPData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> GetPatientAdmissionDetailsByLabID(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.BillNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientAdmissionDetailsByLabIDNew", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> GetSampleDescription(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@Samplecode", SqlDbType.VarChar);
                    arParms[0].Value = objD.Samplecode;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetSampleTypeDesc", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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

        public List<RadioLabReportVerificationData> GetLabCommentTemplateByID(RadioLabReportVerificationData obj)
        {
            List<RadioLabReportVerificationData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = obj.LabID;

                    arParms[1] = new SqlParameter("@InvNo", SqlDbType.VarChar);
                    arParms[1].Value = obj.InVnumber;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_Lab_test_comment_for_patient_BY_ID", arParms);
                    List<RadioLabReportVerificationData> ListTemplateData = ORHelper<RadioLabReportVerificationData>.FromDataReaderToList(sqlReader);
                    result = ListTemplateData;
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
        public List<SampleCollectionData> GetRadioInvestigationno(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@Investigationumber", SqlDbType.VarChar);
                    arParms[0].Value = objD.Investigationumber;

                    arParms[1] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[1].Value = objD.PatientTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetRadioInvetgigationumber", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> GetRadioPatientName(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientName;

                    arParms[1] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[1].Value = objD.PatientTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetRadioPatientNames", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<SampleCollectionData> GetRadioTestNames(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[0].Value = objD.TestName;

                    arParms[1] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[1].Value = objD.PatientTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetRadioTestNames", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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

        public List<SampleCollectionData> GetRadioTestList(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@Investigationumber", SqlDbType.VarChar);
                    arParms[0].Value = objSampleCollectionData.Investigationumber;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objSampleCollectionData.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[2].Value = objSampleCollectionData.DateTo;

                    arParms[3] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[3].Value = objSampleCollectionData.PatientTypeID;

                    arParms[4] = new SqlParameter("@Name", SqlDbType.VarChar);
                    arParms[4].Value = objSampleCollectionData.PatientName;

                    arParms[5] = new SqlParameter("@ConsultantID", SqlDbType.BigInt);
                    arParms[5].Value = objSampleCollectionData.ConsultantID;

                    arParms[6] = new SqlParameter("@Status", SqlDbType.Int);
                    arParms[6].Value = objSampleCollectionData.StatusID;

                    arParms[7] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[7].Value = objSampleCollectionData.UHID;

                    arParms[8] = new SqlParameter("@LabServiceID", SqlDbType.Int);
                    arParms[8].Value = objSampleCollectionData.LabServiceID;

                    arParms[9] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[9].Value = objSampleCollectionData.IPNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetRadioTestList ", arParms);
                    List<SampleCollectionData> lstresult = ORHelper<SampleCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<LabResultData> GetRadioReportDetails(LabResultData objSampleCollectionData)
        {
            List<LabResultData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@Investigationumber", SqlDbType.VarChar);
                    arParms[0].Value = objSampleCollectionData.Investigationumber;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.Int);
                    arParms[1].Value = objSampleCollectionData.UHID;

                    arParms[2] = new SqlParameter("@TestID", SqlDbType.Int);
                    arParms[2].Value = objSampleCollectionData.LabServiceID;

                    arParms[3] = new SqlParameter("@VerifiedBy", SqlDbType.BigInt);
                    arParms[3].Value = objSampleCollectionData.VerifiedBy;

                    arParms[4] = new SqlParameter("@Signature", SqlDbType.Image);
                    arParms[4].Value = null;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_Radio_ReportDetails", arParms);
                    List<LabResultData> lstresult = ORHelper<LabResultData>.FromDataReaderToList(sqlReader);
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
