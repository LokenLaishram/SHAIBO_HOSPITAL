using Mediqura.CommonData.MedUtilityData;
using Mediqura.CommonData.MedBankData;
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

namespace Mediqura.DAL.MedBankDA
{
    public class BankMasterDA
    {
         public int UpdateBankChkBookIssueDetails(BankMasterData objBankMasterMaster)
        {
            int result = 0;
            try
            {

                {
                    SqlParameter[] arParms = new SqlParameter[11];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objBankMasterMaster.ID;

                    arParms[1] = new SqlParameter("@IssueDate", SqlDbType.DateTime);
                    arParms[1].Value = objBankMasterMaster.IssueDate;

                    arParms[2] = new SqlParameter("@ChequeNoFrom", SqlDbType.Int);
                    arParms[2].Value = objBankMasterMaster.ChequeNoFrom;

                    arParms[3] = new SqlParameter("@ChequeNoTo", SqlDbType.Int);
                    arParms[3].Value = objBankMasterMaster.ChequeNoTo;

                    arParms[4] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[4].Value = objBankMasterMaster.HospitalID;

                    arParms[5] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[5].Value = objBankMasterMaster.ActionType;

                    arParms[6] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[6].Direction = ParameterDirection.Output;

                    arParms[7] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[7].Value = objBankMasterMaster.IsActive;

                    arParms[8] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[8].Value = objBankMasterMaster.IPaddress;

                    arParms[9] = new SqlParameter("@BankID", SqlDbType.Int);
                    arParms[9].Value = objBankMasterMaster.BankID;

                    arParms[10] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[10].Value = objBankMasterMaster.EmployeeID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_CheckbookIssue", arParms);
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
        public List<BankMasterData> SearchBankChkBookIssueDetails(BankMasterData objBankMasterMaster)
        {
            List<BankMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@BankID", SqlDbType.Int);
                    arParms[0].Value = objBankMasterMaster.BankID;

                    arParms[1] = new SqlParameter("@IssueDate", SqlDbType.DateTime);
                    arParms[1].Value = objBankMasterMaster.IssueDate;

                    arParms[2] = new SqlParameter("@ChequeNoFrom", SqlDbType.Int);
                    arParms[2].Value = objBankMasterMaster.ChequeNoFrom;

                    arParms[3] = new SqlParameter("@ChequeNoTo", SqlDbType.Int);
                    arParms[3].Value = objBankMasterMaster.ChequeNoTo;

                    arParms[4] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[4].Value = objBankMasterMaster.IsActive;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchCheckBookIssue", arParms);
                    List<BankMasterData> lstBankMasterDetails = ORHelper<BankMasterData>.FromDataReaderToList(sqlReader);
                    result = lstBankMasterDetails;
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
        public List<BankMasterData> GetBankChkBookIssueDetailsByID(BankMasterData objBankMasterMaster)
        {
            List<BankMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objBankMasterMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetBankChequeBookByID", arParms);
                    List<BankMasterData> lstLabUnitDetails = ORHelper<BankMasterData>.FromDataReaderToList(sqlReader);
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
        public int DeleteBankChkBookIssueDetailsByID(BankMasterData objBankMasterMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objBankMasterMaster.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objBankMasterMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objBankMasterMaster.Remarks;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objBankMasterMaster.IPaddress;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteChequeBookIssueByID", arParms);
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
