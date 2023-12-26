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
    public class LabAcntTranDA
    {
        public List<LabAcntTranData> UpdateLabAccountTransaction(LabAcntTranData ObjData)
        {
            List<LabAcntTranData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@TransactionTypeID", SqlDbType.Int);
                    arParms[0].Value = ObjData.TransactionTypeID;

                    arParms[1] = new SqlParameter("@TransactionAmount", SqlDbType.Money);
                    arParms[1].Value = ObjData.TransactionAmount;

                    arParms[2] = new SqlParameter("@TransactionNaration", SqlDbType.VarChar);
                    arParms[2].Value = ObjData.TransactionNaration;

                    arParms[3] = new SqlParameter("@TransactionDate", SqlDbType.DateTime);
                    arParms[3].Value = ObjData.TransactionDate;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[4].Value = ObjData.EmployeeID;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[5].Value = ObjData.HospitalID;

                    arParms[6] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[6].Value = ObjData.FinancialYearID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Lab_UpdateLabTransactionDetails", arParms);
                    List<LabAcntTranData> listexpensestran = ORHelper<LabAcntTranData>.FromDataReaderToList(sqlReader);
                    result = listexpensestran;
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
        public List<LabAcntTranData> GetIncomeTransactionList(LabAcntTranData ObjData)
        {
            List<LabAcntTranData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@TransactionTypeID", SqlDbType.Int);
                    arParms[0].Value = ObjData.TransactionTypeID;

                    arParms[1] = new SqlParameter("@dateFrom", SqlDbType.DateTime);
                    arParms[1].Value = ObjData.FromDate;

                    arParms[2] = new SqlParameter("@dateto", SqlDbType.DateTime);
                    arParms[2].Value = ObjData.ToDate;

                    arParms[3] = new SqlParameter("@AccountStatusID", SqlDbType.Int);
                    arParms[3].Value = ObjData.AccountStatusID;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = ObjData.EmployeeID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_LAB_GetIncomeTransactionList", arParms);
                    List<LabAcntTranData> listexpensestran = ORHelper<LabAcntTranData>.FromDataReaderToList(sqlReader);
                    result = listexpensestran;
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
        public List<LabAcntTranData> GetExpensesTransactionList(LabAcntTranData ObjData)
        {
            List<LabAcntTranData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@TransactionTypeID", SqlDbType.Int);
                    arParms[0].Value = ObjData.TransactionTypeID;

                    arParms[1] = new SqlParameter("@dateFrom", SqlDbType.DateTime);
                    arParms[1].Value = ObjData.FromDate;

                    arParms[2] = new SqlParameter("@dateto", SqlDbType.DateTime);
                    arParms[2].Value = ObjData.ToDate;

                    arParms[3] = new SqlParameter("@AccountStatusID", SqlDbType.Int);
                    arParms[3].Value = ObjData.AccountStatusID;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = ObjData.EmployeeID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_LAB_GetExpenditureTransactionList", arParms);
                    List<LabAcntTranData> listexpensestran = ORHelper<LabAcntTranData>.FromDataReaderToList(sqlReader);
                    result = listexpensestran;
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
         public int DeleteTransactionByVoucherNo(LabAcntTranData objbill)
        {
            int result = 0;
            try
            {
                SqlParameter[] arParms = new SqlParameter[4];

                arParms[0] = new SqlParameter("@VoucherNo", SqlDbType.VarChar);
                arParms[0].Value = objbill.VoucherNo;

                arParms[1] = new SqlParameter("@Remark", SqlDbType.VarChar);
                arParms[1].Value = objbill.Remarks;

                arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                arParms[2].Value = objbill.EmployeeID;

                arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                arParms[3].Direction = ParameterDirection.Output;

                int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteTransactionByPVNo", arParms);
                if (result_ > 0 || result_ == -1)
                    result = Convert.ToInt32(arParms[3].Value);

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
