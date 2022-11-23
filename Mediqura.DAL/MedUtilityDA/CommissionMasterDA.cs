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
   public class CommissionMasterDA
    {
       public int saveCommisionData(CommissionMasterData objMasterData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[11];

                    arParms[0] = new SqlParameter("@ServiceType", SqlDbType.Int);
                    arParms[0].Value = objMasterData.Servicetype;

                    arParms[1] = new SqlParameter("@ServiceID", SqlDbType.Int);
                    arParms[1].Value = objMasterData.ServiceID;

                    arParms[2] = new SqlParameter("@DoctorType", SqlDbType.Int);
                    arParms[2].Value = objMasterData.Doctortype;

                    arParms[3] = new SqlParameter("@DoctorID", SqlDbType.Int);
                    arParms[3].Value = objMasterData.DoctorID;

                    arParms[4] = new SqlParameter("@ServiceCharge", SqlDbType.Money);
                    arParms[4].Value = objMasterData.ServiceCharge;

                    arParms[5] = new SqlParameter("@Commision", SqlDbType.Money);
                    arParms[5].Value = objMasterData.Commission;

                    arParms[6] = new SqlParameter("@Tax", SqlDbType.Money);
                    arParms[6].Value = objMasterData.Tax;

                    arParms[7] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[7].Direction = ParameterDirection.Output;

                    arParms[8] = new SqlParameter("@AddedBy", SqlDbType.Int);
                    arParms[8].Value = objMasterData.AddedBy;

                    arParms[8] = new SqlParameter("@EmpID", SqlDbType.Int);
                    arParms[8].Value = objMasterData.EmployeeID;

                    arParms[8] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[8].Value = objMasterData.FinancialYearID;

                    arParms[8] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[8].Value = objMasterData.HospitalID;

                    arParms[9] = new SqlParameter("@ActionType", SqlDbType.SmallInt);
                    arParms[9].Value = objMasterData.ActionType;
                    arParms[10] = new SqlParameter("@CommissionID", SqlDbType.Int);
                    arParms[10].Value = objMasterData.CommissionID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateDoctorCommission", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[7].Value);
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
       public List<CommissionMasterData> SearchCommissionDetails(CommissionMasterData objMasterData)
       {
           List<CommissionMasterData> result = null;
           try
           {
               {
                   SqlParameter[] arParms = new SqlParameter[5];
                   arParms[0] = new SqlParameter("@ServiceType", SqlDbType.Int);
                   arParms[0].Value = objMasterData.Servicetype;

                   arParms[1] = new SqlParameter("@ServiceID", SqlDbType.Int);
                   arParms[1].Value = objMasterData.ServiceID;

                   arParms[2] = new SqlParameter("@DoctorType", SqlDbType.Int);
                   arParms[2].Value = objMasterData.Doctortype;

                   arParms[3] = new SqlParameter("@DoctorID", SqlDbType.Int);
                   arParms[3].Value = objMasterData.DoctorID;

                   arParms[4] = new SqlParameter("@ServiceCharge", SqlDbType.Money);
                   arParms[4].Value = objMasterData.ServiceCharge;
  
                   SqlDataReader sqlReader = null;
                   sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetDoctorCommissionDetails", arParms);
                   List<CommissionMasterData> listCommissiondetails = ORHelper<CommissionMasterData>.FromDataReaderToList(sqlReader);
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
       public List<CommissionMasterData> GetCommissionDeatilbyID(CommissionMasterData objMasterData)
       {
           List<CommissionMasterData> result = null;
           try
           {
               {
                   SqlParameter[] arParms = new SqlParameter[1];

                   arParms[0] = new SqlParameter("@CommisionID", SqlDbType.BigInt);
                   arParms[0].Value = objMasterData.CommissionID;

                   SqlDataReader sqlReader = null;
                   sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetCommissionDetailsByID", arParms);
                   List<CommissionMasterData> listCommissiondetails = ORHelper<CommissionMasterData>.FromDataReaderToList(sqlReader);
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
       public int DeleteCommissionDetailsByID(CommissionMasterData objMasterData)
       {
           int result = 0;
           try
           {
               {
                   SqlParameter[] arParms = new SqlParameter[4];

                   arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                   arParms[0].Value = objMasterData.CommissionID;
                   arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                   arParms[1].Direction = ParameterDirection.Output;
                   arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                   arParms[2].Value = objMasterData.Remarks;
                   arParms[3] = new SqlParameter("@UserId", SqlDbType.BigInt);
                   arParms[3].Value = objMasterData.EmployeeID;
                   int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteCommissionbyID", arParms);
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
