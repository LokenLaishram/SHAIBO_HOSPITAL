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
using Mediqura.CommonData.MedNurseData;
namespace Mediqura.DAL.MedNurseDA
{
    public class DailyNursingAssessmentDA
    {
        public List<NurseNotesData> GetNurseNotesList(NurseNotesData objpat)
        {
            List<NurseNotesData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objpat.IPNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetNurseNotesList", arParms);
                    List<NurseNotesData> listdetails = ORHelper<NurseNotesData>.FromDataReaderToList(sqlReader);
                    result = listdetails;
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
        public List<DailyNursingAssessmentData> GetPatientDetailByID(DailyNursingAssessmentData objpat)
        {
            List<DailyNursingAssessmentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objpat.IPNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientDetailByID", arParms);
                    List<DailyNursingAssessmentData> listdetails = ORHelper<DailyNursingAssessmentData>.FromDataReaderToList(sqlReader);
                    result = listdetails;
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
        public List<DailyNursingAssessmentData> GetNursingAssementByIPNo(DailyNursingAssessmentData objpat)
        {
            List<DailyNursingAssessmentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objpat.IPNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_NursingAssementByIPNo", arParms);
                    List<DailyNursingAssessmentData> listdetails = ORHelper<DailyNursingAssessmentData>.FromDataReaderToList(sqlReader);
                    result = listdetails;
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
        public List<DailyNursingAssessmentData> GetNurseProgressSheet(DailyNursingAssessmentData objpat)
        {
            List<DailyNursingAssessmentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objpat.IPNo;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objpat.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[2].Value = objpat.DateTo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDailyNursingAssessment", arParms);
                    List<DailyNursingAssessmentData> listdetails = ORHelper<DailyNursingAssessmentData>.FromDataReaderToList(sqlReader);
                    result = listdetails;
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
        public int UpdateDailyNursingAssessment(DailyNursingAssessmentData objpat)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[77];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objpat.IPNo;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objpat.PatientName;

                    arParms[2] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[2].Value = objpat.UHID;

                    arParms[3] = new SqlParameter("@NurseShift", SqlDbType.Int);
                    arParms[3].Value = objpat.NurseShift;

                    arParms[4] = new SqlParameter("@EmotionalState", SqlDbType.Int);
                    arParms[4].Value = objpat.EmotionalState;

                    arParms[5] = new SqlParameter("@Consciousness", SqlDbType.Int);
                    arParms[5].Value = objpat.Consciousness;

                    arParms[6] = new SqlParameter("@Speech", SqlDbType.Int);
                    arParms[6].Value = objpat.Speech;

                    arParms[7] = new SqlParameter("@PhysicalType", SqlDbType.Int);
                    arParms[7].Value = objpat.PhysicalType;

                    arParms[8] = new SqlParameter("@RbTime", SqlDbType.Int);
                    arParms[8].Value = objpat.RbTime;

                    arParms[9] = new SqlParameter("@RbPlace", SqlDbType.Int);
                    arParms[9].Value = objpat.RbPlace;

                    arParms[10] = new SqlParameter("@RbPerson", SqlDbType.Int);
                    arParms[10].Value = objpat.RbPerson;

                    arParms[11] = new SqlParameter("@Respiratory", SqlDbType.Int);
                    arParms[11].Value = objpat.Respiratory;

                    arParms[12] = new SqlParameter("@Pulse", SqlDbType.VarChar);
                    arParms[12].Value = objpat.Pulse;

                    arParms[13] = new SqlParameter("@Breath", SqlDbType.VarChar);
                    arParms[13].Value = objpat.Breath;

                    arParms[14] = new SqlParameter("@Cough", SqlDbType.VarChar);
                    arParms[14].Value = objpat.Cough;

                    arParms[15] = new SqlParameter("@Oxygen", SqlDbType.VarChar);
                    arParms[15].Value = objpat.Oxygen;

                    arParms[16] = new SqlParameter("@ChestDrain", SqlDbType.Int);
                    arParms[16].Value = objpat.ChestDrain;

                    arParms[17] = new SqlParameter("@RbL_Plueral", SqlDbType.Int);
                    arParms[17].Value = objpat.RbL_Plueral;

                    arParms[18] = new SqlParameter("@RbR_Plueral", SqlDbType.Int);
                    arParms[18].Value = objpat.RbR_Plueral;

                    arParms[19] = new SqlParameter("@Cardio", SqlDbType.Int);
                    arParms[19].Value = objpat.Cardio;

                    arParms[20] = new SqlParameter("@Tension", SqlDbType.Int);
                    arParms[20].Value = objpat.Tension;

                    arParms[21] = new SqlParameter("@Peripheral", SqlDbType.Int);
                    arParms[21].Value = objpat.Peripheral;

                    arParms[22] = new SqlParameter("@RbDistension", SqlDbType.Int);
                    arParms[22].Value = objpat.RbDistension;

                    arParms[23] = new SqlParameter("@RbChestpain", SqlDbType.Int);
                    arParms[23].Value = objpat.RbChestpain;

                    arParms[24] = new SqlParameter("@Gastrointestinalmouth", SqlDbType.Int);
                    arParms[24].Value = objpat.Gastrointestinalmouth;

                    arParms[25] = new SqlParameter("@Gastrointestinalteeth", SqlDbType.Int);
                    arParms[25].Value = objpat.Gastrointestinalteeth;

                    arParms[26] = new SqlParameter("@Gastrointestinaltongue", SqlDbType.Int);
                    arParms[26].Value = objpat.Gastrointestinaltongue;

                    arParms[27] = new SqlParameter("@RbOralulcers", SqlDbType.Int);
                    arParms[27].Value = objpat.RbOralulcers;

                    arParms[28] = new SqlParameter("@RbAbndistension", SqlDbType.Int);
                    arParms[28].Value = objpat.RbAbndistension;

                    arParms[29] = new SqlParameter("@RbNausea", SqlDbType.Int);
                    arParms[29].Value = objpat.RbNausea;

                    arParms[30] = new SqlParameter("@RbVomitting", SqlDbType.Int);
                    arParms[30].Value = objpat.RbVomitting;

                    arParms[31] = new SqlParameter("@RbNpo", SqlDbType.Int);
                    arParms[31].Value = objpat.RbNpo;

                    arParms[32] = new SqlParameter("@Nutrition", SqlDbType.Int);
                    arParms[32].Value = objpat.Nutrition;

                    arParms[33] = new SqlParameter("@RbOral", SqlDbType.Int);
                    arParms[33].Value = objpat.RbOral;

                    arParms[34] = new SqlParameter("@RbTubefeeding", SqlDbType.Int);
                    arParms[34].Value = objpat.RbTubefeeding;

                    arParms[35] = new SqlParameter("@RbParenteral", SqlDbType.Int);
                    arParms[35].Value = objpat.RbParenteral;

                    arParms[36] = new SqlParameter("@RbBowel", SqlDbType.Int);
                    arParms[36].Value = objpat.RbBowel;

                    arParms[37] = new SqlParameter("@RbConstipation", SqlDbType.Int);
                    arParms[37].Value = objpat.RbConstipation;

                    arParms[38] = new SqlParameter("@RbDiarrhoea", SqlDbType.Int);
                    arParms[38].Value = objpat.RbDiarrhoea;

                    arParms[39] = new SqlParameter("@RbMalena", SqlDbType.Int);
                    arParms[39].Value = objpat.RbMalena;

                    arParms[40] = new SqlParameter("@Mouth", SqlDbType.Int);
                    arParms[40].Value = objpat.Mouth;

                    arParms[41] = new SqlParameter("@RbUrine", SqlDbType.Int);
                    arParms[41].Value = objpat.RbUrine;

                    arParms[42] = new SqlParameter("@RbHematuria", SqlDbType.Int);
                    arParms[42].Value = objpat.RbHematuria;

                    arParms[43] = new SqlParameter("@Skin", SqlDbType.Int);
                    arParms[43].Value = objpat.Skin;

                    arParms[44] = new SqlParameter("@Cyanosis", SqlDbType.Int);
                    arParms[44].Value = objpat.Cyanosis;

                    arParms[45] = new SqlParameter("@Peripheries", SqlDbType.Int);
                    arParms[45].Value = objpat.Peripheries;

                    arParms[46] = new SqlParameter("@Oedemasite", SqlDbType.VarChar);
                    arParms[46].Value = objpat.Oedemasite;

                    arParms[47] = new SqlParameter("@Temperature", SqlDbType.Int);
                    arParms[47].Value = objpat.Temperature;

                    arParms[48] = new SqlParameter("@Scalp", SqlDbType.Int);
                    arParms[48].Value = objpat.Scalp;

                    arParms[49] = new SqlParameter("@Eyes", SqlDbType.Int);
                    arParms[49].Value = objpat.Eyes;

                    arParms[50] = new SqlParameter("@Nose", SqlDbType.Int);
                    arParms[50].Value = objpat.Nose;

                    arParms[51] = new SqlParameter("@Ear", SqlDbType.Int);
                    arParms[51].Value = objpat.Ear;

                    arParms[52] = new SqlParameter("@Sleep", SqlDbType.Int);
                    arParms[52].Value = objpat.Sleep;

                    arParms[53] = new SqlParameter("@Joint", SqlDbType.Int);
                    arParms[53].Value = objpat.Joint;

                    arParms[54] = new SqlParameter("@Ambulate", SqlDbType.Int);
                    arParms[54].Value = objpat.Ambulate;

                    arParms[55] = new SqlParameter("@Sitecentralline", SqlDbType.VarChar);
                    arParms[55].Value = objpat.Sitecentralline;

                    arParms[56] = new SqlParameter("@Conditioncentralline", SqlDbType.VarChar);
                    arParms[56].Value = objpat.Conditioncentralline;

                    arParms[57] = new SqlParameter("@Siteperipheralline", SqlDbType.VarChar);
                    arParms[57].Value = objpat.Siteperipheralline;

                    arParms[58] = new SqlParameter("@Conditionperipheralline", SqlDbType.VarChar);
                    arParms[58].Value = objpat.Conditionperipheralline;

                    arParms[59] = new SqlParameter("@Sitearterialline", SqlDbType.VarChar);
                    arParms[59].Value = objpat.Sitearterialline;

                    arParms[60] = new SqlParameter("@Conditionarterialline", SqlDbType.VarChar);
                    arParms[60].Value = objpat.Conditionarterialline;

                    arParms[61] = new SqlParameter("@Siteanyotherline", SqlDbType.VarChar);
                    arParms[61].Value = objpat.Siteanyotherline;

                    arParms[62] = new SqlParameter("@Conditionanyotherline", SqlDbType.VarChar);
                    arParms[62].Value = objpat.Conditionanyotherline;

                    arParms[63] = new SqlParameter("@RbHealthy", SqlDbType.Int);
                    arParms[63].Value = objpat.RbHealthy;

                    arParms[64] = new SqlParameter("@RbDressing", SqlDbType.Int);
                    arParms[64].Value = objpat.RbDressing;

                    arParms[65] = new SqlParameter("@PainScale", SqlDbType.Int);
                    arParms[65].Value = objpat.PainScale;

                    arParms[66] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[66].Value = objpat.AddedBy;

                    arParms[67] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[67].Value = objpat.EmployeeID;

                    arParms[68] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[68].Direction = ParameterDirection.Output;

                    arParms[69] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[69].Value = objpat.HospitalID;

                    arParms[70] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[70].Value = objpat.FinancialYearID;

                    arParms[71] = new SqlParameter("@ActionType", SqlDbType.SmallInt);
                    arParms[71].Value = objpat.ActionType;

                    arParms[72] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[72].Value = objpat.ID;

                    arParms[73] = new SqlParameter("@WardBedName", SqlDbType.VarChar);
                    arParms[73].Value = objpat.WardBedName;

                    arParms[74] = new SqlParameter("@AgeCount", SqlDbType.VarChar);
                    arParms[74].Value = objpat.AgeCount;

                    arParms[75] = new SqlParameter("@Sex", SqlDbType.VarChar);
                    arParms[75].Value = objpat.Sex;

                    arParms[76] = new SqlParameter("@AdmissionDate", SqlDbType.DateTime);
                    arParms[76].Value = objpat.AdmissionDate;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateDailyNursingAssessment", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[68].Value);
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
        public int UpdateNurseProgressSheet(NurseNotesData objpat)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.ID;

                    arParms[1] = new SqlParameter("@NotedDate", SqlDbType.DateTime);
                    arParms[1].Value = objpat.NotedDate;

                    arParms[2] = new SqlParameter("@NotedTime", SqlDbType.VarChar);
                    arParms[2].Value = objpat.NotedTimes;

                    arParms[3] = new SqlParameter("@Particular", SqlDbType.VarChar);
                    arParms[3].Value = objpat.Particular;

                    arParms[4] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[4].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateNurseNotesDetails", arParms);
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
        public int CancelNurseNotesDataLists(NurseNotesData objpat)
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
                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_CancelNurseNotesDataLists", arParms);
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
        public int DeleteNurseProgressSheet(NurseNotesData objpat)
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
                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteNurseProgressSheet", arParms);
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
        public List<NurseNotesData> GetNurseProgressSheetByID(NurseNotesData objpat)
        {
            List<NurseNotesData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_NUR_GetNurseProgressSheetByID", arParms);
                    List<NurseNotesData> listNuresenotes = ORHelper<NurseNotesData>.FromDataReaderToList(sqlReader);
                    result = listNuresenotes;
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
        public List<DailyNursingAssessmentData> GetPtientDeatilbyID(DailyNursingAssessmentData objpat)
        {
            List<DailyNursingAssessmentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.ID;

                    arParms[1] = new SqlParameter("@NurseShift", SqlDbType.Int);
                    arParms[1].Value = objpat.NurseShift;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_NUR_GetDailyNursingAssessmentByID", arParms);
                    List<DailyNursingAssessmentData> listpatientdetails = ORHelper<DailyNursingAssessmentData>.FromDataReaderToList(sqlReader);
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
        public List<DailyNursingAssessmentData> GetPatientDetailsByIPNo(DailyNursingAssessmentData objD)
        {
            List<DailyNursingAssessmentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientDetailsByIPNo", arParms);
                    List<DailyNursingAssessmentData> lstresult = ORHelper<DailyNursingAssessmentData>.FromDataReaderToList(sqlReader);
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
        public List<DailyNursingAssessmentData> SearchPatientList(DailyNursingAssessmentData objpat)
        {
            List<DailyNursingAssessmentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objpat.IPNo;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objpat.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[2].Value = objpat.DateTo;

                    arParms[3] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[3].Value = objpat.IsActive;

                    arParms[4] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[4].Value = objpat.CurrentIndex;

                    //arParms[5] = new SqlParameter("@userID", SqlDbType.Int);
                    //arParms[5].Value = objpat.userID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDailyAssessemtList", arParms);
                    List<DailyNursingAssessmentData> listpatientdetails = ORHelper<DailyNursingAssessmentData>.FromDataReaderToList(sqlReader);
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
        public List<DailyNursingAssessmentData> SearchPatientDetails(DailyNursingAssessmentData objpat)
        {
            List<DailyNursingAssessmentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.UHID;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objpat.PatientName;

                    arParms[2] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[2].Value = objpat.IPNo;

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
                    List<DailyNursingAssessmentData> listpatientdetails = ORHelper<DailyNursingAssessmentData>.FromDataReaderToList(sqlReader);
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
        public int DeletePatientDetailsByID(DailyNursingAssessmentData objpat)
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
        public List<DailyNursingAssessmentData> GetAdmittedPatientDetails(DailyNursingAssessmentData objD)
        {
            List<DailyNursingAssessmentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@PatientDetails", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientDetails;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetNursePatientDetails", arParms);
                    List<DailyNursingAssessmentData> lstresult = ORHelper<DailyNursingAssessmentData>.FromDataReaderToList(sqlReader);
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
