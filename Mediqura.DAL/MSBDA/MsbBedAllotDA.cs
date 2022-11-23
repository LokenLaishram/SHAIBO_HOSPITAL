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
   public class MsbBedAllotDA
    {
       public List<MsbBedAllotData> GetMsbBedAlloted()
        {
            List<MsbBedAllotData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[0];
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_MSB_Bed_alloted", arParms);
                    List<MsbBedAllotData> listMsbBed = ORHelper<MsbBedAllotData>.FromDataReaderToList(sqlReader);
                    result = listMsbBed;
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
       public List<MsbBedAllotData> GetMsbBedDetails(MsbBedAllotData objData)
       {
           List<MsbBedAllotData> result = null;
           try
           {
               {
                   SqlParameter[] arParms = new SqlParameter[2];


                   arParms[0] = new SqlParameter("@EmployeeGradeID", SqlDbType.Int);
                   arParms[0].Value = objData.EmployeeGradeID;

                   arParms[1] = new SqlParameter("@WardID", SqlDbType.Int);
                   arParms[1].Value = objData.BedAllotedID;


                   SqlDataReader sqlReader = null;
                   sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetMsbBedDetails", arParms);
                   List<MsbBedAllotData> listMsbBed = ORHelper<MsbBedAllotData>.FromDataReaderToList(sqlReader);
                   result = listMsbBed;
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
       public int UpdateMsbBedAllotation(MsbBedAllotData objData)
       {
           int result = 0;
           try
           {
               {
                   SqlParameter[] arParms = new SqlParameter[5];

                   arParms[0] = new SqlParameter("@Output", SqlDbType.SmallInt);
                   arParms[0].Direction = ParameterDirection.Output;

                   arParms[1] = new SqlParameter("@XMLData", SqlDbType.Xml);
                   arParms[1].Value = objData.XMLData;


                   arParms[2] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                   arParms[2].Value = objData.EmployeeID;

                   arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                   arParms[3].Value = objData.HospitalID;

                   arParms[4] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                   arParms[4].Value = objData.FinancialYearID;

                   int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_MSB_Bed_allotation", arParms);
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
       public int UpdateMsbBedAllotationDetails(MsbBedAllotData objData)
       {
           int result = 0;
           try
           {
               {
                   SqlParameter[] arParms = new SqlParameter[6];

                   arParms[0] = new SqlParameter("@Output", SqlDbType.SmallInt);
                   arParms[0].Direction = ParameterDirection.Output;

                   arParms[1] = new SqlParameter("@XMLData", SqlDbType.Xml);
                   arParms[1].Value = objData.XMLData;


                   arParms[2] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                   arParms[2].Value = objData.EmployeeID;

                   arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                   arParms[3].Value = objData.HospitalID;

                   arParms[4] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                   arParms[4].Value = objData.FinancialYearID;

                   arParms[5] = new SqlParameter("@EmployeeGradeID", SqlDbType.Int);
                   arParms[5].Value = objData.EmployeeGradeID;
                   
                   int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_MSB_Bed_allotationDetails", arParms);
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
       public int DeleteMsbBedAllotationDetailsID(MsbBedAllotData objMaster)
       {
           int result = 0;
           try
           {

               SqlParameter[] arParms = new SqlParameter[3];

               arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
               arParms[0].Value = objMaster.ID;

               arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
               arParms[1].Value = objMaster.EmployeeID;

               arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
               arParms[2].Direction = ParameterDirection.Output;


               int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteMsbBedAllotationDetailsID", arParms);
               if (result_ > 0 || result_ == -1)
                   result = Convert.ToInt32(arParms[2].Value);

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
