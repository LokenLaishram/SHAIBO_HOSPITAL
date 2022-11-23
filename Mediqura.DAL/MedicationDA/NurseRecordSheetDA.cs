using Mediqura.CommonData.MedMedication;
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

namespace Mediqura.DAL.MedicationDA
{
    public class NurseRecordSheetDA
    {
        public List<NureseRecordSheetData> GetIPPatientName(NureseRecordSheetData objD)
        {
            List<NureseRecordSheetData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPPatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPPatientName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Medi_GetIPPatientName", arParms);
                    List<NureseRecordSheetData> lstresult = ORHelper<NureseRecordSheetData>.FromDataReaderToList(sqlReader);
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
        public List<NureseRecordSheetData> GetPatientDetailsByIPNO(NureseRecordSheetData objD)
        {
            List<NureseRecordSheetData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Medi_GetPatientNameByIPNO", arParms);
                    List<NureseRecordSheetData> lstresult = ORHelper<NureseRecordSheetData>.FromDataReaderToList(sqlReader);
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
        public int UpdateNurseRecordSheet(NureseRecordSheetData objot)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[24];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objot.ID;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objot.UHID;

                    arParms[2] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[2].Value = objot.IPNo;

                    arParms[3] = new SqlParameter("@IPPatientName", SqlDbType.VarChar);
                    arParms[3].Value = objot.IPPatientName;

                    arParms[4] = new SqlParameter("@WardBedNo", SqlDbType.VarChar);
                    arParms[4].Value = objot.WardBedNo;

                    arParms[5] = new SqlParameter("@EntryDate", SqlDbType.DateTime);
                    arParms[5].Value = objot.EntryDate;

                    arParms[6] = new SqlParameter("@EntryTime", SqlDbType.VarChar);
                    arParms[6].Value = objot.EntryTime;

                    arParms[7] = new SqlParameter("@Temperature", SqlDbType.VarChar);
                    arParms[7].Value = objot.Temperature;

                    arParms[8] = new SqlParameter("@Pulse", SqlDbType.VarChar);
                    arParms[8].Value = objot.Pulse;

                    arParms[9] = new SqlParameter("@BP", SqlDbType.VarChar);
                    arParms[9].Value = objot.BP;

                    arParms[10] = new SqlParameter("@RR", SqlDbType.VarChar);
                    arParms[10].Value = objot.RR;

                    arParms[11] = new SqlParameter("@SpO2", SqlDbType.VarChar);
                    arParms[11].Value = objot.SpO2;

                    arParms[12] = new SqlParameter("@GCSEyeOpeningID", SqlDbType.Int);
                    arParms[12].Value = objot.GCSEyeOpeningID;

                    arParms[13] = new SqlParameter("@GCSEVerbalID", SqlDbType.Int);
                    arParms[13].Value = objot.GCSEVerbalID;

                    arParms[14] = new SqlParameter("@GCSMotorResponseID", SqlDbType.Int);
                    arParms[14].Value = objot.GCSMotorResponseID;

                    arParms[15] = new SqlParameter("@RightPupilValue", SqlDbType.Int);
                    arParms[15].Value = objot.RightPupilValue;

                    arParms[16] = new SqlParameter("@LeftPupilValue", SqlDbType.Int);
                    arParms[16].Value = objot.LeftPupilValue;

                    arParms[17] = new SqlParameter("@InvasiveLine", SqlDbType.VarChar);
                    arParms[17].Value = objot.InvasiveLine;

                    arParms[18] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[18].Value = objot.EmployeeID;

                    arParms[19] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[19].Value = objot.HospitalID;

                    arParms[20] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[20].Value = objot.FinancialYearID;

                    arParms[21] = new SqlParameter("@ActionType", SqlDbType.SmallInt);
                    arParms[21].Value = objot.ActionType;

                    arParms[22] = new SqlParameter("@IPaddress", SqlDbType.VarChar);
                    arParms[22].Value = objot.IPaddress;

                    arParms[23] = new SqlParameter("@Output", SqlDbType.BigInt);
                    arParms[23].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Medi_Update_NurseRecordSheet", arParms);
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
        public List<NureseRecordSheetData> GetNurseRecordList(NureseRecordSheetData objmedi)
        {
            List<NureseRecordSheetData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objmedi.UHID;

                    arParms[1] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[1].Value = objmedi.IPNo;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[2].Value = objmedi.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[3].Value = objmedi.DateTo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Medi_NurseRecordList", arParms);
                    List<NureseRecordSheetData> listRecorddetails = ORHelper<NureseRecordSheetData>.FromDataReaderToList(sqlReader);
                    result = listRecorddetails;
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
        public List<NureseRecordSheetData> GetNurseRecordEntryByID(NureseRecordSheetData objdata)
        {
            List<NureseRecordSheetData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objdata.ID;
                   
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Medi_GetNurseRecordEntryByID", arParms);
                    List<NureseRecordSheetData> lstLabUnitDetails = ORHelper<NureseRecordSheetData>.FromDataReaderToList(sqlReader);
                    result = lstLabUnitDetails;
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
        public int DeleteNurseRecordEntryByID(NureseRecordSheetData objdata)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objdata.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objdata.EmployeeID;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objdata.Remarks;

                    arParms[3] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[3].Value = objdata.IPaddress;

                    arParms[4] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[4].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Medi_DeleteNurseRecordEntryByID", arParms);
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
    }
}
