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
    public class PackageWiseServiceDA
    {
        public int UpdatePackageServiceDetails(PackageWiseServiceData objItemMasterData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[12];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objItemMasterData.ID;

                    arParms[1] = new SqlParameter("@CompanyID", SqlDbType.Int);
                    arParms[1].Value = objItemMasterData.CompanyID;

                    arParms[2] = new SqlParameter("@packageID", SqlDbType.Int);
                    arParms[2].Value = objItemMasterData.packageID;

                    arParms[3] = new SqlParameter("@ServiceID", SqlDbType.Int);
                    arParms[3].Value = objItemMasterData.ServiceID;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objItemMasterData.EmployeeID;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[5].Value = objItemMasterData.HospitalID;

                    arParms[6] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[6].Value = objItemMasterData.ActionType;

                    arParms[7] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[7].Direction = ParameterDirection.Output;

                    arParms[8] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[8].Value = objItemMasterData.IsActive;

                    arParms[9] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[9].Value = objItemMasterData.FinancialYearID;

                    arParms[10] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[10].Value = objItemMasterData.IPaddress;

                    arParms[11] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[11].Value = objItemMasterData.Remarks;

                   
                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdatePackageServiceMST", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[8].Value);
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
        public List<PackageWiseServiceData> GetPackageServiceDetailsByID(PackageWiseServiceData objItemMaster)
        {
            List<PackageWiseServiceData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objItemMaster.ID;

                    arParms[1] = new SqlParameter("@ServiceID", SqlDbType.Int);
                    arParms[1].Value = objItemMaster.ServiceID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GeTServPackageDetailsByID", arParms);
                    List<PackageWiseServiceData> lstItemMasterDetails = ORHelper<PackageWiseServiceData>.FromDataReaderToList(sqlReader);
                    result = lstItemMasterDetails;
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
        public List<PackageWiseServiceData> GetPackageService(PackageWiseServiceData objItemMaster)
        {
            List<PackageWiseServiceData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@CompanyID", SqlDbType.Int);
                    arParms[0].Value = objItemMaster.CompanyID;

                    arParms[1] = new SqlParameter("@packageID", SqlDbType.Int);
                    arParms[1].Value = objItemMaster.packageID;

                    arParms[2] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[2].Value = objItemMaster.IsActive;

                    arParms[3] = new SqlParameter("@ServiceID", SqlDbType.Int);
                    arParms[3].Value = objItemMaster.ServiceID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_SearchPackageWise", arParms);
                    List<PackageWiseServiceData> lstItemMasterDetails = ORHelper<PackageWiseServiceData>.FromDataReaderToList(sqlReader);
                    result = lstItemMasterDetails;
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
        public int DeletePackageServiceDetailsByID(PackageWiseServiceData objItemMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objItemMaster.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[1].Value = objItemMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objItemMaster.Remarks;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteServcPackageDetailsByID", arParms);
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
    }
}
