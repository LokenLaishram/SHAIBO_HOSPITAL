using Mediqura.CommonData.MedPharData;
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

namespace Mediqura.DAL.MedPharDA
{
    public class EmgDrugIssueDA
    {
        public List<EmgDrugIssueData> GetEmgPatientName(EmgDrugIssueData objD)
        {
            List<EmgDrugIssueData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@EmgPatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.EmgPatientName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmgPatientName", arParms);
                    List<EmgDrugIssueData> lstresult = ORHelper<EmgDrugIssueData>.FromDataReaderToList(sqlReader);
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
        public List<EmgDrugIssueData> GetPatientDetailsByEmgNo(EmgDrugIssueData objD)
        {
            List<EmgDrugIssueData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@EmgNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.EmgNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientNameByEmgNo", arParms);
                    List<EmgDrugIssueData> lstresult = ORHelper<EmgDrugIssueData>.FromDataReaderToList(sqlReader);
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
        public List<EmgDrugIssueData> GetDrugName(EmgDrugIssueData objD)
        {
            List<EmgDrugIssueData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objD.ItemName;
                    arParms[1] = new SqlParameter("@MedSubStockID", SqlDbType.Int);
                    arParms[1].Value = objD.MedSubStockID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Med_ItemNameBySubtckID", arParms);
                    List<EmgDrugIssueData> lstresult = ORHelper<EmgDrugIssueData>.FromDataReaderToList(sqlReader);
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
        public List<EmgDrugIssueData> GetDoctorName(EmgDrugIssueData objD)
        {
            List<EmgDrugIssueData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@DoctorName", SqlDbType.VarChar);
                    arParms[0].Value = objD.DoctorName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Medi_GetDoctorName", arParms);
                    List<EmgDrugIssueData> lstresult = ORHelper<EmgDrugIssueData>.FromDataReaderToList(sqlReader);
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
        public List<EmgDrugIssueData> UpdateEmgDrugIssueDetails(EmgDrugIssueData objot)
        {
            List<EmgDrugIssueData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[19];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objot.XMLData;

                    arParms[1] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[1].Value = objot.ID;

                    arParms[2] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[2].Value = objot.UHID;

                    arParms[3] = new SqlParameter("@EmgNo", SqlDbType.VarChar);
                    arParms[3].Value = objot.EmgNo;

                    arParms[4] = new SqlParameter("@IPPatientName", SqlDbType.VarChar);
                    arParms[4].Value = objot.IPPatientName;

                    arParms[5] = new SqlParameter("@WardBedNo", SqlDbType.VarChar);
                    arParms[5].Value = objot.WardBedNo;

                    arParms[6] = new SqlParameter("@MedSubStockID", SqlDbType.Int);
                    arParms[6].Value = objot.MedSubStockID;

                    arParms[7] = new SqlParameter("@TotalMRPperQty", SqlDbType.Money);
                    arParms[7].Value = objot.TotalMRPperQty;

                    arParms[8] = new SqlParameter("@TotalNoUnit", SqlDbType.Money);
                    arParms[8].Value = objot.TotalNoUnit;

                    arParms[9] = new SqlParameter("@TotalEqvQty", SqlDbType.Money);
                    arParms[9].Value = objot.TotalEqvQty;

                    arParms[10] = new SqlParameter("@TotalNetCharge", SqlDbType.Money);
                    arParms[10].Value = objot.TotalNetCharge;                      

                    arParms[11] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[11].Value = objot.DoctorID;

                    arParms[12] = new SqlParameter("@DoctorName", SqlDbType.VarChar);
                    arParms[12].Value = objot.DoctorName;

                    arParms[13] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[13].Value = objot.EmployeeID;

                    arParms[14] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[14].Value = objot.HospitalID;

                    arParms[15] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[15].Value = objot.FinancialYearID;

                    arParms[16] = new SqlParameter("@ActionType", SqlDbType.SmallInt);
                    arParms[16].Value = objot.ActionType;

                    arParms[17] = new SqlParameter("@IPaddress", SqlDbType.VarChar);
                    arParms[17].Value = objot.IPaddress;

                    arParms[18] = new SqlParameter("@DepositAmount", SqlDbType.Money);
                    arParms[18].Value = objot.DepositAmount;

                 
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_EmgDrugRecordDetails", arParms);
                    List<EmgDrugIssueData> listDrug = ORHelper<EmgDrugIssueData>.FromDataReaderToList(sqlReader);
                    result = listDrug;
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
        public List<EmgDrugIssueData> GetEmgDrugRecordList(EmgDrugIssueData objmedi)
        {
            List<EmgDrugIssueData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objmedi.UHID;

                    arParms[1] = new SqlParameter("@EmgNo", SqlDbType.VarChar);
                    arParms[1].Value = objmedi.EmgNo;

                    arParms[2] = new SqlParameter("@SubStockID", SqlDbType.Int);
                    arParms[2].Value = objmedi.ItemID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phar_EmgDrugRecordList", arParms);
                    List<EmgDrugIssueData> listMedidetails = ORHelper<EmgDrugIssueData>.FromDataReaderToList(sqlReader);
                    result = listMedidetails;
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
        ///--------TAB2-------------//
        public List<EmgDrugIssueData> Get_EmgDrugRecordList(EmgDrugIssueData objmedi)
        {
            List<EmgDrugIssueData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objmedi.UHID;

                    arParms[1] = new SqlParameter("@EmgNo", SqlDbType.VarChar);
                    arParms[1].Value = objmedi.EmgNo;

                    arParms[2] = new SqlParameter("@ItemID", SqlDbType.Int);
                    arParms[2].Value = objmedi.ItemID;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[3].Value = objmedi.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[4].Value = objmedi.DateTo;

                    arParms[5] = new SqlParameter("@Status", SqlDbType.Bit);
                    arParms[5].Value = objmedi.Status;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phar_EmgDrugRecordDetailsList", arParms);
                    List<EmgDrugIssueData> listMedidetails = ORHelper<EmgDrugIssueData>.FromDataReaderToList(sqlReader);
                    result = listMedidetails;
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
        public int DeleteEmgPatientDrugRecordByID(EmgDrugIssueData objdata)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objdata.ID;

                    arParms[1] = new SqlParameter("@EmgDrgIssueNo", SqlDbType.VarChar);
                    arParms[1].Value = objdata.EmgDrgIssueNo;

                    arParms[2] = new SqlParameter("@DepositNos", SqlDbType.VarChar);
                    arParms[2].Value = objdata.DepositNos;

                    arParms[3] = new SqlParameter("@DepositAmount", SqlDbType.Money);
                    arParms[3].Value = objdata.DepositAmount;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objdata.EmployeeID;

                    arParms[5] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[5].Value = objdata.Remarks;

                    arParms[6] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[6].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Phar_DeleteEmgDrugRecordByID", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[6].Value);
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
