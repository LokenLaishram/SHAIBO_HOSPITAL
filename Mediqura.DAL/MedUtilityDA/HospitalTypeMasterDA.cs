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
    public class HospitalTypeMasterDA
    {
        public int UpdateHospitalTypeDetails(HospitalTypeMasterData objHospitalTypeMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[18];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objHospitalTypeMaster.ID;

                    arParms[1] = new SqlParameter("@HospitalTypeCode", SqlDbType.VarChar);
                    arParms[1].Value = objHospitalTypeMaster.HospitalTypeCode;

                    arParms[2] = new SqlParameter("@HospitalName", SqlDbType.VarChar);
                    arParms[2].Value = objHospitalTypeMaster.HospitalName;

                    arParms[3] = new SqlParameter("@HospitalAddress", SqlDbType.VarChar);
                    arParms[3].Value = objHospitalTypeMaster.HospitalAddress;

                    //arParms[4] = new SqlParameter("@LogoLocationImage", SqlDbType.VarChar);
                    //arParms[4].Value = objHospitalTypeMaster.LogoLocationimage;
                    
                    arParms[4] = new SqlParameter("@CountryID", SqlDbType.Int);
                    arParms[4].Value = objHospitalTypeMaster.CountryID;

                    arParms[5] = new SqlParameter("@StateID", SqlDbType.Int);
                    arParms[5].Value = objHospitalTypeMaster.StateID;

                    arParms[6] = new SqlParameter("@DistrictID", SqlDbType.Int);
                    arParms[6].Value = objHospitalTypeMaster.DistrictID;

                    arParms[7] = new SqlParameter("@PinNo", SqlDbType.Int);
                    arParms[7].Value = objHospitalTypeMaster.PinNo;

                    arParms[8] = new SqlParameter("@Website", SqlDbType.VarChar);
                    arParms[8].Value = objHospitalTypeMaster.Website;

                    arParms[9] = new SqlParameter("@EmailID", SqlDbType.VarChar);
                    arParms[9].Value = objHospitalTypeMaster.EmailID;

                    arParms[10] = new SqlParameter("@RecognitionNo", SqlDbType.VarChar);
                    arParms[10].Value = objHospitalTypeMaster.RecognitionNo;

                    arParms[11] = new SqlParameter("@PhoneNo", SqlDbType.VarChar);
                    arParms[11].Value = objHospitalTypeMaster.PhoneNo;

                    arParms[12] = new SqlParameter("@MobileNo", SqlDbType.VarChar);
                    arParms[12].Value = objHospitalTypeMaster.MobileNo;

                    arParms[13] = new SqlParameter("@FaxNo", SqlDbType.VarChar);
                    arParms[13].Value = objHospitalTypeMaster.FaxNo;

                    arParms[14] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[14].Value = objHospitalTypeMaster.EmployeeID;

                    arParms[15] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[15].Direction = ParameterDirection.Output;

                    arParms[16] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[16].Value = objHospitalTypeMaster.IsActive;

                    arParms[17] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[17].Value = objHospitalTypeMaster.ActionType;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateHospitalTypeMST", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[15].Value);
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
        public List<HospitalTypeMasterData> GetHospitalTypeDetailsByID(HospitalTypeMasterData objHospitalTypeMaster)
        {
            List<HospitalTypeMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objHospitalTypeMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetHospitalDetailsByID", arParms);
                    List<HospitalTypeMasterData> lstHospitalTypeDetails = ORHelper<HospitalTypeMasterData>.FromDataReaderToList(sqlReader);
                    result = lstHospitalTypeDetails;
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
        public int DeleteHospitalTypeDetailsByID(HospitalTypeMasterData objHospitalTypeMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objHospitalTypeMaster.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objHospitalTypeMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objHospitalTypeMaster.Remarks;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteHospitalTypeDetailsByID", arParms);
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
        public List<HospitalTypeMasterData> SearchHospitalTypeDetails(HospitalTypeMasterData objHospitalTypeMaster)
        {
            List<HospitalTypeMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@HospitalTypeCode", SqlDbType.VarChar);
                    arParms[0].Value = objHospitalTypeMaster.HospitalTypeCode;

                    arParms[1] = new SqlParameter("@HospitalName", SqlDbType.VarChar);
                    arParms[1].Value = objHospitalTypeMaster.HospitalName;

                    arParms[2] = new SqlParameter("@HospitalAddress", SqlDbType.VarChar);
                    arParms[2].Value = objHospitalTypeMaster.HospitalAddress;

                    arParms[3] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[3].Value = objHospitalTypeMaster.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchHospitalType", arParms);
                    List<HospitalTypeMasterData> lstHospitalTypeDetails = ORHelper<HospitalTypeMasterData>.FromDataReaderToList(sqlReader);
                    result = lstHospitalTypeDetails;
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
