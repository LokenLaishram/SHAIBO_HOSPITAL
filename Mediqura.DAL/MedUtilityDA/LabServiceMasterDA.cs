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
    public class LabServiceMasterDA
    {
        public int UpdateLabServiceDetails(LabServiceMasterData objLabServiceMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[14];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objLabServiceMaster.ID;

                    arParms[1] = new SqlParameter("@LabGroupID", SqlDbType.Int);
                    arParms[1].Value = objLabServiceMaster.LabGroupID;

                    arParms[2] = new SqlParameter("@LabSubGroupID", SqlDbType.Int);
                    arParms[2].Value = objLabServiceMaster.LabSubGroupID;

                    arParms[3] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[3].Value = objLabServiceMaster.TestName;

                    arParms[4] = new SqlParameter("@TestAmount", SqlDbType.Money);
                    arParms[4].Value = objLabServiceMaster.TestAmount;

                    arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[5].Value = objLabServiceMaster.EmployeeID;

                    arParms[6] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[6].Value = objLabServiceMaster.ActionType;

                    arParms[7] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[7].Direction = ParameterDirection.Output;

                    arParms[8] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[8].Value = objLabServiceMaster.IsActive;

                    arParms[9] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[9].Value = objLabServiceMaster.HospitalID;

                    arParms[10] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[10].Value = objLabServiceMaster.FinancialYearID;

                    arParms[11] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[11].Value = objLabServiceMaster.IPaddress;

                    arParms[12] = new SqlParameter("@TestCenterID", SqlDbType.Int);
                    arParms[12].Value = objLabServiceMaster.TestCenterID;

                    arParms[13] = new SqlParameter("@ReportTypeID", SqlDbType.Int);
                    arParms[13].Value = objLabServiceMaster.ReportTypeID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateLabServiceMST", arParms);
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
        public List<LabServiceMasterData> GetLabServicesByserviceTypeID(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.TestName;

                    arParms[1] = new SqlParameter("@LabSubGroupID", SqlDbType.Int);
                    arParms[1].Value = objservice.LabSubGroupID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_OPLab_ServicesByserviceTypeID", arParms);
                    List<LabServiceMasterData> lstServiceDetails = ORHelper<LabServiceMasterData>.FromDataReaderToList(sqlReader);
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
        public List<LabServiceMasterData> GetTestNames(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.TestName;

                    arParms[1] = new SqlParameter("@LabSubGroupID", SqlDbType.Int);
                    arParms[1].Value = objservice.LabSubGroupID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetTestNames", arParms);
                    List<LabServiceMasterData> lstServiceDetails = ORHelper<LabServiceMasterData>.FromDataReaderToList(sqlReader);
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
        public List<LabServiceMasterData> GetTestNamesWithID(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.TestName;

                    arParms[1] = new SqlParameter("@LabSubGroupID", SqlDbType.Int);
                    arParms[1].Value = objservice.LabSubGroupID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetTestNameswithID", arParms);
                    List<LabServiceMasterData> lstServiceDetails = ORHelper<LabServiceMasterData>.FromDataReaderToList(sqlReader);
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
        public List<LabServiceMasterData> GetTestName(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.TestName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetTestNameWithID", arParms);
                    List<LabServiceMasterData> lstServiceDetails = ORHelper<LabServiceMasterData>.FromDataReaderToList(sqlReader);
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
        public List<LabServiceMasterData> GetRadioTestName(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.TestName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetRadioTestName", arParms);
                    List<LabServiceMasterData> lstServiceDetails = ORHelper<LabServiceMasterData>.FromDataReaderToList(sqlReader);
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
        public List<LabServiceMasterData> GetEndosTestName(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.TestName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetEndosTestName", arParms);
                    List<LabServiceMasterData> lstServiceDetails = ORHelper<LabServiceMasterData>.FromDataReaderToList(sqlReader);
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
        public int UpdateLabSubTest(LabServiceMasterData objLabServiceMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[11];

                    arParms[0] = new SqlParameter("@TestID", SqlDbType.Int);
                    arParms[0].Value = objLabServiceMaster.TestID;

                    arParms[1] = new SqlParameter("@LabGroupID", SqlDbType.Int);
                    arParms[1].Value = objLabServiceMaster.LabGroupID;

                    arParms[2] = new SqlParameter("@LabSubGroupID", SqlDbType.Int);
                    arParms[2].Value = objLabServiceMaster.LabSubGroupID;

                    arParms[3] = new SqlParameter("@XMLData", SqlDbType.VarChar);
                    arParms[3].Value = objLabServiceMaster.XMLData;

                    arParms[4] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[4].Value = objLabServiceMaster.EmployeeID;

                    arParms[5] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[5].Direction = ParameterDirection.Output;

                    arParms[6] = new SqlParameter("@IPAddress", SqlDbType.Bit);
                    arParms[6].Value = objLabServiceMaster.IPaddress;

                    arParms[7] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[7].Value = objLabServiceMaster.HospitalID;

                    arParms[8] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[8].Value = objLabServiceMaster.FinancialYearID;

                    arParms[9] = new SqlParameter("@MachineID", SqlDbType.Int);
                    arParms[9].Value = objLabServiceMaster.MachineID;

                    arParms[10] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[10].Value = objLabServiceMaster.Remarks;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateLabSubTestMST", arParms);
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
        public int UpdateLabSubTestName(LabServiceMasterData objLabServiceMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[22];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objLabServiceMaster.ID;

                    arParms[1] = new SqlParameter("@Code", SqlDbType.VarChar);
                    arParms[1].Value = objLabServiceMaster.Code;

                    arParms[2] = new SqlParameter("@SubTestName", SqlDbType.VarChar);
                    arParms[2].Value = objLabServiceMaster.SubTestName;

                    arParms[3] = new SqlParameter("@Unit", SqlDbType.BigInt);
                    arParms[3].Value = objLabServiceMaster.UnitID;

                    arParms[4] = new SqlParameter("@Sample", SqlDbType.BigInt);
                    arParms[4].Value = objLabServiceMaster.SampleTypeID;

                    arParms[5] = new SqlParameter("@AgeRangeFrom", SqlDbType.VarChar);
                    arParms[5].Value = objLabServiceMaster.AgeRangeFrom;

                    arParms[6] = new SqlParameter("@AgeRangeTo", SqlDbType.VarChar);
                    arParms[6].Value = objLabServiceMaster.AgeRangeTo;

                    arParms[7] = new SqlParameter("@NormalRangeMaleFrom", SqlDbType.VarChar);
                    arParms[7].Value = objLabServiceMaster.NormalRangeMaleFrom;

                    arParms[8] = new SqlParameter("@NormalRangeMaleTo", SqlDbType.VarChar);
                    arParms[8].Value = objLabServiceMaster.NormalRangeMaleTo;

                    arParms[9] = new SqlParameter("@NormalRangeFeMaleFrom", SqlDbType.VarChar);
                    arParms[9].Value = objLabServiceMaster.NormalRangeFeMaleFrom;

                    arParms[10] = new SqlParameter("@NormalRangeFeMaleTo", SqlDbType.VarChar);
                    arParms[10].Value = objLabServiceMaster.NormalRangeFeMaleTo;

                    arParms[11] = new SqlParameter("@RowTypeID", SqlDbType.Int);
                    arParms[11].Value = objLabServiceMaster.RowTypeID;

                    arParms[12] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[12].Value = objLabServiceMaster.EmployeeID;

                    arParms[13] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[13].Value = objLabServiceMaster.ActionType;

                    arParms[14] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[14].Direction = ParameterDirection.Output;

                    arParms[15] = new SqlParameter("@IPAddress", SqlDbType.Bit);
                    arParms[15].Value = objLabServiceMaster.IPaddress;

                    arParms[16] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[16].Value = objLabServiceMaster.HospitalID;

                    arParms[17] = new SqlParameter("@TestID", SqlDbType.Int);
                    arParms[17].Value = objLabServiceMaster.LabservicetypeID;

                    arParms[18] = new SqlParameter("@LabGroupID", SqlDbType.Int);
                    arParms[18].Value = objLabServiceMaster.LabGroupID;

                    arParms[19] = new SqlParameter("@LabSubGroupID", SqlDbType.Int);
                    arParms[19].Value = objLabServiceMaster.LabSubGroupID;

                    arParms[20] = new SqlParameter("@Reagent", SqlDbType.Int);
                    arParms[20].Value = objLabServiceMaster.ReagentTypeID;

                    arParms[21] = new SqlParameter("@Default", SqlDbType.TinyInt);
                    arParms[21].Value = objLabServiceMaster.defaultValue;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateLabSubTestNameMST", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[14].Value);
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
        public List<LabServiceMasterData> GetLabServiceDetailsByID(LabServiceMasterData objLabServiceMaster)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objLabServiceMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GeTLabServiceDetailsByID", arParms);
                    List<LabServiceMasterData> lstLabServiceDetails = ORHelper<LabServiceMasterData>.FromDataReaderToList(sqlReader);
                    result = lstLabServiceDetails;
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
        public List<LabServiceMasterData> GetLabSubTestDetailsByID(LabServiceMasterData objLabServiceMaster)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objLabServiceMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GeTLabSubTestDetailsByID", arParms);
                    List<LabServiceMasterData> lstLabServiceDetails = ORHelper<LabServiceMasterData>.FromDataReaderToList(sqlReader);
                    result = lstLabServiceDetails;
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
        public int DeleteLabServiceDetailsByID(LabServiceMasterData objLabServiceMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objLabServiceMaster.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objLabServiceMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objLabServiceMaster.Remarks;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteLabServiceDetailsByID", arParms);
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
        public int DeleteLabSubTestDetailsByID(LabServiceMasterData objLabServiceMaster)
        {
            int result = 0;
            try
            {
              
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objLabServiceMaster.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objLabServiceMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteLabSubTestDetailsByID", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[2].Value);
                
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;

        }
        public List<LabServiceMasterData> SearchServiceDetails(LabServiceMasterData objLabServiceMaster)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@LabGroupID", SqlDbType.Int);
                    arParms[0].Value = objLabServiceMaster.LabGroupID;

                    arParms[1] = new SqlParameter("@LabSubGroupID", SqlDbType.Int);
                    arParms[1].Value = objLabServiceMaster.LabSubGroupID;

                    arParms[2] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[2].Value = objLabServiceMaster.TestName;

                    arParms[3] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[3].Value = objLabServiceMaster.IsActive;

                    arParms[4] = new SqlParameter("@ReportTypeID", SqlDbType.Int);
                    arParms[4].Value = objLabServiceMaster.ReportTypeID;

                    arParms[5] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[5].Value = objLabServiceMaster.CurrentIndex;

                    arParms[6] = new SqlParameter("@TestID", SqlDbType.Int);
                    arParms[6].Value = objLabServiceMaster.TestID;
      

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchLabService", arParms);
                    List<LabServiceMasterData> lstLabServiceDetails = ORHelper<LabServiceMasterData>.FromDataReaderToList(sqlReader);
                    result = lstLabServiceDetails;
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
        public List<LabServiceMasterData> SearchLabServiceDetails(LabServiceMasterData objLabServiceMaster)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@LabGroupID", SqlDbType.Int);
                    arParms[0].Value = objLabServiceMaster.LabGroupID;

                    arParms[1] = new SqlParameter("@LabSubGroupID", SqlDbType.Int);
                    arParms[1].Value = objLabServiceMaster.LabSubGroupID;

                    arParms[2] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[2].Value = objLabServiceMaster.TestName;

                    arParms[3] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[3].Value = objLabServiceMaster.IsActive;

                    arParms[4] = new SqlParameter("@ReportTypeID", SqlDbType.Int);
                    arParms[4].Value = objLabServiceMaster.ReportTypeID;

             
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchLabServiceDetails", arParms);
                    List<LabServiceMasterData> lstLabServiceDetails = ORHelper<LabServiceMasterData>.FromDataReaderToList(sqlReader);
                    result = lstLabServiceDetails;
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
        public List<LabServiceMasterData> SearchLabSubTestDetails(LabServiceMasterData objLabServiceMaster)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[4];

                arParms[0] = new SqlParameter("@LabGroupID", SqlDbType.Int);
                arParms[0].Value = objLabServiceMaster.LabGroupID;

                arParms[1] = new SqlParameter("@LabSubGroupID", SqlDbType.Int);
                arParms[1].Value = objLabServiceMaster.LabSubGroupID;

                arParms[2] = new SqlParameter("@TestID", SqlDbType.Int);
                arParms[2].Value = objLabServiceMaster.TestID;

                arParms[3] = new SqlParameter("@LabSubTestName", SqlDbType.VarChar);
                arParms[3].Value = objLabServiceMaster.SubTestName;


                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchLabSubTest", arParms);
                List<LabServiceMasterData> lstLabServiceDetails = ORHelper<LabServiceMasterData>.FromDataReaderToList(sqlReader);
                result = lstLabServiceDetails;

            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public List<LabServiceMasterData> GetLabServices(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.TestName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_OPLab_Services", arParms);
                    List<LabServiceMasterData> lstServiceDetails = ORHelper<LabServiceMasterData>.FromDataReaderToList(sqlReader);
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
        public List<LabServiceMasterData> Getinvestigationlist(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.TestName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_Investigations", arParms);
                    List<LabServiceMasterData> lstServiceDetails = ORHelper<LabServiceMasterData>.FromDataReaderToList(sqlReader);
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
        public List<LabServiceMasterData> GetOPLabServices(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.TestName;

                    arParms[1] = new SqlParameter("@ServiceCatgeoryID", SqlDbType.Int);
                    arParms[1].Value = objservice.LabSubGroupID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_OPLab_Services", arParms);
                    List<LabServiceMasterData> lstServiceDetails = ORHelper<LabServiceMasterData>.FromDataReaderToList(sqlReader);
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
        public List<LabServiceMasterData> GetPackageServices(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.TestName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_Package_Service", arParms);
                    List<LabServiceMasterData> lstServiceDetails = ORHelper<LabServiceMasterData>.FromDataReaderToList(sqlReader);
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
        public List<LabServiceMasterData> GetIPServices(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ServiceName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.ServiceName;

                    arParms[1] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[1].Value = objservice.ServicetypeID;

                    arParms[2] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[2].Value = objservice.DoctorID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_IP_Services", arParms);
                    List<LabServiceMasterData> lstServiceDetails = ORHelper<LabServiceMasterData>.FromDataReaderToList(sqlReader);
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
        public List<LabServiceMasterData> GetOTServices(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ServiceName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.ServiceName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_OT_Services", arParms);
                    List<LabServiceMasterData> lstServiceDetails = ORHelper<LabServiceMasterData>.FromDataReaderToList(sqlReader);
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
        public List<LabServiceMasterData> GetOPPhrServices(LabServiceMasterData objservice)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.ServiceName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetOPPhrServices", arParms);
                    List<LabServiceMasterData> lstServiceDetails = ORHelper<LabServiceMasterData>.FromDataReaderToList(sqlReader);
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
        public List<LabServiceMasterData> GetserviceListByID(LabServiceMasterData objLabServiceMaster)
        {
            List<LabServiceMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@LabGroupID", SqlDbType.Int);
                    arParms[0].Value = objLabServiceMaster.LabGroupID;

                    arParms[1] = new SqlParameter("@LabSubGroupID", SqlDbType.Int);
                    arParms[1].Value = objLabServiceMaster.LabSubGroupID;

                    arParms[2] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[2].Value = objLabServiceMaster.CurrentIndex;

                    arParms[3] = new SqlParameter("@TestID", SqlDbType.Int);
                    arParms[3].Value = objLabServiceMaster.TestID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchLabServicebyID", arParms);
                    List<LabServiceMasterData> lstLabServiceDetails = ORHelper<LabServiceMasterData>.FromDataReaderToList(sqlReader);
                    result = lstLabServiceDetails;
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
