using Mediqura.CommonData.MedMedication;
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

namespace Mediqura.DAL.MedicationDA
{
    public class MedicationDA
    {
        public List<MedicationChartData> GetIPPatientName(MedicationChartData objD)
        {
            List<MedicationChartData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPPatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPPatientName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIPPatientName", arParms);
                    List<MedicationChartData> lstresult = ORHelper<MedicationChartData>.FromDataReaderToList(sqlReader);
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
        public List<MedicationChartData> GetPatientDetailsByIPNO(MedicationChartData objD)
        {
            List<MedicationChartData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientNameByIPNO", arParms);
                    List<MedicationChartData> lstresult = ORHelper<MedicationChartData>.FromDataReaderToList(sqlReader);
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
        public List<MedicationChartData> GetDrugName(MedicationChartData objD)
        {
            List<MedicationChartData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@DrugName", SqlDbType.VarChar);
                    arParms[0].Value = objD.DrugName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDrugName", arParms);
                    List<MedicationChartData> lstresult = ORHelper<MedicationChartData>.FromDataReaderToList(sqlReader);
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
        public List<MedicationChartData> GetDoctorName(MedicationChartData objD)
        {
            List<MedicationChartData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@DoctorName", SqlDbType.VarChar);
                    arParms[0].Value = objD.DoctorName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Medi_GetDoctorName", arParms);
                    List<MedicationChartData> lstresult = ORHelper<MedicationChartData>.FromDataReaderToList(sqlReader);
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
        public int UpdateMedicationChart(MedicationChartData objot)
        {
            int result = 0;       
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[18];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objot.ID;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objot.UHID;

                    arParms[2] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[2].Value = objot.IPNo;

                    arParms[3] = new SqlParameter("@IPPatientName", SqlDbType.VarChar);
                    arParms[3].Value = objot.IPPatientName;

                    arParms[4] = new SqlParameter("@WardBedNo", SqlDbType.VarChar);
                    arParms[4].Value = objot.WardBedNo;

                    arParms[5] = new SqlParameter("@DrugID", SqlDbType.Int);
                    arParms[5].Value = objot.DrugID;

                    arParms[6] = new SqlParameter("@DrugName", SqlDbType.VarChar);
                    arParms[6].Value = objot.DrugName; 

                    arParms[7] = new SqlParameter("@StartDate", SqlDbType.DateTime);
                    arParms[7].Value = objot.StartDate;

                    arParms[8] = new SqlParameter("@Frequency", SqlDbType.VarChar);
                    arParms[8].Value = objot.Frequency;

                    arParms[9] = new SqlParameter("@RouteID", SqlDbType.Int);
                    arParms[9].Value = objot.RouteID;

                    arParms[10] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[10].Value = objot.DoctorID;

                    arParms[11] = new SqlParameter("@DoctorName", SqlDbType.VarChar);
                    arParms[11].Value = objot.DoctorName;

                    arParms[12] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[12].Value = objot.EmployeeID;

                    arParms[13] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[13].Value = objot.HospitalID;

                    arParms[14] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[14].Value = objot.FinancialYearID;

                    arParms[15] = new SqlParameter("@ActionType", SqlDbType.SmallInt);
                    arParms[15].Value = objot.ActionType;

                    arParms[16] = new SqlParameter("@IPaddress", SqlDbType.VarChar);
                    arParms[16].Value = objot.IPaddress;

                    arParms[17] = new SqlParameter("@Output", SqlDbType.BigInt);
                    arParms[17].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_MedicationChart", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[17].Value);
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
        public List<MedicationChartData> GetMedicationList(MedicationChartData objmedi)
        {
            List<MedicationChartData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objmedi.UHID;

                    arParms[1] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[1].Value = objmedi.IPNo;

                    arParms[2] = new SqlParameter("@DrugID", SqlDbType.Int);
                    arParms[2].Value = objmedi.DrugID;
                                                                             
                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[3].Value = objmedi.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[4].Value = objmedi.DateTo;

                    arParms[5] = new SqlParameter("@Status", SqlDbType.Int);
                    arParms[5].Value = objmedi.Status;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_MedicationListDetails", arParms);
                    List<MedicationChartData> listMedidetails = ORHelper<MedicationChartData>.FromDataReaderToList(sqlReader);
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
        public List<MedicationChartData> GetIPPatientDrugEntryByID(MedicationChartData objDrgE)
        {
            List<MedicationChartData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objDrgE.ID;

                    arParms[1] = new SqlParameter("@MedCNo", SqlDbType.VarChar);
                    arParms[1].Value = objDrgE.MedCNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Medi_GetIPPatientDrugEntryByID", arParms);
                    List<MedicationChartData> lstLabUnitDetails = ORHelper<MedicationChartData>.FromDataReaderToList(sqlReader);
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
        public int DeleteIPPatientDrugEntryByID(MedicationChartData objdata)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objdata.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objdata.EmployeeID;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objdata.Remarks;

                    arParms[3] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[3].Value = objdata.IPaddress;

                    arParms[4] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[4].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Medi_DeleteIPPatientDrugByID", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[4].Value);
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
        //----TAB2-------
        public List<MedicationChartData> GetMedicationDetailsList(MedicationChartData objmedi)
        {
            List<MedicationChartData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@MedCNo", SqlDbType.VarChar);
                    arParms[0].Value = objmedi.MedCNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_MedicationChartDetails", arParms);
                    List<MedicationChartData> listcondemndetails = ORHelper<MedicationChartData>.FromDataReaderToList(sqlReader);
                    result = listcondemndetails;
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
        public int UpdateDrugMedication(MedicationChartData objdrg)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[17];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objdrg.ID;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objdrg.UHID;

                    arParms[2] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[2].Value = objdrg.IPNo;

                    arParms[3] = new SqlParameter("@IPPatientName", SqlDbType.VarChar);
                    arParms[3].Value = objdrg.IPPatientName;

                    arParms[4] = new SqlParameter("@WardBedNo", SqlDbType.VarChar);
                    arParms[4].Value = objdrg.WardBedNo;

                    arParms[5] = new SqlParameter("@DrugID", SqlDbType.Int);
                    arParms[5].Value = objdrg.DrugID;

                    arParms[6] = new SqlParameter("@DrugName", SqlDbType.VarChar);
                    arParms[6].Value = objdrg.DrugName;

                    arParms[7] = new SqlParameter("@MedicationDate", SqlDbType.DateTime);
                    arParms[7].Value = objdrg.MedicationDate;

                    arParms[8] = new SqlParameter("@MedicationTime", SqlDbType.VarChar);
                    arParms[8].Value = objdrg.MedicationTime;

                    arParms[9] = new SqlParameter("@RouteID", SqlDbType.Int);
                    arParms[9].Value = objdrg.RouteID;

                    arParms[10] = new SqlParameter("@MedicationNo", SqlDbType.VarChar);
                    arParms[10].Value = objdrg.MedicationNo;

                    arParms[11] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[11].Value = objdrg.EmployeeID;

                    arParms[12] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[12].Value = objdrg.HospitalID;

                    arParms[13] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[13].Value = objdrg.FinancialYearID;

                    arParms[14] = new SqlParameter("@ActionType", SqlDbType.SmallInt);
                    arParms[14].Value = objdrg.ActionType;

                    arParms[15] = new SqlParameter("@IPaddress", SqlDbType.VarChar);
                    arParms[15].Value = objdrg.IPaddress;

                    arParms[16] = new SqlParameter("@Output", SqlDbType.BigInt);
                    arParms[16].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_DrugMedication", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[16].Value);
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
        public List<MedicationChartData> GetDrugMedicationList(MedicationChartData objmedi)
        {
            List<MedicationChartData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objmedi.UHID;

                    arParms[1] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[1].Value = objmedi.IPNo;

                    arParms[2] = new SqlParameter("@DrugID", SqlDbType.Int);
                    arParms[2].Value = objmedi.DrugID;

                    arParms[3] = new SqlParameter("@MonthID", SqlDbType.Int);
                    arParms[3].Value = objmedi.MonthID;

                    arParms[4] = new SqlParameter("@Status", SqlDbType.Int);
                    arParms[4].Value = objmedi.Status;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DrugMedicationList", arParms);
                    List<MedicationChartData> listMedidetails = ORHelper<MedicationChartData>.FromDataReaderToList(sqlReader);
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
        public List<MedicationChartData> GetDrugMedicationEntryByID(MedicationChartData objDrgE)
        {
            List<MedicationChartData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objDrgE.ID;

                    arParms[1] = new SqlParameter("@MedicationNo", SqlDbType.VarChar);
                    arParms[1].Value = objDrgE.MedicationNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Medi_GetDrugMedicationEntryByID", arParms);
                    List<MedicationChartData> lstLabUnitDetails = ORHelper<MedicationChartData>.FromDataReaderToList(sqlReader);
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
        public int DeleteDrugMedicationEntryByID(MedicationChartData objdata)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objdata.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objdata.EmployeeID;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objdata.Remarks;

                    arParms[3] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[3].Value = objdata.IPaddress;

                    arParms[4] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[4].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Medi_DeleteDrugMedicationByID", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[4].Value);
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
