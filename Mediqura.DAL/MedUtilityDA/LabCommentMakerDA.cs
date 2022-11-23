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
    public class LabCommentMakerDA
    {
        public int UpdateReportTemplate(LabCommentMakerData objRadioReportMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];


                    arParms[0] = new SqlParameter("@LabSubGroupID", SqlDbType.Int);
                    arParms[0].Value = objRadioReportMaster.LabSubGroupId;

                    arParms[1] = new SqlParameter("@TestID", SqlDbType.Int);
                    arParms[1].Value = objRadioReportMaster.TestId;

                    arParms[2] = new SqlParameter("@Template", SqlDbType.VarChar);
                    arParms[2].Value = objRadioReportMaster.Template;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objRadioReportMaster.EmployeeID;

                    arParms[4] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[4].Direction = ParameterDirection.Output;

                    arParms[5] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[5].Value = objRadioReportMaster.ID;

                    arParms[6] = new SqlParameter("@Gender", SqlDbType.Int);
                    arParms[6].Value = objRadioReportMaster.Gender;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_LabCommentMaster", arParms);
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
        public List<LabCommentMakerData> GetRadioReportTemplateByTestId(LabCommentMakerData objRadioReportMaster)
        {
            List<LabCommentMakerData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@LabSubGroupID", SqlDbType.Int);
                    arParms[0].Value = objRadioReportMaster.LabSubGroupId;

                    arParms[1] = new SqlParameter("@TestID", SqlDbType.Int);
                    arParms[1].Value = objRadioReportMaster.TestId;

                    arParms[2] = new SqlParameter("@Gender", SqlDbType.Int);
                    arParms[2].Value = objRadioReportMaster.Gender;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_LabTestComment", arParms);
                    List<LabCommentMakerData> ListTemplateData = ORHelper<LabCommentMakerData>.FromDataReaderToList(sqlReader);
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
        public int UpdateTestComment(LabCommentMakerData objRadioReportMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];


                    arParms[0] = new SqlParameter("@LabSubGroupID", SqlDbType.Int);
                    arParms[0].Value = objRadioReportMaster.LabSubGroupId;

                    arParms[1] = new SqlParameter("@TestID", SqlDbType.Int);
                    arParms[1].Value = objRadioReportMaster.TestId;

                    arParms[2] = new SqlParameter("@Template", SqlDbType.VarChar);
                    arParms[2].Value = objRadioReportMaster.Template;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objRadioReportMaster.EmployeeID;

                    arParms[4] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[4].Direction = ParameterDirection.Output;

                    arParms[5] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[5].Value = objRadioReportMaster.ID;

                    arParms[6] = new SqlParameter("@Gender", SqlDbType.Int);
                    arParms[6].Value = objRadioReportMaster.Gender;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_LabComment", arParms);
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
        public List<LabCommentMakerData> GetTestCommentTemplateByID(LabCommentMakerData objRadioReportMaster)
        {
            List<LabCommentMakerData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@LabSubGroupID", SqlDbType.Int);
                    arParms[0].Value = objRadioReportMaster.LabSubGroupId;

                    arParms[1] = new SqlParameter("@TestID", SqlDbType.Int);
                    arParms[1].Value = objRadioReportMaster.TestId;

                    arParms[2] = new SqlParameter("@Gender", SqlDbType.Int);
                    arParms[2].Value = objRadioReportMaster.Gender;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_LabTestComment", arParms);
                    //sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_LabTestCommentByName", arParms);
                    List<LabCommentMakerData> ListTemplateData = ORHelper<LabCommentMakerData>.FromDataReaderToList(sqlReader);
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
        public List<LabCommentMakerData> GetTestCommentList(LabCommentMakerData objRadioReportMaster)
        {
            List<LabCommentMakerData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@LabSubGroupID", SqlDbType.Int);
                    arParms[0].Value = objRadioReportMaster.LabSubGroupId;

                    arParms[1] = new SqlParameter("@TestID", SqlDbType.Int);
                    arParms[1].Value = objRadioReportMaster.TestId;

                    arParms[2] = new SqlParameter("@Gender", SqlDbType.Int);
                    arParms[2].Value = objRadioReportMaster.Gender;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objRadioReportMaster.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objRadioReportMaster.DateTo;

                    SqlDataReader sqlReader = null;
                    //sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_LabTestComment", arParms);
                    //sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_LabTestCommentByName", arParms);
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_LabTestCommentList", arParms);
                    List<LabCommentMakerData> ListTemplateData = ORHelper<LabCommentMakerData>.FromDataReaderToList(sqlReader);
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
        public List<LabCommentMakerData> GetTestCommentTemplate(LabCommentMakerData objRadioReportMaster)
        {
            List<LabCommentMakerData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objRadioReportMaster.ID;

                    //arParms[1] = new SqlParameter("@TypeFeatureID", SqlDbType.Int);
                    //arParms[1].Value = objRadioReportMaster.TypeFeatureID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_TestCommentView", arParms);
                    List<LabCommentMakerData> ListTemplateData = ORHelper<LabCommentMakerData>.FromDataReaderToList(sqlReader);
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
        public int DeleteTestComment(LabCommentMakerData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.ID;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[3].Value = objbill.HospitalID;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objbill.IPaddress;

                    arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.EmployeeID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteTestComment", arParms);
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
    }
}
