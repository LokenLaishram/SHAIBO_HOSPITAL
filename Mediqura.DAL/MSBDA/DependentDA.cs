using Mediqura.CommonData.MSBData;
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

namespace Mediqura.DAL.MSBDA
{
    public class DependentDA
    {
        public List<DepandentData> GetDependentEligibilityList(DepandentData objData)
        {
            List<DepandentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@MaritalStatus", SqlDbType.Int);
                    arParms[0].Value = objData.MaritalStatusID;

                    arParms[1] = new SqlParameter("@RelationShip", SqlDbType.VarChar);
                    arParms[1].Value = objData.Relation;

                    arParms[2] = new SqlParameter("@Age", SqlDbType.Int);
                    arParms[2].Value = objData.Age;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_MSB_Dependent_eligibility", arParms);
                    List<DepandentData> listpatientdetails = ORHelper<DepandentData>.FromDataReaderToList(sqlReader);
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
        public int UpdateDependentEligibility(DepandentData objData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[0].Direction = ParameterDirection.Output;

                    arParms[1] = new SqlParameter("@MaritalStatus", SqlDbType.Int);
                    arParms[1].Value = objData.MaritalStatusID;

                    arParms[2] = new SqlParameter("@RelationShip", SqlDbType.VarChar);
                    arParms[2].Value = objData.Relation;

                    arParms[3] = new SqlParameter("@Age", SqlDbType.Int);
                    arParms[3].Value = objData.Age;

                    arParms[4] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[4].Value = objData.EmployeeID;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[5].Value = objData.HospitalID;

                    arParms[6] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[6].Value = objData.FinancialYearID;

                    arParms[7] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[7].Value = objData.ID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_MSB_Dependent_Eligibility", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[0].Value);
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
        public int DeleteDependentEligibilityByID(DepandentData objData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objData.ID;
                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;
                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objData.Remarks;
                    arParms[3] = new SqlParameter("@UserId", SqlDbType.BigInt);
                    arParms[3].Value = objData.EmployeeID;
                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteDependentEligibilityID", arParms);
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
        public List<MsbDepandentData> GetDependentDetailByUHID(MsbDepandentData objD)
        {
            List<MsbDepandentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objD.UHID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDependentDetailsByUHID", arParms);
                    List<MsbDepandentData> lstresult = ORHelper<MsbDepandentData>.FromDataReaderToList(sqlReader);
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
        public List<MsbDepandentData> GetEmpDetailByEmpID(MsbDepandentData objD)
        {
            List<MsbDepandentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objD.EmpID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDependentEmployeeDetailsByID", arParms);
                    List<MsbDepandentData> lstresult = ORHelper<MsbDepandentData>.FromDataReaderToList(sqlReader);
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
        public List<MsbDepandentData> CheckMsbDependentEligibility(MsbDepandentData objD)
        {
            List<MsbDepandentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@EligibiltyID", SqlDbType.Int);
                    arParms[0].Value = objD.DependentRelation;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objD.UHID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_Check_MSB_Eligibility", arParms);
                    List<MsbDepandentData> lstresult = ORHelper<MsbDepandentData>.FromDataReaderToList(sqlReader);
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
        public int UpdateMsbDependent(MsbDepandentData objData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[17];

                    arParms[0] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[0].Direction = ParameterDirection.Output;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objData.UHID;

                    arParms[2] = new SqlParameter("@EmpID", SqlDbType.BigInt);
                    arParms[2].Value = objData.EmpID;

                    arParms[3] = new SqlParameter("@Income", SqlDbType.Money);
                    arParms[3].Value = objData.DependentIncome;

                    arParms[4] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[4].Value = objData.EmployeeID;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[5].Value = objData.HospitalID;

                    arParms[6] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[6].Value = objData.FinancialYearID;

                    arParms[7] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[7].Value = objData.ID;

                    arParms[8] = new SqlParameter("@DependentRelation", SqlDbType.Int);
                    arParms[8].Value = objData.DependentRelation;

                    arParms[9] = new SqlParameter("@ApplyDate", SqlDbType.DateTime);
                    arParms[9].Value = objData.Applydate;

                    arParms[10] = new SqlParameter("@IssueDate", SqlDbType.DateTime);
                    arParms[10].Value = objData.IssueDate;

                    arParms[11] = new SqlParameter("@ValidUpto", SqlDbType.DateTime);
                    arParms[11].Value = objData.ValidUpto;

                    arParms[12] = new SqlParameter("@isSpecial", SqlDbType.Int);
                    arParms[12].Value = objData.isSpecial;

                    arParms[13] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[13].Value = objData.Remarks;

                    arParms[14] = new SqlParameter("@Occupation", SqlDbType.VarChar);
                    arParms[14].Value = objData.DependentOccupation;

                    arParms[15] = new SqlParameter("@MsbType", SqlDbType.Int);
                    arParms[15].Value = objData.MsbType;

                    arParms[16] = new SqlParameter("@isActive", SqlDbType.Int);
                    arParms[16].Value = objData.Activation;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_MSB_Dependent", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[0].Value);
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
        public List<MsbDepandentData> GetDependentList(MsbDepandentData objD)
        {
            List<MsbDepandentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@EmpID", SqlDbType.BigInt);
                    arParms[0].Value = objD.EmployeeID;

                    arParms[1] = new SqlParameter("@DependentName", SqlDbType.VarChar);
                    arParms[1].Value = objD.DependentName;

                    arParms[2] = new SqlParameter("@Status", SqlDbType.Int);
                    arParms[2].Value = objD.Activation;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDependentList", arParms);
                    List<MsbDepandentData> lstresult = ORHelper<MsbDepandentData>.FromDataReaderToList(sqlReader);
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
        public int DeleteMSBDependentByID(MsbDepandentData objData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objData.ID;
                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;
                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objData.Remarks;
                    arParms[3] = new SqlParameter("@UserId", SqlDbType.BigInt);
                    arParms[3].Value = objData.EmployeeID;
                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteMSB_dependentByID", arParms);
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
