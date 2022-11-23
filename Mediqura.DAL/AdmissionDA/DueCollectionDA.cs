using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.MedHrData;
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

namespace Mediqura.DAL.AdmissionDA
{
    public class DueCollectionDA
    {
        public List<DueCollectionData> GetDueCollectionList(DueCollectionData objbill)
        {
            List<DueCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@PatientCategory", SqlDbType.Int);
                    arParms[0].Value = objbill.PatientCategory;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objbill.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[2].Value = objbill.DateTo;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDueCollectionList", arParms);
                    List<DueCollectionData> listpatientdetails = ORHelper<DueCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<EmployeeData> GetRespEmployees(EmployeeData objbill)
        {
            List<EmployeeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.BillNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDueResponsibleDetails", arParms);
                    List<EmployeeData> listpatientdetails = ORHelper<EmployeeData>.FromDataReaderToList(sqlReader);
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
        public List<DueCollectionData> GetPatientDueCollectionList(DueCollectionData objbill)
        {
            List<DueCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.UHID;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objbill.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[2].Value = objbill.DateTo;

                    arParms[3] = new SqlParameter("@BillNo", SqlDbType.Int);
                    arParms[3].Value = objbill.BillNo;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objbill.EmployeeID;   
                  
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientDueList", arParms);
                    List<DueCollectionData> listpatientdetails = ORHelper<DueCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<DueCollectionData> DueDetailByBillNo(DueCollectionData objbill)
        {
            List<DueCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.BillNo;

                    arParms[1] = new SqlParameter("@PatientCategory", SqlDbType.Int);
                    arParms[1].Value = objbill.PatientCategory;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientDueByBillNo", arParms);
                    List<DueCollectionData> listpatientdetails = ORHelper<DueCollectionData>.FromDataReaderToList(sqlReader);
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
        public int UpdateDueDiscount(DueCollectionData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.BillNo;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objbill.UHID;

                    arParms[2] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[2].Value = objbill.IPNo;

                    arParms[3] = new SqlParameter("@DueBalance", SqlDbType.Money);
                    arParms[3].Value = objbill.DueBalance;

                    arParms[4] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[4].Value = objbill.HospitalID;

                    arParms[5] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[5].Value = objbill.FinancialYearID;

                    arParms[6] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[6].Value = objbill.EmployeeID;

                    arParms[7] = new SqlParameter("@PatientCategory", SqlDbType.Int);
                    arParms[7].Value = objbill.PatientCategory;

                    arParms[8] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[8].Value = objbill.XMLData;

                    arParms[9] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[9].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdatepatientDueDiscount", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[9].Value);
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
        public List<DueCollectionData> UpdateDueDetail(DueCollectionData objbill)
        {
            List<DueCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[17];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.BillNo;

                    arParms[1] = new SqlParameter("@LastDuePaid", SqlDbType.Money);
                    arParms[1].Value = objbill.LastDuePaid;

                    arParms[2] = new SqlParameter("@DueBalance", SqlDbType.Money);
                    arParms[2].Value = objbill.DueBalance;

                    arParms[3] = new SqlParameter("@PaymentMode", SqlDbType.Int);
                    arParms[3].Value = objbill.PaymentMode;

                    arParms[4] = new SqlParameter("@BankName", SqlDbType.VarChar);
                    arParms[4].Value = objbill.BankName;

                    arParms[5] = new SqlParameter("@Chequenumber", SqlDbType.VarChar);
                    arParms[5].Value = objbill.Chequenumber;

                    arParms[6] = new SqlParameter("@Invoicenumber", SqlDbType.VarChar);
                    arParms[6].Value = objbill.Invoicenumber;

                    arParms[7] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[7].Value = objbill.HospitalID;

                    arParms[8] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[8].Value = objbill.FinancialYearID;

                    arParms[9] = new SqlParameter("@BankID", SqlDbType.Int);
                    arParms[9].Value = objbill.BankID;

                    arParms[10] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[10].Value = objbill.EmployeeID;

                    arParms[11] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[11].Value = objbill.UHID;

                    arParms[12] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[12].Value = objbill.IPNo;

                    arParms[13] = new SqlParameter("@PatientCategory", SqlDbType.Int);
                    arParms[13].Value = objbill.PatientCategory;

                    arParms[14] = new SqlParameter("@SettlementMode", SqlDbType.Int);
                    arParms[14].Value = objbill.SettlementModeID;

                    arParms[15] = new SqlParameter("@CurrentDue", SqlDbType.Money);
                    arParms[15].Value = objbill.CurrentDue;

                    arParms[16] = new SqlParameter("@Paidamnt", SqlDbType.Money);
                    arParms[16].Value = objbill.Paidamnt;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdatepatientDue", arParms);
                    List<DueCollectionData> lstresult = ORHelper<DueCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<DueCollectionData> GetPatientCollectionList(DueCollectionData objbill)
        {
            List<DueCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@PatientCategory", SqlDbType.Int);
                    arParms[0].Value = objbill.PatientCategory;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objbill.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[2].Value = objbill.DateTo;

                    arParms[3] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[3].Value = objbill.CurrentIndex;

                    arParms[4] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[4].Value = objbill.IPNo;

                    arParms[5] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[5].Value = objbill.PatientName;

                    arParms[6] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[6].Value = objbill.BillNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientDueCollection", arParms);
                    List<DueCollectionData> listpatientdetails = ORHelper<DueCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<DueCollectionData> GetPatientNameList(DueCollectionData objD)
        {
            List<DueCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientName;

                    arParms[1] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[1].Value = objD.PatientType;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DuePatientNameDetails", arParms);
                    List<DueCollectionData> lstresult = ORHelper<DueCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<DueCollectionData> GetDueBillList(DueCollectionData objD)
        {
            List<DueCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.BillNo;

                    arParms[1] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[1].Value = objD.PatientType;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DuePatientBillDetail", arParms);
                    List<DueCollectionData> lstresult = ORHelper<DueCollectionData>.FromDataReaderToList(sqlReader);
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
        public List<DueCollectionData> getIPNoNemrgNoList(DueCollectionData objD)
        {
            List<DueCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;

                    arParms[1] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[1].Value = objD.PatientType;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_IPNoEmrgNoDueDetails", arParms);
                    List<DueCollectionData> lstresult = ORHelper<DueCollectionData>.FromDataReaderToList(sqlReader);
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
        public int DeleteDueCollectionDetail(DueCollectionData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@IPnoEmrgNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.IPnoEmrgNo;

                    arParms[1] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[1].Value = objbill.BillNo;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[3].Value = objbill.HospitalID;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objbill.IPaddress;

                    arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.EmployeeID;

                    arParms[6] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[6].Value = objbill.UHID;

                    arParms[7] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[7].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DueCollectionDetailsByDueBillNO", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[7].Value);
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


    }
}
