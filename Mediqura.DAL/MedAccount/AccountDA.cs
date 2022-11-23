using Mediqura.CommonData.MedAccount;
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

namespace Mediqura.DAL.MedAccount
{
  public  class AccountDA
    {
      public int UpdateAccountGroup(AcountData objData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@GroupName", SqlDbType.VarChar);
                    arParms[0].Value = objData.GroupName;

                    arParms[1] = new SqlParameter("@GroupUnderID", SqlDbType.Int);
                    arParms[1].Value = objData.GroupUnderID;

                    arParms[2] = new SqlParameter("@GroupNatureID", SqlDbType.Int);
                    arParms[2].Value = objData.NatureID;

                    arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[3].Direction = ParameterDirection.Output;

                    arParms[4] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[4].Value = objData.EmployeeID;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[5].Value = objData.HospitalID;

                    arParms[6] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[6].Value = objData.FinancialYearID;

                    arParms[7] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[7].Value = objData.ID;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_AccountGroup", arParms);
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
      public List<AccountGroupMasterData> GetAccntGrpList(AcountData objData)
        {
            List<AccountGroupMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@GroupName", SqlDbType.VarChar);
                    arParms[0].Value = objData.GroupName;

                    arParms[1] = new SqlParameter("@GroupUnderID", SqlDbType.Int);
                    arParms[1].Value = objData.GroupUnderID;

                    arParms[2] = new SqlParameter("@GroupNatureID", SqlDbType.Int);
                    arParms[2].Value = objData.NatureID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetAccountgroupList", arParms);
                    List<AccountGroupMasterData> listpatientdetails = ORHelper<AccountGroupMasterData>.FromDataReaderToList(sqlReader);
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
      public int DeleteAccountGroupByID(AcountData objpat)
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
                  int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteAccntGrpbyID", arParms);
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
      public int UpdateAccountLedger(AcountLedgerData objData)
      {
          int result = 0;
          try
          {
              {
                  SqlParameter[] arParms = new SqlParameter[11];

                  arParms[0] = new SqlParameter("@AccountName", SqlDbType.VarChar);
                  arParms[0].Value = objData.AccountName;

                  arParms[1] = new SqlParameter("@GroupUnderID", SqlDbType.Int);
                  arParms[1].Value = objData.GroupUnderID;

                  arParms[2] = new SqlParameter("@GroupNatureID", SqlDbType.Int);
                  arParms[2].Value = objData.NatureID;

                  arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                  arParms[3].Direction = ParameterDirection.Output;

                  arParms[4] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                  arParms[4].Value = objData.EmployeeID;

                  arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                  arParms[5].Value = objData.HospitalID;

                  arParms[6] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                  arParms[6].Value = objData.FinancialYearID;

                  arParms[7] = new SqlParameter("@ID", SqlDbType.Int);
                  arParms[7].Value = objData.ID;

                  arParms[8] = new SqlParameter("@Site", SqlDbType.VarChar);
                  arParms[8].Value = objData.Site;

                  arParms[9] = new SqlParameter("@OpnBalance", SqlDbType.Money);
                  arParms[9].Value = objData.Opnbal;

                  arParms[10] = new SqlParameter("@OpnDate", SqlDbType.DateTime);
                  arParms[10].Value = objData.OpnDate;


                  int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_AccountLedger", arParms);
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
      public List<AccountLedgerMasterData> GetAccntLedgerList(AcountLedgerData objData)
      {
          List<AccountLedgerMasterData> result = null;
          try
          {
              {
                  SqlParameter[] arParms = new SqlParameter[5];

                  arParms[0] = new SqlParameter("@AccountName", SqlDbType.VarChar);
                  arParms[0].Value = objData.AccountName;

                  arParms[1] = new SqlParameter("@GroupUnderID", SqlDbType.Int);
                  arParms[1].Value = objData.GroupUnderID;

                  arParms[2] = new SqlParameter("@GroupNatureID", SqlDbType.Int);
                  arParms[2].Value = objData.NatureID;

                  arParms[3] = new SqlParameter("@Site", SqlDbType.VarChar);
                  arParms[3].Value = objData.Site;

                  arParms[4] = new SqlParameter("@OpnBalance", SqlDbType.Money);
                  arParms[4].Value = objData.Opnbal;

                  SqlDataReader sqlReader = null;
                  sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetAccountLedgerList", arParms);
                  List<AccountLedgerMasterData> listpatientdetails = ORHelper<AccountLedgerMasterData>.FromDataReaderToList(sqlReader);
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
      public int DeleteAccountLedgerByID(AcountLedgerData objpat)
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
                  int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteAccntLedgerbyID", arParms);
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

      public List<AccountMappingMasterData> GetAcntServiceMappingList(AccountMappingMasterData objData)
      {
          List<AccountMappingMasterData> result = null;
          try
          {
              {
                  SqlParameter[] arParms = new SqlParameter[4];

                  arParms[0] = new SqlParameter("@ServiceType", SqlDbType.Int);
                  arParms[0].Value = objData.ServiceType;

                  arParms[1] = new SqlParameter("@subServicetype", SqlDbType.Int);
                  arParms[1].Value = objData.SubServiceType;

                  arParms[2] = new SqlParameter("@MappingType", SqlDbType.Int);
                  arParms[2].Value = objData.MappingType;

                  arParms[3] = new SqlParameter("@GroupType", SqlDbType.Int);
                  arParms[3].Value = objData.GroupType;

                    SqlDataReader sqlReader = null;
                  sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetServiceMappingAccount", arParms);
                  List<AccountMappingMasterData> listpatientdetails = ORHelper<AccountMappingMasterData>.FromDataReaderToList(sqlReader);
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
      public int UpdateAccntMappingMaster(AccountMappingMasterData objData)
      {
          int result = 0;
          try
          {
              {
                  SqlParameter[] arParms = new SqlParameter[5];

                  arParms[0] = new SqlParameter("@XmlData", SqlDbType.Xml);
                  arParms[0].Value = objData.XMLData;

                  arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                  arParms[1].Direction = ParameterDirection.Output;

                  arParms[2] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                  arParms[2].Value = objData.EmployeeID;

                  arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                  arParms[3].Value = objData.HospitalID;

                  arParms[4] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                  arParms[4].Value = objData.FinancialYearID;



                  int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_AccountMappingMaster", arParms);
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
      public List<AcountLedgerData> SearchLedgerByName(AcountLedgerData objLedger)
      {
          List<AcountLedgerData> result = null;
          try
          {
              {
                  SqlParameter[] arParms = new SqlParameter[2];

                  arParms[0] = new SqlParameter("@LedgerName", SqlDbType.VarChar);
                  arParms[0].Value = objLedger.AccountName;

                  arParms[1] = new SqlParameter("@PaymentType", SqlDbType.Int);
                  arParms[1].Value = objLedger.Payment_type;

                    SqlDataReader sqlReader = null;
                  sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_SearchLedgerByName", arParms);
                  List<AcountLedgerData> lstServiceDetails = ORHelper<AcountLedgerData>.FromDataReaderToList(sqlReader);
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
      public AccountTransactionOutput UpdateAccountTransaction(AccountTransactionData objData)
      {

          AccountTransactionOutput outputdata = new AccountTransactionOutput();
          
          try
          {
              {
                  SqlParameter[] arParms = new SqlParameter[18];

                  arParms[0] = new SqlParameter("@DebitXml", SqlDbType.Xml);
                  arParms[0].Value = objData.DebitXml;

                  arParms[1] = new SqlParameter("@CreditXml", SqlDbType.Xml);
                  arParms[1].Value = objData.CreditXml;
                  
                  arParms[2] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                  arParms[2].Value = objData.EmployeeID;

                  arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                  arParms[3].Value = objData.HospitalID;

                  arParms[4] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                  arParms[4].Value = objData.FinancialYearID;

                  arParms[5] = new SqlParameter("@TotalDebit", SqlDbType.Money);
                  arParms[5].Value = objData.TotalDebit;

                  arParms[6] = new SqlParameter("@TotalCredit", SqlDbType.Money);
                  arParms[6].Value = objData.TotalCredit;

                  arParms[7] = new SqlParameter("@VoucherNo", SqlDbType.VarChar);
                  arParms[7].Value = objData.VoucherNo;

                  arParms[8] = new SqlParameter("@Date", SqlDbType.DateTime);
                  arParms[8].Value = objData.Date;

                  arParms[9] = new SqlParameter("@Naration", SqlDbType.VarChar);
                  arParms[9].Value = objData.Naration;

                  arParms[10] = new SqlParameter("@PaymentType", SqlDbType.Int);
                  arParms[10].Value = objData.PaymentType;

                  arParms[11] = new SqlParameter("@PaymentMode", SqlDbType.Int);
                  arParms[11].Value = objData.PaymentMode;

                  arParms[12] = new SqlParameter("@TransactionType", SqlDbType.Int);
                  arParms[12].Value = objData.TransactionType;

                  arParms[13] = new SqlParameter("@InstrumentName", SqlDbType.VarChar);
                  arParms[13].Value = objData.InstrumentName;

                  arParms[14] = new SqlParameter("@InstrumentDate", SqlDbType.DateTime);
                  arParms[14].Value = objData.InstrumentDate;

                  arParms[15] = new SqlParameter("@BankPayeeName", SqlDbType.VarChar);
                  arParms[15].Value = objData.BankPayeeName;

                  arParms[16] = new SqlParameter("@BankBranchName", SqlDbType.VarChar);
                  arParms[16].Value = objData.BankBranchName;

                  arParms[17] = new SqlParameter("@CrossInt", SqlDbType.VarChar);
                  arParms[17].Value = objData.CrossInt;

                  SqlDataReader sqlReader = null;
                  sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_Account_transaction", arParms);
                  List<AccountTransactionOutput> output = ORHelper<AccountTransactionOutput>.FromDataReaderToList(sqlReader);

                  outputdata.outputdata = Convert.ToInt32(output[0].outputdata);
                  outputdata.voucher = output[0].voucher;
                  
              }
          }
          catch (Exception ex) //Exception of the business layer(itself)//unhandle
          {
              PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
              LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
              throw new DataAccessException("5000001", ex);
          }
          return outputdata;
      }
        public AccountTransactionOutput UpdateIncomeTransaction(AccountTransactionData objData)
        {

            AccountTransactionOutput outputdata = new AccountTransactionOutput();

            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@DebitID", SqlDbType.Int);
                    arParms[0].Value = objData.DebitID;
                    
                    arParms[1] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[1].Value = objData.EmployeeID;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[2].Value = objData.HospitalID;

                    arParms[3] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[3].Value = objData.FinancialYearID;

                    arParms[4] = new SqlParameter("@TotalAmount", SqlDbType.Money);
                    arParms[4].Value = objData.TotalDebit;
                    
                    arParms[5] = new SqlParameter("@Naration", SqlDbType.VarChar);
                    arParms[5].Value = objData.Naration;
                    
                    arParms[6] = new SqlParameter("@PaymentMode", SqlDbType.Int);
                    arParms[6].Value = objData.PaymentMode;
                    
                    arParms[7] = new SqlParameter("@BankName", SqlDbType.VarChar);
                    arParms[7].Value = objData.BankName;

                    arParms[8] = new SqlParameter("@Cheque", SqlDbType.VarChar);
                    arParms[8].Value = objData.Cheque;

                    arParms[9] = new SqlParameter("@Invoicenumber", SqlDbType.VarChar);
                    arParms[9].Value = objData.Invoicenumber;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_Income_transaction", arParms);
                    List<AccountTransactionOutput> output = ORHelper<AccountTransactionOutput>.FromDataReaderToList(sqlReader);

                    outputdata.outputdata = Convert.ToInt32(output[0].outputdata);
                    outputdata.voucher = output[0].voucher;

                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return outputdata;
        }


    }
}
