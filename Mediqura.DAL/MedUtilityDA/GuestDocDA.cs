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
    public class GuestDocDA
    {
        public int UpdateGuestDocDetails(GuestDocData objGuest)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[14];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objGuest.ID;

                    arParms[1] = new SqlParameter("@Code", SqlDbType.VarChar);
                    arParms[1].Value = objGuest.Code;

                    arParms[2] = new SqlParameter("@Name", SqlDbType.VarChar);
                    arParms[2].Value = objGuest.Name;

                    arParms[3] = new SqlParameter("@ContactNo", SqlDbType.VarChar);
                    arParms[3].Value = objGuest.ContactNo;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objGuest.EmployeeID;

                    arParms[5] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[5].Value = objGuest.ActionType;

                    arParms[6] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[6].Direction = ParameterDirection.Output;

                    arParms[7] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[7].Value = objGuest.IsActive;

                    arParms[8] = new SqlParameter("@HospitalID", SqlDbType.Bit);
                    arParms[8].Value = objGuest.HospitalID;

                    arParms[9] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[9].Value = objGuest.FinancialYearID;

                    arParms[10] = new SqlParameter("@Address", SqlDbType.VarChar);
                    arParms[10].Value = objGuest.Address;

                    arParms[11] = new SqlParameter("@IPaddress", SqlDbType.VarChar);
                    arParms[11].Value = objGuest.IPaddress;

                    arParms[12] = new SqlParameter("@DeptID", SqlDbType.Int);
                    arParms[12].Value = objGuest.DeptID;

                    arParms[13] = new SqlParameter("@Deptname", SqlDbType.VarChar);
                    arParms[13].Value = objGuest.Deptname;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateGuestDoctorMST", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[5].Value);
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
        public List<GuestDocData> GetGuestDocDetailsByID(GuestDocData objGuest)
        {
            List<GuestDocData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objGuest.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GeTGuestDocDetailsByID", arParms);
                    List<GuestDocData> lstDepartmentTypeDetails = ORHelper<GuestDocData>.FromDataReaderToList(sqlReader);
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
        public int DeleteGuestDocDetailsByID(GuestDocData objGuest)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objGuest.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[1].Value = objGuest.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objGuest.Remarks;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteGuestDocByID", arParms);
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
        public List<GuestDocData> SearchGuestDocDetails(GuestDocData objGuest)
        {
            List<GuestDocData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@Code", SqlDbType.NVarChar);
                    arParms[0].Value = objGuest.Code;

                    arParms[1] = new SqlParameter("@Name", SqlDbType.NVarChar);
                    arParms[1].Value = objGuest.Name;

                    arParms[2] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[2].Value = objGuest.IsActive;

                    arParms[3] = new SqlParameter("@DeptID", SqlDbType.Int);
                    arParms[3].Value = objGuest.DeptID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchGuestDoctor", arParms);
                    List<GuestDocData> lstBlockTypeDetails = ORHelper<GuestDocData>.FromDataReaderToList(sqlReader);
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
