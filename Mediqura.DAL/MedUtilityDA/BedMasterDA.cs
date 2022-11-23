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
    public class BedMasterDA
    {
        public int UpdateBedDetails(BedMasterData objBlockMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[15];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objBlockMaster.ID;

                    arParms[1] = new SqlParameter("@BlockID", SqlDbType.Int);
                    arParms[1].Value = objBlockMaster.BlockID;

                    arParms[2] = new SqlParameter("@FloorID", SqlDbType.Int);
                    arParms[2].Value = objBlockMaster.FloorID;

                    arParms[3] = new SqlParameter("@Room", SqlDbType.VarChar);
                    arParms[3].Value = objBlockMaster.Room;

                    arParms[4] = new SqlParameter("@BedNo", SqlDbType.VarChar);
                    arParms[4].Value = objBlockMaster.BedNo;

                    arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[5].Value = objBlockMaster.EmployeeID;

                    arParms[6] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[6].Value = objBlockMaster.ActionType;

                    arParms[7] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[7].Direction = ParameterDirection.Output;

                    arParms[8] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[8].Value = objBlockMaster.IsActive;

                    arParms[9] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[9].Value = objBlockMaster.HospitalID;

                    arParms[10] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[10].Value = objBlockMaster.FinancialYearID;

                    arParms[11] = new SqlParameter("@Charges", SqlDbType.Money);
                    arParms[11].Value = objBlockMaster.Charges;

                    arParms[12] = new SqlParameter("@WardID", SqlDbType.Int);
                    arParms[12].Value = objBlockMaster.WardID;

                    arParms[13] = new SqlParameter("@NuCharges", SqlDbType.Money);
                    arParms[13].Value = objBlockMaster.NuCharges;

                    arParms[14] = new SqlParameter("@Consumable", SqlDbType.Money);
                    arParms[14].Value = objBlockMaster.Consumable;
                                                      
                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateBedTypeMST", arParms);
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
        public List<BedMasterData> GetBedTypeDetailsByID(BedMasterData objBlockTypeMaster)
        {
            List<BedMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objBlockTypeMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GeTBedTypeDetailsByID", arParms);
                    List<BedMasterData> lstDepartmentTypeDetails = ORHelper<BedMasterData>.FromDataReaderToList(sqlReader);
                    result = lstDepartmentTypeDetails;
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
        public List<BedMasterData> Getautorooms(BedMasterData objroom)
        {
            List<BedMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@Room", SqlDbType.VarChar);
                    arParms[0].Value = objroom.Room;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetAultorooms", arParms);
                    List<BedMasterData> lstDepartmentTypeDetails = ORHelper<BedMasterData>.FromDataReaderToList(sqlReader);
                    result = lstDepartmentTypeDetails;
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
        public List<BedMasterData> Getautobednos(BedMasterData objbed)
        {
            List<BedMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@BedNo", SqlDbType.VarChar);
                    arParms[0].Value = objbed.BedNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetAutobednos", arParms);
                    List<BedMasterData> lstDepartmentTypeDetails = ORHelper<BedMasterData>.FromDataReaderToList(sqlReader);
                    result = lstDepartmentTypeDetails;
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
        public int DeleteBedTypeDetailsByID(BedMasterData objBlockTypeMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objBlockTypeMaster.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objBlockTypeMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objBlockTypeMaster.Remarks;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteBedTypeDetailsByID", arParms);
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
        public List<BedMasterData> SearchBedTypeDetails(BedMasterData objBlockTypeMaster)
        {
            List<BedMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@BlockID", SqlDbType.Int);
                    arParms[0].Value = objBlockTypeMaster.BlockID;

                    arParms[1] = new SqlParameter("@FloorID", SqlDbType.Int);
                    arParms[1].Value = objBlockTypeMaster.FloorID;

                    arParms[2] = new SqlParameter("@WardID", SqlDbType.Int);
                    arParms[2].Value = objBlockTypeMaster.WardID;

                    arParms[3] = new SqlParameter("@Room", SqlDbType.VarChar);
                    arParms[3].Value = objBlockTypeMaster.Room;

                    arParms[4] = new SqlParameter("@BedNo", SqlDbType.VarChar);
                    arParms[4].Value = objBlockTypeMaster.BedNo;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objBlockTypeMaster.IsActive;

                    arParms[6] = new SqlParameter("@BedStatus", SqlDbType.Int);
                    arParms[6].Value = objBlockTypeMaster.BedStatus;

                   
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchBedType", arParms);
                    List<BedMasterData> lstBlockTypeDetails = ORHelper<BedMasterData>.FromDataReaderToList(sqlReader);
                    result = lstBlockTypeDetails;
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
        public List<BedMasterData> SearchBedDetails(BedMasterData objBlockTypeMaster)
        {
            List<BedMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@BlockID", SqlDbType.Int);
                    arParms[0].Value = objBlockTypeMaster.BlockID;

                    arParms[1] = new SqlParameter("@FloorID", SqlDbType.Int);
                    arParms[1].Value = objBlockTypeMaster.FloorID;

                    arParms[2] = new SqlParameter("@WardID", SqlDbType.Int);
                    arParms[2].Value = objBlockTypeMaster.WardID;

                    arParms[3] = new SqlParameter("@Room", SqlDbType.VarChar);
                    arParms[3].Value = objBlockTypeMaster.Room;

                    arParms[4] = new SqlParameter("@BedNo", SqlDbType.VarChar);
                    arParms[4].Value = objBlockTypeMaster.BedNo;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objBlockTypeMaster.IsActive;

                    arParms[6] = new SqlParameter("@BedStatus", SqlDbType.Int);
                    arParms[6].Value = objBlockTypeMaster.BedStatus;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchBedDetails", arParms);
                    List<BedMasterData> lstBlockTypeDetails = ORHelper<BedMasterData>.FromDataReaderToList(sqlReader);
                    result = lstBlockTypeDetails;
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
        public List<BedDasboard> NurseBedDashBoard(Int64 user)
        {
            List<BedDasboard> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@UserId", SqlDbType.BigInt);
                    arParms[0].Value = user;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_nurse_dashboard", arParms);
                    List<BedDasboard> lstBlockTypeDetails = ORHelper<BedDasboard>.FromDataReaderToList(sqlReader);
                    result = lstBlockTypeDetails;
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
