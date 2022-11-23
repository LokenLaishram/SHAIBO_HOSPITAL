using Mediqura.CommonData.MedUtilityData;
using Mediqura.CommonData.MSBData;
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


namespace Mediqura.DAL.MSBDA
{
    public class DocumentUploaderDA
    {
        public List<DocumentUploadData> GetPatientAdmissionDetailsByName(DocumentUploadData objD)
        {
            List<DocumentUploadData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];
                    arParms[0] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientAdmissionDetailsByName", arParms);
                    List<DocumentUploadData> lstresult = ORHelper<DocumentUploadData>.FromDataReaderToList(sqlReader);
                    result = lstresult;
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
        public List<DocumentUploadData> GetUploadList(DocumentUploadData objUpload)
        {
            List<DocumentUploadData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objUpload.EmployeeID;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objUpload.UHID;

                    arParms[2] = new SqlParameter("@Tittle", SqlDbType.VarChar);
                    arParms[2].Value = objUpload.Tittle;

                    //arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    //arParms[3].Value = objUpload.DateFrom;

                    //arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    //arParms[4].Value = objUpload.DateTo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DependentFileList", arParms);
                    List<DocumentUploadData> lstLabGroupTypeDetails = ORHelper<DocumentUploadData>.FromDataReaderToList(sqlReader);
                    result = lstLabGroupTypeDetails;
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

        //public int UpdateOPDLabBill(DocumentUploadData objUploadData)
        //{
        //    int result = 0;
        //    try
        //    {
        //        {
        //            SqlParameter[] arParms = new SqlParameter[10];

        //            arParms[1] = new SqlParameter("@IPNo", SqlDbType.VarChar);
        //            arParms[1].Value = objUploadData.IPNo;

        //            arParms[2] = new SqlParameter("@Name", SqlDbType.VarChar);
        //            arParms[2].Value = objUploadData.PatientName;

        //            arParms[3] = new SqlParameter("@Address", SqlDbType.VarChar);
        //            arParms[3].Value = objUploadData.Address;

        //            arParms[4] = new SqlParameter("@XMLData", SqlDbType.Xml);
        //            arParms[4].Value = objUploadData.XMLData;

        //            arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
        //            arParms[5].Value = objUploadData.EmployeeID;

        //            arParms[6] = new SqlParameter("@HospitalID", SqlDbType.Int);
        //            arParms[6].Value = objUploadData.HospitalID;

        //            arParms[7] = new SqlParameter("@ActionType", SqlDbType.Int);
        //            arParms[7].Value = objUploadData.ActionType;

        //            arParms[8] = new SqlParameter("@Output", SqlDbType.SmallInt);
        //            arParms[8].Direction = ParameterDirection.Output;

        //            arParms[9] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
        //            arParms[9].Value = objUploadData.IPaddress;

        //            int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateFileUpload", arParms);
        //            if (result_ > 0 || result_ == -1)
        //                result = Convert.ToInt32(arParms[8].Value);
        //        }
        //    }
        //    catch (Exception ex) //Exception of the business layer(itself)//unhandle
        //    {
        //        PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
        //        LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
        //        throw new DataAccessException("5000001", ex);
        //    }
        //    return result;
        //}
        public int UpdateEmpFile(DocumentUploadData objUploadData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    //arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    //arParms[1].Value = objUploadData.EmployeeID;



                    arParms[0] = new SqlParameter("@DependentName", SqlDbType.VarChar);
                    arParms[0].Value = objUploadData.DependentName;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objUploadData.UHID;

                    arParms[2] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[2].Value = objUploadData.XMLData;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objUploadData.EmployeeID;

                    arParms[4] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[4].Value = objUploadData.HospitalID;

                    arParms[5] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[5].Value = objUploadData.ActionType;

                    arParms[6] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[6].Direction = ParameterDirection.Output;

                    arParms[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[7].Value = objUploadData.IPaddress;

                    //arParms[8] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    //arParms[8].Value = objUploadData.EmployeeID;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateMSBDocument", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[6].Value);
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
        public int DeleteDepFiledetailByID(DocumentUploadData objOTRolesMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@fileID", SqlDbType.BigInt);
                    arParms[0].Value = objOTRolesMaster.fileID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objOTRolesMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objOTRolesMaster.Remarks;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objOTRolesMaster.IPaddress;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteDepDetailsByID", arParms);
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
