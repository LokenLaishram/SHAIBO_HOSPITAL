using Mediqura.CommonData.MedBillData;
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

namespace Mediqura.DAL.MedBillDA
{
    public class DoctorWiseDailyCollectionDA
    {
        public List<DoctorWiseCollectionMasterData> GetDoctorsDailyCollectionList(DoctorWiseCollectionMasterData objMasterData)
        {
            List<DoctorWiseCollectionMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[0].Value = objMasterData.DepartmentID;

                    arParms[1] = new SqlParameter("@DoctorType", SqlDbType.Int);
                    arParms[1].Value = objMasterData.Doctortype;

                    arParms[2] = new SqlParameter("@DoctorID", SqlDbType.Int);
                    arParms[2].Value = objMasterData.DoctorID;

                    arParms[3] = new SqlParameter("@ServiceType", SqlDbType.Int);
                    arParms[3].Value = objMasterData.Servicetype;

                    arParms[4] = new SqlParameter("@ServiceID", SqlDbType.Int);
                    arParms[4].Value = objMasterData.ServiceID;

                    arParms[5] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[5].Value = objMasterData.DateFrom;

                    arParms[6] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[6].Value = objMasterData.DateTo;

                    arParms[7] = new SqlParameter("@Month", SqlDbType.Int);
                    arParms[7].Value = objMasterData.Month;

                    arParms[8] = new SqlParameter("@paid", SqlDbType.Int);
                    arParms[8].Value = objMasterData.paid;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDoctorWiseCollectioList", arParms);
                    List<DoctorWiseCollectionMasterData> listCommissiondetails = ORHelper<DoctorWiseCollectionMasterData>.FromDataReaderToList(sqlReader);
                    result = listCommissiondetails;
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
        public List<DoctorWiseCollectionMasterData> GetDoctorspaymentCollectionList(DoctorWiseCollectionMasterData objMasterData)
        {
            List<DoctorWiseCollectionMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[0].Value = objMasterData.DepartmentID;

                    arParms[1] = new SqlParameter("@DoctorType", SqlDbType.Int);
                    arParms[1].Value = objMasterData.Doctortype;

                    arParms[2] = new SqlParameter("@DoctorID", SqlDbType.Int);
                    arParms[2].Value = objMasterData.DoctorID;

                    arParms[3] = new SqlParameter("@ServiceType", SqlDbType.Int);
                    arParms[3].Value = objMasterData.Servicetype;

                    arParms[4] = new SqlParameter("@ServiceID", SqlDbType.Int);
                    arParms[4].Value = objMasterData.ServiceID;

                    arParms[5] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[5].Value = objMasterData.DateFrom;

                    arParms[6] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[6].Value = objMasterData.DateTo;

                    arParms[7] = new SqlParameter("@Month", SqlDbType.Int);
                    arParms[7].Value = objMasterData.Month;

                    arParms[8] = new SqlParameter("@paid", SqlDbType.Int);
                    arParms[8].Value = objMasterData.paid;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDoctorWisePaymentList", arParms);
                    List<DoctorWiseCollectionMasterData> listCommissiondetails = ORHelper<DoctorWiseCollectionMasterData>.FromDataReaderToList(sqlReader);
                    result = listCommissiondetails;
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
        public List<DoctorWiseCollectionMasterData> GetDoctorspaymentHistory(DoctorWiseCollectionMasterData objMasterData)
        {
            List<DoctorWiseCollectionMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[0].Value = objMasterData.DepartmentID;

                    arParms[1] = new SqlParameter("@DoctorType", SqlDbType.Int);
                    arParms[1].Value = objMasterData.Doctortype;

                    arParms[2] = new SqlParameter("@DoctorID", SqlDbType.Int);
                    arParms[2].Value = objMasterData.DoctorID;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objMasterData.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objMasterData.DateTo;

                    arParms[5] = new SqlParameter("@Month", SqlDbType.Int);
                    arParms[5].Value = objMasterData.Month;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDoctorWisePaymentHistory", arParms);
                    List<DoctorWiseCollectionMasterData> listCommissiondetails = ORHelper<DoctorWiseCollectionMasterData>.FromDataReaderToList(sqlReader);
                    result = listCommissiondetails;
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
     
        public int UpdateCollectionVerification(DoctorWiseCollectionMasterData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objbill.XMLData;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.BigInt);
                    arParms[1].Direction = ParameterDirection.Output;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_CommonFooter", arParms);
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
        public int UpdateDoctorPaymentCollection(DoctorWiseCollectionMasterData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objbill.XMLData;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.BigInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@TotalPayable", SqlDbType.Money);
                    arParms[2].Value = objbill.TotalPayable;

                    arParms[3] = new SqlParameter("@TotalAmount", SqlDbType.Money);
                    arParms[3].Value = objbill.TotalAmount;

                    arParms[4] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[4].Value = objbill.HospitalID;

                    arParms[5] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[5].Value = objbill.AddedByID;

                    arParms[6] = new SqlParameter("@FinancialID", SqlDbType.Int);
                    arParms[6].Value = objbill.FinancialYearID;

                    arParms[7] = new SqlParameter("@DoctorID", SqlDbType.Int);
                    arParms[7].Value = objbill.DoctorID;

                    arParms[8] = new SqlParameter("@DoctorTypelID", SqlDbType.Int);
                    arParms[8].Value = objbill.Doctortype;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_Doctor_payment", arParms);
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
