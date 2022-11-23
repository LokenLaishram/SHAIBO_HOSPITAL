using Mediqura.CommonData.MedLab;
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

namespace Mediqura.DAL.MedLab
{
    public class LabReportCollectionDA
    {
        public List<LadReportCollectionData> GetTestPatientList(LadReportCollectionData objTestPatient)
        {
            List<LadReportCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@InVnumber", SqlDbType.VarChar);
                    arParms[0].Value = objTestPatient.InVnumber;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objTestPatient.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[2].Value = objTestPatient.DateTo;

                    arParms[3] = new SqlParameter("@UHID", SqlDbType.VarChar);
                    arParms[3].Value = objTestPatient.UHID;

                    arParms[4] = new SqlParameter("@TestStatus", SqlDbType.Int);
                    arParms[4].Value = objTestPatient.TestStatus;

                    arParms[5] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[5].Value = objTestPatient.CurrentIndex;

                    arParms[6] = new SqlParameter("@pageSize", SqlDbType.Int);
                    arParms[6].Value = objTestPatient.PageSize;

                    arParms[7] = new SqlParameter("@RunnerID", SqlDbType.Int);
                    arParms[7].Value = objTestPatient.RunnerID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabReportPatientList ", arParms);
                    List<LadReportCollectionData> lstresult = ORHelper<LadReportCollectionData>.FromDataReaderToList(sqlReader);
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

        public List<LadReportCollectionData> GetPatientTestList(LadReportCollectionData objpatient)
        {
            List<LadReportCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objpatient.UHID;

                    arParms[1] = new SqlParameter("@InVnumber", SqlDbType.VarChar);
                    arParms[1].Value = objpatient.InVnumber;

                    arParms[2] = new SqlParameter("@DeliveryStatus", SqlDbType.Int);
                    arParms[2].Value = objpatient.DeliveryStatus;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_Get_LAB_Report_collection_Patient_list", arParms);
                    List<LadReportCollectionData> lstpatientData = ORHelper<LadReportCollectionData>.FromDataReaderToList(sqlReader);
                    result = lstpatientData;
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
        public List<LadReportCollectionData> GetInvestigationPatientList(LadReportCollectionData objpatient)
        {
            List<LadReportCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@IpNo", SqlDbType.VarChar);
                    arParms[0].Value = objpatient.IpNo;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objpatient.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[2].Value = objpatient.DateTo;

                    arParms[3] = new SqlParameter("@InvNumber", SqlDbType.VarChar);
                    arParms[3].Value = objpatient.InVnumber;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_Get_Report_collection_Patient_list", arParms);
                    List<LadReportCollectionData> lstpatientData = ORHelper<LadReportCollectionData>.FromDataReaderToList(sqlReader);
                    result = lstpatientData;
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
        public List<LadReportCollectionData> GetRadioReportTemplateByTestId(LadReportCollectionData objRadioReportMaster)
        {
            List<LadReportCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objRadioReportMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_RadioReport_for_patient_BY_ID", arParms);
                    List<LadReportCollectionData> ListTemplateData = ORHelper<LadReportCollectionData>.FromDataReaderToList(sqlReader);
                    result = ListTemplateData;
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
        public int UpdateReportPrintStatus(LadReportCollectionData objRadioReportMaster)
        {
            int result = 0;
            try
            {
                SqlParameter[] arParms = new SqlParameter[5];

                arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                arParms[0].Value = objRadioReportMaster.ID;

                arParms[1] = new SqlParameter("@verifionType", SqlDbType.Int);
                arParms[1].Value = objRadioReportMaster.DeliveryStatus;

                arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                arParms[2].Direction = ParameterDirection.Output;

                arParms[3] = new SqlParameter("@BillID", SqlDbType.BigInt);
                arParms[3].Value = objRadioReportMaster.billID;

                arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                arParms[4].Value = objRadioReportMaster.EmployeeID;


                int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UPDATE_ReportPrintStatus_for_patient", arParms);
                if (result_ > 0 || result_ == -1)
                    result = Convert.ToInt32(arParms[2].Value);



            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public int UpdateEmaildeliveryStatus(LadReportCollectionData objRadioReportMaster)
        {
            int result = 0;
            try
            {
                SqlParameter[] arParms = new SqlParameter[4];

                arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                arParms[0].Value = objRadioReportMaster.UHID;

                arParms[1] = new SqlParameter("@InVnumber", SqlDbType.VarChar);
                arParms[1].Value = objRadioReportMaster.InVnumber;

                arParms[2] = new SqlParameter("@Testids", SqlDbType.VarChar);
                arParms[2].Value = objRadioReportMaster.Testids;

                arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                arParms[3].Direction = ParameterDirection.Output;

                int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_emaildelivery", arParms);
                if (result_ > 0 || result_ == -1)
                {
                    result = Convert.ToInt32(arParms[3].Value);
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
        public List<LadReportCollectionData> GetLabTestList(LadReportCollectionData objpatient)
        {
            List<LadReportCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@patienttype", SqlDbType.Int);
                    arParms[0].Value = objpatient.PatientType;

                    arParms[1] = new SqlParameter("@LabGroupType", SqlDbType.Int);
                    arParms[1].Value = objpatient.LabGrpID;

                    arParms[2] = new SqlParameter("@LabSubGroupType", SqlDbType.Int);
                    arParms[2].Value = objpatient.LabSubGrpID;

                    arParms[3] = new SqlParameter("@TestID", SqlDbType.Int);
                    arParms[3].Value = objpatient.TestID;

                    arParms[4] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[4].Value = objpatient.DateFrom;

                    arParms[5] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[5].Value = objpatient.DateTo;


                    SqlDataReader sqlReader = null;
                    //sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_Get_LAB_Report_collection_Patient_listReport", arParms);
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_Get_LAB_Patient_listReport", arParms);
                    List<LadReportCollectionData> lstpatientData = ORHelper<LadReportCollectionData>.FromDataReaderToList(sqlReader);
                    result = lstpatientData;
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
        public List<LadReportCollectionData> GetPatientListReport(LadReportCollectionData objpatient)
        {
            List<LadReportCollectionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];


                    arParms[0] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[0].Value = objpatient.DateFrom;

                    arParms[1] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[1].Value = objpatient.DateTo;

                    arParms[2] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[2].Value = objpatient.CurrentIndex;

                    arParms[3] = new SqlParameter("@InvNumber", SqlDbType.VarChar);
                    arParms[3].Value = objpatient.InVnumber;

                    arParms[4] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[4].Value = objpatient.UHID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_Get_LAB_Report_collection_PatientWise", arParms);
                    List<LadReportCollectionData> lstpatientData = ORHelper<LadReportCollectionData>.FromDataReaderToList(sqlReader);
                    result = lstpatientData;
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
