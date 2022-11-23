using Mediqura.CommonData.AdmissionData;
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

namespace Mediqura.DAL.AdmissionDA
{
    public class BedAssignedDA
    {
        public int UpdateBedAdmissionDetails(BedAssignData objadm)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[18];

                    arParms[0] = new SqlParameter("@IPNO", SqlDbType.VarChar);
                    arParms[0].Value = objadm.IPNo;

                    arParms[1] = new SqlParameter("@DeptID", SqlDbType.Int);
                    arParms[1].Value = objadm.DischargeStatus;

                    arParms[2] = new SqlParameter("@DocID", SqlDbType.Int);
                    arParms[2].Value = objadm.DocID;

                    arParms[3] = new SqlParameter("@AdmissionCharge", SqlDbType.Money);
                    arParms[3].Value = objadm.TotalAdmissionCharge;

                    arParms[4] = new SqlParameter("@Case", SqlDbType.VarChar);
                    arParms[4].Value = objadm.Cases;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[5].Value = objadm.HospitalID;

                    arParms[6] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[6].Value = objadm.EmployeeID;

                    arParms[7] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[7].Direction = ParameterDirection.Output;

                    arParms[8] = new SqlParameter("@ActionType", SqlDbType.SmallInt);
                    arParms[8].Value = objadm.ActionType;

                    arParms[9] = new SqlParameter("@PaymentMode", SqlDbType.Int);
                    arParms[9].Value = objadm.PaymentMode;

                    arParms[10] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[10].Value = objadm.FinancialYearID;

                    arParms[11] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[11].Value = objadm.IsActive;

                    arParms[12] = new SqlParameter("@BlockID", SqlDbType.Int);
                    arParms[12].Value = objadm.BlockID;

                    arParms[13] = new SqlParameter("@FloorID", SqlDbType.Int);
                    arParms[13].Value = objadm.FloorID;

                    arParms[14] = new SqlParameter("@WardID", SqlDbType.Int);
                    arParms[14].Value = objadm.WardID;

                    arParms[15] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[15].Value = objadm.XMLData;

                    arParms[16] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[16].Value = objadm.IPaddress;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_BedDetails", arParms);
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
    }
}
