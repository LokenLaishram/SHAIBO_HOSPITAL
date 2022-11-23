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
   public class EmployeeTypeDA
    {
       public List<EmployeeTypeData> Getemplyeetypelist(EmployeeTypeData objemployee)
       {
           List<EmployeeTypeData> result = null;
           try
           {
               {
                   SqlParameter[] arParms = new SqlParameter[0];
                   SqlDataReader sqlReader = null;
                   sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmployeetypelist", arParms);
                   List<EmployeeTypeData> listpatientdetails = ORHelper<EmployeeTypeData>.FromDataReaderToList(sqlReader);
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
       public int UpdateEmployeeMSBeligibility(EmployeeTypeData objadm)
       {
           int result = 0;
           try
           {
               {
                   SqlParameter[] arParms = new SqlParameter[2];

                   arParms[0] = new SqlParameter("@Output", SqlDbType.SmallInt);
                   arParms[0].Direction = ParameterDirection.Output;

                   arParms[1] = new SqlParameter("@XMLData", SqlDbType.Xml);
                   arParms[1].Value = objadm.XMLData;

                   int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_Employee_MSB_Eligibility", arParms);
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
       public int UpdateEmployeeMSBapplicability(EmployeeDependentData objData)
       {
           int result = 0;
           try
           {
               {
                   SqlParameter[] arParms = new SqlParameter[4];

                   arParms[0] = new SqlParameter("@Output", SqlDbType.SmallInt);
                   arParms[0].Direction = ParameterDirection.Output;

                   arParms[1] = new SqlParameter("@dependent", SqlDbType.Int);
                   arParms[1].Value = objData.dependent;

                   arParms[2] = new SqlParameter("@exclusion", SqlDbType.Int);
                   arParms[2].Value = objData.IsExclution;

                   arParms[3] = new SqlParameter("@modifyBy", SqlDbType.BigInt);
                   arParms[3].Value = objData.AddedBy;

                   int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_Employee_MSB_Appicablility", arParms);
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
       public List<EmployeeDependentData> GetMsbApplicability()
       {
           List<EmployeeDependentData> result = null;
           try
           {
               {
                   SqlParameter[] arParms = new SqlParameter[0];
                   SqlDataReader sqlReader = null;
                   sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_MSB_Applicability", arParms);
                   List<EmployeeDependentData> list = ORHelper<EmployeeDependentData>.FromDataReaderToList(sqlReader);
                   result = list;
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
       public List<BenefitData> GetMsbBenefit()
       {
           List<BenefitData> result = null;
           try
           {
               {
                   SqlParameter[] arParms = new SqlParameter[0];
                   SqlDataReader sqlReader = null;
                   sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_MSB_Benefit", arParms);
                   List<BenefitData> list = ORHelper<BenefitData>.FromDataReaderToList(sqlReader);
                   result = list;
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
       public int UpdateMsbBenefit(BenefitData objadm)
       {
           int result = 0;
           try
           {
               {
                   SqlParameter[] arParms = new SqlParameter[2];

                   arParms[0] = new SqlParameter("@Output", SqlDbType.SmallInt);
                   arParms[0].Direction = ParameterDirection.Output;

                   arParms[1] = new SqlParameter("@XMLData", SqlDbType.Xml);
                   arParms[1].Value = objadm.XMLData;

                   int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_MSB_benefit", arParms);
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
    }
}
