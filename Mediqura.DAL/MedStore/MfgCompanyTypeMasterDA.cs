using Mediqura.CommonData.MedStore;
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

namespace Mediqura.DAL.MedStore
{
    public class MfgCompanyTypeMasterDA
    {
        public int UpdateCompanyTypeDetails(MfgCompanyMasterData objCompanyMasterData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objCompanyMasterData.ID;

                    arParms[1] = new SqlParameter("@MfgCompanyTypeCode", SqlDbType.VarChar);
                    arParms[1].Value = objCompanyMasterData.MfgCompanyTypeCode;

                    arParms[2] = new SqlParameter("@MfgCompanyType", SqlDbType.VarChar);
                    arParms[2].Value = objCompanyMasterData.MfgCompanyType;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objCompanyMasterData.EmployeeID;

                    arParms[4] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[4].Value = objCompanyMasterData.HospitalID;

                    arParms[5] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[5].Value = objCompanyMasterData.ActionType;

                    arParms[6] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[6].Direction = ParameterDirection.Output;

                    arParms[7] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[7].Value = objCompanyMasterData.IsActive;

                    arParms[8] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[8].Value = objCompanyMasterData.FinancialYearID;

                    arParms[9] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[9].Value = objCompanyMasterData.IPaddress;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateMfgCompanyMasterMST", arParms);
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
        public List<MfgCompanyMasterData> GetCompanyTypeDetailsByID(MfgCompanyMasterData objcompanyTypeMaster)
        {
            List<MfgCompanyMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objcompanyTypeMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GeTCompanyTypeDetailsByID", arParms);
                    List<MfgCompanyMasterData> lstItemSubGroupTypeDetails = ORHelper<MfgCompanyMasterData>.FromDataReaderToList(sqlReader);
                    result = lstItemSubGroupTypeDetails;
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
        public int DeleteMfgCompanyTypeDetailsByID(MfgCompanyMasterData objCompanyTypeMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objCompanyTypeMaster.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[1].Value = objCompanyTypeMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objCompanyTypeMaster.Remarks;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objCompanyTypeMaster.IPaddress;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.VarChar);
                    arParms[5].Value = objCompanyTypeMaster.HospitalID;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteCompanyTypeDetailsByID", arParms);
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
        public List<MfgCompanyMasterData> SearchCompanyTypeExcel(MfgCompanyMasterData objCompanyTypeMaster)
        {
            List<MfgCompanyMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@MfgCompanyTypeCode", SqlDbType.VarChar);
                    arParms[0].Value = objCompanyTypeMaster.MfgCompanyTypeCode;

                    arParms[1] = new SqlParameter("@MfgCompanyType", SqlDbType.VarChar);
                    arParms[1].Value = objCompanyTypeMaster.MfgCompanyType;

                    arParms[2] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[2].Value = objCompanyTypeMaster.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_SearchCompanyExcel", arParms);
                    List<MfgCompanyMasterData> lstItemSubGroupTypeDetails = ORHelper<MfgCompanyMasterData>.FromDataReaderToList(sqlReader);
                    result = lstItemSubGroupTypeDetails;
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
        public List<MfgCompanyMasterData> SearchCompanyMasterDetails(MfgCompanyMasterData objCompanyTypeMaster)
        {
            List<MfgCompanyMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@MfgCompanyTypeCode", SqlDbType.VarChar);
                    arParms[0].Value = objCompanyTypeMaster.MfgCompanyTypeCode;

                    arParms[1] = new SqlParameter("@MfgCompanyType", SqlDbType.VarChar);
                    arParms[1].Value = objCompanyTypeMaster.MfgCompanyType;

                    arParms[2] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[2].Value = objCompanyTypeMaster.IsActive;

                    arParms[3] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[3].Value = objCompanyTypeMaster.CurrentIndex;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_SearchCompanyType", arParms);
                    List<MfgCompanyMasterData> lstItemSubGroupTypeDetails = ORHelper<MfgCompanyMasterData>.FromDataReaderToList(sqlReader);
                    result = lstItemSubGroupTypeDetails;
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
