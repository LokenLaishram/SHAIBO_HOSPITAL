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
    public class OutsidePatientDA
    {
        public int UpdateOutpatientDetails(OutsidePatientData objreferal)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[12];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objreferal.ID;

                    arParms[1] = new SqlParameter("@Code", SqlDbType.VarChar);
                    arParms[1].Value = objreferal.Code;

                    arParms[2] = new SqlParameter("@Name", SqlDbType.VarChar);
                    arParms[2].Value = objreferal.Name;

                    arParms[3] = new SqlParameter("@ContactNo", SqlDbType.VarChar);
                    arParms[3].Value = objreferal.ContactNo;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objreferal.EmployeeID;

                    arParms[5] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[5].Value = objreferal.ActionType;

                    arParms[6] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[6].Direction = ParameterDirection.Output;

                    arParms[7] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[7].Value = objreferal.IsActive;

                    arParms[8] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[8].Value = objreferal.HospitalID;

                    arParms[9] = new SqlParameter("@FinancialYearID", SqlDbType.Bit);
                    arParms[9].Value = objreferal.FinancialYearID;

                    arParms[10] = new SqlParameter("@Address", SqlDbType.VarChar);
                    arParms[10].Value = objreferal.Address;

                    arParms[11] = new SqlParameter("@IPaddress", SqlDbType.VarChar);
                    arParms[11].Value = objreferal.IPaddress;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateOutpatientMST", arParms);
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

    }
}
