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
    public class InsuranceReceivableDA
    {
        public List<InsuranceReceivableData> GetBillNo(InsuranceReceivableData objData)
        {
            List<InsuranceReceivableData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objData.BillNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetExtraDiscountedBillNo", arParms);
                    List<InsuranceReceivableData> lstresult = ORHelper<InsuranceReceivableData>.FromDataReaderToList(sqlReader);
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
        public List<InsuranceReceivableData> GetExtraDiscountedList(InsuranceReceivableData objpat)
        {
            List<InsuranceReceivableData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.UHID;

                    arParms[1] = new SqlParameter("@ServiceType", SqlDbType.BigInt);
                    arParms[1].Value = objpat.ServiceTypeID;

                    arParms[2] = new SqlParameter("@PatientCategory", SqlDbType.Int);
                    arParms[2].Value = objpat.PatientCategoryID;

                    arParms[3] = new SqlParameter("@SubCategory", SqlDbType.Int);
                    arParms[3].Value = objpat.SubCategoryID;

                    arParms[4] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[4].Value = objpat.BillNo;
                
                    arParms[5] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[5].Value = objpat.CurrentIndex;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_InsuranceReceivableLists", arParms);
                    List<InsuranceReceivableData> listdetails = ORHelper<InsuranceReceivableData>.FromDataReaderToList(sqlReader);
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
        public int UpdateInsuranceReceivableDetails(InsuranceReceivableData objpat)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@XmlData", SqlDbType.Xml);
                    arParms[0].Value = objpat.XMLData;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[2].Value = objpat.HospitalID;

                    arParms[3] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[3].Value = objpat.FinancialYearID;

                    arParms[4] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[4].Value = objpat.EmployeeID;

                    arParms[5] = new SqlParameter("@PaymentMode", SqlDbType.Int);
                    arParms[5].Value = objpat.PaymentMode;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateInsuranceReceivable", arParms);
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
        public List<InsuranceReceivableData> GetExtraDiscountedLists(InsuranceReceivableData objpat)
        {
            List<InsuranceReceivableData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.UHID;

                    arParms[1] = new SqlParameter("@ServiceType", SqlDbType.BigInt);
                    arParms[1].Value = objpat.ServiceTypeID;

                    arParms[2] = new SqlParameter("@PatientCategory", SqlDbType.Int);
                    arParms[2].Value = objpat.PatientCategoryID;

                    arParms[3] = new SqlParameter("@SubCategory", SqlDbType.Int);
                    arParms[3].Value = objpat.SubCategoryID;

                    arParms[4] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[4].Value = objpat.BillNo;

                    arParms[5] = new SqlParameter("@status", SqlDbType.VarChar);
                    arParms[5].Value = objpat.IsReceived;

                    arParms[6] = new SqlParameter("@receivable", SqlDbType.VarChar);
                    arParms[6].Value = objpat.Receivable;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_ExtraDiscountedInduranceReceivableList", arParms);
                    List<InsuranceReceivableData> listdetails = ORHelper<InsuranceReceivableData>.FromDataReaderToList(sqlReader);
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
  
    }
}
