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
    public class LabTestPackageMasterDA
    {
        public int UpdateLabTestPackageDetails(LabTestPackageMasterData objLabServiceMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[12];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objLabServiceMaster.ID;

                    arParms[1] = new SqlParameter("@CompanyID", SqlDbType.Int);
                    arParms[1].Value = objLabServiceMaster.CompanyID;

                    arParms[2] = new SqlParameter("@PackageID", SqlDbType.Int);
                    arParms[2].Value = objLabServiceMaster.PackageID;

                    arParms[3] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[3].Value = objLabServiceMaster.TestName;

                    arParms[4] = new SqlParameter("@TestAmount", SqlDbType.Money);
                    arParms[4].Value = objLabServiceMaster.TestAmount;

                    arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[5].Value = objLabServiceMaster.EmployeeID;

                    arParms[6] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[6].Value = objLabServiceMaster.ActionType;

                    arParms[7] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[7].Direction = ParameterDirection.Output;

                    arParms[8] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[8].Value = objLabServiceMaster.IsActive;

                    arParms[9] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[9].Value = objLabServiceMaster.HospitalID;

                    arParms[10] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[10].Value = objLabServiceMaster.FinancialYearID;

                    arParms[11] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[11].Value = objLabServiceMaster.IPaddress;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateLabTestPackageMST", arParms);
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
        public List<LabTestPackageMasterData> GetLabTestPackageDetailsByID(LabTestPackageMasterData objLabServiceMaster)
        {
            List<LabTestPackageMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objLabServiceMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GeTLabTestPackageDetailsByID", arParms);
                    List<LabTestPackageMasterData> lstLabServiceDetails = ORHelper<LabTestPackageMasterData>.FromDataReaderToList(sqlReader);
                    result = lstLabServiceDetails;
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

        public int DeleteLabTestPackageDetailsByID(LabTestPackageMasterData objLabServiceMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objLabServiceMaster.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objLabServiceMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objLabServiceMaster.Remarks;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteLabTestPackageDetailsByID", arParms);
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

        public List<LabTestPackageMasterData> SearchLabTestPackageDetails(LabTestPackageMasterData objLabServiceMaster)
        {
            List<LabTestPackageMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@CompanyID", SqlDbType.Int);
                    arParms[0].Value = objLabServiceMaster.CompanyID;

                    arParms[1] = new SqlParameter("@PackageID", SqlDbType.Int);
                    arParms[1].Value = objLabServiceMaster.PackageID;

                    arParms[2] = new SqlParameter("@TestName", SqlDbType.VarChar);
                    arParms[2].Value = objLabServiceMaster.TestName;

                    arParms[3] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[3].Value = objLabServiceMaster.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchLabTestPackageService", arParms);
                    List<LabTestPackageMasterData> lstLabServiceDetails = ORHelper<LabTestPackageMasterData>.FromDataReaderToList(sqlReader);
                    result = lstLabServiceDetails;
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