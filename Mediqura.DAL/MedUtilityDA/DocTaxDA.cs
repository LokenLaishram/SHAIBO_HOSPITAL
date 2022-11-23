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
    public class DocTaxDA
    {
        public int UpdateDocTaxDetails(DocTaxData objOTRolesMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[11];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objOTRolesMaster.ID;

                    arParms[1] = new SqlParameter("@ServiceID", SqlDbType.Int);
                    arParms[1].Value = objOTRolesMaster.ServiceID;

                    arParms[2] = new SqlParameter("@ServiceSubGrpID", SqlDbType.Int);
                    arParms[2].Value = objOTRolesMaster.ServiceSubGrpID;

                    arParms[3] = new SqlParameter("@DoctorID", SqlDbType.Int);
                    arParms[3].Value = objOTRolesMaster.DoctorID;

                    arParms[4] = new SqlParameter("@Tax", SqlDbType.Decimal);
                    arParms[4].Value = objOTRolesMaster.Tax;

                    arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[5].Value = objOTRolesMaster.EmployeeID;

                    arParms[6] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[6].Value = objOTRolesMaster.HospitalID;

                    arParms[7] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[7].Value = objOTRolesMaster.ActionType;

                    arParms[8] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[8].Direction = ParameterDirection.Output;

                    arParms[9] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[9].Value = objOTRolesMaster.IsActive;

                    arParms[10] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[10].Value = objOTRolesMaster.IPaddress;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateDocTaxMST", arParms);
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
        public List<DocTaxData> SearchDocTaxDetails(DocTaxData objOTRolesMaster)
        {
            List<DocTaxData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@ServiceID", SqlDbType.Int);
                    arParms[0].Value = objOTRolesMaster.ServiceID;

                    arParms[1] = new SqlParameter("@ServiceSubGrpID", SqlDbType.Int);
                    arParms[1].Value = objOTRolesMaster.ServiceSubGrpID;

                    arParms[2] = new SqlParameter("@DoctorID", SqlDbType.Int);
                    arParms[2].Value = objOTRolesMaster.DoctorID;

                    arParms[3] = new SqlParameter("@Tax", SqlDbType.Decimal);
                    arParms[3].Value = objOTRolesMaster.Tax;

                    arParms[4] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[4].Value = objOTRolesMaster.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchDocTax", arParms);
                    List<DocTaxData> lstOTRolesDetails = ORHelper<DocTaxData>.FromDataReaderToList(sqlReader);
                    result = lstOTRolesDetails;
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
        public List<DocTaxData> GetDocTaxDetailsByID(DocTaxData objOTRolesMaster)
        {
            List<DocTaxData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objOTRolesMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetDocTaxDetailsByID", arParms);
                    List<DocTaxData> lstLabUnitDetails = ORHelper<DocTaxData>.FromDataReaderToList(sqlReader);
                    result = lstLabUnitDetails;
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
        public int DeleteDocTaxDetailsByID(DocTaxData objOTRolesMaster)
        {
            int result = 0;   
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objOTRolesMaster.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[1].Value = objOTRolesMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objOTRolesMaster.Remarks;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objOTRolesMaster.IPaddress;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteDocTaxDetailsByID", arParms);
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
