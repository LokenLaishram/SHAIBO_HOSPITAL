using Mediqura.CommonData.MedStore;
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

namespace Mediqura.DAL.MedStore
{
    public class GenSupplierTypeMasterDA
    {
        public int UpdateSupplierTypeMasterDetails(GenSupplierTypeMasterData objSupplierMasterData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[12];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objSupplierMasterData.ID;

                    arParms[1] = new SqlParameter("@SupplierTypeCode", SqlDbType.VarChar);
                    arParms[1].Value = objSupplierMasterData.SupplierTypeCode;

                    arParms[2] = new SqlParameter("@SupplierType", SqlDbType.VarChar);
                    arParms[2].Value = objSupplierMasterData.SupplierType;

                    arParms[3] = new SqlParameter("@ContactNo", SqlDbType.VarChar);
                    arParms[3].Value = objSupplierMasterData.ContactNo;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objSupplierMasterData.EmployeeID;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[5].Value = objSupplierMasterData.HospitalID;

                    arParms[6] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[6].Value = objSupplierMasterData.ActionType;

                    arParms[7] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[7].Direction = ParameterDirection.Output;

                    arParms[8] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[8].Value = objSupplierMasterData.IsActive;

                    arParms[9] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[9].Value = objSupplierMasterData.FinancialYearID;

                    arParms[10] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[10].Value = objSupplierMasterData.IPaddress;

                    arParms[11] = new SqlParameter("@SupplierAddress", SqlDbType.VarChar);
                    arParms[11].Value = objSupplierMasterData.SupplierAddress;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_UpdateSupplierMasterMST", arParms);
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
        public List<GenSupplierTypeMasterData> GetISupplierMasterDetailsByID(GenSupplierTypeMasterData objItemSubGroupTypeMaster)
        {
            List<GenSupplierTypeMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objItemSubGroupTypeMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_GeTSupplierTypeDetailsByID", arParms);
                    List<GenSupplierTypeMasterData> lstItemSubGroupTypeDetails = ORHelper<GenSupplierTypeMasterData>.FromDataReaderToList(sqlReader);
                    result = lstItemSubGroupTypeDetails;
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
        public int DeleteSupplierMasterDetailsByID(GenSupplierTypeMasterData objTypeMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objTypeMaster.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[1].Value = objTypeMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objTypeMaster.Remarks;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_DeleteSupplierTypeDetailsByID", arParms);
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
        public List<GenSupplierTypeMasterData> SearchSupplierTypeExcel(GenSupplierTypeMasterData objTypeMaster)
        {
            List<GenSupplierTypeMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@SupplierTypeCode", SqlDbType.VarChar);
                    arParms[0].Value = objTypeMaster.SupplierTypeCode;

                    arParms[1] = new SqlParameter("@SupplierType", SqlDbType.VarChar);
                    arParms[1].Value = objTypeMaster.SupplierType;

                    arParms[2] = new SqlParameter("@ContactNo", SqlDbType.VarChar);
                    arParms[2].Value = objTypeMaster.ContactNo;

                    arParms[3] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[3].Value = objTypeMaster.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_SearchSupplierExcel", arParms);
                    List<GenSupplierTypeMasterData> lstItemSubGroupTypeDetails = ORHelper<GenSupplierTypeMasterData>.FromDataReaderToList(sqlReader);
                    result = lstItemSubGroupTypeDetails;
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
        public List<GenSupplierTypeMasterData> SearchSupplierMasterDetails(GenSupplierTypeMasterData objItemSubGroupTypeMaster)
        {
            List<GenSupplierTypeMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@SupplierTypeCode", SqlDbType.VarChar);
                    arParms[0].Value = objItemSubGroupTypeMaster.SupplierTypeCode;

                    arParms[1] = new SqlParameter("@SupplierType", SqlDbType.VarChar);
                    arParms[1].Value = objItemSubGroupTypeMaster.SupplierType;

                    arParms[2] = new SqlParameter("@ContactNo", SqlDbType.VarChar);
                    arParms[2].Value = objItemSubGroupTypeMaster.ContactNo;

                    arParms[3] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[3].Value = objItemSubGroupTypeMaster.IsActive;

                    arParms[4] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[4].Value = objItemSubGroupTypeMaster.CurrentIndex;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_SearchSupplierType", arParms);
                    List<GenSupplierTypeMasterData> lstItemSubGroupTypeDetails = ORHelper<GenSupplierTypeMasterData>.FromDataReaderToList(sqlReader);
                    result = lstItemSubGroupTypeDetails;
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
