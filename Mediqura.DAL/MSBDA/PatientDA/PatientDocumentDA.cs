using Mediqura.CommonData.PatientData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.DAL.PatientDA
{
   public class PatientDocumentDA
    {
        public int SavePatientDocuments(PatientCaseHistoryData objpat)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[13];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.UHID;

                    arParms[1] = new SqlParameter("@CaseType", SqlDbType.VarChar);
                    arParms[1].Value = objpat.CaseType;

                    arParms[2] = new SqlParameter("@CaseNo", SqlDbType.VarChar);
                    arParms[2].Value = objpat.CaseNo;

                    arParms[3] = new SqlParameter("@FileName", SqlDbType.VarChar);
                    arParms[3].Value = objpat.FileName;

                    arParms[4] = new SqlParameter("@Extension", SqlDbType.VarChar);
                    arParms[4].Value = objpat.Extension;

                    arParms[5] = new SqlParameter("@ContentType", SqlDbType.VarChar);
                    arParms[5].Value = objpat.ContentType;

                    arParms[6] = new SqlParameter("@FileData", SqlDbType.VarBinary);
                    arParms[6].Value = objpat.FileData;

                    arParms[7] = new SqlParameter("@FileSize", SqlDbType.BigInt);
                    arParms[7].Value = objpat.FileSize;

                    arParms[8] = new SqlParameter("@FileAddedBy", SqlDbType.BigInt);
                    arParms[8].Value = objpat.FileAddedBy;

                    arParms[9] = new SqlParameter("@FileAddedDate", SqlDbType.DateTime);
                    arParms[9].Value = objpat.FileAddedDate;

                    arParms[10] = new SqlParameter("@serviceID", SqlDbType.BigInt);
                    arParms[10].Value = objpat.serviceID;

                    arParms[11] = new SqlParameter("@ServiceName", SqlDbType.VarChar);
                    arParms[11].Value = objpat.ServiceName;

                    arParms[12] = new SqlParameter("@Output", SqlDbType.Int);
                    arParms[12].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_patient_documents", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[8].Value);
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
    }
}
