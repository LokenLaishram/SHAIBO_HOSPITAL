using Mediqura.CommonData.MedPharData;
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

namespace Mediqura.DAL.MedPharDA
{
    public class PhrDueCollectionDA
    {
        public List<PhrDueCollectionData> GetPhrCustomerDueList(PhrDueCollectionData objmedi)
        {
            List<PhrDueCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@PatientTypeID", SqlDbType.BigInt);
                    arParms[0].Value = objmedi.PatientTypeID;

                    arParms[1] = new SqlParameter("@DueReponsibleBy", SqlDbType.BigInt);
                    arParms[1].Value = objmedi.DueReponsibleBy;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[2].Value = objmedi.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[3].Value = objmedi.DateTo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phr_GetDueCustomerList", arParms);
                    List<PhrDueCollectionData> listMedidetails = ORHelper<PhrDueCollectionData>.FromDataReaderToList(sqlReader);
                    result = listMedidetails;
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

        public List<PhrDueCollectionData> GetPhrBillDetails(PhrDueCollectionData objmedi)
        {
            List<PhrDueCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objmedi.BillNo;

                    arParms[1] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[1].Value = objmedi.PatientTypeID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phr_Get_BillDetails", arParms);
                    List<PhrDueCollectionData> listcondemndetails = ORHelper<PhrDueCollectionData>.FromDataReaderToList(sqlReader);
                    result = listcondemndetails;
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
        public List<PhrDueCollectionData> UpdatePhrDueCollection(PhrDueCollectionData objdue)
        {
            List<PhrDueCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[27];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objdue.ID;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objdue.UHID;

                    arParms[2] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[2].Value = objdue.IPNo;                    

                    arParms[3] = new SqlParameter("@CustomerName", SqlDbType.VarChar);
                    arParms[3].Value = objdue.CustomerName;

                    arParms[4] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[4].Value = objdue.BillNo;

                    arParms[5] = new SqlParameter("@DueReponsibleBy", SqlDbType.BigInt);
                    arParms[5].Value = objdue.DueReponsibleBy;

                    arParms[6] = new SqlParameter("@DueReponsibleName", SqlDbType.VarChar);
                    arParms[6].Value = objdue.DueReponsibleName;

                    arParms[7] = new SqlParameter("@TotalBillAmount", SqlDbType.Money);
                    arParms[7].Value = objdue.TotalBillAmount;

                    arParms[8] = new SqlParameter("@Discount", SqlDbType.Money);
                    arParms[8].Value = objdue.Discount;

                    arParms[8] = new SqlParameter("@PaidAmount", SqlDbType.Money);
                    arParms[8].Value = objdue.PaidAmount;

                    arParms[9] = new SqlParameter("@DueAmount", SqlDbType.Money);
                    arParms[9].Value = objdue.DueAmount;

                    arParms[10] = new SqlParameter("@LastDuePaid", SqlDbType.Money);
                    arParms[10].Value = objdue.LastDuePaid;

                    arParms[11] = new SqlParameter("@DueBalance", SqlDbType.Money);
                    arParms[11].Value = objdue.DueBalance;

                    arParms[12] = new SqlParameter("@Paid", SqlDbType.Money);
                    arParms[12].Value = objdue.Paid;

                    arParms[13] = new SqlParameter("@Due", SqlDbType.Money);
                    arParms[13].Value = objdue.Due;

                    arParms[14] = new SqlParameter("@DueDiscount", SqlDbType.Money);
                    arParms[14].Value = objdue.DueDiscount;

                    arParms[15] = new SqlParameter("@DiscountRemark", SqlDbType.VarChar);
                    arParms[15].Value = objdue.DiscountRemark;

                    arParms[16] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[16].Value = objdue.EmployeeID;

                    arParms[17] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[17].Value = objdue.HospitalID;

                    arParms[18] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[18].Value = objdue.FinancialYearID;

                    arParms[19] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[19].Value = objdue.PatientTypeID;

                    arParms[20] = new SqlParameter("@IPaddress", SqlDbType.VarChar);
                    arParms[20].Value = objdue.IPaddress;

                    arParms[21] = new SqlParameter("@Output", SqlDbType.BigInt);
                    arParms[21].Direction = ParameterDirection.Output;

                    arParms[22] = new SqlParameter("@PaymentModeID", SqlDbType.Int);
                    arParms[22].Value = objdue.PaymentModeID;

                    arParms[23] = new SqlParameter("@BankID", SqlDbType.Int);
                    arParms[23].Value = objdue.BankID;

                    arParms[24] = new SqlParameter("@BankName", SqlDbType.VarChar);
                    arParms[24].Value = objdue.BankName;

                    arParms[25] = new SqlParameter("@Cheque", SqlDbType.VarChar);
                    arParms[25].Value = objdue.Cheque;

                    arParms[26] = new SqlParameter("@InvoiceNo", SqlDbType.VarChar);
                    arParms[26].Value = objdue.InvoiceNo;

                    

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phr_UpdateDueCollection", arParms);
                    List<PhrDueCollectionData> listColldetails = ORHelper<PhrDueCollectionData>.FromDataReaderToList(sqlReader);
                    result = listColldetails;

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
        //----TAB3---//
        public List<PhrDueCollectionData> GetDuePaidCustomer(PhrDueCollectionData objD)
        {
            List<PhrDueCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@CustomerName", SqlDbType.VarChar);
                    arParms[0].Value = objD.CustomerName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phr_GetDuePaidCustomer", arParms);
                    List<PhrDueCollectionData> lstresult = ORHelper<PhrDueCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<PhrDueCollectionData> GetPhrDueCollectionList(PhrDueCollectionData objduecoll)
        {
            List<PhrDueCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objduecoll.BillNo;

                    arParms[1] = new SqlParameter("@RecieptNo", SqlDbType.VarChar);
                    arParms[1].Value = objduecoll.RecieptNo;

                    arParms[2] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[2].Value = objduecoll.PatientTypeID;

                    arParms[3] = new SqlParameter("@DueReponsibleBy", SqlDbType.BigInt);
                    arParms[3].Value = objduecoll.DueReponsibleBy;

                    arParms[4] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[4].Value = objduecoll.DateFrom;

                    arParms[5] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[5].Value = objduecoll.DateTo;

                    arParms[6] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[6].Value = objduecoll.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phr_GetDueCollectionList", arParms);
                    List<PhrDueCollectionData> listMedidetails = ORHelper<PhrDueCollectionData>.FromDataReaderToList(sqlReader);
                    result = listMedidetails;
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
        public int Delete_Phr_DueCollectionRecordByID(PhrDueCollectionData objdata)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objdata.ID;

                    arParms[1] = new SqlParameter("@PatientTypeID", SqlDbType.Int);
                    arParms[1].Value = objdata.PatientTypeID;

                    arParms[2] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[2].Value = objdata.BillNo;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objdata.EmployeeID;

                    arParms[4] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[4].Value = objdata.Remarks;

                    arParms[5] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[5].Value = objdata.IPaddress;

                    arParms[6] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[6].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phr_DeleteDueCollectionRecordByID", arParms);
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
    }
}
